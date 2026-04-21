using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace Chet.Utils.Helpers;

/// <summary>
/// 反射操作帮助类，提供类型信息获取、属性操作、方法调用、动态创建等功能。
/// </summary>
/// <remarks>
/// <para>本类提供以下功能模块：</para>
/// <list type="bullet">
///   <item><description>类型信息获取：属性、方法、字段、特性、接口、基类等</description></item>
///   <item><description>属性操作：获取/设置属性值、批量操作、特性获取等</description></item>
///   <item><description>方法调用：实例方法、静态方法、异步方法调用等</description></item>
///   <item><description>字段操作：获取/设置字段值、特性获取等</description></item>
///   <item><description>动态创建：实例创建、类型判断、默认值获取等</description></item>
///   <item><description>表达式树：属性名称获取、高性能属性访问器创建等</description></item>
///   <item><description>类型检查：数值、布尔、字符串、集合、可空、枚举等类型判断</description></item>
///   <item><description>高级反射：扩展方法获取、程序集加载、类型筛选等</description></item>
/// </list>
/// </remarks>
public static class ReflectHelper
{
    #region 类型信息获取

    /// <summary>
    /// 获取类型的所有公共属性。
    /// </summary>
    /// <param name="type">目标类型。</param>
    /// <returns>属性信息数组。</returns>
    /// <exception cref="ArgumentNullException">type 为 null 时抛出。</exception>
    /// <example>
    /// <code>
    /// var properties = ReflectHelper.GetPublicProperties(typeof(MyClass));
    /// foreach (var prop in properties)
    /// {
    ///     Console.WriteLine($"属性: {prop.Name}, 类型: {prop.PropertyType.Name}");
    /// }
    /// </code>
    /// </example>
    public static PropertyInfo[] GetPublicProperties(Type type)
    {
        ArgumentNullException.ThrowIfNull(type);
        return type.GetProperties(BindingFlags.Public | BindingFlags.Instance);
    }

    /// <summary>
    /// 获取类型的所有属性（包括私有属性）。
    /// </summary>
    /// <param name="type">目标类型。</param>
    /// <returns>属性信息数组。</returns>
    /// <exception cref="ArgumentNullException">type 为 null 时抛出。</exception>
    /// <example>
    /// <code>
    /// var allProperties = ReflectHelper.GetAllProperties(typeof(MyClass));
    /// </code>
    /// </example>
    public static PropertyInfo[] GetAllProperties(Type type)
    {
        ArgumentNullException.ThrowIfNull(type);
        return type.GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static);
    }

    /// <summary>
    /// 根据名称获取类型的属性。
    /// </summary>
    /// <param name="type">目标类型。</param>
    /// <param name="propertyName">属性名称。</param>
    /// <returns>属性信息，如果不存在则返回 null。</returns>
    /// <exception cref="ArgumentNullException">type 或 propertyName 为 null 时抛出。</exception>
    /// <example>
    /// <code>
    /// var property = ReflectHelper.GetProperty(typeof(MyClass), "Name");
    /// if (property != null)
    /// {
    ///     Console.WriteLine($"找到属性: {property.Name}");
    /// }
    /// </code>
    /// </example>
    public static PropertyInfo? GetProperty(Type type, string propertyName)
    {
        ArgumentNullException.ThrowIfNull(type);
        ArgumentException.ThrowIfNullOrEmpty(propertyName);
        return type.GetProperty(propertyName, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static);
    }

    /// <summary>
    /// 获取类型的所有公共方法。
    /// </summary>
    /// <param name="type">目标类型。</param>
    /// <returns>方法信息数组。</returns>
    /// <exception cref="ArgumentNullException">type 为 null 时抛出。</exception>
    /// <example>
    /// <code>
    /// var methods = ReflectHelper.GetPublicMethods(typeof(MyClass));
    /// foreach (var method in methods)
    /// {
    ///     Console.WriteLine($"方法: {method.Name}");
    /// }
    /// </code>
    /// </example>
    public static MethodInfo[] GetPublicMethods(Type type)
    {
        ArgumentNullException.ThrowIfNull(type);
        return type.GetMethods(BindingFlags.Public | BindingFlags.Instance);
    }

    /// <summary>
    /// 获取类型的所有方法（包括私有方法）。
    /// </summary>
    /// <param name="type">目标类型。</param>
    /// <returns>方法信息数组。</returns>
    /// <exception cref="ArgumentNullException">type 为 null 时抛出。</exception>
    public static MethodInfo[] GetAllMethods(Type type)
    {
        ArgumentNullException.ThrowIfNull(type);
        return type.GetMethods(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static);
    }

    /// <summary>
    /// 根据名称获取类型的方法。
    /// </summary>
    /// <param name="type">目标类型。</param>
    /// <param name="methodName">方法名称。</param>
    /// <param name="parameterTypes">参数类型数组，用于重载方法区分。</param>
    /// <returns>方法信息，如果不存在则返回 null。</returns>
    /// <exception cref="ArgumentNullException">type 或 methodName 为 null 时抛出。</exception>
    /// <example>
    /// <code>
    /// var method = ReflectHelper.GetMethod(typeof(MyClass), "Calculate", typeof(int), typeof(int));
    /// </code>
    /// </example>
    public static MethodInfo? GetMethod(Type type, string methodName, params Type[] parameterTypes)
    {
        ArgumentNullException.ThrowIfNull(type);
        ArgumentException.ThrowIfNullOrEmpty(methodName);

        if (parameterTypes == null || parameterTypes.Length == 0)
        {
            return type.GetMethod(methodName, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static);
        }

        return type.GetMethod(methodName, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static, null, parameterTypes, null);
    }

    /// <summary>
    /// 获取类型的所有公共字段。
    /// </summary>
    /// <param name="type">目标类型。</param>
    /// <returns>字段信息数组。</returns>
    /// <exception cref="ArgumentNullException">type 为 null 时抛出。</exception>
    public static FieldInfo[] GetPublicFields(Type type)
    {
        ArgumentNullException.ThrowIfNull(type);
        return type.GetFields(BindingFlags.Public | BindingFlags.Instance);
    }

    /// <summary>
    /// 获取类型的所有字段（包括私有字段）。
    /// </summary>
    /// <param name="type">目标类型。</param>
    /// <returns>字段信息数组。</returns>
    /// <exception cref="ArgumentNullException">type 为 null 时抛出。</exception>
    public static FieldInfo[] GetAllFields(Type type)
    {
        ArgumentNullException.ThrowIfNull(type);
        return type.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static);
    }

    /// <summary>
    /// 根据名称获取类型的字段。
    /// </summary>
    /// <param name="type">目标类型。</param>
    /// <param name="fieldName">字段名称。</param>
    /// <returns>字段信息，如果不存在则返回 null。</returns>
    /// <exception cref="ArgumentNullException">type 或 fieldName 为 null 时抛出。</exception>
    public static FieldInfo? GetField(Type type, string fieldName)
    {
        ArgumentNullException.ThrowIfNull(type);
        ArgumentException.ThrowIfNullOrEmpty(fieldName);
        return type.GetField(fieldName, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static);
    }

    /// <summary>
    /// 获取类型的特性。
    /// </summary>
    /// <typeparam name="T">特性类型。</typeparam>
    /// <param name="type">目标类型。</param>
    /// <param name="inherit">是否从基类继承特性。</param>
    /// <returns>特性实例，如果不存在则返回 null。</returns>
    /// <exception cref="ArgumentNullException">type 为 null 时抛出。</exception>
    /// <example>
    /// <code>
    /// var displayAttr = ReflectHelper.GetCustomAttribute&lt;DisplayAttribute&gt;(typeof(MyClass));
    /// if (displayAttr != null)
    /// {
    ///     Console.WriteLine($"显示名称: {displayAttr.Name}");
    /// }
    /// </code>
    /// </example>
    public static T? GetCustomAttribute<T>(Type type, bool inherit = true) where T : Attribute
    {
        ArgumentNullException.ThrowIfNull(type);
        return type.GetCustomAttribute<T>(inherit);
    }

    /// <summary>
    /// 获取类型的所有特性。
    /// </summary>
    /// <param name="type">目标类型。</param>
    /// <param name="inherit">是否从基类继承特性。</param>
    /// <returns>特性数组。</returns>
    /// <exception cref="ArgumentNullException">type 为 null 时抛出。</exception>
    public static Attribute[] GetCustomAttributes(Type type, bool inherit = true)
    {
        ArgumentNullException.ThrowIfNull(type);
        return Attribute.GetCustomAttributes(type, inherit);
    }

    /// <summary>
    /// 检查类型是否具有指定特性。
    /// </summary>
    /// <typeparam name="T">特性类型。</typeparam>
    /// <param name="type">目标类型。</param>
    /// <param name="inherit">是否从基类继承特性。</param>
    /// <returns>是否具有指定特性。</returns>
    /// <exception cref="ArgumentNullException">type 为 null 时抛出。</exception>
    public static bool HasCustomAttribute<T>(Type type, bool inherit = true) where T : Attribute
    {
        ArgumentNullException.ThrowIfNull(type);
        return type.IsDefined(typeof(T), inherit);
    }

    /// <summary>
    /// 获取类型实现的所有接口。
    /// </summary>
    /// <param name="type">目标类型。</param>
    /// <returns>接口类型数组。</returns>
    /// <exception cref="ArgumentNullException">type 为 null 时抛出。</exception>
    public static Type[] GetInterfaces(Type type)
    {
        ArgumentNullException.ThrowIfNull(type);
        return type.GetInterfaces();
    }

    /// <summary>
    /// 检查类型是否实现指定接口。
    /// </summary>
    /// <param name="type">目标类型。</param>
    /// <param name="interfaceType">接口类型。</param>
    /// <returns>是否实现指定接口。</returns>
    /// <exception cref="ArgumentNullException">type 或 interfaceType 为 null 时抛出。</exception>
    /// <example>
    /// <code>
    /// if (ReflectHelper.ImplementsInterface(typeof(MyList), typeof(IEnumerable)))
    /// {
    ///     Console.WriteLine("MyList 实现了 IEnumerable 接口");
    /// }
    /// </code>
    /// </example>
    public static bool ImplementsInterface(Type type, Type interfaceType)
    {
        ArgumentNullException.ThrowIfNull(type);
        ArgumentNullException.ThrowIfNull(interfaceType);
        return interfaceType.IsAssignableFrom(type);
    }

    /// <summary>
    /// 检查类型是否实现指定泛型接口。
    /// </summary>
    /// <typeparam name="TInterface">接口类型。</typeparam>
    /// <param name="type">目标类型。</param>
    /// <returns>是否实现指定接口。</returns>
    /// <exception cref="ArgumentNullException">type 为 null 时抛出。</exception>
    public static bool ImplementsInterface<TInterface>(Type type)
    {
        return ImplementsInterface(type, typeof(TInterface));
    }

    /// <summary>
    /// 获取类型的基类型。
    /// </summary>
    /// <param name="type">目标类型。</param>
    /// <returns>基类型，如果没有基类则返回 null。</returns>
    /// <exception cref="ArgumentNullException">type 为 null 时抛出。</exception>
    public static Type? GetBaseType(Type type)
    {
        ArgumentNullException.ThrowIfNull(type);
        return type.BaseType;
    }

    /// <summary>
    /// 检查类型是否继承自指定类型。
    /// </summary>
    /// <param name="type">目标类型。</param>
    /// <param name="baseType">基类型。</param>
    /// <returns>是否继承自指定类型。</returns>
    /// <exception cref="ArgumentNullException">type 或 baseType 为 null 时抛出。</exception>
    public static bool IsSubclassOf(Type type, Type baseType)
    {
        ArgumentNullException.ThrowIfNull(type);
        ArgumentNullException.ThrowIfNull(baseType);
        return type.IsSubclassOf(baseType);
    }

    /// <summary>
    /// 检查类型是否为值类型。
    /// </summary>
    /// <param name="type">目标类型。</param>
    /// <returns>是否为值类型。</returns>
    /// <exception cref="ArgumentNullException">type 为 null 时抛出。</exception>
    public static bool IsValueType(Type type)
    {
        ArgumentNullException.ThrowIfNull(type);
        return type.IsValueType;
    }

    /// <summary>
    /// 检查类型是否为引用类型。
    /// </summary>
    /// <param name="type">目标类型。</param>
    /// <returns>是否为引用类型。</returns>
    /// <exception cref="ArgumentNullException">type 为 null 时抛出。</exception>
    public static bool IsReferenceType(Type type)
    {
        ArgumentNullException.ThrowIfNull(type);
        return !type.IsValueType;
    }

    /// <summary>
    /// 检查类型是否为抽象类型。
    /// </summary>
    /// <param name="type">目标类型。</param>
    /// <returns>是否为抽象类型。</returns>
    /// <exception cref="ArgumentNullException">type 为 null 时抛出。</exception>
    public static bool IsAbstract(Type type)
    {
        ArgumentNullException.ThrowIfNull(type);
        return type.IsAbstract;
    }

    /// <summary>
    /// 检查类型是否为接口。
    /// </summary>
    /// <param name="type">目标类型。</param>
    /// <returns>是否为接口。</returns>
    /// <exception cref="ArgumentNullException">type 为 null 时抛出。</exception>
    public static bool IsInterface(Type type)
    {
        ArgumentNullException.ThrowIfNull(type);
        return type.IsInterface;
    }

    /// <summary>
    /// 检查类型是否为泛型类型。
    /// </summary>
    /// <param name="type">目标类型。</param>
    /// <returns>是否为泛型类型。</returns>
    /// <exception cref="ArgumentNullException">type 为 null 时抛出。</exception>
    public static bool IsGenericType(Type type)
    {
        ArgumentNullException.ThrowIfNull(type);
        return type.IsGenericType;
    }

    /// <summary>
    /// 检查类型是否为泛型类型定义。
    /// </summary>
    /// <param name="type">目标类型。</param>
    /// <returns>是否为泛型类型定义。</returns>
    /// <exception cref="ArgumentNullException">type 为 null 时抛出。</exception>
    public static bool IsGenericTypeDefinition(Type type)
    {
        ArgumentNullException.ThrowIfNull(type);
        return type.IsGenericTypeDefinition;
    }

    /// <summary>
    /// 获取泛型类型的类型参数。
    /// </summary>
    /// <param name="type">泛型类型。</param>
    /// <returns>类型参数数组。</returns>
    /// <exception cref="ArgumentNullException">type 为 null 时抛出。</exception>
    public static Type[] GetGenericArguments(Type type)
    {
        ArgumentNullException.ThrowIfNull(type);
        return type.GetGenericArguments();
    }

    #endregion

    #region 属性操作

    /// <summary>
    /// 获取对象属性值。
    /// </summary>
    /// <param name="obj">目标对象。</param>
    /// <param name="propertyName">属性名称。</param>
    /// <returns>属性值。</returns>
    /// <exception cref="ArgumentNullException">obj 为 null 时抛出。</exception>
    /// <exception cref="ArgumentException">propertyName 为空或属性不存在时抛出。</exception>
    /// <example>
    /// <code>
    /// var person = new Person { Name = "张三", Age = 25 };
    /// var name = ReflectHelper.GetPropertyValue(person, "Name");
    /// Console.WriteLine($"姓名: {name}");
    /// </code>
    /// </example>
    public static object? GetPropertyValue(object obj, string propertyName)
    {
        ArgumentNullException.ThrowIfNull(obj);
        ArgumentException.ThrowIfNullOrEmpty(propertyName);

        var property = obj.GetType().GetProperty(propertyName, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static);
        if (property == null)
            throw new ArgumentException($"属性 '{propertyName}' 在类型 '{obj.GetType().Name}' 中不存在");

        return property.GetValue(obj);
    }

    /// <summary>
    /// 获取对象属性值（泛型版本）。
    /// </summary>
    /// <typeparam name="T">属性类型。</typeparam>
    /// <param name="obj">目标对象。</param>
    /// <param name="propertyName">属性名称。</param>
    /// <returns>属性值。</returns>
    /// <exception cref="ArgumentNullException">obj 为 null 时抛出。</exception>
    /// <exception cref="ArgumentException">propertyName 为空或属性不存在时抛出。</exception>
    /// <exception cref="InvalidCastException">类型转换失败时抛出。</exception>
    public static T? GetPropertyValue<T>(object obj, string propertyName)
    {
        var value = GetPropertyValue(obj, propertyName);
        return value == null ? default : (T?)value;
    }

    /// <summary>
    /// 设置对象属性值。
    /// </summary>
    /// <param name="obj">目标对象。</param>
    /// <param name="propertyName">属性名称。</param>
    /// <param name="value">属性值。</param>
    /// <exception cref="ArgumentNullException">obj 为 null 时抛出。</exception>
    /// <exception cref="ArgumentException">propertyName 为空或属性不存在时抛出。</exception>
    /// <example>
    /// <code>
    /// var person = new Person();
    /// ReflectHelper.SetPropertyValue(person, "Name", "李四");
    /// ReflectHelper.SetPropertyValue(person, "Age", 30);
    /// </code>
    /// </example>
    public static void SetPropertyValue(object obj, string propertyName, object? value)
    {
        ArgumentNullException.ThrowIfNull(obj);
        ArgumentException.ThrowIfNullOrEmpty(propertyName);

        var property = obj.GetType().GetProperty(propertyName, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static);
        if (property == null)
            throw new ArgumentException($"属性 '{propertyName}' 在类型 '{obj.GetType().Name}' 中不存在");

        if (!property.CanWrite)
            throw new ArgumentException($"属性 '{propertyName}' 不可写");

        property.SetValue(obj, value);
    }

    /// <summary>
    /// 尝试设置对象属性值，失败时返回 false。
    /// </summary>
    /// <param name="obj">目标对象。</param>
    /// <param name="propertyName">属性名称。</param>
    /// <param name="value">属性值。</param>
    /// <returns>是否设置成功。</returns>
    public static bool TrySetPropertyValue(object obj, string propertyName, object? value)
    {
        try
        {
            SetPropertyValue(obj, propertyName, value);
            return true;
        }
        catch
        {
            return false;
        }
    }

    /// <summary>
    /// 获取属性的特性。
    /// </summary>
    /// <typeparam name="T">特性类型。</typeparam>
    /// <param name="property">属性信息。</param>
    /// <param name="inherit">是否从基类继承特性。</param>
    /// <returns>特性实例，如果不存在则返回 null。</returns>
    /// <exception cref="ArgumentNullException">property 为 null 时抛出。</exception>
    public static T? GetCustomAttribute<T>(PropertyInfo property, bool inherit = true) where T : Attribute
    {
        ArgumentNullException.ThrowIfNull(property);
        return property.GetCustomAttribute<T>(inherit);
    }

    /// <summary>
    /// 检查属性是否具有指定特性。
    /// </summary>
    /// <typeparam name="T">特性类型。</typeparam>
    /// <param name="property">属性信息。</param>
    /// <param name="inherit">是否从基类继承特性。</param>
    /// <returns>是否具有指定特性。</returns>
    /// <exception cref="ArgumentNullException">property 为 null 时抛出。</exception>
    public static bool HasCustomAttribute<T>(PropertyInfo property, bool inherit = true) where T : Attribute
    {
        ArgumentNullException.ThrowIfNull(property);
        return property.IsDefined(typeof(T), inherit);
    }

    /// <summary>
    /// 获取属性的显示名称。
    /// </summary>
    /// <param name="property">属性信息。</param>
    /// <returns>显示名称。</returns>
    /// <exception cref="ArgumentNullException">property 为 null 时抛出。</exception>
    /// <example>
    /// <code>
    /// var property = typeof(Person).GetProperty("Name");
    /// var displayName = ReflectHelper.GetPropertyDisplayName(property);
    /// Console.WriteLine($"显示名称: {displayName}");
    /// </code>
    /// </example>
    public static string GetPropertyDisplayName(PropertyInfo property)
    {
        ArgumentNullException.ThrowIfNull(property);

        var displayAttribute = GetCustomAttribute<DisplayAttribute>(property);
        if (displayAttribute != null && !string.IsNullOrEmpty(displayAttribute.Name))
            return displayAttribute.Name;

        var displayNameAttribute = GetCustomAttribute<DisplayNameAttribute>(property);
        if (displayNameAttribute != null && !string.IsNullOrEmpty(displayNameAttribute.DisplayName))
            return displayNameAttribute.DisplayName;

        return property.Name;
    }

    /// <summary>
    /// 获取属性的描述信息。
    /// </summary>
    /// <param name="property">属性信息。</param>
    /// <returns>描述信息。</returns>
    /// <exception cref="ArgumentNullException">property 为 null 时抛出。</exception>
    public static string GetPropertyDescription(PropertyInfo property)
    {
        ArgumentNullException.ThrowIfNull(property);

        var displayAttribute = GetCustomAttribute<DisplayAttribute>(property);
        if (displayAttribute != null && !string.IsNullOrEmpty(displayAttribute.Description))
            return displayAttribute.Description;

        var descriptionAttribute = GetCustomAttribute<DescriptionAttribute>(property);
        if (descriptionAttribute != null && !string.IsNullOrEmpty(descriptionAttribute.Description))
            return descriptionAttribute.Description;

        return string.Empty;
    }

    /// <summary>
    /// 获取对象所有属性的名称和值。
    /// </summary>
    /// <param name="obj">目标对象。</param>
    /// <returns>属性名称和值的字典。</returns>
    /// <example>
    /// <code>
    /// var person = new Person { Name = "张三", Age = 25 };
    /// var propertyValues = ReflectHelper.GetPropertyValues(person);
    /// foreach (var kvp in propertyValues)
    /// {
    ///     Console.WriteLine($"{kvp.Key} = {kvp.Value}");
    /// }
    /// </code>
    /// </example>
    public static Dictionary<string, object?> GetPropertyValues(object? obj)
    {
        if (obj == null) return new Dictionary<string, object?>();

        var properties = GetPublicProperties(obj.GetType());
        var result = new Dictionary<string, object?>();

        foreach (var property in properties)
        {
            try
            {
                result[property.Name] = property.GetValue(obj);
            }
            catch
            {
                result[property.Name] = null;
            }
        }

        return result;
    }

    /// <summary>
    /// 批量设置对象属性值。
    /// </summary>
    /// <param name="obj">目标对象。</param>
    /// <param name="propertyValues">属性值字典。</param>
    /// <exception cref="ArgumentNullException">obj 为 null 时抛出。</exception>
    /// <example>
    /// <code>
    /// var person = new Person();
    /// var values = new Dictionary&lt;string, object&gt;
    /// {
    ///     ["Name"] = "王五",
    ///     ["Age"] = 28
    /// };
    /// ReflectHelper.SetPropertyValues(person, values);
    /// </code>
    /// </example>
    public static void SetPropertyValues(object obj, Dictionary<string, object?> propertyValues)
    {
        ArgumentNullException.ThrowIfNull(obj);
        if (propertyValues == null || propertyValues.Count == 0) return;

        var properties = GetPublicProperties(obj.GetType())
            .Where(p => p.CanWrite)
            .ToDictionary(p => p.Name);

        foreach (var kvp in propertyValues)
        {
            if (properties.TryGetValue(kvp.Key, out var property))
            {
                try
                {
                    var convertedValue = ConvertValue(kvp.Value, property.PropertyType);
                    property.SetValue(obj, convertedValue);
                }
                catch
                {
                    // 转换失败时跳过
                }
            }
        }
    }

    /// <summary>
    /// 检查属性是否可读。
    /// </summary>
    /// <param name="property">属性信息。</param>
    /// <returns>是否可读。</returns>
    /// <exception cref="ArgumentNullException">property 为 null 时抛出。</exception>
    public static bool CanRead(PropertyInfo property)
    {
        ArgumentNullException.ThrowIfNull(property);
        return property.CanRead;
    }

    /// <summary>
    /// 检查属性是否可写。
    /// </summary>
    /// <param name="property">属性信息。</param>
    /// <returns>是否可写。</returns>
    /// <exception cref="ArgumentNullException">property 为 null 时抛出。</exception>
    public static bool CanWrite(PropertyInfo property)
    {
        ArgumentNullException.ThrowIfNull(property);
        return property.CanWrite;
    }

    /// <summary>
    /// 获取属性类型。
    /// </summary>
    /// <param name="property">属性信息。</param>
    /// <returns>属性类型。</returns>
    /// <exception cref="ArgumentNullException">property 为 null 时抛出。</exception>
    public static Type GetPropertyType(PropertyInfo property)
    {
        ArgumentNullException.ThrowIfNull(property);
        return property.PropertyType;
    }

    #endregion

    #region 方法调用

    /// <summary>
    /// 调用对象方法。
    /// </summary>
    /// <param name="obj">目标对象。</param>
    /// <param name="methodName">方法名称。</param>
    /// <param name="parameters">参数数组。</param>
    /// <returns>方法返回值。</returns>
    /// <exception cref="ArgumentNullException">obj 为 null 时抛出。</exception>
    /// <exception cref="ArgumentException">methodName 为空或方法不存在时抛出。</exception>
    /// <example>
    /// <code>
    /// var calculator = new Calculator();
    /// var result = ReflectHelper.InvokeMethod(calculator, "Add", 5, 3);
    /// Console.WriteLine($"结果: {result}");
    /// </code>
    /// </example>
    public static object? InvokeMethod(object obj, string methodName, params object?[] parameters)
    {
        ArgumentNullException.ThrowIfNull(obj);
        ArgumentException.ThrowIfNullOrEmpty(methodName);

        var method = obj.GetType().GetMethod(methodName, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
        if (method == null)
            throw new ArgumentException($"方法 '{methodName}' 在类型 '{obj.GetType().Name}' 中不存在");

        return method.Invoke(obj, parameters);
    }

    /// <summary>
    /// 调用对象方法（泛型版本）。
    /// </summary>
    /// <typeparam name="T">返回值类型。</typeparam>
    /// <param name="obj">目标对象。</param>
    /// <param name="methodName">方法名称。</param>
    /// <param name="parameters">参数数组。</param>
    /// <returns>方法返回值。</returns>
    /// <exception cref="ArgumentNullException">obj 为 null 时抛出。</exception>
    /// <exception cref="ArgumentException">methodName 为空或方法不存在时抛出。</exception>
    /// <exception cref="InvalidCastException">类型转换失败时抛出。</exception>
    public static T? InvokeMethod<T>(object obj, string methodName, params object?[] parameters)
    {
        var value = InvokeMethod(obj, methodName, parameters);
        return value == null ? default : (T?)value;
    }

    /// <summary>
    /// 调用静态方法。
    /// </summary>
    /// <param name="type">目标类型。</param>
    /// <param name="methodName">方法名称。</param>
    /// <param name="parameters">参数数组。</param>
    /// <returns>方法返回值。</returns>
    /// <exception cref="ArgumentNullException">type 为 null 时抛出。</exception>
    /// <exception cref="ArgumentException">methodName 为空或方法不存在时抛出。</exception>
    /// <example>
    /// <code>
    /// var result = ReflectHelper.InvokeStaticMethod(typeof(Math), "Abs", -10);
    /// Console.WriteLine($"绝对值: {result}");
    /// </code>
    /// </example>
    public static object? InvokeStaticMethod(Type type, string methodName, params object?[] parameters)
    {
        ArgumentNullException.ThrowIfNull(type);
        ArgumentException.ThrowIfNullOrEmpty(methodName);

        var method = type.GetMethod(methodName, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static);
        if (method == null)
            throw new ArgumentException($"静态方法 '{methodName}' 在类型 '{type.Name}' 中不存在");

        return method.Invoke(null, parameters);
    }

    /// <summary>
    /// 调用静态方法（泛型版本）。
    /// </summary>
    /// <typeparam name="T">返回值类型。</typeparam>
    /// <param name="type">目标类型。</param>
    /// <param name="methodName">方法名称。</param>
    /// <param name="parameters">参数数组。</param>
    /// <returns>方法返回值。</returns>
    public static T? InvokeStaticMethod<T>(Type type, string methodName, params object?[] parameters)
    {
        var value = InvokeStaticMethod(type, methodName, parameters);
        return value == null ? default : (T?)value;
    }

    /// <summary>
    /// 获取方法的特性。
    /// </summary>
    /// <typeparam name="T">特性类型。</typeparam>
    /// <param name="method">方法信息。</param>
    /// <param name="inherit">是否从基类继承特性。</param>
    /// <returns>特性实例，如果不存在则返回 null。</returns>
    /// <exception cref="ArgumentNullException">method 为 null 时抛出。</exception>
    public static T? GetCustomAttribute<T>(MethodInfo method, bool inherit = true) where T : Attribute
    {
        ArgumentNullException.ThrowIfNull(method);
        return method.GetCustomAttribute<T>(inherit);
    }

    /// <summary>
    /// 检查方法是否具有指定特性。
    /// </summary>
    /// <typeparam name="T">特性类型。</typeparam>
    /// <param name="method">方法信息。</param>
    /// <param name="inherit">是否从基类继承特性。</param>
    /// <returns>是否具有指定特性。</returns>
    /// <exception cref="ArgumentNullException">method 为 null 时抛出。</exception>
    public static bool HasCustomAttribute<T>(MethodInfo method, bool inherit = true) where T : Attribute
    {
        ArgumentNullException.ThrowIfNull(method);
        return method.IsDefined(typeof(T), inherit);
    }

    /// <summary>
    /// 获取方法参数信息。
    /// </summary>
    /// <param name="method">方法信息。</param>
    /// <returns>参数信息数组。</returns>
    /// <exception cref="ArgumentNullException">method 为 null 时抛出。</exception>
    public static ParameterInfo[] GetMethodParameters(MethodInfo method)
    {
        ArgumentNullException.ThrowIfNull(method);
        return method.GetParameters();
    }

    /// <summary>
    /// 获取方法返回值类型。
    /// </summary>
    /// <param name="method">方法信息。</param>
    /// <returns>返回值类型。</returns>
    /// <exception cref="ArgumentNullException">method 为 null 时抛出。</exception>
    public static Type GetMethodReturnType(MethodInfo method)
    {
        ArgumentNullException.ThrowIfNull(method);
        return method.ReturnType;
    }

    /// <summary>
    /// 检查方法是否为异步方法。
    /// </summary>
    /// <param name="method">方法信息。</param>
    /// <returns>是否为异步方法。</returns>
    /// <exception cref="ArgumentNullException">method 为 null 时抛出。</exception>
    public static bool IsAsyncMethod(MethodInfo method)
    {
        ArgumentNullException.ThrowIfNull(method);

        var returnType = method.ReturnType;
        if (returnType == typeof(void))
            return false;

        if (returnType == typeof(Task))
            return true;

        if (returnType.IsGenericType && returnType.GetGenericTypeDefinition() == typeof(Task<>))
            return true;

        if (returnType == typeof(ValueTask))
            return true;

        if (returnType.IsGenericType && returnType.GetGenericTypeDefinition() == typeof(ValueTask<>))
            return true;

        return false;
    }

    /// <summary>
    /// 检查方法是否为静态方法。
    /// </summary>
    /// <param name="method">方法信息。</param>
    /// <returns>是否为静态方法。</returns>
    /// <exception cref="ArgumentNullException">method 为 null 时抛出。</exception>
    public static bool IsStaticMethod(MethodInfo method)
    {
        ArgumentNullException.ThrowIfNull(method);
        return method.IsStatic;
    }

    /// <summary>
    /// 检查方法是否为公共方法。
    /// </summary>
    /// <param name="method">方法信息。</param>
    /// <returns>是否为公共方法。</returns>
    /// <exception cref="ArgumentNullException">method 为 null 时抛出。</exception>
    public static bool IsPublicMethod(MethodInfo method)
    {
        ArgumentNullException.ThrowIfNull(method);
        return method.IsPublic;
    }

    #endregion

    #region 字段操作

    /// <summary>
    /// 获取对象字段值。
    /// </summary>
    /// <param name="obj">目标对象。</param>
    /// <param name="fieldName">字段名称。</param>
    /// <returns>字段值。</returns>
    /// <exception cref="ArgumentNullException">obj 为 null 时抛出。</exception>
    /// <exception cref="ArgumentException">fieldName 为空或字段不存在时抛出。</exception>
    public static object? GetFieldValue(object obj, string fieldName)
    {
        ArgumentNullException.ThrowIfNull(obj);
        ArgumentException.ThrowIfNullOrEmpty(fieldName);

        var field = obj.GetType().GetField(fieldName, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static);
        if (field == null)
            throw new ArgumentException($"字段 '{fieldName}' 在类型 '{obj.GetType().Name}' 中不存在");

        return field.GetValue(obj);
    }

    /// <summary>
    /// 获取对象字段值（泛型版本）。
    /// </summary>
    /// <typeparam name="T">字段类型。</typeparam>
    /// <param name="obj">目标对象。</param>
    /// <param name="fieldName">字段名称。</param>
    /// <returns>字段值。</returns>
    public static T? GetFieldValue<T>(object obj, string fieldName)
    {
        var value = GetFieldValue(obj, fieldName);
        return value == null ? default : (T?)value;
    }

    /// <summary>
    /// 设置对象字段值。
    /// </summary>
    /// <param name="obj">目标对象。</param>
    /// <param name="fieldName">字段名称。</param>
    /// <param name="value">字段值。</param>
    /// <exception cref="ArgumentNullException">obj 为 null 时抛出。</exception>
    /// <exception cref="ArgumentException">fieldName 为空或字段不存在时抛出。</exception>
    public static void SetFieldValue(object obj, string fieldName, object? value)
    {
        ArgumentNullException.ThrowIfNull(obj);
        ArgumentException.ThrowIfNullOrEmpty(fieldName);

        var field = obj.GetType().GetField(fieldName, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static);
        if (field == null)
            throw new ArgumentException($"字段 '{fieldName}' 在类型 '{obj.GetType().Name}' 中不存在");

        field.SetValue(obj, value);
    }

    /// <summary>
    /// 获取字段的特性。
    /// </summary>
    /// <typeparam name="T">特性类型。</typeparam>
    /// <param name="field">字段信息。</param>
    /// <param name="inherit">是否从基类继承特性。</param>
    /// <returns>特性实例，如果不存在则返回 null。</returns>
    /// <exception cref="ArgumentNullException">field 为 null 时抛出。</exception>
    public static T? GetCustomAttribute<T>(FieldInfo field, bool inherit = true) where T : Attribute
    {
        ArgumentNullException.ThrowIfNull(field);
        return field.GetCustomAttribute<T>(inherit);
    }

    /// <summary>
    /// 检查字段是否具有指定特性。
    /// </summary>
    /// <typeparam name="T">特性类型。</typeparam>
    /// <param name="field">字段信息。</param>
    /// <param name="inherit">是否从基类继承特性。</param>
    /// <returns>是否具有指定特性。</returns>
    /// <exception cref="ArgumentNullException">field 为 null 时抛出。</exception>
    public static bool HasCustomAttribute<T>(FieldInfo field, bool inherit = true) where T : Attribute
    {
        ArgumentNullException.ThrowIfNull(field);
        return field.IsDefined(typeof(T), inherit);
    }

    /// <summary>
    /// 检查字段是否为静态字段。
    /// </summary>
    /// <param name="field">字段信息。</param>
    /// <returns>是否为静态字段。</returns>
    /// <exception cref="ArgumentNullException">field 为 null 时抛出。</exception>
    public static bool IsStaticField(FieldInfo field)
    {
        ArgumentNullException.ThrowIfNull(field);
        return field.IsStatic;
    }

    /// <summary>
    /// 检查字段是否为公共字段。
    /// </summary>
    /// <param name="field">字段信息。</param>
    /// <returns>是否为公共字段。</returns>
    /// <exception cref="ArgumentNullException">field 为 null 时抛出。</exception>
    public static bool IsPublicField(FieldInfo field)
    {
        ArgumentNullException.ThrowIfNull(field);
        return field.IsPublic;
    }

    #endregion

    #region 动态创建与实例化

    /// <summary>
    /// 创建类型实例。
    /// </summary>
    /// <typeparam name="T">类型。</typeparam>
    /// <returns>实例。</returns>
    /// <example>
    /// <code>
    /// var person = ReflectHelper.CreateInstance&lt;Person&gt;();
    /// </code>
    /// </example>
    public static T CreateInstance<T>() where T : new()
    {
        return new T();
    }

    /// <summary>
    /// 创建类型实例（通过反射）。
    /// </summary>
    /// <param name="type">类型。</param>
    /// <param name="args">构造函数参数。</param>
    /// <returns>实例。</returns>
    /// <exception cref="ArgumentNullException">type 为 null 时抛出。</exception>
    /// <example>
    /// <code>
    /// var person = ReflectHelper.CreateInstance(typeof(Person), "张三", 25);
    /// </code>
    /// </example>
    public static object? CreateInstance(Type type, params object?[] args)
    {
        ArgumentNullException.ThrowIfNull(type);
        return Activator.CreateInstance(type, args);
    }

    /// <summary>
    /// 创建类型实例（泛型版本）。
    /// </summary>
    /// <typeparam name="T">类型。</typeparam>
    /// <param name="args">构造函数参数。</param>
    /// <returns>实例。</returns>
    public static T? CreateInstance<T>(params object?[] args)
    {
        return (T?)CreateInstance(typeof(T), args);
    }

    /// <summary>
    /// 通过类型名称创建实例。
    /// </summary>
    /// <param name="typeName">类型名称（完整限定名）。</param>
    /// <param name="assemblyName">程序集名称，可选。</param>
    /// <returns>实例。</returns>
    /// <exception cref="ArgumentException">类型未找到时抛出。</exception>
    /// <example>
    /// <code>
    /// var instance = ReflectHelper.CreateInstance("System.Text.StringBuilder");
    /// </code>
    /// </example>
    public static object? CreateInstance(string typeName, string? assemblyName = null)
    {
        Type? type;

        if (string.IsNullOrEmpty(assemblyName))
        {
            type = Type.GetType(typeName);
        }
        else
        {
            var assembly = Assembly.Load(assemblyName);
            type = assembly.GetType(typeName);
        }

        if (type == null)
            throw new ArgumentException($"类型 '{typeName}' 未找到");

        return Activator.CreateInstance(type);
    }

    /// <summary>
    /// 获取类型的默认值。
    /// </summary>
    /// <param name="type">类型。</param>
    /// <returns>默认值。</returns>
    /// <exception cref="ArgumentNullException">type 为 null 时抛出。</exception>
    /// <example>
    /// <code>
    /// var defaultInt = ReflectHelper.GetDefaultValue(typeof(int)); // 0
    /// var defaultString = ReflectHelper.GetDefaultValue(typeof(string)); // null
    /// </code>
    /// </example>
    public static object? GetDefaultValue(Type type)
    {
        ArgumentNullException.ThrowIfNull(type);

        if (type.IsValueType)
        {
            return Activator.CreateInstance(type);
        }

        return null;
    }

    /// <summary>
    /// 获取类型的默认值（泛型版本）。
    /// </summary>
    /// <typeparam name="T">类型。</typeparam>
    /// <returns>默认值。</returns>
    public static T? GetDefaultValue<T>()
    {
        return default;
    }

    /// <summary>
    /// 检查类型是否可以被实例化。
    /// </summary>
    /// <param name="type">类型。</param>
    /// <returns>是否可以被实例化。</returns>
    /// <exception cref="ArgumentNullException">type 为 null 时抛出。</exception>
    public static bool IsInstantiable(Type type)
    {
        ArgumentNullException.ThrowIfNull(type);
        return !type.IsAbstract && !type.IsInterface && !type.ContainsGenericParameters;
    }

    /// <summary>
    /// 获取类型的所有公共构造函数。
    /// </summary>
    /// <param name="type">类型。</param>
    /// <returns>构造函数信息数组。</returns>
    /// <exception cref="ArgumentNullException">type 为 null 时抛出。</exception>
    public static ConstructorInfo[] GetConstructors(Type type)
    {
        ArgumentNullException.ThrowIfNull(type);
        return type.GetConstructors(BindingFlags.Public | BindingFlags.Instance);
    }

    /// <summary>
    /// 获取类型的所有构造函数（包括私有构造函数）。
    /// </summary>
    /// <param name="type">类型。</param>
    /// <returns>构造函数信息数组。</returns>
    /// <exception cref="ArgumentNullException">type 为 null 时抛出。</exception>
    public static ConstructorInfo[] GetAllConstructors(Type type)
    {
        ArgumentNullException.ThrowIfNull(type);
        return type.GetConstructors(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
    }

    /// <summary>
    /// 检查类型是否有无参构造函数。
    /// </summary>
    /// <param name="type">类型。</param>
    /// <returns>是否有无参构造函数。</returns>
    /// <exception cref="ArgumentNullException">type 为 null 时抛出。</exception>
    public static bool HasParameterlessConstructor(Type type)
    {
        ArgumentNullException.ThrowIfNull(type);
        return type.GetConstructor(Type.EmptyTypes) != null;
    }

    #endregion

    #region 表达式树操作

    /// <summary>
    /// 获取属性名称（通过表达式树）。
    /// </summary>
    /// <typeparam name="T">对象类型。</typeparam>
    /// <typeparam name="TProperty">属性类型。</typeparam>
    /// <param name="propertyExpression">属性表达式。</param>
    /// <returns>属性名称。</returns>
    /// <exception cref="ArgumentNullException">propertyExpression 为 null 时抛出。</exception>
    /// <exception cref="ArgumentException">表达式不是有效的属性表达式时抛出。</exception>
    /// <example>
    /// <code>
    /// var propertyName = ReflectHelper.GetPropertyName&lt;Person, string&gt;(p => p.Name);
    /// Console.WriteLine($"属性名: {propertyName}");
    /// </code>
    /// </example>
    public static string GetPropertyName<T, TProperty>(Expression<Func<T, TProperty>> propertyExpression)
    {
        ArgumentNullException.ThrowIfNull(propertyExpression);

        if (propertyExpression.Body is MemberExpression memberExpression)
        {
            return memberExpression.Member.Name;
        }

        if (propertyExpression.Body is UnaryExpression unaryExpression &&
            unaryExpression.Operand is MemberExpression unaryMemberExpression)
        {
            return unaryMemberExpression.Member.Name;
        }

        throw new ArgumentException("表达式不是有效的属性表达式", nameof(propertyExpression));
    }

    /// <summary>
    /// 获取属性名称（通过表达式树，非泛型版本）。
    /// </summary>
    /// <typeparam name="T">对象类型。</typeparam>
    /// <param name="propertyExpression">属性表达式。</param>
    /// <returns>属性名称。</returns>
    /// <exception cref="ArgumentNullException">propertyExpression 为 null 时抛出。</exception>
    public static string GetPropertyName<T>(Expression<Func<T, object?>> propertyExpression)
    {
        return GetPropertyName<T, object?>(propertyExpression);
    }

    /// <summary>
    /// 创建属性获取器（通过表达式树）。
    /// </summary>
    /// <typeparam name="T">对象类型。</typeparam>
    /// <typeparam name="TProperty">属性类型。</typeparam>
    /// <param name="propertyExpression">属性表达式。</param>
    /// <returns>属性获取器函数。</returns>
    /// <exception cref="ArgumentNullException">propertyExpression 为 null 时抛出。</exception>
    public static Func<T, TProperty> CreatePropertyGetter<T, TProperty>(Expression<Func<T, TProperty>> propertyExpression)
    {
        ArgumentNullException.ThrowIfNull(propertyExpression);
        return propertyExpression.Compile();
    }

    /// <summary>
    /// 创建属性设置器（通过表达式树）。
    /// </summary>
    /// <typeparam name="T">对象类型。</typeparam>
    /// <typeparam name="TProperty">属性类型。</typeparam>
    /// <param name="propertyExpression">属性表达式。</param>
    /// <returns>属性设置器函数。</returns>
    /// <exception cref="ArgumentNullException">propertyExpression 为 null 时抛出。</exception>
    /// <exception cref="ArgumentException">表达式不是有效的属性表达式时抛出。</exception>
    public static Action<T, TProperty> CreatePropertySetter<T, TProperty>(Expression<Func<T, TProperty>> propertyExpression)
    {
        ArgumentNullException.ThrowIfNull(propertyExpression);

        var memberExpression = propertyExpression.Body as MemberExpression;
        if (memberExpression == null && propertyExpression.Body is UnaryExpression unaryExpression)
        {
            memberExpression = unaryExpression.Operand as MemberExpression;
        }

        if (memberExpression == null)
            throw new ArgumentException("表达式不是有效的属性表达式", nameof(propertyExpression));

        var propertyInfo = memberExpression.Member as PropertyInfo;
        if (propertyInfo == null)
            throw new ArgumentException("表达式不是有效的属性表达式", nameof(propertyExpression));

        var targetParameter = Expression.Parameter(typeof(T), "target");
        var valueParameter = Expression.Parameter(typeof(TProperty), "value");

        var property = Expression.Property(targetParameter, propertyInfo);
        var assign = Expression.Assign(property, valueParameter);

        return Expression.Lambda<Action<T, TProperty>>(assign, targetParameter, valueParameter).Compile();
    }

    /// <summary>
    /// 创建方法调用器（通过表达式树）。
    /// </summary>
    /// <param name="method">方法信息。</param>
    /// <returns>方法调用器函数。</returns>
    /// <exception cref="ArgumentNullException">method 为 null 时抛出。</exception>
    public static Func<object?[], object?> CreateMethodInvoker(MethodInfo method)
    {
        ArgumentNullException.ThrowIfNull(method);

        return parameters =>
        {
            if (method.IsStatic)
            {
                return method.Invoke(null, parameters);
            }

            if (parameters == null || parameters.Length == 0)
                throw new ArgumentException("实例方法需要提供实例参数");

            var instance = parameters[0];
            var args = parameters.Skip(1).ToArray();
            return method.Invoke(instance, args);
        };
    }

    #endregion

    #region 类型检查

    /// <summary>
    /// 检查类型是否为数值类型。
    /// </summary>
    /// <param name="type">类型。</param>
    /// <returns>是否为数值类型。</returns>
    /// <exception cref="ArgumentNullException">type 为 null 时抛出。</exception>
    /// <example>
    /// <code>
    /// if (ReflectHelper.IsNumericType(typeof(int)))
    /// {
    ///     Console.WriteLine("int 是数值类型");
    /// }
    /// </code>
    /// </example>
    public static bool IsNumericType(Type type)
    {
        ArgumentNullException.ThrowIfNull(type);

        var underlyingType = Nullable.GetUnderlyingType(type) ?? type;

        return underlyingType == typeof(byte) ||
               underlyingType == typeof(sbyte) ||
               underlyingType == typeof(short) ||
               underlyingType == typeof(ushort) ||
               underlyingType == typeof(int) ||
               underlyingType == typeof(uint) ||
               underlyingType == typeof(long) ||
               underlyingType == typeof(ulong) ||
               underlyingType == typeof(float) ||
               underlyingType == typeof(double) ||
               underlyingType == typeof(decimal);
    }

    /// <summary>
    /// 检查类型是否为整数类型。
    /// </summary>
    /// <param name="type">类型。</param>
    /// <returns>是否为整数类型。</returns>
    /// <exception cref="ArgumentNullException">type 为 null 时抛出。</exception>
    public static bool IsIntegerType(Type type)
    {
        ArgumentNullException.ThrowIfNull(type);

        var underlyingType = Nullable.GetUnderlyingType(type) ?? type;

        return underlyingType == typeof(byte) ||
               underlyingType == typeof(sbyte) ||
               underlyingType == typeof(short) ||
               underlyingType == typeof(ushort) ||
               underlyingType == typeof(int) ||
               underlyingType == typeof(uint) ||
               underlyingType == typeof(long) ||
               underlyingType == typeof(ulong);
    }

    /// <summary>
    /// 检查类型是否为浮点数类型。
    /// </summary>
    /// <param name="type">类型。</param>
    /// <returns>是否为浮点数类型。</returns>
    /// <exception cref="ArgumentNullException">type 为 null 时抛出。</exception>
    public static bool IsFloatingPointType(Type type)
    {
        ArgumentNullException.ThrowIfNull(type);

        var underlyingType = Nullable.GetUnderlyingType(type) ?? type;

        return underlyingType == typeof(float) ||
               underlyingType == typeof(double) ||
               underlyingType == typeof(decimal);
    }

    /// <summary>
    /// 检查类型是否为布尔类型。
    /// </summary>
    /// <param name="type">类型。</param>
    /// <returns>是否为布尔类型。</returns>
    /// <exception cref="ArgumentNullException">type 为 null 时抛出。</exception>
    public static bool IsBooleanType(Type type)
    {
        ArgumentNullException.ThrowIfNull(type);

        var underlyingType = Nullable.GetUnderlyingType(type) ?? type;
        return underlyingType == typeof(bool);
    }

    /// <summary>
    /// 检查类型是否为字符串类型。
    /// </summary>
    /// <param name="type">类型。</param>
    /// <returns>是否为字符串类型。</returns>
    /// <exception cref="ArgumentNullException">type 为 null 时抛出。</exception>
    public static bool IsStringType(Type type)
    {
        ArgumentNullException.ThrowIfNull(type);
        return type == typeof(string);
    }

    /// <summary>
    /// 检查类型是否为日期时间类型。
    /// </summary>
    /// <param name="type">类型。</param>
    /// <returns>是否为日期时间类型。</returns>
    /// <exception cref="ArgumentNullException">type 为 null 时抛出。</exception>
    public static bool IsDateTimeType(Type type)
    {
        ArgumentNullException.ThrowIfNull(type);

        var underlyingType = Nullable.GetUnderlyingType(type) ?? type;
        return underlyingType == typeof(DateTime) ||
               underlyingType == typeof(DateTimeOffset) ||
               underlyingType == typeof(DateOnly) ||
               underlyingType == typeof(TimeOnly);
    }

    /// <summary>
    /// 检查类型是否为集合类型。
    /// </summary>
    /// <param name="type">类型。</param>
    /// <returns>是否为集合类型。</returns>
    /// <exception cref="ArgumentNullException">type 为 null 时抛出。</exception>
    public static bool IsCollectionType(Type type)
    {
        ArgumentNullException.ThrowIfNull(type);

        return type.IsArray ||
               (type.IsGenericType && typeof(System.Collections.IEnumerable).IsAssignableFrom(type)) ||
               typeof(System.Collections.IEnumerable).IsAssignableFrom(type) && type != typeof(string);
    }

    /// <summary>
    /// 检查类型是否为字典类型。
    /// </summary>
    /// <param name="type">类型。</param>
    /// <returns>是否为字典类型。</returns>
    /// <exception cref="ArgumentNullException">type 为 null 时抛出。</exception>
    public static bool IsDictionaryType(Type type)
    {
        ArgumentNullException.ThrowIfNull(type);

        if (!type.IsGenericType)
            return false;

        var genericDef = type.GetGenericTypeDefinition();
        return genericDef == typeof(Dictionary<,>) ||
               genericDef == typeof(IDictionary<,>) ||
               genericDef == typeof(IReadOnlyDictionary<,>) ||
               typeof(System.Collections.IDictionary).IsAssignableFrom(type);
    }

    /// <summary>
    /// 检查类型是否为可空类型。
    /// </summary>
    /// <param name="type">类型。</param>
    /// <returns>是否为可空类型。</returns>
    /// <exception cref="ArgumentNullException">type 为 null 时抛出。</exception>
    public static bool IsNullableType(Type type)
    {
        ArgumentNullException.ThrowIfNull(type);
        return type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>);
    }

    /// <summary>
    /// 获取可空类型的基础类型。
    /// </summary>
    /// <param name="type">可空类型。</param>
    /// <returns>基础类型，如果不是可空类型则返回原类型。</returns>
    /// <exception cref="ArgumentNullException">type 为 null 时抛出。</exception>
    public static Type GetNullableUnderlyingType(Type type)
    {
        ArgumentNullException.ThrowIfNull(type);
        return Nullable.GetUnderlyingType(type) ?? type;
    }

    /// <summary>
    /// 检查类型是否为枚举类型。
    /// </summary>
    /// <param name="type">类型。</param>
    /// <returns>是否为枚举类型。</returns>
    /// <exception cref="ArgumentNullException">type 为 null 时抛出。</exception>
    public static bool IsEnumType(Type type)
    {
        ArgumentNullException.ThrowIfNull(type);

        var underlyingType = Nullable.GetUnderlyingType(type) ?? type;
        return underlyingType.IsEnum;
    }

    /// <summary>
    /// 获取枚举的所有值。
    /// </summary>
    /// <param name="enumType">枚举类型。</param>
    /// <returns>枚举值数组。</returns>
    /// <exception cref="ArgumentNullException">enumType 为 null 时抛出。</exception>
    /// <exception cref="ArgumentException">类型不是枚举类型时抛出。</exception>
    public static Array GetEnumValues(Type enumType)
    {
        ArgumentNullException.ThrowIfNull(enumType);

        var underlyingType = Nullable.GetUnderlyingType(enumType) ?? enumType;
        if (!underlyingType.IsEnum)
            throw new ArgumentException("类型不是枚举类型", nameof(enumType));

        return Enum.GetValues(underlyingType);
    }

    /// <summary>
    /// 获取枚举的所有名称。
    /// </summary>
    /// <param name="enumType">枚举类型。</param>
    /// <returns>枚举名称数组。</returns>
    /// <exception cref="ArgumentNullException">enumType 为 null 时抛出。</exception>
    /// <exception cref="ArgumentException">类型不是枚举类型时抛出。</exception>
    public static string[] GetEnumNames(Type enumType)
    {
        ArgumentNullException.ThrowIfNull(enumType);

        var underlyingType = Nullable.GetUnderlyingType(enumType) ?? enumType;
        if (!underlyingType.IsEnum)
            throw new ArgumentException("类型不是枚举类型", nameof(enumType));

        return Enum.GetNames(underlyingType);
    }

    /// <summary>
    /// 获取枚举值的显示名称。
    /// </summary>
    /// <param name="enumValue">枚举值。</param>
    /// <returns>显示名称。</returns>
    public static string? GetEnumDisplayName(object enumValue)
    {
        if (enumValue == null) return null;

        var type = enumValue.GetType();
        if (!type.IsEnum) return enumValue.ToString();

        var name = Enum.GetName(type, enumValue);
        if (name == null) return enumValue.ToString();

        var field = type.GetField(name);
        if (field == null) return name;

        var displayAttribute = field.GetCustomAttribute<DisplayAttribute>();
        if (displayAttribute != null && !string.IsNullOrEmpty(displayAttribute.Name))
            return displayAttribute.Name;

        var descriptionAttribute = field.GetCustomAttribute<DescriptionAttribute>();
        if (descriptionAttribute != null && !string.IsNullOrEmpty(descriptionAttribute.Description))
            return descriptionAttribute.Description;

        return name;
    }

    /// <summary>
    /// 检查两个类型是否兼容。
    /// </summary>
    /// <param name="fromType">源类型。</param>
    /// <param name="toType">目标类型。</param>
    /// <returns>是否兼容。</returns>
    /// <exception cref="ArgumentNullException">fromType 或 toType 为 null 时抛出。</exception>
    public static bool IsCompatibleType(Type fromType, Type toType)
    {
        ArgumentNullException.ThrowIfNull(fromType);
        ArgumentNullException.ThrowIfNull(toType);

        if (fromType == toType) return true;

        if (toType.IsAssignableFrom(fromType)) return true;

        if (IsNumericType(fromType) && IsNumericType(toType)) return true;

        if (IsNullableType(toType) && IsCompatibleType(fromType, GetNullableUnderlyingType(toType))) return true;

        return false;
    }

    #endregion

    #region 高级反射操作

    /// <summary>
    /// 获取类型的所有扩展方法。
    /// </summary>
    /// <param name="type">目标类型。</param>
    /// <param name="assembly">程序集。</param>
    /// <returns>扩展方法信息数组。</returns>
    /// <exception cref="ArgumentNullException">type 或 assembly 为 null 时抛出。</exception>
    public static MethodInfo[] GetExtensionMethods(Type type, Assembly assembly)
    {
        ArgumentNullException.ThrowIfNull(type);
        ArgumentNullException.ThrowIfNull(assembly);

        var query = from typeInfo in assembly.DefinedTypes
                    where typeInfo.IsSealed && !typeInfo.IsGenericType && !typeInfo.IsNested
                    from method in typeInfo.DeclaredMethods
                    where method.IsStatic && method.IsPublic && method.IsDefined(typeof(ExtensionAttribute), false)
                    let parameters = method.GetParameters()
                    where parameters.Length > 0 && parameters[0].ParameterType == type
                    select method;

        return query.ToArray();
    }

    /// <summary>
    /// 获取程序集中的所有类型。
    /// </summary>
    /// <param name="assembly">程序集。</param>
    /// <returns>类型数组。</returns>
    /// <exception cref="ArgumentNullException">assembly 为 null 时抛出。</exception>
    public static Type[] GetTypes(Assembly assembly)
    {
        ArgumentNullException.ThrowIfNull(assembly);
        return assembly.GetTypes();
    }

    /// <summary>
    /// 根据条件筛选程序集中的类型。
    /// </summary>
    /// <param name="assembly">程序集。</param>
    /// <param name="predicate">筛选条件。</param>
    /// <returns>类型数组。</returns>
    /// <exception cref="ArgumentNullException">assembly 为 null 时抛出。</exception>
    public static Type[] GetTypes(Assembly assembly, Func<Type, bool> predicate)
    {
        ArgumentNullException.ThrowIfNull(assembly);
        ArgumentNullException.ThrowIfNull(predicate);

        return assembly.GetTypes().Where(predicate).ToArray();
    }

    /// <summary>
    /// 获取当前应用程序域中的所有程序集。
    /// </summary>
    /// <returns>程序集数组。</returns>
    public static Assembly[] GetAssemblies()
    {
        return AppDomain.CurrentDomain.GetAssemblies();
    }

    /// <summary>
    /// 根据名称加载程序集。
    /// </summary>
    /// <param name="assemblyName">程序集名称。</param>
    /// <returns>程序集。</returns>
    /// <exception cref="ArgumentNullException">assemblyName 为 null 时抛出。</exception>
    /// <exception cref="FileNotFoundException">程序集未找到时抛出。</exception>
    public static Assembly LoadAssembly(string assemblyName)
    {
        ArgumentException.ThrowIfNullOrEmpty(assemblyName);
        return Assembly.Load(assemblyName);
    }

    /// <summary>
    /// 从文件加载程序集。
    /// </summary>
    /// <param name="assemblyFile">程序集文件路径。</param>
    /// <returns>程序集。</returns>
    /// <exception cref="ArgumentNullException">assemblyFile 为 null 时抛出。</exception>
    /// <exception cref="FileNotFoundException">文件未找到时抛出。</exception>
    public static Assembly LoadAssemblyFromFile(string assemblyFile)
    {
        ArgumentException.ThrowIfNullOrEmpty(assemblyFile);
        return Assembly.LoadFrom(assemblyFile);
    }

    /// <summary>
    /// 检查类型是否为匿名类型。
    /// </summary>
    /// <param name="type">类型。</param>
    /// <returns>是否为匿名类型。</returns>
    /// <exception cref="ArgumentNullException">type 为 null 时抛出。</exception>
    public static bool IsAnonymousType(Type type)
    {
        ArgumentNullException.ThrowIfNull(type);

        return type.Name.StartsWith("<>f__AnonymousType") ||
               type.Name.StartsWith("VB$AnonymousType");
    }

    /// <summary>
    /// 获取类型的所有嵌套类型。
    /// </summary>
    /// <param name="type">类型。</param>
    /// <returns>嵌套类型数组。</returns>
    /// <exception cref="ArgumentNullException">type 为 null 时抛出。</exception>
    public static Type[] GetNestedTypes(Type type)
    {
        ArgumentNullException.ThrowIfNull(type);
        return type.GetNestedTypes(BindingFlags.Public | BindingFlags.NonPublic);
    }

    /// <summary>
    /// 获取类型的事件信息。
    /// </summary>
    /// <param name="type">类型。</param>
    /// <returns>事件信息数组。</returns>
    /// <exception cref="ArgumentNullException">type 为 null 时抛出。</exception>
    public static EventInfo[] GetEvents(Type type)
    {
        ArgumentNullException.ThrowIfNull(type);
        return type.GetEvents(BindingFlags.Public | BindingFlags.Instance);
    }

    /// <summary>
    /// 获取类型的完整信息。
    /// </summary>
    /// <param name="type">类型。</param>
    /// <returns>类型信息对象。</returns>
    /// <exception cref="ArgumentNullException">type 为 null 时抛出。</exception>
    /// <example>
    /// <code>
    /// var info = ReflectHelper.GetTypeInfo(typeof(MyClass));
    /// Console.WriteLine($"类型: {info.Name}");
    /// Console.WriteLine($"属性数: {info.Properties.Length}");
    /// Console.WriteLine($"方法数: {info.Methods.Length}");
    /// </code>
    /// </example>
    public static TypeDetailedInfo GetDetailedTypeInfo(Type type)
    {
        ArgumentNullException.ThrowIfNull(type);

        return new TypeDetailedInfo
        {
            Name = type.Name,
            FullName = type.FullName,
            Namespace = type.Namespace,
            Assembly = type.Assembly.FullName ?? string.Empty,
            BaseType = type.BaseType?.Name,
            IsAbstract = type.IsAbstract,
            IsInterface = type.IsInterface,
            IsEnum = type.IsEnum,
            IsGenericType = type.IsGenericType,
            IsValueType = type.IsValueType,
            Properties = GetPublicProperties(type).Select(p => p.Name).ToArray(),
            Methods = GetPublicMethods(type).Select(m => m.Name).ToArray(),
            Fields = GetPublicFields(type).Select(f => f.Name).ToArray(),
            Interfaces = GetInterfaces(type).Select(i => i.Name).ToArray()
        };
    }

    #endregion

    #region 私有方法

    /// <summary>
    /// 转换值到目标类型。
    /// </summary>
    private static object? ConvertValue(object? value, Type targetType)
    {
        if (value == null)
            return GetDefaultValue(targetType);

        if (targetType.IsAssignableFrom(value.GetType()))
            return value;

        var underlyingType = Nullable.GetUnderlyingType(targetType);
        if (underlyingType != null)
        {
            if (value == null || value.ToString() == string.Empty)
                return null;

            return Convert.ChangeType(value, underlyingType);
        }

        return Convert.ChangeType(value, targetType);
    }

    #endregion
}

#region 辅助类定义

/// <summary>
/// 类型详细信息。
/// </summary>
public class TypeDetailedInfo
{
    /// <summary>
    /// 类型名称。
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// 完整名称。
    /// </summary>
    public string? FullName { get; set; }

    /// <summary>
    /// 命名空间。
    /// </summary>
    public string? Namespace { get; set; }

    /// <summary>
    /// 程序集。
    /// </summary>
    public string Assembly { get; set; } = string.Empty;

    /// <summary>
    /// 基类型名称。
    /// </summary>
    public string? BaseType { get; set; }

    /// <summary>
    /// 是否为抽象类型。
    /// </summary>
    public bool IsAbstract { get; set; }

    /// <summary>
    /// 是否为接口。
    /// </summary>
    public bool IsInterface { get; set; }

    /// <summary>
    /// 是否为枚举。
    /// </summary>
    public bool IsEnum { get; set; }

    /// <summary>
    /// 是否为泛型类型。
    /// </summary>
    public bool IsGenericType { get; set; }

    /// <summary>
    /// 是否为值类型。
    /// </summary>
    public bool IsValueType { get; set; }

    /// <summary>
    /// 属性名称列表。
    /// </summary>
    public string[] Properties { get; set; } = Array.Empty<string>();

    /// <summary>
    /// 方法名称列表。
    /// </summary>
    public string[] Methods { get; set; } = Array.Empty<string>();

    /// <summary>
    /// 字段名称列表。
    /// </summary>
    public string[] Fields { get; set; } = Array.Empty<string>();

    /// <summary>
    /// 接口名称列表。
    /// </summary>
    public string[] Interfaces { get; set; } = Array.Empty<string>();

    /// <summary>
    /// 返回类型名称。
    /// </summary>
    public override string ToString()
    {
        return Name;
    }
}

#endregion
