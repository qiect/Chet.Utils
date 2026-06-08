---
title: "StringExtensions"
description: "为 string 类型提供了丰富的扩展方法，包括空值判断、类型验证、类型转换、编码解码、字符串操作等功能，旨在简化字符串处理，提高代码的安全性和可读性，特别是用于处理可能为空的字符串类型的日常操作场景。"
targetType: "string"
namespace: "Chet.Utils"
className: "StringExtensions"
category: "Extensions"
order: 12
---

# StringExtensions 扩展类文档

## 概述

[StringExtensions](../../Chet.Utils/Extensions/StringExtensions.cs) 是一个静态扩展类，为 `string` 类型提供了丰富的扩展方法，包括空值判断、类型验证、类型转换、编码解码、字符串操作等功能，旨在简化字符串处理，提高代码的安全性和可读性，特别是用于处理可能为空的字符串类型的日常操作场景。

## 主要功能模块

### 1. 空值判断方法

提供字符串状态判断和空值验证的便捷方法。

**主要方法：**
- `IsNullOrEmpty()` - 判断字符串是否为 null 或空字符串
- `IsNullOrWhiteSpace()` - 判断字符串是否为 null 或仅包含空白字符
- `IsNotNullOrEmpty()` - 判断字符串是否不为空
- `IsNotNullOrWhiteSpace()` - 判断字符串是否不为空白
- `IsNull(string nullStrings, bool isTrim)` - 判断字符串是否为空（支持自定义空值判断）

### 2. 类型判断方法

提供字符串类型验证的便捷方法。

**主要方法：**
- `IsNumeric()` - 判断字符串是否为数字
- `IsInt()` - 判断字符串是否为整数
- `IsLong()` - 判断字符串是否为长整数
- `IsFloat()` - 判断字符串是否为浮点数
- `IsDouble()` - 判断字符串是否为双精度浮点数
- `IsDecimal()` - 判断字符串是否为十进制数
- `IsGuid()` - 判断字符串是否为 Guid
- `IsBool()` - 判断字符串是否为布尔值
- `IsDateTime()` - 判断字符串是否为日期时间
- `IsLetter()` - 判断字符串是否仅包含字母
- `IsLetterOrDigit()` - 判断字符串是否仅包含字母或数字
- `IsChinese()` - 判断字符串是否为中文字符
- `HasChinese()` - 判断字符串中是否包含中文
- `IsDigits()` - 判断字符串是否为纯数字字符串
- `IsHex()` - 判断字符串是否为十六进制字符串

### 3. 正则表达式验证

提供基于正则表达式的字符串格式验证功能。

**主要方法：**
- `IsEmail()` - 验证字符串是否为有效的电子邮件地址
- `IsMobile()` - 验证字符串是否为有效的手机号码（中国大陆）
- `IsTel()` - 验证字符串是否为有效的固定电话号码
- `IsUrl()` - 验证字符串是否为有效的 URL
- `IsIdCard()` - 验证字符串是否为有效的身份证号码（中国大陆）
- `IsIpAddress()` - 验证字符串是否为有效的 IP 地址
- `IsIPv4()` - 验证字符串是否为有效的 IPv4 地址
- `IsIPv6()` - 验证字符串是否为有效的 IPv6 地址
- `IsZipCode()` - 验证字符串是否为有效的邮政编码
- `IsBankCard()` - 验证字符串是否为有效的银行卡号
- `IsJson()` - 验证字符串是否为有效的 JSON 格式
- `IsXml()` - 验证字符串是否为有效的 XML 格式
- `IsDate()` - 验证字符串是否为有效的日期格式
- `IsTime()` - 验证字符串是否为有效的时间格式

### 4. 类型转换方法

提供字符串到各种类型的转换功能。

**主要方法：**
- `ToInt(int defaultValue = 0)` - 字符串转 int，失败返回默认值
- `ToLong(long defaultValue = 0)` - 字符串转 long，失败返回默认值
- `ToFloat(float defaultValue = 0)` - 字符串转 float，失败返回默认值
- `ToDouble(double defaultValue = 0)` - 字符串转 double，失败返回默认值
- `ToDecimal(decimal defaultValue = 0)` - 字符串转 decimal，失败返回默认值
- `ToBool(bool defaultValue = false)` - 字符串转 bool，失败返回默认值
- `ToGuid(Guid? defaultValue = null)` - 字符串转 Guid，失败返回默认值
- `ToDateTime(DateTime? defaultValue = null)` - 字符串转 DateTime，失败返回默认值

#### 数值约束转换方法
- `ToDoubleRound(int decimals)` - 字符串转 double 并保留指定小数位数（四舍五入）
- `ToDoubleTruncate(int decimals)` - 字符串转 double 并保留指定小数位数（截取）
- `ToFloatRound(int decimals)` - 字符串转 float 并保留指定小数位数（四舍五入）
- `ToFloatTruncate(int decimals)` - 字符串转 float 并保留指定小数位数（截取）
- `KeepDecimal(int decimals)` - 保留数值字符串的小数位数

### 5. 字符串操作方法

提供字符串处理和转换功能。

**主要方法：**
- `TrimSafe()` - 安全去除字符串首尾空白字符
- `RemoveWhiteSpace()` - 移除字符串中的所有空白字符
- `SubstringSafe(int startIndex, int length)` - 安全截取字符串
- `Left(int length)` - 获取字符串左部指定长度的子串
- `Right(int length)` - 获取字符串右部指定长度的子串
- `Reverse()` - 反转字符串
- `RemoveSpecialChars()` - 移除字符串中的特殊字符
- `ToCamelCase()` - 转为 camelCase（首字母小写）
- `ToPascalCase()` - 转为 PascalCase（首字母大写）
- `Repeat(int count)` - 重复字符串指定次数
- `ReplaceIgnoreCase(string oldValue, string newValue)` - 忽略大小写替换字符串中的指定内容
- `ContainsIgnoreCase(string value)` - 判断字符串是否包含指定子串（忽略大小写）
- `SplitSafe(string separator)` - 将字符串按分隔符分割为字符串数组

### 6. 编码解码方法

提供字符串编码解码功能。

**主要方法：**
- `ToBase64()` - 将字符串编码为 Base64
- `FromBase64()` - 将 Base64 字符串解码
- `UrlEncode()` - URL 编码
- `UrlDecode()` - URL 解码
- `HtmlEncode()` - HTML 编码
- `HtmlDecode()` - HTML 解码

### 7. 哈希计算方法

提供字符串哈希值计算功能。

**主要方法：**
- `ToMd5()` - 获取字符串的 MD5 值（32位小写）
- `ToSha256()` - 获取字符串的 SHA256 值
- `ToSha1()` - 获取字符串的 SHA1 值

### 8. 掩码和脱敏方法

提供字符串掩码和脱敏功能。

**主要方法：**
- `Mask(int start, int length, char maskChar = '*')` - 对字符串指定位置进行掩码
- `MaskEmail()` - 对邮箱地址进行掩码
- `MaskMobile()` - 对手机号码进行掩码
- `MaskIdCard()` - 对身份证号码进行掩码
- `MaskBankCard()` - 对银行卡号进行掩码

---

## 方法详细说明

### 空值判断

#### IsNullOrEmpty
判断字符串是否为 null 或空字符串。

```csharp
public static bool IsNullOrEmpty(this string value)
```

**参数：**
- `value`: 待判断的字符串

**返回值：**
- 如果字符串为 null 或空字符串，返回 true；否则返回 false

**示例：**
```csharp
string str1 = null;
string str2 = "";
string str3 = "hello";

bool result1 = str1.IsNullOrEmpty(); // true
bool result2 = str2.IsNullOrEmpty(); // true
bool result3 = str3.IsNullOrEmpty(); // false
```

#### IsNull
判断字符串是否为空（支持自定义空值判断）。

```csharp
public static bool IsNull(this string value, string nullStrings = "null|{}|[]", bool isTrim = false)
```

**参数：**
- `value`: 源字符串
- `nullStrings`: 自定义空字符串，使用 "|" 分隔，默认为 "null|{}|[]"
- `isTrim`: 是否移除首尾空白字符后再判断，默认为 false

**返回值：**
- 如果字符串为空或匹配自定义空字符串，返回 true；否则返回 false

**示例：**
```csharp
string str1 = "null";
string str2 = "{}";
string str3 = "hello";

bool result1 = str1.IsNull(); // true
bool result2 = str2.IsNull(); // true
bool result3 = str3.IsNull(); // false
```

### 类型判断

#### IsNumeric
判断字符串是否为数字。

```csharp
public static bool IsNumeric(this string value)
```

**参数：**
- `value`: 待判断的字符串

**返回值：**
- 如果字符串可以解析为 double，返回 true；否则返回 false

**示例：**
```csharp
string str1 = "123.45";
string str2 = "abc";

bool result1 = str1.IsNumeric(); // true
bool result2 = str2.IsNumeric(); // false
```

#### IsChinese
判断字符串是否为中文字符。

```csharp
public static bool IsChinese(this string value)
```

**参数：**
- `value`: 待判断的字符串

**返回值：**
- 如果字符串仅包含中文和中文标点，返回 true；否则返回 false

**示例：**
```csharp
string str1 = "你好世界";
string str2 = "Hello";

bool result1 = str1.IsChinese(); // true
bool result2 = str2.IsChinese(); // false
```

### 正则表达式验证

#### IsEmail
验证字符串是否为有效的电子邮件地址。

```csharp
public static bool IsEmail(this string value)
```

**参数：**
- `value`: 待验证的字符串

**返回值：**
- 如果字符串为有效的电子邮件地址，返回 true；否则返回 false

**示例：**
```csharp
string str1 = "test@example.com";
string str2 = "invalid-email";

bool result1 = str1.IsEmail(); // true
bool result2 = str2.IsEmail(); // false
```

#### IsMobile
验证字符串是否为有效的手机号码（中国大陆）。

```csharp
public static bool IsMobile(this string value)
```

**参数：**
- `value`: 待验证的字符串

**返回值：**
- 如果字符串为有效的手机号码，返回 true；否则返回 false

**示例：**
```csharp
string str1 = "13812345678";
string str2 = "12345678901";

bool result1 = str1.IsMobile(); // true
bool result2 = str2.IsMobile(); // false
```

#### IsIdCard
验证字符串是否为有效的身份证号码（中国大陆）。

```csharp
public static bool IsIdCard(this string value)
```

**参数：**
- `value`: 待验证的字符串

**返回值：**
- 如果字符串为有效的身份证号码，返回 true；否则返回 false

**示例：**
```csharp
string str1 = "11010519900307233X";
string str2 = "123456789012345";

bool result1 = str1.IsIdCard(); // true
bool result2 = str2.IsIdCard(); // false
```

### 类型转换

#### ToInt
将字符串转换为 int。

```csharp
public static int ToInt(this string value, int defaultValue = 0)
```

**参数：**
- `value`: 待转换的字符串
- `defaultValue`: 转换失败时的默认值，默认为 0

**返回值：**
- 转换后的 int 值，失败时返回默认值

**示例：**
```csharp
string str1 = "123";
string str2 = "abc";

int result1 = str1.ToInt(); // 123
int result2 = str2.ToInt(); // 0
int result3 = str2.ToInt(-1); // -1
```

#### ToDouble
将字符串转换为 double。

```csharp
public static double ToDouble(this string value, double defaultValue = 0)
```

**参数：**
- `value`: 待转换的字符串
- `defaultValue`: 转换失败时的默认值，默认为 0

**返回值：**
- 转换后的 double 值，失败时返回默认值

**示例：**
```csharp
string str1 = "123.45";
string str2 = "abc";

double result1 = str1.ToDouble(); // 123.45
double result2 = str2.ToDouble(); // 0
```

### 字符串操作

#### SubstringSafe
安全截取字符串。

```csharp
public static string SubstringSafe(this string value, int startIndex, int length)
```

**参数：**
- `value`: 源字符串
- `startIndex`: 起始位置
- `length`: 截取长度

**返回值：**
- 截取的子串，如果越界则返回尽可能多的字符

**示例：**
```csharp
string str = "Hello World";

string result1 = str.SubstringSafe(0, 5); // "Hello"
string result2 = str.SubstringSafe(6, 100); // "World"
```

#### Left
获取字符串左部指定长度的子串。

```csharp
public static string Left(this string value, int length)
```

**参数：**
- `value`: 源字符串
- `length`: 要获取的长度

**返回值：**
- 左部子串

**示例：**
```csharp
string str = "Hello World";

string result = str.Left(5); // "Hello"
```

#### Reverse
反转字符串。

```csharp
public static string Reverse(this string value)
```

**参数：**
- `value`: 源字符串

**返回值：**
- 反转后的字符串

**示例：**
```csharp
string str = "Hello";

string result = str.Reverse(); // "olleH"
```

### 编码解码

#### ToBase64
将字符串编码为 Base64。

```csharp
public static string ToBase64(this string value)
```

**参数：**
- `value`: 源字符串

**返回值：**
- Base64 编码后的字符串

**示例：**
```csharp
string str = "Hello World";

string result = str.ToBase64(); // "SGVsbG8gV29ybGQ="
```

#### FromBase64
将 Base64 字符串解码。

```csharp
public static string FromBase64(this string value)
```

**参数：**
- `value`: Base64 编码的字符串

**返回值：**
- 解码后的字符串

**示例：**
```csharp
string str = "SGVsbG8gV29ybGQ=";

string result = str.FromBase64(); // "Hello World"
```

### 哈希计算

#### ToMd5
获取字符串的 MD5 值。

```csharp
public static string ToMd5(this string value)
```

**参数：**
- `value`: 源字符串

**返回值：**
- MD5 哈希值（32位小写）

**示例：**
```csharp
string str = "Hello World";

string result = str.ToMd5(); // "b10a8db164e0754105b7a99be72e3fe5"
```

### 掩码脱敏

#### Mask
对字符串指定位置进行掩码。

```csharp
public static string Mask(this string value, int start, int length, char maskChar = '*')
```

**参数：**
- `value`: 源字符串
- `start`: 掩码起始位置
- `length`: 掩码长度
- `maskChar`: 掩码字符，默认为 '*'

**返回值：**
- 掩码后的字符串

**示例：**
```csharp
string str = "13812345678";

string result = str.Mask(3, 4); // "138****5678"
```

#### MaskMobile
对手机号码进行掩码。

```csharp
public static string MaskMobile(this string value)
```

**参数：**
- `value`: 手机号码

**返回值：**
- 掩码后的手机号码

**示例：**
```csharp
string str = "13812345678";

string result = str.MaskMobile(); // "138****5678"
```

---

## 使用场景

1. **表单验证** - 验证用户输入的格式是否正确（如邮箱、手机号、地址等）
2. **数据转换** - 安全地将字符串转换为各种数值类型
3. **字符串处理** - 字符串截取、拼接、格式化等操作
4. **数据清洗** - 移除特殊字符、空白字符等预处理
5. **格式验证** - 验证数据是否符合特定格式要求
6. **文本分析** - 判断文本内容类型（中文、数字、字母等）
7. **安全处理** - 避免 null 异常的安全字符串操作
8. **编码转换** - 字符串与各种编码格式之间的相互转换

---

## 注意事项

1. 所有方法都是扩展方法，需要通过 `string` 实例调用
2. 带有 "Safe" 后缀的方法对 null 值做了安全处理，不会抛出异常
3. 类型转换方法在转换失败时返回指定的默认值，而不是抛出异常
4. 正则表达式验证方法使用预编译的正则模式进行格式验证
5. 数值约束方法支持四舍五入和截取两种小数处理方式
6. 字符串操作方法支持边界检查，避免越界异常
7. 哈希计算方法使用 UTF-8 编码进行字节转换
8. 中文判断方法对 Unicode 字符范围进行识别
9. 自定义空字符串判断支持多空值配置
10. 所有方法都进行了空值有效性检查
