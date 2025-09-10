function buildRoleOptions() {
    const roles = [];
    // Order: Shapeshifter, Scientist, GuardianAngel, Engineer, Noisemaker, Phantom, Tracker, Detective, Viper
    roles.push({
        RoleOptions: {
            Type: 'Shapeshifter',
            ShapeshifterLeaveSkin: checked('ShapeshifterLeaveSkin'),
            ShapeshifterCooldown: num('ShapeshifterCooldown'),
            ShapeshifterDuration: num('ShapeshifterDuration')
        },
        Rate: {MaxCount: num('Role_Shapeshifter_MaxCount'), Chance: num('Role_Shapeshifter_Chance')}
    });

    roles.push({
        RoleOptions: {
            Type: 'Scientist',
            ScientistCooldown: num('ScientistCooldown'),
            ScientistBatteryCharge: num('ScientistBatteryCharge')
        },
        Rate: {MaxCount: num('Role_Scientist_MaxCount'), Chance: num('Role_Scientist_Chance')}
    });

    roles.push({
        RoleOptions: {
            Type: 'GuardianAngel',
            GuardianAngelCooldown: num('GuardianAngelCooldown'),
            ProtectionDurationSeconds: num('ProtectionDurationSeconds'),
            ImpostorsCanSeeProtect: checked('ImpostorsCanSeeProtect')
        },
        Rate: {MaxCount: num('Role_Guardian_MaxCount'), Chance: num('Role_Guardian_Chance')}
    });

    roles.push({
        RoleOptions: {
            Type: 'Engineer',
            EngineerCooldown: num('EngineerCooldown'),
            EngineerInVentMaxTime: num('EngineerInVentMaxTime')
        },
        Rate: {MaxCount: num('Role_Engineer_MaxCount'), Chance: num('Role_Engineer_Chance')}
    });

    roles.push({
        RoleOptions: {
            Type: 'Noisemaker',
            NoisemakerImpostorAlert: checked('NoisemakerImpostorAlert'),
            NoisemakerAlertDuration: num('NoisemakerAlertDuration')
        },
        Rate: {MaxCount: num('Role_Noisemaker_MaxCount'), Chance: num('Role_Noisemaker_Chance')}
    });

    roles.push({
        RoleOptions: {
            Type: 'Phantom',
            PhantomCooldown: num('PhantomCooldown'),
            PhantomDuration: num('PhantomDuration')
        },
        Rate: {MaxCount: num('Role_Phantom_MaxCount'), Chance: num('Role_Phantom_Chance')}
    });

    roles.push({
        RoleOptions: {
            Type: 'Tracker',
            TrackerCooldown: num('TrackerCooldown'),
            TrackerDuration: num('TrackerDuration'),
            TrackerDelay: num('TrackerDelay')
        },
        Rate: {MaxCount: num('Role_Tracker_MaxCount'), Chance: num('Role_Tracker_Chance')}
    });

    roles.push({
        RoleOptions: {
            Type: 'Detective',
            DetectiveSuspectLimit: num('DetectiveSuspectLimit'),
        },
        Rate: {MaxCount: num('Role_Detective_MaxCount'), Chance: num('Role_Detective_Chance')}
    });

    roles.push({
        RoleOptions: {
            Type: 'Viper',
            DetectiveSuspectLimit: num('ViperDissolveTime'),
        },
        Rate: {MaxCount: num('Role_Viper_MaxCount'), Chance: num('Role_Viper_Chance')}
    });
    
    return {Roles: roles};
}