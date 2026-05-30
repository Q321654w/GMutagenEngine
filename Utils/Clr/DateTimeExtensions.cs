using System.Runtime.CompilerServices;

namespace Utils.Clr;

/// <summary>
/// Оптимизированные расширения для работы с датой и временем
/// </summary>
public static class DateTimeExtensions
{
    #region Базовые проверки

    /// <summary>
    /// Проверяет, находится ли дата в указанном диапазоне (включительно)
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsInRange(this DateTime date, DateTime start, DateTime end) => 
        date >= start && date <= end;

    /// <summary>
    /// Проверяет, является ли дата сегодняшним днем
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsToday(this DateTime date) => date.Date == DateTime.Today;

    /// <summary>
    /// Проверяет, является ли дата рабочим днем (понедельник - пятница)
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsWeekday(this DateTime date) => 
        date.DayOfWeek != DayOfWeek.Saturday && date.DayOfWeek != DayOfWeek.Sunday;

    /// <summary>
    /// Проверяет, является ли дата выходным днем
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsWeekend(this DateTime date) => 
        date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday;

    /// <summary>
    /// Проверяет, является ли год високосным
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsLeapYear(this DateTime date) => 
        DateTime.IsLeapYear(date.Year);

    #endregion

    #region Начало/конец периодов

    /// <summary>
    /// Возвращает начало дня (00:00:00)
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static DateTime StartOfDay(this DateTime date) => date.Date;

    /// <summary>
    /// Возвращает конец дня (23:59:59.999)
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static DateTime EndOfDay(this DateTime date) => 
        date.Date.AddDays(1).AddTicks(-1);

    /// <summary>
    /// Возвращает начало недели (понедельник)
    /// </summary>
    public static DateTime StartOfWeek(this DateTime date, DayOfWeek startOfWeek = DayOfWeek.Monday)
    {
        var diff = (7 + (date.DayOfWeek - startOfWeek)) % 7;
        return date.AddDays(-1 * diff).Date;
    }

    /// <summary>
    /// Возвращает конец недели (воскресенье 23:59:59.999)
    /// </summary>
    public static DateTime EndOfWeek(this DateTime date, DayOfWeek startOfWeek = DayOfWeek.Monday) => 
        date.StartOfWeek(startOfWeek).AddDays(6).EndOfDay();

    /// <summary>
    /// Возвращает начало месяца
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static DateTime StartOfMonth(this DateTime date) => 
        new DateTime(date.Year, date.Month, 1);

    /// <summary>
    /// Возвращает конец месяца
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static DateTime EndOfMonth(this DateTime date) => 
        new DateTime(date.Year, date.Month, DateTime.DaysInMonth(date.Year, date.Month)).EndOfDay();

    /// <summary>
    /// Возвращает начало года
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static DateTime StartOfYear(this DateTime date) => 
        new DateTime(date.Year, 1, 1);

    /// <summary>
    /// Возвращает конец года
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static DateTime EndOfYear(this DateTime date) => 
        new DateTime(date.Year, 12, 31).EndOfDay();

    #endregion

    #region Арифметика дат

    /// <summary>
    /// Добавляет рабочие дни к дате (исключая выходные)
    /// </summary>
    public static DateTime AddBusinessDays(this DateTime date, int days)
    {
        var sign = Math.Sign(days);
        var absoluteDays = Math.Abs(days);
        var currentDate = date;

        while (absoluteDays > 0)
        {
            currentDate = currentDate.AddDays(sign);
            if (currentDate.IsWeekday())
                absoluteDays--;
        }

        return currentDate;
    }

    /// <summary>
    /// Вычисляет возраст в годах на указанную дату
    /// </summary>
    public static int GetAge(this DateTime birthDate, DateTime? referenceDate = null)
    {
        var refDate = referenceDate ?? DateTime.Today;
        var age = refDate.Year - birthDate.Year;
        if (birthDate.Date > refDate.AddYears(-age)) 
            age--;
        return age;
    }

    /// <summary>
    /// Возвращает разницу во времени в читаемом формате
    /// </summary>
    public static string ToRelativeTime(this DateTime date, DateTime? referenceDate = null)
    {
        var refDate = referenceDate ?? DateTime.Now;
        var timeSpan = refDate - date;

        if (timeSpan.TotalSeconds < 0)
            return "in the future";

        if (timeSpan.TotalSeconds < 60)
            return "just now";

        if (timeSpan.TotalMinutes < 60)
            return $"{(int)timeSpan.TotalMinutes} minute{(timeSpan.TotalMinutes >= 2 ? "s" : "")} ago";

        if (timeSpan.TotalHours < 24)
            return $"{(int)timeSpan.TotalHours} hour{(timeSpan.TotalHours >= 2 ? "s" : "")} ago";

        if (timeSpan.TotalDays < 30)
            return $"{(int)timeSpan.TotalDays} day{(timeSpan.TotalDays >= 2 ? "s" : "")} ago";

        if (timeSpan.TotalDays < 365)
        {
            var months = (int)(timeSpan.TotalDays / 30);
            return $"{months} month{(months >= 2 ? "s" : "")} ago";
        }

        var years = (int)(timeSpan.TotalDays / 365);
        return $"{years} year{(years >= 2 ? "s" : "")} ago";
    }

    #endregion

    #region Форматирование

    /// <summary>
    /// Форматирует дату в ISO 8601 строку
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static string ToIsoString(this DateTime date) => 
        date.ToString("yyyy-MM-ddTHH:mm:ss.fffZ");

    /// <summary>
    /// Форматирует дату в короткую строку с временем
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static string ToShortDateTimeString(this DateTime date) => 
        date.ToString("g");

    /// <summary>
    /// Форматирует только время
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static string ToTimeString(this DateTime date) => 
        date.ToString("HH:mm:ss");

    #endregion

    #region Unix Time

    /// <summary>
    /// Конвертирует DateTime в Unix time seconds
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static long ToUnixTimeSeconds(this DateTime date) => 
        new DateTimeOffset(date).ToUnixTimeSeconds();

    /// <summary>
    /// Конвертирует DateTime в Unix time milliseconds
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static long ToUnixTimeMilliseconds(this DateTime date) => 
        new DateTimeOffset(date).ToUnixTimeMilliseconds();

    /// <summary>
    /// Создает DateTime из Unix time seconds
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static DateTime FromUnixTimeSeconds(this long unixTime) => 
        DateTimeOffset.FromUnixTimeSeconds(unixTime).DateTime;

    /// <summary>
    /// Создает DateTime из Unix time milliseconds
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static DateTime FromUnixTimeMilliseconds(this long unixTime) => 
        DateTimeOffset.FromUnixTimeMilliseconds(unixTime).DateTime;

    #endregion
}