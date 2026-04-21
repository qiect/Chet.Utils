using System.Globalization;

namespace Chet.Utils
{
    /// <summary>
    /// decimal 扩展方法类，提供常用的判断、转换、运算、格式化等功能。
    /// </summary>
    /// <remarks>
    /// <para>本类提供丰富的 decimal 扩展方法，涵盖以下功能：</para>
    /// <list type="bullet">
    ///   <item><description>基础判断：IsZero、IsPositive、IsNegative、IsInteger、IsEven、IsOdd</description></item>
    ///   <item><description>范围判断：IsBetween、Clamp、IsInRange</description></item>
    ///   <item><description>类型转换：ToInt、ToLong、ToDouble、ToFloat、ToBool</description></item>
    ///   <item><description>数学运算：Round、Truncate、Abs、Pow、Sqrt、Ceiling、Floor</description></item>
    ///   <item><description>格式化输出：ToFixedString、ToCurrencyString、ToPercentString、ToScientificString</description></item>
    ///   <item><description>中文转换：ToChineseUpper、ToChineseNumber、ToChineseAmount</description></item>
    ///   <item><description>比较运算：Max、Min、AbsDiff、EqualsTolerance</description></item>
    /// </list>
    /// </remarks>
    public static class DecimalExtensions
    {
        #region 基础判断

        /// <summary>
        /// 判断 decimal 是否为零。
        /// </summary>
        /// <param name="value">待判断的 decimal。</param>
        /// <returns>如果值为零返回 true；否则返回 false。</returns>
        /// <example>
        /// <code>
        /// decimal value1 = 0m;
        /// decimal value2 = 123.45m;
        /// 
        /// bool result1 = value1.IsZero(); // true
        /// bool result2 = value2.IsZero(); // false
        /// </code>
        /// </example>
        public static bool IsZero(this decimal value) => value == 0m;

        /// <summary>
        /// 判断 decimal 是否为正数。
        /// </summary>
        /// <param name="value">待判断的 decimal。</param>
        /// <returns>如果值大于零返回 true；否则返回 false。</returns>
        /// <example>
        /// <code>
        /// decimal value1 = 123.45m;
        /// decimal value2 = -123.45m;
        /// 
        /// bool result1 = value1.IsPositive(); // true
        /// bool result2 = value2.IsPositive(); // false
        /// </code>
        /// </example>
        public static bool IsPositive(this decimal value) => value > 0m;

        /// <summary>
        /// 判断 decimal 是否为负数。
        /// </summary>
        /// <param name="value">待判断的 decimal。</param>
        /// <returns>如果值小于零返回 true；否则返回 false。</returns>
        /// <example>
        /// <code>
        /// decimal value1 = -123.45m;
        /// decimal value2 = 123.45m;
        /// 
        /// bool result1 = value1.IsNegative(); // true
        /// bool result2 = value2.IsNegative(); // false
        /// </code>
        /// </example>
        public static bool IsNegative(this decimal value) => value < 0m;

        /// <summary>
        /// 判断 decimal 是否为整数（无小数部分）。
        /// </summary>
        /// <param name="value">待判断的 decimal。</param>
        /// <returns>如果值为整数返回 true；否则返回 false。</returns>
        /// <example>
        /// <code>
        /// decimal value1 = 123m;
        /// decimal value2 = 123.45m;
        /// 
        /// bool result1 = value1.IsInteger(); // true
        /// bool result2 = value2.IsInteger(); // false
        /// </code>
        /// </example>
        public static bool IsInteger(this decimal value) => decimal.Truncate(value) == value;

        /// <summary>
        /// 判断 decimal 是否为偶数（仅整数时有效）。
        /// </summary>
        /// <param name="value">待判断的 decimal。</param>
        /// <returns>如果是偶数返回 true；否则返回 false。</returns>
        /// <example>
        /// <code>
        /// decimal value1 = 4m;
        /// decimal value2 = 5m;
        /// decimal value3 = 4.5m;
        /// 
        /// bool result1 = value1.IsEven(); // true
        /// bool result2 = value2.IsEven(); // false
        /// bool result3 = value3.IsEven(); // false（非整数）
        /// </code>
        /// </example>
        public static bool IsEven(this decimal value) => IsInteger(value) && ((long)value % 2 == 0);

        /// <summary>
        /// 判断 decimal 是否为奇数（仅整数时有效）。
        /// </summary>
        /// <param name="value">待判断的 decimal。</param>
        /// <returns>如果是奇数返回 true；否则返回 false。</returns>
        /// <example>
        /// <code>
        /// decimal value1 = 5m;
        /// decimal value2 = 4m;
        /// decimal value3 = 5.5m;
        /// 
        /// bool result1 = value1.IsOdd(); // true
        /// bool result2 = value2.IsOdd(); // false
        /// bool result3 = value3.IsOdd(); // false（非整数）
        /// </code>
        /// </example>
        public static bool IsOdd(this decimal value) => IsInteger(value) && ((long)value % 2 != 0);

        #endregion

        #region 范围判断

        /// <summary>
        /// 判断 decimal 是否在指定范围内（包含边界）。
        /// </summary>
        /// <param name="value">待判断的 decimal。</param>
        /// <param name="min">最小值。</param>
        /// <param name="max">最大值。</param>
        /// <returns>如果值在范围内返回 true；否则返回 false。</returns>
        /// <example>
        /// <code>
        /// decimal value = 50m;
        /// 
        /// bool result1 = value.IsBetween(0m, 100m); // true
        /// bool result2 = value.IsBetween(60m, 100m); // false
        /// </code>
        /// </example>
        public static bool IsBetween(this decimal value, decimal min, decimal max) =>
            value >= min && value <= max;

        /// <summary>
        /// 判断 decimal 是否在指定范围内（包含边界）。
        /// </summary>
        /// <param name="value">待判断的 decimal。</param>
        /// <param name="min">最小值。</param>
        /// <param name="max">最大值。</param>
        /// <returns>如果值在范围内返回 true；否则返回 false。</returns>
        public static bool IsInRange(this decimal value, decimal min, decimal max) =>
            value >= min && value <= max;

        /// <summary>
        /// 将 decimal 限制在指定范围内，超出则取边界值。
        /// </summary>
        /// <param name="value">待处理的 decimal。</param>
        /// <param name="min">最小值。</param>
        /// <param name="max">最大值。</param>
        /// <returns>限制后的值。</returns>
        /// <example>
        /// <code>
        /// decimal value1 = 150m;
        /// decimal value2 = 50m;
        /// decimal value3 = -10m;
        /// 
        /// decimal result1 = value1.Clamp(0m, 100m); // 100
        /// decimal result2 = value2.Clamp(0m, 100m); // 50
        /// decimal result3 = value3.Clamp(0m, 100m); // 0
        /// </code>
        /// </example>
        public static decimal Clamp(this decimal value, decimal min, decimal max) =>
            value < min ? min : (value > max ? max : value);

        #endregion

        #region 类型转换

        /// <summary>
        /// 将 decimal 转换为 int，四舍五入。
        /// </summary>
        /// <param name="value">待转换的 decimal。</param>
        /// <returns>转换后的 int 值。</returns>
        /// <example>
        /// <code>
        /// decimal value1 = 123.4m;
        /// decimal value2 = 123.5m;
        /// 
        /// int result1 = value1.ToInt(); // 123
        /// int result2 = value2.ToInt(); // 124
        /// </code>
        /// </example>
        public static int ToInt(this decimal value) => (int)Math.Round(value, 0, MidpointRounding.AwayFromZero);

        /// <summary>
        /// 将 decimal 转换为 long，四舍五入。
        /// </summary>
        /// <param name="value">待转换的 decimal。</param>
        /// <returns>转换后的 long 值。</returns>
        /// <example>
        /// <code>
        /// decimal value = 123456789.5m;
        /// long result = value.ToLong(); // 123456790
        /// </code>
        /// </example>
        public static long ToLong(this decimal value) => (long)Math.Round(value, 0, MidpointRounding.AwayFromZero);

        /// <summary>
        /// 将 decimal 转换为 double。
        /// </summary>
        /// <param name="value">待转换的 decimal。</param>
        /// <returns>转换后的 double 值。</returns>
        /// <example>
        /// <code>
        /// decimal value = 123.45m;
        /// double result = value.ToDouble(); // 123.45
        /// </code>
        /// </example>
        public static double ToDouble(this decimal value) => (double)value;

        /// <summary>
        /// 将 decimal 转换为 float。
        /// </summary>
        /// <param name="value">待转换的 decimal。</param>
        /// <returns>转换后的 float 值。</returns>
        /// <example>
        /// <code>
        /// decimal value = 123.45m;
        /// float result = value.ToFloat(); // 123.45f
        /// </code>
        /// </example>
        public static float ToFloat(this decimal value) => (float)value;

        /// <summary>
        /// 将 decimal 转换为 bool（非零为 true）。
        /// </summary>
        /// <param name="value">待转换的 decimal。</param>
        /// <returns>如果值非零返回 true；否则返回 false。</returns>
        /// <example>
        /// <code>
        /// decimal value1 = 0m;
        /// decimal value2 = 123.45m;
        /// 
        /// bool result1 = value1.ToBool(); // false
        /// bool result2 = value2.ToBool(); // true
        /// </code>
        /// </example>
        public static bool ToBool(this decimal value) => value != 0m;

        #endregion

        #region 数学运算

        /// <summary>
        /// 将 decimal 四舍五入到指定小数位。
        /// </summary>
        /// <param name="value">待处理的 decimal。</param>
        /// <param name="digits">保留的小数位数，默认为 2。</param>
        /// <returns>四舍五入后的值。</returns>
        /// <example>
        /// <code>
        /// decimal value = 123.4567m;
        /// 
        /// decimal result1 = value.Round(); // 123.46
        /// decimal result2 = value.Round(3); // 123.457
        /// </code>
        /// </example>
        public static decimal Round(this decimal value, int digits = 2) =>
            Math.Round(value, digits, MidpointRounding.AwayFromZero);

        /// <summary>
        /// 将 decimal 截断到指定小数位（向零取整）。
        /// </summary>
        /// <param name="value">待处理的 decimal。</param>
        /// <param name="digits">保留的小数位数，默认为 2。</param>
        /// <returns>截断后的值。</returns>
        /// <example>
        /// <code>
        /// decimal value = 123.4567m;
        /// 
        /// decimal result1 = value.Truncate(); // 123.45
        /// decimal result2 = value.Truncate(3); // 123.456
        /// </code>
        /// </example>
        public static decimal Truncate(this decimal value, int digits = 2)
        {
            decimal factor = (decimal)Math.Pow(10, digits);
            return Math.Truncate(value * factor) / factor;
        }

        /// <summary>
        /// 获取 decimal 的绝对值。
        /// </summary>
        /// <param name="value">待处理的 decimal。</param>
        /// <returns>绝对值。</returns>
        /// <example>
        /// <code>
        /// decimal value1 = -123.45m;
        /// decimal value2 = 123.45m;
        /// 
        /// decimal result1 = value1.Abs(); // 123.45
        /// decimal result2 = value2.Abs(); // 123.45
        /// </code>
        /// </example>
        public static decimal Abs(this decimal value) => Math.Abs(value);

        /// <summary>
        /// 计算 decimal 的幂次方。
        /// </summary>
        /// <param name="value">底数。</param>
        /// <param name="power">指数。</param>
        /// <returns>计算结果。</returns>
        /// <example>
        /// <code>
        /// decimal value = 2m;
        /// 
        /// decimal result1 = value.Pow(3); // 8
        /// decimal result2 = value.Pow(10); // 1024
        /// </code>
        /// </example>
        public static decimal Pow(this decimal value, int power) => (decimal)Math.Pow((double)value, power);

        /// <summary>
        /// 计算 decimal 的平方根。
        /// </summary>
        /// <param name="value">待处理的 decimal。</param>
        /// <returns>平方根。</returns>
        /// <example>
        /// <code>
        /// decimal value = 16m;
        /// decimal result = value.Sqrt(); // 4
        /// </code>
        /// </example>
        public static decimal Sqrt(this decimal value) => (decimal)Math.Sqrt((double)value);

        /// <summary>
        /// 向上取整（天花板函数）。
        /// </summary>
        /// <param name="value">待处理的 decimal。</param>
        /// <returns>向上取整后的值。</returns>
        /// <example>
        /// <code>
        /// decimal value1 = 123.1m;
        /// decimal value2 = 123.9m;
        /// 
        /// decimal result1 = value1.Ceiling(); // 124
        /// decimal result2 = value2.Ceiling(); // 124
        /// </code>
        /// </example>
        public static decimal Ceiling(this decimal value) => Math.Ceiling(value);

        /// <summary>
        /// 向下取整（地板函数）。
        /// </summary>
        /// <param name="value">待处理的 decimal。</param>
        /// <returns>向下取整后的值。</returns>
        /// <example>
        /// <code>
        /// decimal value1 = 123.1m;
        /// decimal value2 = 123.9m;
        /// 
        /// decimal result1 = value1.Floor(); // 123
        /// decimal result2 = value2.Floor(); // 123
        /// </code>
        /// </example>
        public static decimal Floor(this decimal value) => Math.Floor(value);

        /// <summary>
        /// 计算 decimal 的符号。
        /// </summary>
        /// <param name="value">待处理的 decimal。</param>
        /// <returns>如果值为正返回 1，为零返回 0，为负返回 -1。</returns>
        /// <example>
        /// <code>
        /// decimal value1 = 123.45m;
        /// decimal value2 = 0m;
        /// decimal value3 = -123.45m;
        /// 
        /// int result1 = value1.Sign(); // 1
        /// int result2 = value2.Sign(); // 0
        /// int result3 = value3.Sign(); // -1
        /// </code>
        /// </example>
        public static int Sign(this decimal value) => Math.Sign(value);

        #endregion

        #region 四则运算

        /// <summary>
        /// decimal 加法。
        /// </summary>
        /// <param name="value">第一个值。</param>
        /// <param name="other">第二个值。</param>
        /// <returns>两数之和。</returns>
        /// <example>
        /// <code>
        /// decimal value = 100m;
        /// decimal result = value.Add(50m); // 150
        /// </code>
        /// </example>
        public static decimal Add(this decimal value, decimal other) => value + other;

        /// <summary>
        /// decimal 减法。
        /// </summary>
        /// <param name="value">第一个值。</param>
        /// <param name="other">第二个值。</param>
        /// <returns>两数之差。</returns>
        /// <example>
        /// <code>
        /// decimal value = 100m;
        /// decimal result = value.Subtract(30m); // 70
        /// </code>
        /// </example>
        public static decimal Subtract(this decimal value, decimal other) => value - other;

        /// <summary>
        /// decimal 乘法。
        /// </summary>
        /// <param name="value">第一个值。</param>
        /// <param name="other">第二个值。</param>
        /// <returns>两数之积。</returns>
        /// <example>
        /// <code>
        /// decimal value = 100m;
        /// decimal result = value.Multiply(1.5m); // 150
        /// </code>
        /// </example>
        public static decimal Multiply(this decimal value, decimal other) => value * other;

        /// <summary>
        /// decimal 除法，除数为零时返回指定默认值。
        /// </summary>
        /// <param name="value">被除数。</param>
        /// <param name="other">除数。</param>
        /// <param name="defaultValue">除数为零时的默认返回值，默认为 0。</param>
        /// <returns>两数之商，或默认值。</returns>
        /// <example>
        /// <code>
        /// decimal value = 100m;
        /// 
        /// decimal result1 = value.DivideSafe(4m); // 25
        /// decimal result2 = value.DivideSafe(0m); // 0
        /// </code>
        /// </example>
        public static decimal DivideSafe(this decimal value, decimal other, decimal defaultValue = 0m) =>
            other == 0m ? defaultValue : value / other;

        /// <summary>
        /// decimal 求余。
        /// </summary>
        /// <param name="value">被除数。</param>
        /// <param name="other">除数。</param>
        /// <returns>余数，除数为零时返回 0。</returns>
        /// <example>
        /// <code>
        /// decimal value = 10m;
        /// decimal result = value.Mod(3m); // 1
        /// </code>
        /// </example>
        public static decimal Mod(this decimal value, decimal other) => other == 0m ? 0m : value % other;

        #endregion

        #region 比较运算

        /// <summary>
        /// 获取两个 decimal 中的较大值。
        /// </summary>
        /// <param name="value">第一个值。</param>
        /// <param name="other">第二个值。</param>
        /// <returns>较大的值。</returns>
        /// <example>
        /// <code>
        /// decimal value = 100m;
        /// decimal result = value.Max(200m); // 200
        /// </code>
        /// </example>
        public static decimal Max(this decimal value, decimal other) => Math.Max(value, other);

        /// <summary>
        /// 获取两个 decimal 中的较小值。
        /// </summary>
        /// <param name="value">第一个值。</param>
        /// <param name="other">第二个值。</param>
        /// <returns>较小的值。</returns>
        /// <example>
        /// <code>
        /// decimal value = 100m;
        /// decimal result = value.Min(200m); // 100
        /// </code>
        /// </example>
        public static decimal Min(this decimal value, decimal other) => Math.Min(value, other);

        /// <summary>
        /// 计算两个 decimal 的绝对差值。
        /// </summary>
        /// <param name="value">第一个值。</param>
        /// <param name="other">第二个值。</param>
        /// <returns>绝对差值。</returns>
        /// <example>
        /// <code>
        /// decimal value = 100m;
        /// decimal result = value.AbsDiff(130m); // 30
        /// </code>
        /// </example>
        public static decimal AbsDiff(this decimal value, decimal other) => Math.Abs(value - other);

        /// <summary>
        /// 判断两个 decimal 是否在指定精度范围内相等。
        /// </summary>
        /// <param name="value">第一个值。</param>
        /// <param name="other">第二个值。</param>
        /// <param name="tolerance">容差，默认为 0.0001。</param>
        /// <returns>如果差值小于容差返回 true；否则返回 false。</returns>
        /// <example>
        /// <code>
        /// decimal value1 = 1.00001m;
        /// decimal value2 = 1.00002m;
        /// 
        /// bool result = value1.EqualsTolerance(value2, 0.0001m); // true
        /// </code>
        /// </example>
        public static bool EqualsTolerance(this decimal value, decimal other, decimal tolerance = 0.0001m) =>
            Math.Abs(value - other) <= tolerance;

        #endregion

        #region 格式化输出

        /// <summary>
        /// 将 decimal 转换为固定小数位字符串。
        /// </summary>
        /// <param name="value">待处理的 decimal。</param>
        /// <param name="digits">保留的小数位数，默认为 2。</param>
        /// <returns>格式化后的字符串。</returns>
        /// <example>
        /// <code>
        /// decimal value = 123.4567m;
        /// 
        /// string result1 = value.ToFixedString(); // "123.46"
        /// string result2 = value.ToFixedString(3); // "123.457"
        /// </code>
        /// </example>
        public static string ToFixedString(this decimal value, int digits = 2) =>
            value.ToString($"F{digits}");

        /// <summary>
        /// 将 decimal 转换为货币格式字符串。
        /// </summary>
        /// <param name="value">待处理的 decimal。</param>
        /// <param name="culture">区域信息，默认为 "zh-CN"。</param>
        /// <returns>货币格式字符串。</returns>
        /// <example>
        /// <code>
        /// decimal value = 1234.56m;
        /// 
        /// string result1 = value.ToCurrencyString(); // "￥1,234.56"
        /// string result2 = value.ToCurrencyString("en-US"); // "$1,234.56"
        /// </code>
        /// </example>
        public static string ToCurrencyString(this decimal value, string culture = "zh-CN") =>
            value.ToString("C", new CultureInfo(culture));

        /// <summary>
        /// 将 decimal 转换为百分比字符串。
        /// </summary>
        /// <param name="value">待处理的 decimal（如 0.1234 表示 12.34%）。</param>
        /// <param name="digits">保留的小数位数，默认为 2。</param>
        /// <returns>百分比字符串。</returns>
        /// <example>
        /// <code>
        /// decimal value = 0.1234m;
        /// string result = value.ToPercentString(); // "12.34%"
        /// </code>
        /// </example>
        public static string ToPercentString(this decimal value, int digits = 2) =>
            (value * 100).ToString($"F{digits}") + "%";

        /// <summary>
        /// 将 decimal 转换为科学计数法字符串。
        /// </summary>
        /// <param name="value">待处理的 decimal。</param>
        /// <param name="digits">保留的小数位数，默认为 2。</param>
        /// <returns>科学计数法字符串。</returns>
        /// <example>
        /// <code>
        /// decimal value = 1234567.89m;
        /// string result = value.ToScientificString(); // "1.23E+006"
        /// </code>
        /// </example>
        public static string ToScientificString(this decimal value, int digits = 2) =>
            value.ToString($"E{digits}");

        /// <summary>
        /// 将 decimal 转换为友好字符串（如 "1.23万"、"1.23亿"）。
        /// </summary>
        /// <param name="value">待处理的 decimal。</param>
        /// <param name="digits">保留的小数位数，默认为 2。</param>
        /// <returns>友好格式字符串。</returns>
        /// <example>
        /// <code>
        /// decimal value1 = 12345m;
        /// decimal value2 = 123456789m;
        /// 
        /// string result1 = value1.ToFriendlyString(); // "1.23万"
        /// string result2 = value2.ToFriendlyString(); // "1.23亿"
        /// </code>
        /// </example>
        public static string ToFriendlyString(this decimal value, int digits = 2)
        {
            if (value >= 1_0000_0000m)
                return (value / 1_0000_0000m).ToString($"F{digits}") + "亿";
            if (value >= 1_0000m)
                return (value / 1_0000m).ToString($"F{digits}") + "万";
            return value.ToString($"F{digits}");
        }

        /// <summary>
        /// 将 decimal 转换为带千分位的字符串。
        /// </summary>
        /// <param name="value">待处理的 decimal。</param>
        /// <param name="digits">保留的小数位数，默认为 2。</param>
        /// <returns>带千分位的字符串。</returns>
        /// <example>
        /// <code>
        /// decimal value = 1234567.89m;
        /// string result = value.ToThousandsString(); // "1,234,567.89"
        /// </code>
        /// </example>
        public static string ToThousandsString(this decimal value, int digits = 2) =>
            value.ToString($"N{digits}");

        #endregion

        #region 中文转换

        /// <summary>
        /// 将 decimal 转换为中文大写金额。
        /// </summary>
        /// <param name="value">待处理的 decimal。</param>
        /// <returns>中文大写金额字符串。</returns>
        /// <example>
        /// <code>
        /// decimal value = 1234.56m;
        /// string result = value.ToChineseUpper(); // "壹仟贰佰叁拾肆元伍角陆分"
        /// </code>
        /// </example>
        public static string ToChineseUpper(this decimal value)
        {
            string[] cnNums = { "零", "壹", "贰", "叁", "肆", "伍", "陆", "柒", "捌", "玖" };
            string[] cnIntRadice = { "", "拾", "佰", "仟" };
            string[] cnIntUnits = { "", "万", "亿", "兆" };
            string[] cnDecUnits = { "角", "分" };
            string cnInteger = "整";
            string cnIntLast = "元";
            string maxNum = "999999999999999.99";

            if (value == 0) return cnNums[0] + cnIntLast + cnInteger;
            if (value > decimal.Parse(maxNum)) return "超出最大处理数";

            long integerPart = (long)Math.Floor(value);
            int decimalPart = (int)((value - integerPart) * 100);

            string intStr = integerPart.ToString();
            int intLen = intStr.Length;
            int groupCount = (intLen + 3) / 4;
            string result = "";
            bool needZero = false;

            for (int g = 0; g < groupCount; g++)
            {
                int groupStart = intLen - (g + 1) * 4;
                int groupLen = groupStart < 0 ? 4 + groupStart : 4;
                groupStart = Math.Max(0, groupStart);
                string group = intStr.Substring(groupStart, groupLen);
                int groupInt = int.Parse(group);
                string groupResult = "";
                bool localZero = false;

                for (int i = 0; i < group.Length; i++)
                {
                    int n = group[i] - '0';
                    int p = group.Length - i - 1;
                    if (n == 0)
                    {
                        localZero = true;
                    }
                    else
                    {
                        if (localZero || (needZero && groupResult.Length > 0))
                        {
                            groupResult += cnNums[0];
                        }
                        groupResult += cnNums[n] + cnIntRadice[p];
                        localZero = false;
                    }
                }

                if (groupInt != 0)
                {
                    groupResult += cnIntUnits[g];
                    result = groupResult + result;
                    needZero = true;
                }
                else
                {
                    if (result.Length > 0 && !result.StartsWith(cnNums[0]))
                        result = cnNums[0] + result;
                    needZero = false;
                }
            }

            while (result.Contains("零零")) result = result.Replace("零零", "零");
            result = result.TrimEnd('零');
            if (result == "") result = cnNums[0];
            result += cnIntLast;

            if (decimalPart > 0)
            {
                int jiao = decimalPart / 10;
                int fen = decimalPart % 10;
                if (jiao > 0) result += cnNums[jiao] + cnDecUnits[0];
                if (fen > 0) result += cnNums[fen] + cnDecUnits[1];
            }
            else
            {
                result += cnInteger;
            }

            result = result.Replace("零元", "元");
            if (result.StartsWith("零")) result = result.Substring(1);
            return result;
        }

        /// <summary>
        /// 将 decimal 转换为中文数字。
        /// </summary>
        /// <param name="value">待处理的 decimal（仅支持非负整数）。</param>
        /// <returns>中文数字字符串。</returns>
        /// <example>
        /// <code>
        /// decimal value = 1234m;
        /// string result = value.ToChineseNumber(); // "一千二百三十四"
        /// </code>
        /// </example>
        public static string ToChineseNumber(this decimal value)
        {
            if (value < 0) return "负" + ToChineseNumber(-value);
            if (!IsInteger(value)) return value.ToString();

            string[] cnNums = { "零", "一", "二", "三", "四", "五", "六", "七", "八", "九" };
            string[] cnUnits = { "", "十", "百", "千", "万", "十", "百", "千", "亿" };

            long num = (long)value;
            if (num == 0) return cnNums[0];

            string result = "";
            int unitIndex = 0;
            bool lastZero = false;

            while (num > 0)
            {
                int digit = (int)(num % 10);
                if (digit == 0)
                {
                    if (!lastZero && unitIndex > 0)
                    {
                        result = cnNums[0] + result;
                        lastZero = true;
                    }
                }
                else
                {
                    string unit = unitIndex < cnUnits.Length ? cnUnits[unitIndex] : "";
                    result = cnNums[digit] + unit + result;
                    lastZero = false;
                }
                num /= 10;
                unitIndex++;
            }

            result = result.Replace("一十", "十");
            while (result.Contains("零零")) result = result.Replace("零零", "零");
            result = result.TrimEnd('零');

            return result;
        }

        #endregion
    }
}
