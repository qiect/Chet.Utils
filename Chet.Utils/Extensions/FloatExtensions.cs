using System.Globalization;

namespace Chet.Utils
{
    /// <summary>
    /// float 扩展方法类，提供常用的判断、转换、运算、格式化等功能。
    /// </summary>
    /// <remarks>
    /// <para>本类提供丰富的 float 扩展方法，涵盖以下功能：</para>
    /// <list type="bullet">
    ///   <item><description>基础判断：IsZero、IsPositive、IsNegative、IsInteger、IsEven、IsOdd</description></item>
    ///   <item><description>特殊值判断：IsNaN、IsInfinity、IsPositiveInfinity、IsNegativeInfinity</description></item>
    ///   <item><description>范围判断：IsBetween、Clamp、IsInRange</description></item>
    ///   <item><description>类型转换：ToInt、ToLong、ToDecimal、ToDouble、ToBool</description></item>
    ///   <item><description>数学运算：Round、Truncate、Abs、Pow、Sqrt、Ceiling、Floor</description></item>
    ///   <item><description>格式化输出：ToFixedString、ToCurrencyString、ToPercentString、ToScientificString</description></item>
    ///   <item><description>进制转换：ToHexString、ToBinaryString、ToOctalString</description></item>
    ///   <item><description>比较运算：Max、Min、AbsDiff、EqualsTolerance</description></item>
    /// </list>
    /// </remarks>
    public static class FloatExtensions
    {
        #region 基础判断

        /// <summary>
        /// 判断 float 是否为零。
        /// </summary>
        /// <param name="value">待判断的 float。</param>
        /// <returns>如果值为零返回 true；否则返回 false。</returns>
        /// <example>
        /// <code>
        /// float value1 = 0f;
        /// float value2 = 123.45f;
        /// 
        /// bool result1 = value1.IsZero(); // true
        /// bool result2 = value2.IsZero(); // false
        /// </code>
        /// </example>
        public static bool IsZero(this float value) => value == 0f;

        /// <summary>
        /// 判断 float 是否为正数。
        /// </summary>
        /// <param name="value">待判断的 float。</param>
        /// <returns>如果值大于零返回 true；否则返回 false。</returns>
        /// <example>
        /// <code>
        /// float value1 = 123.45f;
        /// float value2 = -123.45f;
        /// 
        /// bool result1 = value1.IsPositive(); // true
        /// bool result2 = value2.IsPositive(); // false
        /// </code>
        /// </example>
        public static bool IsPositive(this float value) => value > 0f;

        /// <summary>
        /// 判断 float 是否为负数。
        /// </summary>
        /// <param name="value">待判断的 float。</param>
        /// <returns>如果值小于零返回 true；否则返回 false。</returns>
        /// <example>
        /// <code>
        /// float value1 = -123.45f;
        /// float value2 = 123.45f;
        /// 
        /// bool result1 = value1.IsNegative(); // true
        /// bool result2 = value2.IsNegative(); // false
        /// </code>
        /// </example>
        public static bool IsNegative(this float value) => value < 0f;

        /// <summary>
        /// 判断 float 是否为整数（无小数部分）。
        /// </summary>
        /// <param name="value">待判断的 float。</param>
        /// <returns>如果值为整数返回 true；否则返回 false。</returns>
        /// <example>
        /// <code>
        /// float value1 = 123f;
        /// float value2 = 123.45f;
        /// 
        /// bool result1 = value1.IsInteger(); // true
        /// bool result2 = value2.IsInteger(); // false
        /// </code>
        /// </example>
        public static bool IsInteger(this float value) => MathF.Truncate(value) == value;

        /// <summary>
        /// 判断 float 是否为偶数（仅整数时有效）。
        /// </summary>
        /// <param name="value">待判断的 float。</param>
        /// <returns>如果是偶数返回 true；否则返回 false。</returns>
        /// <example>
        /// <code>
        /// float value1 = 4f;
        /// float value2 = 5f;
        /// float value3 = 4.5f;
        /// 
        /// bool result1 = value1.IsEven(); // true
        /// bool result2 = value2.IsEven(); // false
        /// bool result3 = value3.IsEven(); // false（非整数）
        /// </code>
        /// </example>
        public static bool IsEven(this float value) => IsInteger(value) && ((long)value % 2 == 0);

        /// <summary>
        /// 判断 float 是否为奇数（仅整数时有效）。
        /// </summary>
        /// <param name="value">待判断的 float。</param>
        /// <returns>如果是奇数返回 true；否则返回 false。</returns>
        /// <example>
        /// <code>
        /// float value1 = 5f;
        /// float value2 = 4f;
        /// float value3 = 5.5f;
        /// 
        /// bool result1 = value1.IsOdd(); // true
        /// bool result2 = value2.IsOdd(); // false
        /// bool result3 = value3.IsOdd(); // false（非整数）
        /// </code>
        /// </example>
        public static bool IsOdd(this float value) => IsInteger(value) && ((long)value % 2 != 0);

        #endregion

        #region 特殊值判断

        /// <summary>
        /// 判断 float 是否为 NaN（非数字）。
        /// </summary>
        /// <param name="value">待判断的 float。</param>
        /// <returns>如果值为 NaN 返回 true；否则返回 false。</returns>
        /// <example>
        /// <code>
        /// float value1 = float.NaN;
        /// float value2 = 123.45f;
        /// 
        /// bool result1 = value1.IsNaN(); // true
        /// bool result2 = value2.IsNaN(); // false
        /// </code>
        /// </example>
        public static bool IsNaN(this float value) => float.IsNaN(value);

        /// <summary>
        /// 判断 float 是否为无穷大（正无穷或负无穷）。
        /// </summary>
        /// <param name="value">待判断的 float。</param>
        /// <returns>如果值为无穷大返回 true；否则返回 false。</returns>
        /// <example>
        /// <code>
        /// float value1 = float.PositiveInfinity;
        /// float value2 = 123.45f;
        /// 
        /// bool result1 = value1.IsInfinity(); // true
        /// bool result2 = value2.IsInfinity(); // false
        /// </code>
        /// </example>
        public static bool IsInfinity(this float value) => float.IsInfinity(value);

        /// <summary>
        /// 判断 float 是否为正无穷。
        /// </summary>
        /// <param name="value">待判断的 float。</param>
        /// <returns>如果值为正无穷返回 true；否则返回 false。</returns>
        /// <example>
        /// <code>
        /// float value1 = float.PositiveInfinity;
        /// float value2 = float.NegativeInfinity;
        /// 
        /// bool result1 = value1.IsPositiveInfinity(); // true
        /// bool result2 = value2.IsPositiveInfinity(); // false
        /// </code>
        /// </example>
        public static bool IsPositiveInfinity(this float value) => float.IsPositiveInfinity(value);

        /// <summary>
        /// 判断 float 是否为负无穷。
        /// </summary>
        /// <param name="value">待判断的 float。</param>
        /// <returns>如果值为负无穷返回 true；否则返回 false。</returns>
        /// <example>
        /// <code>
        /// float value1 = float.NegativeInfinity;
        /// float value2 = float.PositiveInfinity;
        /// 
        /// bool result1 = value1.IsNegativeInfinity(); // true
        /// bool result2 = value2.IsNegativeInfinity(); // false
        /// </code>
        /// </example>
        public static bool IsNegativeInfinity(this float value) => float.IsNegativeInfinity(value);

        /// <summary>
        /// 判断 float 是否为有效数值（非 NaN 且非无穷大）。
        /// </summary>
        /// <param name="value">待判断的 float。</param>
        /// <returns>如果值为有效数值返回 true；否则返回 false。</returns>
        /// <example>
        /// <code>
        /// float value1 = 123.45f;
        /// float value2 = float.NaN;
        /// float value3 = float.PositiveInfinity;
        /// 
        /// bool result1 = value1.IsValid(); // true
        /// bool result2 = value2.IsValid(); // false
        /// bool result3 = value3.IsValid(); // false
        /// </code>
        /// </example>
        public static bool IsValid(this float value) => !float.IsNaN(value) && !float.IsInfinity(value);

        #endregion

        #region 范围判断

        /// <summary>
        /// 判断 float 是否在指定范围内（包含边界）。
        /// </summary>
        /// <param name="value">待判断的 float。</param>
        /// <param name="min">最小值。</param>
        /// <param name="max">最大值。</param>
        /// <returns>如果值在范围内返回 true；否则返回 false。</returns>
        /// <example>
        /// <code>
        /// float value = 50f;
        /// 
        /// bool result1 = value.IsBetween(0f, 100f); // true
        /// bool result2 = value.IsBetween(60f, 100f); // false
        /// </code>
        /// </example>
        public static bool IsBetween(this float value, float min, float max) =>
            value >= min && value <= max;

        /// <summary>
        /// 判断 float 是否在指定范围内（包含边界）。
        /// </summary>
        /// <param name="value">待判断的 float。</param>
        /// <param name="min">最小值。</param>
        /// <param name="max">最大值。</param>
        /// <returns>如果值在范围内返回 true；否则返回 false。</returns>
        public static bool IsInRange(this float value, float min, float max) =>
            value >= min && value <= max;

        /// <summary>
        /// 将 float 限制在指定范围内，超出则取边界值。
        /// </summary>
        /// <param name="value">待处理的 float。</param>
        /// <param name="min">最小值。</param>
        /// <param name="max">最大值。</param>
        /// <returns>限制后的值。</returns>
        /// <example>
        /// <code>
        /// float value1 = 150f;
        /// float value2 = 50f;
        /// float value3 = -10f;
        /// 
        /// float result1 = value1.Clamp(0f, 100f); // 100
        /// float result2 = value2.Clamp(0f, 100f); // 50
        /// float result3 = value3.Clamp(0f, 100f); // 0
        /// </code>
        /// </example>
        public static float Clamp(this float value, float min, float max) =>
            value < min ? min : (value > max ? max : value);

        #endregion

        #region 类型转换

        /// <summary>
        /// 将 float 转换为 int，四舍五入。
        /// </summary>
        /// <param name="value">待转换的 float。</param>
        /// <returns>转换后的 int 值。</returns>
        /// <example>
        /// <code>
        /// float value1 = 123.4f;
        /// float value2 = 123.5f;
        /// 
        /// int result1 = value1.ToInt(); // 123
        /// int result2 = value2.ToInt(); // 124
        /// </code>
        /// </example>
        public static int ToInt(this float value) => (int)Math.Round(value, 0, MidpointRounding.AwayFromZero);

        /// <summary>
        /// 将 float 转换为 long，四舍五入。
        /// </summary>
        /// <param name="value">待转换的 float。</param>
        /// <returns>转换后的 long 值。</returns>
        /// <example>
        /// <code>
        /// float value = 123456789.5f;
        /// long result = value.ToLong(); // 123456790
        /// </code>
        /// </example>
        public static long ToLong(this float value) => (long)Math.Round(value, 0, MidpointRounding.AwayFromZero);

        /// <summary>
        /// 将 float 转换为 decimal。
        /// </summary>
        /// <param name="value">待转换的 float。</param>
        /// <returns>转换后的 decimal 值。</returns>
        /// <example>
        /// <code>
        /// float value = 123.45f;
        /// decimal result = value.ToDecimal(); // 123.45m
        /// </code>
        /// </example>
        public static decimal ToDecimal(this float value) => (decimal)value;

        /// <summary>
        /// 将 float 转换为 double。
        /// </summary>
        /// <param name="value">待转换的 float。</param>
        /// <returns>转换后的 double 值。</returns>
        /// <example>
        /// <code>
        /// float value = 123.45f;
        /// double result = value.ToDouble(); // 123.45d
        /// </code>
        /// </example>
        public static double ToDouble(this float value) => (double)value;

        /// <summary>
        /// 将 float 转换为 bool（非零为 true）。
        /// </summary>
        /// <param name="value">待转换的 float。</param>
        /// <returns>如果值非零返回 true；否则返回 false。</returns>
        /// <example>
        /// <code>
        /// float value1 = 0f;
        /// float value2 = 123.45f;
        /// 
        /// bool result1 = value1.ToBool(); // false
        /// bool result2 = value2.ToBool(); // true
        /// </code>
        /// </example>
        public static bool ToBool(this float value) => value != 0f;

        #endregion

        #region 数学运算

        /// <summary>
        /// 将 float 四舍五入到指定小数位。
        /// </summary>
        /// <param name="value">待处理的 float。</param>
        /// <param name="digits">保留的小数位数，默认为 2。</param>
        /// <returns>四舍五入后的值。</returns>
        /// <example>
        /// <code>
        /// float value = 123.4567f;
        /// 
        /// float result1 = value.Round(); // 123.46
        /// float result2 = value.Round(3); // 123.457
        /// </code>
        /// </example>
        public static float Round(this float value, int digits = 2) =>
            (float)Math.Round(value, digits, MidpointRounding.AwayFromZero);

        /// <summary>
        /// 将 float 截断到指定小数位（向零取整）。
        /// </summary>
        /// <param name="value">待处理的 float。</param>
        /// <param name="digits">保留的小数位数，默认为 2。</param>
        /// <returns>截断后的值。</returns>
        /// <example>
        /// <code>
        /// float value = 123.4567f;
        /// 
        /// float result1 = value.Truncate(); // 123.45
        /// float result2 = value.Truncate(3); // 123.456
        /// </code>
        /// </example>
        public static float Truncate(this float value, int digits = 2)
        {
            float factor = (float)Math.Pow(10, digits);
            return (float)(Math.Truncate(value * factor) / factor);
        }

        /// <summary>
        /// 获取 float 的绝对值。
        /// </summary>
        /// <param name="value">待处理的 float。</param>
        /// <returns>绝对值。</returns>
        /// <example>
        /// <code>
        /// float value1 = -123.45f;
        /// float value2 = 123.45f;
        /// 
        /// float result1 = value1.Abs(); // 123.45
        /// float result2 = value2.Abs(); // 123.45
        /// </code>
        /// </example>
        public static float Abs(this float value) => MathF.Abs(value);

        /// <summary>
        /// 计算 float 的幂次方。
        /// </summary>
        /// <param name="value">底数。</param>
        /// <param name="power">指数。</param>
        /// <returns>计算结果。</returns>
        /// <example>
        /// <code>
        /// float value = 2f;
        /// 
        /// float result1 = value.Pow(3); // 8
        /// float result2 = value.Pow(10); // 1024
        /// </code>
        /// </example>
        public static float Pow(this float value, float power) => (float)Math.Pow(value, power);

        /// <summary>
        /// 计算 float 的平方根。
        /// </summary>
        /// <param name="value">待处理的 float。</param>
        /// <returns>平方根。</returns>
        /// <example>
        /// <code>
        /// float value = 16f;
        /// float result = value.Sqrt(); // 4
        /// </code>
        /// </example>
        public static float Sqrt(this float value) => MathF.Sqrt(value);

        /// <summary>
        /// 向上取整（天花板函数）。
        /// </summary>
        /// <param name="value">待处理的 float。</param>
        /// <returns>向上取整后的值。</returns>
        /// <example>
        /// <code>
        /// float value1 = 123.1f;
        /// float value2 = 123.9f;
        /// 
        /// float result1 = value1.Ceiling(); // 124
        /// float result2 = value2.Ceiling(); // 124
        /// </code>
        /// </example>
        public static float Ceiling(this float value) => MathF.Ceiling(value);

        /// <summary>
        /// 向下取整（地板函数）。
        /// </summary>
        /// <param name="value">待处理的 float。</param>
        /// <returns>向下取整后的值。</returns>
        /// <example>
        /// <code>
        /// float value1 = 123.1f;
        /// float value2 = 123.9f;
        /// 
        /// float result1 = value1.Floor(); // 123
        /// float result2 = value2.Floor(); // 123
        /// </code>
        /// </example>
        public static float Floor(this float value) => MathF.Floor(value);

        /// <summary>
        /// 计算 float 的符号。
        /// </summary>
        /// <param name="value">待处理的 float。</param>
        /// <returns>如果值为正返回 1，为零返回 0，为负返回 -1。</returns>
        /// <example>
        /// <code>
        /// float value1 = 123.45f;
        /// float value2 = 0f;
        /// float value3 = -123.45f;
        /// 
        /// int result1 = value1.Sign(); // 1
        /// int result2 = value2.Sign(); // 0
        /// int result3 = value3.Sign(); // -1
        /// </code>
        /// </example>
        public static int Sign(this float value) => Math.Sign(value);

        #endregion

        #region 四则运算

        /// <summary>
        /// float 加法。
        /// </summary>
        /// <param name="value">第一个值。</param>
        /// <param name="other">第二个值。</param>
        /// <returns>两数之和。</returns>
        /// <example>
        /// <code>
        /// float value = 100f;
        /// float result = value.Add(50f); // 150
        /// </code>
        /// </example>
        public static float Add(this float value, float other) => value + other;

        /// <summary>
        /// float 减法。
        /// </summary>
        /// <param name="value">第一个值。</param>
        /// <param name="other">第二个值。</param>
        /// <returns>两数之差。</returns>
        /// <example>
        /// <code>
        /// float value = 100f;
        /// float result = value.Subtract(30f); // 70
        /// </code>
        /// </example>
        public static float Subtract(this float value, float other) => value - other;

        /// <summary>
        /// float 乘法。
        /// </summary>
        /// <param name="value">第一个值。</param>
        /// <param name="other">第二个值。</param>
        /// <returns>两数之积。</returns>
        /// <example>
        /// <code>
        /// float value = 100f;
        /// float result = value.Multiply(1.5f); // 150
        /// </code>
        /// </example>
        public static float Multiply(this float value, float other) => value * other;

        /// <summary>
        /// float 除法，除数为零时返回指定默认值。
        /// </summary>
        /// <param name="value">被除数。</param>
        /// <param name="other">除数。</param>
        /// <param name="defaultValue">除数为零时的默认返回值，默认为 0。</param>
        /// <returns>两数之商，或默认值。</returns>
        /// <example>
        /// <code>
        /// float value = 100f;
        /// 
        /// float result1 = value.DivideSafe(4f); // 25
        /// float result2 = value.DivideSafe(0f); // 0
        /// </code>
        /// </example>
        public static float DivideSafe(this float value, float other, float defaultValue = 0f) =>
            other == 0f ? defaultValue : value / other;

        /// <summary>
        /// float 求余。
        /// </summary>
        /// <param name="value">被除数。</param>
        /// <param name="other">除数。</param>
        /// <returns>余数，除数为零时返回 0。</returns>
        /// <example>
        /// <code>
        /// float value = 10f;
        /// float result = value.Mod(3f); // 1
        /// </code>
        /// </example>
        public static float Mod(this float value, float other) => other == 0f ? 0f : value % other;

        #endregion

        #region 比较运算

        /// <summary>
        /// 获取两个 float 中的较大值。
        /// </summary>
        /// <param name="value">第一个值。</param>
        /// <param name="other">第二个值。</param>
        /// <returns>较大的值。</returns>
        /// <example>
        /// <code>
        /// float value = 100f;
        /// float result = value.Max(200f); // 200
        /// </code>
        /// </example>
        public static float Max(this float value, float other) => MathF.Max(value, other);

        /// <summary>
        /// 获取两个 float 中的较小值。
        /// </summary>
        /// <param name="value">第一个值。</param>
        /// <param name="other">第二个值。</param>
        /// <returns>较小的值。</returns>
        /// <example>
        /// <code>
        /// float value = 100f;
        /// float result = value.Min(200f); // 100
        /// </code>
        /// </example>
        public static float Min(this float value, float other) => MathF.Min(value, other);

        /// <summary>
        /// 计算两个 float 的绝对差值。
        /// </summary>
        /// <param name="value">第一个值。</param>
        /// <param name="other">第二个值。</param>
        /// <returns>绝对差值。</returns>
        /// <example>
        /// <code>
        /// float value = 100f;
        /// float result = value.AbsDiff(130f); // 30
        /// </code>
        /// </example>
        public static float AbsDiff(this float value, float other) => MathF.Abs(value - other);

        /// <summary>
        /// 判断两个 float 是否在指定精度范围内相等。
        /// </summary>
        /// <param name="value">第一个值。</param>
        /// <param name="other">第二个值。</param>
        /// <param name="tolerance">容差，默认为 0.0001。</param>
        /// <returns>如果差值小于容差返回 true；否则返回 false。</returns>
        /// <example>
        /// <code>
        /// float value1 = 1.00001f;
        /// float value2 = 1.00002f;
        /// 
        /// bool result = value1.EqualsTolerance(value2, 0.0001f); // true
        /// </code>
        /// </example>
        public static bool EqualsTolerance(this float value, float other, float tolerance = 0.0001f) =>
            MathF.Abs(value - other) <= tolerance;

        #endregion

        #region 格式化输出

        /// <summary>
        /// 将 float 转换为固定小数位字符串。
        /// </summary>
        /// <param name="value">待处理的 float。</param>
        /// <param name="digits">保留的小数位数，默认为 2。</param>
        /// <returns>格式化后的字符串。</returns>
        /// <example>
        /// <code>
        /// float value = 123.4567f;
        /// 
        /// string result1 = value.ToFixedString(); // "123.46"
        /// string result2 = value.ToFixedString(3); // "123.457"
        /// </code>
        /// </example>
        public static string ToFixedString(this float value, int digits = 2) =>
            value.ToString($"F{digits}");

        /// <summary>
        /// 将 float 转换为货币格式字符串。
        /// </summary>
        /// <param name="value">待处理的 float。</param>
        /// <param name="culture">区域信息，默认为 "zh-CN"。</param>
        /// <returns>货币格式字符串。</returns>
        /// <example>
        /// <code>
        /// float value = 1234.56f;
        /// 
        /// string result1 = value.ToCurrencyString(); // "￥1,234.56"
        /// string result2 = value.ToCurrencyString("en-US"); // "$1,234.56"
        /// </code>
        /// </example>
        public static string ToCurrencyString(this float value, string culture = "zh-CN") =>
            value.ToString("C", new CultureInfo(culture));

        /// <summary>
        /// 将 float 转换为百分比字符串。
        /// </summary>
        /// <param name="value">待处理的 float（如 0.1234 表示 12.34%）。</param>
        /// <param name="digits">保留的小数位数，默认为 2。</param>
        /// <returns>百分比字符串。</returns>
        /// <example>
        /// <code>
        /// float value = 0.1234f;
        /// string result = value.ToPercentString(); // "12.34%"
        /// </code>
        /// </example>
        public static string ToPercentString(this float value, int digits = 2) =>
            (value * 100).ToString($"F{digits}") + "%";

        /// <summary>
        /// 将 float 转换为科学计数法字符串。
        /// </summary>
        /// <param name="value">待处理的 float。</param>
        /// <param name="digits">保留的小数位数，默认为 2。</param>
        /// <returns>科学计数法字符串。</returns>
        /// <example>
        /// <code>
        /// float value = 1234567.89f;
        /// string result = value.ToScientificString(); // "1.23E+006"
        /// </code>
        /// </example>
        public static string ToScientificString(this float value, int digits = 2) =>
            value.ToString($"E{digits}");

        /// <summary>
        /// 将 float 转换为友好字符串（如 "1.23万"、"1.23亿"）。
        /// </summary>
        /// <param name="value">待处理的 float。</param>
        /// <param name="digits">保留的小数位数，默认为 2。</param>
        /// <returns>友好格式字符串。</returns>
        /// <example>
        /// <code>
        /// float value1 = 12345f;
        /// float value2 = 123456789f;
        /// 
        /// string result1 = value1.ToFriendlyString(); // "1.23万"
        /// string result2 = value2.ToFriendlyString(); // "1.23亿"
        /// </code>
        /// </example>
        public static string ToFriendlyString(this float value, int digits = 2)
        {
            if (value >= 1_0000_0000f)
                return (value / 1_0000_0000f).ToString($"F{digits}") + "亿";
            if (value >= 1_0000f)
                return (value / 1_0000f).ToString($"F{digits}") + "万";
            return value.ToString($"F{digits}");
        }

        /// <summary>
        /// 将 float 转换为带千分位的字符串。
        /// </summary>
        /// <param name="value">待处理的 float。</param>
        /// <param name="digits">保留的小数位数，默认为 2。</param>
        /// <returns>带千分位的字符串。</returns>
        /// <example>
        /// <code>
        /// float value = 1234567.89f;
        /// string result = value.ToThousandsString(); // "1,234,567.89"
        /// </code>
        /// </example>
        public static string ToThousandsString(this float value, int digits = 2) =>
            value.ToString($"N{digits}");

        #endregion

        #region 进制转换

        /// <summary>
        /// 将 float 的整数部分转换为十六进制字符串。
        /// </summary>
        /// <param name="value">待转换的 float。</param>
        /// <returns>十六进制字符串。</returns>
        /// <example>
        /// <code>
        /// float value = 255f;
        /// string result = value.ToHexString(); // "FF"
        /// </code>
        /// </example>
        public static string ToHexString(this float value) => ((long)value).ToString("X");

        /// <summary>
        /// 将 float 的整数部分转换为二进制字符串。
        /// </summary>
        /// <param name="value">待转换的 float。</param>
        /// <returns>二进制字符串。</returns>
        /// <example>
        /// <code>
        /// float value = 10f;
        /// string result = value.ToBinaryString(); // "1010"
        /// </code>
        /// </example>
        public static string ToBinaryString(this float value) => Convert.ToString((long)value, 2);

        /// <summary>
        /// 将 float 的整数部分转换为八进制字符串。
        /// </summary>
        /// <param name="value">待转换的 float。</param>
        /// <returns>八进制字符串。</returns>
        /// <example>
        /// <code>
        /// float value = 64f;
        /// string result = value.ToOctalString(); // "100"
        /// </code>
        /// </example>
        public static string ToOctalString(this float value) => Convert.ToString((long)value, 8);

        #endregion
    }
}
