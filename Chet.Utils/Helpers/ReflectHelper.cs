using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace Chet.Utils.Helpers
{
    /// <summary>
    /// 高级反射操作帮助类
    /// 提供类型反射、属性操作、方法调用、动态创建等功能
    /// </summary>
    public static class ReflectHelper
    {
        #region 类型信息获取

        /// <summary>
        /// 获取类型的所有公共属性
        /// </summary>
        /// <param name="type">目标类型</param>
        /// <returns>属性信息数组</returns>
        public static PropertyInfo[] GetPublicProperties(Type type)
        {
            return type.GetProperties(BindingFlags.Public | BindingFlags.Instance);
        }

        /// <summary>
        /// 获取类型的所有属性（包括私有）
        /// </summary>
        /// <param name="type">目标类型</param>
        /// <returns>属性信息数组</returns>
        public static PropertyInfo[] GetAllProperties(Type type)
        {
            return type.GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
        }

        /// <summary>
        /// 获取类型的所有公共方法
        /// </summary>
        /// <param name="type">目标类型</param>
        /// <returns>方法信息数组</returns>
        public static MethodInfo[] GetPublicMethods(Type type)
        {
            return type.GetMethods(BindingFlags.Public | BindingFlags.Instance);
        }

        /// <summary>
        /// 获取类型的所有方法（包括私有）
        /// </summary>
        /// <param name="type">目标类型</param>
        /// <returns>方法信息数组</returns>
        public static MethodInfo[] GetAllMethods(Type type)
        {
            return type.GetMethods(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
        }

        /// <summary>
        /// 获取类型的所有公共字段
        /// </summary>
        /// <param name="type">目标类型</param>
        /// <returns>字段信息数组</returns>
        public static FieldInfo[] GetPublicFields(Type type)
        {
            return type.GetFields(BindingFlags.Public | BindingFlags.Instance);
        }

        /// <summary>
        /// 获取类型的所有字段（包括私有）
        /// </summary>
        /// <param name="type">目标类型</param>
        /// <returns>字段信息数组</returns>
        public static FieldInfo[] GetAllFields(Type type)
        {
            return type.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
        }

        /// <summary>
        /// 获取类型的特性
        /// </summary>
        /// <typeparam name="T">特性类型</typeparam>
        /// <param name="type">目标类型</param>
        /// <param name="inherit">是否继承</param>
        /// <returns>特性实例</returns>
        public static T GetCustomAttribute<T>(Type type, bool inherit = true) where T : Attribute
        {
            return type.GetCustomAttribute<T>(inherit);
        }

        /// <summary>
        /// 获取类型的所有特性
        /// </summary>
        /// <param name="type">目标类型</param>
        /// <param name="inherit">是否继承</param>
        /// <returns>特性数组</returns>
        public static Attribute[] GetCustomAttributes(Type type, bool inherit = true)
        {
            return Attribute.GetCustomAttributes(type, inherit);
        }

        /// <summary>
        /// 检查类型是否具有指定特性
        /// </summary>
        /// <typeparam name="T">特性类型</typeparam>
        /// <param name="type">目标类型</param>
        /// <param name="inherit">是否继承</param>
        /// <returns>是否具有特性</returns>
        public static bool HasCustomAttribute<T>(Type type, bool inherit = true) where T : Attribute
        {
            return type.IsDefined(typeof(T), inherit);
        }

        /// <summary>
        /// 获取类型实现的所有接口
        /// </summary>
        /// <param name="type">目标类型</param>
        /// <returns>接口类型数组</returns>
        public static Type[] GetInterfaces(Type type)
        {
            return type.GetInterfaces();
        }

        /// <summary>
        /// 检查类型是否实现指定接口
        /// </summary>
        /// <param name="type">目标类型</param>
        /// <param name="interfaceType">接口类型</param>
        /// <returns>是否实现接口</returns>
        public static bool ImplementsInterface(Type type, Type interfaceType)
        {
            return interfaceType.IsAssignableFrom(type);
        }

        /// <summary>
        /// 获取类型的基类型
        /// </summary>
        /// <param name="type">目标类型</param>
        /// <returns>基类型</returns>
        public static Type GetBaseType(Type type)
        {
            return type.BaseType;
        }

        /// <summary>
        /// 检查类型是否继承自指定类型
        /// </summary>
        /// <param name="type">目标类型</param>
        /// <param name="baseType">基类型</param>
        /// <returns>是否继承</returns>
        public static bool IsSubclassOf(Type type, Type baseType)
        {
            return type.IsSubclassOf(baseType);
        }

        #endregion

        #region 属性操作

        /// <summary>
        /// 获取对象属性值
        /// </summary>
        /// <param name="obj">目标对象</param>
        /// <param name="propertyName">属性名称</param>
        /// <returns>属性值</returns>
        public static object GetPropertyValue(object obj, string propertyName)
        {
            if (obj == null) throw new ArgumentNullException(nameof(obj));
            if (string.IsNullOrEmpty(propertyName)) throw new ArgumentException("属性名称不能为空", nameof(propertyName));

            var property = obj.GetType().GetProperty(propertyName);
            if (property == null) throw new ArgumentException($"属性 '{propertyName}' 不存在");

            return property.GetValue(obj);
        }

        /// <summary>
        /// 获取对象属性值（泛型版本）
        /// </summary>
        /// <typeparam name="T">属性类型</typeparam>
        /// <param name="obj">目标对象</param>
        /// <param name="propertyName">属性名称</param>
        /// <returns>属性值</returns>
        public static T GetPropertyValue<T>(object obj, string propertyName)
        {
            return (T)GetPropertyValue(obj, propertyName);
        }

        /// <summary>
        /// 设置对象属性值
        /// </summary>
        /// <param name="obj">目标对象</param>
        /// <param name="propertyName">属性名称</param>
        /// <param name="value">属性值</param>
        public static void SetPropertyValue(object obj, string propertyName, object value)
        {
            if (obj == null) throw new ArgumentNullException(nameof(obj));
            if (string.IsNullOrEmpty(propertyName)) throw new ArgumentException("属性名称不能为空", nameof(propertyName));

            var property = obj.GetType().GetProperty(propertyName);
            if (property == null) throw new ArgumentException($"属性 '{propertyName}' 不存在");

            property.SetValue(obj, value);
        }

        /// <summary>
        /// 获取属性的特性
        /// </summary>
        /// <typeparam name="T">特性类型</typeparam>
        /// <param name="property">属性信息</param>
        /// <param name="inherit">是否继承</param>
        /// <returns>特性实例</returns>
        public static T GetCustomAttribute<T>(PropertyInfo property, bool inherit = true) where T : Attribute
        {
            return property.GetCustomAttribute<T>(inherit);
        }

        /// <summary>
        /// 检查属性是否具有指定特性
        /// </summary>
        /// <typeparam name="T">特性类型</typeparam>
        /// <param name="property">属性信息</param>
        /// <param name="inherit">是否继承</param>
        /// <returns>是否具有特性</returns>
        public static bool HasCustomAttribute<T>(PropertyInfo property, bool inherit = true) where T : Attribute
        {
            return property.IsDefined(typeof(T), inherit);
        }

        /// <summary>
        /// 获取属性的显示名称
        /// </summary>
        /// <param name="property">属性信息</param>
        /// <returns>显示名称</returns>
        public static string GetPropertyDisplayName(PropertyInfo property)
        {
            var displayAttribute = GetCustomAttribute<DisplayAttribute>(property);
            if (displayAttribute != null)
                return displayAttribute.Name ?? property.Name;

            var displayNameAttribute = GetCustomAttribute<DisplayNameAttribute>(property);
            if (displayNameAttribute != null)
                return displayNameAttribute.DisplayName;

            return property.Name;
        }

        /// <summary>
        /// 获取属性的描述信息
        /// </summary>
        /// <param name="property">属性信息</param>
        /// <returns>描述信息</returns>
        public static string GetPropertyDescription(PropertyInfo property)
        {
            var displayAttribute = GetCustomAttribute<DisplayAttribute>(property);
            if (displayAttribute != null)
                return displayAttribute.Description;

            var descriptionAttribute = GetCustomAttribute<DescriptionAttribute>(property);
            if (descriptionAttribute != null)
                return descriptionAttribute.Description;

            return string.Empty;
        }

        /// <summary>
        /// 获取对象所有属性的名称和值
        /// </summary>
        /// <param name="obj">目标对象</param>
        /// <returns>属性名称和值的字典</returns>
        public static Dictionary<string, object> GetPropertyValues(object obj)
        {
            if (obj == null) return new Dictionary<string, object>();

            var properties = GetPublicProperties(obj.GetType());
            var result = new Dictionary<string, object>();

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
        /// 批量设置对象属性值
        /// </summary>
        /// <param name="obj">目标对象</param>
        /// <param name="propertyValues">属性值字典</param>
        public static void SetPropertyValues(object obj, Dictionary<string, object> propertyValues)
        {
            if (obj == null) throw new ArgumentNullException(nameof(obj));
            if (propertyValues == null) return;

            var properties = GetPublicProperties(obj.GetType()).ToDictionary(p => p.Name);

            foreach (var kvp in propertyValues)
            {
                if (properties.ContainsKey(kvp.Key))
                {
                    var property = properties[kvp.Key];
                    try
                    {
                        var convertedValue = Convert.ChangeType(kvp.Value, property.PropertyType);
                        property.SetValue(obj, convertedValue);
                    }
                    catch
                    {
                        // 转换失败时跳过
                    }
                }
            }
        }

        #endregion

        #region 方法调用

        /// <summary>
        /// 调用对象方法
        /// </summary>
        /// <param name="obj">目标对象</param>
        /// <param name="methodName">方法名称</param>
        /// <param name="parameters">参数数组</param>
        /// <returns>方法返回值</returns>
        public static object InvokeMethod(object obj, string methodName, params object[] parameters)
        {
            if (obj == null) throw new ArgumentNullException(nameof(obj));
            if (string.IsNullOrEmpty(methodName)) throw new ArgumentException("方法名称不能为空", nameof(methodName));

            var method = obj.GetType().GetMethod(methodName, BindingFlags.Public | BindingFlags.Instance);
            if (method == null) throw new ArgumentException($"方法 '{methodName}' 不存在");

            return method.Invoke(obj, parameters);
        }

        /// <summary>
        /// 调用对象方法（泛型版本）
        /// </summary>
        /// <typeparam name="T">返回值类型</typeparam>
        /// <param name="obj">目标对象</param>
        /// <param name="methodName">方法名称</param>
        /// <param name="parameters">参数数组</param>
        /// <returns>方法返回值</returns>
        public static T InvokeMethod<T>(object obj, string methodName, params object[] parameters)
        {
            return (T)InvokeMethod(obj, methodName, parameters);
        }

        /// <summary>
        /// 调用静态方法
        /// </summary>
        /// <param name="type">目标类型</param>
        /// <param name="methodName">方法名称</param>
        /// <param name="parameters">参数数组</param>
        /// <returns>方法返回值</returns>
        public static object InvokeStaticMethod(Type type, string methodName, params object[] parameters)
        {
            if (type == null) throw new ArgumentNullException(nameof(type));
            if (string.IsNullOrEmpty(methodName)) throw new ArgumentException("方法名称不能为空", nameof(methodName));

            var method = type.GetMethod(methodName, BindingFlags.Public | BindingFlags.Static);
            if (method == null) throw new ArgumentException($"静态方法 '{methodName}' 不存在");

            return method.Invoke(null, parameters);
        }

        /// <summary>
        /// 调用静态方法（泛型版本）
        /// </summary>
        /// <typeparam name="T">返回值类型</typeparam>
        /// <param name="type">目标类型</param>
        /// <param name="methodName">方法名称</param>
        /// <param name="parameters">参数数组</param>
        /// <returns>方法返回值</returns>
        public static T InvokeStaticMethod<T>(Type type, string methodName, params object[] parameters)
        {
            return (T)InvokeStaticMethod(type, methodName, parameters);
        }

        /// <summary>
        /// 获取方法的特性
        /// </summary>
        /// <typeparam name="T">特性类型</typeparam>
        /// <param name="method">方法信息</param>
        /// <param name="inherit">是否继承</param>
        /// <returns>特性实例</returns>
        public static T GetCustomAttribute<T>(MethodInfo method, bool inherit = true) where T : Attribute
        {
            return method.GetCustomAttribute<T>(inherit);
        }

        /// <summary>
        /// 检查方法是否具有指定特性
        /// </summary>
        /// <typeparam name="T">特性类型</typeparam>
        /// <param name="method">方法信息</param>
        /// <param name="inherit">是否继承</param>
        /// <returns>是否具有特性</returns>
        public static bool HasCustomAttribute<T>(MethodInfo method, bool inherit = true) where T : Attribute
        {
            return method.IsDefined(typeof(T), inherit);
        }

        /// <summary>
        /// 获取方法参数信息
        /// </summary>
        /// <param name="method">方法信息</param>
        /// <returns>参数信息数组</returns>
        public static ParameterInfo[] GetMethodParameters(MethodInfo method)
        {
            return method.GetParameters();
        }

        /// <summary>
        /// 获取方法返回值类型
        /// </summary>
        /// <param name="method">方法信息</param>
        /// <returns>返回值类型</returns>
        public static Type GetMethodReturnType(MethodInfo method)
        {
            return method.ReturnType;
        }

        /// <summary>
        /// 检查方法是否为异步方法
        /// </summary>
        /// <param name="method">方法信息</param>
        /// <returns>是否为异步方法</returns>
        public static bool IsAsyncMethod(MethodInfo method)
        {
            return method.ReturnType == typeof(void) ? false :
                   method.ReturnType.BaseType == typeof(Task) ||
                   method.ReturnType == typeof(Task);
        }

        #endregion

        #region 字段操作

        /// <summary>
        /// 获取对象字段值
        /// </summary>
        /// <param name="obj">目标对象</param>
        /// <param name="fieldName">字段名称</param>
        /// <returns>字段值</returns>
        public static object GetFieldValue(object obj, string fieldName)
        {
            if (obj == null) throw new ArgumentNullException(nameof(obj));
            if (string.IsNullOrEmpty(fieldName)) throw new ArgumentException("字段名称不能为空", nameof(fieldName));

            var field = obj.GetType().GetField(fieldName, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
            if (field == null) throw new ArgumentException($"字段 '{fieldName}' 不存在");

            return field.GetValue(obj);
        }

        /// <summary>
        /// 获取对象字段值（泛型版本）
        /// </summary>
        /// <typeparam name="T">字段类型</typeparam>
        /// <param name="obj">目标对象</param>
        /// <param name="fieldName">字段名称</param>
        /// <returns>字段值</returns>
        public static T GetFieldValue<T>(object obj, string fieldName)
        {
            return (T)GetFieldValue(obj, fieldName);
        }

        /// <summary>
        /// 设置对象字段值
        /// </summary>
        /// <param name="obj">目标对象</param>
        /// <param name="fieldName">字段名称</param>
        /// <param name="value">字段值</param>
        public static void SetFieldValue(object obj, string fieldName, object value)
        {
            if (obj == null) throw new ArgumentNullException(nameof(obj));
            if (string.IsNullOrEmpty(fieldName)) throw new ArgumentException("字段名称不能为空", nameof(fieldName));

            var field = obj.GetType().GetField(fieldName, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
            if (field == null) throw new ArgumentException($"字段 '{fieldName}' 不存在");

            field.SetValue(obj, value);
        }

        /// <summary>
        /// 获取字段的特性
        /// </summary>
        /// <typeparam name="T">特性类型</typeparam>
        /// <param name="field">字段信息</param>
        /// <param name="inherit">是否继承</param>
        /// <returns>特性实例</returns>
        public static T GetCustomAttribute<T>(FieldInfo field, bool inherit = true) where T : Attribute
        {
            return field.GetCustomAttribute<T>(inherit);
        }

        /// <summary>
        /// 检查字段是否具有指定特性
        /// </summary>
        /// <typeparam name="T">特性类型</typeparam>
        /// <param name="field">字段信息</param>
        /// <param name="inherit">是否继承</param>
        /// <returns>是否具有特性</returns>
        public static bool HasCustomAttribute<T>(FieldInfo field, bool inherit = true) where T : Attribute
        {
            return field.IsDefined(typeof(T), inherit);
        }

        #endregion

        #region 动态创建与实例化

        /// <summary>
        /// 创建类型实例
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <returns>实例</returns>
        public static T CreateInstance<T>() where T : new()
        {
            return new T();
        }

        /// <summary>
        /// 创建类型实例（通过反射）
        /// </summary>
        /// <param name="type">类型</param>
        /// <param name="args">构造函数参数</param>
        /// <returns>实例</returns>
        public static object CreateInstance(Type type, params object[] args)
        {
            if (type == null) throw new ArgumentNullException(nameof(type));

            return Activator.CreateInstance(type, args);
        }

        /// <summary>
        /// 创建类型实例（泛型版本）
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="args">构造函数参数</param>
        /// <returns>实例</returns>
        public static T CreateInstance<T>(params object[] args)
        {
            return (T)CreateInstance(typeof(T), args);
        }

        /// <summary>
        /// 通过无参构造函数创建类型实例
        /// </summary>
        /// <param name="typeName">类型名称</param>
        /// <param name="assemblyName">程序集名称</param>
        /// <returns>实例</returns>
        public static object CreateInstance(string typeName, string assemblyName = null)
        {
            Type type;
            if (string.IsNullOrEmpty(assemblyName))
            {
                type = Type.GetType(typeName);
            }
            else
            {
                var assembly = Assembly.Load(assemblyName);
                type = assembly.GetType(typeName);
            }

            if (type == null) throw new ArgumentException($"类型 '{typeName}' 未找到");

            return Activator.CreateInstance(type);
        }

        /// <summary>
        /// 获取类型的默认值
        /// </summary>
        /// <param name="type">类型</param>
        /// <returns>默认值</returns>
        public static object GetDefaultValue(Type type)
        {
            if (type == null) return null;
            if (type.IsValueType)
            {
                return Activator.CreateInstance(type);
            }
            return null;
        }

        /// <summary>
        /// 检查类型是否可以被实例化
        /// </summary>
        /// <param name="type">类型</param>
        /// <returns>是否可以被实例化</returns>
        public static bool IsInstantiable(Type type)
        {
            return !type.IsAbstract && !type.IsInterface && type.GetConstructor(Type.EmptyTypes) != null;
        }

        /// <summary>
        /// 获取类型的所有构造函数
        /// </summary>
        /// <param name="type">类型</param>
        /// <returns>构造函数信息数组</returns>
        public static ConstructorInfo[] GetConstructors(Type type)
        {
            return type.GetConstructors(BindingFlags.Public | BindingFlags.Instance);
        }

        /// <summary>
        /// 获取类型的所有构造函数（包括私有）
        /// </summary>
        /// <param name="type">类型</param>
        /// <returns>构造函数信息数组</returns>
        public static ConstructorInfo[] GetAllConstructors(Type type)
        {
            return type.GetConstructors(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
        }

        #endregion

        #region 表达式树操作

        /// <summary>
        /// 获取属性名称（通过表达式树）
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <typeparam name="TProperty">属性类型</typeparam>
        /// <param name="propertyExpression">属性表达式</param>
        /// <returns>属性名称</returns>
        public static string GetPropertyName<T, TProperty>(Expression<Func<T, TProperty>> propertyExpression)
        {
            if (propertyExpression == null) throw new ArgumentNullException(nameof(propertyExpression));

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
        /// 获取属性值（通过表达式树）
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <typeparam name="TProperty">属性类型</typeparam>
        /// <param name="obj">目标对象</param>
        /// <param name="propertyExpression">属性表达式</param>
        /// <returns>属性值</returns>
        public static TProperty GetPropertyValue<T, TProperty>(T obj, Expression<Func<T, TProperty>> propertyExpression)
        {
            if (obj == null) throw new ArgumentNullException(nameof(obj));
            if (propertyExpression == null) throw new ArgumentNullException(nameof(propertyExpression));

            return propertyExpression.Compile()(obj);
        }

        /// <summary>
        /// 设置属性值（通过表达式树）
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <typeparam name="TProperty">属性类型</typeparam>
        /// <param name="obj">目标对象</param>
        /// <param name="propertyExpression">属性表达式</param>
        /// <param name="value">属性值</param>
        public static void SetPropertyValue<T, TProperty>(T obj, Expression<Func<T, TProperty>> propertyExpression, TProperty value)
        {
            if (obj == null) throw new ArgumentNullException(nameof(obj));
            if (propertyExpression == null) throw new ArgumentNullException(nameof(propertyExpression));

            var memberExpression = propertyExpression.Body as MemberExpression;
            if (memberExpression == null)
                throw new ArgumentException("表达式不是有效的属性表达式", nameof(propertyExpression));

            var propertyInfo = memberExpression.Member as PropertyInfo;
            if (propertyInfo == null)
                throw new ArgumentException("表达式不是有效的属性表达式", nameof(propertyExpression));

            propertyInfo.SetValue(obj, value);
        }

        /// <summary>
        /// 创建属性获取器（通过表达式树）
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <typeparam name="TProperty">属性类型</typeparam>
        /// <param name="propertyExpression">属性表达式</param>
        /// <returns>属性获取器函数</returns>
        public static Func<T, TProperty> CreatePropertyGetter<T, TProperty>(Expression<Func<T, TProperty>> propertyExpression)
        {
            if (propertyExpression == null) throw new ArgumentNullException(nameof(propertyExpression));
            return propertyExpression.Compile();
        }

        /// <summary>
        /// 创建属性设置器（通过表达式树）
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <typeparam name="TProperty">属性类型</typeparam>
        /// <param name="propertyExpression">属性表达式</param>
        /// <returns>属性设置器函数</returns>
        public static Action<T, TProperty> CreatePropertySetter<T, TProperty>(Expression<Func<T, TProperty>> propertyExpression)
        {
            if (propertyExpression == null) throw new ArgumentNullException(nameof(propertyExpression));

            var memberExpression = propertyExpression.Body as MemberExpression;
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
        /// 创建方法调用器（通过表达式树）
        /// </summary>
        /// <param name="method">方法信息</param>
        /// <returns>方法调用器函数</returns>
        public static Func<object, object[], object> CreateMethodCaller(MethodInfo method)
        {
            if (method == null) throw new ArgumentNullException(nameof(method));

            var instanceParameter = Expression.Parameter(typeof(object), "instance");
            var parametersParameter = Expression.Parameter(typeof(object[]), "parameters");

            var paramExpressions = new List<Expression>();
            var parameters = method.GetParameters();

            for (int i = 0; i < parameters.Length; i++)
            {
                var indexedAccess = Expression.ArrayIndex(parametersParameter, Expression.Constant(i));
                var paramType = parameters[i].ParameterType;
                paramExpressions.Add(Expression.Convert(indexedAccess, paramType));
            }

            Expression instanceExpression;
            if (method.IsStatic)
            {
                instanceExpression = null;
            }
            else
            {
                instanceExpression = Expression.Convert(instanceParameter, method.DeclaringType);
            }

            var methodCall = Expression.Call(instanceExpression, method, paramExpressions);

            if (method.ReturnType == typeof(void))
            {
                var lambda = Expression.Lambda<Action<object, object[]>>(
                    methodCall, instanceParameter, parametersParameter);
                var action = lambda.Compile();
                return (instance, parameters) =>
                {
                    action(instance, parameters);
                    return null;
                };
            }
            else
            {
                var convertedMethodCall = Expression.Convert(methodCall, typeof(object));
                var lambda = Expression.Lambda<Func<object, object[], object>>(
                    convertedMethodCall, instanceParameter, parametersParameter);
                return lambda.Compile();
            }
        }

        #endregion

        #region 类型转换与检查

        /// <summary>
        /// 检查类型是否为数值类型
        /// </summary>
        /// <param name="type">类型</param>
        /// <returns>是否为数值类型</returns>
        public static bool IsNumericType(Type type)
        {
            if (type == null) return false;

            switch (Type.GetTypeCode(type))
            {
                case TypeCode.Byte:
                case TypeCode.SByte:
                case TypeCode.UInt16:
                case TypeCode.UInt32:
                case TypeCode.UInt64:
                case TypeCode.Int16:
                case TypeCode.Int32:
                case TypeCode.Int64:
                case TypeCode.Decimal:
                case TypeCode.Double:
                case TypeCode.Single:
                    return true;
                default:
                    return false;
            }
        }

        /// <summary>
        /// 检查类型是否为布尔类型
        /// </summary>
        /// <param name="type">类型</param>
        /// <returns>是否为布尔类型</returns>
        public static bool IsBooleanType(Type type)
        {
            return type == typeof(bool);
        }

        /// <summary>
        /// 检查类型是否为字符串类型
        /// </summary>
        /// <param name="type">类型</param>
        /// <returns>是否为字符串类型</returns>
        public static bool IsStringType(Type type)
        {
            return type == typeof(string);
        }

        /// <summary>
        /// 检查类型是否为集合类型
        /// </summary>
        /// <param name="type">类型</param>
        /// <returns>是否为集合类型</returns>
        public static bool IsCollectionType(Type type)
        {
            if (type == null) return false;

            return type.IsArray ||
                   (type.IsGenericType && typeof(System.Collections.IEnumerable).IsAssignableFrom(type)) ||
                   typeof(System.Collections.IEnumerable).IsAssignableFrom(type);
        }

        /// <summary>
        /// 检查类型是否为可空类型
        /// </summary>
        /// <param name="type">类型</param>
        /// <returns>是否为可空类型</returns>
        public static bool IsNullableType(Type type)
        {
            return type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>);
        }

        /// <summary>
        /// 获取可空类型的基础类型
        /// </summary>
        /// <param name="type">可空类型</param>
        /// <returns>基础类型</returns>
        public static Type GetNullableUnderlyingType(Type type)
        {
            return Nullable.GetUnderlyingType(type);
        }

        /// <summary>
        /// 检查类型是否为枚举类型
        /// </summary>
        /// <param name="type">类型</param>
        /// <returns>是否为枚举类型</returns>
        public static bool IsEnumType(Type type)
        {
            return type.IsEnum;
        }

        /// <summary>
        /// 获取枚举的所有值
        /// </summary>
        /// <param name="enumType">枚举类型</param>
        /// <returns>枚举值数组</returns>
        public static Array GetEnumValues(Type enumType)
        {
            if (!enumType.IsEnum) throw new ArgumentException("类型不是枚举类型", nameof(enumType));
            return Enum.GetValues(enumType);
        }

        /// <summary>
        /// 获取枚举的所有名称
        /// </summary>
        /// <param name="enumType">枚举类型</param>
        /// <returns>枚举名称数组</returns>
        public static string[] GetEnumNames(Type enumType)
        {
            if (!enumType.IsEnum) throw new ArgumentException("类型不是枚举类型", nameof(enumType));
            return Enum.GetNames(enumType);
        }

        /// <summary>
        /// 检查两个类型是否兼容
        /// </summary>
        /// <param name="fromType">源类型</param>
        /// <param name="toType">目标类型</param>
        /// <returns>是否兼容</returns>
        public static bool IsCompatibleType(Type fromType, Type toType)
        {
            if (fromType == null || toType == null) return false;
            if (fromType == toType) return true;

            return toType.IsAssignableFrom(fromType) ||
                   (IsNumericType(fromType) && IsNumericType(toType)) ||
                   (IsNullableType(toType) && IsCompatibleType(fromType, GetNullableUnderlyingType(toType)));
        }

        #endregion

        #region 高级反射操作

        /// <summary>
        /// 获取类型的所有扩展方法
        /// </summary>
        /// <param name="type">目标类型</param>
        /// <param name="assembly">程序集</param>
        /// <returns>扩展方法信息数组</returns>
        public static MethodInfo[] GetExtensionMethods(Type type, Assembly assembly)
        {
            if (type == null || assembly == null) return new MethodInfo[0];

            var query = from typeInfo in assembly.DefinedTypes
                        from method in typeInfo.DeclaredMethods
                        where method.IsStatic && method.IsPublic && method.IsDefined(typeof(ExtensionAttribute), false)
                        where method.GetParameters()[0].ParameterType == type
                        select method;

            return query.ToArray();
        }

        /// <summary>
        /// 获取程序集中的所有类型
        /// </summary>
        /// <param name="assembly">程序集</param>
        /// <returns>类型数组</returns>
        public static Type[] GetTypes(Assembly assembly)
        {
            return assembly.GetTypes();
        }

        /// <summary>
        /// 根据条件筛选程序集中的类型
        /// </summary>
        /// <param name="assembly">程序集</param>
        /// <param name="predicate">筛选条件</param>
        /// <returns>类型数组</returns>
        public static Type[] GetTypes(Assembly assembly, Func<Type, bool> predicate)
        {
            return assembly.GetTypes().Where(predicate).ToArray();
        }

        /// <summary>
        /// 获取当前应用程序域中的所有程序集
        /// </summary>
        /// <returns>程序集数组</returns>
        public static Assembly[] GetAssemblies()
        {
            return AppDomain.CurrentDomain.GetAssemblies();
        }

        /// <summary>
        /// 根据名称加载程序集
        /// </summary>
        /// <param name="assemblyName">程序集名称</param>
        /// <returns>程序集</returns>
        public static Assembly LoadAssembly(string assemblyName)
        {
            return Assembly.Load(assemblyName);
        }

        /// <summary>
        /// 从文件加载程序集
        /// </summary>
        /// <param name="assemblyFile">程序集文件路径</param>
        /// <returns>程序集</returns>
        public static Assembly LoadAssemblyFromFile(string assemblyFile)
        {
            return Assembly.LoadFrom(assemblyFile);
        }

        /// <summary>
        /// 获取类型的完整信息
        /// </summary>
        /// <param name="type">类型</param>
        /// <returns>类型信息</returns>
        public static TypeInfo GetTypeInfo(Type type)
        {
            return type.GetTypeInfo();
        }

        /// <summary>
        /// 检查类型是否为匿名类型
        /// </summary>
        /// <param name="type">类型</param>
        /// <returns>是否为匿名类型</returns>
        public static bool IsAnonymousType(Type type)
        {
            if (type == null) return false;

            return type.Name.StartsWith("<>f__AnonymousType") ||
                   type.Name.StartsWith("VB$AnonymousType");
        }

        /// <summary>
        /// 获取类型的所有嵌套类型
        /// </summary>
        /// <param name="type">类型</param>
        /// <returns>嵌套类型数组</returns>
        public static Type[] GetNestedTypes(Type type)
        {
            return type.GetNestedTypes(BindingFlags.Public | BindingFlags.NonPublic);
        }

        /// <summary>
        /// 获取类型的事件信息
        /// </summary>
        /// <param name="type">类型</param>
        /// <returns>事件信息数组</returns>
        public static EventInfo[] GetEvents(Type type)
        {
            return type.GetEvents(BindingFlags.Public | BindingFlags.Instance);
        }

        #endregion

        #region 性能优化的反射操作

        /// <summary>
        /// 创建高性能的属性获取器
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <typeparam name="TProperty">属性类型</typeparam>
        /// <param name="propertyName">属性名称</param>
        /// <returns>属性获取器函数</returns>
        public static Func<T, TProperty> CreateFastPropertyGetter<T, TProperty>(string propertyName)
        {
            var type = typeof(T);
            var property = type.GetProperty(propertyName);
            if (property == null) throw new ArgumentException($"属性 '{propertyName}' 不存在");

            var parameter = Expression.Parameter(type, "obj");
            var propertyAccess = Expression.Property(parameter, property);
            var convert = Expression.Convert(propertyAccess, typeof(TProperty));

            return Expression.Lambda<Func<T, TProperty>>(convert, parameter).Compile();
        }

        /// <summary>
        /// 创建高性能的属性设置器
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <typeparam name="TProperty">属性类型</typeparam>
        /// <param name="propertyName">属性名称</param>
        /// <returns>属性设置器函数</returns>
        public static Action<T, TProperty> CreateFastPropertySetter<T, TProperty>(string propertyName)
        {
            var type = typeof(T);
            var property = type.GetProperty(propertyName);
            if (property == null) throw new ArgumentException($"属性 '{propertyName}' 不存在");

            var target = Expression.Parameter(type, "target");
            var value = Expression.Parameter(typeof(TProperty), "value");
            var propertyAccess = Expression.Property(target, property);
            var assign = Expression.Assign(propertyAccess, value);

            return Expression.Lambda<Action<T, TProperty>>(assign, target, value).Compile();
        }

        /// <summary>
        /// 创建高性能的方法调用器
        /// </summary>
        /// <param name="type">类型</param>
        /// <param name="methodName">方法名称</param>
        /// <returns>方法调用器函数</returns>
        public static Func<object, object[], object> CreateFastMethodCaller(Type type, string methodName)
        {
            var method = type.GetMethod(methodName);
            if (method == null) throw new ArgumentException($"方法 '{methodName}' 不存在");

            return CreateMethodCaller(method);
        }

        /// <summary>
        /// 获取属性的快速访问委托
        /// </summary>
        /// <param name="property">属性信息</param>
        /// <returns>属性访问委托</returns>
        public static Delegate CreatePropertyAccessor(PropertyInfo property)
        {
            if (property == null) throw new ArgumentNullException(nameof(property));

            var getter = property.GetGetMethod(true);
            if (getter != null)
            {
                return Delegate.CreateDelegate(typeof(Func<,>).MakeGenericType(property.DeclaringType, property.PropertyType), getter);
            }

            return null;
        }

        #endregion
    }

    #region 辅助类

    /// <summary>
    /// 属性信息包装类
    /// </summary>
    public class PropertyInfoWrapper
    {
        private readonly PropertyInfo _propertyInfo;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="propertyInfo">属性信息</param>
        public PropertyInfoWrapper(PropertyInfo propertyInfo)
        {
            _propertyInfo = propertyInfo ?? throw new ArgumentNullException(nameof(propertyInfo));
        }

        /// <summary>
        /// 属性名称
        /// </summary>
        public string Name => _propertyInfo.Name;

        /// <summary>
        /// 属性类型
        /// </summary>
        public Type PropertyType => _propertyInfo.PropertyType;

        /// <summary>
        /// 是否可读
        /// </summary>
        public bool CanRead => _propertyInfo.CanRead;

        /// <summary>
        /// 是否可写
        /// </summary>
        public bool CanWrite => _propertyInfo.CanWrite;

        /// <summary>
        /// 获取属性值
        /// </summary>
        /// <param name="obj">目标对象</param>
        /// <returns>属性值</returns>
        public object GetValue(object obj)
        {
            return _propertyInfo.GetValue(obj);
        }

        /// <summary>
        /// 设置属性值
        /// </summary>
        /// <param name="obj">目标对象</param>
        /// <param name="value">属性值</param>
        public void SetValue(object obj, object value)
        {
            _propertyInfo.SetValue(obj, value);
        }

        /// <summary>
        /// 获取属性的特性
        /// </summary>
        /// <typeparam name="T">特性类型</typeparam>
        /// <param name="inherit">是否继承</param>
        /// <returns>特性实例</returns>
        public T GetCustomAttribute<T>(bool inherit = true) where T : Attribute
        {
            return _propertyInfo.GetCustomAttribute<T>(inherit);
        }

        /// <summary>
        /// 检查属性是否具有指定特性
        /// </summary>
        /// <typeparam name="T">特性类型</typeparam>
        /// <param name="inherit">是否继承</param>
        /// <returns>是否具有特性</returns>
        public bool HasCustomAttribute<T>(bool inherit = true) where T : Attribute
        {
            return _propertyInfo.IsDefined(typeof(T), inherit);
        }
    }

    /// <summary>
    /// 方法信息包装类
    /// </summary>
    public class MethodInfoWrapper
    {
        private readonly MethodInfo _methodInfo;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="methodInfo">方法信息</param>
        public MethodInfoWrapper(MethodInfo methodInfo)
        {
            _methodInfo = methodInfo ?? throw new ArgumentNullException(nameof(methodInfo));
        }

        /// <summary>
        /// 方法名称
        /// </summary>
        public string Name => _methodInfo.Name;

        /// <summary>
        /// 返回值类型
        /// </summary>
        public Type ReturnType => _methodInfo.ReturnType;

        /// <summary>
        /// 是否为静态方法
        /// </summary>
        public bool IsStatic => _methodInfo.IsStatic;

        /// <summary>
        /// 是否为公共方法
        /// </summary>
        public bool IsPublic => _methodInfo.IsPublic;

        /// <summary>
        /// 参数信息
        /// </summary>
        public ParameterInfo[] Parameters => _methodInfo.GetParameters();

        /// <summary>
        /// 调用方法
        /// </summary>
        /// <param name="obj">目标对象</param>
        /// <param name="parameters">参数数组</param>
        /// <returns>方法返回值</returns>
        public object Invoke(object obj, params object[] parameters)
        {
            return _methodInfo.Invoke(obj, parameters);
        }

        /// <summary>
        /// 获取方法的特性
        /// </summary>
        /// <typeparam name="T">特性类型</typeparam>
        /// <param name="inherit">是否继承</param>
        /// <returns>特性实例</returns>
        public T GetCustomAttribute<T>(bool inherit = true) where T : Attribute
        {
            return _methodInfo.GetCustomAttribute<T>(inherit);
        }

        /// <summary>
        /// 检查方法是否具有指定特性
        /// </summary>
        /// <typeparam name="T">特性类型</typeparam>
        /// <param name="inherit">是否继承</param>
        /// <returns>是否具有特性</returns>
        public bool HasCustomAttribute<T>(bool inherit = true) where T : Attribute
        {
            return _methodInfo.IsDefined(typeof(T), inherit);
        }
    }

    #endregion
}