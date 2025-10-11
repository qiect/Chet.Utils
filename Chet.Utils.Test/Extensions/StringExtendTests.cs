using Xunit;

namespace Chet.Utils.Tests.Extensions
{
    public class StringExtendTests
    {
        [Theory]
        [InlineData(null, true)]
        [InlineData("", true)]
        [InlineData("abc", false)]
        public void IsNullOrEmpty_Test(string input, bool expected)
        {
            string str = null;
            Assert.Equal(expected, input.IsNullOrEmpty());
        }

        [Theory]
        [InlineData(null, true)]
        [InlineData("", true)]
        [InlineData("   ", true)]
        [InlineData("abc", false)]
        public void IsNullOrWhiteSpace_Test(string input, bool expected)
        {
            Assert.Equal(expected, input.IsNullOrWhiteSpace());
        }

        [Theory]
        [InlineData("123", true)]
        [InlineData("12.3", true)]
        [InlineData("abc", false)]
        [InlineData("", false)]
        public void IsNumeric_Test(string input, bool expected)
        {
            Assert.Equal(expected, input.IsNumeric());
        }

        [Theory]
        [InlineData("123", true)]
        [InlineData("-123", true)]
        [InlineData("12.3", false)]
        [InlineData("abc", false)]
        public void IsInt_Test(string input, bool expected)
        {
            Assert.Equal(expected, input.IsInt());
        }

        [Theory]
        [InlineData("12.3", true)]
        [InlineData("123", true)]
        [InlineData("abc", false)]
        public void IsFloat_Test(string input, bool expected)
        {
            Assert.Equal(expected, input.IsFloat());
        }

        [Theory]
        [InlineData("12.3", true)]
        [InlineData("123", true)]
        [InlineData("abc", false)]
        public void IsDecimal_Test(string input, bool expected)
        {
            Assert.Equal(expected, input.IsDecimal());
        }

        [Theory]
        [InlineData("d3e1c2b2-4f2e-4c2a-9e2e-1e2e1e2e1e2e", true)]
        [InlineData("not-guid", false)]
        public void IsGuid_Test(string input, bool expected)
        {
            Assert.Equal(expected, input.IsGuid());
        }

        [Theory]
        [InlineData("abc", "ABC", true)]
        [InlineData("abc", "def", false)]
        public void EqualsIgnoreCase_Test(string a, string b, bool expected)
        {
            Assert.Equal(expected, a.EqualsIgnoreCase(b));
        }

        [Theory]
        [InlineData("你好", true)]
        [InlineData("abc", false)]
        public void IsChinese_Test(string input, bool expected)
        {
            Assert.Equal(expected, input.IsChinese());
        }

        [Theory]
        [InlineData("hello你好", true)]
        [InlineData("hello", false)]
        public void HasChinese_Test(string input, bool expected)
        {
            Assert.Equal(expected, input.HasChinese());
        }

        [Theory]
        [InlineData(null, true)]
        [InlineData("", true)]
        [InlineData("null", true)]
        [InlineData("{}", true)]
        [InlineData("[]", true)]
        [InlineData("abc", false)]
        public void IsNull_Test(string input, bool expected)
        {
            Assert.Equal(expected, input.IsNull());
        }

        [Theory]
        [InlineData("abc", true)]
        [InlineData("123", false)]
        public void IsLetterByRegex_Test(string input, bool expected)
        {
            Assert.Equal(expected, input.IsLetterByRegex());
        }

        [Theory]
        [InlineData("123", true)]
        [InlineData("abc", false)]
        public void IsNumByRegex_Test(string input, bool expected)
        {
            Assert.Equal(expected, input.IsNumByRegex());
        }

        [Theory]
        [InlineData("abc123def", "123")]
        [InlineData("no numbers", "")]
        public void ExtractNumByRegex_Test(string input, string expected)
        {
            Assert.Equal(expected, input.ExtractNumByRegex());
        }

        [Theory]
        [InlineData("12.3", true)]
        [InlineData("123", true)]
        [InlineData("abc", false)]
        public void IsFloatByRegex_Test(string input, bool expected)
        {
            Assert.Equal(expected, input.IsFloatByRegex());
        }

        [Theory]
        [InlineData("test@abc.com", true)]
        [InlineData("not-an-email", false)]
        public void IsEmailByRegex_Test(string input, bool expected)
        {
            Assert.Equal(expected, input.IsEmailByRegex());
        }

        [Theory]
        [InlineData("010-12345678", true)]
        [InlineData("12345678", true)]
        [InlineData("not-tel", false)]
        public void IsTelByRegex_Test(string input, bool expected)
        {
            Assert.Equal(expected, input.IsTelByRegex());
        }

        [Theory]
        [InlineData("13812345678", true)]
        [InlineData("23812345678", false)]
        public void IsMobileByRegex_Test(string input, bool expected)
        {
            Assert.Equal(expected, input.IsMobileByRegex());
        }

        [Theory]
        [InlineData("http://abc.com", true)]
        [InlineData("not-url", false)]
        public void IsUrlByRegex_Test(string input, bool expected)
        {
            Assert.Equal(expected, input.IsUrlByRegex());
        }

        [Theory]
        [InlineData("2024-09-30", true)]
        [InlineData("2024/09/30", true)]
        [InlineData("not-date", false)]
        public void IsDateByRegex_Test(string input, bool expected)
        {
            Assert.Equal(expected, input.IsDateByRegex());
        }

        [Theory]
        [InlineData("12:34:56", true)]
        [InlineData("not-time", false)]
        public void IsTimeByRegex_Test(string input, bool expected)
        {
            Assert.Equal(expected, input.IsTimeByRegex());
        }

        [Theory]
        [InlineData("2024-09-30 12:34:56", true)]
        [InlineData("not-datetime", false)]
        public void IsDateTimeByRegex_Test(string input, bool expected)
        {
            Assert.Equal(expected, input.IsDateTimeByRegex());
        }

        [Theory]
        [InlineData("123", 123)]
        [InlineData("abc", 0)]
        public void ToInt_Test(string input, int expected)
        {
            Assert.Equal(expected, input.ToInt());
        }

        [Theory]
        [InlineData("12.3", 12.3f)]
        [InlineData("abc", 0f)]
        public void ToFloat_Test(string input, float expected)
        {
            Assert.Equal(expected, input.ToFloat());
        }

        [Theory]
        [InlineData("12.3", 12.3)]
        [InlineData("abc", 0)]
        public void ToDouble_Test(string input, double expected)
        {
            Assert.Equal(expected, input.ToDouble());
        }

        [Theory]
        [InlineData("12.3456", 2, 12.35)]
        [InlineData("abc", 2, 0)]
        public void ToDoubleRound_Test(string input, int digits, double expected)
        {
            Assert.Equal(expected, input.ToDoubleRound(digits));
        }

        [Theory]
        [InlineData("12.3456", 2, 12.34)]
        [InlineData("abc", 2, 0)]
        public void ToDoubleTruncate_Test(string input, int digits, double expected)
        {
            Assert.Equal(expected, input.ToDoubleTruncate(digits));
        }

        [Theory]
        [InlineData("12.3456", 2, 12.35f)]
        [InlineData("abc", 2, 0f)]
        public void ToFloatRound_Test(string input, int digits, float expected)
        {
            Assert.Equal(expected, input.ToFloatRound(digits));
        }

        [Theory]
        [InlineData("12.3456", 2, 12.34f)]
        [InlineData("abc", 2, 0f)]
        public void ToFloatTruncate_Test(string input, int digits, float expected)
        {
            Assert.Equal(expected, input.ToFloatTruncate(digits));
        }

        [Theory]
        [InlineData("12.3456", 2, "12.35")]
        [InlineData("abc", 2, "abc")]
        public void KeepDecimal_Test(string input, int digits, string expected)
        {
            Assert.Equal(expected, input.KeepDecimal(digits));
        }

        [Theory]
        [InlineData("12.3", 12.3)]
        [InlineData("abc", 0)]
        public void ToDecimal_Test(string input, decimal expected)
        {
            Assert.Equal(expected, input.ToDecimal());
        }

        [Theory]
        [InlineData("true", true)]
        [InlineData("false", false)]
        [InlineData("abc", false)]
        public void ToBool_Test(string input, bool expected)
        {
            Assert.Equal(expected, input.ToBool());
        }

        [Fact]
        public void ToGuid_Test()
        {
            var guid = Guid.NewGuid();
            Assert.Equal(guid, guid.ToString().ToGuid());
            Assert.Equal(Guid.Empty, "not-guid".ToGuid());
        }

        [Fact]
        public void ToDateTime_Test()
        {
            var dt = DateTime.Now;
            Assert.Equal(dt.Date, dt.ToString("yyyy-MM-dd").ToDateTime().Date);
            Assert.Equal(DateTime.MinValue, "not-date".ToDateTime());
        }

        [Theory]
        [InlineData(" abc ", "abc")]
        [InlineData(null, "")]
        public void TrimSafe_Test(string input, string expected)
        {
            Assert.Equal(expected, input.TrimSafe());
        }

        [Theory]
        [InlineData(" a b c ", "abc")]
        [InlineData("", "")]
        public void RemoveWhiteSpace_Test(string input, string expected)
        {
            Assert.Equal(expected, input.RemoveWhiteSpace());
        }

        [Theory]
        [InlineData("abcdef", 2, 3, "cde")]
        [InlineData("abc", 10, 2, "")]
        public void SubstringSafe_Test(string input, int start, int len, string expected)
        {
            Assert.Equal(expected, input.SubstringSafe(start, len));
        }

        [Theory]
        [InlineData("abcdef", 3, "abc")]
        [InlineData("ab", 5, "ab")]
        public void Left_Test(string input, int len, string expected)
        {
            Assert.Equal(expected, input.Left(len));
        }

        [Theory]
        [InlineData("abcdef", 3, "def")]
        [InlineData("ab", 5, "ab")]
        public void Right_Test(string input, int len, string expected)
        {
            Assert.Equal(expected, input.Right(len));
        }

        [Theory]
        [InlineData("abc", "cba")]
        [InlineData("", "")]
        public void Reverse_Test(string input, string expected)
        {
            Assert.Equal(expected, input.Reverse());
        }

        [Theory]
        [InlineData("abc!@#", "abc")]
        [InlineData("123 456", "123 456")]
        public void RemoveSpecialChars_Test(string input, string expected)
        {
            Assert.Equal(expected, input.RemoveSpecialChars());
        }

        [Theory]
        [InlineData("Abc", "abc")]
        [InlineData("A", "a")]
        public void ToCamelCase_Test(string input, string expected)
        {
            Assert.Equal(expected, input.ToCamelCase());
        }

        [Theory]
        [InlineData("abc", "Abc")]
        [InlineData("a", "A")]
        public void ToPascalCase_Test(string input, string expected)
        {
            Assert.Equal(expected, input.ToPascalCase());
        }

        [Theory]
        [InlineData("ab", 3, "ababab")]
        [InlineData("ab", 0, "")]
        public void Repeat_Test(string input, int count, string expected)
        {
            Assert.Equal(expected, input.Repeat(count));
        }

        [Theory]
        [InlineData("abcABCabc", "abc", "x", "xxx")]
        [InlineData("abc", "def", "x", "abc")]
        public void ReplaceIgnoreCase_Test(string input, string oldValue, string newValue, string expected)
        {
            Assert.Equal(expected, input.ReplaceIgnoreCase(oldValue, newValue));
        }

        [Fact]
        public void ToMd5_Test()
        {
            var md5 = "abc".ToMd5();
            Assert.Equal(32, md5.Length);
            Assert.True(System.Text.RegularExpressions.Regex.IsMatch(md5, "^[a-f0-9]{32}$"));
        }

        [Theory]
        [InlineData("abcDEF", "def", true)]
        [InlineData("abc", "xyz", false)]
        public void ContainsIgnoreCase_Test(string input, string sub, bool expected)
        {
            Assert.Equal(expected, input.ContainsIgnoreCase(sub));
        }

        [Theory]
        [InlineData("a,b,c", ',', new[] { "a", "b", "c" })]
        [InlineData("", ',', new string[0])]
        public void SplitSafe_Test(string input, char sep, string[] expected)
        {
            Assert.Equal(expected, input.SplitSafe(sep));
        }
    }
}