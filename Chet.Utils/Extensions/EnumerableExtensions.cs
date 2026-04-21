using System.Collections.Concurrent;

namespace Chet.Utils;

/// <summary>
/// IEnumerable/ICollection 扩展方法类，提供常用的判断、转换、操作、统计等功能。
/// </summary>
/// <remarks>
/// <para>本类提供丰富的集合扩展方法，涵盖以下功能：</para>
/// <list type="bullet">
///   <item><description>空值判断：IsNullOrEmpty、IsNotEmpty</description></item>
///   <item><description>安全操作：FirstOrDefaultSafe、ContainsSafe、ToListSafe 等</description></item>
///   <item><description>集合操作：DistinctSafe、WhereSafe、SelectSafe、GroupBySafe 等</description></item>
///   <item><description>分页排序：Page、OrderBySafe、Shuffle、ReverseSafe</description></item>
///   <item><description>统计计算：SumSafe、AverageSafe、MaxSafe、MinSafe</description></item>
///   <item><description>集合转换：ToDataTable、ToConcurrentDictionary、ToHashSetSafe</description></item>
///   <item><description>批量操作：ForEachSafe、ForEachAsync、Chunk、Batch</description></item>
///   <item><description>集合判断：AllSafe、AnySafe、SequenceEqualSafe</description></item>
/// </list>
/// </remarks>
public static class EnumerableExtensions
{
    #region 空值判断

    /// <summary>
    /// 判断集合是否为 null 或空。
    /// </summary>
    /// <typeparam name="T">元素类型。</typeparam>
    /// <param name="source">待判断的集合。</param>
    /// <returns>如果集合为 null 或不包含任何元素，返回 true；否则返回 false。</returns>
    /// <example>
    /// <code>
    /// List&lt;int&gt; list1 = null;
    /// List&lt;int&gt; list2 = new List&lt;int&gt;();
    /// List&lt;int&gt; list3 = new List&lt;int&gt; { 1, 2, 3 };
    /// 
    /// bool result1 = list1.IsNullOrEmpty(); // true
    /// bool result2 = list2.IsNullOrEmpty(); // true
    /// bool result3 = list3.IsNullOrEmpty(); // false
    /// </code>
    /// </example>
    public static bool IsNullOrEmpty<T>(this IEnumerable<T> source) =>
        source == null || !source.Any();

    /// <summary>
    /// 判断集合是否不为空。
    /// </summary>
    /// <typeparam name="T">元素类型。</typeparam>
    /// <param name="source">待判断的集合。</param>
    /// <returns>如果集合不为 null 且包含至少一个元素，返回 true；否则返回 false。</returns>
    public static bool IsNotEmpty<T>(this IEnumerable<T> source) =>
        source != null && source.Any();

    /// <summary>
    /// 获取集合元素数量（安全，支持 null 集合）。
    /// </summary>
    /// <typeparam name="T">元素类型。</typeparam>
    /// <param name="source">集合。</param>
    /// <returns>集合元素数量，如果集合为 null 则返回 0。</returns>
    public static int SafeCount<T>(this IEnumerable<T> source) =>
        source?.Count() ?? 0;

    /// <summary>
    /// 获取集合元素数量（使用 Predicate 条件过滤）。
    /// </summary>
    /// <typeparam name="T">元素类型。</typeparam>
    /// <param name="source">集合。</param>
    /// <param name="predicate">过滤条件。</param>
    /// <returns>满足条件的元素数量。</returns>
    public static int CountIf<T>(this IEnumerable<T> source, Func<T, bool> predicate) =>
        source == null || predicate == null ? 0 : source.Count(predicate);

    #endregion

    #region 安全获取元素

    /// <summary>
    /// 获取集合的第一个元素，若为空则返回默认值。
    /// </summary>
    /// <typeparam name="T">元素类型。</typeparam>
    /// <param name="source">集合。</param>
    /// <returns>第一个元素或默认值。</returns>
    public static T FirstOrDefaultSafe<T>(this IEnumerable<T> source) =>
        source == null ? default : source.FirstOrDefault();

    /// <summary>
    /// 获取集合的第一个满足条件的元素，若不存在则返回默认值。
    /// </summary>
    /// <typeparam name="T">元素类型。</typeparam>
    /// <param name="source">集合。</param>
    /// <param name="predicate">过滤条件。</param>
    /// <returns>第一个满足条件的元素或默认值。</returns>
    public static T FirstOrDefaultSafe<T>(this IEnumerable<T> source, Func<T, bool> predicate) =>
        source == null || predicate == null ? default : source.FirstOrDefault(predicate);

    /// <summary>
    /// 获取集合的最后一个元素，若为空则返回默认值。
    /// </summary>
    /// <typeparam name="T">元素类型。</typeparam>
    /// <param name="source">集合。</param>
    /// <returns>最后一个元素或默认值。</returns>
    public static T LastOrDefaultSafe<T>(this IEnumerable<T> source) =>
        source == null ? default : source.LastOrDefault();

    /// <summary>
    /// 获取集合的最后一个满足条件的元素，若不存在则返回默认值。
    /// </summary>
    /// <typeparam name="T">元素类型。</typeparam>
    /// <param name="source">集合。</param>
    /// <param name="predicate">过滤条件。</param>
    /// <returns>最后一个满足条件的元素或默认值。</returns>
    public static T LastOrDefaultSafe<T>(this IEnumerable<T> source, Func<T, bool> predicate) =>
        source == null || predicate == null ? default : source.LastOrDefault(predicate);

    /// <summary>
    /// 获取集合中指定索引的元素，若索引越界则返回默认值。
    /// </summary>
    /// <typeparam name="T">元素类型。</typeparam>
    /// <param name="source">集合。</param>
    /// <param name="index">索引位置。</param>
    /// <returns>指定索引的元素或默认值。</returns>
    public static T ElementAtOrDefaultSafe<T>(this IEnumerable<T> source, int index)
    {
        if (source == null || index < 0) return default;
        if (source is IList<T> list && index < list.Count)
            return list[index];
        return source.ElementAtOrDefault(index);
    }

    /// <summary>
    /// 获取集合的单个元素，若集合为空或包含多个元素则返回默认值。
    /// </summary>
    /// <typeparam name="T">元素类型。</typeparam>
    /// <param name="source">集合。</param>
    /// <returns>单个元素或默认值。</returns>
    public static T SingleOrDefaultSafe<T>(this IEnumerable<T> source) =>
        source == null ? default : source.SingleOrDefault();

    /// <summary>
    /// 获取集合的单个满足条件的元素，若不存在或存在多个则返回默认值。
    /// </summary>
    /// <typeparam name="T">元素类型。</typeparam>
    /// <param name="source">集合。</param>
    /// <param name="predicate">过滤条件。</param>
    /// <returns>单个满足条件的元素或默认值。</returns>
    public static T SingleOrDefaultSafe<T>(this IEnumerable<T> source, Func<T, bool> predicate) =>
        source == null || predicate == null ? default : source.SingleOrDefault(predicate);

    #endregion

    #region 集合操作

    /// <summary>
    /// 判断集合是否包含指定元素（支持 null 集合）。
    /// </summary>
    /// <typeparam name="T">元素类型。</typeparam>
    /// <param name="source">集合。</param>
    /// <param name="item">要查找的元素。</param>
    /// <returns>如果集合包含指定元素返回 true；否则返回 false。</returns>
    public static bool ContainsSafe<T>(this IEnumerable<T> source, T item) =>
        source != null && source.Contains(item);

    /// <summary>
    /// 判断集合是否包含满足指定条件的元素。
    /// </summary>
    /// <typeparam name="T">元素类型。</typeparam>
    /// <param name="source">集合。</param>
    /// <param name="predicate">条件。</param>
    /// <returns>如果集合包含满足条件的元素返回 true；否则返回 false。</returns>
    public static bool Contains<T>(this IEnumerable<T> source, Func<T, bool> predicate) =>
        source != null && predicate != null && source.Any(predicate);

    /// <summary>
    /// 将集合转换为 List（安全，支持 null 集合）。
    /// </summary>
    /// <typeparam name="T">元素类型。</typeparam>
    /// <param name="source">集合。</param>
    /// <returns>List 集合，如果源集合为 null 则返回空 List。</returns>
    public static List<T> ToListSafe<T>(this IEnumerable<T> source) =>
        source == null ? new List<T>() : source.ToList();

    /// <summary>
    /// 将集合转换为数组（安全，支持 null 集合）。
    /// </summary>
    /// <typeparam name="T">元素类型。</typeparam>
    /// <param name="source">集合。</param>
    /// <returns>数组，如果源集合为 null 则返回空数组。</returns>
    public static T[] ToArraySafe<T>(this IEnumerable<T> source) =>
        source == null ? Array.Empty<T>() : source.ToArray();

    /// <summary>
    /// 集合去重（安全）。
    /// </summary>
    /// <typeparam name="T">元素类型。</typeparam>
    /// <param name="source">集合。</param>
    /// <returns>去重后的集合。</returns>
    public static IEnumerable<T> DistinctSafe<T>(this IEnumerable<T> source) =>
        source == null ? Enumerable.Empty<T>() : source.Distinct();

    /// <summary>
    /// 根据指定键去重。
    /// </summary>
    /// <typeparam name="TSource">源类型。</typeparam>
    /// <typeparam name="TKey">键类型。</typeparam>
    /// <param name="source">集合。</param>
    /// <param name="keySelector">键选择器。</param>
    /// <returns>去重后的集合。</returns>
    public static IEnumerable<TSource> DistinctBy<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector)
    {
        if (source == null || keySelector == null) yield break;
        var seenKeys = new HashSet<TKey>();
        foreach (var element in source)
        {
            if (seenKeys.Add(keySelector(element)))
                yield return element;
        }
    }

    /// <summary>
    /// 集合过滤（安全）。
    /// </summary>
    /// <typeparam name="T">元素类型。</typeparam>
    /// <param name="source">集合。</param>
    /// <param name="predicate">过滤条件。</param>
    /// <returns>过滤后的集合。</returns>
    public static IEnumerable<T> WhereSafe<T>(this IEnumerable<T> source, Func<T, bool> predicate) =>
        source == null ? Enumerable.Empty<T>() : predicate == null ? source : source.Where(predicate);

    /// <summary>
    /// 集合投影（安全）。
    /// </summary>
    /// <typeparam name="TSource">源类型。</typeparam>
    /// <typeparam name="TResult">目标类型。</typeparam>
    /// <param name="source">集合。</param>
    /// <param name="selector">投影函数。</param>
    /// <returns>投影后的集合。</returns>
    public static IEnumerable<TResult> SelectSafe<TSource, TResult>(this IEnumerable<TSource> source, Func<TSource, TResult> selector) =>
        source == null || selector == null ? Enumerable.Empty<TResult>() : source.Select(selector);

    /// <summary>
    /// 集合分组（安全）。
    /// </summary>
    /// <typeparam name="TSource">源类型。</typeparam>
    /// <typeparam name="TKey">分组键类型。</typeparam>
    /// <param name="source">集合。</param>
    /// <param name="keySelector">分组键选择器。</param>
    /// <returns>分组后的集合。</returns>
    public static IEnumerable<IGrouping<TKey, TSource>> GroupBySafe<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector) =>
        source == null || keySelector == null ? Enumerable.Empty<IGrouping<TKey, TSource>>() : source.GroupBy(keySelector);

    /// <summary>
    /// 集合排序（安全）。
    /// </summary>
    /// <typeparam name="TSource">源类型。</typeparam>
    /// <typeparam name="TKey">排序键类型。</typeparam>
    /// <param name="source">集合。</param>
    /// <param name="keySelector">排序键选择器。</param>
    /// <returns>排序后的集合。</returns>
    public static IEnumerable<TSource> OrderBySafe<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector) =>
        source == null || keySelector == null ? Enumerable.Empty<TSource>() : source.OrderBy(keySelector);

    /// <summary>
    /// 集合逆序排序（安全）。
    /// </summary>
    /// <typeparam name="TSource">源类型。</typeparam>
    /// <typeparam name="TKey">排序键类型。</typeparam>
    /// <param name="source">集合。</param>
    /// <param name="keySelector">排序键选择器。</param>
    /// <returns>逆序排序后的集合。</returns>
    public static IEnumerable<TSource> OrderByDescendingSafe<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector) =>
        source == null || keySelector == null ? Enumerable.Empty<TSource>() : source.OrderByDescending(keySelector);

    /// <summary>
    /// 集合反转（安全）。
    /// </summary>
    /// <typeparam name="T">元素类型。</typeparam>
    /// <param name="source">集合。</param>
    /// <returns>反转后的集合。</returns>
    public static IEnumerable<T> ReverseSafe<T>(this IEnumerable<T> source) =>
        source == null ? Enumerable.Empty<T>() : source.Reverse();

    /// <summary>
    /// 集合随机排序（洗牌）。
    /// </summary>
    /// <typeparam name="T">元素类型。</typeparam>
    /// <param name="source">集合。</param>
    /// <returns>随机排序后的集合。</returns>
    public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> source)
    {
        if (source == null) return Enumerable.Empty<T>();
        var random = new Random();
        return source.OrderBy(x => random.Next());
    }

    #endregion

    #region 分页

    /// <summary>
    /// 集合分页。
    /// </summary>
    /// <typeparam name="T">元素类型。</typeparam>
    /// <param name="source">集合。</param>
    /// <param name="pageIndex">页索引（从 0 开始）。</param>
    /// <param name="pageSize">每页大小。</param>
    /// <returns>分页后的集合。</returns>
    /// <example>
    /// <code>
    /// var list = Enumerable.Range(1, 100);
    /// var page1 = list.Page(0, 10); // 第1页，包含1-10
    /// var page2 = list.Page(1, 10); // 第2页，包含11-20
    /// </code>
    /// </example>
    public static IEnumerable<T> Page<T>(this IEnumerable<T> source, int pageIndex, int pageSize)
    {
        if (source == null || pageIndex < 0 || pageSize <= 0) return Enumerable.Empty<T>();
        return source.Skip(pageIndex * pageSize).Take(pageSize);
    }

    /// <summary>
    /// 获取分页信息。
    /// </summary>
    /// <typeparam name="T">元素类型。</typeparam>
    /// <param name="source">集合。</param>
    /// <param name="pageIndex">页索引（从 0 开始）。</param>
    /// <param name="pageSize">每页大小。</param>
    /// <returns>包含分页数据和分页信息的元组。</returns>
    public static (IEnumerable<T> Items, int TotalCount, int TotalPages, int CurrentPage) GetPaged<T>(
        this IEnumerable<T> source, int pageIndex, int pageSize)
    {
        if (source == null)
            return (Enumerable.Empty<T>(), 0, 0, 0);

        int totalCount = source.Count();
        int totalPages = pageSize > 0 ? (int)Math.Ceiling((double)totalCount / pageSize) : 0;
        var items = source.Page(pageIndex, pageSize);

        return (items, totalCount, totalPages, pageIndex + 1);
    }

    #endregion

    #region 统计计算

    /// <summary>
    /// 集合求和（安全）。
    /// </summary>
    /// <param name="source">集合。</param>
    /// <returns>集合元素之和，如果集合为 null 则返回 0。</returns>
    public static int SumSafe(this IEnumerable<int> source) => source?.Sum() ?? 0;

    /// <summary>
    /// 集合求和（安全）。
    /// </summary>
    public static double SumSafe(this IEnumerable<double> source) => source?.Sum() ?? 0;

    /// <summary>
    /// 集合求和（安全）。
    /// </summary>
    public static decimal SumSafe(this IEnumerable<decimal> source) => source?.Sum() ?? 0;

    /// <summary>
    /// 集合求和（安全）。
    /// </summary>
    public static float SumSafe(this IEnumerable<float> source) => source?.Sum() ?? 0;

    /// <summary>
    /// 集合求和（安全，可空类型）。
    /// </summary>
    public static int SumSafe(this IEnumerable<int?> source) => source?.Sum() ?? 0;

    /// <summary>
    /// 集合求和（安全，可空类型）。
    /// </summary>
    public static double SumSafe(this IEnumerable<double?> source) => source?.Sum() ?? 0;

    /// <summary>
    /// 集合求和（安全，可空类型）。
    /// </summary>
    public static decimal SumSafe(this IEnumerable<decimal?> source) => source?.Sum() ?? 0;

    /// <summary>
    /// 集合平均值（安全）。
    /// </summary>
    /// <param name="source">集合。</param>
    /// <returns>集合元素平均值，如果集合为 null 或空则返回 0。</returns>
    public static double AverageSafe(this IEnumerable<int> source) =>
        source != null && source.Any() ? source.Average() : 0;

    /// <summary>
    /// 集合平均值（安全）。
    /// </summary>
    public static double AverageSafe(this IEnumerable<double> source) =>
        source != null && source.Any() ? source.Average() : 0;

    /// <summary>
    /// 集合平均值（安全）。
    /// </summary>
    public static decimal AverageSafe(this IEnumerable<decimal> source) =>
        source != null && source.Any() ? source.Average() : 0;

    /// <summary>
    /// 集合平均值（安全）。
    /// </summary>
    public static float AverageSafe(this IEnumerable<float> source) =>
        source != null && source.Any() ? source.Average() : 0;

    /// <summary>
    /// 集合最大值（安全）。
    /// </summary>
    /// <param name="source">集合。</param>
    /// <returns>集合元素最大值，如果集合为 null 或空则返回 0。</returns>
    public static int MaxSafe(this IEnumerable<int> source) =>
        source != null && source.Any() ? source.Max() : 0;

    /// <summary>
    /// 集合最大值（安全）。
    /// </summary>
    public static double MaxSafe(this IEnumerable<double> source) =>
        source != null && source.Any() ? source.Max() : 0;

    /// <summary>
    /// 集合最大值（安全）。
    /// </summary>
    public static decimal MaxSafe(this IEnumerable<decimal> source) =>
        source != null && source.Any() ? source.Max() : 0;

    /// <summary>
    /// 集合最大值（安全）。
    /// </summary>
    public static float MaxSafe(this IEnumerable<float> source) =>
        source != null && source.Any() ? source.Max() : 0;

    /// <summary>
    /// 集合最小值（安全）。
    /// </summary>
    /// <param name="source">集合。</param>
    /// <returns>集合元素最小值，如果集合为 null 或空则返回 0。</returns>
    public static int MinSafe(this IEnumerable<int> source) =>
        source != null && source.Any() ? source.Min() : 0;

    /// <summary>
    /// 集合最小值（安全）。
    /// </summary>
    public static double MinSafe(this IEnumerable<double> source) =>
        source != null && source.Any() ? source.Min() : 0;

    /// <summary>
    /// 集合最小值（安全）。
    /// </summary>
    public static decimal MinSafe(this IEnumerable<decimal> source) =>
        source != null && source.Any() ? source.Min() : 0;

    /// <summary>
    /// 集合最小值（安全）。
    /// </summary>
    public static float MinSafe(this IEnumerable<float> source) =>
        source != null && source.Any() ? source.Min() : 0;

    /// <summary>
    /// 获取集合中最大值对应的元素。
    /// </summary>
    /// <typeparam name="TSource">源类型。</typeparam>
    /// <typeparam name="TKey">键类型。</typeparam>
    /// <param name="source">集合。</param>
    /// <param name="keySelector">键选择器。</param>
    /// <returns>最大值对应的元素，如果集合为空则返回默认值。</returns>
    public static TSource MaxBy<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector)
        where TKey : IComparable<TKey>
    {
        if (source == null || keySelector == null) return default;
        using var enumerator = source.GetEnumerator();
        if (!enumerator.MoveNext()) return default;

        TSource maxItem = enumerator.Current;
        TKey maxKey = keySelector(maxItem);

        while (enumerator.MoveNext())
        {
            TKey currentKey = keySelector(enumerator.Current);
            if (currentKey.CompareTo(maxKey) > 0)
            {
                maxKey = currentKey;
                maxItem = enumerator.Current;
            }
        }

        return maxItem;
    }

    /// <summary>
    /// 获取集合中最小值对应的元素。
    /// </summary>
    /// <typeparam name="TSource">源类型。</typeparam>
    /// <typeparam name="TKey">键类型。</typeparam>
    /// <param name="source">集合。</param>
    /// <param name="keySelector">键选择器。</param>
    /// <returns>最小值对应的元素，如果集合为空则返回默认值。</returns>
    public static TSource MinBy<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector)
        where TKey : IComparable<TKey>
    {
        if (source == null || keySelector == null) return default;
        using var enumerator = source.GetEnumerator();
        if (!enumerator.MoveNext()) return default;

        TSource minItem = enumerator.Current;
        TKey minKey = keySelector(minItem);

        while (enumerator.MoveNext())
        {
            TKey currentKey = keySelector(enumerator.Current);
            if (currentKey.CompareTo(minKey) < 0)
            {
                minKey = currentKey;
                minItem = enumerator.Current;
            }
        }

        return minItem;
    }

    #endregion

    #region 集合转换

    /// <summary>
    /// 集合去除 null 元素。
    /// </summary>
    /// <typeparam name="T">元素类型。</typeparam>
    /// <param name="source">集合。</param>
    /// <returns>不包含 null 元素的集合。</returns>
    public static IEnumerable<T> RemoveNulls<T>(this IEnumerable<T> source) =>
        source == null ? Enumerable.Empty<T>() : source.Where(x => x != null);

    /// <summary>
    /// 集合转为字典（安全）。
    /// </summary>
    /// <typeparam name="TSource">源类型。</typeparam>
    /// <typeparam name="TKey">键类型。</typeparam>
    /// <typeparam name="TValue">值类型。</typeparam>
    /// <param name="source">集合。</param>
    /// <param name="keySelector">键选择器。</param>
    /// <param name="valueSelector">值选择器。</param>
    /// <returns>字典，如果源集合为 null 则返回空字典。</returns>
    public static Dictionary<TKey, TValue> ToDictionarySafe<TSource, TKey, TValue>(
        this IEnumerable<TSource> source, Func<TSource, TKey> keySelector, Func<TSource, TValue> valueSelector)
    {
        if (source == null || keySelector == null || valueSelector == null)
            return new Dictionary<TKey, TValue>();
        return source.ToDictionary(keySelector, valueSelector);
    }

    /// <summary>
    /// 集合转为 HashSet（安全）。
    /// </summary>
    /// <typeparam name="T">元素类型。</typeparam>
    /// <param name="source">集合。</param>
    /// <returns>HashSet，如果源集合为 null 则返回空 HashSet。</returns>
    public static HashSet<T> ToHashSetSafe<T>(this IEnumerable<T> source) =>
        source == null ? new HashSet<T>() : new HashSet<T>(source);

    /// <summary>
    /// IEnumerable 转为 DataTable（自动推断列名和类型）。
    /// </summary>
    /// <typeparam name="T">元素类型。</typeparam>
    /// <param name="source">集合。</param>
    /// <returns>DataTable。</returns>
    public static System.Data.DataTable ToDataTable<T>(this IEnumerable<T> source)
    {
        var dt = new System.Data.DataTable(typeof(T).Name);
        if (source == null) return dt;
        var props = typeof(T).GetProperties();
        foreach (var prop in props)
            dt.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
        foreach (var item in source)
        {
            var values = props.Select(p => p.GetValue(item, null)).ToArray();
            dt.Rows.Add(values);
        }
        return dt;
    }

    /// <summary>
    /// IEnumerable&lt;KeyValuePair&gt; 转为 ConcurrentDictionary。
    /// </summary>
    /// <typeparam name="TKey">键类型。</typeparam>
    /// <typeparam name="TValue">值类型。</typeparam>
    /// <param name="source">集合。</param>
    /// <returns>ConcurrentDictionary。</returns>
    public static ConcurrentDictionary<TKey, TValue> ToConcurrentDictionary<TKey, TValue>(
        this IEnumerable<KeyValuePair<TKey, TValue>> source)
    {
        var dict = new ConcurrentDictionary<TKey, TValue>();
        if (source == null) return dict;
        foreach (var kv in source)
            dict.TryAdd(kv.Key, kv.Value);
        return dict;
    }

    /// <summary>
    /// IEnumerable 转为 ConcurrentDictionary（自定义键值选择器）。
    /// </summary>
    /// <typeparam name="TSource">源类型。</typeparam>
    /// <typeparam name="TKey">键类型。</typeparam>
    /// <typeparam name="TValue">值类型。</typeparam>
    /// <param name="source">集合。</param>
    /// <param name="keySelector">键选择器。</param>
    /// <param name="valueSelector">值选择器。</param>
    /// <returns>ConcurrentDictionary。</returns>
    public static ConcurrentDictionary<TKey, TValue> ToConcurrentDictionary<TSource, TKey, TValue>(
        this IEnumerable<TSource> source, Func<TSource, TKey> keySelector, Func<TSource, TValue> valueSelector)
    {
        var dict = new ConcurrentDictionary<TKey, TValue>();
        if (source == null || keySelector == null || valueSelector == null) return dict;
        foreach (var item in source)
            dict.TryAdd(keySelector(item), valueSelector(item));
        return dict;
    }

    /// <summary>
    /// 将集合转换为以指定分隔符连接的字符串。
    /// </summary>
    /// <typeparam name="T">元素类型。</typeparam>
    /// <param name="source">集合。</param>
    /// <param name="separator">分隔符，默认为逗号。</param>
    /// <returns>连接后的字符串。</returns>
    public static string JoinToString<T>(this IEnumerable<T> source, string separator = ",")
    {
        if (source == null) return string.Empty;
        return string.Join(separator, source);
    }

    /// <summary>
    /// 将集合转换为以指定分隔符连接的字符串（带选择器）。
    /// </summary>
    /// <typeparam name="T">元素类型。</typeparam>
    /// <param name="source">集合。</param>
    /// <param name="selector">字符串选择器。</param>
    /// <param name="separator">分隔符，默认为逗号。</param>
    /// <returns>连接后的字符串。</returns>
    public static string JoinToString<T>(this IEnumerable<T> source, Func<T, string> selector, string separator = ",")
    {
        if (source == null || selector == null) return string.Empty;
        return string.Join(separator, source.Select(selector));
    }

    #endregion

    #region 批量操作

    /// <summary>
    /// 集合遍历执行操作（安全）。
    /// </summary>
    /// <typeparam name="T">元素类型。</typeparam>
    /// <param name="source">集合。</param>
    /// <param name="action">操作委托。</param>
    public static void ForEachSafe<T>(this IEnumerable<T> source, Action<T> action)
    {
        if (source == null || action == null) return;
        foreach (var item in source) action(item);
    }

    /// <summary>
    /// 集合遍历执行操作（带索引）。
    /// </summary>
    /// <typeparam name="T">元素类型。</typeparam>
    /// <param name="source">集合。</param>
    /// <param name="action">操作委托，参数为元素和索引。</param>
    public static void ForEach<T>(this IEnumerable<T> source, Action<T, int> action)
    {
        if (source == null || action == null) return;
        int index = 0;
        foreach (var item in source)
        {
            action(item, index++);
        }
    }

    /// <summary>
    /// 集合异步遍历执行操作。
    /// </summary>
    /// <typeparam name="T">元素类型。</typeparam>
    /// <param name="source">集合。</param>
    /// <param name="action">异步操作委托。</param>
    /// <param name="maxDegreeOfParallelism">最大并行度，默认为处理器数量。</param>
    /// <param name="cancellationToken">取消令牌。</param>
    public static async Task ForEachAsync<T>(
        this IEnumerable<T> source,
        Func<T, Task> action,
        int maxDegreeOfParallelism = 0,
        CancellationToken cancellationToken = default)
    {
        if (source == null || action == null) return;

        if (maxDegreeOfParallelism <= 0)
            maxDegreeOfParallelism = Environment.ProcessorCount;

        var options = new ParallelOptions
        {
            MaxDegreeOfParallelism = maxDegreeOfParallelism,
            CancellationToken = cancellationToken
        };

        await Parallel.ForEachAsync(source, options, async (item, ct) =>
        {
            await action(item);
        });
    }

    /// <summary>
    /// 集合分块（按指定大小分组）。
    /// </summary>
    /// <typeparam name="T">元素类型。</typeparam>
    /// <param name="source">集合。</param>
    /// <param name="size">分块大小。</param>
    /// <returns>分块后的集合的集合。</returns>
    /// <example>
    /// <code>
    /// var list = Enumerable.Range(1, 10);
    /// var chunks = list.Chunk(3);
    /// // 结果: [[1,2,3], [4,5,6], [7,8,9], [10]]
    /// </code>
    /// </example>
    public static IEnumerable<IEnumerable<T>> Chunk<T>(this IEnumerable<T> source, int size)
    {
        if (source == null || size <= 0) yield break;
        var chunk = new List<T>(size);
        foreach (var item in source)
        {
            chunk.Add(item);
            if (chunk.Count == size)
            {
                yield return chunk.ToList();
                chunk.Clear();
            }
        }
        if (chunk.Count > 0)
            yield return chunk.ToList();
    }

    /// <summary>
    /// 集合分批处理。
    /// </summary>
    /// <typeparam name="T">元素类型。</typeparam>
    /// <param name="source">集合。</param>
    /// <param name="batchSize">批次大小。</param>
    /// <param name="action">批次处理操作。</param>
    public static void Batch<T>(this IEnumerable<T> source, int batchSize, Action<IEnumerable<T>> action)
    {
        if (source == null || action == null || batchSize <= 0) return;
        foreach (var batch in source.Chunk(batchSize))
        {
            action(batch);
        }
    }

    /// <summary>
    /// 集合分批异步处理。
    /// </summary>
    /// <typeparam name="T">元素类型。</typeparam>
    /// <param name="source">集合。</param>
    /// <param name="batchSize">批次大小。</param>
    /// <param name="action">批次异步处理操作。</param>
    public static async Task BatchAsync<T>(this IEnumerable<T> source, int batchSize, Func<IEnumerable<T>, Task> action)
    {
        if (source == null || action == null || batchSize <= 0) return;
        foreach (var batch in source.Chunk(batchSize))
        {
            await action(batch);
        }
    }

    #endregion

    #region 集合判断

    /// <summary>
    /// 集合是否全部满足条件（安全）。
    /// </summary>
    /// <typeparam name="T">元素类型。</typeparam>
    /// <param name="source">集合。</param>
    /// <param name="predicate">条件。</param>
    /// <returns>如果集合为 null 或所有元素都满足条件返回 true；否则返回 false。</returns>
    public static bool AllSafe<T>(this IEnumerable<T> source, Func<T, bool> predicate) =>
        source != null && predicate != null && source.All(predicate);

    /// <summary>
    /// 集合是否有任意元素满足条件（安全）。
    /// </summary>
    /// <typeparam name="T">元素类型。</typeparam>
    /// <param name="source">集合。</param>
    /// <param name="predicate">条件。</param>
    /// <returns>如果集合不为 null 且有任意元素满足条件返回 true；否则返回 false。</returns>
    public static bool AnySafe<T>(this IEnumerable<T> source, Func<T, bool> predicate) =>
        source != null && predicate != null && source.Any(predicate);

    /// <summary>
    /// 判断两个集合是否相等（顺序和元素都相同）。
    /// </summary>
    /// <typeparam name="T">元素类型。</typeparam>
    /// <param name="first">第一个集合。</param>
    /// <param name="second">第二个集合。</param>
    /// <returns>如果两个集合相等返回 true；否则返回 false。</returns>
    public static bool SequenceEqualSafe<T>(this IEnumerable<T> first, IEnumerable<T> second)
    {
        if (first == null && second == null) return true;
        if (first == null || second == null) return false;
        return first.SequenceEqual(second);
    }

    /// <summary>
    /// 判断两个集合是否包含相同的元素（忽略顺序）。
    /// </summary>
    /// <typeparam name="T">元素类型。</typeparam>
    /// <param name="first">第一个集合。</param>
    /// <param name="second">第二个集合。</param>
    /// <returns>如果两个集合包含相同的元素返回 true；否则返回 false。</returns>
    public static bool ContainsSameElements<T>(this IEnumerable<T> first, IEnumerable<T> second)
    {
        if (first == null && second == null) return true;
        if (first == null || second == null) return false;

        var firstSet = new HashSet<T>(first);
        var secondSet = new HashSet<T>(second);
        return firstSet.SetEquals(secondSet);
    }

    /// <summary>
    /// 判断集合是否为空集或包含 null 元素。
    /// </summary>
    /// <typeparam name="T">元素类型。</typeparam>
    /// <param name="source">集合。</param>
    /// <returns>如果集合为空或包含 null 元素返回 true；否则返回 false。</returns>
    public static bool ContainsNull<T>(this IEnumerable<T> source) =>
        source != null && source.Any(x => x == null);

    #endregion

    #region 集合合并

    /// <summary>
    /// 合并两个集合（安全）。
    /// </summary>
    /// <typeparam name="T">元素类型。</typeparam>
    /// <param name="first">第一个集合。</param>
    /// <param name="second">第二个集合。</param>
    /// <returns>合并后的集合。</returns>
    public static IEnumerable<T> ConcatSafe<T>(this IEnumerable<T> first, IEnumerable<T> second)
    {
        if (first == null && second == null) return Enumerable.Empty<T>();
        if (first == null) return second;
        if (second == null) return first;
        return first.Concat(second);
    }

    /// <summary>
    /// 合并多个集合。
    /// </summary>
    /// <typeparam name="T">元素类型。</typeparam>
    /// <param name="sources">集合数组。</param>
    /// <returns>合并后的集合。</returns>
    public static IEnumerable<T> ConcatMany<T>(params IEnumerable<T>[] sources)
    {
        if (sources == null) yield break;
        foreach (var source in sources)
        {
            if (source != null)
            {
                foreach (var item in source)
                    yield return item;
            }
        }
    }

    /// <summary>
    /// 获取两个集合的交集。
    /// </summary>
    /// <typeparam name="T">元素类型。</typeparam>
    /// <param name="first">第一个集合。</param>
    /// <param name="second">第二个集合。</param>
    /// <returns>交集。</returns>
    public static IEnumerable<T> IntersectSafe<T>(this IEnumerable<T> first, IEnumerable<T> second)
    {
        if (first == null || second == null) return Enumerable.Empty<T>();
        return first.Intersect(second);
    }

    /// <summary>
    /// 获取两个集合的并集。
    /// </summary>
    /// <typeparam name="T">元素类型。</typeparam>
    /// <param name="first">第一个集合。</param>
    /// <param name="second">第二个集合。</param>
    /// <returns>并集。</returns>
    public static IEnumerable<T> UnionSafe<T>(this IEnumerable<T> first, IEnumerable<T> second)
    {
        if (first == null && second == null) return Enumerable.Empty<T>();
        if (first == null) return second;
        if (second == null) return first;
        return first.Union(second);
    }

    /// <summary>
    /// 获取两个集合的差集（在第一个集合但不在第二个集合）。
    /// </summary>
    /// <typeparam name="T">元素类型。</typeparam>
    /// <param name="first">第一个集合。</param>
    /// <param name="second">第二个集合。</param>
    /// <returns>差集。</returns>
    public static IEnumerable<T> ExceptSafe<T>(this IEnumerable<T> first, IEnumerable<T> second)
    {
        if (first == null) return Enumerable.Empty<T>();
        if (second == null) return first;
        return first.Except(second);
    }

    #endregion

    #region ICollection 扩展

    /// <summary>
    /// 判断集合是否为 null 或空。
    /// </summary>
    /// <typeparam name="T">元素类型。</typeparam>
    /// <param name="collection">待判断的集合。</param>
    /// <returns>如果集合为 null 或不包含任何元素返回 true；否则返回 false。</returns>
    public static bool IsNullOrEmpty<T>(this ICollection<T> collection) =>
        collection == null || collection.Count == 0;

    /// <summary>
    /// 安全添加元素到集合。
    /// </summary>
    /// <typeparam name="T">元素类型。</typeparam>
    /// <param name="collection">集合。</param>
    /// <param name="item">要添加的元素。</param>
    public static void AddSafe<T>(this ICollection<T> collection, T item)
    {
        if (collection != null) collection.Add(item);
    }

    /// <summary>
    /// 安全移除元素。
    /// </summary>
    /// <typeparam name="T">元素类型。</typeparam>
    /// <param name="collection">集合。</param>
    /// <param name="item">要移除的元素。</param>
    public static void RemoveSafe<T>(this ICollection<T> collection, T item)
    {
        if (collection != null && collection.Contains(item)) collection.Remove(item);
    }

    /// <summary>
    /// 清空集合（安全）。
    /// </summary>
    /// <typeparam name="T">元素类型。</typeparam>
    /// <param name="collection">集合。</param>
    public static void ClearSafe<T>(this ICollection<T> collection)
    {
        collection?.Clear();
    }

    /// <summary>
    /// 集合批量添加元素（安全）。
    /// </summary>
    /// <typeparam name="T">元素类型。</typeparam>
    /// <param name="collection">集合。</param>
    /// <param name="items">要添加的元素集合。</param>
    public static void AddRangeSafe<T>(this ICollection<T> collection, IEnumerable<T> items)
    {
        if (collection == null || items == null) return;
        foreach (var item in items) collection.Add(item);
    }

    /// <summary>
    /// 集合批量移除元素（安全）。
    /// </summary>
    /// <typeparam name="T">元素类型。</typeparam>
    /// <param name="collection">集合。</param>
    /// <param name="items">要移除的元素集合。</param>
    public static void RemoveRangeSafe<T>(this ICollection<T> collection, IEnumerable<T> items)
    {
        if (collection == null || items == null) return;
        foreach (var item in items.ToList()) collection.Remove(item);
    }

    #endregion
}
