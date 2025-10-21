using System.Text.RegularExpressions;

namespace Chet.Utils.Helpers
{
    /// <summary>
    /// 正则表达式帮助类，提供验证、匹配、替换、提取和格式化等多种功能
    /// </summary>
    public static class RegexHelper
    {
        #region 验证相关

        /// <summary>
        /// 验证是否为有效的电子邮件地址
        /// </summary>
        /// <param name="email">电子邮件地址</param>
        /// <returns>是否有效</returns>
        public static bool IsValidEmail(string email)
        {
            if (string.IsNullOrEmpty(email)) return false;
            string pattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
            return Regex.IsMatch(email, pattern);
        }

        /// <summary>
        /// 验证是否为有效的手机号码（中国）
        /// </summary>
        /// <param name="phone">手机号码</param>
        /// <returns>是否有效</returns>
        public static bool IsValidPhoneNumber(string phone)
        {
            if (string.IsNullOrEmpty(phone)) return false;
            string pattern = @"^1[3-9]\d{9}$";
            return Regex.IsMatch(phone, pattern);
        }

        /// <summary>
        /// 验证是否为有效的固定电话号码（中国）
        /// </summary>
        /// <param name="tel">固定电话号码</param>
        /// <returns>是否有效</returns>
        public static bool IsValidTelephoneNumber(string tel)
        {
            if (string.IsNullOrEmpty(tel)) return false;
            string pattern = @"^(\d{3,4}-?)?\d{7,8}(-\d{1,6})?$";
            return Regex.IsMatch(tel, pattern);
        }

        /// <summary>
        /// 验证是否为有效的身份证号码（中国18位）
        /// </summary>
        /// <param name="idCard">身份证号码</param>
        /// <returns>是否有效</returns>
        public static bool IsValidIdCard(string idCard)
        {
            if (string.IsNullOrEmpty(idCard)) return false;
            string pattern = @"^[1-9]\d{5}(18|19|20)\d{2}((0[1-9])|(1[0-2]))(([0-2][1-9])|10|20|30|31)\d{3}[0-9Xx]$";
            return Regex.IsMatch(idCard, pattern);
        }

        /// <summary>
        /// 验证是否为有效的邮政编码（中国）
        /// </summary>
        /// <param name="zipCode">邮政编码</param>
        /// <returns>是否有效</returns>
        public static bool IsValidZipCode(string zipCode)
        {
            if (string.IsNullOrEmpty(zipCode)) return false;
            string pattern = @"^[1-9]\d{5}$";
            return Regex.IsMatch(zipCode, pattern);
        }

        /// <summary>
        /// 验证是否为有效的IPv4地址
        /// </summary>
        /// <param name="ip">IP地址</param>
        /// <returns>是否有效</returns>
        public static bool IsValidIPv4(string ip)
        {
            if (string.IsNullOrEmpty(ip)) return false;
            string pattern = @"^((25[0-5]|2[0-4]\d|[01]?\d\d?)\.){3}(25[0-5]|2[0-4]\d|[01]?\d\d?)$";
            return Regex.IsMatch(ip, pattern);
        }

        /// <summary>
        /// 验证是否为有效的URL
        /// </summary>
        /// <param name="url">URL地址</param>
        /// <returns>是否有效</returns>
        public static bool IsValidUrl(string url)
        {
            if (string.IsNullOrEmpty(url)) return false;
            string pattern = @"^https?:\/\/([\w\-]+\.)+[\w\-]+(\/[\w\-\.\/\?\%\&\=]*)?$";
            return Regex.IsMatch(url, pattern);
        }

        /// <summary>
        /// 验证是否为有效的日期格式（yyyy-MM-dd）
        /// </summary>
        /// <param name="date">日期字符串</param>
        /// <returns>是否有效</returns>
        public static bool IsValidDate(string date)
        {
            if (string.IsNullOrEmpty(date)) return false;
            string pattern = @"^\d{4}-((0[1-9])|(1[0-2]))-((0[1-9])|([1-2][0-9])|(3[0-1]))$";
            return Regex.IsMatch(date, pattern);
        }

        /// <summary>
        /// 验证是否为有效的金额格式
        /// </summary>
        /// <param name="amount">金额字符串</param>
        /// <returns>是否有效</returns>
        public static bool IsValidAmount(string amount)
        {
            if (string.IsNullOrEmpty(amount)) return false;
            string pattern = @"^\d+(\.\d{1,2})?$";
            return Regex.IsMatch(amount, pattern);
        }

        /// <summary>
        /// 验证是否为有效的QQ号码
        /// </summary>
        /// <param name="qq">QQ号码</param>
        /// <returns>是否有效</returns>
        public static bool IsValidQQ(string qq)
        {
            if (string.IsNullOrEmpty(qq)) return false;
            string pattern = @"^[1-9][0-9]{4,10}$";
            return Regex.IsMatch(qq, pattern);
        }

        #endregion

        #region 匹配相关

        /// <summary>
        /// 匹配字符串中的所有数字
        /// </summary>
        /// <param name="input">输入字符串</param>
        /// <returns>数字集合</returns>
        public static List<string> MatchNumbers(string input)
        {
            if (string.IsNullOrEmpty(input)) return new List<string>();
            string pattern = @"\d+";
            MatchCollection matches = Regex.Matches(input, pattern);
            return matches.Cast<Match>().Select(m => m.Value).ToList();
        }

        /// <summary>
        /// 匹配字符串中的所有英文单词
        /// </summary>
        /// <param name="input">输入字符串</param>
        /// <returns>英文单词集合</returns>
        public static List<string> MatchEnglishWords(string input)
        {
            if (string.IsNullOrEmpty(input)) return new List<string>();
            string pattern = @"\b[a-zA-Z]+\b";
            MatchCollection matches = Regex.Matches(input, pattern);
            return matches.Cast<Match>().Select(m => m.Value).ToList();
        }

        /// <summary>
        /// 匹配字符串中的所有中文字符
        /// </summary>
        /// <param name="input">输入字符串</param>
        /// <returns>中文字符集合</returns>
        public static List<string> MatchChineseCharacters(string input)
        {
            if (string.IsNullOrEmpty(input)) return new List<string>();
            string pattern = @"[\u4e00-\u9fa5]+";
            MatchCollection matches = Regex.Matches(input, pattern);
            return matches.Cast<Match>().Select(m => m.Value).ToList();
        }

        /// <summary>
        /// 匹配字符串中的所有邮箱地址
        /// </summary>
        /// <param name="input">输入字符串</param>
        /// <returns>邮箱地址集合</returns>
        public static List<string> MatchEmails(string input)
        {
            if (string.IsNullOrEmpty(input)) return new List<string>();
            string pattern = @"[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}";
            MatchCollection matches = Regex.Matches(input, pattern);
            return matches.Cast<Match>().Select(m => m.Value).ToList();
        }

        /// <summary>
        /// 匹配字符串中的所有手机号码
        /// </summary>
        /// <param name="input">输入字符串</param>
        /// <returns>手机号码集合</returns>
        public static List<string> MatchPhoneNumbers(string input)
        {
            if (string.IsNullOrEmpty(input)) return new List<string>();
            string pattern = @"1[3-9]\d{9}";
            MatchCollection matches = Regex.Matches(input, pattern);
            return matches.Cast<Match>().Select(m => m.Value).ToList();
        }

        /// <summary>
        /// 匹配字符串中的所有URL链接
        /// </summary>
        /// <param name="input">输入字符串</param>
        /// <returns>URL链接集合</returns>
        public static List<string> MatchUrls(string input)
        {
            if (string.IsNullOrEmpty(input)) return new List<string>();
            string pattern = @"https?:\/\/([\w\-]+\.)+[\w\-]+(\/[\w\-\.\/\?\%\&\=]*)?";
            MatchCollection matches = Regex.Matches(input, pattern);
            return matches.Cast<Match>().Select(m => m.Value).ToList();
        }

        /// <summary>
        /// 匹配HTML标签
        /// </summary>
        /// <param name="input">输入字符串</param>
        /// <returns>HTML标签集合</returns>
        public static List<string> MatchHtmlTags(string input)
        {
            if (string.IsNullOrEmpty(input)) return new List<string>();
            string pattern = @"<[^>]+>";
            MatchCollection matches = Regex.Matches(input, pattern);
            return matches.Cast<Match>().Select(m => m.Value).ToList();
        }

        /// <summary>
        /// 匹配字符串中的所有IP地址
        /// </summary>
        /// <param name="input">输入字符串</param>
        /// <returns>IP地址集合</returns>
        public static List<string> MatchIPs(string input)
        {
            if (string.IsNullOrEmpty(input)) return new List<string>();
            string pattern = @"((25[0-5]|2[0-4]\d|[01]?\d\d?)\.){3}(25[0-5]|2[0-4]\d|[01]?\d\d?)";
            MatchCollection matches = Regex.Matches(input, pattern);
            return matches.Cast<Match>().Select(m => m.Value).ToList();
        }

        /// <summary>
        /// 匹配字符串中的所有颜色代码（#FFFFFF格式）
        /// </summary>
        /// <param name="input">输入字符串</param>
        /// <returns>颜色代码集合</returns>
        public static List<string> MatchHexColors(string input)
        {
            if (string.IsNullOrEmpty(input)) return new List<string>();
            string pattern = @"#([A-Fa-f0-9]{6}|[A-Fa-f0-9]{3})";
            MatchCollection matches = Regex.Matches(input, pattern);
            return matches.Cast<Match>().Select(m => m.Value).ToList();
        }

        /// <summary>
        /// 匹配字符串中的所有用户名（以@开头）
        /// </summary>
        /// <param name="input">输入字符串</param>
        /// <returns>用户名集合</returns>
        public static List<string> MatchUsernames(string input)
        {
            if (string.IsNullOrEmpty(input)) return new List<string>();
            string pattern = @"@[\w\u4e00-\u9fa5]+";
            MatchCollection matches = Regex.Matches(input, pattern);
            return matches.Cast<Match>().Select(m => m.Value).ToList();
        }

        #endregion

        #region 替换相关

        /// <summary>
        /// 隐藏手机号码中间4位
        /// </summary>
        /// <param name="phone">手机号码</param>
        /// <returns>隐藏后的手机号码</returns>
        public static string HidePhoneNumber(string phone)
        {
            if (string.IsNullOrEmpty(phone)) return phone;
            string pattern = @"(\d{3})\d{4}(\d{4})";
            return Regex.Replace(phone, pattern, "$1****$2");
        }

        /// <summary>
        /// 隐藏邮箱地址用户名部分
        /// </summary>
        /// <param name="email">邮箱地址</param>
        /// <returns>隐藏后的邮箱地址</returns>
        public static string HideEmail(string email)
        {
            if (string.IsNullOrEmpty(email)) return email;
            string pattern = @"(.{2}).*(@.*)";
            return Regex.Replace(email, pattern, "$1***$2");
        }

        /// <summary>
        /// 隐藏身份证号码中间部分
        /// </summary>
        /// <param name="idCard">身份证号码</param>
        /// <returns>隐藏后的身份证号码</returns>
        public static string HideIdCard(string idCard)
        {
            if (string.IsNullOrEmpty(idCard)) return idCard;
            string pattern = @"(\d{4})\d{10}(\w{4})";
            return Regex.Replace(idCard, pattern, "$1**********$2");
        }

        /// <summary>
        /// 移除字符串中的HTML标签
        /// </summary>
        /// <param name="input">包含HTML标签的字符串</param>
        /// <returns>移除HTML标签后的字符串</returns>
        public static string RemoveHtmlTags(string input)
        {
            if (string.IsNullOrEmpty(input)) return input;
            string pattern = @"<[^>]+>";
            return Regex.Replace(input, pattern, "");
        }

        /// <summary>
        /// 移除字符串中的所有数字
        /// </summary>
        /// <param name="input">输入字符串</param>
        /// <returns>移除数字后的字符串</returns>
        public static string RemoveNumbers(string input)
        {
            if (string.IsNullOrEmpty(input)) return input;
            string pattern = @"\d+";
            return Regex.Replace(input, pattern, "");
        }

        /// <summary>
        /// 移除字符串中的所有英文字符
        /// </summary>
        /// <param name="input">输入字符串</param>
        /// <returns>移除英文字符后的字符串</returns>
        public static string RemoveEnglish(string input)
        {
            if (string.IsNullOrEmpty(input)) return input;
            string pattern = @"[a-zA-Z]+";
            return Regex.Replace(input, pattern, "");
        }

        /// <summary>
        /// 移除字符串中的所有中文字符
        /// </summary>
        /// <param name="input">输入字符串</param>
        /// <returns>移除中文字符后的字符串</returns>
        public static string RemoveChinese(string input)
        {
            if (string.IsNullOrEmpty(input)) return input;
            string pattern = @"[\u4e00-\u9fa5]+";
            return Regex.Replace(input, pattern, "");
        }

        /// <summary>
        /// 将多个连续空格替换为单个空格
        /// </summary>
        /// <param name="input">输入字符串</param>
        /// <returns>处理后的字符串</returns>
        public static string ReplaceMultipleSpaces(string input)
        {
            if (string.IsNullOrEmpty(input)) return input;
            string pattern = @"\s+";
            return Regex.Replace(input, pattern, " ");
        }

        /// <summary>
        /// 统一换行符为指定格式
        /// </summary>
        /// <param name="input">输入字符串</param>
        /// <param name="newLine">新的换行符</param>
        /// <returns>处理后的字符串</returns>
        public static string NormalizeLineEndings(string input, string newLine = "\n")
        {
            if (string.IsNullOrEmpty(input)) return input;
            string pattern = @"\r\n|\r|\n";
            return Regex.Replace(input, pattern, newLine);
        }

        /// <summary>
        /// 移除字符串中的所有特殊字符（只保留字母、数字、中文和空格）
        /// </summary>
        /// <param name="input">输入字符串</param>
        /// <returns>移除特殊字符后的字符串</returns>
        public static string RemoveSpecialCharacters(string input)
        {
            if (string.IsNullOrEmpty(input)) return input;
            string pattern = @"[^\w\s\u4e00-\u9fa5]";
            return Regex.Replace(input, pattern, "");
        }

        #endregion

        #region 提取相关

        /// <summary>
        /// 提取字符串中的第一个数字
        /// </summary>
        /// <param name="input">输入字符串</param>
        /// <returns>第一个数字</returns>
        public static string ExtractFirstNumber(string input)
        {
            if (string.IsNullOrEmpty(input)) return string.Empty;
            string pattern = @"\d+";
            Match match = Regex.Match(input, pattern);
            return match.Success ? match.Value : string.Empty;
        }

        /// <summary>
        /// 提取字符串中的第一个邮箱地址
        /// </summary>
        /// <param name="input">输入字符串</param>
        /// <returns>第一个邮箱地址</returns>
        public static string ExtractFirstEmail(string input)
        {
            if (string.IsNullOrEmpty(input)) return string.Empty;
            string pattern = @"[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}";
            Match match = Regex.Match(input, pattern);
            return match.Success ? match.Value : string.Empty;
        }

        /// <summary>
        /// 提取字符串中的第一个URL
        /// </summary>
        /// <param name="input">输入字符串</param>
        /// <returns>第一个URL</returns>
        public static string ExtractFirstUrl(string input)
        {
            if (string.IsNullOrEmpty(input)) return string.Empty;
            string pattern = @"https?:\/\/([\w\-]+\.)+[\w\-]+(\/[\w\-\.\/\?\%\&\=]*)?";
            Match match = Regex.Match(input, pattern);
            return match.Success ? match.Value : string.Empty;
        }

        /// <summary>
        /// 提取HTML标签中的内容
        /// </summary>
        /// <param name="input">包含HTML标签的字符串</param>
        /// <returns>HTML标签中的内容</returns>
        public static string ExtractHtmlContent(string input)
        {
            if (string.IsNullOrEmpty(input)) return string.Empty;
            string pattern = @"<[^>]+>([^<]+)</[^>]+>";
            Match match = Regex.Match(input, pattern);
            return match.Success ? match.Groups[1].Value : string.Empty;
        }

        /// <summary>
        /// 提取字符串中的所有链接文本
        /// </summary>
        /// <param name="input">包含链接的字符串</param>
        /// <returns>链接文本集合</returns>
        public static List<string> ExtractLinkTexts(string input)
        {
            if (string.IsNullOrEmpty(input)) return new List<string>();
            string pattern = @"<a[^>]*>(.*?)</a>";
            MatchCollection matches = Regex.Matches(input, pattern);
            return matches.Cast<Match>().Select(m => m.Groups[1].Value).ToList();
        }

        /// <summary>
        /// 提取价格（匹配￥、$等货币符号后的数字）
        /// </summary>
        /// <param name="input">输入字符串</param>
        /// <returns>价格数字</returns>
        public static string ExtractPrice(string input)
        {
            if (string.IsNullOrEmpty(input)) return string.Empty;
            string pattern = @"[¥$€£]\s*(\d+(?:\.\d{2})?)";
            Match match = Regex.Match(input, pattern);
            return match.Success ? match.Groups[1].Value : string.Empty;
        }

        /// <summary>
        /// 提取文件扩展名
        /// </summary>
        /// <param name="fileName">文件名</param>
        /// <returns>文件扩展名</returns>
        public static string ExtractFileExtension(string fileName)
        {
            if (string.IsNullOrEmpty(fileName)) return string.Empty;
            string pattern = @"\.([^.]+)$";
            Match match = Regex.Match(fileName, pattern);
            return match.Success ? match.Groups[1].Value : string.Empty;
        }

        /// <summary>
        /// 提取域名
        /// </summary>
        /// <param name="url">URL地址</param>
        /// <returns>域名</returns>
        public static string ExtractDomain(string url)
        {
            if (string.IsNullOrEmpty(url)) return string.Empty;
            string pattern = @"^(?:https?:\/\/)?(?:www\.)?([^\/]+)";
            Match match = Regex.Match(url, pattern);
            return match.Success ? match.Groups[1].Value : string.Empty;
        }

        /// <summary>
        /// 提取查询参数
        /// </summary>
        /// <param name="url">URL地址</param>
        /// <param name="paramName">参数名</param>
        /// <returns>参数值</returns>
        public static string ExtractQueryParam(string url, string paramName)
        {
            if (string.IsNullOrEmpty(url) || string.IsNullOrEmpty(paramName)) return string.Empty;
            string pattern = $@"[?&]{paramName}=([^&]*)";
            Match match = Regex.Match(url, pattern);
            return match.Success ? match.Groups[1].Value : string.Empty;
        }

        /// <summary>
        /// 提取版本号
        /// </summary>
        /// <param name="input">输入字符串</param>
        /// <returns>版本号</returns>
        public static string ExtractVersion(string input)
        {
            if (string.IsNullOrEmpty(input)) return string.Empty;
            string pattern = @"\d+\.\d+\.\d+(?:\.\d+)?";
            Match match = Regex.Match(input, pattern);
            return match.Success ? match.Value : string.Empty;
        }

        #endregion

        #region 格式化相关

        /// <summary>
        /// 格式化手机号码（添加分隔符）
        /// </summary>
        /// <param name="phone">手机号码</param>
        /// <returns>格式化后的手机号码</returns>
        public static string FormatPhoneNumber(string phone)
        {
            if (string.IsNullOrEmpty(phone)) return phone;
            string pattern = @"(\d{3})(\d{4})(\d{4})";
            return Regex.Replace(phone, pattern, "$1-$2-$3");
        }

        /// <summary>
        /// 格式化银行卡号（每隔4位添加空格）
        /// </summary>
        /// <param name="cardNumber">银行卡号</param>
        /// <returns>格式化后的银行卡号</returns>
        public static string FormatBankCardNumber(string cardNumber)
        {
            if (string.IsNullOrEmpty(cardNumber)) return cardNumber;
            string pattern = @"(\d{4})(?=\d)";
            return Regex.Replace(cardNumber, pattern, "$1 ");
        }

        /// <summary>
        /// 格式化金额（添加千位分隔符）
        /// </summary>
        /// <param name="amount">金额</param>
        /// <returns>格式化后的金额</returns>
        public static string FormatAmount(string amount)
        {
            if (string.IsNullOrEmpty(amount)) return amount;
            string pattern = @"(\d)(?=(\d{3})+(?!\d))";
            return Regex.Replace(amount, pattern, "$1,");
        }

        /// <summary>
        /// 驼峰命名转下划线命名
        /// </summary>
        /// <param name="input">驼峰命名字符串</param>
        /// <returns>下划线命名字符串</returns>
        public static string CamelToUnderline(string input)
        {
            if (string.IsNullOrEmpty(input)) return input;
            string pattern = @"([a-z])([A-Z])";
            return Regex.Replace(input, pattern, "$1_$2").ToLower();
        }

        /// <summary>
        /// 下划线命名转驼峰命名
        /// </summary>
        /// <param name="input">下划线命名字符串</param>
        /// <returns>驼峰命名字符串</returns>
        public static string UnderlineToCamel(string input)
        {
            if (string.IsNullOrEmpty(input)) return input;
            string pattern = @"_([a-z])";
            return Regex.Replace(input, pattern, m => m.Groups[1].Value.ToUpper());
        }

        /// <summary>
        /// 首字母大写
        /// </summary>
        /// <param name="input">输入字符串</param>
        /// <returns>首字母大写的字符串</returns>
        public static string CapitalizeFirstLetter(string input)
        {
            if (string.IsNullOrEmpty(input)) return input;
            string pattern = @"^\w";
            return Regex.Replace(input, pattern, m => m.Value.ToUpper());
        }

        /// <summary>
        /// 单词首字母大写
        /// </summary>
        /// <param name="input">输入字符串</param>
        /// <returns>每个单词首字母大写的字符串</returns>
        public static string CapitalizeWords(string input)
        {
            if (string.IsNullOrEmpty(input)) return input;
            string pattern = @"\b\w";
            return Regex.Replace(input, pattern, m => m.Value.ToUpper());
        }

        /// <summary>
        /// 格式化日期（将yyyyMMdd格式化为yyyy-MM-dd）
        /// </summary>
        /// <param name="date">日期字符串</param>
        /// <returns>格式化后的日期</returns>
        public static string FormatDate(string date)
        {
            if (string.IsNullOrEmpty(date) || date.Length != 8) return date;
            string pattern = @"(\d{4})(\d{2})(\d{2})";
            return Regex.Replace(date, pattern, "$1-$2-$3");
        }

        /// <summary>
        /// 格式化时间（将HHmmss格式化为HH:mm:ss）
        /// </summary>
        /// <param name="time">时间字符串</param>
        /// <returns>格式化后的时间</returns>
        public static string FormatTime(string time)
        {
            if (string.IsNullOrEmpty(time) || time.Length != 6) return time;
            string pattern = @"(\d{2})(\d{2})(\d{2})";
            return Regex.Replace(time, pattern, "$1:$2:$3");
        }

        /// <summary>
        /// 清理字符串（移除多余空格和换行符）
        /// </summary>
        /// <param name="input">输入字符串</param>
        /// <returns>清理后的字符串</returns>
        public static string CleanString(string input)
        {
            if (string.IsNullOrEmpty(input)) return input;
            // 先统一换行符
            input = NormalizeLineEndings(input, " ");
            // 再将多个空格替换为单个空格
            input = ReplaceMultipleSpaces(input);
            // 最后去除首尾空格
            return input.Trim();
        }

        #endregion
    }
}