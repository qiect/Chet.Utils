using System.Diagnostics;

namespace Chet.Utils.Helpers;

/// <summary>
/// 计时器帮助类，提供便捷的性能测量和计时功能。
/// </summary>
/// <remarks>
/// <para>本类提供以下功能模块：</para>
/// <list type="bullet">
///   <item><description>基础计时：执行操作并测量耗时</description></item>
///   <item><description>统计计时：多次执行并计算统计信息</description></item>
///   <item><description>高精度计时：纳秒级计时支持</description></item>
///   <item><description>条件计时：条件执行的计时</description></item>
///   <item><description>范围计时：使用 using 语句的计时</description></item>
///   <item><description>基准测试：性能基准测试工具</description></item>
///   <item><description>格式化输出：友好的时间格式化</description></item>
/// </list>
/// </remarks>
public static class StopwatchHelper
{
    #region 基础计时方法

    /// <summary>
    /// 执行操作并返回耗时（毫秒）。
    /// </summary>
    /// <param name="action">要执行的操作。</param>
    /// <returns>执行耗时（毫秒）。</returns>
    /// <exception cref="ArgumentNullException">action 为 null 时抛出。</exception>
    /// <example>
    /// <code>
    /// long elapsedMs = StopwatchHelper.Time(() =>
    /// {
    ///     // 执行一些操作
    ///     Thread.Sleep(100);
    /// });
    /// Console.WriteLine($"耗时: {elapsedMs} 毫秒");
    /// </code>
    /// </example>
    public static long Time(Action action)
    {
        ArgumentNullException.ThrowIfNull(action);

        var stopwatch = Stopwatch.StartNew();
        action();
        stopwatch.Stop();
        return stopwatch.ElapsedMilliseconds;
    }

    /// <summary>
    /// 执行操作并返回耗时（毫秒）。
    /// </summary>
    /// <param name="action">要执行的操作。</param>
    /// <param name="iterations">执行次数。</param>
    /// <returns>执行耗时（毫秒）。</returns>
    /// <exception cref="ArgumentNullException">action 为 null 时抛出。</exception>
    /// <exception cref="ArgumentException">iterations 小于等于 0 时抛出。</exception>
    public static long Time(Action action, int iterations)
    {
        ArgumentNullException.ThrowIfNull(action);

        if (iterations <= 0)
            throw new ArgumentException("迭代次数必须大于0", nameof(iterations));

        var stopwatch = Stopwatch.StartNew();

        for (int i = 0; i < iterations; i++)
        {
            action();
        }

        stopwatch.Stop();
        return stopwatch.ElapsedMilliseconds;
    }

    /// <summary>
    /// 执行异步操作并返回耗时（毫秒）。
    /// </summary>
    /// <param name="action">要执行的异步操作。</param>
    /// <returns>执行耗时（毫秒）。</returns>
    /// <exception cref="ArgumentNullException">action 为 null 时抛出。</exception>
    /// <example>
    /// <code>
    /// long elapsedMs = await StopwatchHelper.TimeAsync(async () =>
    /// {
    ///     await Task.Delay(100);
    /// });
    /// Console.WriteLine($"耗时: {elapsedMs} 毫秒");
    /// </code>
    /// </example>
    public static async Task<long> TimeAsync(Func<Task> action)
    {
        ArgumentNullException.ThrowIfNull(action);

        var stopwatch = Stopwatch.StartNew();
        await action();
        stopwatch.Stop();
        return stopwatch.ElapsedMilliseconds;
    }

    /// <summary>
    /// 执行异步操作并返回耗时（毫秒）。
    /// </summary>
    /// <param name="action">要执行的异步操作。</param>
    /// <param name="iterations">执行次数。</param>
    /// <returns>执行耗时（毫秒）。</returns>
    /// <exception cref="ArgumentNullException">action 为 null 时抛出。</exception>
    /// <exception cref="ArgumentException">iterations 小于等于 0 时抛出。</exception>
    public static async Task<long> TimeAsync(Func<Task> action, int iterations)
    {
        ArgumentNullException.ThrowIfNull(action);

        if (iterations <= 0)
            throw new ArgumentException("迭代次数必须大于0", nameof(iterations));

        var stopwatch = Stopwatch.StartNew();

        for (int i = 0; i < iterations; i++)
        {
            await action();
        }

        stopwatch.Stop();
        return stopwatch.ElapsedMilliseconds;
    }

    /// <summary>
    /// 执行操作并返回详细计时结果。
    /// </summary>
    /// <param name="action">要执行的操作。</param>
    /// <returns>计时结果。</returns>
    /// <exception cref="ArgumentNullException">action 为 null 时抛出。</exception>
    /// <example>
    /// <code>
    /// var result = StopwatchHelper.TimeDetailed(() =>
    /// {
    ///     Thread.Sleep(100);
    /// });
    /// Console.WriteLine($"耗时: {result.TotalMilliseconds} 毫秒");
    /// Console.WriteLine($"耗时: {result.TotalMicroseconds} 微秒");
    /// </code>
    /// </example>
    public static TimeSpan TimeDetailed(Action action)
    {
        ArgumentNullException.ThrowIfNull(action);

        var stopwatch = Stopwatch.StartNew();
        action();
        stopwatch.Stop();
        return stopwatch.Elapsed;
    }

    /// <summary>
    /// 执行异步操作并返回详细计时结果。
    /// </summary>
    /// <param name="action">要执行的异步操作。</param>
    /// <returns>计时结果。</returns>
    /// <exception cref="ArgumentNullException">action 为 null 时抛出。</exception>
    public static async Task<TimeSpan> TimeDetailedAsync(Func<Task> action)
    {
        ArgumentNullException.ThrowIfNull(action);

        var stopwatch = Stopwatch.StartNew();
        await action();
        stopwatch.Stop();
        return stopwatch.Elapsed;
    }

    /// <summary>
    /// 执行带返回值的操作并返回结果和耗时。
    /// </summary>
    /// <typeparam name="T">返回值类型。</typeparam>
    /// <param name="func">要执行的操作。</param>
    /// <returns>执行结果和耗时。</returns>
    /// <exception cref="ArgumentNullException">func 为 null 时抛出。</exception>
    /// <example>
    /// <code>
    /// var (result, elapsedMs) = StopwatchHelper.Time(() => ComputeSum(1, 100));
    /// Console.WriteLine($"结果: {result}, 耗时: {elapsedMs} 毫秒");
    /// </code>
    /// </example>
    public static (T Result, long ElapsedMilliseconds) Time<T>(Func<T> func)
    {
        ArgumentNullException.ThrowIfNull(func);

        var stopwatch = Stopwatch.StartNew();
        var result = func();
        stopwatch.Stop();
        return (result, stopwatch.ElapsedMilliseconds);
    }

    /// <summary>
    /// 执行带返回值的异步操作并返回结果和耗时。
    /// </summary>
    /// <typeparam name="T">返回值类型。</typeparam>
    /// <param name="func">要执行的异步操作。</param>
    /// <returns>执行结果和耗时。</returns>
    /// <exception cref="ArgumentNullException">func 为 null 时抛出。</exception>
    public static async Task<(T Result, long ElapsedMilliseconds)> TimeAsync<T>(Func<Task<T>> func)
    {
        ArgumentNullException.ThrowIfNull(func);

        var stopwatch = Stopwatch.StartNew();
        var result = await func();
        stopwatch.Stop();
        return (result, stopwatch.ElapsedMilliseconds);
    }

    #endregion

    #region 统计计时方法

    /// <summary>
    /// 多次执行操作并返回详细统计信息。
    /// </summary>
    /// <param name="action">要执行的操作。</param>
    /// <param name="iterations">执行次数。</param>
    /// <returns>执行时间统计信息。</returns>
    /// <exception cref="ArgumentNullException">action 为 null 时抛出。</exception>
    /// <exception cref="ArgumentException">iterations 小于等于 0 时抛出。</exception>
    /// <example>
    /// <code>
    /// var stats = StopwatchHelper.TimeStatistics(() =>
    /// {
    ///     Thread.Sleep(10);
    /// }, 100);
    /// Console.WriteLine($"平均: {stats.AverageMilliseconds} 毫秒");
    /// Console.WriteLine($"最小: {stats.MinMilliseconds} 毫秒");
    /// Console.WriteLine($"最大: {stats.MaxMilliseconds} 毫秒");
    /// </code>
    /// </example>
    public static TimingStatistics TimeStatistics(Action action, int iterations)
    {
        ArgumentNullException.ThrowIfNull(action);

        if (iterations <= 0)
            throw new ArgumentException("迭代次数必须大于0", nameof(iterations));

        var times = new List<long>(iterations);

        for (int i = 0; i < iterations; i++)
        {
            times.Add(Time(action));
        }

        return CalculateStatistics(times);
    }

    /// <summary>
    /// 多次执行异步操作并返回详细统计信息。
    /// </summary>
    /// <param name="action">要执行的异步操作。</param>
    /// <param name="iterations">执行次数。</param>
    /// <returns>执行时间统计信息。</returns>
    /// <exception cref="ArgumentNullException">action 为 null 时抛出。</exception>
    /// <exception cref="ArgumentException">iterations 小于等于 0 时抛出。</exception>
    public static async Task<TimingStatistics> TimeStatisticsAsync(Func<Task> action, int iterations)
    {
        ArgumentNullException.ThrowIfNull(action);

        if (iterations <= 0)
            throw new ArgumentException("迭代次数必须大于0", nameof(iterations));

        var times = new List<long>(iterations);

        for (int i = 0; i < iterations; i++)
        {
            times.Add(await TimeAsync(action));
        }

        return CalculateStatistics(times);
    }

    /// <summary>
    /// 多次执行操作并返回每次的耗时列表。
    /// </summary>
    /// <param name="action">要执行的操作。</param>
    /// <param name="iterations">执行次数。</param>
    /// <returns>每次执行的耗时列表（毫秒）。</returns>
    /// <exception cref="ArgumentNullException">action 为 null 时抛出。</exception>
    /// <exception cref="ArgumentException">iterations 小于等于 0 时抛出。</exception>
    public static List<long> TimeEach(Action action, int iterations)
    {
        ArgumentNullException.ThrowIfNull(action);

        if (iterations <= 0)
            throw new ArgumentException("迭代次数必须大于0", nameof(iterations));

        var times = new List<long>(iterations);

        for (int i = 0; i < iterations; i++)
        {
            times.Add(Time(action));
        }

        return times;
    }

    /// <summary>
    /// 多次执行异步操作并返回每次的耗时列表。
    /// </summary>
    /// <param name="action">要执行的异步操作。</param>
    /// <param name="iterations">执行次数。</param>
    /// <returns>每次执行的耗时列表（毫秒）。</returns>
    /// <exception cref="ArgumentNullException">action 为 null 时抛出。</exception>
    /// <exception cref="ArgumentException">iterations 小于等于 0 时抛出。</exception>
    public static async Task<List<long>> TimeEachAsync(Func<Task> action, int iterations)
    {
        ArgumentNullException.ThrowIfNull(action);

        if (iterations <= 0)
            throw new ArgumentException("迭代次数必须大于0", nameof(iterations));

        var times = new List<long>(iterations);

        for (int i = 0; i < iterations; i++)
        {
            times.Add(await TimeAsync(action));
        }

        return times;
    }

    /// <summary>
    /// 计算统计信息。
    /// </summary>
    /// <param name="times">时间列表（毫秒）。</param>
    /// <returns>统计信息。</returns>
    /// <exception cref="ArgumentNullException">times 为 null 时抛出。</exception>
    public static TimingStatistics CalculateStatistics(IEnumerable<long> times)
    {
        ArgumentNullException.ThrowIfNull(times);

        var timesList = times.ToList();

        if (timesList.Count == 0)
        {
            return new TimingStatistics();
        }

        timesList.Sort();

        var sum = timesList.Sum();
        var average = (double)sum / timesList.Count;

        double sumOfSquares = 0;
        foreach (var time in timesList)
        {
            sumOfSquares += Math.Pow(time - average, 2);
        }

        var stdDev = Math.Sqrt(sumOfSquares / timesList.Count);

        int medianIndex = timesList.Count / 2;
        var median = timesList.Count % 2 == 0
            ? (timesList[medianIndex - 1] + timesList[medianIndex]) / 2.0
            : timesList[medianIndex];

        return new TimingStatistics
        {
            Count = timesList.Count,
            TotalMilliseconds = sum,
            AverageMilliseconds = average,
            MinMilliseconds = timesList[0],
            MaxMilliseconds = timesList[^1],
            MedianMilliseconds = median,
            StandardDeviation = stdDev,
            P90Milliseconds = GetPercentile(timesList, 90),
            P95Milliseconds = GetPercentile(timesList, 95),
            P99Milliseconds = GetPercentile(timesList, 99)
        };
    }

    /// <summary>
    /// 获取百分位数。
    /// </summary>
    private static double GetPercentile(List<long> sortedTimes, double percentile)
    {
        if (sortedTimes.Count == 0) return 0;
        if (sortedTimes.Count == 1) return sortedTimes[0];

        var index = (percentile / 100) * (sortedTimes.Count - 1);
        var lowerIndex = (int)Math.Floor(index);
        var upperIndex = (int)Math.Ceiling(index);

        if (lowerIndex == upperIndex)
        {
            return sortedTimes[lowerIndex];
        }

        var weight = index - lowerIndex;
        return sortedTimes[lowerIndex] * (1 - weight) + sortedTimes[upperIndex] * weight;
    }

    #endregion

    #region 高精度计时方法

    /// <summary>
    /// 执行操作并返回高精度耗时（Ticks）。
    /// </summary>
    /// <param name="action">要执行的操作。</param>
    /// <returns>执行耗时（Ticks）。</returns>
    /// <exception cref="ArgumentNullException">action 为 null 时抛出。</exception>
    /// <example>
    /// <code>
    /// long ticks = StopwatchHelper.TimeHighPrecision(() =>
    /// {
    ///     // 高精度操作
    /// });
    /// Console.WriteLine($"耗时: {ticks} Ticks");
    /// </code>
    /// </example>
    public static long TimeHighPrecision(Action action)
    {
        ArgumentNullException.ThrowIfNull(action);

        var stopwatch = Stopwatch.StartNew();
        action();
        stopwatch.Stop();
        return stopwatch.ElapsedTicks;
    }

    /// <summary>
    /// 执行操作并返回纳秒级耗时。
    /// </summary>
    /// <param name="action">要执行的操作。</param>
    /// <returns>执行耗时（纳秒）。</returns>
    /// <exception cref="ArgumentNullException">action 为 null 时抛出。</exception>
    public static double TimeNanoseconds(Action action)
    {
        ArgumentNullException.ThrowIfNull(action);

        var stopwatch = Stopwatch.StartNew();
        action();
        stopwatch.Stop();

        return stopwatch.ElapsedTicks * (1_000_000_000.0 / Stopwatch.Frequency);
    }

    /// <summary>
    /// 执行操作并返回微秒级耗时。
    /// </summary>
    /// <param name="action">要执行的操作。</param>
    /// <returns>执行耗时（微秒）。</returns>
    /// <exception cref="ArgumentNullException">action 为 null 时抛出。</exception>
    public static double TimeMicroseconds(Action action)
    {
        ArgumentNullException.ThrowIfNull(action);

        var stopwatch = Stopwatch.StartNew();
        action();
        stopwatch.Stop();

        return stopwatch.ElapsedTicks * (1_000_000.0 / Stopwatch.Frequency);
    }

    /// <summary>
    /// 获取计时器频率（每秒 Tick 数）。
    /// </summary>
    /// <returns>计时器频率。</returns>
    public static long GetFrequency()
    {
        return Stopwatch.Frequency;
    }

    /// <summary>
    /// 检查计时器是否为高精度。
    /// </summary>
    /// <returns>是否为高精度计时器。</returns>
    public static bool IsHighResolution()
    {
        return Stopwatch.IsHighResolution;
    }

    /// <summary>
    /// 获取时间戳（Ticks）。
    /// </summary>
    /// <returns>当前时间戳。</returns>
    public static long GetTimestamp()
    {
        return Stopwatch.GetTimestamp();
    }

    /// <summary>
    /// 计算两个时间戳之间的时间间隔。
    /// </summary>
    /// <param name="startTimestamp">开始时间戳。</param>
    /// <param name="endTimestamp">结束时间戳。</param>
    /// <returns>时间间隔。</returns>
    public static TimeSpan GetElapsedTime(long startTimestamp, long endTimestamp)
    {
        return Stopwatch.GetElapsedTime(startTimestamp, endTimestamp);
    }

    /// <summary>
    /// 计算从指定时间戳到现在的时间间隔。
    /// </summary>
    /// <param name="startTimestamp">开始时间戳。</param>
    /// <returns>时间间隔。</returns>
    public static TimeSpan GetElapsedTime(long startTimestamp)
    {
        return Stopwatch.GetElapsedTime(startTimestamp);
    }

    #endregion

    #region 条件计时方法

    /// <summary>
    /// 条件执行计时：仅当条件为 true 时执行并计时。
    /// </summary>
    /// <param name="condition">执行条件。</param>
    /// <param name="action">要执行的操作。</param>
    /// <returns>执行耗时（毫秒），如果条件为 false 则返回 -1。</returns>
    /// <exception cref="ArgumentNullException">action 为 null 时抛出。</exception>
    /// <example>
    /// <code>
    /// bool shouldRun = true;
    /// long elapsedMs = StopwatchHelper.TimeIf(shouldRun, () =>
    /// {
    ///     Thread.Sleep(100);
    /// });
    /// Console.WriteLine($"耗时: {elapsedMs} 毫秒");
    /// </code>
    /// </example>
    public static long TimeIf(bool condition, Action action)
    {
        ArgumentNullException.ThrowIfNull(action);

        if (!condition) return -1;
        return Time(action);
    }

    /// <summary>
    /// 条件执行异步计时：仅当条件为 true 时执行并计时。
    /// </summary>
    /// <param name="condition">执行条件。</param>
    /// <param name="action">要执行的异步操作。</param>
    /// <returns>执行耗时（毫秒），如果条件为 false 则返回 -1。</returns>
    /// <exception cref="ArgumentNullException">action 为 null 时抛出。</exception>
    public static async Task<long> TimeIfAsync(bool condition, Func<Task> action)
    {
        ArgumentNullException.ThrowIfNull(action);

        if (!condition) return -1;
        return await TimeAsync(action);
    }

    /// <summary>
    /// 超时执行：如果超时则返回 null。
    /// </summary>
    /// <typeparam name="T">返回值类型。</typeparam>
    /// <param name="func">要执行的操作。</param>
    /// <param name="timeoutMilliseconds">超时时间（毫秒）。</param>
    /// <returns>执行结果和耗时，如果超时则返回 null。</returns>
    /// <exception cref="ArgumentNullException">func 为 null 时抛出。</exception>
    public static (T? Result, long ElapsedMilliseconds)? TimeWithTimeout<T>(Func<T> func, long timeoutMilliseconds)
    {
        ArgumentNullException.ThrowIfNull(func);

        if (timeoutMilliseconds <= 0)
            throw new ArgumentException("超时时间必须大于0", nameof(timeoutMilliseconds));

        var stopwatch = Stopwatch.StartNew();
        var task = Task.Run(() => func());

        if (task.Wait((int)timeoutMilliseconds))
        {
            stopwatch.Stop();
            return (task.Result, stopwatch.ElapsedMilliseconds);
        }

        return null;
    }

    /// <summary>
    /// 重试执行：失败时重试指定次数。
    /// </summary>
    /// <param name="action">要执行的操作。</param>
    /// <param name="maxRetries">最大重试次数。</param>
    /// <param name="retryDelayMilliseconds">重试延迟（毫秒）。</param>
    /// <returns>执行结果（是否成功、耗时、重试次数）。</returns>
    /// <exception cref="ArgumentNullException">action 为 null 时抛出。</exception>
    public static (bool Success, long ElapsedMilliseconds, int RetryCount) TimeWithRetry(
        Action action,
        int maxRetries = 3,
        int retryDelayMilliseconds = 100)
    {
        ArgumentNullException.ThrowIfNull(action);

        if (maxRetries < 0)
            throw new ArgumentException("最大重试次数不能为负数", nameof(maxRetries));

        var stopwatch = Stopwatch.StartNew();
        int retryCount = 0;
        bool success = false;
        Exception? lastException = null;

        while (retryCount <= maxRetries)
        {
            try
            {
                action();
                success = true;
                break;
            }
            catch (Exception ex)
            {
                lastException = ex;
                retryCount++;

                if (retryCount <= maxRetries && retryDelayMilliseconds > 0)
                {
                    Thread.Sleep(retryDelayMilliseconds);
                }
            }
        }

        stopwatch.Stop();

        return (success, stopwatch.ElapsedMilliseconds, Math.Max(0, retryCount - 1));
    }

    #endregion

    #region 范围计时方法

    /// <summary>
    /// 创建一个范围计时器，使用 using 语句自动计时。
    /// </summary>
    /// <param name="action">计时结束后的回调操作。</param>
    /// <returns>范围计时器。</returns>
    /// <example>
    /// <code>
    /// using (var timer = StopwatchHelper.StartScope(elapsed => 
    /// {
    ///     Console.WriteLine($"耗时: {elapsed.TotalMilliseconds} 毫秒");
    /// }))
    /// {
    ///     // 执行一些操作
    ///     Thread.Sleep(100);
    /// }
    /// </code>
    /// </example>
    public static ScopeTimer StartScope(Action<TimeSpan> action)
    {
        ArgumentNullException.ThrowIfNull(action);
        return new ScopeTimer(action);
    }

    /// <summary>
    /// 创建一个范围计时器，使用 using 语句自动计时并输出到控制台。
    /// </summary>
    /// <param name="label">计时标签。</param>
    /// <returns>范围计时器。</returns>
    /// <example>
    /// <code>
    /// using (StopwatchHelper.StartScope("数据库查询"))
    /// {
    ///     // 执行数据库查询
    /// }
    /// // 输出: [数据库查询] 耗时: 123.45 毫秒
    /// </code>
    /// </example>
    public static ScopeTimer StartScope(string label)
    {
        ArgumentException.ThrowIfNullOrEmpty(label);

        return new ScopeTimer(elapsed =>
        {
            Console.WriteLine($"[{label}] 耗时: {FormatTime(elapsed)}");
        });
    }

    /// <summary>
    /// 创建一个范围计时器，使用 using 语句自动计时。
    /// </summary>
    /// <returns>范围计时器。</returns>
    /// <example>
    /// <code>
    /// using (var timer = StopwatchHelper.StartScope())
    /// {
    ///     // 执行一些操作
    /// }
    /// Console.WriteLine($"耗时: {timer.ElapsedMilliseconds} 毫秒");
    /// </code>
    /// </example>
    public static ScopeTimer StartScope()
    {
        return new ScopeTimer();
    }

    #endregion

    #region 基准测试方法

    /// <summary>
    /// 执行基准测试。
    /// </summary>
    /// <param name="name">测试名称。</param>
    /// <param name="action">要测试的操作。</param>
    /// <param name="iterations">迭代次数。</param>
    /// <param name="warmupIterations">预热次数。</param>
    /// <returns>基准测试结果。</returns>
    /// <exception cref="ArgumentNullException">name 或 action 为 null 时抛出。</exception>
    /// <exception cref="ArgumentException">iterations 小于等于 0 时抛出。</exception>
    /// <example>
    /// <code>
    /// var result = StopwatchHelper.Benchmark("List.Add", () =>
    /// {
    ///     var list = new List&lt;int&gt;();
    ///     list.Add(1);
    /// }, iterations: 10000, warmupIterations: 100);
    /// Console.WriteLine(result);
    /// </code>
    /// </example>
    public static BenchmarkResult Benchmark(
        string name,
        Action action,
        int iterations = 1000,
        int warmupIterations = 10)
    {
        ArgumentException.ThrowIfNullOrEmpty(name);
        ArgumentNullException.ThrowIfNull(action);

        if (iterations <= 0)
            throw new ArgumentException("迭代次数必须大于0", nameof(iterations));

        if (warmupIterations < 0)
            throw new ArgumentException("预热次数不能为负数", nameof(warmupIterations));

        for (int i = 0; i < warmupIterations; i++)
        {
            action();
        }

        GC.Collect();
        GC.WaitForPendingFinalizers();
        GC.Collect();

        var stats = TimeStatistics(action, iterations);

        return new BenchmarkResult
        {
            Name = name,
            Iterations = iterations,
            Statistics = stats
        };
    }

    /// <summary>
    /// 执行异步基准测试。
    /// </summary>
    /// <param name="name">测试名称。</param>
    /// <param name="action">要测试的异步操作。</param>
    /// <param name="iterations">迭代次数。</param>
    /// <param name="warmupIterations">预热次数。</param>
    /// <returns>基准测试结果。</returns>
    /// <exception cref="ArgumentNullException">name 或 action 为 null 时抛出。</exception>
    public static async Task<BenchmarkResult> BenchmarkAsync(
        string name,
        Func<Task> action,
        int iterations = 1000,
        int warmupIterations = 10)
    {
        ArgumentException.ThrowIfNullOrEmpty(name);
        ArgumentNullException.ThrowIfNull(action);

        if (iterations <= 0)
            throw new ArgumentException("迭代次数必须大于0", nameof(iterations));

        if (warmupIterations < 0)
            throw new ArgumentException("预热次数不能为负数", nameof(warmupIterations));

        for (int i = 0; i < warmupIterations; i++)
        {
            await action();
        }

        GC.Collect();
        GC.WaitForPendingFinalizers();
        GC.Collect();

        var stats = await TimeStatisticsAsync(action, iterations);

        return new BenchmarkResult
        {
            Name = name,
            Iterations = iterations,
            Statistics = stats
        };
    }

    /// <summary>
    /// 比较两个方法的性能。
    /// </summary>
    /// <param name="name1">第一个方法名称。</param>
    /// <param name="action1">第一个方法。</param>
    /// <param name="name2">第二个方法名称。</param>
    /// <param name="action2">第二个方法。</param>
    /// <param name="iterations">迭代次数。</param>
    /// <returns>比较结果。</returns>
    /// <exception cref="ArgumentNullException">任何参数为 null 时抛出。</exception>
    /// <example>
    /// <code>
    /// var comparison = StopwatchHelper.Compare(
    ///     "StringBuilder", () =>
    ///     {
    ///         var sb = new StringBuilder();
    ///         for (int i = 0; i &lt; 100; i++) sb.Append(i);
    ///     },
    ///     "StringConcat", () =>
    ///     {
    ///         var s = "";
    ///         for (int i = 0; i &lt; 100; i++) s += i;
    ///     },
    ///     iterations: 10000
    /// );
    /// Console.WriteLine(comparison);
    /// </code>
    /// </example>
    public static BenchmarkComparison Compare(
        string name1,
        Action action1,
        string name2,
        Action action2,
        int iterations = 1000)
    {
        ArgumentException.ThrowIfNullOrEmpty(name1);
        ArgumentException.ThrowIfNullOrEmpty(name2);
        ArgumentNullException.ThrowIfNull(action1);
        ArgumentNullException.ThrowIfNull(action2);

        var result1 = Benchmark(name1, action1, iterations);
        var result2 = Benchmark(name2, action2, iterations);

        return new BenchmarkComparison
        {
            Result1 = result1,
            Result2 = result2
        };
    }

    #endregion

    #region 格式化方法

    /// <summary>
    /// 格式化时间间隔为友好字符串。
    /// </summary>
    /// <param name="elapsed">时间间隔。</param>
    /// <returns>格式化后的字符串。</returns>
    /// <example>
    /// <code>
    /// var formatted = StopwatchHelper.FormatTime(TimeSpan.FromMilliseconds(1234.56));
    /// // 输出: "1.23 秒"
    /// </code>
    /// </example>
    public static string FormatTime(TimeSpan elapsed)
    {
        if (elapsed.TotalSeconds < 1)
        {
            if (elapsed.TotalMilliseconds < 1)
            {
                if (elapsed.TotalMicroseconds < 1)
                {
                    return $"{elapsed.TotalNanoseconds:F2} 纳秒";
                }
                return $"{elapsed.TotalMicroseconds:F2} 微秒";
            }
            return $"{elapsed.TotalMilliseconds:F2} 毫秒";
        }

        if (elapsed.TotalMinutes < 1)
        {
            return $"{elapsed.TotalSeconds:F2} 秒";
        }

        if (elapsed.TotalHours < 1)
        {
            return $"{elapsed.TotalMinutes:F2} 分钟";
        }

        return $"{elapsed.TotalHours:F2} 小时";
    }

    /// <summary>
    /// 格式化毫秒数为友好字符串。
    /// </summary>
    /// <param name="milliseconds">毫秒数。</param>
    /// <returns>格式化后的字符串。</returns>
    public static string FormatTime(long milliseconds)
    {
        return FormatTime(TimeSpan.FromMilliseconds(milliseconds));
    }

    /// <summary>
    /// 格式化毫秒数为友好字符串。
    /// </summary>
    /// <param name="milliseconds">毫秒数。</param>
    /// <returns>格式化后的字符串。</returns>
    public static string FormatTime(double milliseconds)
    {
        return FormatTime(TimeSpan.FromMilliseconds(milliseconds));
    }

    /// <summary>
    /// 格式化统计信息为友好字符串。
    /// </summary>
    /// <param name="stats">统计信息。</param>
    /// <returns>格式化后的字符串。</returns>
    /// <exception cref="ArgumentNullException">stats 为 null 时抛出。</exception>
    public static string FormatStatistics(TimingStatistics stats)
    {
        ArgumentNullException.ThrowIfNull(stats);

        if (stats.Count == 0)
        {
            return "无数据";
        }

        return $@"执行次数: {stats.Count}
总耗时: {FormatTime(stats.TotalMilliseconds)}
平均耗时: {FormatTime(stats.AverageMilliseconds)}
最小耗时: {FormatTime(stats.MinMilliseconds)}
最大耗时: {FormatTime(stats.MaxMilliseconds)}
中位数: {FormatTime(stats.MedianMilliseconds)}
标准差: {FormatTime(stats.StandardDeviation)}
P90: {FormatTime(stats.P90Milliseconds)}
P95: {FormatTime(stats.P95Milliseconds)}
P99: {FormatTime(stats.P99Milliseconds)}";
    }

    #endregion
}

#region 辅助类定义

/// <summary>
/// 计时统计信息。
/// </summary>
public class TimingStatistics
{
    /// <summary>
    /// 执行次数。
    /// </summary>
    public int Count { get; init; }

    /// <summary>
    /// 总耗时（毫秒）。
    /// </summary>
    public long TotalMilliseconds { get; init; }

    /// <summary>
    /// 平均耗时（毫秒）。
    /// </summary>
    public double AverageMilliseconds { get; init; }

    /// <summary>
    /// 最小耗时（毫秒）。
    /// </summary>
    public long MinMilliseconds { get; init; }

    /// <summary>
    /// 最大耗时（毫秒）。
    /// </summary>
    public long MaxMilliseconds { get; init; }

    /// <summary>
    /// 中位数（毫秒）。
    /// </summary>
    public double MedianMilliseconds { get; init; }

    /// <summary>
    /// 标准差（毫秒）。
    /// </summary>
    public double StandardDeviation { get; init; }

    /// <summary>
    /// P90 百分位数（毫秒）。
    /// </summary>
    public double P90Milliseconds { get; init; }

    /// <summary>
    /// P95 百分位数（毫秒）。
    /// </summary>
    public double P95Milliseconds { get; init; }

    /// <summary>
    /// P99 百分位数（毫秒）。
    /// </summary>
    public double P99Milliseconds { get; init; }

    /// <summary>
    /// 返回统计信息的字符串表示。
    /// </summary>
    public override string ToString()
    {
        return StopwatchHelper.FormatStatistics(this);
    }
}

/// <summary>
/// 范围计时器。
/// </summary>
public sealed class ScopeTimer : IDisposable
{
    private readonly Stopwatch _stopwatch;
    private readonly Action<TimeSpan>? _action;
    private bool _disposed;

    /// <summary>
    /// 初始化范围计时器。
    /// </summary>
    public ScopeTimer()
    {
        _stopwatch = Stopwatch.StartNew();
    }

    /// <summary>
    /// 初始化范围计时器。
    /// </summary>
    /// <param name="action">计时结束后的回调操作。</param>
    public ScopeTimer(Action<TimeSpan> action) : this()
    {
        _action = action;
    }

    /// <summary>
    /// 获取已耗时（毫秒）。
    /// </summary>
    public long ElapsedMilliseconds => _stopwatch.ElapsedMilliseconds;

    /// <summary>
    /// 获取已耗时。
    /// </summary>
    public TimeSpan Elapsed => _stopwatch.Elapsed;

    /// <summary>
    /// 获取已耗时（Ticks）。
    /// </summary>
    public long ElapsedTicks => _stopwatch.ElapsedTicks;

    /// <summary>
    /// 停止计时。
    /// </summary>
    public void Stop()
    {
        _stopwatch.Stop();
    }

    /// <summary>
    /// 重新开始计时。
    /// </summary>
    public void Restart()
    {
        _stopwatch.Restart();
    }

    /// <summary>
    /// 释放资源。
    /// </summary>
    public void Dispose()
    {
        if (_disposed) return;

        _stopwatch.Stop();
        _action?.Invoke(_stopwatch.Elapsed);
        _disposed = true;
    }
}

/// <summary>
/// 基准测试结果。
/// </summary>
public class BenchmarkResult
{
    /// <summary>
    /// 测试名称。
    /// </summary>
    public string Name { get; init; } = string.Empty;

    /// <summary>
    /// 迭代次数。
    /// </summary>
    public int Iterations { get; init; }

    /// <summary>
    /// 统计信息。
    /// </summary>
    public TimingStatistics Statistics { get; init; } = new();

    /// <summary>
    /// 每秒操作数。
    /// </summary>
    public double OperationsPerSecond => Statistics.AverageMilliseconds > 0
        ? 1000.0 / Statistics.AverageMilliseconds
        : 0;

    /// <summary>
    /// 返回结果的字符串表示。
    /// </summary>
    public override string ToString()
    {
        return $@"=== {Name} ===
迭代次数: {Iterations}
{Statistics}
每秒操作数: {OperationsPerSecond:F2} ops/s";
    }
}

/// <summary>
/// 基准测试比较结果。
/// </summary>
public class BenchmarkComparison
{
    /// <summary>
    /// 第一个方法的结果。
    /// </summary>
    public BenchmarkResult Result1 { get; init; } = new();

    /// <summary>
    /// 第二个方法的结果。
    /// </summary>
    public BenchmarkResult Result2 { get; init; } = new();

    /// <summary>
    /// 性能比率（Result2 / Result1）。
    /// </summary>
    public double SpeedRatio => Result1.Statistics.AverageMilliseconds > 0
        ? Result2.Statistics.AverageMilliseconds / Result1.Statistics.AverageMilliseconds
        : 0;

    /// <summary>
    /// 返回比较结果的字符串表示。
    /// </summary>
    public override string ToString()
    {
        var faster = SpeedRatio > 1 ? Result1.Name : Result2.Name;
        var ratio = SpeedRatio > 1 ? SpeedRatio : 1 / SpeedRatio;

        return $@"{Result1}
{Result2}

{faster} 快 {ratio:F2}x";
    }
}

#endregion
