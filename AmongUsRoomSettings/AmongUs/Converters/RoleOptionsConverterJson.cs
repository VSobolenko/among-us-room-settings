using System.Text.Json;
using System.Text.Json.Serialization;
using AmongUsRoomSettings.AmongUs.Client;

namespace AmongUsRoomSettings.AmongUs.Converters;

internal class RoleOptionsConverterJson : JsonConverter<IRoleOptions>
{
    public override IRoleOptions? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions? options)
    {
        using var doc = JsonDocument.ParseValue(ref reader);
        var json = doc.RootElement.GetRawText();

        if (doc.RootElement.TryGetProperty("Type", out var typeProp))
        {
            var typeString = typeProp.GetString();

            if (string.IsNullOrEmpty(typeString))
                throw new JsonException();

            var type = (RoleTypes)Enum.Parse(typeof(RoleTypes), typeString);

            return type switch
            {
                RoleTypes.Shapeshifter => JsonSerializer.Deserialize<ShapeshifterRoleOptionsV09>(json, options),
                RoleTypes.Scientist => JsonSerializer.Deserialize<ScientistRoleOptionsV09>(json, options),
                RoleTypes.GuardianAngel => JsonSerializer.Deserialize<GuardianAngelRoleOptionsV09>(json, options),
                RoleTypes.Engineer => JsonSerializer.Deserialize<EngineerRoleOptionsV09>(json, options),
                RoleTypes.Noisemaker => JsonSerializer.Deserialize<NoisemakerRoleOptionsV09>(json, options),
                RoleTypes.Phantom => JsonSerializer.Deserialize<PhantomRoleOptionsV09>(json, options),
                RoleTypes.Tracker => JsonSerializer.Deserialize<TrackerRoleOptionsV09>(json, options),
                RoleTypes.Detective => JsonSerializer.Deserialize<DetectiveRoleOptionsV10>(json, options),
                RoleTypes.Viper => JsonSerializer.Deserialize<ViperRoleOptionsV10>(json, options),
                _ => throw new NotSupportedException(typeString)
            };
        }

        throw new InvalidOperationException("Unknown role type");
    }

    public override void Write(Utf8JsonWriter writer, IRoleOptions value, JsonSerializerOptions options)
    {
        JsonSerializer.Serialize(writer, (object)value, options);
    }
}