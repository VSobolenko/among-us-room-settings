using System.Globalization;
using Microsoft.AspNetCore.Mvc;

namespace AmongUsRoomSettings.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AccessController : ControllerBase
{
    [HttpPost("check")]
    public IActionResult CheckKey([FromBody] KeyRequest request)
    {
        if (AccessKeys.IsValid(request.Key))
        {
            return Ok(new { Success = true, Message = "Ключ верный" });
        }
        else
        {
            return Unauthorized(new { Success = false, Message = "Ключ неверный" });
        }
    }
}

public class KeyRequest
{
    public string Key { get; set; } = "";
}

public static class AccessKeys
{
    private static readonly Dictionary<DayOfWeek, (int Even, int Odd)> ScheduleStrings = new()
    {
        //Чётный - нечётный
        { DayOfWeek.Monday,    (1234, 5678) },
        { DayOfWeek.Tuesday,   (2345, 6789) },
        { DayOfWeek.Wednesday, (3456, 7890) },
        { DayOfWeek.Thursday,  (4567, 8901) },
        { DayOfWeek.Friday,    (5678, 9012) },
        { DayOfWeek.Saturday,  (6789, 1235) },
        { DayOfWeek.Sunday,    (7890, 2346) },
    };


    public static bool IsValid(string key)
    {
        var dayOfWeek = GetDayOfWeek();
        var day = GetCalendarDay();
        var password = GetDayPassword(dayOfWeek, day);
        
        return key == password.ToString();
    }

    public static int GetDayPassword(DayOfWeek dayOfWeek, int dayNumber)
    {
        var isEven = dayNumber % 2 == 0;
        if (ScheduleStrings.TryGetValue(dayOfWeek, out var values))
        {
            return isEven ? values.Even : values.Odd;
        }

        throw new ArgumentException();
    }

    public static int GetCalendarDay()
    {
        var moscowNow = DateTime.UtcNow.AddHours(3);
        var calendar = CultureInfo.InvariantCulture.Calendar;
        return calendar.GetWeekOfYear(
            moscowNow,
            CalendarWeekRule.FirstFourDayWeek,
            DayOfWeek.Monday
        );
    }

    public static DayOfWeek GetDayOfWeek()
    {
        var moscowNow = DateTime.UtcNow.AddHours(3);
        return moscowNow.DayOfWeek;
    }
}