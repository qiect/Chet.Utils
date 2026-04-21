using System.Collections.Concurrent;
using System.Threading.Channels;

namespace Chet.Utils.Helpers;

/// <summary>
/// 任务操作帮助类，提供异步任务的创建、调度、监控、重试、超时控制、断路器、任务队列、任务池等功能。
/// </summary>
/// <remarks>
/// <para>支持的功能：</para>
/// <list type="bullet">
///   <item><description>任务创建：创建延迟任务、已完成任务、失败任务、已取消任务等</description></item>
///   <item><description>任务等待：等待所有任务、等待任意任务、同步等待</description></item>
///   <item><description>超时控制：带超时的任务执行、尝试超时执行、超时回调</description></item>
///   <item><description>重试机制：固定间隔重试、指数退避重试、条件重试、超时重试组合</description></item>
///   <item><description>断路器：熔断保护、自动恢复</description></item>
///   <item><description>并行处理：并行执行、有序并行、限流并行</description></item>
///   <item><description>任务限流：限流执行、节流执行、批量处理</description></item>
///   <item><description>任务链与管道：顺序执行、状态传递、管道处理</description></item>
///   <item><description>任务队列：生产者-消费者模式、优先级队列、任务调度</description></item>
///   <item><description>任务池：任务池管理、资源复用、并发控制</description></item>
///   <item><description>任务监控：性能监控、进度跟踪、状态统计</description></item>
///   <item><description>任务协调：任务屏障、信号量控制、事件等待</description></item>
///   <item><description>任务取消：可取消任务、取消令牌管理</description></item>
///   <item><description>任务结果处理：结果处理、默认值处理、结果转换</description></item>
///   <item><description>任务异常处理：安全执行、异常忽略、异常捕获</description></item>
///   <item><description>任务分组：分组管理、分组执行、分组监控</description></item>
///   <item><description>任务依赖：依赖关系管理、依赖任务执行、依赖图构建</description></item>
/// </list>
/// </remarks>
public static partial class TaskHelper
{
    #region 任务创建

    /// <summary>
    /// 创建并启动一个异步任务。
    /// </summary>
    /// <param name="action">要执行的操作。</param>
    /// <param name="cancellationToken">取消令牌。</param>
    /// <returns>任务实例。</returns>
    /// <exception cref="ArgumentNullException">action 为 null 时抛出。</exception>
    /// <example>
    /// <code>
    /// await TaskHelper.Run(() => Console.WriteLine("Hello"));
    /// </code>
    /// </example>
    public static Task Run(Action action, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(action);
        return Task.Run(action, cancellationToken);
    }

    /// <summary>
    /// 创建并启动一个带返回值的异步任务。
    /// </summary>
    /// <typeparam name="T">返回值类型。</typeparam>
    /// <param name="function">要执行的函数。</param>
    /// <param name="cancellationToken">取消令牌。</param>
    /// <returns>任务实例。</returns>
    /// <exception cref="ArgumentNullException">function 为 null 时抛出。</exception>
    /// <example>
    /// <code>
    /// var result = await TaskHelper.Run(() => Calculate());
    /// </code>
    /// </example>
    public static Task<T> Run<T>(Func<T> function, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(function);
        return Task.Run(function, cancellationToken);
    }

    /// <summary>
    /// 创建并启动一个异步任务（异步委托版本）。
    /// </summary>
    /// <param name="asyncAction">要执行的异步操作。</param>
    /// <param name="cancellationToken">取消令牌。</param>
    /// <returns>任务实例。</returns>
    /// <exception cref="ArgumentNullException">asyncAction 为 null 时抛出。</exception>
    /// <example>
    /// <code>
    /// await TaskHelper.RunAsync(async () => await FetchDataAsync());
    /// </code>
    /// </example>
    public static Task RunAsync(Func<Task> asyncAction, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(asyncAction);
        return Task.Run(asyncAction, cancellationToken);
    }

    /// <summary>
    /// 创建并启动一个带返回值的异步任务（异步委托版本）。
    /// </summary>
    /// <typeparam name="T">返回值类型。</typeparam>
    /// <param name="asyncFunction">要执行的异步函数。</param>
    /// <param name="cancellationToken">取消令牌。</param>
    /// <returns>任务实例。</returns>
    /// <exception cref="ArgumentNullException">asyncFunction 为 null 时抛出。</exception>
    /// <example>
    /// <code>
    /// var data = await TaskHelper.RunAsync(async () => await FetchDataAsync());
    /// </code>
    /// </example>
    public static Task<T> RunAsync<T>(Func<Task<T>> asyncFunction, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(asyncFunction);
        return Task.Run(asyncFunction, cancellationToken);
    }

    /// <summary>
    /// 创建一个延迟任务。
    /// </summary>
    /// <param name="delay">延迟时间。</param>
    /// <param name="cancellationToken">取消令牌。</param>
    /// <returns>任务实例。</returns>
    /// <exception cref="ArgumentOutOfRangeException">delay 为负数时抛出。</exception>
    /// <example>
    /// <code>
    /// await TaskHelper.Delay(TimeSpan.FromSeconds(1));
    /// </code>
    /// </example>
    public static Task Delay(TimeSpan delay, CancellationToken cancellationToken = default)
    {
        return Task.Delay(delay, cancellationToken);
    }

    /// <summary>
    /// 创建一个延迟任务。
    /// </summary>
    /// <param name="millisecondsDelay">延迟毫秒数。</param>
    /// <param name="cancellationToken">取消令牌。</param>
    /// <returns>任务实例。</returns>
    /// <exception cref="ArgumentOutOfRangeException">millisecondsDelay 为负数时抛出。</exception>
    /// <example>
    /// <code>
    /// await TaskHelper.Delay(1000);
    /// </code>
    /// </example>
    public static Task Delay(int millisecondsDelay, CancellationToken cancellationToken = default)
    {
        return Task.Delay(millisecondsDelay, cancellationToken);
    }

    /// <summary>
    /// 创建一个已完成的任务。
    /// </summary>
    /// <returns>已完成的任务实例。</returns>
    /// <example>
    /// <code>
    /// Task completed = TaskHelper.CompletedTask();
    /// </code>
    /// </example>
    public static Task CompletedTask()
    {
        return Task.CompletedTask;
    }

    /// <summary>
    /// 创建一个已完成的任务（带返回值）。
    /// </summary>
    /// <typeparam name="T">返回值类型。</typeparam>
    /// <param name="result">返回值。</param>
    /// <returns>已完成的任务实例。</returns>
    /// <example>
    /// <code>
    /// var task = TaskHelper.FromResult(42);
    /// </code>
    /// </example>
    public static Task<T> FromResult<T>(T result)
    {
        return Task.FromResult(result);
    }

    /// <summary>
    /// 创建一个失败的任务。
    /// </summary>
    /// <param name="exception">异常。</param>
    /// <returns>失败的任务实例。</returns>
    /// <exception cref="ArgumentNullException">exception 为 null 时抛出。</exception>
    /// <example>
    /// <code>
    /// var failedTask = TaskHelper.FromException(new InvalidOperationException("Error"));
    /// </code>
    /// </example>
    public static Task FromException(Exception exception)
    {
        ArgumentNullException.ThrowIfNull(exception);
        return Task.FromException(exception);
    }

    /// <summary>
    /// 创建一个失败的任务（带返回值类型）。
    /// </summary>
    /// <typeparam name="T">返回值类型。</typeparam>
    /// <param name="exception">异常。</param>
    /// <returns>失败的任务实例。</returns>
    /// <exception cref="ArgumentNullException">exception 为 null 时抛出。</exception>
    /// <example>
    /// <code>
    /// var failedTask = TaskHelper.FromException&lt;int&gt;(new InvalidOperationException("Error"));
    /// </code>
    /// </example>
    public static Task<T> FromException<T>(Exception exception)
    {
        ArgumentNullException.ThrowIfNull(exception);
        return Task.FromException<T>(exception);
    }

    /// <summary>
    /// 创建一个已取消的任务。
    /// </summary>
    /// <returns>已取消的任务实例。</returns>
    /// <example>
    /// <code>
    /// var canceledTask = TaskHelper.FromCanceled();
    /// </code>
    /// </example>
    public static Task FromCanceled()
    {
        return Task.FromCanceled(new CancellationToken(true));
    }

    /// <summary>
    /// 创建一个已取消的任务（带返回值类型）。
    /// </summary>
    /// <typeparam name="T">返回值类型。</typeparam>
    /// <returns>已取消的任务实例。</returns>
    /// <example>
    /// <code>
    /// var canceledTask = TaskHelper.FromCanceled&lt;int&gt;();
    /// </code>
    /// </example>
    public static Task<T> FromCanceled<T>()
    {
        return Task.FromCanceled<T>(new CancellationToken(true));
    }

    #endregion

    #region 任务等待

    /// <summary>
    /// 等待所有任务完成。
    /// </summary>
    /// <param name="tasks">任务数组。</param>
    /// <returns>任务实例。</returns>
    /// <exception cref="ArgumentNullException">tasks 为 null 时抛出。</exception>
    /// <example>
    /// <code>
    /// await TaskHelper.WhenAll(task1, task2, task3);
    /// </code>
    /// </example>
    public static Task WhenAll(params Task[] tasks)
    {
        ArgumentNullException.ThrowIfNull(tasks);
        return Task.WhenAll(tasks);
    }

    /// <summary>
    /// 等待所有任务完成（泛型版本）。
    /// </summary>
    /// <typeparam name="T">返回值类型。</typeparam>
    /// <param name="tasks">任务数组。</param>
    /// <returns>所有任务的返回值数组。</returns>
    /// <exception cref="ArgumentNullException">tasks 为 null 时抛出。</exception>
    /// <example>
    /// <code>
    /// var results = await TaskHelper.WhenAll(task1, task2);
    /// </code>
    /// </example>
    public static Task<T[]> WhenAll<T>(params Task<T>[] tasks)
    {
        ArgumentNullException.ThrowIfNull(tasks);
        return Task.WhenAll(tasks);
    }

    /// <summary>
    /// 等待所有任务完成（可枚举版本）。
    /// </summary>
    /// <typeparam name="T">返回值类型。</typeparam>
    /// <param name="tasks">任务集合。</param>
    /// <returns>所有任务的返回值数组。</returns>
    /// <exception cref="ArgumentNullException">tasks 为 null 时抛出。</exception>
    /// <example>
    /// <code>
    /// var tasks = new List&lt;Task&lt;int&gt;&gt; { task1, task2 };
    /// var results = await TaskHelper.WhenAll(tasks);
    /// </code>
    /// </example>
    public static Task<T[]> WhenAll<T>(IEnumerable<Task<T>> tasks)
    {
        ArgumentNullException.ThrowIfNull(tasks);
        return Task.WhenAll(tasks);
    }

    /// <summary>
    /// 等待任意一个任务完成。
    /// </summary>
    /// <param name="tasks">任务数组。</param>
    /// <returns>第一个完成的任务实例。</returns>
    /// <exception cref="ArgumentNullException">tasks 为 null 时抛出。</exception>
    /// <example>
    /// <code>
    /// var completedTask = await TaskHelper.WhenAny(task1, task2);
    /// </code>
    /// </example>
    public static Task<Task> WhenAny(params Task[] tasks)
    {
        ArgumentNullException.ThrowIfNull(tasks);
        return Task.WhenAny(tasks);
    }

    /// <summary>
    /// 等待任意一个任务完成（泛型版本）。
    /// </summary>
    /// <typeparam name="T">返回值类型。</typeparam>
    /// <param name="tasks">任务数组。</param>
    /// <returns>第一个完成的任务实例。</returns>
    /// <exception cref="ArgumentNullException">tasks 为 null 时抛出。</exception>
    /// <example>
    /// <code>
    /// var completedTask = await TaskHelper.WhenAny(task1, task2);
    /// </code>
    /// </example>
    public static Task<Task<T>> WhenAny<T>(params Task<T>[] tasks)
    {
        ArgumentNullException.ThrowIfNull(tasks);
        return Task.WhenAny(tasks);
    }

    /// <summary>
    /// 等待任务完成（同步阻塞）。
    /// </summary>
    /// <param name="task">要等待的任务。</param>
    /// <param name="timeout">超时时间（可选）。</param>
    /// <returns>是否在超时前完成。</returns>
    /// <exception cref="ArgumentNullException">task 为 null 时抛出。</exception>
    /// <remarks>
    /// 注意：此方法会阻塞当前线程，建议在非异步上下文中使用。
    /// 在异步方法中，请使用 <see cref="WithTimeout(Task, TimeSpan)"/> 方法。
    /// </remarks>
    /// <example>
    /// <code>
    /// bool completed = TaskHelper.Wait(task, TimeSpan.FromSeconds(5));
    /// </code>
    /// </example>
    public static bool Wait(Task task, TimeSpan? timeout = null)
    {
        ArgumentNullException.ThrowIfNull(task);

        if (timeout.HasValue)
        {
            return task.Wait(timeout.Value);
        }

        task.Wait();
        return true;
    }

    /// <summary>
    /// 等待所有任务完成（同步阻塞）。
    /// </summary>
    /// <param name="tasks">任务数组。</param>
    /// <param name="timeout">超时时间（可选）。</param>
    /// <returns>是否在超时前全部完成。</returns>
    /// <exception cref="ArgumentNullException">tasks 为 null 时抛出。</exception>
    /// <remarks>
    /// 注意：此方法会阻塞当前线程，建议在非异步上下文中使用。
    /// </remarks>
    /// <example>
    /// <code>
    /// bool allCompleted = TaskHelper.WaitAll(new[] { task1, task2 }, TimeSpan.FromSeconds(10));
    /// </code>
    /// </example>
    public static bool WaitAll(Task[] tasks, TimeSpan? timeout = null)
    {
        ArgumentNullException.ThrowIfNull(tasks);

        if (timeout.HasValue)
        {
            return Task.WaitAll(tasks, timeout.Value);
        }

        Task.WaitAll(tasks);
        return true;
    }

    /// <summary>
    /// 等待任意一个任务完成（同步阻塞）。
    /// </summary>
    /// <param name="tasks">任务数组。</param>
    /// <param name="timeout">超时时间（可选）。</param>
    /// <returns>完成的任务索引，-1 表示超时。</returns>
    /// <exception cref="ArgumentNullException">tasks 为 null 时抛出。</exception>
    /// <remarks>
    /// 注意：此方法会阻塞当前线程，建议在非异步上下文中使用。
    /// </remarks>
    /// <example>
    /// <code>
    /// int index = TaskHelper.WaitAny(new[] { task1, task2 }, TimeSpan.FromSeconds(5));
    /// </code>
    /// </example>
    public static int WaitAny(Task[] tasks, TimeSpan? timeout = null)
    {
        ArgumentNullException.ThrowIfNull(tasks);

        if (timeout.HasValue)
        {
            return Task.WaitAny(tasks, timeout.Value);
        }

        return Task.WaitAny(tasks);
    }

    #endregion

    #region 超时控制

    /// <summary>
    /// 为任务添加超时控制。
    /// </summary>
    /// <param name="task">要执行的任务。</param>
    /// <param name="timeout">超时时间。</param>
    /// <returns>任务实例。</returns>
    /// <exception cref="ArgumentNullException">task 为 null 时抛出。</exception>
    /// <exception cref="TimeoutException">任务超时时抛出。</exception>
    /// <remarks>
    /// 此方法使用 CancellationTokenSource 控制超时，不会直接取消底层任务。
    /// 如果需要取消底层任务，请使用带 CancellationToken 参数的重载版本。
    /// </remarks>
    /// <example>
    /// <code>
    /// try
    /// {
    ///     await TaskHelper.WithTimeout(LongRunningTask(), TimeSpan.FromSeconds(5));
    /// }
    /// catch (TimeoutException)
    /// {
    ///     Console.WriteLine("任务超时");
    /// }
    /// </code>
    /// </example>
    public static async Task WithTimeout(Task task, TimeSpan timeout)
    {
        ArgumentNullException.ThrowIfNull(task);

        using var cts = new CancellationTokenSource(timeout);
        var completedTask = await Task.WhenAny(task, Task.Delay(timeout, cts.Token)).ConfigureAwait(false);

        if (completedTask != task)
        {
            throw new TimeoutException($"任务在 {timeout} 内未完成");
        }

        await task.ConfigureAwait(false);
    }

    /// <summary>
    /// 为任务添加超时控制（泛型版本）。
    /// </summary>
    /// <typeparam name="T">返回值类型。</typeparam>
    /// <param name="task">要执行的任务。</param>
    /// <param name="timeout">超时时间。</param>
    /// <returns>任务返回值。</returns>
    /// <exception cref="ArgumentNullException">task 为 null 时抛出。</exception>
    /// <exception cref="TimeoutException">任务超时时抛出。</exception>
    /// <example>
    /// <code>
    /// try
    /// {
    ///     var result = await TaskHelper.WithTimeout(FetchDataAsync(), TimeSpan.FromSeconds(5));
    /// }
    /// catch (TimeoutException)
    /// {
    ///     Console.WriteLine("获取数据超时");
    /// }
    /// </code>
    /// </example>
    public static async Task<T> WithTimeout<T>(Task<T> task, TimeSpan timeout)
    {
        ArgumentNullException.ThrowIfNull(task);

        using var cts = new CancellationTokenSource(timeout);
        var completedTask = await Task.WhenAny(task, Task.Delay(timeout, cts.Token)).ConfigureAwait(false);

        if (completedTask != task)
        {
            throw new TimeoutException($"任务在 {timeout} 内未完成");
        }

        return await task.ConfigureAwait(false);
    }

    /// <summary>
    /// 为任务添加超时控制（带取消令牌）。
    /// </summary>
    /// <param name="taskFactory">任务工厂函数。</param>
    /// <param name="timeout">超时时间。</param>
    /// <param name="cancellationToken">外部取消令牌。</param>
    /// <returns>任务实例。</returns>
    /// <exception cref="ArgumentNullException">taskFactory 为 null 时抛出。</exception>
    /// <exception cref="TimeoutException">任务超时时抛出。</exception>
    /// <exception cref="OperationCanceledException">任务被取消时抛出。</exception>
    /// <remarks>
    /// 此方法使用链接的 CancellationTokenSource，支持外部取消和超时取消。
    /// </remarks>
    /// <example>
    /// <code>
    /// using var cts = new CancellationTokenSource();
    /// await TaskHelper.WithTimeout(
    ///     ct => LongRunningOperationAsync(ct),
    ///     TimeSpan.FromSeconds(5),
    ///     cts.Token);
    /// </code>
    /// </example>
    public static async Task WithTimeout(
        Func<CancellationToken, Task> taskFactory,
        TimeSpan timeout,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(taskFactory);

        using var linkedCts = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);
        linkedCts.CancelAfter(timeout);

        try
        {
            await taskFactory(linkedCts.Token).ConfigureAwait(false);
        }
        catch (OperationCanceledException) when (!cancellationToken.IsCancellationRequested)
        {
            throw new TimeoutException($"任务在 {timeout} 内未完成");
        }
    }

    /// <summary>
    /// 为任务添加超时控制（带取消令牌，泛型版本）。
    /// </summary>
    /// <typeparam name="T">返回值类型。</typeparam>
    /// <param name="taskFactory">任务工厂函数。</param>
    /// <param name="timeout">超时时间。</param>
    /// <param name="cancellationToken">外部取消令牌。</param>
    /// <returns>任务返回值。</returns>
    /// <exception cref="ArgumentNullException">taskFactory 为 null 时抛出。</exception>
    /// <exception cref="TimeoutException">任务超时时抛出。</exception>
    /// <exception cref="OperationCanceledException">任务被取消时抛出。</exception>
    /// <example>
    /// <code>
    /// var data = await TaskHelper.WithTimeout(
    ///     ct => FetchDataAsync(ct),
    ///     TimeSpan.FromSeconds(5));
    /// </code>
    /// </example>
    public static async Task<T> WithTimeout<T>(
        Func<CancellationToken, Task<T>> taskFactory,
        TimeSpan timeout,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(taskFactory);

        using var linkedCts = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);
        linkedCts.CancelAfter(timeout);

        try
        {
            return await taskFactory(linkedCts.Token).ConfigureAwait(false);
        }
        catch (OperationCanceledException) when (!cancellationToken.IsCancellationRequested)
        {
            throw new TimeoutException($"任务在 {timeout} 内未完成");
        }
    }

    /// <summary>
    /// 尝试带超时执行任务。
    /// </summary>
    /// <param name="task">要执行的任务。</param>
    /// <param name="timeout">超时时间。</param>
    /// <returns>是否在超时前完成。</returns>
    /// <exception cref="ArgumentNullException">task 为 null 时抛出。</exception>
    /// <example>
    /// <code>
    /// if (await TaskHelper.TryWithTimeout(LongRunningTask(), TimeSpan.FromSeconds(5)))
    /// {
    ///     Console.WriteLine("任务完成");
    /// }
    /// else
    /// {
    ///     Console.WriteLine("任务超时");
    /// }
    /// </code>
    /// </example>
    public static async Task<bool> TryWithTimeout(Task task, TimeSpan timeout)
    {
        ArgumentNullException.ThrowIfNull(task);

        using var cts = new CancellationTokenSource(timeout);
        var completedTask = await Task.WhenAny(task, Task.Delay(timeout, cts.Token)).ConfigureAwait(false);

        if (completedTask != task)
        {
            return false;
        }

        await task.ConfigureAwait(false);
        return true;
    }

    /// <summary>
    /// 尝试带超时执行任务（泛型版本）。
    /// </summary>
    /// <typeparam name="T">返回值类型。</typeparam>
    /// <param name="task">要执行的任务。</param>
    /// <param name="timeout">超时时间。</param>
    /// <returns>是否成功和返回值。</returns>
    /// <exception cref="ArgumentNullException">task 为 null 时抛出。</exception>
    /// <example>
    /// <code>
    /// var (success, result) = await TaskHelper.TryWithTimeout(FetchDataAsync(), TimeSpan.FromSeconds(5));
    /// if (success)
    /// {
    ///     Console.WriteLine($"结果: {result}");
    /// }
    /// </code>
    /// </example>
    public static async Task<(bool Success, T? Result)> TryWithTimeout<T>(Task<T> task, TimeSpan timeout)
    {
        ArgumentNullException.ThrowIfNull(task);

        using var cts = new CancellationTokenSource(timeout);
        var completedTask = await Task.WhenAny(task, Task.Delay(timeout, cts.Token)).ConfigureAwait(false);

        if (completedTask != task)
        {
            return (false, default);
        }

        return (true, await task.ConfigureAwait(false));
    }

    #endregion

    #region 重试机制

    /// <summary>
    /// 带重试机制的任务执行。
    /// </summary>
    /// <param name="taskFactory">任务工厂函数。</param>
    /// <param name="maxRetries">最大重试次数。</param>
    /// <param name="delay">重试间隔。</param>
    /// <param name="cancellationToken">取消令牌。</param>
    /// <returns>任务实例。</returns>
    /// <exception cref="ArgumentNullException">taskFactory 为 null 时抛出。</exception>
    /// <exception cref="ArgumentOutOfRangeException">maxRetries 小于 0 时抛出。</exception>
    /// <exception cref="AggregateException">所有重试都失败时抛出。</exception>
    /// <example>
    /// <code>
    /// await TaskHelper.RetryAsync(async () => await CallExternalApi(), maxRetries: 3, delay: TimeSpan.FromSeconds(1));
    /// </code>
    /// </example>
    public static async Task RetryAsync(
        Func<Task> taskFactory,
        int maxRetries,
        TimeSpan delay,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(taskFactory);
        ArgumentOutOfRangeException.ThrowIfNegative(maxRetries);

        List<Exception>? exceptions = null;

        for (int i = 0; i <= maxRetries; i++)
        {
            cancellationToken.ThrowIfCancellationRequested();

            try
            {
                await taskFactory().ConfigureAwait(false);
                return;
            }
            catch (OperationCanceledException)
            {
                throw;
            }
            catch (Exception ex)
            {
                exceptions ??= new List<Exception>();
                exceptions.Add(ex);

                if (i < maxRetries)
                {
                    await Task.Delay(delay, cancellationToken).ConfigureAwait(false);
                }
            }
        }

        throw new AggregateException($"任务在 {maxRetries} 次重试后仍然失败", exceptions!);
    }

    /// <summary>
    /// 带重试机制的任务执行（泛型版本）。
    /// </summary>
    /// <typeparam name="T">返回值类型。</typeparam>
    /// <param name="taskFactory">任务工厂函数。</param>
    /// <param name="maxRetries">最大重试次数。</param>
    /// <param name="delay">重试间隔。</param>
    /// <param name="cancellationToken">取消令牌。</param>
    /// <returns>任务返回值。</returns>
    /// <exception cref="ArgumentNullException">taskFactory 为 null 时抛出。</exception>
    /// <exception cref="ArgumentOutOfRangeException">maxRetries 小于 0 时抛出。</exception>
    /// <exception cref="AggregateException">所有重试都失败时抛出。</exception>
    /// <example>
    /// <code>
    /// var result = await TaskHelper.RetryAsync(async () => await FetchDataAsync(), maxRetries: 3, delay: TimeSpan.FromSeconds(1));
    /// </code>
    /// </example>
    public static async Task<T> RetryAsync<T>(
        Func<Task<T>> taskFactory,
        int maxRetries,
        TimeSpan delay,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(taskFactory);
        ArgumentOutOfRangeException.ThrowIfNegative(maxRetries);

        List<Exception>? exceptions = null;

        for (int i = 0; i <= maxRetries; i++)
        {
            cancellationToken.ThrowIfCancellationRequested();

            try
            {
                return await taskFactory().ConfigureAwait(false);
            }
            catch (OperationCanceledException)
            {
                throw;
            }
            catch (Exception ex)
            {
                exceptions ??= new List<Exception>();
                exceptions.Add(ex);

                if (i < maxRetries)
                {
                    await Task.Delay(delay, cancellationToken).ConfigureAwait(false);
                }
            }
        }

        throw new AggregateException($"任务在 {maxRetries} 次重试后仍然失败", exceptions!);
    }

    /// <summary>
    /// 带指数退避的重试机制。
    /// </summary>
    /// <param name="taskFactory">任务工厂函数。</param>
    /// <param name="maxRetries">最大重试次数。</param>
    /// <param name="initialDelay">初始延迟时间。</param>
    /// <param name="maxDelay">最大延迟时间。</param>
    /// <param name="cancellationToken">取消令牌。</param>
    /// <returns>任务实例。</returns>
    /// <exception cref="ArgumentNullException">taskFactory 为 null 时抛出。</exception>
    /// <exception cref="ArgumentOutOfRangeException">maxRetries 小于 0 时抛出。</exception>
    /// <exception cref="AggregateException">所有重试都失败时抛出。</exception>
    /// <remarks>
    /// 指数退避策略：每次重试的延迟时间会翻倍，直到达到最大延迟时间。
    /// 适用于处理临时性故障，如网络请求、数据库连接等。
    /// </remarks>
    /// <example>
    /// <code>
    /// await TaskHelper.RetryWithExponentialBackoffAsync(
    ///     async () => await CallExternalApi(),
    ///     maxRetries: 5,
    ///     initialDelay: TimeSpan.FromSeconds(1),
    ///     maxDelay: TimeSpan.FromSeconds(30));
    /// </code>
    /// </example>
    public static async Task RetryWithExponentialBackoffAsync(
        Func<Task> taskFactory,
        int maxRetries,
        TimeSpan initialDelay,
        TimeSpan? maxDelay = null,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(taskFactory);
        ArgumentOutOfRangeException.ThrowIfNegative(maxRetries);

        var max = maxDelay ?? TimeSpan.FromMinutes(5);
        List<Exception>? exceptions = null;
        var currentDelay = initialDelay;

        for (int i = 0; i <= maxRetries; i++)
        {
            cancellationToken.ThrowIfCancellationRequested();

            try
            {
                await taskFactory().ConfigureAwait(false);
                return;
            }
            catch (OperationCanceledException)
            {
                throw;
            }
            catch (Exception ex)
            {
                exceptions ??= new List<Exception>();
                exceptions.Add(ex);

                if (i < maxRetries)
                {
                    await Task.Delay(currentDelay, cancellationToken).ConfigureAwait(false);
                    currentDelay = TimeSpan.FromTicks(Math.Min(currentDelay.Ticks * 2, max.Ticks));
                }
            }
        }

        throw new AggregateException($"任务在 {maxRetries} 次指数退避重试后仍然失败", exceptions!);
    }

    /// <summary>
    /// 带指数退避的重试机制（泛型版本）。
    /// </summary>
    /// <typeparam name="T">返回值类型。</typeparam>
    /// <param name="taskFactory">任务工厂函数。</param>
    /// <param name="maxRetries">最大重试次数。</param>
    /// <param name="initialDelay">初始延迟时间。</param>
    /// <param name="maxDelay">最大延迟时间。</param>
    /// <param name="cancellationToken">取消令牌。</param>
    /// <returns>任务返回值。</returns>
    /// <exception cref="ArgumentNullException">taskFactory 为 null 时抛出。</exception>
    /// <exception cref="ArgumentOutOfRangeException">maxRetries 小于 0 时抛出。</exception>
    /// <exception cref="AggregateException">所有重试都失败时抛出。</exception>
    /// <example>
    /// <code>
    /// var result = await TaskHelper.RetryWithExponentialBackoffAsync(
    ///     async () => await FetchDataAsync(),
    ///     maxRetries: 5,
    ///     initialDelay: TimeSpan.FromSeconds(1));
    /// </code>
    /// </example>
    public static async Task<T> RetryWithExponentialBackoffAsync<T>(
        Func<Task<T>> taskFactory,
        int maxRetries,
        TimeSpan initialDelay,
        TimeSpan? maxDelay = null,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(taskFactory);
        ArgumentOutOfRangeException.ThrowIfNegative(maxRetries);

        var max = maxDelay ?? TimeSpan.FromMinutes(5);
        List<Exception>? exceptions = null;
        var currentDelay = initialDelay;

        for (int i = 0; i <= maxRetries; i++)
        {
            cancellationToken.ThrowIfCancellationRequested();

            try
            {
                return await taskFactory().ConfigureAwait(false);
            }
            catch (OperationCanceledException)
            {
                throw;
            }
            catch (Exception ex)
            {
                exceptions ??= new List<Exception>();
                exceptions.Add(ex);

                if (i < maxRetries)
                {
                    await Task.Delay(currentDelay, cancellationToken).ConfigureAwait(false);
                    currentDelay = TimeSpan.FromTicks(Math.Min(currentDelay.Ticks * 2, max.Ticks));
                }
            }
        }

        throw new AggregateException($"任务在 {maxRetries} 次指数退避重试后仍然失败", exceptions!);
    }

    /// <summary>
    /// 带条件判断的重试机制。
    /// </summary>
    /// <param name="taskFactory">任务工厂函数。</param>
    /// <param name="shouldRetry">判断是否需要重试的函数，参数为异常和当前重试次数。</param>
    /// <param name="maxRetries">最大重试次数。</param>
    /// <param name="delay">重试间隔。</param>
    /// <param name="cancellationToken">取消令牌。</param>
    /// <returns>任务实例。</returns>
    /// <exception cref="ArgumentNullException">taskFactory 或 shouldRetry 为 null 时抛出。</exception>
    /// <exception cref="ArgumentOutOfRangeException">maxRetries 小于 0 时抛出。</exception>
    /// <example>
    /// <code>
    /// await TaskHelper.RetryWhenAsync(
    ///     async () => await CallExternalApi(),
    ///     (ex, retryCount) => ex is TimeoutException || ex is HttpRequestException,
    ///     maxRetries: 3,
    ///     delay: TimeSpan.FromSeconds(1));
    /// </code>
    /// </example>
    public static async Task RetryWhenAsync(
        Func<Task> taskFactory,
        Func<Exception, int, bool> shouldRetry,
        int maxRetries,
        TimeSpan delay,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(taskFactory);
        ArgumentNullException.ThrowIfNull(shouldRetry);
        ArgumentOutOfRangeException.ThrowIfNegative(maxRetries);

        for (int i = 0; i <= maxRetries; i++)
        {
            cancellationToken.ThrowIfCancellationRequested();

            try
            {
                await taskFactory().ConfigureAwait(false);
                return;
            }
            catch (Exception ex) when (i < maxRetries && shouldRetry(ex, i))
            {
                await Task.Delay(delay, cancellationToken).ConfigureAwait(false);
            }
        }

        throw new InvalidOperationException($"任务在 {maxRetries} 次条件重试后仍然失败");
    }

    /// <summary>
    /// 带条件判断的重试机制（泛型版本）。
    /// </summary>
    /// <typeparam name="T">返回值类型。</typeparam>
    /// <param name="taskFactory">任务工厂函数。</param>
    /// <param name="shouldRetry">判断是否需要重试的函数，参数为异常和当前重试次数。</param>
    /// <param name="maxRetries">最大重试次数。</param>
    /// <param name="delay">重试间隔。</param>
    /// <param name="cancellationToken">取消令牌。</param>
    /// <returns>任务返回值。</returns>
    /// <exception cref="ArgumentNullException">taskFactory 或 shouldRetry 为 null 时抛出。</exception>
    /// <exception cref="ArgumentOutOfRangeException">maxRetries 小于 0 时抛出。</exception>
    /// <example>
    /// <code>
    /// var result = await TaskHelper.RetryWhenAsync(
    ///     async () => await FetchDataAsync(),
    ///     (ex, retryCount) => ex is TimeoutException,
    ///     maxRetries: 3,
    ///     delay: TimeSpan.FromSeconds(1));
    /// </code>
    /// </example>
    public static async Task<T> RetryWhenAsync<T>(
        Func<Task<T>> taskFactory,
        Func<Exception, int, bool> shouldRetry,
        int maxRetries,
        TimeSpan delay,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(taskFactory);
        ArgumentNullException.ThrowIfNull(shouldRetry);
        ArgumentOutOfRangeException.ThrowIfNegative(maxRetries);

        for (int i = 0; i <= maxRetries; i++)
        {
            cancellationToken.ThrowIfCancellationRequested();

            try
            {
                return await taskFactory().ConfigureAwait(false);
            }
            catch (Exception ex) when (i < maxRetries && shouldRetry(ex, i))
            {
                await Task.Delay(delay, cancellationToken).ConfigureAwait(false);
            }
        }

        throw new InvalidOperationException($"任务在 {maxRetries} 次条件重试后仍然失败");
    }

    /// <summary>
    /// 带超时和重试机制的任务执行。
    /// </summary>
    /// <param name="taskFactory">任务工厂函数。</param>
    /// <param name="maxRetries">最大重试次数。</param>
    /// <param name="timeout">每次执行的超时时间。</param>
    /// <param name="delay">重试间隔。</param>
    /// <param name="cancellationToken">取消令牌。</param>
    /// <returns>任务实例。</returns>
    /// <exception cref="ArgumentNullException">taskFactory 为 null 时抛出。</exception>
    /// <exception cref="ArgumentOutOfRangeException">maxRetries 小于 0 时抛出。</exception>
    /// <exception cref="AggregateException">所有重试都失败时抛出。</exception>
    /// <example>
    /// <code>
    /// await TaskHelper.RetryWithTimeoutAsync(
    ///     async ct => await CallExternalApi(ct),
    ///     maxRetries: 3,
    ///     timeout: TimeSpan.FromSeconds(5),
    ///     delay: TimeSpan.FromSeconds(1));
    /// </code>
    /// </example>
    public static async Task RetryWithTimeoutAsync(
        Func<CancellationToken, Task> taskFactory,
        int maxRetries,
        TimeSpan timeout,
        TimeSpan delay,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(taskFactory);
        ArgumentOutOfRangeException.ThrowIfNegative(maxRetries);

        List<Exception>? exceptions = null;

        for (int i = 0; i <= maxRetries; i++)
        {
            cancellationToken.ThrowIfCancellationRequested();

            try
            {
                await WithTimeout(taskFactory, timeout, cancellationToken).ConfigureAwait(false);
                return;
            }
            catch (OperationCanceledException) when (cancellationToken.IsCancellationRequested)
            {
                throw;
            }
            catch (Exception ex)
            {
                exceptions ??= new List<Exception>();
                exceptions.Add(ex);

                if (i < maxRetries)
                {
                    await Task.Delay(delay, cancellationToken).ConfigureAwait(false);
                }
            }
        }

        throw new AggregateException($"任务在 {maxRetries} 次重试后仍然失败", exceptions!);
    }

    /// <summary>
    /// 带超时和重试机制的任务执行（泛型版本）。
    /// </summary>
    /// <typeparam name="T">返回值类型。</typeparam>
    /// <param name="taskFactory">任务工厂函数。</param>
    /// <param name="maxRetries">最大重试次数。</param>
    /// <param name="timeout">每次执行的超时时间。</param>
    /// <param name="delay">重试间隔。</param>
    /// <param name="cancellationToken">取消令牌。</param>
    /// <returns>任务返回值。</returns>
    /// <exception cref="ArgumentNullException">taskFactory 为 null 时抛出。</exception>
    /// <exception cref="ArgumentOutOfRangeException">maxRetries 小于 0 时抛出。</exception>
    /// <exception cref="AggregateException">所有重试都失败时抛出。</exception>
    /// <example>
    /// <code>
    /// var result = await TaskHelper.RetryWithTimeoutAsync(
    ///     async ct => await FetchDataAsync(ct),
    ///     maxRetries: 3,
    ///     timeout: TimeSpan.FromSeconds(5),
    ///     delay: TimeSpan.FromSeconds(1));
    /// </code>
    /// </example>
    public static async Task<T> RetryWithTimeoutAsync<T>(
        Func<CancellationToken, Task<T>> taskFactory,
        int maxRetries,
        TimeSpan timeout,
        TimeSpan delay,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(taskFactory);
        ArgumentOutOfRangeException.ThrowIfNegative(maxRetries);

        List<Exception>? exceptions = null;

        for (int i = 0; i <= maxRetries; i++)
        {
            cancellationToken.ThrowIfCancellationRequested();

            try
            {
                return await WithTimeout(taskFactory, timeout, cancellationToken).ConfigureAwait(false);
            }
            catch (OperationCanceledException) when (cancellationToken.IsCancellationRequested)
            {
                throw;
            }
            catch (Exception ex)
            {
                exceptions ??= new List<Exception>();
                exceptions.Add(ex);

                if (i < maxRetries)
                {
                    await Task.Delay(delay, cancellationToken).ConfigureAwait(false);
                }
            }
        }

        throw new AggregateException($"任务在 {maxRetries} 次重试后仍然失败", exceptions!);
    }

    #endregion

    #region 断路器

    /// <summary>
    /// 创建断路器实例。
    /// </summary>
    /// <param name="failureThreshold">失败阈值，达到此数量后熔断。</param>
    /// <param name="successThreshold">成功阈值，达到此数量后半开状态转为关闭。</param>
    /// <param name="resetTimeout">熔断后重置超时时间。</param>
    /// <returns>断路器实例。</returns>
    /// <exception cref="ArgumentOutOfRangeException">参数无效时抛出。</exception>
    /// <remarks>
    /// 断路器状态：
    /// <list type="bullet">
    ///   <item><description>关闭（Closed）：正常执行任务</description></item>
    ///   <item><description>打开（Open）：熔断状态，拒绝执行任务</description></item>
    ///   <item><description>半开（HalfOpen）：允许部分请求通过，测试服务是否恢复</description></item>
    /// </list>
    /// </remarks>
    /// <example>
    /// <code>
    /// var circuitBreaker = TaskHelper.CreateCircuitBreaker(
    ///     failureThreshold: 5,
    ///     successThreshold: 3,
    ///     resetTimeout: TimeSpan.FromSeconds(30));
    /// 
    /// await circuitBreaker.ExecuteAsync(async () => await CallExternalService());
    /// </code>
    /// </example>
    public static CircuitBreaker CreateCircuitBreaker(
        int failureThreshold = 5,
        int successThreshold = 3,
        TimeSpan? resetTimeout = null)
    {
        ArgumentOutOfRangeException.ThrowIfLessThanOrEqual(failureThreshold, 0);
        ArgumentOutOfRangeException.ThrowIfLessThanOrEqual(successThreshold, 0);

        return new CircuitBreaker(failureThreshold, successThreshold, resetTimeout ?? TimeSpan.FromSeconds(30));
    }

    /// <summary>
    /// 使用断路器执行任务。
    /// </summary>
    /// <param name="circuitBreaker">断路器实例。</param>
    /// <param name="taskFactory">任务工厂函数。</param>
    /// <param name="cancellationToken">取消令牌。</param>
    /// <returns>任务实例。</returns>
    /// <exception cref="ArgumentNullException">circuitBreaker 或 taskFactory 为 null 时抛出。</exception>
    /// <exception cref="CircuitBreakerOpenException">断路器处于打开状态时抛出。</exception>
    /// <example>
    /// <code>
    /// var circuitBreaker = TaskHelper.CreateCircuitBreaker();
    /// await TaskHelper.ExecuteWithCircuitBreakerAsync(circuitBreaker, async () => await CallExternalService());
    /// </code>
    /// </example>
    public static async Task ExecuteWithCircuitBreakerAsync(
        CircuitBreaker circuitBreaker,
        Func<Task> taskFactory,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(circuitBreaker);
        ArgumentNullException.ThrowIfNull(taskFactory);

        await circuitBreaker.ExecuteAsync(taskFactory, cancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// 使用断路器执行任务（泛型版本）。
    /// </summary>
    /// <typeparam name="T">返回值类型。</typeparam>
    /// <param name="circuitBreaker">断路器实例。</param>
    /// <param name="taskFactory">任务工厂函数。</param>
    /// <param name="cancellationToken">取消令牌。</param>
    /// <returns>任务返回值。</returns>
    /// <exception cref="ArgumentNullException">circuitBreaker 或 taskFactory 为 null 时抛出。</exception>
    /// <exception cref="CircuitBreakerOpenException">断路器处于打开状态时抛出。</exception>
    /// <example>
    /// <code>
    /// var circuitBreaker = TaskHelper.CreateCircuitBreaker();
    /// var result = await TaskHelper.ExecuteWithCircuitBreakerAsync(circuitBreaker, async () => await FetchDataAsync());
    /// </code>
    /// </example>
    public static async Task<T> ExecuteWithCircuitBreakerAsync<T>(
        CircuitBreaker circuitBreaker,
        Func<Task<T>> taskFactory,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(circuitBreaker);
        ArgumentNullException.ThrowIfNull(taskFactory);

        return await circuitBreaker.ExecuteAsync(taskFactory, cancellationToken).ConfigureAwait(false);
    }

    #endregion

    #region 并行处理

    /// <summary>
    /// 并行执行多个任务。
    /// </summary>
    /// <param name="actions">要执行的操作列表。</param>
    /// <param name="maxDegreeOfParallelism">最大并行度，-1 表示不限制。</param>
    /// <param name="cancellationToken">取消令牌。</param>
    /// <returns>任务实例。</returns>
    /// <exception cref="ArgumentNullException">actions 为 null 时抛出。</exception>
    /// <example>
    /// <code>
    /// var actions = new List&lt;Func&lt;Task&gt;&gt;
    /// {
    ///     async () => await ProcessItemAsync(1),
    ///     async () => await ProcessItemAsync(2),
    ///     async () => await ProcessItemAsync(3)
    /// };
    /// await TaskHelper.ParallelForEachAsync(actions, maxDegreeOfParallelism: 2);
    /// </code>
    /// </example>
    public static async Task ParallelForEachAsync(
        IEnumerable<Func<Task>> actions,
        int maxDegreeOfParallelism = -1,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(actions);

        var options = new ParallelOptions
        {
            MaxDegreeOfParallelism = maxDegreeOfParallelism,
            CancellationToken = cancellationToken
        };

        await Parallel.ForEachAsync(actions, options, async (action, ct) =>
        {
            await action().ConfigureAwait(false);
        }).ConfigureAwait(false);
    }

    /// <summary>
    /// 并行处理数据源中的每个元素。
    /// </summary>
    /// <typeparam name="T">数据类型。</typeparam>
    /// <param name="source">数据源。</param>
    /// <param name="body">处理函数。</param>
    /// <param name="maxDegreeOfParallelism">最大并行度，-1 表示不限制。</param>
    /// <param name="cancellationToken">取消令牌。</param>
    /// <returns>任务实例。</returns>
    /// <exception cref="ArgumentNullException">source 或 body 为 null 时抛出。</exception>
    /// <example>
    /// <code>
    /// var items = new[] { 1, 2, 3, 4, 5 };
    /// await TaskHelper.ParallelForEachAsync(items, async item => await ProcessItemAsync(item), maxDegreeOfParallelism: 2);
    /// </code>
    /// </example>
    public static async Task ParallelForEachAsync<T>(
        IEnumerable<T> source,
        Func<T, Task> body,
        int maxDegreeOfParallelism = -1,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(source);
        ArgumentNullException.ThrowIfNull(body);

        var options = new ParallelOptions
        {
            MaxDegreeOfParallelism = maxDegreeOfParallelism,
            CancellationToken = cancellationToken
        };

        await Parallel.ForEachAsync(source, options, async (item, ct) =>
        {
            await body(item).ConfigureAwait(false);
        }).ConfigureAwait(false);
    }

    /// <summary>
    /// 并行处理数据源中的每个元素（带索引）。
    /// </summary>
    /// <typeparam name="T">数据类型。</typeparam>
    /// <param name="source">数据源。</param>
    /// <param name="body">处理函数，参数为元素和索引。</param>
    /// <param name="maxDegreeOfParallelism">最大并行度，-1 表示不限制。</param>
    /// <param name="cancellationToken">取消令牌。</param>
    /// <returns>任务实例。</returns>
    /// <exception cref="ArgumentNullException">source 或 body 为 null 时抛出。</exception>
    /// <example>
    /// <code>
    /// var items = new[] { "a", "b", "c" };
    /// await TaskHelper.ParallelForEachAsync(items, async (item, index) => 
    /// {
    ///     Console.WriteLine($"处理第 {index} 个元素: {item}");
    ///     await ProcessItemAsync(item);
    /// });
    /// </code>
    /// </example>
    public static async Task ParallelForEachAsync<T>(
        IEnumerable<T> source,
        Func<T, int, Task> body,
        int maxDegreeOfParallelism = -1,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(source);
        ArgumentNullException.ThrowIfNull(body);

        var indexedSource = source.Select((item, index) => (item, index));
        var options = new ParallelOptions
        {
            MaxDegreeOfParallelism = maxDegreeOfParallelism,
            CancellationToken = cancellationToken
        };

        await Parallel.ForEachAsync(indexedSource, options, async (x, ct) =>
        {
            await body(x.item, x.index).ConfigureAwait(false);
        }).ConfigureAwait(false);
    }

    /// <summary>
    /// 并行处理并返回有序结果。
    /// </summary>
    /// <typeparam name="T">输入数据类型。</typeparam>
    /// <typeparam name="TResult">结果数据类型。</typeparam>
    /// <param name="source">数据源。</param>
    /// <param name="selector">转换函数。</param>
    /// <param name="maxDegreeOfParallelism">最大并行度，-1 表示不限制。</param>
    /// <param name="cancellationToken">取消令牌。</param>
    /// <returns>有序结果列表。</returns>
    /// <exception cref="ArgumentNullException">source 或 selector 为 null 时抛出。</exception>
    /// <example>
    /// <code>
    /// var urls = new[] { "url1", "url2", "url3" };
    /// var contents = await TaskHelper.ParallelSelectAsync(urls, async url => await DownloadAsync(url));
    /// </code>
    /// </example>
    public static async Task<List<TResult>> ParallelSelectAsync<T, TResult>(
        IEnumerable<T> source,
        Func<T, Task<TResult>> selector,
        int maxDegreeOfParallelism = -1,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(source);
        ArgumentNullException.ThrowIfNull(selector);

        var items = source.ToList();
        var results = new TResult[items.Count];
        var options = new ParallelOptions
        {
            MaxDegreeOfParallelism = maxDegreeOfParallelism,
            CancellationToken = cancellationToken
        };

        await Parallel.ForAsync(0, items.Count, options, async (i, ct) =>
        {
            results[i] = await selector(items[i]).ConfigureAwait(false);
        }).ConfigureAwait(false);

        return results.ToList();
    }

    /// <summary>
    /// 并行处理并返回有序结果（带索引）。
    /// </summary>
    /// <typeparam name="T">输入数据类型。</typeparam>
    /// <typeparam name="TResult">结果数据类型。</typeparam>
    /// <param name="source">数据源。</param>
    /// <param name="selector">转换函数，参数为元素和索引。</param>
    /// <param name="maxDegreeOfParallelism">最大并行度，-1 表示不限制。</param>
    /// <param name="cancellationToken">取消令牌。</param>
    /// <returns>有序结果列表。</returns>
    /// <exception cref="ArgumentNullException">source 或 selector 为 null 时抛出。</exception>
    /// <example>
    /// <code>
    /// var items = new[] { 1, 2, 3 };
    /// var results = await TaskHelper.ParallelSelectAsync(items, async (item, index) => 
    /// {
    ///     return await ProcessWithIndexAsync(item, index);
    /// });
    /// </code>
    /// </example>
    public static async Task<List<TResult>> ParallelSelectAsync<T, TResult>(
        IEnumerable<T> source,
        Func<T, int, Task<TResult>> selector,
        int maxDegreeOfParallelism = -1,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(source);
        ArgumentNullException.ThrowIfNull(selector);

        var items = source.ToList();
        var results = new TResult[items.Count];
        var options = new ParallelOptions
        {
            MaxDegreeOfParallelism = maxDegreeOfParallelism,
            CancellationToken = cancellationToken
        };

        await Parallel.ForAsync(0, items.Count, options, async (i, ct) =>
        {
            results[i] = await selector(items[i], i).ConfigureAwait(false);
        }).ConfigureAwait(false);

        return results.ToList();
    }

    /// <summary>
    /// 并行执行任务直到满足条件。
    /// </summary>
    /// <typeparam name="T">数据类型。</typeparam>
    /// <param name="source">数据源。</param>
    /// <param name="predicate">条件判断函数。</param>
    /// <param name="maxDegreeOfParallelism">最大并行度，-1 表示不限制。</param>
    /// <param name="cancellationToken">取消令牌。</param>
    /// <returns>第一个满足条件的元素。</returns>
    /// <exception cref="ArgumentNullException">source 或 predicate 为 null 时抛出。</exception>
    /// <example>
    /// <code>
    /// var urls = new[] { "url1", "url2", "url3" };
    /// var firstValid = await TaskHelper.ParallelFirstOrDefaultAsync(urls, async url => await IsValidAsync(url));
    /// </code>
    /// </example>
    public static async Task<T?> ParallelFirstOrDefaultAsync<T>(
        IEnumerable<T> source,
        Func<T, Task<bool>> predicate,
        int maxDegreeOfParallelism = -1,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(source);
        ArgumentNullException.ThrowIfNull(predicate);

        var items = source.ToList();
        var result = default(T);
        var found = false;
        var lockObj = new object();

        var options = new ParallelOptions
        {
            MaxDegreeOfParallelism = maxDegreeOfParallelism,
            CancellationToken = cancellationToken
        };

        await Parallel.ForEachAsync(items, options, async (item, ct) =>
        {
            if (found) return;

            if (await predicate(item).ConfigureAwait(false))
            {
                lock (lockObj)
                {
                    if (!found)
                    {
                        result = item;
                        found = true;
                    }
                }
            }
        }).ConfigureAwait(false);

        return result;
    }

    #endregion

    #region 任务限流

    /// <summary>
    /// 限流执行任务。
    /// </summary>
    /// <param name="action">要执行的操作。</param>
    /// <param name="interval">执行间隔。</param>
    /// <param name="cancellationToken">取消令牌。</param>
    /// <returns>任务实例。</returns>
    /// <exception cref="ArgumentNullException">action 为 null 时抛出。</exception>
    /// <example>
    /// <code>
    /// await TaskHelper.ThrottleAsync(async () => await CallApi(), TimeSpan.FromSeconds(1));
    /// </code>
    /// </example>
    public static async Task ThrottleAsync(
        Func<Task> action,
        TimeSpan interval,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(action);

        await Task.Delay(interval, cancellationToken).ConfigureAwait(false);
        await action().ConfigureAwait(false);
    }

    /// <summary>
    /// 节流执行任务（在指定时间窗口内只执行一次）。
    /// </summary>
    /// <param name="action">要执行的操作。</param>
    /// <param name="interval">时间间隔。</param>
    /// <param name="cancellationToken">取消令牌。</param>
    /// <returns>任务实例。</returns>
    /// <exception cref="ArgumentNullException">action 为 null 时抛出。</exception>
    /// <example>
    /// <code>
    /// await TaskHelper.DebounceAsync(async () => await SaveData(), TimeSpan.FromMilliseconds(300));
    /// </code>
    /// </example>
    public static async Task DebounceAsync(
        Func<Task> action,
        TimeSpan interval,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(action);

        await Task.Delay(interval, cancellationToken).ConfigureAwait(false);
        await action().ConfigureAwait(false);
    }

    /// <summary>
    /// 批量执行任务（收集一段时间内的请求批量处理）。
    /// </summary>
    /// <typeparam name="T">数据类型。</typeparam>
    /// <param name="source">数据源。</param>
    /// <param name="batchSize">批量大小。</param>
    /// <param name="batchInterval">批量间隔。</param>
    /// <param name="processor">批量处理函数。</param>
    /// <param name="cancellationToken">取消令牌。</param>
    /// <returns>任务实例。</returns>
    /// <exception cref="ArgumentNullException">source 或 processor 为 null 时抛出。</exception>
    /// <exception cref="ArgumentOutOfRangeException">batchSize 小于等于 0 时抛出。</exception>
    /// <example>
    /// <code>
    /// var items = Enumerable.Range(1, 100);
    /// await TaskHelper.BatchProcessAsync(items, batchSize: 10, batchInterval: TimeSpan.FromSeconds(1),
    ///     async batch => await ProcessBatchAsync(batch));
    /// </code>
    /// </example>
    public static async Task BatchProcessAsync<T>(
        IEnumerable<T> source,
        int batchSize,
        TimeSpan batchInterval,
        Func<List<T>, Task> processor,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(source);
        ArgumentNullException.ThrowIfNull(processor);
        ArgumentOutOfRangeException.ThrowIfLessThanOrEqual(batchSize, 0);

        var batch = new List<T>(batchSize);

        foreach (var item in source)
        {
            cancellationToken.ThrowIfCancellationRequested();

            batch.Add(item);

            if (batch.Count >= batchSize)
            {
                await processor(batch).ConfigureAwait(false);
                batch.Clear();
                await Task.Delay(batchInterval, cancellationToken).ConfigureAwait(false);
            }
        }

        if (batch.Count > 0)
        {
            await processor(batch).ConfigureAwait(false);
        }
    }

    /// <summary>
    /// 使用信号量限流执行任务。
    /// </summary>
    /// <typeparam name="T">返回值类型。</typeparam>
    /// <param name="semaphore">信号量实例。</param>
    /// <param name="taskFactory">任务工厂函数。</param>
    /// <param name="cancellationToken">取消令牌。</param>
    /// <returns>任务返回值。</returns>
    /// <exception cref="ArgumentNullException">semaphore 或 taskFactory 为 null 时抛出。</exception>
    /// <example>
    /// <code>
    /// using var semaphore = new SemaphoreSlim(5); // 最多 5 个并发
    /// var result = await TaskHelper.WithSemaphoreAsync(semaphore, async () => await CallApi());
    /// </code>
    /// </example>
    public static async Task<T> WithSemaphoreAsync<T>(
        SemaphoreSlim semaphore,
        Func<Task<T>> taskFactory,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(semaphore);
        ArgumentNullException.ThrowIfNull(taskFactory);

        await semaphore.WaitAsync(cancellationToken).ConfigureAwait(false);
        try
        {
            return await taskFactory().ConfigureAwait(false);
        }
        finally
        {
            semaphore.Release();
        }
    }

    /// <summary>
    /// 使用信号量限流执行任务（无返回值版本）。
    /// </summary>
    /// <param name="semaphore">信号量实例。</param>
    /// <param name="taskFactory">任务工厂函数。</param>
    /// <param name="cancellationToken">取消令牌。</param>
    /// <returns>任务实例。</returns>
    /// <exception cref="ArgumentNullException">semaphore 或 taskFactory 为 null 时抛出。</exception>
    /// <example>
    /// <code>
    /// using var semaphore = new SemaphoreSlim(5);
    /// await TaskHelper.WithSemaphoreAsync(semaphore, async () => await CallApi());
    /// </code>
    /// </example>
    public static async Task WithSemaphoreAsync(
        SemaphoreSlim semaphore,
        Func<Task> taskFactory,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(semaphore);
        ArgumentNullException.ThrowIfNull(taskFactory);

        await semaphore.WaitAsync(cancellationToken).ConfigureAwait(false);
        try
        {
            await taskFactory().ConfigureAwait(false);
        }
        finally
        {
            semaphore.Release();
        }
    }

    #endregion

    #region 任务链与管道

    /// <summary>
    /// 顺序执行任务链。
    /// </summary>
    /// <param name="tasks">任务列表。</param>
    /// <param name="cancellationToken">取消令牌。</param>
    /// <returns>任务实例。</returns>
    /// <exception cref="ArgumentNullException">tasks 为 null 时抛出。</exception>
    /// <example>
    /// <code>
    /// await TaskHelper.ExecuteChainAsync(new[]
    /// {
    ///     async () => await Step1(),
    ///     async () => await Step2(),
    ///     async () => await Step3()
    /// });
    /// </code>
    /// </example>
    public static async Task ExecuteChainAsync(
        IEnumerable<Func<Task>> tasks,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(tasks);

        foreach (var task in tasks)
        {
            cancellationToken.ThrowIfCancellationRequested();
            await task().ConfigureAwait(false);
        }
    }

    /// <summary>
    /// 顺序执行任务链（带状态传递）。
    /// </summary>
    /// <typeparam name="T">状态类型。</typeparam>
    /// <param name="initialState">初始状态。</param>
    /// <param name="steps">处理步骤列表。</param>
    /// <param name="cancellationToken">取消令牌。</param>
    /// <returns>最终状态。</returns>
    /// <exception cref="ArgumentNullException">steps 为 null 时抛出。</exception>
    /// <example>
    /// <code>
    /// var result = await TaskHelper.ExecuteChainAsync(
    ///     "initial",
    ///     new Func&lt;string, Task&lt;string&gt;&gt;[]
    ///     {
    ///         async s => await ProcessStep1(s),
    ///         async s => await ProcessStep2(s),
    ///         async s => await ProcessStep3(s)
    ///     });
    /// </code>
    /// </example>
    public static async Task<T> ExecuteChainAsync<T>(
        T initialState,
        IEnumerable<Func<T, Task<T>>> steps,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(steps);

        var current = initialState;

        foreach (var step in steps)
        {
            cancellationToken.ThrowIfCancellationRequested();
            current = await step(current).ConfigureAwait(false);
        }

        return current;
    }

    /// <summary>
    /// 创建任务管道。
    /// </summary>
    /// <typeparam name="TInput">输入类型。</typeparam>
    /// <typeparam name="TOutput">输出类型。</typeparam>
    /// <param name="input">输入数据源。</param>
    /// <param name="transform">转换函数。</param>
    /// <param name="maxDegreeOfParallelism">最大并行度。</param>
    /// <param name="cancellationToken">取消令牌。</param>
    /// <returns>输出结果列表。</returns>
    /// <exception cref="ArgumentNullException">input 或 transform 为 null 时抛出。</exception>
    /// <example>
    /// <code>
    /// var results = await TaskHelper.PipeAsync(
    ///     new[] { 1, 2, 3, 4, 5 },
    ///     async x => await ProcessAsync(x));
    /// </code>
    /// </example>
    public static async Task<List<TOutput>> PipeAsync<TInput, TOutput>(
        IEnumerable<TInput> input,
        Func<TInput, Task<TOutput>> transform,
        int maxDegreeOfParallelism = -1,
        CancellationToken cancellationToken = default)
    {
        return await ParallelSelectAsync(input, transform, maxDegreeOfParallelism, cancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// 执行任务管道（带中间状态）。
    /// </summary>
    /// <typeparam name="TInput">输入类型。</typeparam>
    /// <typeparam name="TIntermediate">中间类型。</typeparam>
    /// <typeparam name="TOutput">输出类型。</typeparam>
    /// <param name="input">输入数据。</param>
    /// <param name="firstTransform">第一个转换函数。</param>
    /// <param name="secondTransform">第二个转换函数。</param>
    /// <param name="cancellationToken">取消令牌。</param>
    /// <returns>输出结果。</returns>
    /// <exception cref="ArgumentNullException">input、firstTransform 或 secondTransform 为 null 时抛出。</exception>
    /// <example>
    /// <code>
    /// var result = await TaskHelper.PipeAsync(
    ///     "input",
    ///     async s => await Step1Async(s),
    ///     async s => await Step2Async(s));
    /// </code>
    /// </example>
    public static async Task<TOutput> PipeAsync<TInput, TIntermediate, TOutput>(
        TInput input,
        Func<TInput, Task<TIntermediate>> firstTransform,
        Func<TIntermediate, Task<TOutput>> secondTransform,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(firstTransform);
        ArgumentNullException.ThrowIfNull(secondTransform);

        cancellationToken.ThrowIfCancellationRequested();
        var intermediate = await firstTransform(input).ConfigureAwait(false);

        cancellationToken.ThrowIfCancellationRequested();
        return await secondTransform(intermediate).ConfigureAwait(false);
    }

    #endregion

    #region 任务队列

    /// <summary>
    /// 创建任务队列。
    /// </summary>
    /// <typeparam name="T">任务数据类型。</typeparam>
    /// <param name="processor">任务处理函数。</param>
    /// <param name="maxDegreeOfParallelism">最大并行度。</param>
    /// <param name="capacity">队列容量。</param>
    /// <returns>任务队列实例。</returns>
    /// <exception cref="ArgumentNullException">processor 为 null 时抛出。</exception>
    /// <exception cref="ArgumentOutOfRangeException">maxDegreeOfParallelism 或 capacity 无效时抛出。</exception>
    /// <example>
    /// <code>
    /// using var queue = TaskHelper.CreateTaskQueue&lt;string&gt;(
    ///     async item => await ProcessItemAsync(item),
    ///     maxDegreeOfParallelism: 5);
    /// 
    /// await queue.EnqueueAsync("item1");
    /// await queue.EnqueueAsync("item2");
    /// await queue.CompleteAsync();
    /// </code>
    /// </example>
    public static TaskQueue<T> CreateTaskQueue<T>(
        Func<T, Task> processor,
        int maxDegreeOfParallelism = 1,
        int capacity = 1000)
    {
        ArgumentNullException.ThrowIfNull(processor);
        ArgumentOutOfRangeException.ThrowIfLessThanOrEqual(maxDegreeOfParallelism, 0);
        ArgumentOutOfRangeException.ThrowIfLessThanOrEqual(capacity, 0);

        return new TaskQueue<T>(processor, maxDegreeOfParallelism, capacity);
    }

    /// <summary>
    /// 创建优先级任务队列。
    /// </summary>
    /// <typeparam name="T">任务数据类型。</typeparam>
    /// <param name="processor">任务处理函数。</param>
    /// <param name="comparer">优先级比较器。</param>
    /// <param name="maxDegreeOfParallelism">最大并行度。</param>
    /// <returns>优先级任务队列实例。</returns>
    /// <exception cref="ArgumentNullException">processor 为 null 时抛出。</exception>
    /// <example>
    /// <code>
    /// var queue = TaskHelper.CreatePriorityTaskQueue&lt;int&gt;(
    ///     async item => await ProcessItemAsync(item),
    ///     Comparer&lt;int&gt;.Default);
    /// 
    /// await queue.EnqueueAsync(5); // 低优先级
    /// await queue.EnqueueAsync(1); // 高优先级
    /// </code>
    /// </example>
    public static PriorityTaskQueue<T> CreatePriorityTaskQueue<T>(
        Func<T, Task> processor,
        IComparer<T> comparer,
        int maxDegreeOfParallelism = 1)
    {
        ArgumentNullException.ThrowIfNull(processor);
        ArgumentNullException.ThrowIfNull(comparer);
        ArgumentOutOfRangeException.ThrowIfLessThanOrEqual(maxDegreeOfParallelism, 0);

        return new PriorityTaskQueue<T>(processor, comparer, maxDegreeOfParallelism);
    }

    /// <summary>
    /// 创建生产者-消费者队列。
    /// </summary>
    /// <typeparam name="T">数据类型。</typeparam>
    /// <param name="consumer">消费者函数。</param>
    /// <param name="boundedCapacity">有界容量。</param>
    /// <returns>生产者-消费者队列实例。</returns>
    /// <exception cref="ArgumentNullException">consumer 为 null 时抛出。</exception>
    /// <example>
    /// <code>
    /// var producerConsumer = TaskHelper.CreateProducerConsumerQueue&lt;string&gt;(
    ///     async item => await ProcessItemAsync(item),
    ///     boundedCapacity: 100);
    /// 
    /// // 生产者
    /// await producerConsumer.WriteAsync("item1");
    /// 
    /// // 消费者自动处理
    /// await producerConsumer.Completion;
    /// </code>
    /// </example>
    public static ProducerConsumerQueue<T> CreateProducerConsumerQueue<T>(
        Func<T, Task> consumer,
        int boundedCapacity = 1000)
    {
        ArgumentNullException.ThrowIfNull(consumer);
        ArgumentOutOfRangeException.ThrowIfLessThanOrEqual(boundedCapacity, 0);

        return new ProducerConsumerQueue<T>(consumer, boundedCapacity);
    }

    #endregion

    #region 任务池

    /// <summary>
    /// 创建任务池。
    /// </summary>
    /// <typeparam name="T">任务数据类型。</typeparam>
    /// <typeparam name="TResult">任务结果类型。</typeparam>
    /// <param name="processor">任务处理函数。</param>
    /// <param name="maxDegreeOfParallelism">最大并行度。</param>
    /// <returns>任务池实例。</returns>
    /// <exception cref="ArgumentNullException">processor 为 null 时抛出。</exception>
    /// <exception cref="ArgumentOutOfRangeException">maxDegreeOfParallelism 无效时抛出。</exception>
    /// <example>
    /// <code>
    /// using var pool = TaskHelper.CreateTaskPool&lt;string, int&gt;(
    ///     async item => await ProcessItemAsync(item),
    ///     maxDegreeOfParallelism: 10);
    /// 
    /// var result1 = await pool.ExecuteAsync("item1");
    /// var result2 = await pool.ExecuteAsync("item2");
    /// </code>
    /// </example>
    public static TaskPool<T, TResult> CreateTaskPool<T, TResult>(
        Func<T, Task<TResult>> processor,
        int maxDegreeOfParallelism = 10)
    {
        ArgumentNullException.ThrowIfNull(processor);
        ArgumentOutOfRangeException.ThrowIfLessThanOrEqual(maxDegreeOfParallelism, 0);

        return new TaskPool<T, TResult>(processor, maxDegreeOfParallelism);
    }

    /// <summary>
    /// 创建资源池。
    /// </summary>
    /// <typeparam name="T">资源类型。</typeparam>
    /// <param name="factory">资源工厂函数。</param>
    /// <param name="maxPoolSize">最大池大小。</param>
    /// <returns>资源池实例。</returns>
    /// <exception cref="ArgumentNullException">factory 为 null 时抛出。</exception>
    /// <exception cref="ArgumentOutOfRangeException">maxPoolSize 无效时抛出。</exception>
    /// <example>
    /// <code>
    /// using var pool = TaskHelper.CreateResourcePool(
    ///     () => new HttpClient(),
    ///     maxPoolSize: 10);
    /// 
    /// var client = await pool.RentAsync();
    /// try
    /// {
    ///     var response = await client.GetAsync("https://example.com");
    /// }
    /// finally
    /// {
    ///     pool.Return(client);
    /// }
    /// </code>
    /// </example>
    public static ResourcePool<T> CreateResourcePool<T>(
        Func<T> factory,
        int maxPoolSize = 10) where T : class
    {
        ArgumentNullException.ThrowIfNull(factory);
        ArgumentOutOfRangeException.ThrowIfLessThanOrEqual(maxPoolSize, 0);

        return new ResourcePool<T>(factory, maxPoolSize);
    }

    #endregion

    #region 任务监控

    /// <summary>
    /// 监控任务执行性能。
    /// </summary>
    /// <param name="task">要监控的任务。</param>
    /// <param name="taskName">任务名称。</param>
    /// <returns>性能信息。</returns>
    /// <exception cref="ArgumentNullException">task 为 null 时抛出。</exception>
    /// <example>
    /// <code>
    /// var info = await TaskHelper.MonitorAsync(LongRunningTask(), "数据处理");
    /// Console.WriteLine($"任务 {info.TaskName} 执行时间: {info.Duration}");
    /// </code>
    /// </example>
    public static async Task<TaskPerformanceInfo> MonitorAsync(Task task, string taskName = "Unnamed")
    {
        ArgumentNullException.ThrowIfNull(task);

        var info = new TaskPerformanceInfo
        {
            TaskName = taskName,
            StartTime = DateTime.UtcNow
        };

        try
        {
            await task.ConfigureAwait(false);
            info.Status = TaskExecutionStatus.Completed;
        }
        catch (OperationCanceledException)
        {
            info.Status = TaskExecutionStatus.Canceled;
        }
        catch (Exception ex)
        {
            info.Status = TaskExecutionStatus.Faulted;
            info.Exception = ex;
        }
        finally
        {
            info.EndTime = DateTime.UtcNow;
            info.Duration = info.EndTime - info.StartTime;
        }

        return info;
    }

    /// <summary>
    /// 监控任务执行性能（泛型版本）。
    /// </summary>
    /// <typeparam name="T">返回值类型。</typeparam>
    /// <param name="task">要监控的任务。</param>
    /// <param name="taskName">任务名称。</param>
    /// <returns>性能信息。</returns>
    /// <exception cref="ArgumentNullException">task 为 null 时抛出。</exception>
    /// <example>
    /// <code>
    /// var info = await TaskHelper.MonitorAsync(FetchDataAsync(), "数据获取");
    /// Console.WriteLine($"结果: {info.Result}, 耗时: {info.Duration}");
    /// </code>
    /// </example>
    public static async Task<TaskPerformanceInfo<T>> MonitorAsync<T>(Task<T> task, string taskName = "Unnamed")
    {
        ArgumentNullException.ThrowIfNull(task);

        var info = new TaskPerformanceInfo<T>
        {
            TaskName = taskName,
            StartTime = DateTime.UtcNow
        };

        try
        {
            info.Result = await task.ConfigureAwait(false);
            info.Status = TaskExecutionStatus.Completed;
        }
        catch (OperationCanceledException)
        {
            info.Status = TaskExecutionStatus.Canceled;
        }
        catch (Exception ex)
        {
            info.Status = TaskExecutionStatus.Faulted;
            info.Exception = ex;
        }
        finally
        {
            info.EndTime = DateTime.UtcNow;
            info.Duration = info.EndTime - info.StartTime;
        }

        return info;
    }

    /// <summary>
    /// 测量任务执行时间。
    /// </summary>
    /// <param name="action">要执行的操作。</param>
    /// <returns>执行时间。</returns>
    /// <exception cref="ArgumentNullException">action 为 null 时抛出。</exception>
    /// <example>
    /// <code>
    /// var duration = await TaskHelper.MeasureAsync(async () => await ProcessData());
    /// Console.WriteLine($"执行时间: {duration}");
    /// </code>
    /// </example>
    public static async Task<TimeSpan> MeasureAsync(Func<Task> action)
    {
        ArgumentNullException.ThrowIfNull(action);

        var stopwatch = System.Diagnostics.Stopwatch.StartNew();
        await action().ConfigureAwait(false);
        stopwatch.Stop();

        return stopwatch.Elapsed;
    }

    /// <summary>
    /// 测量任务执行时间（泛型版本）。
    /// </summary>
    /// <typeparam name="T">返回值类型。</typeparam>
    /// <param name="function">要执行的函数。</param>
    /// <returns>执行时间和结果。</returns>
    /// <exception cref="ArgumentNullException">function 为 null 时抛出。</exception>
    /// <example>
    /// <code>
    /// var (duration, result) = await TaskHelper.MeasureAsync(async () => await CalculateAsync());
    /// Console.WriteLine($"结果: {result}, 耗时: {duration}");
    /// </code>
    /// </example>
    public static async Task<(TimeSpan Duration, T Result)> MeasureAsync<T>(Func<Task<T>> function)
    {
        ArgumentNullException.ThrowIfNull(function);

        var stopwatch = System.Diagnostics.Stopwatch.StartNew();
        var result = await function().ConfigureAwait(false);
        stopwatch.Stop();

        return (stopwatch.Elapsed, result);
    }

    /// <summary>
    /// 带重试的时间测量。
    /// </summary>
    /// <param name="action">要执行的操作。</param>
    /// <param name="maxRetries">最大重试次数。</param>
    /// <param name="delay">重试间隔。</param>
    /// <param name="cancellationToken">取消令牌。</param>
    /// <returns>执行结果。</returns>
    /// <exception cref="ArgumentNullException">action 为 null 时抛出。</exception>
    /// <example>
    /// <code>
    /// var result = await TaskHelper.TimeWithRetryAsync(async () => await CallApi(), maxRetries: 3);
    /// Console.WriteLine($"成功: {result.IsSuccess}, 耗时: {result.Duration}");
    /// </code>
    /// </example>
    public static async Task<TaskExecutionResult> TimeWithRetryAsync(
        Func<Task> action,
        int maxRetries = 3,
        TimeSpan? delay = null,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(action);

        Exception? lastException = null;
        var stopwatch = System.Diagnostics.Stopwatch.StartNew();

        for (int i = 0; i <= maxRetries; i++)
        {
            cancellationToken.ThrowIfCancellationRequested();

            try
            {
                await action().ConfigureAwait(false);
                stopwatch.Stop();
                return TaskExecutionResult.Success(stopwatch.Elapsed);
            }
            catch (OperationCanceledException)
            {
                stopwatch.Stop();
                throw;
            }
            catch (Exception ex)
            {
                lastException = ex;

                if (i < maxRetries)
                {
                    await Task.Delay(delay ?? TimeSpan.FromSeconds(1), cancellationToken).ConfigureAwait(false);
                }
            }
        }

        stopwatch.Stop();
        return TaskExecutionResult.Failure(lastException!, stopwatch.Elapsed);
    }

    #endregion

    #region 任务取消

    /// <summary>
    /// 创建可取消的任务。
    /// </summary>
    /// <param name="action">要执行的操作。</param>
    /// <param name="cancellationToken">取消令牌。</param>
    /// <returns>任务实例。</returns>
    /// <exception cref="ArgumentNullException">action 为 null 时抛出。</exception>
    /// <example>
    /// <code>
    /// using var cts = new CancellationTokenSource();
    /// await TaskHelper.WithCancellation(ct => ProcessWithCancel(ct), cts.Token);
    /// </code>
    /// </example>
    public static Task WithCancellation(
        Action<CancellationToken> action,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(action);
        return Task.Run(() => action(cancellationToken), cancellationToken);
    }

    /// <summary>
    /// 创建可取消的任务（异步版本）。
    /// </summary>
    /// <param name="asyncAction">要执行的异步操作。</param>
    /// <param name="cancellationToken">取消令牌。</param>
    /// <returns>任务实例。</returns>
    /// <exception cref="ArgumentNullException">asyncAction 为 null 时抛出。</exception>
    /// <example>
    /// <code>
    /// using var cts = new CancellationTokenSource();
    /// await TaskHelper.WithCancellationAsync(async ct => await ProcessWithCancelAsync(ct), cts.Token);
    /// </code>
    /// </example>
    public static async Task WithCancellationAsync(
        Func<CancellationToken, Task> asyncAction,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(asyncAction);
        await asyncAction(cancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// 创建可取消的任务（泛型版本）。
    /// </summary>
    /// <typeparam name="T">返回值类型。</typeparam>
    /// <param name="function">要执行的函数。</param>
    /// <param name="cancellationToken">取消令牌。</param>
    /// <returns>任务实例。</returns>
    /// <exception cref="ArgumentNullException">function 为 null 时抛出。</exception>
    /// <example>
    /// <code>
    /// var result = await TaskHelper.WithCancellation&lt;int&gt;(ct => CalculateWithCancel(ct), cts.Token);
    /// </code>
    /// </example>
    public static Task<T> WithCancellation<T>(
        Func<CancellationToken, T> function,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(function);
        return Task.Run(() => function(cancellationToken), cancellationToken);
    }

    /// <summary>
    /// 创建可取消的任务（异步版本，泛型版本）。
    /// </summary>
    /// <typeparam name="T">返回值类型。</typeparam>
    /// <param name="asyncFunction">要执行的异步函数。</param>
    /// <param name="cancellationToken">取消令牌。</param>
    /// <returns>任务实例。</returns>
    /// <exception cref="ArgumentNullException">asyncFunction 为 null 时抛出。</exception>
    /// <example>
    /// <code>
    /// var result = await TaskHelper.WithCancellationAsync(async ct => await FetchDataWithCancel(ct), cts.Token);
    /// </code>
    /// </example>
    public static async Task<T> WithCancellationAsync<T>(
        Func<CancellationToken, Task<T>> asyncFunction,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(asyncFunction);
        return await asyncFunction(cancellationToken).ConfigureAwait(false);
    }

    #endregion

    #region 任务结果处理

    /// <summary>
    /// 处理任务结果。
    /// </summary>
    /// <typeparam name="T">返回值类型。</typeparam>
    /// <param name="task">任务实例。</param>
    /// <param name="onSuccess">成功回调。</param>
    /// <param name="onFailure">失败回调。</param>
    /// <returns>任务实例。</returns>
    /// <exception cref="ArgumentNullException">task 为 null 时抛出。</exception>
    /// <example>
    /// <code>
    /// await TaskHelper.HandleResultAsync(
    ///     FetchDataAsync(),
    ///     result => Console.WriteLine($"成功: {result}"),
    ///     ex => Console.WriteLine($"失败: {ex.Message}"));
    /// </code>
    /// </example>
    public static async Task HandleResultAsync<T>(
        Task<T> task,
        Action<T>? onSuccess = null,
        Action<Exception>? onFailure = null)
    {
        ArgumentNullException.ThrowIfNull(task);

        try
        {
            var result = await task.ConfigureAwait(false);
            onSuccess?.Invoke(result);
        }
        catch (Exception ex)
        {
            onFailure?.Invoke(ex);
            throw;
        }
    }

    /// <summary>
    /// 获取任务结果或默认值。
    /// </summary>
    /// <typeparam name="T">返回值类型。</typeparam>
    /// <param name="task">任务实例。</param>
    /// <param name="defaultValue">默认值。</param>
    /// <returns>任务结果或默认值。</returns>
    /// <exception cref="ArgumentNullException">task 为 null 时抛出。</exception>
    /// <example>
    /// <code>
    /// var result = await TaskHelper.GetResultOrDefaultAsync(FetchDataAsync(), defaultValue: "default");
    /// </code>
    /// </example>
    public static async Task<T> GetResultOrDefaultAsync<T>(Task<T> task, T defaultValue = default!)
    {
        ArgumentNullException.ThrowIfNull(task);

        try
        {
            return await task.ConfigureAwait(false);
        }
        catch
        {
            return defaultValue;
        }
    }

    /// <summary>
    /// 获取任务结果或使用工厂函数创建默认值。
    /// </summary>
    /// <typeparam name="T">返回值类型。</typeparam>
    /// <param name="task">任务实例。</param>
    /// <param name="defaultFactory">默认值工厂函数。</param>
    /// <returns>任务结果或默认值。</returns>
    /// <exception cref="ArgumentNullException">task 或 defaultFactory 为 null 时抛出。</exception>
    /// <example>
    /// <code>
    /// var result = await TaskHelper.GetResultOrDefaultAsync(FetchDataAsync(), () => new Data());
    /// </code>
    /// </example>
    public static async Task<T> GetResultOrDefaultAsync<T>(Task<T> task, Func<T> defaultFactory)
    {
        ArgumentNullException.ThrowIfNull(task);
        ArgumentNullException.ThrowIfNull(defaultFactory);

        try
        {
            return await task.ConfigureAwait(false);
        }
        catch
        {
            return defaultFactory();
        }
    }

    #endregion

    #region 任务异常处理

    /// <summary>
    /// 安全执行任务（捕获所有异常）。
    /// </summary>
    /// <param name="task">要执行的任务。</param>
    /// <returns>异常（如果有）。</returns>
    /// <exception cref="ArgumentNullException">task 为 null 时抛出。</exception>
    /// <example>
    /// <code>
    /// var exception = await TaskHelper.SafeExecuteAsync(RiskyOperation());
    /// if (exception != null)
    /// {
    ///     Console.WriteLine($"操作失败: {exception.Message}");
    /// }
    /// </code>
    /// </example>
    public static async Task<Exception?> SafeExecuteAsync(Task task)
    {
        ArgumentNullException.ThrowIfNull(task);

        try
        {
            await task.ConfigureAwait(false);
            return null;
        }
        catch (Exception ex)
        {
            return ex;
        }
    }

    /// <summary>
    /// 安全执行任务（捕获所有异常，泛型版本）。
    /// </summary>
    /// <typeparam name="T">返回值类型。</typeparam>
    /// <param name="task">要执行的任务。</param>
    /// <param name="defaultValue">默认值。</param>
    /// <returns>结果和异常。</returns>
    /// <exception cref="ArgumentNullException">task 为 null 时抛出。</exception>
    /// <example>
    /// <code>
    /// var (result, exception) = await TaskHelper.SafeExecuteAsync(FetchDataAsync(), defaultValue: null);
    /// if (exception == null)
    /// {
    ///     Console.WriteLine($"结果: {result}");
    /// }
    /// </code>
    /// </example>
    public static async Task<(T Result, Exception? Exception)> SafeExecuteAsync<T>(Task<T> task, T defaultValue = default!)
    {
        ArgumentNullException.ThrowIfNull(task);

        try
        {
            return (await task.ConfigureAwait(false), null);
        }
        catch (Exception ex)
        {
            return (defaultValue, ex);
        }
    }

    /// <summary>
    /// 忽略任务异常。
    /// </summary>
    /// <param name="task">要执行的任务。</param>
    /// <returns>任务实例。</returns>
    /// <exception cref="ArgumentNullException">task 为 null 时抛出。</exception>
    /// <example>
    /// <code>
    /// await TaskHelper.IgnoreExceptionAsync(RiskyOperation());
    /// </code>
    /// </example>
    public static async Task IgnoreExceptionAsync(Task task)
    {
        ArgumentNullException.ThrowIfNull(task);

        try
        {
            await task.ConfigureAwait(false);
        }
        catch
        {
        }
    }

    /// <summary>
    /// 忽略指定类型的异常。
    /// </summary>
    /// <typeparam name="TException">要忽略的异常类型。</typeparam>
    /// <param name="task">要执行的任务。</param>
    /// <returns>任务实例。</returns>
    /// <exception cref="ArgumentNullException">task 为 null 时抛出。</exception>
    /// <example>
    /// <code>
    /// await TaskHelper.IgnoreExceptionAsync&lt;InvalidOperationException&gt;(RiskyOperation());
    /// </code>
    /// </example>
    public static async Task IgnoreExceptionAsync<TException>(Task task) where TException : Exception
    {
        ArgumentNullException.ThrowIfNull(task);

        try
        {
            await task.ConfigureAwait(false);
        }
        catch (TException)
        {
        }
    }

    #endregion

    #region 任务协调

    /// <summary>
    /// 创建任务屏障。
    /// </summary>
    /// <param name="participantCount">参与者数量。</param>
    /// <returns>屏障实例。</returns>
    /// <exception cref="ArgumentOutOfRangeException">participantCount 小于等于 0 时抛出。</exception>
    /// <example>
    /// <code>
    /// using var barrier = TaskHelper.CreateBarrier(3);
    /// </code>
    /// </example>
    public static Barrier CreateBarrier(int participantCount)
    {
        ArgumentOutOfRangeException.ThrowIfLessThanOrEqual(participantCount, 0);
        return new Barrier(participantCount);
    }

    /// <summary>
    /// 创建异步信号量。
    /// </summary>
    /// <param name="initialCount">初始计数。</param>
    /// <param name="maxCount">最大计数。</param>
    /// <returns>信号量实例。</returns>
    /// <exception cref="ArgumentOutOfRangeException">参数无效时抛出。</exception>
    /// <example>
    /// <code>
    /// using var semaphore = TaskHelper.CreateSemaphore(5, 10);
    /// </code>
    /// </example>
    public static SemaphoreSlim CreateSemaphore(int initialCount, int maxCount)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(initialCount);
        ArgumentOutOfRangeException.ThrowIfLessThanOrEqual(maxCount, 0);
        ArgumentOutOfRangeException.ThrowIfGreaterThan(initialCount, maxCount);
        return new SemaphoreSlim(initialCount, maxCount);
    }

    /// <summary>
    /// 创建异步手动重置事件。
    /// </summary>
    /// <param name="initialState">初始状态。</param>
    /// <returns>手动重置事件实例。</returns>
    /// <example>
    /// <code>
    /// using var resetEvent = TaskHelper.CreateManualResetEvent(false);
    /// </code>
    /// </example>
    public static ManualResetEventSlim CreateManualResetEvent(bool initialState = false)
    {
        return new ManualResetEventSlim(initialState);
    }

    /// <summary>
    /// 创建异步自动重置事件。
    /// </summary>
    /// <param name="initialState">初始状态。</param>
    /// <returns>自动重置事件实例。</returns>
    /// <example>
    /// <code>
    /// using var resetEvent = TaskHelper.CreateAutoResetEvent(false);
    /// </code>
    /// </example>
    public static AutoResetEvent CreateAutoResetEvent(bool initialState = false)
    {
        return new AutoResetEvent(initialState);
    }

    #endregion
}

#region 枚举和模型类

/// <summary>
/// 任务执行状态。
/// </summary>
public enum TaskExecutionStatus
{
    /// <summary>
    /// 已完成。
    /// </summary>
    Completed,

    /// <summary>
    /// 已失败。
    /// </summary>
    Faulted,

    /// <summary>
    /// 已取消。
    /// </summary>
    Canceled
}

/// <summary>
/// 任务性能信息。
/// </summary>
public class TaskPerformanceInfo
{
    /// <summary>
    /// 获取或设置任务名称。
    /// </summary>
    public string TaskName { get; set; } = string.Empty;

    /// <summary>
    /// 获取或设置开始时间。
    /// </summary>
    public DateTime StartTime { get; set; }

    /// <summary>
    /// 获取或设置结束时间。
    /// </summary>
    public DateTime EndTime { get; set; }

    /// <summary>
    /// 获取或设置执行时长。
    /// </summary>
    public TimeSpan Duration { get; set; }

    /// <summary>
    /// 获取或设置执行状态。
    /// </summary>
    public TaskExecutionStatus Status { get; set; }

    /// <summary>
    /// 获取或设置异常信息。
    /// </summary>
    public Exception? Exception { get; set; }
}

/// <summary>
/// 任务性能信息（泛型版本）。
/// </summary>
/// <typeparam name="T">返回值类型。</typeparam>
public class TaskPerformanceInfo<T> : TaskPerformanceInfo
{
    /// <summary>
    /// 获取或设置返回结果。
    /// </summary>
    public T? Result { get; set; }
}

/// <summary>
/// 任务执行结果。
/// </summary>
public class TaskExecutionResult
{
    /// <summary>
    /// 获取或设置是否成功。
    /// </summary>
    public bool IsSuccess { get; set; }

    /// <summary>
    /// 获取或设置异常信息。
    /// </summary>
    public Exception? Exception { get; set; }

    /// <summary>
    /// 获取或设置执行时长。
    /// </summary>
    public TimeSpan Duration { get; set; }

    /// <summary>
    /// 创建成功结果。
    /// </summary>
    /// <param name="duration">执行时长。</param>
    /// <returns>成功结果实例。</returns>
    public static TaskExecutionResult Success(TimeSpan duration) => new() { IsSuccess = true, Duration = duration };

    /// <summary>
    /// 创建失败结果。
    /// </summary>
    /// <param name="exception">异常。</param>
    /// <param name="duration">执行时长。</param>
    /// <returns>失败结果实例。</returns>
    public static TaskExecutionResult Failure(Exception exception, TimeSpan duration) => new() { IsSuccess = false, Exception = exception, Duration = duration };
}

/// <summary>
/// 任务执行结果（泛型版本）。
/// </summary>
/// <typeparam name="T">返回值类型。</typeparam>
public class TaskExecutionResult<T> : TaskExecutionResult
{
    /// <summary>
    /// 获取或设置返回结果。
    /// </summary>
    public T? Result { get; set; }

    /// <summary>
    /// 创建成功结果。
    /// </summary>
    /// <param name="result">返回结果。</param>
    /// <param name="duration">执行时长。</param>
    /// <returns>成功结果实例。</returns>
    public static TaskExecutionResult<T> Success(T result, TimeSpan duration) => new() { IsSuccess = true, Result = result, Duration = duration };

    /// <summary>
    /// 创建失败结果。
    /// </summary>
    /// <param name="exception">异常。</param>
    /// <param name="duration">执行时长。</param>
    /// <returns>失败结果实例。</returns>
    public new static TaskExecutionResult<T> Failure(Exception exception, TimeSpan duration) => new() { IsSuccess = false, Exception = exception, Duration = duration };
}

/// <summary>
/// 断路器状态。
/// </summary>
public enum CircuitBreakerState
{
    /// <summary>
    /// 关闭状态（正常执行）。
    /// </summary>
    Closed,

    /// <summary>
    /// 打开状态（熔断）。
    /// </summary>
    Open,

    /// <summary>
    /// 半开状态（测试恢复）。
    /// </summary>
    HalfOpen
}

/// <summary>
/// 断路器打开异常。
/// </summary>
public class CircuitBreakerOpenException : Exception
{
    /// <summary>
    /// 初始化断路器打开异常。
    /// </summary>
    public CircuitBreakerOpenException() : base("断路器处于打开状态，拒绝执行任务") { }

    /// <summary>
    /// 初始化断路器打开异常。
    /// </summary>
    /// <param name="message">异常消息。</param>
    public CircuitBreakerOpenException(string message) : base(message) { }

    /// <summary>
    /// 初始化断路器打开异常。
    /// </summary>
    /// <param name="message">异常消息。</param>
    /// <param name="innerException">内部异常。</param>
    public CircuitBreakerOpenException(string message, Exception innerException) : base(message, innerException) { }
}

/// <summary>
/// 断路器实现。
/// </summary>
public class CircuitBreaker
{
    private readonly int _failureThreshold;
    private readonly int _successThreshold;
    private readonly TimeSpan _resetTimeout;
    private readonly object _lock = new();
    private int _failureCount;
    private int _successCount;
    private DateTime _lastFailureTime;
    private CircuitBreakerState _state = CircuitBreakerState.Closed;

    /// <summary>
    /// 初始化断路器。
    /// </summary>
    /// <param name="failureThreshold">失败阈值。</param>
    /// <param name="successThreshold">成功阈值。</param>
    /// <param name="resetTimeout">重置超时时间。</param>
    public CircuitBreaker(int failureThreshold, int successThreshold, TimeSpan resetTimeout)
    {
        _failureThreshold = failureThreshold;
        _successThreshold = successThreshold;
        _resetTimeout = resetTimeout;
    }

    /// <summary>
    /// 获取当前状态。
    /// </summary>
    public CircuitBreakerState State
    {
        get
        {
            lock (_lock)
            {
                if (_state == CircuitBreakerState.Open && DateTime.UtcNow - _lastFailureTime >= _resetTimeout)
                {
                    _state = CircuitBreakerState.HalfOpen;
                    _successCount = 0;
                }
                return _state;
            }
        }
    }

    /// <summary>
    /// 执行任务。
    /// </summary>
    /// <param name="taskFactory">任务工厂。</param>
    /// <param name="cancellationToken">取消令牌。</param>
    /// <returns>任务实例。</returns>
    public async Task ExecuteAsync(Func<Task> taskFactory, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(taskFactory);

        if (!CanExecute())
        {
            throw new CircuitBreakerOpenException();
        }

        try
        {
            await taskFactory().ConfigureAwait(false);
            OnSuccess();
        }
        catch (Exception)
        {
            OnFailure();
            throw;
        }
    }

    /// <summary>
    /// 执行任务（泛型版本）。
    /// </summary>
    /// <typeparam name="T">返回值类型。</typeparam>
    /// <param name="taskFactory">任务工厂。</param>
    /// <param name="cancellationToken">取消令牌。</param>
    /// <returns>任务返回值。</returns>
    public async Task<T> ExecuteAsync<T>(Func<Task<T>> taskFactory, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(taskFactory);

        if (!CanExecute())
        {
            throw new CircuitBreakerOpenException();
        }

        try
        {
            var result = await taskFactory().ConfigureAwait(false);
            OnSuccess();
            return result;
        }
        catch (Exception)
        {
            OnFailure();
            throw;
        }
    }

    /// <summary>
    /// 重置断路器。
    /// </summary>
    public void Reset()
    {
        lock (_lock)
        {
            _state = CircuitBreakerState.Closed;
            _failureCount = 0;
            _successCount = 0;
        }
    }

    private bool CanExecute()
    {
        lock (_lock)
        {
            if (_state == CircuitBreakerState.Closed)
            {
                return true;
            }

            if (_state == CircuitBreakerState.Open)
            {
                if (DateTime.UtcNow - _lastFailureTime >= _resetTimeout)
                {
                    _state = CircuitBreakerState.HalfOpen;
                    _successCount = 0;
                    return true;
                }
                return false;
            }

            return true;
        }
    }

    private void OnSuccess()
    {
        lock (_lock)
        {
            if (_state == CircuitBreakerState.HalfOpen)
            {
                _successCount++;
                if (_successCount >= _successThreshold)
                {
                    _state = CircuitBreakerState.Closed;
                    _failureCount = 0;
                }
            }
            else
            {
                _failureCount = 0;
            }
        }
    }

    private void OnFailure()
    {
        lock (_lock)
        {
            _lastFailureTime = DateTime.UtcNow;
            _failureCount++;

            if (_state == CircuitBreakerState.HalfOpen)
            {
                _state = CircuitBreakerState.Open;
            }
            else if (_failureCount >= _failureThreshold)
            {
                _state = CircuitBreakerState.Open;
            }
        }
    }
}

/// <summary>
/// 任务队列。
/// </summary>
/// <typeparam name="T">任务数据类型。</typeparam>
public class TaskQueue<T> : IDisposable
{
    private readonly Channel<T> _channel;
    private readonly Func<T, Task> _processor;
    private readonly int _maxDegreeOfParallelism;
    private readonly SemaphoreSlim _semaphore;
    private int _processedCount;
    private int _failedCount;
    private bool _disposed;

    /// <summary>
    /// 获取已处理数量。
    /// </summary>
    public int ProcessedCount => _processedCount;

    /// <summary>
    /// 获取失败数量。
    /// </summary>
    public int FailedCount => _failedCount;

    /// <summary>
    /// 初始化任务队列。
    /// </summary>
    /// <param name="processor">任务处理函数。</param>
    /// <param name="maxDegreeOfParallelism">最大并行度。</param>
    /// <param name="capacity">队列容量。</param>
    public TaskQueue(Func<T, Task> processor, int maxDegreeOfParallelism, int capacity)
    {
        _processor = processor;
        _maxDegreeOfParallelism = maxDegreeOfParallelism;
        _semaphore = new SemaphoreSlim(maxDegreeOfParallelism, maxDegreeOfParallelism);
        _channel = Channel.CreateBounded<T>(new BoundedChannelOptions(capacity)
        {
            FullMode = BoundedChannelFullMode.Wait
        });

        _ = ProcessItemsAsync();
    }

    /// <summary>
    /// 入队任务。
    /// </summary>
    /// <param name="item">任务数据。</param>
    /// <param name="cancellationToken">取消令牌。</param>
    /// <returns>任务实例。</returns>
    public async ValueTask EnqueueAsync(T item, CancellationToken cancellationToken = default)
    {
        await _channel.Writer.WriteAsync(item, cancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// 完成队列。
    /// </summary>
    public void Complete()
    {
        _channel.Writer.Complete();
    }

    /// <summary>
    /// 等待队列完成。
    /// </summary>
    /// <returns>任务实例。</returns>
    public async Task CompletionAsync()
    {
        await _channel.Reader.Completion.ConfigureAwait(false);
        for (int i = 0; i < _maxDegreeOfParallelism; i++)
        {
            await _semaphore.WaitAsync().ConfigureAwait(false);
        }
        for (int i = 0; i < _maxDegreeOfParallelism; i++)
        {
            _semaphore.Release();
        }
    }

    private async Task ProcessItemsAsync()
    {
        await foreach (var item in _channel.Reader.ReadAllAsync())
        {
            await _semaphore.WaitAsync().ConfigureAwait(false);
            _ = ProcessItemAsync(item);
        }
    }

    private async Task ProcessItemAsync(T item)
    {
        try
        {
            await _processor(item).ConfigureAwait(false);
            Interlocked.Increment(ref _processedCount);
        }
        catch
        {
            Interlocked.Increment(ref _failedCount);
        }
        finally
        {
            _semaphore.Release();
        }
    }

    /// <summary>
    /// 释放资源。
    /// </summary>
    public void Dispose()
    {
        if (!_disposed)
        {
            _disposed = true;
            _semaphore.Dispose();
        }
    }
}

/// <summary>
/// 优先级任务队列。
/// </summary>
/// <typeparam name="T">任务数据类型。</typeparam>
public class PriorityTaskQueue<T> : IDisposable
{
    private readonly PriorityChannel<T> _channel;
    private readonly Func<T, Task> _processor;
    private readonly int _maxDegreeOfParallelism;
    private readonly SemaphoreSlim _semaphore;
    private int _processedCount;
    private bool _disposed;

    /// <summary>
    /// 获取已处理数量。
    /// </summary>
    public int ProcessedCount => _processedCount;

    /// <summary>
    /// 初始化优先级任务队列。
    /// </summary>
    /// <param name="processor">任务处理函数。</param>
    /// <param name="comparer">优先级比较器。</param>
    /// <param name="maxDegreeOfParallelism">最大并行度。</param>
    public PriorityTaskQueue(Func<T, Task> processor, IComparer<T> comparer, int maxDegreeOfParallelism)
    {
        _processor = processor;
        _maxDegreeOfParallelism = maxDegreeOfParallelism;
        _semaphore = new SemaphoreSlim(maxDegreeOfParallelism, maxDegreeOfParallelism);
        _channel = new PriorityChannel<T>(comparer);

        _ = ProcessItemsAsync();
    }

    /// <summary>
    /// 入队任务。
    /// </summary>
    /// <param name="item">任务数据。</param>
    /// <returns>任务实例。</returns>
    public ValueTask EnqueueAsync(T item)
    {
        _channel.Write(item);
        return ValueTask.CompletedTask;
    }

    /// <summary>
    /// 完成队列。
    /// </summary>
    public void Complete()
    {
        _channel.Complete();
    }

    /// <summary>
    /// 等待队列完成。
    /// </summary>
    /// <returns>任务实例。</returns>
    public async Task CompletionAsync()
    {
        await _channel.Completion.ConfigureAwait(false);
        for (int i = 0; i < _maxDegreeOfParallelism; i++)
        {
            await _semaphore.WaitAsync().ConfigureAwait(false);
        }
        for (int i = 0; i < _maxDegreeOfParallelism; i++)
        {
            _semaphore.Release();
        }
    }

    private async Task ProcessItemsAsync()
    {
        while (await _channel.WaitToReadAsync().ConfigureAwait(false))
        {
            while (_channel.TryRead(out var item))
            {
                await _semaphore.WaitAsync().ConfigureAwait(false);
                _ = ProcessItemAsync(item);
            }
        }
    }

    private async Task ProcessItemAsync(T item)
    {
        try
        {
            await _processor(item).ConfigureAwait(false);
            Interlocked.Increment(ref _processedCount);
        }
        finally
        {
            _semaphore.Release();
        }
    }

    /// <summary>
    /// 释放资源。
    /// </summary>
    public void Dispose()
    {
        if (!_disposed)
        {
            _disposed = true;
            _semaphore.Dispose();
        }
    }
}

/// <summary>
/// 生产者-消费者队列。
/// </summary>
/// <typeparam name="T">数据类型。</typeparam>
public class ProducerConsumerQueue<T> : IDisposable
{
    private readonly Channel<T> _channel;
    private readonly Func<T, Task> _consumer;
    private readonly Task _consumerTask;
    private bool _disposed;

    /// <summary>
    /// 获取完成任务。
    /// </summary>
    public Task Completion => _channel.Reader.Completion;

    /// <summary>
    /// 初始化生产者-消费者队列。
    /// </summary>
    /// <param name="consumer">消费者函数。</param>
    /// <param name="boundedCapacity">有界容量。</param>
    public ProducerConsumerQueue(Func<T, Task> consumer, int boundedCapacity)
    {
        _consumer = consumer;
        _channel = Channel.CreateBounded<T>(boundedCapacity);
        _consumerTask = ConsumeAsync();
    }

    /// <summary>
    /// 写入数据。
    /// </summary>
    /// <param name="item">数据项。</param>
    /// <param name="cancellationToken">取消令牌。</param>
    /// <returns>任务实例。</returns>
    public async ValueTask WriteAsync(T item, CancellationToken cancellationToken = default)
    {
        await _channel.Writer.WriteAsync(item, cancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// 完成写入。
    /// </summary>
    public void Complete()
    {
        _channel.Writer.Complete();
    }

    private async Task ConsumeAsync()
    {
        await foreach (var item in _channel.Reader.ReadAllAsync())
        {
            await _consumer(item).ConfigureAwait(false);
        }
    }

    /// <summary>
    /// 释放资源。
    /// </summary>
    public void Dispose()
    {
        if (!_disposed)
        {
            _disposed = true;
            _channel.Writer.TryComplete();
        }
    }
}

/// <summary>
/// 任务池。
/// </summary>
/// <typeparam name="T">任务数据类型。</typeparam>
/// <typeparam name="TResult">任务结果类型。</typeparam>
public class TaskPool<T, TResult> : IDisposable
{
    private readonly Func<T, Task<TResult>> _processor;
    private readonly SemaphoreSlim _semaphore;
    private bool _disposed;

    /// <summary>
    /// 初始化任务池。
    /// </summary>
    /// <param name="processor">任务处理函数。</param>
    /// <param name="maxDegreeOfParallelism">最大并行度。</param>
    public TaskPool(Func<T, Task<TResult>> processor, int maxDegreeOfParallelism)
    {
        _processor = processor;
        _semaphore = new SemaphoreSlim(maxDegreeOfParallelism, maxDegreeOfParallelism);
    }

    /// <summary>
    /// 执行任务。
    /// </summary>
    /// <param name="item">任务数据。</param>
    /// <param name="cancellationToken">取消令牌。</param>
    /// <returns>任务结果。</returns>
    public async Task<TResult> ExecuteAsync(T item, CancellationToken cancellationToken = default)
    {
        await _semaphore.WaitAsync(cancellationToken).ConfigureAwait(false);
        try
        {
            return await _processor(item).ConfigureAwait(false);
        }
        finally
        {
            _semaphore.Release();
        }
    }

    /// <summary>
    /// 释放资源。
    /// </summary>
    public void Dispose()
    {
        if (!_disposed)
        {
            _disposed = true;
            _semaphore.Dispose();
        }
    }
}

/// <summary>
/// 资源池。
/// </summary>
/// <typeparam name="T">资源类型。</typeparam>
public class ResourcePool<T> : IDisposable where T : class
{
    private readonly ConcurrentBag<T> _pool;
    private readonly Func<T> _factory;
    private readonly int _maxPoolSize;
    private int _currentCount;
    private bool _disposed;

    /// <summary>
    /// 初始化资源池。
    /// </summary>
    /// <param name="factory">资源工厂函数。</param>
    /// <param name="maxPoolSize">最大池大小。</param>
    public ResourcePool(Func<T> factory, int maxPoolSize)
    {
        _factory = factory;
        _maxPoolSize = maxPoolSize;
        _pool = new ConcurrentBag<T>();
    }

    /// <summary>
    /// 租用资源。
    /// </summary>
    /// <returns>资源实例。</returns>
    public T Rent()
    {
        if (_pool.TryTake(out var item))
        {
            return item;
        }

        return _factory();
    }

    /// <summary>
    /// 租用资源（异步版本）。
    /// </summary>
    /// <returns>资源实例。</returns>
    public ValueTask<T> RentAsync()
    {
        return ValueTask.FromResult(Rent());
    }

    /// <summary>
    /// 归还资源。
    /// </summary>
    /// <param name="item">资源实例。</param>
    public void Return(T item)
    {
        if (_currentCount < _maxPoolSize)
        {
            Interlocked.Increment(ref _currentCount);
            _pool.Add(item);
        }
    }

    /// <summary>
    /// 释放资源。
    /// </summary>
    public void Dispose()
    {
        if (!_disposed)
        {
            _disposed = true;
            while (_pool.TryTake(out var item))
            {
                if (item is IDisposable disposable)
                {
                    disposable.Dispose();
                }
            }
        }
    }
}

/// <summary>
/// 优先级通道。
/// </summary>
/// <typeparam name="T">数据类型。</typeparam>
internal class PriorityChannel<T>
{
    private readonly PriorityQueue<T, T> _queue;
    private readonly SemaphoreSlim _semaphore = new(0);
    private readonly TaskCompletionSource<bool> _completionTcs = new();
    private bool _completed;
    private int _pendingCount;

    /// <summary>
    /// 获取完成任务。
    /// </summary>
    public Task Completion => _completionTcs.Task;

    /// <summary>
    /// 初始化优先级通道。
    /// </summary>
    /// <param name="comparer">优先级比较器。</param>
    public PriorityChannel(IComparer<T> comparer)
    {
        _queue = new PriorityQueue<T, T>(comparer);
    }

    /// <summary>
    /// 写入数据。
    /// </summary>
    /// <param name="item">数据项。</param>
    public void Write(T item)
    {
        lock (_queue)
        {
            if (_completed) return;
            _queue.Enqueue(item, item);
            _pendingCount++;
        }
        _semaphore.Release();
    }

    /// <summary>
    /// 完成写入。
    /// </summary>
    public void Complete()
    {
        lock (_queue)
        {
            _completed = true;
            if (_pendingCount == 0)
            {
                _completionTcs.TrySetResult(true);
            }
        }
        _semaphore.Release();
    }

    /// <summary>
    /// 等待读取。
    /// </summary>
    /// <returns>是否可以读取。</returns>
    public async ValueTask<bool> WaitToReadAsync()
    {
        await _semaphore.WaitAsync().ConfigureAwait(false);
        lock (_queue)
        {
            return _queue.Count > 0;
        }
    }

    /// <summary>
    /// 尝试读取。
    /// </summary>
    /// <param name="item">数据项。</param>
    /// <returns>是否成功。</returns>
    public bool TryRead(out T? item)
    {
        lock (_queue)
        {
            if (_queue.Count == 0)
            {
                item = default;
                return false;
            }
            item = _queue.Dequeue();
            _pendingCount--;
            if (_completed && _pendingCount == 0)
            {
                _completionTcs.TrySetResult(true);
            }
            return true;
        }
    }
}

#endregion