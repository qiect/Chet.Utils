using Xunit;

namespace Chet.Utils.Tests.Extensions
{
    public class DateTimeExtendTests
    {
        [Fact]
        public void IsDefault_DefaultValue_ReturnsTrue()
        {
            DateTime dt = default;
            Assert.True(dt.IsDefault());
        }

        [Fact]
        public void IsMinValue_MinValue_ReturnsTrue()
        {
            DateTime dt = DateTime.MinValue;
            Assert.True(dt.IsMinValue());
        }

        [Fact]
        public void IsMaxValue_MaxValue_ReturnsTrue()
        {
            DateTime dt = DateTime.MaxValue;
            Assert.True(dt.IsMaxValue());
        }

        [Fact]
        public void IsToday_Today_ReturnsTrue()
        {
            DateTime dt = DateTime.Now;
            Assert.True(dt.IsToday());
        }

        [Fact]
        public void IsLeapYear_LeapYear_ReturnsTrue()
        {
            DateTime dt = new DateTime(2024, 2, 29);
            Assert.True(dt.IsLeapYear());
        }

        [Fact]
        public void IsWeekend_Saturday_ReturnsTrue()
        {
            DateTime dt = new DateTime(2024, 6, 29); // Saturday
            Assert.True(dt.IsWeekend());
        }

        [Fact]
        public void IsWeekday_Monday_ReturnsTrue()
        {
            DateTime dt = new DateTime(2024, 7, 1); // Monday
            Assert.True(dt.IsWeekday());
        }

        [Fact]
        public void ToUnixTimestamp_KnownDate_ReturnsExpected()
        {
            DateTime dt = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            Assert.Equal(0, dt.ToUnixTimestamp());
        }

        [Fact]
        public void ToUnixTimestampMs_KnownDate_ReturnsExpected()
        {
            DateTime dt = new DateTime(1970, 1, 1, 0, 0, 1, DateTimeKind.Utc);
            Assert.Equal(1000, dt.ToUnixTimestampMs());
        }

        [Fact]
        public void FromUnixTimestamp_Zero_ReturnsEpoch()
        {
            long ts = 0;
            var dt = ts.FromUnixTimestamp();
            Assert.Equal(new DateTime(1970, 1, 1, 8, 0, 0), dt); // Local time
        }

        [Fact]
        public void FromUnixTimestampMs_Zero_ReturnsEpoch()
        {
            long ts = 0;
            var dt = ts.FromUnixTimestampMs();
            Assert.Equal(new DateTime(1970, 1, 1, 8, 0, 0), dt); // Local time
        }

        [Fact]
        public void ToFormatString_CustomFormat_ReturnsExpected()
        {
            DateTime dt = new DateTime(2025, 9, 30, 14, 30, 0);
            Assert.Equal("2025-09-30 14:30:00", dt.ToFormatString());
            Assert.Equal("2025/09/30", dt.ToFormatString("yyyy/MM/dd"));
        }

        [Fact]
        public void ToIso8601String_KnownDate_ReturnsExpected()
        {
            DateTime dt = new DateTime(2025, 9, 30, 14, 30, 0);
            Assert.Equal(dt.ToString("o"), dt.ToIso8601String());
        }

        [Fact]
        public void ToRfc1123String_KnownDate_ReturnsExpected()
        {
            DateTime dt = new DateTime(2025, 9, 30, 14, 30, 0);
            Assert.Equal(dt.ToString("R"), dt.ToRfc1123String());
        }

        [Fact]
        public void ToChineseDateString_KnownDate_ReturnsExpected()
        {
            DateTime dt = new DateTime(2025, 9, 30);
            Assert.Equal("2025年09月30日", dt.ToChineseDateString());
        }

        [Fact]
        public void ToChineseDateTimeString_KnownDate_ReturnsExpected()
        {
            DateTime dt = new DateTime(2025, 9, 30, 14, 30, 0);
            Assert.Equal("2025年09月30日 14时30分", dt.ToChineseDateTimeString());
        }

        [Fact]
        public void ToTimestampString_KnownDate_ReturnsExpected()
        {
            DateTime dt = new DateTime(2025, 9, 30, 14, 30, 0);
            Assert.Equal("20250930143000", dt.ToTimestampString());
        }

        [Fact]
        public void ToDateString_KnownDate_ReturnsExpected()
        {
            DateTime dt = new DateTime(2025, 9, 30);
            Assert.Equal("2025-09-30", dt.ToDateString());
        }

        [Fact]
        public void ToTimeString_KnownDate_ReturnsExpected()
        {
            DateTime dt = new DateTime(2025, 9, 30, 14, 30, 0);
            Assert.Equal("14:30:00", dt.ToTimeString());
        }

        [Fact]
        public void ToShortTimeString_KnownDate_ReturnsExpected()
        {
            DateTime dt = new DateTime(2025, 9, 30, 14, 30, 0);
            Assert.Equal("14:30", dt.ToShortTimeString());
        }

        [Fact]
        public void ToChineseWeekday_Sunday_ReturnsExpected()
        {
            DateTime dt = new DateTime(2024, 6, 30); // Sunday
            Assert.Equal("星期日", dt.ToChineseWeekday());
        }

        [Fact]
        public void ToEnglishWeekday_Monday_ReturnsExpected()
        {
            DateTime dt = new DateTime(2024, 7, 1); // Monday
            Assert.Equal("Monday", dt.ToEnglishWeekday());
        }

        [Fact]
        public void ToQuarter_March_ReturnsQ1()
        {
            DateTime dt = new DateTime(2024, 3, 1);
            Assert.Equal("Q1", dt.ToQuarter());
        }

        [Fact]
        public void ToChineseLunarDate_KnownDate_ReturnsString()
        {
            DateTime dt = new DateTime(2025, 2, 1);
            var result = dt.ToChineseLunarDate();
            Assert.False(string.IsNullOrEmpty(result));
        }

        [Fact]
        public void AddDaysSafe_Add1Day_ReturnsExpected()
        {
            DateTime dt = new DateTime(2024, 6, 30);
            Assert.Equal(new DateTime(2024, 7, 1), dt.AddDaysSafe(1));
        }

        [Fact]
        public void AddHoursSafe_Add1Hour_ReturnsExpected()
        {
            DateTime dt = new DateTime(2024, 6, 30, 10, 0, 0);
            Assert.Equal(new DateTime(2024, 6, 30, 11, 0, 0), dt.AddHoursSafe(1));
        }

        [Fact]
        public void AddMinutesSafe_Add30Minutes_ReturnsExpected()
        {
            DateTime dt = new DateTime(2024, 6, 30, 10, 0, 0);
            Assert.Equal(new DateTime(2024, 6, 30, 10, 30, 0), dt.AddMinutesSafe(30));
        }

        [Fact]
        public void AddSecondsSafe_Add60Seconds_ReturnsExpected()
        {
            DateTime dt = new DateTime(2024, 6, 30, 10, 0, 0);
            Assert.Equal(new DateTime(2024, 6, 30, 10, 1, 0), dt.AddSecondsSafe(60));
        }

        [Fact]
        public void AddMonthsSafe_Add1Month_ReturnsExpected()
        {
            DateTime dt = new DateTime(2024, 6, 30);
            Assert.Equal(new DateTime(2024, 7, 30), dt.AddMonthsSafe(1));
        }

        [Fact]
        public void AddYearsSafe_Add1Year_ReturnsExpected()
        {
            DateTime dt = new DateTime(2024, 6, 30);
            Assert.Equal(new DateTime(2025, 6, 30), dt.AddYearsSafe(1));
        }

        [Fact]
        public void DaysBetween_1DayApart_Returns1()
        {
            DateTime dt1 = new DateTime(2024, 6, 30);
            DateTime dt2 = new DateTime(2024, 7, 1);
            Assert.Equal(1, dt1.DaysBetween(dt2));
        }

        [Fact]
        public void HoursBetween_2HoursApart_Returns2()
        {
            DateTime dt1 = new DateTime(2024, 6, 30, 10, 0, 0);
            DateTime dt2 = new DateTime(2024, 6, 30, 12, 0, 0);
            Assert.Equal(2, dt1.HoursBetween(dt2));
        }

        [Fact]
        public void MinutesBetween_30MinutesApart_Returns30()
        {
            DateTime dt1 = new DateTime(2024, 6, 30, 10, 0, 0);
            DateTime dt2 = new DateTime(2024, 6, 30, 10, 30, 0);
            Assert.Equal(30, dt1.MinutesBetween(dt2));
        }

        [Fact]
        public void SecondsBetween_60SecondsApart_Returns60()
        {
            DateTime dt1 = new DateTime(2024, 6, 30, 10, 0, 0);
            DateTime dt2 = new DateTime(2024, 6, 30, 10, 1, 0);
            Assert.Equal(60, dt1.SecondsBetween(dt2));
        }

        [Fact]
        public void SpanBetween_2DaysApart_Returns2Days()
        {
            DateTime dt1 = new DateTime(2024, 6, 30);
            DateTime dt2 = new DateTime(2024, 7, 2);
            Assert.Equal(TimeSpan.FromDays(2), dt1.SpanBetween(dt2));
        }

        [Fact]
        public void IsBetween_ValueInRange_ReturnsTrue()
        {
            DateTime dt = new DateTime(2024, 6, 30, 12, 0, 0);
            DateTime start = new DateTime(2024, 6, 30, 10, 0, 0);
            DateTime end = new DateTime(2024, 6, 30, 14, 0, 0);
            Assert.True(dt.IsBetween(start, end));
        }

        [Fact]
        public void IsBefore_Before_ReturnsTrue()
        {
            DateTime dt1 = new DateTime(2024, 6, 30, 10, 0, 0);
            DateTime dt2 = new DateTime(2024, 6, 30, 12, 0, 0);
            Assert.True(dt1.IsBefore(dt2));
        }

        [Fact]
        public void IsAfter_After_ReturnsTrue()
        {
            DateTime dt1 = new DateTime(2024, 6, 30, 14, 0, 0);
            DateTime dt2 = new DateTime(2024, 6, 30, 12, 0, 0);
            Assert.True(dt1.IsAfter(dt2));
        }

        [Fact]
        public void ToLocalTimeSafe_AlreadyLocal_ReturnsSame()
        {
            DateTime dt = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Local);
            Assert.Equal(dt, dt.ToLocalTimeSafe());
        }

        [Fact]
        public void ToUtcTimeSafe_AlreadyUtc_ReturnsSame()
        {
            DateTime dt = DateTime.SpecifyKind(DateTime.UtcNow, DateTimeKind.Utc);
            Assert.Equal(dt, dt.ToUtcTimeSafe());
        }

        [Fact]
        public void ToTimeZone_ConvertToChinaStandardTime_ReturnsExpected()
        {
            DateTime dt = new DateTime(2024, 6, 30, 0, 0, 0, DateTimeKind.Utc);
            var converted = dt.ToTimeZone("China Standard Time");
            Assert.Equal(8, converted.Hour);
        }

        [Fact]
        public void ToCustomTimestamp_KnownDate_ReturnsExpected()
        {
            DateTime dt = new DateTime(2025, 9, 30, 14, 30, 0, 123);
            Assert.Equal("20250930143000123", dt.ToCustomTimestamp());
        }

        [Fact]
        public void ToFriendlyString_JustNow_Returns刚刚()
        {
            DateTime dt = DateTime.Now.AddSeconds(-10);
            Assert.Equal("刚刚", dt.ToFriendlyString());
        }

        [Fact]
        public void ToAge_BirthdayToday_ReturnsAge()
        {
            DateTime dt = new DateTime(DateTime.Now.Year - 20, DateTime.Now.Month, DateTime.Now.Day);
            Assert.Equal(20, dt.ToAge());
        }

        [Fact]
        public void ToWeekdayNumber_Sunday_Returns0()
        {
            DateTime dt = new DateTime(2024, 6, 30); // Sunday
            Assert.Equal(0, dt.ToWeekdayNumber());
        }

        [Fact]
        public void ToYMD_KnownDate_ReturnsTuple()
        {
            DateTime dt = new DateTime(2025, 9, 30);
            var ymd = dt.ToYMD();
            Assert.Equal((2025, 9, 30), ymd);
        }

        [Fact]
        public void ToHMS_KnownTime_ReturnsTuple()
        {
            DateTime dt = new DateTime(2025, 9, 30, 14, 30, 15);
            var hms = dt.ToHMS();
            Assert.Equal((14, 30, 15), hms);
        }

        [Fact]
        public void ToDateOnly_KnownDate_ReturnsDateOnly()
        {
            DateTime dt = new DateTime(2025, 9, 30, 14, 30, 0);
            var dateOnly = dt.ToDateOnly();
            Assert.Equal(new DateOnly(2025, 9, 30), dateOnly);
        }

        [Fact]
        public void ToTimeOnly_KnownTime_ReturnsTimeOnly()
        {
            DateTime dt = new DateTime(2025, 9, 30, 14, 30, 0);
            var timeOnly = dt.ToTimeOnly();
            Assert.Equal(new TimeOnly(14, 30, 0), timeOnly);
        }

        [Fact]
        public void IsAM_Morning_ReturnsTrue()
        {
            DateTime dt = new DateTime(2025, 9, 30, 9, 0, 0);
            Assert.True(dt.IsAM());
        }

        [Fact]
        public void IsPM_Afternoon_ReturnsTrue()
        {
            DateTime dt = new DateTime(2025, 9, 30, 13, 0, 0);
            Assert.True(dt.IsPM());
        }

        [Fact]
        public void ToMinguoString_KnownDate_ReturnsExpected()
        {
            DateTime dt = new DateTime(2025, 9, 30);
            Assert.Equal("民国114年09月30日", dt.ToMinguoString());
        }

        [Fact]
        public void ToJulianDayNumber_KnownDate_ReturnsExpected()
        {
            DateTime dt = new DateTime(2000, 1, 1, 12, 0, 0);
            double jd = dt.ToJulianDayNumber();
            Assert.Equal(2451545.0, jd, 0.0001);
        }
    }
}