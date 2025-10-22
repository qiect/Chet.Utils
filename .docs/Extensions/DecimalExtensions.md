# DecimalExtensions 类功能文档

## 概述

[DecimalExtensions](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\DecimalExtensions.cs#L8-L310) 是一个静态扩展类，为 `decimal` 类型提供了丰富的扩展方法。该类包含数值判断、转换、运算、格式化等多种功能，旨在简化 decimal 类型的数学运算和格式化操作，特别适用于金融、会计等需要高精度计算的场景。

## 主要功能模块

### 1. 数值判断方法

提供 decimal 数值状态检查的便捷方法。

**主要方法：**
- [IsZero()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\DecimalExtensions.cs#L13-L13) - 判断 decimal 是否为零
- [IsPositive()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\DecimalExtensions.cs#L19-L19) - 判断 decimal 是否为正数
- [IsNegative()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\DecimalExtensions.cs#L25-L25) - 判断 decimal 是否为负数
- [IsInteger()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\DecimalExtensions.cs#L31-L31) - 判断 decimal 是否为整数
- [IsEven()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\DecimalExtensions.cs#L37-L37) - 判断 decimal 是否为偶数
- [IsOdd()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\DecimalExtensions.cs#L43-L43) - 判断 decimal 是否为奇数

### 2. 数值处理方法

提供数值修约和数学运算功能。

**主要方法：**
- [Round()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\DecimalExtensions.cs#L49-L50) - decimal 四舍五入到指定小数位
- [Truncate()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\DecimalExtensions.cs#L56-L61) - decimal 截断到指定小数位（向零取整）
- [Abs()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\DecimalExtensions.cs#L67-L67) - decimal 取绝对值
- [Max()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\DecimalExtensions.cs#L172-L172) - decimal 取最大值
- [Min()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\DecimalExtensions.cs#L178-L178) - decimal 取最小值
- [Clamp()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\DecimalExtensions.cs#L192-L193) - decimal 保证在指定范围内，超出则取边界值
- [Sqrt()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\DecimalExtensions.cs#L289-L289) - decimal 求平方根
- [AbsDiff()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\DecimalExtensions.cs#L295-L295) - decimal 求绝对差值

### 3. 字符串格式化方法

提供多种数值格式的字符串转换。

**主要方法：**
- [ToFixedString()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\DecimalExtensions.cs#L73-L74) - decimal 转为字符串，保留指定小数位
- [ToCurrencyString()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\DecimalExtensions.cs#L80-L81) - decimal 转为货币格式字符串
- [ToPercentString()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\DecimalExtensions.cs#L87-L88) - decimal 转为百分比字符串
- [ToChineseUpper()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\DecimalExtensions.cs#L94-L157) - decimal 转为大写金额（中文）
- [ToScientificString()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\DecimalExtensions.cs#L163-L164) - decimal 转为科学计数法字符串

### 4. 范围判断方法

提供数值范围检查功能。

**主要方法：**
- [IsBetween()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\DecimalExtensions.cs#L185-L186) - decimal 是否在指定范围内（包含边界）

### 5. 类型转换方法

提供 decimal 到其他数据类型的转换。

**主要方法：**
- [ToInt()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\DecimalExtensions.cs#L199-L199) - decimal 转为 int，四舍五入
- [ToDouble()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\DecimalExtensions.cs#L205-L205) - decimal 转为 double
- [ToFloat()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\DecimalExtensions.cs#L211-L211) - decimal 转为 float
- [ToLong()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\DecimalExtensions.cs#L217-L217) - decimal 转为 long，四舍五入
- [ToBool()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\DecimalExtensions.cs#L223-L223) - decimal 转为 bool（非零为 true）

### 6. 数学运算方法

提供安全的数学运算功能。

**主要方法：**
- [Multiply()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\DecimalExtensions.cs#L229-L229) - decimal 乘法
- [DivideSafe()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\DecimalExtensions.cs#L235-L236) - decimal 除法，除数为零时返回零
- [Add()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\DecimalExtensions.cs#L242-L242) - decimal 加法
- [Subtract()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\DecimalExtensions.cs#L248-L248) - decimal 减法
- [Mod()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\DecimalExtensions.cs#L254-L254) - decimal 求余
- [Pow()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\DecimalExtensions.cs#L260-L260) - decimal 求幂

## 使用场景

1. **金融计算** - 处理货币金额、利率计算等需要高精度的场景
2. **报表生成** - 数值格式化显示，包括货币、百分比等格式
3. **数据验证** - 数值范围检查和状态判断
4. **科学计算** - 高精度数学运算和科学计数法表示
5. **中文处理** - 金额大写转换，满足财务票据要求
6. **数据转换** - decimal 与其他数值类型的安全转换
7. **业务逻辑** - 数值状态判断（正负、奇偶、整数等）
8. **用户界面** - 提供友好的数值显示格式

## 注意事项

1. 所有方法都是扩展方法，需要通过 `decimal` 实例调用
2. 四舍五入方法使用 `MidpointRounding.AwayFromZero` 策略，符合常规数学规则
3. [ToChineseUpper()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\DecimalExtensions.cs#L94-L157) 方法实现了完整的中文大写金额转换，适用于财务场景
4. 除法运算提供安全版本 [DivideSafe()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\DecimalExtensions.cs#L235-L236)，避免除零异常
5. 货币格式化支持多区域文化设置，默认为中文环境
6. 数值范围限制在 999999999999999.99 以内，超出范围会返回提示信息
7. 类型转换方法在转换过程中会进行四舍五入处理
8. 奇偶数判断仅对整数有效，非整数会被判定为非奇非偶