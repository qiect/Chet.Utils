using System.Collections.Concurrent;

namespace Chet.Utils
{
    /// <summary>
    /// IEnumerable/ICollection 扩展方法类，提供常用的判断、转换、操作、统计等功能。
    /// </summary>
    public static class EnumerableExtensions
    {
        #region IEnumerable 扩展

        /// <summary>
        /// 判断集合是否为 null 或空。
        /// </summary>
        /// <typeparam name="T">元素类型。</typeparam>
        /// <param name="source">待判断的集合。</param>
        public static bool IsNullOrEmpty<T>(this IEnumerable<T> source) =>
            source == null || !source.Any();

        /// <summary>
        /// 判断集合是否不为空。
        /// </summary>
        /// <typeparam name="T">元素类型。</typeparam>
        /// <param name="source">待判断的集合。</param>
        public static bool IsNotEmpty<T>(this IEnumerable<T> source) =>
            source != null && source.Any();

        /// <summary>
        /// 获取集合元素数量（安全）。
        /// </summary>
        /// <typeparam name="T">元素类型。</typeparam>
        /// <param name="source">集合。</param>
        public static int SafeCount<T>(this IEnumerable<T> source) =>
            source?.Count() ?? 0;

        /// <summary>
        /// 获取集合的第一个元素，若为空则返回默认值。
        /// </summary>
        /// <typeparam name="T">元素类型。</typeparam>
        /// <param name="source">集合。</param>
        public static T FirstOrDefaultSafe<T>(this IEnumerable<T> source) =>
            source == null ? default : source.FirstOrDefault();

        /// <summary>
        /// 获取集合的最后一个元素，若为空则返回默认值。
        /// </summary>
        /// <typeparam name="T">元素类型。</typeparam>
        /// <param name="source">集合。</param>
        public static T LastOrDefaultSafe<T>(this IEnumerable<T> source) =>
            source == null ? default : source.LastOrDefault();

        /// <summary>
        /// 判断集合是否包含指定元素（支持 null 集合）。
        /// </summary>
        /// <typeparam name="T">元素类型。</typeparam>
        /// <param name="source">集合。</param>
        /// <param name="item">要查找的元素。</param>
        public static bool ContainsSafe<T>(this IEnumerable<T> source, T item) =>
            source != null && source.Contains(item);

        /// <summary>
        /// 将集合转换为 List（安全）。
        /// </summary>
        /// <typeparam name="T">元素类型。</typeparam>
        /// <param name="source">集合。</param>
        public static List<T> ToListSafe<T>(this IEnumerable<T> source) =>
            source == null ? new List<T>() : source.ToList();

        /// <summary>
        /// 将集合转换为数组（安全）。
        /// </summary>
        /// <typeparam name="T">元素类型。</typeparam>
        /// <param name="source">集合。</param>
        public static T[] ToArraySafe<T>(this IEnumerable<T> source) =>
            source == null ? Array.Empty<T>() : source.ToArray();

        /// <summary>
        /// 集合去重（安全）。
        /// </summary>
        /// <typeparam name="T">元素类型。</typeparam>
        /// <param name="source">集合。</param>
        public static IEnumerable<T> DistinctSafe<T>(this IEnumerable<T> source) =>
            source == null ? Enumerable.Empty<T>() : source.Distinct();

        /// <summary>
        /// 集合过滤（安全）。
        /// </summary>
        /// <typeparam name="T">元素类型。</typeparam>
        /// <param name="source">集合。</param>
        /// <param name="predicate">过滤条件。</param>
        public static IEnumerable<T> WhereSafe<T>(this IEnumerable<T> source, Func<T, bool> predicate) =>
            source == null ? Enumerable.Empty<T>() : source.Where(predicate ?? (_ => true));

        /// <summary>
        /// 集合投影（安全）。
        /// </summary>
        /// <typeparam name="TSource">源类型。</typeparam>
        /// <typeparam name="TResult">目标类型。</typeparam>
        /// <param name="source">集合。</param>
        /// <param name="selector">投影函数。</param>
        public static IEnumerable<TResult> SelectSafe<TSource, TResult>(this IEnumerable<TSource> source, Func<TSource, TResult> selector) =>
            source == null ? Enumerable.Empty<TResult>() : source.Select(selector ?? (_ => default));

        /// <summary>
        /// 集合分组（安全）。
        /// </summary>
        /// <typeparam name="TSource">源类型。</typeparam>
        /// <typeparam name="TKey">分组键类型。</typeparam>
        /// <param name="source">集合。</param>
        /// <param name="keySelector">分组键选择器。</param>
        public static IEnumerable<IGrouping<TKey, TSource>> GroupBySafe<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector) =>
            source == null ? Enumerable.Empty<IGrouping<TKey, TSource>>() : source.GroupBy(keySelector ?? (_ => default));

        /// <summary>
        /// 集合排序（安全）。
        /// </summary>
        /// <typeparam name="TSource">源类型。</typeparam>
        /// <typeparam name="TKey">排序键类型。</typeparam>
        /// <param name="source">集合。</param>
        /// <param name="keySelector">排序键选择器。</param>
        public static IEnumerable<TSource> OrderBySafe<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector) =>
            source == null ? Enumerable.Empty<TSource>() : source.OrderBy(keySelector ?? (_ => default));

        /// <summary>
        /// 集合逆序排序（安全）。
        /// </summary>
        /// <typeparam name="TSource">源类型。</typeparam>
        /// <typeparam name="TKey">排序键类型。</typeparam>
        /// <param name="source">集合。</param>
        /// <param name="keySelector">排序键选择器。</param>
        public static IEnumerable<TSource> OrderByDescendingSafe<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector) =>
            source == null ? Enumerable.Empty<TSource>() : source.OrderByDescending(keySelector ?? (_ => default));

        /// <summary>
        /// 集合分页（安全）。
        /// </summary>
        /// <typeparam name="T">元素类型。</typeparam>
        /// <param name="source">集合。</param>
        /// <param name="pageIndex">页索引（从 0 开始）。</param>
        /// <param name="pageSize">每页大小。</param>
        public static IEnumerable<T> Page<T>(this IEnumerable<T> source, int pageIndex, int pageSize)
        {
            if (source == null || pageIndex < 0 || pageSize <= 0) return Enumerable.Empty<T>();
            return source.Skip(pageIndex * pageSize).Take(pageSize);
        }

        /// <summary>
        /// 集合求和（安全）。
        /// </summary>
        /// <param name="source">集合。</param>
        public static int SumSafe(this IEnumerable<int> source) => source?.Sum() ?? 0;
        public static double SumSafe(this IEnumerable<double> source) => source?.Sum() ?? 0;
        public static decimal SumSafe(this IEnumerable<decimal> source) => source?.Sum() ?? 0;
        public static float SumSafe(this IEnumerable<float> source) => source?.Sum() ?? 0;

        /// <summary>
        /// 集合平均值（安全）。
        /// </summary>
        /// <param name="source">集合。</param>
        public static double AverageSafe(this IEnumerable<int> source) => source != null && source.Any() ? source.Average() : 0;
        public static double AverageSafe(this IEnumerable<double> source) => source != null && source.Any() ? source.Average() : 0;
        public static decimal AverageSafe(this IEnumerable<decimal> source) => source != null && source.Any() ? source.Average() : 0;
        public static float AverageSafe(this IEnumerable<float> source) => source != null && source.Any() ? source.Average() : 0;

        /// <summary>
        /// 集合最大值（安全）。
        /// </summary>
        /// <param name="source">集合。</param>
        public static int MaxSafe(this IEnumerable<int> source) => source != null && source.Any() ? source.Max() : 0;
        public static double MaxSafe(this IEnumerable<double> source) => source != null && source.Any() ? source.Max() : 0;
        public static decimal MaxSafe(this IEnumerable<decimal> source) => source != null && source.Any() ? source.Max() : 0;
        public static float MaxSafe(this IEnumerable<float> source) => source != null && source.Any() ? source.Max() : 0;

        /// <summary>
        /// 集合最小值（安全）。
        /// </summary>
        /// <param name="source">集合。</param>
        public static int MinSafe(this IEnumerable<int> source) => source != null && source.Any() ? source.Min() : 0;
        public static double MinSafe(this IEnumerable<double> source) => source != null && source.Any() ? source.Min() : 0;
        public static decimal MinSafe(this IEnumerable<decimal> source) => source != null && source.Any() ? source.Min() : 0;
        public static float MinSafe(this IEnumerable<float> source) => source != null && source.Any() ? source.Min() : 0;

        /// <summary>
        /// 集合去除 null 元素。
        /// </summary>
        /// <typeparam name="T">元素类型。</typeparam>
        /// <param name="source">集合。</param>
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
        public static Dictionary<TKey, TValue> ToDictionarySafe<TSource, TKey, TValue>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector, Func<TSource, TValue> valueSelector)
        {
            if (source == null) return new Dictionary<TKey, TValue>();
            return source.ToDictionary(keySelector ?? (_ => default), valueSelector ?? (_ => default));
        }

        /// <summary>
        /// 集合转为 HashSet（安全）。
        /// </summary>
        /// <typeparam name="T">元素类型。</typeparam>
        /// <param name="source">集合。</param>
        public static HashSet<T> ToHashSetSafe<T>(this IEnumerable<T> source) =>
            source == null ? new HashSet<T>() : new HashSet<T>(source);

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
        /// 集合分块（按指定大小分组）。
        /// </summary>
        /// <typeparam name="T">元素类型。</typeparam>
        /// <param name="source">集合。</param>
        /// <param name="size">分块大小。</param>
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
        /// 集合是否全部满足条件（安全）。
        /// </summary>
        /// <typeparam name="T">元素类型。</typeparam>
        /// <param name="source">集合。</param>
        /// <param name="predicate">条件。</param>
        public static bool AllSafe<T>(this IEnumerable<T> source, Func<T, bool> predicate) =>
            source != null && predicate != null && source.All(predicate);

        /// <summary>
        /// 集合是否有任意元素满足条件（安全）。
        /// </summary>
        /// <typeparam name="T">元素类型。</typeparam>
        /// <param name="source">集合。</param>
        /// <param name="predicate">条件。</param>
        public static bool AnySafe<T>(this IEnumerable<T> source, Func<T, bool> predicate) =>
            source != null && predicate != null && source.Any(predicate);

        /// <summary>
        /// 集合反转（安全）。
        /// </summary>
        /// <typeparam name="T">元素类型。</typeparam>
        /// <param name="source">集合。</param>
        public static IEnumerable<T> ReverseSafe<T>(this IEnumerable<T> source) =>
            source == null ? Enumerable.Empty<T>() : source.Reverse();

        /// <summary>
        /// IEnumerable 转为 DataTable（自动推断列名和类型）。
        /// </summary>
        /// <typeparam name="T">元素类型。</typeparam>
        /// <param name="source">集合。</param>
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
        public static ConcurrentDictionary<TKey, TValue> ToConcurrentDictionary<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>> source)
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
        public static ConcurrentDictionary<TKey, TValue> ToConcurrentDictionary<TSource, TKey, TValue>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector, Func<TSource, TValue> valueSelector)
        {
            var dict = new ConcurrentDictionary<TKey, TValue>();
            if (source == null || keySelector == null || valueSelector == null) return dict;
            foreach (var item in source)
                dict.TryAdd(keySelector(item), valueSelector(item));
            return dict;
        }

        #endregion

        #region ICollection 扩展

        /// <summary>
        /// 判断集合是否为 null 或空。
        /// </summary>
        /// <typeparam name="T">元素类型。</typeparam>
        /// <param name="collection">待判断的集合。</param>
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
            foreach (var item in items) collection.Remove(item);
        }
        #endregion
    }
}