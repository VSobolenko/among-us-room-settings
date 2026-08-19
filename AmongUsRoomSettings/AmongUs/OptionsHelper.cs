using System.Text;
using AmongUsRoomSettings.AmongUs.Client;
using AmongUsRoomSettings.Hazel;

namespace AmongUsRoomSettings.AmongUs;

internal class OptionsHelper
{
    public static string GetDebugStringValues(string base64)
    {
        var data = Convert.FromBase64String(base64);
        var reader = MessageReader.Get(data);
        var output = new StringBuilder();

        output.AppendLine($"{nameof(Version)}: {reader.ReadByte()}");
        output.AppendLine($"Length: {reader.ReadUInt16()}");
        output.AppendLine($"MessageReader Tag: {reader.ReadByte()}");
        output.AppendLine($"SpecialMode: {(SpecialGameModes)reader.ReadByte()}");
        output.AppendLine($"RulesPreset: {(RulesPresets)reader.ReadByte()}");
        output.AppendLine($"UNKNOWN: {reader.ReadByte()}");
        output.AppendLine($"MaxPlayers: {reader.ReadByte()}");
        output.AppendLine($"Keywords: {(GameKeywords)reader.ReadUInt32()}");
        output.AppendLine($"MapId: {reader.ReadByte()}");
        output.AppendLine($"PlayerSpeedMod: {reader.ReadSingle()}");
        output.AppendLine($"CrewLightMod: {reader.ReadSingle()}");
        output.AppendLine($"ImpostorLightMod: {reader.ReadSingle()}");
        output.AppendLine($"KillCooldown: {reader.ReadSingle()}");
        output.AppendLine($"NumCommonTasks: {reader.ReadByte()}");
        output.AppendLine($"NumLongTasks: {reader.ReadByte()}");
        output.AppendLine($"NumShortTasks: {reader.ReadByte()}");
        output.AppendLine($"NumEmergencyMeetings: {reader.ReadInt32()}");
        output.AppendLine($"NumImpostors: {reader.ReadByte()}");
        output.AppendLine($"KillDistance: {(KillDistance)reader.ReadByte()}");
        output.AppendLine($"DiscussionTime: {reader.ReadInt32()}");
        output.AppendLine($"VotingTime: {reader.ReadInt32()}");
        output.AppendLine($"IsDefaults: {reader.ReadBoolean()}");
        output.AppendLine($"EmergencyCooldown: {reader.ReadByte()}");
        output.AppendLine($"ConfirmImpostor: {reader.ReadBoolean()}");
        output.AppendLine($"VisualTasks: {reader.ReadBoolean()}");
        output.AppendLine($"AnonymousVotes: {reader.ReadBoolean()}");
        output.AppendLine($"TaskBarMode: {(TaskBarMode)reader.ReadByte()}");
        output.AppendLine($"Tag: {reader.ReadByte()}");

        var roleCount = reader.ReadByte();
        output.AppendLine($">>>>> Count Roles: {roleCount}");

        for (var i = 0; i < roleCount; i++)
        {
            var roleType = (RoleTypes)reader.ReadUInt16();
            var maxCount = reader.ReadByte();
            var chance = reader.ReadByte();
            var fieldLength = reader.ReadUInt16();
            var tag = reader.ReadByte();
            var valueStart = reader.Position;
            var label = roleType.ToString().ToUpperInvariant();

            output.AppendLine($"[{label}] Type: {(ushort)roleType}");
            output.AppendLine($"[{label}] Count: {maxCount}");
            output.AppendLine($"[{label}] Chance: {chance}");
            output.AppendLine($"[{label}] Length/Tag: {fieldLength}/{tag}");

            switch (roleType)
            {
                case RoleTypes.Shapeshifter:
                    output.AppendLine($"[{label}] leave skin: {reader.ReadBoolean()}");
                    output.AppendLine($"[{label}] cooldown: {reader.ReadByte()}");
                    output.AppendLine($"[{label}] duration: {reader.ReadByte()}");
                    break;

                case RoleTypes.Scientist:
                    output.AppendLine($"[{label}] cooldown: {reader.ReadByte()}");
                    output.AppendLine($"[{label}] charge: {reader.ReadByte()}");
                    break;

                case RoleTypes.GuardianAngel:
                    output.AppendLine($"[{label}] cooldown: {reader.ReadByte()}");
                    output.AppendLine($"[{label}] duration: {reader.ReadByte()}");
                    output.AppendLine($"[{label}] can see protect: {reader.ReadBoolean()}");
                    break;

                case RoleTypes.Engineer:
                    output.AppendLine($"[{label}] cooldown: {reader.ReadByte()}");
                    output.AppendLine($"[{label}] vent max time: {reader.ReadByte()}");
                    break;

                case RoleTypes.Noisemaker:
                    output.AppendLine($"[{label}] alert duration: {reader.ReadByte()}");
                    output.AppendLine($"[{label}] impostor view: {reader.ReadBoolean()}");
                    break;

                case RoleTypes.Phantom:
                    output.AppendLine($"[{label}] cooldown: {reader.ReadByte()}");
                    output.AppendLine($"[{label}] duration: {reader.ReadByte()}");
                    break;

                case RoleTypes.Tracker:
                    output.AppendLine($"[{label}] cooldown: {reader.ReadByte()}");
                    output.AppendLine($"[{label}] duration: {reader.ReadByte()}");
                    output.AppendLine($"[{label}] delay: {reader.ReadByte()}");
                    break;

                case RoleTypes.Detective:
                    output.AppendLine($"[{label}] Suspects: {reader.ReadByte()}");
                    break;

                case RoleTypes.Viper:
                    output.AppendLine($"[{label}] Dissolve time: {reader.ReadByte()}");
                    break;

                case RoleTypes.Judge:
                    output.AppendLine($"[{label}] TaskRequirementPercentage: {reader.ReadByte()}");
                    break;
            }

            reader.Position = valueStart + fieldLength;
        }

        return output.ToString();
    }
}