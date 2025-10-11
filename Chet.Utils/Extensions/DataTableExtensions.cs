using System.Data;

namespace Chet.Utils
{
    /// <summary>
    /// DataTable 扩展方法类，提供常用的转换、查询、操作等功能。
    /// </summary>
    public static class DataTableExtensions
    {
        /// <summary>
        /// 判断 DataTable 是否为 null 或无行数据。
        /// </summary>
        /// <param name="dt">待判断的 DataTable。</param>
        public static bool IsNullOrEmpty(this DataTable dt) =>
            dt == null || dt.Rows.Count == 0;

        /// <summary>
        /// 将 DataTable 转换为泛型集合。
        /// </summary>
        /// <typeparam name="T">目标类型。</typeparam>
        /// <param name="dt">待转换的 DataTable。</param>
        /// <param name="converter">行转换委托。</param>
        public static List<T> ToList<T>(this DataTable dt, Func<DataRow, T> converter)
        {
            if (dt == null || converter == null) return new List<T>();
            return dt.Rows.Cast<DataRow>().Select(converter).ToList();
        }

        /// <summary>
        /// 获取 DataTable 的所有列名。
        /// </summary>
        /// <param name="dt">待处理的 DataTable。</param>
        public static List<string> GetColumnNames(this DataTable dt)
        {
            if (dt == null) return new List<string>();
            return dt.Columns.Cast<DataColumn>().Select(c => c.ColumnName).ToList();
        }

        /// <summary>
        /// 获取 DataTable 的所有行数据（每行为字典）。
        /// </summary>
        /// <param name="dt">待处理的 DataTable。</param>
        public static List<Dictionary<string, object>> ToDictionaryList(this DataTable dt)
        {
            var list = new List<Dictionary<string, object>>();
            if (dt == null) return list;
            foreach (DataRow row in dt.Rows)
            {
                var dict = new Dictionary<string, object>();
                foreach (DataColumn col in dt.Columns)
                {
                    dict[col.ColumnName] = row[col];
                }
                list.Add(dict);
            }
            return list;
        }

        /// <summary>
        /// DataTable 按条件筛选，返回新 DataTable。
        /// </summary>
        /// <param name="dt">待筛选的 DataTable。</param>
        /// <param name="filter">筛选表达式，如 "Age &gt; 18 AND Name LIKE '张%'".</param>
        public static DataTable Filter(this DataTable dt, string filter)
        {
            if (dt == null || string.IsNullOrWhiteSpace(filter)) return dt;
            var rows = dt.Select(filter);
            if (rows.Length == 0) return dt.Clone();
            var newDt = dt.Clone();
            foreach (var row in rows)
                newDt.ImportRow(row);
            return newDt;
        }

        /// <summary>
        /// DataTable 按指定列排序，返回新 DataTable。
        /// </summary>
        /// <param name="dt">待排序的 DataTable。</param>
        /// <param name="sort">排序表达式，如 "Age DESC, Name ASC"。</param>
        public static DataTable Sort(this DataTable dt, string sort)
        {
            if (dt == null || string.IsNullOrWhiteSpace(sort)) return dt;
            var rows = dt.Select("", sort);
            if (rows.Length == 0) return dt.Clone();
            var newDt = dt.Clone();
            foreach (var row in rows)
                newDt.ImportRow(row);
            return newDt;
        }

        /// <summary>
        /// DataTable 转为一行一字典的枚举（便于遍历）。
        /// </summary>
        /// <param name="dt">待处理的 DataTable。</param>
        public static IEnumerable<IDictionary<string, object>> AsEnumerableDictionary(this DataTable dt)
        {
            if (dt == null) yield break;
            foreach (DataRow row in dt.Rows)
            {
                var dict = new Dictionary<string, object>();
                foreach (DataColumn col in dt.Columns)
                {
                    dict[col.ColumnName] = row[col];
                }
                yield return dict;
            }
        }

        /// <summary>
        /// DataTable 克隆结构并复制所有数据。
        /// </summary>
        /// <param name="dt">待复制的 DataTable。</param>
        public static DataTable CopyAll(this DataTable dt)
        {
            if (dt == null) return null;
            return dt.Copy();
        }

        /// <summary>
        /// DataTable 克隆结构但不复制数据。
        /// </summary>
        /// <param name="dt">待克隆的 DataTable。</param>
        public static DataTable CloneStructure(this DataTable dt)
        {
            if (dt == null) return null;
            return dt.Clone();
        }

        /// <summary>
        /// DataTable 添加一行数据。
        /// </summary>
        /// <param name="dt">目标 DataTable。</param>
        /// <param name="values">各列的值，顺序需与列一致。</param>
        public static void AddRow(this DataTable dt, params object[] values)
        {
            if (dt == null || values == null) return;
            var row = dt.NewRow();
            for (int i = 0; i < Math.Min(dt.Columns.Count, values.Length); i++)
            {
                row[i] = values[i];
            }
            dt.Rows.Add(row);
        }

        /// <summary>
        /// DataTable 删除所有行。
        /// </summary>
        /// <param name="dt">待清空的 DataTable。</param>
        public static void ClearRows(this DataTable dt)
        {
            dt?.Rows.Clear();
        }

        /// <summary>
        /// DataTable 转为二维数组。
        /// </summary>
        /// <param name="dt">待转换的 DataTable。</param>
        public static object[,] ToArray(this DataTable dt)
        {
            if (dt == null) return new object[0, 0];
            var rows = dt.Rows.Count;
            var cols = dt.Columns.Count;
            var arr = new object[rows, cols];
            for (int i = 0; i < rows; i++)
                for (int j = 0; j < cols; j++)
                    arr[i, j] = dt.Rows[i][j];
            return arr;
        }
    }
}