# DateTimeExtensions 类功能文档

## 概述

[DateTimeExtensions](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\DateTimeExtensions.cs#L8-L440) 是一个静态扩展类，为 `DateTime` 类型提供了丰富的扩展方法。该类包含时间判断、转换、计算、格式化等多种功能，旨在简化日期时间的处理操作，提高代码的可读性和便利性。

## 主要功能模块

### 1. 状态判断方法

提供 DateTime 状态检查的便捷方法。

**主要方法：**
- [IsDefault()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\DateTimeExtensions.cs#L13-L13) - 判断 DateTime 是否为默认值（未初始化）
- [IsMinValue()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\DateTimeExtensions.cs#L19-L19) - 判断 DateTime 是否为最小值
- [IsMaxValue()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\DateTimeExtensions.cs#L25-L25) - 判断 DateTime 是否为最大值
- [IsToday()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\DateTimeExtensions.cs#L31-L31) - 判断 DateTime 是否为今天
- [IsLeapYear()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\DateTimeExtensions.cs#L37-L37) - 判断 DateTime 是否为闰年
- [IsWeekend()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\DateTimeExtensions.cs#L43-L44) - 判断 DateTime 是否为周末（周六或周日）
- [IsWeekday()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\DateTimeExtensions.cs#L50-L51) - 判断 DateTime 是否为工作日（周一到周五）
- [IsAM()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\DateTimeExtensions.cs#L398-L398) - 判断 DateTime 是否为上午
- [IsPM()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\DateTimeExtensions.cs#L404-L404) - 判断 DateTime 是否为下午

### 2. 时间戳转换方法

提供 DateTime 与 Unix 时间戳之间的相互转换。

**主要方法：**
- [ToUnixTimestamp()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\DateTimeExtensions.cs#L60-L61) - DateTime 转为 Unix 时间戳（秒）
- [ToUnixTimestampMs()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\DateTimeExtensions.cs#L67-L68) - DateTime 转为 Unix 时间戳（毫秒）
- [FromUnixTimestamp()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\DateTimeExtensions.cs#L74-L75) - Unix 时间戳（秒）转为 DateTime
- [FromUnixTimestampMs()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\DateTimeExtensions.cs#L81-L82) - Unix 时间戳（毫秒）转为 DateTime

### 3. 字符串格式化方法

提供多种日期时间格式的字符串转换。

**主要方法：**
- [ToFormatString()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\DateTimeExtensions.cs#L89-L90) - DateTime 转为指定格式字符串
- [ToIso8601String()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\DateTimeExtensions.cs#L96-L97) - DateTime 转为 ISO8601 格式字符串
- [ToRfc1123String()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\DateTimeExtensions.cs#L103-L104) - DateTime 转为 RFC1123 格式字符串
- [ToChineseDateString()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\DateTimeExtensions.cs#L110-L111) - DateTime 转为中文日期字符串
- [ToChineseDateTimeString()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\DateTimeExtensions.cs#L117-L118) - DateTime 转为中文日期时间字符串
- [ToTimestampString()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\DateTimeExtensions.cs#L124-L125) - DateTime 转为时间戳字符串
- [ToDateString()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\DateTimeExtensions.cs#L131-L132) - DateTime 转为日期字符串
- [ToTimeString()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\DateTimeExtensions.cs#L138-L139) - DateTime 转为时间字符串
- [ToShortTimeString()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\DateTimeExtensions.cs#L145-L146) - DateTime 转为短时间字符串
- [ToChineseWeekday()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\DateTimeExtensions.cs#L152-L155) - DateTime 转为星期中文
- [ToEnglishWeekday()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\DateTimeExtensions.cs#L161-L162) - DateTime 转为英文星期
- [ToQuarter()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\DateTimeExtensions.cs#L168-L171) - DateTime 转为季度
- [ToChineseLunarDate()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\DateTimeExtensions.cs#L177-L188) - DateTime 转为农历日期字符串
- [ToCustomTimestamp()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\DateTimeExtensions.cs#L329-L330) - DateTime 转为时间戳（自定义格式，精度到毫秒）
- [ToFriendlyString()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\DateTimeExtensions.cs#L336-L345) - DateTime 转为友好时间描述
- [ToMinguoString()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\DateTimeExtensions.cs#L410-L414) - DateTime 转为民国纪年字符串

### 4. 时间计算方法

提供时间增量操作和时间差计算功能。

**主要方法：**
- [AddDaysSafe()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\DateTimeExtensions.cs#L194-L195) - DateTime 增加指定天数
- [AddHoursSafe()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\DateTimeExtensions.cs#L201-L202) - DateTime 增加指定小时数
- [AddMinutesSafe()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\DateTimeExtensions.cs#L208-L209) - DateTime 增加指定分钟数
- [AddSecondsSafe()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\DateTimeExtensions.cs#L215-L216) - DateTime 增加指定秒数
- [AddMonthsSafe()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\DateTimeExtensions.cs#L222-L223) - DateTime 增加指定月份数
- [AddYearsSafe()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\DateTimeExtensions.cs#L229-L230) - DateTime 增加指定年份数
- [DaysBetween()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\DateTimeExtensions.cs#L236-L237) - 获取两个 DateTime 之间的天数差
- [HoursBetween()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\DateTimeExtensions.cs#L243-L244) - 获取两个 DateTime 之间的小时差
- [MinutesBetween()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\DateTimeExtensions.cs#L250-L251) - 获取两个 DateTime 之间的分钟差
- [SecondsBetween()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\DateTimeExtensions.cs#L257-L258) - 获取两个 DateTime 之间的秒数差
- [SpanBetween()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\DateTimeExtensions.cs#L264-L265) - 获取两个 DateTime 之间的时间间隔（TimeSpan）

### 5. 时间比较方法

提供时间比较和范围判断功能。

**主要方法：**
- [IsBetween()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\DateTimeExtensions.cs#L272-L273) - 判断 DateTime 是否在指定范围内（包含边界）
- [IsBefore()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\DateTimeExtensions.cs#L279-L280) - 判断 DateTime 是否早于指定时间
- [IsAfter()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\DateTimeExtensions.cs#L286-L287) - 判断 DateTime 是否晚于指定时间

### 6. 时区转换方法

提供时区转换功能。

**主要方法：**
- [ToLocalTimeSafe()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\DateTimeExtensions.cs#L293-L294) - DateTime 转为本地时间
- [ToUtcTimeSafe()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\DateTimeExtensions.cs#L300-L301) - DateTime 转为 UTC 时间
- [ToTimeZone()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\DateTimeExtensions.cs#L307-L311) - DateTime 转为指定时区时间

### 7. 特殊计算方法

提供年龄计算和特殊日期系统转换功能。

**主要方法：**
- [ToAge()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\DateTimeExtensions.cs#L351-L358) - DateTime 转为 Age（年龄，按年份计算）
- [ToJulianDayNumber()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\DateTimeExtensions.cs#L420-L439) - DateTime 转为 JDN（儒略日号）

### 8. 数据结构转换方法

提供 DateTime 到其他数据结构的转换功能。

**主要方法：**
- [ToWeekdayNumber()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\DateTimeExtensions.cs#L364-L364) - DateTime 转为星期几的数字
- [ToYMD()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\DateTimeExtensions.cs#L370-L371) - DateTime 转为年月日元组
- [ToHMS()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\DateTimeExtensions.cs#L377-L378) - DateTime 转为时分秒元组
- [ToDateOnly()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\DateTimeExtensions.cs#L384-L384) - DateTime 转为 DateOnly（.NET 6+）
- [ToTimeOnly()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\DateTimeExtensions.cs#L390-L390) - DateTime 转为 TimeOnly（.NET 6+）

## 使用场景

1. **时间格式化** - 将 DateTime 转换为各种格式的字符串显示
2. **时间计算** - 计算时间差、增加时间等操作
3. **国际化显示** - 支持中文、英文等多种语言的时间显示
4. **时区处理** - 处理不同时区的时间转换
5. **用户界面** - 提供友好的时间显示（如"刚刚"、"5分钟前"）
6. **数据交换** - 时间戳转换，便于系统间数据传输
7. **报表生成** - 生成各种格式的日期时间报表
8. **业务逻辑** - 判断工作日、周末、闰年等业务相关时间属性

## 注意事项

1. 所有方法都是扩展方法，需要通过 `DateTime` 实例调用
2. 大部分方法都对边界条件进行了安全处理
3. 时间戳转换基于 DateTimeOffset 实现，处理时区更准确
4. 农历转换仅支持中国农历，需要引用 System.Globalization.ChineseLunisolarCalendar
5. 友好时间描述基于当前时间计算，适用于相对时间显示
6. 年龄计算考虑了月份和日期，计算更精确
7. 时区转换需要系统支持相应的时区标识
8. 部分方法（如 ToDateOnly、ToTimeOnly）需要 .NET 6+ 版本支持