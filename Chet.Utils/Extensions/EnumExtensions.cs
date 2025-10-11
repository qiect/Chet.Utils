using System.ComponentModel;

namespace Chet.Utils
{
    /// <summary>
    /// Enum 扩展方法类，提供常用的判断、转换、描述、枚举值操作等功能。
    /// </summary>
    public static class EnumExtensions
    {
        /// <summary>
        /// 判断枚举值是否定义在枚举类型中。
        /// </summary>
        /// <typeparam name="TEnum">枚举类型。</typeparam>
        /// <param name="value">待判断的枚举值。</param>
        public static bool IsDefined<TEnum>(this TEnum value) where TEnum : Enum =>
            Enum.IsDefined(typeof(TEnum), value);

        /// <summary>
        /// 获取枚举类型所有值列表。
        /// </summary>
        /// <typeparam name="TEnum">枚举类型。</typeparam>
        public static List<TEnum> GetValues<TEnum>() where TEnum : Enum =>
            Enum.GetValues(typeof(TEnum)).Cast<TEnum>().ToList();

        /// <summary>
        /// 获取枚举类型所有名称列表。
        /// </summary>
        /// <typeparam name="TEnum">枚举类型。</typeparam>
        public static List<string> GetNames<TEnum>() where TEnum : Enum =>
            Enum.GetNames(typeof(TEnum)).ToList();

        /// <summary>
        /// 枚举值转为 int。
        /// </summary>
        /// <typeparam name="TEnum">枚举类型。</typeparam>
        /// <param name="value">枚举值。</param>
        public static int ToInt<TEnum>(this TEnum value) where TEnum : Enum =>
            Convert.ToInt32(value);

        /// <summary>
        /// 枚举值转为 long。
        /// </summary>
        /// <typeparam name="TEnum">枚举类型。</typeparam>
        /// <param name="value">枚举值。</param>
        public static long ToLong<TEnum>(this TEnum value) where TEnum : Enum =>
            Convert.ToInt64(value);

        /// <summary>
        /// 枚举值转为字符串。
        /// </summary>
        /// <typeparam name="TEnum">枚举类型。</typeparam>
        /// <param name="value">枚举值。</param>
        public static string ToStringValue<TEnum>(this TEnum value) where TEnum : Enum =>
            value.ToString();

        /// <summary>
        /// 枚举值转为描述（DescriptionAttribute），无描述则返回名称。
        /// </summary>
        /// <typeparam name="TEnum">枚举类型。</typeparam>
        /// <param name="value">枚举值。</param>
        public static string GetDescription<TEnum>(this TEnum value) where TEnum : Enum
        {
            var field = typeof(TEnum).GetField(value.ToString());
            if (field == null) return value.ToString();
            var attr = field.GetCustomAttributes(typeof(DescriptionAttribute), false)
                            .FirstOrDefault() as DescriptionAttribute;
            return attr?.Description ?? value.ToString();
        }

        /// <summary>
        /// 根据描述获取枚举值（DescriptionAttribute），找不到则返回默认值。
        /// </summary>
        /// <typeparam name="TEnum">枚举类型。</typeparam>
        /// <param name="description">描述文本。</param>
        /// <param name="defaultValue">找不到时的默认值。</param>
        public static TEnum FromDescription<TEnum>(this string description, TEnum defaultValue = default) where TEnum : Enum
        {
            foreach (var field in typeof(TEnum).GetFields())
            {
                var attr = field.GetCustomAttributes(typeof(DescriptionAttribute), false)
                                .FirstOrDefault() as DescriptionAttribute;
                if ((attr?.Description ?? field.Name) == description)
                {
                    return (TEnum)field.GetValue(null);
                }
            }
            return defaultValue;
        }

        /// <summary>
        /// 字符串转为枚举值，转换失败返回默认值。
        /// </summary>
        /// <typeparam name="TEnum">枚举类型。</typeparam>
        /// <param name="value">字符串值。</param>
        /// <param name="defaultValue">转换失败时的默认值。</param>
        public static TEnum Parse<TEnum>(this string value, TEnum defaultValue = default) where TEnum : struct, Enum
        {
            if (string.IsNullOrWhiteSpace(value))
                return defaultValue;
            if (Enum.TryParse<TEnum>(value, true, out var result))
                return result;
            return defaultValue;
        }

        /// <summary>
        /// int 转为枚举值，转换失败返回默认值。
        /// </summary>
        /// <typeparam name="TEnum">枚举类型。</typeparam>
        /// <param name="value">整型值。</param>
        /// <param name="defaultValue">转换失败时的默认值。</param>
        public static TEnum ToEnum<TEnum>(this int value, TEnum defaultValue = default) where TEnum : Enum
        {
            if (Enum.IsDefined(typeof(TEnum), value))
                return (TEnum)Enum.ToObject(typeof(TEnum), value);
            return defaultValue;
        }

        /// <summary>
        /// long 转为枚举值，转换失败返回默认值。
        /// </summary>
        /// <typeparam name="TEnum">枚举类型。</typeparam>
        /// <param name="value">长整型值。</param>
        /// <param name="defaultValue">转换失败时的默认值。</param>
        public static TEnum ToEnum<TEnum>(this long value, TEnum defaultValue = default) where TEnum : Enum
        {
            var underlyingType = Enum.GetUnderlyingType(typeof(TEnum));
            object convertedValue;
            try
            {
                convertedValue = Convert.ChangeType(value, underlyingType);
            }
            catch
            {
                return defaultValue;
            }
            if (Enum.IsDefined(typeof(TEnum), convertedValue))
                return (TEnum)Enum.ToObject(typeof(TEnum), convertedValue);
            return defaultValue;
        }

        /// <summary>
        /// 获取枚举类型所有值及描述的字典。
        /// </summary>
        /// <typeparam name="TEnum">枚举类型。</typeparam>
        public static Dictionary<TEnum, string> GetValueDescriptionDict<TEnum>() where TEnum : Enum
        {
            var dict = new Dictionary<TEnum, string>();
            foreach (var value in GetValues<TEnum>())
            {
                dict[value] = value.GetDescription();
            }
            return dict;
        }

        /// <summary>
        /// 获取枚举类型所有名称及描述的字典。
        /// </summary>
        /// <typeparam name="TEnum">枚举类型。</typeparam>
        public static Dictionary<string, string> GetNameDescriptionDict<TEnum>() where TEnum : Enum
        {
            var dict = new Dictionary<string, string>();
            foreach (var value in GetValues<TEnum>())
            {
                dict[value.ToString()] = value.GetDescription();
            }
            return dict;
        }

        /// <summary>
        /// 判断枚举值是否包含指定标志（仅用于 [Flags] 枚举）。
        /// </summary>
        /// <typeparam name="TEnum">枚举类型。</typeparam>
        /// <param name="value">枚举值。</param>
        /// <param name="flag">标志位。</param>
        public static bool HasFlag<TEnum>(this TEnum value, TEnum flag) where TEnum : Enum =>
            (Convert.ToInt64(value) & Convert.ToInt64(flag)) != 0;

        /// <summary>
        /// 枚举值添加标志（仅用于 [Flags] 枚举）。
        /// </summary>
        /// <typeparam name="TEnum">枚举类型。</typeparam>
        /// <param name="value">原枚举值。</param>
        /// <param name="flag">要添加的标志。</param>
        public static TEnum AddFlag<TEnum>(this TEnum value, TEnum flag) where TEnum : Enum
        {
            var result = Convert.ToInt64(value) | Convert.ToInt64(flag);
            return (TEnum)Enum.ToObject(typeof(TEnum), result);
        }

        /// <summary>
        /// 枚举值移除标志（仅用于 [Flags] 枚举）。
        /// </summary>
        /// <typeparam name="TEnum">枚举类型。</typeparam>
        /// <param name="value">原枚举值。</param>
        /// <param name="flag">要移除的标志。</param>
        public static TEnum RemoveFlag<TEnum>(this TEnum value, TEnum flag) where TEnum : Enum
        {
            var result = Convert.ToInt64(value) & ~Convert.ToInt64(flag);
            return (TEnum)Enum.ToObject(typeof(TEnum), result);
        }

        /// <summary>
        /// 获取枚举类型的基础类型（如 int、byte）。
        /// </summary>
        /// <typeparam name="TEnum">枚举类型。</typeparam>
        public static Type GetUnderlyingType<TEnum>() where TEnum : Enum =>
            Enum.GetUnderlyingType(typeof(TEnum));
    }
}