# EnumerableExtensions 扩展类文档

## 概述

[EnumerableExtensions](../../Chet.Utils/Extensions/EnumerableExtensions.cs) 是一个静态扩展类，为 `IEnumerable<T>` 和 `ICollection<T>` 类型提供了丰富的扩展方法，包括空值判断、安全操作、集合操作、分页排序、统计计算、集合转换、批量操作等功能，旨在简化集合处理，提高代码的安全性和可读性，特别是用于处理可能为空的集合类型的日常操作场景。

## 主要功能模块

### 1. IEnumerable 扩展方法

提供对可枚举集合的各种安全操作方法。

#### 状态判断方法
- `IsNullOrEmpty<T>()` - 判断集合是否为 null 或空
- `IsNotEmpty<T>()` - 判断集合是否不为空
- `SafeCount<T>()` - 获取集合元素数量（安全）
- `CountIf<T>(Func<T, bool> predicate)` - 获取集合元素数量（使用条件过滤）

#### 安全获取元素方法
- `FirstOrDefaultSafe<T>()` - 获取集合的第一个元素，若为空则返回默认值
- `FirstOrDefaultSafe<T>(Func<T, bool> predicate)` - 获取集合的第一个满足条件的元素
- `LastOrDefaultSafe<T>()` - 获取集合的最后一个元素，若为空则返回默认值
- `LastOrDefaultSafe<T>(Func<T, bool> predicate)` - 获取集合的最后一个满足条件的元素
- `ElementAtOrDefaultSafe<T>(int index)` - 获取集合中指定索引的元素，若越界则返回默认值
- `SingleOrDefaultSafe<T>()` - 获取集合的单个元素，若为空或包含多个元素则返回默认值
- `SingleOrDefaultSafe<T>(Func<T, bool> predicate)` - 获取集合的单个满足条件的元素

#### 集合操作方法
- `ContainsSafe<T>(T item)` - 判断集合是否包含指定元素（支持 null 集合）
- `Contains<T>(Func<T, bool> predicate)` - 判断集合是否包含满足指定条件的元素
- `ToListSafe<T>()` - 将集合转换为 List（安全）
- `ToArraySafe<T>()` - 将集合转换为数组（安全）
- `DistinctSafe<T>()` - 集合去重（安全）
- `DistinctBy<TSource, TKey>(Func<TSource, TKey> keySelector)` - 根据指定键去重
- `WhereSafe<T>(Func<T, bool> predicate)` - 集合过滤（安全）
- `SelectSafe<TSource, TResult>(Func<TSource, TResult> selector)` - 集合投影（安全）
- `GroupBySafe<TSource, TKey>(Func<TSource, TKey> keySelector)` - 集合分组（安全）
- `OrderBySafe<TSource, TKey>(Func<TSource, TKey> keySelector)` - 集合排序（安全）
- `OrderByDescendingSafe<TSource, TKey>(Func<TSource, TKey> keySelector)` - 集合逆序排序（安全）
- `ReverseSafe<T>()` - 集合反转（安全）
- `Shuffle<T>()` - 集合随机排序（洗牌）

#### 分页方法
- `Page<T>(int pageIndex, int pageSize)` - 集合分页（安全）
- `GetPaged<T>(int pageIndex, int pageSize)` - 获取分页信息（包含分页数据和分页信息）

#### 统计方法
- `SumSafe()` - 集合求和（安全）- 支持 int、double、decimal、float
- `AverageSafe()` - 集合平均值（安全）- 支持 int、double、decimal、float
- `MaxSafe()` - 集合最大值（安全）- 支持 int、double、decimal、float
- `MinSafe()` - 集合最小值（安全）- 支持 int、double、decimal、float
- `MaxBy<TSource, TKey>(Func<TSource, TKey> keySelector)` - 获取集合中最大值对应的元素
- `MinBy<TSource, TKey>(Func<TSource, TKey> keySelector)` - 获取集合中最小值对应的元素

#### 批量操作方法
- `ForEachSafe<T>(Action<T> action)` - 集合遍历执行操作（安全）
- `ForEachAsync<T>(Func<T, Task> action)` - 集合遍历执行异步操作
- `Chunk<T>(int size)` - 集合分块（按指定大小分组）
- `Batch<T>(int batchSize)` - 集合分批（与 Chunk 类似）

#### 集合判断方法
- `AllSafe<T>(Func<T, bool> predicate)` - 判断是否全部满足条件（安全）
- `AnySafe<T>(Func<T, bool> predicate)` - 判断是否存在满足条件的元素（安全）
- `SequenceEqualSafe<T>(IEnumerable<T> other)` - 判断两个集合是否相等（安全）

#### 集合转换方法
- `ToDictionarySafe<TSource, TKey, TValue>(Func<TSource, TKey> keySelector, Func<TSource, TValue> valueSelector)` - 集合转换为字典（安全）
- `ToHashSetSafe<T>()` - 集合转换为 HashSet（安全）
- `ToDataTable<T>()` - IEnumerable 转换为 DataTable
- `ToConcurrentDictionary<TKey, TValue>()` - IEnumerable<KeyValuePair> 转换为 ConcurrentDictionary
- `ToConcurrentDictionary<TSource, TKey, TValue>(Func<TSource, TKey> keySelector, Func<TSource, TValue> valueSelector)` - IEnumerable 转换为 ConcurrentDictionary（自定义键值选择器）

#### 其他方法
- `RemoveNulls<T>()` - 集合去除 null 元素
- `Join<T>(string separator)` - 将集合元素连接为字符串

### 2. ICollection 扩展方法

提供对集合类型的安全操作方法。

#### 状态判断方法
- `IsNullOrEmpty<T>()` - 判断集合是否为 null 或空

#### 元素操作方法
- `AddSafe<T>(T item)` - 安全添加元素到集合
- `RemoveSafe<T>(T item)` - 安全移除元素
- `ClearSafe<T>()` - 清空集合（安全）
- `AddRangeSafe<T>(IEnumerable<T> items)` - 批量添加元素（安全）
- `RemoveRangeSafe<T>(IEnumerable<T> items)` - 批量移除元素（安全）

---

## 方法详细说明

### 状态判断

#### IsNullOrEmpty
判断集合是否为 null 或空。

```csharp
public static bool IsNullOrEmpty<T>(this IEnumerable<T> source)
```

**参数：**
- `source`: 待判断的集合

**返回值：**
- 如果集合为 null 或不包含任何元素，返回 true；否则返回 false

**示例：**
```csharp
List<int> list1 = null;
List<int> list2 = new List<int>();
List<int> list3 = new List<int> { 1, 2, 3 };

bool result1 = list1.IsNullOrEmpty(); // true
bool result2 = list2.IsNullOrEmpty(); // true
bool result3 = list3.IsNullOrEmpty(); // false
```

#### SafeCount
获取集合元素数量（安全，支持 null 集合）。

```csharp
public static int SafeCount<T>(this IEnumerable<T> source)
```

**参数：**
- `source`: 集合

**返回值：**
- 集合元素数量，如果集合为 null 则返回 0

**示例：**
```csharp
List<int> list1 = null;
List<int> list2 = new List<int> { 1, 2, 3 };

int count1 = list1.SafeCount(); // 0
int count2 = list2.SafeCount(); // 3
```

### 安全获取元素

#### FirstOrDefaultSafe
获取集合的第一个元素，若为空则返回默认值。

```csharp
public static T FirstOrDefaultSafe<T>(this IEnumerable<T> source)
public static T FirstOrDefaultSafe<T>(this IEnumerable<T> source, Func<T, bool> predicate)
```

**参数：**
- `source`: 集合
- `predicate`: 过滤条件（可选）

**返回值：**
- 第一个元素或默认值

**示例：**
```csharp
List<int> list1 = null;
List<int> list2 = new List<int> { 1, 2, 3, 4, 5 };

int result1 = list1.FirstOrDefaultSafe(); // 0 (default)
int result2 = list2.FirstOrDefaultSafe(); // 1
int result3 = list2.FirstOrDefaultSafe(x => x > 3); // 4
```

#### ElementAtOrDefaultSafe
获取集合中指定索引的元素，若索引越界则返回默认值。

```csharp
public static T ElementAtOrDefaultSafe<T>(this IEnumerable<T> source, int index)
```

**参数：**
- `source`: 集合
- `index`: 索引位置

**返回值：**
- 指定索引的元素或默认值

**示例：**
```csharp
List<int> list = new List<int> { 1, 2, 3 };

int result1 = list.ElementAtOrDefaultSafe(1); // 2
int result2 = list.ElementAtOrDefaultSafe(10); // 0 (default)
```

### 集合操作

#### ContainsSafe
判断集合是否包含指定元素（支持 null 集合）。

```csharp
public static bool ContainsSafe<T>(this IEnumerable<T> source, T item)
```

**参数：**
- `source`: 集合
- `item`: 要查找的元素

**返回值：**
- 如果集合包含指定元素返回 true；否则返回 false

**示例：**
```csharp
List<int> list1 = null;
List<int> list2 = new List<int> { 1, 2, 3 };

bool result1 = list1.ContainsSafe(2); // false
bool result2 = list2.ContainsSafe(2); // true
```

#### DistinctBy
根据指定键去重。

```csharp
public static IEnumerable<TSource> DistinctBy<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector)
```

**参数：**
- `source`: 集合
- `keySelector`: 键选择器

**返回值：**
- 去重后的集合

**示例：**
```csharp
var list = new List<Person>
{
    new Person { Id = 1, Name = "Alice" },
    new Person { Id = 2, Name = "Bob" },
    new Person { Id = 1, Name = "Alice2" }
};

var distinct = list.DistinctBy(x => x.Id).ToList();
// 结果：Id=1 (Alice), Id=2 (Bob)
```

### 分页

#### Page
集合分页。

```csharp
public static IEnumerable<T> Page<T>(this IEnumerable<T> source, int pageIndex, int pageSize)
```

**参数：**
- `source`: 集合
- `pageIndex`: 页索引（从 0 开始）
- `pageSize`: 每页大小

**返回值：**
- 分页后的集合

**示例：**
```csharp
var list = Enumerable.Range(1, 100);
var page1 = list.Page(0, 10); // 第1页，包含1-10
var page2 = list.Page(1, 10); // 第2页，包含11-20
```

#### GetPaged
获取分页信息。

```csharp
public static (IEnumerable<T> Items, int TotalCount, int TotalPages, int CurrentPage) GetPaged<T>(
    this IEnumerable<T> source, int pageIndex, int pageSize)
```

**参数：**
- `source`: 集合
- `pageIndex`: 页索引（从 0 开始）
- `pageSize`: 每页大小

**返回值：**
- 包含分页数据和分页信息的元组

**示例：**
```csharp
var list = Enumerable.Range(1, 100);
var (items, totalCount, totalPages, currentPage) = list.GetPaged(0, 10);
// items: 1-10, totalCount: 100, totalPages: 10, currentPage: 1
```

### 统计计算

#### SumSafe
集合求和（安全）。

```csharp
public static int SumSafe(this IEnumerable<int> source)
public static double SumSafe(this IEnumerable<double> source)
public static decimal SumSafe(this IEnumerable<decimal> source)
public static float SumSafe(this IEnumerable<float> source)
```

**参数：**
- `source`: 集合

**返回值：**
- 集合元素之和，如果集合为 null 则返回 0

**示例：**
```csharp
List<int> list1 = null;
List<int> list2 = new List<int> { 1, 2, 3 };

int sum1 = list1.SumSafe(); // 0
int sum2 = list2.SumSafe(); // 6
```

#### AverageSafe
集合平均值（安全）。

```csharp
public static double AverageSafe(this IEnumerable<int> source)
public static double AverageSafe(this IEnumerable<double> source)
```

**参数：**
- `source`: 集合

**返回值：**
- 集合元素平均值，如果集合为 null 或空则返回 0

**示例：**
```csharp
List<int> list = new List<int> { 1, 2, 3, 4, 5 };

double avg = list.AverageSafe(); // 3
```

#### MaxBy
获取集合中最大值对应的元素。

```csharp
public static TSource MaxBy<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector)
    where TKey : IComparable<TKey>
```

**参数：**
- `source`: 集合
- `keySelector`: 键选择器

**返回值：**
- 最大值对应的元素，如果集合为空则返回默认值

**示例：**
```csharp
var list = new List<Person>
{
    new Person { Id = 1, Age = 20 },
    new Person { Id = 2, Age = 30 },
    new Person { Id = 3, Age = 25 }
};

var oldest = list.MaxBy(x => x.Age); // Id=2, Age=30
```

### 批量操作

#### ForEachSafe
集合遍历执行操作（安全）。

```csharp
public static void ForEachSafe<T>(this IEnumerable<T> source, Action<T> action)
```

**参数：**
- `source`: 集合
- `action`: 要执行的操作

**示例：**
```csharp
List<int> list = new List<int> { 1, 2, 3 };
list.ForEachSafe(x => Console.WriteLine(x));
```

#### ForEachAsync
集合遍历执行异步操作。

```csharp
public static Task ForEachAsync<T>(this IEnumerable<T> source, Func<T, Task> action)
```

**参数：**
- `source`: 集合
- `action`: 要执行的异步操作

**返回值：**
- Task

**示例：**
```csharp
List<string> urls = new List<string> { "url1", "url2", "url3" };
await urls.ForEachAsync(async url => 
{
    await DownloadAsync(url);
});
```

#### Chunk
集合分块（按指定大小分组）。

```csharp
public static IEnumerable<IEnumerable<T>> Chunk<T>(this IEnumerable<T> source, int size)
```

**参数：**
- `source`: 集合
- `size`: 每块大小

**返回值：**
- 分块后的集合

**示例：**
```csharp
var list = Enumerable.Range(1, 10);
var chunks = list.Chunk(3).ToList();
// 结果：[1,2,3], [4,5,6], [7,8,9], [10]
```

### 集合转换

#### ToDictionarySafe
集合转换为字典（安全）。

```csharp
public static Dictionary<TKey, TValue> ToDictionarySafe<TSource, TKey, TValue>(
    this IEnumerable<TSource> source,
    Func<TSource, TKey> keySelector,
    Func<TSource, TValue> valueSelector)
```

**参数：**
- `source`: 集合
- `keySelector`: 键选择器
- `valueSelector`: 值选择器

**返回值：**
- 字典，如果源集合为 null 则返回空字典

**示例：**
```csharp
var list = new List<Person>
{
    new Person { Id = 1, Name = "Alice" },
    new Person { Id = 2, Name = "Bob" }
};

var dict = list.ToDictionarySafe(x => x.Id, x => x.Name);
// { 1: "Alice", 2: "Bob" }
```

#### ToDataTable
IEnumerable 转换为 DataTable。

```csharp
public static DataTable ToDataTable<T>(this IEnumerable<T> source)
```

**参数：**
- `source`: 集合

**返回值：**
- DataTable

**示例：**
```csharp
var list = new List<Person>
{
    new Person { Id = 1, Name = "Alice" },
    new Person { Id = 2, Name = "Bob" }
};

DataTable table = list.ToDataTable();
```

#### ToConcurrentDictionary
IEnumerable 转换为 ConcurrentDictionary。

```csharp
public static ConcurrentDictionary<TKey, TValue> ToConcurrentDictionary<TSource, TKey, TValue>(
    this IEnumerable<TSource> source,
    Func<TSource, TKey> keySelector,
    Func<TSource, TValue> valueSelector)
```

**参数：**
- `source`: 集合
- `keySelector`: 键选择器
- `valueSelector`: 值选择器

**返回值：**
- ConcurrentDictionary

**示例：**
```csharp
var list = new List<Person>
{
    new Person { Id = 1, Name = "Alice" },
    new Person { Id = 2, Name = "Bob" }
};

var dict = list.ToConcurrentDictionary(x => x.Id, x => x.Name);
```

### ICollection 扩展

#### AddSafe
安全添加元素到集合。

```csharp
public static void AddSafe<T>(this ICollection<T> collection, T item)
```

**参数：**
- `collection`: 集合
- `item`: 要添加的元素

**示例：**
```csharp
List<int> list = null;
list.AddSafe(1); // 不会抛出异常
```

#### AddRangeSafe
批量添加元素（安全）。

```csharp
public static void AddRangeSafe<T>(this ICollection<T> collection, IEnumerable<T> items)
```

**参数：**
- `collection`: 集合
- `items`: 要添加的元素集合

**示例：**
```csharp
List<int> list = new List<int> { 1, 2 };
list.AddRangeSafe(new[] { 3, 4, 5 });
// list: [1, 2, 3, 4, 5]
```

---

## 使用场景

1. **数据处理** - 安全处理可能为空的集合数据，避免 null 异常
2. **Web API** - 处理请求和响应数据的集合操作
3. **数据转换** - 在各种数据结构（DataTable、Dictionary、ConcurrentDictionary）之间转换
4. **分页处理** - 对数据集合进行分页处理
5. **统计分析** - 对集合数据进行求和、平均值、最大值等统计计算
6. **业务逻辑** - 过滤、排序、分组等业务操作
7. **并发处理** - 转换为线程安全的 ConcurrentDictionary
8. **数据清洗** - 去重、去除 null 值、批量处理等

---

## 注意事项

1. 所有方法都是扩展方法，需要通过集合实例调用
2. 带有 "Safe" 后缀的方法对 null 值做了安全处理，不会抛出异常
3. 大部分方法在集合为 null 时会返回空集合或默认值，而不是抛出异常
4. `Page<T>()` 方法使用 0 作为起始页索引
5. `ToDataTable<T>()` 方法基于反射实现，可能不支持某些字段映射
6. `Chunk<T>()` 方法使用延迟执行，内存效率较高
7. 统计方法在集合为空时会返回默认值（通常为 0），而不是抛出异常
8. 转换方法在选择器委托为 null 时，使用默认委托进行处理
9. 集合操作方法都进行了空值有效性检查，使代码更健壮
