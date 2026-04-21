# StopwatchHelper 帮助类

## 概述

[StopwatchHelper](../../Chet.Utils/Helpers/StopwatchHelper.cs) 是一个静态帮助类，为性能测量和计时提供了丰富的功能，包括基础计时、高精度计时、多次执行统计、条件计时、范围计时、基准测试、时间格式化等，旨在简化性能测量和计时的开发工作，提供友好的时间格式化输出。

## 主要特性

- 提供同步和异步的基础计时功能
- 支持多次执行的统计分析（平均时间、标准差、百分位数等）
- 提供高精度计时（纳秒、微秒级别）
- 支持带返回值的计时操作
- 提供条件计时和重试计时功能
- 支持范围计时（使用 using 语句）
- 提供基准测试工具
- 包含友好的时间格式化输出

## 类定义

```csharp
public static class StopwatchHelper
```

## 基础计时方法

### Time

执行操作并返回耗时（毫秒）。

```csharp
public static long Time(Action action)
```

**参数：**
- `action`: 要执行的操作

**返回值：**
- 执行耗时（毫秒）

**异常：**
- `ArgumentNullException`: action 为 null 时抛出

**示例：**
```csharp
long elapsedMs = StopwatchHelper.Time(() =>
{
    Thread.Sleep(100);
});
Console.WriteLine($"耗时: {elapsedMs} 毫秒");
```

### Time（带迭代次数）

执行操作多次并返回总耗时（毫秒）。

```csharp
public static long Time(Action action, int iterations)
```

**参数：**
- `action`: 要执行的操作
- `iterations`: 执行次数

**返回值：**
- 执行耗时（毫秒）

**异常：**
- `ArgumentNullException`: action 为 null 时抛出
- `ArgumentException`: iterations 小于等于 0 时抛出

### TimeAsync

执行异步操作并返回耗时（毫秒）。

```csharp
public static async Task<long> TimeAsync(Func<Task> action)
```

**参数：**
- `action`: 要执行的异步操作

**返回值：**
- 执行耗时（毫秒）

**示例：**
```csharp
long elapsedMs = await StopwatchHelper.TimeAsync(async () =>
{
    await Task.Delay(100);
});
Console.WriteLine($"耗时: {elapsedMs} 毫秒");
```

### TimeDetailed

执行操作并返回详细计时结果。

```csharp
public static TimeSpan TimeDetailed(Action action)
```

**参数：**
- `action`: 要执行的操作

**返回值：**
- 计时结果（TimeSpan）

**示例：**
```csharp
var result = StopwatchHelper.TimeDetailed(() =>
{
    Thread.Sleep(100);
});
Console.WriteLine($"耗时: {result.TotalMilliseconds} 毫秒");
Console.WriteLine($"耗时: {result.TotalMicroseconds} 微秒");
```

### Time（带返回值）

执行带返回值的操作并返回结果和耗时。

```csharp
public static (T Result, long ElapsedMilliseconds) Time<T>(Func<T> func)
```

**参数：**
- `func`: 要执行的操作

**返回值：**
- 执行结果和耗时（元组）

**示例：**
```csharp
var (result, elapsedMs) = StopwatchHelper.Time(() => ComputeSum(1, 100));
Console.WriteLine($"结果: {result}, 耗时: {elapsedMs} 毫秒");
```

### TimeAsync（带返回值）

执行带返回值的异步操作并返回结果和耗时。

```csharp
public static async Task<(T Result, long ElapsedMilliseconds)> TimeAsync<T>(Func<Task<T>> func)
```

**参数：**
- `func`: 要执行的异步操作

**返回值：**
- 执行结果和耗时（元组）

## 统计计时方法

### TimeStatistics

多次执行操作并返回详细统计信息。

```csharp
public static TimingStatistics TimeStatistics(Action action, int iterations)
```

**参数：**
- `action`: 要执行的操作
- `iterations`: 执行次数

**返回值：**
- 执行时间统计信息

**示例：**
```csharp
var stats = StopwatchHelper.TimeStatistics(() =>
{
    Thread.Sleep(10);
}, 100);
Console.WriteLine($"平均: {stats.AverageMilliseconds} 毫秒");
Console.WriteLine($"最小: {stats.MinMilliseconds} 毫秒");
Console.WriteLine($"最大: {stats.MaxMilliseconds} 毫秒");
Console.WriteLine($"中位数: {stats.MedianMilliseconds} 毫秒");
Console.WriteLine($"标准差: {stats.StandardDeviation}");
Console.WriteLine($"P90: {stats.P90Milliseconds} 毫秒");
Console.WriteLine($"P95: {stats.P95Milliseconds} 毫秒");
Console.WriteLine($"P99: {stats.P99Milliseconds} 毫秒");
```

### TimeStatisticsAsync

多次执行异步操作并返回详细统计信息。

```csharp
public static async Task<TimingStatistics> TimeStatisticsAsync(Func<Task> action, int iterations)
```

**参数：**
- `action`: 要执行的异步操作
- `iterations`: 执行次数

**返回值：**
- 执行时间统计信息

### TimeEach

多次执行操作并返回每次的耗时列表。

```csharp
public static List<long> TimeEach(Action action, int iterations)
```

**参数：**
- `action`: 要执行的操作
- `iterations`: 执行次数

**返回值：**
- 每次执行的耗时列表（毫秒）

### CalculateStatistics

计算统计信息。

```csharp
public static TimingStatistics CalculateStatistics(IEnumerable<long> times)
```

**参数：**
- `times`: 时间列表（毫秒）

**返回值：**
- 统计信息

## 高精度计时方法

### TimeHighPrecision

执行操作并返回高精度耗时（Ticks）。

```csharp
public static long TimeHighPrecision(Action action)
```

**参数：**
- `action`: 要执行的操作

**返回值：**
- 执行耗时（Ticks）

**示例：**
```csharp
long ticks = StopwatchHelper.TimeHighPrecision(() =>
{
    // 高精度操作
});
Console.WriteLine($"耗时: {ticks} Ticks");
```

### TimeNanoseconds

执行操作并返回纳秒级耗时。

```csharp
public static double TimeNanoseconds(Action action)
```

**参数：**
- `action`: 要执行的操作

**返回值：**
- 执行耗时（纳秒）

### TimeMicroseconds

执行操作并返回微秒级耗时。

```csharp
public static double TimeMicroseconds(Action action)
```

**参数：**
- `action`: 要执行的操作

**返回值：**
- 执行耗时（微秒）

### GetFrequency

获取计时器频率（每秒 Tick 数）。

```csharp
public static long GetFrequency()
```

**返回值：**
- 计时器频率

### IsHighResolution

检查计时器是否为高精度。

```csharp
public static bool IsHighResolution()
```

**返回值：**
- 是否为高精度计时器

### GetTimestamp

获取时间戳（Ticks）。

```csharp
public static long GetTimestamp()
```

**返回值：**
- 当前时间戳

### GetElapsedTime

计算两个时间戳之间的时间间隔。

```csharp
public static TimeSpan GetElapsedTime(long startTimestamp, long endTimestamp)
```

**参数：**
- `startTimestamp`: 开始时间戳
- `endTimestamp`: 结束时间戳

**返回值：**
- 时间间隔

## 条件计时方法

### TimeIf

条件执行的计时。

```csharp
public static long TimeIf(Action action, bool condition)
```

**参数：**
- `action`: 要执行的操作
- `condition`: 执行条件

**返回值：**
- 执行耗时（毫秒），条件不满足时返回 0

**示例：**
```csharp
long elapsedMs = StopwatchHelper.TimeIf(() =>
{
    Thread.Sleep(100);
}, shouldExecute);
```

### TimeWithRetry

带重试机制的计时。

```csharp
public static (long ElapsedMilliseconds, int RetryCount, bool Success) TimeWithRetry(Action action, int maxRetries, Func<Exception, bool> shouldRetry = null)
```

**参数：**
- `action`: 要执行的操作
- `maxRetries`: 最大重试次数
- `shouldRetry`: 判断是否重试的函数（可选）

**返回值：**
- 耗时、重试次数、是否成功

**示例：**
```csharp
var (elapsedMs, retryCount, success) = StopwatchHelper.TimeWithRetry(() =>
{
    // 可能失败的操作
}, 3);
Console.WriteLine($"耗时: {elapsedMs}ms, 重试: {retryCount}次, 成功: {success}");
```

## 范围计时方法

### Scope

创建范围计时器（使用 using 语句）。

```csharp
public static IDisposable Scope(Action<long> onDispose)
```

**参数：**
- `onDispose`: 范围结束时执行的回调（参数为耗时毫秒）

**返回值：**
- 可释放的范围计时器

**示例：**
```csharp
using (StopwatchHelper.Scope(ms => Console.WriteLine($"耗时: {ms}ms")))
{
    // 执行一些操作
    Thread.Sleep(100);
}
```

### ScopeAsync

创建异步范围计时器（使用 using 语句）。

```csharp
public static IAsyncDisposable ScopeAsync(Action<long> onDispose)
```

**参数：**
- `onDispose`: 范围结束时执行的回调（参数为耗时毫秒）

**返回值：**
- 可异步释放的范围计时器

**示例：**
```csharp
await using (StopwatchHelper.ScopeAsync(ms => Console.WriteLine($"耗时: {ms}ms")))
{
    // 执行一些异步操作
    await Task.Delay(100);
}
```

## 基准测试方法

### Benchmark

执行基准测试。

```csharp
public static BenchmarkResult Benchmark(Action action, int iterations = 100, int warmupIterations = 10)
```

**参数：**
- `action`: 要测试的操作
- `iterations`: 测试迭代次数，默认 100
- `warmupIterations`: 预热迭代次数，默认 10

**返回值：**
- 基准测试结果

**示例：**
```csharp
var result = StopwatchHelper.Benchmark(() =>
{
    // 要测试的代码
}, iterations: 1000);
Console.WriteLine($"平均: {result.AverageMilliseconds}ms");
Console.WriteLine($"最小: {result.MinMilliseconds}ms");
Console.WriteLine($"最大: {result.MaxMilliseconds}ms");
Console.WriteLine($"P95: {result.P95Milliseconds}ms");
```

### Compare

比较两个操作的性能。

```csharp
public static (BenchmarkResult First, BenchmarkResult Second) Compare(Action first, Action second, int iterations = 100)
```

**参数：**
- `first`: 第一个操作
- `second`: 第二个操作
- `iterations`: 测试迭代次数

**返回值：**
- 两个操作的基准测试结果

**示例：**
```csharp
var (result1, result2) = StopwatchHelper.Compare(
    () => MethodA(),
    () => MethodB(),
    iterations: 1000);
Console.WriteLine($"方法A平均: {result1.AverageMilliseconds}ms");
Console.WriteLine($"方法B平均: {result2.AverageMilliseconds}ms");
```

## 格式化方法

### FormatTime

格式化时间（自动选择合适的单位）。

```csharp
public static string FormatTime(TimeSpan time)
```

**参数：**
- `time`: 时间间隔

**返回值：**
- 格式化后的时间字符串

**示例：**
```csharp
var formatted = StopwatchHelper.FormatTime(TimeSpan.FromMilliseconds(1234));
// 输出: "1.23 秒"
```

### FormatTime（毫秒）

格式化时间（毫秒输入）。

```csharp
public static string FormatTime(double milliseconds)
```

**参数：**
- `milliseconds`: 毫秒数

**返回值：**
- 格式化后的时间字符串

**示例：**
```csharp
var formatted = StopwatchHelper.FormatTime(1234.5);
// 输出: "1.23 秒"
```

## 辅助类

### TimingStatistics

计时统计信息类。

**属性：**
- `Count`: 执行次数
- `TotalMilliseconds`: 总耗时（毫秒）
- `AverageMilliseconds`: 平均耗时（毫秒）
- `MinMilliseconds`: 最小耗时（毫秒）
- `MaxMilliseconds`: 最大耗时（毫秒）
- `MedianMilliseconds`: 中位数耗时（毫秒）
- `StandardDeviation`: 标准差
- `P90Milliseconds`: 第 90 百分位数（毫秒）
- `P95Milliseconds`: 第 95 百分位数（毫秒）
- `P99Milliseconds`: 第 99 百分位数（毫秒）

### BenchmarkResult

基准测试结果类。

**属性：**
- `Iterations`: 迭代次数
- `TotalMilliseconds`: 总耗时（毫秒）
- `AverageMilliseconds`: 平均耗时（毫秒）
- `MinMilliseconds`: 最小耗时（毫秒）
- `MaxMilliseconds`: 最大耗时（毫秒）
- `MedianMilliseconds`: 中位数耗时（毫秒）
- `StandardDeviation`: 标准差
- `P90Milliseconds`: 第 90 百分位数（毫秒）
- `P95Milliseconds`: 第 95 百分位数（毫秒）
- `P99Milliseconds`: 第 99 百分位数（毫秒）
- `OperationsPerSecond`: 每秒操作数

## 使用建议

1. **基础计时**：使用 `Time` 或 `TimeAsync` 方法进行简单的性能测量
2. **统计分析**：使用 `TimeStatistics` 方法进行多次执行的统计分析
3. **高精度计时**：使用 `TimeNanoseconds` 或 `TimeMicroseconds` 方法进行高精度测量
4. **基准测试**：使用 `Benchmark` 方法进行代码性能基准测试
5. **性能比较**：使用 `Compare` 方法比较不同实现的性能差异
6. **范围计时**：使用 `Scope` 方法配合 `using` 语句进行代码块级别的计时

## 注意事项

1. 计时结果受系统负载影响，建议多次测量取平均值
2. 首次执行可能较慢（JIT 编译），建议使用预热
3. 高精度计时依赖于硬件计时器的精度
4. 异步计时方法需要注意异步上下文的开销
