using Xunit;

namespace Chet.Utils.Tests.Extensions
{
    public class IntExtendTests
    {
        [Theory]
        [InlineData(0, true)]
        [InlineData(1, false)]
        public void IsZero_Test(int value, bool expected)
        {
            Assert.Equal(expected, value.IsZero());
        }

        [Theory]
        [InlineData(1, true)]
        [InlineData(0, false)]
        [InlineData(-1, false)]
        public void IsPositive_Test(int value, bool expected)
        {
            Assert.Equal(expected, value.IsPositive());
        }

        [Theory]
        [InlineData(-1, true)]
        [InlineData(0, false)]
        [InlineData(1, false)]
        public void IsNegative_Test(int value, bool expected)
        {
            Assert.Equal(expected, value.IsNegative());
        }

        [Theory]
        [InlineData(2, true)]
        [InlineData(3, false)]
        public void IsEven_Test(int value, bool expected)
        {
            Assert.Equal(expected, value.IsEven());
        }

        [Theory]
        [InlineData(2, false)]
        [InlineData(3, true)]
        public void IsOdd_Test(int value, bool expected)
        {
            Assert.Equal(expected, value.IsOdd());
        }

        [Theory]
        [InlineData(5, 1, 10, true)]
        [InlineData(0, 1, 10, false)]
        [InlineData(10, 1, 10, true)]
        public void IsBetween_Test(int value, int min, int max, bool expected)
        {
            Assert.Equal(expected, value.IsBetween(min, max));
        }

        [Theory]
        [InlineData(5, 1, 10, 5)]
        [InlineData(0, 1, 10, 1)]
        [InlineData(11, 1, 10, 10)]
        public void Clamp_Test(int value, int min, int max, int expected)
        {
            Assert.Equal(expected, value.Clamp(min, max));
        }

        [Theory]
        [InlineData(0, false)]
        [InlineData(1, true)]
        public void ToBool_Test(int value, bool expected)
        {
            Assert.Equal(expected, value.ToBool());
        }

        [Fact]
        public void ToDouble_Test()
        {
            Assert.Equal(5.0, 5.ToDouble());
        }

        [Fact]
        public void ToFloat_Test()
        {
            Assert.Equal(5f, 5.ToFloat());
        }

        [Fact]
        public void ToLong_Test()
        {
            Assert.Equal(5L, 5.ToLong());
        }

        [Fact]
        public void ToDecimal_Test()
        {
            Assert.Equal(5m, 5.ToDecimal());
        }

        [Theory]
        [InlineData(5, null, "5")]
        [InlineData(5, "D4", "0005")]
        public void ToStringFormat_Test(int value, string format, string expected)
        {
            Assert.Equal(expected, value.ToStringFormat(format));
        }

        [Fact]
        public void ToCurrencyString_Test()
        {
            var result = 1234.ToCurrencyString();
            Assert.Contains("¥", result);
            Assert.Contains("1,234", result);
        }

        [Fact]
        public void ToPercentString_Test()
        {
            Assert.Equal("12%", 12.ToPercentString());
        }

        [Theory]
        [InlineData(0, "零元")]
        [InlineData(1, "壹元")]
        [InlineData(10, "壹拾元")]
        [InlineData(1001, "壹仟零壹元")]
        public void ToChineseUpper_Test(int value, string expected)
        {
            Assert.Equal(expected, value.ToChineseUpper());
        }

        [Theory]
        [InlineData(-5, 5)]
        [InlineData(5, 5)]
        public void Abs_Test(int value, int expected)
        {
            Assert.Equal(expected, value.Abs());
        }

        [Theory]
        [InlineData(5, 10, 10)]
        [InlineData(10, 5, 10)]
        public void Max_Test(int a, int b, int expected)
        {
            Assert.Equal(expected, a.Max(b));
        }

        [Theory]
        [InlineData(5, 10, 5)]
        [InlineData(10, 5, 5)]
        public void Min_Test(int a, int b, int expected)
        {
            Assert.Equal(expected, a.Min(b));
        }

        [Theory]
        [InlineData(5, 10, 15)]
        public void Add_Test(int a, int b, int expected)
        {
            Assert.Equal(expected, a.Add(b));
        }

        [Theory]
        [InlineData(10, 5, 5)]
        public void Subtract_Test(int a, int b, int expected)
        {
            Assert.Equal(expected, a.Subtract(b));
        }

        [Theory]
        [InlineData(5, 10, 50)]
        public void Multiply_Test(int a, int b, int expected)
        {
            Assert.Equal(expected, a.Multiply(b));
        }

        [Theory]
        [InlineData(10, 2, 5)]
        [InlineData(10, 0, 0)]
        public void DivideSafe_Test(int a, int b, int expected)
        {
            Assert.Equal(expected, a.DivideSafe(b));
        }

        [Theory]
        [InlineData(10, 3, 1)]
        [InlineData(10, 0, 0)]
        public void Mod_Test(int a, int b, int expected)
        {
            Assert.Equal(expected, a.Mod(b));
        }

        [Theory]
        [InlineData(2, 3, 8)]
        [InlineData(5, 0, 1)]
        public void Pow_Test(int a, int power, int expected)
        {
            Assert.Equal(expected, a.Pow(power));
        }

        [Theory]
        [InlineData(5, 10, 5)]
        [InlineData(10, 5, 5)]
        public void AbsDiff_Test(int a, int b, int expected)
        {
            Assert.Equal(expected, a.AbsDiff(b));
        }

        [Theory]
        [InlineData(255, "FF")]
        [InlineData(16, "10")]
        public void ToHexString_Test(int value, string expected)
        {
            Assert.Equal(expected, value.ToHexString());
        }

        [Theory]
        [InlineData(5, "101")]
        [InlineData(8, "1000")]
        public void ToBinaryString_Test(int value, string expected)
        {
            Assert.Equal(expected, value.ToBinaryString());
        }

        [Theory]
        [InlineData(8, "10")]
        [InlineData(15, "17")]
        public void ToOctalString_Test(int value, string expected)
        {
            Assert.Equal(expected, value.ToOctalString());
        }

        [Theory]
        [InlineData(1, "I")]
        [InlineData(4, "IV")]
        [InlineData(3999, "MMMCMXCIX")]
        [InlineData(0, "0")]
        [InlineData(4000, "4000")]
        public void ToRomanString_Test(int value, string expected)
        {
            Assert.Equal(expected, value.ToRomanString());
        }

        [Theory]
        [InlineData(0, "星期日")]
        [InlineData(6, "星期六")]
        [InlineData(7, "7")]
        public void ToChineseWeekday_Test(int value, string expected)
        {
            Assert.Equal(expected, value.ToChineseWeekday());
        }

        [Theory]
        [InlineData(0, "Sunday")]
        [InlineData(6, "Saturday")]
        [InlineData(7, "7")]
        public void ToEnglishWeekday_Test(int value, string expected)
        {
            Assert.Equal(expected, value.ToEnglishWeekday());
        }

        public enum TestEnum { Zero = 0, One = 1, Two = 2 }

        [Theory]
        [InlineData(1, TestEnum.One)]
        [InlineData(3, default(TestEnum))]
        public void ToEnum_Test(int value, TestEnum expected)
        {
            Assert.Equal(expected, value.ToEnum<TestEnum>());
        }

        [Fact]
        public void Repeat_Action_Test()
        {
            int count = 0;
            5.Repeat(() => count++);
            Assert.Equal(5, count);
        }

        [Fact]
        public void Repeat_ActionInt_Test()
        {
            int sum = 0;
            3.Repeat(i => sum += i);
            Assert.Equal(3, sum); // 0+1+2=3
        }

        [Fact]
        public void Repeat_NullAction_DoesNothing()
        {
            int count = 0;
            5.Repeat((Action)null);
            Assert.Equal(0, count);
        }

        [Fact]
        public void Repeat_NullActionInt_DoesNothing()
        {
            int count = 0;
            5.Repeat((Action<int>)null);
            Assert.Equal(0, count);
        }

        [Fact]
        public void Repeat_Negative_DoesNothing()
        {
            int count = 0;
            (-1).Repeat(() => count++);
            Assert.Equal(0, count);
        }
    }
}