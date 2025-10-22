# IntExtensions 类功能文档

## 概述

[IntExtensions](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\IntExtensions.cs#L8-L324) 是一个静态扩展类，为 `int` 类型提供了丰富的扩展方法。该类包含数值判断、转换、运算、格式化等多种功能，旨在简化整数类型的处理操作，提高代码的可读性和便利性，特别适用于需要处理整数的日常开发场景。

## 主要功能模块

### 1. 数值判断方法

提供整数数值状态检查的便捷方法。

**主要方法：**
- [IsZero()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\IntExtensions.cs#L13-L13) - 判断 int 是否为零
- [IsPositive()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\IntExtensions.cs#L19-L19) - 判断 int 是否为正数
- [IsNegative()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\IntExtensions.cs#L25-L25) - 判断 int 是否为负数
- [IsEven()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\IntExtensions.cs#L31-L31) - 判断 int 是否为偶数
- [IsOdd()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\IntExtensions.cs#L37-L37) - 判断 int 是否为奇数
- [IsBetween()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\IntExtensions.cs#L43-L43) - 判断 int 是否在指定范围内（包含边界）

### 2. 数值处理方法

提供数值约束和数学运算功能。

**主要方法：**
- [Clamp()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\IntExtensions.cs#L49-L49) - 保证 int 在指定范围内，超出则取边界值
- [Abs()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\IntExtensions.cs#L133-L133) - int 求绝对值
- [Max()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\IntExtensions.cs#L139-L139) - int 求最大值
- [Min()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\IntExtensions.cs#L145-L145) - int 求最小值
- [Add()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\IntExtensions.cs#L151-L151) - int 加法
- [Subtract()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\IntExtensions.cs#L157-L157) - int 减法
- [Multiply()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\IntExtensions.cs#L163-L163) - int 乘法
- [DivideSafe()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\IntExtensions.cs#L169-L169) - int 除法，除数为零时返回零
- [Mod()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\IntExtensions.cs#L175-L175) - int 求余
- [Pow()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\IntExtensions.cs#L181-L181) - int 求幂
- [AbsDiff()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\IntExtensions.cs#L187-L187) - int 求绝对差值

### 3. 字符串格式化方法

提供多种数值格式的字符串转换。

**主要方法：**
- [ToStringFormat()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\IntExtensions.cs#L85-L86) - int 转为字符串，支持指定格式
- [ToCurrencyString()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\IntExtensions.cs#L92-L93) - int 转为货币格式字符串
- [ToPercentString()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\IntExtensions.cs#L99-L99) - int 转为百分比字符串
- [ToChineseUpper()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\IntExtensions.cs#L105-L131) - int 转为中文大写金额
- [ToHexString()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\IntExtensions.cs#L193-L193) - int 转为十六进制字符串
- [ToBinaryString()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\IntExtensions.cs#L199-L199) - int 转为二进制字符串
- [ToOctalString()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\IntExtensions.cs#L205-L205) - int 转为八进制字符串
- [ToRomanString()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\IntExtensions.cs#L211-L237) - int 转为罗马数字字符串（1~3999）
- [ToChineseWeekday()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\IntExtensions.cs#L243-L247) - int 转为星期中文（0~6，周日~周六）
- [ToEnglishWeekday()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\IntExtensions.cs#L253-L257) - int 转为英文星期（0~6，Sunday~Saturday）

### 4. 类型转换方法

提供 int 到其他数据类型的转换。

**主要方法：**
- [ToBool()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\IntExtensions.cs#L55-L55) - int 转为 bool（非零为 true）
- [ToDouble()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\IntExtensions.cs#L61-L61) - int 转为 double
- [ToFloat()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\IntExtensions.cs#L67-L67) - int 转为 float
- [ToLong()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\IntExtensions.cs#L73-L73) - int 转为 long
- [ToDecimal()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\IntExtensions.cs#L79-L79) - int 转为 decimal
- [ToEnum<TEnum>()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\IntExtensions.cs#L263-L264) - int 转为枚举类型

### 5. 循环操作方法

提供基于整数的循环执行功能。

**主要方法：**
- [Repeat()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\IntExtensions.cs#L270-L274) - int 重复指定操作
- [Repeat()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\IntExtensions.cs#L281-L285) - int 重复指定操作（带索引）

## 使用场景

1. **数值计算** - 处理整数的数学运算和比较
2. **数据展示** - 将整数转换为各种格式的字符串显示
3. **数据验证** - 数值范围检查和状态判断
4. **金融计算** - 货币格式化显示和中文大写金额转换
5. **数据转换** - int 与其他数值类型的安全转换
6. **业务逻辑** - 数值状态判断（正负、奇偶等）
7. **循环控制** - 基于整数的循环执行操作
8. **特殊显示** - 罗马数字、进制转换、星期显示等特殊格式
9. **枚举处理** - 整数与枚举类型的相互转换

## 注意事项

1. 所有方法都是扩展方法，需要通过 `int` 实例调用
2. 除法运算提供安全版本 [DivideSafe()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\IntExtensions.cs#L169-L169)，避免除零异常
3. 货币格式化支持多区域文化设置，默认为中文环境
4. 中文大写金额转换实现了完整的中文数字转换规则
5. 罗马数字转换支持 1~3999 范围内的整数
6. 星期转换方法对超出范围的值返回原数字字符串
7. 重复执行方法对无效参数（null 或负数）进行了安全处理
8. 枚举转换方法在枚举值未定义时返回默认值
9. 进制转换方法使用系统内置的转换函数，性能较好
10. 格式化方法支持标准的 .NET 格式字符串