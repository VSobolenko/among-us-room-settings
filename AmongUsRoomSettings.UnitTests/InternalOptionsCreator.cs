using AmongUsRoomSettings.AmongUs.Client;

namespace AmongUsRoomSettings.UnitTests;

internal abstract class InternalOptionsCreator
{
    public static NormalGameOptionsV09 GetDefault() => new();

    public static NormalGameOptionsV09 GetCustom() => new()
    {
        MaxPlayers = 15,

        NumImpostors = 3, // 1 - 3
        KillCooldown = 19.99f, // MinValue- 0f- 999_999f - 999_999_999_999f
        ImpostorLightMod = 74848.17f, // MinValue- 0f- 999_999f - 999_999_999_999f
        KillDistance = KillDistance.Medium,

        PlayerSpeedMod = 1.7788f, // 0.001f - 3f
        CrewLightMod = 1.19f, // MinValue - 0f- 999_999f - 999_999_999_999f

        NumEmergencyMeetings = 1, // int.MinValue -int.MaxValue
        EmergencyCooldown = 19, // 0 - byte.MaxValue
        DiscussionTime = 29, // int.MinValue -int.MaxValue
        VotingTime = 71, // int.MinValue - int.MaxValue
        AnonymousVotes = true,
        ConfirmImpostor = true,

        TaskBarMode = TaskBarMode.MeetingOnly,
        NumCommonTasks = 0, // 0 - byte.MaxValue
        NumLongTasks = 0, // 0 - byte.MaxValue
        NumShortTasks = 7, // 0 - byte.MaxValue
        VisualTasks = false,

        RoleOptions = new RoleOptionsCollectionV09
        {
            Roles = GetCustomData()
        }
    };

    private static RoleDataV09[] GetCustomData()
    {
        return
        [
            new RoleDataV09(1, 99, new ShapeshifterRoleOptionsV09
            {
                ShapeshifterLeaveSkin = true,
                ShapeshifterDuration = 25,
                ShapeshifterCooldown = 8,
            }),
            new RoleDataV09(3, 97, new ScientistRoleOptionsV09
            {
                ScientistCooldown = 1,
                ScientistBatteryCharge = 255,
            }),
            new RoleDataV09(1, 96, new GuardianAngelRoleOptionsV09
            {
                GuardianAngelCooldown = 33,
                ProtectionDurationSeconds = 29,
                ImpostorsCanSeeProtect = false,
            }),
            new RoleDataV09(3, 89, new EngineerRoleOptionsV09
            {
                EngineerCooldown = 23,
                EngineerInVentMaxTime = 27,
            }),
            new RoleDataV09(3, 95, new NoisemakerRoleOptionsV09
            {
                NoisemakerImpostorAlert = true,
                NoisemakerAlertDuration = 190,
            }),
            new RoleDataV09(1, 96, new PhantomRoleOptionsV09
            {
                PhantomDuration = 210,
                PhantomCooldown = 17,
            }),
            new RoleDataV09(2, 97, new TrackerRoleOptionsV09
            {
                TrackerCooldown = 14,
                TrackerDelay = 1,
                TrackerDuration = 33,
            }),
            new RoleDataV09(2, 99, new DetectiveRoleOptionsV10()
            {
                DetectiveSuspectLimit = 148,
            }),
            new RoleDataV09(2, 97, new ViperRoleOptionsV10()
            {
                ViperDissolveTime = 12,
            })
        ];
    }
}