using System.Globalization;

namespace Chet.Utils;

/// <summary>
/// DateTime 扩展方法类，提供常用的判断、转换、计算、格式化等功能。
/// </summary>
/// <remarks>
/// <para>本类提供丰富的日期时间扩展方法，涵盖以下功能：</para>
/// <list type="bullet">
///   <item><description>基础判断：IsDefault、IsMinValue、IsMaxValue、IsToday 等</description></item>
///   <item><description>日期属性：IsLeapYear、IsWeekend、IsWeekday、IsAM、IsPM 等</description></item>
///   <item><description>时间转换：ToUnixTimestamp、FromUnixTimestamp、ToLocalTime、ToUtcTime 等</description></item>
///   <item><description>格式化输出：ToFormatString、ToIso8601String、ToChineseDateString 等</description></item>
///   <item><description>日期计算：AddDaysSafe、DaysBetween、GetAge、GetQuarter 等</description></item>
///   <item><description>时间范围：IsBetween、IsBefore、IsAfter、GetStartOfDay、GetEndOfDay 等</description></item>
///   <item><description>特殊功能：ToChineseLunarDate、ToJulianDayNumber、ToFriendlyString 等</description></item>
/// </list>
/// </remarks>
public static class DateTimeExtensions
{
    #region 基础判断

    /// <summary>
    /// 判断 DateTime 是否为默认值（0001-01-01 00:00:00）。
    /// </summary>
    /// <param name="dt">待判断的 DateTime。</param>
    /// <returns>如果 DateTime 等于 default(DateTime)，返回 true；否则返回 false。</returns>
    /// <example>
    /// <code>
    /// DateTime dt1 = default;
    /// DateTime dt2 = DateTime.Now;
    /// 
    /// bool result1 = dt1.IsDefault(); // true
    /// bool result2 = dt2.IsDefault(); // false
    /// </code>
    /// </example>
    public static bool IsDefault(this DateTime dt) => dt == default;

    /// <summary>
    /// 判断 DateTime 是否为最小值（0001-01-01 00:00:00）。
    /// </summary>
    /// <param name="dt">待判断的 DateTime。</param>
    /// <returns>如果 DateTime 等于 DateTime.MinValue，返回 true；否则返回 false。</returns>
    /// <example>
    /// <code>
    /// DateTime dt = DateTime.MinValue;
    /// bool result = dt.IsMinValue(); // true
    /// </code>
    /// </example>
    public static bool IsMinValue(this DateTime dt) => dt == DateTime.MinValue;

    /// <summary>
    /// 判断 DateTime 是否为最大值（9999-12-31 23:59:59.9999999）。
    /// </summary>
    /// <param name="dt">待判断的 DateTime。</param>
    /// <returns>如果 DateTime 等于 DateTime.MaxValue，返回 true；否则返回 false。</returns>
    /// <example>
    /// <code>
    /// DateTime dt = DateTime.MaxValue;
    /// bool result = dt.IsMaxValue(); // true
    /// </code>
    /// </example>
    public static bool IsMaxValue(this DateTime dt) => dt == DateTime.MaxValue;

    /// <summary>
    /// 判断 DateTime 是否为今天。
    /// </summary>
    /// <param name="dt">待判断的 DateTime。</param>
    /// <returns>如果 DateTime 的日期部分等于今天的日期，返回 true；否则返回 false。</returns>
    /// <example>
    /// <code>
    /// DateTime dt = DateTime.Now;
    /// bool result = dt.IsToday(); // true
    /// </code>
    /// </example>
    public static bool IsToday(this DateTime dt) => dt.Date == DateTime.Today;

    /// <summary>
    /// 判断 DateTime 是否为昨天。
    /// </summary>
    /// <param name="dt">待判断的 DateTime。</param>
    /// <returns>如果 DateTime 的日期部分等于昨天的日期，返回 true；否则返回 false。</returns>
    public static bool IsYesterday(this DateTime dt) => dt.Date == DateTime.Today.AddDays(-1);

    /// <summary>
    /// 判断 DateTime 是否为明天。
    /// </summary>
    /// <param name="dt">待判断的 DateTime。</param>
    /// <returns>如果 DateTime 的日期部分等于明天的日期，返回 true；否则返回 false。</returns>
    public static bool IsTomorrow(this DateTime dt) => dt.Date == DateTime.Today.AddDays(1);

    /// <summary>
    /// 判断 DateTime 是否为 null 或默认值（针对 Nullable&lt;DateTime&gt;）。
    /// </summary>
    /// <param name="dt">待判断的可空 DateTime。</param>
    /// <returns>如果 DateTime 为 null 或等于默认值，返回 true；否则返回 false。</returns>
    public static bool IsNullOrDefault(this DateTime? dt) => !dt.HasValue || dt.Value == default;

    #endregion

    #region 日期属性判断

    /// <summary>
    /// 判断 DateTime 所在年份是否为闰年。
    /// </summary>
    /// <param name="dt">待判断的 DateTime。</param>
    /// <returns>如果是闰年返回 true；否则返回 false。</returns>
    /// <example>
    /// <code>
    /// DateTime dt = new DateTime(2024, 1, 1);
    /// bool result = dt.IsLeapYear(); // true (2024年是闰年)
    /// </code>
    /// </example>
    public static bool IsLeapYear(this DateTime dt) => DateTime.IsLeapYear(dt.Year);

    /// <summary>
    /// 判断 DateTime 是否为周末（周六或周日）。
    /// </summary>
    /// <param name="dt">待判断的 DateTime。</param>
    /// <returns>如果是周六或周日返回 true；否则返回 false。</returns>
    /// <example>
    /// <code>
    /// DateTime dt = new DateTime(2024, 1, 6); // 周六
    /// bool result = dt.IsWeekend(); // true
    /// </code>
    /// </example>
    public static bool IsWeekend(this DateTime dt) =>
        dt.DayOfWeek == DayOfWeek.Saturday || dt.DayOfWeek == DayOfWeek.Sunday;

    /// <summary>
    /// 判断 DateTime 是否为工作日（周一到周五）。
    /// </summary>
    /// <param name="dt">待判断的 DateTime。</param>
    /// <returns>如果是周一到周五返回 true；否则返回 false。</returns>
    /// <example>
    /// <code>
    /// DateTime dt = new DateTime(2024, 1, 8); // 周一
    /// bool result = dt.IsWeekday(); // true
    /// </code>
    /// </example>
    public static bool IsWeekday(this DateTime dt) =>
        dt.DayOfWeek >= DayOfWeek.Monday && dt.DayOfWeek <= DayOfWeek.Friday;

    /// <summary>
    /// 判断 DateTime 是否为上午（0:00 - 11:59）。
    /// </summary>
    /// <param name="dt">待判断的 DateTime。</param>
    /// <returns>如果小时数小于 12 返回 true；否则返回 false。</returns>
    public static bool IsAM(this DateTime dt) => dt.Hour < 12;

    /// <summary>
    /// 判断 DateTime 是否为下午（12:00 - 23:59）。
    /// </summary>
    /// <param name="dt">待判断的 DateTime。</param>
    /// <returns>如果小时数大于等于 12 返回 true；否则返回 false。</returns>
    public static bool IsPM(this DateTime dt) => dt.Hour >= 12;

    /// <summary>
    /// 判断 DateTime 是否为指定月份的第一天。
    /// </summary>
    /// <param name="dt">待判断的 DateTime。</param>
    /// <returns>如果是当月第一天返回 true；否则返回 false。</returns>
    public static bool IsFirstDayOfMonth(this DateTime dt) => dt.Day == 1;

    /// <summary>
    /// 判断 DateTime 是否为指定月份的最后一天。
    /// </summary>
    /// <param name="dt">待判断的 DateTime。</param>
    /// <returns>如果是当月最后一天返回 true；否则返回 false。</returns>
    public static bool IsLastDayOfMonth(this DateTime dt) => dt.Day == DateTime.DaysInMonth(dt.Year, dt.Month);

    #endregion

    #region 季度和周计算

    /// <summary>
    /// 获取 DateTime 所在的季度（1-4）。
    /// </summary>
    /// <param name="dt">待处理的 DateTime。</param>
    /// <returns>季度数字，范围 1-4。</returns>
    /// <example>
    /// <code>
    /// DateTime dt = new DateTime(2024, 5, 15);
    /// int quarter = dt.GetQuarter(); // 2
    /// </code>
    /// </example>
    public static int GetQuarter(this DateTime dt) => (dt.Month - 1) / 3 + 1;

    /// <summary>
    /// 获取 DateTime 所在季度（GetQuarter 的别名方法）。
    /// </summary>
    /// <param name="dt">待处理的 DateTime。</param>
    /// <returns>季度数，范围 1-4。</returns>
    public static int ToQuarter(this DateTime dt) => dt.GetQuarter();

    /// <summary>
    /// 获取 DateTime 所在季度的字符串表示（如 "Q1"）。
    /// </summary>
    /// <param name="dt">待处理的 DateTime。</param>
    /// <returns>季度字符串，格式为 "Q1"、"Q2"、"Q3" 或 "Q4"。</returns>
    public static string ToQuarterString(this DateTime dt) => $"Q{dt.GetQuarter()}";

    /// <summary>
    /// 获取 DateTime 所在年份的第几周。
    /// </summary>
    /// <param name="dt">待处理的 DateTime。</param>
    /// <param name="rule">确定日历周的规则，默认为当前文化的规则。</param>
    /// <returns>周数，范围 1-53。</returns>
    /// <example>
    /// <code>
    /// DateTime dt = new DateTime(2024, 1, 15);
    /// int week = dt.GetWeekOfYear(); // 返回该日期是当年的第几周
    /// </code>
    /// </example>
    public static int GetWeekOfYear(this DateTime dt, CalendarWeekRule rule = CalendarWeekRule.FirstDay)
    {
        var culture = CultureInfo.CurrentCulture;
        return culture.Calendar.GetWeekOfYear(dt, rule, culture.DateTimeFormat.FirstDayOfWeek);
    }

    /// <summary>
    /// 获取 DateTime 所在月份的第几周。
    /// </summary>
    /// <param name="dt">待处理的 DateTime。</param>
    /// <returns>当月周数，范围 1-6。</returns>
    public static int GetWeekOfMonth(this DateTime dt)
    {
        var firstDayOfMonth = new DateTime(dt.Year, dt.Month, 1);
        var firstDayOfWeek = CultureInfo.CurrentCulture.DateTimeFormat.FirstDayOfWeek;
        var offset = (firstDayOfMonth.DayOfWeek - firstDayOfWeek + 7) % 7;
        return (dt.Day + offset - 1) / 7 + 1;
    }

    /// <summary>
    /// 获取 DateTime 所在季度的第一天。
    /// </summary>
    /// <param name="dt">待处理的 DateTime。</param>
    /// <returns>季度第一天的 DateTime。</returns>
    public static DateTime GetFirstDayOfQuarter(this DateTime dt)
    {
        int quarter = dt.GetQuarter();
        int month = (quarter - 1) * 3 + 1;
        return new DateTime(dt.Year, month, 1);
    }

    /// <summary>
    /// 获取 DateTime 所在季度的最后一天。
    /// </summary>
    /// <param name="dt">待处理的 DateTime。</param>
    /// <returns>季度最后一天的 DateTime。</returns>
    public static DateTime GetLastDayOfQuarter(this DateTime dt)
    {
        int quarter = dt.GetQuarter();
        int month = quarter * 3;
        return new DateTime(dt.Year, month, DateTime.DaysInMonth(dt.Year, month));
    }

    #endregion

    #region 时间转换

    /// <summary>
    /// 将 DateTime 转换为 Unix 时间戳（秒）。
    /// </summary>
    /// <param name="dt">待转换的 DateTime。</param>
    /// <returns>Unix 时间戳（自 1970-01-01 00:00:00 UTC 以来的秒数）。</returns>
    /// <example>
    /// <code>
    /// DateTime dt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc);
    /// long timestamp = dt.ToUnixTimestamp(); // 1704067200
    /// </code>
    /// </example>
    public static long ToUnixTimestamp(this DateTime dt) =>
        new DateTimeOffset(dt).ToUnixTimeSeconds();

    /// <summary>
    /// 将 DateTime 转换为 Unix 时间戳（毫秒）。
    /// </summary>
    /// <param name="dt">待转换的 DateTime。</param>
    /// <returns>Unix 时间戳（自 1970-01-01 00:00:00 UTC 以来的毫秒数）。</returns>
    public static long ToUnixTimestampMs(this DateTime dt) =>
        new DateTimeOffset(dt).ToUnixTimeMilliseconds();

    /// <summary>
    /// 将 Unix 时间戳（秒）转换为 DateTime。
    /// </summary>
    /// <param name="timestamp">Unix 时间戳（秒）。</param>
    /// <returns>本地时间的 DateTime。</returns>
    /// <example>
    /// <code>
    /// long timestamp = 1704067200;
    /// DateTime dt = timestamp.FromUnixTimestamp();
    /// </code>
    /// </example>
    public static DateTime FromUnixTimestamp(this long timestamp) =>
        DateTimeOffset.FromUnixTimeSeconds(timestamp).LocalDateTime;

    /// <summary>
    /// 将 Unix 时间戳（毫秒）转换为 DateTime。
    /// </summary>
    /// <param name="timestampMs">Unix 时间戳（毫秒）。</param>
    /// <returns>本地时间的 DateTime。</returns>
    public static DateTime FromUnixTimestampMs(this long timestampMs) =>
        DateTimeOffset.FromUnixTimeMilliseconds(timestampMs).LocalDateTime;

    /// <summary>
    /// 将 DateTime 转换为本地时间。
    /// </summary>
    /// <param name="dt">待转换的 DateTime。</param>
    /// <returns>本地时间的 DateTime。</returns>
    public static DateTime ToLocalTimeSafe(this DateTime dt) =>
        dt.Kind == DateTimeKind.Local ? dt : dt.ToLocalTime();

    /// <summary>
    /// 将 DateTime 转换为 UTC 时间。
    /// </summary>
    /// <param name="dt">待转换的 DateTime。</param>
    /// <returns>UTC 时间的 DateTime。</returns>
    public static DateTime ToUtcTimeSafe(this DateTime dt) =>
        dt.Kind == DateTimeKind.Utc ? dt : dt.ToUniversalTime();

    /// <summary>
    /// 将 DateTime 转换为指定时区的时间。
    /// </summary>
    /// <param name="dt">待转换的 DateTime。</param>
    /// <param name="timeZoneId">时区标识，如 "China Standard Time"、"Pacific Standard Time"。</param>
    /// <returns>指定时区的 DateTime。</returns>
    /// <exception cref="TimeZoneNotFoundException">时区标识无效时抛出。</exception>
    /// <example>
    /// <code>
    /// DateTime dt = DateTime.UtcNow;
    /// DateTime chinaTime = dt.ToTimeZone("China Standard Time");
    /// </code>
    /// </example>
    public static DateTime ToTimeZone(this DateTime dt, string timeZoneId)
    {
        var tz = TimeZoneInfo.FindSystemTimeZoneById(timeZoneId);
        return TimeZoneInfo.ConvertTime(dt, tz);
    }

    /// <summary>
    /// 将 DateTime 转换为 DateTimeOffset。
    /// </summary>
    /// <param name="dt">待转换的 DateTime。</param>
    /// <returns>DateTimeOffset 对象。</returns>
    public static DateTimeOffset ToDateTimeOffset(this DateTime dt) => new DateTimeOffset(dt);

    #endregion

    #region 格式化输出

    /// <summary>
    /// 将 DateTime 格式化为指定格式的字符串。
    /// </summary>
    /// <param name="dt">待格式化的 DateTime。</param>
    /// <param name="format">格式字符串，如 "yyyy-MM-dd HH:mm:ss"。</param>
    /// <returns>格式化后的日期时间字符串。</returns>
    /// <example>
    /// <code>
    /// DateTime dt = DateTime.Now;
    /// string result = dt.ToFormatString("yyyy年MM月dd日 HH:mm:ss");
    /// </code>
    /// </example>
    public static string ToFormatString(this DateTime dt, string format = "yyyy-MM-dd HH:mm:ss") =>
        dt.ToString(format);

    /// <summary>
    /// 将 DateTime 转换为 ISO8601 格式字符串。
    /// </summary>
    /// <param name="dt">待格式化的 DateTime。</param>
    /// <returns>ISO8601 格式的字符串（如 "2024-01-15T14:30:00.0000000+08:00"）。</returns>
    public static string ToIso8601String(this DateTime dt) => dt.ToString("o");

    /// <summary>
    /// 将 DateTime 转换为 RFC1123 格式字符串。
    /// </summary>
    /// <param name="dt">待格式化的 DateTime。</param>
    /// <returns>RFC1123 格式的字符串（如 "Mon, 15 Jan 2024 14:30:00 GMT"）。</returns>
    public static string ToRfc1123String(this DateTime dt) => dt.ToString("R");

    /// <summary>
    /// 将 DateTime 转换为中文日期字符串。
    /// </summary>
    /// <param name="dt">待格式化的 DateTime。</param>
    /// <returns>中文日期字符串（如 "2024年01月15日"）。</returns>
    public static string ToChineseDateString(this DateTime dt) => dt.ToString("yyyy年MM月dd日");

    /// <summary>
    /// 将 DateTime 转换为中文日期时间字符串。
    /// </summary>
    /// <param name="dt">待格式化的 DateTime。</param>
    /// <returns>中文日期时间字符串（如 "2024年01月15日 14时30分"）。</returns>
    public static string ToChineseDateTimeString(this DateTime dt) => dt.ToString("yyyy年MM月dd日 HH时mm分");

    /// <summary>
    /// 将 DateTime 转换为时间戳字符串。
    /// </summary>
    /// <param name="dt">待格式化的 DateTime。</param>
    /// <returns>时间戳字符串（如 "20240115143000"）。</returns>
    public static string ToTimestampString(this DateTime dt) => dt.ToString("yyyyMMddHHmmss");

    /// <summary>
    /// 将 DateTime 转换为精确到毫秒的时间戳字符串。
    /// </summary>
    /// <param name="dt">待格式化的 DateTime。</param>
    /// <returns>时间戳字符串（如 "20240115143000123"）。</returns>
    public static string ToCustomTimestamp(this DateTime dt) => dt.ToString("yyyyMMddHHmmssfff");

    /// <summary>
    /// 将 DateTime 转换为日期字符串。
    /// </summary>
    /// <param name="dt">待格式化的 DateTime。</param>
    /// <returns>日期字符串（如 "2024-01-15"）。</returns>
    public static string ToDateString(this DateTime dt) => dt.ToString("yyyy-MM-dd");

    /// <summary>
    /// 将 DateTime 转换为时间字符串。
    /// </summary>
    /// <param name="dt">待格式化的 DateTime。</param>
    /// <returns>时间字符串（如 "14:30:00"）。</returns>
    public static string ToTimeString(this DateTime dt) => dt.ToString("HH:mm:ss");

    /// <summary>
    /// 将 DateTime 转换为短时间字符串。
    /// </summary>
    /// <param name="dt">待格式化的 DateTime。</param>
    /// <returns>短时间字符串（如 "14:30"）。</returns>
    public static string ToShortTimeString(this DateTime dt) => dt.ToString("HH:mm");

    /// <summary>
    /// 将 DateTime 转换为星期中文名称。
    /// </summary>
    /// <param name="dt">待格式化的 DateTime。</param>
    /// <returns>星期中文名称（如 "星期一"）。</returns>
    public static string ToChineseWeekday(this DateTime dt)
    {
        var weekDays = new[] { "星期日", "星期一", "星期二", "星期三", "星期四", "星期五", "星期六" };
        return weekDays[(int)dt.DayOfWeek];
    }

    /// <summary>
    /// 将 DateTime 转换为星期英文名称。
    /// </summary>
    /// <param name="dt">待格式化的 DateTime。</param>
    /// <returns>星期英文名称（如 "Monday"）。</returns>
    public static string ToEnglishWeekday(this DateTime dt) => dt.DayOfWeek.ToString();

    /// <summary>
    /// 将 DateTime 转换为星期数字（周日为 0，周一为 1，依此类推）。
    /// </summary>
    /// <param name="dt">待处理的 DateTime。</param>
    /// <returns>星期数字，范围 0-6。</returns>
    public static int ToWeekdayNumber(this DateTime dt) => (int)dt.DayOfWeek;

    #endregion

    #region 日期计算

    /// <summary>
    /// 安全地增加指定天数。
    /// </summary>
    /// <param name="dt">原始 DateTime。</param>
    /// <param name="days">要增加的天数，可为负数。</param>
    /// <returns>增加天数后的 DateTime。</returns>
    public static DateTime AddDaysSafe(this DateTime dt, double days) => dt.AddDays(days);

    /// <summary>
    /// 安全地增加指定小时数。
    /// </summary>
    /// <param name="dt">原始 DateTime。</param>
    /// <param name="hours">要增加的小时数，可为负数。</param>
    /// <returns>增加小时数后的 DateTime。</returns>
    public static DateTime AddHoursSafe(this DateTime dt, double hours) => dt.AddHours(hours);

    /// <summary>
    /// 安全地增加指定分钟数。
    /// </summary>
    /// <param name="dt">原始 DateTime。</param>
    /// <param name="minutes">要增加的分钟数，可为负数。</param>
    /// <returns>增加分钟数后的 DateTime。</returns>
    public static DateTime AddMinutesSafe(this DateTime dt, double minutes) => dt.AddMinutes(minutes);

    /// <summary>
    /// 安全地增加指定秒数。
    /// </summary>
    /// <param name="dt">原始 DateTime。</param>
    /// <param name="seconds">要增加的秒数，可为负数。</param>
    /// <returns>增加秒数后的 DateTime。</returns>
    public static DateTime AddSecondsSafe(this DateTime dt, double seconds) => dt.AddSeconds(seconds);

    /// <summary>
    /// 安全地增加指定月份数。
    /// </summary>
    /// <param name="dt">原始 DateTime。</param>
    /// <param name="months">要增加的月份数，可为负数。</param>
    /// <returns>增加月份数后的 DateTime。</returns>
    public static DateTime AddMonthsSafe(this DateTime dt, int months) => dt.AddMonths(months);

    /// <summary>
    /// 安全地增加指定年份数。
    /// </summary>
    /// <param name="dt">原始 DateTime。</param>
    /// <param name="years">要增加的年份数，可为负数。</param>
    /// <returns>增加年份数后的 DateTime。</returns>
    public static DateTime AddYearsSafe(this DateTime dt, int years) => dt.AddYears(years);

    /// <summary>
    /// 安全地增加指定工作日数（跳过周末）。
    /// </summary>
    /// <param name="dt">原始 DateTime。</param>
    /// <param name="workDays">要增加的工作日数，可为负数。</param>
    /// <returns>增加工作日数后的 DateTime。</returns>
    /// <example>
    /// <code>
    /// DateTime dt = new DateTime(2024, 1, 5); // 周五
    /// DateTime result = dt.AddWorkDays(1); // 返回下周一
    /// </code>
    /// </example>
    public static DateTime AddWorkDays(this DateTime dt, int workDays)
    {
        int direction = workDays < 0 ? -1 : 1;
        int remaining = Math.Abs(workDays);
        DateTime result = dt;

        while (remaining > 0)
        {
            result = result.AddDays(direction);
            if (result.IsWeekday())
                remaining--;
        }

        return result;
    }

    /// <summary>
    /// 获取两个 DateTime 之间的天数差。
    /// </summary>
    /// <param name="dt">起始 DateTime。</param>
    /// <param name="other">结束 DateTime。</param>
    /// <returns>两个日期之间的天数差的绝对值。</returns>
    public static double DaysBetween(this DateTime dt, DateTime other) =>
        Math.Abs((dt.Date - other.Date).TotalDays);

    /// <summary>
    /// 获取两个 DateTime 之间的小时差。
    /// </summary>
    /// <param name="dt">起始 DateTime。</param>
    /// <param name="other">结束 DateTime。</param>
    /// <returns>两个日期时间之间的小时差的绝对值。</returns>
    public static double HoursBetween(this DateTime dt, DateTime other) =>
        Math.Abs((dt - other).TotalHours);

    /// <summary>
    /// 获取两个 DateTime 之间的分钟差。
    /// </summary>
    /// <param name="dt">起始 DateTime。</param>
    /// <param name="other">结束 DateTime。</param>
    /// <returns>两个日期时间之间的分钟差的绝对值。</returns>
    public static double MinutesBetween(this DateTime dt, DateTime other) =>
        Math.Abs((dt - other).TotalMinutes);

    /// <summary>
    /// 获取两个 DateTime 之间的秒数差。
    /// </summary>
    /// <param name="dt">起始 DateTime。</param>
    /// <param name="other">结束 DateTime。</param>
    /// <returns>两个日期时间之间的秒数差的绝对值。</returns>
    public static double SecondsBetween(this DateTime dt, DateTime other) =>
        Math.Abs((dt - other).TotalSeconds);

    /// <summary>
    /// 获取两个 DateTime 之间的时间间隔。
    /// </summary>
    /// <param name="dt">起始 DateTime。</param>
    /// <param name="other">结束 DateTime。</param>
    /// <returns>两个日期时间之间的时间间隔（绝对值）。</returns>
    public static TimeSpan SpanBetween(this DateTime dt, DateTime other) =>
        dt > other ? dt - other : other - dt;

    /// <summary>
    /// 根据出生日期计算年龄。
    /// </summary>
    /// <param name="birthDate">出生日期。</param>
    /// <returns>年龄（周岁），如果出生日期在未来则返回 0。</returns>
    /// <example>
    /// <code>
    /// DateTime birthDate = new DateTime(1990, 5, 15);
    /// int age = birthDate.ToAge(); // 返回当前年龄
    /// </code>
    /// </example>
    public static int ToAge(this DateTime birthDate)
    {
        var now = DateTime.Now;
        int age = now.Year - birthDate.Year;
        if (now.Month < birthDate.Month || (now.Month == birthDate.Month && now.Day < birthDate.Day))
            age--;
        return age < 0 ? 0 : age;
    }

    /// <summary>
    /// 根据出生日期计算精确年龄（包含月和天）。
    /// </summary>
    /// <param name="birthDate">出生日期。</param>
    /// <returns>包含年、月、日的元组。</returns>
    public static (int Years, int Months, int Days) ToExactAge(this DateTime birthDate)
    {
        var now = DateTime.Now;
        int years = now.Year - birthDate.Year;
        int months = now.Month - birthDate.Month;
        int days = now.Day - birthDate.Day;

        if (days < 0)
        {
            months--;
            days += DateTime.DaysInMonth(now.Year, now.Month == 1 ? 12 : now.Month - 1);
        }

        if (months < 0)
        {
            years--;
            months += 12;
        }

        return years < 0 ? (0, 0, 0) : (years, months, days);
    }

    #endregion

    #region 时间范围

    /// <summary>
    /// 判断 DateTime 是否在指定范围内（包含边界）。
    /// </summary>
    /// <param name="dt">待判断的 DateTime。</param>
    /// <param name="start">起始时间。</param>
    /// <param name="end">结束时间。</param>
    /// <returns>如果 DateTime 在范围内返回 true；否则返回 false。</returns>
    public static bool IsBetween(this DateTime dt, DateTime start, DateTime end) =>
        dt >= start && dt <= end;

    /// <summary>
    /// 判断 DateTime 是否早于指定时间。
    /// </summary>
    /// <param name="dt">待判断的 DateTime。</param>
    /// <param name="other">比较的时间。</param>
    /// <returns>如果早于指定时间返回 true；否则返回 false。</returns>
    public static bool IsBefore(this DateTime dt, DateTime other) => dt < other;

    /// <summary>
    /// 判断 DateTime 是否晚于指定时间。
    /// </summary>
    /// <param name="dt">待判断的 DateTime。</param>
    /// <param name="other">比较的时间。</param>
    /// <returns>如果晚于指定时间返回 true；否则返回 false。</returns>
    public static bool IsAfter(this DateTime dt, DateTime other) => dt > other;

    /// <summary>
    /// 获取当天的开始时间（00:00:00）。
    /// </summary>
    /// <param name="dt">待处理的 DateTime。</param>
    /// <returns>当天的开始时间。</returns>
    public static DateTime GetStartOfDay(this DateTime dt) => dt.Date;

    /// <summary>
    /// 获取当天的结束时间（23:59:59.9999999）。
    /// </summary>
    /// <param name="dt">待处理的 DateTime。</param>
    /// <returns>当天的结束时间。</returns>
    public static DateTime GetEndOfDay(this DateTime dt) => dt.Date.AddDays(1).AddTicks(-1);

    /// <summary>
    /// 获取当周的第一天（周一）。
    /// </summary>
    /// <param name="dt">待处理的 DateTime。</param>
    /// <param name="startOfWeek">每周的第一天，默认为周一。</param>
    /// <returns>当周第一天的日期。</returns>
    public static DateTime GetStartOfWeek(this DateTime dt, DayOfWeek startOfWeek = DayOfWeek.Monday)
    {
        int diff = (7 + (dt.DayOfWeek - startOfWeek)) % 7;
        return dt.Date.AddDays(-diff);
    }

    /// <summary>
    /// 获取当周的最后一天（周日）。
    /// </summary>
    /// <param name="dt">待处理的 DateTime。</param>
    /// <param name="startOfWeek">每周的第一天，默认为周一。</param>
    /// <returns>当周最后一天的日期。</returns>
    public static DateTime GetEndOfWeek(this DateTime dt, DayOfWeek startOfWeek = DayOfWeek.Monday)
    {
        return dt.GetStartOfWeek(startOfWeek).AddDays(6);
    }

    /// <summary>
    /// 获取当月的第一天。
    /// </summary>
    /// <param name="dt">待处理的 DateTime。</param>
    /// <returns>当月第一天的日期。</returns>
    public static DateTime GetStartOfMonth(this DateTime dt) => new DateTime(dt.Year, dt.Month, 1);

    /// <summary>
    /// 获取当月的最后一天。
    /// </summary>
    /// <param name="dt">待处理的 DateTime。</param>
    /// <returns>当月最后一天的日期。</returns>
    public static DateTime GetEndOfMonth(this DateTime dt) =>
        new DateTime(dt.Year, dt.Month, DateTime.DaysInMonth(dt.Year, dt.Month));

    /// <summary>
    /// 获取当年的第一天。
    /// </summary>
    /// <param name="dt">待处理的 DateTime。</param>
    /// <returns>当年第一天的日期。</returns>
    public static DateTime GetStartOfYear(this DateTime dt) => new DateTime(dt.Year, 1, 1);

    /// <summary>
    /// 获取当年的最后一天。
    /// </summary>
    /// <param name="dt">待处理的 DateTime。</param>
    /// <returns>当年最后一天的日期。</returns>
    public static DateTime GetEndOfYear(this DateTime dt) => new DateTime(dt.Year, 12, 31);

    #endregion

    #region 特殊功能

    /// <summary>
    /// 将 DateTime 转换为农历日期字符串。
    /// </summary>
    /// <param name="dt">待格式化的 DateTime。</param>
    /// <returns>农历日期字符串（如 "正月初一"）。</returns>
    /// <remarks>仅支持中国农历，需引用 System.Globalization.ChineseLunisolarCalendar。</remarks>
    public static string ToChineseLunarDate(this DateTime dt)
    {
        var calendar = new ChineseLunisolarCalendar();
        int year = calendar.GetYear(dt);
        int month = calendar.GetMonth(dt);
        int day = calendar.GetDayOfMonth(dt);
        string[] months = { "", "正月", "二月", "三月", "四月", "五月", "六月", "七月", "八月", "九月", "十月", "冬月", "腊月" };
        string[] days = { "", "初一", "初二", "初三", "初四", "初五", "初六", "初七", "初八", "初九", "初十",
            "十一", "十二", "十三", "十四", "十五", "十六", "十七", "十八", "十九", "二十",
            "廿一", "廿二", "廿三", "廿四", "廿五", "廿六", "廿七", "廿八", "廿九", "三十" };
        return $"{months[month]}{days[day]}";
    }

    /// <summary>
    /// 将 DateTime 转换为友好时间描述。
    /// </summary>
    /// <param name="dt">待处理的 DateTime。</param>
    /// <returns>友好时间描述（如 "刚刚"、"5分钟前"、"昨天"、"3天前"）。</returns>
    /// <example>
    /// <code>
    /// DateTime dt = DateTime.Now.AddMinutes(-5);
    /// string result = dt.ToFriendlyString(); // "5分钟前"
    /// </code>
    /// </example>
    public static string ToFriendlyString(this DateTime dt)
    {
        var span = DateTime.Now - dt;
        if (span.TotalSeconds < 60) return "刚刚";
        if (span.TotalMinutes < 60) return $"{(int)span.TotalMinutes}分钟前";
        if (span.TotalHours < 24) return $"{(int)span.TotalHours}小时前";
        if (span.TotalDays < 2) return "昨天";
        if (span.TotalDays < 30) return $"{(int)span.TotalDays}天前";
        if (span.TotalDays < 365) return $"{(int)(span.TotalDays / 30)}个月前";
        return $"{(int)(span.TotalDays / 365)}年前";
    }

    /// <summary>
    /// 将 DateTime 转换为民国纪年字符串。
    /// </summary>
    /// <param name="dt">待格式化的 DateTime。</param>
    /// <returns>民国纪年字符串（如 "民国113年01月15日"）。</returns>
    public static string ToMinguoString(this DateTime dt)
    {
        int year = dt.Year - 1911;
        return $"民国{year}年{dt.Month:D2}月{dt.Day:D2}日";
    }

    /// <summary>
    /// 将 DateTime 转换为儒略日号（Julian Day Number）。
    /// </summary>
    /// <param name="dt">待处理的 DateTime。</param>
    /// <returns>儒略日号。</returns>
    public static double ToJulianDayNumber(this DateTime dt)
    {
        int y = dt.Year;
        int m = dt.Month;
        int d = dt.Day;
        if (m <= 2)
        {
            y -= 1;
            m += 12;
        }
        int A = y / 100;
        int B = 2 - A + A / 4;
        double JD = Math.Floor(365.25 * (y + 4716))
                    + Math.Floor(30.6001 * (m + 1))
                    + d + B - 1524.5
                    + (dt.Hour + dt.Minute / 60.0 + dt.Second / 3600.0) / 24.0;
        return JD;
    }

    #endregion

    #region 元组转换

    /// <summary>
    /// 将 DateTime 转换为年月日元组。
    /// </summary>
    /// <param name="dt">待处理的 DateTime。</param>
    /// <returns>包含年、月、日的元组。</returns>
    public static (int Year, int Month, int Day) ToYMD(this DateTime dt) =>
        (dt.Year, dt.Month, dt.Day);

    /// <summary>
    /// 将 DateTime 转换为时分秒元组。
    /// </summary>
    /// <param name="dt">待处理的 DateTime。</param>
    /// <returns>包含时、分、秒的元组。</returns>
    public static (int Hour, int Minute, int Second) ToHMS(this DateTime dt) =>
        (dt.Hour, dt.Minute, dt.Second);

    /// <summary>
    /// 将 DateTime 转换为年月日时分秒元组。
    /// </summary>
    /// <param name="dt">待处理的 DateTime。</param>
    /// <returns>包含年、月、日、时、分、秒的元组。</returns>
    public static (int Year, int Month, int Day, int Hour, int Minute, int Second) ToYMDHMS(this DateTime dt) =>
        (dt.Year, dt.Month, dt.Day, dt.Hour, dt.Minute, dt.Second);

    #endregion

    #region .NET 6+ 类型转换

    /// <summary>
    /// 将 DateTime 转换为 DateOnly。
    /// </summary>
    /// <param name="dt">待转换的 DateTime。</param>
    /// <returns>DateOnly 对象。</returns>
    public static DateOnly ToDateOnly(this DateTime dt) => DateOnly.FromDateTime(dt);

    /// <summary>
    /// 将 DateTime 转换为 TimeOnly。
    /// </summary>
    /// <param name="dt">待转换的 DateTime。</param>
    /// <returns>TimeOnly 对象。</returns>
    public static TimeOnly ToTimeOnly(this DateTime dt) => TimeOnly.FromDateTime(dt);

    #endregion
}
