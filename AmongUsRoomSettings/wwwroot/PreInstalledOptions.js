function setOptions(values) {
    for (let key in values) {
        const el = document.getElementById(key);
        if (!el) continue;

        if (el.type === "checkbox") {
            el.checked = values[key]; // для чекбоксов
        } else {
            el.value = values[key];   // для числовых полей
        }
    }
}

// варианты настроек
const presets = {
    default: {
        MaxPlayers: 15,
        PlayerSpeedMod: 1.0,
        CrewLightMod: 1.0,
        NumImpostors: 2,
        ImpostorLightMod: 1.5,
        KillCooldown: 45,
        NumCommonTasks: 1,
        DiscussionTime: 15,
        NumShortTasks: 2,
        VotingTime: 120,
        NumLongTasks: 1,
        VisualTasks: true,
        NumEmergencyMeetings: 1,
        ConfirmImpostor: true,
        EmergencyCooldown: 15,
        AnonymousVotes: false,
        Tag: 0,
        IsDefaults: true,

        // Shapeshifter
        Role_Shapeshifter_MaxCount: 1,
        Role_Shapeshifter_Chance: 100,
        ShapeshifterLeaveSkin: true,
        ShapeshifterCooldown: 30,
        ShapeshifterDuration: 10,

        // Scientist
        Role_Scientist_MaxCount: 1,
        Role_Scientist_Chance: 100,
        ScientistCooldown: 15,
        ScientistBatteryCharge: 5,

        // Guardian Angel
        Role_Guardian_MaxCount: 1,
        Role_Guardian_Chance: 100,
        GuardianAngelCooldown: 60,
        ProtectionDurationSeconds: 10,
        ImpostorsCanSeeProtect: false,

        // Engineer
        Role_Engineer_MaxCount: 1,
        Role_Engineer_Chance: 100,
        EngineerCooldown: 30,
        EngineerInVentMaxTime: 15,

        // Noisemaker
        Role_Noisemaker_MaxCount: 1,
        Role_Noisemaker_Chance: 100,
        NoisemakerImpostorAlert: true,
        NoisemakerAlertDuration: 10,

        // Phantom
        Role_Phantom_MaxCount: 1,
        Role_Phantom_Chance: 100,
        PhantomCooldown: 30,
        PhantomDuration: 15,

        // Tracker
        Role_Tracker_MaxCount: 1,
        Role_Tracker_Chance: 100,
        TrackerCooldown: 15,
        TrackerDuration: 30,
        TrackerDelay: 1
    },
    magic: {
        MaxPlayers: 15,
        PlayerSpeedMod: 0.0001,
        CrewLightMod: 7484817,
        NumImpostors: 3,
        ImpostorLightMod: -696969,
        KillCooldown: 0.000001,
        NumCommonTasks: 255,
        DiscussionTime: 30,
        NumShortTasks: 255,
        VotingTime: 30,
        NumLongTasks: 255,
        VisualTasks: true,
        NumEmergencyMeetings: 2147483640,
        ConfirmImpostor: true,
        EmergencyCooldown: 255,
        AnonymousVotes: false,
        Tag: 0,
        IsDefaults: true,

        // Shapeshifter
        Role_Shapeshifter_MaxCount: 250,
        Role_Shapeshifter_Chance: 111,
        ShapeshifterLeaveSkin: true,
        ShapeshifterCooldown: 0,
        ShapeshifterDuration: 255,

        // Scientist
        Role_Scientist_MaxCount: 253,
        Role_Scientist_Chance: 99,
        ScientistCooldown: 255,
        ScientistBatteryCharge: 255,

        // Guardian Angel
        Role_Guardian_MaxCount: 254,
        Role_Guardian_Chance: 212,
        GuardianAngelCooldown: 0,
        ProtectionDurationSeconds: 255,
        ImpostorsCanSeeProtect: true,

        // Engineer
        Role_Engineer_MaxCount: 255,
        Role_Engineer_Chance: 169,
        EngineerCooldown: 0,
        EngineerInVentMaxTime: 194,

        // Noisemaker
        Role_Noisemaker_MaxCount: 251,
        Role_Noisemaker_Chance: 181,
        NoisemakerImpostorAlert: true,
        NoisemakerAlertDuration: 190,

        // Phantom
        Role_Phantom_MaxCount: 249,
        Role_Phantom_Chance: 123,
        PhantomCooldown: 0,
        PhantomDuration: 210,

        // Tracker
        Role_Tracker_MaxCount: 252,
        Role_Tracker_Chance: 255,
        TrackerCooldown: 255,
        TrackerDuration: 255,
        TrackerDelay: 0
    },
    personal: {
        MaxPlayers: 15,
        PlayerSpeedMod: 0.0001,
        CrewLightMod: 7484817,
        NumImpostors: 3,
        ImpostorLightMod: -696969,
        KillCooldown: 0.000001,
        NumCommonTasks: 255,
        DiscussionTime: 30,
        NumShortTasks: 255,
        VotingTime: 30,
        NumLongTasks: 255,
        VisualTasks: true,
        NumEmergencyMeetings: 2147483640,
        ConfirmImpostor: true,
        EmergencyCooldown: 255,
        AnonymousVotes: false,
        Tag: 0,
        IsDefaults: true,

        // Shapeshifter
        Role_Shapeshifter_MaxCount: 250,
        Role_Shapeshifter_Chance: 111,
        ShapeshifterLeaveSkin: true,
        ShapeshifterCooldown: 0,
        ShapeshifterDuration: 255,

        // Scientist
        Role_Scientist_MaxCount: 253,
        Role_Scientist_Chance: 99,
        ScientistCooldown: 255,
        ScientistBatteryCharge: 255,

        // Guardian Angel
        Role_Guardian_MaxCount: 254,
        Role_Guardian_Chance: 212,
        GuardianAngelCooldown: 0,
        ProtectionDurationSeconds: 255,
        ImpostorsCanSeeProtect: false,

        // Engineer
        Role_Engineer_MaxCount: 255,
        Role_Engineer_Chance: 169,
        EngineerCooldown: 0,
        EngineerInVentMaxTime: 194,

        // Noisemaker
        Role_Noisemaker_MaxCount: 251,
        Role_Noisemaker_Chance: 181,
        NoisemakerImpostorAlert: true,
        NoisemakerAlertDuration: 190,

        // Phantom
        Role_Phantom_MaxCount: 249,
        Role_Phantom_Chance: 123,
        PhantomCooldown: 0,
        PhantomDuration: 210,

        // Tracker
        Role_Tracker_MaxCount: 252,
        Role_Tracker_Chance: 255,
        TrackerCooldown: 255,
        TrackerDuration: 255,
        TrackerDelay: 0
    },
    shpeksi: {
        MaxPlayers: 10,
        PlayerSpeedMod: 1.69,
        CrewLightMod: 1.0,
        NumImpostors: 2,
        ImpostorLightMod: 1.5,
        KillCooldown: 0.00001,
        NumCommonTasks: 141,
        DiscussionTime: 45,
        NumShortTasks: 0,
        VotingTime: 60,
        NumLongTasks: 0,
        VisualTasks: false,
        NumEmergencyMeetings: 1,
        ConfirmImpostor: true,
        EmergencyCooldown: 15,
        AnonymousVotes: true,
        Tag: 0,
        IsDefaults: true,

        // Shapeshifter
        Role_Shapeshifter_MaxCount: 1,
        Role_Shapeshifter_Chance: 100,
        ShapeshifterLeaveSkin: false,
        ShapeshifterCooldown: 30,
        ShapeshifterDuration: 10,

        // Scientist
        Role_Scientist_MaxCount: 1,
        Role_Scientist_Chance: 100,
        ScientistCooldown: 255,
        ScientistBatteryCharge: 255,

        // Guardian Angel
        Role_Guardian_MaxCount: 255,
        Role_Guardian_Chance: 255,
        GuardianAngelCooldown: 255,
        ProtectionDurationSeconds: 255,
        ImpostorsCanSeeProtect: true,

        // Engineer
        Role_Engineer_MaxCount: 1,
        Role_Engineer_Chance: 100,
        EngineerCooldown: 10,
        EngineerInVentMaxTime: 25,

        // Noisemaker
        Role_Noisemaker_MaxCount: 1,
        Role_Noisemaker_Chance: 100,
        NoisemakerImpostorAlert: true,
        NoisemakerAlertDuration: 8,

        // Phantom
        Role_Phantom_MaxCount: 1,
        Role_Phantom_Chance: 100,
        PhantomCooldown: 10,
        PhantomDuration: 30,

        // Tracker
        Role_Tracker_MaxCount: 1,
        Role_Tracker_Chance: 100,
        TrackerCooldown: 15,
        TrackerDuration: 35,
        TrackerDelay: 1
    },
    clown: {
        MaxPlayers: 15,
        PlayerSpeedMod: 1.69,
        CrewLightMod: 696969,
        NumImpostors: 1,
        ImpostorLightMod: 7484817,
        KillCooldown: 0.00001,
        NumCommonTasks: 12,
        DiscussionTime: 10,
        NumShortTasks: 1,
        VotingTime: 120,
        NumLongTasks: 0,
        VisualTasks: true,
        NumEmergencyMeetings: 7484817,
        ConfirmImpostor: true,
        EmergencyCooldown: 15,
        AnonymousVotes: false,
        Tag: 0,
        IsDefaults: true,

        // Shapeshifter
        Role_Shapeshifter_MaxCount: 3,
        Role_Shapeshifter_Chance: 100,
        ShapeshifterLeaveSkin: true,
        ShapeshifterCooldown: 0,
        ShapeshifterDuration: 255,

        // Scientist
        Role_Scientist_MaxCount: 255,
        Role_Scientist_Chance: 255,
        ScientistCooldown: 255,
        ScientistBatteryCharge: 255,

        // Guardian Angel
        Role_Guardian_MaxCount: 255,
        Role_Guardian_Chance: 255,
        GuardianAngelCooldown: 255,
        ProtectionDurationSeconds: 255,
        ImpostorsCanSeeProtect: true,

        // Engineer
        Role_Engineer_MaxCount: 15,
        Role_Engineer_Chance: 100,
        EngineerCooldown: 3,
        EngineerInVentMaxTime: 3,

        // Noisemaker
        Role_Noisemaker_MaxCount: 255,
        Role_Noisemaker_Chance: 255,
        NoisemakerImpostorAlert: true,
        NoisemakerAlertDuration: 255,

        // Phantom
        Role_Phantom_MaxCount: 0,
        Role_Phantom_Chance: 255,
        PhantomCooldown: 255,
        PhantomDuration: 255,

        // Tracker
        Role_Tracker_MaxCount: 255,
        Role_Tracker_Chance: 255,
        TrackerCooldown: 255,
        TrackerDuration: 255,
        TrackerDelay: 255
    }
};
