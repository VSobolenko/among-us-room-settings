using System.Runtime.CompilerServices;
using System.Text;

namespace AmongUsRoomSettings.Hazel;

internal class MessageReader : IRecyclable
{
    public static readonly ObjectPool<MessageReader> ReaderPool = new ObjectPool<MessageReader>(() => new MessageReader());

    public byte[] Buffer;
    public byte Tag;

    public int Length;
    public int Offset;

    public int BytesRemaining => Length - Position;

    private MessageReader Parent;

    public int Position
    {
        get { return _position; }
        set
        {
            _position = value;
            readHead = value + Offset;
        }
    }

    private int _position;
    private int readHead;

    public static MessageReader GetSized(int minSize)
    {
        var output = ReaderPool.GetObject();

        if (output.Buffer == null || output.Buffer.Length < minSize)
        {
            output.Buffer = new byte[minSize];
        }
        else
        {
            Array.Clear(output.Buffer, 0, output.Buffer.Length);
        }

        output.Offset = 0;
        output.Position = 0;
        output.Tag = byte.MaxValue;
        return output;
    }

    public static MessageReader Get(byte[] buffer)
    {
        var output = ReaderPool.GetObject();

        output.Buffer = buffer;
        output.Offset = 0;
        output.Position = 0;
        output.Length = buffer.Length;
        output.Tag = byte.MaxValue;

        return output;
    }

    public static MessageReader CopyMessageIntoParent(MessageReader source)
    {
        var output = GetSized(source.Length + 3);
        System.Buffer.BlockCopy(source.Buffer, source.Offset - 3, output.Buffer, 0, source.Length + 3);

        output.Offset = 0;
        output.Position = 0;
        output.Length = source.Length + 3;

        return output;
    }

    public static MessageReader Get(MessageReader source)
    {
        var output = GetSized(source.Buffer.Length);
        System.Buffer.BlockCopy(source.Buffer, 0, output.Buffer, 0, source.Buffer.Length);

        output.Offset = source.Offset;

        output._position = source._position;
        output.readHead = source.readHead;

        output.Length = source.Length;
        output.Tag = source.Tag;

        return output;
    }

    public static MessageReader Get(byte[] buffer, int offset)
    {
        // Ensure there is at least a header
        if (offset + 3 > buffer.Length) return null;

        var output = ReaderPool.GetObject();

        output.Buffer = buffer;
        output.Offset = offset;
        output.Position = 0;

        output.Length = output.ReadUInt16();
        output.Tag = output.ReadByte();

        output.Offset += 3;
        output.Position = 0;

        return output;
    }

    /// <summary>
    /// Produces a MessageReader using the parent's buffer. This MessageReader should **NOT** be recycled.
    /// </summary>
    public MessageReader ReadMessage()
    {
        // Ensure there is at least a header
        if (BytesRemaining < 3) throw new InvalidDataException($"ReadMessage header is longer than message length: 3 of {BytesRemaining}");

        var output = new MessageReader();

        output.Parent = this;
        output.Buffer = Buffer;
        output.Offset = readHead;
        output.Position = 0;

        output.Length = output.ReadUInt16();
        output.Tag = output.ReadByte();

        output.Offset += 3;
        output.Position = 0;

        if (BytesRemaining < output.Length + 3) throw new InvalidDataException($"Message Length at Position {readHead} is longer than message length: {output.Length + 3} of {BytesRemaining}");

        Position += output.Length + 3;
        return output;
    }

    public MessageReader ReadMessageAsNewBuffer()
    {
        if (BytesRemaining < 3) throw new InvalidDataException($"ReadMessage header is longer than message length: 3 of {BytesRemaining}");

        var len = ReadUInt16();
        var tag = ReadByte();

        if (BytesRemaining < len) throw new InvalidDataException($"Message Length at Position {readHead} is longer than message length: {len} of {BytesRemaining}");

        var output = GetSized(len);

        Array.Copy(Buffer, readHead, output.Buffer, 0, len);

        output.Length = len;
        output.Tag = tag;

        Position += output.Length;
        return output;
    }

    public MessageWriter StartWriter()
    {
        var output = new MessageWriter(Buffer);
        output.Position = readHead;
        return output;
    }

    public MessageReader Duplicate()
    {
        var output = GetSized(Length);
        Array.Copy(Buffer, Offset, output.Buffer, 0, Length);
        output.Length = Length;
        output.Offset = 0;
        output.Position = 0;

        return output;
    }

    public void RemoveMessage(MessageReader reader)
    {
        var temp = GetSized(reader.Buffer.Length);
        try
        {
            var headerOffset = reader.Offset - 3;
            var endOfMessage = reader.Offset + reader.Length;
            var len = reader.Buffer.Length - endOfMessage;

            Array.Copy(reader.Buffer, endOfMessage, temp.Buffer, 0, len);
            Array.Copy(temp.Buffer, 0, Buffer, headerOffset, len);

            AdjustLength(reader.Offset, reader.Length + 3);
        }
        finally
        {
            temp.Recycle();
        }
    }

    public void InsertMessage(MessageReader reader, MessageWriter writer)
    {
        var temp = GetSized(reader.Buffer.Length);
        try 
        {
            var headerOffset = reader.Offset - 3;
            var startOfMessage = reader.Offset;
            var len = reader.Buffer.Length - startOfMessage;
            int writerOffset = 3;
            switch (writer.SendOption)
            {
                case SendOption.Reliable:
                    writerOffset = 3;
                    break;
                case SendOption.None:
                    writerOffset = 1;
                    break;
            }
                
            //store the original buffer in temp
            Array.Copy(reader.Buffer, headerOffset, temp.Buffer, 0, len);

            //put the contents of writer in at headerOffset
            Array.Copy(writer.Buffer, writerOffset, Buffer, headerOffset, writer.Length-writerOffset);

            //put the original buffer in after that
            Array.Copy(temp.Buffer, 0, Buffer, headerOffset + (writer.Length-writerOffset), len - writer.Length);

            AdjustLength(-1 * reader.Offset , -1 * (writer.Length - writerOffset));
        }
        finally
        {
            temp.Recycle();
        }
    }

    private void AdjustLength(int offset, int amount)
    {
        if (readHead > offset)
        {
            Position -= amount;
        }

        Length -= amount;

        if (Parent == null)
        {
            // If there's no parent reference, we're at the top-most message
            // and this is not a normal Message, as it either contains no data, or it contains
            // a network reliability header
            return;
        }

        var lengthOffset = Offset - 3;
        var curLen = Buffer[lengthOffset]
                     | (Buffer[lengthOffset + 1] << 8);

        curLen -= amount;

        Buffer[lengthOffset] = (byte)curLen;
        Buffer[lengthOffset + 1] = (byte)(Buffer[lengthOffset + 1] >> 8);

        Parent.AdjustLength(offset, amount);
    }

    public void Recycle()
    {
        Parent = null;
        ReaderPool.PutObject(this);
    }

    #region Read Methods
    public bool ReadBoolean()
    {
        byte val = FastByte();
        return val != 0;
    }

    public sbyte ReadSByte()
    {
        return (sbyte)FastByte();
    }

    public byte ReadByte()
    {
        return FastByte();
    }

    public ushort ReadUInt16()
    {
        ushort output =
            (ushort)(FastByte()
                     | FastByte() << 8);
        return output;
    }

    public short ReadInt16()
    {
        short output =
            (short)(FastByte()
                    | FastByte() << 8);
        return output;
    }

    public uint ReadUInt32()
    {
        uint output = FastByte()
                      | (uint)FastByte() << 8
                      | (uint)FastByte() << 16
                      | (uint)FastByte() << 24;

        return output;
    }

    public int ReadInt32()
    {
        int output = FastByte()
                     | FastByte() << 8
                     | FastByte() << 16
                     | FastByte() << 24;

        return output;
    }

    public ulong ReadUInt64()
    {
        ulong output = (ulong)FastByte()
                       | (ulong)FastByte() << 8
                       | (ulong)FastByte() << 16
                       | (ulong)FastByte() << 24
                       | (ulong)FastByte() << 32
                       | (ulong)FastByte() << 40
                       | (ulong)FastByte() << 48
                       | (ulong)FastByte() << 56;

        return output;
    }

    public long ReadInt64()
    {
        long output = (long)FastByte()
                      | (long)FastByte() << 8
                      | (long)FastByte() << 16
                      | (long)FastByte() << 24
                      | (long)FastByte() << 32
                      | (long)FastByte() << 40
                      | (long)FastByte() << 48
                      | (long)FastByte() << 56;

        return output;
    }

    public unsafe float ReadSingle()
    {
        float output = 0;
        fixed (byte* bufPtr = &Buffer[readHead])
        {
            byte* outPtr = (byte*)&output;

            *outPtr = *bufPtr;
            *(outPtr + 1) = *(bufPtr + 1);
            *(outPtr + 2) = *(bufPtr + 2);
            *(outPtr + 3) = *(bufPtr + 3);
        }

        Position += 4;
        return output;
    }

    public string ReadString()
    {
        int len = ReadPackedInt32();
        if (BytesRemaining < len) throw new InvalidDataException($"Read length is longer than message length: {len} of {BytesRemaining}");

        string output = UTF8Encoding.UTF8.GetString(Buffer, readHead, len);

        Position += len;
        return output;
    }


    public int ReadBytesAndSize(byte[] destination)
    {
        int length = ReadPackedInt32();
        if (BytesRemaining < length)
        {
            throw new InvalidDataException($"Read length is longer than message length: {length} of {BytesRemaining}");
        }

        if (destination.Length < length)
        {
            throw new OverflowException($"Read length is longer than buffer length: {length} > {destination.Length}");
        }

        ReadBytes(length, destination);
        return length;
    }

    public byte[] ReadBytesAndSize()
    {
        int len = ReadPackedInt32();
        if (BytesRemaining < len) throw new InvalidDataException($"Read length is longer than message length: {len} of {BytesRemaining}");

        return ReadBytes(len);
    }

    public byte[] ReadBytes(int length)
    {
        if (BytesRemaining < length) throw new InvalidDataException($"Read length is longer than message length: {length} of {BytesRemaining}");

        byte[] output = new byte[length];
        ReadBytes(length, output);
        return output;
    }

    public void ReadBytes(int length, byte[] destination)
    {
        if (BytesRemaining < length) throw new InvalidDataException($"Read length is longer than message length: {length} of {BytesRemaining}");

        Array.Copy(Buffer, readHead, destination, 0, length);
        Position += length;
    }

    ///
    public int ReadPackedInt32()
    {
        return (int)ReadPackedUInt32();
    }

    ///
    public uint ReadPackedUInt32()
    {
        bool readMore = true;
        int shift = 0;
        uint output = 0;

        while (readMore)
        {
            if (BytesRemaining < 1) throw new InvalidDataException($"Read length is longer than message length.");

            byte b = ReadByte();
            if (b >= 0x80)
            {
                readMore = true;
                b ^= 0x80;
            }
            else
            {
                readMore = false;
            }

            output |= (uint)(b << shift);
            shift += 7;
        }

        return output;
    }
    #endregion

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private byte FastByte()
    {
        _position++;
        return Buffer[readHead++];
    }

    public unsafe static bool IsLittleEndian()
    {
        byte b;
        unsafe
        {
            int i = 1;
            byte* bp = (byte*)&i;
            b = *bp;
        }

        return b == 1;
    }
}