using Xunit;

namespace Chet.Utils.Helpers.Tests;

public class TaskHelperTests
{
    #region 任务创建测试

    [Fact]
    public async Task Run_ExecutesAction()
    {
        var executed = false;

        await TaskHelper.Run(() => executed = true);

        Assert.True(executed);
    }

    [Fact]
    public async Task Run_Generic_ReturnsValue()
    {
        var result = await TaskHelper.Run(() => 42);

        Assert.Equal(42, result);
    }

    [Fact]
    public async Task RunAsync_ExecutesAsyncAction()
    {
        var executed = false;

        await TaskHelper.RunAsync(async () =>
        {
            await Task.Delay(1);
            executed = true;
        });

        Assert.True(executed);
    }

    [Fact]
    public async Task RunAsync_Generic_ReturnsValue()
    {
        var result = await TaskHelper.RunAsync(async () =>
        {
            await Task.Delay(1);
            return 42;
        });

        Assert.Equal(42, result);
    }

    [Fact]
    public async Task Delay_DelaysExecution()
    {
        var sw = System.Diagnostics.Stopwatch.StartNew();

        await TaskHelper.Delay(50);

        sw.Stop();
        Assert.True(sw.ElapsedMilliseconds >= 40);
    }

    [Fact]
    public async Task Delay_WithTimeSpan_DelaysExecution()
    {
        var sw = System.Diagnostics.Stopwatch.StartNew();

        await TaskHelper.Delay(TimeSpan.FromMilliseconds(50));

        sw.Stop();
        Assert.True(sw.ElapsedMilliseconds >= 40);
    }

    [Fact]
    public async Task CompletedTask_ReturnsCompletedTask()
    {
        var task = TaskHelper.CompletedTask();

        Assert.True(task.IsCompleted);
        await task;
    }

    [Fact]
    public async Task FromResult_ReturnsCompletedTaskWithValue()
    {
        var task = TaskHelper.FromResult(42);

        Assert.True(task.IsCompleted);
        Assert.Equal(42, await task);
    }

    [Fact]
    public async Task FromException_ReturnsFaultedTask()
    {
        var exception = new InvalidOperationException("Test");
        var task = TaskHelper.FromException(exception);

        Assert.True(task.IsFaulted);
        await Assert.ThrowsAsync<InvalidOperationException>(() => task);
    }

    [Fact]
    public async Task FromException_Generic_ReturnsFaultedTask()
    {
        var exception = new InvalidOperationException("Test");
        var task = TaskHelper.FromException<int>(exception);

        Assert.True(task.IsFaulted);
        await Assert.ThrowsAsync<InvalidOperationException>(() => task);
    }

    [Fact]
    public async Task FromCanceled_ReturnsCanceledTask()
    {
        var task = TaskHelper.FromCanceled();

        Assert.True(task.IsCanceled);
        await Assert.ThrowsAsync<TaskCanceledException>(() => task);
    }

    [Fact]
    public async Task FromCanceled_Generic_ReturnsCanceledTask()
    {
        var task = TaskHelper.FromCanceled<int>();

        Assert.True(task.IsCanceled);
        await Assert.ThrowsAsync<TaskCanceledException>(() => task);
    }

    [Fact]
    public async Task Run_NullAction_ThrowsArgumentNullException()
    {
        await Assert.ThrowsAsync<ArgumentNullException>(() => TaskHelper.Run(null!));
    }

    [Fact]
    public async Task Run_Generic_NullFunction_ThrowsArgumentNullException()
    {
        await Assert.ThrowsAsync<ArgumentNullException>(() => TaskHelper.Run<int>(null!));
    }

    #endregion

    #region 任务等待测试

    [Fact]
    public async Task WhenAll_AllTasksComplete()
    {
        var counter = 0;
        var tasks = new[]
        {
            TaskHelper.Run(() => counter++),
            TaskHelper.Run(() => counter++),
            TaskHelper.Run(() => counter++)
        };

        await TaskHelper.WhenAll(tasks);

        Assert.Equal(3, counter);
    }

    [Fact]
    public async Task WhenAll_Generic_ReturnsAllResults()
    {
        var tasks = new[]
        {
            TaskHelper.FromResult(1),
            TaskHelper.FromResult(2),
            TaskHelper.FromResult(3)
        };

        var results = await TaskHelper.WhenAll(tasks);

        Assert.Equal(3, results.Length);
        Assert.Contains(1, results);
        Assert.Contains(2, results);
        Assert.Contains(3, results);
    }

    [Fact]
    public async Task WhenAll_Generic_Enumerable_ReturnsAllResults()
    {
        var tasks = new List<Task<int>>
        {
            TaskHelper.FromResult(1),
            TaskHelper.FromResult(2),
            TaskHelper.FromResult(3)
        };

        var results = await TaskHelper.WhenAll(tasks);

        Assert.Equal(3, results.Length);
    }

    [Fact]
    public async Task WhenAny_ReturnsFirstCompleted()
    {
        var tasks = new[]
        {
            TaskHelper.Delay(100),
            TaskHelper.Delay(50),
            TaskHelper.Delay(200)
        };

        var completedTask = await TaskHelper.WhenAny(tasks);

        Assert.NotNull(completedTask);
        Assert.True(completedTask.IsCompleted);
    }

    [Fact]
    public async Task WhenAny_Generic_ReturnsFirstCompleted()
    {
        var tasks = new[]
        {
            TaskHelper.FromResult(1),
            TaskHelper.Delay(100).ContinueWith(_ => 2),
            TaskHelper.FromResult(3)
        };

        var completedTask = await TaskHelper.WhenAny(tasks);

        Assert.NotNull(completedTask);
        Assert.True(completedTask.IsCompleted);
    }

    [Fact]
    public void Wait_CompletesWithinTimeout()
    {
        var task = TaskHelper.Delay(50);

        var result = TaskHelper.Wait(task, TimeSpan.FromSeconds(1));

        Assert.True(result);
    }

    [Fact]
    public void Wait_ExceedsTimeout_ReturnsFalse()
    {
        var task = TaskHelper.Delay(1000);

        var result = TaskHelper.Wait(task, TimeSpan.FromMilliseconds(50));

        Assert.False(result);
    }

    [Fact]
    public void WaitAll_AllTasksComplete()
    {
        var counter = 0;
        var tasks = new[]
        {
            Task.Run(() => counter++),
            Task.Run(() => counter++),
            Task.Run(() => counter++)
        };

        var result = TaskHelper.WaitAll(tasks, TimeSpan.FromSeconds(1));

        Assert.True(result);
        Assert.Equal(3, counter);
    }

    [Fact]
    public void WaitAny_ReturnsIndex()
    {
        var tasks = new[]
        {
            TaskHelper.Delay(100),
            TaskHelper.Delay(50),
            TaskHelper.Delay(200)
        };

        var index = TaskHelper.WaitAny(tasks, TimeSpan.FromSeconds(1));

        Assert.True(index >= 0);
    }

    #endregion

    #region 超时控制测试

    [Fact]
    public async Task WithTimeout_CompletesWithinTimeout()
    {
        var task = TaskHelper.Delay(50);

        await TaskHelper.WithTimeout(task, TimeSpan.FromSeconds(1));
    }

    [Fact]
    public async Task WithTimeout_ExceedsTimeout_ThrowsTimeoutException()
    {
        var task = TaskHelper.Delay(1000);

        await Assert.ThrowsAsync<TimeoutException>(() => TaskHelper.WithTimeout(task, TimeSpan.FromMilliseconds(50)));
    }

    [Fact]
    public async Task WithTimeout_Generic_ReturnsValue()
    {
        var task = TaskHelper.FromResult(42);

        var result = await TaskHelper.WithTimeout(task, TimeSpan.FromSeconds(1));

        Assert.Equal(42, result);
    }

    [Fact]
    public async Task WithTimeout_WithCancellationToken_CompletesWithinTimeout()
    {
        using var cts = new CancellationTokenSource();

        await TaskHelper.WithTimeout(
            ct => Task.Delay(50, ct),
            TimeSpan.FromSeconds(1),
            cts.Token);
    }

    [Fact]
    public async Task WithTimeout_WithCancellationToken_ExceedsTimeout_ThrowsTimeoutException()
    {
        using var cts = new CancellationTokenSource();

        await Assert.ThrowsAsync<TimeoutException>(() =>
            TaskHelper.WithTimeout(
                ct => Task.Delay(1000, ct),
                TimeSpan.FromMilliseconds(50),
                cts.Token));
    }

    [Fact]
    public async Task WithTimeout_WithCancellationToken_Generic_ReturnsValue()
    {
        using var cts = new CancellationTokenSource();

        var result = await TaskHelper.WithTimeout(
            ct => Task.FromResult(42),
            TimeSpan.FromSeconds(1),
            cts.Token);

        Assert.Equal(42, result);
    }

    [Fact]
    public async Task TryWithTimeout_CompletesWithinTimeout_ReturnsTrue()
    {
        var task = TaskHelper.Delay(50);

        var result = await TaskHelper.TryWithTimeout(task, TimeSpan.FromSeconds(1));

        Assert.True(result);
    }

    [Fact]
    public async Task TryWithTimeout_ExceedsTimeout_ReturnsFalse()
    {
        var task = TaskHelper.Delay(1000);

        var result = await TaskHelper.TryWithTimeout(task, TimeSpan.FromMilliseconds(50));

        Assert.False(result);
    }

    [Fact]
    public async Task TryWithTimeout_Generic_CompletesWithinTimeout_ReturnsSuccessAndResult()
    {
        var task = TaskHelper.FromResult(42);

        var (success, result) = await TaskHelper.TryWithTimeout(task, TimeSpan.FromSeconds(1));

        Assert.True(success);
        Assert.Equal(42, result);
    }

    [Fact]
    public async Task TryWithTimeout_Generic_ExceedsTimeout_ReturnsFalse()
    {
        var task = TaskHelper.Delay(1000).ContinueWith(_ => 42);

        var (success, result) = await TaskHelper.TryWithTimeout(task, TimeSpan.FromMilliseconds(50));

        Assert.False(success);
    }

    #endregion

    #region 重试机制测试

    [Fact]
    public async Task RetryAsync_SucceedsOnFirstTry()
    {
        var attempts = 0;

        await TaskHelper.RetryAsync(async () =>
        {
            attempts++;
            await Task.CompletedTask;
        }, 3, TimeSpan.FromMilliseconds(10));

        Assert.Equal(1, attempts);
    }

    [Fact]
    public async Task RetryAsync_RetriesOnFailure()
    {
        var attempts = 0;

        await TaskHelper.RetryAsync(async () =>
        {
            attempts++;
            if (attempts < 3)
                throw new Exception("Test");
            await Task.CompletedTask;
        }, 5, TimeSpan.FromMilliseconds(10));

        Assert.Equal(3, attempts);
    }

    [Fact]
    public async Task RetryAsync_Generic_ReturnsValue()
    {
        var attempts = 0;

        var result = await TaskHelper.RetryAsync(async () =>
        {
            attempts++;
            if (attempts < 2)
                throw new Exception("Test");
            return await Task.FromResult(42);
        }, 5, TimeSpan.FromMilliseconds(10));

        Assert.Equal(42, result);
        Assert.Equal(2, attempts);
    }

    [Fact]
    public async Task RetryAsync_ExceedsMaxRetries_ThrowsAggregateException()
    {
        await Assert.ThrowsAsync<AggregateException>(() =>
            TaskHelper.RetryAsync(async () =>
            {
                throw new Exception("Test");
            }, 2, TimeSpan.FromMilliseconds(10)));
    }

    [Fact]
    public async Task RetryWithExponentialBackoffAsync_SucceedsEventually()
    {
        var attempts = 0;

        await TaskHelper.RetryWithExponentialBackoffAsync(async () =>
        {
            attempts++;
            if (attempts < 3)
                throw new Exception("Test");
            await Task.CompletedTask;
        }, 5, TimeSpan.FromMilliseconds(10));

        Assert.Equal(3, attempts);
    }

    [Fact]
    public async Task RetryWithExponentialBackoffAsync_Generic_ReturnsValue()
    {
        var attempts = 0;

        var result = await TaskHelper.RetryWithExponentialBackoffAsync(async () =>
        {
            attempts++;
            if (attempts < 2)
                throw new Exception("Test");
            return 42;
        }, 5, TimeSpan.FromMilliseconds(10));

        Assert.Equal(42, result);
    }

    [Fact]
    public async Task RetryWhenAsync_RetriesOnlyWhenConditionMet()
    {
        var attempts = 0;

        await TaskHelper.RetryWhenAsync(
            async () =>
            {
                attempts++;
                if (attempts < 3)
                    throw new InvalidOperationException("Test");
                await Task.CompletedTask;
            },
            (ex, i) => ex is InvalidOperationException,
            5,
            TimeSpan.FromMilliseconds(10));

        Assert.Equal(3, attempts);
    }

    [Fact]
    public async Task RetryWhenAsync_Generic_ReturnsValue()
    {
        var attempts = 0;

        var result = await TaskHelper.RetryWhenAsync(
            async () =>
            {
                attempts++;
                if (attempts < 2)
                    throw new InvalidOperationException("Test");
                return 42;
            },
            (ex, i) => ex is InvalidOperationException,
            5,
            TimeSpan.FromMilliseconds(10));

        Assert.Equal(42, result);
    }

    #endregion

    #region 断路器测试

    [Fact]
    public void CreateCircuitBreaker_CreatesInstance()
    {
        var circuitBreaker = TaskHelper.CreateCircuitBreaker(
            failureThreshold: 5,
            successThreshold: 3,
            resetTimeout: TimeSpan.FromSeconds(30));

        Assert.NotNull(circuitBreaker);
        Assert.Equal(CircuitBreakerState.Closed, circuitBreaker.State);
    }

    [Fact]
    public async Task ExecuteWithCircuitBreakerAsync_ExecutesTask()
    {
        var circuitBreaker = TaskHelper.CreateCircuitBreaker();
        var executed = false;

        await TaskHelper.ExecuteWithCircuitBreakerAsync(circuitBreaker, async () =>
        {
            executed = true;
            await Task.CompletedTask;
        });

        Assert.True(executed);
    }

    [Fact]
    public async Task ExecuteWithCircuitBreakerAsync_Generic_ReturnsValue()
    {
        var circuitBreaker = TaskHelper.CreateCircuitBreaker();

        var result = await TaskHelper.ExecuteWithCircuitBreakerAsync(circuitBreaker, async () =>
        {
            return await Task.FromResult(42);
        });

        Assert.Equal(42, result);
    }

    [Fact]
    public async Task CircuitBreaker_OpensAfterFailures()
    {
        var circuitBreaker = new CircuitBreaker(
            failureThreshold: 2,
            successThreshold: 1,
            resetTimeout: TimeSpan.FromMilliseconds(100));

        for (int i = 0; i < 2; i++)
        {
            try
            {
                await circuitBreaker.ExecuteAsync(() => throw new Exception("Test"));
            }
            catch
            {
            }
        }

        Assert.Equal(CircuitBreakerState.Open, circuitBreaker.State);
    }

    [Fact]
    public async Task CircuitBreaker_RejectsWhenOpen()
    {
        var circuitBreaker = new CircuitBreaker(
            failureThreshold: 1,
            successThreshold: 1,
            resetTimeout: TimeSpan.FromSeconds(30));

        try
        {
            await circuitBreaker.ExecuteAsync(() => throw new Exception("Test"));
        }
        catch
        {
        }

        await Assert.ThrowsAsync<CircuitBreakerOpenException>(() =>
            circuitBreaker.ExecuteAsync(() => Task.CompletedTask));
    }

    [Fact]
    public async Task CircuitBreaker_TransitionsToHalfOpenAfterTimeout()
    {
        var circuitBreaker = new CircuitBreaker(
            failureThreshold: 1,
            successThreshold: 1,
            resetTimeout: TimeSpan.FromMilliseconds(50));

        try
        {
            await circuitBreaker.ExecuteAsync(() => throw new Exception("Test"));
        }
        catch
        {
        }

        await Task.Delay(100);

        Assert.Equal(CircuitBreakerState.HalfOpen, circuitBreaker.State);
    }

    [Fact]
    public async Task CircuitBreaker_ResetsToClosedAfterSuccesses()
    {
        var circuitBreaker = new CircuitBreaker(
            failureThreshold: 1,
            successThreshold: 2,
            resetTimeout: TimeSpan.FromMilliseconds(50));

        try
        {
            await circuitBreaker.ExecuteAsync(() => throw new Exception("Test"));
        }
        catch
        {
        }

        await Task.Delay(100);

        await circuitBreaker.ExecuteAsync(() => Task.CompletedTask);
        await circuitBreaker.ExecuteAsync(() => Task.CompletedTask);

        Assert.Equal(CircuitBreakerState.Closed, circuitBreaker.State);
    }

    [Fact]
    public void CircuitBreaker_Reset_ResetsToClosed()
    {
        var circuitBreaker = new CircuitBreaker(
            failureThreshold: 1,
            successThreshold: 1,
            resetTimeout: TimeSpan.FromSeconds(30));

        try
        {
            circuitBreaker.ExecuteAsync(() => throw new Exception("Test")).Wait();
        }
        catch
        {
        }

        circuitBreaker.Reset();

        Assert.Equal(CircuitBreakerState.Closed, circuitBreaker.State);
    }

    #endregion

    #region 并行处理测试

    [Fact]
    public async Task ParallelForEachAsync_ExecutesAllActions()
    {
        var counter = 0;
        var actions = new List<Func<Task>>
        {
            () => { counter++; return Task.CompletedTask; },
            () => { counter++; return Task.CompletedTask; },
            () => { counter++; return Task.CompletedTask; }
        };

        await TaskHelper.ParallelForEachAsync(actions);

        Assert.Equal(3, counter);
    }

    [Fact]
    public async Task ParallelForEachAsync_WithMaxDegree_LimitsConcurrency()
    {
        var concurrentCount = 0;
        var maxConcurrent = 0;
        var lockObj = new object();
        var actions = Enumerable.Range(0, 10).Select(_ => new Func<Task>(async () =>
        {
            lock (lockObj)
            {
                concurrentCount++;
                maxConcurrent = Math.Max(maxConcurrent, concurrentCount);
            }
            await Task.Delay(50);
            lock (lockObj)
            {
                concurrentCount--;
            }
        })).ToList();

        await TaskHelper.ParallelForEachAsync(actions, maxDegreeOfParallelism: 2);

        Assert.True(maxConcurrent <= 2);
    }

    [Fact]
    public async Task ParallelForEachAsync_WithDataSource_ProcessesAllItems()
    {
        var processed = new List<int>();
        var data = Enumerable.Range(1, 5);

        await TaskHelper.ParallelForEachAsync(data, async item =>
        {
            lock (processed)
            {
                processed.Add(item);
            }
            await Task.Delay(1);
        });

        Assert.Equal(5, processed.Count);
        Assert.Equal(new HashSet<int> { 1, 2, 3, 4, 5 }, new HashSet<int>(processed));
    }

    [Fact]
    public async Task ParallelForEachAsync_WithIndex_ProcessesAllItems()
    {
        var processed = new List<(int Item, int Index)>();
        var data = new[] { "a", "b", "c" };

        await TaskHelper.ParallelForEachAsync(data, async (item, index) =>
        {
            lock (processed)
            {
                processed.Add((item[0], index));
            }
            await Task.Delay(1);
        });

        Assert.Equal(3, processed.Count);
    }

    [Fact]
    public async Task ParallelSelectAsync_ReturnsOrderedResults()
    {
        var data = Enumerable.Range(1, 5);

        var results = await TaskHelper.ParallelSelectAsync(data, async item =>
        {
            await Task.Delay(new Random().Next(1, 10));
            return item * 2;
        });

        Assert.Equal(5, results.Count);
        Assert.Equal(new List<int> { 2, 4, 6, 8, 10 }, results);
    }

    [Fact]
    public async Task ParallelSelectAsync_WithIndex_ReturnsOrderedResults()
    {
        var data = new[] { 10, 20, 30 };

        var results = await TaskHelper.ParallelSelectAsync(data, async (item, index) =>
        {
            await Task.Delay(1);
            return item + index;
        });

        Assert.Equal(new List<int> { 10, 21, 32 }, results);
    }

    [Fact]
    public async Task ParallelFirstOrDefaultAsync_ReturnsFirstMatch()
    {
        var data = Enumerable.Range(1, 10);

        var result = await TaskHelper.ParallelFirstOrDefaultAsync(data, async item =>
        {
            await Task.Delay(1);
            return item > 5;
        });

        Assert.True(result > 5);
    }

    #endregion

    #region 任务限流测试

    [Fact]
    public async Task ThrottleAsync_DelaysBeforeExecution()
    {
        var executed = false;
        var sw = System.Diagnostics.Stopwatch.StartNew();

        await TaskHelper.ThrottleAsync(() =>
        {
            executed = true;
            return Task.CompletedTask;
        }, TimeSpan.FromMilliseconds(50));

        sw.Stop();
        Assert.True(executed);
        Assert.True(sw.ElapsedMilliseconds >= 40);
    }

    [Fact]
    public async Task DebounceAsync_DelaysBeforeExecution()
    {
        var executed = false;
        var sw = System.Diagnostics.Stopwatch.StartNew();

        await TaskHelper.DebounceAsync(() =>
        {
            executed = true;
            return Task.CompletedTask;
        }, TimeSpan.FromMilliseconds(50));

        sw.Stop();
        Assert.True(executed);
        Assert.True(sw.ElapsedMilliseconds >= 40);
    }

    [Fact]
    public async Task BatchProcessAsync_ProcessesInBatches()
    {
        var data = Enumerable.Range(1, 10);
        var processedBatches = new List<List<int>>();

        await TaskHelper.BatchProcessAsync(data, 3, TimeSpan.FromMilliseconds(10), batch =>
        {
            processedBatches.Add(new List<int>(batch));
            return Task.CompletedTask;
        });

        Assert.Equal(4, processedBatches.Count);
        Assert.Equal(3, processedBatches[0].Count);
        Assert.Equal(3, processedBatches[1].Count);
        Assert.Equal(3, processedBatches[2].Count);
        Assert.Equal(1, processedBatches[3].Count);
    }

    [Fact]
    public async Task WithSemaphoreAsync_LimitsConcurrency()
    {
        using var semaphore = new SemaphoreSlim(2);
        var concurrentCount = 0;
        var maxConcurrent = 0;
        var lockObj = new object();

        var tasks = Enumerable.Range(0, 5).Select(async _ =>
        {
            await TaskHelper.WithSemaphoreAsync(semaphore, async () =>
            {
                lock (lockObj)
                {
                    concurrentCount++;
                    maxConcurrent = Math.Max(maxConcurrent, concurrentCount);
                }
                await Task.Delay(50);
                lock (lockObj)
                {
                    concurrentCount--;
                }
            });
        }).ToList();

        await Task.WhenAll(tasks);

        Assert.True(maxConcurrent <= 2);
    }

    [Fact]
    public async Task WithSemaphoreAsync_Generic_ReturnsValue()
    {
        using var semaphore = new SemaphoreSlim(1);

        var result = await TaskHelper.WithSemaphoreAsync(semaphore, async () =>
        {
            return await Task.FromResult(42);
        });

        Assert.Equal(42, result);
    }

    #endregion

    #region 任务链与管道测试

    [Fact]
    public async Task ExecuteChainAsync_ExecutesTasksInOrder()
    {
        var order = new List<int>();

        await TaskHelper.ExecuteChainAsync(new List<Func<Task>>
        {
            () => { order.Add(1); return Task.CompletedTask; },
            () => { order.Add(2); return Task.CompletedTask; },
            () => { order.Add(3); return Task.CompletedTask; }
        });

        Assert.Equal(new List<int> { 1, 2, 3 }, order);
    }

    [Fact]
    public async Task ExecuteChainAsync_WithState_PassesStateThrough()
    {
        var result = await TaskHelper.ExecuteChainAsync(
            1,
            new Func<int, Task<int>>[]
            {
                async x => { await Task.Delay(1); return x + 1; },
                async x => { await Task.Delay(1); return x * 2; },
                async x => { await Task.Delay(1); return x + 3; }
            });

        Assert.Equal(7, result);
    }

    [Fact]
    public async Task PipeAsync_TransformsData()
    {
        var input = Enumerable.Range(1, 5);

        var output = await TaskHelper.PipeAsync(input, async x =>
        {
            await Task.Delay(1);
            return x * 2;
        });

        Assert.Equal(new List<int> { 2, 4, 6, 8, 10 }, output);
    }

    #endregion

    #region 任务监控测试

    [Fact]
    public async Task MonitorAsync_ReturnsPerformanceInfo()
    {
        var task = TaskHelper.Delay(50);

        var info = await TaskHelper.MonitorAsync(task, "TestTask");

        Assert.Equal("TestTask", info.TaskName);
        Assert.Equal(TaskExecutionStatus.Completed, info.Status);
        Assert.True(info.Duration.TotalMilliseconds >= 40);
    }

    [Fact]
    public async Task MonitorAsync_Generic_ReturnsPerformanceInfo()
    {
        var task = TaskHelper.FromResult(42);

        var info = await TaskHelper.MonitorAsync(task, "TestTask");

        Assert.Equal("TestTask", info.TaskName);
        Assert.Equal(42, info.Result);
        Assert.Equal(TaskExecutionStatus.Completed, info.Status);
    }

    [Fact]
    public async Task MonitorAsync_FailedTask_ReturnsException()
    {
        var task = Task.FromException(new InvalidOperationException("Test"));

        var info = await TaskHelper.MonitorAsync(task, "TestTask");

        Assert.Equal(TaskExecutionStatus.Faulted, info.Status);
        Assert.NotNull(info.Exception);
        Assert.IsType<InvalidOperationException>(info.Exception);
    }

    [Fact]
    public async Task MonitorAsync_CanceledTask_ReturnsCanceled()
    {
        var task = TaskHelper.FromCanceled();

        var info = await TaskHelper.MonitorAsync(task, "TestTask");

        Assert.Equal(TaskExecutionStatus.Canceled, info.Status);
    }

    [Fact]
    public async Task MeasureAsync_ReturnsDuration()
    {
        var duration = await TaskHelper.MeasureAsync(async () =>
        {
            await Task.Delay(50);
        });

        Assert.True(duration.TotalMilliseconds >= 40);
    }

    [Fact]
    public async Task MeasureAsync_Generic_ReturnsDurationAndResult()
    {
        var (duration, result) = await TaskHelper.MeasureAsync(async () =>
        {
            await Task.Delay(50);
            return 42;
        });

        Assert.True(duration.TotalMilliseconds >= 40);
        Assert.Equal(42, result);
    }

    [Fact]
    public async Task TimeWithRetryAsync_Success_ReturnsSuccessResult()
    {
        var result = await TaskHelper.TimeWithRetryAsync(async () =>
        {
            await Task.Delay(10);
        }, maxRetries: 3);

        Assert.True(result.IsSuccess);
        Assert.Null(result.Exception);
    }

    [Fact]
    public async Task TimeWithRetryAsync_Failure_ReturnsFailureResult()
    {
        var result = await TaskHelper.TimeWithRetryAsync(async () =>
        {
            throw new Exception("Test");
        }, maxRetries: 2, delay: TimeSpan.FromMilliseconds(10));

        Assert.False(result.IsSuccess);
        Assert.NotNull(result.Exception);
    }

    #endregion

    #region 任务取消测试

    [Fact]
    public async Task WithCancellation_ExecutesWithToken()
    {
        var executed = false;

        await TaskHelper.WithCancellation(ct =>
        {
            Assert.NotNull(ct);
            executed = true;
        });

        Assert.True(executed);
    }

    [Fact]
    public async Task WithCancellationAsync_ExecutesWithToken()
    {
        var executed = false;

        await TaskHelper.WithCancellationAsync(async ct =>
        {
            Assert.NotNull(ct);
            await Task.Delay(1);
            executed = true;
        });

        Assert.True(executed);
    }

    [Fact]
    public async Task WithCancellation_Generic_ReturnsValue()
    {
        var result = await TaskHelper.WithCancellation(ct => 42);

        Assert.Equal(42, result);
    }

    [Fact]
    public async Task WithCancellationAsync_Generic_ReturnsValue()
    {
        var result = await TaskHelper.WithCancellationAsync(async ct =>
        {
            await Task.Delay(1);
            return 42;
        });

        Assert.Equal(42, result);
    }

    #endregion

    #region 任务结果处理测试

    [Fact]
    public async Task HandleResultAsync_Success_CallsOnSuccess()
    {
        var task = TaskHelper.FromResult(42);
        var onSuccessCalled = false;
        var onFailureCalled = false;

        await TaskHelper.HandleResultAsync(task,
            result => { onSuccessCalled = true; Assert.Equal(42, result); },
            _ => onFailureCalled = true);

        Assert.True(onSuccessCalled);
        Assert.False(onFailureCalled);
    }

    [Fact]
    public async Task HandleResultAsync_Failure_CallsOnFailure()
    {
        var task = Task.FromException<int>(new InvalidOperationException("Test"));
        var onSuccessCalled = false;
        var onFailureCalled = false;

        await Assert.ThrowsAsync<InvalidOperationException>(() =>
            TaskHelper.HandleResultAsync(task,
                _ => onSuccessCalled = true,
                ex => { onFailureCalled = true; Assert.IsType<InvalidOperationException>(ex); }));

        Assert.False(onSuccessCalled);
        Assert.True(onFailureCalled);
    }

    [Fact]
    public async Task GetResultOrDefaultAsync_Success_ReturnsResult()
    {
        var task = TaskHelper.FromResult(42);

        var result = await TaskHelper.GetResultOrDefaultAsync(task);

        Assert.Equal(42, result);
    }

    [Fact]
    public async Task GetResultOrDefaultAsync_Failure_ReturnsDefault()
    {
        var task = Task.FromException<int>(new InvalidOperationException("Test"));

        var result = await TaskHelper.GetResultOrDefaultAsync(task, 0);

        Assert.Equal(0, result);
    }

    [Fact]
    public async Task GetResultOrDefaultAsync_WithFactory_ReturnsDefault()
    {
        var task = Task.FromException<int>(new InvalidOperationException("Test"));

        var result = await TaskHelper.GetResultOrDefaultAsync(task, () => 99);

        Assert.Equal(99, result);
    }

    #endregion

    #region 任务异常处理测试

    [Fact]
    public async Task SafeExecuteAsync_Success_ReturnsNull()
    {
        var task = Task.CompletedTask;

        var exception = await TaskHelper.SafeExecuteAsync(task);

        Assert.Null(exception);
    }

    [Fact]
    public async Task SafeExecuteAsync_Failure_ReturnsException()
    {
        var task = Task.FromException(new InvalidOperationException("Test"));

        var exception = await TaskHelper.SafeExecuteAsync(task);

        Assert.NotNull(exception);
        Assert.IsType<InvalidOperationException>(exception);
    }

    [Fact]
    public async Task SafeExecuteAsync_Generic_Success_ReturnsResultAndNull()
    {
        var task = TaskHelper.FromResult(42);

        var (result, exception) = await TaskHelper.SafeExecuteAsync(task);

        Assert.Equal(42, result);
        Assert.Null(exception);
    }

    [Fact]
    public async Task SafeExecuteAsync_Generic_Failure_ReturnsDefaultAndException()
    {
        var task = Task.FromException<int>(new InvalidOperationException("Test"));

        var (result, exception) = await TaskHelper.SafeExecuteAsync(task, 0);

        Assert.Equal(0, result);
        Assert.NotNull(exception);
        Assert.IsType<InvalidOperationException>(exception);
    }

    [Fact]
    public async Task IgnoreExceptionAsync_SwallowsException()
    {
        var task = Task.FromException(new InvalidOperationException("Test"));

        await TaskHelper.IgnoreExceptionAsync(task);
    }

    [Fact]
    public async Task IgnoreExceptionAsync_Generic_SwallowsSpecificException()
    {
        var task = Task.FromException(new InvalidOperationException("Test"));

        await TaskHelper.IgnoreExceptionAsync<InvalidOperationException>(task);
    }

    [Fact]
    public async Task IgnoreExceptionAsync_Generic_DoesNotSwallowOtherExceptions()
    {
        var task = Task.FromException(new ArgumentException("Test"));

        await Assert.ThrowsAsync<ArgumentException>(() =>
            TaskHelper.IgnoreExceptionAsync<InvalidOperationException>(task));
    }

    #endregion

    #region 任务协调测试

    [Fact]
    public void CreateBarrier_CreatesBarrier()
    {
        var barrier = TaskHelper.CreateBarrier(3);

        Assert.NotNull(barrier);
        Assert.Equal(3, barrier.ParticipantCount);
    }

    [Fact]
    public void CreateBarrier_InvalidCount_ThrowsArgumentOutOfRangeException()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => TaskHelper.CreateBarrier(0));
        Assert.Throws<ArgumentOutOfRangeException>(() => TaskHelper.CreateBarrier(-1));
    }

    [Fact]
    public void CreateSemaphore_CreatesSemaphore()
    {
        var semaphore = TaskHelper.CreateSemaphore(2, 5);

        Assert.NotNull(semaphore);
    }

    [Fact]
    public void CreateSemaphore_InvalidParams_ThrowsArgumentOutOfRangeException()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => TaskHelper.CreateSemaphore(-1, 5));
        Assert.Throws<ArgumentOutOfRangeException>(() => TaskHelper.CreateSemaphore(2, 0));
        Assert.Throws<ArgumentOutOfRangeException>(() => TaskHelper.CreateSemaphore(10, 5));
    }

    [Fact]
    public void CreateManualResetEvent_CreatesEvent()
    {
        var resetEvent = TaskHelper.CreateManualResetEvent(false);

        Assert.NotNull(resetEvent);
        Assert.False(resetEvent.IsSet);
    }

    [Fact]
    public void CreateAutoResetEvent_CreatesEvent()
    {
        var resetEvent = TaskHelper.CreateAutoResetEvent(false);

        Assert.NotNull(resetEvent);
    }

    #endregion

    #region 模型类测试

    [Fact]
    public void TaskExecutionResult_Success_CreatesSuccessResult()
    {
        var result = TaskExecutionResult.Success(TimeSpan.FromSeconds(1));

        Assert.True(result.IsSuccess);
        Assert.Null(result.Exception);
        Assert.Equal(TimeSpan.FromSeconds(1), result.Duration);
    }

    [Fact]
    public void TaskExecutionResult_Failure_CreatesFailureResult()
    {
        var exception = new Exception("Test");
        var result = TaskExecutionResult.Failure(exception, TimeSpan.FromSeconds(1));

        Assert.False(result.IsSuccess);
        Assert.Equal(exception, result.Exception);
        Assert.Equal(TimeSpan.FromSeconds(1), result.Duration);
    }

    [Fact]
    public void TaskExecutionResult_Generic_Success_CreatesSuccessResult()
    {
        var result = TaskExecutionResult<int>.Success(42, TimeSpan.FromSeconds(1));

        Assert.True(result.IsSuccess);
        Assert.Equal(42, result.Result);
        Assert.Equal(TimeSpan.FromSeconds(1), result.Duration);
    }

    [Fact]
    public void TaskExecutionResult_Generic_Failure_CreatesFailureResult()
    {
        var exception = new Exception("Test");
        var result = TaskExecutionResult<int>.Failure(exception, TimeSpan.FromSeconds(1));

        Assert.False(result.IsSuccess);
        Assert.Equal(exception, result.Exception);
    }

    #endregion

    #region 任务队列测试

    [Fact]
    public async Task CreateTaskQueue_CreatesInstance()
    {
        using var queue = TaskHelper.CreateTaskQueue<string>(
            async item => await Task.CompletedTask,
            maxDegreeOfParallelism: 2);

        Assert.NotNull(queue);
    }

    [Fact]
    public async Task TaskQueue_EnqueueAsync_ProcessesItems()
    {
        var processed = new List<int>();
        using var queue = TaskHelper.CreateTaskQueue<int>(
            async item =>
            {
                lock (processed)
                {
                    processed.Add(item);
                }
                await Task.Delay(1);
            },
            maxDegreeOfParallelism: 2);

        for (int i = 0; i < 5; i++)
        {
            await queue.EnqueueAsync(i);
        }

        queue.Complete();
        await queue.CompletionAsync();

        Assert.Equal(5, processed.Count);
    }

    [Fact]
    public async Task TaskQueue_TracksProcessedCount()
    {
        using var queue = TaskHelper.CreateTaskQueue<int>(
            async item => await Task.Delay(1),
            maxDegreeOfParallelism: 2);

        for (int i = 0; i < 3; i++)
        {
            await queue.EnqueueAsync(i);
        }

        queue.Complete();
        await queue.CompletionAsync();

        Assert.Equal(3, queue.ProcessedCount);
    }

    [Fact]
    public async Task TaskQueue_TracksFailedCount()
    {
        using var queue = TaskHelper.CreateTaskQueue<int>(
            async item =>
            {
                if (item == 1)
                    throw new Exception("Test");
                await Task.CompletedTask;
            },
            maxDegreeOfParallelism: 1);

        await queue.EnqueueAsync(0);
        await queue.EnqueueAsync(1);
        await queue.EnqueueAsync(2);

        queue.Complete();
        await queue.CompletionAsync();

        Assert.Equal(1, queue.FailedCount);
    }

    [Fact]
    public void CreateTaskQueue_NullProcessor_ThrowsArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>(() =>
            TaskHelper.CreateTaskQueue<int>(null!, maxDegreeOfParallelism: 1));
    }

    [Fact]
    public void CreateTaskQueue_InvalidMaxDegree_ThrowsArgumentOutOfRangeException()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() =>
            TaskHelper.CreateTaskQueue<int>(async _ => { }, maxDegreeOfParallelism: 0));
    }

    [Fact]
    public async Task CreatePriorityTaskQueue_CreatesInstance()
    {
        using var queue = TaskHelper.CreatePriorityTaskQueue(
            async item => await Task.CompletedTask,
            Comparer<int>.Default,
            maxDegreeOfParallelism: 2);

        Assert.NotNull(queue);
    }

    [Fact]
    public async Task PriorityTaskQueue_ProcessesByPriority()
    {
        var processed = new List<int>();
        using var queue = TaskHelper.CreatePriorityTaskQueue<int>(
            async item =>
            {
                lock (processed)
                {
                    processed.Add(item);
                }
                await Task.Delay(10);
            },
            Comparer<int>.Default,
            maxDegreeOfParallelism: 1);

        queue.EnqueueAsync(3);
        queue.EnqueueAsync(1);
        queue.EnqueueAsync(2);

        queue.Complete();
        await queue.CompletionAsync();

        Assert.Equal(new List<int> { 1, 2, 3 }, processed);
    }

    [Fact]
    public async Task CreateProducerConsumerQueue_CreatesInstance()
    {
        using var queue = TaskHelper.CreateProducerConsumerQueue<int>(
            async item => await Task.CompletedTask,
            boundedCapacity: 10);

        Assert.NotNull(queue);
    }

    [Fact]
    public async Task ProducerConsumerQueue_WritesAndConsumes()
    {
        var consumed = new List<int>();
        using var queue = TaskHelper.CreateProducerConsumerQueue<int>(
            async item =>
            {
                lock (consumed)
                {
                    consumed.Add(item);
                }
                await Task.CompletedTask;
            },
            boundedCapacity: 10);

        for (int i = 0; i < 5; i++)
        {
            await queue.WriteAsync(i);
        }

        queue.Complete();
        await queue.Completion;

        Assert.Equal(5, consumed.Count);
    }

    #endregion

    #region 任务池测试

    [Fact]
    public void CreateTaskPool_CreatesInstance()
    {
        using var pool = TaskHelper.CreateTaskPool<int, string>(
            async item => await Task.FromResult(item.ToString()),
            maxDegreeOfParallelism: 5);

        Assert.NotNull(pool);
    }

    [Fact]
    public async Task TaskPool_ExecuteAsync_ReturnsResult()
    {
        using var pool = TaskHelper.CreateTaskPool<int, int>(
            async item => await Task.FromResult(item * 2),
            maxDegreeOfParallelism: 5);

        var result = await pool.ExecuteAsync(21);

        Assert.Equal(42, result);
    }

    [Fact]
    public async Task TaskPool_LimitsConcurrency()
    {
        var concurrentCount = 0;
        var maxConcurrent = 0;
        var lockObj = new object();

        using var pool = TaskHelper.CreateTaskPool<int, int>(
            async item =>
            {
                lock (lockObj)
                {
                    concurrentCount++;
                    maxConcurrent = Math.Max(maxConcurrent, concurrentCount);
                }
                await Task.Delay(50);
                lock (lockObj)
                {
                    concurrentCount--;
                }
                return item;
            },
            maxDegreeOfParallelism: 2);

        var tasks = Enumerable.Range(0, 5).Select(i => pool.ExecuteAsync(i)).ToArray();
        await Task.WhenAll(tasks);

        Assert.True(maxConcurrent <= 2);
    }

    [Fact]
    public void CreateTaskPool_NullProcessor_ThrowsArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>(() =>
            TaskHelper.CreateTaskPool<int, string>(null!, maxDegreeOfParallelism: 5));
    }

    #endregion

    #region 资源池测试

    [Fact]
    public void CreateResourcePool_CreatesInstance()
    {
        using var pool = TaskHelper.CreateResourcePool(
            () => new List<int>(),
            maxPoolSize: 5);

        Assert.NotNull(pool);
    }

    [Fact]
    public void ResourcePool_Rent_ReturnsNewInstance()
    {
        using var pool = TaskHelper.CreateResourcePool(
            () => new List<int>(),
            maxPoolSize: 5);

        var resource = pool.Rent();

        Assert.NotNull(resource);
        Assert.IsType<List<int>>(resource);
    }

    [Fact]
    public void ResourcePool_Return_ReusesInstance()
    {
        using var pool = TaskHelper.CreateResourcePool(
            () => new List<int>(),
            maxPoolSize: 5);

        var resource1 = pool.Rent();
        pool.Return(resource1);
        var resource2 = pool.Rent();

        Assert.Same(resource1, resource2);
    }

    [Fact]
    public async Task ResourcePool_RentAsync_ReturnsInstance()
    {
        using var pool = TaskHelper.CreateResourcePool(
            () => new List<int>(),
            maxPoolSize: 5);

        var resource = await pool.RentAsync();

        Assert.NotNull(resource);
    }

    [Fact]
    public void CreateResourcePool_NullFactory_ThrowsArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>(() =>
            TaskHelper.CreateResourcePool<List<int>>(null!, maxPoolSize: 5));
    }

    #endregion
}
