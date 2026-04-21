using System.Text.RegularExpressions;
using Xunit;

namespace Chet.Utils.Helpers.Tests;

public class RegexHelperTests
{
    #region 常用正则表达式模式测试

    [Fact]
    public void EmailPattern_IsValidPattern()
    {
        Assert.NotNull(RegexHelper.EmailPattern);
        Assert.True(Regex.IsMatch("test@example.com", RegexHelper.EmailPattern));
    }

    [Fact]
    public void MobilePhonePattern_IsValidPattern()
    {
        Assert.NotNull(RegexHelper.MobilePhonePattern);
        Assert.True(Regex.IsMatch("13812345678", RegexHelper.MobilePhonePattern));
    }

    [Fact]
    public void UrlPattern_IsValidPattern()
    {
        Assert.NotNull(RegexHelper.UrlPattern);
        Assert.True(Regex.IsMatch("https://example.com", RegexHelper.UrlPattern, RegexOptions.IgnoreCase));
    }

    #endregion

    #region 基础验证方法测试

    [Fact]
    public void IsMatch_ValidPattern_ReturnsTrue()
    {
        var result = RegexHelper.IsMatch("test@example.com", RegexHelper.EmailPattern);

        Assert.True(result);
    }

    [Fact]
    public void IsMatch_InvalidPattern_ReturnsFalse()
    {
        var result = RegexHelper.IsMatch("invalid-email", RegexHelper.EmailPattern);

        Assert.False(result);
    }

    [Fact]
    public void IsMatch_NullInput_ReturnsFalse()
    {
        var result = RegexHelper.IsMatch(null, RegexHelper.EmailPattern);

        Assert.False(result);
    }

    [Fact]
    public void IsMatch_EmptyInput_ReturnsFalse()
    {
        var result = RegexHelper.IsMatch("", RegexHelper.EmailPattern);

        Assert.False(result);
    }

    [Theory]
    [InlineData("test@example.com", true)]
    [InlineData("user.name@domain.co.uk", true)]
    [InlineData("invalid-email", false)]
    [InlineData("test@", false)]
    [InlineData("@example.com", false)]
    public void IsValidEmail_VariousInputs_ReturnsExpectedResult(string email, bool expected)
    {
        var result = RegexHelper.IsValidEmail(email);
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData("13812345678", true)]
    [InlineData("15912345678", true)]
    [InlineData("18812345678", true)]
    [InlineData("12812345678", false)]
    [InlineData("12345678901", false)]
    [InlineData("1381234567", false)]
    public void IsValidMobilePhone_VariousInputs_ReturnsExpectedResult(string mobile, bool expected)
    {
        var result = RegexHelper.IsValidMobilePhone(mobile);
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData("010-12345678", true)]
    [InlineData("021-87654321-123", true)]
    [InlineData("12345678", true)]
    [InlineData("1234567", true)]
    public void IsValidTelephone_VariousInputs_ReturnsExpectedResult(string telephone, bool expected)
    {
        var result = RegexHelper.IsValidTelephone(telephone);
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData("110101199001011234", false)]
    [InlineData("11010119900101123x", false)]
    [InlineData("11010119900101123X", false)]
    [InlineData("123456789012345678", false)]
    public void IsValidIdCard_VariousInputs_ReturnsExpectedResult(string idCard, bool expected)
    {
        var result = RegexHelper.IsValidIdCard(idCard);
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData("https://www.example.com", true)]
    [InlineData("http://example.com/path?query=value", true)]
    [InlineData("ftp://ftp.example.com", true)]
    [InlineData("invalid-url", false)]
    [InlineData("www.example.com", false)]
    public void IsValidUrl_VariousInputs_ReturnsExpectedResult(string url, bool expected)
    {
        var result = RegexHelper.IsValidUrl(url);
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData("192.168.1.1", true)]
    [InlineData("255.255.255.255", true)]
    [InlineData("0.0.0.0", true)]
    [InlineData("256.1.1.1", false)]
    [InlineData("192.168.1", false)]
    public void IsValidIPv4_VariousInputs_ReturnsExpectedResult(string ip, bool expected)
    {
        var result = RegexHelper.IsValidIPv4(ip);
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData("100000", true)]
    [InlineData("200000", true)]
    [InlineData("10000", false)]
    [InlineData("1000000", false)]
    public void IsValidPostalCode_VariousInputs_ReturnsExpectedResult(string postalCode, bool expected)
    {
        var result = RegexHelper.IsValidPostalCode(postalCode);
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData("6222021234567890123", true)]
    [InlineData("6222021234567890", true)]
    [InlineData("123456789012345", false)]
    public void IsValidBankCard_VariousInputs_ReturnsExpectedResult(string bankCard, bool expected)
    {
        var result = RegexHelper.IsValidBankCard(bankCard);
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData("京A12345", true)]
    [InlineData("沪B12345D", true)]
    [InlineData("粤Z1234港", true)]
    [InlineData("A12345", false)]
    public void IsValidLicensePlate_VariousInputs_ReturnsExpectedResult(string plate, bool expected)
    {
        var result = RegexHelper.IsValidLicensePlate(plate);
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData("12345", true)]
    [InlineData("1234567890", true)]
    [InlineData("1234", false)]
    [InlineData("12345678901", true)]
    public void IsValidQQ_VariousInputs_ReturnsExpectedResult(string qq, bool expected)
    {
        var result = RegexHelper.IsValidQQ(qq);
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData("#FF0000", true)]
    [InlineData("#fff", true)]
    [InlineData("FF0000", true)]
    [InlineData("#GG0000", false)]
    public void IsValidHexColor_VariousInputs_ReturnsExpectedResult(string color, bool expected)
    {
        var result = RegexHelper.IsValidHexColor(color);
        Assert.Equal(expected, result);
    }

    #endregion

    #region 格式验证方法测试

    [Theory]
    [InlineData("123", true)]
    [InlineData("-123", true)]
    [InlineData("0", true)]
    [InlineData("12.3", false)]
    [InlineData("abc", false)]
    public void IsInteger_VariousInputs_ReturnsExpectedResult(string input, bool expected)
    {
        var result = RegexHelper.IsInteger(input);
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData("123", true)]
    [InlineData("1", true)]
    [InlineData("0", false)]
    [InlineData("-1", false)]
    public void IsPositiveInteger_VariousInputs_ReturnsExpectedResult(string input, bool expected)
    {
        var result = RegexHelper.IsPositiveInteger(input);
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData("123.45", true)]
    [InlineData("-123.45", true)]
    [InlineData("123", true)]
    [InlineData("abc", false)]
    public void IsFloat_VariousInputs_ReturnsExpectedResult(string input, bool expected)
    {
        var result = RegexHelper.IsFloat(input);
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData("你好世界", true)]
    [InlineData("Hello", false)]
    [InlineData("你好World", false)]
    public void IsChinese_VariousInputs_ReturnsExpectedResult(string input, bool expected)
    {
        var result = RegexHelper.IsChinese(input);
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData("Hello", true)]
    [InlineData("HELLO", true)]
    [InlineData("Hello123", false)]
    public void IsLetter_VariousInputs_ReturnsExpectedResult(string input, bool expected)
    {
        var result = RegexHelper.IsLetter(input);
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData("Hello123", true)]
    [InlineData("Hello", true)]
    [InlineData("123", true)]
    [InlineData("Hello_123", false)]
    public void IsAlphanumeric_VariousInputs_ReturnsExpectedResult(string input, bool expected)
    {
        var result = RegexHelper.IsAlphanumeric(input);
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData("2024-01-15", true)]
    [InlineData("2024-12-31", true)]
    [InlineData("2024-13-01", false)]
    [InlineData("2024-01-32", false)]
    public void IsValidDate_VariousInputs_ReturnsExpectedResult(string input, bool expected)
    {
        var result = RegexHelper.IsValidDate(input);
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData("12:30:45", true)]
    [InlineData("23:59:59", true)]
    [InlineData("24:00:00", false)]
    [InlineData("12:60:00", false)]
    public void IsValidTime_VariousInputs_ReturnsExpectedResult(string input, bool expected)
    {
        var result = RegexHelper.IsValidTime(input);
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData("Abc123!@", 4)]
    [InlineData("abcdefgh", 2)]
    [InlineData("ABCDEFGH", 2)]
    [InlineData("12345678", 2)]
    [InlineData("Abc123", 3)]
    [InlineData("", 0)]
    public void GetPasswordStrength_VariousInputs_ReturnsExpectedStrength(string password, int expected)
    {
        var result = RegexHelper.GetPasswordStrength(password);
        Assert.Equal(expected, result);
    }

    [Fact]
    public void IsLengthInRange_ValidRange_ReturnsTrue()
    {
        var result = RegexHelper.IsLengthInRange("Hello", 1, 10);
        Assert.True(result);
    }

    [Fact]
    public void IsLengthInRange_OutOfRange_ReturnsFalse()
    {
        var result = RegexHelper.IsLengthInRange("Hello World", 1, 5);
        Assert.False(result);
    }

    [Fact]
    public void ContainsChinese_TextWithChinese_ReturnsTrue()
    {
        var result = RegexHelper.ContainsChinese("Hello 世界");
        Assert.True(result);
    }

    [Fact]
    public void ContainsChinese_TextWithoutChinese_ReturnsFalse()
    {
        var result = RegexHelper.ContainsChinese("Hello World");
        Assert.False(result);
    }

    [Fact]
    public void ContainsDigit_TextWithDigit_ReturnsTrue()
    {
        var result = RegexHelper.ContainsDigit("Hello123");
        Assert.True(result);
    }

    [Fact]
    public void ContainsDigit_TextWithoutDigit_ReturnsFalse()
    {
        var result = RegexHelper.ContainsDigit("Hello World");
        Assert.False(result);
    }

    [Fact]
    public void ContainsLetter_TextWithLetter_ReturnsTrue()
    {
        var result = RegexHelper.ContainsLetter("123abc");
        Assert.True(result);
    }

    [Fact]
    public void ContainsLetter_TextWithoutLetter_ReturnsFalse()
    {
        var result = RegexHelper.ContainsLetter("123456");
        Assert.False(result);
    }

    [Fact]
    public void ContainsSpecialChar_TextWithSpecialChar_ReturnsTrue()
    {
        var result = RegexHelper.ContainsSpecialChar("Hello@World");
        Assert.True(result);
    }

    [Fact]
    public void ContainsSpecialChar_TextWithoutSpecialChar_ReturnsFalse()
    {
        var result = RegexHelper.ContainsSpecialChar("HelloWorld123");
        Assert.False(result);
    }

    #endregion

    #region 匹配操作测试

    [Fact]
    public void Match_ValidPattern_ReturnsMatch()
    {
        var match = RegexHelper.Match("Hello 123 World", @"\d+");

        Assert.NotNull(match);
        Assert.True(match!.Success);
        Assert.Equal("123", match.Value);
    }

    [Fact]
    public void Match_NoMatch_ReturnsNull()
    {
        var match = RegexHelper.Match("Hello World", @"\d+");

        Assert.Null(match);
    }

    [Fact]
    public void Matches_MultipleMatches_ReturnsAllMatches()
    {
        var matches = RegexHelper.Matches("Hello 123 World 456", @"\d+");

        Assert.Equal(2, matches.Count);
    }

    [Fact]
    public void GetMatchValues_ReturnsValuesList()
    {
        var values = RegexHelper.GetMatchValues("Hello 123 World 456", @"\d+");

        Assert.Equal(2, values.Count);
        Assert.Contains("123", values);
        Assert.Contains("456", values);
    }

    [Fact]
    public void GetGroupValue_ReturnsGroupValue()
    {
        var value = RegexHelper.GetGroupValue("13812345678", @"(?<prefix>1[3-9])(?<number>\d{8})", "prefix");

        Assert.Equal("13", value);
    }

    [Fact]
    public void GetGroupValues_ReturnsAllGroupValues()
    {
        var values = RegexHelper.GetGroupValues("13812345678 15987654321", @"(?<prefix>1[3-9])\d{8}", "prefix");

        Assert.Equal(2, values.Count);
        Assert.Contains("13", values);
        Assert.Contains("15", values);
    }

    [Fact]
    public void GetAllGroupValues_ReturnsAllNamedGroups()
    {
        var groups = RegexHelper.GetAllGroupValues("13812345678", @"(?<prefix>1[3-9])(?<number>\d{8})");

        Assert.True(groups.ContainsKey("prefix"));
        Assert.True(groups.ContainsKey("number"));
    }

    #endregion

    #region 替换操作测试

    [Fact]
    public void Replace_ReplacesMatches()
    {
        var result = RegexHelper.Replace("Hello 123 World", @"\d+", "XXX");

        Assert.Equal("Hello XXX World", result);
    }

    [Fact]
    public void Replace_WithEvaluator_ReplacesMatches()
    {
        var result = RegexHelper.Replace("Hello 123 World 456", @"\d+", match => $"[{match.Value}]");

        Assert.Equal("Hello [123] World [456]", result);
    }

    [Fact]
    public void Remove_RemovesMatches()
    {
        var result = RegexHelper.Remove("Hello 123 World 456", @"\d+");

        Assert.Equal("Hello  World ", result);
    }

    [Fact]
    public void RemoveHtmlTags_RemovesTags()
    {
        var result = RegexHelper.RemoveHtmlTags("<p>Hello</p><div>World</div>");

        Assert.Equal("HelloWorld", result);
    }

    [Fact]
    public void RemoveWhitespace_RemovesAllWhitespace()
    {
        var result = RegexHelper.RemoveWhitespace("Hello World\t\nTest");

        Assert.Equal("HelloWorldTest", result);
    }

    [Fact]
    public void CompressWhitespace_CompressesWhitespace()
    {
        var result = RegexHelper.CompressWhitespace("Hello    World\t\tTest");

        Assert.Equal("Hello World Test", result);
    }

    [Fact]
    public void ReplaceIfMatch_MatchingInput_Replaces()
    {
        var result = RegexHelper.ReplaceIfMatch("Hello 123", @"\d+", "XXX");

        Assert.Equal("Hello XXX", result);
    }

    [Fact]
    public void ReplaceIfMatch_NonMatchingInput_ReturnsOriginal()
    {
        var result = RegexHelper.ReplaceIfMatch("Hello World", @"\d+", "XXX");

        Assert.Equal("Hello World", result);
    }

    #endregion

    #region 提取操作测试

    [Fact]
    public void ExtractNumbers_ReturnsNumbers()
    {
        var numbers = RegexHelper.ExtractNumbers("Price: $123.45, Tax: $67.89");

        Assert.True(numbers.Count >= 4);
    }

    [Fact]
    public void ExtractIntegers_ReturnsIntegers()
    {
        var integers = RegexHelper.ExtractIntegers("Values: -10, 20, 30");

        Assert.Equal(3, integers.Count);
        Assert.Contains(-10, integers);
        Assert.Contains(20, integers);
        Assert.Contains(30, integers);
    }

    [Fact]
    public void ExtractDoubles_ReturnsDoubles()
    {
        var doubles = RegexHelper.ExtractDoubles("Values: 1.5, 2.75, 3.0");

        Assert.Equal(3, doubles.Count);
        Assert.Contains(1.5, doubles);
        Assert.Contains(2.75, doubles);
    }

    [Fact]
    public void ExtractEmails_ReturnsEmails()
    {
        var emails = RegexHelper.ExtractEmails("Contact: test@example.com or support@example.org");

        Assert.Equal(2, emails.Count);
        Assert.Contains("test@example.com", emails);
        Assert.Contains("support@example.org", emails);
    }

    [Fact]
    public void ExtractUrls_ReturnsUrls()
    {
        var urls = RegexHelper.ExtractUrls("Visit https://example.com or http://test.org");

        Assert.Equal(2, urls.Count);
    }

    [Fact]
    public void ExtractMobilePhones_ReturnsPhoneNumbers()
    {
        var phones = RegexHelper.ExtractMobilePhones("Call 13812345678 or 15987654321");

        Assert.Equal(2, phones.Count);
    }

    #endregion

    #region 格式化操作测试

    [Fact]
    public void Mask_MasksCorrectly()
    {
        var result = RegexHelper.Mask("13812345678", 3, 4);

        Assert.Contains("*", result);
    }

    [Fact]
    public void MaskEmail_MasksCorrectly()
    {
        var result = RegexHelper.Mask("test@example.com", 2, 4);

        Assert.Contains("*", result);
    }

    [Fact]
    public void MaskIdCard_MasksCorrectly()
    {
        var result = RegexHelper.Mask("110101199001011234", 6, 4);

        Assert.Contains("*", result);
    }

    [Fact]
    public void FormatMobilePhone_FormatsCorrectly()
    {
        var result = RegexHelper.FormatMobilePhone("13812345678");

        Assert.Equal("138 1234 5678", result);
    }

    #endregion

    #region 高级操作测试

    [Fact]
    public void Split_SplitsByPattern()
    {
        var parts = RegexHelper.Split("Hello,World;Test", @"[,;]");

        Assert.Equal(3, parts.Length);
        Assert.Equal("Hello", parts[0]);
        Assert.Equal("World", parts[1]);
        Assert.Equal("Test", parts[2]);
    }

    [Fact]
    public void Matches_ReturnsCorrectCount()
    {
        var matches = RegexHelper.Matches("Hello 123 World 456 Test 789", @"\d+");

        Assert.Equal(3, matches.Count);
    }

    [Fact]
    public void Escape_EscapesSpecialChars()
    {
        var escaped = RegexHelper.Escape("a.b*c+d?e");

        Assert.Equal(@"a\.b\*c\+d\?e", escaped);
    }

    [Fact]
    public void Unescape_UnescapesSpecialChars()
    {
        var unescaped = RegexHelper.Unescape(@"a\.b\*c\+d\?e");

        Assert.Equal("a.b*c+d?e", unescaped);
    }

    #endregion

    #region 异常测试

    [Fact]
    public void IsMatch_NullPattern_ThrowsArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>(() => RegexHelper.IsMatch("test", null!));
    }

    [Fact]
    public void Match_NullPattern_ThrowsArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>(() => RegexHelper.Match("test", null!));
    }

    [Fact]
    public void Replace_NullPattern_ThrowsArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>(() => RegexHelper.Replace("test", null!, "replacement"));
    }

    [Fact]
    public void Replace_NullReplacement_ThrowsArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>(() => RegexHelper.Replace("test", @"\d+", (string)null!));
    }

    #endregion
}
