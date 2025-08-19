using AmongUsRoomSettings.AmongUs.Client;
using AmongUsRoomSettings.Hazel;

namespace AmongUsRoomSettings.AmongUs;

internal class RoomSettings
{
    public string GetBase64String(NormalGameOptionsV09 options)
    {
        var writer = MessageWriter.Get(SendOption.Reliable);
        options.Serialize(writer, options);

        var data = writer.ToByteArray(false);
        var base64 = Convert.ToBase64String(data);

        return base64;
    }
}