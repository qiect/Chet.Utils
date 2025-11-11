using System.Text.RegularExpressions;

namespace Chet.Utils
{
    /// <summary>
    /// string 扩展方法类，提供常用的判断、正则表达式验证、类型转换和字符串操作方法。
    /// </summary>          
    public static class StringExtensions
    {
        #region 判断

        /// <summary>
        /// 判断字符串是否为 null 或空字符串。
        /// </summary>
        /// <param name="value">待判断的字符串。</param>
        public static bool IsNullOrEmpty(this string value) => string.IsNullOrEmpty(value);

        /// <summary>
        /// 判断字符串是否为 null 或仅包含空白字符。
        /// </summary>
        /// <param name="value">待判断的字符串。</param>
        public static bool IsNullOrWhiteSpace(this string value) => string.IsNullOrWhiteSpace(value);

        /// <summary>
        /// 判断字符串是否为数字（可解析为 double）。
        /// </summary>
        /// <param name="value">待判断的字符串。</param>
        public static bool IsNumeric(this string value) =>
            !string.IsNullOrWhiteSpace(value) && double.TryParse(value, out _);

        /// <summary>
        /// 判断字符串是否为整数。
        /// </summary>
        /// <param name="value">待判断的字符串。</param>
        public static bool IsInt(this string value) =>
            !string.IsNullOrWhiteSpace(value) && int.TryParse(value, out _);

        /// <summary>
        /// 判断字符串是否为浮点数（float）。
        /// </summary>
        /// <param name="value">待判断的字符串。</param>
        public static bool IsFloat(this string value) =>
            !string.IsNullOrWhiteSpace(value) && float.TryParse(value, out _);

        /// <summary>
        /// 判断字符串是否为十进制数（decimal）。
        /// </summary>
        /// <param name="value">待判断的字符串。</param>
        public static bool IsDecimal(this string value) =>
            !string.IsNullOrWhiteSpace(value) && decimal.TryParse(value, out _);

        /// <summary>
        /// 判断字符串是否为 Guid。
        /// </summary>
        /// <param name="value">待判断的字符串。</param>
        public static bool IsGuid(this string value) =>
            Guid.TryParse(value, out _);

        /// <summary>
        /// 忽略大小写判断字符串是否相等。
        /// </summary>
        /// <param name="value">原字符串。</param>
        /// <param name="compare">要比较的字符串。</param>
        public static bool EqualsIgnoreCase(this string value, string compare) =>
            string.Equals(value, compare, StringComparison.OrdinalIgnoreCase);

        /// <summary>
        /// 判断字符是否为中文字符
        /// </summary>
        /// <param name="value">要验证的字符</param>
        /// <returns>bool</returns>
        public static bool IsChinese(this string value)
        {
            return Regex.IsMatch(value, @"^[\u4e00-\u9fa5？，“”‘’。、；：]+$");
        }

        /// <summary>
        /// 判断字符串中是否包含中文
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool HasChinese(this string value)
        {
            return Regex.IsMatch(value, @"[\u4e00-\u9fa5]");
        }

        /// <summary>
        /// 判断字符串是否为空
        /// </summary>
        /// <param name="value">源字符串</param>
        /// <param name="nullStrings">自定义空字符串，中间“|”分隔</param>
        /// <param name="isTrim">是否移除收尾空白字符串，默认：false</param>
        /// <returns>bool</returns>
        public static bool IsNull(this string value, string nullStrings = "null|{}|[]", bool isTrim = false)
        {
            var result = true;
            if (value != null)
            {
                if (isTrim)
                    result = value.Trim() == "";
                else
                    result = value == "";
                //是否为自定义空字符串
                if (!result && !string.IsNullOrWhiteSpace(nullStrings))
                {
                    if (isTrim)
                        result = nullStrings.Split('|').Contains(value.Trim().ToLower());
                    else
                        result = nullStrings.Split('|').Contains(value.ToLower());
                }
            }
            return result;
        }

        #endregion

        #region 正则表达式验证
        /// <summary>
        /// 验证字符是否是字母类型[如果是true则是字母类型]
        /// </summary>
        /// <param name="value">字符串名</param>
        /// <returns>bool</returns>
        public static bool IsLetterByRegex(this string value)
        {
            return Regex.IsMatch(value, @"^[a-zA-Z]+$");
        }
        /// <summary>
        /// 验证字符是否是数字类型[如果是true则是数字类型]
        /// </summary>
        /// <param name="value">字符串名</param>
        /// <returns>bool</returns>
        public static bool IsNumByRegex(this string value)
        {
            return Regex.IsMatch(value, @"^\d+$");
        }
        /// <summary>
        /// 提取字符串中的数字部分
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static string ExtractNumByRegex(this string source)
        {
            return Regex.Replace(source, @"[^0-9]+", "");
        }
        /// <summary>
        /// 验证字符是不是浮点类型[如果是true则是浮点类型]
        /// </summary>
        /// <param name="value">字符串名</param>
        /// <returns>bool</returns>
        public static bool IsFloatByRegex(this string value)
        {
            return Regex.IsMatch(value, @"^\d*[.]{0,1}\d*$");
        }
        /// <summary>
        /// 验证字符是否是Email格式[如果是true则是Email格式]
        /// </summary>
        /// <param name="value">要验证的字符串</param>
        /// <returns>bool</returns>
        public static bool IsEmailByRegex(this string value)
        {
            return Regex.IsMatch(value, @"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*");
        }
        /// <summary>
        /// 验证字符是否是Tel格式[如果是true则是Tel格式]
        /// </summary>
        /// <param name="value">要验证的字符串</param>
        /// <returns>bool</returns>
        public static bool IsTelByRegex(this string value)
        {
            return Regex.IsMatch(value, @"^[0-9]{3,4}\-[0-9]{3,8}\-[0-9]{1,4}$|(^[0-9]{3,4}\-[0-9]{3,8}$)|(^[0-9]{3,8}$)|(^\([0-9]{3,4}\)[0-9]{3,8}$)");
        }
        /// <summary>
        /// 验证是否是手机[如果是true则是Tel格式]
        /// </summary>
        /// <param name="value">要验证的字符串</param>
        /// <returns>bool</returns>
        public static bool IsMobileByRegex(this string value)
        {
            return Regex.IsMatch(value, @"^1[34578]\d{9}$");
        }
        /// <summary>
        /// 验证是否是网址[如果是true则是网址格式]
        /// </summary>
        /// <param name="value">要验证的字符串</param>
        /// <returns>bool</returns>
        public static bool IsUrlByRegex(this string value)
        {
            return Regex.IsMatch(value, @"http(s)?://([\w-]+\.)+[\w-]+(/[\w- ./?%&=]*)?");
        }
        /// <summary>
        /// 验证字符串是否是日期类型[如果是true则是日期类型]
        /// </summary>
        /// <param name="value">要验证的字符串</param>
        /// <returns>bool</returns>
        public static bool IsDateByRegex(this string value)
        {
            return Regex.IsMatch(value, @"^(\d{2}|\d{4})(-|\/)(\d{1,2})\2(\d{1,2})$");
        }
        /// <summary>
        /// 验证字符串是否是时间类型[如果是true则是时间类型]
        /// </summary>
        /// <param name="value">要验证的字符串</param>
        /// <returns>bool</returns>
        public static bool IsTimeByRegex(this string value)
        {
            return Regex.IsMatch(value, @"^\d{1,2}\:\d{1,2}\:\d{1,2}$");
        }
        /// <summary>
        /// 验证字符串是否是日期时间类型[如果是true则是日期时间类型]
        /// </summary>
        /// <param name="value">要验证的字符串</param>
        /// <returns>bool</returns>
        public static bool IsDateTimeByRegex(this string value)
        {
            return Regex.IsMatch(value, @"^(\d{2}|\d{4})(-|\/)(\d{1,2})\2(\d{1,2})\s\d{1,2}\:\d{1,2}\:\d{1,2}$");
        }
        #endregion

        #region 类型转换

        /// <summary>
        /// 字符串转 int，失败返回默认值。
        /// </summary>
        /// <param name="value">待转换的字符串。</param>
        /// <param name="defaultValue">转换失败时的默认值。</param>
        public static int ToInt(this string value, int defaultValue = 0) =>
            int.TryParse(value, out var result) ? result : defaultValue;

        /// <summary>
        /// 字符串转 int，失败返回null。
        /// </summary>
        /// <param name="value">待转换的字符串。</param>
        public static int? ToInt(this string value) =>
            int.TryParse(value, out var result) ? result : null;

        /// <summary>
        /// 字符串转 float，失败返回默认值。
        /// </summary>
        /// <param name="value">待转换的字符串。</param>
        /// <param name="defaultValue">转换失败时的默认值。</param>
        public static float ToFloat(this string value, float defaultValue = 0) =>
            float.TryParse(value, out var result) ? result : defaultValue;

        /// <summary>
        /// 字符串转 float，失败返回null。
        /// </summary>
        /// <param name="value">待转换的字符串。</param>
        public static float? ToFloat(this string value) =>
            float.TryParse(value, out var result) ? result : null;

        /// <summary>
        /// 字符串转 float，保留指定小数位，四舍五入，失败返回默认值。
        /// </summary>
        /// <param name="value">待转换的字符串。</param>
        /// <param name="digits">保留的小数位数。</param>
        /// <param name="defaultValue">转换失败时的默认值。</param>
        public static float ToFloatRound(this string value, int digits = 2, float defaultValue = 0)
        {
            if (float.TryParse(value, out var result))
                return (float)Math.Round(result, digits, MidpointRounding.AwayFromZero);
            return defaultValue;
        }

        /// <summary>
        /// 字符串转 float，保留指定小数位，向零取整，失败返回默认值。
        /// </summary>
        /// <param name="value">待转换的字符串。</param>
        /// <param name="digits">保留的小数位数。</param>
        /// <param name="defaultValue">转换失败时的默认值。</param>
        public static float ToFloatTruncate(this string value, int digits = 2, float defaultValue = 0)
        {
            if (float.TryParse(value, out var result))
            {
                float factor = (float)Math.Pow(10, digits);
                return (float)(Math.Truncate(result * factor) / factor);
            }
            return defaultValue;
        }

        /// <summary>
        /// 字符串转 double，失败返回默认值。
        /// </summary>
        /// <param name="value">待转换的字符串。</param>
        /// <param name="defaultValue">转换失败时的默认值。</param>
        public static double ToDouble(this string value, double defaultValue = 0) =>
            double.TryParse(value, out var result) ? result : defaultValue;

        /// <summary>
        /// 字符串转 double，失败返回默认值。
        /// </summary>
        /// <param name="value">待转换的字符串。</param>
        public static double? ToDouble(this string value) =>
            double.TryParse(value, out var result) ? result : null;

        /// <summary>
        /// 字符串转 double，保留指定小数位，四舍五入，失败返回默认值。
        /// </summary>
        /// <param name="value">待转换的字符串。</param>
        /// <param name="digits">保留的小数位数。</param>
        /// <param name="defaultValue">转换失败时的默认值。</param>
        public static double ToDoubleRound(this string value, int digits = 2, double defaultValue = 0)
        {
            if (double.TryParse(value, out var result))
                return Math.Round(result, digits, MidpointRounding.AwayFromZero);
            return defaultValue;
        }

        /// <summary>
        /// 字符串转 double，保留指定小数位，向零取整，失败返回默认值。
        /// </summary>
        /// <param name="value">待转换的字符串。</param>
        /// <param name="digits">保留的小数位数。</param>
        /// <param name="defaultValue">转换失败时的默认值。</param>
        public static double ToDoubleTruncate(this string value, int digits = 2, double defaultValue = 0)
        {
            if (double.TryParse(value, out var result))
            {
                double factor = Math.Pow(10, digits);
                return Math.Truncate(result * factor) / factor;
            }
            return defaultValue;
        }

        /// <summary>
        /// 字符串转 decimal，失败返回默认值。
        /// </summary>
        /// <param name="value">待转换的字符串。</param>
        /// <param name="defaultValue">转换失败时的默认值。</param>
        public static decimal ToDecimal(this string value, decimal defaultValue = 0) =>
            decimal.TryParse(value, out var result) ? result : defaultValue;

        /// <summary>
        /// 字符串转 decimal，失败返回null。
        /// </summary>
        /// <param name="value">待转换的字符串。</param>
        public static decimal? ToDecimal(this string value) =>
            decimal.TryParse(value, out var result) ? result : null;

        /// <summary>
        /// 保留数值型字符串的小数位（默认两位），非数值型原样返回。
        /// </summary>
        /// <param name="value">待处理的字符串。</param>
        /// <param name="digits">保留的小数位数。</param>
        public static string KeepDecimal(this string value, int digits = 2)
        {
            if (double.TryParse(value, out var result))
                return Math.Round(result, digits, MidpointRounding.AwayFromZero).ToString($"F{digits}");
            return value;
        }

        /// <summary>
        /// 字符串转 bool，失败返回默认值。
        /// </summary>
        /// <param name="value">待转换的字符串。</param>
        /// <param name="defaultValue">转换失败时的默认值。</param>
        public static bool ToBool(this string value, bool defaultValue = false) =>
            bool.TryParse(value, out var result) ? result : defaultValue;

        /// <summary>
        /// 字符串转 bool，失败返回null。
        /// </summary>
        /// <param name="value">待转换的字符串。</param>
        public static bool? ToBool(this string value) =>
            bool.TryParse(value, out var result) ? result : null;

        /// <summary>
        /// 字符串转 Guid，失败返回默认值。
        /// </summary>
        /// <param name="value">待转换的字符串。</param>
        /// <param name="defaultValue">转换失败时的默认值。</param>
        public static Guid ToGuid(this string value, Guid? defaultValue = null) =>
            Guid.TryParse(value, out var result) ? result : (defaultValue ?? Guid.Empty);

        /// <summary>
        /// 字符串转 Guid，失败返回null。
        /// </summary>
        /// <param name="value">待转换的字符串。</param>
        public static Guid? ToGuid(this string value) =>
            Guid.TryParse(value, out var result) ? result : null;

        /// <summary>
        /// 字符串转 DateTime，失败返回默认值。
        /// </summary>
        /// <param name="value">待转换的字符串。</param>
        /// <param name="defaultValue">转换失败时的默认值。</param>
        public static DateTime ToDateTime(this string value, DateTime? defaultValue = null) =>
            DateTime.TryParse(value, out var result) ? result : (defaultValue ?? DateTime.MinValue);

        /// <summary>
        /// 字符串转 DateTime，失败返回默认值。
        /// </summary>
        /// <param name="value">待转换的字符串。</param>
        public static DateTime? ToDateTime(this string value) =>
            DateTime.TryParse(value, out var result) ? result : null;

        #endregion

        #region 字符串操作

        /// <summary>
        /// 安全去除字符串首尾空白字符，null 返回空字符串。
        /// </summary>
        /// <param name="value">待处理的字符串。</param>
        public static string TrimSafe(this string value) => value?.Trim() ?? string.Empty;

        /// <summary>
        /// 移除字符串中的所有空白字符。
        /// </summary>
        /// <param name="value">待处理的字符串。</param>
        public static string RemoveWhiteSpace(this string value) =>
            string.IsNullOrEmpty(value) ? string.Empty : string.Concat(value.Where(c => !char.IsWhiteSpace(c)));

        /// <summary>
        /// 安全截取字符串，超出范围返回空字符串。
        /// </summary>
        /// <param name="value">待处理的字符串。</param>
        /// <param name="startIndex">起始索引。</param>
        /// <param name="length">截取长度。</param>
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
        public static string Left(this string value, int length)
        {
            if (string.IsNullOrEmpty(value) || length <= 0) return string.Empty;
            return value.Length <= length ? value : value.Substring(0, length);
        }

        /// <summary>
        /// 获取字符串右侧指定长度的子串。
        /// </summary>
        /// <param name="value">待处理的字符串。</param>
        /// <param name="length">子串长度。</param>
        public static string Right(this string value, int length)
        {
            if (string.IsNullOrEmpty(value) || length <= 0) return string.Empty;
            return value.Length <= length ? value : value.Substring(value.Length - length, length);
        }

        /// <summary>
        /// 反转字符串。
        /// </summary>
        /// <param name="value">待处理的字符串。</param>
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
        public static string RemoveSpecialChars(this string value)
        {
            if (string.IsNullOrEmpty(value)) return string.Empty;
            return new string(value.Where(c => char.IsLetterOrDigit(c) || char.IsWhiteSpace(c)).ToArray());
        }

        /// <summary>
        /// 转为 camelCase（首字母小写）。
        /// </summary>
        /// <param name="value">待处理的字符串。</param>
        public static string ToCamelCase(this string value)
        {
            if (string.IsNullOrEmpty(value)) return string.Empty;
            if (value.Length == 1) return value.ToLower();
            return char.ToLower(value[0]) + value[1..];
        }

        /// <summary>
        /// 转为 PascalCase（首字母大写）。
        /// </summary>
        /// <param name="value">待处理的字符串。</param>
        public static string ToPascalCase(this string value)
        {
            if (string.IsNullOrEmpty(value)) return string.Empty;
            if (value.Length == 1) return value.ToUpper();
            return char.ToUpper(value[0]) + value[1..];
        }

        /// <summary>
        /// 重复字符串指定次数。
        /// </summary>
        /// <param name="value">待处理的字符串。</param>
        /// <param name="count">重复次数。</param>
        public static string Repeat(this string value, int count)
        {
            if (string.IsNullOrEmpty(value) || count <= 0) return string.Empty;
            return string.Concat(Enumerable.Repeat(value, count));
        }

        /// <summary>
        /// 忽略大小写替换字符串中的指定内容。
        /// </summary>
        /// <param name="value">原字符串。</param>
        /// <param name="oldValue">要替换的内容。</param>
        /// <param name="newValue">新内容。</param>
        public static string ReplaceIgnoreCase(this string value, string oldValue, string newValue)
        {
            if (string.IsNullOrEmpty(value) || string.IsNullOrEmpty(oldValue)) return value;
            return Regex.Replace(
                value, Regex.Escape(oldValue), newValue ?? string.Empty, RegexOptions.IgnoreCase);
        }

        /// <summary>
        /// 获取字符串的 MD5 值（32位小写）。
        /// </summary>
        /// <param name="value">待处理的字符串。</param>
        public static string ToMd5(this string value)
        {
            if (string.IsNullOrEmpty(value)) return string.Empty;
            using var md5 = System.Security.Cryptography.MD5.Create();
            var bytes = System.Text.Encoding.UTF8.GetBytes(value);
            var hash = md5.ComputeHash(bytes);
            return BitConverter.ToString(hash).Replace("-", "").ToLowerInvariant();
        }

        /// <summary>
        /// 判断字符串是否包含指定子串，忽略大小写。
        /// </summary>
        /// <param name="value">原字符串。</param>
        /// <param name="sub">要查找的子串。</param>
        public static bool ContainsIgnoreCase(this string value, string sub)
        {
            if (string.IsNullOrEmpty(value) || string.IsNullOrEmpty(sub)) return false;
            return value.IndexOf(sub, StringComparison.OrdinalIgnoreCase) >= 0;
        }

        /// <summary>
        /// 将字符串按指定分隔符分割为字符串数组，自动去除空项。
        /// </summary>
        /// <param name="value">待分割的字符串。</param>
        /// <param name="separators">分隔符数组。</param>
        public static string[] SplitSafe(this string value, params char[] separators)
        {
            if (string.IsNullOrEmpty(value)) return Array.Empty<string>();
            return value.Split(separators, StringSplitOptions.RemoveEmptyEntries);
        }
        #endregion
    }
}
