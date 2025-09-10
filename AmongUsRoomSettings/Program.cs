using System.Text.Json;
using System.Text.Json.Serialization;
using AmongUsRoomSettings.AmongUs;
using AmongUsRoomSettings.AmongUs.Client;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseDefaultFiles();
app.UseStaticFiles();
app.MapControllers();

app.MapGet("/", () => "Server started!");

app.MapPost("/encode", async (HttpContext context) =>
{
    var rawJson = await new StreamReader(context.Request.Body).ReadToEndAsync();

    try
    {
        var options = JsonSerializer.Deserialize<NormalGameOptionsV09>(rawJson, new JsonSerializerOptions
        {
            Converters = { new JsonStringEnumConverter() }
        });
        
        if (options is null)
            return Results.BadRequest("Неверные данные");

        var encoded = new RoomSettings().GetBase64String(options);
        return Results.Ok(encoded);
    }
    catch (Exception ex)
    {
        Console.WriteLine("Deserialize error: " + ex.Message);
        return Results.BadRequest("Deserialize JSON error");
    }
});

app.Run();