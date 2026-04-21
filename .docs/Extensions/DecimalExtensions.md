# DecimalExtensions 功能文档

## 概述

[DecimalExtensions](../../Chet.Utils/Extensions/DecimalExtensions.cs) 是一个静态扩展类，为 `decimal` 类型提供了丰富的扩展方法，涵盖数值判断、转换、运算、格式化等功能，旨在简化 decimal 类型的数学运算和格式化处理，特别适合金融、财务等需要高精度计算的场景。

## 主要功能模块

### 1. 基础判断

提供 decimal 数值状态相关的数据判断方法。

#### IsZero
判断 decimal 是否为零。

```csharp
public static bool IsZero(this decimal value)
```

**参数：**
- `value`: 待判断的 decimal

**返回值：**
- 如果值为零返回 true；否则返回 false

**示例：**
```csharp
decimal value1 = 0m;
decimal value2 = 123.45m;

bool result1 = value1.IsZero(); // true
bool result2 = value2.IsZero(); // false
```

#### IsPositive
判断 decimal 是否为正数。

```csharp
public static bool IsPositive(this decimal value)
```

**参数：**
- `value`: 待判断的 decimal

**返回值：**
- 如果值大于零返回 true；否则返回 false

**示例：**
```csharp
decimal value1 = 123.45m;
decimal value2 = -123.45m;

bool result1 = value1.IsPositive(); // true
bool result2 = value2.IsPositive(); // false
```

#### IsNegative
判断 decimal 是否为负数。

```csharp
public static bool IsNegative(this decimal value)
```

**参数：**
- `value`: 待判断的 decimal

**返回值：**
- 如果值小于零返回 true；否则返回 false

**示例：**
```csharp
decimal value1 = -123.45m;
decimal value2 = 123.45m;

bool result1 = value1.IsNegative(); // true
bool result2 = value2.IsNegative(); // false
```

#### IsInteger
判断 decimal 是否为整数（无小数部分）。

```csharp
public static bool IsInteger(this decimal value)
```

**参数：**
- `value`: 待判断的 decimal

**返回值：**
- 如果值为整数返回 true；否则返回 false

**示例：**
```csharp
decimal value1 = 123m;
decimal value2 = 123.45m;

bool result1 = value1.IsInteger(); // true
bool result2 = value2.IsInteger(); // false
```

#### IsEven
判断 decimal 是否为偶数（仅整数时有效）。

```csharp
public static bool IsEven(this decimal value)
```

**参数：**
- `value`: 待判断的 decimal

**返回值：**
- 如果是偶数返回 true；否则返回 false

**示例：**
```csharp
decimal value1 = 4m;
decimal value2 = 5m;
decimal value3 = 4.5m;

bool result1 = value1.IsEven(); // true
bool result2 = value2.IsEven(); // false
bool result3 = value3.IsEven(); // false（非整数）
```

#### IsOdd
判断 decimal 是否为奇数（仅整数时有效）。

```csharp
public static bool IsOdd(this decimal value)
```

**参数：**
- `value`: 待判断的 decimal

**返回值：**
- 如果是奇数返回 true；否则返回 false

**示例：**
```csharp
decimal value1 = 5m;
decimal value2 = 4m;
decimal value3 = 5.5m;

bool result1 = value1.IsOdd(); // true
bool result2 = value2.IsOdd(); // false
bool result3 = value3.IsOdd(); // false（非整数）
```

### 2. 范围判断

提供数值范围相关的判断和约束功能。

#### IsBetween
判断 decimal 是否在指定范围内（包含边界）。

```csharp
public static bool IsBetween(this decimal value, decimal min, decimal max)
```

**参数：**
- `value`: 待判断的 decimal
- `min`: 最小值
- `max`: 最大值

**返回值：**
- 如果值在范围内返回 true；否则返回 false

**示例：**
```csharp
decimal value = 50m;

bool result1 = value.IsBetween(0m, 100m); // true
bool result2 = value.IsBetween(60m, 100m); // false
```

#### IsInRange
判断 decimal 是否在指定范围内（包含边界）。

```csharp
public static bool IsInRange(this decimal value, decimal min, decimal max)
```

**参数：**
- `value`: 待判断的 decimal
- `min`: 最小值
- `max`: 最大值

**返回值：**
- 如果值在范围内返回 true；否则返回 false

**示例：**
```csharp
decimal value = 50m;
bool result = value.IsInRange(0m, 100m); // true
```

#### Clamp
将 decimal 限制在指定范围内，超出则取边界值。

```csharp
public static decimal Clamp(this decimal value, decimal min, decimal max)
```

**参数：**
- `value`: 待处理的 decimal
- `min`: 最小值
- `max`: 最大值

**返回值：**
- 限制后的值

**示例：**
```csharp
decimal value1 = 150m;
decimal value2 = 50m;
decimal value3 = -10m;

decimal result1 = value1.Clamp(0m, 100m); // 100
decimal result2 = value2.Clamp(0m, 100m); // 50
decimal result3 = value3.Clamp(0m, 100m); // 0
```

### 3. 类型转换

提供 decimal 与其他数值类型之间的转换。

#### ToInt
将 decimal 转换为 int，四舍五入。

```csharp
public static int ToInt(this decimal value)
```

**参数：**
- `value`: 待转换的 decimal

**返回值：**
- 转换后的 int 值

**示例：**
```csharp
decimal value1 = 123.4m;
decimal value2 = 123.5m;

int result1 = value1.ToInt(); // 123
int result2 = value2.ToInt(); // 124
```

#### ToLong
将 decimal 转换为 long，四舍五入。

```csharp
public static long ToLong(this decimal value)
```

**参数：**
- `value`: 待转换的 decimal

**返回值：**
- 转换后的 long 值

**示例：**
```csharp
decimal value = 123456789.5m;
long result = value.ToLong(); // 123456790
```

#### ToDouble
将 decimal 转换为 double。

```csharp
public static double ToDouble(this decimal value)
```

**参数：**
- `value`: 待转换的 decimal

**返回值：**
- 转换后的 double 值

**示例：**
```csharp
decimal value = 123.45m;
double result = value.ToDouble(); // 123.45
```

#### ToFloat
将 decimal 转换为 float。

```csharp
public static float ToFloat(this decimal value)
```

**参数：**
- `value`: 待转换的 decimal

**返回值：**
- 转换后的 float 值

**示例：**
```csharp
decimal value = 123.45m;
float result = value.ToFloat(); // 123.45f
```

#### ToBool
将 decimal 转换为 bool（非零为 true）。

```csharp
public static bool ToBool(this decimal value)
```

**参数：**
- `value`: 待转换的 decimal

**返回值：**
- 如果值非零返回 true；否则返回 false

**示例：**
```csharp
decimal value1 = 0m;
decimal value2 = 123.45m;

bool result1 = value1.ToBool(); // false
bool result2 = value2.ToBool(); // true
```

### 4. 数学运算

提供数值约束和数学计算功能。

#### Round
将 decimal 四舍五入到指定小数位。

```csharp
public static decimal Round(this decimal value, int digits = 2)
```

**参数：**
- `value`: 待处理的 decimal
- `digits`: 保留的小数位数，默认为 2

**返回值：**
- 四舍五入后的值

**示例：**
```csharp
decimal value = 123.4567m;

decimal result1 = value.Round(); // 123.46
decimal result2 = value.Round(3); // 123.457
```

#### Truncate
将 decimal 截断到指定小数位（向零取整）。

```csharp
public static decimal Truncate(this decimal value, int digits = 2)
```

**参数：**
- `value`: 待处理的 decimal
- `digits`: 保留的小数位数，默认为 2

**返回值：**
- 截断后的值

**示例：**
```csharp
decimal value = 123.4567m;

decimal result1 = value.Truncate(); // 123.45
decimal result2 = value.Truncate(3); // 123.456
```

#### Abs
获取 decimal 的绝对值。

```csharp
public static decimal Abs(this decimal value)
```

**参数：**
- `value`: 待处理的 decimal

**返回值：**
- 绝对值

**示例：**
```csharp
decimal value1 = -123.45m;
decimal value2 = 123.45m;

decimal result1 = value1.Abs(); // 123.45
decimal result2 = value2.Abs(); // 123.45
```

#### Pow
计算 decimal 的幂次方。

```csharp
public static decimal Pow(this decimal value, int power)
```

**参数：**
- `value`: 底数
- `power`: 指数

**返回值：**
- 计算结果

**示例：**
```csharp
decimal value = 2m;

decimal result1 = value.Pow(3); // 8
decimal result2 = value.Pow(10); // 1024
```

#### Sqrt
计算 decimal 的平方根。

```csharp
public static decimal Sqrt(this decimal value)
```

**参数：**
- `value`: 待处理的 decimal

**返回值：**
- 平方根

**示例：**
```csharp
decimal value = 16m;
decimal result = value.Sqrt(); // 4
```

#### Ceiling
向上取整（天花板函数）。

```csharp
public static decimal Ceiling(this decimal value)
```

**参数：**
- `value`: 待处理的 decimal

**返回值：**
- 向上取整后的值

**示例：**
```csharp
decimal value1 = 123.1m;
decimal value2 = 123.9m;

decimal result1 = value1.Ceiling(); // 124
decimal result2 = value2.Ceiling(); // 124
```

#### Floor
向下取整（地板函数）。

```csharp
public static decimal Floor(this decimal value)
```

**参数：**
- `value`: 待处理的 decimal

**返回值：**
- 向下取整后的值

**示例：**
```csharp
decimal value1 = 123.1m;
decimal value2 = 123.9m;

decimal result1 = value1.Floor(); // 123
decimal result2 = value2.Floor(); // 123
```

#### Sign
计算 decimal 的符号。

```csharp
public static int Sign(this decimal value)
```

**参数：**
- `value`: 待处理的 decimal

**返回值：**
- 如果值为正返回 1，为零返回 0，为负返回 -1

**示例：**
```csharp
decimal value1 = 123.45m;
decimal value2 = 0m;
decimal value3 = -123.45m;

int result1 = value1.Sign(); // 1
int result2 = value2.Sign(); // 0
int result3 = value3.Sign(); // -1
```

### 5. 四则运算

提供安全的四则运算方法。

#### Add
decimal 加法。

```csharp
public static decimal Add(this decimal value, decimal other)
```

**参数：**
- `value`: 第一个值
- `other`: 第二个值

**返回值：**
- 两数之和

**示例：**
```csharp
decimal value = 100m;
decimal result = value.Add(50m); // 150
```

#### Subtract
decimal 减法。

```csharp
public static decimal Subtract(this decimal value, decimal other)
```

**参数：**
- `value`: 第一个值
- `other`: 第二个值

**返回值：**
- 两数之差

**示例：**
```csharp
decimal value = 100m;
decimal result = value.Subtract(30m); // 70
```

#### Multiply
decimal 乘法。

```csharp
public static decimal Multiply(this decimal value, decimal other)
```

**参数：**
- `value`: 第一个值
- `other`: 第二个值

**返回值：**
- 两数之积

**示例：**
```csharp
decimal value = 100m;
decimal result = value.Multiply(1.5m); // 150
```

#### DivideSafe
decimal 除法，除数为零时返回指定默认值。

```csharp
public static decimal DivideSafe(this decimal value, decimal other, decimal defaultValue = 0m)
```

**参数：**
- `value`: 被除数
- `other`: 除数
- `defaultValue`: 除数为零时的默认返回值，默认为 0

**返回值：**
- 两数之商，或默认值

**示例：**
```csharp
decimal value = 100m;

decimal result1 = value.DivideSafe(4m); // 25
decimal result2 = value.DivideSafe(0m); // 0
```

#### Mod
decimal 求余。

```csharp
public static decimal Mod(this decimal value, decimal other)
```

**参数：**
- `value`: 被除数
- `other`: 除数

**返回值：**
- 余数，除数为零时返回 0

**示例：**
```csharp
decimal value = 10m;
decimal result = value.Mod(3m); // 1
```

### 6. 比较运算

提供数值比较相关的方法。

#### Max
获取两个 decimal 中的较大值。

```csharp
public static decimal Max(this decimal value, decimal other)
```

**参数：**
- `value`: 第一个值
- `other`: 第二个值

**返回值：**
- 较大的值

**示例：**
```csharp
decimal value = 100m;
decimal result = value.Max(200m); // 200
```

#### Min
获取两个 decimal 中的较小值。

```csharp
public static decimal Min(this decimal value, decimal other)
```

**参数：**
- `value`: 第一个值
- `other`: 第二个值

**返回值：**
- 较小的值

**示例：**
```csharp
decimal value = 100m;
decimal result = value.Min(200m); // 100
```

#### AbsDiff
计算两个 decimal 的绝对差值。

```csharp
public static decimal AbsDiff(this decimal value, decimal other)
```

**参数：**
- `value`: 第一个值
- `other`: 第二个值

**返回值：**
- 绝对差值

**示例：**
```csharp
decimal value = 100m;
decimal result = value.AbsDiff(130m); // 30
```

#### EqualsTolerance
判断两个 decimal 是否在指定精度范围内相等。

```csharp
public static bool EqualsTolerance(this decimal value, decimal other, decimal tolerance = 0.0001m)
```

**参数：**
- `value`: 第一个值
- `other`: 第二个值
- `tolerance`: 容差，默认为 0.0001

**返回值：**
- 如果差值小于容差返回 true；否则返回 false

**示例：**
```csharp
decimal value1 = 1.00001m;
decimal value2 = 1.00002m;

bool result = value1.EqualsTolerance(value2, 0.0001m); // true
```

### 7. 格式化输出

提供将数值格式化为字符串的功能。

#### ToFixedString
将 decimal 转换为固定小数位字符串。

```csharp
public static string ToFixedString(this decimal value, int digits = 2)
```

**参数：**
- `value`: 待处理的 decimal
- `digits`: 保留的小数位数，默认为 2

**返回值：**
- 格式化后的字符串

**示例：**
```csharp
decimal value = 123.4567m;

string result1 = value.ToFixedString(); // "123.46"
string result2 = value.ToFixedString(3); // "123.457"
```

#### ToCurrencyString
将 decimal 转换为货币格式字符串。

```csharp
public static string ToCurrencyString(this decimal value, string culture = "zh-CN")
```

**参数：**
- `value`: 待处理的 decimal
- `culture`: 区域信息，默认为 "zh-CN"

**返回值：**
- 货币格式字符串

**示例：**
```csharp
decimal value = 1234.56m;

string result1 = value.ToCurrencyString(); // "￥1,234.56"
string result2 = value.ToCurrencyString("en-US"); // "$1,234.56"
```

#### ToPercentString
将 decimal 转换为百分比字符串。

```csharp
public static string ToPercentString(this decimal value, int digits = 2)
```

**参数：**
- `value`: 待处理的 decimal（如 0.1234 表示 12.34%）
- `digits`: 保留的小数位数，默认为 2

**返回值：**
- 百分比字符串

**示例：**
```csharp
decimal value = 0.1234m;
string result = value.ToPercentString(); // "12.34%"
```

#### ToScientificString
将 decimal 转换为科学计数法字符串。

```csharp
public static string ToScientificString(this decimal value, int digits = 2)
```

**参数：**
- `value`: 待处理的 decimal
- `digits`: 保留的小数位数，默认为 2

**返回值：**
- 科学计数法字符串

**示例：**
```csharp
decimal value = 1234567.89m;
string result = value.ToScientificString(); // "1.23E+006"
```

#### ToFriendlyString
将 decimal 转换为友好字符串（如 "1.23万"、"1.23亿"）。

```csharp
public static string ToFriendlyString(this decimal value, int digits = 2)
```

**参数：**
- `value`: 待处理的 decimal
- `digits`: 保留的小数位数，默认为 2

**返回值：**
- 友好格式字符串

**示例：**
```csharp
decimal value1 = 12345m;
decimal value2 = 123456789m;

string result1 = value1.ToFriendlyString(); // "1.23万"
string result2 = value2.ToFriendlyString(); // "1.23亿"
```

#### ToThousandsString
将 decimal 转换为带千分位的字符串。

```csharp
public static string ToThousandsString(this decimal value, int digits = 2)
```

**参数：**
- `value`: 待处理的 decimal
- `digits`: 保留的小数位数，默认为 2

**返回值：**
- 带千分位的字符串

**示例：**
```csharp
decimal value = 1234567.89m;
string result = value.ToThousandsString(); // "1,234,567.89"
```

### 8. 中文转换

提供将数值转换为中文表示的功能。

#### ToChineseUpper
将 decimal 转换为中文大写金额。

```csharp
public static string ToChineseUpper(this decimal value)
```

**参数：**
- `value`: 待处理的 decimal

**返回值：**
- 中文大写金额字符串

**示例：**
```csharp
decimal value = 1234.56m;
string result = value.ToChineseUpper(); // "壹仟贰佰叁拾肆元伍角陆分"

decimal value2 = 100m;
string result2 = value2.ToChineseUpper(); // "壹佰元整"

decimal value3 = 0.5m;
string result3 = value3.ToChineseUpper(); // "零元伍角"
```

#### ToChineseNumber
将 decimal 转换为中文数字。

```csharp
public static string ToChineseNumber(this decimal value)
```

**参数：**
- `value`: 待处理的 decimal（仅支持非负整数）

**返回值：**
- 中文数字字符串

**示例：**
```csharp
decimal value = 1234m;
string result = value.ToChineseNumber(); // "一千二百三十四"

decimal value2 = 10m;
string result2 = value2.ToChineseNumber(); // "十"

decimal value3 = 10000m;
string result3 = value3.ToChineseNumber(); // "一万"
```

## 使用场景

1. **金融计算** - 处理货币、财务、利息等需要高精度的场景
2. **数据展示** - 数值格式化显示，如货币、百分比、科学计数等格式
3. **数据验证** - 数值范围检查和状态判断
4. **数学运算** - 高精度数学运算和科学计数表示
5. **票据打印** - 金额大写转换，满足财务票据要求
6. **类型转换** - decimal 与其他数值类型的安全转换
7. **业务逻辑** - 数值状态判断（如正负、奇偶、整数等）
8. **用户界面** - 提供友好的数值表示形式

## 注意事项

1. 所有方法都是扩展方法，需要通过 `decimal` 实例调用
2. 四舍五入方法使用 `MidpointRounding.AwayFromZero` 策略，符合传统数学习惯
3. `ToChineseUpper()` 方法实现了完整的中文大写金额转换，适合用于财务场景
4. 除法运算提供安全版本 `DivideSafe()`，避免除零异常
5. 货币格式支持多语言文化设置，默认为中文环境
6. 数值范围超过 999999999999999.99 时会返回提示信息
7. 类型转换方法在转换前会进行四舍五入处理
8. 奇偶判断仅对整数有效，小数会被判断为非奇非偶
