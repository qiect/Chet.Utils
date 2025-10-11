using System.Globalization;

namespace Chet.Utils
{
    /// <summary>
    /// double 扩展方法类，提供常用的判断、转换、运算、格式化等功能。
    /// </summary>
    public static class DoubleExtensions
    {
        /// <summary>
        /// 判断 double 是否为零。
        /// </summary>
        /// <param name="value">待判断的 double。</param>
        public static bool IsZero(this double value) => value == 0d;

        /// <summary>
        /// 判断 double 是否为正数。
        /// </summary>
        /// <param name="value">待判断的 double。</param>
        public static bool IsPositive(this double value) => value > 0d;

        /// <summary>
        /// 判断 double 是否为负数。
        /// </summary>
        /// <param name="value">待判断的 double。</param>
        public static bool IsNegative(this double value) => value < 0d;

        /// <summary>
        /// 判断 double 是否为整数。
        /// </summary>
        /// <param name="value">待判断的 double。</param>
        public static bool IsInteger(this double value) => Math.Truncate(value) == value;

        /// <summary>
        /// 判断 double 是否为偶数（仅整数时有效）。
        /// </summary>
        /// <param name="value">待判断的 double。</param>
        public static bool IsEven(this double value) => IsInteger(value) && ((long)value % 2 == 0);

        /// <summary>
        /// 判断 double 是否为奇数（仅整数时有效）。
        /// </summary>
        /// <param name="value">待判断的 double。</param>
        public static bool IsOdd(this double value) => IsInteger(value) && ((long)value % 2 != 0);

        /// <summary>
        /// 判断 double 是否为 NaN。
        /// </summary>
        /// <param name="value">待判断的 double。</param>
        public static bool IsNaN(this double value) => double.IsNaN(value);

        /// <summary>
        /// 判断 double 是否为正无穷。
        /// </summary>
        /// <param name="value">待判断的 double。</param>
        public static bool IsPositiveInfinity(this double value) => double.IsPositiveInfinity(value);

        /// <summary>
        /// 判断 double 是否为负无穷。
        /// </summary>
        /// <param name="value">待判断的 double。</param>
        public static bool IsNegativeInfinity(this double value) => double.IsNegativeInfinity(value);

        /// <summary>
        /// double 四舍五入到指定小数位。
        /// </summary>
        /// <param name="value">待处理的 double。</param>
        /// <param name="digits">保留的小数位数。</param>
        public static double Round(this double value, int digits = 2) =>
            Math.Round(value, digits, MidpointRounding.AwayFromZero);

        /// <summary>
        /// double 截断到指定小数位（向零取整）。
        /// </summary>
        /// <param name="value">待处理的 double。</param>
        /// <param name="digits">保留的小数位数。</param>
        public static double Truncate(this double value, int digits = 2)
        {
            double factor = Math.Pow(10, digits);
            return Math.Truncate(value * factor) / factor;
        }

        /// <summary>
        /// double 取绝对值。
        /// </summary>
        /// <param name="value">待处理的 double。</param>
        public static double Abs(this double value) => Math.Abs(value);

        /// <summary>
        /// double 转为字符串，保留指定小数位。
        /// </summary>
        /// <param name="value">待处理的 double。</param>
        /// <param name="digits">保留的小数位数。</param>
        public static string ToFixedString(this double value, int digits = 2) =>
            value.ToString($"F{digits}");

        /// <summary>
        /// double 转为货币格式字符串（如 "￥1,234.56"）。
        /// </summary>
        /// <param name="value">待处理的 double。</param>
        /// <param name="culture">区域信息，默认中文。</param>
        public static string ToCurrencyString(this double value, string culture = "zh-CN") =>
            value.ToString("C", new CultureInfo(culture));

        /// <summary>
        /// double 转为百分比字符串（如 "12.34%"）。
        /// </summary>
        /// <param name="value">待处理的 double。</param>
        /// <param name="digits">保留的小数位数。</param>
        public static string ToPercentString(this double value, int digits = 2) =>
            (value * 100).ToString($"F{digits}") + "%";

        /// <summary>
        /// double 转为科学计数法字符串。
        /// </summary>
        /// <param name="value">待处理的 double。</param>
        /// <param name="digits">保留的小数位数。</param>
        public static string ToScientificString(this double value, int digits = 2) =>
            value.ToString($"E{digits}");

        /// <summary>
        /// double 取最大值。
        /// </summary>
        /// <param name="value">第一个值。</param>
        /// <param name="other">第二个值。</param>
        public static double Max(this double value, double other) => Math.Max(value, other);

        /// <summary>
        /// double 取最小值。
        /// </summary>
        /// <param name="value">第一个值。</param>
        /// <param name="other">第二个值。</param>
        public static double Min(this double value, double other) => Math.Min(value, other);

        /// <summary>
        /// double 是否在指定范围内（包含边界）。
        /// </summary>
        /// <param name="value">待判断的 double。</param>
        /// <param name="min">最小值。</param>
        /// <param name="max">最大值。</param>
        public static bool IsBetween(this double value, double min, double max) =>
            value >= min && value <= max;

        /// <summary>
        /// double 保证在指定范围内，超出则取边界值。
        /// </summary>
        /// <param name="value">待处理的 double。</param>
        /// <param name="min">最小值。</param>
        /// <param name="max">最大值。</param>
        public static double Clamp(this double value, double min, double max) =>
            value < min ? min : (value > max ? max : value);

        /// <summary>
        /// double 转为 int，四舍五入。
        /// </summary>
        /// <param name="value">待转换的 double。</param>
        public static int ToInt(this double value) => (int)Math.Round(value, 0, MidpointRounding.AwayFromZero);

        /// <summary>
        /// double 转为 float。
        /// </summary>
        /// <param name="value">待转换的 double。</param>
        public static float ToFloat(this double value) => (float)value;

        /// <summary>
        /// double 转为 long，四舍五入。
        /// </summary>
        /// <param name="value">待转换的 double。</param>
        public static long ToLong(this double value) => (long)Math.Round(value, 0, MidpointRounding.AwayFromZero);

        /// <summary>
        /// double 转为 decimal。
        /// </summary>
        /// <param name="value">待转换的 double。</param>
        public static decimal ToDecimal(this double value) => (decimal)value;

        /// <summary>
        /// double 转为 bool（非零为 true）。
        /// </summary>
        /// <param name="value">待转换的 double。</param>
        public static bool ToBool(this double value) => value != 0d;

        /// <summary>
        /// double 加法。
        /// </summary>
        /// <param name="value">第一个值。</param>
        /// <param name="other">第二个值。</param>
        public static double Add(this double value, double other) => value + other;

        /// <summary>
        /// double 减法。
        /// </summary>
        /// <param name="value">第一个值。</param>
        /// <param name="other">第二个值。</param>
        public static double Subtract(this double value, double other) => value - other;

        /// <summary>
        /// double 乘法。
        /// </summary>
        /// <param name="value">第一个值。</param>
        /// <param name="other">第二个值。</param>
        public static double Multiply(this double value, double other) => value * other;

        /// <summary>
        /// double 除法，除数为零时返回零。
        /// </summary>
        /// <param name="value">被除数。</param>
        /// <param name="other">除数。</param>
        public static double DivideSafe(this double value, double other) =>
            other == 0d ? 0d : value / other;

        /// <summary>
        /// double 求余。
        /// </summary>
        /// <param name="value">被除数。</param>
        /// <param name="other">除数。</param>
        public static double Mod(this double value, double other) => other == 0d ? 0d : value % other;

        /// <summary>
        /// double 求幂。
        /// </summary>
        /// <param name="value">底数。</param>
        /// <param name="power">指数。</param>
        public static double Pow(this double value, int power) => Math.Pow(value, power);

        /// <summary>
        /// double 求平方根。
        /// </summary>
        /// <param name="value">待处理的 double。</param>
        public static double Sqrt(this double value) => Math.Sqrt(value);

        /// <summary>
        /// double 求绝对差值。
        /// </summary>
        /// <param name="value">第一个值。</param>
        /// <param name="other">第二个值。</param>
        public static double AbsDiff(this double value, double other) => Math.Abs(value - other);

        /// <summary>
        /// double 转为十六进制字符串（仅整数部分）。
        /// </summary>
        /// <param name="value">待转换的 double。</param>
        public static string ToHexString(this double value) => ((long)value).ToString("X");

        /// <summary>
        /// double 转为二进制字符串（仅整数部分）。
        /// </summary>
        /// <param name="value">待转换的 double。</param>
        public static string ToBinaryString(this double value) => Convert.ToString((long)value, 2);

        /// <summary>
        /// double 转为八进制字符串（仅整数部分）。
        /// </summary>
        /// <param name="value">待转换的 double。</param>
        public static string ToOctalString(this double value) => Convert.ToString((long)value, 8);

        /// <summary>
        /// double 转为友好字符串（如 "1.23万"、"1.23亿"）。
        /// </summary>
        /// <param name="value">待处理的 double。</param>
        /// <param name="digits">保留的小数位数。</param>
        public static string ToFriendlyString(this double value, int digits = 2)
        {
            if (value >= 1_0000_0000)
                return (value / 1_0000_0000).ToString($"F{digits}") + "亿";
            if (value >= 1_0000)
                return (value / 1_0000).ToString($"F{digits}") + "万";
            return value.ToString($"F{digits}");
        }
    }
}