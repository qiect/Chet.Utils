using System.Globalization;

namespace Chet.Utils
{
    /// <summary>
    /// double 扩展方法类，提供常用的判断、转换、运算、格式化等功能。
    /// </summary>
    /// <remarks>
    /// <para>本类提供丰富的 double 扩展方法，涵盖以下功能：</para>
    /// <list type="bullet">
    ///   <item><description>基础判断：IsZero、IsPositive、IsNegative、IsInteger、IsEven、IsOdd</description></item>
    ///   <item><description>特殊值判断：IsNaN、IsInfinity、IsPositiveInfinity、IsNegativeInfinity</description></item>
    ///   <item><description>范围判断：IsBetween、Clamp、IsInRange</description></item>
    ///   <item><description>类型转换：ToInt、ToLong、ToDecimal、ToFloat、ToBool</description></item>
    ///   <item><description>数学运算：Round、Truncate、Abs、Pow、Sqrt、Ceiling、Floor、Log、Exp</description></item>
    ///   <item><description>格式化输出：ToFixedString、ToCurrencyString、ToPercentString、ToScientificString</description></item>
    ///   <item><description>进制转换：ToHexString、ToBinaryString、ToOctalString</description></item>
    ///   <item><description>比较运算：Max、Min、AbsDiff、EqualsTolerance</description></item>
    /// </list>
    /// </remarks>
    public static class DoubleExtensions
    {
        #region 基础判断

        /// <summary>
        /// 判断 double 是否为零。
        /// </summary>
        /// <param name="value">待判断的 double。</param>
        /// <returns>如果值为零返回 true；否则返回 false。</returns>
        /// <example>
        /// <code>
        /// double value1 = 0d;
        /// double value2 = 123.45d;
        /// 
        /// bool result1 = value1.IsZero(); // true
        /// bool result2 = value2.IsZero(); // false
        /// </code>
        /// </example>
        public static bool IsZero(this double value) => value == 0d;

        /// <summary>
        /// 判断 double 是否为正数。
        /// </summary>
        /// <param name="value">待判断的 double。</param>
        /// <returns>如果值大于零返回 true；否则返回 false。</returns>
        /// <example>
        /// <code>
        /// double value1 = 123.45d;
        /// double value2 = -123.45d;
        /// 
        /// bool result1 = value1.IsPositive(); // true
        /// bool result2 = value2.IsPositive(); // false
        /// </code>
        /// </example>
        public static bool IsPositive(this double value) => value > 0d;

        /// <summary>
        /// 判断 double 是否为负数。
        /// </summary>
        /// <param name="value">待判断的 double。</param>
        /// <returns>如果值小于零返回 true；否则返回 false。</returns>
        /// <example>
        /// <code>
        /// double value1 = -123.45d;
        /// double value2 = 123.45d;
        /// 
        /// bool result1 = value1.IsNegative(); // true
        /// bool result2 = value2.IsNegative(); // false
        /// </code>
        /// </example>
        public static bool IsNegative(this double value) => value < 0d;

        /// <summary>
        /// 判断 double 是否为整数（无小数部分）。
        /// </summary>
        /// <param name="value">待判断的 double。</param>
        /// <returns>如果值为整数返回 true；否则返回 false。</returns>
        /// <example>
        /// <code>
        /// double value1 = 123d;
        /// double value2 = 123.45d;
        /// 
        /// bool result1 = value1.IsInteger(); // true
        /// bool result2 = value2.IsInteger(); // false
        /// </code>
        /// </example>
        public static bool IsInteger(this double value) => Math.Truncate(value) == value;

        /// <summary>
        /// 判断 double 是否为偶数（仅整数时有效）。
        /// </summary>
        /// <param name="value">待判断的 double。</param>
        /// <returns>如果是偶数返回 true；否则返回 false。</returns>
        /// <example>
        /// <code>
        /// double value1 = 4d;
        /// double value2 = 5d;
        /// double value3 = 4.5d;
        /// 
        /// bool result1 = value1.IsEven(); // true
        /// bool result2 = value2.IsEven(); // false
        /// bool result3 = value3.IsEven(); // false（非整数）
        /// </code>
        /// </example>
        public static bool IsEven(this double value) => IsInteger(value) && ((long)value % 2 == 0);

        /// <summary>
        /// 判断 double 是否为奇数（仅整数时有效）。
        /// </summary>
        /// <param name="value">待判断的 double。</param>
        /// <returns>如果是奇数返回 true；否则返回 false。</returns>
        /// <example>
        /// <code>
        /// double value1 = 5d;
        /// double value2 = 4d;
        /// double value3 = 5.5d;
        /// 
        /// bool result1 = value1.IsOdd(); // true
        /// bool result2 = value2.IsOdd(); // false
        /// bool result3 = value3.IsOdd(); // false（非整数）
        /// </code>
        /// </example>
        public static bool IsOdd(this double value) => IsInteger(value) && ((long)value % 2 != 0);

        #endregion

        #region 特殊值判断

        /// <summary>
        /// 判断 double 是否为 NaN（非数字）。
        /// </summary>
        /// <param name="value">待判断的 double。</param>
        /// <returns>如果值为 NaN 返回 true；否则返回 false。</returns>
        /// <example>
        /// <code>
        /// double value1 = double.NaN;
        /// double value2 = 123.45d;
        /// 
        /// bool result1 = value1.IsNaN(); // true
        /// bool result2 = value2.IsNaN(); // false
        /// </code>
        /// </example>
        public static bool IsNaN(this double value) => double.IsNaN(value);

        /// <summary>
        /// 判断 double 是否为无穷大（正无穷或负无穷）。
        /// </summary>
        /// <param name="value">待判断的 double。</param>
        /// <returns>如果值为无穷大返回 true；否则返回 false。</returns>
        /// <example>
        /// <code>
        /// double value1 = double.PositiveInfinity;
        /// double value2 = 123.45d;
        /// 
        /// bool result1 = value1.IsInfinity(); // true
        /// bool result2 = value2.IsInfinity(); // false
        /// </code>
        /// </example>
        public static bool IsInfinity(this double value) => double.IsInfinity(value);

        /// <summary>
        /// 判断 double 是否为正无穷。
        /// </summary>
        /// <param name="value">待判断的 double。</param>
        /// <returns>如果值为正无穷返回 true；否则返回 false。</returns>
        /// <example>
        /// <code>
        /// double value1 = double.PositiveInfinity;
        /// double value2 = double.NegativeInfinity;
        /// 
        /// bool result1 = value1.IsPositiveInfinity(); // true
        /// bool result2 = value2.IsPositiveInfinity(); // false
        /// </code>
        /// </example>
        public static bool IsPositiveInfinity(this double value) => double.IsPositiveInfinity(value);

        /// <summary>
        /// 判断 double 是否为负无穷。
        /// </summary>
        /// <param name="value">待判断的 double。</param>
        /// <returns>如果值为负无穷返回 true；否则返回 false。</returns>
        /// <example>
        /// <code>
        /// double value1 = double.NegativeInfinity;
        /// double value2 = double.PositiveInfinity;
        /// 
        /// bool result1 = value1.IsNegativeInfinity(); // true
        /// bool result2 = value2.IsNegativeInfinity(); // false
        /// </code>
        /// </example>
        public static bool IsNegativeInfinity(this double value) => double.IsNegativeInfinity(value);

        /// <summary>
        /// 判断 double 是否为有效数值（非 NaN 且非无穷大）。
        /// </summary>
        /// <param name="value">待判断的 double。</param>
        /// <returns>如果值为有效数值返回 true；否则返回 false。</returns>
        /// <example>
        /// <code>
        /// double value1 = 123.45d;
        /// double value2 = double.NaN;
        /// double value3 = double.PositiveInfinity;
        /// 
        /// bool result1 = value1.IsValid(); // true
        /// bool result2 = value2.IsValid(); // false
        /// bool result3 = value3.IsValid(); // false
        /// </code>
        /// </example>
        public static bool IsValid(this double value) => !double.IsNaN(value) && !double.IsInfinity(value);

        #endregion

        #region 范围判断

        /// <summary>
        /// 判断 double 是否在指定范围内（包含边界）。
        /// </summary>
        /// <param name="value">待判断的 double。</param>
        /// <param name="min">最小值。</param>
        /// <param name="max">最大值。</param>
        /// <returns>如果值在范围内返回 true；否则返回 false。</returns>
        /// <example>
        /// <code>
        /// double value = 50d;
        /// 
        /// bool result1 = value.IsBetween(0d, 100d); // true
        /// bool result2 = value.IsBetween(60d, 100d); // false
        /// </code>
        /// </example>
        public static bool IsBetween(this double value, double min, double max) =>
            value >= min && value <= max;

        /// <summary>
        /// 判断 double 是否在指定范围内（包含边界）。
        /// </summary>
        /// <param name="value">待判断的 double。</param>
        /// <param name="min">最小值。</param>
        /// <param name="max">最大值。</param>
        /// <returns>如果值在范围内返回 true；否则返回 false。</returns>
        public static bool IsInRange(this double value, double min, double max) =>
            value >= min && value <= max;

        /// <summary>
        /// 将 double 限制在指定范围内，超出则取边界值。
        /// </summary>
        /// <param name="value">待处理的 double。</param>
        /// <param name="min">最小值。</param>
        /// <param name="max">最大值。</param>
        /// <returns>限制后的值。</returns>
        /// <example>
        /// <code>
        /// double value1 = 150d;
        /// double value2 = 50d;
        /// double value3 = -10d;
        /// 
        /// double result1 = value1.Clamp(0d, 100d); // 100
        /// double result2 = value2.Clamp(0d, 100d); // 50
        /// double result3 = value3.Clamp(0d, 100d); // 0
        /// </code>
        /// </example>
        public static double Clamp(this double value, double min, double max) =>
            value < min ? min : (value > max ? max : value);

        #endregion

        #region 类型转换

        /// <summary>
        /// 将 double 转换为 int，四舍五入。
        /// </summary>
        /// <param name="value">待转换的 double。</param>
        /// <returns>转换后的 int 值。</returns>
        /// <example>
        /// <code>
        /// double value1 = 123.4d;
        /// double value2 = 123.5d;
        /// 
        /// int result1 = value1.ToInt(); // 123
        /// int result2 = value2.ToInt(); // 124
        /// </code>
        /// </example>
        public static int ToInt(this double value) => (int)Math.Round(value, 0, MidpointRounding.AwayFromZero);

        /// <summary>
        /// 将 double 转换为 long，四舍五入。
        /// </summary>
        /// <param name="value">待转换的 double。</param>
        /// <returns>转换后的 long 值。</returns>
        /// <example>
        /// <code>
        /// double value = 123456789.5d;
        /// long result = value.ToLong(); // 123456790
        /// </code>
        /// </example>
        public static long ToLong(this double value) => (long)Math.Round(value, 0, MidpointRounding.AwayFromZero);

        /// <summary>
        /// 将 double 转换为 decimal。
        /// </summary>
        /// <param name="value">待转换的 double。</param>
        /// <returns>转换后的 decimal 值。</returns>
        /// <example>
        /// <code>
        /// double value = 123.45d;
        /// decimal result = value.ToDecimal(); // 123.45m
        /// </code>
        /// </example>
        public static decimal ToDecimal(this double value) => (decimal)value;

        /// <summary>
        /// 将 double 转换为 float。
        /// </summary>
        /// <param name="value">待转换的 double。</param>
        /// <returns>转换后的 float 值。</returns>
        /// <example>
        /// <code>
        /// double value = 123.45d;
        /// float result = value.ToFloat(); // 123.45f
        /// </code>
        /// </example>
        public static float ToFloat(this double value) => (float)value;

        /// <summary>
        /// 将 double 转换为 bool（非零为 true）。
        /// </summary>
        /// <param name="value">待转换的 double。</param>
        /// <returns>如果值非零返回 true；否则返回 false。</returns>
        /// <example>
        /// <code>
        /// double value1 = 0d;
        /// double value2 = 123.45d;
        /// 
        /// bool result1 = value1.ToBool(); // false
        /// bool result2 = value2.ToBool(); // true
        /// </code>
        /// </example>
        public static bool ToBool(this double value) => value != 0d;

        #endregion

        #region 数学运算

        /// <summary>
        /// 将 double 四舍五入到指定小数位。
        /// </summary>
        /// <param name="value">待处理的 double。</param>
        /// <param name="digits">保留的小数位数，默认为 2。</param>
        /// <returns>四舍五入后的值。</returns>
        /// <example>
        /// <code>
        /// double value = 123.4567d;
        /// 
        /// double result1 = value.Round(); // 123.46
        /// double result2 = value.Round(3); // 123.457
        /// </code>
        /// </example>
        public static double Round(this double value, int digits = 2) =>
            Math.Round(value, digits, MidpointRounding.AwayFromZero);

        /// <summary>
        /// 将 double 截断到指定小数位（向零取整）。
        /// </summary>
        /// <param name="value">待处理的 double。</param>
        /// <param name="digits">保留的小数位数，默认为 2。</param>
        /// <returns>截断后的值。</returns>
        /// <example>
        /// <code>
        /// double value = 123.4567d;
        /// 
        /// double result1 = value.Truncate(); // 123.45
        /// double result2 = value.Truncate(3); // 123.456
        /// </code>
        /// </example>
        public static double Truncate(this double value, int digits = 2)
        {
            double factor = Math.Pow(10, digits);
            return Math.Truncate(value * factor) / factor;
        }

        /// <summary>
        /// 获取 double 的绝对值。
        /// </summary>
        /// <param name="value">待处理的 double。</param>
        /// <returns>绝对值。</returns>
        /// <example>
        /// <code>
        /// double value1 = -123.45d;
        /// double value2 = 123.45d;
        /// 
        /// double result1 = value1.Abs(); // 123.45
        /// double result2 = value2.Abs(); // 123.45
        /// </code>
        /// </example>
        public static double Abs(this double value) => Math.Abs(value);

        /// <summary>
        /// 计算 double 的幂次方。
        /// </summary>
        /// <param name="value">底数。</param>
        /// <param name="power">指数。</param>
        /// <returns>计算结果。</returns>
        /// <example>
        /// <code>
        /// double value = 2d;
        /// 
        /// double result1 = value.Pow(3); // 8
        /// double result2 = value.Pow(10); // 1024
        /// </code>
        /// </example>
        public static double Pow(this double value, double power) => Math.Pow(value, power);

        /// <summary>
        /// 计算 double 的平方根。
        /// </summary>
        /// <param name="value">待处理的 double。</param>
        /// <returns>平方根。</returns>
        /// <example>
        /// <code>
        /// double value = 16d;
        /// double result = value.Sqrt(); // 4
        /// </code>
        /// </example>
        public static double Sqrt(this double value) => Math.Sqrt(value);

        /// <summary>
        /// 向上取整（天花板函数）。
        /// </summary>
        /// <param name="value">待处理的 double。</param>
        /// <returns>向上取整后的值。</returns>
        /// <example>
        /// <code>
        /// double value1 = 123.1d;
        /// double value2 = 123.9d;
        /// 
        /// double result1 = value1.Ceiling(); // 124
        /// double result2 = value2.Ceiling(); // 124
        /// </code>
        /// </example>
        public static double Ceiling(this double value) => Math.Ceiling(value);

        /// <summary>
        /// 向下取整（地板函数）。
        /// </summary>
        /// <param name="value">待处理的 double。</param>
        /// <returns>向下取整后的值。</returns>
        /// <example>
        /// <code>
        /// double value1 = 123.1d;
        /// double value2 = 123.9d;
        /// 
        /// double result1 = value1.Floor(); // 123
        /// double result2 = value2.Floor(); // 123
        /// </code>
        /// </example>
        public static double Floor(this double value) => Math.Floor(value);

        /// <summary>
        /// 计算 double 的符号。
        /// </summary>
        /// <param name="value">待处理的 double。</param>
        /// <returns>如果值为正返回 1，为零返回 0，为负返回 -1。</returns>
        /// <example>
        /// <code>
        /// double value1 = 123.45d;
        /// double value2 = 0d;
        /// double value3 = -123.45d;
        /// 
        /// int result1 = value1.Sign(); // 1
        /// int result2 = value2.Sign(); // 0
        /// int result3 = value3.Sign(); // -1
        /// </code>
        /// </example>
        public static int Sign(this double value) => Math.Sign(value);

        /// <summary>
        /// 计算 double 的自然对数。
        /// </summary>
        /// <param name="value">待处理的 double。</param>
        /// <returns>自然对数值。</returns>
        /// <example>
        /// <code>
        /// double value = Math.E;
        /// double result = value.Log(); // 1
        /// </code>
        /// </example>
        public static double Log(this double value) => Math.Log(value);

        /// <summary>
        /// 计算 double 的指定底数的对数。
        /// </summary>
        /// <param name="value">待处理的 double。</param>
        /// <param name="newBase">对数的底数。</param>
        /// <returns>对数值。</returns>
        /// <example>
        /// <code>
        /// double value = 100d;
        /// double result = value.Log(10); // 2
        /// </code>
        /// </example>
        public static double Log(this double value, double newBase) => Math.Log(value, newBase);

        /// <summary>
        /// 计算 double 的以 10 为底的对数。
        /// </summary>
        /// <param name="value">待处理的 double。</param>
        /// <returns>以 10 为底的对数值。</returns>
        /// <example>
        /// <code>
        /// double value = 100d;
        /// double result = value.Log10(); // 2
        /// </code>
        /// </example>
        public static double Log10(this double value) => Math.Log10(value);

        /// <summary>
        /// 计算 e 的指定次幂。
        /// </summary>
        /// <param name="value">指数。</param>
        /// <returns>e 的 value 次幂。</returns>
        /// <example>
        /// <code>
        /// double value = 1d;
        /// double result = value.Exp(); // 2.71828182845905 (e)
        /// </code>
        /// </example>
        public static double Exp(this double value) => Math.Exp(value);

        /// <summary>
        /// 计算 double 的正弦值。
        /// </summary>
        /// <param name="value">弧度值。</param>
        /// <returns>正弦值。</returns>
        public static double Sin(this double value) => Math.Sin(value);

        /// <summary>
        /// 计算 double 的余弦值。
        /// </summary>
        /// <param name="value">弧度值。</param>
        /// <returns>余弦值。</returns>
        public static double Cos(this double value) => Math.Cos(value);

        /// <summary>
        /// 计算 double 的正切值。
        /// </summary>
        /// <param name="value">弧度值。</param>
        /// <returns>正切值。</returns>
        public static double Tan(this double value) => Math.Tan(value);

        /// <summary>
        /// 将角度转换为弧度。
        /// </summary>
        /// <param name="degrees">角度值。</param>
        /// <returns>弧度值。</returns>
        /// <example>
        /// <code>
        /// double degrees = 180d;
        /// double radians = degrees.ToRadians(); // 3.14159265358979 (π)
        /// </code>
        /// </example>
        public static double ToRadians(this double degrees) => degrees * Math.PI / 180d;

        /// <summary>
        /// 将弧度转换为角度。
        /// </summary>
        /// <param name="radians">弧度值。</param>
        /// <returns>角度值。</returns>
        /// <example>
        /// <code>
        /// double radians = Math.PI;
        /// double degrees = radians.ToDegrees(); // 180
        /// </code>
        /// </example>
        public static double ToDegrees(this double radians) => radians * 180d / Math.PI;

        #endregion

        #region 四则运算

        /// <summary>
        /// double 加法。
        /// </summary>
        /// <param name="value">第一个值。</param>
        /// <param name="other">第二个值。</param>
        /// <returns>两数之和。</returns>
        /// <example>
        /// <code>
        /// double value = 100d;
        /// double result = value.Add(50d); // 150
        /// </code>
        /// </example>
        public static double Add(this double value, double other) => value + other;

        /// <summary>
        /// double 减法。
        /// </summary>
        /// <param name="value">第一个值。</param>
        /// <param name="other">第二个值。</param>
        /// <returns>两数之差。</returns>
        /// <example>
        /// <code>
        /// double value = 100d;
        /// double result = value.Subtract(30d); // 70
        /// </code>
        /// </example>
        public static double Subtract(this double value, double other) => value - other;

        /// <summary>
        /// double 乘法。
        /// </summary>
        /// <param name="value">第一个值。</param>
        /// <param name="other">第二个值。</param>
        /// <returns>两数之积。</returns>
        /// <example>
        /// <code>
        /// double value = 100d;
        /// double result = value.Multiply(1.5d); // 150
        /// </code>
        /// </example>
        public static double Multiply(this double value, double other) => value * other;

        /// <summary>
        /// double 除法，除数为零时返回指定默认值。
        /// </summary>
        /// <param name="value">被除数。</param>
        /// <param name="other">除数。</param>
        /// <param name="defaultValue">除数为零时的默认返回值，默认为 0。</param>
        /// <returns>两数之商，或默认值。</returns>
        /// <example>
        /// <code>
        /// double value = 100d;
        /// 
        /// double result1 = value.DivideSafe(4d); // 25
        /// double result2 = value.DivideSafe(0d); // 0
        /// </code>
        /// </example>
        public static double DivideSafe(this double value, double other, double defaultValue = 0d) =>
            other == 0d ? defaultValue : value / other;

        /// <summary>
        /// double 求余。
        /// </summary>
        /// <param name="value">被除数。</param>
        /// <param name="other">除数。</param>
        /// <returns>余数，除数为零时返回 0。</returns>
        /// <example>
        /// <code>
        /// double value = 10d;
        /// double result = value.Mod(3d); // 1
        /// </code>
        /// </example>
        public static double Mod(this double value, double other) => other == 0d ? 0d : value % other;

        #endregion

        #region 比较运算

        /// <summary>
        /// 获取两个 double 中的较大值。
        /// </summary>
        /// <param name="value">第一个值。</param>
        /// <param name="other">第二个值。</param>
        /// <returns>较大的值。</returns>
        /// <example>
        /// <code>
        /// double value = 100d;
        /// double result = value.Max(200d); // 200
        /// </code>
        /// </example>
        public static double Max(this double value, double other) => Math.Max(value, other);

        /// <summary>
        /// 获取两个 double 中的较小值。
        /// </summary>
        /// <param name="value">第一个值。</param>
        /// <param name="other">第二个值。</param>
        /// <returns>较小的值。</returns>
        /// <example>
        /// <code>
        /// double value = 100d;
        /// double result = value.Min(200d); // 100
        /// </code>
        /// </example>
        public static double Min(this double value, double other) => Math.Min(value, other);

        /// <summary>
        /// 计算两个 double 的绝对差值。
        /// </summary>
        /// <param name="value">第一个值。</param>
        /// <param name="other">第二个值。</param>
        /// <returns>绝对差值。</returns>
        /// <example>
        /// <code>
        /// double value = 100d;
        /// double result = value.AbsDiff(130d); // 30
        /// </code>
        /// </example>
        public static double AbsDiff(this double value, double other) => Math.Abs(value - other);

        /// <summary>
        /// 判断两个 double 是否在指定精度范围内相等。
        /// </summary>
        /// <param name="value">第一个值。</param>
        /// <param name="other">第二个值。</param>
        /// <param name="tolerance">容差，默认为 0.0001。</param>
        /// <returns>如果差值小于容差返回 true；否则返回 false。</returns>
        /// <example>
        /// <code>
        /// double value1 = 1.00001d;
        /// double value2 = 1.00002d;
        /// 
        /// bool result = value1.EqualsTolerance(value2, 0.0001d); // true
        /// </code>
        /// </example>
        public static bool EqualsTolerance(this double value, double other, double tolerance = 0.0001d) =>
            Math.Abs(value - other) <= tolerance;

        #endregion

        #region 格式化输出

        /// <summary>
        /// 将 double 转换为固定小数位字符串。
        /// </summary>
        /// <param name="value">待处理的 double。</param>
        /// <param name="digits">保留的小数位数，默认为 2。</param>
        /// <returns>格式化后的字符串。</returns>
        /// <example>
        /// <code>
        /// double value = 123.4567d;
        /// 
        /// string result1 = value.ToFixedString(); // "123.46"
        /// string result2 = value.ToFixedString(3); // "123.457"
        /// </code>
        /// </example>
        public static string ToFixedString(this double value, int digits = 2) =>
            value.ToString($"F{digits}");

        /// <summary>
        /// 将 double 转换为货币格式字符串。
        /// </summary>
        /// <param name="value">待处理的 double。</param>
        /// <param name="culture">区域信息，默认为 "zh-CN"。</param>
        /// <returns>货币格式字符串。</returns>
        /// <example>
        /// <code>
        /// double value = 1234.56d;
        /// 
        /// string result1 = value.ToCurrencyString(); // "￥1,234.56"
        /// string result2 = value.ToCurrencyString("en-US"); // "$1,234.56"
        /// </code>
        /// </example>
        public static string ToCurrencyString(this double value, string culture = "zh-CN") =>
            value.ToString("C", new CultureInfo(culture));

        /// <summary>
        /// 将 double 转换为百分比字符串。
        /// </summary>
        /// <param name="value">待处理的 double（如 0.1234 表示 12.34%）。</param>
        /// <param name="digits">保留的小数位数，默认为 2。</param>
        /// <returns>百分比字符串。</returns>
        /// <example>
        /// <code>
        /// double value = 0.1234d;
        /// string result = value.ToPercentString(); // "12.34%"
        /// </code>
        /// </example>
        public static string ToPercentString(this double value, int digits = 2) =>
            (value * 100).ToString($"F{digits}") + "%";

        /// <summary>
        /// 将 double 转换为科学计数法字符串。
        /// </summary>
        /// <param name="value">待处理的 double。</param>
        /// <param name="digits">保留的小数位数，默认为 2。</param>
        /// <returns>科学计数法字符串。</returns>
        /// <example>
        /// <code>
        /// double value = 1234567.89d;
        /// string result = value.ToScientificString(); // "1.23E+006"
        /// </code>
        /// </example>
        public static string ToScientificString(this double value, int digits = 2) =>
            value.ToString($"E{digits}");

        /// <summary>
        /// 将 double 转换为友好字符串（如 "1.23万"、"1.23亿"）。
        /// </summary>
        /// <param name="value">待处理的 double。</param>
        /// <param name="digits">保留的小数位数，默认为 2。</param>
        /// <returns>友好格式字符串。</returns>
        /// <example>
        /// <code>
        /// double value1 = 12345d;
        /// double value2 = 123456789d;
        /// 
        /// string result1 = value1.ToFriendlyString(); // "1.23万"
        /// string result2 = value2.ToFriendlyString(); // "1.23亿"
        /// </code>
        /// </example>
        public static string ToFriendlyString(this double value, int digits = 2)
        {
            if (value >= 1_0000_0000d)
                return (value / 1_0000_0000d).ToString($"F{digits}") + "亿";
            if (value >= 1_0000d)
                return (value / 1_0000d).ToString($"F{digits}") + "万";
            return value.ToString($"F{digits}");
        }

        /// <summary>
        /// 将 double 转换为带千分位的字符串。
        /// </summary>
        /// <param name="value">待处理的 double。</param>
        /// <param name="digits">保留的小数位数，默认为 2。</param>
        /// <returns>带千分位的字符串。</returns>
        /// <example>
        /// <code>
        /// double value = 1234567.89d;
        /// string result = value.ToThousandsString(); // "1,234,567.89"
        /// </code>
        /// </example>
        public static string ToThousandsString(this double value, int digits = 2) =>
            value.ToString($"N{digits}");

        #endregion

        #region 进制转换

        /// <summary>
        /// 将 double 的整数部分转换为十六进制字符串。
        /// </summary>
        /// <param name="value">待转换的 double。</param>
        /// <returns>十六进制字符串。</returns>
        /// <example>
        /// <code>
        /// double value = 255d;
        /// string result = value.ToHexString(); // "FF"
        /// </code>
        /// </example>
        public static string ToHexString(this double value) => ((long)value).ToString("X");

        /// <summary>
        /// 将 double 的整数部分转换为二进制字符串。
        /// </summary>
        /// <param name="value">待转换的 double。</param>
        /// <returns>二进制字符串。</returns>
        /// <example>
        /// <code>
        /// double value = 10d;
        /// string result = value.ToBinaryString(); // "1010"
        /// </code>
        /// </example>
        public static string ToBinaryString(this double value) => Convert.ToString((long)value, 2);

        /// <summary>
        /// 将 double 的整数部分转换为八进制字符串。
        /// </summary>
        /// <param name="value">待转换的 double。</param>
        /// <returns>八进制字符串。</returns>
        /// <example>
        /// <code>
        /// double value = 64d;
        /// string result = value.ToOctalString(); // "100"
        /// </code>
        /// </example>
        public static string ToOctalString(this double value) => Convert.ToString((long)value, 8);

        #endregion
    }
}
