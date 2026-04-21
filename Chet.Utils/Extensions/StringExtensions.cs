using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Web;

namespace Chet.Utils;

/// <summary>
/// string 扩展方法类，提供常用的判断、验证、转换、编码、格式化和操作方法。
/// </summary>
/// <remarks>
/// <para>本类提供丰富的字符串扩展方法，涵盖以下功能：</para>
/// <list type="bullet">
///   <item><description>空值判断：IsNullOrEmpty、IsNullOrWhiteSpace、IsNull</description></item>
///   <item><description>类型判断：IsNumeric、IsInt、IsDecimal、IsGuid、IsEmail、IsMobile 等</description></item>
///   <item><description>类型转换：ToInt、ToDecimal、ToDateTime、ToBool 等</description></item>
///   <item><description>编码解码：Base64、URL、HTML、MD5、SHA256 等</description></item>
///   <item><description>字符串操作：截取、反转、掩码、脱敏等</description></item>
///   <item><description>格式化：驼峰转换、下划线转换、帕斯卡转换等</description></item>
/// </list>
/// </remarks>
public static class StringExtensions
{
    #region 空值判断

    /// <summary>
    /// 判断字符串是否为 null 或空字符串。
    /// </summary>
    /// <param name="value">待判断的字符串。</param>
    /// <returns>如果字符串为 null 或空字符串，返回 true；否则返回 false。</returns>
    /// <example>
    /// <code>
    /// string str1 = null;
    /// string str2 = "";
    /// string str3 = "hello";
    /// 
    /// bool result1 = str1.IsNullOrEmpty(); // true
    /// bool result2 = str2.IsNullOrEmpty(); // true
    /// bool result3 = str3.IsNullOrEmpty(); // false
    /// </code>
    /// </example>
    public static bool IsNullOrEmpty(this string value) => string.IsNullOrEmpty(value);

    /// <summary>
    /// 判断字符串是否为 null 或仅包含空白字符。
    /// </summary>
    /// <param name="value">待判断的字符串。</param>
    /// <returns>如果字符串为 null 或仅包含空白字符，返回 true；否则返回 false。</returns>
    public static bool IsNullOrWhiteSpace(this string value) => string.IsNullOrWhiteSpace(value);

    /// <summary>
    /// 判断字符串是否不为空（非 null 且非空字符串）。
    /// </summary>
    /// <param name="value">待判断的字符串。</param>
    /// <returns>如果字符串不为 null 且不为空字符串，返回 true；否则返回 false。</returns>
    public static bool IsNotNullOrEmpty(this string value) => !string.IsNullOrEmpty(value);

    /// <summary>
    /// 判断字符串是否不为空白（非 null 且包含非空白字符）。
    /// </summary>
    /// <param name="value">待判断的字符串。</param>
    /// <returns>如果字符串不为 null 且包含非空白字符，返回 true；否则返回 false。</returns>
    public static bool IsNotNullOrWhiteSpace(this string value) => !string.IsNullOrWhiteSpace(value);

    /// <summary>
    /// 判断字符串是否为空（支持自定义空值判断）。
    /// </summary>
    /// <param name="value">源字符串。</param>
    /// <param name="nullStrings">自定义空字符串，使用 "|" 分隔，默认为 "null|{}|[]"。</param>
    /// <param name="isTrim">是否移除首尾空白字符后再判断，默认为 false。</param>
    /// <returns>如果字符串为空或匹配自定义空字符串，返回 true；否则返回 false。</returns>
    public static bool IsNull(this string value, string nullStrings = "null|{}|[]", bool isTrim = false)
    {
        if (value == null) return true;
        
        var checkValue = isTrim ? value.Trim() : value;
        if (string.IsNullOrEmpty(checkValue)) return true;
        
        if (!string.IsNullOrWhiteSpace(nullStrings))
        {
            var nullList = nullStrings.Split('|');
            var compareValue = isTrim ? value.Trim().ToLower() : value.ToLower();
            return nullList.Contains(compareValue);
        }
        
        return false;
    }

    #endregion

    #region 类型判断

    /// <summary>
    /// 判断字符串是否为数字（可解析为 double）。
    /// </summary>
    /// <param name="value">待判断的字符串。</param>
    /// <returns>如果字符串可以解析为 double，返回 true；否则返回 false。</returns>
    public static bool IsNumeric(this string value) =>
        !string.IsNullOrWhiteSpace(value) && double.TryParse(value, out _);

    /// <summary>
    /// 判断字符串是否为整数。
    /// </summary>
    /// <param name="value">待判断的字符串。</param>
    /// <returns>如果字符串可以解析为 int，返回 true；否则返回 false。</returns>
    public static bool IsInt(this string value) =>
        !string.IsNullOrWhiteSpace(value) && int.TryParse(value, out _);

    /// <summary>
    /// 判断字符串是否为长整数。
    /// </summary>
    /// <param name="value">待判断的字符串。</param>
    /// <returns>如果字符串可以解析为 long，返回 true；否则返回 false。</returns>
    public static bool IsLong(this string value) =>
        !string.IsNullOrWhiteSpace(value) && long.TryParse(value, out _);

    /// <summary>
    /// 判断字符串是否为浮点数（float）。
    /// </summary>
    /// <param name="value">待判断的字符串。</param>
    /// <returns>如果字符串可以解析为 float，返回 true；否则返回 false。</returns>
    public static bool IsFloat(this string value) =>
        !string.IsNullOrWhiteSpace(value) && float.TryParse(value, out _);

    /// <summary>
    /// 判断字符串是否为双精度浮点数（double）。
    /// </summary>
    /// <param name="value">待判断的字符串。</param>
    /// <returns>如果字符串可以解析为 double，返回 true；否则返回 false。</returns>
    public static bool IsDouble(this string value) =>
        !string.IsNullOrWhiteSpace(value) && double.TryParse(value, out _);

    /// <summary>
    /// 判断字符串是否为十进制数（decimal）。
    /// </summary>
    /// <param name="value">待判断的字符串。</param>
    /// <returns>如果字符串可以解析为 decimal，返回 true；否则返回 false。</returns>
    public static bool IsDecimal(this string value) =>
        !string.IsNullOrWhiteSpace(value) && decimal.TryParse(value, out _);

    /// <summary>
    /// 判断字符串是否为 Guid。
    /// </summary>
    /// <param name="value">待判断的字符串。</param>
    /// <returns>如果字符串可以解析为 Guid，返回 true；否则返回 false。</returns>
    public static bool IsGuid(this string value) => Guid.TryParse(value, out _);

    /// <summary>
    /// 判断字符串是否为布尔值。
    /// </summary>
    /// <param name="value">待判断的字符串。</param>
    /// <returns>如果字符串可以解析为 bool，返回 true；否则返回 false。</returns>
    public static bool IsBool(this string value) =>
        !string.IsNullOrWhiteSpace(value) && bool.TryParse(value, out _);

    /// <summary>
    /// 判断字符串是否为日期时间。
    /// </summary>
    /// <param name="value">待判断的字符串。</param>
    /// <returns>如果字符串可以解析为 DateTime，返回 true；否则返回 false。</returns>
    public static bool IsDateTime(this string value) =>
        !string.IsNullOrWhiteSpace(value) && DateTime.TryParse(value, out _);

    /// <summary>
    /// 判断字符串是否为字母（仅包含 a-z、A-Z）。
    /// </summary>
    /// <param name="value">待判断的字符串。</param>
    /// <returns>如果字符串仅包含字母，返回 true；否则返回 false。</returns>
    public static bool IsLetter(this string value) =>
        !string.IsNullOrEmpty(value) && value.All(char.IsLetter);

    /// <summary>
    /// 判断字符串是否为字母或数字。
    /// </summary>
    /// <param name="value">待判断的字符串。</param>
    /// <returns>如果字符串仅包含字母或数字，返回 true；否则返回 false。</returns>
    public static bool IsLetterOrDigit(this string value) =>
        !string.IsNullOrEmpty(value) && value.All(char.IsLetterOrDigit);

    /// <summary>
    /// 判断字符串是否为中文字符。
    /// </summary>
    /// <param name="value">待判断的字符串。</param>
    /// <returns>如果字符串仅包含中文和中文标点，返回 true；否则返回 false。</returns>
    public static bool IsChinese(this string value) =>
        !string.IsNullOrEmpty(value) && Regex.IsMatch(value, @"^[\u4e00-\u9fa5？，。""''。、；：！]+$");

    /// <summary>
    /// 判断字符串中是否包含中文。
    /// </summary>
    /// <param name="value">待判断的字符串。</param>
    /// <returns>如果字符串包含中文字符，返回 true；否则返回 false。</returns>
    public static bool HasChinese(this string value) =>
        !string.IsNullOrEmpty(value) && Regex.IsMatch(value, @"[\u4e00-\u9fa5]");

    /// <summary>
    /// 判断字符串是否为纯数字字符串（仅包含 0-9）。
    /// </summary>
    /// <param name="value">待判断的字符串。</param>
    /// <returns>如果字符串仅包含数字字符，返回 true；否则返回 false。</returns>
    public static bool IsDigits(this string value) =>
        !string.IsNullOrEmpty(value) && value.All(char.IsDigit);

    /// <summary>
    /// 判断字符串是否为十六进制字符串。
    /// </summary>
    /// <param name="value">待判断的字符串。</param>
    /// <returns>如果字符串为有效的十六进制字符串，返回 true；否则返回 false。</returns>
    public static bool IsHex(this string value) =>
        !string.IsNullOrEmpty(value) && Regex.IsMatch(value, @"^[0-9A-Fa-f]+$");

    #endregion

    #region 正则表达式验证

    /// <summary>
    /// 验证字符串是否为有效的电子邮件地址。
    /// </summary>
    /// <param name="value">待验证的字符串。</param>
    /// <returns>如果字符串为有效的电子邮件地址，返回 true；否则返回 false。</returns>
    public static bool IsEmail(this string value) =>
        !string.IsNullOrWhiteSpace(value) && 
        Regex.IsMatch(value, @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$", RegexOptions.Compiled);

    /// <summary>
    /// 验证字符串是否为有效的手机号码（中国大陆）。
    /// </summary>
    /// <param name="value">待验证的字符串。</param>
    /// <returns>如果字符串为有效的手机号码，返回 true；否则返回 false。</returns>
    public static bool IsMobile(this string value) =>
        !string.IsNullOrWhiteSpace(value) && 
        Regex.IsMatch(value, @"^1[3-9]\d{9}$", RegexOptions.Compiled);

    /// <summary>
    /// 验证字符串是否为有效的固定电话号码。
    /// </summary>
    /// <param name="value">待验证的字符串。</param>
    /// <returns>如果字符串为有效的固定电话号码，返回 true；否则返回 false。</returns>
    public static bool IsTel(this string value) =>
        !string.IsNullOrWhiteSpace(value) && 
        Regex.IsMatch(value, @"^(\d{3,4}-)?\d{7,8}(-\d{1,4})?$", RegexOptions.Compiled);

    /// <summary>
    /// 验证字符串是否为有效的 URL。
    /// </summary>
    /// <param name="value">待验证的字符串。</param>
    /// <returns>如果字符串为有效的 URL，返回 true；否则返回 false。</returns>
    public static bool IsUrl(this string value) =>
        !string.IsNullOrWhiteSpace(value) && 
        (Uri.TryCreate(value, UriKind.Absolute, out var result) && 
         (result.Scheme == Uri.UriSchemeHttp || result.Scheme == Uri.UriSchemeHttps));

    /// <summary>
    /// 验证字符串是否为有效的身份证号码（中国大陆）。
    /// </summary>
    /// <param name="value">待验证的字符串。</param>
    /// <returns>如果字符串为有效的身份证号码，返回 true；否则返回 false。</returns>
    public static bool IsIdCard(this string value)
    {
        if (string.IsNullOrWhiteSpace(value)) return false;
        if (value.Length != 15 && value.Length != 18) return false;
        
        if (value.Length == 15)
            return Regex.IsMatch(value, @"^\d{15}$");
        
        if (value.Length == 18)
        {
            if (!Regex.IsMatch(value, @"^\d{17}[\dXx]$")) return false;
            
            var weight = new[] { 7, 9, 10, 5, 8, 4, 2, 1, 6, 3, 7, 9, 10, 5, 8, 4, 2 };
            var checkCodes = "10X98765432";
            var sum = 0;
            for (int i = 0; i < 17; i++)
                sum += (value[i] - '0') * weight[i];
            var checkCode = checkCodes[sum % 11];
            return char.ToUpper(value[17]) == checkCode;
        }
        
        return false;
    }

    /// <summary>
    /// 验证字符串是否为有效的 IP 地址（IPv4 或 IPv6）。
    /// </summary>
    /// <param name="value">待验证的字符串。</param>
    /// <returns>如果字符串为有效的 IP 地址，返回 true；否则返回 false。</returns>
    public static bool IsIpAddress(this string value) =>
        !string.IsNullOrWhiteSpace(value) && System.Net.IPAddress.TryParse(value, out _);

    /// <summary>
    /// 验证字符串是否为有效的 IPv4 地址。
    /// </summary>
    /// <param name="value">待验证的字符串。</param>
    /// <returns>如果字符串为有效的 IPv4 地址，返回 true；否则返回 false。</returns>
    public static bool IsIPv4(this string value)
    {
        if (string.IsNullOrWhiteSpace(value)) return false;
        if (!System.Net.IPAddress.TryParse(value, out var ip)) return false;
        return ip.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork;
    }

    /// <summary>
    /// 验证字符串是否为有效的 IPv6 地址。
    /// </summary>
    /// <param name="value">待验证的字符串。</param>
    /// <returns>如果字符串为有效的 IPv6 地址，返回 true；否则返回 false。</returns>
    public static bool IsIPv6(this string value)
    {
        if (string.IsNullOrWhiteSpace(value)) return false;
        if (!System.Net.IPAddress.TryParse(value, out var ip)) return false;
        return ip.AddressFamily == System.Net.Sockets.AddressFamily.InterNetworkV6;
    }

    /// <summary>
    /// 验证字符串是否为有效的邮政编码（中国大陆）。
    /// </summary>
    /// <param name="value">待验证的字符串。</param>
    /// <returns>如果字符串为有效的邮政编码，返回 true；否则返回 false。</returns>
    public static bool IsZipCode(this string value) =>
        !string.IsNullOrWhiteSpace(value) && Regex.IsMatch(value, @"^\d{6}$");

    /// <summary>
    /// 验证字符串是否为有效的银行卡号（使用 Luhn 算法）。
    /// </summary>
    /// <param name="value">待验证的字符串。</param>
    /// <returns>如果字符串为有效的银行卡号，返回 true；否则返回 false。</returns>
    public static bool IsBankCard(this string value)
    {
        if (string.IsNullOrWhiteSpace(value) || !value.All(char.IsDigit)) return false;
        if (value.Length < 12 || value.Length > 19) return false;
        
        var digits = value.Select(c => c - '0').ToArray();
        var sum = 0;
        var alternate = false;
        
        for (int i = digits.Length - 1; i >= 0; i--)
        {
            var digit = digits[i];
            if (alternate)
            {
                digit *= 2;
                if (digit > 9) digit -= 9;
            }
            sum += digit;
            alternate = !alternate;
        }
        
        return sum % 10 == 0;
    }

    /// <summary>
    /// 验证字符串是否为有效的 JSON 格式。
    /// </summary>
    /// <param name="value">待验证的字符串。</param>
    /// <returns>如果字符串为有效的 JSON 格式，返回 true；否则返回 false。</returns>
    public static bool IsJson(this string value)
    {
        if (string.IsNullOrWhiteSpace(value)) return false;
        value = value.Trim();
        if ((!value.StartsWith("{") || !value.EndsWith("}")) && 
            (!value.StartsWith("[") || !value.EndsWith("]"))) 
            return false;
        
        try
        {
            using var doc = JsonDocument.Parse(value);
            return true;
        }
        catch
        {
            return false;
        }
    }

    /// <summary>
    /// 验证字符串是否为有效的 XML 格式。
    /// </summary>
    /// <param name="value">待验证的字符串。</param>
    /// <returns>如果字符串为有效的 XML 格式，返回 true；否则返回 false。</returns>
    public static bool IsXml(this string value)
    {
        if (string.IsNullOrWhiteSpace(value)) return false;
        try
        {
            var doc = new System.Xml.XmlDocument();
            doc.LoadXml(value);
            return true;
        }
        catch
        {
            return false;
        }
    }

    /// <summary>
    /// 验证字符串是否为有效的日期格式。
    /// </summary>
    /// <param name="value">待验证的字符串。</param>
    /// <returns>如果字符串为有效的日期格式，返回 true；否则返回 false。</returns>
    public static bool IsDate(this string value) =>
        !string.IsNullOrWhiteSpace(value) && DateTime.TryParse(value, out _);

    /// <summary>
    /// 验证字符串是否为有效的时间格式（HH:mm:ss）。
    /// </summary>
    /// <param name="value">待验证的字符串。</param>
    /// <returns>如果字符串为有效的时间格式，返回 true；否则返回 false。</returns>
    public static bool IsTime(this string value) =>
        !string.IsNullOrWhiteSpace(value) && 
        Regex.IsMatch(value, @"^([01]?\d|2[0-3]):[0-5]\d(:[0-5]\d)?$");

    /// <summary>
    /// 验证字符串是否为有效的 URL（使用正则表达式）。IsUrl 的别名方法。
    /// </summary>
    /// <param name="value">待验证的字符串。</param>
    /// <returns>如果字符串为有效的 URL，返回 true；否则返回 false。</returns>
    public static bool IsUrlByRegex(this string value) => value.IsUrl();

    /// <summary>
    /// 验证字符串是否为有效的日期格式（使用正则表达式）。IsDate 的别名方法。
    /// </summary>
    /// <param name="value">待验证的字符串。</param>
    /// <returns>如果字符串为有效的日期格式，返回 true；否则返回 false。</returns>
    public static bool IsDateByRegex(this string value) => value.IsDate();

    /// <summary>
    /// 验证字符串是否为有效的时间格式（使用正则表达式）。IsTime 的别名方法。
    /// </summary>
    /// <param name="value">待验证的字符串。</param>
    /// <returns>如果字符串为有效的时间格式，返回 true；否则返回 false。</returns>
    public static bool IsTimeByRegex(this string value) => value.IsTime();

    /// <summary>
    /// 验证字符串是否为有效的日期时间格式（使用正则表达式）。IsDateTime 的别名方法。
    /// </summary>
    /// <param name="value">待验证的字符串。</param>
    /// <returns>如果字符串为有效的日期时间格式，返回 true；否则返回 false。</returns>
    public static bool IsDateTimeByRegex(this string value) => value.IsDateTime();

    /// <summary>
    /// 验证字符串是否仅包含字母（使用正则表达式）。IsLetter 的别名方法。
    /// </summary>
    /// <param name="value">待验证的字符串。</param>
    /// <returns>如果字符串仅包含字母，返回 true；否则返回 false。</returns>
    public static bool IsLetterByRegex(this string value) => value.IsLetter();

    /// <summary>
    /// 验证字符串是否仅包含数字（使用正则表达式）。IsNumeric 的别名方法。
    /// </summary>
    /// <param name="value">待验证的字符串。</param>
    /// <returns>如果字符串仅包含数字，返回 true；否则返回 false。</returns>
    public static bool IsNumByRegex(this string value) => value.IsNumeric();

    /// <summary>
    /// 提取字符串中的数字部分（使用正则表达式）。ExtractDigits 的别名方法。
    /// </summary>
    /// <param name="value">待处理的字符串。</param>
    /// <returns>提取的数字字符串。</returns>
    public static string ExtractNumByRegex(this string value) => value.ExtractDigits();

    /// <summary>
    /// 验证字符串是否为浮点数（使用正则表达式）。IsFloat 的别名方法。
    /// </summary>
    /// <param name="value">待验证的字符串。</param>
    /// <returns>如果字符串为浮点数，返回 true；否则返回 false。</returns>
    public static bool IsFloatByRegex(this string value) => value.IsFloat();

    /// <summary>
    /// 验证字符串是否为有效的电子邮件地址（使用正则表达式）。IsEmail 的别名方法。
    /// </summary>
    /// <param name="value">待验证的字符串。</param>
    /// <returns>如果字符串为有效的电子邮件地址，返回 true；否则返回 false。</returns>
    public static bool IsEmailByRegex(this string value) => value.IsEmail();

    /// <summary>
    /// 验证字符串是否为有效的固定电话号码（使用正则表达式）。IsTel 的别名方法。
    /// </summary>
    /// <param name="value">待验证的字符串。</param>
    /// <returns>如果字符串为有效的固定电话号码，返回 true；否则返回 false。</returns>
    public static bool IsTelByRegex(this string value) => value.IsTel();

    /// <summary>
    /// 验证字符串是否为有效的手机号码（使用正则表达式）。IsMobile 的别名方法。
    /// </summary>
    /// <param name="value">待验证的字符串。</param>
    /// <returns>如果字符串为有效的手机号码，返回 true；否则返回 false。</returns>
    public static bool IsMobileByRegex(this string value) => value.IsMobile();

    #endregion

    #region 类型转换

    /// <summary>
    /// 将字符串转换为整数，转换失败返回默认值。
    /// </summary>
    /// <param name="value">待转换的字符串。</param>
    /// <param name="defaultValue">转换失败时的默认值，默认为 0。</param>
    /// <returns>转换后的整数值。</returns>
    public static int ToInt(this string value, int defaultValue = 0) =>
        int.TryParse(value, out var result) ? result : defaultValue;

    /// <summary>
    /// 将字符串转换为可空整数，转换失败返回 null。
    /// </summary>
    /// <param name="value">待转换的字符串。</param>
    /// <returns>转换后的可空整数值。</returns>
    public static int? ToIntOrNull(this string value) =>
        int.TryParse(value, out var result) ? result : null;

    /// <summary>
    /// 将字符串转换为长整数，转换失败返回默认值。
    /// </summary>
    /// <param name="value">待转换的字符串。</param>
    /// <param name="defaultValue">转换失败时的默认值，默认为 0。</param>
    /// <returns>转换后的长整数值。</returns>
    public static long ToLong(this string value, long defaultValue = 0) =>
        long.TryParse(value, out var result) ? result : defaultValue;

    /// <summary>
    /// 将字符串转换为可空长整数，转换失败返回 null。
    /// </summary>
    /// <param name="value">待转换的字符串。</param>
    /// <returns>转换后的可空长整数值。</returns>
    public static long? ToLongOrNull(this string value) =>
        long.TryParse(value, out var result) ? result : null;

    /// <summary>
    /// 将字符串转换为短整数，转换失败返回默认值。
    /// </summary>
    /// <param name="value">待转换的字符串。</param>
    /// <param name="defaultValue">转换失败时的默认值，默认为 0。</param>
    /// <returns>转换后的短整数值。</returns>
    public static short ToShort(this string value, short defaultValue = 0) =>
        short.TryParse(value, out var result) ? result : defaultValue;

    /// <summary>
    /// 将字符串转换为可空短整数，转换失败返回 null。
    /// </summary>
    /// <param name="value">待转换的字符串。</param>
    /// <returns>转换后的可空短整数值。</returns>
    public static short? ToShortOrNull(this string value) =>
        short.TryParse(value, out var result) ? result : null;

    /// <summary>
    /// 将字符串转换为单精度浮点数，转换失败返回默认值。
    /// </summary>
    /// <param name="value">待转换的字符串。</param>
    /// <param name="defaultValue">转换失败时的默认值，默认为 0。</param>
    /// <returns>转换后的单精度浮点数值。</returns>
    public static float ToFloat(this string value, float defaultValue = 0) =>
        float.TryParse(value, out var result) ? result : defaultValue;

    /// <summary>
    /// 将字符串转换为可空单精度浮点数，转换失败返回 null。
    /// </summary>
    /// <param name="value">待转换的字符串。</param>
    /// <returns>转换后的可空单精度浮点数值。</returns>
    public static float? ToFloatOrNull(this string value) =>
        float.TryParse(value, out var result) ? result : null;

    /// <summary>
    /// 将字符串转换为单精度浮点数并保留指定小数位（四舍五入）。
    /// </summary>
    /// <param name="value">待转换的字符串。</param>
    /// <param name="digits">保留的小数位数，默认为 2。</param>
    /// <param name="defaultValue">转换失败时的默认值，默认为 0。</param>
    /// <returns>转换后的单精度浮点数值。</returns>
    public static float ToFloatRound(this string value, int digits = 2, float defaultValue = 0)
    {
        if (float.TryParse(value, out var result))
            return (float)Math.Round(result, digits, MidpointRounding.AwayFromZero);
        return defaultValue;
    }

    /// <summary>
    /// 将字符串转换为单精度浮点数并截断到指定小数位。
    /// </summary>
    /// <param name="value">待转换的字符串。</param>
    /// <param name="digits">保留的小数位数，默认为 2。</param>
    /// <param name="defaultValue">转换失败时的默认值，默认为 0。</param>
    /// <returns>转换后的单精度浮点数值。</returns>
    public static float ToFloatTruncate(this string value, int digits = 2, float defaultValue = 0)
    {
        if (float.TryParse(value, out var result))
        {
            var factor = (float)Math.Pow(10, digits);
            return (float)Math.Truncate(result * factor) / factor;
        }
        return defaultValue;
    }

    /// <summary>
    /// 将字符串转换为双精度浮点数，转换失败返回默认值。
    /// </summary>
    /// <param name="value">待转换的字符串。</param>
    /// <param name="defaultValue">转换失败时的默认值，默认为 0。</param>
    /// <returns>转换后的双精度浮点数值。</returns>
    public static double ToDouble(this string value, double defaultValue = 0) =>
        double.TryParse(value, out var result) ? result : defaultValue;

    /// <summary>
    /// 将字符串转换为可空双精度浮点数，转换失败返回 null。
    /// </summary>
    /// <param name="value">待转换的字符串。</param>
    /// <returns>转换后的可空双精度浮点数值。</returns>
    public static double? ToDoubleOrNull(this string value) =>
        double.TryParse(value, out var result) ? result : null;

    /// <summary>
    /// 将字符串转换为双精度浮点数并保留指定小数位（四舍五入）。
    /// </summary>
    /// <param name="value">待转换的字符串。</param>
    /// <param name="digits">保留的小数位数，默认为 2。</param>
    /// <param name="defaultValue">转换失败时的默认值，默认为 0。</param>
    /// <returns>转换后的双精度浮点数值。</returns>
    public static double ToDoubleRound(this string value, int digits = 2, double defaultValue = 0)
    {
        if (double.TryParse(value, out var result))
            return Math.Round(result, digits, MidpointRounding.AwayFromZero);
        return defaultValue;
    }

    /// <summary>
    /// 将字符串转换为双精度浮点数并截断到指定小数位。
    /// </summary>
    /// <param name="value">待转换的字符串。</param>
    /// <param name="digits">保留的小数位数，默认为 2。</param>
    /// <param name="defaultValue">转换失败时的默认值，默认为 0。</param>
    /// <returns>转换后的双精度浮点数值。</returns>
    public static double ToDoubleTruncate(this string value, int digits = 2, double defaultValue = 0)
    {
        if (double.TryParse(value, out var result))
        {
            var factor = Math.Pow(10, digits);
            return Math.Truncate(result * factor) / factor;
        }
        return defaultValue;
    }

    /// <summary>
    /// 将字符串转换为十进制数，转换失败返回默认值。
    /// </summary>
    /// <param name="value">待转换的字符串。</param>
    /// <param name="defaultValue">转换失败时的默认值，默认为 0。</param>
    /// <returns>转换后的十进制数值。</returns>
    public static decimal ToDecimal(this string value, decimal defaultValue = 0) =>
        decimal.TryParse(value, out var result) ? result : defaultValue;

    /// <summary>
    /// 将字符串转换为可空十进制数，转换失败返回 null。
    /// </summary>
    /// <param name="value">待转换的字符串。</param>
    /// <returns>转换后的可空十进制数值。</returns>
    public static decimal? ToDecimalOrNull(this string value) =>
        decimal.TryParse(value, out var result) ? result : null;

    /// <summary>
    /// 将字符串转换为十进制数并保留指定小数位（四舍五入）。
    /// </summary>
    /// <param name="value">待转换的字符串。</param>
    /// <param name="digits">保留的小数位数，默认为 2。</param>
    /// <param name="defaultValue">转换失败时的默认值，默认为 0。</param>
    /// <returns>转换后的十进制数值。</returns>
    public static decimal ToDecimalRound(this string value, int digits = 2, decimal defaultValue = 0)
    {
        if (decimal.TryParse(value, out var result))
            return Math.Round(result, digits, MidpointRounding.AwayFromZero);
        return defaultValue;
    }

    /// <summary>
    /// 将字符串转换为布尔值，转换失败返回默认值。
    /// </summary>
    /// <param name="value">待转换的字符串。</param>
    /// <param name="defaultValue">转换失败时的默认值，默认为 false。</param>
    /// <returns>转换后的布尔值。</returns>
    public static bool ToBool(this string value, bool defaultValue = false) =>
        bool.TryParse(value, out var result) ? result : defaultValue;

    /// <summary>
    /// 将字符串转换为可空布尔值，转换失败返回 null。
    /// </summary>
    /// <param name="value">待转换的字符串。</param>
    /// <returns>转换后的可空布尔值。</returns>
    public static bool? ToBoolOrNull(this string value) =>
        bool.TryParse(value, out var result) ? result : null;

    /// <summary>
    /// 将字符串转换为布尔值（支持多种格式）。
    /// </summary>
    /// <param name="value">待转换的字符串。</param>
    /// <param name="trueValues">被视为 true 的字符串数组，默认包含 "true", "1", "yes", "on", "y"。</param>
    /// <param name="defaultValue">转换失败时的默认值。</param>
    /// <returns>转换后的布尔值。</returns>
    public static bool ToBoolExtended(this string value, string[] trueValues = null, bool defaultValue = false)
    {
        if (string.IsNullOrWhiteSpace(value)) return defaultValue;
        
        var trueList = trueValues ?? new[] { "true", "1", "yes", "on", "y", "是", "真", "开启" };
        return trueList.Contains(value.ToLower().Trim());
    }

    /// <summary>
    /// 将字符串转换为 Guid，转换失败返回默认值。
    /// </summary>
    /// <param name="value">待转换的字符串。</param>
    /// <param name="defaultValue">转换失败时的默认值，默认为 Guid.Empty。</param>
    /// <returns>转换后的 Guid 值。</returns>
    public static Guid ToGuid(this string value, Guid defaultValue = default) =>
        Guid.TryParse(value, out var result) ? result : defaultValue;

    /// <summary>
    /// 将字符串转换为可空 Guid，转换失败返回 null。
    /// </summary>
    /// <param name="value">待转换的字符串。</param>
    /// <returns>转换后的可空 Guid 值。</returns>
    public static Guid? ToGuidOrNull(this string value) =>
        Guid.TryParse(value, out var result) ? result : null;

    /// <summary>
    /// 将字符串转换为日期时间，转换失败返回默认值。
    /// </summary>
    /// <param name="value">待转换的字符串。</param>
    /// <param name="defaultValue">转换失败时的默认值，默认为 DateTime.MinValue。</param>
    /// <returns>转换后的日期时间值。</returns>
    public static DateTime ToDateTime(this string value, DateTime defaultValue = default) =>
        DateTime.TryParse(value, out var result) ? result : defaultValue;

    /// <summary>
    /// 将字符串转换为可空日期时间，转换失败返回 null。
    /// </summary>
    /// <param name="value">待转换的字符串。</param>
    /// <returns>转换后的可空日期时间值。</returns>
    public static DateTime? ToDateTimeOrNull(this string value) =>
        DateTime.TryParse(value, out var result) ? result : null;

    /// <summary>
    /// 将字符串按指定格式转换为日期时间。
    /// </summary>
    /// <param name="value">待转换的字符串。</param>
    /// <param name="format">日期时间格式字符串。</param>
    /// <param name="defaultValue">转换失败时的默认值。</param>
    /// <returns>转换后的日期时间值。</returns>
    public static DateTime ToDateTimeExact(this string value, string format, DateTime defaultValue = default) =>
        DateTime.TryParseExact(value, format, null, System.Globalization.DateTimeStyles.None, out var result) 
            ? result : defaultValue;

    /// <summary>
    /// 将字符串转换为枚举类型，转换失败返回默认值。
    /// </summary>
    /// <typeparam name="TEnum">目标枚举类型。</typeparam>
    /// <param name="value">待转换的字符串。</param>
    /// <param name="defaultValue">转换失败时的默认值。</param>
    /// <param name="ignoreCase">是否忽略大小写，默认为 true。</param>
    /// <returns>转换后的枚举值。</returns>
    public static TEnum ToEnum<TEnum>(this string value, TEnum defaultValue = default, bool ignoreCase = true) 
        where TEnum : struct, Enum
    {
        if (string.IsNullOrWhiteSpace(value)) return defaultValue;
        return Enum.TryParse<TEnum>(value, ignoreCase, out var result) ? result : defaultValue;
    }

    /// <summary>
    /// 将字符串转换为字节数组。
    /// </summary>
    /// <param name="value">待转换的字符串。</param>
    /// <param name="encoding">编码方式，默认为 UTF-8。</param>
    /// <returns>转换后的字节数组。</returns>
    public static byte[] ToBytes(this string value, Encoding encoding = null)
    {
        encoding ??= Encoding.UTF8;
        return string.IsNullOrEmpty(value) ? Array.Empty<byte>() : encoding.GetBytes(value);
    }

    /// <summary>
    /// 将字符串转换为指定类型的对象。
    /// </summary>
    /// <typeparam name="T">目标类型。</typeparam>
    /// <param name="value">待转换的字符串。</param>
    /// <param name="defaultValue">转换失败时的默认值。</param>
    /// <returns>转换后的对象。</returns>
    public static T To<T>(this string value, T defaultValue = default)
    {
        if (string.IsNullOrWhiteSpace(value)) return defaultValue;
        
        try
        {
            var targetType = typeof(T);
            var underlyingType = Nullable.GetUnderlyingType(targetType) ?? targetType;
            
            if (underlyingType == typeof(string)) return (T)(object)value;
            if (underlyingType == typeof(Guid)) return (T)(object)Guid.Parse(value);
            if (underlyingType == typeof(DateTime)) return (T)(object)DateTime.Parse(value);
            
            return (T)Convert.ChangeType(value, underlyingType);
        }
        catch
        {
            return defaultValue;
        }
    }

    #endregion

    #region 编码解码

    /// <summary>
    /// 将字符串编码为 Base64 字符串。
    /// </summary>
    /// <param name="value">待编码的字符串。</param>
    /// <param name="encoding">编码方式，默认为 UTF-8。</param>
    /// <returns>Base64 编码后的字符串。</returns>
    public static string ToBase64(this string value, Encoding encoding = null)
    {
        if (string.IsNullOrEmpty(value)) return string.Empty;
        encoding ??= Encoding.UTF8;
        return Convert.ToBase64String(encoding.GetBytes(value));
    }

    /// <summary>
    /// 将 Base64 字符串解码为原始字符串。
    /// </summary>
    /// <param name="value">Base64 编码的字符串。</param>
    /// <param name="encoding">编码方式，默认为 UTF-8。</param>
    /// <returns>解码后的原始字符串。</returns>
    public static string FromBase64(this string value, Encoding encoding = null)
    {
        if (string.IsNullOrEmpty(value)) return string.Empty;
        encoding ??= Encoding.UTF8;
        try
        {
            var bytes = Convert.FromBase64String(value);
            return encoding.GetString(bytes);
        }
        catch
        {
            return string.Empty;
        }
    }

    /// <summary>
    /// 将字符串进行 URL 编码。
    /// </summary>
    /// <param name="value">待编码的字符串。</param>
    /// <returns>URL 编码后的字符串。</returns>
    public static string UrlEncode(this string value)
    {
        if (string.IsNullOrEmpty(value)) return string.Empty;
        return HttpUtility.UrlEncode(value);
    }

    /// <summary>
    /// 将 URL 编码的字符串解码。
    /// </summary>
    /// <param name="value">URL 编码的字符串。</param>
    /// <returns>解码后的原始字符串。</returns>
    public static string UrlDecode(this string value)
    {
        if (string.IsNullOrEmpty(value)) return string.Empty;
        return HttpUtility.UrlDecode(value);
    }

    /// <summary>
    /// 将字符串进行 HTML 编码。
    /// </summary>
    /// <param name="value">待编码的字符串。</param>
    /// <returns>HTML 编码后的字符串。</returns>
    public static string HtmlEncode(this string value)
    {
        if (string.IsNullOrEmpty(value)) return string.Empty;
        return HttpUtility.HtmlEncode(value);
    }

    /// <summary>
    /// 将 HTML 编码的字符串解码。
    /// </summary>
    /// <param name="value">HTML 编码的字符串。</param>
    /// <returns>解码后的原始字符串。</returns>
    public static string HtmlDecode(this string value)
    {
        if (string.IsNullOrEmpty(value)) return string.Empty;
        return HttpUtility.HtmlDecode(value);
    }

    /// <summary>
    /// 计算字符串的 MD5 哈希值。
    /// </summary>
    /// <param name="value">待计算的字符串。</param>
    /// <param name="encoding">编码方式，默认为 UTF-8。</param>
    /// <returns>MD5 哈希值（32位小写十六进制字符串）。</returns>
    public static string ToMd5(this string value, Encoding encoding = null)
    {
        if (string.IsNullOrEmpty(value)) return string.Empty;
        encoding ??= Encoding.UTF8;
        using var md5 = MD5.Create();
        var bytes = md5.ComputeHash(encoding.GetBytes(value));
        return Convert.ToHexString(bytes).ToLowerInvariant();
    }

    /// <summary>
    /// 计算字符串的 SHA256 哈希值。
    /// </summary>
    /// <param name="value">待计算的字符串。</param>
    /// <param name="encoding">编码方式，默认为 UTF-8。</param>
    /// <returns>SHA256 哈希值（64位小写十六进制字符串）。</returns>
    public static string ToSha256(this string value, Encoding encoding = null)
    {
        if (string.IsNullOrEmpty(value)) return string.Empty;
        encoding ??= Encoding.UTF8;
        using var sha256 = SHA256.Create();
        var bytes = sha256.ComputeHash(encoding.GetBytes(value));
        return Convert.ToHexString(bytes).ToLowerInvariant();
    }

    /// <summary>
    /// 计算字符串的 SHA1 哈希值。
    /// </summary>
    /// <param name="value">待计算的字符串。</param>
    /// <param name="encoding">编码方式，默认为 UTF-8。</param>
    /// <returns>SHA1 哈希值（40位小写十六进制字符串）。</returns>
    public static string ToSha1(this string value, Encoding encoding = null)
    {
        if (string.IsNullOrEmpty(value)) return string.Empty;
        encoding ??= Encoding.UTF8;
        using var sha1 = SHA1.Create();
        var bytes = sha1.ComputeHash(encoding.GetBytes(value));
        return Convert.ToHexString(bytes).ToLowerInvariant();
    }

    /// <summary>
    /// 计算字符串的 HMAC-SHA256 哈希值。
    /// </summary>
    /// <param name="value">待计算的字符串。</param>
    /// <param name="key">密钥。</param>
    /// <param name="encoding">编码方式，默认为 UTF-8。</param>
    /// <returns>HMAC-SHA256 哈希值（小写十六进制字符串）。</returns>
    public static string ToHmacSha256(this string value, string key, Encoding encoding = null)
    {
        if (string.IsNullOrEmpty(value)) return string.Empty;
        encoding ??= Encoding.UTF8;
        using var hmac = new HMACSHA256(encoding.GetBytes(key));
        var bytes = hmac.ComputeHash(encoding.GetBytes(value));
        return Convert.ToHexString(bytes).ToLowerInvariant();
    }

    #endregion

    #region 字符串操作

    /// <summary>
    /// 安全去除字符串首尾空白字符，null 返回空字符串。
    /// </summary>
    /// <param name="value">待处理的字符串。</param>
    /// <returns>去除首尾空白后的字符串。</returns>
    public static string TrimSafe(this string value) => value?.Trim() ?? string.Empty;

    /// <summary>
    /// 移除字符串中的所有空白字符。
    /// </summary>
    /// <param name="value">待处理的字符串。</param>
    /// <returns>移除空白后的字符串。</returns>
    public static string RemoveWhiteSpace(this string value) =>
        string.IsNullOrEmpty(value) ? string.Empty : string.Concat(value.Where(c => !char.IsWhiteSpace(c)));

    /// <summary>
    /// 安全截取字符串，超出范围返回空字符串或部分字符串。
    /// </summary>
    /// <param name="value">待处理的字符串。</param>
    /// <param name="startIndex">起始索引。</param>
    /// <param name="length">截取长度。</param>
    /// <returns>截取后的字符串。</returns>
    public static string SubstringSafe(this string value, int startIndex, int length)
    {
        if (string.IsNullOrEmpty(value) || startIndex < 0 || length <= 0 || startIndex >= value.Length)
            return string.Empty;
        return value.Length - startIndex < length
            ? value[startIndex..]
            : value.Substring(startIndex, length);
    }

    /// <summary>
    /// 获取字符串左侧指定长度的子串。
    /// </summary>
    /// <param name="value">待处理的字符串。</param>
    /// <param name="length">子串长度。</param>
    /// <returns>左侧子串。</returns>
    public static string Left(this string value, int length)
    {
        if (string.IsNullOrEmpty(value) || length <= 0) return string.Empty;
        return value.Length <= length ? value : value[..length];
    }

    /// <summary>
    /// 获取字符串右侧指定长度的子串。
    /// </summary>
    /// <param name="value">待处理的字符串。</param>
    /// <param name="length">子串长度。</param>
    /// <returns>右侧子串。</returns>
    public static string Right(this string value, int length)
    {
        if (string.IsNullOrEmpty(value) || length <= 0) return string.Empty;
        return value.Length <= length ? value : value.Substring(value.Length - length);
    }

    /// <summary>
    /// 反转字符串。
    /// </summary>
    /// <param name="value">待处理的字符串。</param>
    /// <returns>反转后的字符串。</returns>
    public static string Reverse(this string value)
    {
        if (string.IsNullOrEmpty(value)) return string.Empty;
        var arr = value.ToCharArray();
        Array.Reverse(arr);
        return new string(arr);
    }

    /// <summary>
    /// 移除字符串中的特殊字符，仅保留字母、数字和空白。
    /// </summary>
    /// <param name="value">待处理的字符串。</param>
    /// <returns>处理后的字符串。</returns>
    public static string RemoveSpecialChars(this string value)
    {
        if (string.IsNullOrEmpty(value)) return string.Empty;
        return new string(value.Where(c => char.IsLetterOrDigit(c) || char.IsWhiteSpace(c)).ToArray());
    }

    /// <summary>
    /// 移除字符串中的数字。
    /// </summary>
    /// <param name="value">待处理的字符串。</param>
    /// <returns>移除数字后的字符串。</returns>
    public static string RemoveDigits(this string value)
    {
        if (string.IsNullOrEmpty(value)) return string.Empty;
        return new string(value.Where(c => !char.IsDigit(c)).ToArray());
    }

    /// <summary>
    /// 移除字符串中的字母。
    /// </summary>
    /// <param name="value">待处理的字符串。</param>
    /// <returns>移除字母后的字符串。</returns>
    public static string RemoveLetters(this string value)
    {
        if (string.IsNullOrEmpty(value)) return string.Empty;
        return new string(value.Where(c => !char.IsLetter(c)).ToArray());
    }

    /// <summary>
    /// 提取字符串中的数字部分。
    /// </summary>
    /// <param name="value">待处理的字符串。</param>
    /// <returns>提取的数字字符串。</returns>
    public static string ExtractDigits(this string value)
    {
        if (string.IsNullOrEmpty(value)) return string.Empty;
        return new string(value.Where(char.IsDigit).ToArray());
    }

    /// <summary>
    /// 提取字符串中的字母部分。
    /// </summary>
    /// <param name="value">待处理的字符串。</param>
    /// <returns>提取的字母字符串。</returns>
    public static string ExtractLetters(this string value)
    {
        if (string.IsNullOrEmpty(value)) return string.Empty;
        return new string(value.Where(char.IsLetter).ToArray());
    }

    /// <summary>
    /// 截断字符串到指定长度，超出部分用省略号替代。
    /// </summary>
    /// <param name="value">待处理的字符串。</param>
    /// <param name="maxLength">最大长度。</param>
    /// <param name="suffix">后缀字符串，默认为 "..."。</param>
    /// <returns>截断后的字符串。</returns>
    public static string Truncate(this string value, int maxLength, string suffix = "...")
    {
        if (string.IsNullOrEmpty(value) || maxLength <= 0) return string.Empty;
        if (value.Length <= maxLength) return value;
        return value[..(maxLength - suffix.Length)] + suffix;
    }

    /// <summary>
    /// 重复字符串指定次数。
    /// </summary>
    /// <param name="value">待重复的字符串。</param>
    /// <param name="count">重复次数。</param>
    /// <returns>重复后的字符串。</returns>
    public static string Repeat(this string value, int count)
    {
        if (string.IsNullOrEmpty(value) || count <= 0) return string.Empty;
        return string.Concat(Enumerable.Repeat(value, count));
    }

    /// <summary>
    /// 将字符串首字母大写。
    /// </summary>
    /// <param name="value">待处理的字符串。</param>
    /// <returns>首字母大写后的字符串。</returns>
    public static string Capitalize(this string value)
    {
        if (string.IsNullOrEmpty(value)) return string.Empty;
        return char.ToUpper(value[0]) + value[1..];
    }

    /// <summary>
    /// 将字符串首字母小写。
    /// </summary>
    /// <param name="value">待处理的字符串。</param>
    /// <returns>首字母小写后的字符串。</returns>
    public static string Uncapitalize(this string value)
    {
        if (string.IsNullOrEmpty(value)) return string.Empty;
        return char.ToLower(value[0]) + value[1..];
    }

    /// <summary>
    /// 忽略大小写判断字符串是否相等。
    /// </summary>
    /// <param name="value">原字符串。</param>
    /// <param name="compare">要比较的字符串。</param>
    /// <returns>如果忽略大小写后相等，返回 true；否则返回 false。</returns>
    public static bool EqualsIgnoreCase(this string value, string compare) =>
        string.Equals(value, compare, StringComparison.OrdinalIgnoreCase);

    /// <summary>
    /// 忽略大小写判断字符串是否包含指定子串。
    /// </summary>
    /// <param name="value">原字符串。</param>
    /// <param name="substring">要查找的子串。</param>
    /// <returns>如果忽略大小写后包含，返回 true；否则返回 false。</returns>
    public static bool ContainsIgnoreCase(this string value, string substring)
    {
        if (string.IsNullOrEmpty(value) || string.IsNullOrEmpty(substring)) return false;
        return value.IndexOf(substring, StringComparison.OrdinalIgnoreCase) >= 0;
    }

    /// <summary>
    /// 忽略大小写判断字符串是否以指定前缀开头。
    /// </summary>
    /// <param name="value">原字符串。</param>
    /// <param name="prefix">前缀字符串。</param>
    /// <returns>如果忽略大小写后以指定前缀开头，返回 true；否则返回 false。</returns>
    public static bool StartsWithIgnoreCase(this string value, string prefix) =>
        !string.IsNullOrEmpty(value) && value.StartsWith(prefix, StringComparison.OrdinalIgnoreCase);

    /// <summary>
    /// 忽略大小写判断字符串是否以指定后缀结尾。
    /// </summary>
    /// <param name="value">原字符串。</param>
    /// <param name="suffix">后缀字符串。</param>
    /// <returns>如果忽略大小写后以指定后缀结尾，返回 true；否则返回 false。</returns>
    public static bool EndsWithIgnoreCase(this string value, string suffix) =>
        !string.IsNullOrEmpty(value) && value.EndsWith(suffix, StringComparison.OrdinalIgnoreCase);

    /// <summary>
    /// 忽略大小写替换字符串中的所有匹配项。
    /// </summary>
    /// <param name="value">原字符串。</param>
    /// <param name="oldValue">要替换的字符串。</param>
    /// <param name="newValue">替换后的字符串。</param>
    /// <returns>替换后的字符串。</returns>
    public static string ReplaceIgnoreCase(this string value, string oldValue, string newValue)
    {
        if (string.IsNullOrEmpty(value) || string.IsNullOrEmpty(oldValue)) return value ?? string.Empty;
        return Regex.Replace(value, Regex.Escape(oldValue), newValue ?? string.Empty, RegexOptions.IgnoreCase);
    }

    /// <summary>
    /// 保留字符串中的小数位数（格式化）。
    /// </summary>
    /// <param name="value">待处理的字符串。</param>
    /// <param name="digits">保留的小数位数。</param>
    /// <returns>格式化后的字符串。</returns>
    public static string KeepDecimal(this string value, int digits = 2)
    {
        if (string.IsNullOrWhiteSpace(value)) return value ?? string.Empty;
        if (double.TryParse(value, out var num))
            return num.ToString($"F{digits}");
        return value;
    }

    #endregion

    #region 掩码与脱敏

    /// <summary>
    /// 对字符串进行掩码处理（保留首尾，中间用指定字符替换）。
    /// </summary>
    /// <param name="value">待处理的字符串。</param>
    /// <param name="startLength">保留开头的字符数，默认为 3。</param>
    /// <param name="endLength">保留结尾的字符数，默认为 4。</param>
    /// <param name="maskChar">掩码字符，默认为 '*'。</param>
    /// <returns>掩码后的字符串。</returns>
    public static string Mask(this string value, int startLength = 3, int endLength = 4, char maskChar = '*')
    {
        if (string.IsNullOrEmpty(value)) return string.Empty;
        if (value.Length <= startLength + endLength) return new string(maskChar, value.Length);
        
        var start = value[..startLength];
        var end = value.Substring(value.Length - endLength);
        var maskLength = value.Length - startLength - endLength;
        return start + new string(maskChar, maskLength) + end;
    }

    /// <summary>
    /// 对手机号码进行脱敏处理。
    /// </summary>
    /// <param name="value">手机号码字符串。</param>
    /// <returns>脱敏后的手机号码。</returns>
    public static string MaskMobile(this string value) => value.Mask(3, 4);

    /// <summary>
    /// 对邮箱地址进行脱敏处理。
    /// </summary>
    /// <param name="value">邮箱地址字符串。</param>
    /// <returns>脱敏后的邮箱地址。</returns>
    public static string MaskEmail(this string value)
    {
        if (string.IsNullOrEmpty(value) || !value.Contains('@')) return value ?? string.Empty;
        
        var parts = value.Split('@');
        var name = parts[0];
        var domain = parts[1];
        
        if (name.Length <= 2) return name[0] + "***@" + domain;
        return name[..2] + "***@" + domain;
    }

    /// <summary>
    /// 对身份证号码进行脱敏处理。
    /// </summary>
    /// <param name="value">身份证号码字符串。</param>
    /// <returns>脱敏后的身份证号码。</returns>
    public static string MaskIdCard(this string value)
    {
        if (string.IsNullOrEmpty(value)) return string.Empty;
        if (value.Length == 15) return value.Mask(3, 3);
        if (value.Length == 18) return value.Mask(3, 4);
        return value.Mask(3, 3);
    }

    /// <summary>
    /// 对银行卡号进行脱敏处理。
    /// </summary>
    /// <param name="value">银行卡号字符串。</param>
    /// <returns>脱敏后的银行卡号。</returns>
    public static string MaskBankCard(this string value) => value.Mask(4, 4);

    /// <summary>
    /// 对姓名进行脱敏处理。
    /// </summary>
    /// <param name="value">姓名字符串。</param>
    /// <returns>脱敏后的姓名。</returns>
    public static string MaskName(this string value)
    {
        if (string.IsNullOrEmpty(value)) return string.Empty;
        if (value.Length == 1) return value;
        if (value.Length == 2) return value[0] + "*";
        return value[0] + new string('*', value.Length - 1);
    }

    #endregion

    #region 格式转换

    /// <summary>
    /// 将字符串转换为 camelCase（驼峰命名）。
    /// </summary>
    /// <param name="value">待处理的字符串。</param>
    /// <returns>驼峰命名格式的字符串。</returns>
    public static string ToCamelCase(this string value)
    {
        if (string.IsNullOrEmpty(value)) return string.Empty;
        
        var words = GetWords(value);
        if (words.Length == 0) return string.Empty;
        
        var result = new StringBuilder();
        result.Append(words[0].ToLower());
        
        for (int i = 1; i < words.Length; i++)
        {
            var word = words[i];
            if (word.Length > 0)
            {
                result.Append(char.ToUpper(word[0]));
                result.Append(word[1..].ToLower());
            }
        }
        
        return result.ToString();
    }

    /// <summary>
    /// 将字符串转换为 PascalCase（帕斯卡命名）。
    /// </summary>
    /// <param name="value">待处理的字符串。</param>
    /// <returns>帕斯卡命名格式的字符串。</returns>
    public static string ToPascalCase(this string value)
    {
        if (string.IsNullOrEmpty(value)) return string.Empty;
        
        var words = GetWords(value);
        if (words.Length == 0) return string.Empty;
        
        var result = new StringBuilder();
        foreach (var word in words)
        {
            if (word.Length > 0)
            {
                result.Append(char.ToUpper(word[0]));
                result.Append(word[1..].ToLower());
            }
        }
        
        return result.ToString();
    }

    /// <summary>
    /// 将字符串转换为 snake_case（蛇形命名）。
    /// </summary>
    /// <param name="value">待处理的字符串。</param>
    /// <returns>蛇形命名格式的字符串。</returns>
    public static string ToSnakeCase(this string value)
    {
        if (string.IsNullOrEmpty(value)) return string.Empty;
        
        var words = GetWords(value);
        return string.Join("_", words.Select(w => w.ToLower()));
    }

    /// <summary>
    /// 将字符串转换为 kebab-case（短横线命名）。
    /// </summary>
    /// <param name="value">待处理的字符串。</param>
    /// <returns>短横线命名格式的字符串。</returns>
    public static string ToKebabCase(this string value)
    {
        if (string.IsNullOrEmpty(value)) return string.Empty;
        
        var words = GetWords(value);
        return string.Join("-", words.Select(w => w.ToLower()));
    }

    /// <summary>
    /// 将字符串转换为常量命名（UPPER_SNAKE_CASE）。
    /// </summary>
    /// <param name="value">待处理的字符串。</param>
    /// <returns>常量命名格式的字符串。</returns>
    public static string ToConstantCase(this string value)
    {
        if (string.IsNullOrEmpty(value)) return string.Empty;
        
        var words = GetWords(value);
        return string.Join("_", words.Select(w => w.ToUpper()));
    }

    private static string[] GetWords(string value)
    {
        if (string.IsNullOrEmpty(value)) return Array.Empty<string>();
        
        var result = new List<string>();
        var currentWord = new StringBuilder();
        
        foreach (var c in value)
        {
            if (char.IsUpper(c))
            {
                if (currentWord.Length > 0)
                {
                    result.Add(currentWord.ToString());
                    currentWord.Clear();
                }
                currentWord.Append(c);
            }
            else if (c == '_' || c == '-' || c == ' ')
            {
                if (currentWord.Length > 0)
                {
                    result.Add(currentWord.ToString());
                    currentWord.Clear();
                }
            }
            else
            {
                currentWord.Append(c);
            }
        }
        
        if (currentWord.Length > 0)
            result.Add(currentWord.ToString());
        
        return result.ToArray();
    }

    #endregion

    #region 分割与连接

    /// <summary>
    /// 将字符串按指定分隔符分割为数组。
    /// </summary>
    /// <param name="value">待分割的字符串。</param>
    /// <param name="separator">分隔符，默认为逗号。</param>
    /// <param name="options">分割选项。</param>
    /// <returns>分割后的字符串数组。</returns>
    public static string[] SplitSafe(this string value, string separator = ",", StringSplitOptions options = StringSplitOptions.None)
    {
        if (string.IsNullOrEmpty(value)) return Array.Empty<string>();
        return value.Split(new[] { separator }, options);
    }

    /// <summary>
    /// 将字符串按指定分隔符分割为数组。
    /// </summary>
    /// <param name="value">待分割的字符串。</param>
    /// <param name="separator">分隔符字符。</param>
    /// <param name="options">分割选项。</param>
    /// <returns>分割后的字符串数组。</returns>
    public static string[] SplitSafe(this string value, char separator, StringSplitOptions options = StringSplitOptions.None)
    {
        if (string.IsNullOrEmpty(value)) return Array.Empty<string>();
        return value.Split(separator, options);
    }

    /// <summary>
    /// 将字符串按指定分隔符分割并转换为整数数组。
    /// </summary>
    /// <param name="value">待分割的字符串。</param>
    /// <param name="separator">分隔符，默认为逗号。</param>
    /// <returns>分割后的整数数组。</returns>
    public static int[] SplitToIntArray(this string value, string separator = ",")
    {
        if (string.IsNullOrEmpty(value)) return Array.Empty<int>();
        return value.Split(new[] { separator }, StringSplitOptions.RemoveEmptyEntries)
                    .Select(s => s.Trim().ToInt())
                    .ToArray();
    }

    /// <summary>
    /// 将字符串按指定分隔符分割并转换为泛型数组。
    /// </summary>
    /// <typeparam name="T">目标类型。</typeparam>
    /// <param name="value">待分割的字符串。</param>
    /// <param name="separator">分隔符，默认为逗号。</param>
    /// <returns>分割后的泛型数组。</returns>
    public static T[] SplitToArray<T>(this string value, string separator = ",") where T : IConvertible
    {
        if (string.IsNullOrEmpty(value)) return Array.Empty<T>();
        return value.Split(new[] { separator }, StringSplitOptions.RemoveEmptyEntries)
                    .Select(s => s.Trim().To<T>())
                    .ToArray();
    }

    /// <summary>
    /// 使用指定分隔符连接字符串集合。
    /// </summary>
    /// <param name="values">字符串集合。</param>
    /// <param name="separator">分隔符，默认为逗号。</param>
    /// <returns>连接后的字符串。</returns>
    public static string JoinString(this IEnumerable<string> values, string separator = ",") =>
        values == null ? string.Empty : string.Join(separator, values);

    /// <summary>
    /// 使用指定分隔符连接对象集合（调用 ToString 方法）。
    /// </summary>
    /// <typeparam name="T">对象类型。</typeparam>
    /// <param name="values">对象集合。</param>
    /// <param name="separator">分隔符，默认为逗号。</param>
    /// <returns>连接后的字符串。</returns>
    public static string JoinString<T>(this IEnumerable<T> values, string separator = ",") =>
        values == null ? string.Empty : string.Join(separator, values);

    #endregion

    #region 其他实用方法

    /// <summary>
    /// 如果字符串为 null 或空，返回指定默认值。
    /// </summary>
    /// <param name="value">原字符串。</param>
    /// <param name="defaultValue">默认值。</param>
    /// <returns>原字符串或默认值。</returns>
    public static string IfNullOrEmpty(this string value, string defaultValue) =>
        string.IsNullOrEmpty(value) ? defaultValue : value;

    /// <summary>
    /// 如果字符串为 null 或空白，返回指定默认值。
    /// </summary>
    /// <param name="value">原字符串。</param>
    /// <param name="defaultValue">默认值。</param>
    /// <returns>原字符串或默认值。</returns>
    public static string IfNullOrWhiteSpace(this string value, string defaultValue) =>
        string.IsNullOrWhiteSpace(value) ? defaultValue : value;

    /// <summary>
    /// 获取字符串的字节长度（按指定编码计算）。
    /// </summary>
    /// <param name="value">待计算的字符串。</param>
    /// <param name="encoding">编码方式，默认为 UTF-8。</param>
    /// <returns>字节长度。</returns>
    public static int GetByteLength(this string value, Encoding encoding = null)
    {
        encoding ??= Encoding.UTF8;
        return string.IsNullOrEmpty(value) ? 0 : encoding.GetByteCount(value);
    }

    /// <summary>
    /// 判断字符串是否匹配指定通配符模式。
    /// </summary>
    /// <param name="value">待判断的字符串。</param>
    /// <param name="pattern">通配符模式（* 表示任意多个字符，? 表示单个字符）。</param>
    /// <returns>如果匹配，返回 true；否则返回 false。</returns>
    public static bool IsLike(this string value, string pattern)
    {
        if (string.IsNullOrEmpty(value) || string.IsNullOrEmpty(pattern)) return false;
        
        var regexPattern = "^" + Regex.Escape(pattern)
            .Replace("\\*", ".*")
            .Replace("\\?", ".") + "$";
        
        return Regex.IsMatch(value, regexPattern, RegexOptions.IgnoreCase);
    }

    /// <summary>
    /// 将字符串转换为安全的文件名（移除无效字符）。
    /// </summary>
    /// <param name="value">待处理的字符串。</param>
    /// <param name="replacement">替换字符，默认为下划线。</param>
    /// <returns>安全的文件名。</returns>
    public static string ToSafeFileName(this string value, char replacement = '_')
    {
        if (string.IsNullOrEmpty(value)) return string.Empty;
        
        var invalidChars = Path.GetInvalidFileNameChars();
        var result = new StringBuilder(value.Length);
        
        foreach (var c in value)
        {
            result.Append(invalidChars.Contains(c) ? replacement : c);
        }
        
        return result.ToString();
    }

    /// <summary>
    /// 将字符串转换为安全的 URL 路径（移除无效字符）。
    /// </summary>
    /// <param name="value">待处理的字符串。</param>
    /// <param name="replacement">替换字符，默认为短横线。</param>
    /// <returns>安全的 URL 路径。</returns>
    public static string ToSafeUrlPath(this string value, char replacement = '-')
    {
        if (string.IsNullOrEmpty(value)) return string.Empty;
        
        var result = new StringBuilder(value.Length);
        foreach (var c in value.Trim().ToLower())
        {
            if (char.IsLetterOrDigit(c))
                result.Append(c);
            else if (c == ' ' || c == '_')
                result.Append(replacement);
            else if (c == '-' || c == '.')
                result.Append(c);
        }
        
        return result.ToString();
    }

    #endregion
}
