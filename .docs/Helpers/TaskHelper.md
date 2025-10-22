# TaskHelper 类功能文档

## 概述

[TaskHelper](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\TaskHelper.cs#L10-L1234) 是一个静态工具类，专门用于异步任务的管理、调度、监控和控制。该类提供了丰富的任务操作功能，包括任务创建、控制管理、调度并行处理、监控统计、组合操作以及高级任务模式等，旨在简化复杂的异步编程场景。

## 主要功能模块

### 1. 任务创建与启动

提供多种方式创建和启动异步任务。

**主要方法：**
- [Run()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\TaskHelper.cs#L21-L24) - 创建并启动一个异步任务
- [Run<T>()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\TaskHelper.cs#L34-L37) - 创建并启动一个带返回值的异步任务
- [RunAsync()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\TaskHelper.cs#L46-L49) - 创建并启动一个异步任务（异步委托版本）
- [RunAsync<T>()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\TaskHelper.cs#L60-L63) - 创建并启动一个带返回值的异步任务（异步委托版本）
- [Delay()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\TaskHelper.cs#L71-L74) - 创建一个延迟任务（TimeSpan版本）
- [Delay()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\TaskHelper.cs#L82-L85) - 创建一个延迟任务（毫秒版本）
- [CompletedTask()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\TaskHelper.cs#L91-L94) - 创建一个已完成的任务
- [FromResult<T>()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\TaskHelper.cs#L102-L105) - 创建一个已完成的任务（带返回值）
- [FromException()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\TaskHelper.cs#L112-L115) - 创建一个失败的任务
- [FromException<T>()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\TaskHelper.cs#L124-L127) - 创建一个失败的任务（带返回值类型）
- [FromCanceled()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\TaskHelper.cs#L133-L136) - 创建一个已取消的任务
- [FromCanceled<T>()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\TaskHelper.cs#L144-L147) - 创建一个已取消的任务（带返回值类型）

### 2. 任务控制与管理

提供任务的控制和管理功能，包括等待、超时、重试和熔断等。

**主要方法：**
- [WhenAll()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\TaskHelper.cs#L157-L160) - 等待所有任务完成
- [WhenAll<T>()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\TaskHelper.cs#L168-L171) - 等待所有任务完成（泛型版本）
- [WhenAny()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\TaskHelper.cs#L179-L182) - 等待任意一个任务完成
- [WhenAny<T>()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\TaskHelper.cs#L191-L194) - 等待任意一个任务完成（泛型版本）
- [WithTimeout()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\TaskHelper.cs#L202-L217) - 超时控制执行任务
- [WithTimeout<T>()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\TaskHelper.cs#L227-L243) - 超时控制执行任务（泛型版本）
- [RetryAsync()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\TaskHelper.cs#L253-L271) - 带重试机制的任务执行
- [RetryAsync<T>()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\TaskHelper.cs#L281-L303) - 带重试机制的任务执行（泛型版本）
- [CircuitBreakerAsync()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\TaskHelper.cs#L312-L316) - 任务熔断器模式实现
- [CircuitBreakerAsync<T>()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\TaskHelper.cs#L326-L331) - 任务熔断器模式实现（泛型版本）

### 3. 任务调度与并行处理

提供任务的调度和并行处理功能。

**主要方法：**
- [ParallelForEachAsync()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\TaskHelper.cs#L342-L374) - 并行执行多个任务
- [ParallelForEachAsync<T>()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\TaskHelper.cs#L386-L418) - 并行执行多个任务（带数据源）
- [ParallelSelectAsync<T, TResult>()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\TaskHelper.cs#L431-L476) - 有序并行执行任务
- [ScheduleTasksAsync()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\TaskHelper.cs#L486-L529) - 任务队列调度器

### 4. 任务监控与统计

提供任务执行的监控和统计功能。

**主要方法：**
- [MonitorTaskAsync()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\TaskHelper.cs#L540-L566) - 任务性能监控
- [MonitorTaskAsync<T>()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\TaskHelper.cs#L577-L606) - 任务性能监控（泛型版本）
- [MonitorBatchTasksAsync()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\TaskHelper.cs#L615-L643) - 批量任务性能监控
- [TrackTaskProgress()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\TaskHelper.cs#L653-L677) - 实时任务状态跟踪

### 5. 任务组合与链式操作

提供任务的组合和链式操作功能。

**主要方法：**
- [ChainTasksAsync()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\TaskHelper.cs#L686-L693) - 任务链式执行
- [ChainConditionalTasksAsync()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\TaskHelper.cs#L702-L713) - 带条件的任务链式执行
- [PipelineAsync<T>()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\TaskHelper.cs#L723-L732) - 任务流水线处理
- [ForkAsync()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\TaskHelper.cs#L741-L750) - 任务分叉执行
- [CombineAsync<T>()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\TaskHelper.cs#L761-L767) - 任务合并执行
- [RaceAsync<T>()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\TaskHelper.cs#L777-L784) - 任务竞争执行（只取第一个完成的）

### 6. 高级任务模式

提供高级任务模式的实现。

**主要方法：**
- [ProducerConsumerAsync<T>()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\TaskHelper.cs#L796-L806) - 生产者-消费者模式实现
- [CreateTaskPool()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\TaskHelper.cs#L814-L817) - 任务池管理
- [ThrottleAsync()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\TaskHelper.cs#L826-L857) - 任务节流执行
- [ExecutePriorityTasksAsync()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\TaskHelper.cs#L865-L873) - 任务优先级队列执行
- [StateMachineAsync()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\TaskHelper.cs#L884-L899) - 任务状态机模式

## 辅助类和数据结构

### 枚举类型
- [TaskStatus](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\TaskHelper.cs#L912-L932) - 任务状态枚举

### 数据结构类
- [TaskExecutionInfo](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\TaskHelper.cs#L937-L959) - 任务执行信息
- [TaskSchedulerResult](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\TaskHelper.cs#L964-L981) - 任务调度器结果
- [TaskPerformanceInfo](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\TaskHelper.cs#L986-L1013) - 任务性能信息
- [TaskPerformanceInfo<T>](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\TaskHelper.cs#L1019-L1029) - 任务性能信息（泛型版本）
- [BatchTaskPerformanceInfo](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\TaskHelper.cs#L1034-L1056) - 批量任务性能信息
- [TaskProgressInfo](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\TaskHelper.cs#L1061-L1083) - 任务进度信息
- [PriorityTask](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\TaskHelper.cs#L1088-L1100) - 优先级任务

### 工具类
- [CircuitBreaker](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\TaskHelper.cs#L1105-L1192) - 熔断器
- [CircuitBreakerOpenException](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\TaskHelper.cs#L1197-L1207) - 熔断器开启异常
- [TaskPool](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\TaskHelper.cs#L1212-L1233) - 任务池

## 使用场景

1. **异步编程简化** - 简化复杂的异步任务创建和管理
2. **性能监控** - 监控任务执行性能和状态
3. **容错处理** - 通过重试和熔断机制提高系统稳定性
4. **并发控制** - 控制任务并发执行数量
5. **任务调度** - 实现复杂任务的调度和执行
6. **数据处理管道** - 构建数据处理流水线
7. **生产者消费者模式** - 实现生产者消费者并发模式
8. **系统资源保护** - 通过节流等方式保护系统资源

## 注意事项

1. 大部分方法都支持取消令牌（CancellationToken）参数
2. 并行处理方法支持最大并发度控制
3. 超时控制方法会抛出TimeoutException异常
4. 重试机制支持取消操作
5. 熔断器模式有助于防止级联故障
6. 任务池可以有效控制资源使用
7. 生产者消费者模式基于System.Threading.Channels实现
8. 部分方法需要传入委托或Lambda表达式作为参数