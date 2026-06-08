---
title: "ApplicationHelper"
description: "是一个静态帮助类，为应用程序提供了丰富的信息和控制功能，包括应用程序信息获取、系统环境信息检测、硬件信息获取、运行时监控、应用程序控制、环境变量管理等，旨在简化应用程序信息获取和系统环境检测的开发工作。"
namespace: "Chet.Utils.Helpers"
className: "ApplicationHelper"
category: "Helpers"
order: 1
---

# ApplicationHelper 帮助类

## 概述

[ApplicationHelper](../../Chet.Utils/Helpers/ApplicationHelper.cs) 是一个静态帮助类，为应用程序提供了丰富的信息和控制功能，包括应用程序信息获取、系统环境信息检测、硬件信息获取、运行时监控、应用程序控制、环境变量管理等，旨在简化应用程序信息获取和系统环境检测的开发工作。

## 主要特性

- 📱 应用程序信息获取（名称、版本、路径等）
- 🖥️ 系统环境信息检测（操作系统、架构、区域设置等）
- 💾 硬件信息获取（处理器、磁盘、网络接口等）
- 📊 运行时监控（进程信息、GC 信息、性能监控等）
- 🔧 应用程序控制（重启、退出、权限检测等）
- 🌍 环境变量管理

## 类定义

```csharp
public static partial class ApplicationHelper
```

---

## 应用程序基本信息

### GetApplicationName

获取当前应用程序的名称。

```csharp
public static string GetApplicationName()
```

**返回值：** 应用程序名称，如果无法获取则返回 "Unknown"。

### GetApplicationFullName

获取当前应用程序的完整名称（包含版本信息）。

```csharp
public static string GetApplicationFullName()
```

**返回值：** 应用程序完整名称，格式为 "名称 版本号"。

### GetApplicationVersion

获取当前应用程序的版本。

```csharp
public static Version GetApplicationVersion()
```

**返回值：** 应用程序版本，如果无法获取则返回 0.0.0.0。

### GetApplicationVersionString

获取当前应用程序的版本字符串。

```csharp
public static string GetApplicationVersionString(int fieldCount = 3)
```

| 参数名 | 类型 | 描述 |
|--------|------|------|
| fieldCount | int | 版本字段数量（1-4），默认为 3 |

### GetApplicationPath

获取当前应用程序的启动路径（基目录）。

```csharp
public static string GetApplicationPath()
```

### GetExecutablePath

获取当前应用程序的可执行文件路径。

```csharp
public static string GetExecutablePath()
```

**注意：** 在单文件发布模式下，此方法可能返回空字符串。

### GetConfigurationPath

获取当前应用程序的配置文件路径。

```csharp
public static string GetConfigurationPath()
```

### GetAppDomainInfo

获取应用程序域信息。

```csharp
public static AppDomainInfo GetAppDomainInfo()
```

**返回值：** AppDomainInfo 对象，包含 FriendlyName、BaseDirectory、RelativeSearchPath、ShadowCopyFiles、IsFullyTrusted 属性。

### GetProcessId

获取当前进程 ID。

```csharp
public static int GetProcessId()
```

### GetProcessName

获取当前进程名称。

```csharp
public static string GetProcessName()
```

### GetStartTime

获取应用程序启动时间。

```csharp
public static DateTime GetStartTime()
```

### GetUptime

获取应用程序运行时长。

```csharp
public static TimeSpan GetUptime()
```

---

## 系统环境信息

### GetOperatingSystemInfo

获取操作系统信息。

```csharp
public static OperatingSystemInfo GetOperatingSystemInfo()
```

**返回值：** OperatingSystemInfo 对象，包含 Platform、Version、ServicePack、Is64Bit、MachineName、UserName、UserDomainName 属性。

### GetOSDescription

获取操作系统描述字符串。

```csharp
public static string GetOSDescription()
```

### GetSystemArchitecture

获取当前系统架构。

```csharp
public static string GetSystemArchitecture()
```

**返回值：** 系统架构（x64 或 x86）。

### GetRuntimeArchitecture

获取当前运行时架构。

```csharp
public static string GetRuntimeArchitecture()
```

### GetFrameworkDescription

获取运行时框架描述。

```csharp
public static string GetFrameworkDescription()
```

### IsWindows

检查操作系统是否为 Windows。

```csharp
public static bool IsWindows()
```

### IsLinux

检查操作系统是否为 Linux。

```csharp
public static bool IsLinux()
```

### IsMacOS

检查操作系统是否为 macOS。

```csharp
public static bool IsMacOS()
```

### GetCurrentPlatform

获取当前操作系统平台。

```csharp
public static string GetCurrentPlatform()
```

**返回值：** 操作系统平台名称（Windows、Linux、macOS 或 Unknown）。

### GetSystemCulture

获取系统区域信息。

```csharp
public static CultureInfo GetSystemCulture()
```

### GetSystemUICulture

获取系统 UI 区域信息。

```csharp
public static CultureInfo GetSystemUICulture()
```

### GetSystemTimeZone

获取系统时区信息。

```csharp
public static TimeZoneInfo GetSystemTimeZone()
```

### GetSystemTimeZoneName

获取系统时区名称。

```csharp
public static string GetSystemTimeZoneName()
```

### GetMachineName

获取计算机名称。

```csharp
public static string GetMachineName()
```

### GetUserName

获取当前用户名。

```csharp
public static string GetUserName()
```

### GetUserDomainName

获取用户域名。

```csharp
public static string GetUserDomainName()
```

### GetSystemDirectory

获取系统目录路径。

```csharp
public static string GetSystemDirectory()
```

---

## 硬件信息

### GetProcessorCount

获取处理器核心数。

```csharp
public static int GetProcessorCount()
```

### GetTotalMemory

获取系统总内存（字节）。

```csharp
public static long GetTotalMemory()
```

### GetAvailableMemory

获取可用内存（字节）。

```csharp
public static long GetAvailableMemory()
```

### GetDiskDriveInfo

获取磁盘驱动器信息列表。

```csharp
public static List<DiskDriveInfo> GetDiskDriveInfo()
```

### GetNetworkInterfaces

获取网络接口信息列表。

```csharp
public static List<NetworkInterfaceInfo> GetNetworkInterfaces()
```

---

## 运行时监控

### GetWorkingSet

获取工作集内存大小（字节）。

```csharp
public static long GetWorkingSet()
```

### GetPeakWorkingSet

获取峰值工作集内存大小（字节）。

```csharp
public static long GetPeakWorkingSet()
```

### GetVirtualMemorySize

获取虚拟内存大小（字节）。

```csharp
public static long GetVirtualMemorySize()
```

### GetThreadCount

获取线程数。

```csharp
public static int GetThreadCount()
```

### GetHandleCount

获取句柄数。

```csharp
public static int GetHandleCount()
```

### GetGCTotalMemory

获取 GC 总内存（字节）。

```csharp
public static long GetGCTotalMemory()
```

### GetGCGenerationInfo

获取 GC 代信息。

```csharp
public static GCGenerationInfo GetGCGenerationInfo()
```

### ForceGC

强制执行垃圾回收。

```csharp
public static void ForceGC(int generation = 2)
```

---

## 应用程序控制

### IsAdministrator

检查当前进程是否以管理员权限运行。

```csharp
public static bool IsAdministrator()
```

### Restart

重启应用程序。

```csharp
public static void Restart(int exitCode = 0)
```

### Exit

退出应用程序。

```csharp
public static void Exit(int exitCode = 0)
```

### EnsureSingleInstance

确保应用程序单实例运行。

```csharp
public static bool EnsureSingleInstance(string mutexName)
```

**返回值：** 如果是第一个实例返回 true，否则返回 false。

---

## 环境变量管理

### GetEnvironmentVariable

获取环境变量值。

```csharp
public static string GetEnvironmentVariable(string variableName)
```

### SetEnvironmentVariable

设置环境变量值。

```csharp
public static void SetEnvironmentVariable(string variableName, string value)
```

### GetEnvironmentVariables

获取所有环境变量。

```csharp
public static Dictionary<string, string> GetEnvironmentVariables()
```

---

## 使用示例

### 获取应用程序基本信息

```csharp
using Chet.Utils.Helpers;

// 获取应用程序名称
var appName = ApplicationHelper.GetApplicationName();
Console.WriteLine($"应用程序名称: {appName}");

// 获取应用程序版本
var version = ApplicationHelper.GetApplicationVersion();
Console.WriteLine($"应用程序版本: {version}");

// 获取应用程序路径
var path = ApplicationHelper.GetApplicationPath();
Console.WriteLine($"应用程序路径: {path}");

// 获取运行时长
var uptime = ApplicationHelper.GetUptime();
Console.WriteLine($"已运行: {uptime.TotalHours:F2} 小时");
```

### 获取系统环境信息

```csharp
// 获取操作系统信息
var osInfo = ApplicationHelper.GetOperatingSystemInfo();
Console.WriteLine($"操作系统: {osInfo.Platform}");
Console.WriteLine($"版本: {osInfo.Version}");
Console.WriteLine($"64位: {osInfo.Is64Bit}");

// 检测操作系统平台
if (ApplicationHelper.IsWindows())
{
    Console.WriteLine("当前运行在 Windows 系统");
}
else if (ApplicationHelper.IsLinux())
{
    Console.WriteLine("当前运行在 Linux 系统");
}

// 获取时区信息
var timeZone = ApplicationHelper.GetSystemTimeZone();
Console.WriteLine($"时区: {timeZone.DisplayName}");
```

### 获取硬件信息

```csharp
// 获取处理器核心数
var processorCount = ApplicationHelper.GetProcessorCount();
Console.WriteLine($"处理器核心数: {processorCount}");

// 获取内存信息
var totalMemory = ApplicationHelper.GetTotalMemory();
var availableMemory = ApplicationHelper.GetAvailableMemory();
Console.WriteLine($"总内存: {totalMemory / 1024 / 1024} MB");
Console.WriteLine($"可用内存: {availableMemory / 1024 / 1024} MB");

// 获取磁盘信息
var disks = ApplicationHelper.GetDiskDriveInfo();
foreach (var disk in disks)
{
    Console.WriteLine($"磁盘 {disk.Name}: {disk.TotalSize / 1024 / 1024 / 1024} GB");
}
```

### 运行时监控

```csharp
// 获取进程内存使用
var workingSet = ApplicationHelper.GetWorkingSet();
Console.WriteLine($"工作集内存: {workingSet / 1024 / 1024} MB");

// 获取线程数
var threadCount = ApplicationHelper.GetThreadCount();
Console.WriteLine($"线程数: {threadCount}");

// 获取 GC 内存
var gcMemory = ApplicationHelper.GetGCTotalMemory();
Console.WriteLine($"GC 总内存: {gcMemory / 1024 / 1024} MB");
```

### 权限检测和单实例

```csharp
// 检查是否以管理员权限运行
if (ApplicationHelper.IsAdministrator())
{
    Console.WriteLine("当前以管理员权限运行");
}
else
{
    Console.WriteLine("当前以普通用户权限运行");
}

// 确保单实例运行
if (!ApplicationHelper.EnsureSingleInstance("MyUniqueAppName"))
{
    Console.WriteLine("应用程序已在运行，退出...");
    ApplicationHelper.Exit(1);
}
```

---

## 信息类定义

### AppDomainInfo

| 属性名 | 类型 | 描述 |
|--------|------|------|
| FriendlyName | string | 应用程序域友好名称 |
| BaseDirectory | string | 基目录 |
| RelativeSearchPath | string | 相对搜索路径 |
| ShadowCopyFiles | bool | 是否启用卷影复制 |
| IsFullyTrusted | bool | 是否完全受信任 |

### OperatingSystemInfo

| 属性名 | 类型 | 描述 |
|--------|------|------|
| Platform | string | 操作系统平台 |
| Version | string | 版本号 |
| ServicePack | string | 服务包 |
| Is64Bit | bool | 是否为 64 位 |
| MachineName | string | 计算机名 |
| UserName | string | 用户名 |
| UserDomainName | string | 用户域名 |

### DiskDriveInfo

| 属性名 | 类型 | 描述 |
|--------|------|------|
| Name | string | 驱动器名称 |
| TotalSize | long | 总大小（字节） |
| AvailableFreeSpace | long | 可用空间（字节） |
| DriveFormat | string | 文件系统格式 |
| DriveType | string | 驱动器类型 |

### NetworkInterfaceInfo

| 属性名 | 类型 | 描述 |
|--------|------|------|
| Name | string | 接口名称 |
| Description | string | 描述 |
| Status | string | 状态 |
| Speed | long | 速度（bps） |
| MacAddress | string | MAC 地址 |

---

## 注意事项

1. 部分方法在不同操作系统上可能返回不同的结果。
2. 获取硬件信息可能需要相应的权限。
3. 单实例检测使用 Mutex 实现，确保 mutexName 全局唯一。
4. 在单文件发布模式下，GetExecutablePath 可能返回空字符串。

---

## 版本兼容性

- .NET Framework 4.6.1 及以上版本
- .NET Core 2.0 及以上版本
- .NET 5.0 及以上版本
- .NET Standard 2.0 及以上版本
