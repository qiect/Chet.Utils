---
title: "RegexHelper"
description: "是一个静态帮助类，为正则表达式操作提供了丰富的功能，包括基础验证、格式验证、匹配操作、文本替换、信息提取、格式化等，旨在简化常见的正则表达式应用场景开发，提高代码的可读性和可维护性。"
namespace: "Chet.Utils.Helpers"
className: "RegexHelper"
category: "Helpers"
order: 6
---

# RegexHelper 帮助类

## 概述

[RegexHelper](../../Chet.Utils/Helpers/RegexHelper.cs) 是一个静态帮助类，为正则表达式操作提供了丰富的功能，包括基础验证、格式验证、匹配操作、文本替换、信息提取、格式化等，旨在简化常见的正则表达式应用场景开发，提高代码的可读性和可维护性。

## 常用正则表达式模式

该类预定义了以下常用正则表达式模式常量：

| 常量名 | 说明 | 模式示例 |
|--------|------|----------|
| `EmailPattern` | 电子邮件 | `^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$` |
| `MobilePhonePattern` | 中国大陆手机号 | `^1[3-9]\d{9}$` |
| `TelephonePattern` | 固定电话 | `^(\d{3,4}-)?\d{7,8}(-\d{1,4})?$` |
| `IdCardPattern` | 身份证号码（15位或18位） | 18位含校验码 |
| `UrlPattern` | URL地址 | `^(https?|ftp)://[^\s/$.?#].[^\s]*$` |
| `IPv4Pattern` | IPv4地址 | 标准 IPv4 格式 |
| `IPv6Pattern` | IPv6地址 | 标准 IPv6 格式 |
| `IntegerPattern` | 整数 | `^-?\d+$` |
| `PositiveIntegerPattern` | 正整数 | `^[1-9]\d*$` |
| `FloatPattern` | 浮点数 | `^-?\d+(\.\d+)?$` |
| `ChinesePattern` | 中文字符 | `^[\u4e00-\u9fa5]+$` |
| `LetterPattern` | 字母 | `^[a-zA-Z]+$` |
| `AlphanumericPattern` | 字母数字 | `^[a-zA-Z0-9]+$` |
| `PostalCodePattern` | 中国邮政编码 | `^[1-9]\d{5}$` |
| `BankCardPattern` | 银行卡号 | `^\d{16,19}$` |
| `LicensePlatePattern` | 中国车牌号 | 含新能源车牌 |
| `DatePattern` | 日期（yyyy-MM-dd） | `^\d{4}-(0[1-9]|1[0-2])-(0[1-9]|[12]\d|3[01])$` |
| `TimePattern` | 时间（HH:mm:ss） | `^([01]\d|2[0-3]):[0-5]\d:[0-5]\d$` |
| `HexColorPattern` | 十六进制颜色 | `^#?([a-fA-F0-9]{6}|[a-fA-F0-9]{3})$` |
| `QQPattern` | QQ号 | `^[1-9]\d{4,10}$` |
| `WeChatPattern` | 微信号 | `^[a-zA-Z][a-zA-Z0-9_-]{5,19}$` |
| `UsernamePattern` | 用户名 | `^[a-zA-Z][a-zA-Z0-9_]{4,15}$` |
| `StrongPasswordPattern` | 强密码 | 至少8位，含大小写字母和数字 |

## 主要功能模块

### 1. 基础验证方法

提供常见数据格式的验证功能，确保输入数据符合预期格式。

**主要方法：**

#### IsMatch
验证字符串是否匹配指定正则表达式。
```csharp
public static bool IsMatch(string? input, string pattern, RegexOptions options = RegexOptions.None)
```
**参数：**
- `input`: 输入字符串
- `pattern`: 正则表达式模式
- `options`: 正则表达式选项

**返回值：**
- 是否匹配

**示例：**
```csharp
bool isValid = RegexHelper.IsMatch("test@example.com", RegexHelper.EmailPattern);
```

#### IsValidEmail
验证是否为有效的电子邮件地址。
```csharp
public static bool IsValidEmail(string? email)
```
**示例：**
```csharp
bool isValid = RegexHelper.IsValidEmail("test@example.com");
Console.WriteLine($"邮箱有效: {isValid}");
```

#### IsValidMobilePhone
验证是否为有效的手机号码（中国大陆）。
```csharp
public static bool IsValidMobilePhone(string? mobile)
```
**示例：**
```csharp
bool isValid = RegexHelper.IsValidMobilePhone("13812345678");
Console.WriteLine($"手机号有效: {isValid}");
```

#### IsValidIdCard
验证是否为有效的身份证号码（15位或18位），包含校验码验证。
```csharp
public static bool IsValidIdCard(string? idCard)
```
**示例：**
```csharp
bool isValid = RegexHelper.IsValidIdCard("110101199001011234");
Console.WriteLine($"身份证有效: {isValid}");
```

#### IsValidUrl / IsValidIPv4 / IsValidIPv6 / IsValidIP
验证 URL、IPv4、IPv6 或 IP 地址。
```csharp
public static bool IsValidUrl(string? url)
public static bool IsValidIPv4(string? ip)
public static bool IsValidIPv6(string? ip)
public static bool IsValidIP(string? ip)
```
**示例：**
```csharp
bool isValidUrl = RegexHelper.IsValidUrl("https://www.example.com");
bool isValidIPv4 = RegexHelper.IsValidIPv4("192.168.1.1");
```

#### IsValidPostalCode / IsValidBankCard / IsValidLicensePlate
验证邮政编码、银行卡号、车牌号。
```csharp
public static bool IsValidPostalCode(string? postalCode)
public static bool IsValidBankCard(string? bankCard)
public static bool IsValidLicensePlate(string? licensePlate)
```

#### IsValidQQ / IsValidWeChat / IsValidHexColor
验证 QQ号、微信号、十六进制颜色值。
```csharp
public static bool IsValidQQ(string? qq)
public static bool IsValidWeChat(string? wechat)
public static bool IsValidHexColor(string? color)
```

### 2. 格式验证方法

提供数据格式类型的验证功能。

**主要方法：**

#### IsInteger / IsPositiveInteger / IsFloat / IsNumber
验证整数、正整数、浮点数、数字。
```csharp
public static bool IsInteger(string? input)
public static bool IsPositiveInteger(string? input)
public static bool IsFloat(string? input)
public static bool IsNumber(string? input)
```
**示例：**
```csharp
bool isInt = RegexHelper.IsInteger("123");
bool isFloat = RegexHelper.IsFloat("123.45");
Console.WriteLine($"是整数: {isInt}");
Console.WriteLine($"是浮点数: {isFloat}");
```

#### IsChinese / IsLetter / IsAlphanumeric
验证中文、字母、字母数字。
```csharp
public static bool IsChinese(string? input)
public static bool IsLetter(string? input)
public static bool IsAlphanumeric(string? input)
```
**示例：**
```csharp
bool isChinese = RegexHelper.IsChinese("你好世界");
Console.WriteLine($"是中文: {isChinese}");
```

#### IsValidDate / IsValidTime / IsValidDateTime
验证日期、时间、日期时间格式。
```csharp
public static bool IsValidDate(string? input)
public static bool IsValidTime(string? input)
public static bool IsValidDateTime(string? input)
```

#### IsValidUsername / IsStrongPassword / GetPasswordStrength
验证用户名、密码强度。
```csharp
public static bool IsValidUsername(string? username)
public static bool IsStrongPassword(string? password)
public static int GetPasswordStrength(string? password)
```
**示例：**
```csharp
int strength = RegexHelper.GetPasswordStrength("Abc123!@#");
Console.WriteLine($"密码强度: {strength}");
```

#### IsLengthInRange / IsWhitespace
验证字符串长度范围、空白字符。
```csharp
public static bool IsLengthInRange(string? input, int minLength, int maxLength)
public static bool IsWhitespace(string? input)
```

#### ContainsChinese / ContainsDigit / ContainsLetter / ContainsSpecialChar
检查是否包含中文、数字、字母、特殊字符。
```csharp
public static bool ContainsChinese(string? input)
public static bool ContainsDigit(string? input)
public static bool ContainsLetter(string? input)
public static bool ContainsSpecialChar(string? input)
```

### 3. 匹配操作

提供文本中匹配特定模式内容的功能，返回匹配结果。

**主要方法：**

#### Match
获取第一个匹配结果。
```csharp
public static Match? Match(string? input, string pattern, RegexOptions options = RegexOptions.None)
```
**参数：**
- `input`: 输入字符串
- `pattern`: 正则表达式模式
- `options`: 正则表达式选项

**返回值：**
- 匹配结果，如果没有匹配则返回 null

**示例：**
```csharp
var match = RegexHelper.Match("Hello 123 World", @"\d+");
if (match != null)
{
    Console.WriteLine($"匹配到: {match.Value}");
}
```

#### Matches
获取所有匹配结果。
```csharp
public static MatchCollection Matches(string? input, string pattern, RegexOptions options = RegexOptions.None)
```
**示例：**
```csharp
var matches = RegexHelper.Matches("Hello 123 World 456", @"\d+");
foreach (Match match in matches)
{
    Console.WriteLine($"匹配到: {match.Value}");
}
```

#### GetMatchValues
获取所有匹配的字符串值。
```csharp
public static List<string> GetMatchValues(string? input, string pattern, RegexOptions options = RegexOptions.None)
```
**示例：**
```csharp
var values = RegexHelper.GetMatchValues("Hello 123 World 456", @"\d+");
// values = ["123", "456"]
```

#### GetGroupValue / GetGroupValues / GetAllGroupValues
获取匹配的分组值。
```csharp
public static string? GetGroupValue(string? input, string pattern, string groupName, RegexOptions options = RegexOptions.None)
public static List<string> GetGroupValues(string? input, string pattern, string groupName, RegexOptions options = RegexOptions.None)
public static Dictionary<string, string> GetAllGroupValues(string? input, string pattern, RegexOptions options = RegexOptions.None)
```
**示例：**
```csharp
var phone = "13812345678";
var pattern = @"(?<prefix>1[3-9])(?<number>\d{8})";
var prefix = RegexHelper.GetGroupValue(phone, pattern, "prefix");
Console.WriteLine($"前缀: {prefix}");
```

### 4. 替换操作

提供文本内容的替换功能，包括信息脱敏和格式化等。

**主要方法：**

#### Replace
替换所有匹配的字符串。
```csharp
public static string Replace(string? input, string pattern, string replacement, RegexOptions options = RegexOptions.None)
public static string Replace(string? input, string pattern, MatchEvaluator evaluator, RegexOptions options = RegexOptions.None)
```
**参数：**
- `input`: 输入字符串
- `pattern`: 正则表达式模式
- `replacement`: 替换字符串 或 回调函数

**返回值：**
- 替换后的字符串

**示例：**
```csharp
var result = RegexHelper.Replace("Hello 123 World", @"\d+", "XXX");
// result = "Hello XXX World"

var result2 = RegexHelper.Replace("Hello 123 World 456", @"\d+", match =>
{
    return $"[{match.Value}]";
});
// result2 = "Hello [123] World [456]"
```

#### Remove
移除所有匹配的字符串。
```csharp
public static string Remove(string? input, string pattern, RegexOptions options = RegexOptions.None)
```
**示例：**
```csharp
var result = RegexHelper.Remove("Hello 123 World 456", @"\d+");
// result = "Hello  World "
```

#### RemoveHtmlTags / RemoveWhitespace / CompressWhitespace
移除 HTML 标签、空白字符或压缩空白。
```csharp
public static string RemoveHtmlTags(string? input)
public static string RemoveWhitespace(string? input)
public static string CompressWhitespace(string? input)
```
**示例：**
```csharp
var result = RegexHelper.RemoveHtmlTags("<p>Hello</p>");
// result = "Hello"

var result2 = RegexHelper.CompressWhitespace("Hello    World");
// result2 = "Hello World"
```

#### ReplaceIfMatch
条件替换：仅当匹配时替换。
```csharp
public static string ReplaceIfMatch(string? input, string pattern, string replacement, RegexOptions options = RegexOptions.None)
```

### 5. 提取操作

提供从文本中提取特定信息的功能。

**主要方法：**

#### ExtractNumbers / ExtractIntegers / ExtractDoubles
提取数字、整数、浮点数。
```csharp
public static List<string> ExtractNumbers(string? input)
public static List<int> ExtractIntegers(string? input)
public static List<double> ExtractDoubles(string? input)
```
**示例：**
```csharp
var numbers = RegexHelper.ExtractNumbers("Price: $123.45, Tax: $67.89");
// numbers = ["123", "45", "67", "89"]

var integers = RegexHelper.ExtractIntegers("Price: $123, Tax: $67");
// integers = [123, 67]
```

#### ExtractEmails
提取所有邮箱地址。
```csharp
public static List<string> ExtractEmails(string? input)
```
**示例：**
```csharp
var emails = RegexHelper.ExtractEmails("Contact: test@example.com or support@example.org");
// emails = ["test@example.com", "support@example.org"]
```

#### ExtractMobilePhones
提取所有手机号码。
```csharp
public static List<string> ExtractMobilePhones(string? input)
```

#### ExtractUrls
提取所有 URL。
```csharp
public static List<string> ExtractUrls(string? input)
```

#### ExtractHtmlTags
提取所有 HTML 标签。
```csharp
public static List<string> ExtractHtmlTags(string? input)
```

#### ExtractIPs
提取所有 IP 地址。
```csharp
public static List<string> ExtractIPs(string? input)
```

#### ExtractHexColors
提取所有十六进制颜色值。
```csharp
public static List<string> ExtractHexColors(string? input)
```

### 6. 格式化操作

提供文本格式化功能，改善文本显示效果。

**主要方法：**

#### Mask
对字符串进行脱敏处理。
```csharp
public static string Mask(string? input, int start, int length, char maskChar = '*')
```
**参数：**
- `input`: 输入字符串
- `start`: 开始位置
- `length`: 脱敏长度
- `maskChar`: 脱敏字符

**示例：**
```csharp
var masked = RegexHelper.Mask("13812345678", 3, 4);
// masked = "138****5678"
```

#### MaskMobilePhone / MaskEmail / MaskIdCard / MaskBankCard
对手机号、邮箱、身份证、银行卡进行脱敏。
```csharp
public static string MaskMobilePhone(string? mobile)
public static string MaskEmail(string? email)
public static string MaskIdCard(string? idCard)
public static string MaskBankCard(string? bankCard)
```
**示例：**
```csharp
var maskedPhone = RegexHelper.MaskMobilePhone("13812345678");
// maskedPhone = "138****5678"

var maskedEmail = RegexHelper.MaskEmail("test@example.com");
// maskedEmail = "t***@example.com"

var maskedIdCard = RegexHelper.MaskIdCard("110101199001011234");
// maskedIdCard = "110101********1234"
```

#### FormatMobilePhone
格式化手机号（添加分隔符）。
```csharp
public static string FormatMobilePhone(string? mobile)
```
**示例：**
```csharp
var formatted = RegexHelper.FormatMobilePhone("13812345678");
// formatted = "138-1234-5678"
```

#### FormatBankCardNumber
格式化银行卡号（添加空格分隔）。
```csharp
public static string FormatBankCardNumber(string? bankCard)
```
**示例：**
```csharp
var formatted = RegexHelper.FormatBankCardNumber("6222021234567890123");
// formatted = "6222 0212 3456 7890 123"
```

#### CamelToUnderline / UnderlineToCamel
驼峰命名与下划线命名互转。
```csharp
public static string CamelToUnderline(string? input)
public static string UnderlineToCamel(string? input)
```
**示例：**
```csharp
var underline = RegexHelper.CamelToUnderline("userName");
// underline = "user_name"

var camel = RegexHelper.UnderlineToCamel("user_name");
// camel = "userName"
```

#### CapitalizeFirstLetter / CapitalizeWords
首字母大写、每个单词首字母大写。
```csharp
public static string CapitalizeFirstLetter(string? input)
public static string CapitalizeWords(string? input)
```

#### Split
使用正则表达式分割字符串。
```csharp
public static string[] Split(string? input, string pattern, RegexOptions options = RegexOptions.None)
```

## 使用场景

1. **表单数据验证** - 验证用户输入的邮箱、手机号、身份证等信息格式
2. **数据清洗处理** - 从非结构化的文本中提取有用信息
3. **敏感信息脱敏** - 对手机号、邮箱、身份证等进行脱敏处理
4. **文本格式化** - 统一文本格式，改善显示效果
5. **数据过滤** - 移除HTML标签、特殊字符等不需要的内容
6. **数据解析** - 从日志、网页文本中提取特定信息
7. **命名规范转换** - 对变量名进行格式转换
8. **网页内容处理** - 从页面源码中提取链接、价格等信息

## 注意事项

1. 所有方法对空值和空字符串进行了安全处理，不会抛出异常
2. 身份证验证包含中国标准校验码验证
3. 手机号验证仅支持中国大陆手机号
4. 当匹配不到结果时，返回空列表或 null
5. 提取方法在匹配失败时返回空字符串
6. 正则表达式模式为静态常量，不会影响性能
7. 验证 URL 方法为通用格式，无法验证链接是否真实存在
8. 身份证验证仅验证格式和校验码，不验证日期的真实有效性
9. 金额验证支持最多两位小数
