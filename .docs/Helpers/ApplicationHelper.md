# ApplicationHelper 类功能文档

## 概述

[ApplicationHelper](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\ApplicationHelper.cs#L14-L748) 是一个静态工具类，提供了丰富的应用程序信息获取、系统环境检测、硬件信息查询、运行时监控、应用程序控制和诊断等功能。该类旨在简化开发者获取应用程序运行环境信息和控制应用程序行为的操作。

## 主要功能模块

### 1. 应用程序基本信息

获取应用程序的基本信息，包括名称、版本、路径等。

**主要方法：**
- [GetApplicationName()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\ApplicationHelper.cs#L22-L25) - 获取当前应用程序的名称
- [GetApplicationFullName()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\ApplicationHelper.cs#L31-L35) - 获取当前应用程序的完整名称（包含版本信息）
- [GetApplicationVersion()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\ApplicationHelper.cs#L41-L44) - 获取当前应用程序的版本
- [GetApplicationPath()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\ApplicationHelper.cs#L50-L53) - 获取当前应用程序的启动路径
- [GetExecutablePath()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\ApplicationHelper.cs#L59-L62) - 获取当前应用程序的可执行文件路径
- [GetConfigurationPath()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\ApplicationHelper.cs#L68-L72) - 获取当前应用程序的配置文件路径
- [GetAppDomainInfo()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\ApplicationHelper.cs#L78-L89) - 获取应用程序域信息

### 2. 系统环境信息

获取操作系统和系统环境相关信息。

**主要方法：**
- [GetOperatingSystemInfo()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\ApplicationHelper.cs#L99-L112) - 获取操作系统信息
- [GetSystemArchitecture()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\ApplicationHelper.cs#L118-L121) - 获取当前系统架构（x86/x64）
- [GetRuntimeArchitecture()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\ApplicationHelper.cs#L127-L130) - 获取当前运行时架构
- [IsWindows()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\ApplicationHelper.cs#L136-L139) - 检查操作系统是否为Windows
- [IsLinux()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\ApplicationHelper.cs#L145-L148) - 检查操作系统是否为Linux
- [IsMacOS()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\ApplicationHelper.cs#L154-L157) - 检查操作系统是否为macOS
- [GetSystemCulture()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\ApplicationHelper.cs#L163-L166) - 获取系统区域信息
- [GetSystemUICulture()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\ApplicationHelper.cs#L172-L175) - 获取系统UI区域信息
- [GetSystemTimeZone()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\ApplicationHelper.cs#L181-L184) - 获取系统时区信息

### 3. 硬件信息

获取系统硬件相关信息，包括处理器、磁盘驱动器和网络接口信息。

**主要方法：**
- [GetProcessorInfo()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\ApplicationHelper.cs#L194-L202) - 获取处理器信息
- [GetDiskDriveInfo()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\ApplicationHelper.cs#L208-L236) - 获取磁盘驱动器信息列表
- [GetNetworkInterfaceInfo()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\ApplicationHelper.cs#L242-L271) - 获取网络接口信息列表

### 4. 运行时监控

监控应用程序运行时状态，包括性能、内存使用情况等。

**主要方法：**
- [GetRuntimeInfo()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\ApplicationHelper.cs#L281-L300) - 获取应用程序运行时信息
- [GetGCInfo()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\ApplicationHelper.cs#L306-L316) - 获取垃圾回收信息
- [ForceGarbageCollection()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\ApplicationHelper.cs#L323-L337) - 强制执行垃圾回收
- [MonitorPerformanceAsync()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\ApplicationHelper.cs#L346-L371) - 监控应用程序性能
- [GetResourceUsageSnapshot()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\ApplicationHelper.cs#L377-L389) - 获取应用程序资源使用情况快照

### 5. 应用程序控制

控制应用程序的行为，如重启、退出、权限管理等。

**主要方法：**
- [Restart()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\ApplicationHelper.cs#L427-L435) - 重启当前应用程序
- [Exit()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\ApplicationHelper.cs#L441-L444) - 退出当前应用程序
- [IsRunningAsAdministrator()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\ApplicationHelper.cs#L450-L462) - 检查是否以管理员权限运行
- [RestartAsAdministrator()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\ApplicationHelper.cs#L467-L491) - 以管理员权限重新启动应用程序
- [SetSingleInstance()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\ApplicationHelper.cs#L498-L509) - 设置应用程序为单实例运行
- [EnableHighPerformanceMode()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\ApplicationHelper.cs#L514-L529) - 启用高性能模式

### 6. 应用程序配置

管理应用程序的配置信息，包括环境变量、特殊文件夹路径等。

**主要方法：**
- [GetStartupArguments()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\ApplicationHelper.cs#L551-L554) - 获取应用程序启动参数
- [GetEnvironmentVariable()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\ApplicationHelper.cs#L562-L565) - 获取环境变量
- [SetEnvironmentVariable()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\ApplicationHelper.cs#L573-L576) - 设置环境变量
- [GetEnvironmentVariables()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\ApplicationHelper.cs#L583-L594) - 获取所有环境变量
- [GetSpecialFolderPath()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\ApplicationHelper.cs#L601-L604) - 获取特殊文件夹路径
- [GetApplicationDataDirectory()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\ApplicationHelper.cs#L610-L622) - 获取应用程序数据目录
- [GetTempDirectory()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\ApplicationHelper.cs#L628-L631) - 获取临时文件目录
- [CreateTempFile()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\ApplicationHelper.cs#L638-L641) - 创建临时文件

### 7. 应用程序诊断

提供应用程序诊断功能，包括生成诊断报告、异常捕获、日志记录等。

**主要方法：**
- [GenerateDiagnosticReport()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\ApplicationHelper.cs#L651-L670) - 生成应用程序诊断报告
- [CaptureException()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\ApplicationHelper.cs#L678-L689) - 捕获应用程序异常
- [Log()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\ApplicationHelper.cs#L697-L709) - 记录应用程序日志
- [StartHealthCheckAsync()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\ApplicationHelper.cs#L718-L745) - 启用应用程序健康检查

## 数据结构

### 信息类
- [AppDomainInfo](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\ApplicationHelper.cs#L755-L781) - 应用程序域信息
- [OperatingSystemInfo](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\ApplicationHelper.cs#L786-L822) - 操作系统信息
- [ProcessorInfo](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\ApplicationHelper.cs#L827-L843) - 处理器信息
- [MemoryInfo](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\ApplicationHelper.cs#L848-L869) - 内存信息
- [DiskDriveInfo](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\ApplicationHelper.cs#L874-L905) - 磁盘驱动器信息
- [NetworkInterfaceInfo](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\ApplicationHelper.cs#L910-L946) - 网络接口信息
- [RuntimeInfo](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\ApplicationHelper.cs#L951-L1002) - 运行时信息
- [GCInfo](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\ApplicationHelper.cs#L1007-L1033) - 垃圾回收信息
- [PerformanceData](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\ApplicationHelper.cs#L1038-L1059) - 性能数据
- [ResourceUsageSnapshot](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\ApplicationHelper.cs#L1064-L1090) - 资源使用情况快照
- [DiagnosticReport](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\ApplicationHelper.cs#L1095-L1136) - 诊断报告
- [ApplicationInfo](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\ApplicationHelper.cs#L1141-L1162) - 应用程序信息
- [ExceptionReport](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\ApplicationHelper.cs#L1167-L1198) - 异常报告
- [LogEntry](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\ApplicationHelper.cs#L1203-L1224) - 日志条目
- [HealthStatus](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\ApplicationHelper.cs#L1260-L1276) - 健康状态

### 枚举类
- [LogLevel](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\ApplicationHelper.cs#L1229-L1255) - 日志级别（Debug, Info, Warning, Error, Fatal）

## 使用场景

1. **系统信息获取** - 获取应用程序和系统环境的详细信息
2. **性能监控** - 实时监控应用程序的性能和资源使用情况
3. **应用程序控制** - 控制应用程序的启动、退出、权限等行为
4. **诊断和日志** - 记录应用程序运行状态和异常信息
5. **配置管理** - 管理应用程序的环境变量和配置信息

## 注意事项

1. 部分方法可能需要特定权限才能正常执行
2. 硬件信息获取方法在不同操作系统上可能返回不同的结果
3. 性能监控方法可能会对应用程序性能产生轻微影响
4. 某些平台特定功能（如高性能模式）可能仅在特定操作系统上有效