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
        //Even - odd
        { DayOfWeek.Monday,    (4821, 7364) },
        { DayOfWeek.Tuesday,   (1957, 8642) },
        { DayOfWeek.Wednesday, (3084, 5271) },
        { DayOfWeek.Thursday,  (6712, 4398) },
        { DayOfWeek.Friday,    (2549, 9863) },
        { DayOfWeek.Saturday,  (8437, 1205) },
        { DayOfWeek.Sunday,    (6174, 3928) },
    };
    
    public static bool IsValid(string key)
    {
        var dayOfWeek = GetDayOfWeek();
        var day = GetCalendarDay();
        var password = GetDayPassword(dayOfWeek, day);
        
        return key == password.ToString();
    }

    private static int GetDayPassword(DayOfWeek dayOfWeek, int dayNumber)
    {
        var isEven = dayNumber % 2 == 0;
        if (ScheduleStrings.TryGetValue(dayOfWeek, out var values))
        {
            return isEven ? values.Even : values.Odd;
        }

        throw new ArgumentException();
    }

    private static int GetCalendarDay()
    {
        var moscowNow = DateTime.UtcNow.AddHours(3);
        var calendar = CultureInfo.InvariantCulture.Calendar;
        return calendar.GetWeekOfYear(
            moscowNow,
            CalendarWeekRule.FirstFourDayWeek,
            DayOfWeek.Monday
        );
    }

    private static DayOfWeek GetDayOfWeek()
    {
        var moscowNow = DateTime.UtcNow.AddHours(3);
        return moscowNow.DayOfWeek;
    }
}