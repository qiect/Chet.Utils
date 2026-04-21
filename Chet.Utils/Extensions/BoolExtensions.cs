namespace Chet.Utils
{
    /// <summary>
    /// bool 扩展方法类，提供常用的判断、转换、格式化、运算等功能。
    /// </summary>
    /// <remarks>
    /// <para>本类提供丰富的布尔扩展方法，涵盖以下功能：</para>
    /// <list type="bullet">
    ///   <item><description>基础判断：IsTrue、IsFalse</description></item>
    ///   <item><description>逻辑运算：Not、And、Or、Xor、Xnor</description></item>
    ///   <item><description>类型转换：ToInt、ToLong、ToFloat、ToDouble、ToDecimal、ToByte、ToShort</description></item>
    ///   <item><description>字符串转换：ToStringValue、ToChineseString、ToYesNo、ToOnOff、ToYN、ToOneZero</description></item>
    ///   <item><description>自定义转换：ToCustomString、ToValue、ToEnum</description></item>
    ///   <item><description>特殊功能：ToNullable、ToReverseString、ToReverseChineseString</description></item>
    /// </list>
    /// </remarks>
    public static class BoolExtensions
    {
        #region 基础判断

        /// <summary>
        /// 判断 bool 是否为 true。
        /// </summary>
        /// <param name="value">待判断的 bool。</param>
        /// <returns>如果值为 true 返回 true；否则返回 false。</returns>
        /// <example>
        /// <code>
        /// bool value1 = true;
        /// bool value2 = false;
        /// 
        /// bool result1 = value1.IsTrue(); // true
        /// bool result2 = value2.IsTrue(); // false
        /// </code>
        /// </example>
        public static bool IsTrue(this bool value) => value;

        /// <summary>
        /// 判断 bool 是否为 false。
        /// </summary>
        /// <param name="value">待判断的 bool。</param>
        /// <returns>如果值为 false 返回 true；否则返回 false。</returns>
        /// <example>
        /// <code>
        /// bool value1 = false;
        /// bool value2 = true;
        /// 
        /// bool result1 = value1.IsFalse(); // true
        /// bool result2 = value2.IsFalse(); // false
        /// </code>
        /// </example>
        public static bool IsFalse(this bool value) => !value;

        #endregion

        #region 逻辑运算

        /// <summary>
        /// 对 bool 取反。
        /// </summary>
        /// <param name="value">待处理的 bool。</param>
        /// <returns>取反后的值。</returns>
        /// <example>
        /// <code>
        /// bool value1 = true;
        /// bool value2 = false;
        /// 
        /// bool result1 = value1.Not(); // false
        /// bool result2 = value2.Not(); // true
        /// </code>
        /// </example>
        public static bool Not(this bool value) => !value;

        /// <summary>
        /// 对两个 bool 进行与运算。
        /// </summary>
        /// <param name="value">第一个 bool。</param>
        /// <param name="other">第二个 bool。</param>
        /// <returns>与运算结果。</returns>
        /// <example>
        /// <code>
        /// bool value1 = true;
        /// bool value2 = false;
        /// 
        /// bool result = value1.And(value2); // false
        /// </code>
        /// </example>
        public static bool And(this bool value, bool other) => value && other;

        /// <summary>
        /// 对两个 bool 进行或运算。
        /// </summary>
        /// <param name="value">第一个 bool。</param>
        /// <param name="other">第二个 bool。</param>
        /// <returns>或运算结果。</returns>
        /// <example>
        /// <code>
        /// bool value1 = true;
        /// bool value2 = false;
        /// 
        /// bool result = value1.Or(value2); // true
        /// </code>
        /// </example>
        public static bool Or(this bool value, bool other) => value || other;

        /// <summary>
        /// 对两个 bool 进行异或运算。
        /// </summary>
        /// <param name="value">第一个 bool。</param>
        /// <param name="other">第二个 bool。</param>
        /// <returns>异或运算结果。</returns>
        /// <example>
        /// <code>
        /// bool value1 = true;
        /// bool value2 = false;
        /// 
        /// bool result = value1.Xor(value2); // true
        /// </code>
        /// </example>
        public static bool Xor(this bool value, bool other) => value ^ other;

        /// <summary>
        /// 对两个 bool 进行同或运算（等价于相等判断）。
        /// </summary>
        /// <param name="value">第一个 bool。</param>
        /// <param name="other">第二个 bool。</param>
        /// <returns>同或运算结果。</returns>
        /// <example>
        /// <code>
        /// bool value1 = true;
        /// bool value2 = true;
        /// 
        /// bool result = value1.Xnor(value2); // true
        /// </code>
        /// </example>
        public static bool Xnor(this bool value, bool other) => value == other;

        /// <summary>
        /// 对两个 bool 进行与非运算。
        /// </summary>
        /// <param name="value">第一个 bool。</param>
        /// <param name="other">第二个 bool。</param>
        /// <returns>与非运算结果。</returns>
        /// <example>
        /// <code>
        /// bool value1 = true;
        /// bool value2 = false;
        /// 
        /// bool result = value1.Nand(value2); // true
        /// </code>
        /// </example>
        public static bool Nand(this bool value, bool other) => !(value && other);

        /// <summary>
        /// 对两个 bool 进行或非运算。
        /// </summary>
        /// <param name="value">第一个 bool。</param>
        /// <param name="other">第二个 bool。</param>
        /// <returns>或非运算结果。</returns>
        /// <example>
        /// <code>
        /// bool value1 = true;
        /// bool value2 = false;
        /// 
        /// bool result = value1.Nor(value2); // false
        /// </code>
        /// </example>
        public static bool Nor(this bool value, bool other) => !(value || other);

        #endregion

        #region 数值类型转换

        /// <summary>
        /// 将 bool 转换为 int（true 为 1，false 为 0）。
        /// </summary>
        /// <param name="value">待转换的 bool。</param>
        /// <returns>转换后的 int 值。</returns>
        /// <example>
        /// <code>
        /// bool value1 = true;
        /// bool value2 = false;
        /// 
        /// int result1 = value1.ToInt(); // 1
        /// int result2 = value2.ToInt(); // 0
        /// </code>
        /// </example>
        public static int ToInt(this bool value) => value ? 1 : 0;

        /// <summary>
        /// 将 bool 转换为 long（true 为 1，false 为 0）。
        /// </summary>
        /// <param name="value">待转换的 bool。</param>
        /// <returns>转换后的 long 值。</returns>
        /// <example>
        /// <code>
        /// bool value = true;
        /// long result = value.ToLong(); // 1
        /// </code>
        /// </example>
        public static long ToLong(this bool value) => value ? 1L : 0L;

        /// <summary>
        /// 将 bool 转换为 byte（true 为 1，false 为 0）。
        /// </summary>
        /// <param name="value">待转换的 bool。</param>
        /// <returns>转换后的 byte 值。</returns>
        /// <example>
        /// <code>
        /// bool value = true;
        /// byte result = value.ToByte(); // 1
        /// </code>
        /// </example>
        public static byte ToByte(this bool value) => value ? (byte)1 : (byte)0;

        /// <summary>
        /// 将 bool 转换为 short（true 为 1，false 为 0）。
        /// </summary>
        /// <param name="value">待转换的 bool。</param>
        /// <returns>转换后的 short 值。</returns>
        /// <example>
        /// <code>
        /// bool value = true;
        /// short result = value.ToShort(); // 1
        /// </code>
        /// </example>
        public static short ToShort(this bool value) => value ? (short)1 : (short)0;

        /// <summary>
        /// 将 bool 转换为 float（true 为 1.0，false 为 0.0）。
        /// </summary>
        /// <param name="value">待转换的 bool。</param>
        /// <returns>转换后的 float 值。</returns>
        /// <example>
        /// <code>
        /// bool value = true;
        /// float result = value.ToFloat(); // 1.0f
        /// </code>
        /// </example>
        public static float ToFloat(this bool value) => value ? 1f : 0f;

        /// <summary>
        /// 将 bool 转换为 double（true 为 1.0，false 为 0.0）。
        /// </summary>
        /// <param name="value">待转换的 bool。</param>
        /// <returns>转换后的 double 值。</returns>
        /// <example>
        /// <code>
        /// bool value = true;
        /// double result = value.ToDouble(); // 1.0d
        /// </code>
        /// </example>
        public static double ToDouble(this bool value) => value ? 1d : 0d;

        /// <summary>
        /// 将 bool 转换为 decimal（true 为 1.0，false 为 0.0）。
        /// </summary>
        /// <param name="value">待转换的 bool。</param>
        /// <returns>转换后的 decimal 值。</returns>
        /// <example>
        /// <code>
        /// bool value = true;
        /// decimal result = value.ToDecimal(); // 1.0m
        /// </code>
        /// </example>
        public static decimal ToDecimal(this bool value) => value ? 1m : 0m;

        #endregion

        #region 字符串转换

        /// <summary>
        /// 将 bool 转换为字符串（"True"/"False"）。
        /// </summary>
        /// <param name="value">待转换的 bool。</param>
        /// <returns>字符串表示。</returns>
        /// <example>
        /// <code>
        /// bool value1 = true;
        /// bool value2 = false;
        /// 
        /// string result1 = value1.ToStringValue(); // "True"
        /// string result2 = value2.ToStringValue(); // "False"
        /// </code>
        /// </example>
        public static string ToStringValue(this bool value) => value.ToString();

        /// <summary>
        /// 将 bool 转换为小写字符串（"true"/"false"）。
        /// </summary>
        /// <param name="value">待转换的 bool。</param>
        /// <returns>小写字符串表示。</returns>
        /// <example>
        /// <code>
        /// bool value = true;
        /// string result = value.ToLowerString(); // "true"
        /// </code>
        /// </example>
        public static string ToLowerString(this bool value) => value.ToString().ToLower();

        /// <summary>
        /// 将 bool 转换为中文字符串（"是"/"否"）。
        /// </summary>
        /// <param name="value">待转换的 bool。</param>
        /// <returns>中文字符串表示。</returns>
        /// <example>
        /// <code>
        /// bool value1 = true;
        /// bool value2 = false;
        /// 
        /// string result1 = value1.ToChineseString(); // "是"
        /// string result2 = value2.ToChineseString(); // "否"
        /// </code>
        /// </example>
        public static string ToChineseString(this bool value) => value ? "是" : "否";

        /// <summary>
        /// 将 bool 转换为 Yes/No 字符串。
        /// </summary>
        /// <param name="value">待转换的 bool。</param>
        /// <returns>Yes/No 字符串。</returns>
        /// <example>
        /// <code>
        /// bool value1 = true;
        /// bool value2 = false;
        /// 
        /// string result1 = value1.ToYesNo(); // "Yes"
        /// string result2 = value2.ToYesNo(); // "No"
        /// </code>
        /// </example>
        public static string ToYesNo(this bool value) => value ? "Yes" : "No";

        /// <summary>
        /// 将 bool 转换为 On/Off 字符串。
        /// </summary>
        /// <param name="value">待转换的 bool。</param>
        /// <returns>On/Off 字符串。</returns>
        /// <example>
        /// <code>
        /// bool value1 = true;
        /// bool value2 = false;
        /// 
        /// string result1 = value1.ToOnOff(); // "On"
        /// string result2 = value2.ToOnOff(); // "Off"
        /// </code>
        /// </example>
        public static string ToOnOff(this bool value) => value ? "On" : "Off";

        /// <summary>
        /// 将 bool 转换为 Y/N 字符串。
        /// </summary>
        /// <param name="value">待转换的 bool。</param>
        /// <returns>Y/N 字符串。</returns>
        /// <example>
        /// <code>
        /// bool value1 = true;
        /// bool value2 = false;
        /// 
        /// string result1 = value1.ToYN(); // "Y"
        /// string result2 = value2.ToYN(); // "N"
        /// </code>
        /// </example>
        public static string ToYN(this bool value) => value ? "Y" : "N";

        /// <summary>
        /// 将 bool 转换为 1/0 字符串。
        /// </summary>
        /// <param name="value">待转换的 bool。</param>
        /// <returns>1/0 字符串。</returns>
        /// <example>
        /// <code>
        /// bool value1 = true;
        /// bool value2 = false;
        /// 
        /// string result1 = value1.ToOneZero(); // "1"
        /// string result2 = value2.ToOneZero(); // "0"
        /// </code>
        /// </example>
        public static string ToOneZero(this bool value) => value ? "1" : "0";

        /// <summary>
        /// 将 bool 转换为反向字符串（"False"/"True"）。
        /// </summary>
        /// <param name="value">待转换的 bool。</param>
        /// <returns>反向字符串表示。</returns>
        /// <example>
        /// <code>
        /// bool value = true;
        /// string result = value.ToReverseString(); // "False"
        /// </code>
        /// </example>
        public static string ToReverseString(this bool value) => (!value).ToString();

        /// <summary>
        /// 将 bool 转换为反向中文字符串（"否"/"是"）。
        /// </summary>
        /// <param name="value">待转换的 bool。</param>
        /// <returns>反向中文字符串表示。</returns>
        /// <example>
        /// <code>
        /// bool value = true;
        /// string result = value.ToReverseChineseString(); // "否"
        /// </code>
        /// </example>
        public static string ToReverseChineseString(this bool value) => value ? "否" : "是";

        /// <summary>
        /// 将 bool 转换为启用/禁用字符串。
        /// </summary>
        /// <param name="value">待转换的 bool。</param>
        /// <returns>启用/禁用字符串。</returns>
        /// <example>
        /// <code>
        /// bool value1 = true;
        /// bool value2 = false;
        /// 
        /// string result1 = value1.ToEnabledDisabled(); // "启用"
        /// string result2 = value2.ToEnabledDisabled(); // "禁用"
        /// </code>
        /// </example>
        public static string ToEnabledDisabled(this bool value) => value ? "启用" : "禁用";

        /// <summary>
        /// 将 bool 转换为成功/失败字符串。
        /// </summary>
        /// <param name="value">待转换的 bool。</param>
        /// <returns>成功/失败字符串。</returns>
        /// <example>
        /// <code>
        /// bool value1 = true;
        /// bool value2 = false;
        /// 
        /// string result1 = value1.ToSuccessFailed(); // "成功"
        /// string result2 = value2.ToSuccessFailed(); // "失败"
        /// </code>
        /// </example>
        public static string ToSuccessFailed(this bool value) => value ? "成功" : "失败";

        #endregion

        #region 自定义转换

        /// <summary>
        /// 将 bool 转换为自定义字符串。
        /// </summary>
        /// <param name="value">待转换的 bool。</param>
        /// <param name="trueString">true 时的字符串。</param>
        /// <param name="falseString">false 时的字符串。</param>
        /// <returns>自定义字符串。</returns>
        /// <example>
        /// <code>
        /// bool value = true;
        /// string result = value.ToCustomString("通过", "不通过"); // "通过"
        /// </code>
        /// </example>
        public static string ToCustomString(this bool value, string trueString, string falseString) =>
            value ? trueString : falseString;

        /// <summary>
        /// 将 bool 转换为指定类型的值。
        /// </summary>
        /// <typeparam name="T">目标类型。</typeparam>
        /// <param name="value">待转换的 bool。</param>
        /// <param name="trueValue">true 时的值。</param>
        /// <param name="falseValue">false 时的值。</param>
        /// <returns>转换后的值。</returns>
        /// <example>
        /// <code>
        /// bool value = true;
        /// int result = value.ToValue(100, 0); // 100
        /// </code>
        /// </example>
        public static T ToValue<T>(this bool value, T trueValue, T falseValue) =>
            value ? trueValue : falseValue;

        /// <summary>
        /// 将 bool 转换为枚举值。
        /// </summary>
        /// <typeparam name="TEnum">目标枚举类型。</typeparam>
        /// <param name="value">待转换的 bool。</param>
        /// <param name="trueEnum">true 时的枚举值。</param>
        /// <param name="falseEnum">false 时的枚举值。</param>
        /// <returns>转换后的枚举值。</returns>
        /// <example>
        /// <code>
        /// bool value = true;
        /// Status result = value.ToEnum(Status.Active, Status.Inactive); // Status.Active
        /// </code>
        /// </example>
        public static TEnum ToEnum<TEnum>(this bool value, TEnum trueEnum, TEnum falseEnum) where TEnum : Enum =>
            value ? trueEnum : falseEnum;

        /// <summary>
        /// 将 bool 转换为可空 bool。
        /// </summary>
        /// <param name="value">待转换的 bool。</param>
        /// <param name="nullable">是否返回 null，默认为 false。</param>
        /// <returns>可空 bool 值。</returns>
        /// <example>
        /// <code>
        /// bool value = true;
        /// 
        /// bool? result1 = value.ToNullable(); // true
        /// bool? result2 = value.ToNullable(true); // null
        /// </code>
        /// </example>
        public static bool? ToNullable(this bool value, bool nullable = false) =>
            nullable ? (bool?)null : value;

        #endregion

        #region 条件执行

        /// <summary>
        /// 如果 bool 为 true，则执行指定操作。
        /// </summary>
        /// <param name="value">待判断的 bool。</param>
        /// <param name="action">要执行的操作。</param>
        /// <example>
        /// <code>
        /// bool value = true;
        /// value.IfTrue(() => Console.WriteLine("值为 true"));
        /// </code>
        /// </example>
        public static void IfTrue(this bool value, Action action)
        {
            if (value && action != null)
                action();
        }

        /// <summary>
        /// 如果 bool 为 false，则执行指定操作。
        /// </summary>
        /// <param name="value">待判断的 bool。</param>
        /// <param name="action">要执行的操作。</param>
        /// <example>
        /// <code>
        /// bool value = false;
        /// value.IfFalse(() => Console.WriteLine("值为 false"));
        /// </code>
        /// </example>
        public static void IfFalse(this bool value, Action action)
        {
            if (!value && action != null)
                action();
        }

        /// <summary>
        /// 根据 bool 值执行不同的操作。
        /// </summary>
        /// <param name="value">待判断的 bool。</param>
        /// <param name="trueAction">true 时执行的操作。</param>
        /// <param name="falseAction">false 时执行的操作。</param>
        /// <example>
        /// <code>
        /// bool value = true;
        /// value.IfElse(
        ///     () => Console.WriteLine("值为 true"),
        ///     () => Console.WriteLine("值为 false")
        /// );
        /// </code>
        /// </example>
        public static void IfElse(this bool value, Action trueAction, Action falseAction)
        {
            if (value)
                trueAction?.Invoke();
            else
                falseAction?.Invoke();
        }

        /// <summary>
        /// 根据 bool 值返回不同的结果。
        /// </summary>
        /// <typeparam name="T">返回值类型。</typeparam>
        /// <param name="value">待判断的 bool。</param>
        /// <param name="trueValue">true 时返回的值。</param>
        /// <param name="falseValue">false 时返回的值。</param>
        /// <returns>根据 bool 值返回的结果。</returns>
        /// <example>
        /// <code>
        /// bool value = true;
        /// string result = value.Match("是", "否"); // "是"
        /// </code>
        /// </example>
        public static T Match<T>(this bool value, T trueValue, T falseValue) =>
            value ? trueValue : falseValue;

        /// <summary>
        /// 根据 bool 值通过函数返回不同的结果。
        /// </summary>
        /// <typeparam name="T">返回值类型。</typeparam>
        /// <param name="value">待判断的 bool。</param>
        /// <param name="trueFunc">true 时执行的函数。</param>
        /// <param name="falseFunc">false 时执行的函数。</param>
        /// <returns>根据 bool 值返回的结果。</returns>
        /// <example>
        /// <code>
        /// bool value = true;
        /// int result = value.Match(() => 100, () => 0); // 100
        /// </code>
        /// </example>
        public static T Match<T>(this bool value, Func<T> trueFunc, Func<T> falseFunc) =>
            value ? trueFunc() : falseFunc();

        #endregion
    }
}
