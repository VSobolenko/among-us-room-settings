using System.Text.Json.Serialization;
using AmongUsRoomSettings.Hazel;

namespace AmongUsRoomSettings.AmongUs.Client;

internal class RoleOptionsCollectionV09
{
    private const byte DefaultCount = 0;
    private const byte DefaultChance = 0;

    //Sequence: Shapeshifter, Scientist, GuardianAngel, Engineer, Noisemaker, PhantomRole, Tracker
    public RoleDataV09[] Roles { get; set; } =
    {
        new(DefaultCount, DefaultChance, new ShapeshifterRoleOptionsV09()),
        new(DefaultCount, DefaultChance, new ScientistRoleOptionsV09()),
        new(DefaultCount, DefaultChance, new GuardianAngelRoleOptionsV09()),
        new(DefaultCount, DefaultChance, new EngineerRoleOptionsV09()),
        new(DefaultCount, DefaultChance, new NoisemakerRoleOptionsV09()),
        new(DefaultCount, DefaultChance, new PhantomRoleOptionsV09()),
        new(DefaultCount, DefaultChance, new TrackerRoleOptionsV09()),
    };

    public static void Serialize(MessageWriter writer, RoleOptionsCollectionV09 options)
    {
        writer.Write((byte)options.Roles.Length);

        foreach (var data in options.Roles)
        {
            writer.Write((ushort)data.RoleOptions.Type);
            writer.Write((byte)data.Rate.MaxCount);
            writer.Write((byte)data.Rate.Chance);
            writer.Write((short)data.RoleOptions.FieldLength);
            writer.Write((byte)0); // Tag
            data.RoleOptions.Serialize(writer, data.RoleOptions);
        }
    }
}

[Serializable]
internal class RoleDataV09
{
    [JsonConverter(typeof(RoleOptionsConverter))]
    public IRoleOptions RoleOptions { get; set; }
    public RoleRate Rate { get; set; }

    public RoleDataV09() { }

    public RoleDataV09(byte maxCount, byte chance, IRoleOptions options)
    {
        RoleOptions = options;
        Rate = new RoleRate(maxCount, chance);
    }
}

public struct RoleRate
{
    public byte MaxCount { get; set; }
    public byte Chance { get; set; }

    public RoleRate() { }

    public RoleRate(byte maxCount, byte chance)
    {
        MaxCount = maxCount;
        Chance = chance;
    }
}