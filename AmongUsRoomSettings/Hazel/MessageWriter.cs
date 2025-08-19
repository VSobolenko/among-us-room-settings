using System.Text;

namespace AmongUsRoomSettings.Hazel;

internal class MessageWriter : IRecyclable
{
    public static readonly int BufferSize = 64000;
    public static readonly ObjectPool<MessageWriter> WriterPool = new ObjectPool<MessageWriter>(() => new MessageWriter(BufferSize));

    public readonly byte[] Buffer;
    public int Length;
    public int Position;

    public SendOption SendOption { get; private set; }

    private readonly Stack<int> _messageStarts = new Stack<int>();
        
    public MessageWriter(byte[] buffer)
    {
        Buffer = buffer;
        Length = Buffer.Length;
    }

    public MessageWriter(int bufferSize)
    {
        Buffer = new byte[bufferSize];
    }

    public byte[] ToByteArray(bool includeHeader)
    {
        if (includeHeader)
        {
            byte[] output = new byte[Length];
            System.Buffer.BlockCopy(Buffer, 0, output, 0, Length);
            return output;
        }
        else
        {
            switch (SendOption)
            {
                case SendOption.Reliable:
                {
                    byte[] output = new byte[Length - 3];
                    System.Buffer.BlockCopy(Buffer, 3, output, 0, Length - 3);
                    return output;
                }
                case SendOption.None:
                {
                    byte[] output = new byte[Length - 1];
                    System.Buffer.BlockCopy(Buffer, 1, output, 0, Length - 1);
                    return output;
                }
            }
        }

        throw new NotImplementedException();
    }

    ///
    /// <param name="sendOption">The option specifying how the message should be sent.</param>
    public static MessageWriter Get(SendOption sendOption = SendOption.None)
    {
        var output = WriterPool.GetObject();
        output.Clear(sendOption);

        return output;
    }

    public bool HasBytes(int expected)
    {
        if (SendOption == SendOption.None)
        {
            return Length > 1 + expected;
        }

        return Length > 3 + expected;
    }

    ///
    public void StartMessage(byte typeFlag)
    {
        var messageStart = Position;
        _messageStarts.Push(messageStart);
        Buffer[messageStart] = 0;
        Buffer[messageStart + 1] = 0;
        Position += 2;
        Write(typeFlag);
    }

    ///
    public void EndMessage()
    {
        var lastMessageStart = _messageStarts.Pop();
        ushort length = (ushort)(Position - lastMessageStart - 3); // Minus length and type byte
        Buffer[lastMessageStart] = (byte)length;
        Buffer[lastMessageStart + 1] = (byte)(length >> 8);
    }

    public void CancelMessage()
    {
        Position = _messageStarts.Pop();
        Length = Position;
    }

    public void Clear(SendOption sendOption)
    {
        Array.Clear(Buffer, 0, Buffer.Length);
        _messageStarts.Clear();
        SendOption = sendOption;
        Buffer[0] = (byte)sendOption;
        switch (sendOption)
        {
            default:
            case SendOption.None:
                Length = Position = 1;
                break;
            case SendOption.Reliable:
                Length = Position = 3;
                break;
        }
    }

    ///
    public void Recycle()
    {
        Position = Length = 0;
        WriterPool.PutObject(this);
    }

    #region WriteMethods

    public void CopyFrom(MessageReader target)
    {
        int offset, length;
        if (target.Tag == byte.MaxValue)
        {
            offset = target.Offset;
            length = target.Length;
        }
        else
        {
            offset = target.Offset - 3;
            length = target.Length + 3;
        }

        System.Buffer.BlockCopy(target.Buffer, offset, Buffer, Position, length);
        Position += length;
        if (Position > Length) Length = Position;
    }

    public void Write(bool value)
    {
        Buffer[Position++] = (byte)(value ? 1 : 0);
        if (Position > Length) Length = Position;
    }

    public void Write(sbyte value)
    {
        Buffer[Position++] = (byte)value;
        if (Position > Length) Length = Position;
    }

    public void Write(byte value)
    {
        Buffer[Position++] = value;
        if (Position > Length) Length = Position;
    }

    public void Write(short value)
    {
        Buffer[Position++] = (byte)value;
        Buffer[Position++] = (byte)(value >> 8);
        if (Position > Length) Length = Position;
    }

    public void Write(ushort value)
    {
        Buffer[Position++] = (byte)value;
        Buffer[Position++] = (byte)(value >> 8);
        if (Position > Length) Length = Position;
    }

    public void Write(uint value)
    {
        Buffer[Position++] = (byte)value;
        Buffer[Position++] = (byte)(value >> 8);
        Buffer[Position++] = (byte)(value >> 16);
        Buffer[Position++] = (byte)(value >> 24);
        if (Position > Length) Length = Position;
    }

    public void Write(int value)
    {
        Buffer[Position++] = (byte)value;
        Buffer[Position++] = (byte)(value >> 8);
        Buffer[Position++] = (byte)(value >> 16);
        Buffer[Position++] = (byte)(value >> 24);
        if (Position > Length) Length = Position;
    }

    public void Write(ulong value)
    {
        Buffer[Position++] = (byte)value;
        Buffer[Position++] = (byte)(value >> 8);
        Buffer[Position++] = (byte)(value >> 16);
        Buffer[Position++] = (byte)(value >> 24);
        Buffer[Position++] = (byte)(value >> 32);
        Buffer[Position++] = (byte)(value >> 40);
        Buffer[Position++] = (byte)(value >> 48);
        Buffer[Position++] = (byte)(value >> 56);
        if (Position > Length) Length = Position;
    }

    public void Write(long value)
    {
        Buffer[Position++] = (byte)value;
        Buffer[Position++] = (byte)(value >> 8);
        Buffer[Position++] = (byte)(value >> 16);
        Buffer[Position++] = (byte)(value >> 24);
        Buffer[Position++] = (byte)(value >> 32);
        Buffer[Position++] = (byte)(value >> 40);
        Buffer[Position++] = (byte)(value >> 48);
        Buffer[Position++] = (byte)(value >> 56);
        if (Position > Length) Length = Position;
    }

    public unsafe void Write(float value)
    {
        fixed (byte* ptr = &Buffer[Position])
        {
            byte* valuePtr = (byte*)&value;

            *ptr = *valuePtr;
            *(ptr + 1) = *(valuePtr + 1);
            *(ptr + 2) = *(valuePtr + 2);
            *(ptr + 3) = *(valuePtr + 3);
        }

        Position += 4;
        if (Position > Length) Length = Position;
    }

    public void Write(string value)
    {
        var bytes = Encoding.UTF8.GetBytes(value);
        WritePacked(bytes.Length);
        Write(bytes);
    }

    public void WriteBytesAndSize(byte[] bytes)
    {
        WritePacked((uint)bytes.Length);
        Write(bytes);
    }

    public void WriteBytesAndSize(byte[] bytes, int length)
    {
        WritePacked((uint)length);
        Write(bytes, length);
    }

    public void WriteBytesAndSize(byte[] bytes, int offset, int length)
    {
        WritePacked((uint)length);
        Write(bytes, offset, length);
    }

    public void Write(byte[] bytes)
    {
        Array.Copy(bytes, 0, Buffer, Position, bytes.Length);
        Position += bytes.Length;
        if (Position > Length) Length = Position;
    }

    public void Write(byte[] bytes, int offset, int length)
    {
        Array.Copy(bytes, offset, Buffer, Position, length);
        Position += length;
        if (Position > Length) Length = Position;
    }

    public void Write(byte[] bytes, int length)
    {
        Array.Copy(bytes, 0, Buffer, Position, length);
        Position += length;
        if (Position > Length) Length = Position;
    }

    public void WritePacked(int value)
    {
        WritePacked((uint)value);
    }

    public void WritePacked(uint value)
    {
        do
        {
            byte b = (byte)(value & 0xFF);
            if (value >= 0x80)
            {
                b |= 0x80;
            }

            Write(b);
            value >>= 7;
        } while (value > 0);
    }
    #endregion

    public void Write(MessageWriter msg, bool includeHeader)
    {
        int offset = 0;
        if (!includeHeader)
        {
            switch (msg.SendOption)
            {
                case SendOption.None:
                    offset = 1;
                    break;
                case SendOption.Reliable:
                    offset = 3;
                    break;
            }
        }

        Write(msg.Buffer, offset, msg.Length - offset);
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