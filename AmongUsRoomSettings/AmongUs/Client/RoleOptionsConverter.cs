using System.Text.Json;
using System.Text.Json.Serialization;

namespace AmongUsRoomSettings.AmongUs.Client;

internal class RoleOptionsConverter : JsonConverter<IRoleOptions>
{
    public override IRoleOptions? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions? options)
    {
        using var doc = JsonDocument.ParseValue(ref reader);
        var json = doc.RootElement.GetRawText();

        if (doc.RootElement.TryGetProperty("Type", out var typeProp))
        {
            string type = typeProp.GetString();
            return type switch
            {
                "Shapeshifter" => JsonSerializer.Deserialize<ShapeshifterRoleOptionsV09>(json, options),
                "Scientist" => JsonSerializer.Deserialize<ScientistRoleOptionsV09>(json, options),
                "GuardianAngel" => JsonSerializer.Deserialize<GuardianAngelRoleOptionsV09>(json, options),
                "Engineer" => JsonSerializer.Deserialize<EngineerRoleOptionsV09>(json, options),
                "Noisemaker" => JsonSerializer.Deserialize<NoisemakerRoleOptionsV09>(json, options),
                "Phantom" => JsonSerializer.Deserialize<PhantomRoleOptionsV09>(json, options),
                "Tracker" => JsonSerializer.Deserialize<TrackerRoleOptionsV09>(json, options),
                _ => throw new NotSupportedException(type)
            };
        }
        throw new InvalidOperationException("Не удалось определить тип роли");
    }

    public override void Write(Utf8JsonWriter writer, IRoleOptions value, JsonSerializerOptions options)
    {
        JsonSerializer.Serialize(writer, (object)value, options);
    }
}
