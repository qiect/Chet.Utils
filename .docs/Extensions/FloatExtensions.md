# FloatExtensions 类功能文档

## 概述

[FloatExtensions](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\FloatExtensions.cs#L8-L269) 是一个静态扩展类，为 `float` 类型提供了丰富的扩展方法。该类包含数值判断、转换、运算、格式化等多种功能，旨在简化 float 类型的数学运算和格式化操作，特别适用于需要处理浮点数的日常开发场景。

## 主要功能模块

### 1. 数值判断方法

提供 float 数值状态检查的便捷方法。

**主要方法：**
- [IsZero()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\FloatExtensions.cs#L13-L13) - 判断 float 是否为零
- [IsPositive()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\FloatExtensions.cs#L19-L19) - 判断 float 是否为正数
- [IsNegative()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\FloatExtensions.cs#L25-L25) - 判断 float 是否为负数
- [IsInteger()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\FloatExtensions.cs#L31-L31) - 判断 float 是否为整数
- [IsEven()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\FloatExtensions.cs#L37-L37) - 判断 float 是否为偶数（仅整数时有效）
- [IsOdd()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\FloatExtensions.cs#L43-L43) - 判断 float 是否为奇数（仅整数时有效）
- [IsNaN()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\FloatExtensions.cs#L49-L49) - 判断 float 是否为 NaN
- [IsPositiveInfinity()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\FloatExtensions.cs#L55-L55) - 判断 float 是否为正无穷
- [IsNegativeInfinity()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\FloatExtensions.cs#L61-L61) - 判断 float 是否为负无穷

### 2. 数值处理方法

提供数值修约和数学运算功能。

**主要方法：**
- [Round()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\FloatExtensions.cs#L67-L68) - float 四舍五入到指定小数位
- [Truncate()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\FloatExtensions.cs#L74-L79) - float 截断到指定小数位（向零取整）
- [Abs()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\FloatExtensions.cs#L85-L85) - float 取绝对值
- [Max()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\FloatExtensions.cs#L118-L118) - float 取最大值
- [Min()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\FloatExtensions.cs#L124-L124) - float 取最小值
- [Clamp()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\FloatExtensions.cs#L138-L139) - float 保证在指定范围内，超出则取边界值
- [Sqrt()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\FloatExtensions.cs#L223-L223) - float 求平方根
- [AbsDiff()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\FloatExtensions.cs#L229-L229) - float 求绝对差值

### 3. 字符串格式化方法

提供多种数值格式的字符串转换。

**主要方法：**
- [ToFixedString()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\FloatExtensions.cs#L91-L92) - float 转为字符串，保留指定小数位
- [ToCurrencyString()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\FloatExtensions.cs#L98-L99) - float 转为货币格式字符串
- [ToPercentString()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\FloatExtensions.cs#L105-L106) - float 转为百分比字符串
- [ToScientificString()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\FloatExtensions.cs#L112-L113) - float 转为科学计数法字符串
- [ToFriendlyString()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\FloatExtensions.cs#L263-L269) - float 转为友好字符串（如 "1.23万"、"1.23亿"）

### 4. 进制转换方法

提供数值到不同进制字符串的转换。

**主要方法：**
- [ToHexString()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\FloatExtensions.cs#L235-L235) - float 转为十六进制字符串（仅整数部分）
- [ToBinaryString()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\FloatExtensions.cs#L241-L241) - float 转为二进制字符串（仅整数部分）
- [ToOctalString()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\FloatExtensions.cs#L247-L247) - float 转为八进制字符串（仅整数部分）

### 5. 范围判断方法

提供数值范围检查功能。

**主要方法：**
- [IsBetween()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\FloatExtensions.cs#L131-L132) - float 是否在指定范围内（包含边界）

### 6. 类型转换方法

提供 float 到其他数据类型的转换。

**主要方法：**
- [ToInt()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\FloatExtensions.cs#L145-L145) - float 转为 int，四舍五入
- [ToDouble()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\FloatExtensions.cs#L151-L151) - float 转为 double
- [ToLong()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\FloatExtensions.cs#L157-L157) - float 转为 long，四舍五入
- [ToDecimal()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\FloatExtensions.cs#L163-L163) - float 转为 decimal
- [ToBool()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\FloatExtensions.cs#L169-L169) - float 转为 bool（非零为 true）

### 7. 数学运算方法

提供安全的数学运算功能。

**主要方法：**
- [Add()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\FloatExtensions.cs#L175-L175) - float 加法
- [Subtract()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\FloatExtensions.cs#L181-L181) - float 减法
- [Multiply()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\FloatExtensions.cs#L187-L187) - float 乘法
- [DivideSafe()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\FloatExtensions.cs#L193-L194) - float 除法，除数为零时返回零
- [Mod()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\FloatExtensions.cs#L200-L200) - float 求余
- [Pow()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\FloatExtensions.cs#L206-L206) - float 求幂

## 使用场景

1. **数值计算** - 处理浮点数的数学运算和比较
2. **数据展示** - 将浮点数转换为各种格式的字符串显示
3. **数据验证** - 数值范围检查和状态判断
4. **科学计算** - 高精度数学运算和科学计数法表示
5. **金融计算** - 货币格式化显示和百分比计算
6. **数据转换** - float 与其他数值类型的安全转换
7. **业务逻辑** - 数值状态判断（正负、奇偶、整数等）
8. **用户界面** - 提供友好的数值显示格式（如万、亿单位）

## 注意事项

1. 所有方法都是扩展方法，需要通过 `float` 实例调用
2. 四舍五入方法使用 `MidpointRounding.AwayFromZero` 策略，符合常规数学规则
3. [IsInteger()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\FloatExtensions.cs#L31-L31) 方法使用 `MathF.Truncate` 进行整数判断
4. 除法运算提供安全版本 [DivideSafe()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\FloatExtensions.cs#L193-L194)，避免除零异常
5. 货币格式化支持多区域文化设置，默认为中文环境
6. 奇偶数判断仅对整数有效，非整数会被判定为非奇非偶
7. 进制转换方法仅处理数值的整数部分
8. [ToFriendlyString()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\FloatExtensions.cs#L263-L269) 方法提供中文习惯的大数显示格式
9. 类型转换方法在转换过程中会进行四舍五入处理
10. 特殊值判断方法可以识别 NaN 和无穷大值