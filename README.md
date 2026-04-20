# Chet.Utils

Chet.Utils 是一个轻量且实用的 C# 工具类库，旨在为日常 .NET 开发提供常用的辅助函数与扩展方法，帮助减少样板代码、提高开发效率。本仓库包含字符串处理、日期时间工具、JSON 序列化、文件与 IO、加密辅助、HTTP 帮助函数、验证与随机工具等一系列实用模块。

简介
- 仓库描述：a c# utility library. C#工具包。
- 语言：C#
- 目标：提供小而可靠、易于测试的工具函数集合，便于在各类 .NET 应用（Web API、桌面应用、CLI 工具）中复用。

主要功能（示例）
- 字符串与文本处理（截断、格式化、空检查、去重空格）
- JSON 序列化/反序列化与安全解析
- 日期/时间辅助（友好显示、时区转换、范围检查）
- 文件/目录操作与临时文件助手
- 常用加密/哈希封装（MD5、SHA、Base64）
- HTTP 客户端包装与简化调用（基于 HttpClient 的便利方法）
- 验证工具（Email、Phone、IP、正则校验）
- 随机数与 ID 生成（GUID、短 ID、随机密码）
- 常用扩展方法（字符串、集合、日期等）
- 异常与错误处理/帮助信息生成

先决条件
- .NET 6/7/8 SDK（建议与项目当前目标框架一致）
- 支持 Windows / Linux / macOS（基于 .NET）

快速开始

- NuGet 安装（已发布）：

```bash
dotnet add package Chet.Utils
```

- 安装后在项目中使用：

```csharp
using Chet.Utils;
// 引入你需要的子命名空间，例如：
using Chet.Utils.Text;
using Chet.Utils.Json;
```

示例（常见用法）
```csharp
// 字符串工具
string s = "  hello world  ";
string trimmed = StringUtil.TrimToNull(s); // "hello world"

// JSON 工具
var obj = new { Name = "qiect", Age = 24 };
string json = JsonUtil.Serialize(obj);
var parsed = JsonUtil.Deserialize<dynamic>(json);

// 日期友好显示
var friendly = DateTimeUtil.ToRelativeString(DateTime.UtcNow.AddMinutes(-90)); // "1 小时 30 分钟前"

// 文件写入示例
FileUtil.WriteAllTextSafe("out.txt", "内容");

// 加密
var md5 = CryptoUtil.MD5Hex("hello");

// HttpClient 简化调用
var resp = await HttpClientUtil.GetStringAsync("https://api.example.com/status");
```

项目结构（建议）
- src/ — 库源码（建议以类库项目组织）
  - Chet.Utils/ (主命名空间)
    - Text/ (字符串工具)
    - Json/ (序列化)
    - Crypto/ (加密)
    - IO/ (文件与路径)
    - Net/ (HTTP 客户端简化)
    - Time/ (日期时间工具)
    - Validation/ (校验函数)
    - Extensions/ (扩展方法)
- tests/ — 单元测试项目
- docs/ — 文档和 API 说明
- samples/ — 使用示例/控制台演示项目

贡献
欢迎贡献：
- 提交 Issue：报告 bug 或建议新功能
- 提交 PR：改善实现、增加测试或补充文档
- 在 PR 中请附带测试用例并说明兼容性影响

编码规范与测试
- 遵守项目现有的命名和样式约定
- 对每个功能提供单元测试，使用 xUnit / NUnit / MSTest 任意一种
- 在合并前运行 dotnet build && dotnet test

许可证
- 仓库当前已添加 MIT LICENSE。

---

(更新：快速开始已改为 NuGet 安装说明。)
