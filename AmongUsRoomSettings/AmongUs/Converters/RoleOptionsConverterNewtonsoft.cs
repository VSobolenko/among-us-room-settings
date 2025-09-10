using AmongUsRoomSettings.AmongUs.Client;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace AmongUsRoomSettings.AmongUs.Converters;

internal class RoleOptionsConverterNewtonsoft : JsonConverter<IRoleOptions>
{
    public override bool CanWrite => true;

    public override IRoleOptions? ReadJson(JsonReader reader, Type objectType, IRoleOptions? existingValue, bool hasExistingValue, JsonSerializer serializer)
    {
        var jo = JObject.Load(reader);

        if (!jo.TryGetValue("Type", out var typeToken))
            throw new JsonSerializationException("Missing Type property");

        var typeString = typeToken.ToString();
        if (string.IsNullOrEmpty(typeString))
            throw new JsonSerializationException("Invalid Type property");

        var type = (RoleTypes)Enum.Parse(typeof(RoleTypes), typeString);

        return type switch
        {
            RoleTypes.Shapeshifter => jo.ToObject<ShapeshifterRoleOptionsV09>(serializer),
            RoleTypes.Scientist => jo.ToObject<ScientistRoleOptionsV09>(serializer),
            RoleTypes.GuardianAngel => jo.ToObject<GuardianAngelRoleOptionsV09>(serializer),
            RoleTypes.Engineer => jo.ToObject<EngineerRoleOptionsV09>(serializer),
            RoleTypes.Noisemaker => jo.ToObject<NoisemakerRoleOptionsV09>(serializer),
            RoleTypes.Phantom => jo.ToObject<PhantomRoleOptionsV09>(serializer),
            RoleTypes.Tracker => jo.ToObject<TrackerRoleOptionsV09>(serializer),
            RoleTypes.Detective => jo.ToObject<DetectiveRoleOptionsV10>(serializer),
            RoleTypes.Viper => jo.ToObject<ViperRoleOptionsV10>(serializer),
            _ => throw new NotSupportedException(typeString)
        };
    }

    public override void WriteJson(JsonWriter writer, IRoleOptions value, JsonSerializer serializer)
    {
        serializer.Serialize(writer, (object)value);
    }
}