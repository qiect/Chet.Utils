using System.Diagnostics;
using System.Runtime.InteropServices;
using Xunit;

namespace Chet.Utils.Helpers.Tests;

public class ApplicationHelperTests
{
    #region 应用程序基本信息测试

    [Fact]
    public void GetApplicationName_ReturnsNonEmptyString()
    {
        var name = ApplicationHelper.GetApplicationName();
        Assert.NotNull(name);
        Assert.NotEmpty(name);
    }

    [Fact]
    public void GetApplicationFullName_ReturnsValidFormat()
    {
        var fullName = ApplicationHelper.GetApplicationFullName();
        Assert.NotNull(fullName);
        Assert.NotEmpty(fullName);
    }

    [Fact]
    public void GetApplicationVersion_ReturnsValidVersion()
    {
        var version = ApplicationHelper.GetApplicationVersion();
        Assert.NotNull(version);
        Assert.True(version.Major >= 0);
    }

    [Fact]
    public void GetApplicationVersionString_WithDefaultFieldCount_ReturnsValidString()
    {
        var versionStr = ApplicationHelper.GetApplicationVersionString();
        Assert.NotNull(versionStr);
        Assert.NotEmpty(versionStr);
    }

    [Theory]
    [InlineData(1)]
    [InlineData(2)]
    [InlineData(3)]
    [InlineData(4)]
    public void GetApplicationVersionString_WithValidFieldCount_ReturnsCorrectFormat(int fieldCount)
    {
        var versionStr = ApplicationHelper.GetApplicationVersionString(fieldCount);
        Assert.NotNull(versionStr);
        var parts = versionStr.Split('.');
        Assert.Equal(fieldCount, parts.Length);
    }

    [Fact]
    public void GetApplicationPath_ReturnsValidPath()
    {
        var path = ApplicationHelper.GetApplicationPath();
        Assert.NotNull(path);
        Assert.True(Directory.Exists(path));
    }

    [Fact]
    public void GetProcessId_ReturnsPositiveValue()
    {
        var processId = ApplicationHelper.GetProcessId();
        Assert.True(processId > 0);
    }

    [Fact]
    public void GetProcessName_ReturnsNonEmptyString()
    {
        var processName = ApplicationHelper.GetProcessName();
        Assert.NotNull(processName);
        Assert.NotEmpty(processName);
    }

    [Fact]
    public void GetStartTime_ReturnsValidDateTime()
    {
        var startTime = ApplicationHelper.GetStartTime();
        Assert.True(startTime <= DateTime.Now);
        Assert.True(startTime > DateTime.MinValue);
    }

    [Fact]
    public void GetUptime_ReturnsPositiveTimeSpan()
    {
        var uptime = ApplicationHelper.GetUptime();
        Assert.True(uptime >= TimeSpan.Zero);
    }

    [Fact]
    public void GetAppDomainInfo_ReturnsValidInfo()
    {
        var info = ApplicationHelper.GetAppDomainInfo();
        Assert.NotNull(info);
        Assert.NotNull(info.FriendlyName);
        Assert.NotNull(info.BaseDirectory);
    }

    #endregion

    #region 系统环境信息测试

    [Fact]
    public void GetOperatingSystemInfo_ReturnsValidInfo()
    {
        var osInfo = ApplicationHelper.GetOperatingSystemInfo();
        Assert.NotNull(osInfo);
        Assert.NotNull(osInfo.Platform);
        Assert.NotNull(osInfo.Version);
        Assert.NotNull(osInfo.MachineName);
        Assert.NotNull(osInfo.UserName);
    }

    [Fact]
    public void GetOSDescription_ReturnsNonEmptyString()
    {
        var osDesc = ApplicationHelper.GetOSDescription();
        Assert.NotNull(osDesc);
        Assert.NotEmpty(osDesc);
    }

    [Fact]
    public void GetSystemArchitecture_ReturnsValidArchitecture()
    {
        var arch = ApplicationHelper.GetSystemArchitecture();
        Assert.True(arch == "x64" || arch == "x86");
    }

    [Fact]
    public void GetRuntimeArchitecture_ReturnsValidArchitecture()
    {
        var arch = ApplicationHelper.GetRuntimeArchitecture();
        Assert.True(arch == "x64" || arch == "x86");
    }

    [Fact]
    public void GetFrameworkDescription_ReturnsNonEmptyString()
    {
        var framework = ApplicationHelper.GetFrameworkDescription();
        Assert.NotNull(framework);
        Assert.NotEmpty(framework);
    }

    [Fact]
    public void IsWindows_ReturnsConsistentValue()
    {
        var isWindows = ApplicationHelper.IsWindows();
        Assert.Equal(RuntimeInformation.IsOSPlatform(OSPlatform.Windows), isWindows);
    }

    [Fact]
    public void IsLinux_ReturnsConsistentValue()
    {
        var isLinux = ApplicationHelper.IsLinux();
        Assert.Equal(RuntimeInformation.IsOSPlatform(OSPlatform.Linux), isLinux);
    }

    [Fact]
    public void IsMacOS_ReturnsConsistentValue()
    {
        var isMacOS = ApplicationHelper.IsMacOS();
        Assert.Equal(RuntimeInformation.IsOSPlatform(OSPlatform.OSX), isMacOS);
    }

    [Fact]
    public void GetCurrentPlatform_ReturnsValidPlatform()
    {
        var platform = ApplicationHelper.GetCurrentPlatform();
        Assert.NotNull(platform);
        Assert.True(platform == "Windows" || platform == "Linux" || platform == "macOS" || platform == "Unknown");
    }

    [Fact]
    public void GetSystemCulture_ReturnsNonNullCulture()
    {
        var culture = ApplicationHelper.GetSystemCulture();
        Assert.NotNull(culture);
    }

    [Fact]
    public void GetSystemUICulture_ReturnsNonNullCulture()
    {
        var uiCulture = ApplicationHelper.GetSystemUICulture();
        Assert.NotNull(uiCulture);
    }

    [Fact]
    public void GetSystemTimeZone_ReturnsNonNullTimeZone()
    {
        var timeZone = ApplicationHelper.GetSystemTimeZone();
        Assert.NotNull(timeZone);
    }

    [Fact]
    public void GetSystemTimeZoneName_ReturnsNonEmptyString()
    {
        var tzName = ApplicationHelper.GetSystemTimeZoneName();
        Assert.NotNull(tzName);
        Assert.NotEmpty(tzName);
    }

    [Fact]
    public void GetMachineName_ReturnsNonEmptyString()
    {
        var machineName = ApplicationHelper.GetMachineName();
        Assert.NotNull(machineName);
        Assert.NotEmpty(machineName);
    }

    [Fact]
    public void GetUserName_ReturnsNonEmptyString()
    {
        var userName = ApplicationHelper.GetUserName();
        Assert.NotNull(userName);
        Assert.NotEmpty(userName);
    }

    [Fact]
    public void GetUserDomainName_ReturnsNonEmptyString()
    {
        var domain = ApplicationHelper.GetUserDomainName();
        Assert.NotNull(domain);
        Assert.NotEmpty(domain);
    }

    [Fact]
    public void GetSystemDirectory_ReturnsValidDirectory()
    {
        var systemDir = ApplicationHelper.GetSystemDirectory();
        Assert.NotNull(systemDir);
        Assert.True(Directory.Exists(systemDir));
    }

    [Fact]
    public void GetTickCount_ReturnsPositiveValue()
    {
        var tickCount = ApplicationHelper.GetTickCount();
        Assert.True(tickCount >= 0);
    }

    #endregion

    #region 硬件信息测试

    [Fact]
    public void GetProcessorInfo_ReturnsValidInfo()
    {
        var cpuInfo = ApplicationHelper.GetProcessorInfo();
        Assert.NotNull(cpuInfo);
        Assert.True(cpuInfo.ProcessorCount > 0);
        Assert.NotNull(cpuInfo.ProcessorArchitecture);
        Assert.NotNull(cpuInfo.OSArchitecture);
    }

    [Fact]
    public void GetProcessorCount_ReturnsPositiveValue()
    {
        var count = ApplicationHelper.GetProcessorCount();
        Assert.True(count > 0);
    }

    [Fact]
    public void GetAvailableMemory_ReturnsPositiveValue()
    {
        var memory = ApplicationHelper.GetAvailableMemory();
        Assert.True(memory > 0);
    }

    [Fact]
    public void GetDiskDriveInfo_ReturnsNonEmptyList()
    {
        var drives = ApplicationHelper.GetDiskDriveInfo();
        Assert.NotNull(drives);
        Assert.NotEmpty(drives);
    }

    [Fact]
    public void GetDiskDriveInfo_WithValidDrive_ReturnsInfo()
    {
        var drives = ApplicationHelper.GetDiskDriveInfo();
        if (drives.Count > 0)
        {
            var firstDrive = drives[0];
            var driveInfo = ApplicationHelper.GetDiskDriveInfo(firstDrive.Name);
            Assert.NotNull(driveInfo);
            Assert.Equal(firstDrive.Name, driveInfo.Name);
        }
    }

    [Fact]
    public void GetNetworkInterfaceInfo_ReturnsValidList()
    {
        var interfaces = ApplicationHelper.GetNetworkInterfaceInfo();
        Assert.NotNull(interfaces);
    }

    [Fact]
    public void HasNetworkConnection_ReturnsBoolean()
    {
        var hasConnection = ApplicationHelper.HasNetworkConnection();
        Assert.IsType<bool>(hasConnection);
    }

    #endregion

    #region 运行时监控测试

    [Fact]
    public void GetRuntimeInfo_ReturnsValidInfo()
    {
        var runtimeInfo = ApplicationHelper.GetRuntimeInfo();
        Assert.NotNull(runtimeInfo);
        Assert.True(runtimeInfo.ProcessId > 0);
        Assert.NotNull(runtimeInfo.ProcessName);
        Assert.True(runtimeInfo.WorkingSet > 0);
        Assert.True(runtimeInfo.Threads > 0);
        Assert.True(runtimeInfo.Handles > 0);
    }

    [Fact]
    public void GetGCInfo_ReturnsValidInfo()
    {
        var gcInfo = ApplicationHelper.GetGCInfo();
        Assert.NotNull(gcInfo);
        Assert.True(gcInfo.TotalMemory > 0);
        Assert.True(gcInfo.MaxGeneration >= 0);
        Assert.True(gcInfo.CollectionCountGen0 >= 0);
        Assert.True(gcInfo.CollectionCountGen1 >= 0);
        Assert.True(gcInfo.CollectionCountGen2 >= 0);
    }

    [Fact]
    public void GetMemoryUsage_ReturnsPositiveValue()
    {
        var memoryUsage = ApplicationHelper.GetMemoryUsage();
        Assert.True(memoryUsage > 0);
    }

    [Fact]
    public void GetPeakMemoryUsage_ReturnsPositiveValue()
    {
        var peakMemory = ApplicationHelper.GetPeakMemoryUsage();
        Assert.True(peakMemory > 0);
    }

    [Fact]
    public void GetThreadCount_ReturnsPositiveValue()
    {
        var threadCount = ApplicationHelper.GetThreadCount();
        Assert.True(threadCount > 0);
    }

    [Fact]
    public void GetHandleCount_ReturnsPositiveValue()
    {
        var handleCount = ApplicationHelper.GetHandleCount();
        Assert.True(handleCount > 0);
    }

    [Fact]
    public void ForceGarbageCollection_DoesNotThrow()
    {
        var exception = Record.Exception(() => ApplicationHelper.ForceGarbageCollection());
        Assert.Null(exception);
    }

    [Fact]
    public void GetResourceUsageSnapshot_ReturnsValidSnapshot()
    {
        var snapshot = ApplicationHelper.GetResourceUsageSnapshot();
        Assert.NotNull(snapshot);
        Assert.True(snapshot.MemoryUsage > 0);
        Assert.True(snapshot.ThreadCount > 0);
        Assert.True(snapshot.HandleCount > 0);
        Assert.True(snapshot.CpuUsage >= 0 && snapshot.CpuUsage <= 100);
    }

    #endregion

    #region 环境变量测试

    [Fact]
    public void GetEnvironmentVariable_ExistingVariable_ReturnsValue()
    {
        var path = ApplicationHelper.GetEnvironmentVariable("PATH");
        Assert.NotNull(path);
    }

    [Fact]
    public void GetEnvironmentVariable_NonExistingVariable_ReturnsNull()
    {
        var value = ApplicationHelper.GetEnvironmentVariable("NON_EXISTING_VARIABLE_12345");
        Assert.Null(value);
    }

    [Fact]
    public void GetEnvironmentVariables_ReturnsNonEmptyDictionary()
    {
        var variables = ApplicationHelper.GetEnvironmentVariables();
        Assert.NotNull(variables);
        Assert.True(variables.Count > 0);
    }

    [Fact]
    public void SetEnvironmentVariable_SetsValueCorrectly()
    {
        var testName = "TEST_VARIABLE_12345";
        var testValue = "TestValue123";

        ApplicationHelper.SetEnvironmentVariable(testName, testValue);
        var retrievedValue = ApplicationHelper.GetEnvironmentVariable(testName);

        Assert.Equal(testValue, retrievedValue);

        ApplicationHelper.SetEnvironmentVariable(testName, null);
    }

    #endregion
}
