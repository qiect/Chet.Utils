# Chet.Utils

## 概述

Chet.Utils 是一个为 .NET 开发提供全面辅助工具的类库。该项目旨在简化代码编写过程中的常见操作，提高开发效率，减少重复代码。所有扩展方法和帮助类都经过精心设计，遵循 .NET 最佳实践，提供完整的 XML 文档注释。

## 特性

- 🚀 **高性能** - 经过优化的实现，减少不必要的内存分配
- 🔒 **安全可靠** - 完善的空值检查和异常处理
- 📖 **完整文档** - 所有方法都有详细的 XML 注释和使用示例
- 🧪 **单元测试** - 核心功能都有对应的单元测试覆盖
- 🎯 **易于使用** - 直观的 API 设计，符合 .NET 命名规范

## 主要功能

### 扩展方法

| 扩展类                      | 说明                                              | 详细文档                                       |
| ------------------------ | ----------------------------------------------- | ------------------------------------------ |
| **BoolExtensions**       | Bool 扩展方法，提供基础判断、逻辑运算、类型转换、条件执行等功能              | [查看详情](Extensions/BoolExtensions.md)       |
| **DataTableExtensions**  | DataTable 扩展方法，提供数据转换、查询、操作、序列化等功能              | [查看详情](Extensions/DataTableExtensions.md)  |
| **DateTimeExtensions**   | DateTime 扩展方法，提供日期判断、格式化、计算、转换等功能               | [查看详情](Extensions/DateTimeExtensions.md)   |
| **DecimalExtensions**    | decimal 扩展方法，提供数值判断、数学运算、格式化、类型转换等功能            | [查看详情](Extensions/DecimalExtensions.md)    |
| **DoubleExtensions**     | double 扩展方法，提供数值判断、数学运算、格式化、类型转换等功能             | [查看详情](Extensions/DoubleExtensions.md)     |
| **EnumExtensions**       | Enum 扩展方法，提供枚举判断、转换、描述获取、标志操作等功能                | [查看详情](Extensions/EnumExtensions.md)       |
| **EnumerableExtensions** | IEnumerable/ICollection 扩展方法，提供集合判断、转换、分页、统计等功能 | [查看详情](Extensions/EnumerableExtensions.md) |
| **FileExtensions**       | 文件扩展方法，提供文件读写、判断、信息获取、哈希计算等功能                   | [查看详情](Extensions/FileExtensions.md)       |
| **FloatExtensions**      | float 扩展方法，提供数值判断、数学运算、格式化、类型转换等功能              | [查看详情](Extensions/FloatExtensions.md)      |
| **IntExtensions**        | int 扩展方法，提供数值判断、数学运算、格式化、中文转换等功能                | [查看详情](Extensions/IntExtensions.md)        |
| **StreamExtensions**     | Stream 扩展方法，提供流读写、转换、压缩、哈希计算等功能                 | [查看详情](Extensions/StreamExtensions.md)     |
| **StringExtensions**     | string 扩展方法，提供空值判断、正则验证、类型转换、编码解码等功能            | [查看详情](Extensions/StringExtensions.md)     |

### 帮助类

| 帮助类                   | 说明                                       | 详细文档                                 |
| --------------------- | ---------------------------------------- | ------------------------------------ |
| **ApplicationHelper** | 应用程序帮助类，提供进程管理、内存优化、系统信息获取等功能            | [查看详情](Helpers/ApplicationHelper.md) |
| **DataTableHelper**   | 数据表帮助类，提供数据透视、数据分析、数据清洗等高级功能             | [查看详情](Helpers/DataTableHelper.md)   |
| **FileHelper**        | 文件帮助类，提供文件监控、临时文件管理、文件比较等高级功能            | [查看详情](Helpers/FileHelper.md)        |
| **HttpClientHelper**  | HTTP 客户端帮助类，提供重试机制、超时控制、认证管理等功能          | [查看详情](Helpers/HttpClientHelper.md)  |
| **ReflectHelper**     | 反射帮助类，提供类型检查、属性操作、方法调用、表达式树等功能           | [查看详情](Helpers/ReflectHelper.md)     |
| **RegexHelper**       | 正则表达式帮助类，提供常用验证、匹配、替换、提取等功能              | [查看详情](Helpers/RegexHelper.md)       |
| **StopwatchHelper**   | 计时帮助类，提供高精度计时、性能统计、基准测试等功能               | [查看详情](Helpers/StopwatchHelper.md)   |
| **TaskHelper**        | 任务帮助类，提供超时控制、重试机制、并行处理、任务调度等功能           | [查看详情](Helpers/TaskHelper.md)        |
| **UnitHelper**        | 单位帮助类，提供长度、货币、质量、角度等多种单位转换功能             | [查看详情](Helpers/UnitHelper.md)        |
| **WebSocketHelper**   | WebSocket 帮助类，提供客户端/服务端连接管理、消息收发、心跳检测等功能 | [查看详情](Helpers/WebSocketHelper.md)   |

## 安装

### NuGet 包管理器

```powershell
Install-Package Chet.Utils
```

### .NET CLI

```bash
dotnet add package Chet.Utils
```

### PackageReference

```xml
<PackageReference Include="Chet.Utils" Version="1.5.0" />
```

## 快速开始

### 使用扩展方法

```csharp
using Chet.Utils;

// 字符串扩展
string email = "test@example.com";
bool isValid = email.IsEmail(); // true

// 日期扩展
DateTime date = DateTime.Now;
bool isWeekend = date.IsWeekend(); // 判断是否周末
string formatted = date.ToChineseDateString(); // "二〇二四年一月一日"

// 集合扩展
var list = new List<int> { 1, 2, 3, 4, 5 };
var page = list.GetPage(1, 2); // 分页获取数据
bool isEmpty = list.IsNullOrEmpty(); // 判断是否为空

// 数值扩展
int value = 12345;
string thousands = value.ToThousandsString(); // "12,345"
string chinese = value.ToChineseNumber(); // "一万二千三百四十五"
```

### 使用帮助类

```csharp
using Chet.Utils.Helpers;

// HTTP 客户端
var httpHelper = new HttpClientHelper();
var response = await httpHelper.GetAsync<string>("https://api.example.com/data");

// 正则表达式
bool isPhone = RegexHelper.IsMobilePhone("13800138000");
var emails = RegexHelper.ExtractEmails("联系邮箱: test@example.com");

// 计时器
var elapsed = StopwatchHelper.Measure(() => 
{
    // 要测试的代码
});
Console.WriteLine($"执行时间: {elapsed.TotalMilliseconds}ms");

// WebSocket 客户端
var wsClient = new WebSocketHelper();
wsClient.OnMessageReceived += (sender, e) => Console.WriteLine(e.Message);
await wsClient.ConnectAsync(new Uri("ws://localhost:8080"));
await wsClient.SendMessageAsync("Hello, World!");
```

## 系统要求

- .NET 8.0 或更高版本
- .NET Standard 2.0（部分功能）

## 版本更新

### v1.5.0 (当前版本)

**新功能**

重构所有扩展类和帮助类，使其更通用、更易用

### v1.4.0

**新增功能**

- ✨ 添加 WebSocket 帮助类，包含客户端和服务端实现
  - 支持连接管理、消息收发、心跳检测、自动重连
  - 支持消息队列、广播、组播等功能
- ✨ StringExtensions 类扩展了部分方法

**改进**

- 📝 完善所有扩展类和帮助类的 XML 文档注释
- 📝 更新所有功能文档，添加详细的使用示例
- 🔧 优化代码结构，提高代码可读性

### v1.3.0

**新增功能**

- ✨ 添加 ApplicationHelper 应用程序帮助类
- ✨ 添加 ReflectHelper 反射帮助类
- ✨ 添加 StopwatchHelper 计时帮助类
- ✨ 添加 TaskHelper 任务帮助类
- ✨ 添加 UnitHelper 单位帮助类

### v1.2.0

**新增功能**

- ✨ 添加 DataTableHelper 数据表帮助类
- ✨ 添加 FileHelper 文件帮助类
- ✨ 添加 RegexHelper 正则表达式帮助类

### v1.1.0

**新增功能**

- ✨ 添加 HttpClientHelper HTTP 客户端帮助类
- ✨ 完善扩展方法类

### v1.0.0

**初始版本**

- 🎉 基础扩展方法类（Bool、DateTime、Decimal、Double、Float、Int、String、Enumerable）
- 🎉 基础帮助类

## 贡献指南

欢迎提交 Issue 和 Pull Request 来帮助改进这个项目。

## 许可证

本项目采用 MIT 许可证。详见 [LICENSE](../LICENSE) 文件。

## 联系方式

如有问题或建议，请通过以下方式联系：

- 提交 [GitHub Issue](https://github.com/qiect/Chet.Utils/issues)
- 发送邮件至：<support@example.com>

