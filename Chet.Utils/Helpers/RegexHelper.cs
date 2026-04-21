using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

namespace Chet.Utils.Helpers;

/// <summary>
/// 正则表达式帮助类，提供验证、匹配、替换、提取和格式化等多种功能。
/// </summary>
/// <remarks>
/// <para>本类提供以下功能模块：</para>
/// <list type="bullet">
///   <item><description>基础验证：邮箱、手机号、身份证、URL、IP地址等</description></item>
///   <item><description>格式验证：数字、整数、浮点数、中文、字母、日期等</description></item>
///   <item><description>匹配操作：单次匹配、全部匹配、分组匹配等</description></item>
///   <item><description>替换操作：简单替换、条件替换、回调替换等</description></item>
///   <item><description>提取操作：提取数字、提取链接、提取邮箱等</description></item>
///   <item><description>格式化操作：隐藏手机号、格式化手机号、脱敏处理等</description></item>
///   <item><description>高级操作：正则测试、分割字符串、验证规则等</description></item>
/// </list>
/// </remarks>
public static partial class RegexHelper
{
    #region 常用正则表达式模式

    /// <summary>
    /// 电子邮件正则表达式模式。
    /// </summary>
    public const string EmailPattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";

    /// <summary>
    /// 手机号码正则表达式模式（中国大陆）。
    /// </summary>
    public const string MobilePhonePattern = @"^1[3-9]\d{9}$";

    /// <summary>
    /// 固定电话正则表达式模式。
    /// </summary>
    public const string TelephonePattern = @"^(\d{3,4}-)?\d{7,8}(-\d{1,4})?$";

    /// <summary>
    /// 身份证号码正则表达式模式（15位或18位）。
    /// </summary>
    public const string IdCardPattern = @"^[1-9]\d{5}(19|20)\d{2}(0[1-9]|1[0-2])(0[1-9]|[12]\d|3[01])\d{3}[\dXx]$|^[1-9]\d{7}(0[1-9]|1[0-2])(0[1-9]|[12]\d|3[01])\d{3}$";

    /// <summary>
    /// URL 正则表达式模式。
    /// </summary>
    public const string UrlPattern = @"^(https?|ftp)://[^\s/$.?#].[^\s]*$";

    /// <summary>
    /// IPv4 地址正则表达式模式。
    /// </summary>
    public const string IPv4Pattern = @"^((25[0-5]|2[0-4]\d|[01]?\d\d?)\.){3}(25[0-5]|2[0-4]\d|[01]?\d\d?)$";

    /// <summary>
    /// IPv6 地址正则表达式模式。
    /// </summary>
    public const string IPv6Pattern = @"^([\da-fA-F]{1,4}:){7}[\da-fA-F]{1,4}$|^([\da-fA-F]{1,4}:){1,7}:$|^([\da-fA-F]{1,4}:){1,6}:[\da-fA-F]{1,4}$|^([\da-fA-F]{1,4}:){1,5}(:[\da-fA-F]{1,4}){1,2}$|^([\da-fA-F]{1,4}:){1,4}(:[\da-fA-F]{1,4}){1,3}$|^([\da-fA-F]{1,4}:){1,3}(:[\da-fA-F]{1,4}){1,4}$|^([\da-fA-F]{1,4}:){1,2}(:[\da-fA-F]{1,4}){1,5}$|^[\da-fA-F]{1,4}:((:[\da-fA-F]{1,4}){1,6})$|^:((:[\da-fA-F]{1,4}){1,7}|:)$|^::(ffff(:0{1,4}){0,1}:){0,1}((25[0-5]|(2[0-4]|1{0,1}\d){0,1}\d)\.){3}(25[0-5]|(2[0-4]|1{0,1}\d){0,1}\d)$";

    /// <summary>
    /// 整数正则表达式模式。
    /// </summary>
    public const string IntegerPattern = @"^-?\d+$";

    /// <summary>
    /// 正整数正则表达式模式。
    /// </summary>
    public const string PositiveIntegerPattern = @"^[1-9]\d*$";

    /// <summary>
    /// 浮点数正则表达式模式。
    /// </summary>
    public const string FloatPattern = @"^-?\d+(\.\d+)?$";

    /// <summary>
    /// 中文正则表达式模式。
    /// </summary>
    public const string ChinesePattern = @"^[\u4e00-\u9fa5]+$";

    /// <summary>
    /// 字母正则表达式模式。
    /// </summary>
    public const string LetterPattern = @"^[a-zA-Z]+$";

    /// <summary>
    /// 字母数字正则表达式模式。
    /// </summary>
    public const string AlphanumericPattern = @"^[a-zA-Z0-9]+$";

    /// <summary>
    /// 邮政编码正则表达式模式（中国）。
    /// </summary>
    public const string PostalCodePattern = @"^[1-9]\d{5}$";

    /// <summary>
    /// 银行卡号正则表达式模式。
    /// </summary>
    public const string BankCardPattern = @"^\d{16,19}$";

    /// <summary>
    /// 车牌号正则表达式模式（中国）。
    /// </summary>
    public const string LicensePlatePattern = @"^[京津沪渝冀豫云辽黑湘皖鲁新苏浙赣鄂桂甘晋蒙陕吉闽贵粤青藏川宁琼使领][A-Z][A-HJ-NP-Z0-9]{4,5}[A-HJ-NP-Z0-9挂学警港澳]$";

    /// <summary>
    /// 日期正则表达式模式（yyyy-MM-dd）。
    /// </summary>
    public const string DatePattern = @"^\d{4}-(0[1-9]|1[0-2])-(0[1-9]|[12]\d|3[01])$";

    /// <summary>
    /// 时间正则表达式模式（HH:mm:ss）。
    /// </summary>
    public const string TimePattern = @"^([01]\d|2[0-3]):[0-5]\d:[0-5]\d$";

    /// <summary>
    /// 日期时间正则表达式模式。
    /// </summary>
    public const string DateTimePattern = @"^\d{4}-(0[1-9]|1[0-2])-(0[1-9]|[12]\d|3[01])\s([01]\d|2[0-3]):[0-5]\d:[0-5]\d$";

    /// <summary>
    /// 十六进制颜色正则表达式模式。
    /// </summary>
    public const string HexColorPattern = @"^#?([a-fA-F0-9]{6}|[a-fA-F0-9]{3})$";

    /// <summary>
    /// QQ 号正则表达式模式。
    /// </summary>
    public const string QQPattern = @"^[1-9]\d{4,10}$";

    /// <summary>
    /// 微信号正则表达式模式。
    /// </summary>
    public const string WeChatPattern = @"^[a-zA-Z][a-zA-Z0-9_-]{5,19}$";

    /// <summary>
    /// 用户名正则表达式模式（字母开头，允许字母数字下划线）。
    /// </summary>
    public const string UsernamePattern = @"^[a-zA-Z][a-zA-Z0-9_]{4,15}$";

    /// <summary>
    /// 密码正则表达式模式（至少8位，包含大小写字母和数字）。
    /// </summary>
    public const string StrongPasswordPattern = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)[a-zA-Z\d@$!%*?&]{8,}$";

    /// <summary>
    /// HTML 标签正则表达式模式。
    /// </summary>
    public const string HtmlTagPattern = @"<[^>]+>";

    /// <summary>
    /// 空白字符正则表达式模式。
    /// </summary>
    public const string WhitespacePattern = @"\s+";

    /// <summary>
    /// 数字正则表达式模式。
    /// </summary>
    public const string NumberPattern = @"\d+";

    /// <summary>
    /// 非数字正则表达式模式。
    /// </summary>
    public const string NonNumberPattern = @"\D+";

    /// <summary>
    /// 单词字符正则表达式模式。
    /// </summary>
    public const string WordPattern = @"\w+";

    #endregion

    #region 基础验证方法

    /// <summary>
    /// 验证字符串是否匹配指定正则表达式。
    /// </summary>
    /// <param name="input">输入字符串。</param>
    /// <param name="pattern">正则表达式模式。</param>
    /// <param name="options">正则表达式选项。</param>
    /// <returns>是否匹配。</returns>
    /// <exception cref="ArgumentNullException">pattern 为 null 时抛出。</exception>
    /// <example>
    /// <code>
    /// bool isValid = RegexHelper.IsMatch("test@example.com", RegexHelper.EmailPattern);
    /// </code>
    /// </example>
    public static bool IsMatch(string? input, string pattern, RegexOptions options = RegexOptions.None)
    {
        ArgumentException.ThrowIfNullOrEmpty(pattern);
        if (string.IsNullOrEmpty(input)) return false;
        return Regex.IsMatch(input, pattern, options);
    }

    /// <summary>
    /// 验证是否为有效的电子邮件地址。
    /// </summary>
    /// <param name="email">电子邮件地址。</param>
    /// <returns>是否有效。</returns>
    /// <example>
    /// <code>
    /// bool isValid = RegexHelper.IsValidEmail("test@example.com");
    /// Console.WriteLine($"邮箱有效: {isValid}");
    /// </code>
    /// </example>
    public static bool IsValidEmail(string? email)
    {
        return IsMatch(email, EmailPattern, RegexOptions.IgnoreCase);
    }

    /// <summary>
    /// 验证是否为有效的手机号码（中国大陆）。
    /// </summary>
    /// <param name="mobile">手机号码。</param>
    /// <returns>是否有效。</returns>
    /// <example>
    /// <code>
    /// bool isValid = RegexHelper.IsValidMobilePhone("13812345678");
    /// Console.WriteLine($"手机号有效: {isValid}");
    /// </code>
    /// </example>
    public static bool IsValidMobilePhone(string? mobile)
    {
        return IsMatch(mobile, MobilePhonePattern);
    }

    /// <summary>
    /// 验证是否为有效的固定电话号码。
    /// </summary>
    /// <param name="telephone">固定电话号码。</param>
    /// <returns>是否有效。</returns>
    public static bool IsValidTelephone(string? telephone)
    {
        return IsMatch(telephone, TelephonePattern);
    }

    /// <summary>
    /// 验证是否为有效的身份证号码（15位或18位）。
    /// </summary>
    /// <param name="idCard">身份证号码。</param>
    /// <returns>是否有效。</returns>
    /// <example>
    /// <code>
    /// bool isValid = RegexHelper.IsValidIdCard("110101199001011234");
    /// Console.WriteLine($"身份证有效: {isValid}");
    /// </code>
    /// </example>
    public static bool IsValidIdCard(string? idCard)
    {
        if (!IsMatch(idCard, IdCardPattern)) return false;

        if (idCard!.Length == 18)
        {
            return ValidateIdCardChecksum(idCard);
        }

        return true;
    }

    /// <summary>
    /// 验证是否为有效的 URL。
    /// </summary>
    /// <param name="url">URL 字符串。</param>
    /// <returns>是否有效。</returns>
    /// <example>
    /// <code>
    /// bool isValid = RegexHelper.IsValidUrl("https://www.example.com");
    /// Console.WriteLine($"URL有效: {isValid}");
    /// </code>
    /// </example>
    public static bool IsValidUrl(string? url)
    {
        return IsMatch(url, UrlPattern, RegexOptions.IgnoreCase);
    }

    /// <summary>
    /// 验证是否为有效的 IPv4 地址。
    /// </summary>
    /// <param name="ip">IP 地址。</param>
    /// <returns>是否有效。</returns>
    /// <example>
    /// <code>
    /// bool isValid = RegexHelper.IsValidIPv4("192.168.1.1");
    /// Console.WriteLine($"IPv4有效: {isValid}");
    /// </code>
    /// </example>
    public static bool IsValidIPv4(string? ip)
    {
        return IsMatch(ip, IPv4Pattern);
    }

    /// <summary>
    /// 验证是否为有效的 IPv6 地址。
    /// </summary>
    /// <param name="ip">IP 地址。</param>
    /// <returns>是否有效。</returns>
    public static bool IsValidIPv6(string? ip)
    {
        return IsMatch(ip, IPv6Pattern, RegexOptions.IgnoreCase);
    }

    /// <summary>
    /// 验证是否为有效的 IP 地址（IPv4 或 IPv6）。
    /// </summary>
    /// <param name="ip">IP 地址。</param>
    /// <returns>是否有效。</returns>
    public static bool IsValidIP(string? ip)
    {
        return IsValidIPv4(ip) || IsValidIPv6(ip);
    }

    /// <summary>
    /// 验证是否为有效的邮政编码（中国）。
    /// </summary>
    /// <param name="postalCode">邮政编码。</param>
    /// <returns>是否有效。</returns>
    public static bool IsValidPostalCode(string? postalCode)
    {
        return IsMatch(postalCode, PostalCodePattern);
    }

    /// <summary>
    /// 验证是否为有效的银行卡号。
    /// </summary>
    /// <param name="bankCard">银行卡号。</param>
    /// <returns>是否有效。</returns>
    public static bool IsValidBankCard(string? bankCard)
    {
        return IsMatch(bankCard, BankCardPattern);
    }

    /// <summary>
    /// 验证是否为有效的车牌号（中国）。
    /// </summary>
    /// <param name="licensePlate">车牌号。</param>
    /// <returns>是否有效。</returns>
    public static bool IsValidLicensePlate(string? licensePlate)
    {
        return IsMatch(licensePlate, LicensePlatePattern);
    }

    /// <summary>
    /// 验证是否为有效的 QQ 号。
    /// </summary>
    /// <param name="qq">QQ 号。</param>
    /// <returns>是否有效。</returns>
    public static bool IsValidQQ(string? qq)
    {
        return IsMatch(qq, QQPattern);
    }

    /// <summary>
    /// 验证是否为有效的微信号。
    /// </summary>
    /// <param name="wechat">微信号。</param>
    /// <returns>是否有效。</returns>
    public static bool IsValidWeChat(string? wechat)
    {
        return IsMatch(wechat, WeChatPattern);
    }

    /// <summary>
    /// 验证是否为有效的十六进制颜色值。
    /// </summary>
    /// <param name="color">颜色值。</param>
    /// <returns>是否有效。</returns>
    public static bool IsValidHexColor(string? color)
    {
        return IsMatch(color, HexColorPattern, RegexOptions.IgnoreCase);
    }

    #endregion

    #region 格式验证方法

    /// <summary>
    /// 验证是否为整数。
    /// </summary>
    /// <param name="input">输入字符串。</param>
    /// <returns>是否为整数。</returns>
    /// <example>
    /// <code>
    /// bool isInt = RegexHelper.IsInteger("123");
    /// Console.WriteLine($"是整数: {isInt}");
    /// </code>
    /// </example>
    public static bool IsInteger(string? input)
    {
        return IsMatch(input, IntegerPattern);
    }

    /// <summary>
    /// 验证是否为正整数。
    /// </summary>
    /// <param name="input">输入字符串。</param>
    /// <returns>是否为正整数。</returns>
    public static bool IsPositiveInteger(string? input)
    {
        return IsMatch(input, PositiveIntegerPattern);
    }

    /// <summary>
    /// 验证是否为浮点数。
    /// </summary>
    /// <param name="input">输入字符串。</param>
    /// <returns>是否为浮点数。</returns>
    /// <example>
    /// <code>
    /// bool isFloat = RegexHelper.IsFloat("123.45");
    /// Console.WriteLine($"是浮点数: {isFloat}");
    /// </code>
    /// </example>
    public static bool IsFloat(string? input)
    {
        return IsMatch(input, FloatPattern);
    }

    /// <summary>
    /// 验证是否为数字。
    /// </summary>
    /// <param name="input">输入字符串。</param>
    /// <returns>是否为数字。</returns>
    public static bool IsNumber(string? input)
    {
        return IsMatch(input, NumberPattern);
    }

    /// <summary>
    /// 验证是否全部为中文字符。
    /// </summary>
    /// <param name="input">输入字符串。</param>
    /// <returns>是否全部为中文。</returns>
    /// <example>
    /// <code>
    /// bool isChinese = RegexHelper.IsChinese("你好世界");
    /// Console.WriteLine($"是中文: {isChinese}");
    /// </code>
    /// </example>
    public static bool IsChinese(string? input)
    {
        return IsMatch(input, ChinesePattern);
    }

    /// <summary>
    /// 验证是否全部为字母。
    /// </summary>
    /// <param name="input">输入字符串。</param>
    /// <returns>是否全部为字母。</returns>
    public static bool IsLetter(string? input)
    {
        return IsMatch(input, LetterPattern);
    }

    /// <summary>
    /// 验证是否全部为字母或数字。
    /// </summary>
    /// <param name="input">输入字符串。</param>
    /// <returns>是否全部为字母或数字。</returns>
    public static bool IsAlphanumeric(string? input)
    {
        return IsMatch(input, AlphanumericPattern);
    }

    /// <summary>
    /// 验证是否为有效的日期格式（yyyy-MM-dd）。
    /// </summary>
    /// <param name="input">输入字符串。</param>
    /// <returns>是否为有效日期格式。</returns>
    public static bool IsValidDate(string? input)
    {
        if (!IsMatch(input, DatePattern)) return false;

        try
        {
            var date = DateTime.ParseExact(input!, "yyyy-MM-dd", CultureInfo.InvariantCulture);
            return true;
        }
        catch
        {
            return false;
        }
    }

    /// <summary>
    /// 验证是否为有效的时间格式（HH:mm:ss）。
    /// </summary>
    /// <param name="input">输入字符串。</param>
    /// <returns>是否为有效时间格式。</returns>
    public static bool IsValidTime(string? input)
    {
        return IsMatch(input, TimePattern);
    }

    /// <summary>
    /// 验证是否为有效的日期时间格式。
    /// </summary>
    /// <param name="input">输入字符串。</param>
    /// <returns>是否为有效日期时间格式。</returns>
    public static bool IsValidDateTime(string? input)
    {
        if (!IsMatch(input, DateTimePattern)) return false;

        try
        {
            var date = DateTime.ParseExact(input!, "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);
            return true;
        }
        catch
        {
            return false;
        }
    }

    /// <summary>
    /// 验证是否为有效的用户名（字母开头，允许字母数字下划线，5-16位）。
    /// </summary>
    /// <param name="username">用户名。</param>
    /// <returns>是否有效。</returns>
    public static bool IsValidUsername(string? username)
    {
        return IsMatch(username, UsernamePattern);
    }

    /// <summary>
    /// 验证是否为强密码（至少8位，包含大小写字母和数字）。
    /// </summary>
    /// <param name="password">密码。</param>
    /// <returns>是否为强密码。</returns>
    public static bool IsStrongPassword(string? password)
    {
        return IsMatch(password, StrongPasswordPattern);
    }

    /// <summary>
    /// 验证密码强度。
    /// </summary>
    /// <param name="password">密码。</param>
    /// <returns>密码强度等级（0-4）。</returns>
    /// <example>
    /// <code>
    /// int strength = RegexHelper.GetPasswordStrength("Abc123!@#");
    /// Console.WriteLine($"密码强度: {strength}");
    /// </code>
    /// </example>
    public static int GetPasswordStrength(string? password)
    {
        if (string.IsNullOrEmpty(password)) return 0;

        int strength = 0;

        if (password.Length >= 8) strength++;
        if (password.Length >= 12) strength++;
        if (Regex.IsMatch(password, @"[a-z]")) strength++;
        if (Regex.IsMatch(password, @"[A-Z]")) strength++;
        if (Regex.IsMatch(password, @"\d")) strength++;
        if (Regex.IsMatch(password, @"[!@#$%^&*(),.?""':{}|<>]")) strength++;

        return Math.Min(strength, 4);
    }

    /// <summary>
    /// 验证字符串长度是否在指定范围内。
    /// </summary>
    /// <param name="input">输入字符串。</param>
    /// <param name="minLength">最小长度。</param>
    /// <param name="maxLength">最大长度。</param>
    /// <returns>是否在范围内。</returns>
    public static bool IsLengthInRange(string? input, int minLength, int maxLength)
    {
        if (input == null) return minLength <= 0;
        return input.Length >= minLength && input.Length <= maxLength;
    }

    /// <summary>
    /// 验证是否只包含空白字符。
    /// </summary>
    /// <param name="input">输入字符串。</param>
    /// <returns>是否只包含空白字符。</returns>
    public static bool IsWhitespace(string? input)
    {
        if (string.IsNullOrEmpty(input)) return true;
        return string.IsNullOrWhiteSpace(input);
    }

    /// <summary>
    /// 验证是否包含中文。
    /// </summary>
    /// <param name="input">输入字符串。</param>
    /// <returns>是否包含中文。</returns>
    public static bool ContainsChinese(string? input)
    {
        if (string.IsNullOrEmpty(input)) return false;
        return Regex.IsMatch(input, @"[\u4e00-\u9fa5]");
    }

    /// <summary>
    /// 验证是否包含数字。
    /// </summary>
    /// <param name="input">输入字符串。</param>
    /// <returns>是否包含数字。</returns>
    public static bool ContainsDigit(string? input)
    {
        if (string.IsNullOrEmpty(input)) return false;
        return Regex.IsMatch(input, @"\d");
    }

    /// <summary>
    /// 验证是否包含字母。
    /// </summary>
    /// <param name="input">输入字符串。</param>
    /// <returns>是否包含字母。</returns>
    public static bool ContainsLetter(string? input)
    {
        if (string.IsNullOrEmpty(input)) return false;
        return Regex.IsMatch(input, @"[a-zA-Z]");
    }

    /// <summary>
    /// 验证是否包含特殊字符。
    /// </summary>
    /// <param name="input">输入字符串。</param>
    /// <returns>是否包含特殊字符。</returns>
    public static bool ContainsSpecialChar(string? input)
    {
        if (string.IsNullOrEmpty(input)) return false;
        return Regex.IsMatch(input, @"[!@#$%^&*(),.?""':{}|<>]");
    }

    #endregion

    #region 匹配操作

    /// <summary>
    /// 获取第一个匹配结果。
    /// </summary>
    /// <param name="input">输入字符串。</param>
    /// <param name="pattern">正则表达式模式。</param>
    /// <param name="options">正则表达式选项。</param>
    /// <returns>匹配结果，如果没有匹配则返回 null。</returns>
    /// <exception cref="ArgumentNullException">pattern 为 null 时抛出。</exception>
    /// <example>
    /// <code>
    /// var match = RegexHelper.Match("Hello 123 World", @"\d+");
    /// if (match != null)
    /// {
    ///     Console.WriteLine($"匹配到: {match.Value}");
    /// }
    /// </code>
    /// </example>
    public static Match? Match(string? input, string pattern, RegexOptions options = RegexOptions.None)
    {
        ArgumentException.ThrowIfNullOrEmpty(pattern);
        if (string.IsNullOrEmpty(input)) return null;

        var match = Regex.Match(input, pattern, options);
        return match.Success ? match : null;
    }

    /// <summary>
    /// 获取所有匹配结果。
    /// </summary>
    /// <param name="input">输入字符串。</param>
    /// <param name="pattern">正则表达式模式。</param>
    /// <param name="options">正则表达式选项。</param>
    /// <returns>匹配结果集合。</returns>
    /// <exception cref="ArgumentNullException">pattern 为 null 时抛出。</exception>
    /// <example>
    /// <code>
    /// var matches = RegexHelper.Matches("Hello 123 World 456", @"\d+");
    /// foreach (Match match in matches)
    /// {
    ///     Console.WriteLine($"匹配到: {match.Value}");
    /// }
    /// </code>
    /// </example>
    public static MatchCollection Matches(string? input, string pattern, RegexOptions options = RegexOptions.None)
    {
        ArgumentException.ThrowIfNullOrEmpty(pattern);
        if (string.IsNullOrEmpty(input)) return Regex.Matches(string.Empty, pattern, options);

        return Regex.Matches(input, pattern, options);
    }

    /// <summary>
    /// 获取所有匹配的字符串值。
    /// </summary>
    /// <param name="input">输入字符串。</param>
    /// <param name="pattern">正则表达式模式。</param>
    /// <param name="options">正则表达式选项。</param>
    /// <returns>匹配的字符串列表。</returns>
    /// <exception cref="ArgumentNullException">pattern 为 null 时抛出。</exception>
    /// <example>
    /// <code>
    /// var values = RegexHelper.GetMatchValues("Hello 123 World 456", @"\d+");
    /// // values = ["123", "456"]
    /// </code>
    /// </example>
    public static List<string> GetMatchValues(string? input, string pattern, RegexOptions options = RegexOptions.None)
    {
        var matches = Matches(input, pattern, options);
        var result = new List<string>();

        foreach (Match match in matches)
        {
            result.Add(match.Value);
        }

        return result;
    }

    /// <summary>
    /// 获取匹配的分组值。
    /// </summary>
    /// <param name="input">输入字符串。</param>
    /// <param name="pattern">正则表达式模式。</param>
    /// <param name="groupName">分组名称。</param>
    /// <param name="options">正则表达式选项。</param>
    /// <returns>分组值，如果没有匹配则返回 null。</returns>
    /// <exception cref="ArgumentNullException">pattern 或 groupName 为 null 时抛出。</exception>
    /// <example>
    /// <code>
    /// var phone = "13812345678";
    /// var pattern = @"(?&lt;prefix&gt;1[3-9])(?&lt;number&gt;\d{8})";
    /// var prefix = RegexHelper.GetGroupValue(phone, pattern, "prefix");
    /// Console.WriteLine($"前缀: {prefix}");
    /// </code>
    /// </example>
    public static string? GetGroupValue(string? input, string pattern, string groupName, RegexOptions options = RegexOptions.None)
    {
        ArgumentException.ThrowIfNullOrEmpty(pattern);
        ArgumentException.ThrowIfNullOrEmpty(groupName);

        var match = Match(input, pattern, options);
        if (match == null) return null;

        var group = match.Groups[groupName];
        return group.Success ? group.Value : null;
    }

    /// <summary>
    /// 获取所有匹配的分组值。
    /// </summary>
    /// <param name="input">输入字符串。</param>
    /// <param name="pattern">正则表达式模式。</param>
    /// <param name="groupName">分组名称。</param>
    /// <param name="options">正则表达式选项。</param>
    /// <returns>分组值列表。</returns>
    /// <exception cref="ArgumentNullException">pattern 或 groupName 为 null 时抛出。</exception>
    public static List<string> GetGroupValues(string? input, string pattern, string groupName, RegexOptions options = RegexOptions.None)
    {
        var matches = Matches(input, pattern, options);
        var result = new List<string>();

        foreach (Match match in matches)
        {
            var group = match.Groups[groupName];
            if (group.Success)
            {
                result.Add(group.Value);
            }
        }

        return result;
    }

    /// <summary>
    /// 获取匹配的所有分组值。
    /// </summary>
    /// <param name="input">输入字符串。</param>
    /// <param name="pattern">正则表达式模式。</param>
    /// <param name="options">正则表达式选项。</param>
    /// <returns>分组值字典（分组名 -> 值）。</returns>
    /// <exception cref="ArgumentNullException">pattern 为 null 时抛出。</exception>
    public static Dictionary<string, string> GetAllGroupValues(string? input, string pattern, RegexOptions options = RegexOptions.None)
    {
        var result = new Dictionary<string, string>();
        var match = Match(input, pattern, options);

        if (match != null)
        {
            var groups = match.Groups;
            foreach (Group group in groups)
            {
                if (group.Success && !int.TryParse(group.Name, out _))
                {
                    result[group.Name] = group.Value;
                }
            }
        }

        return result;
    }

    #endregion

    #region 替换操作

    /// <summary>
    /// 替换所有匹配的字符串。
    /// </summary>
    /// <param name="input">输入字符串。</param>
    /// <param name="pattern">正则表达式模式。</param>
    /// <param name="replacement">替换字符串。</param>
    /// <param name="options">正则表达式选项。</param>
    /// <returns>替换后的字符串。</returns>
    /// <exception cref="ArgumentNullException">pattern 或 replacement 为 null 时抛出。</exception>
    /// <example>
    /// <code>
    /// var result = RegexHelper.Replace("Hello 123 World", @"\d+", "XXX");
    /// // result = "Hello XXX World"
    /// </code>
    /// </example>
    public static string Replace(string? input, string pattern, string replacement, RegexOptions options = RegexOptions.None)
    {
        ArgumentException.ThrowIfNullOrEmpty(pattern);
        ArgumentNullException.ThrowIfNull(replacement);

        if (string.IsNullOrEmpty(input)) return input ?? string.Empty;

        return Regex.Replace(input, pattern, replacement, options);
    }

    /// <summary>
    /// 使用回调函数替换匹配的字符串。
    /// </summary>
    /// <param name="input">输入字符串。</param>
    /// <param name="pattern">正则表达式模式。</param>
    /// <param name="evaluator">替换回调函数。</param>
    /// <param name="options">正则表达式选项。</param>
    /// <returns>替换后的字符串。</returns>
    /// <exception cref="ArgumentNullException">pattern 或 evaluator 为 null 时抛出。</exception>
    /// <example>
    /// <code>
    /// var result = RegexHelper.Replace("Hello 123 World 456", @"\d+", match =>
    /// {
    ///     return $"[{match.Value}]";
    /// });
    /// // result = "Hello [123] World [456]"
    /// </code>
    /// </example>
    public static string Replace(string? input, string pattern, MatchEvaluator evaluator, RegexOptions options = RegexOptions.None)
    {
        ArgumentException.ThrowIfNullOrEmpty(pattern);
        ArgumentNullException.ThrowIfNull(evaluator);

        if (string.IsNullOrEmpty(input)) return input ?? string.Empty;

        return Regex.Replace(input, pattern, evaluator, options);
    }

    /// <summary>
    /// 移除所有匹配的字符串。
    /// </summary>
    /// <param name="input">输入字符串。</param>
    /// <param name="pattern">正则表达式模式。</param>
    /// <param name="options">正则表达式选项。</param>
    /// <returns>移除后的字符串。</returns>
    /// <exception cref="ArgumentNullException">pattern 为 null 时抛出。</exception>
    /// <example>
    /// <code>
    /// var result = RegexHelper.Remove("Hello 123 World 456", @"\d+");
    /// // result = "Hello  World "
    /// </code>
    /// </example>
    public static string Remove(string? input, string pattern, RegexOptions options = RegexOptions.None)
    {
        return Replace(input, pattern, string.Empty, options);
    }

    /// <summary>
    /// 移除所有 HTML 标签。
    /// </summary>
    /// <param name="input">输入字符串。</param>
    /// <returns>移除后的字符串。</returns>
    /// <example>
    /// <code>
    /// var result = RegexHelper.RemoveHtmlTags("&lt;p&gt;Hello&lt;/p&gt;");
    /// // result = "Hello"
    /// </code>
    /// </example>
    public static string RemoveHtmlTags(string? input)
    {
        return Remove(input, HtmlTagPattern);
    }

    /// <summary>
    /// 移除所有空白字符。
    /// </summary>
    /// <param name="input">输入字符串。</param>
    /// <returns>移除后的字符串。</returns>
    public static string RemoveWhitespace(string? input)
    {
        return Remove(input, WhitespacePattern);
    }

    /// <summary>
    /// 压缩空白字符（将连续空白替换为单个空格）。
    /// </summary>
    /// <param name="input">输入字符串。</param>
    /// <returns>压缩后的字符串。</returns>
    /// <example>
    /// <code>
    /// var result = RegexHelper.CompressWhitespace("Hello    World");
    /// // result = "Hello World"
    /// </code>
    /// </example>
    public static string CompressWhitespace(string? input)
    {
        if (string.IsNullOrEmpty(input)) return input ?? string.Empty;
        return Replace(input, WhitespacePattern, " ").Trim();
    }

    /// <summary>
    /// 条件替换：仅当匹配时替换。
    /// </summary>
    /// <param name="input">输入字符串。</param>
    /// <param name="pattern">正则表达式模式。</param>
    /// <param name="replacement">替换字符串。</param>
    /// <param name="options">正则表达式选项。</param>
    /// <returns>替换后的字符串，如果没有匹配则返回原字符串。</returns>
    /// <exception cref="ArgumentNullException">pattern 或 replacement 为 null 时抛出。</exception>
    public static string ReplaceIfMatch(string? input, string pattern, string replacement, RegexOptions options = RegexOptions.None)
    {
        if (!IsMatch(input, pattern, options)) return input ?? string.Empty;
        return Replace(input, pattern, replacement, options);
    }

    #endregion

    #region 提取操作

    /// <summary>
    /// 提取所有数字。
    /// </summary>
    /// <param name="input">输入字符串。</param>
    /// <returns>数字列表。</returns>
    /// <example>
    /// <code>
    /// var numbers = RegexHelper.ExtractNumbers("Price: $123.45, Tax: $67.89");
    /// // numbers = ["123", "45", "67", "89"]
    /// </code>
    /// </example>
    public static List<string> ExtractNumbers(string? input)
    {
        return GetMatchValues(input, NumberPattern);
    }

    /// <summary>
    /// 提取所有整数。
    /// </summary>
    /// <param name="input">输入字符串。</param>
    /// <returns>整数列表。</returns>
    public static List<int> ExtractIntegers(string? input)
    {
        var matches = Matches(input, @"-?\d+");
        var result = new List<int>();

        foreach (Match match in matches)
        {
            if (int.TryParse(match.Value, out var number))
            {
                result.Add(number);
            }
        }

        return result;
    }

    /// <summary>
    /// 提取所有浮点数。
    /// </summary>
    /// <param name="input">输入字符串。</param>
    /// <returns>浮点数列表。</returns>
    public static List<double> ExtractDoubles(string? input)
    {
        var matches = Matches(input, @"-?\d+\.?\d*");
        var result = new List<double>();

        foreach (Match match in matches)
        {
            if (double.TryParse(match.Value, out var number))
            {
                result.Add(number);
            }
        }

        return result;
    }

    /// <summary>
    /// 提取所有邮箱地址。
    /// </summary>
    /// <param name="input">输入字符串。</param>
    /// <returns>邮箱地址列表。</returns>
    /// <example>
    /// <code>
    /// var emails = RegexHelper.ExtractEmails("Contact: test@example.com or support@example.org");
    /// // emails = ["test@example.com", "support@example.org"]
    /// </code>
    /// </example>
    public static List<string> ExtractEmails(string? input)
    {
        return GetMatchValues(input, @"[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}", RegexOptions.IgnoreCase);
    }

    /// <summary>
    /// 提取所有 URL。
    /// </summary>
    /// <param name="input">输入字符串。</param>
    /// <returns>URL 列表。</returns>
    public static List<string> ExtractUrls(string? input)
    {
        return GetMatchValues(input, @"https?://[^\s/$.?#].[^\s]*", RegexOptions.IgnoreCase);
    }

    /// <summary>
    /// 提取所有手机号码（中国大陆）。
    /// </summary>
    /// <param name="input">输入字符串。</param>
    /// <returns>手机号码列表。</returns>
    public static List<string> ExtractMobilePhones(string? input)
    {
        return GetMatchValues(input, @"1[3-9]\d{9}");
    }

    /// <summary>
    /// 提取所有 IP 地址（IPv4）。
    /// </summary>
    /// <param name="input">输入字符串。</param>
    /// <returns>IP 地址列表。</returns>
    public static List<string> ExtractIPv4Addresses(string? input)
    {
        return GetMatchValues(input, @"((25[0-5]|2[0-4]\d|[01]?\d\d?)\.){3}(25[0-5]|2[0-4]\d|[01]?\d\d?)");
    }

    /// <summary>
    /// 提取所有日期（yyyy-MM-dd 格式）。
    /// </summary>
    /// <param name="input">输入字符串。</param>
    /// <returns>日期字符串列表。</returns>
    public static List<string> ExtractDates(string? input)
    {
        return GetMatchValues(input, @"\d{4}-(0[1-9]|1[0-2])-(0[1-9]|[12]\d|3[01])");
    }

    /// <summary>
    /// 提取所有中文字符。
    /// </summary>
    /// <param name="input">输入字符串。</param>
    /// <returns>中文字符列表。</returns>
    public static List<string> ExtractChinese(string? input)
    {
        return GetMatchValues(input, @"[\u4e00-\u9fa5]+");
    }

    /// <summary>
    /// 提取所有单词。
    /// </summary>
    /// <param name="input">输入字符串。</param>
    /// <returns>单词列表。</returns>
    public static List<string> ExtractWords(string? input)
    {
        return GetMatchValues(input, WordPattern);
    }

    /// <summary>
    /// 提取第一个匹配的值。
    /// </summary>
    /// <param name="input">输入字符串。</param>
    /// <param name="pattern">正则表达式模式。</param>
    /// <param name="options">正则表达式选项。</param>
    /// <returns>第一个匹配的值，如果没有匹配则返回 null。</returns>
    /// <exception cref="ArgumentNullException">pattern 为 null 时抛出。</exception>
    public static string? ExtractFirst(string? input, string pattern, RegexOptions options = RegexOptions.None)
    {
        var match = Match(input, pattern, options);
        return match?.Value;
    }

    #endregion

    #region 格式化操作

    /// <summary>
    /// 隐藏手机号码中间4位。
    /// </summary>
    /// <param name="phone">手机号码。</param>
    /// <returns>隐藏后的手机号码。</returns>
    /// <example>
    /// <code>
    /// var hidden = RegexHelper.HideMobilePhone("13812345678");
    /// // hidden = "138****5678"
    /// </code>
    /// </example>
    public static string? HideMobilePhone(string? phone)
    {
        if (string.IsNullOrEmpty(phone)) return phone;
        return Replace(phone, @"(\d{3})\d{4}(\d{4})", "$1****$2");
    }

    /// <summary>
    /// 隐藏邮箱部分字符。
    /// </summary>
    /// <param name="email">邮箱地址。</param>
    /// <returns>隐藏后的邮箱地址。</returns>
    /// <example>
    /// <code>
    /// var hidden = RegexHelper.HideEmail("test@example.com");
    /// // hidden = "t**t@example.com"
    /// </code>
    /// </example>
    public static string? HideEmail(string? email)
    {
        if (string.IsNullOrEmpty(email)) return email;
        return Replace(email, @"(.{1,2})(.*)@", "$1***@");
    }

    /// <summary>
    /// 隐藏身份证号码中间部分。
    /// </summary>
    /// <param name="idCard">身份证号码。</param>
    /// <returns>隐藏后的身份证号码。</returns>
    public static string? HideIdCard(string? idCard)
    {
        if (string.IsNullOrEmpty(idCard)) return idCard;

        if (idCard.Length == 18)
        {
            return Replace(idCard, @"(.{6})(.*)(.{4})", "$1********$3");
        }

        if (idCard.Length == 15)
        {
            return Replace(idCard, @"(.{6})(.*)(.{3})", "$1*******$3");
        }

        return idCard;
    }

    /// <summary>
    /// 隐藏银行卡号中间部分。
    /// </summary>
    /// <param name="bankCard">银行卡号。</param>
    /// <returns>隐藏后的银行卡号。</returns>
    public static string? HideBankCard(string? bankCard)
    {
        if (string.IsNullOrEmpty(bankCard)) return bankCard;
        return Replace(bankCard, @"(.{4})(.*)(.{4})", "$1****$3");
    }

    /// <summary>
    /// 格式化手机号码（添加空格）。
    /// </summary>
    /// <param name="phone">手机号码。</param>
    /// <returns>格式化后的手机号码。</returns>
    /// <example>
    /// <code>
    /// var formatted = RegexHelper.FormatMobilePhone("13812345678");
    /// // formatted = "138 1234 5678"
    /// </code>
    /// </example>
    public static string? FormatMobilePhone(string? phone)
    {
        if (string.IsNullOrEmpty(phone)) return phone;
        return Replace(phone, @"(\d{3})(\d{4})(\d{4})", "$1 $2 $3");
    }

    /// <summary>
    /// 格式化银行卡号（每4位添加空格）。
    /// </summary>
    /// <param name="bankCard">银行卡号。</param>
    /// <returns>格式化后的银行卡号。</returns>
    public static string? FormatBankCard(string? bankCard)
    {
        if (string.IsNullOrEmpty(bankCard)) return bankCard;
        return Replace(bankCard, @"(\d{4})", "$1 ").Trim();
    }

    /// <summary>
    /// 脱敏处理（保留首尾字符，中间用星号替换）。
    /// </summary>
    /// <param name="input">输入字符串。</param>
    /// <param name="keepStart">保留开头字符数。</param>
    /// <param name="keepEnd">保留结尾字符数。</param>
    /// <returns>脱敏后的字符串。</returns>
    /// <example>
    /// <code>
    /// var masked = RegexHelper.Mask("HelloWorld", 2, 2);
    /// // masked = "He******ld"
    /// </code>
    /// </example>
    public static string? Mask(string? input, int keepStart, int keepEnd)
    {
        if (string.IsNullOrEmpty(input)) return input;

        if (input.Length <= keepStart + keepEnd)
        {
            return new string('*', input.Length);
        }

        var start = input.Substring(0, keepStart);
        var end = input.Substring(input.Length - keepEnd);
        var middle = new string('*', input.Length - keepStart - keepEnd);

        return start + middle + end;
    }

    /// <summary>
    /// 将字符串转换为驼峰命名。
    /// </summary>
    /// <param name="input">输入字符串（下划线或连字符分隔）。</param>
    /// <returns>驼峰命名字符串。</returns>
    /// <example>
    /// <code>
    /// var camel = RegexHelper.ToCamelCase("hello_world");
    /// // camel = "helloWorld"
    /// </code>
    /// </example>
    public static string ToCamelCase(string? input)
    {
        if (string.IsNullOrEmpty(input)) return input ?? string.Empty;

        var words = Regex.Split(input, @"[_\-\s]+");
        var result = new StringBuilder();

        for (int i = 0; i < words.Length; i++)
        {
            var word = words[i];
            if (string.IsNullOrEmpty(word)) continue;

            if (i == 0)
            {
                result.Append(char.ToLowerInvariant(word[0]));
            }
            else
            {
                result.Append(char.ToUpperInvariant(word[0]));
            }

            if (word.Length > 1)
            {
                result.Append(word.Substring(1).ToLowerInvariant());
            }
        }

        return result.ToString();
    }

    /// <summary>
    /// 将字符串转换为帕斯卡命名。
    /// </summary>
    /// <param name="input">输入字符串（下划线或连字符分隔）。</param>
    /// <returns>帕斯卡命名字符串。</returns>
    /// <example>
    /// <code>
    /// var pascal = RegexHelper.ToPascalCase("hello_world");
    /// // pascal = "HelloWorld"
    /// </code>
    /// </example>
    public static string ToPascalCase(string? input)
    {
        if (string.IsNullOrEmpty(input)) return input ?? string.Empty;

        var words = Regex.Split(input, @"[_\-\s]+");
        var result = new StringBuilder();

        foreach (var word in words)
        {
            if (string.IsNullOrEmpty(word)) continue;

            result.Append(char.ToUpperInvariant(word[0]));
            if (word.Length > 1)
            {
                result.Append(word.Substring(1).ToLowerInvariant());
            }
        }

        return result.ToString();
    }

    /// <summary>
    /// 将驼峰命名转换为下划线命名。
    /// </summary>
    /// <param name="input">驼峰命名字符串。</param>
    /// <returns>下划线命名字符串。</returns>
    /// <example>
    /// <code>
    /// var snake = RegexHelper.ToSnakeCase("helloWorld");
    /// // snake = "hello_world"
    /// </code>
    /// </example>
    public static string ToSnakeCase(string? input)
    {
        if (string.IsNullOrEmpty(input)) return input ?? string.Empty;

        var result = Replace(input, @"([a-z])([A-Z])", "$1_$2");
        return result.ToLowerInvariant();
    }

    /// <summary>
    /// 将驼峰命名转换为连字符命名。
    /// </summary>
    /// <param name="input">驼峰命名字符串。</param>
    /// <returns>连字符命名字符串。</returns>
    public static string ToKebabCase(string? input)
    {
        if (string.IsNullOrEmpty(input)) return input ?? string.Empty;

        var result = Replace(input, @"([a-z])([A-Z])", "$1-$2");
        return result.ToLowerInvariant();
    }

    #endregion

    #region 高级操作

    /// <summary>
    /// 分割字符串。
    /// </summary>
    /// <param name="input">输入字符串。</param>
    /// <param name="pattern">分隔符正则表达式模式。</param>
    /// <param name="options">正则表达式选项。</param>
    /// <returns>分割后的字符串数组。</returns>
    /// <exception cref="ArgumentNullException">pattern 为 null 时抛出。</exception>
    /// <example>
    /// <code>
    /// var parts = RegexHelper.Split("Hello,World;Test", @"[,;]");
    /// // parts = ["Hello", "World", "Test"]
    /// </code>
    /// </example>
    public static string[] Split(string? input, string pattern, RegexOptions options = RegexOptions.None)
    {
        ArgumentException.ThrowIfNullOrEmpty(pattern);

        if (string.IsNullOrEmpty(input)) return Array.Empty<string>();

        return Regex.Split(input, pattern, options);
    }

    /// <summary>
    /// 测试正则表达式并返回详细信息。
    /// </summary>
    /// <param name="input">输入字符串。</param>
    /// <param name="pattern">正则表达式模式。</param>
    /// <param name="options">正则表达式选项。</param>
    /// <returns>测试结果。</returns>
    /// <exception cref="ArgumentNullException">pattern 为 null 时抛出。</exception>
    /// <example>
    /// <code>
    /// var result = RegexHelper.Test("Hello 123 World", @"\d+");
    /// Console.WriteLine($"匹配成功: {result.IsMatch}");
    /// Console.WriteLine($"匹配次数: {result.MatchCount}");
    /// </code>
    /// </example>
    public static RegexTestResult Test(string? input, string pattern, RegexOptions options = RegexOptions.None)
    {
        ArgumentException.ThrowIfNullOrEmpty(pattern);

        var result = new RegexTestResult
        {
            Pattern = pattern,
            Input = input ?? string.Empty,
            IsMatch = false,
            MatchCount = 0,
            Matches = new List<RegexMatchInfo>()
        };

        if (string.IsNullOrEmpty(input))
        {
            return result;
        }

        try
        {
            var matches = Regex.Matches(input, pattern, options);
            result.IsMatch = matches.Count > 0;
            result.MatchCount = matches.Count;

            foreach (Match match in matches)
            {
                var matchInfo = new RegexMatchInfo
                {
                    Value = match.Value,
                    Index = match.Index,
                    Length = match.Length,
                    Groups = new Dictionary<string, string>()
                };

                foreach (Group group in match.Groups)
                {
                    if (group.Success && !int.TryParse(group.Name, out _))
                    {
                        matchInfo.Groups[group.Name] = group.Value;
                    }
                }

                result.Matches.Add(matchInfo);
            }
        }
        catch (Exception ex)
        {
            result.Error = ex.Message;
        }

        return result;
    }

    /// <summary>
    /// 验证正则表达式模式是否有效。
    /// </summary>
    /// <param name="pattern">正则表达式模式。</param>
    /// <param name="errorMessage">错误信息。</param>
    /// <returns>是否有效。</returns>
    /// <example>
    /// <code>
    /// if (RegexHelper.ValidatePattern(@"\d+", out var error))
    /// {
    ///     Console.WriteLine("正则表达式有效");
    /// }
    /// else
    /// {
    ///     Console.WriteLine($"正则表达式无效: {error}");
    /// }
    /// </code>
    /// </example>
    public static bool ValidatePattern(string? pattern, out string? errorMessage)
    {
        errorMessage = null;

        if (string.IsNullOrEmpty(pattern))
        {
            errorMessage = "正则表达式模式不能为空";
            return false;
        }

        try
        {
            _ = new Regex(pattern);
            return true;
        }
        catch (Exception ex)
        {
            errorMessage = ex.Message;
            return false;
        }
    }

    /// <summary>
    /// 转义正则表达式特殊字符。
    /// </summary>
    /// <param name="input">输入字符串。</param>
    /// <returns>转义后的字符串。</returns>
    /// <example>
    /// <code>
    /// var escaped = RegexHelper.Escape("Hello.World");
    /// // escaped = "Hello\.World"
    /// </code>
    /// </example>
    public static string Escape(string? input)
    {
        if (string.IsNullOrEmpty(input)) return input ?? string.Empty;
        return Regex.Escape(input);
    }

    /// <summary>
    /// 取消转义正则表达式特殊字符。
    /// </summary>
    /// <param name="input">输入字符串。</param>
    /// <returns>取消转义后的字符串。</returns>
    public static string Unescape(string? input)
    {
        if (string.IsNullOrEmpty(input)) return input ?? string.Empty;
        return Regex.Unescape(input);
    }

    /// <summary>
    /// 统计匹配次数。
    /// </summary>
    /// <param name="input">输入字符串。</param>
    /// <param name="pattern">正则表达式模式。</param>
    /// <param name="options">正则表达式选项。</param>
    /// <returns>匹配次数。</returns>
    /// <exception cref="ArgumentNullException">pattern 为 null 时抛出。</exception>
    public static int Count(string? input, string pattern, RegexOptions options = RegexOptions.None)
    {
        return Matches(input, pattern, options).Count;
    }

    /// <summary>
    /// 查找匹配的位置。
    /// </summary>
    /// <param name="input">输入字符串。</param>
    /// <param name="pattern">正则表达式模式。</param>
    /// <param name="options">正则表达式选项。</param>
    /// <returns>匹配位置列表（索引和长度）。</returns>
    /// <exception cref="ArgumentNullException">pattern 为 null 时抛出。</exception>
    public static List<(int Index, int Length)> FindPositions(string? input, string pattern, RegexOptions options = RegexOptions.None)
    {
        var matches = Matches(input, pattern, options);
        var result = new List<(int Index, int Length)>();

        foreach (Match match in matches)
        {
            result.Add((match.Index, match.Length));
        }

        return result;
    }

    #endregion

    #region 私有方法

    /// <summary>
    /// 验证身份证校验码。
    /// </summary>
    private static bool ValidateIdCardChecksum(string idCard)
    {
        if (idCard.Length != 18) return false;

        var weights = new[] { 7, 9, 10, 5, 8, 4, 2, 1, 6, 3, 7, 9, 10, 5, 8, 4, 2 };
        var checkCodes = new[] { '1', '0', 'X', '9', '8', '7', '6', '5', '4', '3', '2' };

        int sum = 0;
        for (int i = 0; i < 17; i++)
        {
            if (!char.IsDigit(idCard[i])) return false;
            sum += (idCard[i] - '0') * weights[i];
        }

        var checkCode = checkCodes[sum % 11];
        return char.ToUpperInvariant(idCard[17]) == checkCode;
    }

    #endregion
}

#region 辅助类定义

/// <summary>
/// 正则表达式测试结果。
/// </summary>
public class RegexTestResult
{
    /// <summary>
    /// 正则表达式模式。
    /// </summary>
    public string Pattern { get; set; } = string.Empty;

    /// <summary>
    /// 输入字符串。
    /// </summary>
    public string Input { get; set; } = string.Empty;

    /// <summary>
    /// 是否匹配。
    /// </summary>
    public bool IsMatch { get; set; }

    /// <summary>
    /// 匹配次数。
    /// </summary>
    public int MatchCount { get; set; }

    /// <summary>
    /// 匹配信息列表。
    /// </summary>
    public List<RegexMatchInfo> Matches { get; set; } = new();

    /// <summary>
    /// 错误信息。
    /// </summary>
    public string? Error { get; set; }
}

/// <summary>
/// 正则表达式匹配信息。
/// </summary>
public class RegexMatchInfo
{
    /// <summary>
    /// 匹配值。
    /// </summary>
    public string Value { get; set; } = string.Empty;

    /// <summary>
    /// 匹配起始位置。
    /// </summary>
    public int Index { get; set; }

    /// <summary>
    /// 匹配长度。
    /// </summary>
    public int Length { get; set; }

    /// <summary>
    /// 分组信息。
    /// </summary>
    public Dictionary<string, string> Groups { get; set; } = new();
}

#endregion
