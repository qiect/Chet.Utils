# StopwatchHelper 帮助类

## 概述

`StopwatchHelper` 是一个全面的性能测量和计时工具类，提供了丰富的计时功能，包括基础计时、高精度计时、多次执行统计、性能比较、条件计时、分段计时等功能。该类使用静态方法设计，无需实例化即可直接使用，同时还提供了多个辅助类以支持更复杂的计时场景。

## 主要特性

- 提供同步和异步的基础计时功能
- 支持多次执行的统计分析（平均时间、标准差等）
- 提供高精度计时（纳秒级别）
- 支持两个操作的性能比较
- 包含条件计时和日志记录功能
- 提供分段计时器用于多阶段测量
- 支持超时控制和操作重试
- 提供内存使用跟踪功能
- 包含丰富的辅助类和数据结构

## 类定义

```csharp
public static class StopwatchHelper
{
    // 计时方法和辅助功能
}
```

## 基础计时方法

### 同步执行并计时

```csharp
public static long Time(Action action)
```

**参数说明：**
- `action`: 要执行的操作

**返回值：**
- 执行耗时（毫秒）

### 同步执行并高精度计时

```csharp
public static TimeSpan TimePrecise(Action action)
```

**参数说明：**
- `action`: 要执行的操作

**返回值：**
- 执行耗时（精确时间跨度）

### 异步执行并计时

```csharp
public static async Task<long> TimeAsync(Func<Task> action)
```

**参数说明：**
- `action`: 要执行的异步操作

**返回值：**
- 执行耗时（毫秒）

### 异步执行并高精度计时

```csharp
public static async Task<TimeSpan> TimePreciseAsync(Func<Task> action)
```

**参数说明：**
- `action`: 要执行的异步操作

**返回值：**
- 执行耗时（精确时间跨度）

### 创建并启动计时器

```csharp
public static Stopwatch StartNew()
```

**返回值：**
- 已启动的计时器实例

## 多次执行测量

### 多次执行并计算平均时间

```csharp
public static double TimeAverage(Action action, int iterations)
```

**参数说明：**
- `action`: 要执行的操作
- `iterations`: 执行次数

**返回值：**
- 平均执行耗时（毫秒）

### 多次执行并返回统计信息

```csharp
public static TimingStatistics TimeStatistics(Action action, int iterations)
```

**参数说明：**
- `action`: 要执行的操作
- `iterations`: 执行次数

**返回值：**
- 执行时间统计信息（包含最小值、最大值、平均值、标准差等）

### 异步多次执行并计算平均时间

```csharp
public static async Task<double> TimeAverageAsync(Func<Task> action, int iterations)
```

**参数说明：**
- `action`: 要执行的异步操作
- `iterations`: 执行次数

**返回值：**
- 平均执行耗时（毫秒）

### 异步多次执行并返回统计信息

```csharp
public static async Task<TimingStatistics> TimeStatisticsAsync(Func<Task> action, int iterations)
```

**参数说明：**
- `action`: 要执行的异步操作
- `iterations`: 执行次数

**返回值：**
- 执行时间统计信息

### 并发执行并测量总时间

```csharp
public static long TimeConcurrent(Action action, int concurrentCount)
```

**参数说明：**
- `action`: 要执行的操作
- `concurrentCount`: 并发数

**返回值：**
- 并发执行总耗时（毫秒）

### 异步并发执行并测量总时间

```csharp
public static async Task<long> TimeConcurrentAsync(Func<Task> action, int concurrentCount)
```

**参数说明：**
- `action`: 要执行的异步操作
- `concurrentCount`: 并发数

**返回值：**
- 并发执行总耗时（毫秒）

### 比较两个操作的性能

```csharp
public static PerformanceComparison Compare(Action action1, Action action2, int iterations)
```

**参数说明：**
- `action1`: 第一个操作
- `action2`: 第二个操作
- `iterations`: 每项操作的执行次数

**返回值：**
- 性能比较结果（包含两个操作的统计信息和性能比率）

### 异步比较两个操作的性能

```csharp
public static async Task<PerformanceComparison> CompareAsync(Func<Task> action1, Func<Task> action2, int iterations)
```

**参数说明：**
- `action1`: 第一个异步操作
- `action2`: 第二个异步操作
- `iterations`: 每项操作的执行次数

**返回值：**
- 性能比较结果

## 高精度计时

### 获取高精度时间戳

```csharp
public static long GetTimestamp()
```

**返回值：**
- 高精度时间戳

### 将时间戳转换为时间间隔

```csharp
public static TimeSpan TimestampToTimeSpan(long timestamp)
```

**参数说明：**
- `timestamp`: 时间戳

**返回值：**
- 时间间隔

### 获取计时器频率

```csharp
public static long GetFrequency()
```

**返回值：**
- 计时器频率

### 检查计时器是否基于高性能计数器

```csharp
public static bool IsHighResolution()
```

**返回值：**
- 是否基于高性能计数器

### 使用高精度计时器执行操作

```csharp
public static long TimeHighPrecision(Action action)
```

**参数说明：**
- `action`: 要执行的操作

**返回值：**
- 执行耗时（纳秒）

### 高精度多次执行并计算平均时间

```csharp
public static double TimeAverageHighPrecision(Action action, int iterations)
```

**参数说明：**
- `action`: 要执行的操作
- `iterations`: 执行次数

**返回值：**
- 平均执行耗时（纳秒）

### 测量操作的CPU周期数

```csharp
public static long MeasureCpuCycles(Action action)
```

**参数说明：**
- `action`: 要执行的操作

**返回值：**
- CPU周期数

### 创建高精度计时器

```csharp
public static HighPrecisionTimer CreateHighPrecisionTimer()
```

**返回值：**
- 新的高精度计时器

## 条件计时与跟踪

### 条件性执行计时

```csharp
public static long TimeIf(Action action, Func<bool> condition)
```

**参数说明：**
- `action`: 要执行的操作
- `condition`: 计时条件

**返回值：**
- 执行耗时（毫秒），如果不满足条件则返回-1

### 条件性执行计时并输出到控制台

```csharp
public static void TimeAndTrace(Action action, string operationName, long thresholdMs = 0)
```

**参数说明：**
- `action`: 要执行的操作
- `operationName`: 操作名称
- `thresholdMs`: 阈值（毫秒），仅当超过此时间时才输出

### 计时并记录到日志

```csharp
public static void TimeAndLog(Action action, string operationName, Action<string> logAction)
```

**参数说明：**
- `action`: 要执行的操作
- `operationName`: 操作名称
- `logAction`: 日志记录动作

### 创建分段计时器

```csharp
public static SegmentedStopwatch CreateSegmentedStopwatch()
```

**返回值：**
- 分段计时器实例

### 执行操作并在超时时抛出异常

```csharp
public static void TimeWithTimeout(Action action, int timeoutMs)
```

**参数说明：**
- `action`: 要执行的操作
- `timeoutMs`: 超时时间（毫秒）

**异常：**
- `TimeoutException`: 当操作超时时抛出

### 异步执行操作并在超时时抛出异常

```csharp
public static async Task TimeWithTimeoutAsync(Func<Task> action, int timeoutMs)
```

**参数说明：**
- `action`: 要执行的异步操作
- `timeoutMs`: 超时时间（毫秒）

**异常：**
- `TimeoutException`: 当操作超时时抛出

### 计时并重试操作

```csharp
public static RetryResult TimeWithRetry(Action action, int maxAttempts, int retryIntervalMs = 1000)
```

**参数说明：**
- `action`: 要执行的操作
- `maxAttempts`: 最大尝试次数
- `retryIntervalMs`: 重试间隔（毫秒）

**返回值：**
- 执行结果信息（包含成功状态、尝试次数、执行时间等）

### 测量操作的内存分配

```csharp
public static MemoryTimingResult TimeWithMemoryTracking(Action action)
```

**参数说明：**
- `action`: 要执行的操作

**返回值：**
- 计时结果和内存使用信息

## 辅助类和数据结构

### 计时统计信息

```csharp
public class TimingStatistics
{
    public int Count { get; set; }           // 执行次数
    public long Min { get; set; }            // 最短执行时间（毫秒）
    public long Max { get; set; }            // 最长执行时间（毫秒）
    public double Average { get; set; }      // 平均执行时间（毫秒）
    public long Sum { get; set; }            // 总执行时间（毫秒）
    public double StandardDeviation { get; set; } // 标准差
}
```

### 性能比较结果

```csharp
public class PerformanceComparison
{
    public TimingStatistics FirstOperationStats { get; set; }   // 第一个操作的统计信息
    public TimingStatistics SecondOperationStats { get; set; }  // 第二个操作的统计信息
    public bool IsFirstFaster { get; set; }                     // 第一个操作是否更快
    public double PerformanceRatio { get; set; }                // 性能比率
}
```

### 自定义计时器

```csharp
public class CustomStopwatch
{
    public CustomStopwatch();  // 构造函数
    public CustomTimingResult Time(Action action);  // 执行操作并返回计时结果
}
```

### 自定义计时结果

```csharp
public class CustomTimingResult
{
    public bool Success { get; set; }                    // 是否成功执行
    public TimeSpan Elapsed { get; set; }                // 执行耗时
    public long ElapsedMilliseconds { get; set; }        // 执行耗时（毫秒）
    public Exception Exception { get; set; }             // 异常信息（如果有）
}
```

### 分段计时器

```csharp
public class SegmentedStopwatch
{
    public SegmentedStopwatch();  // 构造函数
    public void Start();          // 开始计时
    public void Segment(string name);  // 记录一个时间段
    public void Stop();           // 停止计时
    public List<TimeSegment> GetSegments();  // 获取所有时间段
    public long GetTotalTime();   // 获取总计时间
}
```

### 时间段

```csharp
public class TimeSegment
{
    public string Name { get; set; }      // 段名称
    public long Duration { get; set; }    // 持续时间（毫秒）
}
```

### 高精度计时器

```csharp
public class HighPrecisionTimer
{
    public void Start();                // 开始计时
    public void Stop();                 // 停止计时
    public long GetElapsedNanoseconds();  // 获取经过的时间（纳秒）
    public TimeSpan GetElapsedTime();    // 获取经过的时间
}
```

### 重试结果

```csharp
public class RetryResult
{
    public bool Succeeded { get; set; }                // 是否成功
    public int Attempts { get; set; }                  // 尝试次数
    public List<long> ExecutionTimes { get; set; }     // 每次执行时间（毫秒），失败时为-1
    public long TotalTime { get; set; }                // 总时间（毫秒）
    public Exception LastException { get; set; }       // 最后一次异常（如果有）
}
```

### 内存计时结果

```csharp
public class MemoryTimingResult
{
    public TimeSpan Elapsed { get; set; }              // 执行耗时
    public long MemoryAllocated { get; set; }          // 内存分配量（字节）
    public int GarbageCollections { get; set; }        // 垃圾回收次数
}
```

## 使用示例

### 基础计时示例

```csharp
// 测量代码执行时间
long elapsed = StopwatchHelper.Time(() => {
    // 要测量的操作
    for (int i = 0; i < 1000000; i++) {
        Math.Sqrt(i);
    }
});
Console.WriteLine($"执行耗时: {elapsed} 毫秒");
```

### 异步操作计时示例

```csharp
// 测量异步操作的执行时间
long elapsed = await StopwatchHelper.TimeAsync(async () => {
    // 异步操作
    await Task.Delay(1000);
});
Console.WriteLine($"异步操作耗时: {elapsed} 毫秒");
```

### 多次执行统计示例

```csharp
// 多次执行并获取统计信息
TimingStatistics stats = StopwatchHelper.TimeStatistics(() => {
    // 要测量的操作
    Thread.Sleep(100);
}, 10);

Console.WriteLine(stats.ToString());
// 输出类似: Count: 10, Min: 100ms, Max: 102ms, Average: 100.80ms, StdDev: 0.79ms
```

### 性能比较示例

```csharp
// 比较两个不同算法的性能
PerformanceComparison comparison = StopwatchHelper.Compare(
    () => {
        // 算法1
        for (int i = 0; i < 1000000; i++) {
            Math.Sqrt(i);
        }
    },
    () => {
        // 算法2
        for (int i = 0; i < 1000000; i++) {
            Math.Pow(i, 0.5);
        }
    },
    5
);

Console.WriteLine(comparison.ToFormattedString());
```

### 分段计时示例

```csharp
// 使用分段计时器测量多阶段操作
var segmentedTimer = StopwatchHelper.CreateSegmentedStopwatch();
segmentedTimer.Start();

// 第一阶段
DoFirstOperation();
segmentedTimer.Segment("初始化");

// 第二阶段
DoSecondOperation();
segmentedTimer.Segment("处理");

// 第三阶段
DoThirdOperation();
segmentedTimer.Stop();

// 获取各段时间
foreach (var segment in segmentedTimer.GetSegments()) {
    Console.WriteLine($"{segment.Name}: {segment.Duration} 毫秒");
}

Console.WriteLine($"总时间: {segmentedTimer.GetTotalTime()} 毫秒");
```

### 内存使用跟踪示例

```csharp
// 跟踪内存使用情况
MemoryTimingResult memoryResult = StopwatchHelper.TimeWithMemoryTracking(() => {
    // 创建大量对象
    var list = new List<string>();
    for (int i = 0; i < 100000; i++) {
        list.Add(new string('x', 100));
    }
});

Console.WriteLine($"执行耗时: {memoryResult.Elapsed.TotalMilliseconds:F2} 毫秒");
Console.WriteLine($"内存分配: {memoryResult.MemoryAllocated / 1024 / 1024:F2} MB");
Console.WriteLine($"垃圾回收次数: {memoryResult.GarbageCollections}");
```

## 最佳实践

1. **选择合适的精度**：根据需要选择合适的计时方法，一般场景使用`Time`方法，高精度需求使用`TimeHighPrecision`方法。

2. **预热处理**：在进行精确测量前，先执行几次操作进行JIT编译和缓存预热。

3. **多次测量取平均**：单次测量可能受系统干扰较大，建议使用`TimeStatistics`方法进行多次测量。

4. **避免控制台输出影响测量**：计时代码块内尽量避免包含控制台输出等I/O操作。

5. **资源释放**：使用`SegmentedStopwatch`等辅助类时，确保调用`Stop`方法释放资源。

## 注意事项

1. 计时结果可能受系统负载、CPU频率调整等因素影响。

2. 高精度计时在某些平台上可能不可用，可通过`IsHighResolution()`方法检查。

3. 内存跟踪功能提供的是近似值，实际内存使用可能受GC影响。

4. 并发测量时，应注意操作本身是否线程安全。

5. `MeasureCpuCycles`方法在不同平台上的行为可能不一致。

## 版本兼容性

- .NET Framework 4.0 及以上版本
- .NET Core 2.0 及以上版本
- .NET 5.0 及以上版本

## 故障排除

### 常见问题

1. **计时结果不稳定**
   - 问题：连续多次测量结果差异较大
   - 解决：使用`TimeStatistics`方法进行多次测量取平均值

2. **高精度计时不工作**
   - 问题：高精度计时方法返回异常结果
   - 解决：先使用`IsHighResolution()`检查是否支持高精度计时

3. **并发测量出错**
   - 问题：并发执行时出现线程安全问题
   - 解决：确保被测量的操作是线程安全的，或为每个线程创建独立的资源

4. **内存测量不准确**
   - 问题：内存使用统计与实际不符
   - 解决：测量前调用`GC.Collect()`强制垃圾回收，减少干扰