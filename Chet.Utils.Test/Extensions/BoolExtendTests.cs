using Xunit;

namespace Chet.Utils.Tests.Extensions
{
    public class BoolExtendTests
    {
        public enum TestEnum
        {
            A = 1,
            B = 2
        }

        [Theory]
        [InlineData(true, true)]
        [InlineData(false, false)]
        public void IsTrue_Test(bool value, bool expected)
        {
            Assert.Equal(expected, value.IsTrue());
        }

        [Theory]
        [InlineData(true, false)]
        [InlineData(false, true)]
        public void IsFalse_Test(bool value, bool expected)
        {
            Assert.Equal(expected, value.IsFalse());
        }

        [Theory]
        [InlineData(true, false)]
        [InlineData(false, true)]
        public void Not_Test(bool value, bool expected)
        {
            Assert.Equal(expected, value.Not());
        }

        [Theory]
        [InlineData(true, 1)]
        [InlineData(false, 0)]
        public void ToInt_Test(bool value, int expected)
        {
            Assert.Equal(expected, value.ToInt());
        }

        [Theory]
        [InlineData(true, "True")]
        [InlineData(false, "False")]
        public void ToStringValue_Test(bool value, string expected)
        {
            Assert.Equal(expected, value.ToStringValue());
        }

        [Theory]
        [InlineData(true, "是")]
        [InlineData(false, "否")]
        public void ToChineseString_Test(bool value, string expected)
        {
            Assert.Equal(expected, value.ToChineseString());
        }

        [Theory]
        [InlineData(true, "真", "假", "真")]
        [InlineData(false, "真", "假", "假")]
        public void ToCustomString_Test(bool value, string trueStr, string falseStr, string expected)
        {
            Assert.Equal(expected, value.ToCustomString(trueStr, falseStr));
        }

        [Theory]
        [InlineData(true, "Yes")]
        [InlineData(false, "No")]
        public void ToYesNo_Test(bool value, string expected)
        {
            Assert.Equal(expected, value.ToYesNo());
        }

        [Theory]
        [InlineData(true, "On")]
        [InlineData(false, "Off")]
        public void ToOnOff_Test(bool value, string expected)
        {
            Assert.Equal(expected, value.ToOnOff());
        }

        [Theory]
        [InlineData(true, "1")]
        [InlineData(false, "0")]
        public void ToOneZero_Test(bool value, string expected)
        {
            Assert.Equal(expected, value.ToOneZero());
        }

        [Theory]
        [InlineData(true, "Y")]
        [InlineData(false, "N")]
        public void ToYN_Test(bool value, string expected)
        {
            Assert.Equal(expected, value.ToYN());
        }

        [Theory]
        [InlineData(true, true, true)]
        [InlineData(true, false, false)]
        [InlineData(false, true, false)]
        [InlineData(false, false, false)]
        public void And_Test(bool a, bool b, bool expected)
        {
            Assert.Equal(expected, a.And(b));
        }

        [Theory]
        [InlineData(true, true, true)]
        [InlineData(true, false, true)]
        [InlineData(false, true, true)]
        [InlineData(false, false, false)]
        public void Or_Test(bool a, bool b, bool expected)
        {
            Assert.Equal(expected, a.Or(b));
        }

        [Theory]
        [InlineData(true, true, false)]
        [InlineData(true, false, true)]
        [InlineData(false, true, true)]
        [InlineData(false, false, false)]
        public void Xor_Test(bool a, bool b, bool expected)
        {
            Assert.Equal(expected, a.Xor(b));
        }

        [Theory]
        [InlineData(true, true, true)]
        [InlineData(true, false, false)]
        [InlineData(false, true, false)]
        [InlineData(false, false, true)]
        public void Xnor_Test(bool a, bool b, bool expected)
        {
            Assert.Equal(expected, a.Xnor(b));
        }

        [Theory]
        [InlineData(true, TestEnum.A, TestEnum.B, TestEnum.A)]
        [InlineData(false, TestEnum.A, TestEnum.B, TestEnum.B)]
        public void ToEnum_Test(bool value, TestEnum trueEnum, TestEnum falseEnum, TestEnum expected)
        {
            Assert.Equal(expected, value.ToEnum(trueEnum, falseEnum));
        }

        [Theory]
        [InlineData(true, false, true)]
        [InlineData(false, false, false)]
        [InlineData(true, true, null)]
        [InlineData(false, true, null)]
        public void ToNullable_Test(bool value, bool nullable, bool? expected)
        {
            Assert.Equal(expected, value.ToNullable(nullable));
        }

        [Theory]
        [InlineData(true, 10, 20, 10)]
        [InlineData(false, 10, 20, 20)]
        [InlineData(true, "A", "B", "A")]
        [InlineData(false, "A", "B", "B")]
        public void ToValue_Test<T>(bool value, T trueValue, T falseValue, T expected)
        {
            Assert.Equal(expected, value.ToValue(trueValue, falseValue));
        }

        [Theory]
        [InlineData(true, (byte)1)]
        [InlineData(false, (byte)0)]
        public void ToByte_Test(bool value, byte expected)
        {
            Assert.Equal(expected, value.ToByte());
        }

        [Theory]
        [InlineData(true, (short)1)]
        [InlineData(false, (short)0)]
        public void ToShort_Test(bool value, short expected)
        {
            Assert.Equal(expected, value.ToShort());
        }

        [Theory]
        [InlineData(true, 1L)]
        [InlineData(false, 0L)]
        public void ToLong_Test(bool value, long expected)
        {
            Assert.Equal(expected, value.ToLong());
        }

        [Theory]
        [InlineData(true, 1f)]
        [InlineData(false, 0f)]
        public void ToFloat_Test(bool value, float expected)
        {
            Assert.Equal(expected, value.ToFloat());
        }

        [Theory]
        [InlineData(true, 1d)]
        [InlineData(false, 0d)]
        public void ToDouble_Test(bool value, double expected)
        {
            Assert.Equal(expected, value.ToDouble());
        }

        [Theory]
        [InlineData(true, 1.0)]
        [InlineData(false, 0.0)]
        public void ToDecimal_Test(bool value, decimal expected)
        {
            Assert.Equal(expected, value.ToDecimal());
        }

        [Theory]
        [InlineData(true, "False")]
        [InlineData(false, "True")]
        public void ToReverseString_Test(bool value, string expected)
        {
            Assert.Equal(expected, value.ToReverseString());
        }

        [Theory]
        [InlineData(true, "否")]
        [InlineData(false, "是")]
        public void ToReverseChineseString_Test(bool value, string expected)
        {
            Assert.Equal(expected, value.ToReverseChineseString());
        }
    }
}