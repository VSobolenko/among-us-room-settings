using System.Text.Json.Serialization;
using AmongUsRoomSettings.AmongUs.Converters;
using AmongUsRoomSettings.Hazel;

namespace AmongUsRoomSettings.AmongUs.Client;

internal class RoleOptionsCollectionV09
{
    private const byte DefaultCount = 1;
    private const byte DefaultChance = 100;

    //Sequence: Shapeshifter, Scientist, GuardianAngel, Engineer, Noisemaker, PhantomRole, Tracker, Detective, Viper
    public RoleDataV09[] Roles { get; set; } =
    {
        new()
        {
            Rate = new RoleRate { MaxCount = DefaultCount, Chance = DefaultChance },
            RoleOptions = new ShapeshifterRoleOptionsV09()
        },
        new()
        {
            Rate = new RoleRate { MaxCount = DefaultCount, Chance = DefaultChance },
            RoleOptions = new ScientistRoleOptionsV09()
        },
        new()
        {
            Rate = new RoleRate { MaxCount = DefaultCount, Chance = DefaultChance },
            RoleOptions = new GuardianAngelRoleOptionsV09()
        },
        new()
        {
            Rate = new RoleRate { MaxCount = DefaultCount, Chance = DefaultChance },
            RoleOptions = new EngineerRoleOptionsV09()
        },
        new()
        {
            Rate = new RoleRate { MaxCount = DefaultCount, Chance = DefaultChance },
            RoleOptions = new NoisemakerRoleOptionsV09()
        },
        new()
        {
            Rate = new RoleRate { MaxCount = DefaultCount, Chance = DefaultChance },
            RoleOptions = new PhantomRoleOptionsV09()
        },
        new()
        {
            Rate = new RoleRate { MaxCount = DefaultCount, Chance = DefaultChance },
            RoleOptions = new TrackerRoleOptionsV09()
        },
        new()
        {
            Rate = new RoleRate { MaxCount = DefaultCount, Chance = DefaultChance },
            RoleOptions = new DetectiveRoleOptionsV10()
        },
        new()
        {
            Rate = new RoleRate { MaxCount = DefaultCount, Chance = DefaultChance },
            RoleOptions = new ViperRoleOptionsV10()
        },
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
    [JsonConverter(typeof(RoleOptionsConverterJson))]
    public IRoleOptions RoleOptions { get; set; }

    public RoleRate Rate { get; set; }

    public RoleDataV09() { }

    public RoleDataV09(byte count, byte chance, IRoleOptions roleOptions)
    {
        Rate = new RoleRate { MaxCount = count, Chance = chance };
        RoleOptions = roleOptions;
    }
}

public struct RoleRate
{
    public byte MaxCount { get; set; }
    public byte Chance { get; set; }
}