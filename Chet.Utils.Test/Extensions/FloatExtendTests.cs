using Xunit;

namespace Chet.Utils.Tests.Extensions
{
    public class FloatExtendTests
    {
        [Theory]
        [InlineData(0f, true)]
        [InlineData(1.23f, false)]
        public void IsZero_Test(float value, bool expected)
        {
            Assert.Equal(expected, value.IsZero());
        }

        [Theory]
        [InlineData(1.23f, true)]
        [InlineData(0f, false)]
        [InlineData(-1.23f, false)]
        public void IsPositive_Test(float value, bool expected)
        {
            Assert.Equal(expected, value.IsPositive());
        }

        [Theory]
        [InlineData(-1.23f, true)]
        [InlineData(0f, false)]
        [InlineData(1.23f, false)]
        public void IsNegative_Test(float value, bool expected)
        {
            Assert.Equal(expected, value.IsNegative());
        }

        [Theory]
        [InlineData(2f, true)]
        [InlineData(2.5f, false)]
        [InlineData(-3f, true)]
        public void IsInteger_Test(float value, bool expected)
        {
            Assert.Equal(expected, value.IsInteger());
        }

        [Theory]
        [InlineData(2f, true)]
        [InlineData(3f, false)]
        [InlineData(2.5f, false)]
        public void IsEven_Test(float value, bool expected)
        {
            Assert.Equal(expected, value.IsEven());
        }

        [Theory]
        [InlineData(3f, true)]
        [InlineData(2f, false)]
        [InlineData(3.5f, false)]
        public void IsOdd_Test(float value, bool expected)
        {
            Assert.Equal(expected, value.IsOdd());
        }

        [Fact]
        public void IsNaN_Test()
        {
            Assert.True(float.NaN.IsNaN());
            Assert.False(1.23f.IsNaN());
        }

        [Fact]
        public void IsPositiveInfinity_Test()
        {
            Assert.True(float.PositiveInfinity.IsPositiveInfinity());
            Assert.False(1.23f.IsPositiveInfinity());
        }

        [Fact]
        public void IsNegativeInfinity_Test()
        {
            Assert.True(float.NegativeInfinity.IsNegativeInfinity());
            Assert.False(1.23f.IsNegativeInfinity());
        }

        [Theory]
        [InlineData(1.2345f, 2, 1.23f)]
        [InlineData(1.2355f, 2, 1.24f)]
        public void Round_Test(float value, int digits, float expected)
        {
            Assert.Equal(expected, value.Round(digits));
        }

        [Theory]
        [InlineData(1.239f, 2, 1.23f)]
        [InlineData(-1.239f, 2, -1.23f)]
        public void Truncate_Test(float value, int digits, float expected)
        {
            Assert.Equal(expected, value.Truncate(digits));
        }

        [Theory]
        [InlineData(-1.23f, 1.23f)]
        [InlineData(1.23f, 1.23f)]
        public void Abs_Test(float value, float expected)
        {
            Assert.Equal(expected, value.Abs());
        }

        [Theory]
        [InlineData(1.2345f, 2, "1.23")]
        [InlineData(1.2f, 3, "1.200")]
        public void ToFixedString_Test(float value, int digits, string expected)
        {
            Assert.Equal(expected, value.ToFixedString(digits));
        }

        [Fact]
        public void ToCurrencyString_Test()
        {
            float value = 1234.56f;
            string result = value.ToCurrencyString("zh-CN");
            Assert.Contains("¥", result);
            Assert.Contains("1,234.56", result);
        }

        [Theory]
        [InlineData(0.1234f, 2, "12.34%")]
        [InlineData(1f, 0, "100%")]
        public void ToPercentString_Test(float value, int digits, string expected)
        {
            Assert.Equal(expected, value.ToPercentString(digits));
        }

        [Theory]
        [InlineData(12345.6789f, 2, "1.23E+004")]
        [InlineData(0.00123f, 3, "1.230E-003")]
        public void ToScientificString_Test(float value, int digits, string expected)
        {
            Assert.Equal(expected, value.ToScientificString(digits));
        }

        [Theory]
        [InlineData(1.2f, 2.3f, 2.3f)]
        [InlineData(5.6f, 2.3f, 5.6f)]
        public void Max_Test(float value, float other, float expected)
        {
            Assert.Equal(expected, value.Max(other));
        }

        [Theory]
        [InlineData(1.2f, 2.3f, 1.2f)]
        [InlineData(5.6f, 2.3f, 2.3f)]
        public void Min_Test(float value, float other, float expected)
        {
            Assert.Equal(expected, value.Min(other));
        }

        [Theory]
        [InlineData(5f, 1f, 10f, true)]
        [InlineData(0f, 1f, 10f, false)]
        public void IsBetween_Test(float value, float min, float max, bool expected)
        {
            Assert.Equal(expected, value.IsBetween(min, max));
        }

        [Theory]
        [InlineData(5f, 1f, 10f, 5f)]
        [InlineData(0f, 1f, 10f, 1f)]
        [InlineData(11f, 1f, 10f, 10f)]
        public void Clamp_Test(float value, float min, float max, float expected)
        {
            Assert.Equal(expected, value.Clamp(min, max));
        }

        [Theory]
        [InlineData(1.6f, 2)]
        [InlineData(-1.6f, -2)]
        [InlineData(0f, 0)]
        public void ToInt_Test(float value, int expected)
        {
            Assert.Equal(expected, value.ToInt());
        }

        [Theory]
        [InlineData(1.23f, 1.23)]
        [InlineData(-1.23f, -1.23)]
        public void ToDouble_Test(float value, double expected)
        {
            Assert.Equal(expected, value.ToDouble(), 5);
        }

        [Theory]
        [InlineData(1.6f, 2L)]
        [InlineData(-1.6f, -2L)]
        [InlineData(0f, 0L)]
        public void ToLong_Test(float value, long expected)
        {
            Assert.Equal(expected, value.ToLong());
        }

        [Theory]
        [InlineData(1.23f, 1.23)]
        [InlineData(-1.23f, -1.23)]
        public void ToDecimal_Test(float value, decimal expected)
        {
            Assert.Equal(expected, value.ToDecimal());
        }

        [Theory]
        [InlineData(0f, false)]
        [InlineData(1.23f, true)]
        [InlineData(-1.23f, true)]
        public void ToBool_Test(float value, bool expected)
        {
            Assert.Equal(expected, value.ToBool());
        }

        [Theory]
        [InlineData(1.2f, 2.3f, 3.5f)]
        [InlineData(-1.2f, 2.3f, 1.1f)]
        public void Add_Test(float value, float other, float expected)
        {
            Assert.Equal(expected, value.Add(other), 5);
        }

        [Theory]
        [InlineData(3.5f, 2.3f, 1.2f)]
        [InlineData(2.3f, 3.5f, -1.2f)]
        public void Subtract_Test(float value, float other, float expected)
        {
            Assert.Equal(expected, value.Subtract(other), 5);
        }

        [Theory]
        [InlineData(2f, 3f, 6f)]
        [InlineData(-2f, 3f, -6f)]
        public void Multiply_Test(float value, float other, float expected)
        {
            Assert.Equal(expected, value.Multiply(other), 5);
        }

        [Theory]
        [InlineData(6f, 3f, 2f)]
        [InlineData(6f, 0f, 0f)]
        public void DivideSafe_Test(float value, float other, float expected)
        {
            Assert.Equal(expected, value.DivideSafe(other), 5);
        }

        [Theory]
        [InlineData(7f, 3f, 1f)]
        [InlineData(7f, 0f, 0f)]
        public void Mod_Test(float value, float other, float expected)
        {
            Assert.Equal(expected, value.Mod(other), 5);
        }

        [Theory]
        [InlineData(2f, 3, 8f)]
        [InlineData(4f, 0, 1f)]
        public void Pow_Test(float value, int power, float expected)
        {
            Assert.Equal(expected, value.Pow(power), 5);
        }

        [Theory]
        [InlineData(4f, 2f)]
        [InlineData(9f, 3f)]
        public void Sqrt_Test(float value, float expected)
        {
            Assert.Equal(expected, value.Sqrt(), 5);
        }

        [Theory]
        [InlineData(5f, 3f, 2f)]
        [InlineData(3f, 5f, 2f)]
        public void AbsDiff_Test(float value, float other, float expected)
        {
            Assert.Equal(expected, value.AbsDiff(other), 5);
        }

        [Theory]
        [InlineData(255f, "FF")]
        [InlineData(10f, "A")]
        public void ToHexString_Test(float value, string expected)
        {
            Assert.Equal(expected, value.ToHexString());
        }

        [Theory]
        [InlineData(10f, "1010")]
        [InlineData(7f, "111")]
        public void ToBinaryString_Test(float value, string expected)
        {
            Assert.Equal(expected, value.ToBinaryString());
        }

        [Theory]
        [InlineData(8f, "10")]
        [InlineData(9f, "11")]
        public void ToOctalString_Test(float value, string expected)
        {
            Assert.Equal(expected, value.ToOctalString());
        }

        [Theory]
        [InlineData(123456789f, 2, "1.23亿")]
        [InlineData(123456f, 1, "12.3万")]
        [InlineData(123f, 2, "123.00")]
        public void ToFriendlyString_Test(float value, int digits, string expected)
        {
            Assert.Equal(expected, value.ToFriendlyString(digits));
        }
    }
}