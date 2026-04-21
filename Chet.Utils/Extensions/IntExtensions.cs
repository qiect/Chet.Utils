using System.Globalization;

namespace Chet.Utils;

/// <summary>
/// int 扩展方法类，提供常用的判断、转换、运算、格式化等功能。
/// </summary>
/// <remarks>
/// <para>本类提供丰富的整数扩展方法，涵盖以下功能：</para>
/// <list type="bullet">
///   <item><description>基础判断：IsZero、IsPositive、IsNegative、IsEven、IsOdd</description></item>
///   <item><description>范围判断：IsBetween、Clamp、IsInRange</description></item>
///   <item><description>类型转换：ToBool、ToDouble、ToDecimal、ToLong、ToFloat</description></item>
///   <item><description>数学运算：Abs、Max、Min、Add、Subtract、Multiply、DivideSafe、Pow、Sqrt</description></item>
///   <item><description>格式化输出：ToCurrencyString、ToPercentString、ToHexString、ToBinaryString</description></item>
///   <item><description>中文转换：ToChineseUpper、ToChineseWeekday、ToChineseNumber</description></item>
///   <item><description>特殊功能：ToEnum、ToRomanString、Repeat、Times</description></item>
/// </list>
/// </remarks>
public static class IntExtensions
{
    #region 基础判断

    /// <summary>
    /// 判断 int 是否为零。
    /// </summary>
    /// <param name="value">待判断的 int。</param>
    /// <returns>如果值为零返回 true；否则返回 false。</returns>
    public static bool IsZero(this int value) => value == 0;

    /// <summary>
    /// 判断 int 是否为正数。
    /// </summary>
    /// <param name="value">待判断的 int。</param>
    /// <returns>如果值大于零返回 true；否则返回 false。</returns>
    public static bool IsPositive(this int value) => value > 0;

    /// <summary>
    /// 判断 int 是否为负数。
    /// </summary>
    /// <param name="value">待判断的 int。</param>
    /// <returns>如果值小于零返回 true；否则返回 false。</returns>
    public static bool IsNegative(this int value) => value < 0;

    /// <summary>
    /// 判断 int 是否为偶数。
    /// </summary>
    /// <param name="value">待判断的 int。</param>
    /// <returns>如果是偶数返回 true；否则返回 false。</returns>
    public static bool IsEven(this int value) => value % 2 == 0;

    /// <summary>
    /// 判断 int 是否为奇数。
    /// </summary>
    /// <param name="value">待判断的 int。</param>
    /// <returns>如果是奇数返回 true；否则返回 false。</returns>
    public static bool IsOdd(this int value) => value % 2 != 0;

    /// <summary>
    /// 判断 int 是否为质数。
    /// </summary>
    /// <param name="value">待判断的 int。</param>
    /// <returns>如果是质数返回 true；否则返回 false。</returns>
    public static bool IsPrime(this int value)
    {
        if (value <= 1) return false;
        if (value <= 3) return true;
        if (value % 2 == 0 || value % 3 == 0) return false;
        for (int i = 5; i * i <= value; i += 6)
        {
            if (value % i == 0 || value % (i + 2) == 0)
                return false;
        }
        return true;
    }

    #endregion

    #region 范围判断

    /// <summary>
    /// 判断 int 是否在指定范围内（包含边界）。
    /// </summary>
    /// <param name="value">待判断的 int。</param>
    /// <param name="min">最小值。</param>
    /// <param name="max">最大值。</param>
    /// <returns>如果值在范围内返回 true；否则返回 false。</returns>
    public static bool IsBetween(this int value, int min, int max) => value >= min && value <= max;

    /// <summary>
    /// 判断 int 是否在指定范围内（不包含边界）。
    /// </summary>
    /// <param name="value">待判断的 int。</param>
    /// <param name="min">最小值。</param>
    /// <param name="max">最大值。</param>
    /// <returns>如果值在范围内（不包含边界）返回 true；否则返回 false。</returns>
    public static bool IsBetweenExclusive(this int value, int min, int max) => value > min && value < max;

    /// <summary>
    /// 保证 int 在指定范围内，超出则取边界值。
    /// </summary>
    /// <param name="value">待处理的 int。</param>
    /// <param name="min">最小值。</param>
    /// <param name="max">最大值。</param>
    /// <returns>限制在范围内的值。</returns>
    public static int Clamp(this int value, int min, int max) =>
        value < min ? min : (value > max ? max : value);

    /// <summary>
    /// 将值限制在指定最大值以内。
    /// </summary>
    /// <param name="value">待处理的 int。</param>
    /// <param name="max">最大值。</param>
    /// <returns>限制后的值。</returns>
    public static int ClampMax(this int value, int max) => Math.Min(value, max);

    /// <summary>
    /// 将值限制在指定最小值以上。
    /// </summary>
    /// <param name="value">待处理的 int。</param>
    /// <param name="min">最小值。</param>
    /// <returns>限制后的值。</returns>
    public static int ClampMin(this int value, int min) => Math.Max(value, min);

    #endregion

    #region 类型转换

    /// <summary>
    /// 将 int 转换为 bool（非零为 true）。
    /// </summary>
    /// <param name="value">待转换的 int。</param>
    /// <returns>非零返回 true，零返回 false。</returns>
    public static bool ToBool(this int value) => value != 0;

    /// <summary>
    /// 将 int 转换为 double。
    /// </summary>
    /// <param name="value">待转换的 int。</param>
    /// <returns>double 类型的值。</returns>
    public static double ToDouble(this int value) => (double)value;

    /// <summary>
    /// 将 int 转换为 float。
    /// </summary>
    /// <param name="value">待转换的 int。</param>
    /// <returns>float 类型的值。</returns>
    public static float ToFloat(this int value) => (float)value;

    /// <summary>
    /// 将 int 转换为 long。
    /// </summary>
    /// <param name="value">待转换的 int。</param>
    /// <returns>long 类型的值。</returns>
    public static long ToLong(this int value) => (long)value;

    /// <summary>
    /// 将 int 转换为 decimal。
    /// </summary>
    /// <param name="value">待转换的 int。</param>
    /// <returns>decimal 类型的值。</returns>
    public static decimal ToDecimal(this int value) => (decimal)value;

    /// <summary>
    /// 将 int 转换为 short。
    /// </summary>
    /// <param name="value">待转换的 int。</param>
    /// <returns>short 类型的值。</returns>
    public static short ToShort(this int value) => (short)value;

    /// <summary>
    /// 将 int 转换为 byte。
    /// </summary>
    /// <param name="value">待转换的 int。</param>
    /// <returns>byte 类型的值。</returns>
    public static byte ToByte(this int value) => (byte)value;

    /// <summary>
    /// 将 int 转换为 char。
    /// </summary>
    /// <param name="value">待转换的 int。</param>
    /// <returns>char 类型的值。</returns>
    public static char ToChar(this int value) => (char)value;

    #endregion

    #region 数学运算

    /// <summary>
    /// 求 int 的绝对值。
    /// </summary>
    /// <param name="value">待处理的 int。</param>
    /// <returns>绝对值。</returns>
    public static int Abs(this int value) => Math.Abs(value);

    /// <summary>
    /// 求两个 int 的最大值。
    /// </summary>
    /// <param name="value">第一个值。</param>
    /// <param name="other">第二个值。</param>
    /// <returns>较大的值。</returns>
    public static int Max(this int value, int other) => Math.Max(value, other);

    /// <summary>
    /// 求两个 int 的最小值。
    /// </summary>
    /// <param name="value">第一个值。</param>
    /// <param name="other">第二个值。</param>
    /// <returns>较小的值。</returns>
    public static int Min(this int value, int other) => Math.Min(value, other);

    /// <summary>
    /// int 加法。
    /// </summary>
    /// <param name="value">第一个值。</param>
    /// <param name="other">第二个值。</param>
    /// <returns>两数之和。</returns>
    public static int Add(this int value, int other) => value + other;

    /// <summary>
    /// int 减法。
    /// </summary>
    /// <param name="value">第一个值。</param>
    /// <param name="other">第二个值。</param>
    /// <returns>两数之差。</returns>
    public static int Subtract(this int value, int other) => value - other;

    /// <summary>
    /// int 乘法。
    /// </summary>
    /// <param name="value">第一个值。</param>
    /// <param name="other">第二个值。</param>
    /// <returns>两数之积。</returns>
    public static int Multiply(this int value, int other) => value * other;

    /// <summary>
    /// int 除法，除数为零时返回零。
    /// </summary>
    /// <param name="value">被除数。</param>
    /// <param name="other">除数。</param>
    /// <returns>商，除数为零时返回 0。</returns>
    public static int DivideSafe(this int value, int other) => other == 0 ? 0 : value / other;

    /// <summary>
    /// int 求余。
    /// </summary>
    /// <param name="value">被除数。</param>
    /// <param name="other">除数。</param>
    /// <returns>余数，除数为零时返回 0。</returns>
    public static int Mod(this int value, int other) => other == 0 ? 0 : value % other;

    /// <summary>
    /// int 求幂。
    /// </summary>
    /// <param name="value">底数。</param>
    /// <param name="power">指数。</param>
    /// <returns>幂运算结果。</returns>
    public static int Pow(this int value, int power) => (int)Math.Pow(value, power);

    /// <summary>
    /// int 求平方根。
    /// </summary>
    /// <param name="value">待处理的 int。</param>
    /// <returns>平方根的整数部分。</returns>
    public static int Sqrt(this int value) => (int)Math.Sqrt(value);

    /// <summary>
    /// int 求绝对差值。
    /// </summary>
    /// <param name="value">第一个值。</param>
    /// <param name="other">第二个值。</param>
    /// <returns>两数绝对差。</returns>
    public static int AbsDiff(this int value, int other) => Math.Abs(value - other);

    /// <summary>
    /// int 求符号（-1、0、1）。
    /// </summary>
    /// <param name="value">待处理的 int。</param>
    /// <returns>负数返回 -1，零返回 0，正数返回 1。</returns>
    public static int Sign(this int value) => Math.Sign(value);

    /// <summary>
    /// int 求阶乘。
    /// </summary>
    /// <param name="value">待处理的 int。</param>
    /// <returns>阶乘结果，负数返回 0。</returns>
    public static long Factorial(this int value)
    {
        if (value < 0) return 0;
        if (value <= 1) return 1;
        long result = 1;
        for (int i = 2; i <= value; i++)
            result *= i;
        return result;
    }

    /// <summary>
    /// int 求最大公约数。
    /// </summary>
    /// <param name="value">第一个值。</param>
    /// <param name="other">第二个值。</param>
    /// <returns>最大公约数。</returns>
    public static int Gcd(this int value, int other)
    {
        value = Math.Abs(value);
        other = Math.Abs(other);
        while (other != 0)
        {
            int temp = other;
            other = value % other;
            value = temp;
        }
        return value;
    }

    /// <summary>
    /// int 求最小公倍数。
    /// </summary>
    /// <param name="value">第一个值。</param>
    /// <param name="other">第二个值。</param>
    /// <returns>最小公倍数。</returns>
    public static int Lcm(this int value, int other)
    {
        if (value == 0 || other == 0) return 0;
        return Math.Abs(value / value.Gcd(other) * other);
    }

    #endregion

    #region 格式化输出

    /// <summary>
    /// 将 int 转换为指定格式的字符串。
    /// </summary>
    /// <param name="value">待转换的 int。</param>
    /// <param name="format">格式字符串，如 "D4"、"X"。</param>
    /// <returns>格式化后的字符串。</returns>
    public static string ToStringFormat(this int value, string format = null) =>
        format == null ? value.ToString() : value.ToString(format);

    /// <summary>
    /// 将 int 转换为货币格式字符串。
    /// </summary>
    /// <param name="value">待转换的 int。</param>
    /// <param name="culture">区域信息，默认中文。</param>
    /// <returns>货币格式字符串（如 "￥1,234"）。</returns>
    public static string ToCurrencyString(this int value, string culture = "zh-CN") =>
        value.ToString("C0", new CultureInfo(culture));

    /// <summary>
    /// 将 int 转换为百分比字符串。
    /// </summary>
    /// <param name="value">待转换的 int。</param>
    /// <returns>百分比字符串（如 "12%"）。</returns>
    public static string ToPercentString(this int value) => value.ToString() + "%";

    /// <summary>
    /// 将 int 转换为千分位格式字符串。
    /// </summary>
    /// <param name="value">待转换的 int。</param>
    /// <returns>千分位格式字符串（如 "1,234"）。</returns>
    public static string ToThousandsString(this int value) => value.ToString("N0");

    /// <summary>
    /// 将 int 转换为十六进制字符串。
    /// </summary>
    /// <param name="value">待转换的 int。</param>
    /// <returns>十六进制字符串。</returns>
    public static string ToHexString(this int value) => value.ToString("X");

    /// <summary>
    /// 将 int 转换为指定长度的十六进制字符串。
    /// </summary>
    /// <param name="value">待转换的 int。</param>
    /// <param name="digits">位数。</param>
    /// <returns>指定长度的十六进制字符串。</returns>
    public static string ToHexString(this int value, int digits) => value.ToString("X" + digits);

    /// <summary>
    /// 将 int 转换为二进制字符串。
    /// </summary>
    /// <param name="value">待转换的 int。</param>
    /// <returns>二进制字符串。</returns>
    public static string ToBinaryString(this int value) => Convert.ToString(value, 2);

    /// <summary>
    /// 将 int 转换为八进制字符串。
    /// </summary>
    /// <param name="value">待转换的 int。</param>
    /// <returns>八进制字符串。</returns>
    public static string ToOctalString(this int value) => Convert.ToString(value, 8);

    #endregion

    #region 中文转换

    /// <summary>
    /// 将 int 转换为中文大写金额（仅整数）。
    /// </summary>
    /// <param name="value">待转换的 int。</param>
    /// <returns>中文大写金额字符串（如 "壹万贰仟叁佰肆拾伍元"）。</returns>
    public static string ToChineseUpper(this int value)
    {
        string[] cnNums = { "零", "壹", "贰", "叁", "肆", "伍", "陆", "柒", "捌", "玖" };
        string[] cnIntRadice = { "", "拾", "佰", "仟" };
        string[] cnIntUnits = { "", "万", "亿", "兆" };
        string cnIntLast = "元";
        if (value == 0) return cnNums[0] + cnIntLast;
        string intStr = Math.Abs(value).ToString();
        int length = intStr.Length;
        string result = "";
        bool zeroFlag = false;
        for (int i = 0; i < length; i++)
        {
            int n = intStr[i] - '0';
            int p = length - i - 1;
            int unitPos = p / 4;
            int radicePos = p % 4;
            if (n == 0)
            {
                if (!zeroFlag)
                {
                    result += cnNums[0];
                    zeroFlag = true;
                }
                if (radicePos == 0 && unitPos > 0)
                    result += cnIntUnits[unitPos];
            }
            else
            {
                result += cnNums[n] + cnIntRadice[radicePos];
                if (radicePos == 0 && unitPos > 0)
                    result += cnIntUnits[unitPos];
                zeroFlag = false;
            }
        }
        result += cnIntLast;
        result = result.Replace("零零", "零").Replace("零元", "元");
        if (result.StartsWith("零")) result = result[1..];
        if (value < 0) result = "负" + result;
        return result;
    }

    /// <summary>
    /// 将 int 转换为中文数字。
    /// </summary>
    /// <param name="value">待转换的 int。</param>
    /// <returns>中文数字字符串（如 "一万二千三百四十五"）。</returns>
    public static string ToChineseNumber(this int value)
    {
        if (value == 0) return "零";

        string[] digits = { "", "一", "二", "三", "四", "五", "六", "七", "八", "九" };
        string[] units = { "", "十", "百", "千", "万", "十", "百", "千", "亿" };

        string str = Math.Abs(value).ToString();
        string result = "";
        int n = str.Length;

        for (int i = 0; i < n; i++)
        {
            int digit = str[i] - '0';
            int unitIndex = n - 1 - i;

            if (digit == 0)
            {
                if (unitIndex == 4 || unitIndex == 8)
                    result += units[unitIndex];
                else if (i < n - 1 && str[i + 1] != '0')
                    result += "零";
            }
            else
            {
                if (digit == 1 && (unitIndex == 1 || unitIndex == 5) && i == 0)
                    result += units[unitIndex];
                else
                    result += digits[digit] + units[unitIndex];
            }
        }

        if (value < 0) result = "负" + result;
        return result;
    }

    /// <summary>
    /// 将 int 转换为星期中文（0~6，周日~周六）。
    /// </summary>
    /// <param name="value">待转换的 int。</param>
    /// <returns>星期中文名称。</returns>
    public static string ToChineseWeekday(this int value)
    {
        var weekDays = new[] { "星期日", "星期一", "星期二", "星期三", "星期四", "星期五", "星期六" };
        return value >= 0 && value < 7 ? weekDays[value] : value.ToString();
    }

    /// <summary>
    /// 将 int 转换为英文星期（0~6，Sunday~Saturday）。
    /// </summary>
    /// <param name="value">待转换的 int。</param>
    /// <returns>星期英文名称。</returns>
    public static string ToEnglishWeekday(this int value)
    {
        var weekDays = new[] { "Sunday", "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday" };
        return value >= 0 && value < 7 ? weekDays[value] : value.ToString();
    }

    #endregion

    #region 特殊功能

    /// <summary>
    /// 将 int 转换为罗马数字字符串（1~3999）。
    /// </summary>
    /// <param name="value">待转换的 int。</param>
    /// <returns>罗马数字字符串。</returns>
    public static string ToRomanString(this int value)
    {
        if (value < 1 || value > 3999) return value.ToString();
        var map = new (int Value, string Numeral)[]
        {
            (1000, "M"), (900, "CM"), (500, "D"), (400, "CD"),
            (100, "C"), (90, "XC"), (50, "L"), (40, "XL"),
            (10, "X"), (9, "IX"), (5, "V"), (4, "IV"), (1, "I")
        };
        int num = value;
        string result = "";
        foreach (var item in map)
        {
            while (num >= item.Value)
            {
                result += item.Numeral;
                num -= item.Value;
            }
        }
        return result;
    }

    /// <summary>
    /// 重复执行指定操作。
    /// </summary>
    /// <param name="value">重复次数。</param>
    /// <param name="action">要执行的操作。</param>
    public static void Repeat(this int value, Action action)
    {
        if (action == null || value <= 0) return;
        for (int i = 0; i < value; i++) action();
    }

    /// <summary>
    /// 重复执行指定操作（带索引）。
    /// </summary>
    /// <param name="value">重复次数。</param>
    /// <param name="action">要执行的操作，参数为索引。</param>
    public static void Repeat(this int value, Action<int> action)
    {
        if (action == null || value <= 0) return;
        for (int i = 0; i < value; i++) action(i);
    }

    /// <summary>
    /// 生成从 0 到 value-1 的整数序列。
    /// </summary>
    /// <param name="value">序列长度。</param>
    /// <returns>整数序列。</returns>
    public static IEnumerable<int> Times(this int value)
    {
        for (int i = 0; i < value; i++) yield return i;
    }

    /// <summary>
    /// 生成从 1 到 value 的整数序列。
    /// </summary>
    /// <param name="value">序列结束值。</param>
    /// <returns>整数序列。</returns>
    public static IEnumerable<int> To(this int value)
    {
        for (int i = 1; i <= value; i++) yield return i;
    }

    /// <summary>
    /// 生成从 value 到 end 的整数序列。
    /// </summary>
    /// <param name="value">起始值。</param>
    /// <param name="end">结束值。</param>
    /// <returns>整数序列。</returns>
    public static IEnumerable<int> To(this int value, int end)
    {
        int step = value <= end ? 1 : -1;
        for (int i = value; step > 0 ? i <= end : i >= end; i += step)
            yield return i;
    }

    /// <summary>
    /// 生成从 value 到 end 的整数序列（指定步长）。
    /// </summary>
    /// <param name="value">起始值。</param>
    /// <param name="end">结束值。</param>
    /// <param name="step">步长。</param>
    /// <returns>整数序列。</returns>
    public static IEnumerable<int> StepTo(this int value, int end, int step)
    {
        if (step == 0) yield break;
        bool ascending = step > 0;
        for (int i = value; ascending ? i <= end : i >= end; i += step)
            yield return i;
    }

    #endregion
}
