# FloatExtensions 功能文档

## 概述

[FloatExtensions](../../Chet.Utils/Extensions/FloatExtensions.cs) 是一个静态扩展类，为 `float` 类型提供了丰富的扩展方法，涵盖数值判断、转换、运算、格式化等功能，旨在简化 float 类型的数学运算和格式化处理，特别适用于需要进行数值计算和日常数据展示的场景。

## 主要功能模块

### 1. 基础判断

#### IsZero
判断 float 是否为零。
```csharp
public static bool IsZero(this float value)
```
**示例：**
```csharp
float value1 = 0f;
float value2 = 123.45f;

bool result1 = value1.IsZero(); // true
bool result2 = value2.IsZero(); // false
```

#### IsPositive
判断 float 是否为正数。
```csharp
public static bool IsPositive(this float value)
```
**示例：**
```csharp
float value1 = 123.45f;
float value2 = -123.45f;

bool result1 = value1.IsPositive(); // true
bool result2 = value2.IsPositive(); // false
```

#### IsNegative
判断 float 是否为负数。
```csharp
public static bool IsNegative(this float value)
```
**示例：**
```csharp
float value1 = -123.45f;
float value2 = 123.45f;

bool result1 = value1.IsNegative(); // true
bool result2 = value2.IsNegative(); // false
```

#### IsInteger
判断 float 是否为整数（无小数部分）。
```csharp
public static bool IsInteger(this float value)
```
**示例：**
```csharp
float value1 = 123f;
float value2 = 123.45f;

bool result1 = value1.IsInteger(); // true
bool result2 = value2.IsInteger(); // false
```

#### IsEven
判断 float 是否为偶数（仅整数时有效）。
```csharp
public static bool IsEven(this float value)
```
**示例：**
```csharp
float value1 = 4f;
float value2 = 5f;
float value3 = 4.5f;

bool result1 = value1.IsEven(); // true
bool result2 = value2.IsEven(); // false
bool result3 = value3.IsEven(); // false（非整数）
```

#### IsOdd
判断 float 是否为奇数（仅整数时有效）。
```csharp
public static bool IsOdd(this float value)
```
**示例：**
```csharp
float value1 = 5f;
float value2 = 4f;
float value3 = 5.5f;

bool result1 = value1.IsOdd(); // true
bool result2 = value2.IsOdd(); // false
bool result3 = value3.IsOdd(); // false（非整数）
```

---

### 2. 特殊值判断

#### IsNaN
判断 float 是否为 NaN（非数字）。
```csharp
public static bool IsNaN(this float value)
```
**示例：**
```csharp
float value1 = float.NaN;
float value2 = 123.45f;

bool result1 = value1.IsNaN(); // true
bool result2 = value2.IsNaN(); // false
```

#### IsInfinity
判断 float 是否为无穷大（正无穷或负无穷）。
```csharp
public static bool IsInfinity(this float value)
```
**示例：**
```csharp
float value1 = float.PositiveInfinity;
float value2 = 123.45f;

bool result1 = value1.IsInfinity(); // true
bool result2 = value2.IsInfinity(); // false
```

#### IsPositiveInfinity
判断 float 是否为正无穷。
```csharp
public static bool IsPositiveInfinity(this float value)
```
**示例：**
```csharp
float value1 = float.PositiveInfinity;
float value2 = float.NegativeInfinity;

bool result1 = value1.IsPositiveInfinity(); // true
bool result2 = value2.IsPositiveInfinity(); // false
```

#### IsNegativeInfinity
判断 float 是否为负无穷。
```csharp
public static bool IsNegativeInfinity(this float value)
```
**示例：**
```csharp
float value1 = float.NegativeInfinity;
float value2 = float.PositiveInfinity;

bool result1 = value1.IsNegativeInfinity(); // true
bool result2 = value2.IsNegativeInfinity(); // false
```

#### IsValid
判断 float 是否为有效数值（非 NaN 且非无穷大）。
```csharp
public static bool IsValid(this float value)
```
**示例：**
```csharp
float value1 = 123.45f;
float value2 = float.NaN;
float value3 = float.PositiveInfinity;

bool result1 = value1.IsValid(); // true
bool result2 = value2.IsValid(); // false
bool result3 = value3.IsValid(); // false
```

---

### 3. 范围判断

#### IsBetween
判断 float 是否在指定范围内（包含边界）。
```csharp
public static bool IsBetween(this float value, float min, float max)
```
**示例：**
```csharp
float value = 50f;

bool result1 = value.IsBetween(0f, 100f); // true
bool result2 = value.IsBetween(60f, 100f); // false
```

#### IsInRange
判断 float 是否在指定范围内（包含边界）。
```csharp
public static bool IsInRange(this float value, float min, float max)
```

#### Clamp
将 float 限制在指定范围内，超出则取边界值。
```csharp
public static float Clamp(this float value, float min, float max)
```
**示例：**
```csharp
float value1 = 150f;
float value2 = 50f;
float value3 = -10f;

float result1 = value1.Clamp(0f, 100f); // 100
float result2 = value2.Clamp(0f, 100f); // 50
float result3 = value3.Clamp(0f, 100f); // 0
```

---

### 4. 类型转换

#### ToInt
将 float 转换为 int，四舍五入。
```csharp
public static int ToInt(this float value)
```
**示例：**
```csharp
float value1 = 123.4f;
float value2 = 123.5f;

int result1 = value1.ToInt(); // 123
int result2 = value2.ToInt(); // 124
```

#### ToLong
将 float 转换为 long，四舍五入。
```csharp
public static long ToLong(this float value)
```
**示例：**
```csharp
float value = 123456789.5f;
long result = value.ToLong(); // 123456790
```

#### ToDecimal
将 float 转换为 decimal。
```csharp
public static decimal ToDecimal(this float value)
```
**示例：**
```csharp
float value = 123.45f;
decimal result = value.ToDecimal(); // 123.45m
```

#### ToDouble
将 float 转换为 double。
```csharp
public static double ToDouble(this float value)
```
**示例：**
```csharp
float value = 123.45f;
double result = value.ToDouble(); // 123.45d
```

#### ToBool
将 float 转换为 bool（非零为 true）。
```csharp
public static bool ToBool(this float value)
```
**示例：**
```csharp
float value1 = 0f;
float value2 = 123.45f;

bool result1 = value1.ToBool(); // false
bool result2 = value2.ToBool(); // true
```

---

### 5. 数学运算

#### Round
将 float 四舍五入到指定小数位。
```csharp
public static float Round(this float value, int digits = 2)
```
**示例：**
```csharp
float value = 123.4567f;

float result1 = value.Round(); // 123.46
float result2 = value.Round(3); // 123.457
```

#### Truncate
将 float 截断到指定小数位（向零取整）。
```csharp
public static float Truncate(this float value, int digits = 2)
```
**示例：**
```csharp
float value = 123.4567f;

float result1 = value.Truncate(); // 123.45
float result2 = value.Truncate(3); // 123.456
```

#### Abs
获取 float 的绝对值。
```csharp
public static float Abs(this float value)
```
**示例：**
```csharp
float value1 = -123.45f;
float value2 = 123.45f;

float result1 = value1.Abs(); // 123.45
float result2 = value2.Abs(); // 123.45
```

#### Pow
计算 float 的幂次方。
```csharp
public static float Pow(this float value, float power)
```
**示例：**
```csharp
float value = 2f;

float result1 = value.Pow(3); // 8
float result2 = value.Pow(10); // 1024
```

#### Sqrt
计算 float 的平方根。
```csharp
public static float Sqrt(this float value)
```
**示例：**
```csharp
float value = 16f;
float result = value.Sqrt(); // 4
```

#### Ceiling
向上取整（天花板函数）。
```csharp
public static float Ceiling(this float value)
```
**示例：**
```csharp
float value1 = 123.1f;
float value2 = 123.9f;

float result1 = value1.Ceiling(); // 124
float result2 = value2.Ceiling(); // 124
```

#### Floor
向下取整（地板函数）。
```csharp
public static float Floor(this float value)
```
**示例：**
```csharp
float value1 = 123.1f;
float value2 = 123.9f;

float result1 = value1.Floor(); // 123
float result2 = value2.Floor(); // 123
```

#### Sign
计算 float 的符号。
```csharp
public static int Sign(this float value)
```
**示例：**
```csharp
float value1 = 123.45f;
float value2 = 0f;
float value3 = -123.45f;

int result1 = value1.Sign(); // 1
int result2 = value2.Sign(); // 0
int result3 = value3.Sign(); // -1
```

---

### 6. 四则运算

#### Add
float 加法。
```csharp
public static float Add(this float value, float other)
```
**示例：**
```csharp
float value = 100f;
float result = value.Add(50f); // 150
```

#### Subtract
float 减法。
```csharp
public static float Subtract(this float value, float other)
```
**示例：**
```csharp
float value = 100f;
float result = value.Subtract(30f); // 70
```

#### Multiply
float 乘法。
```csharp
public static float Multiply(this float value, float other)
```
**示例：**
```csharp
float value = 100f;
float result = value.Multiply(1.5f); // 150
```

#### DivideSafe
float 除法，除数为零时返回指定默认值。
```csharp
public static float DivideSafe(this float value, float other, float defaultValue = 0f)
```
**示例：**
```csharp
float value = 100f;

float result1 = value.DivideSafe(4f); // 25
float result2 = value.DivideSafe(0f); // 0
```

#### Mod
float 求余。
```csharp
public static float Mod(this float value, float other)
```

---

### 7. 比较运算

#### Max
求两个 float 的最大值。
```csharp
public static float Max(this float value, float other)
```

#### Min
求两个 float 的最小值。
```csharp
public static float Min(this float value, float other)
```

#### AbsDiff
求两个 float 的绝对差值。
```csharp
public static float AbsDiff(this float value, float other)
```

#### EqualsTolerance
判断两个 float 是否在指定容差范围内相等。
```csharp
public static bool EqualsTolerance(this float value, float other, float tolerance = 0.0001f)
```

---

### 8. 格式化输出

#### ToFixedString
将 float 转换为字符串，保留指定小数位。
```csharp
public static string ToFixedString(this float value, int digits = 2)
```
**示例：**
```csharp
float value = 123.4567f;

string result1 = value.ToFixedString(); // "123.46"
string result2 = value.ToFixedString(3); // "123.457"
```

#### ToCurrencyString
将 float 转换为货币格式字符串。
```csharp
public static string ToCurrencyString(this float value, string culture = "zh-CN")
```
**示例：**
```csharp
float value = 1234.56f;
string result = value.ToCurrencyString(); // "￥1,234.56"
```

#### ToPercentString
将 float 转换为百分比字符串。
```csharp
public static string ToPercentString(this float value, int digits = 2)
```
**示例：**
```csharp
float value = 0.1234f;
string result = value.ToPercentString(); // "12.34%"
```

#### ToScientificString
将 float 转换为科学计数法字符串。
```csharp
public static string ToScientificString(this float value, int digits = 2)
```
**示例：**
```csharp
float value = 123456.78f;
string result = value.ToScientificString(); // "1.23E+05"
```

#### ToFriendlyString
将 float 转换为友好字符串（如 "1.23万"、"1.23亿"）。
```csharp
public static string ToFriendlyString(this float value, int digits = 2)
```
**示例：**
```csharp
float value1 = 12345f;
float value2 = 123456789f;

string result1 = value1.ToFriendlyString(); // "1.23万"
string result2 = value2.ToFriendlyString(); // "1.23亿"
```

#### ToThousandsString
将 float 转换为千分位格式字符串。
```csharp
public static string ToThousandsString(this float value, int digits = 2)
```
**示例：**
```csharp
float value = 1234567.89f;
string result = value.ToThousandsString(); // "1,234,567.89"
```

---

### 9. 进制转换

#### ToHexString
将 float 转换为十六进制字符串（浮点数内存表示）。
```csharp
public static string ToHexString(this float value)
```

#### ToBinaryString
将 float 转换为二进制字符串（浮点数内存表示）。
```csharp
public static string ToBinaryString(this float value)
```

#### ToOctalString
将 float 转换为八进制字符串（浮点数内存表示）。
```csharp
public static string ToOctalString(this float value)
```

---

## 使用场景

1. **数值计算** - 进行各种数学运算和比较
2. **金额展示** - 将数值转换为指定格式的字符串显示
3. **数据验证** - 数值范围和状态判断
4. **科学计算** - 高精度数学运算和科学计数法表示
5. **财务计算** - 货币格式显示和百分比计算
6. **类型转换** - float 与其他数值类型的安全转换
7. **业务逻辑** - 数值状态判断（正负、奇偶等）
8. **用户界面** - 提供友好的数值表示形式（带单位）

## 注意事项

1. 所有方法都是扩展方法，需要通过 `float` 实例来调用
2. 四舍五入方法使用 `MidpointRounding.AwayFromZero` 策略，符合常规数学习惯
3. `IsInteger` 方法使用 `MathF.Truncate` 进行整数判断
4. 除法方法提供安全版本 `DivideSafe`，避免除零异常
5. 货币格式方法支持多语言区域设置，默认为中文区域
6. 奇偶判断仅对整数有效，非整数会被判断为非奇非偶
7. 类型转换方法会对数值进行四舍五入处理
8. `ToFriendlyString` 方法提供符合中文习惯的大数表示
9. 进制转换方法用于查看浮点数的内存表示
10. 特殊值判断方法可识别 NaN 和无穷大等特殊值
