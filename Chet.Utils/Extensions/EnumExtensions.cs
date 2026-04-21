using System.ComponentModel;
using System.Reflection;

namespace Chet.Utils;

/// <summary>
/// Enum 扩展方法类，提供常用的判断、转换、描述、枚举值操作等功能。
/// </summary>
/// <remarks>
/// <para>本类提供丰富的枚举扩展方法，涵盖以下功能：</para>
/// <list type="bullet">
///   <item><description>基础判断：IsDefined、IsValid、IsDefault</description></item>
///   <item><description>类型转换：ToInt、ToLong、ToByte、ToShort、ToStringValue</description></item>
///   <item><description>字符串解析：Parse、TryParse、ParseOrDefault</description></item>
///   <item><description>数值转换：ToEnum（int/long/byte/short 转 枚举）</description></item>
///   <item><description>描述特性：GetDescription、GetDisplayDescription、GetDisplayName</description></item>
///   <item><description>特性获取：GetAttribute、GetAttributes</description></item>
///   <item><description>集合操作：GetValues、GetNames、GetValueDescriptionDict、GetNameDescriptionDict</description></item>
///   <item><description>标志操作：HasFlag、AddFlag、RemoveFlag、ToggleFlag、GetFlags</description></item>
///   <item><description>比较运算：EqualsAny、EqualsNone、In</description></item>
/// </list>
/// </remarks>
public static class EnumExtensions
{
    #region 基础判断

    /// <summary>
    /// 判断枚举值是否定义在枚举类型中。
    /// </summary>
    /// <typeparam name="TEnum">枚举类型。</typeparam>
    /// <param name="value">待判断的枚举值。</param>
    /// <returns>如果枚举值已定义返回 true；否则返回 false。</returns>
    /// <example>
    /// <code>
    /// enum Status { Active = 1, Inactive = 2 }
    /// 
    /// var status = (Status)1;
    /// var isDefined = status.IsDefined(); // true
    /// 
    /// var invalidStatus = (Status)999;
    /// var isInvalid = invalidStatus.IsDefined(); // false
    /// </code>
    /// </example>
    public static bool IsDefined<TEnum>(this TEnum value) where TEnum : struct, Enum =>
        Enum.IsDefined(typeof(TEnum), value);

    /// <summary>
    /// 判断枚举值是否有效（已定义或在标志枚举中为有效组合）。
    /// </summary>
    /// <typeparam name="TEnum">枚举类型。</typeparam>
    /// <param name="value">待判断的枚举值。</param>
    /// <returns>如果枚举值有效返回 true；否则返回 false。</returns>
    /// <remarks>
    /// 对于标志枚举（[Flags]），会检查是否为有效标志组合。
    /// </remarks>
    /// <example>
    /// <code>
    /// [Flags]
    /// enum Permissions { None = 0, Read = 1, Write = 2, Execute = 4 }
    /// 
    /// var perm = Permissions.Read | Permissions.Write;
    /// var isValid = perm.IsValid(); // true
    /// 
    /// var invalidPerm = (Permissions)8;
    /// var isInvalid = invalidPerm.IsValid(); // false
    /// </code>
    /// </example>
    public static bool IsValid<TEnum>(this TEnum value) where TEnum : struct, Enum
    {
        var type = typeof(TEnum);
        if (Enum.IsDefined(type, value))
            return true;

        var flagsAttribute = type.GetCustomAttribute<FlagsAttribute>();
        if (flagsAttribute != null)
        {
            long allFlags = 0;
            foreach (var enumValue in Enum.GetValues(type))
            {
                allFlags |= Convert.ToInt64(enumValue);
            }
            var longValue = Convert.ToInt64(value);
            return (longValue & allFlags) == longValue;
        }

        return false;
    }

    /// <summary>
    /// 判断枚举值是否为默认值（通常为 0）。
    /// </summary>
    /// <typeparam name="TEnum">枚举类型。</typeparam>
    /// <param name="value">待判断的枚举值。</param>
    /// <returns>如果枚举值为默认值返回 true；否则返回 false。</returns>
    /// <example>
    /// <code>
    /// enum Status { None = 0, Active = 1 }
    /// 
    /// var status = Status.None;
    /// var isDefault = status.IsDefault(); // true
    /// 
    /// var active = Status.Active;
    /// var isNotDefault = active.IsDefault(); // false
    /// </code>
    /// </example>
    public static bool IsDefault<TEnum>(this TEnum value) where TEnum : struct, Enum =>
        Convert.ToInt64(value) == 0;

    /// <summary>
    /// 判断枚举值是否为非默认值。
    /// </summary>
    /// <typeparam name="TEnum">枚举类型。</typeparam>
    /// <param name="value">待判断的枚举值。</param>
    /// <returns>如果枚举值不为默认值返回 true；否则返回 false。</returns>
    public static bool IsNotDefault<TEnum>(this TEnum value) where TEnum : struct, Enum =>
        !value.IsDefault();

    #endregion

    #region 类型转换

    /// <summary>
    /// 枚举值转为 int。
    /// </summary>
    /// <typeparam name="TEnum">枚举类型。</typeparam>
    /// <param name="value">枚举值。</param>
    /// <returns>转换后的整数值。</returns>
    /// <example>
    /// <code>
    /// enum Status { Active = 1, Inactive = 2 }
    /// 
    /// var status = Status.Active;
    /// var intValue = status.ToInt(); // 1
    /// </code>
    /// </example>
    public static int ToInt<TEnum>(this TEnum value) where TEnum : struct, Enum =>
        Convert.ToInt32(value);

    /// <summary>
    /// 枚举值转为 long。
    /// </summary>
    /// <typeparam name="TEnum">枚举类型。</typeparam>
    /// <param name="value">枚举值。</param>
    /// <returns>转换后的长整数值。</returns>
    /// <example>
    /// <code>
    /// enum Status : long { Active = 1L, Inactive = 2L }
    /// 
    /// var status = Status.Active;
    /// var longValue = status.ToLong(); // 1L
    /// </code>
    /// </example>
    public static long ToLong<TEnum>(this TEnum value) where TEnum : struct, Enum =>
        Convert.ToInt64(value);

    /// <summary>
    /// 枚举值转为 byte。
    /// </summary>
    /// <typeparam name="TEnum">枚举类型。</typeparam>
    /// <param name="value">枚举值。</param>
    /// <returns>转换后的字节值。</returns>
    public static byte ToByte<TEnum>(this TEnum value) where TEnum : struct, Enum =>
        Convert.ToByte(value);

    /// <summary>
    /// 枚举值转为 short。
    /// </summary>
    /// <typeparam name="TEnum">枚举类型。</typeparam>
    /// <param name="value">枚举值。</param>
    /// <returns>转换后的短整数值。</returns>
    public static short ToShort<TEnum>(this TEnum value) where TEnum : struct, Enum =>
        Convert.ToInt16(value);

    /// <summary>
    /// 枚举值转为 sbyte。
    /// </summary>
    /// <typeparam name="TEnum">枚举类型。</typeparam>
    /// <param name="value">枚举值。</param>
    /// <returns>转换后的有符号字节值。</returns>
    public static sbyte ToSByte<TEnum>(this TEnum value) where TEnum : struct, Enum =>
        Convert.ToSByte(value);

    /// <summary>
    /// 枚举值转为 ushort。
    /// </summary>
    /// <typeparam name="TEnum">枚举类型。</typeparam>
    /// <param name="value">枚举值。</param>
    /// <returns>转换后的无符号短整数值。</returns>
    public static ushort ToUShort<TEnum>(this TEnum value) where TEnum : struct, Enum =>
        Convert.ToUInt16(value);

    /// <summary>
    /// 枚举值转为 uint。
    /// </summary>
    /// <typeparam name="TEnum">枚举类型。</typeparam>
    /// <param name="value">枚举值。</param>
    /// <returns>转换后的无符号整数值。</returns>
    public static uint ToUInt<TEnum>(this TEnum value) where TEnum : struct, Enum =>
        Convert.ToUInt32(value);

    /// <summary>
    /// 枚举值转为 ulong。
    /// </summary>
    /// <typeparam name="TEnum">枚举类型。</typeparam>
    /// <param name="value">枚举值。</param>
    /// <returns>转换后的无符号长整数值。</returns>
    public static ulong ToULong<TEnum>(this TEnum value) where TEnum : struct, Enum =>
        Convert.ToUInt64(value);

    /// <summary>
    /// 枚举值转为字符串。
    /// </summary>
    /// <typeparam name="TEnum">枚举类型。</typeparam>
    /// <param name="value">枚举值。</param>
    /// <returns>枚举值的字符串表示。</returns>
    /// <example>
    /// <code>
    /// enum Status { Active, Inactive }
    /// 
    /// var status = Status.Active;
    /// var str = status.ToStringValue(); // "Active"
    /// </code>
    /// </example>
    public static string ToStringValue<TEnum>(this TEnum value) where TEnum : struct, Enum =>
        value.ToString();

    /// <summary>
    /// 枚举值转为小写字符串。
    /// </summary>
    /// <typeparam name="TEnum">枚举类型。</typeparam>
    /// <param name="value">枚举值。</param>
    /// <returns>枚举值的小写字符串表示。</returns>
    public static string ToLowerString<TEnum>(this TEnum value) where TEnum : struct, Enum =>
        value.ToString().ToLowerInvariant();

    /// <summary>
    /// 枚举值转为大写字符串。
    /// </summary>
    /// <typeparam name="TEnum">枚举类型。</typeparam>
    /// <param name="value">枚举值。</param>
    /// <returns>枚举值的大写字符串表示。</returns>
    public static string ToUpperString<TEnum>(this TEnum value) where TEnum : struct, Enum =>
        value.ToString().ToUpperInvariant();

    #endregion

    #region 字符串解析

    /// <summary>
    /// 字符串转为枚举值，转换失败返回默认值。
    /// </summary>
    /// <typeparam name="TEnum">枚举类型。</typeparam>
    /// <param name="value">字符串值。</param>
    /// <param name="defaultValue">转换失败时的默认值。</param>
    /// <param name="ignoreCase">是否忽略大小写，默认 true。</param>
    /// <returns>解析后的枚举值。</returns>
    /// <example>
    /// <code>
    /// enum Status { Active, Inactive }
    /// 
    /// var status = "Active".Parse&lt;Status&gt;(); // Status.Active
    /// var status2 = "active".Parse&lt;Status&gt;(); // Status.Active (忽略大小写)
    /// var status3 = "invalid".Parse(Status.Inactive); // Status.Inactive (默认值)
    /// </code>
    /// </example>
    public static TEnum Parse<TEnum>(this string value, TEnum defaultValue = default, bool ignoreCase = true) where TEnum : struct, Enum
    {
        if (string.IsNullOrWhiteSpace(value))
            return defaultValue;
        if (Enum.TryParse<TEnum>(value, ignoreCase, out var result))
            return result;
        return defaultValue;
    }

    /// <summary>
    /// 尝试将字符串转为枚举值。
    /// </summary>
    /// <typeparam name="TEnum">枚举类型。</typeparam>
    /// <param name="value">字符串值。</param>
    /// <param name="result">解析后的枚举值。</param>
    /// <param name="ignoreCase">是否忽略大小写，默认 true。</param>
    /// <returns>如果解析成功返回 true；否则返回 false。</returns>
    /// <example>
    /// <code>
    /// enum Status { Active, Inactive }
    /// 
    /// if ("Active".TryParse&lt;Status&gt;(out var status))
    /// {
    ///     Console.WriteLine(status); // Status.Active
    /// }
    /// </code>
    /// </example>
    public static bool TryParse<TEnum>(this string value, out TEnum result, bool ignoreCase = true) where TEnum : struct, Enum
    {
        result = default;
        if (string.IsNullOrWhiteSpace(value))
            return false;
        return Enum.TryParse(value, ignoreCase, out result);
    }

    /// <summary>
    /// 字符串转为枚举值，转换失败抛出异常。
    /// </summary>
    /// <typeparam name="TEnum">枚举类型。</typeparam>
    /// <param name="value">字符串值。</param>
    /// <param name="ignoreCase">是否忽略大小写，默认 true。</param>
    /// <returns>解析后的枚举值。</returns>
    /// <exception cref="ArgumentException">当字符串无法解析为枚举值时抛出。</exception>
    /// <example>
    /// <code>
    /// enum Status { Active, Inactive }
    /// 
    /// var status = "Active".ParseExact&lt;Status&gt;(); // Status.Active
    /// var status2 = "invalid".ParseExact&lt;Status&gt;(); // 抛出 ArgumentException
    /// </code>
    /// </example>
    public static TEnum ParseExact<TEnum>(this string value, bool ignoreCase = true) where TEnum : struct, Enum
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("枚举值不能为空或空白。", nameof(value));
        return (TEnum)Enum.Parse(typeof(TEnum), value, ignoreCase);
    }

    #endregion

    #region 数值转枚举

    /// <summary>
    /// int 转为枚举值，转换失败返回默认值。
    /// </summary>
    /// <typeparam name="TEnum">枚举类型。</typeparam>
    /// <param name="value">整型值。</param>
    /// <param name="defaultValue">转换失败时的默认值。</param>
    /// <returns>转换后的枚举值。</returns>
    /// <example>
    /// <code>
    /// enum Status { Active = 1, Inactive = 2 }
    /// 
    /// var status = 1.ToEnum&lt;Status&gt;(); // Status.Active
    /// var status2 = 999.ToEnum(Status.Inactive); // Status.Inactive (默认值)
    /// </code>
    /// </example>
    public static TEnum ToEnum<TEnum>(this int value, TEnum defaultValue = default) where TEnum : struct, Enum
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
    /// <returns>转换后的枚举值。</returns>
    public static TEnum ToEnum<TEnum>(this long value, TEnum defaultValue = default) where TEnum : struct, Enum
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
    /// byte 转为枚举值，转换失败返回默认值。
    /// </summary>
    /// <typeparam name="TEnum">枚举类型。</typeparam>
    /// <param name="value">字节值。</param>
    /// <param name="defaultValue">转换失败时的默认值。</param>
    /// <returns>转换后的枚举值。</returns>
    public static TEnum ToEnum<TEnum>(this byte value, TEnum defaultValue = default) where TEnum : struct, Enum
    {
        if (Enum.IsDefined(typeof(TEnum), value))
            return (TEnum)Enum.ToObject(typeof(TEnum), value);
        return defaultValue;
    }

    /// <summary>
    /// short 转为枚举值，转换失败返回默认值。
    /// </summary>
    /// <typeparam name="TEnum">枚举类型。</typeparam>
    /// <param name="value">短整型值。</param>
    /// <param name="defaultValue">转换失败时的默认值。</param>
    /// <returns>转换后的枚举值。</returns>
    public static TEnum ToEnum<TEnum>(this short value, TEnum defaultValue = default) where TEnum : struct, Enum
    {
        if (Enum.IsDefined(typeof(TEnum), value))
            return (TEnum)Enum.ToObject(typeof(TEnum), value);
        return defaultValue;
    }

    /// <summary>
    /// 尝试将 int 转为枚举值。
    /// </summary>
    /// <typeparam name="TEnum">枚举类型。</typeparam>
    /// <param name="value">整型值。</param>
    /// <param name="result">转换后的枚举值。</param>
    /// <returns>如果转换成功返回 true；否则返回 false。</returns>
    public static bool TryToEnum<TEnum>(this int value, out TEnum result) where TEnum : struct, Enum
    {
        result = default;
        if (Enum.IsDefined(typeof(TEnum), value))
        {
            result = (TEnum)Enum.ToObject(typeof(TEnum), value);
            return true;
        }
        return false;
    }

    /// <summary>
    /// 尝试将 long 转为枚举值。
    /// </summary>
    /// <typeparam name="TEnum">枚举类型。</typeparam>
    /// <param name="value">长整型值。</param>
    /// <param name="result">转换后的枚举值。</param>
    /// <returns>如果转换成功返回 true；否则返回 false。</returns>
    public static bool TryToEnum<TEnum>(this long value, out TEnum result) where TEnum : struct, Enum
    {
        result = default;
        var underlyingType = Enum.GetUnderlyingType(typeof(TEnum));
        object convertedValue;
        try
        {
            convertedValue = Convert.ChangeType(value, underlyingType);
        }
        catch
        {
            return false;
        }
        if (Enum.IsDefined(typeof(TEnum), convertedValue))
        {
            result = (TEnum)Enum.ToObject(typeof(TEnum), convertedValue);
            return true;
        }
        return false;
    }

    #endregion

    #region 描述特性

    /// <summary>
    /// 枚举值转为描述（DescriptionAttribute），无描述则返回名称。
    /// </summary>
    /// <typeparam name="TEnum">枚举类型。</typeparam>
    /// <param name="value">枚举值。</param>
    /// <returns>枚举值的描述文本。</returns>
    /// <example>
    /// <code>
    /// enum Status
    /// {
    ///     [Description("激活状态")]
    ///     Active = 1,
    ///     [Description("停用状态")]
    ///     Inactive = 2
    /// }
    /// 
    /// var status = Status.Active;
    /// var desc = status.GetDescription(); // "激活状态"
    /// </code>
    /// </example>
    public static string GetDescription<TEnum>(this TEnum value) where TEnum : struct, Enum
    {
        var field = typeof(TEnum).GetField(value.ToString());
        if (field == null) return value.ToString();
        var attr = field.GetCustomAttribute<DescriptionAttribute>();
        return attr?.Description ?? value.ToString();
    }

    /// <summary>
    /// 根据描述获取枚举值（DescriptionAttribute），找不到则返回默认值。
    /// </summary>
    /// <typeparam name="TEnum">枚举类型。</typeparam>
    /// <param name="description">描述文本。</param>
    /// <param name="defaultValue">找不到时的默认值。</param>
    /// <returns>对应的枚举值。</returns>
    /// <example>
    /// <code>
    /// enum Status
    /// {
    ///     [Description("激活状态")]
    ///     Active = 1,
    ///     [Description("停用状态")]
    ///     Inactive = 2
    /// }
    /// 
    /// var status = "激活状态".FromDescription&lt;Status&gt;(); // Status.Active
    /// </code>
    /// </example>
    public static TEnum FromDescription<TEnum>(this string description, TEnum defaultValue = default) where TEnum : struct, Enum
    {
        foreach (var field in typeof(TEnum).GetFields(BindingFlags.Public | BindingFlags.Static))
        {
            var attr = field.GetCustomAttribute<DescriptionAttribute>();
            if ((attr?.Description ?? field.Name) == description)
            {
                return (TEnum)field.GetValue(null);
            }
        }
        return defaultValue;
    }

    /// <summary>
    /// 获取枚举值的显示名称（DisplayAttribute.Name），无特性则返回枚举名称。
    /// </summary>
    /// <typeparam name="TEnum">枚举类型。</typeparam>
    /// <param name="value">枚举值。</param>
    /// <returns>显示名称。</returns>
    /// <remarks>
    /// 需要引用 System.ComponentModel.DataAnnotations 命名空间。
    /// </remarks>
    public static string GetDisplayName<TEnum>(this TEnum value) where TEnum : struct, Enum
    {
        var field = typeof(TEnum).GetField(value.ToString());
        if (field == null) return value.ToString();
        var attr = field.GetCustomAttribute<System.ComponentModel.DataAnnotations.DisplayAttribute>();
        return attr?.GetName() ?? value.ToString();
    }

    /// <summary>
    /// 获取枚举值的显示描述（DisplayAttribute.Description），无特性则返回枚举名称。
    /// </summary>
    /// <typeparam name="TEnum">枚举类型。</typeparam>
    /// <param name="value">枚举值。</param>
    /// <returns>显示描述。</returns>
    public static string GetDisplayDescription<TEnum>(this TEnum value) where TEnum : struct, Enum
    {
        var field = typeof(TEnum).GetField(value.ToString());
        if (field == null) return value.ToString();
        var attr = field.GetCustomAttribute<System.ComponentModel.DataAnnotations.DisplayAttribute>();
        return attr?.GetDescription() ?? value.ToString();
    }

    /// <summary>
    /// 获取枚举值的显示短名称（DisplayAttribute.ShortName），无特性则返回枚举名称。
    /// </summary>
    /// <typeparam name="TEnum">枚举类型。</typeparam>
    /// <param name="value">枚举值。</param>
    /// <returns>显示短名称。</returns>
    public static string GetDisplayShortName<TEnum>(this TEnum value) where TEnum : struct, Enum
    {
        var field = typeof(TEnum).GetField(value.ToString());
        if (field == null) return value.ToString();
        var attr = field.GetCustomAttribute<System.ComponentModel.DataAnnotations.DisplayAttribute>();
        return attr?.GetShortName() ?? value.ToString();
    }

    #endregion

    #region 特性获取

    /// <summary>
    /// 获取枚举值的指定类型特性。
    /// </summary>
    /// <typeparam name="TAttribute">特性类型。</typeparam>
    /// <typeparam name="TEnum">枚举类型。</typeparam>
    /// <param name="value">枚举值。</param>
    /// <returns>特性实例，如果不存在则返回 null。</returns>
    /// <example>
    /// <code>
    /// [AttributeUsage(AttributeTargets.Field)]
    /// public class ColorAttribute : Attribute
    /// {
    ///     public string Color { get; set; }
    /// }
    /// 
    /// enum Status
    /// {
    ///     [Color(Color = "green")]
    ///     Active = 1,
    ///     [Color(Color = "red")]
    ///     Inactive = 2
    /// }
    /// 
    /// var status = Status.Active;
    /// var colorAttr = status.GetAttribute&lt;ColorAttribute, Status&gt;();
    /// Console.WriteLine(colorAttr?.Color); // "green"
    /// </code>
    /// </example>
    public static TAttribute GetAttribute<TAttribute, TEnum>(this TEnum value)
        where TAttribute : Attribute
        where TEnum : struct, Enum
    {
        var field = typeof(TEnum).GetField(value.ToString());
        return field?.GetCustomAttribute<TAttribute>();
    }

    /// <summary>
    /// 获取枚举值的所有指定类型特性。
    /// </summary>
    /// <typeparam name="TAttribute">特性类型。</typeparam>
    /// <typeparam name="TEnum">枚举类型。</typeparam>
    /// <param name="value">枚举值。</param>
    /// <returns>特性实例数组。</returns>
    public static TAttribute[] GetAttributes<TAttribute, TEnum>(this TEnum value)
        where TAttribute : Attribute
        where TEnum : struct, Enum
    {
        var field = typeof(TEnum).GetField(value.ToString());
        return field?.GetCustomAttributes<TAttribute>().ToArray() ?? Array.Empty<TAttribute>();
    }

    /// <summary>
    /// 判断枚举值是否具有指定类型特性。
    /// </summary>
    /// <typeparam name="TAttribute">特性类型。</typeparam>
    /// <typeparam name="TEnum">枚举类型。</typeparam>
    /// <param name="value">枚举值。</param>
    /// <returns>如果具有指定特性返回 true；否则返回 false。</returns>
    public static bool HasAttribute<TAttribute, TEnum>(this TEnum value)
        where TAttribute : Attribute
        where TEnum : struct, Enum
    {
        var field = typeof(TEnum).GetField(value.ToString());
        return field?.GetCustomAttribute<TAttribute>() != null;
    }

    #endregion

    #region 集合操作

    /// <summary>
    /// 获取枚举类型所有值列表。
    /// </summary>
    /// <typeparam name="TEnum">枚举类型。</typeparam>
    /// <returns>枚举值列表。</returns>
    /// <example>
    /// <code>
    /// enum Status { Active = 1, Inactive = 2 }
    /// 
    /// var values = EnumExtensions.GetValues&lt;Status&gt;();
    /// // [Status.Active, Status.Inactive]
    /// </code>
    /// </example>
    public static List<TEnum> GetValues<TEnum>() where TEnum : struct, Enum =>
        Enum.GetValues(typeof(TEnum)).Cast<TEnum>().ToList();

    /// <summary>
    /// 获取枚举类型所有名称列表。
    /// </summary>
    /// <typeparam name="TEnum">枚举类型。</typeparam>
    /// <returns>枚举名称列表。</returns>
    /// <example>
    /// <code>
    /// enum Status { Active = 1, Inactive = 2 }
    /// 
    /// var names = EnumExtensions.GetNames&lt;Status&gt;();
    /// // ["Active", "Inactive"]
    /// </code>
    /// </example>
    public static List<string> GetNames<TEnum>() where TEnum : struct, Enum =>
        Enum.GetNames(typeof(TEnum)).ToList();

    /// <summary>
    /// 获取枚举类型所有值及描述的字典。
    /// </summary>
    /// <typeparam name="TEnum">枚举类型。</typeparam>
    /// <returns>枚举值与描述的字典。</returns>
    /// <example>
    /// <code>
    /// enum Status
    /// {
    ///     [Description("激活状态")]
    ///     Active = 1,
    ///     [Description("停用状态")]
    ///     Inactive = 2
    /// }
    /// 
    /// var dict = EnumExtensions.GetValueDescriptionDict&lt;Status&gt;();
    /// // { Status.Active: "激活状态", Status.Inactive: "停用状态" }
    /// </code>
    /// </example>
    public static Dictionary<TEnum, string> GetValueDescriptionDict<TEnum>() where TEnum : struct, Enum
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
    /// <returns>枚举名称与描述的字典。</returns>
    public static Dictionary<string, string> GetNameDescriptionDict<TEnum>() where TEnum : struct, Enum
    {
        var dict = new Dictionary<string, string>();
        foreach (var value in GetValues<TEnum>())
        {
            dict[value.ToString()] = value.GetDescription();
        }
        return dict;
    }

    /// <summary>
    /// 获取枚举类型所有值及显示名称的字典。
    /// </summary>
    /// <typeparam name="TEnum">枚举类型。</typeparam>
    /// <returns>枚举值与显示名称的字典。</returns>
    public static Dictionary<TEnum, string> GetValueDisplayNameDict<TEnum>() where TEnum : struct, Enum
    {
        var dict = new Dictionary<TEnum, string>();
        foreach (var value in GetValues<TEnum>())
        {
            dict[value] = value.GetDisplayName();
        }
        return dict;
    }

    /// <summary>
    /// 获取枚举类型所有值、名称、描述的列表。
    /// </summary>
    /// <typeparam name="TEnum">枚举类型。</typeparam>
    /// <returns>包含值、名称、描述的元组列表。</returns>
    public static List<(TEnum Value, string Name, string Description)> GetValueNameDescriptionList<TEnum>() where TEnum : struct, Enum
    {
        return GetValues<TEnum>()
            .Select(v => (Value: v, Name: v.ToString(), Description: v.GetDescription()))
            .ToList();
    }

    /// <summary>
    /// 获取枚举类型所有值、名称、描述、显示名称的列表。
    /// </summary>
    /// <typeparam name="TEnum">枚举类型。</typeparam>
    /// <returns>包含值、名称、描述、显示名称的元组列表。</returns>
    public static List<(TEnum Value, string Name, string Description, string DisplayName)> GetFullInfoList<TEnum>() where TEnum : struct, Enum
    {
        return GetValues<TEnum>()
            .Select(v => (Value: v, Name: v.ToString(), Description: v.GetDescription(), DisplayName: v.GetDisplayName()))
            .ToList();
    }

    #endregion

    #region 标志操作

    /// <summary>
    /// 判断枚举值是否包含指定标志（仅用于 [Flags] 枚举）。
    /// </summary>
    /// <typeparam name="TEnum">枚举类型。</typeparam>
    /// <param name="value">枚举值。</param>
    /// <param name="flag">标志位。</param>
    /// <returns>如果包含指定标志返回 true；否则返回 false。</returns>
    /// <example>
    /// <code>
    /// [Flags]
    /// enum Permissions { None = 0, Read = 1, Write = 2, Execute = 4 }
    /// 
    /// var perm = Permissions.Read | Permissions.Write;
    /// var hasRead = perm.HasFlag(Permissions.Read); // true
    /// var hasExecute = perm.HasFlag(Permissions.Execute); // false
    /// </code>
    /// </example>
    public static bool HasFlag<TEnum>(this TEnum value, TEnum flag) where TEnum : struct, Enum =>
        (Convert.ToInt64(value) & Convert.ToInt64(flag)) != 0;

    /// <summary>
    /// 判断枚举值是否包含所有指定标志（仅用于 [Flags] 枚举）。
    /// </summary>
    /// <typeparam name="TEnum">枚举类型。</typeparam>
    /// <param name="value">枚举值。</param>
    /// <param name="flags">标志位数组。</param>
    /// <returns>如果包含所有指定标志返回 true；否则返回 false。</returns>
    public static bool HasAllFlags<TEnum>(this TEnum value, params TEnum[] flags) where TEnum : struct, Enum
    {
        if (flags == null || flags.Length == 0) return true;
        return flags.All(flag => value.HasFlag(flag));
    }

    /// <summary>
    /// 判断枚举值是否包含任意指定标志（仅用于 [Flags] 枚举）。
    /// </summary>
    /// <typeparam name="TEnum">枚举类型。</typeparam>
    /// <param name="value">枚举值。</param>
    /// <param name="flags">标志位数组。</param>
    /// <returns>如果包含任意指定标志返回 true；否则返回 false。</returns>
    public static bool HasAnyFlag<TEnum>(this TEnum value, params TEnum[] flags) where TEnum : struct, Enum
    {
        if (flags == null || flags.Length == 0) return false;
        return flags.Any(flag => value.HasFlag(flag));
    }

    /// <summary>
    /// 枚举值添加标志（仅用于 [Flags] 枚举）。
    /// </summary>
    /// <typeparam name="TEnum">枚举类型。</typeparam>
    /// <param name="value">原枚举值。</param>
    /// <param name="flag">要添加的标志。</param>
    /// <returns>添加标志后的枚举值。</returns>
    /// <example>
    /// <code>
    /// [Flags]
    /// enum Permissions { None = 0, Read = 1, Write = 2, Execute = 4 }
    /// 
    /// var perm = Permissions.Read;
    /// perm = perm.AddFlag(Permissions.Write); // Read | Write
    /// </code>
    /// </example>
    public static TEnum AddFlag<TEnum>(this TEnum value, TEnum flag) where TEnum : struct, Enum
    {
        var result = Convert.ToInt64(value) | Convert.ToInt64(flag);
        return (TEnum)Enum.ToObject(typeof(TEnum), result);
    }

    /// <summary>
    /// 枚举值添加多个标志（仅用于 [Flags] 枚举）。
    /// </summary>
    /// <typeparam name="TEnum">枚举类型。</typeparam>
    /// <param name="value">原枚举值。</param>
    /// <param name="flags">要添加的标志数组。</param>
    /// <returns>添加标志后的枚举值。</returns>
    public static TEnum AddFlags<TEnum>(this TEnum value, params TEnum[] flags) where TEnum : struct, Enum
    {
        if (flags == null || flags.Length == 0) return value;
        var result = Convert.ToInt64(value);
        foreach (var flag in flags)
        {
            result |= Convert.ToInt64(flag);
        }
        return (TEnum)Enum.ToObject(typeof(TEnum), result);
    }

    /// <summary>
    /// 枚举值移除标志（仅用于 [Flags] 枚举）。
    /// </summary>
    /// <typeparam name="TEnum">枚举类型。</typeparam>
    /// <param name="value">原枚举值。</param>
    /// <param name="flag">要移除的标志。</param>
    /// <returns>移除标志后的枚举值。</returns>
    /// <example>
    /// <code>
    /// [Flags]
    /// enum Permissions { None = 0, Read = 1, Write = 2, Execute = 4 }
    /// 
    /// var perm = Permissions.Read | Permissions.Write;
    /// perm = perm.RemoveFlag(Permissions.Write); // Read
    /// </code>
    /// </example>
    public static TEnum RemoveFlag<TEnum>(this TEnum value, TEnum flag) where TEnum : struct, Enum
    {
        var result = Convert.ToInt64(value) & ~Convert.ToInt64(flag);
        return (TEnum)Enum.ToObject(typeof(TEnum), result);
    }

    /// <summary>
    /// 枚举值移除多个标志（仅用于 [Flags] 枚举）。
    /// </summary>
    /// <typeparam name="TEnum">枚举类型。</typeparam>
    /// <param name="value">原枚举值。</param>
    /// <param name="flags">要移除的标志数组。</param>
    /// <returns>移除标志后的枚举值。</returns>
    public static TEnum RemoveFlags<TEnum>(this TEnum value, params TEnum[] flags) where TEnum : struct, Enum
    {
        if (flags == null || flags.Length == 0) return value;
        var result = Convert.ToInt64(value);
        foreach (var flag in flags)
        {
            result &= ~Convert.ToInt64(flag);
        }
        return (TEnum)Enum.ToObject(typeof(TEnum), result);
    }

    /// <summary>
    /// 枚举值切换标志（有则移除，无则添加，仅用于 [Flags] 枚举）。
    /// </summary>
    /// <typeparam name="TEnum">枚举类型。</typeparam>
    /// <param name="value">原枚举值。</param>
    /// <param name="flag">要切换的标志。</param>
    /// <returns>切换标志后的枚举值。</returns>
    /// <example>
    /// <code>
    /// [Flags]
    /// enum Permissions { None = 0, Read = 1, Write = 2, Execute = 4 }
    /// 
    /// var perm = Permissions.Read;
    /// perm = perm.ToggleFlag(Permissions.Write); // Read | Write
    /// perm = perm.ToggleFlag(Permissions.Write); // Read
    /// </code>
    /// </example>
    public static TEnum ToggleFlag<TEnum>(this TEnum value, TEnum flag) where TEnum : struct, Enum
    {
        var result = Convert.ToInt64(value) ^ Convert.ToInt64(flag);
        return (TEnum)Enum.ToObject(typeof(TEnum), result);
    }

    /// <summary>
    /// 获取枚举值中所有已设置的标志列表（仅用于 [Flags] 枚举）。
    /// </summary>
    /// <typeparam name="TEnum">枚举类型。</typeparam>
    /// <param name="value">枚举值。</param>
    /// <returns>已设置的标志列表。</returns>
    /// <example>
    /// <code>
    /// [Flags]
    /// enum Permissions { None = 0, Read = 1, Write = 2, Execute = 4 }
    /// 
    /// var perm = Permissions.Read | Permissions.Write;
    /// var flags = perm.GetFlags(); // [Permissions.Read, Permissions.Write]
    /// </code>
    /// </example>
    public static List<TEnum> GetFlags<TEnum>(this TEnum value) where TEnum : struct, Enum
    {
        var result = new List<TEnum>();
        var longValue = Convert.ToInt64(value);
        foreach (var enumValue in GetValues<TEnum>())
        {
            var flagValue = Convert.ToInt64(enumValue);
            if (flagValue != 0 && (longValue & flagValue) == flagValue)
            {
                result.Add(enumValue);
            }
        }
        return result;
    }

    /// <summary>
    /// 清除枚举值的所有标志（设为 0）。
    /// </summary>
    /// <typeparam name="TEnum">枚举类型。</typeparam>
    /// <param name="value">枚举值。</param>
    /// <returns>清除标志后的枚举值。</returns>
    public static TEnum ClearFlags<TEnum>(this TEnum value) where TEnum : struct, Enum =>
        default;

    #endregion

    #region 比较运算

    /// <summary>
    /// 判断枚举值是否等于任意指定值。
    /// </summary>
    /// <typeparam name="TEnum">枚举类型。</typeparam>
    /// <param name="value">枚举值。</param>
    /// <param name="values">比较值数组。</param>
    /// <returns>如果等于任意指定值返回 true；否则返回 false。</returns>
    /// <example>
    /// <code>
    /// enum Status { Active = 1, Inactive = 2, Pending = 3 }
    /// 
    /// var status = Status.Active;
    /// var isActive = status.EqualsAny(Status.Active, Status.Pending); // true
    /// </code>
    /// </example>
    public static bool EqualsAny<TEnum>(this TEnum value, params TEnum[] values) where TEnum : struct, Enum
    {
        if (values == null || values.Length == 0) return false;
        return values.Contains(value);
    }

    /// <summary>
    /// 判断枚举值是否不等于所有指定值。
    /// </summary>
    /// <typeparam name="TEnum">枚举类型。</typeparam>
    /// <param name="value">枚举值。</param>
    /// <param name="values">比较值数组。</param>
    /// <returns>如果不等于所有指定值返回 true；否则返回 false。</returns>
    public static bool EqualsNone<TEnum>(this TEnum value, params TEnum[] values) where TEnum : struct, Enum
    {
        if (values == null || values.Length == 0) return true;
        return !values.Contains(value);
    }

    /// <summary>
    /// 判断枚举值是否在指定范围内（包含边界）。
    /// </summary>
    /// <typeparam name="TEnum">枚举类型。</typeparam>
    /// <param name="value">枚举值。</param>
    /// <param name="min">最小值。</param>
    /// <param name="max">最大值。</param>
    /// <returns>如果在范围内返回 true；否则返回 false。</returns>
    public static bool IsBetween<TEnum>(this TEnum value, TEnum min, TEnum max) where TEnum : struct, Enum
    {
        var longValue = Convert.ToInt64(value);
        var longMin = Convert.ToInt64(min);
        var longMax = Convert.ToInt64(max);
        return longValue >= longMin && longValue <= longMax;
    }

    /// <summary>
    /// 判断枚举值是否在指定范围内（不包含边界）。
    /// </summary>
    /// <typeparam name="TEnum">枚举类型。</typeparam>
    /// <param name="value">枚举值。</param>
    /// <param name="min">最小值。</param>
    /// <param name="max">最大值。</param>
    /// <returns>如果在范围内返回 true；否则返回 false。</returns>
    public static bool IsBetweenExclusive<TEnum>(this TEnum value, TEnum min, TEnum max) where TEnum : struct, Enum
    {
        var longValue = Convert.ToInt64(value);
        var longMin = Convert.ToInt64(min);
        var longMax = Convert.ToInt64(max);
        return longValue > longMin && longValue < longMax;
    }

    /// <summary>
    /// 获取两个枚举值中的较大值。
    /// </summary>
    /// <typeparam name="TEnum">枚举类型。</typeparam>
    /// <param name="value">枚举值。</param>
    /// <param name="other">另一个枚举值。</param>
    /// <returns>较大的枚举值。</returns>
    public static TEnum Max<TEnum>(this TEnum value, TEnum other) where TEnum : struct, Enum
    {
        var longValue = Convert.ToInt64(value);
        var longOther = Convert.ToInt64(other);
        return longValue >= longOther ? value : other;
    }

    /// <summary>
    /// 获取两个枚举值中的较小值。
    /// </summary>
    /// <typeparam name="TEnum">枚举类型。</typeparam>
    /// <param name="value">枚举值。</param>
    /// <param name="other">另一个枚举值。</param>
    /// <returns>较小的枚举值。</returns>
    public static TEnum Min<TEnum>(this TEnum value, TEnum other) where TEnum : struct, Enum
    {
        var longValue = Convert.ToInt64(value);
        var longOther = Convert.ToInt64(other);
        return longValue <= longOther ? value : other;
    }

    #endregion

    #region 其他操作

    /// <summary>
    /// 获取枚举类型的基础类型（如 int、byte）。
    /// </summary>
    /// <typeparam name="TEnum">枚举类型。</typeparam>
    /// <returns>基础类型。</returns>
    public static Type GetUnderlyingType<TEnum>() where TEnum : struct, Enum =>
        Enum.GetUnderlyingType(typeof(TEnum));

    /// <summary>
    /// 获取枚举值在枚举类型中的索引位置（从 0 开始）。
    /// </summary>
    /// <typeparam name="TEnum">枚举类型。</typeparam>
    /// <param name="value">枚举值。</param>
    /// <returns>索引位置，如果不存在返回 -1。</returns>
    public static int GetIndex<TEnum>(this TEnum value) where TEnum : struct, Enum
    {
        var values = GetValues<TEnum>();
        return values.IndexOf(value);
    }

    /// <summary>
    /// 获取枚举值的下一个值（按定义顺序）。
    /// </summary>
    /// <typeparam name="TEnum">枚举类型。</typeparam>
    /// <param name="value">枚举值。</param>
    /// <param name="wrap">是否循环，默认 true。</param>
    /// <returns>下一个枚举值。</returns>
    public static TEnum Next<TEnum>(this TEnum value, bool wrap = true) where TEnum : struct, Enum
    {
        var values = GetValues<TEnum>();
        var index = values.IndexOf(value);
        if (index < 0) return default;
        var nextIndex = index + 1;
        if (nextIndex >= values.Count)
        {
            return wrap ? values[0] : value;
        }
        return values[nextIndex];
    }

    /// <summary>
    /// 获取枚举值的前一个值（按定义顺序）。
    /// </summary>
    /// <typeparam name="TEnum">枚举类型。</typeparam>
    /// <param name="value">枚举值。</param>
    /// <param name="wrap">是否循环，默认 true。</param>
    /// <returns>前一个枚举值。</returns>
    public static TEnum Previous<TEnum>(this TEnum value, bool wrap = true) where TEnum : struct, Enum
    {
        var values = GetValues<TEnum>();
        var index = values.IndexOf(value);
        if (index < 0) return default;
        var prevIndex = index - 1;
        if (prevIndex < 0)
        {
            return wrap ? values[^1] : value;
        }
        return values[prevIndex];
    }

    #endregion
}
