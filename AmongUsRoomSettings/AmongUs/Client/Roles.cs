using AmongUsRoomSettings.Hazel;

namespace AmongUsRoomSettings.AmongUs.Client;

internal interface IRoleOptions
{
    byte FieldLength { get; }
    public RoleTypes Type { get; }
    void Serialize(MessageWriter writer, IRoleOptions dataRoleOptions);
}

[Serializable]
internal class ShapeshifterRoleOptionsV09 : IRoleOptions
{
    public RoleTypes Type => RoleTypes.Shapeshifter;
    public byte FieldLength => 3;

    public void Serialize(MessageWriter writer, IRoleOptions dataRoleOptions)
    {
        var entity = (ShapeshifterRoleOptionsV09)dataRoleOptions;
        writer.Write(entity.ShapeshifterLeaveSkin);
        writer.Write((byte)entity.ShapeshifterCooldown);
        writer.Write((byte)entity.ShapeshifterDuration);
    }

    public bool ShapeshifterLeaveSkin { get; set; } = false;
    public byte ShapeshifterCooldown { get; set; } = 30;
    public byte ShapeshifterDuration { get; set; } = 10;
}

[Serializable]
internal class ScientistRoleOptionsV09 : IRoleOptions
{
    public RoleTypes Type => RoleTypes.Scientist;
    public byte FieldLength => 2;

    public void Serialize(MessageWriter writer, IRoleOptions dataRoleOptions)
    {
        var entity = (ScientistRoleOptionsV09)dataRoleOptions;
        writer.Write((byte)entity.ScientistCooldown);
        writer.Write((byte)entity.ScientistBatteryCharge);
    }

    public byte ScientistCooldown { get; set; } = 15;
    public byte ScientistBatteryCharge { get; set; } = 5;
}

[Serializable]
internal class GuardianAngelRoleOptionsV09 : IRoleOptions
{
    public RoleTypes Type => RoleTypes.GuardianAngel;
    public byte FieldLength => 3;

    public void Serialize(MessageWriter writer, IRoleOptions dataRoleOptions)
    {
        var entity = (GuardianAngelRoleOptionsV09)dataRoleOptions;
        writer.Write((byte)entity.GuardianAngelCooldown);
        writer.Write((byte)entity.ProtectionDurationSeconds);
        writer.Write(entity.ImpostorsCanSeeProtect);
    }

    public byte GuardianAngelCooldown { get; set; } = 60;
    public byte ProtectionDurationSeconds { get; set; } = 10;
    public bool ImpostorsCanSeeProtect { get; set; } = false;
}

[Serializable]
internal class EngineerRoleOptionsV09 : IRoleOptions
{
    public RoleTypes Type => RoleTypes.Engineer;
    public byte FieldLength => 2;

    public void Serialize(MessageWriter writer, IRoleOptions dataRoleOptions)
    {
        var entity = (EngineerRoleOptionsV09)dataRoleOptions;
        writer.Write((byte)entity.EngineerCooldown);
        writer.Write((byte)entity.EngineerInVentMaxTime);
    }

    public byte EngineerCooldown { get; set; } = 30;
    public byte EngineerInVentMaxTime { get; set; } = 15;
}

[Serializable]
internal class NoisemakerRoleOptionsV09 : IRoleOptions
{
    public RoleTypes Type => RoleTypes.Noisemaker;
    public byte FieldLength => 2;

    public void Serialize(MessageWriter writer, IRoleOptions dataRoleOptions)
    {
        var entity = (NoisemakerRoleOptionsV09)dataRoleOptions;
        writer.Write((byte)entity.NoisemakerAlertDuration);
        writer.Write(entity.NoisemakerImpostorAlert);
    }

    public bool NoisemakerImpostorAlert { get; set; } = true;
    public byte NoisemakerAlertDuration { get; set; } = 10;
}

[Serializable]
internal class PhantomRoleOptionsV09 : IRoleOptions
{
    public RoleTypes Type => RoleTypes.Phantom;
    public byte FieldLength => 2;

    public void Serialize(MessageWriter writer, IRoleOptions dataRoleOptions)
    {
        var entity = (PhantomRoleOptionsV09)dataRoleOptions;
        writer.Write((byte)entity.PhantomCooldown);
        writer.Write((byte)entity.PhantomDuration);
    }

    public byte PhantomCooldown { get; set; } = 30;
    public byte PhantomDuration { get; set; } = 15;
}

[Serializable]
internal class TrackerRoleOptionsV09 : IRoleOptions
{
    public RoleTypes Type => RoleTypes.Tracker;
    public byte FieldLength => 3;

    public void Serialize(MessageWriter writer, IRoleOptions dataRoleOptions)
    {
        var entity = (TrackerRoleOptionsV09)dataRoleOptions;
        writer.Write((byte)entity.TrackerCooldown);
        writer.Write((byte)entity.TrackerDuration);
        writer.Write((byte)entity.TrackerDelay);
    }

    public byte TrackerCooldown { get; set; } = 15;
    public byte TrackerDuration { get; set; } = 30;
    public byte TrackerDelay { get; set; } = 1;
}