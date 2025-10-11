using Xunit;

namespace Chet.Utils.Tests.Extensions
{
    public class DecimalExtendTests
    {
        [Theory]
        [InlineData(0, true)]
        [InlineData(1, false)]
        [InlineData(-1, false)]
        public void IsZero_Test(decimal value, bool expected)
        {
            Assert.Equal(expected, value.IsZero());
        }

        [Theory]
        [InlineData(1, true)]
        [InlineData(0, false)]
        [InlineData(-1, false)]
        public void IsPositive_Test(decimal value, bool expected)
        {
            Assert.Equal(expected, value.IsPositive());
        }

        [Theory]
        [InlineData(-1, true)]
        [InlineData(0, false)]
        [InlineData(1, false)]
        public void IsNegative_Test(decimal value, bool expected)
        {
            Assert.Equal(expected, value.IsNegative());
        }

        [Theory]
        [InlineData(1, true)]
        [InlineData(1.0, true)]
        [InlineData(1.5, false)]
        [InlineData(-2.0, true)]
        public void IsInteger_Test(decimal value, bool expected)
        {
            Assert.Equal(expected, value.IsInteger());
        }

        [Theory]
        [InlineData(2, true)]
        [InlineData(3, false)]
        [InlineData(2.5, false)]
        public void IsEven_Test(decimal value, bool expected)
        {
            Assert.Equal(expected, value.IsEven());
        }

        [Theory]
        [InlineData(3, true)]
        [InlineData(2, false)]
        [InlineData(3.5, false)]
        public void IsOdd_Test(decimal value, bool expected)
        {
            Assert.Equal(expected, value.IsOdd());
        }

        [Theory]
        [InlineData(1.2345, 2, 1.23)]
        [InlineData(1.2355, 2, 1.24)]
        [InlineData(-1.2355, 2, -1.24)]
        public void Round_Test(decimal value, int digits, decimal expected)
        {
            Assert.Equal(expected, value.Round(digits));
        }

        [Theory]
        [InlineData(1.239, 2, 1.23)]
        [InlineData(-1.239, 2, -1.23)]
        [InlineData(1.2, 0, 1)]
        public void Truncate_Test(decimal value, int digits, decimal expected)
        {
            Assert.Equal(expected, value.Truncate(digits));
        }

        [Theory]
        [InlineData(-1.5, 1.5)]
        [InlineData(2.5, 2.5)]
        public void Abs_Test(decimal value, decimal expected)
        {
            Assert.Equal(expected, value.Abs());
        }

        [Theory]
        [InlineData(1.2345, 2, "1.23")]
        [InlineData(1.2, 0, "1")]
        public void ToFixedString_Test(decimal value, int digits, string expected)
        {
            Assert.Equal(expected, value.ToFixedString(digits));
        }

        [Fact]
        public void ToCurrencyString_Test()
        {
            decimal value = 1234.56m;
            string result = value.ToCurrencyString("en-US");
            Assert.Contains("$", result);
            Assert.Contains("1,234.56", result);
        }

        [Theory]
        [InlineData(0.1234, 2, "12.34%")]
        [InlineData(0.1, 0, "10%")]
        public void ToPercentString_Test(decimal value, int digits, string expected)
        {
            Assert.Equal(expected, value.ToPercentString(digits));
        }

        [Theory]
        [InlineData(0, "零元整")]
        [InlineData(123.45, "壹佰贰拾叁元肆角伍分")]
        [InlineData(1001001, "壹佰万壹仟零壹元整")]
        public void ToChineseUpper_Test(decimal value, string expected)
        {
            Assert.Equal(expected, value.ToChineseUpper());
        }

        [Theory]
        [InlineData(12345.6789, 2, "1.23E+004")]
        [InlineData(0.00123, 3, "1.230E-003")]
        public void ToScientificString_Test(decimal value, int digits, string expectedStart)
        {
            Assert.StartsWith(expectedStart, value.ToScientificString(digits));
        }

        [Theory]
        [InlineData(1.2, 2.3, 2.3)]
        [InlineData(5.5, 2.2, 5.5)]
        public void Max_Test(decimal a, decimal b, decimal expected)
        {
            Assert.Equal(expected, a.Max(b));
        }

        [Theory]
        [InlineData(1.2, 2.3, 1.2)]
        [InlineData(5.5, 2.2, 2.2)]
        public void Min_Test(decimal a, decimal b, decimal expected)
        {
            Assert.Equal(expected, a.Min(b));
        }

        [Theory]
        [InlineData(5, 1, 10, true)]
        [InlineData(0, 1, 10, false)]
        [InlineData(10, 1, 10, true)]
        public void IsBetween_Test(decimal value, decimal min, decimal max, bool expected)
        {
            Assert.Equal(expected, value.IsBetween(min, max));
        }

        [Theory]
        [InlineData(5, 1, 10, 5)]
        [InlineData(0, 1, 10, 1)]
        [InlineData(11, 1, 10, 10)]
        public void Clamp_Test(decimal value, decimal min, decimal max, decimal expected)
        {
            Assert.Equal(expected, value.Clamp(min, max));
        }

        [Theory]
        [InlineData(1.6, 2)]
        [InlineData(1.4, 1)]
        [InlineData(-1.6, -2)]
        public void ToInt_Test(decimal value, int expected)
        {
            Assert.Equal(expected, value.ToInt());
        }

        [Theory]
        [InlineData(1.5, 1.5)]
        [InlineData(-2.3, -2.3)]
        public void ToDouble_Test(decimal value, double expected)
        {
            Assert.Equal(expected, value.ToDouble(), 6);
        }

        [Theory]
        [InlineData(1.5, 1.5f)]
        [InlineData(-2.3, -2.3f)]
        public void ToFloat_Test(decimal value, float expected)
        {
            Assert.Equal(expected, value.ToFloat());
        }

        [Theory]
        [InlineData(1.6, 2L)]
        [InlineData(1.4, 1L)]
        [InlineData(-1.6, -2L)]
        public void ToLong_Test(decimal value, long expected)
        {
            Assert.Equal(expected, value.ToLong());
        }

        [Theory]
        [InlineData(0, false)]
        [InlineData(1, true)]
        [InlineData(-1, true)]
        public void ToBool_Test(decimal value, bool expected)
        {
            Assert.Equal(expected, value.ToBool());
        }

        [Theory]
        [InlineData(1.2, 2.3, 2.76)]
        [InlineData(-1, 1, -1)]
        public void Multiply_Test(decimal a, decimal b, decimal expected)
        {
            Assert.Equal(expected, a.Multiply(b));
        }

        [Theory]
        [InlineData(6, 3, 2)]
        [InlineData(1, 0, 0)]
        public void DivideSafe_Test(decimal a, decimal b, decimal expected)
        {
            Assert.Equal(expected, a.DivideSafe(b));
        }

        [Theory]
        [InlineData(1.2, 2.3, 3.5)]
        [InlineData(-1, 1, 0)]
        public void Add_Test(decimal a, decimal b, decimal expected)
        {
            Assert.Equal(expected, a.Add(b));
        }

        [Theory]
        [InlineData(5.5, 2.2, 3.3)]
        [InlineData(1, 1, 0)]
        public void Subtract_Test(decimal a, decimal b, decimal expected)
        {
            Assert.Equal(expected, a.Subtract(b), 10);
        }

        [Theory]
        [InlineData(7, 3, 1)]
        [InlineData(1, 0, 0)]
        public void Mod_Test(decimal a, decimal b, decimal expected)
        {
            Assert.Equal(expected, a.Mod(b));
        }

        [Theory]
        [InlineData(2, 3, 8)]
        [InlineData(4, 0, 1)]
        public void Pow_Test(decimal value, int power, decimal expected)
        {
            Assert.Equal(expected, value.Pow(power));
        }

        [Theory]
        [InlineData(4, 2)]
        [InlineData(9, 3)]
        public void Sqrt_Test(decimal value, decimal expected)
        {
            Assert.Equal(expected, value.Sqrt(), 6);
        }

        [Theory]
        [InlineData(5, 3, 2)]
        [InlineData(3, 5, 2)]
        public void AbsDiff_Test(decimal a, decimal b, decimal expected)
        {
            Assert.Equal(expected, a.AbsDiff(b));
        }
    }
}