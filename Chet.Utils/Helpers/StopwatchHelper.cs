using System.Diagnostics;

namespace Chet.Utils.Helpers
{
    /// <summary>
    /// 计时器帮助类，提供便捷的性能测量和计时功能
    /// </summary>
    public static class StopwatchHelper
    {
        #region 基础计时方法

        /// <summary>
        /// 执行操作并返回耗时
        /// </summary>
        /// <param name="action">要执行的操作</param>
        /// <returns>执行耗时（毫秒）</returns>
        public static long Time(Action action)
        {
            var stopwatch = Stopwatch.StartNew();
            action();
            stopwatch.Stop();
            return stopwatch.ElapsedMilliseconds;
        }

        /// <summary>
        /// 执行操作并返回耗时（高精度）
        /// </summary>
        /// <param name="action">要执行的操作</param>
        /// <returns>执行耗时（时间戳）</returns>
        public static TimeSpan TimePrecise(Action action)
        {
            var stopwatch = Stopwatch.StartNew();
            action();
            stopwatch.Stop();
            return stopwatch.Elapsed;
        }

        /// <summary>
        /// 异步执行操作并返回耗时
        /// </summary>
        /// <param name="action">要执行的异步操作</param>
        /// <returns>执行耗时（毫秒）</returns>
        public static async Task<long> TimeAsync(Func<Task> action)
        {
            var stopwatch = Stopwatch.StartNew();
            await action();
            stopwatch.Stop();
            return stopwatch.ElapsedMilliseconds;
        }

        /// <summary>
        /// 异步执行操作并返回耗时（高精度）
        /// </summary>
        /// <param name="action">要执行的异步操作</param>
        /// <returns>执行耗时（时间戳）</returns>
        public static async Task<TimeSpan> TimePreciseAsync(Func<Task> action)
        {
            var stopwatch = Stopwatch.StartNew();
            await action();
            stopwatch.Stop();
            return stopwatch.Elapsed;
        }

        /// <summary>
        /// 创建并启动一个新的计时器
        /// </summary>
        /// <returns>已启动的计时器实例</returns>
        public static Stopwatch StartNew()
        {
            return Stopwatch.StartNew();
        }

        #endregion

        #region 多次执行测量

        /// <summary>
        /// 多次执行操作并返回平均耗时
        /// </summary>
        /// <param name="action">要执行的操作</param>
        /// <param name="iterations">执行次数</param>
        /// <returns>平均执行耗时（毫秒）</returns>
        public static double TimeAverage(Action action, int iterations)
        {
            if (iterations <= 0)
                throw new ArgumentException("迭代次数必须大于0", nameof(iterations));

            var totalElapsed = 0L;
            for (int i = 0; i < iterations; i++)
            {
                totalElapsed += Time(action);
            }
            return (double)totalElapsed / iterations;
        }

        /// <summary>
        /// 多次执行操作并返回详细统计信息
        /// </summary>
        /// <param name="action">要执行的操作</param>
        /// <param name="iterations">执行次数</param>
        /// <returns>执行时间统计信息</returns>
        public static TimingStatistics TimeStatistics(Action action, int iterations)
        {
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
        /// 异步多次执行操作并返回平均耗时
        /// </summary>
        /// <param name="action">要执行的异步操作</param>
        /// <param name="iterations">执行次数</param>
        /// <returns>平均执行耗时（毫秒）</returns>
        public static async Task<double> TimeAverageAsync(Func<Task> action, int iterations)
        {
            if (iterations <= 0)
                throw new ArgumentException("迭代次数必须大于0", nameof(iterations));

            var totalElapsed = 0L;
            for (int i = 0; i < iterations; i++)
            {
                totalElapsed += await TimeAsync(action);
            }
            return (double)totalElapsed / iterations;
        }

        /// <summary>
        /// 异步多次执行操作并返回详细统计信息
        /// </summary>
        /// <param name="action">要执行的异步操作</param>
        /// <param name="iterations">执行次数</param>
        /// <returns>执行时间统计信息</returns>
        public static async Task<TimingStatistics> TimeStatisticsAsync(Func<Task> action, int iterations)
        {
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
        /// 并发执行操作并测量总耗时
        /// </summary>
        /// <param name="action">要执行的操作</param>
        /// <param name="concurrentCount">并发数</param>
        /// <returns>并发执行总耗时（毫秒）</returns>
        public static long TimeConcurrent(Action action, int concurrentCount)
        {
            if (concurrentCount <= 0)
                throw new ArgumentException("并发数必须大于0", nameof(concurrentCount));

            var tasks = new Task[concurrentCount];
            var stopwatch = Stopwatch.StartNew();

            for (int i = 0; i < concurrentCount; i++)
            {
                tasks[i] = Task.Run(action);
            }

            Task.WaitAll(tasks);
            stopwatch.Stop();
            return stopwatch.ElapsedMilliseconds;
        }

        /// <summary>
        /// 并发执行异步操作并测量总耗时
        /// </summary>
        /// <param name="action">要执行的异步操作</param>
        /// <param name="concurrentCount">并发数</param>
        /// <returns>并发执行总耗时（毫秒）</returns>
        public static async Task<long> TimeConcurrentAsync(Func<Task> action, int concurrentCount)
        {
            if (concurrentCount <= 0)
                throw new ArgumentException("并发数必须大于0", nameof(concurrentCount));

            var tasks = new Task[concurrentCount];
            var stopwatch = Stopwatch.StartNew();

            for (int i = 0; i < concurrentCount; i++)
            {
                tasks[i] = action();
            }

            await Task.WhenAll(tasks);
            stopwatch.Stop();
            return stopwatch.ElapsedMilliseconds;
        }

        /// <summary>
        /// 比较两个操作的性能
        /// </summary>
        /// <param name="action1">第一个操作</param>
        /// <param name="action2">第二个操作</param>
        /// <param name="iterations">每项操作的执行次数</param>
        /// <returns>性能比较结果</returns>
        public static PerformanceComparison Compare(Action action1, Action action2, int iterations)
        {
            var stats1 = TimeStatistics(action1, iterations);
            var stats2 = TimeStatistics(action2, iterations);

            return new PerformanceComparison
            {
                FirstOperationStats = stats1,
                SecondOperationStats = stats2,
                IsFirstFaster = stats1.Average < stats2.Average,
                PerformanceRatio = stats1.Average / stats2.Average
            };
        }

        /// <summary>
        /// 异步比较两个操作的性能
        /// </summary>
        /// <param name="action1">第一个异步操作</param>
        /// <param name="action2">第二个异步操作</param>
        /// <param name="iterations">每项操作的执行次数</param>
        /// <returns>性能比较结果</returns>
        public static async Task<PerformanceComparison> CompareAsync(Func<Task> action1, Func<Task> action2, int iterations)
        {
            var stats1 = await TimeStatisticsAsync(action1, iterations);
            var stats2 = await TimeStatisticsAsync(action2, iterations);

            return new PerformanceComparison
            {
                FirstOperationStats = stats1,
                SecondOperationStats = stats2,
                IsFirstFaster = stats1.Average < stats2.Average,
                PerformanceRatio = stats1.Average / stats2.Average
            };
        }

        /// <summary>
        /// 计算执行时间统计数据
        /// </summary>
        /// <param name="times">执行时间集合</param>
        /// <returns>统计信息</returns>
        private static TimingStatistics CalculateStatistics(List<long> times)
        {
            if (times == null || times.Count == 0)
                throw new ArgumentException("时间集合不能为空", nameof(times));

            long min = long.MaxValue, max = long.MinValue, sum = 0;
            foreach (var time in times)
            {
                if (time < min) min = time;
                if (time > max) max = time;
                sum += time;
            }

            double average = (double)sum / times.Count;
            double variance = 0;
            foreach (var time in times)
            {
                variance += Math.Pow(time - average, 2);
            }
            variance /= times.Count;
            double stdDev = Math.Sqrt(variance);

            return new TimingStatistics
            {
                Count = times.Count,
                Min = min,
                Max = max,
                Average = average,
                Sum = sum,
                StandardDeviation = stdDev
            };
        }

        #endregion

        #region 高精度计时

        /// <summary>
        /// 获取高精度时间戳
        /// </summary>
        /// <returns>高精度时间戳</returns>
        public static long GetTimestamp()
        {
            return Stopwatch.GetTimestamp();
        }

        /// <summary>
        /// 将时间戳转换为时间间隔
        /// </summary>
        /// <param name="timestamp">时间戳</param>
        /// <returns>时间间隔</returns>
        public static TimeSpan TimestampToTimeSpan(long timestamp)
        {
            return TimeSpan.FromTicks(timestamp * TimeSpan.TicksPerSecond / Stopwatch.Frequency);
        }

        /// <summary>
        /// 获取计时器频率
        /// </summary>
        /// <returns>计时器频率</returns>
        public static long GetFrequency()
        {
            return Stopwatch.Frequency;
        }

        /// <summary>
        /// 检查计时器是否基于高性能计数器
        /// </summary>
        /// <returns>是否基于高性能计数器</returns>
        public static bool IsHighResolution()
        {
            return Stopwatch.IsHighResolution;
        }

        /// <summary>
        /// 使用高精度计时器执行操作
        /// </summary>
        /// <param name="action">要执行的操作</param>
        /// <returns>执行耗时（纳秒）</returns>
        public static long TimeHighPrecision(Action action)
        {
            var start = GetTimestamp();
            action();
            var end = GetTimestamp();

            return (long)((end - start) * 1_000_000_000.0 / Stopwatch.Frequency);
        }

        /// <summary>
        /// 高精度多次执行操作并返回平均耗时
        /// </summary>
        /// <param name="action">要执行的操作</param>
        /// <param name="iterations">执行次数</param>
        /// <returns>平均执行耗时（纳秒）</returns>
        public static double TimeAverageHighPrecision(Action action, int iterations)
        {
            if (iterations <= 0)
                throw new ArgumentException("迭代次数必须大于0", nameof(iterations));

            var totalElapsed = 0L;
            for (int i = 0; i < iterations; i++)
            {
                totalElapsed += TimeHighPrecision(action);
            }
            return (double)totalElapsed / iterations;
        }

        /// <summary>
        /// 测量操作的CPU周期数
        /// </summary>
        /// <param name="action">要执行的操作</param>
        /// <returns>CPU周期数</returns>
        public static long MeasureCpuCycles(Action action)
        {
            // 注意：此方法在不同平台上可能不可用
            var start = GetTimestamp();
            action();
            var end = GetTimestamp();

            return end - start;
        }

        /// <summary>
        /// 创建高精度计时器
        /// </summary>
        /// <returns>新的高精度计时器</returns>
        public static HighPrecisionTimer CreateHighPrecisionTimer()
        {
            return new HighPrecisionTimer();
        }

        /// <summary>
        /// 使用自定义计时器执行操作
        /// </summary>
        /// <param name="action">要执行的操作</param>
        /// <returns>计时结果</returns>
        public static CustomTimingResult TimeWithCustomTimer(Action action)
        {
            var timer = new CustomStopwatch();
            var result = timer.Time(action);
            return result;
        }

        #endregion

        #region 条件计时与跟踪

        /// <summary>
        /// 条件性执行计时（仅在满足条件时计时）
        /// </summary>
        /// <param name="action">要执行的操作</param>
        /// <param name="condition">计时条件</param>
        /// <returns>执行耗时（毫秒），如果不满足条件则返回-1</returns>
        public static long TimeIf(Action action, Func<bool> condition)
        {
            if (condition())
            {
                return Time(action);
            }
            return -1;
        }

        /// <summary>
        /// 条件性执行计时并输出到控制台
        /// </summary>
        /// <param name="action">要执行的操作</param>
        /// <param name="operationName">操作名称</param>
        /// <param name="thresholdMs">阈值（毫秒），仅当超过此时间时才输出</param>
        public static void TimeAndTrace(Action action, string operationName, long thresholdMs = 0)
        {
            var elapsed = Time(action);
            if (elapsed >= thresholdMs)
            {
                Console.WriteLine($"{operationName} 耗时: {elapsed} ms");
            }
        }

        /// <summary>
        /// 计时并记录到日志（模拟实现）
        /// </summary>
        /// <param name="action">要执行的操作</param>
        /// <param name="operationName">操作名称</param>
        /// <param name="logAction">日志记录动作</param>
        public static void TimeAndLog(Action action, string operationName, Action<string> logAction)
        {
            var elapsed = Time(action);
            logAction?.Invoke($"{operationName} executed in {elapsed} ms");
        }

        /// <summary>
        /// 分段计时器，用于测量多个阶段的执行时间
        /// </summary>
        /// <returns>分段计时器实例</returns>
        public static SegmentedStopwatch CreateSegmentedStopwatch()
        {
            return new SegmentedStopwatch();
        }

        /// <summary>
        /// 执行操作并在超时时抛出异常
        /// </summary>
        /// <param name="action">要执行的操作</param>
        /// <param name="timeoutMs">超时时间（毫秒）</param>
        /// <exception cref="TimeoutException">当操作超时时抛出</exception>
        public static void TimeWithTimeout(Action action, int timeoutMs)
        {
            var task = Task.Run(action);
            if (!task.Wait(timeoutMs))
            {
                throw new TimeoutException($"操作在 {timeoutMs} ms 内未完成");
            }
        }

        /// <summary>
        /// 异步执行操作并在超时时抛出异常
        /// </summary>
        /// <param name="action">要执行的异步操作</param>
        /// <param name="timeoutMs">超时时间（毫秒）</param>
        /// <exception cref="TimeoutException">当操作超时时抛出</exception>
        public static async Task TimeWithTimeoutAsync(Func<Task> action, int timeoutMs)
        {
            var cts = new CancellationTokenSource(timeoutMs);
            try
            {
                var task = action();
                await task.WaitAsync(cts.Token);
            }
            catch (OperationCanceledException)
            {
                throw new TimeoutException($"操作在 {timeoutMs} ms 内未完成");
            }
        }

        /// <summary>
        /// 计时并重试操作直到成功或达到最大尝试次数
        /// </summary>
        /// <param name="action">要执行的操作</param>
        /// <param name="maxAttempts">最大尝试次数</param>
        /// <param name="retryIntervalMs">重试间隔（毫秒）</param>
        /// <returns>执行结果信息</returns>
        public static RetryResult TimeWithRetry(Action action, int maxAttempts, int retryIntervalMs = 1000)
        {
            var results = new List<long>();
            Exception lastException = null;

            for (int attempt = 1; attempt <= maxAttempts; attempt++)
            {
                try
                {
                    var elapsed = Time(action);
                    results.Add(elapsed);
                    return new RetryResult
                    {
                        Succeeded = true,
                        Attempts = attempt,
                        ExecutionTimes = results,
                        TotalTime = results.Sum()
                    };
                }
                catch (Exception ex)
                {
                    lastException = ex;
                    results.Add(-1); // -1表示失败

                    if (attempt < maxAttempts && retryIntervalMs > 0)
                    {
                        Task.Delay(retryIntervalMs).Wait();
                    }
                }
            }

            return new RetryResult
            {
                Succeeded = false,
                Attempts = maxAttempts,
                ExecutionTimes = results,
                TotalTime = 0,
                LastException = lastException
            };
        }

        /// <summary>
        /// 测量操作的内存分配（近似）
        /// </summary>
        /// <param name="action">要执行的操作</param>
        /// <returns>计时结果和内存使用信息</returns>
        public static MemoryTimingResult TimeWithMemoryTracking(Action action)
        {
            var gcCountBefore = GC.CollectionCount(0);
            var memoryBefore = GC.GetTotalMemory(false);

            var elapsed = TimePrecise(action);

            var memoryAfter = GC.GetTotalMemory(false);
            var gcCountAfter = GC.CollectionCount(0);

            return new MemoryTimingResult
            {
                Elapsed = elapsed,
                MemoryAllocated = memoryAfter - memoryBefore,
                GarbageCollections = gcCountAfter - gcCountBefore
            };
        }

        #endregion
    }

    #region 辅助类和数据结构

    /// <summary>
    /// 计时统计信息
    /// </summary>
    public class TimingStatistics
    {
        /// <summary>
        /// 执行次数
        /// </summary>
        public int Count { get; set; }

        /// <summary>
        /// 最短执行时间（毫秒）
        /// </summary>
        public long Min { get; set; }

        /// <summary>
        /// 最长执行时间（毫秒）
        /// </summary>
        public long Max { get; set; }

        /// <summary>
        /// 平均执行时间（毫秒）
        /// </summary>
        public double Average { get; set; }

        /// <summary>
        /// 总执行时间（毫秒）
        /// </summary>
        public long Sum { get; set; }

        /// <summary>
        /// 标准差
        /// </summary>
        public double StandardDeviation { get; set; }

        /// <summary>
        /// 格式化输出统计信息
        /// </summary>
        /// <returns>格式化的统计信息字符串</returns>
        public override string ToString()
        {
            return $"Count: {Count}, Min: {Min}ms, Max: {Max}ms, Average: {Average:F2}ms, StdDev: {StandardDeviation:F2}ms";
        }
    }

    /// <summary>
    /// 性能比较结果
    /// </summary>
    public class PerformanceComparison
    {
        /// <summary>
        /// 第一个操作的统计信息
        /// </summary>
        public TimingStatistics FirstOperationStats { get; set; }

        /// <summary>
        /// 第二个操作的统计信息
        /// </summary>
        public TimingStatistics SecondOperationStats { get; set; }

        /// <summary>
        /// 第一个操作是否更快
        /// </summary>
        public bool IsFirstFaster { get; set; }

        /// <summary>
        /// 性能比率（第一个操作时间/第二个操作时间）
        /// </summary>
        public double PerformanceRatio { get; set; }

        /// <summary>
        /// 格式化输出比较结果
        /// </summary>
        /// <returns>格式化的比较结果字符串</returns>
        public string ToFormattedString()
        {
            var fasterOp = IsFirstFaster ? "第一个" : "第二个";
            var ratio = IsFirstFaster ? PerformanceRatio : 1 / PerformanceRatio;
            return $"第一个操作平均耗时: {FirstOperationStats.Average:F2}ms\n" +
                   $"第二个操作平均耗时: {SecondOperationStats.Average:F2}ms\n" +
                   $"{fasterOp}操作快 {ratio:F2} 倍";
        }
    }

    /// <summary>
    /// 自定义计时器
    /// </summary>
    public class CustomStopwatch
    {
        private readonly Stopwatch _stopwatch;

        public CustomStopwatch()
        {
            _stopwatch = new Stopwatch();
        }

        /// <summary>
        /// 执行操作并返回计时结果
        /// </summary>
        /// <param name="action">要执行的操作</param>
        /// <returns>计时结果</returns>
        public CustomTimingResult Time(Action action)
        {
            _stopwatch.Restart();
            try
            {
                action();
                _stopwatch.Stop();
                return new CustomTimingResult
                {
                    Success = true,
                    Elapsed = _stopwatch.Elapsed,
                    ElapsedMilliseconds = _stopwatch.ElapsedMilliseconds
                };
            }
            catch (Exception ex)
            {
                _stopwatch.Stop();
                return new CustomTimingResult
                {
                    Success = false,
                    Elapsed = _stopwatch.Elapsed,
                    ElapsedMilliseconds = _stopwatch.ElapsedMilliseconds,
                    Exception = ex
                };
            }
        }
    }

    /// <summary>
    /// 自定义计时结果
    /// </summary>
    public class CustomTimingResult
    {
        /// <summary>
        /// 是否成功执行
        /// </summary>
        public bool Success { get; set; }

        /// <summary>
        /// 执行耗时
        /// </summary>
        public TimeSpan Elapsed { get; set; }

        /// <summary>
        /// 执行耗时（毫秒）
        /// </summary>
        public long ElapsedMilliseconds { get; set; }

        /// <summary>
        /// 异常信息（如果有）
        /// </summary>
        public Exception Exception { get; set; }
    }

    /// <summary>
    /// 分段计时器
    /// </summary>
    public class SegmentedStopwatch
    {
        private readonly Stopwatch _stopwatch;
        private readonly List<TimeSegment> _segments;
        private bool _isRunning;
        private long _lastSegmentStart;

        public SegmentedStopwatch()
        {
            _stopwatch = new Stopwatch();
            _segments = new List<TimeSegment>();
        }

        /// <summary>
        /// 开始计时
        /// </summary>
        public void Start()
        {
            _stopwatch.Start();
            _isRunning = true;
            _lastSegmentStart = 0;
            _segments.Clear();
        }

        /// <summary>
        /// 记录一个时间段
        /// </summary>
        /// <param name="name">时间段名称</param>
        public void Segment(string name)
        {
            if (!_isRunning)
                throw new InvalidOperationException("计时器未运行");

            var currentTime = _stopwatch.ElapsedMilliseconds;
            var segmentTime = currentTime - _lastSegmentStart;

            _segments.Add(new TimeSegment
            {
                Name = name,
                Duration = segmentTime
            });

            _lastSegmentStart = currentTime;
        }

        /// <summary>
        /// 停止计时
        /// </summary>
        public void Stop()
        {
            _stopwatch.Stop();
            _isRunning = false;
        }

        /// <summary>
        /// 获取所有时间段
        /// </summary>
        /// <returns>时间段列表</returns>
        public List<TimeSegment> GetSegments()
        {
            return new List<TimeSegment>(_segments);
        }

        /// <summary>
        /// 获取总计时间
        /// </summary>
        /// <returns>总计时间</returns>
        public long GetTotalTime()
        {
            return _stopwatch.ElapsedMilliseconds;
        }
    }

    /// <summary>
    /// 时间段
    /// </summary>
    public class TimeSegment
    {
        /// <summary>
        /// 段名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 持续时间（毫秒）
        /// </summary>
        public long Duration { get; set; }
    }

    /// <summary>
    /// 高精度计时器
    /// </summary>
    public class HighPrecisionTimer
    {
        private long _startTimestamp;
        private long _endTimestamp;
        private bool _isRunning;

        /// <summary>
        /// 开始计时
        /// </summary>
        public void Start()
        {
            _startTimestamp = Stopwatch.GetTimestamp();
            _isRunning = true;
        }

        /// <summary>
        /// 停止计时
        /// </summary>
        public void Stop()
        {
            if (_isRunning)
            {
                _endTimestamp = Stopwatch.GetTimestamp();
                _isRunning = false;
            }
        }

        /// <summary>
        /// 获取经过的时间（纳秒）
        /// </summary>
        /// <returns>经过的时间（纳秒）</returns>
        public long GetElapsedNanoseconds()
        {
            var end = _isRunning ? Stopwatch.GetTimestamp() : _endTimestamp;
            return (long)((end - _startTimestamp) * 1_000_000_000.0 / Stopwatch.Frequency);
        }

        /// <summary>
        /// 获取经过的时间
        /// </summary>
        /// <returns>经过的时间</returns>
        public TimeSpan GetElapsedTime()
        {
            var end = _isRunning ? Stopwatch.GetTimestamp() : _endTimestamp;
            return TimeSpan.FromTicks((long)((end - _startTimestamp) * TimeSpan.TicksPerSecond / Stopwatch.Frequency));
        }
    }

    /// <summary>
    /// 重试结果
    /// </summary>
    public class RetryResult
    {
        /// <summary>
        /// 是否成功
        /// </summary>
        public bool Succeeded { get; set; }

        /// <summary>
        /// 尝试次数
        /// </summary>
        public int Attempts { get; set; }

        /// <summary>
        /// 每次执行时间（毫秒），失败时为-1
        /// </summary>
        public List<long> ExecutionTimes { get; set; }

        /// <summary>
        /// 总时间（毫秒）
        /// </summary>
        public long TotalTime { get; set; }

        /// <summary>
        /// 最后一次异常（如果有）
        /// </summary>
        public Exception LastException { get; set; }
    }

    /// <summary>
    /// 内存计时结果
    /// </summary>
    public class MemoryTimingResult
    {
        /// <summary>
        /// 执行耗时
        /// </summary>
        public TimeSpan Elapsed { get; set; }

        /// <summary>
        /// 内存分配量（字节）
        /// </summary>
        public long MemoryAllocated { get; set; }

        /// <summary>
        /// 垃圾回收次数
        /// </summary>
        public int GarbageCollections { get; set; }
    }

    #endregion
}