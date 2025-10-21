using System.Threading.Channels;

namespace Chet.Utils.Helpers
{
    /// <summary>
    /// Task操作帮助类
    /// 提供异步任务管理、调度、监控和控制功能
    /// </summary>
    public static class TaskHelper
    {
        #region 任务创建与启动

        /// <summary>
        /// 创建并启动一个异步任务
        /// </summary>
        /// <param name="action">要执行的操作</param>
        /// <param name="cancellationToken">取消令牌</param>
        /// <returns>Task实例</returns>
        public static Task Run(Action action, CancellationToken cancellationToken = default)
        {
            return Task.Run(action, cancellationToken);
        }

        /// <summary>
        /// 创建并启动一个带返回值的异步任务
        /// </summary>
        /// <typeparam name="T">返回值类型</typeparam>
        /// <param name="function">要执行的函数</param>
        /// <param name="cancellationToken">取消令牌</param>
        /// <returns>Task实例</returns>
        public static Task<T> Run<T>(Func<T> function, CancellationToken cancellationToken = default)
        {
            return Task.Run(function, cancellationToken);
        }

        /// <summary>
        /// 创建并启动一个异步任务（异步委托版本）
        /// </summary>
        /// <param name="asyncAction">要执行的异步操作</param>
        /// <param name="cancellationToken">取消令牌</param>
        /// <returns>Task实例</returns>
        public static Task RunAsync(Func<Task> asyncAction, CancellationToken cancellationToken = default)
        {
            return Task.Run(asyncAction, cancellationToken);
        }

        /// <summary>
        /// 创建并启动一个带返回值的异步任务（异步委托版本）
        /// </summary>
        /// <typeparam name="T">返回值类型</typeparam>
        /// <param name="asyncFunction">要执行的异步函数</param>
        /// <param name="cancellationToken">取消令牌</param>
        /// <returns>Task实例</returns>
        public static Task<T> RunAsync<T>(Func<Task<T>> asyncFunction, CancellationToken cancellationToken = default)
        {
            return Task.Run(asyncFunction, cancellationToken);
        }

        /// <summary>
        /// 创建一个延迟任务
        /// </summary>
        /// <param name="delay">延迟时间</param>
        /// <param name="cancellationToken">取消令牌</param>
        /// <returns>Task实例</returns>
        public static Task Delay(TimeSpan delay, CancellationToken cancellationToken = default)
        {
            return Task.Delay(delay, cancellationToken);
        }

        /// <summary>
        /// 创建一个延迟任务
        /// </summary>
        /// <param name="millisecondsDelay">延迟毫秒数</param>
        /// <param name="cancellationToken">取消令牌</param>
        /// <returns>Task实例</returns>
        public static Task Delay(int millisecondsDelay, CancellationToken cancellationToken = default)
        {
            return Task.Delay(millisecondsDelay, cancellationToken);
        }

        /// <summary>
        /// 创建一个已完成的任务
        /// </summary>
        /// <returns>已完成的Task实例</returns>
        public static Task CompletedTask()
        {
            return Task.CompletedTask;
        }

        /// <summary>
        /// 创建一个已完成的任务（带返回值）
        /// </summary>
        /// <typeparam name="T">返回值类型</typeparam>
        /// <param name="result">返回值</param>
        /// <returns>已完成的Task实例</returns>
        public static Task<T> FromResult<T>(T result)
        {
            return Task.FromResult(result);
        }

        /// <summary>
        /// 创建一个失败的任务
        /// </summary>
        /// <param name="exception">异常</param>
        /// <returns>失败的Task实例</returns>
        public static Task FromException(Exception exception)
        {
            return Task.FromException(exception);
        }

        /// <summary>
        /// 创建一个失败的任务（带返回值类型）
        /// </summary>
        /// <typeparam name="T">返回值类型</typeparam>
        /// <param name="exception">异常</param>
        /// <returns>失败的Task实例</returns>
        public static Task<T> FromException<T>(Exception exception)
        {
            return Task.FromException<T>(exception);
        }

        /// <summary>
        /// 创建一个已取消的任务
        /// </summary>
        /// <returns>已取消的Task实例</returns>
        public static Task FromCanceled()
        {
            return Task.FromCanceled(new CancellationToken(true));
        }

        /// <summary>
        /// 创建一个已取消的任务（带返回值类型）
        /// </summary>
        /// <typeparam name="T">返回值类型</typeparam>
        /// <returns>已取消的Task实例</returns>
        public static Task<T> FromCanceled<T>()
        {
            return Task.FromCanceled<T>(new CancellationToken(true));
        }

        #endregion

        #region 任务控制与管理

        /// <summary>
        /// 等待所有任务完成
        /// </summary>
        /// <param name="tasks">任务数组</param>
        /// <returns>Task实例</returns>
        public static Task WhenAll(params Task[] tasks)
        {
            return Task.WhenAll(tasks);
        }

        /// <summary>
        /// 等待所有任务完成（泛型版本）
        /// </summary>
        /// <typeparam name="T">返回值类型</typeparam>
        /// <param name="tasks">任务数组</param>
        /// <returns>Task实例</returns>
        public static Task<T[]> WhenAll<T>(params Task<T>[] tasks)
        {
            return Task.WhenAll(tasks);
        }

        /// <summary>
        /// 等待任意一个任务完成
        /// </summary>
        /// <param name="tasks">任务数组</param>
        /// <returns>Task实例</returns>
        public static Task<Task> WhenAny(params Task[] tasks)
        {
            return Task.WhenAny(tasks);
        }

        /// <summary>
        /// 等待任意一个任务完成（泛型版本）
        /// </summary>
        /// <typeparam name="T">返回值类型</typeparam>
        /// <param name="tasks">任务数组</param>
        /// <returns>Task实例</returns>
        public static Task<Task<T>> WhenAny<T>(params Task<T>[] tasks)
        {
            return Task.WhenAny(tasks);
        }

        /// <summary>
        /// 超时控制执行任务
        /// </summary>
        /// <param name="task">要执行的任务</param>
        /// <param name="timeout">超时时间</param>
        /// <returns>Task实例</returns>
        public static async Task WithTimeout(this Task task, TimeSpan timeout)
        {
            using (var cts = new CancellationTokenSource(timeout))
            {
                var completedTask = await Task.WhenAny(task, Task.Delay(timeout, cts.Token));
                if (completedTask != task)
                {
                    throw new TimeoutException($"任务在 {timeout} 内未完成");
                }

                await task; // 确保异常被传播
            }
        }

        /// <summary>
        /// 超时控制执行任务（泛型版本）
        /// </summary>
        /// <typeparam name="T">返回值类型</typeparam>
        /// <param name="task">要执行的任务</param>
        /// <param name="timeout">超时时间</param>
        /// <returns>Task实例</returns>
        public static async Task<T> WithTimeout<T>(this Task<T> task, TimeSpan timeout)
        {
            using (var cts = new CancellationTokenSource(timeout))
            {
                var completedTask = await Task.WhenAny(task, Task.Delay(timeout, cts.Token));
                if (completedTask != task)
                {
                    throw new TimeoutException($"任务在 {timeout} 内未完成");
                }

                return await task; // 确保异常被传播
            }
        }

        /// <summary>
        /// 带重试机制的任务执行
        /// </summary>
        /// <param name="taskFactory">任务工厂函数</param>
        /// <param name="maxRetries">最大重试次数</param>
        /// <param name="delay">重试间隔</param>
        /// <param name="cancellationToken">取消令牌</param>
        /// <returns>Task实例</returns>
        public static async Task RetryAsync(Func<Task> taskFactory, int maxRetries, TimeSpan delay, CancellationToken cancellationToken = default)
        {
            for (int i = 0; i <= maxRetries; i++)
            {
                try
                {
                    await taskFactory();
                    return;
                }
                catch (Exception) when (i < maxRetries && !cancellationToken.IsCancellationRequested)
                {
                    await Task.Delay(delay, cancellationToken);
                }
            }
        }

        /// <summary>
        /// 带重试机制的任务执行（泛型版本）
        /// </summary>
        /// <typeparam name="T">返回值类型</typeparam>
        /// <param name="taskFactory">任务工厂函数</param>
        /// <param name="maxRetries">最大重试次数</param>
        /// <param name="delay">重试间隔</param>
        /// <param name="cancellationToken">取消令牌</param>
        /// <returns>Task实例</returns>
        public static async Task<T> RetryAsync<T>(Func<Task<T>> taskFactory, int maxRetries, TimeSpan delay, CancellationToken cancellationToken = default)
        {
            for (int i = 0; i <= maxRetries; i++)
            {
                try
                {
                    return await taskFactory();
                }
                catch (Exception) when (i < maxRetries && !cancellationToken.IsCancellationRequested)
                {
                    await Task.Delay(delay, cancellationToken);
                }
            }

            throw new InvalidOperationException("重试次数已用尽");
        }

        /// <summary>
        /// 任务熔断器模式实现
        /// </summary>
        /// <param name="taskFactory">任务工厂函数</param>
        /// <param name="failureThreshold">失败阈值</param>
        /// <param name="retryTimeout">重试超时时间</param>
        /// <returns>Task实例</returns>
        public static async Task CircuitBreakerAsync(Func<Task> taskFactory, int failureThreshold, TimeSpan retryTimeout)
        {
            var circuitBreaker = new CircuitBreaker(failureThreshold, retryTimeout);
            await circuitBreaker.ExecuteAsync(taskFactory);
        }

        /// <summary>
        /// 任务熔断器模式实现（泛型版本）
        /// </summary>
        /// <typeparam name="T">返回值类型</typeparam>
        /// <param name="taskFactory">任务工厂函数</param>
        /// <param name="failureThreshold">失败阈值</param>
        /// <param name="retryTimeout">重试超时时间</param>
        /// <returns>Task实例</returns>
        public static async Task<T> CircuitBreakerAsync<T>(Func<Task<T>> taskFactory, int failureThreshold, TimeSpan retryTimeout)
        {
            var circuitBreaker = new CircuitBreaker(failureThreshold, retryTimeout);
            return await circuitBreaker.ExecuteAsync(taskFactory);
        }

        #endregion

        #region 任务调度与并行处理

        /// <summary>
        /// 并行执行多个任务
        /// </summary>
        /// <param name="actions">要执行的操作列表</param>
        /// <param name="maxDegreeOfParallelism">最大并行度</param>
        /// <param name="cancellationToken">取消令牌</param>
        /// <returns>Task实例</returns>
        public static async Task ParallelForEachAsync(IEnumerable<Func<Task>> actions, int maxDegreeOfParallelism = -1, CancellationToken cancellationToken = default)
        {
            var semaphore = maxDegreeOfParallelism > 0 ? new SemaphoreSlim(maxDegreeOfParallelism) : null;
            var tasks = new List<Task>();

            foreach (var action in actions)
            {
                if (cancellationToken.IsCancellationRequested)
                    break;

                if (semaphore != null)
                    await semaphore.WaitAsync(cancellationToken);

                var task = Task.Run(async () =>
                {
                    try
                    {
                        await action();
                    }
                    finally
                    {
                        semaphore?.Release();
                    }
                }, cancellationToken);

                tasks.Add(task);
            }

            await Task.WhenAll(tasks);
        }

        /// <summary>
        /// 并行执行多个任务（带数据源）
        /// </summary>
        /// <typeparam name="T">数据类型</typeparam>
        /// <param name="source">数据源</param>
        /// <param name="body">处理函数</param>
        /// <param name="maxDegreeOfParallelism">最大并行度</param>
        /// <param name="cancellationToken">取消令牌</param>
        /// <returns>Task实例</returns>
        public static async Task ParallelForEachAsync<T>(IEnumerable<T> source, Func<T, Task> body, int maxDegreeOfParallelism = -1, CancellationToken cancellationToken = default)
        {
            var semaphore = maxDegreeOfParallelism > 0 ? new SemaphoreSlim(maxDegreeOfParallelism) : null;
            var tasks = new List<Task>();

            foreach (var item in source)
            {
                if (cancellationToken.IsCancellationRequested)
                    break;

                if (semaphore != null)
                    await semaphore.WaitAsync(cancellationToken);

                var task = Task.Run(async () =>
                {
                    try
                    {
                        await body(item);
                    }
                    finally
                    {
                        semaphore?.Release();
                    }
                }, cancellationToken);

                tasks.Add(task);
            }

            await Task.WhenAll(tasks);
        }

        /// <summary>
        /// 有序并行执行任务
        /// </summary>
        /// <typeparam name="T">输入数据类型</typeparam>
        /// <typeparam name="TResult">结果数据类型</typeparam>
        /// <param name="source">数据源</param>
        /// <param name="body">处理函数</param>
        /// <param name="maxDegreeOfParallelism">最大并行度</param>
        /// <param name="cancellationToken">取消令牌</param>
        /// <returns>结果列表</returns>
        public static async Task<List<TResult>> ParallelSelectAsync<T, TResult>(IEnumerable<T> source, Func<T, Task<TResult>> body, int maxDegreeOfParallelism = -1, CancellationToken cancellationToken = default)
        {
            var semaphore = maxDegreeOfParallelism > 0 ? new SemaphoreSlim(maxDegreeOfParallelism) : null;
            var tasks = new List<Task<(int Index, TResult Result)>>();
            var items = source.ToList();

            for (int i = 0; i < items.Count; i++)
            {
                if (cancellationToken.IsCancellationRequested)
                    break;

                var index = i;
                var item = items[i];

                if (semaphore != null)
                    await semaphore.WaitAsync(cancellationToken);

                var task = Task.Run(async () =>
                {
                    try
                    {
                        var result = await body(item);
                        return (index, result);
                    }
                    finally
                    {
                        semaphore?.Release();
                    }
                }, cancellationToken);

                tasks.Add(task);
            }

            var results = await Task.WhenAll(tasks);
            return results.OrderBy(r => r.Index).Select(r => r.Result).ToList();
        }

        /// <summary>
        /// 任务队列调度器
        /// </summary>
        /// <param name="taskFactories">任务工厂函数队列</param>
        /// <param name="cancellationToken">取消令牌</param>
        /// <returns>Task实例</returns>
        public static async Task<TaskSchedulerResult> ScheduleTasksAsync(IEnumerable<Func<Task>> taskFactories, CancellationToken cancellationToken = default)
        {
            var result = new TaskSchedulerResult();
            var tasks = new List<Task<TaskExecutionInfo>>();

            foreach (var taskFactory in taskFactories)
            {
                if (cancellationToken.IsCancellationRequested)
                    break;

                var task = Task.Run(async () =>
                {
                    var info = new TaskExecutionInfo
                    {
                        StartTime = DateTime.UtcNow
                    };

                    try
                    {
                        await taskFactory();
                        info.Status = TaskStatus.Success;
                    }
                    catch (Exception ex)
                    {
                        info.Status = TaskStatus.Failed;
                        info.Exception = ex;
                    }
                    finally
                    {
                        info.EndTime = DateTime.UtcNow;
                        info.Duration = info.EndTime - info.StartTime;
                    }

                    return info;
                }, cancellationToken);

                tasks.Add(task);
            }

            var executionInfos = await Task.WhenAll(tasks);
            result.TaskInfos = executionInfos.ToList();
            result.TotalTasks = executionInfos.Length;
            result.SuccessfulTasks = executionInfos.Count(i => i.Status == TaskStatus.Success);
            result.FailedTasks = executionInfos.Count(i => i.Status == TaskStatus.Failed);
            result.TotalDuration = executionInfos.Sum(i => i.Duration.Ticks);

            return result;
        }

        #endregion

        #region 任务监控与统计

        /// <summary>
        /// 任务性能监控
        /// </summary>
        /// <param name="task">要监控的任务</param>
        /// <param name="taskName">任务名称</param>
        /// <returns>Task实例</returns>
        public static async Task<TaskPerformanceInfo> MonitorTaskAsync(Task task, string taskName = "UnnamedTask")
        {
            var performanceInfo = new TaskPerformanceInfo
            {
                TaskName = taskName,
                StartTime = DateTime.UtcNow
            };

            try
            {
                await task;
                performanceInfo.Status = TaskStatus.Success;
            }
            catch (Exception ex)
            {
                performanceInfo.Status = TaskStatus.Failed;
                performanceInfo.Exception = ex;
            }
            finally
            {
                performanceInfo.EndTime = DateTime.UtcNow;
                performanceInfo.Duration = performanceInfo.EndTime - performanceInfo.StartTime;
            }

            return performanceInfo;
        }

        /// <summary>
        /// 任务性能监控（泛型版本）
        /// </summary>
        /// <typeparam name="T">返回值类型</typeparam>
        /// <param name="task">要监控的任务</param>
        /// <param name="taskName">任务名称</param>
        /// <returns>Task实例</returns>
        public static async Task<TaskPerformanceInfo<T>> MonitorTaskAsync<T>(Task<T> task, string taskName = "UnnamedTask")
        {
            var performanceInfo = new TaskPerformanceInfo<T>
            {
                TaskName = taskName,
                StartTime = DateTime.UtcNow
            };

            try
            {
                performanceInfo.Result = await task;
                performanceInfo.Status = TaskStatus.Success;
            }
            catch (Exception ex)
            {
                performanceInfo.Status = TaskStatus.Failed;
                performanceInfo.Exception = ex;
            }
            finally
            {
                performanceInfo.EndTime = DateTime.UtcNow;
                performanceInfo.Duration = performanceInfo.EndTime - performanceInfo.StartTime;
            }

            return performanceInfo;
        }

        /// <summary>
        /// 批量任务性能监控
        /// </summary>
        /// <param name="tasks">任务字典（名称-任务）</param>
        /// <returns>监控结果</returns>
        public static async Task<BatchTaskPerformanceInfo> MonitorBatchTasksAsync(Dictionary<string, Task> tasks)
        {
            var batchInfo = new BatchTaskPerformanceInfo
            {
                StartTime = DateTime.UtcNow,
                TaskInfos = new Dictionary<string, TaskPerformanceInfo>()
            };

            var monitorTasks = tasks.Select(kvp => MonitorTaskAsync(kvp.Value, kvp.Key)).ToList();
            var results = await Task.WhenAll(monitorTasks);

            foreach (var result in results)
            {
                batchInfo.TaskInfos[result.TaskName] = result;
            }

            batchInfo.EndTime = DateTime.UtcNow;
            batchInfo.Duration = batchInfo.EndTime - batchInfo.StartTime;
            batchInfo.SuccessfulTasks = results.Count(r => r.Status == TaskStatus.Success);
            batchInfo.FailedTasks = results.Count(r => r.Status == TaskStatus.Failed);

            return batchInfo;
        }

        /// <summary>
        /// 实时任务状态跟踪
        /// </summary>
        /// <param name="task">要跟踪的任务</param>
        /// <param name="progressCallback">进度回调函数</param>
        /// <param name="interval">检查间隔</param>
        /// <returns>Task实例</returns>
        public static async Task TrackTaskProgress(Task task, Action<TaskProgressInfo> progressCallback, TimeSpan interval)
        {
            var startTime = DateTime.UtcNow;

            while (!task.IsCompleted)
            {
                var progressInfo = new TaskProgressInfo
                {
                    ElapsedTime = DateTime.UtcNow - startTime,
                    Status = task.Status,
                    IsCompleted = task.IsCompleted,
                    IsCanceled = task.IsCanceled,
                    IsFaulted = task.IsFaulted
                };

                progressCallback?.Invoke(progressInfo);
                await Task.Delay(interval);
            }

            await task; // 等待任务完成以传播异常
        }

        #endregion

        #region 任务组合与链式操作

        /// <summary>
        /// 任务链式执行
        /// </summary>
        /// <param name="tasks">任务列表</param>
        /// <returns>Task实例</returns>
        public static async Task ChainTasksAsync(params Func<Task>[] tasks)
        {
            foreach (var task in tasks)
            {
                await task();
            }
        }

        /// <summary>
        /// 带条件的任务链式执行
        /// </summary>
        /// <param name="conditionalTasks">条件任务对列表</param>
        /// <returns>Task实例</returns>
        public static async Task ChainConditionalTasksAsync(params (Func<bool> condition, Func<Task> task)[] conditionalTasks)
        {
            foreach (var (condition, task) in conditionalTasks)
            {
                if (condition())
                {
                    await task();
                }
            }
        }

        /// <summary>
        /// 任务流水线处理
        /// </summary>
        /// <typeparam name="T">数据类型</typeparam>
        /// <param name="data">输入数据</param>
        /// <param name="processors">处理器列表</param>
        /// <returns>处理结果</returns>
        public static async Task<T> PipelineAsync<T>(T data, params Func<T, Task<T>>[] processors)
        {
            var result = data;
            foreach (var processor in processors)
            {
                result = await processor(result);
            }
            return result;
        }

        /// <summary>
        /// 任务分叉执行
        /// </summary>
        /// <param name="mainTask">主任务</param>
        /// <param name="forkedTasks">分叉任务列表</param>
        /// <returns>Task实例</returns>
        public static async Task ForkAsync(Func<Task> mainTask, params Func<Task>[] forkedTasks)
        {
            var tasks = new List<Task> { mainTask() };
            tasks.AddRange(forkedTasks.Select(t => t()));

            await Task.WhenAll(tasks);
        }

        /// <summary>
        /// 任务合并执行
        /// </summary>
        /// <typeparam name="T">返回值类型</typeparam>
        /// <param name="tasks">任务列表</param>
        /// <param name="combiner">合并函数</param>
        /// <returns>合并结果</returns>
        public static async Task<T> CombineAsync<T>(Func<Task<T>>[] tasks, Func<T[], Task<T>> combiner)
        {
            var taskResults = await Task.WhenAll(tasks.Select(t => t()));
            return await combiner(taskResults);
        }

        /// <summary>
        /// 任务竞争执行（只取第一个完成的）
        /// </summary>
        /// <typeparam name="T">返回值类型</typeparam>
        /// <param name="tasks">任务列表</param>
        /// <returns>第一个完成任务的结果</returns>
        public static async Task<T> RaceAsync<T>(params Func<Task<T>>[] tasks)
        {
            var taskList = tasks.Select(t => t()).ToList();
            var completedTask = await Task.WhenAny(taskList);
            return await completedTask;
        }

        #endregion

        #region 高级任务模式

        /// <summary>
        /// 生产者-消费者模式实现
        /// </summary>
        /// <typeparam name="T">数据类型</typeparam>
        /// <param name="producer">生产者函数</param>
        /// <param name="consumer">消费者函数</param>
        /// <param name="bufferSize">缓冲区大小</param>
        /// <param name="cancellationToken">取消令牌</param>
        /// <returns>Task实例</returns>
        public static async Task ProducerConsumerAsync<T>(
            Func<ChannelWriter<T>, CancellationToken, Task> producer,
            Func<ChannelReader<T>, CancellationToken, Task> consumer,
            int bufferSize = 100,
            CancellationToken cancellationToken = default)
        {
            var channel = System.Threading.Channels.Channel.CreateBounded<T>(bufferSize);
            var producerTask = producer(channel.Writer, cancellationToken);
            var consumerTask = consumer(channel.Reader, cancellationToken);

            await Task.WhenAll(producerTask, consumerTask);
        }

        /// <summary>
        /// 任务池管理
        /// </summary>
        /// <param name="maxConcurrency">最大并发数</param>
        /// <returns>TaskPool实例</returns>
        public static TaskPool CreateTaskPool(int maxConcurrency)
        {
            return new TaskPool(maxConcurrency);
        }

        /// <summary>
        /// 任务节流执行
        /// </summary>
        /// <param name="tasks">任务列表</param>
        /// <param name="requestsPerSecond">每秒请求数</param>
        /// <param name="cancellationToken">取消令牌</param>
        /// <returns>Task实例</returns>
        public static async Task ThrottleAsync(IEnumerable<Func<Task>> tasks, int requestsPerSecond, CancellationToken cancellationToken = default)
        {
            var semaphore = new SemaphoreSlim(requestsPerSecond);
            var taskList = new List<Task>();

            foreach (var taskFactory in tasks)
            {
                if (cancellationToken.IsCancellationRequested)
                    break;

                await semaphore.WaitAsync(cancellationToken);

                var task = Task.Run(async () =>
                {
                    try
                    {
                        await taskFactory();
                    }
                    finally
                    {
                        semaphore.Release();
                        await Task.Delay(1000 / requestsPerSecond);
                    }
                }, cancellationToken);

                taskList.Add(task);
            }

            await Task.WhenAll(taskList);
        }

        /// <summary>
        /// 任务优先级队列执行
        /// </summary>
        /// <param name="priorityTasks">优先级任务列表</param>
        /// <returns>Task实例</returns>
        public static async Task ExecutePriorityTasksAsync(IEnumerable<PriorityTask> priorityTasks)
        {
            var tasks = priorityTasks
                .OrderByDescending(pt => pt.Priority)
                .Select(pt => pt.TaskFactory());

            await Task.WhenAll(tasks);
        }

        /// <summary>
        /// 任务状态机模式
        /// </summary>
        /// <param name="initialState">初始状态</param>
        /// <param name="transitions">状态转换函数</param>
        /// <param name="cancellationToken">取消令牌</param>
        /// <returns>最终状态</returns>
        public static async Task<object> StateMachineAsync(
            object initialState,
            Dictionary<object, Func<object, Task<object>>> transitions,
            CancellationToken cancellationToken = default)
        {
            var currentState = initialState;

            while (transitions.ContainsKey(currentState) && !cancellationToken.IsCancellationRequested)
            {
                currentState = await transitions[currentState](currentState);
            }

            return currentState;
        }

        #endregion
    }

    #region 辅助类和数据结构

    /// <summary>
    /// 任务状态枚举
    /// </summary>
    public enum TaskStatus
    {
        /// <summary>
        /// 待处理
        /// </summary>
        Pending,

        /// <summary>
        /// 执行中
        /// </summary>
        Running,

        /// <summary>
        /// 成功
        /// </summary>
        Success,

        /// <summary>
        /// 失败
        /// </summary>
        Failed,

        /// <summary>
        /// 已取消
        /// </summary>
        Canceled
    }

    /// <summary>
    /// 任务执行信息
    /// </summary>
    public class TaskExecutionInfo
    {
        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime StartTime { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime EndTime { get; set; }

        /// <summary>
        /// 执行时长
        /// </summary>
        public TimeSpan Duration { get; set; }

        /// <summary>
        /// 任务状态
        /// </summary>
        public TaskStatus Status { get; set; }

        /// <summary>
        /// 异常信息
        /// </summary>
        public Exception Exception { get; set; }
    }

    /// <summary>
    /// 任务调度器结果
    /// </summary>
    public class TaskSchedulerResult
    {
        /// <summary>
        /// 总任务数
        /// </summary>
        public int TotalTasks { get; set; }

        /// <summary>
        /// 成功任务数
        /// </summary>
        public int SuccessfulTasks { get; set; }

        /// <summary>
        /// 失败任务数
        /// </summary>
        public int FailedTasks { get; set; }

        /// <summary>
        /// 总执行时长
        /// </summary>
        public long TotalDuration { get; set; }

        /// <summary>
        /// 任务信息列表
        /// </summary>
        public List<TaskExecutionInfo> TaskInfos { get; set; }
    }

    /// <summary>
    /// 任务性能信息
    /// </summary>
    public class TaskPerformanceInfo
    {
        /// <summary>
        /// 任务名称
        /// </summary>
        public string TaskName { get; set; }

        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime StartTime { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime EndTime { get; set; }

        /// <summary>
        /// 执行时长
        /// </summary>
        public TimeSpan Duration { get; set; }

        /// <summary>
        /// 任务状态
        /// </summary>
        public TaskStatus Status { get; set; }

        /// <summary>
        /// 异常信息
        /// </summary>
        public Exception Exception { get; set; }
    }

    /// <summary>
    /// 任务性能信息（泛型版本）
    /// </summary>
    /// <typeparam name="T">返回值类型</typeparam>
    public class TaskPerformanceInfo<T> : TaskPerformanceInfo
    {
        /// <summary>
        /// 任务结果
        /// </summary>
        public T Result { get; set; }
    }

    /// <summary>
    /// 批量任务性能信息
    /// </summary>
    public class BatchTaskPerformanceInfo
    {
        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime StartTime { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime EndTime { get; set; }

        /// <summary>
        /// 执行时长
        /// </summary>
        public TimeSpan Duration { get; set; }

        /// <summary>
        /// 成功任务数
        /// </summary>
        public int SuccessfulTasks { get; set; }

        /// <summary>
        /// 失败任务数
        /// </summary>
        public int FailedTasks { get; set; }

        /// <summary>
        /// 任务信息字典
        /// </summary>
        public Dictionary<string, TaskPerformanceInfo> TaskInfos { get; set; }
    }

    /// <summary>
    /// 任务进度信息
    /// </summary>
    public class TaskProgressInfo
    {
        /// <summary>
        /// 已用时间
        /// </summary>
        public TimeSpan ElapsedTime { get; set; }

        /// <summary>
        /// 任务状态
        /// </summary>
        public System.Threading.Tasks.TaskStatus Status { get; set; }

        /// <summary>
        /// 是否完成
        /// </summary>
        public bool IsCompleted { get; set; }

        /// <summary>
        /// 是否取消
        /// </summary>
        public bool IsCanceled { get; set; }

        /// <summary>
        /// 是否出错
        /// </summary>
        public bool IsFaulted { get; set; }
    }

    /// <summary>
    /// 优先级任务
    /// </summary>
    public class PriorityTask
    {
        /// <summary>
        /// 优先级（数值越大优先级越高）
        /// </summary>
        public int Priority { get; set; }

        /// <summary>
        /// 任务工厂函数
        /// </summary>
        public Func<Task> TaskFactory { get; set; }
    }

    /// <summary>
    /// 熔断器
    /// </summary>
    public class CircuitBreaker
    {
        private readonly int _failureThreshold;
        private readonly TimeSpan _retryTimeout;
        private int _failureCount;
        private DateTime _lastFailureTime;
        private CircuitState _state;

        /// <summary>
        /// 熔断器状态枚举
        /// </summary>
        public enum CircuitState
        {
            /// <summary>
            /// 关闭状态
            /// </summary>
            Closed,

            /// <summary>
            /// 开启状态
            /// </summary>
            Open,

            /// <summary>
            /// 半开启状态
            /// </summary>
            HalfOpen
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="failureThreshold">失败阈值</param>
        /// <param name="retryTimeout">重试超时时间</param>
        public CircuitBreaker(int failureThreshold, TimeSpan retryTimeout)
        {
            _failureThreshold = failureThreshold;
            _retryTimeout = retryTimeout;
            _state = CircuitState.Closed;
        }

        /// <summary>
        /// 执行任务
        /// </summary>
        /// <param name="taskFactory">任务工厂函数</param>
        /// <returns>Task实例</returns>
        public async Task ExecuteAsync(Func<Task> taskFactory)
        {
            if (_state == CircuitState.Open)
            {
                if (DateTime.UtcNow - _lastFailureTime < _retryTimeout)
                {
                    throw new CircuitBreakerOpenException("熔断器处于开启状态");
                }
                _state = CircuitState.HalfOpen;
            }

            try
            {
                await taskFactory();
                OnSuccess();
            }
            catch
            {
                OnFailure();
                throw;
            }
        }

        /// <summary>
        /// 执行任务（泛型版本）
        /// </summary>
        /// <typeparam name="T">返回值类型</typeparam>
        /// <param name="taskFactory">任务工厂函数</param>
        /// <returns>Task实例</returns>
        public async Task<T> ExecuteAsync<T>(Func<Task<T>> taskFactory)
        {
            if (_state == CircuitState.Open)
            {
                if (DateTime.UtcNow - _lastFailureTime < _retryTimeout)
                {
                    throw new CircuitBreakerOpenException("熔断器处于开启状态");
                }
                _state = CircuitState.HalfOpen;
            }

            try
            {
                var result = await taskFactory();
                OnSuccess();
                return result;
            }
            catch
            {
                OnFailure();
                throw;
            }
        }

        private void OnSuccess()
        {
            _failureCount = 0;
            _state = CircuitState.Closed;
        }

        private void OnFailure()
        {
            _failureCount++;
            _lastFailureTime = DateTime.UtcNow;

            if (_failureCount >= _failureThreshold)
            {
                _state = CircuitState.Open;
            }
        }
    }

    /// <summary>
    /// 熔断器开启异常
    /// </summary>
    public class CircuitBreakerOpenException : Exception
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="message">异常消息</param>
        public CircuitBreakerOpenException(string message) : base(message)
        {
        }
    }

    /// <summary>
    /// 任务池
    /// </summary>
    public class TaskPool
    {
        private readonly SemaphoreSlim _semaphore;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="maxConcurrency">最大并发数</param>
        public TaskPool(int maxConcurrency)
        {
            _semaphore = new SemaphoreSlim(maxConcurrency, maxConcurrency);
        }

        /// <summary>
        /// 执行任务
        /// </summary>
        /// <param name="taskFactory">任务工厂函数</param>
        /// <returns>Task实例</returns>
        public async Task ExecuteAsync(Func<Task> taskFactory)
        {
            await _semaphore.WaitAsync();
            try
            {
                await taskFactory();
            }
            finally
            {
                _semaphore.Release();
            }
        }

        /// <summary>
        /// 执行任务（泛型版本）
        /// </summary>
        /// <typeparam name="T">返回值类型</typeparam>
        /// <param name="taskFactory">任务工厂函数</param>
        /// <returns>Task实例</returns>
        public async Task<T> ExecuteAsync<T>(Func<Task<T>> taskFactory)
        {
            await _semaphore.WaitAsync();
            try
            {
                return await taskFactory();
            }
            finally
            {
                _semaphore.Release();
            }
        }
    }

    #endregion
}