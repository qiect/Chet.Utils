using System.Globalization;

namespace Chet.Utils
{
    /// <summary>
    /// int 扩展方法类，提供常用的判断、转换、运算、格式化等功能。
    /// </summary>
    public static class IntExtensions
    {
        /// <summary>
        /// 判断 int 是否为零。
        /// </summary>
        /// <param name="value">待判断的 int。</param>
        public static bool IsZero(this int value) => value == 0;

        /// <summary>
        /// 判断 int 是否为正数。
        /// </summary>
        /// <param name="value">待判断的 int。</param>
        public static bool IsPositive(this int value) => value > 0;

        /// <summary>
        /// 判断 int 是否为负数。
        /// </summary>
        /// <param name="value">待判断的 int。</param>
        public static bool IsNegative(this int value) => value < 0;

        /// <summary>
        /// 判断 int 是否为偶数。
        /// </summary>
        /// <param name="value">待判断的 int。</param>
        public static bool IsEven(this int value) => value % 2 == 0;

        /// <summary>
        /// 判断 int 是否为奇数。
        /// </summary>
        /// <param name="value">待判断的 int。</param>
        public static bool IsOdd(this int value) => value % 2 != 0;

        /// <summary>
        /// 判断 int 是否在指定范围内（包含边界）。
        /// </summary>
        /// <param name="value">待判断的 int。</param>
        /// <param name="min">最小值。</param>
        /// <param name="max">最大值。</param>
        public static bool IsBetween(this int value, int min, int max) => value >= min && value <= max;

        /// <summary>
        /// 保证 int 在指定范围内，超出则取边界值。
        /// </summary>
        /// <param name="value">待处理的 int。</param>
        /// <param name="min">最小值。</param>
        /// <param name="max">最大值。</param>
        public static int Clamp(this int value, int min, int max) => value < min ? min : (value > max ? max : value);

        /// <summary>
        /// int 转为 bool（非零为 true）。
        /// </summary>
        /// <param name="value">待转换的 int。</param>
        public static bool ToBool(this int value) => value != 0;

        /// <summary>
        /// int 转为 double。
        /// </summary>
        /// <param name="value">待转换的 int。</param>
        public static double ToDouble(this int value) => (double)value;

        /// <summary>
        /// int 转为 float。
        /// </summary>
        /// <param name="value">待转换的 int。</param>
        public static float ToFloat(this int value) => (float)value;

        /// <summary>
        /// int 转为 long。
        /// </summary>
        /// <param name="value">待转换的 int。</param>
        public static long ToLong(this int value) => (long)value;

        /// <summary>
        /// int 转为 decimal。
        /// </summary>
        /// <param name="value">待转换的 int。</param>
        public static decimal ToDecimal(this int value) => (decimal)value;

        /// <summary>
        /// int 转为字符串，支持指定格式。
        /// </summary>
        /// <param name="value">待转换的 int。</param>
        /// <param name="format">格式字符串，如 "D4"。</param>
        public static string ToStringFormat(this int value, string format = null) =>
            format == null ? value.ToString() : value.ToString(format);

        /// <summary>
        /// int 转为货币格式字符串（如 "￥1,234"）。
        /// </summary>
        /// <param name="value">待转换的 int。</param>
        /// <param name="culture">区域信息，默认中文。</param>
        public static string ToCurrencyString(this int value, string culture = "zh-CN") =>
            value.ToString("C0", new CultureInfo(culture));

        /// <summary>
        /// int 转为百分比字符串（如 "12%"）。
        /// </summary>
        /// <param name="value">待转换的 int。</param>
        public static string ToPercentString(this int value) => value.ToString() + "%";

        /// <summary>
        /// int 转为中文大写金额（仅整数）。
        /// </summary>
        /// <param name="value">待转换的 int。</param>
        public static string ToChineseUpper(this int value)
        {
            string[] cnNums = { "零", "壹", "贰", "叁", "肆", "伍", "陆", "柒", "捌", "玖" };
            string[] cnIntRadice = { "", "拾", "佰", "仟" };
            string[] cnIntUnits = { "", "万", "亿", "兆" };
            string cnIntLast = "元";
            if (value == 0) return cnNums[0] + cnIntLast;
            string intStr = value.ToString();
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
            if (result.StartsWith("零")) result = result.Substring(1);
            return result;
        }

        /// <summary>
        /// int 求绝对值。
        /// </summary>
        /// <param name="value">待处理的 int。</param>
        public static int Abs(this int value) => Math.Abs(value);

        /// <summary>
        /// int 求最大值。
        /// </summary>
        /// <param name="value">第一个值。</param>
        /// <param name="other">第二个值。</param>
        public static int Max(this int value, int other) => Math.Max(value, other);

        /// <summary>
        /// int 求最小值。
        /// </summary>
        /// <param name="value">第一个值。</param>
        /// <param name="other">第二个值。</param>
        public static int Min(this int value, int other) => Math.Min(value, other);

        /// <summary>
        /// int 加法。
        /// </summary>
        /// <param name="value">第一个值。</param>
        /// <param name="other">第二个值。</param>
        public static int Add(this int value, int other) => value + other;

        /// <summary>
        /// int 减法。
        /// </summary>
        /// <param name="value">第一个值。</param>
        /// <param name="other">第二个值。</param>
        public static int Subtract(this int value, int other) => value - other;

        /// <summary>
        /// int 乘法。
        /// </summary>
        /// <param name="value">第一个值。</param>
        /// <param name="other">第二个值。</param>
        public static int Multiply(this int value, int other) => value * other;

        /// <summary>
        /// int 除法，除数为零时返回零。
        /// </summary>
        /// <param name="value">被除数。</param>
        /// <param name="other">除数。</param>
        public static int DivideSafe(this int value, int other) => other == 0 ? 0 : value / other;

        /// <summary>
        /// int 求余。
        /// </summary>
        /// <param name="value">被除数。</param>
        /// <param name="other">除数。</param>
        public static int Mod(this int value, int other) => other == 0 ? 0 : value % other;

        /// <summary>
        /// int 求幂。
        /// </summary>
        /// <param name="value">底数。</param>
        /// <param name="power">指数。</param>
        public static int Pow(this int value, int power) => (int)Math.Pow(value, power);

        /// <summary>
        /// int 求绝对差值。
        /// </summary>
        /// <param name="value">第一个值。</param>
        /// <param name="other">第二个值。</param>
        public static int AbsDiff(this int value, int other) => Math.Abs(value - other);

        /// <summary>
        /// int 转为十六进制字符串。
        /// </summary>
        /// <param name="value">待转换的 int。</param>
        public static string ToHexString(this int value) => value.ToString("X");

        /// <summary>
        /// int 转为二进制字符串。
        /// </summary>
        /// <param name="value">待转换的 int。</param>
        public static string ToBinaryString(this int value) => Convert.ToString(value, 2);

        /// <summary>
        /// int 转为八进制字符串。
        /// </summary>
        /// <param name="value">待转换的 int。</param>
        public static string ToOctalString(this int value) => Convert.ToString(value, 8);

        /// <summary>
        /// int 转为罗马数字字符串（1~3999）。
        /// </summary>
        /// <param name="value">待转换的 int。</param>
        public static string ToRomanString(this int value)
        {
            if (value < 1 || value > 3999) return value.ToString();
            var map = new[]
            {
                new { Value = 1000, Numeral = "M" },
                new { Value = 900, Numeral = "CM" },
                new { Value = 500, Numeral = "D" },
                new { Value = 400, Numeral = "CD" },
                new { Value = 100, Numeral = "C" },
                new { Value = 90, Numeral = "XC" },
                new { Value = 50, Numeral = "L" },
                new { Value = 40, Numeral = "XL" },
                new { Value = 10, Numeral = "X" },
                new { Value = 9, Numeral = "IX" },
                new { Value = 5, Numeral = "V" },
                new { Value = 4, Numeral = "IV" },
                new { Value = 1, Numeral = "I" }
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
        /// int 转为星期中文（0~6，周日~周六）。
        /// </summary>
        /// <param name="value">待转换的 int。</param>
        public static string ToChineseWeekday(this int value)
        {
            var weekDays = new[] { "星期日", "星期一", "星期二", "星期三", "星期四", "星期五", "星期六" };
            return value >= 0 && value < 7 ? weekDays[value] : value.ToString();
        }

        /// <summary>
        /// int 转为英文星期（0~6，Sunday~Saturday）。
        /// </summary>
        /// <param name="value">待转换的 int。</param>
        public static string ToEnglishWeekday(this int value)
        {
            var weekDays = new[] { "Sunday", "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday" };
            return value >= 0 && value < 7 ? weekDays[value] : value.ToString();
        }

        /// <summary>
        /// int 转为枚举类型。
        /// </summary>
        /// <typeparam name="TEnum">目标枚举类型。</typeparam>
        /// <param name="value">待转换的 int。</param>
        public static TEnum ToEnum<TEnum>(this int value) where TEnum : Enum =>
            Enum.IsDefined(typeof(TEnum), value) ? (TEnum)Enum.ToObject(typeof(TEnum), value) : default;

        /// <summary>
        /// int 重复指定操作。
        /// </summary>
        /// <param name="value">重复次数。</param>
        /// <param name="action">要执行的操作。</param>
        public static void Repeat(this int value, Action action)
        {
            if (action == null || value <= 0) return;
            for (int i = 0; i < value; i++) action();
        }

        /// <summary>
        /// int 重复指定操作（带索引）。
        /// </summary>
        /// <param name="value">重复次数。</param>
        /// <param name="action">要执行的操作，参数为索引。</param>
        public static void Repeat(this int value, Action<int> action)
        {
            if (action == null || value <= 0) return;
            for (int i = 0; i < value; i++) action(i);
        }
    }
}