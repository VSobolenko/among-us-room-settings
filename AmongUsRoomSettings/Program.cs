using System.Text.Json;
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

app.UseDefaultFiles(); // ищет index.html
app.UseStaticFiles();  // отдаёт файлы из wwwroot
app.MapControllers();

app.MapGet("/", () => "Сервер работает!");

app.MapPost("/encode", async (HttpContext context) =>
{
    var rawJson = await new StreamReader(context.Request.Body).ReadToEndAsync();
    
    try
    {
        var options = JsonSerializer.Deserialize<NormalGameOptionsV09>(rawJson);
        if (options is null)
            return Results.BadRequest("Неверные данные");

        string encoded = new RoomSettings().GetBase64String(options);
        return Results.Ok(encoded);
    }
    catch (Exception ex)
    {
        Console.WriteLine("Ошибка при десериализации: " + ex.Message);
        return Results.BadRequest("Ошибка десериализации JSON");
    }
});

app.Run();