using AmongUsRoomSettings.Hazel;

namespace AmongUsRoomSettings.AmongUs.Client;

[Serializable]
internal class NormalGameOptionsV09
{
    #region MyRegion

    public const byte Version = 10;
    public const short Length = 132;
    public SpecialGameModes SpecialMode { get; set; } = SpecialGameModes.AprilFools; // byte 
    public RulesPresets RulesPreset { get; set; } = RulesPresets.Standard; // byte 
    public byte UNKNOWN_BYTE { get; set; } = 1;
    public byte MaxPlayers { get; set; } = 15;
    public GameKeywords Keywords { get; set; } = GameKeywords.Russian; // uint
    public byte MapId { get; set; } = 0;
    public float PlayerSpeedMod { get; set; } = 1.0f;
    public float CrewLightMod { get; set; } = 1.0f;
    public float ImpostorLightMod { get; set; } = 1.5f;
    public float KillCooldown { get; set; } = 45f;
    public byte NumCommonTasks { get; set; } = 1;
    public byte NumLongTasks { get; set; } = 1;
    public byte NumShortTasks { get; set; } = 2;
    public int NumEmergencyMeetings { get; set; } = 1;
    public byte NumImpostors { get; set; } = 3;
    public KillDistance KillDistance { get; set; } = KillDistance.Medium; // byte
    public int DiscussionTime { get; set; } = 15;
    public int VotingTime { get; set; } = 120;
    public bool IsDefaults = false; // const
    public byte EmergencyCooldown { get; set; } = 15;
    public bool ConfirmImpostor { get; set; } = true;
    public bool VisualTasks { get; set; } = true;
    public bool AnonymousVotes { get; set; } = false;
    public TaskBarMode TaskBarMode { get; set; } = TaskBarMode.Always; // byte
    public byte Tag { get; set; } = 0;
    public RoleOptionsCollectionV09 RoleOptions { get; set; } = new();

    #endregion

    public void Serialize(MessageWriter writer, NormalGameOptionsV09 options)
    {
        writer.Write((byte)Version);
        writer.Write((short)Length);
        writer.Write((byte)0); // Tag
        writer.Write((byte)options.SpecialMode);
        writer.Write((byte)options.RulesPreset);
        writer.Write((byte)UNKNOWN_BYTE); // UNKNOWN byte
        writer.Write((byte)options.MaxPlayers);
        writer.Write((uint)options.Keywords);
        writer.Write((byte)options.MapId);
        writer.Write((float)options.PlayerSpeedMod);
        writer.Write((float)options.CrewLightMod);
        writer.Write((float)options.ImpostorLightMod);
        writer.Write((float)options.KillCooldown);
        writer.Write((byte)options.NumCommonTasks);
        writer.Write((byte)options.NumLongTasks);
        writer.Write((byte)options.NumShortTasks);
        writer.Write((int)options.NumEmergencyMeetings);
        writer.Write((byte)options.NumImpostors);
        writer.Write((byte)options.KillDistance);
        writer.Write((int)options.DiscussionTime);
        writer.Write((int)options.VotingTime);
        writer.Write((bool)options.IsDefaults);
        writer.Write((byte)options.EmergencyCooldown);
        writer.Write((bool)options.ConfirmImpostor);
        writer.Write((bool)options.VisualTasks);
        writer.Write((bool)options.AnonymousVotes);
        writer.Write((byte)options.TaskBarMode);
        writer.Write((byte)options.Tag);

        RoleOptionsCollectionV09.Serialize(writer, options.RoleOptions);
    }
}