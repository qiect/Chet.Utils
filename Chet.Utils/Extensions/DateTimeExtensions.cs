using System.Globalization;

namespace Chet.Utils
{
    /// <summary>
    /// DateTime 扩展方法类，提供常用的判断、转换、计算、格式化等功能。
    /// </summary>
    public static class DateTimeExtensions
    {
        /// <summary>
        /// 判断 DateTime 是否为默认值（未初始化）。
        /// </summary>
        /// <param name="dt">待判断的 DateTime。</param>
        public static bool IsDefault(this DateTime dt) => dt == default;

        /// <summary>
        /// 判断 DateTime 是否为最小值。
        /// </summary>
        /// <param name="dt">待判断的 DateTime。</param>
        public static bool IsMinValue(this DateTime dt) => dt == DateTime.MinValue;

        /// <summary>
        /// 判断 DateTime 是否为最大值。
        /// </summary>
        /// <param name="dt">待判断的 DateTime。</param>
        public static bool IsMaxValue(this DateTime dt) => dt == DateTime.MaxValue;

        /// <summary>
        /// 判断 DateTime 是否为今天。
        /// </summary>
        /// <param name="dt">待判断的 DateTime。</param>
        public static bool IsToday(this DateTime dt) => dt.Date == DateTime.Today;

        /// <summary>
        /// 判断 DateTime 是否为闰年。
        /// </summary>
        /// <param name="dt">待判断的 DateTime。</param>
        public static bool IsLeapYear(this DateTime dt) => DateTime.IsLeapYear(dt.Year);

        /// <summary>
        /// 判断 DateTime 是否为周末（周六或周日）。
        /// </summary>
        /// <param name="dt">待判断的 DateTime。</param>
        public static bool IsWeekend(this DateTime dt) =>
            dt.DayOfWeek == DayOfWeek.Saturday || dt.DayOfWeek == DayOfWeek.Sunday;

        /// <summary>
        /// 判断 DateTime 是否为工作日（周一到周五）。
        /// </summary>
        /// <param name="dt">待判断的 DateTime。</param>
        public static bool IsWeekday(this DateTime dt) =>
            dt.DayOfWeek >= DayOfWeek.Monday && dt.DayOfWeek <= DayOfWeek.Friday;

        /// <summary>
        /// DateTime 转为 Unix 时间戳（秒）。
        /// </summary>
        /// <param name="dt">待转换的 DateTime。</param>
        public static long ToUnixTimestamp(this DateTime dt) =>
            new DateTimeOffset(dt).ToUnixTimeSeconds();

        /// <summary>
        /// DateTime 转为 Unix 时间戳（毫秒）。
        /// </summary>
        /// <param name="dt">待转换的 DateTime。</param>
        public static long ToUnixTimestampMs(this DateTime dt) =>
            new DateTimeOffset(dt).ToUnixTimeMilliseconds();

        /// <summary>
        /// Unix 时间戳（秒）转为 DateTime。
        /// </summary>
        /// <param name="timestamp">Unix 时间戳（秒）。</param>
        public static DateTime FromUnixTimestamp(this long timestamp) =>
            DateTimeOffset.FromUnixTimeSeconds(timestamp).LocalDateTime;

        /// <summary>
        /// Unix 时间戳（毫秒）转为 DateTime。
        /// </summary>
        /// <param name="timestampMs">Unix 时间戳（毫秒）。</param>
        public static DateTime FromUnixTimestampMs(this long timestampMs) =>
            DateTimeOffset.FromUnixTimeMilliseconds(timestampMs).LocalDateTime;

        /// <summary>
        /// DateTime 转为指定格式字符串。
        /// </summary>
        /// <param name="dt">待格式化的 DateTime。</param>
        /// <param name="format">格式字符串，如 "yyyy-MM-dd HH:mm:ss"。</param>
        public static string ToFormatString(this DateTime dt, string format = "yyyy-MM-dd HH:mm:ss") =>
            dt.ToString(format);

        /// <summary>
        /// DateTime 转为 ISO8601 格式字符串。
        /// </summary>
        /// <param name="dt">待格式化的 DateTime。</param>
        public static string ToIso8601String(this DateTime dt) =>
            dt.ToString("o");

        /// <summary>
        /// DateTime 转为 RFC1123 格式字符串。
        /// </summary>
        /// <param name="dt">待格式化的 DateTime。</param>
        public static string ToRfc1123String(this DateTime dt) =>
            dt.ToString("R");

        /// <summary>
        /// DateTime 转为中文日期字符串（如 "2025年09月30日"）。
        /// </summary>
        /// <param name="dt">待格式化的 DateTime。</param>
        public static string ToChineseDateString(this DateTime dt) =>
            dt.ToString("yyyy年MM月dd日");

        /// <summary>
        /// DateTime 转为中文日期时间字符串（如 "2025年09月30日 14时30分"）。
        /// </summary>
        /// <param name="dt">待格式化的 DateTime。</param>
        public static string ToChineseDateTimeString(this DateTime dt) =>
            dt.ToString("yyyy年MM月dd日 HH时mm分");

        /// <summary>
        /// DateTime 转为时间戳字符串（如 "20250930143000"）。
        /// </summary>
        /// <param name="dt">待格式化的 DateTime。</param>
        public static string ToTimestampString(this DateTime dt) =>
            dt.ToString("yyyyMMddHHmmss");

        /// <summary>
        /// DateTime 转为日期字符串（如 "2025-09-30"）。
        /// </summary>
        /// <param name="dt">待格式化的 DateTime。</param>
        public static string ToDateString(this DateTime dt) =>
            dt.ToString("yyyy-MM-dd");

        /// <summary>
        /// DateTime 转为时间字符串（如 "14:30:00"）。
        /// </summary>
        /// <param name="dt">待格式化的 DateTime。</param>
        public static string ToTimeString(this DateTime dt) =>
            dt.ToString("HH:mm:ss");

        /// <summary>
        /// DateTime 转为短时间字符串（如 "14:30"）。
        /// </summary>
        /// <param name="dt">待格式化的 DateTime。</param>
        public static string ToShortTimeString(this DateTime dt) =>
            dt.ToString("HH:mm");

        /// <summary>
        /// DateTime 转为星期中文（如 "星期一"）。
        /// </summary>
        /// <param name="dt">待格式化的 DateTime。</param>
        public static string ToChineseWeekday(this DateTime dt)
        {
            var weekDays = new[] { "星期日", "星期一", "星期二", "星期三", "星期四", "星期五", "星期六" };
            return weekDays[(int)dt.DayOfWeek];
        }

        /// <summary>
        /// DateTime 转为英文星期（如 "Monday"）。
        /// </summary>
        /// <param name="dt">待格式化的 DateTime。</param>
        public static string ToEnglishWeekday(this DateTime dt) =>
            dt.DayOfWeek.ToString();

        /// <summary>
        /// DateTime 转为季度（如 "Q1"）。
        /// </summary>
        /// <param name="dt">待处理的 DateTime。</param>
        public static string ToQuarter(this DateTime dt)
        {
            int quarter = (dt.Month - 1) / 3 + 1;
            return $"Q{quarter}";
        }

        /// <summary>
        /// DateTime 转为农历日期字符串（如 "正月初一"）。
        /// </summary>
        /// <param name="dt">待格式化的 DateTime。</param>
        public static string ToChineseLunarDate(this DateTime dt)
        {
            // 仅支持中国农历，需引用 System.Globalization.ChineseLunisolarCalendar
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
        /// DateTime 增加指定天数。
        /// </summary>
        /// <param name="dt">原始 DateTime。</param>
        /// <param name="days">要增加的天数。</param>
        public static DateTime AddDaysSafe(this DateTime dt, double days) =>
            dt.AddDays(days);

        /// <summary>
        /// DateTime 增加指定小时数。
        /// </summary>
        /// <param name="dt">原始 DateTime。</param>
        /// <param name="hours">要增加的小时数。</param>
        public static DateTime AddHoursSafe(this DateTime dt, double hours) =>
            dt.AddHours(hours);

        /// <summary>
        /// DateTime 增加指定分钟数。
        /// </summary>
        /// <param name="dt">原始 DateTime。</param>
        /// <param name="minutes">要增加的分钟数。</param>
        public static DateTime AddMinutesSafe(this DateTime dt, double minutes) =>
            dt.AddMinutes(minutes);

        /// <summary>
        /// DateTime 增加指定秒数。
        /// </summary>
        /// <param name="dt">原始 DateTime。</param>
        /// <param name="seconds">要增加的秒数。</param>
        public static DateTime AddSecondsSafe(this DateTime dt, double seconds) =>
            dt.AddSeconds(seconds);

        /// <summary>
        /// DateTime 增加指定月份数。
        /// </summary>
        /// <param name="dt">原始 DateTime。</param>
        /// <param name="months">要增加的月份数。</param>
        public static DateTime AddMonthsSafe(this DateTime dt, int months) =>
            dt.AddMonths(months);

        /// <summary>
        /// DateTime 增加指定年份数。
        /// </summary>
        /// <param name="dt">原始 DateTime。</param>
        /// <param name="years">要增加的年份数。</param>
        public static DateTime AddYearsSafe(this DateTime dt, int years) =>
            dt.AddYears(years);

        /// <summary>
        /// 获取两个 DateTime 之间的天数差。
        /// </summary>
        /// <param name="dt">起始 DateTime。</param>
        /// <param name="other">结束 DateTime。</param>
        public static double DaysBetween(this DateTime dt, DateTime other) =>
            Math.Abs((dt.Date - other.Date).TotalDays);

        /// <summary>
        /// 获取两个 DateTime 之间的小时差。
        /// </summary>
        /// <param name="dt">起始 DateTime。</param>
        /// <param name="other">结束 DateTime。</param>
        public static double HoursBetween(this DateTime dt, DateTime other) =>
            Math.Abs((dt - other).TotalHours);

        /// <summary>
        /// 获取两个 DateTime 之间的分钟差。
        /// </summary>
        /// <param name="dt">起始 DateTime。</param>
        /// <param name="other">结束 DateTime。</param>
        public static double MinutesBetween(this DateTime dt, DateTime other) =>
            Math.Abs((dt - other).TotalMinutes);

        /// <summary>
        /// 获取两个 DateTime 之间的秒数差。
        /// </summary>
        /// <param name="dt">起始 DateTime。</param>
        /// <param name="other">结束 DateTime。</param>
        public static double SecondsBetween(this DateTime dt, DateTime other) =>
            Math.Abs((dt - other).TotalSeconds);

        /// <summary>
        /// 获取两个 DateTime 之间的时间间隔（TimeSpan）。
        /// </summary>
        /// <param name="dt">起始 DateTime。</param>
        /// <param name="other">结束 DateTime。</param>
        public static TimeSpan SpanBetween(this DateTime dt, DateTime other) =>
            dt > other ? dt - other : other - dt;

        /// <summary>
        /// 判断 DateTime 是否在指定范围内（包含边界）。
        /// </summary>
        /// <param name="dt">待判断的 DateTime。</param>
        /// <param name="start">起始时间。</param>
        /// <param name="end">结束时间。</param>
        public static bool IsBetween(this DateTime dt, DateTime start, DateTime end) =>
            dt >= start && dt <= end;

        /// <summary>
        /// 判断 DateTime 是否早于指定时间。
        /// </summary>
        /// <param name="dt">待判断的 DateTime。</param>
        /// <param name="other">比较的时间。</param>
        public static bool IsBefore(this DateTime dt, DateTime other) =>
            dt < other;

        /// <summary>
        /// 判断 DateTime 是否晚于指定时间。
        /// </summary>
        /// <param name="dt">待判断的 DateTime。</param>
        /// <param name="other">比较的时间。</param>
        public static bool IsAfter(this DateTime dt, DateTime other) =>
            dt > other;

        /// <summary>
        /// DateTime 转为本地时间。
        /// </summary>
        /// <param name="dt">待转换的 DateTime。</param>
        public static DateTime ToLocalTimeSafe(this DateTime dt) =>
            dt.Kind == DateTimeKind.Local ? dt : dt.ToLocalTime();

        /// <summary>
        /// DateTime 转为 UTC 时间。
        /// </summary>
        /// <param name="dt">待转换的 DateTime。</param>
        public static DateTime ToUtcTimeSafe(this DateTime dt) =>
            dt.Kind == DateTimeKind.Utc ? dt : dt.ToUniversalTime();

        /// <summary>
        /// DateTime 转为指定时区时间。
        /// </summary>
        /// <param name="dt">待转换的 DateTime。</param>
        /// <param name="timeZoneId">时区标识，如 "China Standard Time"。</param>
        public static DateTime ToTimeZone(this DateTime dt, string timeZoneId)
        {
            var tz = TimeZoneInfo.FindSystemTimeZoneById(timeZoneId);
            return TimeZoneInfo.ConvertTime(dt, tz);
        }

        /// <summary>
        /// DateTime 转为时间戳（自定义格式，精度到毫秒）。
        /// </summary>
        /// <param name="dt">待格式化的 DateTime。</param>
        public static string ToCustomTimestamp(this DateTime dt) =>
            dt.ToString("yyyyMMddHHmmssfff");

        /// <summary>
        /// DateTime 转为友好时间描述（如“刚刚”、“5分钟前”、“昨天”）。
        /// </summary>
        /// <param name="dt">待处理的 DateTime。</param>
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
        /// DateTime 转为 Age（年龄，按年份计算）。
        /// </summary>
        /// <param name="dt">出生日期。</param>
        public static int ToAge(this DateTime dt)
        {
            var now = DateTime.Now;
            int age = now.Year - dt.Year;
            if (now.Month < dt.Month || (now.Month == dt.Month && now.Day < dt.Day))
                age--;
            return age < 0 ? 0 : age;
        }

        /// <summary>
        /// DateTime 转为星期几的数字（周日为0，周一为1）。
        /// </summary>
        /// <param name="dt">待处理的 DateTime。</param>
        public static int ToWeekdayNumber(this DateTime dt) => (int)dt.DayOfWeek;

        /// <summary>
        /// DateTime 转为年月日元组。
        /// </summary>
        /// <param name="dt">待处理的 DateTime。</param>
        public static (int Year, int Month, int Day) ToYMD(this DateTime dt) =>
            (dt.Year, dt.Month, dt.Day);

        /// <summary>
        /// DateTime 转为时分秒元组。
        /// </summary>
        /// <param name="dt">待处理的 DateTime。</param>
        public static (int Hour, int Minute, int Second) ToHMS(this DateTime dt) =>
            (dt.Hour, dt.Minute, dt.Second);

        /// <summary>
        /// DateTime 转为 DateOnly（.NET 6+）。
        /// </summary>
        /// <param name="dt">待转换的 DateTime。</param>
        public static DateOnly ToDateOnly(this DateTime dt) => DateOnly.FromDateTime(dt);

        /// <summary>
        /// DateTime 转为 TimeOnly（.NET 6+）。
        /// </summary>
        /// <param name="dt">待转换的 DateTime。</param>
        public static TimeOnly ToTimeOnly(this DateTime dt) => TimeOnly.FromDateTime(dt);

        /// <summary>
        /// 判断 DateTime 是否为上午。
        /// </summary>
        /// <param name="dt">待判断的 DateTime。</param>
        public static bool IsAM(this DateTime dt) => dt.Hour < 12;

        /// <summary>
        /// 判断 DateTime 是否为下午。
        /// </summary>
        /// <param name="dt">待判断的 DateTime。</param>
        public static bool IsPM(this DateTime dt) => dt.Hour >= 12;

        /// <summary>
        /// DateTime 转为民国纪年字符串（如 "民国114年09月30日"）。
        /// </summary>
        /// <param name="dt">待格式化的 DateTime。</param>
        public static string ToMinguoString(this DateTime dt)
        {
            int year = dt.Year - 1911;
            return $"民国{year}年{dt.Month:D2}月{dt.Day:D2}日";
        }

        /// <summary>
        /// DateTime 转为 JDN（儒略日号）。
        /// </summary>
        /// <param name="dt">待处理的 DateTime。</param>
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
    }
}