using System.Diagnostics;
using System.Globalization;
using System.Net.NetworkInformation;
using System.Reflection;
using System.Runtime;
using System.Runtime.InteropServices;
using System.Security.Principal;

namespace Chet.Utils.Helpers;

/// <summary>
/// 应用程序帮助类，提供应用程序信息获取、系统环境检测、运行时监控等功能。
/// </summary>
/// <remarks>
/// <para>本类提供以下功能模块：</para>
/// <list type="bullet">
///   <item><description>应用程序信息：名称、版本、路径、配置文件等</description></item>
///   <item><description>系统环境信息：操作系统、架构、区域设置、时区等</description></item>
///   <item><description>硬件信息：处理器、磁盘、网络接口等</description></item>
///   <item><description>运行时监控：进程信息、GC信息、性能监控等</description></item>
///   <item><description>应用程序控制：重启、退出、权限检测、单实例等</description></item>
///   <item><description>环境变量管理：获取、设置、枚举环境变量</description></item>
/// </list>
/// </remarks>
public static partial class ApplicationHelper
{
    #region 应用程序基本信息

    /// <summary>
    /// 获取当前应用程序的名称。
    /// </summary>
    /// <returns>应用程序名称，如果无法获取则返回 "Unknown"。</returns>
    /// <example>
    /// <code>
    /// var appName = ApplicationHelper.GetApplicationName();
    /// Console.WriteLine($"应用程序名称: {appName}");
    /// </code>
    /// </example>
    public static string GetApplicationName()
    {
        return Assembly.GetEntryAssembly()?.GetName().Name ?? "Unknown";
    }

    /// <summary>
    /// 获取当前应用程序的完整名称（包含版本信息）。
    /// </summary>
    /// <returns>应用程序完整名称，格式为 "名称 版本号"。</returns>
    /// <example>
    /// <code>
    /// var fullName = ApplicationHelper.GetApplicationFullName();
    /// Console.WriteLine($"应用程序全名: {fullName}");
    /// </code>
    /// </example>
    public static string GetApplicationFullName()
    {
        var assembly = Assembly.GetEntryAssembly();
        return assembly != null ? $"{assembly.GetName().Name} {assembly.GetName().Version}" : "Unknown";
    }

    /// <summary>
    /// 获取当前应用程序的版本。
    /// </summary>
    /// <returns>应用程序版本，如果无法获取则返回 0.0.0.0。</returns>
    /// <example>
    /// <code>
    /// var version = ApplicationHelper.GetApplicationVersion();
    /// Console.WriteLine($"应用程序版本: {version}");
    /// </code>
    /// </example>
    public static Version GetApplicationVersion()
    {
        return Assembly.GetEntryAssembly()?.GetName().Version ?? new Version(0, 0, 0, 0);
    }

    /// <summary>
    /// 获取当前应用程序的版本字符串。
    /// </summary>
    /// <param name="fieldCount">版本字段数量（1-4），默认为3（主版本.次版本.修订版本）。</param>
    /// <returns>版本字符串。</returns>
    /// <example>
    /// <code>
    /// var versionStr = ApplicationHelper.GetApplicationVersionString(3);
    /// Console.WriteLine($"版本: {versionStr}");
    /// </code>
    /// </example>
    public static string GetApplicationVersionString(int fieldCount = 3)
    {
        var version = GetApplicationVersion();
        return version.ToString(fieldCount);
    }

    /// <summary>
    /// 获取当前应用程序的启动路径（基目录）。
    /// </summary>
    /// <returns>应用程序启动路径。</returns>
    /// <example>
    /// <code>
    /// var path = ApplicationHelper.GetApplicationPath();
    /// Console.WriteLine($"应用程序路径: {path}");
    /// </code>
    /// </example>
    public static string GetApplicationPath()
    {
        return AppDomain.CurrentDomain.BaseDirectory;
    }

    /// <summary>
    /// 获取当前应用程序的可执行文件路径。
    /// </summary>
    /// <returns>可执行文件路径，如果无法获取则返回空字符串。</returns>
    /// <remarks>
    /// 注意：在单文件发布模式下，此方法可能返回空字符串。
    /// </remarks>
    /// <example>
    /// <code>
    /// var exePath = ApplicationHelper.GetExecutablePath();
    /// Console.WriteLine($"可执行文件路径: {exePath}");
    /// </code>
    /// </example>
    public static string GetExecutablePath()
    {
        return Assembly.GetEntryAssembly()?.Location ?? string.Empty;
    }

    /// <summary>
    /// 获取当前应用程序的配置文件路径。
    /// </summary>
    /// <returns>配置文件路径，如果无法获取则返回空字符串。</returns>
    /// <example>
    /// <code>
    /// var configPath = ApplicationHelper.GetConfigurationPath();
    /// Console.WriteLine($"配置文件路径: {configPath}");
    /// </code>
    /// </example>
    public static string GetConfigurationPath()
    {
        var exePath = GetExecutablePath();
        return !string.IsNullOrEmpty(exePath) ? $"{exePath}.config" : string.Empty;
    }

    /// <summary>
    /// 获取应用程序域信息。
    /// </summary>
    /// <returns>应用程序域信息对象。</returns>
    /// <example>
    /// <code>
    /// var domainInfo = ApplicationHelper.GetAppDomainInfo();
    /// Console.WriteLine($"域名: {domainInfo.FriendlyName}");
    /// Console.WriteLine($"基目录: {domainInfo.BaseDirectory}");
    /// </code>
    /// </example>
    public static AppDomainInfo GetAppDomainInfo()
    {
        var domain = AppDomain.CurrentDomain;
        return new AppDomainInfo
        {
            FriendlyName = domain.FriendlyName,
            BaseDirectory = domain.BaseDirectory,
            RelativeSearchPath = domain.RelativeSearchPath ?? string.Empty,
            ShadowCopyFiles = domain.ShadowCopyFiles,
            IsFullyTrusted = domain.IsFullyTrusted
        };
    }

    /// <summary>
    /// 获取当前进程ID。
    /// </summary>
    /// <returns>进程ID。</returns>
    /// <example>
    /// <code>
    /// var processId = ApplicationHelper.GetProcessId();
    /// Console.WriteLine($"进程ID: {processId}");
    /// </code>
    /// </example>
    public static int GetProcessId()
    {
        return Environment.ProcessId;
    }

    /// <summary>
    /// 获取当前进程名称。
    /// </summary>
    /// <returns>进程名称。</returns>
    /// <example>
    /// <code>
    /// var processName = ApplicationHelper.GetProcessName();
    /// Console.WriteLine($"进程名称: {processName}");
    /// </code>
    /// </example>
    public static string GetProcessName()
    {
        return Process.GetCurrentProcess().ProcessName;
    }

    /// <summary>
    /// 获取应用程序启动时间。
    /// </summary>
    /// <returns>应用程序启动时间。</returns>
    /// <example>
    /// <code>
    /// var startTime = ApplicationHelper.GetStartTime();
    /// Console.WriteLine($"启动时间: {startTime}");
    /// </code>
    /// </example>
    public static DateTime GetStartTime()
    {
        return Process.GetCurrentProcess().StartTime;
    }

    /// <summary>
    /// 获取应用程序运行时长。
    /// </summary>
    /// <returns>应用程序运行时长。</returns>
    /// <example>
    /// <code>
    /// var uptime = ApplicationHelper.GetUptime();
    /// Console.WriteLine($"已运行: {uptime.TotalHours:F2} 小时");
    /// </code>
    /// </example>
    public static TimeSpan GetUptime()
    {
        return DateTime.Now - Process.GetCurrentProcess().StartTime;
    }

    #endregion

    #region 系统环境信息

    /// <summary>
    /// 获取操作系统信息。
    /// </summary>
    /// <returns>操作系统信息对象。</returns>
    /// <example>
    /// <code>
    /// var osInfo = ApplicationHelper.GetOperatingSystemInfo();
    /// Console.WriteLine($"操作系统: {osInfo.Platform}");
    /// Console.WriteLine($"版本: {osInfo.Version}");
    /// Console.WriteLine($"64位: {osInfo.Is64Bit}");
    /// </code>
    /// </example>
    public static OperatingSystemInfo GetOperatingSystemInfo()
    {
        var os = Environment.OSVersion;
        return new OperatingSystemInfo
        {
            Platform = os.Platform.ToString(),
            Version = os.Version.ToString(),
            ServicePack = os.ServicePack ?? string.Empty,
            Is64Bit = Environment.Is64BitOperatingSystem,
            MachineName = Environment.MachineName,
            UserName = Environment.UserName,
            UserDomainName = Environment.UserDomainName
        };
    }

    /// <summary>
    /// 获取操作系统描述字符串。
    /// </summary>
    /// <returns>操作系统描述字符串。</returns>
    /// <example>
    /// <code>
    /// var osDesc = ApplicationHelper.GetOSDescription();
    /// Console.WriteLine($"操作系统: {osDesc}");
    /// </code>
    /// </example>
    public static string GetOSDescription()
    {
        return RuntimeInformation.OSDescription;
    }

    /// <summary>
    /// 获取当前系统架构。
    /// </summary>
    /// <returns>系统架构（x64 或 x86）。</returns>
    /// <example>
    /// <code>
    /// var arch = ApplicationHelper.GetSystemArchitecture();
    /// Console.WriteLine($"系统架构: {arch}");
    /// </code>
    /// </example>
    public static string GetSystemArchitecture()
    {
        return Environment.Is64BitOperatingSystem ? "x64" : "x86";
    }

    /// <summary>
    /// 获取当前运行时架构。
    /// </summary>
    /// <returns>运行时架构（x64 或 x86）。</returns>
    /// <example>
    /// <code>
    /// var arch = ApplicationHelper.GetRuntimeArchitecture();
    /// Console.WriteLine($"运行时架构: {arch}");
    /// </code>
    /// </example>
    public static string GetRuntimeArchitecture()
    {
        return Environment.Is64BitProcess ? "x64" : "x86";
    }

    /// <summary>
    /// 获取运行时框架描述。
    /// </summary>
    /// <returns>运行时框架描述字符串。</returns>
    /// <example>
    /// <code>
    /// var framework = ApplicationHelper.GetFrameworkDescription();
    /// Console.WriteLine($"运行时: {framework}");
    /// </code>
    /// </example>
    public static string GetFrameworkDescription()
    {
        return RuntimeInformation.FrameworkDescription;
    }

    /// <summary>
    /// 检查操作系统是否为 Windows。
    /// </summary>
    /// <returns>是否为 Windows 系统。</returns>
    /// <example>
    /// <code>
    /// if (ApplicationHelper.IsWindows())
    /// {
    ///     Console.WriteLine("当前运行在 Windows 系统");
    /// }
    /// </code>
    /// </example>
    public static bool IsWindows()
    {
        return RuntimeInformation.IsOSPlatform(OSPlatform.Windows);
    }

    /// <summary>
    /// 检查操作系统是否为 Linux。
    /// </summary>
    /// <returns>是否为 Linux 系统。</returns>
    /// <example>
    /// <code>
    /// if (ApplicationHelper.IsLinux())
    /// {
    ///     Console.WriteLine("当前运行在 Linux 系统");
    /// }
    /// </code>
    /// </example>
    public static bool IsLinux()
    {
        return RuntimeInformation.IsOSPlatform(OSPlatform.Linux);
    }

    /// <summary>
    /// 检查操作系统是否为 macOS。
    /// </summary>
    /// <returns>是否为 macOS 系统。</returns>
    /// <example>
    /// <code>
    /// if (ApplicationHelper.IsMacOS())
    /// {
    ///     Console.WriteLine("当前运行在 macOS 系统");
    /// }
    /// </code>
    /// </example>
    public static bool IsMacOS()
    {
        return RuntimeInformation.IsOSPlatform(OSPlatform.OSX);
    }

    /// <summary>
    /// 获取当前操作系统平台。
    /// </summary>
    /// <returns>操作系统平台名称（Windows、Linux、macOS 或 Unknown）。</returns>
    /// <example>
    /// <code>
    /// var platform = ApplicationHelper.GetCurrentPlatform();
    /// Console.WriteLine($"当前平台: {platform}");
    /// </code>
    /// </example>
    public static string GetCurrentPlatform()
    {
        if (IsWindows()) return "Windows";
        if (IsLinux()) return "Linux";
        if (IsMacOS()) return "macOS";
        return "Unknown";
    }

    /// <summary>
    /// 获取系统区域信息。
    /// </summary>
    /// <returns>当前区域文化信息。</returns>
    /// <example>
    /// <code>
    /// var culture = ApplicationHelper.GetSystemCulture();
    /// Console.WriteLine($"区域设置: {culture.Name} ({culture.DisplayName})");
    /// </code>
    /// </example>
    public static CultureInfo GetSystemCulture()
    {
        return CultureInfo.CurrentCulture;
    }

    /// <summary>
    /// 获取系统 UI 区域信息。
    /// </summary>
    /// <returns>当前 UI 区域文化信息。</returns>
    /// <example>
    /// <code>
    /// var uiCulture = ApplicationHelper.GetSystemUICulture();
    /// Console.WriteLine($"UI区域设置: {uiCulture.Name}");
    /// </code>
    /// </example>
    public static CultureInfo GetSystemUICulture()
    {
        return CultureInfo.CurrentUICulture;
    }

    /// <summary>
    /// 获取系统时区信息。
    /// </summary>
    /// <returns>本地时区信息。</returns>
    /// <example>
    /// <code>
    /// var timeZone = ApplicationHelper.GetSystemTimeZone();
    /// Console.WriteLine($"时区: {timeZone.DisplayName}");
    /// </code>
    /// </example>
    public static TimeZoneInfo GetSystemTimeZone()
    {
        return TimeZoneInfo.Local;
    }

    /// <summary>
    /// 获取系统时区名称。
    /// </summary>
    /// <returns>本地时区名称。</returns>
    /// <example>
    /// <code>
    /// var tzName = ApplicationHelper.GetSystemTimeZoneName();
    /// Console.WriteLine($"时区名称: {tzName}");
    /// </code>
    /// </example>
    public static string GetSystemTimeZoneName()
    {
        return TimeZoneInfo.Local.Id;
    }

    /// <summary>
    /// 获取计算机名称。
    /// </summary>
    /// <returns>计算机名称。</returns>
    /// <example>
    /// <code>
    /// var machineName = ApplicationHelper.GetMachineName();
    /// Console.WriteLine($"计算机名: {machineName}");
    /// </code>
    /// </example>
    public static string GetMachineName()
    {
        return Environment.MachineName;
    }

    /// <summary>
    /// 获取当前用户名。
    /// </summary>
    /// <returns>当前用户名。</returns>
    /// <example>
    /// <code>
    /// var userName = ApplicationHelper.GetUserName();
    /// Console.WriteLine($"用户名: {userName}");
    /// </code>
    /// </example>
    public static string GetUserName()
    {
        return Environment.UserName;
    }

    /// <summary>
    /// 获取用户域名。
    /// </summary>
    /// <returns>用户域名。</returns>
    /// <example>
    /// <code>
    /// var domain = ApplicationHelper.GetUserDomainName();
    /// Console.WriteLine($"域名: {domain}");
    /// </code>
    /// </example>
    public static string GetUserDomainName()
    {
        return Environment.UserDomainName;
    }

    /// <summary>
    /// 获取系统目录路径。
    /// </summary>
    /// <returns>系统目录路径。</returns>
    /// <example>
    /// <code>
    /// var systemDir = ApplicationHelper.GetSystemDirectory();
    /// Console.WriteLine($"系统目录: {systemDir}");
    /// </code>
    /// </example>
    public static string GetSystemDirectory()
    {
        return Environment.SystemDirectory;
    }

    /// <summary>
    /// 获取系统启动以来的毫秒数。
    /// </summary>
    /// <returns>系统启动以来的毫秒数。</returns>
    /// <example>
    /// <code>
    /// var tickCount = ApplicationHelper.GetTickCount();
    /// Console.WriteLine($"系统运行时间: {tickCount / 1000 / 60} 分钟");
    /// </code>
    /// </example>
    public static long GetTickCount()
    {
        return Environment.TickCount64;
    }

    #endregion

    #region 硬件信息

    /// <summary>
    /// 获取处理器信息。
    /// </summary>
    /// <returns>处理器信息对象。</returns>
    /// <example>
    /// <code>
    /// var cpuInfo = ApplicationHelper.GetProcessorInfo();
    /// Console.WriteLine($"处理器数量: {cpuInfo.ProcessorCount}");
    /// Console.WriteLine($"架构: {cpuInfo.ProcessorArchitecture}");
    /// </code>
    /// </example>
    public static ProcessorInfo GetProcessorInfo()
    {
        return new ProcessorInfo
        {
            ProcessorCount = Environment.ProcessorCount,
            ProcessorArchitecture = RuntimeInformation.ProcessArchitecture.ToString(),
            OSArchitecture = RuntimeInformation.OSArchitecture.ToString()
        };
    }

    /// <summary>
    /// 获取处理器核心数。
    /// </summary>
    /// <returns>处理器核心数。</returns>
    /// <example>
    /// <code>
    /// var coreCount = ApplicationHelper.GetProcessorCount();
    /// Console.WriteLine($"CPU核心数: {coreCount}");
    /// </code>
    /// </example>
    public static int GetProcessorCount()
    {
        return Environment.ProcessorCount;
    }

    /// <summary>
    /// 获取系统可用内存大小（近似值）。
    /// </summary>
    /// <returns>可用内存大小（字节）。</returns>
    /// <remarks>
    /// 此方法返回的是 GCMemoryInfo 中可用的内存大小，可能与实际系统可用内存不同。
    /// </remarks>
    /// <example>
    /// <code>
    /// var availableMem = ApplicationHelper.GetAvailableMemory();
    /// Console.WriteLine($"可用内存: {availableMem / 1024 / 1024} MB");
    /// </code>
    /// </example>
    public static long GetAvailableMemory()
    {
        var gcInfo = GC.GetGCMemoryInfo();
        return gcInfo.TotalAvailableMemoryBytes;
    }

    /// <summary>
    /// 获取磁盘驱动器信息。
    /// </summary>
    /// <returns>磁盘驱动器信息列表。</returns>
    /// <example>
    /// <code>
    /// var drives = ApplicationHelper.GetDiskDriveInfo();
    /// foreach (var drive in drives)
    /// {
    ///     Console.WriteLine($"{drive.Name} - 可用空间: {drive.AvailableFreeSpace / 1024 / 1024 / 1024} GB");
    /// }
    /// </code>
    /// </example>
    public static List<DiskDriveInfo> GetDiskDriveInfo()
    {
        var drives = new List<DiskDriveInfo>();

        try
        {
            foreach (var drive in DriveInfo.GetDrives())
            {
                if (drive.IsReady)
                {
                    drives.Add(new DiskDriveInfo
                    {
                        Name = drive.Name,
                        DriveType = drive.DriveType.ToString(),
                        DriveFormat = drive.DriveFormat ?? string.Empty,
                        TotalSize = drive.TotalSize,
                        AvailableFreeSpace = drive.AvailableFreeSpace,
                        TotalFreeSpace = drive.TotalFreeSpace,
                        VolumeLabel = drive.VolumeLabel ?? string.Empty
                    });
                }
            }
        }
        catch (Exception)
        {
            // 忽略驱动器访问错误
        }

        return drives;
    }

    /// <summary>
    /// 获取指定驱动器的信息。
    /// </summary>
    /// <param name="driveName">驱动器名称（如 "C:\"）。</param>
    /// <returns>驱动器信息，如果不存在则返回 null。</returns>
    /// <example>
    /// <code>
    /// var driveC = ApplicationHelper.GetDiskDriveInfo("C:\\");
    /// if (driveC != null)
    /// {
    ///     Console.WriteLine($"C盘剩余空间: {driveC.AvailableFreeSpace / 1024 / 1024 / 1024} GB");
    /// }
    /// </code>
    /// </example>
    public static DiskDriveInfo? GetDiskDriveInfo(string driveName)
    {
        try
        {
            var drive = new DriveInfo(driveName);
            if (drive.IsReady)
            {
                return new DiskDriveInfo
                {
                    Name = drive.Name,
                    DriveType = drive.DriveType.ToString(),
                    DriveFormat = drive.DriveFormat ?? string.Empty,
                    TotalSize = drive.TotalSize,
                    AvailableFreeSpace = drive.AvailableFreeSpace,
                    TotalFreeSpace = drive.TotalFreeSpace,
                    VolumeLabel = drive.VolumeLabel ?? string.Empty
                };
            }
        }
        catch (Exception)
        {
            // 忽略驱动器访问错误
        }

        return null;
    }

    /// <summary>
    /// 获取网络接口信息。
    /// </summary>
    /// <returns>网络接口信息列表。</returns>
    /// <example>
    /// <code>
    /// var interfaces = ApplicationHelper.GetNetworkInterfaceInfo();
    /// foreach (var nic in interfaces)
    /// {
    ///     Console.WriteLine($"{nic.Name} - {nic.Type}");
    /// }
    /// </code>
    /// </example>
    public static List<NetworkInterfaceInfo> GetNetworkInterfaceInfo()
    {
        var interfaces = new List<NetworkInterfaceInfo>();

        try
        {
            foreach (var nic in NetworkInterface.GetAllNetworkInterfaces())
            {
                var ipProps = nic.GetIPProperties();
                interfaces.Add(new NetworkInterfaceInfo
                {
                    Name = nic.Name,
                    Description = nic.Description,
                    Type = nic.NetworkInterfaceType.ToString(),
                    Speed = nic.Speed,
                    PhysicalAddress = nic.GetPhysicalAddress().ToString(),
                    IsOperational = nic.OperationalStatus == OperationalStatus.Up,
                    IpAddresses = ipProps.UnicastAddresses
                        .Select(addr => addr.Address.ToString())
                        .ToList()
                });
            }
        }
        catch (Exception)
        {
            // 忽略网络接口访问错误
        }

        return interfaces;
    }

    /// <summary>
    /// 检查是否存在网络连接。
    /// </summary>
    /// <returns>是否存在网络连接。</returns>
    /// <example>
    /// <code>
    /// if (ApplicationHelper.HasNetworkConnection())
    /// {
    ///     Console.WriteLine("网络已连接");
    /// }
    /// </code>
    /// </example>
    public static bool HasNetworkConnection()
    {
        try
        {
            return NetworkInterface.GetIsNetworkAvailable();
        }
        catch
        {
            return false;
        }
    }

    #endregion

    #region 运行时监控

    /// <summary>
    /// 获取应用程序运行时信息。
    /// </summary>
    /// <returns>运行时信息对象。</returns>
    /// <example>
    /// <code>
    /// var runtimeInfo = ApplicationHelper.GetRuntimeInfo();
    /// Console.WriteLine($"进程ID: {runtimeInfo.ProcessId}");
    /// Console.WriteLine($"工作集: {runtimeInfo.WorkingSet / 1024 / 1024} MB");
    /// Console.WriteLine($"线程数: {runtimeInfo.Threads}");
    /// </code>
    /// </example>
    public static RuntimeInfo GetRuntimeInfo()
    {
        var process = Process.GetCurrentProcess();
        var startTime = process.StartTime;
        var uptime = DateTime.Now - startTime;

        return new RuntimeInfo
        {
            ProcessId = process.Id,
            ProcessName = process.ProcessName,
            StartTime = startTime,
            Uptime = uptime,
            WorkingSet = process.WorkingSet64,
            PeakWorkingSet = process.PeakWorkingSet64,
            PrivateMemorySize = process.PrivateMemorySize64,
            VirtualMemorySize = process.VirtualMemorySize64,
            Threads = process.Threads.Count,
            Handles = process.HandleCount
        };
    }

    /// <summary>
    /// 获取垃圾回收信息。
    /// </summary>
    /// <returns>垃圾回收信息对象。</returns>
    /// <example>
    /// <code>
    /// var gcInfo = ApplicationHelper.GetGCInfo();
    /// Console.WriteLine($"总内存: {gcInfo.TotalMemory / 1024 / 1024} MB");
    /// Console.WriteLine($"Gen0 回收次数: {gcInfo.CollectionCountGen0}");
    /// </code>
    /// </example>
    public static GCInfo GetGCInfo()
    {
        return new GCInfo
        {
            CollectionCountGen0 = GC.CollectionCount(0),
            CollectionCountGen1 = GC.CollectionCount(1),
            CollectionCountGen2 = GC.CollectionCount(2),
            TotalMemory = GC.GetTotalMemory(false),
            MaxGeneration = GC.MaxGeneration
        };
    }

    /// <summary>
    /// 获取当前内存使用量。
    /// </summary>
    /// <returns>内存使用量（字节）。</returns>
    /// <example>
    /// <code>
    /// var memUsage = ApplicationHelper.GetMemoryUsage();
    /// Console.WriteLine($"内存使用: {memUsage / 1024 / 1024} MB");
    /// </code>
    /// </example>
    public static long GetMemoryUsage()
    {
        return Process.GetCurrentProcess().WorkingSet64;
    }

    /// <summary>
    /// 获取峰值内存使用量。
    /// </summary>
    /// <returns>峰值内存使用量（字节）。</returns>
    /// <example>
    /// <code>
    /// var peakMem = ApplicationHelper.GetPeakMemoryUsage();
    /// Console.WriteLine($"峰值内存: {peakMem / 1024 / 1024} MB");
    /// </code>
    /// </example>
    public static long GetPeakMemoryUsage()
    {
        return Process.GetCurrentProcess().PeakWorkingSet64;
    }

    /// <summary>
    /// 获取线程数。
    /// </summary>
    /// <returns>当前线程数。</returns>
    /// <example>
    /// <code>
    /// var threadCount = ApplicationHelper.GetThreadCount();
    /// Console.WriteLine($"线程数: {threadCount}");
    /// </code>
    /// </example>
    public static int GetThreadCount()
    {
        return Process.GetCurrentProcess().Threads.Count;
    }

    /// <summary>
    /// 获取句柄数。
    /// </summary>
    /// <returns>当前句柄数。</returns>
    /// <example>
    /// <code>
    /// var handleCount = ApplicationHelper.GetHandleCount();
    /// Console.WriteLine($"句柄数: {handleCount}");
    /// </code>
    /// </example>
    public static int GetHandleCount()
    {
        return Process.GetCurrentProcess().HandleCount;
    }

    /// <summary>
    /// 强制执行垃圾回收。
    /// </summary>
    /// <param name="generation">回收代数（0-2），默认为最大代数。</param>
    /// <param name="blocking">是否阻塞直到回收完成。</param>
    /// <param name="compacting">是否执行内存压缩。</param>
    /// <example>
    /// <code>
    /// // 执行完整垃圾回收并压缩内存
    /// ApplicationHelper.ForceGarbageCollection(blocking: true, compacting: true);
    /// </code>
    /// </example>
    public static void ForceGarbageCollection(int generation = -1, bool blocking = false, bool compacting = false)
    {
        if (compacting)
        {
            GCSettings.LargeObjectHeapCompactionMode = GCLargeObjectHeapCompactionMode.CompactOnce;
        }

        if (generation < 0)
        {
            GC.Collect();
        }
        else
        {
            GC.Collect(generation);
        }

        if (blocking)
        {
            GC.WaitForPendingFinalizers();
            GC.Collect();
        }
    }

    /// <summary>
    /// 监控应用程序性能。
    /// </summary>
    /// <param name="interval">监控间隔。</param>
    /// <param name="callback">性能数据回调。</param>
    /// <param name="cancellationToken">取消令牌。</param>
    /// <returns>监控任务。</returns>
    /// <example>
    /// <code>
    /// var cts = new CancellationTokenSource();
    /// var monitorTask = ApplicationHelper.MonitorPerformanceAsync(
    ///     TimeSpan.FromSeconds(5),
    ///     data => Console.WriteLine($"内存: {data.RuntimeInfo.WorkingSet / 1024 / 1024} MB"),
    ///     cts.Token);
    /// 
    /// // 停止监控
    /// cts.Cancel();
    /// </code>
    /// </example>
    public static async Task MonitorPerformanceAsync(
        TimeSpan interval,
        Action<PerformanceData> callback,
        CancellationToken cancellationToken = default)
    {
        while (!cancellationToken.IsCancellationRequested)
        {
            var performanceData = new PerformanceData
            {
                Timestamp = DateTime.UtcNow,
                RuntimeInfo = GetRuntimeInfo(),
                GCInfo = GetGCInfo()
            };

            callback?.Invoke(performanceData);

            try
            {
                await Task.Delay(interval, cancellationToken);
            }
            catch (OperationCanceledException)
            {
                break;
            }
        }
    }

    /// <summary>
    /// 获取应用程序资源使用情况快照。
    /// </summary>
    /// <returns>资源使用情况快照。</returns>
    /// <example>
    /// <code>
    /// var snapshot = ApplicationHelper.GetResourceUsageSnapshot();
    /// Console.WriteLine($"CPU: {snapshot.CpuUsage:F1}%");
    /// Console.WriteLine($"内存: {snapshot.MemoryUsage / 1024 / 1024} MB");
    /// </code>
    /// </example>
    public static ResourceUsageSnapshot GetResourceUsageSnapshot()
    {
        var process = Process.GetCurrentProcess();

        return new ResourceUsageSnapshot
        {
            Timestamp = DateTime.UtcNow,
            CpuUsage = GetCpuUsage(process),
            MemoryUsage = process.WorkingSet64,
            ThreadCount = process.Threads.Count,
            HandleCount = process.HandleCount
        };
    }

    /// <summary>
    /// 获取 CPU 使用率。
    /// </summary>
    /// <param name="process">进程对象。</param>
    /// <returns>CPU 使用率百分比。</returns>
    private static double GetCpuUsage(Process process)
    {
        try
        {
            var startTime = DateTime.UtcNow;
            var startCpuTime = process.TotalProcessorTime;

            Thread.Sleep(500);

            var endTime = DateTime.UtcNow;
            var endCpuTime = process.TotalProcessorTime;

            var cpuUsedMs = (endCpuTime - startCpuTime).TotalMilliseconds;
            var totalMsPassed = (endTime - startTime).TotalMilliseconds;

            var cpuUsage = cpuUsedMs / (Environment.ProcessorCount * totalMsPassed) * 100;
            return Math.Max(0, Math.Min(100, cpuUsage));
        }
        catch
        {
            return 0;
        }
    }

    #endregion

    #region 应用程序控制

    /// <summary>
    /// 重启当前应用程序。
    /// </summary>
    /// <param name="args">启动参数，可选。</param>
    /// <remarks>
    /// 注意：此方法会终止当前进程并启动新进程。
    /// </remarks>
    /// <example>
    /// <code>
    /// ApplicationHelper.Restart();
    /// </code>
    /// </example>
    public static void Restart(string[]? args = null)
    {
        var exePath = GetExecutablePath();
        if (!string.IsNullOrEmpty(exePath))
        {
            var startInfo = new ProcessStartInfo(exePath);
            if (args != null && args.Length > 0)
            {
                startInfo.Arguments = string.Join(" ", args);
            }
            Process.Start(startInfo);
            Environment.Exit(0);
        }
    }

    /// <summary>
    /// 退出当前应用程序。
    /// </summary>
    /// <param name="exitCode">退出代码，默认为 0。</param>
    /// <example>
    /// <code>
    /// ApplicationHelper.Exit(0);
    /// </code>
    /// </example>
    public static void Exit(int exitCode = 0)
    {
        Environment.Exit(exitCode);
    }

    /// <summary>
    /// 检查是否以管理员权限运行（仅 Windows）。
    /// </summary>
    /// <returns>是否以管理员权限运行。</returns>
    /// <remarks>
    /// 在非 Windows 系统上，此方法始终返回 false。
    /// </remarks>
    /// <example>
    /// <code>
    /// if (!ApplicationHelper.IsRunningAsAdministrator())
    /// {
    ///     Console.WriteLine("请以管理员权限运行此程序");
    /// }
    /// </code>
    /// </example>
    public static bool IsRunningAsAdministrator()
    {
        if (!IsWindows()) return false;

        try
        {
            using var identity = WindowsIdentity.GetCurrent();
            var principal = new WindowsPrincipal(identity);
            return principal.IsInRole(WindowsBuiltInRole.Administrator);
        }
        catch
        {
            return false;
        }
    }

    /// <summary>
    /// 以管理员权限重新启动应用程序（仅 Windows）。
    /// </summary>
    /// <returns>是否成功启动新进程。</returns>
    /// <remarks>
    /// 如果已经以管理员权限运行，则不执行任何操作并返回 false。
    /// </remarks>
    /// <example>
    /// <code>
    /// if (!ApplicationHelper.IsRunningAsAdministrator())
    /// {
    ///     ApplicationHelper.RestartAsAdministrator();
    /// }
    /// </code>
    /// </example>
    public static bool RestartAsAdministrator()
    {
        if (!IsWindows() || IsRunningAsAdministrator()) return false;

        try
        {
            var exePath = GetExecutablePath();
            if (!string.IsNullOrEmpty(exePath))
            {
                var startInfo = new ProcessStartInfo(exePath)
                {
                    Verb = "runas",
                    UseShellExecute = true
                };

                Process.Start(startInfo);
                Environment.Exit(0);
                return true;
            }
        }
        catch
        {
            // 忽略启动错误
        }

        return false;
    }

    /// <summary>
    /// 设置应用程序为单实例运行。
    /// </summary>
    /// <param name="applicationName">应用程序唯一标识名称。</param>
    /// <returns>是否为第一个实例。</returns>
    /// <remarks>
    /// 注意：返回的 Mutex 需要由调用方保持引用，否则会被垃圾回收导致锁失效。
    /// </remarks>
    /// <example>
    /// <code>
    /// if (!ApplicationHelper.SetSingleInstance("MyUniqueAppName", out var mutex))
    /// {
    ///     Console.WriteLine("应用程序已在运行");
    ///     return;
    /// }
    /// // 保持 mutex 引用直到程序退出
    /// GC.KeepAlive(mutex);
    /// </code>
    /// </example>
    public static bool SetSingleInstance(string applicationName)
    {
        try
        {
            var mutex = new Mutex(true, applicationName, out bool createdNew);
            return createdNew;
        }
        catch
        {
            return false;
        }
    }

    /// <summary>
    /// 设置应用程序为单实例运行，并返回 Mutex 对象。
    /// </summary>
    /// <param name="applicationName">应用程序唯一标识名称。</param>
    /// <param name="mutex">创建的 Mutex 对象。</param>
    /// <returns>是否为第一个实例。</returns>
    /// <example>
    /// <code>
    /// if (!ApplicationHelper.SetSingleInstance("MyUniqueAppName", out var mutex))
    /// {
    ///     Console.WriteLine("应用程序已在运行");
    ///     return;
    /// }
    /// // 使用 mutex...
    /// mutex.ReleaseMutex();
    /// </code>
    /// </example>
    public static bool SetSingleInstance(string applicationName, out Mutex? mutex)
    {
        mutex = null;
        try
        {
            mutex = new Mutex(true, applicationName, out bool createdNew);
            return createdNew;
        }
        catch
        {
            mutex?.Dispose();
            mutex = null;
            return false;
        }
    }

    /// <summary>
    /// 设置进程优先级。
    /// </summary>
    /// <param name="priority">进程优先级。</param>
    /// <returns>是否设置成功。</returns>
    /// <example>
    /// <code>
    /// ApplicationHelper.SetProcessPriority(ProcessPriorityClass.High);
    /// </code>
    /// </example>
    public static bool SetProcessPriority(ProcessPriorityClass priority)
    {
        try
        {
            Process.GetCurrentProcess().PriorityClass = priority;
            return true;
        }
        catch
        {
            return false;
        }
    }

    /// <summary>
    /// 启用高性能模式（提高进程优先级并禁止系统睡眠）。
    /// </summary>
    /// <returns>是否成功启用。</returns>
    /// <remarks>
    /// 注意：此方法仅影响当前进程，不会永久修改系统设置。
    /// </remarks>
    /// <example>
    /// <code>
    /// ApplicationHelper.EnableHighPerformanceMode();
    /// </code>
    /// </example>
    public static bool EnableHighPerformanceMode()
    {
        try
        {
            SetProcessPriority(ProcessPriorityClass.High);

            if (IsWindows())
            {
                SetThreadExecutionState(EXECUTION_STATE.ES_CONTINUOUS | EXECUTION_STATE.ES_SYSTEM_REQUIRED);
            }

            return true;
        }
        catch
        {
            return false;
        }
    }

    [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    private static extern EXECUTION_STATE SetThreadExecutionState(EXECUTION_STATE esFlags);

    [Flags]
    private enum EXECUTION_STATE : uint
    {
        ES_AWAYMODE_REQUIRED = 0x00000040,
        ES_CONTINUOUS = 0x80000000,
        ES_DISPLAY_REQUIRED = 0x00000002,
        ES_SYSTEM_REQUIRED = 0x00000001
    }

    #endregion

    #region 环境变量管理

    /// <summary>
    /// 获取应用程序启动参数。
    /// </summary>
    /// <returns>启动参数数组。</returns>
    /// <example>
    /// <code>
    /// var args = ApplicationHelper.GetStartupArguments();
    /// foreach (var arg in args)
    /// {
    ///     Console.WriteLine($"参数: {arg}");
    /// }
    /// </code>
    /// </example>
    public static string[] GetStartupArguments()
    {
        return Environment.GetCommandLineArgs();
    }

    /// <summary>
    /// 获取环境变量。
    /// </summary>
    /// <param name="variableName">环境变量名称。</param>
    /// <param name="target">环境变量目标，默认为 Process。</param>
    /// <returns>环境变量值，如果不存在则返回 null。</returns>
    /// <example>
    /// <code>
    /// var path = ApplicationHelper.GetEnvironmentVariable("PATH");
    /// Console.WriteLine($"PATH: {path}");
    /// </code>
    /// </example>
    public static string? GetEnvironmentVariable(string variableName, EnvironmentVariableTarget target = EnvironmentVariableTarget.Process)
    {
        return Environment.GetEnvironmentVariable(variableName, target);
    }

    /// <summary>
    /// 设置环境变量。
    /// </summary>
    /// <param name="variableName">环境变量名称。</param>
    /// <param name="value">环境变量值，null 表示删除该变量。</param>
    /// <param name="target">环境变量目标，默认为 Process。</param>
    /// <example>
    /// <code>
    /// ApplicationHelper.SetEnvironmentVariable("MY_VAR", "my_value");
    /// </code>
    /// </example>
    public static void SetEnvironmentVariable(string variableName, string? value, EnvironmentVariableTarget target = EnvironmentVariableTarget.Process)
    {
        Environment.SetEnvironmentVariable(variableName, value, target);
    }

    /// <summary>
    /// 获取所有环境变量。
    /// </summary>
    /// <param name="target">环境变量目标，默认为 Process。</param>
    /// <returns>环境变量字典。</returns>
    /// <example>
    /// <code>
    /// var envVars = ApplicationHelper.GetEnvironmentVariables();
    /// foreach (var kvp in envVars)
    /// {
    ///     Console.WriteLine($"{kvp.Key}={kvp.Value}");
    /// }
    /// </code>
    /// </example>
    public static Dictionary<string, string> GetEnvironmentVariables(EnvironmentVariableTarget target = EnvironmentVariableTarget.Process)
    {
        var variables = Environment.GetEnvironmentVariables(target);
        var result = new Dictionary<string, string>();

        foreach (var key in variables.Keys)
        {
            result[key.ToString() ?? string.Empty] = variables[key]?.ToString() ?? string.Empty;
        }

        return result;
    }

    /// <summary>
    /// 检查环境变量是否存在。
    /// </summary>
    /// <param name="variableName">环境变量名称。</param>
    /// <param name="target">环境变量目标，默认为 Process。</param>
    /// <returns>是否存在。</returns>
    /// <example>
    /// <code>
    /// if (ApplicationHelper.HasEnvironmentVariable("MY_VAR"))
    /// {
    ///     Console.WriteLine("MY_VAR 已设置");
    /// }
    /// </code>
    /// </example>
    public static bool HasEnvironmentVariable(string variableName, EnvironmentVariableTarget target = EnvironmentVariableTarget.Process)
    {
        return Environment.GetEnvironmentVariable(variableName, target) != null;
    }

    #endregion

    #region 特殊文件夹

    /// <summary>
    /// 获取特殊文件夹路径。
    /// </summary>
    /// <param name="folder">特殊文件夹枚举。</param>
    /// <returns>文件夹路径。</returns>
    /// <example>
    /// <code>
    /// var desktop = ApplicationHelper.GetSpecialFolderPath(Environment.SpecialFolder.Desktop);
    /// Console.WriteLine($"桌面: {desktop}");
    /// </code>
    /// </example>
    public static string GetSpecialFolderPath(Environment.SpecialFolder folder)
    {
        return Environment.GetFolderPath(folder);
    }

    /// <summary>
    /// 获取应用程序数据目录。
    /// </summary>
    /// <returns>应用程序数据目录路径。</returns>
    /// <remarks>
    /// 如果目录不存在，会自动创建。
    /// </remarks>
    /// <example>
    /// <code>
    /// var appDataDir = ApplicationHelper.GetApplicationDataDirectory();
    /// Console.WriteLine($"应用数据目录: {appDataDir}");
    /// </code>
    /// </example>
    public static string GetApplicationDataDirectory()
    {
        var appData = GetSpecialFolderPath(Environment.SpecialFolder.ApplicationData);
        var appName = GetApplicationName();
        var appDir = Path.Combine(appData, appName);

        if (!Directory.Exists(appDir))
        {
            Directory.CreateDirectory(appDir);
        }

        return appDir;
    }

    /// <summary>
    /// 获取本地应用程序数据目录。
    /// </summary>
    /// <returns>本地应用程序数据目录路径。</returns>
    /// <remarks>
    /// 如果目录不存在，会自动创建。
    /// </remarks>
    /// <example>
    /// <code>
    /// var localAppDataDir = ApplicationHelper.GetLocalApplicationDataDirectory();
    /// Console.WriteLine($"本地应用数据目录: {localAppDataDir}");
    /// </code>
    /// </example>
    public static string GetLocalApplicationDataDirectory()
    {
        var appData = GetSpecialFolderPath(Environment.SpecialFolder.LocalApplicationData);
        var appName = GetApplicationName();
        var appDir = Path.Combine(appData, appName);

        if (!Directory.Exists(appDir))
        {
            Directory.CreateDirectory(appDir);
        }

        return appDir;
    }

    /// <summary>
    /// 获取临时文件目录。
    /// </summary>
    /// <returns>临时文件目录路径。</returns>
    /// <example>
    /// <code>
    /// var tempDir = ApplicationHelper.GetTempDirectory();
    /// Console.WriteLine($"临时目录: {tempDir}");
    /// </code>
    /// </example>
    public static string GetTempDirectory()
    {
        return Path.GetTempPath();
    }

    /// <summary>
    /// 创建临时文件。
    /// </summary>
    /// <returns>临时文件完整路径。</returns>
    /// <example>
    /// <code>
    /// var tempFile = ApplicationHelper.CreateTempFile();
    /// Console.WriteLine($"临时文件: {tempFile}");
    /// </code>
    /// </example>
    public static string CreateTempFile()
    {
        return Path.GetTempFileName();
    }

    /// <summary>
    /// 获取日志目录。
    /// </summary>
    /// <returns>日志目录路径。</returns>
    /// <remarks>
    /// 如果目录不存在，会自动创建。
    /// </remarks>
    /// <example>
    /// <code>
    /// var logDir = ApplicationHelper.GetLogDirectory();
    /// Console.WriteLine($"日志目录: {logDir}");
    /// </code>
    /// </example>
    public static string GetLogDirectory()
    {
        var appDir = GetLocalApplicationDataDirectory();
        var logDir = Path.Combine(appDir, "Logs");

        if (!Directory.Exists(logDir))
        {
            Directory.CreateDirectory(logDir);
        }

        return logDir;
    }

    #endregion
}

#region 辅助类定义

/// <summary>
/// 应用程序域信息。
/// </summary>
public class AppDomainInfo
{
    /// <summary>
    /// 应用程序域友好名称。
    /// </summary>
    public string FriendlyName { get; set; } = string.Empty;

    /// <summary>
    /// 基目录。
    /// </summary>
    public string BaseDirectory { get; set; } = string.Empty;

    /// <summary>
    /// 相对搜索路径。
    /// </summary>
    public string RelativeSearchPath { get; set; } = string.Empty;

    /// <summary>
    /// 是否启用卷影复制。
    /// </summary>
    public bool ShadowCopyFiles { get; set; }

    /// <summary>
    /// 是否完全受信任。
    /// </summary>
    public bool IsFullyTrusted { get; set; }
}

/// <summary>
/// 操作系统信息。
/// </summary>
public class OperatingSystemInfo
{
    /// <summary>
    /// 操作系统平台。
    /// </summary>
    public string Platform { get; set; } = string.Empty;

    /// <summary>
    /// 操作系统版本。
    /// </summary>
    public string Version { get; set; } = string.Empty;

    /// <summary>
    /// 服务包版本。
    /// </summary>
    public string ServicePack { get; set; } = string.Empty;

    /// <summary>
    /// 是否为 64 位操作系统。
    /// </summary>
    public bool Is64Bit { get; set; }

    /// <summary>
    /// 计算机名称。
    /// </summary>
    public string MachineName { get; set; } = string.Empty;

    /// <summary>
    /// 当前用户名。
    /// </summary>
    public string UserName { get; set; } = string.Empty;

    /// <summary>
    /// 用户域名。
    /// </summary>
    public string UserDomainName { get; set; } = string.Empty;
}

/// <summary>
/// 处理器信息。
/// </summary>
public class ProcessorInfo
{
    /// <summary>
    /// 处理器核心数。
    /// </summary>
    public int ProcessorCount { get; set; }

    /// <summary>
    /// 处理器架构。
    /// </summary>
    public string ProcessorArchitecture { get; set; } = string.Empty;

    /// <summary>
    /// 操作系统架构。
    /// </summary>
    public string OSArchitecture { get; set; } = string.Empty;
}

/// <summary>
/// 磁盘驱动器信息。
/// </summary>
public class DiskDriveInfo
{
    /// <summary>
    /// 驱动器名称。
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// 驱动器类型。
    /// </summary>
    public string DriveType { get; set; } = string.Empty;

    /// <summary>
    /// 文件系统格式。
    /// </summary>
    public string DriveFormat { get; set; } = string.Empty;

    /// <summary>
    /// 总大小（字节）。
    /// </summary>
    public long TotalSize { get; set; }

    /// <summary>
    /// 可用空闲空间（字节）。
    /// </summary>
    public long AvailableFreeSpace { get; set; }

    /// <summary>
    /// 总空闲空间（字节）。
    /// </summary>
    public long TotalFreeSpace { get; set; }

    /// <summary>
    /// 卷标。
    /// </summary>
    public string VolumeLabel { get; set; } = string.Empty;
}

/// <summary>
/// 网络接口信息。
/// </summary>
public class NetworkInterfaceInfo
{
    /// <summary>
    /// 接口名称。
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// 接口描述。
    /// </summary>
    public string Description { get; set; } = string.Empty;

    /// <summary>
    /// 接口类型。
    /// </summary>
    public string Type { get; set; } = string.Empty;

    /// <summary>
    /// 速度（位/秒）。
    /// </summary>
    public long Speed { get; set; }

    /// <summary>
    /// 物理地址（MAC地址）。
    /// </summary>
    public string PhysicalAddress { get; set; } = string.Empty;

    /// <summary>
    /// 是否已连接。
    /// </summary>
    public bool IsOperational { get; set; }

    /// <summary>
    /// IP 地址列表。
    /// </summary>
    public List<string> IpAddresses { get; set; } = new();
}

/// <summary>
/// 运行时信息。
/// </summary>
public class RuntimeInfo
{
    /// <summary>
    /// 进程ID。
    /// </summary>
    public int ProcessId { get; set; }

    /// <summary>
    /// 进程名称。
    /// </summary>
    public string ProcessName { get; set; } = string.Empty;

    /// <summary>
    /// 启动时间。
    /// </summary>
    public DateTime StartTime { get; set; }

    /// <summary>
    /// 运行时长。
    /// </summary>
    public TimeSpan Uptime { get; set; }

    /// <summary>
    /// 工作集大小（字节）。
    /// </summary>
    public long WorkingSet { get; set; }

    /// <summary>
    /// 峰值工作集大小（字节）。
    /// </summary>
    public long PeakWorkingSet { get; set; }

    /// <summary>
    /// 私有内存大小（字节）。
    /// </summary>
    public long PrivateMemorySize { get; set; }

    /// <summary>
    /// 虚拟内存大小（字节）。
    /// </summary>
    public long VirtualMemorySize { get; set; }

    /// <summary>
    /// 线程数。
    /// </summary>
    public int Threads { get; set; }

    /// <summary>
    /// 句柄数。
    /// </summary>
    public int Handles { get; set; }
}

/// <summary>
/// 垃圾回收信息。
/// </summary>
public class GCInfo
{
    /// <summary>
    /// 第 0 代回收次数。
    /// </summary>
    public int CollectionCountGen0 { get; set; }

    /// <summary>
    /// 第 1 代回收次数。
    /// </summary>
    public int CollectionCountGen1 { get; set; }

    /// <summary>
    /// 第 2 代回收次数。
    /// </summary>
    public int CollectionCountGen2 { get; set; }

    /// <summary>
    /// 当前分配的内存总量（字节）。
    /// </summary>
    public long TotalMemory { get; set; }

    /// <summary>
    /// 最大代数。
    /// </summary>
    public int MaxGeneration { get; set; }
}

/// <summary>
/// 性能数据。
/// </summary>
public class PerformanceData
{
    /// <summary>
    /// 时间戳。
    /// </summary>
    public DateTime Timestamp { get; set; }

    /// <summary>
    /// 运行时信息。
    /// </summary>
    public RuntimeInfo RuntimeInfo { get; set; } = new();

    /// <summary>
    /// 垃圾回收信息。
    /// </summary>
    public GCInfo GCInfo { get; set; } = new();
}

/// <summary>
/// 资源使用情况快照。
/// </summary>
public class ResourceUsageSnapshot
{
    /// <summary>
    /// 时间戳。
    /// </summary>
    public DateTime Timestamp { get; set; }

    /// <summary>
    /// CPU 使用率百分比。
    /// </summary>
    public double CpuUsage { get; set; }

    /// <summary>
    /// 内存使用量（字节）。
    /// </summary>
    public long MemoryUsage { get; set; }

    /// <summary>
    /// 线程数。
    /// </summary>
    public int ThreadCount { get; set; }

    /// <summary>
    /// 句柄数。
    /// </summary>
    public int HandleCount { get; set; }
}

#endregion
