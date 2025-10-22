# StringExtensions 类功能文档

## 概述

[StringExtensions](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\StringExtensions.cs#L8-L504) 是一个静态扩展类，为 `string` 类型提供了丰富的扩展方法。该类包含字符串判断、正则表达式验证、类型转换、字符串操作等多种功能，旨在简化字符串处理操作，提高代码的安全性和可读性，特别适用于需要处理各种字符串类型的日常开发场景。

## 主要功能模块

### 1. 字符串判断方法

提供字符串状态检查和内容验证的便捷方法。

**主要方法：**
- [IsNullOrEmpty()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\StringExtensions.cs#L15-L15) - 判断字符串是否为 null 或空字符串
- [IsNullOrWhiteSpace()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\StringExtensions.cs#L21-L21) - 判断字符串是否为 null 或仅包含空白字符
- [IsNumeric()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\StringExtensions.cs#L27-L28) - 判断字符串是否为数字（可解析为 double）
- [IsInt()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\StringExtensions.cs#L34-L35) - 判断字符串是否为整数
- [IsFloat()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\StringExtensions.cs#L41-L42) - 判断字符串是否为浮点数（float）
- [IsDecimal()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\StringExtensions.cs#L48-L49) - 判断字符串是否为十进制数（decimal）
- [IsGuid()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\StringExtensions.cs#L55-L56) - 判断字符串是否为 Guid
- [EqualsIgnoreCase()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\StringExtensions.cs#L62-L63) - 忽略大小写判断字符串是否相等
- [IsChinese()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\StringExtensions.cs#L69-L72) - 判断字符是否为中文字符
- [HasChinese()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\StringExtensions.cs#L78-L81) - 判断字符串中是否包含中文
- [IsNull()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\StringExtensions.cs#L87-L104) - 判断字符串是否为空（支持自定义空字符串）

### 2. 正则表达式验证方法

提供基于正则表达式的字符串格式验证功能。

**主要方法：**
- [IsLetterByRegex()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\StringExtensions.cs#L113-L116) - 验证字符是否是字母类型
- [IsNumByRegex()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\StringExtensions.cs#L121-L124) - 验证字符是否是数字类型
- [ExtractNumByRegex()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\StringExtensions.cs#L129-L132) - 提取字符串中的数字部分
- [IsFloatByRegex()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\StringExtensions.cs#L137-L140) - 验证字符是不是浮点类型
- [IsEmailByRegex()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\StringExtensions.cs#L145-L148) - 验证字符是否是Email格式
- [IsTelByRegex()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\StringExtensions.cs#L153-L156) - 验证字符是否是Tel格式
- [IsMobileByRegex()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\StringExtensions.cs#L161-L164) - 验证是否是手机号码
- [IsUrlByRegex()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\StringExtensions.cs#L169-L172) - 验证是否是网址
- [IsDateByRegex()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\StringExtensions.cs#L177-L180) - 验证字符串是否是日期类型
- [IsTimeByRegex()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\StringExtensions.cs#L185-L188) - 验证字符串是否是时间类型
- [IsDateTimeByRegex()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\StringExtensions.cs#L193-L196) - 验证字符串是否是日期时间类型

### 3. 类型转换方法

提供字符串到各种数据类型的转换功能。

**主要方法：**
- [ToInt()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\StringExtensions.cs#L205-L206) - 字符串转 int，失败返回默认值
- [ToFloat()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\StringExtensions.cs#L212-L213) - 字符串转 float，失败返回默认值
- [ToDouble()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\StringExtensions.cs#L219-L220) - 字符串转 double，失败返回默认值
- [ToDecimal()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\StringExtensions.cs#L323-L324) - 字符串转 decimal，失败返回默认值
- [ToBool()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\StringExtensions.cs#L330-L331) - 字符串转 bool，失败返回默认值
- [ToGuid()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\StringExtensions.cs#L337-L338) - 字符串转 Guid，失败返回默认值
- [ToDateTime()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\StringExtensions.cs#L344-L345) - 字符串转 DateTime，失败返回默认值

#### 数值修约转换方法
- [ToDoubleRound()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\StringExtensions.cs#L226-L231) - 字符串转 double，保留指定小数位，四舍五入
- [ToDoubleTruncate()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\StringExtensions.cs#L237-L245) - 字符串转 double，保留指定小数位，向零取整
- [ToFloatRound()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\StringExtensions.cs#L251-L256) - 字符串转 float，保留指定小数位，四舍五入
- [ToFloatTruncate()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\StringExtensions.cs#L262-L270) - 字符串转 float，保留指定小数位，向零取整
- [KeepDecimal()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\StringExtensions.cs#L276-L281) - 保留数值型字符串的小数位

### 4. 字符串操作方法

提供字符串处理和转换功能。

**主要方法：**
- [TrimSafe()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\StringExtensions.cs#L354-L354) - 安全去除字符串首尾空白字符
- [RemoveWhiteSpace()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\StringExtensions.cs#L360-L361) - 移除字符串中的所有空白字符
- [SubstringSafe()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\StringExtensions.cs#L367-L373) - 安全截取字符串
- [Left()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\StringExtensions.cs#L379-L383) - 获取字符串左侧指定长度的子串
- [Right()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\StringExtensions.cs#L389-L393) - 获取字符串右侧指定长度的子串
- [Reverse()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\StringExtensions.cs#L399-L405) - 反转字符串
- [RemoveSpecialChars()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\StringExtensions.cs#L411-L414) - 移除字符串中的特殊字符
- [ToCamelCase()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\StringExtensions.cs#L420-L424) - 转为 camelCase（首字母小写）
- [ToPascalCase()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\StringExtensions.cs#L430-L434) - 转为 PascalCase（首字母大写）
- [Repeat()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\StringExtensions.cs#L440-L444) - 重复字符串指定次数
- [ReplaceIgnoreCase()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\StringExtensions.cs#L450-L454) - 忽略大小写替换字符串中的指定内容
- [ContainsIgnoreCase()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\StringExtensions.cs#L467-L471) - 判断字符串是否包含指定子串，忽略大小写
- [SplitSafe()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\StringExtensions.cs#L477-L481) - 将字符串按指定分隔符分割为字符串数组

### 5. 哈希计算方法

提供字符串哈希值计算功能。

**主要方法：**
- [ToMd5()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\StringExtensions.cs#L460-L465) - 获取字符串的 MD5 值（32位小写）

## 使用场景

1. **数据验证** - 验证用户输入的格式是否正确（邮箱、手机号、网址等）
2. **类型转换** - 安全地将字符串转换为各种数据类型
3. **字符串处理** - 字符串截取、拼接、格式化等操作
4. **数据清洗** - 移除特殊字符、空白字符等数据预处理
5. **格式验证** - 验证数据是否符合特定格式要求
6. **文本分析** - 判断文本内容类型（中文、数字、字母等）
7. **安全处理** - 避免 null 异常的安全字符串操作
8. **数据转换** - 字符串与其他数据类型之间的相互转换

## 注意事项

1. 所有方法都是扩展方法，需要通过 `string` 实例调用
2. 带有 "Safe" 后缀的方法都对 null 值进行了安全处理，避免抛出异常
3. 类型转换方法在转换失败时返回指定的默认值，而不是抛出异常
4. 正则表达式验证方法使用预定义的正则模式进行格式验证
5. 数值修约方法支持四舍五入和向零取整两种策略
6. 字符串操作方法支持边界检查，避免索引越界异常
7. 哈希计算方法使用 UTF-8 编码进行字节转换
8. 中文判断方法基于 Unicode 字符范围进行识别
9. 自定义空字符串判断支持多种空值定义
10. 所有方法都对输入参数进行了有效性检查