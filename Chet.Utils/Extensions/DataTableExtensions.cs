using System.Data;
using System.Reflection;
using System.Text;

namespace Chet.Utils;

/// <summary>
/// DataTable 扩展方法类，提供常用的转换、查询、操作、导入导出等功能。
/// </summary>
/// <remarks>
/// <para>本类提供丰富的 DataTable 扩展方法，涵盖以下功能：</para>
/// <list type="bullet">
///   <item><description>基础判断：IsNullOrEmpty、HasRows、HasColumns、ContainsColumn</description></item>
///   <item><description>转换操作：ToList、ToDictionaryList、ToArray、ToCsv、ToJson</description></item>
///   <item><description>查询筛选：Filter、Sort、SelectRows、Distinct、Top</description></item>
///   <item><description>聚合统计：Sum、Average、Min、Max、Count</description></item>
///   <item><description>行列操作：AddRow、AddColumn、RemoveColumn、RenameColumn、ClearRows</description></item>
///   <item><description>数据操作：SetValue、GetValue、CloneStructure、CopyAll、Merge</description></item>
///   <item><description>分组操作：GroupBy、GroupBySum、GroupByCount</description></item>
///   <item><description>分页操作：GetPage、GetPagedData</description></item>
///   <item><description>实体转换：ToEntity、ToEntityList、FromEntityList</description></item>
/// </list>
/// </remarks>
public static class DataTableExtensions
{
    #region 基础判断

    /// <summary>
    /// 判断 DataTable 是否为 null 或无行数据。
    /// </summary>
    /// <param name="dt">待判断的 DataTable。</param>
    /// <returns>如果为 null 或无行数据返回 true；否则返回 false。</returns>
    /// <example>
    /// <code>
    /// DataTable dt = null;
    /// var isEmpty = dt.IsNullOrEmpty(); // true
    /// </code>
    /// </example>
    public static bool IsNullOrEmpty(this DataTable dt) =>
        dt == null || dt.Rows.Count == 0;

    /// <summary>
    /// 判断 DataTable 是否有数据行。
    /// </summary>
    /// <param name="dt">待判断的 DataTable。</param>
    /// <returns>如果有数据行返回 true；否则返回 false。</returns>
    public static bool HasRows(this DataTable dt) =>
        dt != null && dt.Rows.Count > 0;

    /// <summary>
    /// 判断 DataTable 是否有列。
    /// </summary>
    /// <param name="dt">待判断的 DataTable。</param>
    /// <returns>如果有列返回 true；否则返回 false。</returns>
    public static bool HasColumns(this DataTable dt) =>
        dt != null && dt.Columns.Count > 0;

    /// <summary>
    /// 判断 DataTable 是否包含指定列名。
    /// </summary>
    /// <param name="dt">待判断的 DataTable。</param>
    /// <param name="columnName">列名。</param>
    /// <returns>如果包含指定列返回 true；否则返回 false。</returns>
    public static bool ContainsColumn(this DataTable dt, string columnName) =>
        dt != null && dt.Columns.Contains(columnName);

    /// <summary>
    /// 判断 DataTable 是否包含指定行。
    /// </summary>
    /// <param name="dt">待判断的 DataTable。</param>
    /// <param name="rowIndex">行索引。</param>
    /// <returns>如果包含指定行返回 true；否则返回 false。</returns>
    public static bool ContainsRow(this DataTable dt, int rowIndex) =>
        dt != null && rowIndex >= 0 && rowIndex < dt.Rows.Count;

    /// <summary>
    /// 获取 DataTable 的行数。
    /// </summary>
    /// <param name="dt">待处理的 DataTable。</param>
    /// <returns>行数，如果为 null 返回 0。</returns>
    public static int GetRowCount(this DataTable dt) =>
        dt?.Rows.Count ?? 0;

    /// <summary>
    /// 获取 DataTable 的列数。
    /// </summary>
    /// <param name="dt">待处理的 DataTable。</param>
    /// <returns>列数，如果为 null 返回 0。</returns>
    public static int GetColumnCount(this DataTable dt) =>
        dt?.Columns.Count ?? 0;

    #endregion

    #region 转换操作

    /// <summary>
    /// 将 DataTable 转换为泛型集合。
    /// </summary>
    /// <typeparam name="T">目标类型。</typeparam>
    /// <param name="dt">待转换的 DataTable。</param>
    /// <param name="converter">行转换委托。</param>
    /// <returns>转换后的泛型集合。</returns>
    /// <example>
    /// <code>
    /// var list = dt.ToList(row => new Person { Name = row["Name"].ToString(), Age = Convert.ToInt32(row["Age"]) });
    /// </code>
    /// </example>
    public static List<T> ToList<T>(this DataTable dt, Func<DataRow, T> converter)
    {
        if (dt == null || converter == null) return new List<T>();
        return dt.Rows.Cast<DataRow>().Select(converter).ToList();
    }

    /// <summary>
    /// 将 DataTable 转换为泛型集合（自动映射）。
    /// </summary>
    /// <typeparam name="T">目标类型。</typeparam>
    /// <param name="dt">待转换的 DataTable。</param>
    /// <returns>转换后的泛型集合。</returns>
    public static List<T> ToList<T>(this DataTable dt) where T : new()
    {
        if (dt == null || dt.Rows.Count == 0) return new List<T>();
        var list = new List<T>();
        var properties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
        foreach (DataRow row in dt.Rows)
        {
            var entity = new T();
            foreach (var prop in properties)
            {
                if (!dt.Columns.Contains(prop.Name)) continue;
                var value = row[prop.Name];
                if (value == DBNull.Value) continue;
                try
                {
                    var targetType = Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType;
                    var convertedValue = Convert.ChangeType(value, targetType);
                    prop.SetValue(entity, convertedValue, null);
                }
                catch { }
            }
            list.Add(entity);
        }
        return list;
    }

    /// <summary>
    /// 获取 DataTable 的所有列名。
    /// </summary>
    /// <param name="dt">待处理的 DataTable。</param>
    /// <returns>列名列表。</returns>
    public static List<string> GetColumnNames(this DataTable dt)
    {
        if (dt == null) return new List<string>();
        return dt.Columns.Cast<DataColumn>().Select(c => c.ColumnName).ToList();
    }

    /// <summary>
    /// 获取 DataTable 的所有列类型。
    /// </summary>
    /// <param name="dt">待处理的 DataTable。</param>
    /// <returns>列类型字典（列名 -> 类型）。</returns>
    public static Dictionary<string, Type> GetColumnTypes(this DataTable dt)
    {
        var dict = new Dictionary<string, Type>();
        if (dt == null) return dict;
        foreach (DataColumn col in dt.Columns)
        {
            dict[col.ColumnName] = col.DataType;
        }
        return dict;
    }

    /// <summary>
    /// 获取 DataTable 的所有行数据（每行为字典）。
    /// </summary>
    /// <param name="dt">待处理的 DataTable。</param>
    /// <returns>字典列表。</returns>
    public static List<Dictionary<string, object>> ToDictionaryList(this DataTable dt)
    {
        var list = new List<Dictionary<string, object>>();
        if (dt == null) return list;
        foreach (DataRow row in dt.Rows)
        {
            var dict = new Dictionary<string, object>();
            foreach (DataColumn col in dt.Columns)
            {
                dict[col.ColumnName] = row[col] == DBNull.Value ? null : row[col];
            }
            list.Add(dict);
        }
        return list;
    }

    /// <summary>
    /// DataTable 转为二维数组。
    /// </summary>
    /// <param name="dt">待转换的 DataTable。</param>
    /// <returns>二维数组。</returns>
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

    /// <summary>
    /// DataTable 转为 CSV 格式字符串。
    /// </summary>
    /// <param name="dt">待转换的 DataTable。</param>
    /// <param name="includeHeader">是否包含列头。</param>
    /// <param name="separator">分隔符，默认逗号。</param>
    /// <returns>CSV 格式字符串。</returns>
    public static string ToCsv(this DataTable dt, bool includeHeader = true, char separator = ',')
    {
        if (dt == null) return string.Empty;
        var sb = new StringBuilder();
        if (includeHeader)
        {
            var headers = dt.Columns.Cast<DataColumn>()
                .Select(c => EscapeCsvField(c.ColumnName, separator));
            sb.AppendLine(string.Join(separator, headers));
        }
        foreach (DataRow row in dt.Rows)
        {
            var fields = row.ItemArray.Select(f => EscapeCsvField(f?.ToString() ?? string.Empty, separator));
            sb.AppendLine(string.Join(separator, fields));
        }
        return sb.ToString();
    }

    /// <summary>
    /// DataTable 转为 JSON 格式字符串。
    /// </summary>
    /// <param name="dt">待转换的 DataTable。</param>
    /// <returns>JSON 格式字符串。</returns>
    public static string ToJson(this DataTable dt)
    {
        if (dt == null) return "[]";
        var list = dt.ToDictionaryList();
        return System.Text.Json.JsonSerializer.Serialize(list);
    }

    #endregion

    #region 查询筛选

    /// <summary>
    /// DataTable 按条件筛选，返回新 DataTable。
    /// </summary>
    /// <param name="dt">待筛选的 DataTable。</param>
    /// <param name="filter">筛选表达式，如 "Age &gt; 18 AND Name LIKE '张%'"。</param>
    /// <returns>筛选后的新 DataTable。</returns>
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
    /// <returns>排序后的新 DataTable。</returns>
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
    /// DataTable 按条件筛选并排序，返回新 DataTable。
    /// </summary>
    /// <param name="dt">待处理的 DataTable。</param>
    /// <param name="filter">筛选表达式。</param>
    /// <param name="sort">排序表达式。</param>
    /// <returns>处理后的新 DataTable。</returns>
    public static DataTable FilterAndSort(this DataTable dt, string filter, string sort)
    {
        if (dt == null) return dt;
        var rows = dt.Select(filter ?? "", sort ?? "");
        if (rows.Length == 0) return dt.Clone();
        var newDt = dt.Clone();
        foreach (var row in rows)
            newDt.ImportRow(row);
        return newDt;
    }

    /// <summary>
    /// 获取满足条件的 DataRow 数组。
    /// </summary>
    /// <param name="dt">待查询的 DataTable。</param>
    /// <param name="filter">筛选表达式。</param>
    /// <returns>满足条件的 DataRow 数组。</returns>
    public static DataRow[] SelectRows(this DataTable dt, string filter)
    {
        if (dt == null) return Array.Empty<DataRow>();
        return dt.Select(filter ?? "");
    }

    /// <summary>
    /// DataTable 按指定列去重，返回新 DataTable。
    /// </summary>
    /// <param name="dt">待去重的 DataTable。</param>
    /// <param name="columnNames">去重依据的列名数组。</param>
    /// <returns>去重后的新 DataTable。</returns>
    public static DataTable Distinct(this DataTable dt, params string[] columnNames)
    {
        if (dt == null) return null;
        if (columnNames == null || columnNames.Length == 0)
            columnNames = dt.Columns.Cast<DataColumn>().Select(c => c.ColumnName).ToArray();
        var newDt = dt.Clone();
        var seen = new HashSet<string>();
        foreach (DataRow row in dt.Rows)
        {
            var key = string.Join("|", columnNames.Select(c => row[c]?.ToString() ?? ""));
            if (seen.Add(key))
                newDt.ImportRow(row);
        }
        return newDt;
    }

    /// <summary>
    /// 获取 DataTable 前 N 行数据。
    /// </summary>
    /// <param name="dt">待处理的 DataTable。</param>
    /// <param name="count">行数。</param>
    /// <returns>前 N 行的新 DataTable。</returns>
    public static DataTable Top(this DataTable dt, int count)
    {
        if (dt == null || count <= 0) return dt?.Clone();
        var newDt = dt.Clone();
        for (int i = 0; i < Math.Min(count, dt.Rows.Count); i++)
            newDt.ImportRow(dt.Rows[i]);
        return newDt;
    }

    /// <summary>
    /// 获取 DataTable 后 N 行数据。
    /// </summary>
    /// <param name="dt">待处理的 DataTable。</param>
    /// <param name="count">行数。</param>
    /// <returns>后 N 行的新 DataTable。</returns>
    public static DataTable Bottom(this DataTable dt, int count)
    {
        if (dt == null || count <= 0) return dt?.Clone();
        var newDt = dt.Clone();
        var start = Math.Max(0, dt.Rows.Count - count);
        for (int i = start; i < dt.Rows.Count; i++)
            newDt.ImportRow(dt.Rows[i]);
        return newDt;
    }

    /// <summary>
    /// 获取 DataTable 指定范围的行数据。
    /// </summary>
    /// <param name="dt">待处理的 DataTable。</param>
    /// <param name="startIndex">起始索引（从 0 开始）。</param>
    /// <param name="count">行数。</param>
    /// <returns>指定范围的新 DataTable。</returns>
    public static DataTable Range(this DataTable dt, int startIndex, int count)
    {
        if (dt == null || startIndex < 0 || count <= 0) return dt?.Clone();
        var newDt = dt.Clone();
        for (int i = startIndex; i < Math.Min(startIndex + count, dt.Rows.Count); i++)
            newDt.ImportRow(dt.Rows[i]);
        return newDt;
    }

    #endregion

    #region 聚合统计

    /// <summary>
    /// 计算指定列的总和。
    /// </summary>
    /// <param name="dt">待计算的 DataTable。</param>
    /// <param name="columnName">列名。</param>
    /// <param name="filter">筛选条件（可选）。</param>
    /// <returns>总和。</returns>
    public static decimal Sum(this DataTable dt, string columnName, string filter = null)
    {
        if (dt == null || !dt.Columns.Contains(columnName)) return 0;
        var result = dt.Compute($"SUM({columnName})", filter ?? "");
        return result == DBNull.Value ? 0 : Convert.ToDecimal(result);
    }

    /// <summary>
    /// 计算指定列的平均值。
    /// </summary>
    /// <param name="dt">待计算的 DataTable。</param>
    /// <param name="columnName">列名。</param>
    /// <param name="filter">筛选条件（可选）。</param>
    /// <returns>平均值。</returns>
    public static decimal Average(this DataTable dt, string columnName, string filter = null)
    {
        if (dt == null || !dt.Columns.Contains(columnName)) return 0;
        var result = dt.Compute($"AVG({columnName})", filter ?? "");
        return result == DBNull.Value ? 0 : Convert.ToDecimal(result);
    }

    /// <summary>
    /// 获取指定列的最小值。
    /// </summary>
    /// <param name="dt">待处理的 DataTable。</param>
    /// <param name="columnName">列名。</param>
    /// <param name="filter">筛选条件（可选）。</param>
    /// <returns>最小值。</returns>
    public static object Min(this DataTable dt, string columnName, string filter = null)
    {
        if (dt == null || !dt.Columns.Contains(columnName)) return null;
        var result = dt.Compute($"MIN({columnName})", filter ?? "");
        return result == DBNull.Value ? null : result;
    }

    /// <summary>
    /// 获取指定列的最大值。
    /// </summary>
    /// <param name="dt">待处理的 DataTable。</param>
    /// <param name="columnName">列名。</param>
    /// <param name="filter">筛选条件（可选）。</param>
    /// <returns>最大值。</returns>
    public static object Max(this DataTable dt, string columnName, string filter = null)
    {
        if (dt == null || !dt.Columns.Contains(columnName)) return null;
        var result = dt.Compute($"MAX({columnName})", filter ?? "");
        return result == DBNull.Value ? null : result;
    }

    /// <summary>
    /// 计算满足条件的行数。
    /// </summary>
    /// <param name="dt">待计算的 DataTable。</param>
    /// <param name="filter">筛选条件（可选）。</param>
    /// <returns>行数。</returns>
    public static int Count(this DataTable dt, string filter = null)
    {
        if (dt == null) return 0;
        var result = dt.Compute("COUNT(*)", filter ?? "");
        return result == DBNull.Value ? 0 : Convert.ToInt32(result);
    }

    /// <summary>
    /// 计算指定列的非空值数量。
    /// </summary>
    /// <param name="dt">待计算的 DataTable。</param>
    /// <param name="columnName">列名。</param>
    /// <param name="filter">筛选条件（可选）。</param>
    /// <returns>非空值数量。</returns>
    public static int CountNonNull(this DataTable dt, string columnName, string filter = null)
    {
        if (dt == null || !dt.Columns.Contains(columnName)) return 0;
        var result = dt.Compute($"COUNT({columnName})", filter ?? "");
        return result == DBNull.Value ? 0 : Convert.ToInt32(result);
    }

    /// <summary>
    /// 获取聚合统计结果。
    /// </summary>
    /// <param name="dt">待处理的 DataTable。</param>
    /// <param name="columnName">列名。</param>
    /// <param name="filter">筛选条件（可选）。</param>
    /// <returns>包含 Sum、Avg、Min、Max、Count 的字典。</returns>
    public static Dictionary<string, object> GetStatistics(this DataTable dt, string columnName, string filter = null)
    {
        var stats = new Dictionary<string, object>();
        if (dt == null || !dt.Columns.Contains(columnName))
        {
            stats["Sum"] = 0m;
            stats["Avg"] = 0m;
            stats["Min"] = null;
            stats["Max"] = null;
            stats["Count"] = 0;
            return stats;
        }
        stats["Sum"] = dt.Sum(columnName, filter);
        stats["Avg"] = dt.Average(columnName, filter);
        stats["Min"] = dt.Min(columnName, filter);
        stats["Max"] = dt.Max(columnName, filter);
        stats["Count"] = dt.Count(filter);
        return stats;
    }

    #endregion

    #region 行列操作

    /// <summary>
    /// DataTable 添加一行数据。
    /// </summary>
    /// <param name="dt">目标 DataTable。</param>
    /// <param name="values">各列的值，顺序需与列一致。</param>
    /// <example>
    /// <code>
    /// dt.AddRow("张三", 25, "北京");
    /// </code>
    /// </example>
    public static void AddRow(this DataTable dt, params object[] values)
    {
        if (dt == null || values == null) return;
        var row = dt.NewRow();
        for (int i = 0; i < Math.Min(dt.Columns.Count, values.Length); i++)
        {
            row[i] = values[i] ?? DBNull.Value;
        }
        dt.Rows.Add(row);
    }

    /// <summary>
    /// DataTable 添加一行数据（使用字典）。
    /// </summary>
    /// <param name="dt">目标 DataTable。</param>
    /// <param name="data">列名和值的字典。</param>
    public static void AddRow(this DataTable dt, Dictionary<string, object> data)
    {
        if (dt == null || data == null) return;
        var row = dt.NewRow();
        foreach (var kvp in data)
        {
            if (dt.Columns.Contains(kvp.Key))
                row[kvp.Key] = kvp.Value ?? DBNull.Value;
        }
        dt.Rows.Add(row);
    }

    /// <summary>
    /// DataTable 添加一列。
    /// </summary>
    /// <param name="dt">目标 DataTable。</param>
    /// <param name="columnName">列名。</param>
    /// <param name="type">列类型，默认 typeof(string)。</param>
    /// <param name="defaultValue">默认值。</param>
    public static void AddColumn(this DataTable dt, string columnName, Type type = null, object defaultValue = null)
    {
        if (dt == null || string.IsNullOrEmpty(columnName)) return;
        if (dt.Columns.Contains(columnName)) return;
        var col = new DataColumn(columnName, type ?? typeof(string));
        if (defaultValue != null)
            col.DefaultValue = defaultValue;
        dt.Columns.Add(col);
    }

    /// <summary>
    /// DataTable 删除指定列。
    /// </summary>
    /// <param name="dt">目标 DataTable。</param>
    /// <param name="columnName">列名。</param>
    public static void RemoveColumn(this DataTable dt, string columnName)
    {
        if (dt == null || string.IsNullOrEmpty(columnName)) return;
        if (dt.Columns.Contains(columnName))
            dt.Columns.Remove(columnName);
    }

    /// <summary>
    /// DataTable 重命名列。
    /// </summary>
    /// <param name="dt">目标 DataTable。</param>
    /// <param name="oldName">原列名。</param>
    /// <param name="newName">新列名。</param>
    public static void RenameColumn(this DataTable dt, string oldName, string newName)
    {
        if (dt == null || string.IsNullOrEmpty(oldName) || string.IsNullOrEmpty(newName)) return;
        if (dt.Columns.Contains(oldName))
            dt.Columns[oldName]!.ColumnName = newName;
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
    /// DataTable 删除满足条件的行。
    /// </summary>
    /// <param name="dt">目标 DataTable。</param>
    /// <param name="filter">筛选表达式。</param>
    public static void DeleteRows(this DataTable dt, string filter)
    {
        if (dt == null || string.IsNullOrWhiteSpace(filter)) return;
        var rows = dt.Select(filter);
        foreach (var row in rows)
            row.Delete();
        dt.AcceptChanges();
    }

    /// <summary>
    /// 设置指定单元格的值。
    /// </summary>
    /// <param name="dt">目标 DataTable。</param>
    /// <param name="rowIndex">行索引。</param>
    /// <param name="columnName">列名。</param>
    /// <param name="value">值。</param>
    public static void SetValue(this DataTable dt, int rowIndex, string columnName, object value)
    {
        if (dt == null || !dt.ContainsRow(rowIndex) || !dt.ContainsColumn(columnName)) return;
        dt.Rows[rowIndex][columnName] = value ?? DBNull.Value;
    }

    /// <summary>
    /// 获取指定单元格的值。
    /// </summary>
    /// <param name="dt">目标 DataTable。</param>
    /// <param name="rowIndex">行索引。</param>
    /// <param name="columnName">列名。</param>
    /// <returns>单元格值，如果不存在返回 null。</returns>
    public static object GetValue(this DataTable dt, int rowIndex, string columnName)
    {
        if (dt == null || !dt.ContainsRow(rowIndex) || !dt.ContainsColumn(columnName)) return null;
        var value = dt.Rows[rowIndex][columnName];
        return value == DBNull.Value ? null : value;
    }

    /// <summary>
    /// 获取指定单元格的值并转换为指定类型。
    /// </summary>
    /// <typeparam name="T">目标类型。</typeparam>
    /// <param name="dt">目标 DataTable。</param>
    /// <param name="rowIndex">行索引。</param>
    /// <param name="columnName">列名。</param>
    /// <param name="defaultValue">默认值。</param>
    /// <returns>转换后的值。</returns>
    public static T GetValue<T>(this DataTable dt, int rowIndex, string columnName, T defaultValue = default)
    {
        var value = dt.GetValue(rowIndex, columnName);
        if (value == null) return defaultValue;
        try
        {
            var targetType = Nullable.GetUnderlyingType(typeof(T)) ?? typeof(T);
            return (T)Convert.ChangeType(value, targetType);
        }
        catch
        {
            return defaultValue;
        }
    }

    #endregion

    #region 数据操作

    /// <summary>
    /// DataTable 克隆结构并复制所有数据。
    /// </summary>
    /// <param name="dt">待复制的 DataTable。</param>
    /// <returns>复制的 DataTable。</returns>
    public static DataTable CopyAll(this DataTable dt)
    {
        if (dt == null) return null;
        return dt.Copy();
    }

    /// <summary>
    /// DataTable 克隆结构但不复制数据。
    /// </summary>
    /// <param name="dt">待克隆的 DataTable。</param>
    /// <returns>克隆结构的 DataTable。</returns>
    public static DataTable CloneStructure(this DataTable dt)
    {
        if (dt == null) return null;
        return dt.Clone();
    }

    /// <summary>
    /// 合并两个 DataTable。
    /// </summary>
    /// <param name="dt">目标 DataTable。</param>
    /// <param name="other">待合并的 DataTable。</param>
    /// <param name="preserveChanges">是否保留现有更改。</param>
    /// <returns>合并后的 DataTable。</returns>
    public static DataTable Merge(this DataTable dt, DataTable other, bool preserveChanges = true)
    {
        if (dt == null) return other?.Copy();
        if (other == null) return dt.Copy();
        var result = dt.Copy();
        result.Merge(other, preserveChanges, MissingSchemaAction.Add);
        return result;
    }

    /// <summary>
    /// DataTable 转为一行一字典的枚举（便于遍历）。
    /// </summary>
    /// <param name="dt">待处理的 DataTable。</param>
    /// <returns>字典枚举。</returns>
    public static IEnumerable<IDictionary<string, object>> AsEnumerableDictionary(this DataTable dt)
    {
        if (dt == null) yield break;
        foreach (DataRow row in dt.Rows)
        {
            var dict = new Dictionary<string, object>();
            foreach (DataColumn col in dt.Columns)
            {
                dict[col.ColumnName] = row[col] == DBNull.Value ? null : row[col];
            }
            yield return dict;
        }
    }

    /// <summary>
    /// 将 DBNull 值转换为 null。
    /// </summary>
    /// <param name="dt">目标 DataTable。</param>
    public static void ConvertDbNullToNull(this DataTable dt)
    {
        if (dt == null) return;
        foreach (DataRow row in dt.Rows)
        {
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                if (row[i] == DBNull.Value)
                    row[i] = null;
            }
        }
    }

    #endregion

    #region 分组操作

    /// <summary>
    /// DataTable 按指定列分组。
    /// </summary>
    /// <param name="dt">待分组的 DataTable。</param>
    /// <param name="groupColumn">分组列名。</param>
    /// <returns>分组字典（分组键 -> 行列表）。</returns>
    public static Dictionary<object, List<DataRow>> GroupBy(this DataTable dt, string groupColumn)
    {
        var result = new Dictionary<object, List<DataRow>>();
        if (dt == null || !dt.Columns.Contains(groupColumn)) return result;
        foreach (DataRow row in dt.Rows)
        {
            var key = row[groupColumn] == DBNull.Value ? null : row[groupColumn];
            if (!result.ContainsKey(key))
                result[key] = new List<DataRow>();
            result[key].Add(row);
        }
        return result;
    }

    /// <summary>
    /// DataTable 按指定列分组并求和。
    /// </summary>
    /// <param name="dt">待分组的 DataTable。</param>
    /// <param name="groupColumn">分组列名。</param>
    /// <param name="sumColumn">求和列名。</param>
    /// <returns>分组求和结果 DataTable。</returns>
    public static DataTable GroupBySum(this DataTable dt, string groupColumn, string sumColumn)
    {
        var result = new DataTable();
        if (dt == null || !dt.Columns.Contains(groupColumn) || !dt.Columns.Contains(sumColumn))
            return result;
        result.Columns.Add(groupColumn, dt.Columns[groupColumn].DataType);
        result.Columns.Add($"{sumColumn}_Sum", typeof(decimal));
        var groups = dt.GroupBy(groupColumn);
        foreach (var group in groups)
        {
            var sum = group.Value.Sum(r => r[sumColumn] == DBNull.Value ? 0m : Convert.ToDecimal(r[sumColumn]));
            result.AddRow(group.Key, sum);
        }
        return result;
    }

    /// <summary>
    /// DataTable 按指定列分组并计数。
    /// </summary>
    /// <param name="dt">待分组的 DataTable。</param>
    /// <param name="groupColumn">分组列名。</param>
    /// <returns>分组计数结果 DataTable。</returns>
    public static DataTable GroupByCount(this DataTable dt, string groupColumn)
    {
        var result = new DataTable();
        if (dt == null || !dt.Columns.Contains(groupColumn))
            return result;
        result.Columns.Add(groupColumn, dt.Columns[groupColumn].DataType);
        result.Columns.Add("Count", typeof(int));
        var groups = dt.GroupBy(groupColumn);
        foreach (var group in groups)
        {
            result.AddRow(group.Key, group.Value.Count);
        }
        return result;
    }

    #endregion

    #region 分页操作

    /// <summary>
    /// 获取 DataTable 指定页的数据。
    /// </summary>
    /// <param name="dt">待分页的 DataTable。</param>
    /// <param name="pageIndex">页索引（从 1 开始）。</param>
    /// <param name="pageSize">每页大小。</param>
    /// <returns>指定页的 DataTable。</returns>
    public static DataTable GetPage(this DataTable dt, int pageIndex, int pageSize)
    {
        if (dt == null || pageIndex < 1 || pageSize < 1) return dt?.Clone();
        var startIndex = (pageIndex - 1) * pageSize;
        return dt.Range(startIndex, pageSize);
    }

    /// <summary>
    /// 获取 DataTable 分页数据信息。
    /// </summary>
    /// <param name="dt">待分页的 DataTable。</param>
    /// <param name="pageIndex">页索引（从 1 开始）。</param>
    /// <param name="pageSize">每页大小。</param>
    /// <returns>包含分页信息和数据的对象。</returns>
    public static PagedDataTable GetPagedData(this DataTable dt, int pageIndex, int pageSize)
    {
        var result = new PagedDataTable
        {
            PageIndex = pageIndex,
            PageSize = pageSize,
            TotalCount = dt?.Rows.Count ?? 0
        };
        result.TotalPages = result.PageSize > 0 ? (int)Math.Ceiling((double)result.TotalCount / result.PageSize) : 0;
        result.Data = dt.GetPage(pageIndex, pageSize);
        return result;
    }

    #endregion

    #region 实体转换

    /// <summary>
    /// 将 DataTable 的第一行转换为实体。
    /// </summary>
    /// <typeparam name="T">实体类型。</typeparam>
    /// <param name="dt">待转换的 DataTable。</param>
    /// <returns>实体对象，如果无数据返回默认值。</returns>
    public static T ToEntity<T>(this DataTable dt) where T : new()
    {
        if (dt == null || dt.Rows.Count == 0) return default;
        return dt.ToList<T>().FirstOrDefault();
    }

    /// <summary>
    /// 将 DataTable 转换为实体列表。
    /// </summary>
    /// <typeparam name="T">实体类型。</typeparam>
    /// <param name="dt">待转换的 DataTable。</param>
    /// <returns>实体列表。</returns>
    public static List<T> ToEntityList<T>(this DataTable dt) where T : new()
    {
        return dt.ToList<T>();
    }

    /// <summary>
    /// 从实体列表创建 DataTable。
    /// </summary>
    /// <typeparam name="T">实体类型。</typeparam>
    /// <param name="entities">实体列表。</param>
    /// <param name="tableName">表名。</param>
    /// <returns>创建的 DataTable。</returns>
    public static DataTable FromEntityList<T>(this IEnumerable<T> entities, string tableName = "Table")
    {
        var dt = new DataTable(tableName);
        if (entities == null) return dt;
        var properties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
        foreach (var prop in properties)
        {
            var type = Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType;
            dt.Columns.Add(prop.Name, type);
        }
        foreach (var entity in entities)
        {
            var row = dt.NewRow();
            foreach (var prop in properties)
            {
                var value = prop.GetValue(entity, null);
                row[prop.Name] = value ?? DBNull.Value;
            }
            dt.Rows.Add(row);
        }
        return dt;
    }

    #endregion

    #region 辅助方法

    /// <summary>
    /// 转义 CSV 字段。
    /// </summary>
    private static string EscapeCsvField(string field, char separator)
    {
        if (field.Contains(separator) || field.Contains("\"") || field.Contains("\n") || field.Contains("\r"))
        {
            return $"\"{field.Replace("\"", "\"\"")}\"";
        }
        return field;
    }

    #endregion
}

/// <summary>
/// 分页 DataTable 结果。
/// </summary>
public class PagedDataTable
{
    /// <summary>
    /// 当前页索引（从 1 开始）。
    /// </summary>
    public int PageIndex { get; set; }

    /// <summary>
    /// 每页大小。
    /// </summary>
    public int PageSize { get; set; }

    /// <summary>
    /// 总记录数。
    /// </summary>
    public int TotalCount { get; set; }

    /// <summary>
    /// 总页数。
    /// </summary>
    public int TotalPages { get; set; }

    /// <summary>
    /// 当前页数据。
    /// </summary>
    public DataTable Data { get; set; }

    /// <summary>
    /// 是否有上一页。
    /// </summary>
    public bool HasPreviousPage => PageIndex > 1;

    /// <summary>
    /// 是否有下一页。
    /// </summary>
    public bool HasNextPage => PageIndex < TotalPages;
}
