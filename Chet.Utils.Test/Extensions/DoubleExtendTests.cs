using Xunit;

namespace Chet.Utils.Tests.Extensions
{
    public class DoubleExtendTests
    {
        [Theory]
        [InlineData(0d, true)]
        [InlineData(1d, false)]
        [InlineData(-1d, false)]
        public void IsZero_Test(double value, bool expected)
        {
            Assert.Equal(expected, value.IsZero());
        }

        [Theory]
        [InlineData(1d, true)]
        [InlineData(0d, false)]
        [InlineData(-1d, false)]
        public void IsPositive_Test(double value, bool expected)
        {
            Assert.Equal(expected, value.IsPositive());
        }

        [Theory]
        [InlineData(-1d, true)]
        [InlineData(0d, false)]
        [InlineData(1d, false)]
        public void IsNegative_Test(double value, bool expected)
        {
            Assert.Equal(expected, value.IsNegative());
        }

        [Theory]
        [InlineData(1d, true)]
        [InlineData(1.0, true)]
        [InlineData(1.5, false)]
        [InlineData(-2.0, true)]
        public void IsInteger_Test(double value, bool expected)
        {
            Assert.Equal(expected, value.IsInteger());
        }

        [Theory]
        [InlineData(2d, true)]
        [InlineData(3d, false)]
        [InlineData(2.5, false)]
        public void IsEven_Test(double value, bool expected)
        {
            Assert.Equal(expected, value.IsEven());
        }

        [Theory]
        [InlineData(3d, true)]
        [InlineData(2d, false)]
        [InlineData(3.5, false)]
        public void IsOdd_Test(double value, bool expected)
        {
            Assert.Equal(expected, value.IsOdd());
        }

        [Fact]
        public void IsNaN_Test()
        {
            Assert.True(double.NaN.IsNaN());
            Assert.False(1d.IsNaN());
        }

        [Fact]
        public void IsPositiveInfinity_Test()
        {
            Assert.True(double.PositiveInfinity.IsPositiveInfinity());
            Assert.False(1d.IsPositiveInfinity());
        }

        [Fact]
        public void IsNegativeInfinity_Test()
        {
            Assert.True(double.NegativeInfinity.IsNegativeInfinity());
            Assert.False(1d.IsNegativeInfinity());
        }

        [Theory]
        [InlineData(1.2345, 2, 1.23)]
        [InlineData(1.2355, 2, 1.24)]
        [InlineData(-1.2355, 2, -1.24)]
        public void Round_Test(double value, int digits, double expected)
        {
            Assert.Equal(expected, value.Round(digits));
        }

        [Theory]
        [InlineData(1.239, 2, 1.23)]
        [InlineData(-1.239, 2, -1.23)]
        [InlineData(1.2, 0, 1)]
        public void Truncate_Test(double value, int digits, double expected)
        {
            Assert.Equal(expected, value.Truncate(digits));
        }

        [Theory]
        [InlineData(-1.5, 1.5)]
        [InlineData(2.5, 2.5)]
        public void Abs_Test(double value, double expected)
        {
            Assert.Equal(expected, value.Abs());
        }

        [Theory]
        [InlineData(1.2345, 2, "1.23")]
        [InlineData(1.2, 0, "1")]
        public void ToFixedString_Test(double value, int digits, string expected)
        {
            Assert.Equal(expected, value.ToFixedString(digits));
        }

        [Fact]
        public void ToCurrencyString_Test()
        {
            double value = 1234.56;
            string result = value.ToCurrencyString("en-US");
            Assert.Contains("$", result);
            Assert.Contains("1,234.56", result);
        }

        [Theory]
        [InlineData(0.1234, 2, "12.34%")]
        [InlineData(0.1, 0, "10%")]
        public void ToPercentString_Test(double value, int digits, string expected)
        {
            Assert.Equal(expected, value.ToPercentString(digits));
        }

        [Theory]
        [InlineData(12345.6789, 2, "1.23E+004")]
        [InlineData(0.00123, 3, "1.230E-003")]
        public void ToScientificString_Test(double value, int digits, string expectedStart)
        {
            Assert.StartsWith(expectedStart, value.ToScientificString(digits));
        }

        [Theory]
        [InlineData(1.2, 2.3, 2.3)]
        [InlineData(5.5, 2.2, 5.5)]
        public void Max_Test(double a, double b, double expected)
        {
            Assert.Equal(expected, a.Max(b));
        }

        [Theory]
        [InlineData(1.2, 2.3, 1.2)]
        [InlineData(5.5, 2.2, 2.2)]
        public void Min_Test(double a, double b, double expected)
        {
            Assert.Equal(expected, a.Min(b));
        }

        [Theory]
        [InlineData(5, 1, 10, true)]
        [InlineData(0, 1, 10, false)]
        [InlineData(10, 1, 10, true)]
        public void IsBetween_Test(double value, double min, double max, bool expected)
        {
            Assert.Equal(expected, value.IsBetween(min, max));
        }

        [Theory]
        [InlineData(5, 1, 10, 5)]
        [InlineData(0, 1, 10, 1)]
        [InlineData(11, 1, 10, 10)]
        public void Clamp_Test(double value, double min, double max, double expected)
        {
            Assert.Equal(expected, value.Clamp(min, max));
        }

        [Theory]
        [InlineData(1.6, 2)]
        [InlineData(1.4, 1)]
        [InlineData(-1.6, -2)]
        public void ToInt_Test(double value, int expected)
        {
            Assert.Equal(expected, value.ToInt());
        }

        [Theory]
        [InlineData(1.5, 1.5f)]
        [InlineData(-2.3, -2.3f)]
        public void ToFloat_Test(double value, float expected)
        {
            Assert.Equal(expected, value.ToFloat());
        }

        [Theory]
        [InlineData(1.6, 2L)]
        [InlineData(1.4, 1L)]
        [InlineData(-1.6, -2L)]
        public void ToLong_Test(double value, long expected)
        {
            Assert.Equal(expected, value.ToLong());
        }

        [Theory]
        [InlineData(1.5, 1.5)]
        [InlineData(-2.3, -2.3)]
        public void ToDecimal_Test(double value, decimal expected)
        {
            Assert.Equal(expected, value.ToDecimal());
        }

        [Theory]
        [InlineData(0d, false)]
        [InlineData(1d, true)]
        [InlineData(-1d, true)]
        public void ToBool_Test(double value, bool expected)
        {
            Assert.Equal(expected, value.ToBool());
        }

        [Theory]
        [InlineData(1.2, 2.3, 3.5)]
        [InlineData(-1, 1, 0)]
        public void Add_Test(double a, double b, double expected)
        {
            Assert.Equal(expected, a.Add(b));
        }

        [Theory]
        [InlineData(5.5, 2.2, 3.3)]
        [InlineData(1, 1, 0)]
        public void Subtract_Test(double a, double b, double expected)
        {
            Assert.Equal(expected, a.Subtract(b), 10);
        }

        [Theory]
        [InlineData(2, 3, 6)]
        [InlineData(-1, 2, -2)]
        public void Multiply_Test(double a, double b, double expected)
        {
            Assert.Equal(expected, a.Multiply(b));
        }

        [Theory]
        [InlineData(6, 3, 2)]
        [InlineData(1, 0, 0)]
        public void DivideSafe_Test(double a, double b, double expected)
        {
            Assert.Equal(expected, a.DivideSafe(b));
        }

        [Theory]
        [InlineData(7, 3, 1)]
        [InlineData(1, 0, 0)]
        public void Mod_Test(double a, double b, double expected)
        {
            Assert.Equal(expected, a.Mod(b));
        }

        [Theory]
        [InlineData(2, 3, 8)]
        [InlineData(4, 0, 1)]
        public void Pow_Test(double value, int power, double expected)
        {
            Assert.Equal(expected, value.Pow(power));
        }

        [Theory]
        [InlineData(4, 2)]
        [InlineData(9, 3)]
        public void Sqrt_Test(double value, double expected)
        {
            Assert.Equal(expected, value.Sqrt());
        }

        [Theory]
        [InlineData(5, 3, 2)]
        [InlineData(3, 5, 2)]
        public void AbsDiff_Test(double a, double b, double expected)
        {
            Assert.Equal(expected, a.AbsDiff(b));
        }

        [Theory]
        [InlineData(255, "FF")]
        [InlineData(10, "A")]
        public void ToHexString_Test(double value, string expected)
        {
            Assert.Equal(expected, value.ToHexString());
        }

        [Theory]
        [InlineData(5, "101")]
        [InlineData(8, "1000")]
        public void ToBinaryString_Test(double value, string expected)
        {
            Assert.Equal(expected, value.ToBinaryString());
        }

        [Theory]
        [InlineData(8, "10")]
        [InlineData(9, "11")]
        public void ToOctalString_Test(double value, string expected)
        {
            Assert.Equal(expected, value.ToOctalString());
        }

        [Theory]
        [InlineData(123456789, 2, "1.23亿")]
        [InlineData(123456, 2, "12.35万")]
        [InlineData(123, 2, "123.00")]
        public void ToFriendlyString_Test(double value, int digits, string expected)
        {
            Assert.Equal(expected, value.ToFriendlyString(digits));
        }
    }
}