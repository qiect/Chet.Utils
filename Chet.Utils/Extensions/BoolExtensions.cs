namespace Chet.Utils
{
    /// <summary>
    /// bool 扩展方法类，提供常用的判断、转换、格式化、运算等功能。
    /// </summary>
    public static class BoolExtensions
    {
        /// <summary>
        /// 判断 bool 是否为 true。
        /// </summary>
        /// <param name="value">待判断的 bool。</param>
        public static bool IsTrue(this bool value) => value;

        /// <summary>
        /// 判断 bool 是否为 false。
        /// </summary>
        /// <param name="value">待判断的 bool。</param>
        public static bool IsFalse(this bool value) => !value;

        /// <summary>
        /// bool 取反。
        /// </summary>
        /// <param name="value">待处理的 bool。</param>
        public static bool Not(this bool value) => !value;

        /// <summary>
        /// bool 转为 int（true 为 1，false 为 0）。
        /// </summary>
        /// <param name="value">待转换的 bool。</param>
        public static int ToInt(this bool value) => value ? 1 : 0;

        /// <summary>
        /// bool 转为字符串（"True"/"False"）。
        /// </summary>
        /// <param name="value">待转换的 bool。</param>
        public static string ToStringValue(this bool value) => value.ToString();

        /// <summary>
        /// bool 转为中文字符串（"是"/"否"）。
        /// </summary>
        /// <param name="value">待转换的 bool。</param>
        public static string ToChineseString(this bool value) => value ? "是" : "否";

        /// <summary>
        /// bool 转为自定义字符串。
        /// </summary>
        /// <param name="value">待转换的 bool。</param>
        /// <param name="trueString">true 时的字符串。</param>
        /// <param name="falseString">false 时的字符串。</param>
        public static string ToCustomString(this bool value, string trueString, string falseString) =>
            value ? trueString : falseString;

        /// <summary>
        /// bool 转为 Yes/No 字符串。
        /// </summary>
        /// <param name="value">待转换的 bool。</param>
        public static string ToYesNo(this bool value) => value ? "Yes" : "No";

        /// <summary>
        /// bool 转为 On/Off 字符串。
        /// </summary>
        /// <param name="value">待转换的 bool。</param>
        public static string ToOnOff(this bool value) => value ? "On" : "Off";

        /// <summary>
        /// bool 转为 1/0 字符串。
        /// </summary>
        /// <param name="value">待转换的 bool。</param>
        public static string ToOneZero(this bool value) => value ? "1" : "0";

        /// <summary>
        /// bool 转为 Y/N 字符串。
        /// </summary>
        /// <param name="value">待转换的 bool。</param>
        public static string ToYN(this bool value) => value ? "Y" : "N";

        /// <summary>
        /// bool 与另一个 bool 进行与运算。
        /// </summary>
        /// <param name="value">第一个 bool。</param>
        /// <param name="other">第二个 bool。</param>
        public static bool And(this bool value, bool other) => value && other;

        /// <summary>
        /// bool 与另一个 bool 进行或运算。
        /// </summary>
        /// <param name="value">第一个 bool。</param>
        /// <param name="other">第二个 bool。</param>
        public static bool Or(this bool value, bool other) => value || other;

        /// <summary>
        /// bool 与另一个 bool 进行异或运算。
        /// </summary>
        /// <param name="value">第一个 bool。</param>
        /// <param name="other">第二个 bool。</param>
        public static bool Xor(this bool value, bool other) => value ^ other;

        /// <summary>
        /// bool 与另一个 bool 进行同或运算（等价于相等）。
        /// </summary>
        /// <param name="value">第一个 bool。</param>
        /// <param name="other">第二个 bool。</param>
        public static bool Xnor(this bool value, bool other) => value == other;

        /// <summary>
        /// bool 转为枚举值（true 为第一个，false 为第二个）。
        /// </summary>
        /// <typeparam name="TEnum">目标枚举类型。</typeparam>
        /// <param name="value">待转换的 bool。</param>
        /// <param name="trueEnum">true 时的枚举值。</param>
        /// <param name="falseEnum">false 时的枚举值。</param>
        public static TEnum ToEnum<TEnum>(this bool value, TEnum trueEnum, TEnum falseEnum) where TEnum : Enum =>
            value ? trueEnum : falseEnum;

        /// <summary>
        /// bool 转为可空 bool（true/false/null）。
        /// </summary>
        /// <param name="value">待转换的 bool。</param>
        /// <param name="nullable">是否返回 null（默认 false）。</param>
        public static bool? ToNullable(this bool value, bool nullable = false) =>
            nullable ? (bool?)null : value;

        /// <summary>
        /// bool 转为指定类型（true/false 映射为任意类型）。
        /// </summary>
        /// <typeparam name="T">目标类型。</typeparam>
        /// <param name="value">待转换的 bool。</param>
        /// <param name="trueValue">true 时的值。</param>
        /// <param name="falseValue">false 时的值。</param>
        public static T ToValue<T>(this bool value, T trueValue, T falseValue) =>
            value ? trueValue : falseValue;

        /// <summary>
        /// bool 转为字节（true 为 1，false 为 0）。
        /// </summary>
        /// <param name="value">待转换的 bool。</param>
        public static byte ToByte(this bool value) => value ? (byte)1 : (byte)0;

        /// <summary>
        /// bool 转为短整型（true 为 1，false 为 0）。
        /// </summary>
        /// <param name="value">待转换的 bool。</param>
        public static short ToShort(this bool value) => value ? (short)1 : (short)0;

        /// <summary>
        /// bool 转为长整型（true 为 1，false 为 0）。
        /// </summary>
        /// <param name="value">待转换的 bool。</param>
        public static long ToLong(this bool value) => value ? 1L : 0L;

        /// <summary>
        /// bool 转为 float（true 为 1.0，false 为 0.0）。
        /// </summary>
        /// <param name="value">待转换的 bool。</param>
        public static float ToFloat(this bool value) => value ? 1f : 0f;

        /// <summary>
        /// bool 转为 double（true 为 1.0，false 为 0.0）。
        /// </summary>
        /// <param name="value">待转换的 bool。</param>
        public static double ToDouble(this bool value) => value ? 1d : 0d;

        /// <summary>
        /// bool 转为 decimal（true 为 1.0，false 为 0.0）。
        /// </summary>
        /// <param name="value">待转换的 bool。</param>
        public static decimal ToDecimal(this bool value) => value ? 1m : 0m;

        /// <summary>
        /// bool 转为反向字符串（"False"/"True"）。
        /// </summary>
        /// <param name="value">待转换的 bool。</param>
        public static string ToReverseString(this bool value) => (!value).ToString();

        /// <summary>
        /// bool 转为反向中文字符串（"否"/"是"）。
        /// </summary>
        /// <param name="value">待转换的 bool。</param>
        public static string ToReverseChineseString(this bool value) => value ? "否" : "是";
    }
}