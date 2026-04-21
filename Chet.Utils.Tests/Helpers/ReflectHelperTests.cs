using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using Xunit;

namespace Chet.Utils.Helpers.Tests;

public class ReflectHelperTests
{
    #region 测试用类定义

    public class TestClass
    {
        public string PublicProperty { get; set; } = "Default";
        public int ReadOnlyProperty { get; } = 42;
        private string PrivateField = "PrivateValue";

        public void PublicMethod() { }
        private void PrivateMethod() { }

        public int Add(int a, int b) => a + b;
        public async Task<int> AddAsync(int a, int b) => await Task.FromResult(a + b);

        public static string StaticMethod() => "Static";
    }

    [Display(Name = "DisplayName", Description = "DisplayDescription")]
    public class TestClassWithAttributes
    {
        [Display(Name = "PropertyName", Description = "PropertyDescription")]
        [DisplayName("DisplayNameValue")]
        public string PropertyWithAttributes { get; set; } = string.Empty;
    }

    public interface ITestInterface { }
    public class TestClassWithInterface : ITestInterface { }
    public class DerivedTestClass : TestClass { }

    #endregion

    #region 类型信息获取测试

    [Fact]
    public void GetPublicProperties_ReturnsPublicProperties()
    {
        var properties = ReflectHelper.GetPublicProperties(typeof(TestClass));

        Assert.NotNull(properties);
        Assert.True(properties.Length > 0);
        Assert.Contains(properties, p => p.Name == nameof(TestClass.PublicProperty));
    }

    [Fact]
    public void GetAllProperties_ReturnsAllProperties()
    {
        var properties = ReflectHelper.GetAllProperties(typeof(TestClass));

        Assert.NotNull(properties);
        Assert.True(properties.Length > 0);
    }

    [Fact]
    public void GetProperty_ExistingProperty_ReturnsPropertyInfo()
    {
        var property = ReflectHelper.GetProperty(typeof(TestClass), nameof(TestClass.PublicProperty));

        Assert.NotNull(property);
        Assert.Equal(nameof(TestClass.PublicProperty), property.Name);
    }

    [Fact]
    public void GetProperty_NonExistingProperty_ReturnsNull()
    {
        var property = ReflectHelper.GetProperty(typeof(TestClass), "NonExistingProperty");

        Assert.Null(property);
    }

    [Fact]
    public void GetPublicMethods_ReturnsPublicMethods()
    {
        var methods = ReflectHelper.GetPublicMethods(typeof(TestClass));

        Assert.NotNull(methods);
        Assert.Contains(methods, m => m.Name == nameof(TestClass.PublicMethod));
    }

    [Fact]
    public void GetAllMethods_ReturnsAllMethods()
    {
        var methods = ReflectHelper.GetAllMethods(typeof(TestClass));

        Assert.NotNull(methods);
        Assert.True(methods.Length > 0);
    }

    [Fact]
    public void GetMethod_ExistingMethod_ReturnsMethodInfo()
    {
        var method = ReflectHelper.GetMethod(typeof(TestClass), nameof(TestClass.Add));

        Assert.NotNull(method);
        Assert.Equal(nameof(TestClass.Add), method.Name);
    }

    [Fact]
    public void GetMethod_WithParameters_ReturnsCorrectMethod()
    {
        var method = ReflectHelper.GetMethod(typeof(TestClass), nameof(TestClass.Add), typeof(int), typeof(int));

        Assert.NotNull(method);
        Assert.Equal(2, method.GetParameters().Length);
    }

    [Fact]
    public void GetPublicFields_ReturnsPublicFields()
    {
        var fields = ReflectHelper.GetPublicFields(typeof(TestClass));

        Assert.NotNull(fields);
    }

    [Fact]
    public void GetAllFields_ReturnsAllFields()
    {
        var fields = ReflectHelper.GetAllFields(typeof(TestClass));

        Assert.NotNull(fields);
        Assert.True(fields.Length > 0);
    }

    [Fact]
    public void GetCustomAttribute_TypeWithAttribute_ReturnsAttribute()
    {
        var attr = ReflectHelper.GetCustomAttribute<DisplayAttribute>(typeof(TestClassWithAttributes));

        Assert.NotNull(attr);
        Assert.Equal("DisplayName", attr.Name);
    }

    [Fact]
    public void GetCustomAttributes_ReturnsAttributes()
    {
        var attrs = ReflectHelper.GetCustomAttributes(typeof(TestClassWithAttributes));

        Assert.NotNull(attrs);
        Assert.True(attrs.Length > 0);
    }

    [Fact]
    public void HasCustomAttribute_TypeWithAttribute_ReturnsTrue()
    {
        var hasAttr = ReflectHelper.HasCustomAttribute<DisplayAttribute>(typeof(TestClassWithAttributes));

        Assert.True(hasAttr);
    }

    [Fact]
    public void HasCustomAttribute_TypeWithoutAttribute_ReturnsFalse()
    {
        var hasAttr = ReflectHelper.HasCustomAttribute<DisplayAttribute>(typeof(TestClass));

        Assert.False(hasAttr);
    }

    [Fact]
    public void GetInterfaces_ReturnsInterfaces()
    {
        var interfaces = ReflectHelper.GetInterfaces(typeof(TestClassWithInterface));

        Assert.NotNull(interfaces);
        Assert.Contains(interfaces, i => i == typeof(ITestInterface));
    }

    [Fact]
    public void ImplementsInterface_TypeImplementsInterface_ReturnsTrue()
    {
        var implements = ReflectHelper.ImplementsInterface(typeof(TestClassWithInterface), typeof(ITestInterface));

        Assert.True(implements);
    }

    [Fact]
    public void ImplementsInterface_TypeDoesNotImplementInterface_ReturnsFalse()
    {
        var implements = ReflectHelper.ImplementsInterface(typeof(TestClass), typeof(ITestInterface));

        Assert.False(implements);
    }

    [Fact]
    public void ImplementsInterface_GenericVersion_WorksCorrectly()
    {
        var implements = ReflectHelper.ImplementsInterface<ITestInterface>(typeof(TestClassWithInterface));

        Assert.True(implements);
    }

    [Fact]
    public void GetBaseType_ReturnsBaseType()
    {
        var baseType = ReflectHelper.GetBaseType(typeof(DerivedTestClass));

        Assert.NotNull(baseType);
        Assert.Equal(typeof(TestClass), baseType);
    }

    [Fact]
    public void IsSubclassOf_DerivedClass_ReturnsTrue()
    {
        var isSubclass = ReflectHelper.IsSubclassOf(typeof(DerivedTestClass), typeof(TestClass));

        Assert.True(isSubclass);
    }

    [Fact]
    public void IsValueType_ValueType_ReturnsTrue()
    {
        var isValueType = ReflectHelper.IsValueType(typeof(int));

        Assert.True(isValueType);
    }

    [Fact]
    public void IsValueType_ReferenceType_ReturnsFalse()
    {
        var isValueType = ReflectHelper.IsValueType(typeof(TestClass));

        Assert.False(isValueType);
    }

    [Fact]
    public void IsReferenceType_ReferenceType_ReturnsTrue()
    {
        var isRefType = ReflectHelper.IsReferenceType(typeof(TestClass));

        Assert.True(isRefType);
    }

    [Fact]
    public void IsAbstract_AbstractType_ReturnsTrue()
    {
        var isAbstract = ReflectHelper.IsAbstract(typeof(AbstractClass));

        Assert.True(isAbstract);
    }

    [Fact]
    public void IsInterface_InterfaceType_ReturnsTrue()
    {
        var isInterface = ReflectHelper.IsInterface(typeof(ITestInterface));

        Assert.True(isInterface);
    }

    [Fact]
    public void IsGenericType_GenericType_ReturnsTrue()
    {
        var isGeneric = ReflectHelper.IsGenericType(typeof(List<int>));

        Assert.True(isGeneric);
    }

    [Fact]
    public void GetGenericArguments_ReturnsTypeArguments()
    {
        var args = ReflectHelper.GetGenericArguments(typeof(List<int>));

        Assert.NotNull(args);
        Assert.Single(args);
        Assert.Equal(typeof(int), args[0]);
    }

    public abstract class AbstractClass { }

    #endregion

    #region 属性操作测试

    [Fact]
    public void GetPropertyValue_ReturnsCorrectValue()
    {
        var obj = new TestClass { PublicProperty = "TestValue" };

        var value = ReflectHelper.GetPropertyValue(obj, nameof(TestClass.PublicProperty));

        Assert.Equal("TestValue", value);
    }

    [Fact]
    public void GetPropertyValue_Generic_ReturnsCorrectValue()
    {
        var obj = new TestClass { PublicProperty = "TestValue" };

        var value = ReflectHelper.GetPropertyValue<string>(obj, nameof(TestClass.PublicProperty));

        Assert.Equal("TestValue", value);
    }

    [Fact]
    public void SetPropertyValue_SetsValueCorrectly()
    {
        var obj = new TestClass();

        ReflectHelper.SetPropertyValue(obj, nameof(TestClass.PublicProperty), "NewValue");

        Assert.Equal("NewValue", obj.PublicProperty);
    }

    [Fact]
    public void TrySetPropertyValue_ValidProperty_ReturnsTrue()
    {
        var obj = new TestClass();

        var result = ReflectHelper.TrySetPropertyValue(obj, nameof(TestClass.PublicProperty), "NewValue");

        Assert.True(result);
        Assert.Equal("NewValue", obj.PublicProperty);
    }

    [Fact]
    public void TrySetPropertyValue_InvalidProperty_ReturnsFalse()
    {
        var obj = new TestClass();

        var result = ReflectHelper.TrySetPropertyValue(obj, "NonExistingProperty", "NewValue");

        Assert.False(result);
    }

    [Fact]
    public void GetPropertyDisplayName_ReturnsDisplayName()
    {
        var property = typeof(TestClassWithAttributes).GetProperty(nameof(TestClassWithAttributes.PropertyWithAttributes));

        var displayName = ReflectHelper.GetPropertyDisplayName(property!);

        Assert.Equal("PropertyName", displayName);
    }

    [Fact]
    public void GetPropertyDescription_ReturnsDescription()
    {
        var property = typeof(TestClassWithAttributes).GetProperty(nameof(TestClassWithAttributes.PropertyWithAttributes));

        var description = ReflectHelper.GetPropertyDescription(property!);

        Assert.Equal("PropertyDescription", description);
    }

    [Fact]
    public void GetPropertyValues_ReturnsAllPropertyValues()
    {
        var obj = new TestClass { PublicProperty = "TestValue" };

        var values = ReflectHelper.GetPropertyValues(obj);

        Assert.NotNull(values);
        Assert.True(values.Count > 0);
        Assert.Equal("TestValue", values[nameof(TestClass.PublicProperty)]);
    }

    [Fact]
    public void SetPropertyValues_SetsMultipleProperties()
    {
        var obj = new TestClass();
        var values = new Dictionary<string, object?>
        {
            [nameof(TestClass.PublicProperty)] = "NewValue"
        };

        ReflectHelper.SetPropertyValues(obj, values);

        Assert.Equal("NewValue", obj.PublicProperty);
    }

    [Fact]
    public void CanRead_ReadableProperty_ReturnsTrue()
    {
        var property = typeof(TestClass).GetProperty(nameof(TestClass.PublicProperty));

        var canRead = ReflectHelper.CanRead(property!);

        Assert.True(canRead);
    }

    [Fact]
    public void CanWrite_WritableProperty_ReturnsTrue()
    {
        var property = typeof(TestClass).GetProperty(nameof(TestClass.PublicProperty));

        var canWrite = ReflectHelper.CanWrite(property!);

        Assert.True(canWrite);
    }

    [Fact]
    public void CanWrite_ReadOnlyProperty_ReturnsFalse()
    {
        var property = typeof(TestClass).GetProperty(nameof(TestClass.ReadOnlyProperty));

        var canWrite = ReflectHelper.CanWrite(property!);

        Assert.False(canWrite);
    }

    [Fact]
    public void GetPropertyType_ReturnsCorrectType()
    {
        var property = typeof(TestClass).GetProperty(nameof(TestClass.PublicProperty));

        var type = ReflectHelper.GetPropertyType(property!);

        Assert.Equal(typeof(string), type);
    }

    #endregion

    #region 方法调用测试

    [Fact]
    public void InvokeMethod_InvokesMethodCorrectly()
    {
        var obj = new TestClass();

        var result = ReflectHelper.InvokeMethod(obj, nameof(TestClass.Add), 5, 3);

        Assert.Equal(8, result);
    }

    [Fact]
    public void InvokeMethod_Generic_ReturnsCorrectType()
    {
        var obj = new TestClass();

        var result = ReflectHelper.InvokeMethod<int>(obj, nameof(TestClass.Add), 5, 3);

        Assert.Equal(8, result);
    }

    [Fact]
    public void InvokeStaticMethod_InvokesStaticMethod()
    {
        var result = ReflectHelper.InvokeStaticMethod(typeof(TestClass), nameof(TestClass.StaticMethod));

        Assert.Equal("Static", result);
    }

    [Fact]
    public void InvokeStaticMethod_Generic_ReturnsCorrectType()
    {
        var result = ReflectHelper.InvokeStaticMethod<string>(typeof(TestClass), nameof(TestClass.StaticMethod));

        Assert.Equal("Static", result);
    }

    [Fact]
    public void IsAsyncMethod_AsyncMethod_ReturnsTrue()
    {
        var method = typeof(TestClass).GetMethod(nameof(TestClass.AddAsync));

        var isAsync = ReflectHelper.IsAsyncMethod(method!);

        Assert.True(isAsync);
    }

    [Fact]
    public void IsAsyncMethod_SyncMethod_ReturnsFalse()
    {
        var method = typeof(TestClass).GetMethod(nameof(TestClass.Add));

        var isAsync = ReflectHelper.IsAsyncMethod(method!);

        Assert.False(isAsync);
    }

    [Fact]
    public void IsStaticMethod_StaticMethod_ReturnsTrue()
    {
        var method = typeof(TestClass).GetMethod(nameof(TestClass.StaticMethod));

        var isStatic = ReflectHelper.IsStaticMethod(method!);

        Assert.True(isStatic);
    }

    [Fact]
    public void IsPublicMethod_PublicMethod_ReturnsTrue()
    {
        var method = typeof(TestClass).GetMethod(nameof(TestClass.Add));

        var isPublic = ReflectHelper.IsPublicMethod(method!);

        Assert.True(isPublic);
    }

    [Fact]
    public void GetMethodParameters_ReturnsParameters()
    {
        var method = typeof(TestClass).GetMethod(nameof(TestClass.Add));

        var parameters = ReflectHelper.GetMethodParameters(method!);

        Assert.Equal(2, parameters.Length);
    }

    [Fact]
    public void GetMethodReturnType_ReturnsCorrectType()
    {
        var method = typeof(TestClass).GetMethod(nameof(TestClass.Add));

        var returnType = ReflectHelper.GetMethodReturnType(method!);

        Assert.Equal(typeof(int), returnType);
    }

    #endregion

    #region 字段操作测试

    [Fact]
    public void GetFieldValue_ReturnsCorrectValue()
    {
        var obj = new TestClass();

        var value = ReflectHelper.GetFieldValue(obj, "PrivateField");

        Assert.Equal("PrivateValue", value);
    }

    [Fact]
    public void GetFieldValue_Generic_ReturnsCorrectValue()
    {
        var obj = new TestClass();

        var value = ReflectHelper.GetFieldValue<string>(obj, "PrivateField");

        Assert.Equal("PrivateValue", value);
    }

    [Fact]
    public void SetFieldValue_SetsValueCorrectly()
    {
        var obj = new TestClass();

        ReflectHelper.SetFieldValue(obj, "PrivateField", "NewPrivateValue");

        var value = ReflectHelper.GetFieldValue(obj, "PrivateField");
        Assert.Equal("NewPrivateValue", value);
    }

    #endregion

    #region 动态创建测试

    [Fact]
    public void CreateInstance_CreatesInstance()
    {
        var instance = ReflectHelper.CreateInstance<TestClass>();

        Assert.NotNull(instance);
        Assert.IsType<TestClass>(instance);
    }

    [Fact]
    public void CreateInstance_ByType_CreatesInstance()
    {
        var instance = ReflectHelper.CreateInstance(typeof(TestClass));

        Assert.NotNull(instance);
        Assert.IsType<TestClass>(instance);
    }

    [Fact]
    public void GetDefaultValue_ValueType_ReturnsDefault()
    {
        var defaultValue = ReflectHelper.GetDefaultValue(typeof(int));

        Assert.Equal(0, defaultValue);
    }

    [Fact]
    public void GetDefaultValue_ReferenceType_ReturnsNull()
    {
        var defaultValue = ReflectHelper.GetDefaultValue(typeof(TestClass));

        Assert.Null(defaultValue);
    }

    #endregion

    #region 类型检查测试

    [Fact]
    public void IsNumericType_NumericType_ReturnsTrue()
    {
        Assert.True(ReflectHelper.IsNumericType(typeof(int)));
        Assert.True(ReflectHelper.IsNumericType(typeof(double)));
        Assert.True(ReflectHelper.IsNumericType(typeof(decimal)));
    }

    [Fact]
    public void IsNumericType_NonNumericType_ReturnsFalse()
    {
        Assert.False(ReflectHelper.IsNumericType(typeof(string)));
        Assert.False(ReflectHelper.IsNumericType(typeof(TestClass)));
    }

    [Fact]
    public void IsNullableType_NullableType_ReturnsTrue()
    {
        Assert.True(ReflectHelper.IsNullableType(typeof(int?)));
        Assert.True(ReflectHelper.IsNullableType(typeof(DateTime?)));
    }

    [Fact]
    public void IsNullableType_NonNullableType_ReturnsFalse()
    {
        Assert.False(ReflectHelper.IsNullableType(typeof(int)));
        Assert.False(ReflectHelper.IsNullableType(typeof(string)));
    }

    [Fact]
    public void IsEnumType_EnumType_ReturnsTrue()
    {
        Assert.True(ReflectHelper.IsEnumType(typeof(DayOfWeek)));
    }

    [Fact]
    public void IsEnumType_NonEnumType_ReturnsFalse()
    {
        Assert.False(ReflectHelper.IsEnumType(typeof(int)));
    }

    [Fact]
    public void IsCollectionType_CollectionType_ReturnsTrue()
    {
        Assert.True(ReflectHelper.IsCollectionType(typeof(List<int>)));
        Assert.True(ReflectHelper.IsCollectionType(typeof(int[])));
    }

    [Fact]
    public void IsCollectionType_NonCollectionType_ReturnsFalse()
    {
        Assert.False(ReflectHelper.IsCollectionType(typeof(int)));
        Assert.False(ReflectHelper.IsCollectionType(typeof(string)));
    }

    #endregion

    #region 异常测试

    [Fact]
    public void GetPublicProperties_NullType_ThrowsArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>(() => ReflectHelper.GetPublicProperties(null!));
    }

    [Fact]
    public void GetPropertyValue_NullObject_ThrowsArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>(() => ReflectHelper.GetPropertyValue(null!, "Property"));
    }

    [Fact]
    public void GetPropertyValue_EmptyPropertyName_ThrowsArgumentException()
    {
        var obj = new TestClass();
        Assert.Throws<ArgumentException>(() => ReflectHelper.GetPropertyValue(obj, ""));
    }

    [Fact]
    public void SetPropertyValue_NullObject_ThrowsArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>(() => ReflectHelper.SetPropertyValue(null!, "Property", "Value"));
    }

    [Fact]
    public void InvokeMethod_NullObject_ThrowsArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>(() => ReflectHelper.InvokeMethod(null!, "Method"));
    }

    #endregion
}
