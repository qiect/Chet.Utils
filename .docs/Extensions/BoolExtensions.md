# BoolExtensions 类功能文档

## 概述

[BoolExtensions](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\BoolExtensions.cs#L7-L179) 是一个静态扩展类，为 `bool` 类型提供了丰富的扩展方法。该类包含判断、转换、格式化、逻辑运算等多种功能，旨在简化布尔值的处理和转换操作，提高代码的可读性和便利性。

## 主要功能模块

### 1. 判断方法

提供布尔值状态判断的便捷方法。

**主要方法：**
- [IsTrue()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\BoolExtensions.cs#L12-L12) - 判断布尔值是否为 true
- [IsFalse()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\BoolExtensions.cs#L18-L18) - 判断布尔值是否为 false
- [Not()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\BoolExtensions.cs#L24-L24) - 对布尔值进行取反操作

### 2. 数值转换方法

提供布尔值到各种数值类型的转换。

**主要方法：**
- [ToInt()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\BoolExtensions.cs#L30-L30) - 转换为 int（true=1, false=0）
- [ToByte()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\BoolExtensions.cs#L126-L126) - 转换为 byte（true=1, false=0）
- [ToShort()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\BoolExtensions.cs#L132-L132) - 转换为 short（true=1, false=0）
- [ToLong()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\BoolExtensions.cs#L138-L138) - 转换为 long（true=1, false=0）
- [ToFloat()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\BoolExtensions.cs#L144-L144) - 转换为 float（true=1.0, false=0.0）
- [ToDouble()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\BoolExtensions.cs#L150-L150) - 转换为 double（true=1.0, false=0.0）
- [ToDecimal()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\BoolExtensions.cs#L156-L156) - 转换为 decimal（true=1.0, false=0.0）

### 3. 字符串转换方法

提供布尔值到各种字符串格式的转换。

**主要方法：**
- [ToStringValue()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\BoolExtensions.cs#L36-L36) - 转换为标准字符串（"True"/"False"）
- [ToChineseString()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\BoolExtensions.cs#L42-L42) - 转换为中文字符串（"是"/"否"）
- [ToCustomString()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\BoolExtensions.cs#L48-L49) - 转换为自定义字符串
- [ToYesNo()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\BoolExtensions.cs#L55-L55) - 转换为 Yes/No 字符串
- [ToOnOff()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\BoolExtensions.cs#L61-L61) - 转换为 On/Off 字符串
- [ToOneZero()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\BoolExtensions.cs#L67-L67) - 转换为 1/0 字符串
- [ToYN()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\BoolExtensions.cs#L73-L73) - 转换为 Y/N 字符串
- [ToReverseString()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\BoolExtensions.cs#L162-L162) - 转换为反向字符串（"False"/"True"）
- [ToReverseChineseString()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\BoolExtensions.cs#L168-L168) - 转换为反向中文字符串（"否"/"是"）

### 4. 逻辑运算方法

提供布尔值的逻辑运算操作。

**主要方法：**
- [And()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\BoolExtensions.cs#L79-L79) - 与运算（AND）
- [Or()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\BoolExtensions.cs#L85-L85) - 或运算（OR）
- [Xor()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\BoolExtensions.cs#L91-L91) - 异或运算（XOR）
- [Xnor()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\BoolExtensions.cs#L97-L97) - 同或运算（等价于相等比较）

### 5. 类型映射方法

提供布尔值到其他类型的映射转换。

**主要方法：**
- [ToEnum<TEnum>()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\BoolExtensions.cs#L103-L104) - 转换为枚举值
- [ToNullable()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\BoolExtensions.cs#L110-L111) - 转换为可空布尔值
- [ToValue<T>()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\BoolExtensions.cs#L117-L118) - 转换为指定类型的值

## 使用场景

1. **界面显示** - 将布尔值转换为用户友好的字符串格式（如中文"是/否"）
2. **数据转换** - 在数据处理中将布尔值转换为数字或其他类型
3. **配置管理** - 处理开关配置项的多种表示形式
4. **逻辑运算** - 使用链式调用进行复杂的布尔逻辑运算
5. **API交互** - 将布尔值转换为特定格式的字符串以适应接口要求
6. **国际化** - 支持多语言环境下的布尔值显示
7. **数据映射** - 将布尔值映射到枚举或其他业务对象

## 注意事项

1. 所有方法都是扩展方法，需要通过 `bool` 实例调用
2. 数值转换方法遵循标准约定：true 映射为 1（或 1.0），false 映射为 0（或 0.0）
3. 字符串转换方法提供了多种常见格式，也可通过自定义方法指定特定格式
4. 逻辑运算方法提供了函数式编程风格的调用方式
5. [ToNullable()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\BoolExtensions.cs#L110-L111) 方法默认返回原值，只有当 `nullable` 参数为 true 时才返回 null
6. [ToValue<T>()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\BoolExtensions.cs#L117-L118) 方法支持将布尔值映射到任意类型
7. 反向转换方法提供与常规转换相反的结果