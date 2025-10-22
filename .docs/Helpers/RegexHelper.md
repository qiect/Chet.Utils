# RegexHelper 类功能文档

## 概述

[RegexHelper](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\RegexHelper.cs#L10-L654) 是一个静态工具类，专门用于处理正则表达式相关的操作。该类提供了丰富的功能，包括数据验证、内容匹配、文本替换、信息提取和格式化等，旨在简化开发中常见的正则表达式应用场景。

## 主要功能模块

### 1. 验证相关

提供各种常见数据格式的验证功能，确保输入数据符合预期格式。

**主要方法：**
- [IsValidEmail()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\RegexHelper.cs#L18-L23) - 验证电子邮件地址格式
- [IsValidPhoneNumber()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\RegexHelper.cs#L30-L35) - 验证中国手机号码格式
- [IsValidTelephoneNumber()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\RegexHelper.cs#L42-L47) - 验证中国固定电话号码格式
- [IsValidIdCard()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\RegexHelper.cs#L54-L59) - 验证中国18位身份证号码格式
- [IsValidZipCode()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\RegexHelper.cs#L66-L71) - 验证中国邮政编码格式
- [IsValidIPv4()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\RegexHelper.cs#L78-L83) - 验证IPv4地址格式
- [IsValidUrl()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\RegexHelper.cs#L90-L95) - 验证URL地址格式
- [IsValidDate()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\RegexHelper.cs#L102-L107) - 验证日期格式（yyyy-MM-dd）
- [IsValidAmount()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\RegexHelper.cs#L114-L119) - 验证金额格式
- [IsValidQQ()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\RegexHelper.cs#L126-L131) - 验证QQ号码格式

### 2. 匹配相关

提供从文本中匹配特定模式内容的功能，返回匹配结果集合。

**主要方法：**
- [MatchNumbers()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\RegexHelper.cs#L142-L148) - 匹配所有数字
- [MatchEnglishWords()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\RegexHelper.cs#L155-L161) - 匹配所有英文单词
- [MatchChineseCharacters()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\RegexHelper.cs#L168-L174) - 匹配所有中文字符
- [MatchEmails()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\RegexHelper.cs#L181-L187) - 匹配所有邮箱地址
- [MatchPhoneNumbers()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\RegexHelper.cs#L194-L200) - 匹配所有手机号码
- [MatchUrls()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\RegexHelper.cs#L207-L213) - 匹配所有URL链接
- [MatchHtmlTags()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\RegexHelper.cs#L220-L226) - 匹配HTML标签
- [MatchIPs()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\RegexHelper.cs#L233-L239) - 匹配所有IP地址
- [MatchHexColors()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\RegexHelper.cs#L246-L252) - 匹配所有十六进制颜色代码
- [MatchUsernames()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\RegexHelper.cs#L259-L265) - 匹配所有用户名（@开头）

### 3. 替换相关

提供文本内容替换功能，包括敏感信息隐藏和格式清理等。

**主要方法：**
- [HidePhoneNumber()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\RegexHelper.cs#L275-L280) - 隐藏手机号码中间4位
- [HideEmail()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\RegexHelper.cs#L287-L292) - 隐藏邮箱地址用户名部分
- [HideIdCard()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\RegexHelper.cs#L299-L304) - 隐藏身份证号码中间部分
- [RemoveHtmlTags()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\RegexHelper.cs#L311-L316) - 移除HTML标签
- [RemoveNumbers()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\RegexHelper.cs#L323-L328) - 移除所有数字
- [RemoveEnglish()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\RegexHelper.cs#L335-L340) - 移除所有英文字符
- [RemoveChinese()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\RegexHelper.cs#L347-L352) - 移除所有中文字符
- [ReplaceMultipleSpaces()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\RegexHelper.cs#L359-L364) - 将多个连续空格替换为单个空格
- [NormalizeLineEndings()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\RegexHelper.cs#L372-L377) - 统一换行符格式
- [RemoveSpecialCharacters()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\RegexHelper.cs#L385-L390) - 移除所有特殊字符

### 4. 提取相关

提供从文本中提取特定信息的功能。

**主要方法：**
- [ExtractFirstNumber()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\RegexHelper.cs#L400-L405) - 提取第一个数字
- [ExtractFirstEmail()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\RegexHelper.cs#L412-L417) - 提取第一个邮箱地址
- [ExtractFirstUrl()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\RegexHelper.cs#L424-L429) - 提取第一个URL
- [ExtractHtmlContent()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\RegexHelper.cs#L436-L441) - 提取HTML标签中的内容
- [ExtractLinkTexts()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\RegexHelper.cs#L448-L454) - 提取所有链接文本
- [ExtractPrice()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\RegexHelper.cs#L461-L466) - 提取价格数字
- [ExtractFileExtension()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\RegexHelper.cs#L473-L478) - 提取文件扩展名
- [ExtractDomain()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\RegexHelper.cs#L485-L490) - 提取域名
- [ExtractQueryParam()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\RegexHelper.cs#L498-L503) - 提取URL查询参数
- [ExtractVersion()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\RegexHelper.cs#L510-L515) - 提取版本号

### 5. 格式化相关

提供文本格式化功能，改善文本的显示效果。

**主要方法：**
- [FormatPhoneNumber()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\RegexHelper.cs#L526-L531) - 格式化手机号码（添加分隔符）
- [FormatBankCardNumber()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\RegexHelper.cs#L538-L543) - 格式化银行卡号（添加空格分隔符）
- [FormatAmount()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\RegexHelper.cs#L550-L555) - 格式化金额（添加千位分隔符）
- [CamelToUnderline()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\RegexHelper.cs#L562-L567) - 驼峰命名转下划线命名
- [UnderlineToCamel()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\RegexHelper.cs#L574-L579) - 下划线命名转驼峰命名
- [CapitalizeFirstLetter()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\RegexHelper.cs#L586-L591) - 首字母大写
- [CapitalizeWords()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\RegexHelper.cs#L598-L603) - 单词首字母大写
- [FormatDate()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\RegexHelper.cs#L610-L615) - 格式化日期（yyyyMMdd转yyyy-MM-dd）
- [FormatTime()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\RegexHelper.cs#L622-L627) - 格式化时间（HHmmss转HH:mm:ss）
- [CleanString()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\RegexHelper.cs#L634-L642) - 清理字符串（移除多余空格和换行符）

## 使用场景

1. **表单数据验证** - 验证用户输入的邮箱、手机号、身份证等信息格式
2. **数据清洗处理** - 从非结构化文本中提取有用信息
3. **敏感信息保护** - 隐藏手机号、邮箱、身份证等敏感信息
4. **文本格式化** - 统一文本格式，改善显示效果
5. **内容过滤** - 移除HTML标签、特殊字符等不需要的内容
6. **数据解析** - 从日志、网页等文本中提取特定信息
7. **代码规范检查** - 检查命名规范，进行格式转换
8. **爬虫数据处理** - 从网页内容中提取链接、价格等信息

## 注意事项

1. 所有方法都对空值和空字符串进行了处理，避免抛出异常
2. 部分验证规则针对中国标准设计，如手机号、身份证、邮政编码等
3. 返回集合的方法在无匹配内容时返回空列表而非null
4. 提取方法在无匹配内容时返回空字符串
5. 部分正则表达式较为复杂，可能影响性能，建议在性能敏感场景下进行测试
6. 邮箱和URL验证规则为通用格式，可能无法覆盖所有特殊情况
7. 日期验证仅检查格式，不验证日期的实际有效性
8. 金额验证支持最多两位小数