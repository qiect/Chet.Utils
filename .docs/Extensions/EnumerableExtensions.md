# EnumerableExtensions 类功能文档

## 概述

[EnumerableExtensions](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\EnumerableExtensions.cs#L8-L395) 是一个静态扩展类，为 `IEnumerable<T>` 和 `ICollection<T>` 类型提供了丰富的扩展方法。该类包含集合判断、转换、操作、统计等多种功能，旨在简化集合操作，提高代码的安全性和可读性，特别适用于处理各种集合类型的日常开发场景。

## 主要功能模块

### 1. IEnumerable 扩展方法

提供针对可枚举集合的各种安全操作方法。

#### 状态判断方法
- [IsNullOrEmpty<T>()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\EnumerableExtensions.cs#L16-L17) - 判断集合是否为 null 或空
- [IsNotEmpty<T>()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\EnumerableExtensions.cs#L23-L24) - 判断集合是否不为空

#### 安全操作方法
- [SafeCount<T>()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\EnumerableExtensions.cs#L30-L31) - 获取集合元素数量（安全）
- [FirstOrDefaultSafe<T>()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\EnumerableExtensions.cs#L37-L38) - 获取集合的第一个元素，若为空则返回默认值
- [LastOrDefaultSafe<T>()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\EnumerableExtensions.cs#L44-L45) - 获取集合的最后一个元素，若为空则返回默认值
- [ContainsSafe<T>()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\EnumerableExtensions.cs#L52-L53) - 判断集合是否包含指定元素（支持 null 集合）

#### 转换方法
- [ToListSafe<T>()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\EnumerableExtensions.cs#L59-L60) - 将集合转换为 List（安全）
- [ToArraySafe<T>()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\EnumerableExtensions.cs#L66-L67) - 将集合转换为数组（安全）
- [ToDictionarySafe<TSource, TKey, TValue>()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\EnumerableExtensions.cs#L223-L230) - 集合转为字典（安全）
- [ToHashSetSafe<T>()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\EnumerableExtensions.cs#L236-L237) - 集合转为 HashSet（安全）
- [ToDataTable<T>()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\EnumerableExtensions.cs#L316-L331) - IEnumerable 转为 DataTable
- [ToConcurrentDictionary<TKey, TValue>()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\EnumerableExtensions.cs#L337-L345) - IEnumerable<KeyValuePair> 转为 ConcurrentDictionary
- [ToConcurrentDictionary<TSource, TKey, TValue>()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\EnumerableExtensions.cs#L353-L362) - IEnumerable 转为 ConcurrentDictionary（自定义键值选择器）

#### 集合操作方法
- [DistinctSafe<T>()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\EnumerableExtensions.cs#L73-L74) - 集合去重（安全）
- [WhereSafe<T>()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\EnumerableExtensions.cs#L80-L81) - 集合过滤（安全）
- [SelectSafe<TSource, TResult>()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\EnumerableExtensions.cs#L87-L88) - 集合投影（安全）
- [GroupBySafe<TSource, TKey>()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\EnumerableExtensions.cs#L94-L95) - 集合分组（安全）
- [OrderBySafe<TSource, TKey>()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\EnumerableExtensions.cs#L101-L102) - 集合排序（安全）
- [OrderByDescendingSafe<TSource, TKey>()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\EnumerableExtensions.cs#L108-L109) - 集合逆序排序（安全）
- [Page<T>()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\EnumerableExtensions.cs#L116-L120) - 集合分页（安全）
- [RemoveNulls<T>()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\EnumerableExtensions.cs#L217-L218) - 集合去除 null 元素
- [Chunk<T>()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\EnumerableExtensions.cs#L252-L267) - 集合分块（按指定大小分组）
- [ReverseSafe<T>()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\EnumerableExtensions.cs#L310-L311) - 集合反转（安全）

#### 统计方法
- [SumSafe()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\EnumerableExtensions.cs#L125-L128) - 集合求和（安全）- 支持 int、double、decimal、float
- [AverageSafe()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\EnumerableExtensions.cs#L133-L136) - 集合平均值（安全）- 支持 int、double、decimal、float
- [MaxSafe()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\EnumerableExtensions.cs#L141-L144) - 集合最大值（安全）- 支持 int、double、decimal、float
- [MinSafe()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\EnumerableExtensions.cs#L149-L152) - 集合最小值（安全）- 支持 int、double、decimal、float

#### 遍历方法
- [ForEachSafe<T>()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\EnumerableExtensions.cs#L243-L247) - 集合遍历执行操作（安全）

#### 条件判断方法
- [AllSafe<T>()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\EnumerableExtensions.cs#L273-L274) - 集合是否全部满足条件（安全）
- [AnySafe<T>()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\EnumerableExtensions.cs#L280-L281) - 集合是否有任意元素满足条件（安全）

### 2. ICollection 扩展方法

提供针对集合类型的安全操作方法。

#### 状态判断方法
- [IsNullOrEmpty<T>()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\EnumerableExtensions.cs#L371-L372) - 判断集合是否为 null 或空

#### 元素操作方法
- [AddSafe<T>()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\EnumerableExtensions.cs#L379-L382) - 安全添加元素到集合
- [RemoveSafe<T>()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\EnumerableExtensions.cs#L388-L391) - 安全移除元素
- [ClearSafe<T>()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\EnumerableExtensions.cs#L397-L400) - 清空集合（安全）
- [AddRangeSafe<T>()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\EnumerableExtensions.cs#L407-L411) - 集合批量添加元素（安全）
- [RemoveRangeSafe<T>()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\EnumerableExtensions.cs#L417-L421) - 集合批量移除元素（安全）

## 使用场景

1. **数据处理** - 安全地处理各种集合数据，避免 null 异常
2. **Web API** - 处理请求参数和响应数据的集合操作
3. **数据转换** - 集合与各种数据结构（DataTable、Dictionary、ConcurrentDictionary）之间的转换
4. **分页处理** - 对大数据集合进行分页操作
5. **统计分析** - 对集合数据进行求和、平均值、最值等统计计算
6. **业务逻辑** - 集合过滤、排序、分组等业务处理
7. **并发处理** - 转换为线程安全的 ConcurrentDictionary
8. **数据清洗** - 去重、去除 null 值等数据清理操作

## 注意事项

1. 所有方法都是扩展方法，需要通过集合实例调用
2. 带有 "Safe" 后缀的方法都对 null 值进行了安全处理，避免抛出异常
3. 大部分方法在输入为 null 时会返回空集合或默认值，而不是抛出异常
4. [Page<T>()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\EnumerableExtensions.cs#L116-L120) 方法使用 0 作为起始页索引
5. [ToDataTable<T>()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\EnumerableExtensions.cs#L316-L331) 方法基于反射实现，性能可能不如手动映射
6. [Chunk<T>()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\EnumerableExtensions.cs#L252-L267) 方法使用延迟执行，内存效率较高
7. 统计方法在集合为空时会返回默认值（通常是 0），而不是抛出异常
8. 转换方法在委托参数为 null 时会使用默认委托，提高容错性
9. 集合操作方法对输入参数进行了完整性检查，提高代码健壮性