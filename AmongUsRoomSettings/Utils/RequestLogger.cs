using System.Text.Json;

namespace AmongUsRoomSettings.Utils;

internal static class RequestLogger
{
    public static void LogRequest(HttpContext context, bool isIndented)
    {
        try
        {
            InternalLogRequest(context, isIndented);
        }
        catch (Exception e)
        {
            Console.WriteLine($"[RequestLogger] Logging error: {e.Message}");
        }
    }

    private static void InternalLogRequest(HttpContext context, bool isIndented)
    {
        var headers = new Dictionary<string, string>();

        TryAddHeader("User-Agent");
        TryAddHeader("Referer");
        TryAddHeader("Authorization");
        TryAddHeader("X-Forwarded-For");

        var info = new
        {
            RemoteIp = context.Connection.RemoteIpAddress?.ToString(),
            RemotePort = context.Connection.RemotePort,
            LocalIp = context.Connection.LocalIpAddress?.ToString(),
            LocalPort = context.Connection.LocalPort,
            Protocol = context.Request.Protocol,
            Headers = headers,
            Cookies = context.Request.Cookies.ToDictionary(c => c.Key, c => c.Value)
        };

        var json = JsonSerializer.Serialize(info, new JsonSerializerOptions
        {
            WriteIndented = isIndented,
        });

        Console.WriteLine(json);
        return;

        void TryAddHeader(string name)
        {
            if (context.Request.Headers.TryGetValue(name, out var value))
                headers[name] = value.ToString();
        }
    }
}