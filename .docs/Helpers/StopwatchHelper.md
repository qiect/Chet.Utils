# StopwatchHelper 类功能文档

## 概述

[StopwatchHelper](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\StopwatchHelper.cs#L10-L908) 是一个静态工具类，专门用于性能测量和计时操作。该类提供了丰富的计时功能，包括基础计时、多次执行测量、高精度计时、条件计时与跟踪等，旨在简化性能测试和代码优化工作。

## 主要功能模块

### 1. 基础计时方法

提供简单的操作执行时间测量功能。

**主要方法：**
- [Time()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\StopwatchHelper.cs#L19-L26) - 执行操作并返回耗时（毫秒）
- [TimePrecise()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\StopwatchHelper.cs#L34-L41) - 执行操作并返回耗时（高精度时间戳）
- [TimeAsync()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\StopwatchHelper.cs#L49-L56) - 异步执行操作并返回耗时（毫秒）
- [TimePreciseAsync()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\StopwatchHelper.cs#L65-L72) - 异步执行操作并返回耗时（高精度时间戳）
- [StartNew()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\StopwatchHelper.cs#L79-L82) - 创建并启动一个新的计时器

### 2. 多次执行测量

提供多次执行操作的统计分析功能。

**主要方法：**
- [TimeAverage()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\StopwatchHelper.cs#L94-L105) - 多次执行操作并返回平均耗时
- [TimeStatistics()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\StopwatchHelper.cs#L115-L130) - 多次执行操作并返回详细统计信息
- [TimeAverageAsync()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\StopwatchHelper.cs#L139-L151) - 异步多次执行操作并返回平均耗时
- [TimeStatisticsAsync()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\StopwatchHelper.cs#L161-L176) - 异步多次执行操作并返回详细统计信息
- [TimeConcurrent()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\StopwatchHelper.cs#L185-L202) - 并发执行操作并测量总耗时
- [TimeConcurrentAsync()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\StopwatchHelper.cs#L211-L228) - 并发执行异步操作并测量总耗时
- [Compare()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\StopwatchHelper.cs#L238-L250) - 比较两个操作的性能
- [CompareAsync()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\StopwatchHelper.cs#L259-L273) - 异步比较两个操作的性能

### 3. 高精度计时

提供更高精度的时间测量功能。

**主要方法：**
- [GetTimestamp()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\StopwatchHelper.cs#L288-L291) - 获取高精度时间戳
- [TimestampToTimeSpan()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\StopwatchHelper.cs#L298-L301) - 将时间戳转换为时间间隔
- [GetFrequency()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\StopwatchHelper.cs#L307-L310) - 获取计时器频率
- [IsHighResolution()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\StopwatchHelper.cs#L317-L320) - 检查计时器是否基于高性能计数器
- [TimeHighPrecision()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\StopwatchHelper.cs#L328-L336) - 使用高精度计时器执行操作
- [TimeAverageHighPrecision()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\StopwatchHelper.cs#L345-L357) - 高精度多次执行操作并返回平均耗时
- [MeasureCpuCycles()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\StopwatchHelper.cs#L365-L373) - 测量操作的CPU周期数
- [CreateHighPrecisionTimer()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\StopwatchHelper.cs#L380-L383) - 创建高精度计时器
- [TimeWithCustomTimer()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\StopwatchHelper.cs#L391-L395) - 使用自定义计时器执行操作

### 4. 条件计时与跟踪

提供条件性计时和跟踪功能。

**主要方法：**
- [TimeIf()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\StopwatchHelper.cs#L407-L415) - 条件性执行计时
- [TimeAndTrace()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\StopwatchHelper.cs#L425-L433) - 条件性执行计时并输出到控制台
- [TimeAndLog()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\StopwatchHelper.cs#L443-L448) - 计时并记录到日志
- [CreateSegmentedStopwatch()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\StopwatchHelper.cs#L455-L458) - 创建分段计时器
- [TimeWithTimeout()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\StopwatchHelper.cs#L467-L475) - 执行操作并在超时时抛出异常
- [TimeWithTimeoutAsync()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\StopwatchHelper.cs#L485-L497) - 异步执行操作并在超时时抛出异常
- [TimeWithRetry()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\StopwatchHelper.cs#L507-L546) - 计时并重试操作直到成功或达到最大尝试次数
- [TimeWithMemoryTracking()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\StopwatchHelper.cs#L555-L570) - 测量操作的内存分配

## 辅助类和数据结构

### 数据结构类
- [TimingStatistics](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\StopwatchHelper.cs#L580-L629) - 计时统计信息
- [PerformanceComparison](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\StopwatchHelper.cs#L634-L678) - 性能比较结果
- [CustomTimingResult](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\StopwatchHelper.cs#L706-L728) - 自定义计时结果
- [TimeSegment](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\StopwatchHelper.cs#L785-L796) - 时间段
- [RetryResult](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\StopwatchHelper.cs#L855-L877) - 重试结果
- [MemoryTimingResult](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\StopwatchHelper.cs#L882-L893) - 内存计时结果

### 工具类
- [CustomStopwatch](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\StopwatchHelper.cs#L683-L729) - 自定义计时器
- [SegmentedStopwatch](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\StopwatchHelper.cs#L734-L797) - 分段计时器
- [HighPrecisionTimer](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\StopwatchHelper.cs#L802-L850) - 高精度计时器

## 使用场景

1. **性能测试** - 测量代码执行时间，识别性能瓶颈
2. **算法优化** - 比较不同算法的执行效率
3. **系统监控** - 监控关键操作的执行时间
4. **基准测试** - 进行代码性能基准测试
5. **调试诊断** - 诊断程序执行时间异常
6. **并发性能分析** - 分析并发操作的性能表现
7. **资源使用分析** - 分析操作的内存使用情况
8. **超时控制** - 为操作设置执行时间限制

## 注意事项

1. 部分方法需要传入委托或Lambda表达式作为参数
2. 多次执行测量方法需要指定合理的迭代次数
3. 高精度计时功能依赖于系统计时器的支持
4. 并发执行方法需要注意线程安全问题
5. 内存跟踪功能提供的是近似值，可能受GC影响
6. 超时控制方法在异步操作中更为有效
7. 重试机制适用于可能临时失败的操作
8. 分段计时器适用于分析复杂操作各阶段耗时