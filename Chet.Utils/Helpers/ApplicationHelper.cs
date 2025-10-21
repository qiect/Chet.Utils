using System.Diagnostics;
using System.Globalization;
using System.Net.NetworkInformation;
using System.Reflection;
using System.Runtime;
using System.Runtime.InteropServices;
using System.Security.Principal;

namespace Chet.Utils.Helpers
{
    /// <summary>
    /// 高级应用程序帮助类
    /// 提供应用程序信息获取、系统环境检测、运行时监控等高级功能
    /// </summary>
    public static class ApplicationHelper
    {
        #region 应用程序基本信息

        /// <summary>
        /// 获取当前应用程序的名称
        /// </summary>
        /// <returns>应用程序名称</returns>
        public static string GetApplicationName()
        {
            return Assembly.GetEntryAssembly()?.GetName().Name ?? "Unknown";
        }

        /// <summary>
        /// 获取当前应用程序的完整名称（包含版本信息）
        /// </summary>
        /// <returns>应用程序完整名称</returns>
        public static string GetApplicationFullName()
        {
            var assembly = Assembly.GetEntryAssembly();
            return assembly != null ? $"{assembly.GetName().Name} {assembly.GetName().Version}" : "Unknown";
        }

        /// <summary>
        /// 获取当前应用程序的版本
        /// </summary>
        /// <returns>应用程序版本</returns>
        public static Version GetApplicationVersion()
        {
            return Assembly.GetEntryAssembly()?.GetName().Version ?? new Version(0, 0, 0, 0);
        }

        /// <summary>
        /// 获取当前应用程序的启动路径
        /// </summary>
        /// <returns>应用程序启动路径</returns>
        public static string GetApplicationPath()
        {
            return AppDomain.CurrentDomain.BaseDirectory;
        }

        /// <summary>
        /// 获取当前应用程序的可执行文件路径
        /// </summary>
        /// <returns>可执行文件路径</returns>
        public static string GetExecutablePath()
        {
            return Assembly.GetEntryAssembly()?.Location ?? string.Empty;
        }

        /// <summary>
        /// 获取当前应用程序的配置文件路径
        /// </summary>
        /// <returns>配置文件路径</returns>
        public static string GetConfigurationPath()
        {
            var exePath = GetExecutablePath();
            return !string.IsNullOrEmpty(exePath) ? $"{exePath}.config" : string.Empty;
        }

        /// <summary>
        /// 获取应用程序域信息
        /// </summary>
        /// <returns>应用程序域信息</returns>
        public static AppDomainInfo GetAppDomainInfo()
        {
            var domain = AppDomain.CurrentDomain;
            return new AppDomainInfo
            {
                FriendlyName = domain.FriendlyName,
                BaseDirectory = domain.BaseDirectory,
                RelativeSearchPath = domain.RelativeSearchPath,
                ShadowCopyFiles = domain.ShadowCopyFiles,
                IsFullyTrusted = domain.IsFullyTrusted
            };
        }

        #endregion

        #region 系统环境信息

        /// <summary>
        /// 获取操作系统信息
        /// </summary>
        /// <returns>操作系统信息</returns>
        public static OperatingSystemInfo GetOperatingSystemInfo()
        {
            var os = Environment.OSVersion;
            return new OperatingSystemInfo
            {
                Platform = os.Platform.ToString(),
                Version = os.Version.ToString(),
                ServicePack = os.ServicePack,
                Is64Bit = Environment.Is64BitOperatingSystem,
                MachineName = Environment.MachineName,
                UserName = Environment.UserName,
                UserDomainName = Environment.UserDomainName
            };
        }

        /// <summary>
        /// 获取当前系统架构
        /// </summary>
        /// <returns>系统架构</returns>
        public static string GetSystemArchitecture()
        {
            return Environment.Is64BitOperatingSystem ? "x64" : "x86";
        }

        /// <summary>
        /// 获取当前运行时架构
        /// </summary>
        /// <returns>运行时架构</returns>
        public static string GetRuntimeArchitecture()
        {
            return Environment.Is64BitProcess ? "x64" : "x86";
        }

        /// <summary>
        /// 检查操作系统是否为Windows
        /// </summary>
        /// <returns>是否为Windows系统</returns>
        public static bool IsWindows()
        {
            return RuntimeInformation.IsOSPlatform(OSPlatform.Windows);
        }

        /// <summary>
        /// 检查操作系统是否为Linux
        /// </summary>
        /// <returns>是否为Linux系统</returns>
        public static bool IsLinux()
        {
            return RuntimeInformation.IsOSPlatform(OSPlatform.Linux);
        }

        /// <summary>
        /// 检查操作系统是否为macOS
        /// </summary>
        /// <returns>是否为macOS系统</returns>
        public static bool IsMacOS()
        {
            return RuntimeInformation.IsOSPlatform(OSPlatform.OSX);
        }

        /// <summary>
        /// 获取系统区域信息
        /// </summary>
        /// <returns>区域信息</returns>
        public static CultureInfo GetSystemCulture()
        {
            return CultureInfo.CurrentCulture;
        }

        /// <summary>
        /// 获取系统UI区域信息
        /// </summary>
        /// <returns>UI区域信息</returns>
        public static CultureInfo GetSystemUICulture()
        {
            return CultureInfo.CurrentUICulture;
        }

        /// <summary>
        /// 获取系统时区信息
        /// </summary>
        /// <returns>时区信息</returns>
        public static TimeZoneInfo GetSystemTimeZone()
        {
            return TimeZoneInfo.Local;
        }

        #endregion

        #region 硬件信息

        /// <summary>
        /// 获取处理器信息
        /// </summary>
        /// <returns>处理器信息</returns>
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
        /// 获取磁盘驱动器信息
        /// </summary>
        /// <returns>磁盘驱动器信息列表</returns>
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
                            DriveFormat = drive.DriveFormat,
                            TotalSize = drive.TotalSize,
                            AvailableFreeSpace = drive.AvailableFreeSpace,
                            TotalFreeSpace = drive.TotalFreeSpace
                        });
                    }
                }
            }
            catch
            {
                // 忽略驱动器访问错误
            }

            return drives;
        }

        /// <summary>
        /// 获取网络接口信息
        /// </summary>
        /// <returns>网络接口信息列表</returns>
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
            catch
            {
                // 忽略网络接口访问错误
            }

            return interfaces;
        }

        #endregion

        #region 运行时监控

        /// <summary>
        /// 获取应用程序运行时信息
        /// </summary>
        /// <returns>运行时信息</returns>
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
        /// 获取垃圾回收信息
        /// </summary>
        /// <returns>垃圾回收信息</returns>
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
        /// 强制执行垃圾回收
        /// </summary>
        /// <param name="blocking">是否阻塞直到回收完成</param>
        /// <param name="compacting">是否执行内存压缩</param>
        public static void ForceGarbageCollection(bool blocking = false, bool compacting = false)
        {
            if (compacting)
            {
                GCSettings.LargeObjectHeapCompactionMode = GCLargeObjectHeapCompactionMode.CompactOnce;
            }

            GC.Collect();

            if (blocking)
            {
                GC.WaitForPendingFinalizers();
                GC.Collect();
            }
        }

        /// <summary>
        /// 监控应用程序性能
        /// </summary>
        /// <param name="interval">监控间隔</param>
        /// <param name="callback">性能数据回调</param>
        /// <param name="cancellationToken">取消令牌</param>
        /// <returns>监控任务</returns>
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
        /// 获取应用程序资源使用情况快照
        /// </summary>
        /// <returns>资源使用情况</returns>
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
        /// 获取CPU使用率
        /// </summary>
        /// <param name="process">进程对象</param>
        /// <returns>CPU使用率百分比</returns>
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
        /// 重启当前应用程序
        /// </summary>
        public static void Restart()
        {
            var exePath = GetExecutablePath();
            if (!string.IsNullOrEmpty(exePath))
            {
                Process.Start(exePath);
                Environment.Exit(0);
            }
        }

        /// <summary>
        /// 退出当前应用程序
        /// </summary>
        /// <param name="exitCode">退出代码</param>
        public static void Exit(int exitCode = 0)
        {
            Environment.Exit(exitCode);
        }

        /// <summary>
        /// 检查是否以管理员权限运行
        /// </summary>
        /// <returns>是否以管理员权限运行</returns>
        public static bool IsRunningAsAdministrator()
        {
            try
            {
                var identity = WindowsIdentity.GetCurrent();
                var principal = new WindowsPrincipal(identity);
                return principal.IsInRole(WindowsBuiltInRole.Administrator);
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 以管理员权限重新启动应用程序
        /// </summary>
        public static void RestartAsAdministrator()
        {
            if (IsRunningAsAdministrator())
                return;

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
                }
            }
            catch
            {
                // 忽略启动错误
            }
        }

        /// <summary>
        /// 设置应用程序为单实例运行
        /// </summary>
        /// <param name="applicationName">应用程序名称</param>
        /// <returns>是否为第一个实例</returns>
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
        /// 启用高性能模式
        /// </summary>
        public static void EnableHighPerformanceMode()
        {
            // 设置进程优先级
            try
            {
                var process = Process.GetCurrentProcess();
                process.PriorityClass = ProcessPriorityClass.High;
            }
            catch
            {
                // 忽略权限错误
            }

            // 禁用系统睡眠
            SetThreadExecutionState(EXECUTION_STATE.ES_CONTINUOUS | EXECUTION_STATE.ES_SYSTEM_REQUIRED);
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

        #region 应用程序配置

        /// <summary>
        /// 获取应用程序启动参数
        /// </summary>
        /// <returns>启动参数数组</returns>
        public static string[] GetStartupArguments()
        {
            return Environment.GetCommandLineArgs();
        }

        /// <summary>
        /// 获取环境变量
        /// </summary>
        /// <param name="variableName">环境变量名称</param>
        /// <param name="target">环境变量目标</param>
        /// <returns>环境变量值</returns>
        public static string GetEnvironmentVariable(string variableName, EnvironmentVariableTarget target = EnvironmentVariableTarget.Process)
        {
            return Environment.GetEnvironmentVariable(variableName, target);
        }

        /// <summary>
        /// 设置环境变量
        /// </summary>
        /// <param name="variableName">环境变量名称</param>
        /// <param name="value">环境变量值</param>
        /// <param name="target">环境变量目标</param>
        public static void SetEnvironmentVariable(string variableName, string value, EnvironmentVariableTarget target = EnvironmentVariableTarget.Process)
        {
            Environment.SetEnvironmentVariable(variableName, value, target);
        }

        /// <summary>
        /// 获取所有环境变量
        /// </summary>
        /// <param name="target">环境变量目标</param>
        /// <returns>环境变量字典</returns>
        public static Dictionary<string, string> GetEnvironmentVariables(EnvironmentVariableTarget target = EnvironmentVariableTarget.Process)
        {
            var variables = Environment.GetEnvironmentVariables(target);
            var result = new Dictionary<string, string>();

            foreach (var key in variables.Keys)
            {
                result[key.ToString()] = variables[key]?.ToString() ?? string.Empty;
            }

            return result;
        }

        /// <summary>
        /// 获取特殊文件夹路径
        /// </summary>
        /// <param name="folder">特殊文件夹枚举</param>
        /// <returns>文件夹路径</returns>
        public static string GetSpecialFolderPath(Environment.SpecialFolder folder)
        {
            return Environment.GetFolderPath(folder);
        }

        /// <summary>
        /// 获取应用程序数据目录
        /// </summary>
        /// <returns>应用程序数据目录</returns>
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
        /// 获取临时文件目录
        /// </summary>
        /// <returns>临时文件目录</returns>
        public static string GetTempDirectory()
        {
            return Path.GetTempPath();
        }

        /// <summary>
        /// 创建临时文件
        /// </summary>
        /// <param name="extension">文件扩展名</param>
        /// <returns>临时文件路径</returns>
        public static string CreateTempFile(string extension = ".tmp")
        {
            return Path.GetTempFileName().Replace(".tmp", extension);
        }

        #endregion

        #region 应用程序诊断

        /// <summary>
        /// 生成应用程序诊断报告
        /// </summary>
        /// <returns>诊断报告</returns>
        public static DiagnosticReport GenerateDiagnosticReport()
        {
            return new DiagnosticReport
            {
                Timestamp = DateTime.UtcNow,
                ApplicationInfo = new ApplicationInfo
                {
                    Name = GetApplicationName(),
                    Version = GetApplicationVersion().ToString(),
                    Path = GetApplicationPath(),
                    Executable = GetExecutablePath()
                },
                SystemInfo = GetOperatingSystemInfo(),
                RuntimeInfo = GetRuntimeInfo(),
                ProcessorInfo = GetProcessorInfo(),
                GCInfo = GetGCInfo(),
                DiskDrives = GetDiskDriveInfo(),
                NetworkInterfaces = GetNetworkInterfaceInfo()
            };
        }

        /// <summary>
        /// 捕获应用程序异常
        /// </summary>
        /// <param name="exception">异常对象</param>
        /// <param name="additionalInfo">附加信息</param>
        /// <returns>异常报告</returns>
        public static ExceptionReport CaptureException(Exception exception, Dictionary<string, object> additionalInfo = null)
        {
            return new ExceptionReport
            {
                Timestamp = DateTime.UtcNow,
                ExceptionType = exception.GetType().FullName,
                Message = exception.Message,
                StackTrace = exception.StackTrace,
                InnerException = exception.InnerException?.Message,
                AdditionalInfo = additionalInfo ?? new Dictionary<string, object>()
            };
        }

        /// <summary>
        /// 记录应用程序日志
        /// </summary>
        /// <param name="message">日志消息</param>
        /// <param name="level">日志级别</param>
        /// <param name="category">日志类别</param>
        public static void Log(string message, LogLevel level = LogLevel.Info, string category = "General")
        {
            var logEntry = new LogEntry
            {
                Timestamp = DateTime.UtcNow,
                Level = level,
                Category = category,
                Message = message
            };

            // 这里可以实现具体的日志记录逻辑
            Console.WriteLine($"[{logEntry.Timestamp:yyyy-MM-dd HH:mm:ss}] [{level}] [{category}] {message}");
        }

        /// <summary>
        /// 启用应用程序健康检查
        /// </summary>
        /// <param name="checkInterval">检查间隔</param>
        /// <param name="healthCheck">健康检查函数</param>
        /// <param name="cancellationToken">取消令牌</param>
        /// <returns>健康检查任务</returns>
        public static async Task StartHealthCheckAsync(
            TimeSpan checkInterval,
            Func<Task<HealthStatus>> healthCheck,
            CancellationToken cancellationToken = default)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                try
                {
                    var status = await healthCheck();
                    Log($"健康检查: {status.Status} - {status.Message}",
                        status.IsHealthy ? LogLevel.Info : LogLevel.Warning);
                }
                catch (Exception ex)
                {
                    Log($"健康检查失败: {ex.Message}", LogLevel.Error);
                }

                try
                {
                    await Task.Delay(checkInterval, cancellationToken);
                }
                catch (OperationCanceledException)
                {
                    break;
                }
            }
        }

        #endregion
    }

    #region 数据结构和枚举

    /// <summary>
    /// 应用程序域信息
    /// </summary>
    public class AppDomainInfo
    {
        /// <summary>
        /// 友好名称
        /// </summary>
        public string FriendlyName { get; set; }

        /// <summary>
        /// 基目录
        /// </summary>
        public string BaseDirectory { get; set; }

        /// <summary>
        /// 相对搜索路径
        /// </summary>
        public string RelativeSearchPath { get; set; }

        /// <summary>
        /// 是否启用影子复制
        /// </summary>
        public bool ShadowCopyFiles { get; set; }

        /// <summary>
        /// 是否完全信任
        /// </summary>
        public bool IsFullyTrusted { get; set; }
    }

    /// <summary>
    /// 操作系统信息
    /// </summary>
    public class OperatingSystemInfo
    {
        /// <summary>
        /// 平台
        /// </summary>
        public string Platform { get; set; }

        /// <summary>
        /// 版本
        /// </summary>
        public string Version { get; set; }

        /// <summary>
        /// 服务包
        /// </summary>
        public string ServicePack { get; set; }

        /// <summary>
        /// 是否64位系统
        /// </summary>
        public bool Is64Bit { get; set; }

        /// <summary>
        /// 计算机名称
        /// </summary>
        public string MachineName { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 用户域名称
        /// </summary>
        public string UserDomainName { get; set; }
    }

    /// <summary>
    /// 处理器信息
    /// </summary>
    public class ProcessorInfo
    {
        /// <summary>
        /// 处理器核心数
        /// </summary>
        public int ProcessorCount { get; set; }

        /// <summary>
        /// 处理器架构
        /// </summary>
        public string ProcessorArchitecture { get; set; }

        /// <summary>
        /// 操作系统架构
        /// </summary>
        public string OSArchitecture { get; set; }
    }

    /// <summary>
    /// 内存信息
    /// </summary>
    public class MemoryInfo
    {
        /// <summary>
        /// 总物理内存（字节）
        /// </summary>
        public long TotalPhysicalMemory { get; set; }

        /// <summary>
        /// 可用物理内存（字节）
        /// </summary>
        public long AvailablePhysicalMemory { get; set; }

        /// <summary>
        /// 工作集大小（字节）
        /// </summary>
        public long WorkingSet { get; set; }

        /// <summary>
        /// GC内存（字节）
        /// </summary>
        public long GCMemory { get; set; }
    }

    /// <summary>
    /// 磁盘驱动器信息
    /// </summary>
    public class DiskDriveInfo
    {
        /// <summary>
        /// 驱动器名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 驱动器类型
        /// </summary>
        public string DriveType { get; set; }

        /// <summary>
        /// 驱动器格式
        /// </summary>
        public string DriveFormat { get; set; }

        /// <summary>
        /// 总大小（字节）
        /// </summary>
        public long TotalSize { get; set; }

        /// <summary>
        /// 可用空间（字节）
        /// </summary>
        public long AvailableFreeSpace { get; set; }

        /// <summary>
        /// 总空闲空间（字节）
        /// </summary>
        public long TotalFreeSpace { get; set; }
    }

    /// <summary>
    /// 网络接口信息
    /// </summary>
    public class NetworkInterfaceInfo
    {
        /// <summary>
        /// 接口名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 接口描述
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 接口类型
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// 速度
        /// </summary>
        public long Speed { get; set; }

        /// <summary>
        /// 物理地址
        /// </summary>
        public string PhysicalAddress { get; set; }

        /// <summary>
        /// 是否运行中
        /// </summary>
        public bool IsOperational { get; set; }

        /// <summary>
        /// IP地址列表
        /// </summary>
        public List<string> IpAddresses { get; set; } = new List<string>();
    }

    /// <summary>
    /// 运行时信息
    /// </summary>
    public class RuntimeInfo
    {
        /// <summary>
        /// 进程ID
        /// </summary>
        public int ProcessId { get; set; }

        /// <summary>
        /// 进程名称
        /// </summary>
        public string ProcessName { get; set; }

        /// <summary>
        /// 启动时间
        /// </summary>
        public DateTime StartTime { get; set; }

        /// <summary>
        /// 运行时间
        /// </summary>
        public TimeSpan Uptime { get; set; }

        /// <summary>
        /// 工作集大小（字节）
        /// </summary>
        public long WorkingSet { get; set; }

        /// <summary>
        /// 峰值工作集大小（字节）
        /// </summary>
        public long PeakWorkingSet { get; set; }

        /// <summary>
        /// 私有内存大小（字节）
        /// </summary>
        public long PrivateMemorySize { get; set; }

        /// <summary>
        /// 虚拟内存大小（字节）
        /// </summary>
        public long VirtualMemorySize { get; set; }

        /// <summary>
        /// 线程数
        /// </summary>
        public int Threads { get; set; }

        /// <summary>
        /// 句柄数
        /// </summary>
        public int Handles { get; set; }
    }

    /// <summary>
    /// 垃圾回收信息
    /// </summary>
    public class GCInfo
    {
        /// <summary>
        /// 第0代回收次数
        /// </summary>
        public int CollectionCountGen0 { get; set; }

        /// <summary>
        /// 第1代回收次数
        /// </summary>
        public int CollectionCountGen1 { get; set; }

        /// <summary>
        /// 第2代回收次数
        /// </summary>
        public int CollectionCountGen2 { get; set; }

        /// <summary>
        /// 总内存（字节）
        /// </summary>
        public long TotalMemory { get; set; }

        /// <summary>
        /// 最大代数
        /// </summary>
        public int MaxGeneration { get; set; }
    }

    /// <summary>
    /// 性能数据
    /// </summary>
    public class PerformanceData
    {
        /// <summary>
        /// 时间戳
        /// </summary>
        public DateTime Timestamp { get; set; }

        /// <summary>
        /// 运行时信息
        /// </summary>
        public RuntimeInfo RuntimeInfo { get; set; }

        /// <summary>
        /// 内存信息
        /// </summary>
        public MemoryInfo MemoryInfo { get; set; }

        /// <summary>
        /// GC信息
        /// </summary>
        public GCInfo GCInfo { get; set; }
    }

    /// <summary>
    /// 资源使用情况快照
    /// </summary>
    public class ResourceUsageSnapshot
    {
        /// <summary>
        /// 时间戳
        /// </summary>
        public DateTime Timestamp { get; set; }

        /// <summary>
        /// CPU使用率（百分比）
        /// </summary>
        public double CpuUsage { get; set; }

        /// <summary>
        /// 内存使用量（字节）
        /// </summary>
        public long MemoryUsage { get; set; }

        /// <summary>
        /// 线程数
        /// </summary>
        public int ThreadCount { get; set; }

        /// <summary>
        /// 句柄数
        /// </summary>
        public int HandleCount { get; set; }
    }

    /// <summary>
    /// 诊断报告
    /// </summary>
    public class DiagnosticReport
    {
        /// <summary>
        /// 时间戳
        /// </summary>
        public DateTime Timestamp { get; set; }

        /// <summary>
        /// 应用程序信息
        /// </summary>
        public ApplicationInfo ApplicationInfo { get; set; }

        /// <summary>
        /// 系统信息
        /// </summary>
        public OperatingSystemInfo SystemInfo { get; set; }

        /// <summary>
        /// 运行时信息
        /// </summary>
        public RuntimeInfo RuntimeInfo { get; set; }

        /// <summary>
        /// 处理器信息
        /// </summary>
        public ProcessorInfo ProcessorInfo { get; set; }

        /// <summary>
        /// GC信息
        /// </summary>
        public GCInfo GCInfo { get; set; }

        /// <summary>
        /// 磁盘驱动器信息
        /// </summary>
        public List<DiskDriveInfo> DiskDrives { get; set; } = new List<DiskDriveInfo>();

        /// <summary>
        /// 网络接口信息
        /// </summary>
        public List<NetworkInterfaceInfo> NetworkInterfaces { get; set; } = new List<NetworkInterfaceInfo>();
    }

    /// <summary>
    /// 应用程序信息
    /// </summary>
    public class ApplicationInfo
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 版本
        /// </summary>
        public string Version { get; set; }

        /// <summary>
        /// 路径
        /// </summary>
        public string Path { get; set; }

        /// <summary>
        /// 可执行文件
        /// </summary>
        public string Executable { get; set; }
    }

    /// <summary>
    /// 异常报告
    /// </summary>
    public class ExceptionReport
    {
        /// <summary>
        /// 时间戳
        /// </summary>
        public DateTime Timestamp { get; set; }

        /// <summary>
        /// 异常类型
        /// </summary>
        public string ExceptionType { get; set; }

        /// <summary>
        /// 消息
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// 堆栈跟踪
        /// </summary>
        public string StackTrace { get; set; }

        /// <summary>
        /// 内部异常
        /// </summary>
        public string InnerException { get; set; }

        /// <summary>
        /// 附加信息
        /// </summary>
        public Dictionary<string, object> AdditionalInfo { get; set; } = new Dictionary<string, object>();
    }

    /// <summary>
    /// 日志条目
    /// </summary>
    public class LogEntry
    {
        /// <summary>
        /// 时间戳
        /// </summary>
        public DateTime Timestamp { get; set; }

        /// <summary>
        /// 日志级别
        /// </summary>
        public LogLevel Level { get; set; }

        /// <summary>
        /// 类别
        /// </summary>
        public string Category { get; set; }

        /// <summary>
        /// 消息
        /// </summary>
        public string Message { get; set; }
    }

    /// <summary>
    /// 日志级别枚举
    /// </summary>
    public enum LogLevel
    {
        /// <summary>
        /// 调试
        /// </summary>
        Debug,

        /// <summary>
        /// 信息
        /// </summary>
        Info,

        /// <summary>
        /// 警告
        /// </summary>
        Warning,

        /// <summary>
        /// 错误
        /// </summary>
        Error,

        /// <summary>
        /// 致命错误
        /// </summary>
        Fatal
    }

    /// <summary>
    /// 健康状态
    /// </summary>
    public class HealthStatus
    {
        /// <summary>
        /// 是否健康
        /// </summary>
        public bool IsHealthy { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// 消息
        /// </summary>
        public string Message { get; set; }
    }

    #endregion
}