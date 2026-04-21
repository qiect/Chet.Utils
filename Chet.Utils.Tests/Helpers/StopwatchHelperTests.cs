using System.Diagnostics;
using Xunit;

namespace Chet.Utils.Helpers.Tests;

public class StopwatchHelperTests
{
    #region 基础计时方法测试

    [Fact]
    public void Time_ExecutesActionAndReturnsElapsedMilliseconds()
    {
        var elapsedMs = StopwatchHelper.Time(() => Thread.Sleep(50));

        Assert.True(elapsedMs >= 40);
        Assert.True(elapsedMs < 200);
    }

    [Fact]
    public void Time_WithIterations_ExecutesMultipleTimes()
    {
        var counter = 0;
        var elapsedMs = StopwatchHelper.Time(() => counter++, 10);

        Assert.Equal(10, counter);
        Assert.True(elapsedMs >= 0);
    }

    [Fact]
    public async Task TimeAsync_ExecutesAsyncActionAndReturnsElapsedMilliseconds()
    {
        var elapsedMs = await StopwatchHelper.TimeAsync(async () =>
        {
            await Task.Delay(50);
        });

        Assert.True(elapsedMs >= 40);
        Assert.True(elapsedMs < 200);
    }

    [Fact]
    public async Task TimeAsync_WithIterations_ExecutesMultipleTimes()
    {
        var counter = 0;
        var elapsedMs = await StopwatchHelper.TimeAsync(async () =>
        {
            counter++;
            await Task.CompletedTask;
        }, 10);

        Assert.Equal(10, counter);
        Assert.True(elapsedMs >= 0);
    }

    [Fact]
    public void TimeDetailed_ReturnsTimeSpan()
    {
        var elapsed = StopwatchHelper.TimeDetailed(() => Thread.Sleep(50));

        Assert.True(elapsed.TotalMilliseconds >= 40);
        Assert.True(elapsed.TotalMilliseconds < 200);
    }

    [Fact]
    public async Task TimeDetailedAsync_ReturnsTimeSpan()
    {
        var elapsed = await StopwatchHelper.TimeDetailedAsync(async () =>
        {
            await Task.Delay(50);
        });

        Assert.True(elapsed.TotalMilliseconds >= 40);
        Assert.True(elapsed.TotalMilliseconds < 200);
    }

    [Fact]
    public void Time_WithReturnValue_ReturnsResultAndElapsed()
    {
        var (result, elapsedMs) = StopwatchHelper.Time(() => 42);

        Assert.Equal(42, result);
        Assert.True(elapsedMs >= 0);
    }

    [Fact]
    public async Task TimeAsync_WithReturnValue_ReturnsResultAndElapsed()
    {
        var (result, elapsedMs) = await StopwatchHelper.TimeAsync(async () =>
        {
            await Task.Delay(10);
            return 42;
        });

        Assert.Equal(42, result);
        Assert.True(elapsedMs >= 0);
    }

    [Fact]
    public void Time_NullAction_ThrowsArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>(() => StopwatchHelper.Time(null!));
    }

    [Fact]
    public void Time_InvalidIterations_ThrowsArgumentException()
    {
        Assert.Throws<ArgumentException>(() => StopwatchHelper.Time(() => { }, 0));
        Assert.Throws<ArgumentException>(() => StopwatchHelper.Time(() => { }, -1));
    }

    #endregion

    #region 统计计时方法测试

    [Fact]
    public void TimeStatistics_ReturnsValidStatistics()
    {
        var stats = StopwatchHelper.TimeStatistics(() => Thread.Sleep(5), 10);

        Assert.NotNull(stats);
        Assert.Equal(10, stats.Count);
        Assert.True(stats.TotalMilliseconds >= 0);
        Assert.True(stats.AverageMilliseconds >= 0);
        Assert.True(stats.MinMilliseconds >= 0);
        Assert.True(stats.MaxMilliseconds >= 0);
        Assert.True(stats.MaxMilliseconds >= stats.MinMilliseconds);
    }

    [Fact]
    public async Task TimeStatisticsAsync_ReturnsValidStatistics()
    {
        var stats = await StopwatchHelper.TimeStatisticsAsync(async () =>
        {
            await Task.Delay(5);
        }, 10);

        Assert.NotNull(stats);
        Assert.Equal(10, stats.Count);
        Assert.True(stats.TotalMilliseconds >= 0);
        Assert.True(stats.AverageMilliseconds >= 0);
    }

    [Fact]
    public void TimeEach_ReturnsListOfTimes()
    {
        var times = StopwatchHelper.TimeEach(() => Thread.Sleep(1), 5);

        Assert.NotNull(times);
        Assert.Equal(5, times.Count);
        Assert.All(times, t => Assert.True(t >= 0));
    }

    [Fact]
    public async Task TimeEachAsync_ReturnsListOfTimes()
    {
        var times = await StopwatchHelper.TimeEachAsync(async () =>
        {
            await Task.Delay(1);
        }, 5);

        Assert.NotNull(times);
        Assert.Equal(5, times.Count);
        Assert.All(times, t => Assert.True(t >= 0));
    }

    [Fact]
    public void CalculateStatistics_ReturnsCorrectValues()
    {
        var times = new List<long> { 10, 20, 30, 40, 50 };

        var stats = StopwatchHelper.CalculateStatistics(times);

        Assert.Equal(5, stats.Count);
        Assert.Equal(150, stats.TotalMilliseconds);
        Assert.Equal(30, stats.AverageMilliseconds);
        Assert.Equal(10, stats.MinMilliseconds);
        Assert.Equal(50, stats.MaxMilliseconds);
        Assert.Equal(30, stats.MedianMilliseconds);
    }

    [Fact]
    public void CalculateStatistics_EmptyList_ReturnsEmptyStats()
    {
        var stats = StopwatchHelper.CalculateStatistics(new List<long>());

        Assert.Equal(0, stats.Count);
    }

    [Fact]
    public void CalculateStatistics_SingleValue_ReturnsCorrectStats()
    {
        var stats = StopwatchHelper.CalculateStatistics(new List<long> { 100 });

        Assert.Equal(1, stats.Count);
        Assert.Equal(100, stats.TotalMilliseconds);
        Assert.Equal(100, stats.AverageMilliseconds);
        Assert.Equal(100, stats.MinMilliseconds);
        Assert.Equal(100, stats.MaxMilliseconds);
        Assert.Equal(100, stats.MedianMilliseconds);
    }

    [Fact]
    public void CalculateStatistics_Percentiles_CalculatedCorrectly()
    {
        var times = Enumerable.Range(1, 100).Select(i => (long)i).ToList();

        var stats = StopwatchHelper.CalculateStatistics(times);

        Assert.True(stats.P90Milliseconds >= 90);
        Assert.True(stats.P95Milliseconds >= 95);
        Assert.True(stats.P99Milliseconds >= 99);
    }

    #endregion

    #region 高精度计时方法测试

    [Fact]
    public void TimeHighPrecision_ReturnsTicks()
    {
        var ticks = StopwatchHelper.TimeHighPrecision(() => { });

        Assert.True(ticks >= 0);
    }

    [Fact]
    public void TimeNanoseconds_ReturnsNanoseconds()
    {
        var nanoseconds = StopwatchHelper.TimeNanoseconds(() => { });

        Assert.True(nanoseconds >= 0);
    }

    [Fact]
    public void TimeMicroseconds_ReturnsMicroseconds()
    {
        var microseconds = StopwatchHelper.TimeMicroseconds(() => { });

        Assert.True(microseconds >= 0);
    }

    [Fact]
    public void GetFrequency_ReturnsPositiveValue()
    {
        var frequency = StopwatchHelper.GetFrequency();

        Assert.True(frequency > 0);
    }

    [Fact]
    public void IsHighResolution_ReturnsBoolean()
    {
        var isHighRes = StopwatchHelper.IsHighResolution();

        Assert.IsType<bool>(isHighRes);
    }

    [Fact]
    public void GetTimestamp_ReturnsPositiveValue()
    {
        var timestamp = StopwatchHelper.GetTimestamp();

        Assert.True(timestamp > 0);
    }

    [Fact]
    public void GetElapsedTime_WithStartAndEnd_ReturnsTimeSpan()
    {
        var start = StopwatchHelper.GetTimestamp();
        Thread.Sleep(10);
        var end = StopwatchHelper.GetTimestamp();

        var elapsed = StopwatchHelper.GetElapsedTime(start, end);

        Assert.True(elapsed.TotalMilliseconds >= 8);
        Assert.True(elapsed.TotalMilliseconds < 100);
    }

    [Fact]
    public void GetElapsedTime_WithStart_ReturnsTimeSpan()
    {
        var start = StopwatchHelper.GetTimestamp();
        Thread.Sleep(10);

        var elapsed = StopwatchHelper.GetElapsedTime(start);

        Assert.True(elapsed.TotalMilliseconds >= 8);
    }

    #endregion

    #region 条件计时方法测试

    [Fact]
    public void TimeIf_ConditionTrue_ExecutesAndTimes()
    {
        var executed = false;
        var elapsedMs = StopwatchHelper.TimeIf(true, () => executed = true);

        Assert.True(executed);
        Assert.True(elapsedMs >= 0);
    }

    [Fact]
    public void TimeIf_ConditionFalse_DoesNotExecute()
    {
        var executed = false;
        var elapsedMs = StopwatchHelper.TimeIf(false, () => executed = true);

        Assert.False(executed);
        Assert.Equal(-1, elapsedMs);
    }

    [Fact]
    public async Task TimeIfAsync_ConditionTrue_ExecutesAndTimes()
    {
        var executed = false;
        var elapsedMs = await StopwatchHelper.TimeIfAsync(true, async () =>
        {
            executed = true;
            await Task.CompletedTask;
        });

        Assert.True(executed);
        Assert.True(elapsedMs >= 0);
    }

    [Fact]
    public async Task TimeIfAsync_ConditionFalse_DoesNotExecute()
    {
        var executed = false;
        var elapsedMs = await StopwatchHelper.TimeIfAsync(false, async () =>
        {
            executed = true;
            await Task.CompletedTask;
        });

        Assert.False(executed);
        Assert.Equal(-1, elapsedMs);
    }

    [Fact]
    public void TimeWithTimeout_CompletesWithinTimeout_ReturnsResult()
    {
        var result = StopwatchHelper.TimeWithTimeout(() => 42, 1000);

        Assert.NotNull(result);
        Assert.Equal(42, result.Value.Result);
        Assert.True(result.Value.ElapsedMilliseconds >= 0);
    }

    [Fact]
    public void TimeWithTimeout_ExceedsTimeout_ReturnsNull()
    {
        var result = StopwatchHelper.TimeWithTimeout(() =>
        {
            Thread.Sleep(500);
            return 42;
        }, 10);

        Assert.Null(result);
    }

    [Fact]
    public void TimeWithRetry_SuccessOnFirstTry_ReturnsSuccess()
    {
        var attempts = 0;
        var (success, elapsedMs, retryCount) = StopwatchHelper.TimeWithRetry(() =>
        {
            attempts++;
        }, 3);

        Assert.True(success);
        Assert.Equal(0, retryCount);
        Assert.Equal(1, attempts);
    }

    [Fact]
    public void TimeWithRetry_FailsThenSucceeds_ReturnsSuccessWithRetries()
    {
        var attempts = 0;
        var (success, elapsedMs, retryCount) = StopwatchHelper.TimeWithRetry(() =>
        {
            attempts++;
            if (attempts < 3)
                throw new Exception("Test exception");
        }, 5, 10);

        Assert.True(success);
        Assert.True(retryCount >= 1);
        Assert.Equal(3, attempts);
    }

    [Fact]
    public void TimeWithRetry_AllAttemptsFail_ReturnsFailure()
    {
        var attempts = 0;
        var (success, elapsedMs, retryCount) = StopwatchHelper.TimeWithRetry(() =>
        {
            attempts++;
            throw new Exception("Test exception");
        }, 2, 10);

        Assert.False(success);
        Assert.Equal(2, retryCount);
        Assert.Equal(3, attempts);
    }

    #endregion

    #region 范围计时方法测试

    [Fact]
    public void StartScope_WithAction_ExecutesOnDispose()
    {
        TimeSpan? capturedElapsed = null;

        using (var timer = StopwatchHelper.StartScope(elapsed => capturedElapsed = elapsed))
        {
            Thread.Sleep(10);
        }

        Assert.NotNull(capturedElapsed);
        Assert.True(capturedElapsed.Value.TotalMilliseconds >= 8);
    }

    [Fact]
    public void StartScope_WithLabel_WritesToConsole()
    {
        using (var timer = StopwatchHelper.StartScope("TestLabel"))
        {
            Thread.Sleep(10);
        }
    }

    [Fact]
    public void StartScope_NoAction_CreatesTimer()
    {
        using (var timer = StopwatchHelper.StartScope())
        {
            Thread.Sleep(10);
            Assert.True(timer.ElapsedMilliseconds >= 8);
        }
    }

    #endregion

    #region 基准测试方法测试

    [Fact]
    public void Benchmark_ReturnsValidResult()
    {
        var result = StopwatchHelper.Benchmark("Test", () => { }, iterations: 100, warmupIterations: 10);

        Assert.NotNull(result);
        Assert.Equal("Test", result.Name);
        Assert.Equal(100, result.Iterations);
        Assert.NotNull(result.Statistics);
        Assert.Equal(100, result.Statistics.Count);
    }

    [Fact]
    public async Task BenchmarkAsync_ReturnsValidResult()
    {
        var result = await StopwatchHelper.BenchmarkAsync("Test", async () =>
        {
            await Task.CompletedTask;
        }, iterations: 100, warmupIterations: 10);

        Assert.NotNull(result);
        Assert.Equal("Test", result.Name);
        Assert.Equal(100, result.Iterations);
        Assert.NotNull(result.Statistics);
    }

    [Fact]
    public void Compare_ReturnsComparisonResult()
    {
        var comparison = StopwatchHelper.Compare(
            "Method1", () => { },
            "Method2", () => { },
            iterations: 100);

        Assert.NotNull(comparison);
        Assert.NotNull(comparison.Result1);
        Assert.NotNull(comparison.Result2);
        Assert.Equal("Method1", comparison.Result1.Name);
        Assert.Equal("Method2", comparison.Result2.Name);
    }

    [Fact]
    public void Benchmark_InvalidIterations_ThrowsArgumentException()
    {
        Assert.Throws<ArgumentException>(() => StopwatchHelper.Benchmark("Test", () => { }, iterations: 0));
    }

    [Fact]
    public void Benchmark_NegativeWarmupIterations_ThrowsArgumentException()
    {
        Assert.Throws<ArgumentException>(() => StopwatchHelper.Benchmark("Test", () => { }, iterations: 100, warmupIterations: -1));
    }

    #endregion

    #region 格式化方法测试

    [Fact]
    public void FormatTime_Milliseconds_ReturnsCorrectFormat()
    {
        var formatted = StopwatchHelper.FormatTime(TimeSpan.FromMilliseconds(500));

        Assert.Contains("毫秒", formatted);
    }

    [Fact]
    public void FormatTime_Seconds_ReturnsCorrectFormat()
    {
        var formatted = StopwatchHelper.FormatTime(TimeSpan.FromSeconds(2.5));

        Assert.Contains("秒", formatted);
    }

    [Fact]
    public void FormatTime_Minutes_ReturnsCorrectFormat()
    {
        var formatted = StopwatchHelper.FormatTime(TimeSpan.FromMinutes(2.5));

        Assert.Contains("分钟", formatted);
    }

    [Fact]
    public void FormatTime_Hours_ReturnsCorrectFormat()
    {
        var formatted = StopwatchHelper.FormatTime(TimeSpan.FromHours(1.5));

        Assert.Contains("小时", formatted);
    }

    [Fact]
    public void FormatTime_Microseconds_ReturnsCorrectFormat()
    {
        var formatted = StopwatchHelper.FormatTime(TimeSpan.FromMicroseconds(500));

        Assert.Contains("微秒", formatted);
    }

    [Fact]
    public void FormatTime_FromMilliseconds_ReturnsCorrectFormat()
    {
        var formatted = StopwatchHelper.FormatTime(1500L);

        Assert.NotNull(formatted);
        Assert.NotEmpty(formatted);
    }

    [Fact]
    public void FormatStatistics_ReturnsFormattedString()
    {
        var stats = new TimingStatistics
        {
            Count = 10,
            TotalMilliseconds = 100,
            AverageMilliseconds = 10,
            MinMilliseconds = 5,
            MaxMilliseconds = 20,
            MedianMilliseconds = 10,
            StandardDeviation = 3,
            P90Milliseconds = 18,
            P95Milliseconds = 19,
            P99Milliseconds = 20
        };

        var formatted = StopwatchHelper.FormatStatistics(stats);

        Assert.NotNull(formatted);
        Assert.Contains("执行次数: 10", formatted);
        Assert.Contains("平均耗时", formatted);
    }

    [Fact]
    public void FormatStatistics_EmptyStats_ReturnsNoData()
    {
        var stats = new TimingStatistics();

        var formatted = StopwatchHelper.FormatStatistics(stats);

        Assert.Equal("无数据", formatted);
    }

    #endregion

    #region TimingStatistics 测试

    [Fact]
    public void TimingStatistics_ToString_ReturnsFormattedString()
    {
        var stats = new TimingStatistics
        {
            Count = 10,
            TotalMilliseconds = 100,
            AverageMilliseconds = 10,
            MinMilliseconds = 5,
            MaxMilliseconds = 20
        };

        var str = stats.ToString();

        Assert.NotNull(str);
        Assert.Contains("执行次数", str);
    }

    #endregion

    #region ScopeTimer 测试

    [Fact]
    public void ScopeTimer_Dispose_InvokesCallback()
    {
        var invoked = false;
        TimeSpan? capturedElapsed = null;

        var timer = new ScopeTimer(elapsed =>
        {
            invoked = true;
            capturedElapsed = elapsed;
        });

        timer.Dispose();

        Assert.True(invoked);
        Assert.NotNull(capturedElapsed);
    }

    [Fact]
    public void ScopeTimer_ElapsedMilliseconds_ReturnsValue()
    {
        using var timer = new ScopeTimer();
        Thread.Sleep(10);

        Assert.True(timer.ElapsedMilliseconds >= 8);
    }

    [Fact]
    public void ScopeTimer_Elapsed_ReturnsTimeSpan()
    {
        using var timer = new ScopeTimer();
        Thread.Sleep(10);

        Assert.True(timer.Elapsed.TotalMilliseconds >= 8);
    }

    #endregion
}
