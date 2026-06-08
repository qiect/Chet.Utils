---
title: "DateTimeExtensions"
description: "为 DateTime 类型提供了丰富的扩展方法，包括日期时间判断、转换、计算、格式化等功能，旨在简化日期时间的处理，提高代码的可读性和便捷性。"
targetType: "DateTime"
namespace: "Chet.Utils"
className: "DateTimeExtensions"
category: "Extensions"
order: 3
---

# DateTimeExtensions 扩展类文档

## 概述

[DateTimeExtensions](../../Chet.Utils/Extensions/DateTimeExtensions.cs) 是一个静态扩展类，为 `DateTime` 类型提供了丰富的扩展方法，包括日期时间判断、转换、计算、格式化等功能，旨在简化日期时间的处理，提高代码的可读性和便捷性。

## 主要功能模块

### 1. 基础判断方法

提供 DateTime 状态判断的便捷方法。

**主要方法：**
- `IsDefault()` - 判断 DateTime 是否为默认值（未初始化）
- `IsMinValue()` - 判断 DateTime 是否为最小值
- `IsMaxValue()` - 判断 DateTime 是否为最大值
- `IsToday()` - 判断 DateTime 是否为今天
- `IsYesterday()` - 判断 DateTime 是否为昨天
- `IsTomorrow()` - 判断 DateTime 是否为明天
- `IsNullOrDefault()` - 判断 DateTime 是否为 null 或默认值（针对 Nullable<DateTime>）

### 2. 日期属性判断方法

提供 DateTime 日期属性判断的便捷方法。

**主要方法：**
- `IsLeapYear()` - 判断 DateTime 是否为闰年
- `IsWeekend()` - 判断 DateTime 是否为周末（周六或周日）
- `IsWeekday()` - 判断 DateTime 是否为工作日（周一到周五）
- `IsAM()` - 判断 DateTime 是否为上午
- `IsPM()` - 判断 DateTime 是否为下午
- `IsFirstDayOfMonth()` - 判断 DateTime 是否为指定月份的第一天
- `IsLastDayOfMonth()` - 判断 DateTime 是否为指定月份的最后一天

### 3. 季度和周计算方法

提供季度和周相关的计算功能。

**主要方法：**
- `GetQuarter()` - 获取 DateTime 所在的季度（1-4）
- `ToQuarter()` - 获取 DateTime 所在季度（GetQuarter 的别名方法）
- `ToQuarterString()` - 获取 DateTime 所在季度的字符串表示（如 "Q1"）
- `GetWeekOfYear()` - 获取 DateTime 所在年份的第几周
- `GetWeekOfMonth()` - 获取 DateTime 所在月份的第几周
- `GetFirstDayOfQuarter()` - 获取 DateTime 所在季度的第一天
- `GetLastDayOfQuarter()` - 获取 DateTime 所在季度的最后一天

### 4. 时间转换方法

提供 DateTime 与 Unix 时间戳之间的相互转换。

**主要方法：**
- `ToUnixTimestamp()` - DateTime 转为 Unix 时间戳（秒）
- `ToUnixTimestampMs()` - DateTime 转为 Unix 时间戳（毫秒）
- `FromUnixTimestamp()` - Unix 时间戳（秒）转为 DateTime
- `FromUnixTimestampMs()` - Unix 时间戳（毫秒）转为 DateTime
- `ToLocalTimeSafe()` - DateTime 转为本地时间
- `ToUtcTimeSafe()` - DateTime 转为 UTC 时间
- `ToTimeZone(string timeZoneId)` - DateTime 转为指定时区时间
- `ToDateTimeOffset()` - DateTime 转为 DateTimeOffset

### 5. 格式化输出方法

提供日期时间到各种格式字符串的转换。

**主要方法：**
- `ToFormatString(string format)` - DateTime 转为指定格式字符串
- `ToIso8601String()` - DateTime 转为 ISO8601 格式字符串
- `ToRfc1123String()` - DateTime 转为 RFC1123 格式字符串
- `ToChineseDateString()` - DateTime 转为中文日期字符串
- `ToChineseDateTimeString()` - DateTime 转为中文日期时间字符串
- `ToTimestampString()` - DateTime 转为时间戳字符串
- `ToCustomTimestamp()` - DateTime 转为时间戳（自定义格式，精确到毫秒）
- `ToDateString()` - DateTime 转为日期字符串
- `ToTimeString()` - DateTime 转为时间字符串
- `ToShortTimeString()` - DateTime 转为短时间字符串
- `ToChineseWeekday()` - DateTime 转为星期中文名称
- `ToEnglishWeekday()` - DateTime 转为星期英文名称
- `ToWeekdayNumber()` - DateTime 转为星期数字

### 6. 日期计算方法

提供日期时间加减和时间差计算功能。

**主要方法：**
- `AddDaysSafe(double days)` - 安全增加指定天数
- `AddHoursSafe(double hours)` - 安全增加指定小时数
- `AddMinutesSafe(double minutes)` - 安全增加指定分钟数
- `AddSecondsSafe(double seconds)` - 安全增加指定秒数
- `AddMonthsSafe(int months)` - 安全增加指定月份数
- `AddYearsSafe(int years)` - 安全增加指定年份数
- `DaysBetween(DateTime other)` - 获取两个 DateTime 之间的天数差
- `HoursBetween(DateTime other)` - 获取两个 DateTime 之间的小时差
- `MinutesBetween(DateTime other)` - 获取两个 DateTime 之间的分钟差
- `SecondsBetween(DateTime other)` - 获取两个 DateTime 之间的秒数差
- `SpanBetween(DateTime other)` - 获取两个 DateTime 之间的时间差（TimeSpan）

### 7. 时间比较方法

提供时间比较和范围判断功能。

**主要方法：**
- `IsBetween(DateTime start, DateTime end)` - 判断 DateTime 是否在指定范围内（包含边界）
- `IsBefore(DateTime other)` - 判断 DateTime 是否早于指定时间
- `IsAfter(DateTime other)` - 判断 DateTime 是否晚于指定时间

### 8. 时间范围方法

提供获取时间范围的功能。

**主要方法：**
- `GetStartOfDay()` - 获取当天的开始时间（00:00:00）
- `GetEndOfDay()` - 获取当天的结束时间（23:59:59）
- `GetStartOfWeek()` - 获取当周的开始时间
- `GetEndOfWeek()` - 获取当周的结束时间
- `GetStartOfMonth()` - 获取当月的开始时间
- `GetEndOfMonth()` - 获取当月的结束时间
- `GetStartOfYear()` - 获取当年的开始时间
- `GetEndOfYear()` - 获取当年的结束时间

### 9. 年龄计算方法

提供年龄计算和日历系统转换功能。

**主要方法：**
- `GetAge(DateTime? referenceDate = null)` - 计算年龄（根据生日计算）
- `ToAge(DateTime? referenceDate = null)` - DateTime 转为 Age（年龄，根据生日计算）
- `ToJulianDayNumber()` - DateTime 转为 JDN（儒略日数）

### 10. 数据结构转换方法

提供 DateTime 与其他数据结构之间的转换功能。

**主要方法：**
- `ToYMD()` - DateTime 转为年月日元组
- `ToHMS()` - DateTime 转为时分秒元组
- `ToDateOnly()` - DateTime 转为 DateOnly（.NET 6+）
- `ToTimeOnly()` - DateTime 转为 TimeOnly（.NET 6+）

### 11. 特殊功能方法

提供农历转换、友好显示等特殊功能。

**主要方法：**
- `ToChineseLunarDate()` - DateTime 转为农历日期字符串
- `ToFriendlyString()` - DateTime 转为友好时间描述
- `ToMinguoString()` - DateTime 转为民国日期字符串

---

## 方法详细说明

### 基础判断

#### IsDefault
判断 DateTime 是否为默认值。

```csharp
public static bool IsDefault(this DateTime dt)
```

**参数：**
- `dt`: 待判断的 DateTime

**返回值：**
- 如果 DateTime 等于 default(DateTime)，返回 true；否则返回 false

**示例：**
```csharp
DateTime dt1 = default;
DateTime dt2 = DateTime.Now;

bool result1 = dt1.IsDefault(); // true
bool result2 = dt2.IsDefault(); // false
```

#### IsToday
判断 DateTime 是否为今天。

```csharp
public static bool IsToday(this DateTime dt)
```

**参数：**
- `dt`: 待判断的 DateTime

**返回值：**
- 如果 DateTime 的日期部分等于今天的日期，返回 true；否则返回 false

**示例：**
```csharp
DateTime dt = DateTime.Now;
bool result = dt.IsToday(); // true
```

### 日期属性判断

#### IsLeapYear
判断 DateTime 所在年份是否为闰年。

```csharp
public static bool IsLeapYear(this DateTime dt)
```

**参数：**
- `dt`: 待判断的 DateTime

**返回值：**
- 如果是闰年返回 true；否则返回 false

**示例：**
```csharp
DateTime dt = new DateTime(2024, 1, 1);
bool result = dt.IsLeapYear(); // true (2024年是闰年)
```

#### IsWeekend
判断 DateTime 是否为周末。

```csharp
public static bool IsWeekend(this DateTime dt)
```

**参数：**
- `dt`: 待判断的 DateTime

**返回值：**
- 如果是周六或周日返回 true；否则返回 false

**示例：**
```csharp
DateTime dt = new DateTime(2024, 1, 6); // 周六
bool result = dt.IsWeekend(); // true
```

### 季度和周计算

#### GetQuarter
获取 DateTime 所在的季度。

```csharp
public static int GetQuarter(this DateTime dt)
```

**参数：**
- `dt`: 待处理的 DateTime

**返回值：**
- 季度数字，范围 1-4

**示例：**
```csharp
DateTime dt = new DateTime(2024, 5, 15);
int quarter = dt.GetQuarter(); // 2
```

#### GetWeekOfYear
获取 DateTime 所在年份的第几周。

```csharp
public static int GetWeekOfYear(this DateTime dt, CalendarWeekRule rule = CalendarWeekRule.FirstDay)
```

**参数：**
- `dt`: 待处理的 DateTime
- `rule`: 确定日历周的规则，默认为当前文化的规则

**返回值：**
- 周数，范围 1-53

**示例：**
```csharp
DateTime dt = new DateTime(2024, 1, 15);
int week = dt.GetWeekOfYear(); // 返回该日期是当年的第几周
```

### 时间转换

#### ToUnixTimestamp
将 DateTime 转换为 Unix 时间戳（秒）。

```csharp
public static long ToUnixTimestamp(this DateTime dt)
```

**参数：**
- `dt`: 待转换的 DateTime

**返回值：**
- Unix 时间戳（自 1970-01-01 00:00:00 UTC 以来的秒数）

**示例：**
```csharp
DateTime dt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc);
long timestamp = dt.ToUnixTimestamp(); // 1704067200
```

#### FromUnixTimestamp
将 Unix 时间戳（秒）转换为 DateTime。

```csharp
public static DateTime FromUnixTimestamp(this long timestamp)
```

**参数：**
- `timestamp`: Unix 时间戳（秒）

**返回值：**
- 本地时间的 DateTime

**示例：**
```csharp
long timestamp = 1704067200;
DateTime dt = timestamp.FromUnixTimestamp();
```

#### ToTimeZone
将 DateTime 转换为指定时区的时间。

```csharp
public static DateTime ToTimeZone(this DateTime dt, string timeZoneId)
```

**参数：**
- `dt`: 待转换的 DateTime
- `timeZoneId`: 时区标识，如 "China Standard Time"、"Pacific Standard Time"

**返回值：**
- 指定时区的 DateTime

**异常：**
- `TimeZoneNotFoundException`: 时区标识无效时抛出

**示例：**
```csharp
DateTime dt = DateTime.UtcNow;
DateTime chinaTime = dt.ToTimeZone("China Standard Time");
```

### 格式化输出

#### ToFormatString
将 DateTime 格式化为指定格式的字符串。

```csharp
public static string ToFormatString(this DateTime dt, string format = "yyyy-MM-dd HH:mm:ss")
```

**参数：**
- `dt`: 待格式化的 DateTime
- `format`: 格式字符串，默认为 "yyyy-MM-dd HH:mm:ss"

**返回值：**
- 格式化后的日期时间字符串

**示例：**
```csharp
DateTime dt = DateTime.Now;
string result = dt.ToFormatString("yyyy年MM月dd日 HH:mm:ss");
```

#### ToChineseDateString
将 DateTime 转换为中文日期字符串。

```csharp
public static string ToChineseDateString(this DateTime dt)
```

**参数：**
- `dt`: 待格式化的 DateTime

**返回值：**
- 中文日期字符串（如 "2024年01月15日"）

**示例：**
```csharp
DateTime dt = DateTime.Now;
string result = dt.ToChineseDateString(); // "2024年01月15日"
```

#### ToChineseWeekday
将 DateTime 转换为星期中文名称。

```csharp
public static string ToChineseWeekday(this DateTime dt)
```

**参数：**
- `dt`: 待格式化的 DateTime

**返回值：**
- 星期中文名称（如 "星期一"）

**示例：**
```csharp
DateTime dt = DateTime.Now;
string result = dt.ToChineseWeekday(); // "星期一"
```

### 日期计算

#### DaysBetween
获取两个 DateTime 之间的天数差。

```csharp
public static int DaysBetween(this DateTime dt, DateTime other)
```

**参数：**
- `dt`: 第一个 DateTime
- `other`: 第二个 DateTime

**返回值：**
- 天数差（绝对值）

**示例：**
```csharp
DateTime dt1 = new DateTime(2024, 1, 1);
DateTime dt2 = new DateTime(2024, 1, 10);

int days = dt1.DaysBetween(dt2); // 9
```

### 时间范围

#### GetStartOfDay
获取当天的开始时间。

```csharp
public static DateTime GetStartOfDay(this DateTime dt)
```

**参数：**
- `dt`: 待处理的 DateTime

**返回值：**
- 当天的开始时间（00:00:00）

**示例：**
```csharp
DateTime dt = DateTime.Now;
DateTime start = dt.GetStartOfDay(); // 2024-01-15 00:00:00
```

#### GetEndOfMonth
获取当月的结束时间。

```csharp
public static DateTime GetEndOfMonth(this DateTime dt)
```

**参数：**
- `dt`: 待处理的 DateTime

**返回值：**
- 当月的结束时间（最后一秒）

**示例：**
```csharp
DateTime dt = new DateTime(2024, 1, 15);
DateTime end = dt.GetEndOfMonth(); // 2024-01-31 23:59:59
```

### 年龄计算

#### GetAge
计算年龄。

```csharp
public static int GetAge(this DateTime birthDate, DateTime? referenceDate = null)
```

**参数：**
- `birthDate`: 出生日期
- `referenceDate`: 参考日期，默认为当前日期

**返回值：**
- 年龄

**示例：**
```csharp
DateTime birthDate = new DateTime(1990, 5, 15);
int age = birthDate.GetAge(); // 根据当前日期计算年龄
```

### 特殊功能

#### ToFriendlyString
将 DateTime 转换为友好时间描述。

```csharp
public static string ToFriendlyString(this DateTime dt)
```

**参数：**
- `dt`: 待格式化的 DateTime

**返回值：**
- 友好时间描述（如 "刚刚"、"5分钟前"、"昨天"）

**示例：**
```csharp
DateTime dt = DateTime.Now.AddMinutes(-5);
string result = dt.ToFriendlyString(); // "5分钟前"
```

#### ToChineseLunarDate
将 DateTime 转换为农历日期字符串。

```csharp
public static string ToChineseLunarDate(this DateTime dt)
```

**参数：**
- `dt`: 待格式化的 DateTime

**返回值：**
- 农历日期字符串（如 "甲辰年腊月初五"）

**示例：**
```csharp
DateTime dt = new DateTime(2024, 1, 15);
string result = dt.ToChineseLunarDate(); // 农历日期
```

---

## 使用场景

1. **时间格式化** - 将 DateTime 转换为各种指定格式的字符串显示
2. **时间计算** - 计算时间差、增加时间等操作
3. **国际化显示** - 支持中文、英文等多种语言的时间表示
4. **时区转换** - 处理不同时区之间的时间转换
5. **用户友好显示** - 提供友好的时间显示（如"刚刚"、"5分钟前"）
6. **数据解析** - 时间戳转换到系统日期数据
7. **报表生成** - 生成指定格式的时间报告
8. **业务逻辑** - 判断工作日、周末、节假日等业务时间逻辑

---

## 注意事项

1. 所有方法都是扩展方法，需要通过 `DateTime` 实例调用
2. 大部分方法对边界进行了安全处理，不会抛出异常
3. 时间戳转换使用 DateTimeOffset 实现，确保时区准确
4. 农历转换支持中国农历，需要引用 System.Globalization.ChineseLunisolarCalendar
5. 友好时间描述基于当前时间计算，不适合处理过去很久的时间
6. 年龄计算考虑了月份和日期，结果准确
7. 时区转换需要系统支持相应时区标识
8. 部分方法如 ToDateOnly、ToTimeOnly 需要 .NET 6+ 版本支持
