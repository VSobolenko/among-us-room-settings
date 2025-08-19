namespace AmongUsRoomSettings.AmongUs.Client;

public enum KillDistance : byte
{
    Short = 0,
    Medium = 1,
    Long = 2,
}

public enum GameModes : byte
{
    None = 0,
    Normal = 1,
    HideNSeek = 2,
    NormalFools = 3,
    SeekFools = 4,
}

[Flags]
public enum SpecialGameModes : byte
{
    None = 0,
    AprilFools = 1,
    Unknown1 = 2,
    Unknown2 = 4,
    Unknown3 = 8,
}

public enum RulesPresets : byte
{
    Standard = 0,
    StandardRoles = 1,
    Flashlight = 2,
}

[Flags]
public enum GameKeywords : uint
{
    All = 0U,
    English = 256U,
    SpanishLA = 2U,
    Brazilian = 2048U,
    Portuguese = 16U,
    Korean = 4U,
    Russian = 8U,
    Dutch = 4096U,
    Filipino = 64U,
    French = 8192U,
    German = 16384U,
    Italian = 32768U,
    Japanese = 512U,
    SpanishEU = 1024U,
    Arabic = 32U,
    Polish = 128U,
    SChinese = 65536U,
    TChinese = 131072U,
    Irish = 262144U,
    Other = 1U
}

public enum RoleTypes : ushort
{
    Scientist = 2,
    Engineer = 3,
    GuardianAngel = 4,
    Shapeshifter = 5,
    Noisemaker = 8,
    Phantom = 9,
    Tracker = 10
}

public enum TaskBarMode
{
    Always = 0,
    MeetingOnly = 1,
    Never = 2
}