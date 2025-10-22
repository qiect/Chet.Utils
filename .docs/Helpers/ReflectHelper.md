# ReflectHelper 类功能文档

## 概述

[ReflectHelper](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\ReflectHelper.cs#L12-L1273) 是一个静态工具类，提供了丰富的反射操作功能。该类封装了 .NET 的反射 API，提供了类型信息获取、属性操作、方法调用、字段操作、动态创建实例、表达式树操作、类型检查与转换、高级反射操作等功能，旨在简化复杂的反射操作并提高开发效率。

## 主要功能模块

### 1. 类型信息获取

提供获取类型元数据信息的功能，包括属性、方法、字段、特性、接口等。

**主要方法：**
- [GetPublicProperties()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\ReflectHelper.cs#L24-L27) - 获取类型的所有公共属性
- [GetAllProperties()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\ReflectHelper.cs#L34-L37) - 获取类型的所有属性（包括私有）
- [GetPublicMethods()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\ReflectHelper.cs#L44-L47) - 获取类型的所有公共方法
- [GetAllMethods()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\ReflectHelper.cs#L54-L57) - 获取类型的所有方法（包括私有）
- [GetPublicFields()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\ReflectHelper.cs#L64-L67) - 获取类型的所有公共字段
- [GetAllFields()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\ReflectHelper.cs#L74-L77) - 获取类型的所有字段（包括私有）
- [GetCustomAttribute<T>()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\ReflectHelper.cs#L85-L88) - 获取类型的特性
- [GetCustomAttributes()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\ReflectHelper.cs#L96-L99) - 获取类型的所有特性
- [HasCustomAttribute<T>()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\ReflectHelper.cs#L108-L111) - 检查类型是否具有指定特性
- [GetInterfaces()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\ReflectHelper.cs#L118-L121) - 获取类型实现的所有接口
- [ImplementsInterface()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\ReflectHelper.cs#L128-L131) - 检查类型是否实现指定接口
- [GetBaseType()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\ReflectHelper.cs#L137-L140) - 获取类型的基类型
- [IsSubclassOf()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\ReflectHelper.cs#L147-L150) - 检查类型是否继承自指定类型

### 2. 属性操作

提供对象属性的获取、设置和元数据操作功能。

**主要方法：**
- [GetPropertyValue()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\ReflectHelper.cs#L161-L170) - 获取对象属性值
- [GetPropertyValue<T>()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\ReflectHelper.cs#L179-L182) - 获取对象属性值（泛型版本）
- [SetPropertyValue()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\ReflectHelper.cs#L191-L200) - 设置对象属性值
- [GetCustomAttribute<T>()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\ReflectHelper.cs#L209-L212) - 获取属性的特性
- [HasCustomAttribute<T>()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\ReflectHelper.cs#L221-L224) - 检查属性是否具有指定特性
- [GetPropertyDisplayName()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\ReflectHelper.cs#L231-L243) - 获取属性的显示名称
- [GetPropertyDescription()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\ReflectHelper.cs#L250-L262) - 获取属性的描述信息
- [GetPropertyValues()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\ReflectHelper.cs#L270-L289) - 获取对象所有属性的名称和值
- [SetPropertyValues()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\ReflectHelper.cs#L296-L320) - 批量设置对象属性值

### 3. 方法调用

提供对象方法的调用和元数据操作功能。

**主要方法：**
- [InvokeMethod()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\ReflectHelper.cs#L331-L341) - 调用对象方法
- [InvokeMethod<T>()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\ReflectHelper.cs#L351-L354) - 调用对象方法（泛型版本）
- [InvokeStaticMethod()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\ReflectHelper.cs#L363-L373) - 调用静态方法
- [InvokeStaticMethod<T>()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\ReflectHelper.cs#L383-L386) - 调用静态方法（泛型版本）
- [GetCustomAttribute<T>()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\ReflectHelper.cs#L395-L398) - 获取方法的特性
- [HasCustomAttribute<T>()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\ReflectHelper.cs#L407-L410) - 检查方法是否具有指定特性
- [GetMethodParameters()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\ReflectHelper.cs#L417-L420) - 获取方法参数信息
- [GetMethodReturnType()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\ReflectHelper.cs#L427-L430) - 获取方法返回值类型
- [IsAsyncMethod()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\ReflectHelper.cs#L437-L441) - 检查方法是否为异步方法

### 4. 字段操作

提供对象字段的获取、设置和元数据操作功能。

**主要方法：**
- [GetFieldValue()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\ReflectHelper.cs#L451-L461) - 获取对象字段值
- [GetFieldValue<T>()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\ReflectHelper.cs#L471-L474) - 获取对象字段值（泛型版本）
- [SetFieldValue()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\ReflectHelper.cs#L482-L492) - 设置对象字段值
- [GetCustomAttribute<T>()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\ReflectHelper.cs#L501-L504) - 获取字段的特性
- [HasCustomAttribute<T>()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\ReflectHelper.cs#L513-L516) - 检查字段是否具有指定特性

### 5. 动态创建与实例化

提供对象实例的动态创建和类型信息获取功能。

**主要方法：**
- [CreateInstance<T>()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\ReflectHelper.cs#L526-L529) - 创建类型实例
- [CreateInstance()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\ReflectHelper.cs#L538-L543) - 创建类型实例（通过反射）
- [CreateInstance<T>()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\ReflectHelper.cs#L552-L555) - 创建类型实例（泛型版本）
- [CreateInstance()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\ReflectHelper.cs#L565-L581) - 通过无参构造函数创建类型实例
- [GetDefaultValue()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\ReflectHelper.cs#L588-L596) - 获取类型的默认值
- [IsInstantiable()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\ReflectHelper.cs#L603-L606) - 检查类型是否可以被实例化
- [GetConstructors()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\ReflectHelper.cs#L613-L616) - 获取类型的所有构造函数
- [GetAllConstructors()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\ReflectHelper.cs#L623-L626) - 获取类型的所有构造函数（包括私有）

### 6. 表达式树操作

提供基于表达式树的高性能属性和方法操作功能。

**主要方法：**
- [GetPropertyName<T, TProperty>()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\ReflectHelper.cs#L637-L652) - 获取属性名称（通过表达式树）
- [GetPropertyValue<T, TProperty>()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\ReflectHelper.cs#L662-L668) - 获取属性值（通过表达式树）
- [SetPropertyValue<T, TProperty>()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\ReflectHelper.cs#L677-L692) - 设置属性值（通过表达式树）
- [CreatePropertyGetter<T, TProperty>()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\ReflectHelper.cs#L701-L704) - 创建属性获取器（通过表达式树）
- [CreatePropertySetter<T, TProperty>()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\ReflectHelper.cs#L713-L731) - 创建属性设置器（通过表达式树）
- [CreateMethodCaller()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\ReflectHelper.cs#L740-L787) - 创建方法调用器（通过表达式树）

### 7. 类型转换与检查

提供类型检查和转换相关的功能。

**主要方法：**
- [IsNumericType()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\ReflectHelper.cs#L796-L814) - 检查类型是否为数值类型
- [IsBooleanType()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\ReflectHelper.cs#L821-L824) - 检查类型是否为布尔类型
- [IsStringType()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\ReflectHelper.cs#L831-L834) - 检查类型是否为字符串类型
- [IsCollectionType()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\ReflectHelper.cs#L841-L846) - 检查类型是否为集合类型
- [IsNullableType()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\ReflectHelper.cs#L853-L856) - 检查类型是否为可空类型
- [GetNullableUnderlyingType()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\ReflectHelper.cs#L863-L866) - 获取可空类型的基础类型
- [IsEnumType()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\ReflectHelper.cs#L873-L876) - 检查类型是否为枚举类型
- [GetEnumValues()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\ReflectHelper.cs#L884-L887) - 获取枚举的所有值
- [GetEnumNames()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\ReflectHelper.cs#L895-L898) - 获取枚举的所有名称
- [IsCompatibleType()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\ReflectHelper.cs#L906-L912) - 检查两个类型是否兼容

### 8. 高级反射操作

提供程序集级别的反射操作和高级类型信息获取功能。

**主要方法：**
- [GetExtensionMethods()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\ReflectHelper.cs#L922-L933) - 获取类型的所有扩展方法
- [GetTypes()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\ReflectHelper.cs#L941-L944) - 获取程序集中的所有类型
- [GetTypes()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\ReflectHelper.cs#L953-L956) - 根据条件筛选程序集中的类型
- [GetAssemblies()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\ReflectHelper.cs#L963-L966) - 获取当前应用程序域中的所有程序集
- [LoadAssembly()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\ReflectHelper.cs#L973-L976) - 根据名称加载程序集
- [LoadAssemblyFromFile()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\ReflectHelper.cs#L983-L986) - 从文件加载程序集
- [GetTypeInfo()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\ReflectHelper.cs#L993-L996) - 获取类型的完整信息
- [IsAnonymousType()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\ReflectHelper.cs#L1003-L1008) - 检查类型是否为匿名类型
- [GetNestedTypes()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\ReflectHelper.cs#L1015-L1018) - 获取类型的所有嵌套类型
- [GetEvents()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\ReflectHelper.cs#L1025-L1028) - 获取类型的事件信息

### 9. 性能优化的反射操作

提供高性能的反射操作实现，通过表达式树编译提升性能。

**主要方法：**
- [CreateFastPropertyGetter<T, TProperty>()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\ReflectHelper.cs#L1039-L1049) - 创建高性能的属性获取器
- [CreateFastPropertySetter<T, TProperty>()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\ReflectHelper.cs#L1059-L1071) - 创建高性能的属性设置器
- [CreateFastMethodCaller()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\ReflectHelper.cs#L1080-L1086) - 创建高性能的方法调用器
- [CreatePropertyAccessor()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\ReflectHelper.cs#L1094-L1105) - 获取属性的快速访问委托

## 辅助类

### 包装类
- [PropertyInfoWrapper](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\ReflectHelper.cs#L1115-L1197) - 属性信息包装类
- [MethodInfoWrapper](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\ReflectHelper.cs#L1202-L1272) - 方法信息包装类

## 使用场景

1. **ORM框架开发** - 动态映射数据库记录到对象属性
2. **序列化/反序列化** - 通过反射访问对象属性进行数据转换
3. **依赖注入容器** - 动态创建和配置对象实例
4. **AOP编程** - 通过反射拦截方法调用
5. **插件系统** - 动态加载和调用外部程序集中的类型
6. **数据验证** - 通过特性检查对象属性的验证规则
7. **UI绑定** - 获取属性的显示名称和描述信息用于界面展示
8. **脚本引擎** - 动态调用对象的方法和访问属性

## 注意事项

1. 部分方法在找不到成员时会抛出异常
2. 性能敏感的场景建议使用表达式树操作方法
3. 私有成员操作需要相应的 BindingFlags
4. 类型转换可能抛出 InvalidCastException
5. 泛型方法需要正确指定类型参数
6. 大量反射操作时建议缓存 MethodInfo、PropertyInfo 等元数据
7. 跨程序集操作时需要注意程序集加载和类型解析