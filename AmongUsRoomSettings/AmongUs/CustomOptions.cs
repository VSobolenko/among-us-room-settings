using AmongUsRoomSettings.AmongUs.Client;

namespace AmongUsRoomSettings.AmongUs;

internal class CustomOptions
{
    public static NormalGameOptionsV09 GetDefaultOptions() => new();

    public static NormalGameOptionsV09 GetCustomOptions()
    {
        var options = new NormalGameOptionsV09
        {
            MaxPlayers = 10,

            NumImpostors = 2, // 1 - 3
            KillCooldown = 0.0000000001f, // MinValue- 0f- 999_999f - 999_999_999_999f
            ImpostorLightMod = -987f, // MinValue- 0f- 999_999f - 999_999_999_999f
            KillDistance = KillDistance.Medium,

            PlayerSpeedMod = 1.5f, // 0.001f - 3f
            CrewLightMod = -7484817, // MinValue - 0f- 999_999f - 999_999_999_999f

            NumEmergencyMeetings = 447484817, // int.MinValue -int.MaxValue
            EmergencyCooldown = byte.MaxValue, // 0 - byte.MaxValue
            DiscussionTime = -7484817, // int.MinValue -int.MaxValue
            VotingTime = -7484817, // int.MinValue - int.MaxValue
            AnonymousVotes = true,
            ConfirmImpostor = true,

            TaskBarMode = TaskBarMode.MeetingOnly,
            NumCommonTasks = 0, // 0 - byte.MaxValue
            NumLongTasks = 0, // 0 - byte.MaxValue
            NumShortTasks = 0, // 0 - byte.MaxValue
            VisualTasks = false,

            RoleOptions = new RoleOptionsCollectionV09
            {
                Roles = GetCustomData(1, 255)
            }
        };
        return options;
    }

    public static NormalGameOptionsV09 GetShpeksiOptions()
    {
        var options = new NormalGameOptionsV09
        {
            MaxPlayers = 10,
            Keywords = GameKeywords.Russian,
            UNKNOWN_BYTE = 1,
            MapId = 0,

            NumImpostors = 2,
            KillCooldown = 0.0001f,
            ImpostorLightMod = 1.5f,
            KillDistance = KillDistance.Medium,

            PlayerSpeedMod = 0.0001f,
            CrewLightMod = 1.0f,

            NumEmergencyMeetings = 7484817,
            EmergencyCooldown = 15,
            DiscussionTime = 45,
            VotingTime = 60,
            AnonymousVotes = true,
            ConfirmImpostor = true,

            TaskBarMode = TaskBarMode.MeetingOnly,
            NumCommonTasks = 142,
            NumLongTasks = 0,
            NumShortTasks = 0,
            VisualTasks = false,

            RoleOptions = new RoleOptionsCollectionV09
            {
                Roles = GetShpeksiData()
            }
        };
        return options;
    }

    public static NormalGameOptionsV09 GetMagicOptions()
    {
        var options = new NormalGameOptionsV09
        {
            MaxPlayers = 15,
            Keywords = GameKeywords.Russian,

            NumImpostors = 2, // 1 - 3
            KillCooldown = 0.000001f, // MinValue- 0f- 999_999f - 999_999_999_999f
            ImpostorLightMod = -7484817, // MinValue- 0f- 999_999f - 999_999_999_999f
            KillDistance = KillDistance.Medium,

            PlayerSpeedMod = 0.001f, // 0.001f - 3f
            CrewLightMod = 999_999_999_999f, // MinValue - 0f- 999_999f - 999_999_999_999f

            NumEmergencyMeetings = -7484817, // int.MinValue -int.MaxValue
            EmergencyCooldown = byte.MaxValue, // 0 - byte.MaxValue
            DiscussionTime = 30, // int.MinValue -int.MaxValue
            VotingTime = 45, // int.MinValue - int.MaxValue
            AnonymousVotes = true,
            ConfirmImpostor = true,

            TaskBarMode = TaskBarMode.Always,
            NumCommonTasks = 117, // 0 - byte.MaxValue
            NumLongTasks = 17, // 0 - byte.MaxValue
            NumShortTasks = 7, // 0 - byte.MaxValue
            VisualTasks = true,

            RoleOptions = new RoleOptionsCollectionV09
            {
                Roles = GetMagicData()
            }
        };
        return options;
    }

    private static RoleDataV09[] GetCustomData(byte count, byte chance)
    {
        return new[]
        {
            new RoleDataV09(count, chance, new ShapeshifterRoleOptionsV09
            {
                ShapeshifterLeaveSkin = false,
                ShapeshifterDuration = 10,
                ShapeshifterCooldown = 30,
            }),
            new RoleDataV09(count, chance, new ScientistRoleOptionsV09
            {
                ScientistCooldown = 15,
                ScientistBatteryCharge = 5,
            }),
            new RoleDataV09(count, chance, new GuardianAngelRoleOptionsV09
            {
                GuardianAngelCooldown = 60,
                ProtectionDurationSeconds = 10,
                ImpostorsCanSeeProtect = false,
            }),
            new RoleDataV09(count, chance, new EngineerRoleOptionsV09
            {
                EngineerCooldown = 30,
                EngineerInVentMaxTime = 15,
            }),
            new RoleDataV09(count, chance, new NoisemakerRoleOptionsV09
            {
                NoisemakerImpostorAlert = true,
                NoisemakerAlertDuration = 10,
            }),
            new RoleDataV09(count, chance, new PhantomRoleOptionsV09
            {
                PhantomDuration = 15,
                PhantomCooldown = 30,
            }),
            new RoleDataV09(count, chance, new TrackerRoleOptionsV09
            {
                TrackerCooldown = 15,
                TrackerDelay = 1,
                TrackerDuration = 30,
            }),
        };
    }

    private static RoleDataV09[] GetMagicData()
    {
        return new[]
        {
            new RoleDataV09(byte.MaxValue - 1, byte.MaxValue - 9, new ShapeshifterRoleOptionsV09
            {
                ShapeshifterLeaveSkin = false,
                ShapeshifterDuration = byte.MaxValue - 1,
                ShapeshifterCooldown = 0,
            }),
            new RoleDataV09(byte.MaxValue - 2, byte.MaxValue - 10, new ScientistRoleOptionsV09
            {
                ScientistCooldown = byte.MaxValue,
                ScientistBatteryCharge = byte.MaxValue - 2,
            }),
            new RoleDataV09(byte.MaxValue - 3, byte.MaxValue - 11, new GuardianAngelRoleOptionsV09
            {
                GuardianAngelCooldown = 0,
                ProtectionDurationSeconds = byte.MaxValue - 3,
                ImpostorsCanSeeProtect = false,
            }),
            new RoleDataV09(byte.MaxValue - 4, byte.MaxValue - 12, new EngineerRoleOptionsV09
            {
                EngineerCooldown = 0,
                EngineerInVentMaxTime = byte.MaxValue - 4,
            }),
            new RoleDataV09(byte.MaxValue - 5, byte.MaxValue - 13, new NoisemakerRoleOptionsV09
            {
                NoisemakerImpostorAlert = true,
                NoisemakerAlertDuration = 0,
            }),
            new RoleDataV09(byte.MaxValue - 6, byte.MaxValue - 14, new PhantomRoleOptionsV09
            {
                PhantomDuration = byte.MaxValue - 5,
                PhantomCooldown = 0,
            }),
            new RoleDataV09(byte.MaxValue - 7, byte.MaxValue - 15, new TrackerRoleOptionsV09
            {
                TrackerCooldown = 0,
                TrackerDelay = 0,
                TrackerDuration = byte.MaxValue - 6,
            }),
        };
    }

    private static RoleDataV09[] GetShpeksiData()
    {
        return new[]
        {
            new RoleDataV09(1, 100, new ShapeshifterRoleOptionsV09
            {
                ShapeshifterLeaveSkin = false,
                ShapeshifterDuration = 30,
                ShapeshifterCooldown = 10,
            }),
            new RoleDataV09(1, 100, new ScientistRoleOptionsV09
            {
                ScientistCooldown = byte.MaxValue - 4,
                ScientistBatteryCharge = byte.MaxValue - 3,
            }),
            new RoleDataV09(byte.MaxValue, byte.MaxValue, new GuardianAngelRoleOptionsV09
            {
                GuardianAngelCooldown = byte.MaxValue - 2,
                ProtectionDurationSeconds = byte.MaxValue - 1,
                ImpostorsCanSeeProtect = false,
            }),
            new RoleDataV09(1, 100, new EngineerRoleOptionsV09
            {
                EngineerCooldown = 10,
                EngineerInVentMaxTime = 25,
            }),
            new RoleDataV09(1, 100, new NoisemakerRoleOptionsV09
            {
                NoisemakerImpostorAlert = true,
                NoisemakerAlertDuration = 8,
            }),
            new RoleDataV09(1, 100, new PhantomRoleOptionsV09
            {
                PhantomDuration = 30,
                PhantomCooldown = 10,
            }),
            new RoleDataV09(1, 100, new TrackerRoleOptionsV09
            {
                TrackerCooldown = 15,
                TrackerDelay = 0,
                TrackerDuration = 35,
            }),
        };
    }
}