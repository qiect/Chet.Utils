using System.Globalization;

namespace Chet.Utils
{
    /// <summary>
    /// float 扩展方法类，提供常用的判断、转换、运算、格式化等功能。
    /// </summary>
    public static class FloatExtensions
    {
        /// <summary>
        /// 判断 float 是否为零。
        /// </summary>
        /// <param name="value">待判断的 float。</param>
        public static bool IsZero(this float value) => value == 0f;

        /// <summary>
        /// 判断 float 是否为正数。
        /// </summary>
        /// <param name="value">待判断的 float。</param>
        public static bool IsPositive(this float value) => value > 0f;

        /// <summary>
        /// 判断 float 是否为负数。
        /// </summary>
        /// <param name="value">待判断的 float。</param>
        public static bool IsNegative(this float value) => value < 0f;

        /// <summary>
        /// 判断 float 是否为整数。
        /// </summary>
        /// <param name="value">待判断的 float。</param>
        public static bool IsInteger(this float value) => MathF.Truncate(value) == value;

        /// <summary>
        /// 判断 float 是否为偶数（仅整数时有效）。
        /// </summary>
        /// <param name="value">待判断的 float。</param>
        public static bool IsEven(this float value) => IsInteger(value) && ((long)value % 2 == 0);

        /// <summary>
        /// 判断 float 是否为奇数（仅整数时有效）。
        /// </summary>
        /// <param name="value">待判断的 float。</param>
        public static bool IsOdd(this float value) => IsInteger(value) && ((long)value % 2 != 0);

        /// <summary>
        /// 判断 float 是否为 NaN。
        /// </summary>
        /// <param name="value">待判断的 float。</param>
        public static bool IsNaN(this float value) => float.IsNaN(value);

        /// <summary>
        /// 判断 float 是否为正无穷。
        /// </summary>
        /// <param name="value">待判断的 float。</param>
        public static bool IsPositiveInfinity(this float value) => float.IsPositiveInfinity(value);

        /// <summary>
        /// 判断 float 是否为负无穷。
        /// </summary>
        /// <param name="value">待判断的 float。</param>
        public static bool IsNegativeInfinity(this float value) => float.IsNegativeInfinity(value);

        /// <summary>
        /// float 四舍五入到指定小数位。
        /// </summary>
        /// <param name="value">待处理的 float。</param>
        /// <param name="digits">保留的小数位数。</param>
        public static float Round(this float value, int digits = 2) =>
            (float)Math.Round(value, digits, MidpointRounding.AwayFromZero);

        /// <summary>
        /// float 截断到指定小数位（向零取整）。
        /// </summary>
        /// <param name="value">待处理的 float。</param>
        /// <param name="digits">保留的小数位数。</param>
        public static float Truncate(this float value, int digits = 2)
        {
            float factor = (float)Math.Pow(10, digits);
            return (float)(Math.Truncate(value * factor) / factor);
        }

        /// <summary>
        /// float 取绝对值。
        /// </summary>
        /// <param name="value">待处理的 float。</param>
        public static float Abs(this float value) => MathF.Abs(value);

        /// <summary>
        /// float 转为字符串，保留指定小数位。
        /// </summary>
        /// <param name="value">待处理的 float。</param>
        /// <param name="digits">保留的小数位数。</param>
        public static string ToFixedString(this float value, int digits = 2) =>
            value.ToString($"F{digits}");

        /// <summary>
        /// float 转为货币格式字符串（如 "￥1,234.56"）。
        /// </summary>
        /// <param name="value">待处理的 float。</param>
        /// <param name="culture">区域信息，默认中文。</param>
        public static string ToCurrencyString(this float value, string culture = "zh-CN") =>
            value.ToString("C", new CultureInfo(culture));

        /// <summary>
        /// float 转为百分比字符串（如 "12.34%"）。
        /// </summary>
        /// <param name="value">待处理的 float。</param>
        /// <param name="digits">保留的小数位数。</param>
        public static string ToPercentString(this float value, int digits = 2) =>
            (value * 100).ToString($"F{digits}") + "%";

        /// <summary>
        /// float 转为科学计数法字符串。
        /// </summary>
        /// <param name="value">待处理的 float。</param>
        /// <param name="digits">保留的小数位数。</param>
        public static string ToScientificString(this float value, int digits = 2) =>
            value.ToString($"E{digits}");

        /// <summary>
        /// float 取最大值。
        /// </summary>
        /// <param name="value">第一个值。</param>
        /// <param name="other">第二个值。</param>
        public static float Max(this float value, float other) => MathF.Max(value, other);

        /// <summary>
        /// float 取最小值。
        /// </summary>
        /// <param name="value">第一个值。</param>
        /// <param name="other">第二个值。</param>
        public static float Min(this float value, float other) => MathF.Min(value, other);

        /// <summary>
        /// float 是否在指定范围内（包含边界）。
        /// </summary>
        /// <param name="value">待判断的 float。</param>
        /// <param name="min">最小值。</param>
        /// <param name="max">最大值。</param>
        public static bool IsBetween(this float value, float min, float max) =>
            value >= min && value <= max;

        /// <summary>
        /// float 保证在指定范围内，超出则取边界值。
        /// </summary>
        /// <param name="value">待处理的 float。</param>
        /// <param name="min">最小值。</param>
        /// <param name="max">最大值。</param>
        public static float Clamp(this float value, float min, float max) =>
            value < min ? min : (value > max ? max : value);

        /// <summary>
        /// float 转为 int，四舍五入。
        /// </summary>
        /// <param name="value">待转换的 float。</param>
        public static int ToInt(this float value) => (int)Math.Round(value, 0, MidpointRounding.AwayFromZero);

        /// <summary>
        /// float 转为 double。
        /// </summary>
        /// <param name="value">待转换的 float。</param>
        public static double ToDouble(this float value) => (double)value;

        /// <summary>
        /// float 转为 long，四舍五入。
        /// </summary>
        /// <param name="value">待转换的 float。</param>
        public static long ToLong(this float value) => (long)Math.Round(value, 0, MidpointRounding.AwayFromZero);

        /// <summary>
        /// float 转为 decimal。
        /// </summary>
        /// <param name="value">待转换的 float。</param>
        public static decimal ToDecimal(this float value) => (decimal)value;

        /// <summary>
        /// float 转为 bool（非零为 true）。
        /// </summary>
        /// <param name="value">待转换的 float。</param>
        public static bool ToBool(this float value) => value != 0f;

        /// <summary>
        /// float 加法。
        /// </summary>
        /// <param name="value">第一个值。</param>
        /// <param name="other">第二个值。</param>
        public static float Add(this float value, float other) => value + other;

        /// <summary>
        /// float 减法。
        /// </summary>
        /// <param name="value">第一个值。</param>
        /// <param name="other">第二个值。</param>
        public static float Subtract(this float value, float other) => value - other;

        /// <summary>
        /// float 乘法。
        /// </summary>
        /// <param name="value">第一个值。</param>
        /// <param name="other">第二个值。</param>
        public static float Multiply(this float value, float other) => value * other;

        /// <summary>
        /// float 除法，除数为零时返回零。
        /// </summary>
        /// <param name="value">被除数。</param>
        /// <param name="other">除数。</param>
        public static float DivideSafe(this float value, float other) =>
            other == 0f ? 0f : value / other;

        /// <summary>
        /// float 求余。
        /// </summary>
        /// <param name="value">被除数。</param>
        /// <param name="other">除数。</param>
        public static float Mod(this float value, float other) => other == 0f ? 0f : value % other;

        /// <summary>
        /// float 求幂。
        /// </summary>
        /// <param name="value">底数。</param>
        /// <param name="power">指数。</param>
        public static float Pow(this float value, int power) => (float)Math.Pow(value, power);

        /// <summary>
        /// float 求平方根。
        /// </summary>
        /// <param name="value">待处理的 float。</param>
        public static float Sqrt(this float value) => MathF.Sqrt(value);

        /// <summary>
        /// float 求绝对差值。
        /// </summary>
        /// <param name="value">第一个值。</param>
        /// <param name="other">第二个值。</param>
        public static float AbsDiff(this float value, float other) => MathF.Abs(value - other);

        /// <summary>
        /// float 转为十六进制字符串（仅整数部分）。
        /// </summary>
        /// <param name="value">待转换的 float。</param>
        public static string ToHexString(this float value) => ((long)value).ToString("X");

        /// <summary>
        /// float 转为二进制字符串（仅整数部分）。
        /// </summary>
        /// <param name="value">待转换的 float。</param>
        public static string ToBinaryString(this float value) => Convert.ToString((long)value, 2);

        /// <summary>
        /// float 转为八进制字符串（仅整数部分）。
        /// </summary>
        /// <param name="value">待转换的 float。</param>
        public static string ToOctalString(this float value) => Convert.ToString((long)value, 8);

        /// <summary>
        /// float 转为友好字符串（如 "1.23万"、"1.23亿"）。
        /// </summary>
        /// <param name="value">待处理的 float。</param>
        /// <param name="digits">保留的小数位数。</param>
        public static string ToFriendlyString(this float value, int digits = 2)
        {
            if (value >= 1_0000_0000f)
                return (value / 1_0000_0000f).ToString($"F{digits}") + "亿";
            if (value >= 1_0000f)
                return (value / 1_0000f).ToString($"F{digits}") + "万";
            return value.ToString($"F{digits}");
        }
    }
}