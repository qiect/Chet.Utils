using System.Globalization;

namespace Chet.Utils
{
    /// <summary>
    /// decimal 扩展方法类，提供常用的判断、转换、运算、格式化等功能。
    /// </summary>
    public static class DecimalExtensions
    {
        /// <summary>
        /// 判断 decimal 是否为零。
        /// </summary>
        /// <param name="value">待判断的 decimal。</param>
        public static bool IsZero(this decimal value) => value == 0m;

        /// <summary>
        /// 判断 decimal 是否为正数。
        /// </summary>
        /// <param name="value">待判断的 decimal。</param>
        public static bool IsPositive(this decimal value) => value > 0m;

        /// <summary>
        /// 判断 decimal 是否为负数。
        /// </summary>
        /// <param name="value">待判断的 decimal。</param>
        public static bool IsNegative(this decimal value) => value < 0m;

        /// <summary>
        /// 判断 decimal 是否为整数。
        /// </summary>
        /// <param name="value">待判断的 decimal。</param>
        public static bool IsInteger(this decimal value) => decimal.Truncate(value) == value;

        /// <summary>
        /// 判断 decimal 是否为偶数。
        /// </summary>
        /// <param name="value">待判断的 decimal。</param>
        public static bool IsEven(this decimal value) => IsInteger(value) && ((long)value % 2 == 0);

        /// <summary>
        /// 判断 decimal 是否为奇数。
        /// </summary>
        /// <param name="value">待判断的 decimal。</param>
        public static bool IsOdd(this decimal value) => IsInteger(value) && ((long)value % 2 != 0);

        /// <summary>
        /// decimal 四舍五入到指定小数位。
        /// </summary>
        /// <param name="value">待处理的 decimal。</param>
        /// <param name="digits">保留的小数位数。</param>
        public static decimal Round(this decimal value, int digits = 2) =>
            Math.Round(value, digits, MidpointRounding.AwayFromZero);

        /// <summary>
        /// decimal 截断到指定小数位（向零取整）。
        /// </summary>
        /// <param name="value">待处理的 decimal。</param>
        /// <param name="digits">保留的小数位数。</param>
        public static decimal Truncate(this decimal value, int digits = 2)
        {
            decimal factor = (decimal)Math.Pow(10, digits);
            return Math.Truncate(value * factor) / factor;
        }

        /// <summary>
        /// decimal 取绝对值。
        /// </summary>
        /// <param name="value">待处理的 decimal。</param>
        public static decimal Abs(this decimal value) => Math.Abs(value);

        /// <summary>
        /// decimal 转为字符串，保留指定小数位。
        /// </summary>
        /// <param name="value">待处理的 decimal。</param>
        /// <param name="digits">保留的小数位数。</param>
        public static string ToFixedString(this decimal value, int digits = 2) =>
            value.ToString($"F{digits}");

        /// <summary>
        /// decimal 转为货币格式字符串（如 "￥1,234.56"）。
        /// </summary>
        /// <param name="value">待处理的 decimal。</param>
        /// <param name="culture">区域信息，默认中文。</param>
        public static string ToCurrencyString(this decimal value, string culture = "zh-CN") =>
            value.ToString("C", new CultureInfo(culture));

        /// <summary>
        /// decimal 转为百分比字符串（如 "12.34%"）。
        /// </summary>
        /// <param name="value">待处理的 decimal。</param>
        /// <param name="digits">保留的小数位数。</param>
        public static string ToPercentString(this decimal value, int digits = 2) =>
            (value * 100).ToString($"F{digits}") + "%";

        /// <summary>
        /// decimal 转为大写金额（中文）。
        /// </summary>
        /// <param name="value">待处理的 decimal。</param>
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

            // 整数部分分组，每4位一组
            string intStr = integerPart.ToString();
            int intLen = intStr.Length;
            int groupCount = (intLen + 3) / 4;
            string result = "";
            bool needZero = false; // 是否需要补零
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
                    // 只有后面有非零组才补零
                    if (result.Length > 0 && !result.StartsWith(cnNums[0]))
                        result = cnNums[0] + result;
                    needZero = false;
                }
            }
            // 清理多余零
            while (result.Contains("零零")) result = result.Replace("零零", "零");
            result = result.TrimEnd('零');
            if (result == "") result = cnNums[0];
            result += cnIntLast;

            // 小数部分
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
            // 处理零元
            result = result.Replace("零元", "元");
            if (result.StartsWith("零")) result = result.Substring(1);
            return result;
        }

        /// <summary>
        /// decimal 转为科学计数法字符串。
        /// </summary>
        /// <param name="value">待处理的 decimal。</param>
        /// <param name="digits">保留的小数位数。</param>
        public static string ToScientificString(this decimal value, int digits = 2) =>
            value.ToString($"E{digits}");

        /// <summary>
        /// decimal 取最大值。
        /// </summary>
        /// <param name="value">第一个值。</param>
        /// <param name="other">第二个值。</param>
        public static decimal Max(this decimal value, decimal other) => Math.Max(value, other);

        /// <summary>
        /// decimal 取最小值。
        /// </summary>
        /// <param name="value">第一个值。</param>
        /// <param name="other">第二个值。</param>
        public static decimal Min(this decimal value, decimal other) => Math.Min(value, other);

        /// <summary>
        /// decimal 是否在指定范围内（包含边界）。
        /// </summary>
        /// <param name="value">待判断的 decimal。</param>
        /// <param name="min">最小值。</param>
        /// <param name="max">最大值。</param>
        public static bool IsBetween(this decimal value, decimal min, decimal max) =>
            value >= min && value <= max;

        /// <summary>
        /// decimal 保证在指定范围内，超出则取边界值。
        /// </summary>
        /// <param name="value">待处理的 decimal。</param>
        /// <param name="min">最小值。</param>
        /// <param name="max">最大值。</param>
        public static decimal Clamp(this decimal value, decimal min, decimal max) =>
            value < min ? min : (value > max ? max : value);

        /// <summary>
        /// decimal 转为 int，四舍五入。
        /// </summary>
        /// <param name="value">待转换的 decimal。</param>
        public static int ToInt(this decimal value) => (int)Math.Round(value, 0, MidpointRounding.AwayFromZero);

        /// <summary>
        /// decimal 转为 double。
        /// </summary>
        /// <param name="value">待转换的 decimal。</param>
        public static double ToDouble(this decimal value) => (double)value;

        /// <summary>
        /// decimal 转为 float。
        /// </summary>
        /// <param name="value">待转换的 decimal。</param>
        public static float ToFloat(this decimal value) => (float)value;

        /// <summary>
        /// decimal 转为 long，四舍五入。
        /// </summary>
        /// <param name="value">待转换的 decimal。</param>
        public static long ToLong(this decimal value) => (long)Math.Round(value, 0, MidpointRounding.AwayFromZero);

        /// <summary>
        /// decimal 转为 bool（非零为 true）。
        /// </summary>
        /// <param name="value">待转换的 decimal。</param>
        public static bool ToBool(this decimal value) => value != 0m;

        /// <summary>
        /// decimal 乘法。
        /// </summary>
        /// <param name="value">第一个值。</param>
        /// <param name="other">第二个值。</param>
        public static decimal Multiply(this decimal value, decimal other) => value * other;

        /// <summary>
        /// decimal 除法，除数为零时返回零。
        /// </summary>
        /// <param name="value">被除数。</param>
        /// <param name="other">除数。</param>
        public static decimal DivideSafe(this decimal value, decimal other) =>
            other == 0m ? 0m : value / other;

        /// <summary>
        /// decimal 加法。
        /// </summary>
        /// <param name="value">第一个值。</param>
        /// <param name="other">第二个值。</param>
        public static decimal Add(this decimal value, decimal other) => value + other;

        /// <summary>
        /// decimal 减法。
        /// </summary>
        /// <param name="value">第一个值。</param>
        /// <param name="other">第二个值。</param>
        public static decimal Subtract(this decimal value, decimal other) => value - other;

        /// <summary>
        /// decimal 求余。
        /// </summary>
        /// <param name="value">被除数。</param>
        /// <param name="other">除数。</param>
        public static decimal Mod(this decimal value, decimal other) => other == 0m ? 0m : value % other;

        /// <summary>
        /// decimal 求幂。
        /// </summary>
        /// <param name="value">底数。</param>
        /// <param name="power">指数。</param>
        public static decimal Pow(this decimal value, int power) => (decimal)Math.Pow((double)value, power);

        /// <summary>
        /// decimal 求平方根。
        /// </summary>
        /// <param name="value">待处理的 decimal。</param>
        public static decimal Sqrt(this decimal value) => (decimal)Math.Sqrt((double)value);

        /// <summary>
        /// decimal 求绝对差值。
        /// </summary>
        /// <param name="value">第一个值。</param>
        /// <param name="other">第二个值。</param>
        public static decimal AbsDiff(this decimal value, decimal other) => Math.Abs(value - other);
    }
}