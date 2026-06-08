---
title: "TaskHelper"
description: "是一个静态帮助类，为异步任务提供了丰富的管理功能，包括任务创建、任务等待、超时控制、重试机制、断路器、并行处理、任务限流、任务队列、任务池、任务监控、任务取消、任务结果处理、任务异常处理、任务协调等，旨在简化复杂的异步编程场景，提高代码的可读性和可维护性。"
namespace: "Chet.Utils.Helpers"
className: "TaskHelper"
category: "Helpers"
order: 8
---

# TaskHelper 帮助类

## 概述

[TaskHelper](../../Chet.Utils/Helpers/TaskHelper.cs) 是一个静态帮助类，为异步任务提供了丰富的管理功能，包括任务创建、任务等待、超时控制、重试机制、断路器、并行处理、任务限流、任务队列、任务池、任务监控、任务取消、任务结果处理、任务异常处理、任务协调等，旨在简化复杂的异步编程场景，提高代码的可读性和可维护性。

## 主要功能模块

### 1. 任务创建

提供多种方式创建异步任务。

**主要方法：**
- `Run()` - 创建并启动一个异步任务
- `Run<T>()` - 创建并启动一个带返回值的异步任务
- `RunAsync()` - 创建并启动一个异步任务（异步委托版本）
- `RunAsync<T>()` - 创建并启动一个带返回值的异步任务（异步委托版本）
- `Delay()` - 创建一个延迟任务
- `CompletedTask()` - 创建一个已完成的任务
- `FromResult<T>()` - 创建一个已完成的任务（带返回值）
- `FromException()` - 创建一个失败的任务
- `FromException<T>()` - 创建一个失败的任务（带返回值类型）
- `FromCanceled()` - 创建一个已取消的任务
- `FromCanceled<T>()` - 创建一个已取消的任务（带返回值类型）

### 2. 任务等待

提供任务等待功能。

**主要方法：**
- `WhenAll()` - 等待所有任务完成
- `WhenAll<T>()` - 等待所有任务完成（泛型版本）
- `WhenAny()` - 等待任意一个任务完成
- `WhenAny<T>()` - 等待任意一个任务完成（泛型版本）
- `Wait()` - 等待任务完成（同步阻塞）

### 3. 超时控制

提供任务超时控制功能。

**主要方法：**
- `WithTimeout()` - 为任务添加超时控制
- `WithTimeout<T>()` - 为任务添加超时控制（泛型版本）
- `TryWithTimeout()` - 尝试在超时时间内完成任务
- `TryWithTimeout<T>()` - 尝试在超时时间内完成任务（泛型版本）

### 4. 重试机制

提供任务重试功能。

**主要方法：**
- `RetryAsync()` - 带重试机制的任务执行
- `RetryAsync<T>()` - 带重试机制的任务执行（泛型版本）
- `RetryWithExponentialBackoffAsync()` - 带指数退避重试机制的任务执行
- `RetryWhenAsync()` - 带条件判断的重试机制

### 5. 并行处理

提供高效的任务并行处理功能。

**主要方法：**
- `ParallelForEachAsync()` - 并行执行多个任务
- `ParallelForEachAsync<T>()` - 并行处理数据源中的每个元素
- `ParallelSelectAsync<T, TResult>()` - 并行处理并返回有序结果
- `ParallelFirstOrDefaultAsync<T>()` - 并行执行任务直到满足条件

### 6. 任务限流

提供任务限流和批量处理功能。

**主要方法：**
- `ThrottleAsync()` - 限流执行任务
- `DebounceAsync()` - 节流执行任务
- `BatchProcessAsync<T>()` - 批量执行任务

### 7. 任务链与管道

提供任务的链式处理和管道功能。

**主要方法：**
- `ExecuteChainAsync()` - 顺序执行任务链
- `ExecutePipelineAsync<T, TResult>()` - 顺序执行任务链（带返回值传递）
- `PipeAsync<TInput, TOutput>()` - 创建任务管道

### 8. 任务监控

提供任务执行的监控功能。

**主要方法：**
- `MonitorAsync()` - 监控任务执行性能
- `MonitorAsync<T>()` - 监控任务执行性能（泛型版本）
- `MeasureAsync()` - 测量任务执行时间
- `MeasureAsync<T>()` - 测量任务执行时间（泛型版本）

### 9. 任务取消

提供任务取消控制功能。

**主要方法：**
- `WithCancellation()` - 创建可取消的任务
- `WithCancellationAsync()` - 创建可取消的任务（异步版本）
- `WithCancellation<T>()` - 创建可取消的任务（泛型版本）

### 10. 任务结果处理

提供任务结果处理功能。

**主要方法：**
- `HandleResultAsync<T>()` - 处理任务结果
- `GetResultOrDefaultAsync<T>()` - 获取任务结果或默认值

### 11. 任务异常处理

提供任务异常处理功能。

**主要方法：**
- `SafeExecuteAsync()` - 安全执行任务（捕获所有异常）
- `SafeExecuteAsync<T>()` - 安全执行任务（泛型版本）
- `IgnoreExceptionAsync()` - 忽略任务异常
- `IgnoreExceptionAsync<TException>()` - 忽略指定类型的异常

### 12. 任务协调

提供任务协调同步功能。

**主要方法：**
- `CreateBarrier()` - 创建任务屏障
- `CreateSemaphore()` - 创建异步信号量
- `CreateManualResetEvent()` - 创建异步手动重置事件
- `CreateAutoResetEvent()` - 创建异步自动重置事件

## 方法详解

## 任务创建

### Run

创建并启动一个异步任务。

```csharp
public static Task Run(Action action, CancellationToken cancellationToken = default)
```

**参数：**
- `action`: 要执行的操作
- `cancellationToken`: 取消令牌

**返回值：**
- Task 实例

**示例：**
```csharp
await TaskHelper.Run(() =>
{
    Console.WriteLine("任务执行中...");
});
```

### Run<T>

创建并启动一个带返回值的异步任务。

```csharp
public static Task<T> Run<T>(Func<T> function, CancellationToken cancellationToken = default)
```

**参数：**
- `function`: 要执行的函数
- `cancellationToken`: 取消令牌

**返回值：**
- Task<T> 实例

**示例：**
```csharp
var result = await TaskHelper.Run(() => ComputeSum(1, 100));
Console.WriteLine($"结果: {result}");
```

### RunAsync

创建并启动一个异步任务（异步委托版本）。

```csharp
public static Task RunAsync(Func<Task> asyncAction, CancellationToken cancellationToken = default)
```

**参数：**
- `asyncAction`: 要执行的异步操作
- `cancellationToken`: 取消令牌

**返回值：**
- Task 实例

**示例：**
```csharp
await TaskHelper.RunAsync(async () =>
{
    await Task.Delay(100);
    Console.WriteLine("异步任务执行完成");
});
```

### Delay

创建一个延迟任务。

```csharp
public static Task Delay(TimeSpan delay, CancellationToken cancellationToken = default)
public static Task Delay(int millisecondsDelay, CancellationToken cancellationToken = default)
```

**参数：**
- `delay`: 延迟时间
- `millisecondsDelay`: 延迟毫秒数
- `cancellationToken`: 取消令牌

**返回值：**
- Task 实例

**示例：**
```csharp
await TaskHelper.Delay(1000); // 延迟 1 秒
await TaskHelper.Delay(TimeSpan.FromSeconds(2)); // 延迟 2 秒
```

### CompletedTask

创建一个已完成的任务。

```csharp
public static Task CompletedTask()
```

**返回值：**
- 已完成的 Task 实例

### FromResult<T>

创建一个已完成的任务（带返回值）。

```csharp
public static Task<T> FromResult<T>(T result)
```

**参数：**
- `result`: 返回值

**返回值：**
- 已完成的 Task<T> 实例

**示例：**
```csharp
var task = TaskHelper.FromResult(42);
var result = await task; // result = 42
```

### FromException

创建一个失败的任务。

```csharp
public static Task FromException(Exception exception)
public static Task<T> FromException<T>(Exception exception)
```

**参数：**
- `exception`: 异常

**返回值：**
- 失败的 Task 实例

## 任务等待

### WhenAll

等待所有任务完成。

```csharp
public static Task WhenAll(params Task[] tasks)
public static Task<T[]> WhenAll<T>(params Task<T>[] tasks)
```

**参数：**
- `tasks`: 任务数组

**返回值：**
- Task 实例

**示例：**
```csharp
var tasks = new[]
{
    TaskHelper.Run(() => DoWork1()),
    TaskHelper.Run(() => DoWork2()),
    TaskHelper.Run(() => DoWork3())
};
await TaskHelper.WhenAll(tasks);
```

### WhenAny

等待任意一个任务完成。

```csharp
public static Task<Task> WhenAny(params Task[] tasks)
public static Task<Task<T>> WhenAny<T>(params Task<T>[] tasks)
```

**参数：**
- `tasks`: 任务数组

**返回值：**
- 第一个完成的任务

**示例：**
```csharp
var tasks = new[]
{
    TaskHelper.Run(() => FastOperation()),
    TaskHelper.Run(() => SlowOperation())
};
var firstCompleted = await TaskHelper.WhenAny(tasks);
Console.WriteLine("第一个任务完成了");
```

### Wait

等待任务完成（同步阻塞）。

```csharp
public static bool Wait(Task task, TimeSpan? timeout = null)
```

**参数：**
- `task`: 要等待的任务
- `timeout`: 超时时间

**返回值：**
- 是否在超时前完成

**示例：**
```csharp
var task = TaskHelper.Delay(1000);
var completed = TaskHelper.Wait(task, TimeSpan.FromSeconds(2));
Console.WriteLine($"任务完成: {completed}");
```

## 超时控制

### WithTimeout

为任务添加超时控制。

```csharp
public static async Task WithTimeout(Task task, TimeSpan timeout)
public static async Task<T> WithTimeout<T>(Task<T> task, TimeSpan timeout)
```

**参数：**
- `task`: 要执行的任务
- `timeout`: 超时时间

**返回值：**
- Task 实例或任务返回值

**异常：**
- `TimeoutException`: 任务在指定时间内未完成时抛出

**示例：**
```csharp
try
{
    var result = await someTask.WithTimeout(TimeSpan.FromSeconds(5));
}
catch (TimeoutException)
{
    Console.WriteLine("任务超时");
}
```

### TryWithTimeout

尝试在超时时间内完成任务。

```csharp
public static async Task<bool> TryWithTimeout(Task task, TimeSpan timeout)
public static async Task<(bool Success, T Result)> TryWithTimeout<T>(Task<T> task, TimeSpan timeout)
```

**参数：**
- `task`: 要执行的任务
- `timeout`: 超时时间

**返回值：**
- 是否在超时前完成（或包含结果的元组）

**示例：**
```csharp
var (success, result) = await someTask.TryWithTimeout(TimeSpan.FromSeconds(5));
if (success)
{
    Console.WriteLine($"结果: {result}");
}
else
{
    Console.WriteLine("任务超时");
}
```

## 重试机制

### RetryAsync

带重试机制的任务执行。

```csharp
public static async Task RetryAsync(Func<Task> taskFactory, int maxRetries, TimeSpan delay, CancellationToken cancellationToken = default)
public static async Task<T> RetryAsync<T>(Func<Task<T>> taskFactory, int maxRetries, TimeSpan delay, CancellationToken cancellationToken = default)
```

**参数：**
- `taskFactory`: 任务工厂函数
- `maxRetries`: 最大重试次数
- `delay`: 重试间隔
- `cancellationToken`: 取消令牌

**返回值：**
- Task 实例或任务返回值

**示例：**
```csharp
await TaskHelper.RetryAsync(async () =>
{
    await CallExternalApi();
}, maxRetries: 3, delay: TimeSpan.FromSeconds(1));
```

### RetryWithExponentialBackoffAsync

带指数退避重试机制的任务执行。

```csharp
public static async Task RetryWithExponentialBackoffAsync(Func<Task> taskFactory, int maxRetries, TimeSpan initialDelay, TimeSpan? maxDelay = null, CancellationToken cancellationToken = default)
```

**参数：**
- `taskFactory`: 任务工厂函数
- `maxRetries`: 最大重试次数
- `initialDelay`: 初始延迟时间
- `maxDelay`: 最大延迟时间
- `cancellationToken`: 取消令牌

**返回值：**
- Task 实例

**示例：**
```csharp
await TaskHelper.RetryWithExponentialBackoffAsync(async () =>
{
    await CallExternalApi();
}, maxRetries: 5, initialDelay: TimeSpan.FromSeconds(1), maxDelay: TimeSpan.FromMinutes(1));
```

### RetryWhenAsync

带条件判断的重试机制。

```csharp
public static async Task RetryWhenAsync(Func<Task> taskFactory, Func<Exception, int, bool> shouldRetry, int maxRetries, TimeSpan delay, CancellationToken cancellationToken = default)
```

**参数：**
- `taskFactory`: 任务工厂函数
- `shouldRetry`: 判断是否需要重试的函数
- `maxRetries`: 最大重试次数
- `delay`: 重试间隔
- `cancellationToken`: 取消令牌

**返回值：**
- Task 实例

**示例：**
```csharp
await TaskHelper.RetryWhenAsync(
    async () => await CallExternalApi(),
    (ex, retryCount) => ex is HttpRequestException,
    maxRetries: 3,
    delay: TimeSpan.FromSeconds(1)
);
```

## 并行处理

### ParallelForEachAsync

并行执行多个任务。

```csharp
public static async Task ParallelForEachAsync(IEnumerable<Func<Task>> actions, int maxDegreeOfParallelism = -1, CancellationToken cancellationToken = default)
public static async Task ParallelForEachAsync<T>(IEnumerable<T> source, Func<T, Task> body, int maxDegreeOfParallelism = -1, CancellationToken cancellationToken = default)
```

**参数：**
- `actions`: 要执行的操作列表
- `source`: 数据源
- `body`: 处理函数
- `maxDegreeOfParallelism`: 最大并行度，-1 表示不限制
- `cancellationToken`: 取消令牌

**返回值：**
- Task 实例

**示例：**
```csharp
var items = Enumerable.Range(1, 100);
await TaskHelper.ParallelForEachAsync(items, async item =>
{
    await ProcessItem(item);
}, maxDegreeOfParallelism: 10);
```

### ParallelSelectAsync

并行处理并返回有序结果。

```csharp
public static async Task<List<TResult>> ParallelSelectAsync<T, TResult>(IEnumerable<T> source, Func<T, Task<TResult>> selector, int maxDegreeOfParallelism = -1, CancellationToken cancellationToken = default)
```

**参数：**
- `source`: 数据源
- `selector`: 转换函数
- `maxDegreeOfParallelism`: 最大并行度
- `cancellationToken`: 取消令牌

**返回值：**
- 结果列表（保持原始顺序）

**示例：**
```csharp
var items = Enumerable.Range(1, 10);
var results = await TaskHelper.ParallelSelectAsync(items, async item =>
{
    await Task.Delay(100);
    return item * 2;
}, maxDegreeOfParallelism: 5);
// results = [2, 4, 6, 8, 10, 12, 14, 16, 18, 20]
```

### ParallelFirstOrDefaultAsync

并行执行任务直到满足条件。

```csharp
public static async Task<T?> ParallelFirstOrDefaultAsync<T>(IEnumerable<T> source, Func<T, Task<bool>> predicate, int maxDegreeOfParallelism = -1, CancellationToken cancellationToken = default)
```

**参数：**
- `source`: 数据源
- `predicate`: 条件判断函数
- `maxDegreeOfParallelism`: 最大并行度
- `cancellationToken`: 取消令牌

**返回值：**
- 第一个满足条件的元素

**示例：**
```csharp
var items = Enumerable.Range(1, 100);
var result = await TaskHelper.ParallelFirstOrDefaultAsync(items, async item =>
{
    await Task.Delay(10);
    return item > 50;
});
// result > 50
```

## 任务限流

### ThrottleAsync

限流执行任务。

```csharp
public static async Task ThrottleAsync(Func<Task> action, TimeSpan interval, CancellationToken cancellationToken = default)
```

**参数：**
- `action`: 要执行的操作
- `interval`: 执行间隔
- `cancellationToken`: 取消令牌

**返回值：**
- Task 实例

**示例：**
```csharp
await TaskHelper.ThrottleAsync(async () =>
{
    await CallApi();
}, TimeSpan.FromMilliseconds(100));
```

### DebounceAsync

节流执行任务（在指定时间窗口内只执行一次）。

```csharp
public static async Task DebounceAsync(Func<Task> action, TimeSpan interval, CancellationToken cancellationToken = default)
```

**参数：**
- `action`: 要执行的操作
- `interval`: 时间间隔
- `cancellationToken`: 取消令牌

**返回值：**
- Task 实例

**示例：**
```csharp
await TaskHelper.DebounceAsync(async () =>
{
    await SaveData();
}, TimeSpan.FromMilliseconds(300));
```

### BatchProcessAsync

批量执行任务（收集一段时间内的请求批量处理）。

```csharp
public static async Task BatchProcessAsync<T>(IEnumerable<T> source, int batchSize, TimeSpan batchInterval, Func<List<T>, Task> processor, CancellationToken cancellationToken = default)
```

**参数：**
- `source`: 数据源
- `batchSize`: 批量大小
- `batchInterval`: 批量间隔
- `processor`: 批量处理函数
- `cancellationToken`: 取消令牌

**返回值：**
- Task 实例

**示例：**
```csharp
var items = Enumerable.Range(1, 100);
await TaskHelper.BatchProcessAsync(items, 10, TimeSpan.FromMilliseconds(100), async batch =>
{
    await ProcessBatch(batch);
});
```

## 任务链与管道

### ExecuteChainAsync

顺序执行任务链。

```csharp
public static async Task ExecuteChainAsync(IEnumerable<Func<Task>> tasks, CancellationToken cancellationToken = default)
```

**参数：**
- `tasks`: 任务列表
- `cancellationToken`: 取消令牌

**返回值：**
- Task 实例

**示例：**
```csharp
await TaskHelper.ExecuteChainAsync(new List<Func<Task>>
{
    () => TaskHelper.Run(() => Console.WriteLine("步骤1")),
    () => TaskHelper.Run(() => Console.WriteLine("步骤2")),
    () => TaskHelper.Run(() => Console.WriteLine("步骤3"))
});
```

### ExecutePipelineAsync

顺序执行任务链（带返回值传递）。

```csharp
public static async Task<TResult> ExecutePipelineAsync<T, TResult>(T initialValue, IEnumerable<Func<object?, Task<object?>>> pipeline, CancellationToken cancellationToken = default)
```

**参数：**
- `initialValue`: 初始值
- `pipeline`: 处理管道
- `cancellationToken`: 取消令牌

**返回值：**
- 最终结果

**示例：**
```csharp
var pipeline = new List<Func<object?, Task<object?>>>
{
    async x => await Task.FromResult((object)((int)x! + 1)),
    async x => await Task.FromResult((object)((int)x! * 2)),
    async x => await Task.FromResult((object)((int)x! + 3))
};
var result = await TaskHelper.ExecutePipelineAsync<int, int>(1, pipeline);
// result = 7
```

### PipeAsync

创建任务管道。

```csharp
public static async Task<List<TOutput>> PipeAsync<TInput, TOutput>(IEnumerable<TInput> input, Func<TInput, Task<TOutput>> transform, int maxDegreeOfParallelism = -1, CancellationToken cancellationToken = default)
```

**参数：**
- `input`: 输入数据源
- `transform`: 转换函数
- `maxDegreeOfParallelism`: 最大并行度
- `cancellationToken`: 取消令牌

**返回值：**
- 输出结果列表

**示例：**
```csharp
var input = Enumerable.Range(1, 10);
var output = await TaskHelper.PipeAsync(input, async x =>
{
    await Task.Delay(10);
    return x * 2;
});
// output = [2, 4, 6, 8, 10, 12, 14, 16, 18, 20]
```

## 任务监控

### MonitorAsync

监控任务执行性能。

```csharp
public static async Task<TaskPerformanceInfo> MonitorAsync(Task task, string taskName = "Unnamed")
public static async Task<TaskPerformanceInfo<T>> MonitorAsync<T>(Task<T> task, string taskName = "Unnamed")
```

**参数：**
- `task`: 要监控的任务
- `taskName`: 任务名称

**返回值：**
- 性能信息

**示例：**
```csharp
var info = await TaskHelper.MonitorAsync(someTask, "数据加载任务");
Console.WriteLine($"任务耗时: {info.Duration}");
Console.WriteLine($"任务状态: {info.Status}");
```

### MeasureAsync

测量任务执行时间。

```csharp
public static async Task<TimeSpan> MeasureAsync(Func<Task> action)
public static async Task<(TimeSpan Duration, T Result)> MeasureAsync<T>(Func<Task<T>> function)
```

**参数：**
- `action`: 要执行的操作
- `function`: 要执行的函数

**返回值：**
- 执行时间（或执行时间和结果）

**示例：**
```csharp
var duration = await TaskHelper.MeasureAsync(async () =>
{
    await ProcessData();
});
Console.WriteLine($"执行时间: {duration}");

var (duration, result) = await TaskHelper.MeasureAsync(async () =>
{
    return await ComputeResult();
});
Console.WriteLine($"执行时间: {duration}, 结果: {result}");
```

## 任务取消

### WithCancellation

创建可取消的任务。

```csharp
public static Task WithCancellation(Action<CancellationToken> action, CancellationToken cancellationToken = default)
public static Task WithCancellationAsync(Func<CancellationToken, Task> asyncAction, CancellationToken cancellationToken = default)
public static Task<T> WithCancellation<T>(Func<CancellationToken, T> function, CancellationToken cancellationToken = default)
```

**参数：**
- `action`: 要执行的操作
- `asyncAction`: 要执行的异步操作
- `function`: 要执行的函数
- `cancellationToken`: 取消令牌

**返回值：**
- Task 实例

**示例：**
```csharp
await TaskHelper.WithCancellation(ct =>
{
    while (!ct.IsCancellationRequested)
    {
        // 执行操作
    }
});
```

## 任务结果处理

### HandleResultAsync

处理任务结果。

```csharp
public static async Task HandleResultAsync<T>(Task<T> task, Action<T>? onSuccess = null, Action<Exception>? onFailure = null)
```

**参数：**
- `task`: 任务实例
- `onSuccess`: 成功回调
- `onFailure`: 失败回调

**返回值：**
- Task 实例

**示例：**
```csharp
await TaskHelper.HandleResultAsync(
    someTask,
    result => Console.WriteLine($"成功: {result}"),
    ex => Console.WriteLine($"失败: {ex.Message}")
);
```

### GetResultOrDefaultAsync

获取任务结果或默认值。

```csharp
public static async Task<T> GetResultOrDefaultAsync<T>(Task<T> task, T defaultValue = default!)
public static async Task<T> GetResultOrDefaultAsync<T>(Task<T> task, Func<T> defaultFactory)
```

**参数：**
- `task`: 任务实例
- `defaultValue`: 默认值
- `defaultFactory`: 默认值工厂函数

**返回值：**
- 任务结果或默认值

**示例：**
```csharp
var result = await TaskHelper.GetResultOrDefaultAsync(someTask, 0);
var result = await TaskHelper.GetResultOrDefaultAsync(someTask, () => GetDefaultValue());
```

## 任务异常处理

### SafeExecuteAsync

安全执行任务（捕获所有异常）。

```csharp
public static async Task<Exception?> SafeExecuteAsync(Task task)
public static async Task<(T Result, Exception? Exception)> SafeExecuteAsync<T>(Task<T> task, T defaultValue = default!)
```

**参数：**
- `task`: 要执行的任务
- `defaultValue`: 默认值

**返回值：**
- 异常（如果有）或结果和异常的元组

**示例：**
```csharp
var exception = await TaskHelper.SafeExecuteAsync(someTask);
if (exception != null)
{
    Console.WriteLine($"任务失败: {exception.Message}");
}

var (result, ex) = await TaskHelper.SafeExecuteAsync(someTask, 0);
if (ex == null)
{
    Console.WriteLine($"结果: {result}");
}
```

### IgnoreExceptionAsync

忽略任务异常。

```csharp
public static async Task IgnoreExceptionAsync(Task task)
public static async Task IgnoreExceptionAsync<TException>(Task task) where TException : Exception
```

**参数：**
- `task`: 要执行的任务

**返回值：**
- Task 实例

**示例：**
```csharp
await TaskHelper.IgnoreExceptionAsync(someTask);
await TaskHelper.IgnoreExceptionAsync<InvalidOperationException>(someTask);
```

## 任务协调

### CreateBarrier

创建任务屏障。

```csharp
public static Barrier CreateBarrier(int participantCount)
```

**参数：**
- `participantCount`: 参与者数量

**返回值：**
- 屏障实例

**示例：**
```csharp
var barrier = TaskHelper.CreateBarrier(3);
// 多个任务在屏障处等待
```

### CreateSemaphore

创建异步信号量。

```csharp
public static SemaphoreSlim CreateSemaphore(int initialCount, int maxCount)
```

**参数：**
- `initialCount`: 初始计数
- `maxCount`: 最大计数

**返回值：**
- 信号量实例

**示例：**
```csharp
var semaphore = TaskHelper.CreateSemaphore(2, 5);
await semaphore.WaitAsync();
try
{
    // 执行受保护的操作
}
finally
{
    semaphore.Release();
}
```

### CreateManualResetEvent

创建异步手动重置事件。

```csharp
public static ManualResetEventSlim CreateManualResetEvent(bool initialState = false)
```

**参数：**
- `initialState`: 初始状态

**返回值：**
- 手动重置事件实例

### CreateAutoResetEvent

创建异步自动重置事件。

```csharp
public static AutoResetEvent CreateAutoResetEvent(bool initialState = false)
```

**参数：**
- `initialState`: 初始状态

**返回值：**
- 自动重置事件实例

## 辅助数据结构

### TaskExecutionStatus 枚举

任务执行状态。

**值：**
- `Completed`: 已完成
- `Faulted`: 已失败
- `Canceled`: 已取消

### TaskPerformanceInfo

任务性能信息。

**属性：**
- `TaskName`: 任务名称
- `StartTime`: 开始时间
- `EndTime`: 结束时间
- `Duration`: 执行时长
- `Status`: 任务状态
- `Exception`: 异常信息

### TaskPerformanceInfo<T>

任务性能信息（泛型版本）。

**属性：**
- `Result`: 任务结果

### TaskExecutionResult

任务执行结果。

**属性：**
- `IsSuccess`: 是否成功
- `Exception`: 异常信息
- `Duration`: 执行时长

**静态方法：**
- `Success(TimeSpan duration)`: 创建成功结果
- `Failure(Exception exception, TimeSpan duration)`: 创建失败结果

### TaskExecutionResult<T>

任务执行结果（泛型版本）。

**属性：**
- `Result`: 返回结果

**静态方法：**
- `Success(T result, TimeSpan duration)`: 创建成功结果
- `Failure(Exception exception, TimeSpan duration)`: 创建失败结果

## 使用建议

1. **任务创建**：使用 `Run` 和 `RunAsync` 方法创建任务
2. **并行处理**：使用 `ParallelForEachAsync` 方法进行并行处理
3. **重试机制**：使用 `RetryAsync` 方法实现自动重试
4. **超时控制**：使用 `WithTimeout` 方法控制任务超时
5. **任务监控**：使用 `MonitorAsync` 方法监控任务性能
6. **流水线处理**：使用 `ExecutePipelineAsync` 方法实现数据处理流水线

## 注意事项

1. 异步方法需要注意异步上下文的开销
2. 并行任务需要合理设置最大并行度
3. 重试机制需要设置合理的重试次数和间隔
4. 任务取消需要正确处理 CancellationToken
5. 使用 `SafeExecuteAsync` 可以安全地执行任务并捕获异常
