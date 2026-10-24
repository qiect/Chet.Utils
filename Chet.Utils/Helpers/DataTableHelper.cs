using System.Data;
using System.Dynamic;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace Chet.Utils.Helpers;


/// <summary>
/// DataTable 高级操作帮助类，提供 <see cref="DataTableExtensions"/> 中未包含的高级数据处理功能。
/// </summary>
/// <remarks>
/// <para>本类提供以下 DataTableExtensions 中未包含的功能：</para>
/// <list type="bullet">
///   <item><description>数据转换：ToDynamicList（动态对象）、ToDataTableFromJson（JSON转表）、MapColumns（列名映射）、MapToEntityList（实体映射）</description></item>
///   <item><description>数据透视：Pivot（透视表）、AdvancedPivot（高级透视）、GroupByAdvanced（高级分组聚合）</description></item>
///   <item><description>数据分析：CalculateStatistics（完整统计）、AnalyzeDistribution（分布分析）、CalculateCorrelation（相关性）</description></item>
///   <item><description>数据导出：ExportToHtml（HTML表格）、ExportToExcelXml（Excel XML）、ExportToMarkdown（Markdown）</description></item>
///   <item><description>数据清洗：ValidateData（数据验证）、CleanData（数据清洗）、FillMissingValues（填充缺失值）、DetectOutliers（异常值检测）</description></item>
///   <item><description>数据连接：InnerJoin（内连接）、LeftJoin（左连接）、FullJoin（全连接）、CrossJoin（交叉连接）、UnionAll（垂直合并）、MergeHorizontally（水平合并）</description></item>
///   <item><description>高级查询：Query（SQL风格查询）、Search（全文搜索）、RegexFilter（正则筛选）</description></item>
/// </list>
/// <para>基础功能请使用 <see cref="DataTableExtensions"/> 扩展方法。</para>
/// </remarks>
public static partial class DataTableHelper
{
    #region 数据转换

    /// <summary>
    /// 将 <see cref="DataTable"/> 转换为动态对象列表。
    /// </summary>
    /// <param name="dataTable">源 <see cref="DataTable"/>。</param>
    /// <returns>动态对象列表，每行转换为一个 <see cref="ExpandoObject"/>。</returns>
    /// <exception cref="ArgumentNullException"><paramref name="dataTable"/> 为 null 时抛出。</exception>
    /// <example>
    /// <code>
    /// var dt = new DataTable();
    /// dt.Columns.Add("Name", typeof(string));
    /// dt.Columns.Add("Age", typeof(int));
    /// dt.Rows.Add("张三", 25);
    /// 
    /// var list = DataTableHelper.ToDynamicList(dt);
    /// foreach (dynamic item in list)
    /// {
    ///     Console.WriteLine($"{item.Name}: {item.Age}"); // 输出: 张三: 25
    /// }
    /// </code>
    /// </example>
    public static List<dynamic> ToDynamicList(DataTable dataTable)
    {
        ThrowHelper.ThrowIfNull(dataTable);

        var result = new List<dynamic>();

        foreach (DataRow row in dataTable.Rows)
        {
            dynamic dynamicObject = new ExpandoObject();
            var dict = (IDictionary<string, object>)dynamicObject;

            foreach (DataColumn column in dataTable.Columns)
            {
                dict[column.ColumnName] = row[column] == DBNull.Value ? null : row[column];
            }

            result.Add(dynamicObject);
        }

        return result;
    }

    /// <summary>
    /// 将 JSON 字符串转换为 <see cref="DataTable"/>，所有值转为字符串。
    /// </summary>
    /// <param name="json">JSON 字符串，必须是对象数组格式。</param>
    /// <returns>转换后的 <see cref="DataTable"/>，列类型均为 <see cref="string"/>。</returns>
    /// <exception cref="ArgumentException"><paramref name="json"/> 为 null 或空字符串时抛出。</exception>
    /// <exception cref="InvalidOperationException">JSON 解析失败时抛出。</exception>
    /// <example>
    /// <code>
    /// string json = "[{\"Name\":\"张三\",\"Age\":25},{\"Name\":\"李四\",\"Age\":30}]";
    /// var dt = DataTableHelper.ToDataTableFromJson(json);
    /// // dt.Rows[0]["Age"] => "25"（字符串）
    /// </code>
    /// </example>
    public static DataTable ToDataTableFromJson(string json)
    {
        return ToDataTableFromJson(json, preserveTypes: false);
    }

    /// <summary>
    /// 将 JSON 字符串转换为 <see cref="DataTable"/>。
    /// </summary>
    /// <param name="json">JSON 字符串，必须是对象数组格式。</param>
    /// <param name="preserveTypes">
    /// 是否保留 JSON 值的原始类型。<see langword="true"/> 时列类型为 <see cref="object"/>，值保留为 long、double、bool、DateTime 等；
    /// <see langword="false"/> 时列类型为 <see cref="string"/>，所有值转为字符串。
    /// </param>
    /// <returns>转换后的 <see cref="DataTable"/>。</returns>
    /// <exception cref="ArgumentException"><paramref name="json"/> 为 null 或空字符串时抛出。</exception>
    /// <exception cref="InvalidOperationException">JSON 解析失败时抛出。</exception>
    /// <example>
    /// <code>
    /// string json = "[{\"Name\":\"张三\",\"Age\":25},{\"Name\":\"李四\",\"Age\":30}]";
    /// 
    /// // 保留类型
    /// var dt = DataTableHelper.ToDataTableFromJson(json, preserveTypes: true);
    /// // dt.Rows[0]["Age"] => 25L（long）
    /// 
    /// // 全转字符串
    /// var dt2 = DataTableHelper.ToDataTableFromJson(json, preserveTypes: false);
    /// // dt2.Rows[0]["Age"] => "25"（string）
    /// </code>
    /// </example>
    public static DataTable ToDataTableFromJson(string json, bool preserveTypes)
    {
        if (string.IsNullOrWhiteSpace(json))
            throw new ArgumentException("JSON 字符串不能为空", nameof(json));

        var dataTable = new DataTable();

        try
        {
            var jsonArray = JsonSerializer.Deserialize<List<Dictionary<string, JsonElement>>>(json);

            if (jsonArray == null || jsonArray.Count == 0)
                return dataTable;

            var columnNames = new HashSet<string>();
            foreach (var item in jsonArray)
            {
                foreach (var key in item.Keys)
                {
                    columnNames.Add(key);
                }
            }

            var columnType = preserveTypes ? typeof(object) : typeof(string);
            foreach (var columnName in columnNames)
            {
                dataTable.Columns.Add(columnName, columnType);
            }

            dataTable.MinimumCapacity = jsonArray.Count;
            dataTable.BeginLoadData();

            foreach (var item in jsonArray)
            {
                var row = dataTable.NewRow();
                foreach (var columnName in columnNames)
                {
                    if (item.TryGetValue(columnName, out var element))
                    {
                        row[columnName] = preserveTypes
                            ? ConvertJsonElementToObject(element)
                            : (element.ValueKind == JsonValueKind.Null ? DBNull.Value : (object)element.ToString());
                    }
                    else
                    {
                        row[columnName] = DBNull.Value;
                    }
                }
                dataTable.Rows.Add(row);
            }

            dataTable.EndLoadData();
        }
        catch (JsonException ex)
        {
            throw new InvalidOperationException("JSON 解析失败，请确保格式正确", ex);
        }

        return dataTable;
    }



    /// <summary>
    /// 映射 <see cref="DataTable"/> 的列名。
    /// </summary>
    /// <param name="dataTable">源 <see cref="DataTable"/>。</param>
    /// <param name="columnMappings">列映射字典（原列名 -> 新列名）。</param>
    /// <returns>映射后的新 <see cref="DataTable"/>。</returns>
    /// <exception cref="ArgumentNullException"><paramref name="dataTable"/> 或 <paramref name="columnMappings"/> 为 null 时抛出。</exception>
    /// <example>
    /// <code>
    /// var mappings = new Dictionary<string, string>
    /// {
    ///     { "user_name", "UserName" },
    ///     { "create_time", "CreateTime" }
    /// };
    /// var mappedDt = DataTableHelper.MapColumns(dt, mappings);
    /// </code>
    /// </example>
    public static DataTable MapColumns(DataTable dataTable, Dictionary<string, string> columnMappings)
    {
        ThrowHelper.ThrowIfNull(dataTable);
        ThrowHelper.ThrowIfNull(columnMappings);

        var result = dataTable.Copy();

        foreach (var mapping in columnMappings)
        {
            if (result.Columns.Contains(mapping.Key) && !string.IsNullOrWhiteSpace(mapping.Value))
            {
                result.Columns[mapping.Key].ColumnName = mapping.Value;
            }
        }

        return result;
    }

    /// <summary>
    /// 将对象列表转换为 <see cref="DataTable"/>。
    /// </summary>
    /// <typeparam name="T">对象类型。</typeparam>
    /// <param name="objects">对象列表。</param>
    /// <param name="tableName">表名，默认为类型名称。</param>
    /// <returns>转换后的 <see cref="DataTable"/>。</returns>
    /// <exception cref="ArgumentNullException"><paramref name="objects"/> 为 null 时抛出。</exception>
    /// <example>
    /// <code>
    /// var users = new List<User> { new User { Name = "张三", Age = 25 } };
    /// var dt = DataTableHelper.ToDataTableFromObjects(users, "Users");
    /// </code>
    /// </example>
    public static DataTable ToDataTableFromObjects<T>(IEnumerable<T> objects, string tableName = null)
    {
        ThrowHelper.ThrowIfNull(objects);

        var dataTable = new DataTable(tableName ?? typeof(T).Name);
        var properties = typeof(T).GetProperties(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance);

        foreach (var prop in properties)
        {
            var propType = Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType;
            dataTable.Columns.Add(prop.Name, propType);
        }

        foreach (var obj in objects)
        {
            var row = dataTable.NewRow();
            foreach (var prop in properties)
            {
                var value = prop.GetValue(obj);
                row[prop.Name] = value ?? DBNull.Value;
            }
            dataTable.Rows.Add(row);
        }

        return dataTable;
    }

    /// <summary>
    /// 将 <see cref="DataTable"/> 中的数据映射为指定类型的实体对象列表。
    /// 映射关系通过字段映射字典指定，仅处理 DataTable 中存在且目标类型中也存在的属性。
    /// </summary>
    /// <typeparam name="T">目标实体类型，必须具有无参构造函数。</typeparam>
    /// <param name="dataTable">源 <see cref="DataTable"/>，包含待转换的行数据。</param>
    /// <param name="fieldMapping">字段映射字典，键为 DataTable 列名，值为目标类型的属性名。</param>
    /// <returns>转换后的实体对象列表。</returns>
    /// <exception cref="ArgumentNullException"><paramref name="dataTable"/> 或 <paramref name="fieldMapping"/> 为 null 时抛出。</exception>
    /// <example>
    /// <code>
    /// var dt = new DataTable();
    /// dt.Columns.Add("UserName", typeof(string));
    /// dt.Columns.Add("UserAge", typeof(int));
    /// dt.Rows.Add("张三", 25);
    /// 
    /// var mapping = new Dictionary<string, string>
    /// {
    ///     { "UserName", "Name" },
    ///     { "UserAge", "Age" }
    /// };
    /// 
    /// var list = DataTableHelper.MapToEntityList<Person>(dt, mapping);
    /// // list[0].Name => "张三"
    /// // list[0].Age => 25
    /// </code>
    /// </example>
    public static List<T> MapToEntityList<T>(DataTable dataTable, Dictionary<string, string> fieldMapping) where T : new()
    {
        ThrowHelper.ThrowIfNull(dataTable);
        ThrowHelper.ThrowIfNull(fieldMapping);

        if (dataTable.Rows.Count == 0)
            return new List<T>();

        var existingColumns = new HashSet<string>(dataTable.Columns.Cast<DataColumn>().Select(c => c.ColumnName));

        var validMapping = new List<(string SourceColumn, PropertyInfo Property)>();
        foreach (var mapping in fieldMapping)
        {
            if (!existingColumns.Contains(mapping.Key)) continue;
            var prop = typeof(T).GetProperty(mapping.Value);
            if (prop != null && prop.CanWrite)
                validMapping.Add((mapping.Key, prop));
        }

        if (validMapping.Count == 0)
        {
            Console.WriteLine("警告: 没有有效的映射关系");
            return new List<T>();
        }

        var result = new List<T>(dataTable.Rows.Count);

        foreach (DataRow row in dataTable.Rows)
        {
            try
            {
                var entity = new T();

                foreach (var (sourceColumn, property) in validMapping)
                {
                    var value = row[sourceColumn];
                    if (value != DBNull.Value)
                    {
                        property.SetValue(entity, ConvertValueSafe(value, property.PropertyType));
                    }
                }

                result.Add(entity);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"处理行时出错: {ex.Message}");
            }
        }

        return result;
    }

    #endregion

    #region 数据透视

    /// <summary>
    /// 创建数据透视表。
    /// </summary>
    /// <param name="dataTable">源 <see cref="DataTable"/>。</param>
    /// <param name="rowField">行字段名。</param>
    /// <param name="columnField">列字段名。</param>
    /// <param name="dataField">数据字段名。</param>
    /// <param name="aggregateFunction">聚合函数，默认为求和。</param>
    /// <returns>透视后的 <see cref="DataTable"/>。</returns>
    /// <exception cref="ArgumentNullException"><paramref name="dataTable"/> 为 null 时抛出。</exception>
    /// <exception cref="ArgumentException">字段名不存在时抛出。</exception>
    /// <example>
    /// <code>
    /// // 销售数据透视：按年份（行）和产品（列）统计销售额
    /// var pivotTable = DataTableHelper.Pivot(
    ///     salesDt, 
    ///     "Year", 
    ///     "Product", 
    ///     "Amount",
    ///     values => values.Sum(v => Convert.ToDecimal(v))
    /// );
    /// </code>
    /// </example>
    public static DataTable Pivot(
        DataTable dataTable,
        string rowField,
        string columnField,
        string dataField,
        Func<IEnumerable<object>, object> aggregateFunction = null)
    {
        ThrowHelper.ThrowIfNull(dataTable);

        if (!dataTable.Columns.Contains(rowField))
            throw new ArgumentException($"列 '{rowField}' 不存在", nameof(rowField));
        if (!dataTable.Columns.Contains(columnField))
            throw new ArgumentException($"列 '{columnField}' 不存在", nameof(columnField));
        if (!dataTable.Columns.Contains(dataField))
            throw new ArgumentException($"列 '{dataField}' 不存在", nameof(dataField));

        aggregateFunction ??= DefaultSumAggregate;

        var result = new DataTable();

        var rowValues = dataTable.AsEnumerable()
            .Select(row => row.Field<object>(rowField))
            .Where(v => v != null && v != DBNull.Value)
            .Distinct()
            .OrderBy(v => v)
            .ToList();

        var columnValues = dataTable.AsEnumerable()
            .Select(row => row.Field<object>(columnField))
            .Where(v => v != null && v != DBNull.Value)
            .Distinct()
            .OrderBy(v => v)
            .ToList();

        result.Columns.Add(rowField);
        foreach (var colValue in columnValues)
        {
            result.Columns.Add(colValue?.ToString() ?? "NULL");
        }

        foreach (var rowValue in rowValues)
        {
            var newRow = result.NewRow();
            newRow[rowField] = rowValue;

            foreach (var colValue in columnValues)
            {
                var cellData = dataTable.AsEnumerable()
                    .Where(row => Equals(row.Field<object>(rowField), rowValue) &&
                                 Equals(row.Field<object>(columnField), colValue))
                    .Select(row => row.Field<object>(dataField));

                newRow[colValue?.ToString() ?? "NULL"] = aggregateFunction(cellData) ?? DBNull.Value;
            }

            result.Rows.Add(newRow);
        }

        return result;
    }

    /// <summary>
    /// 创建高级数据透视表，支持多行字段、多列字段和多数据字段。
    /// </summary>
    /// <param name="dataTable">源 <see cref="DataTable"/>。</param>
    /// <param name="rowFields">行字段数组。</param>
    /// <param name="columnFields">列字段数组。</param>
    /// <param name="dataFields">数据字段配置（字段名 -> 聚合函数）。</param>
    /// <returns>高级透视表。</returns>
    /// <exception cref="ArgumentNullException"><paramref name="dataTable"/> 为 null 时抛出。</exception>
    /// <exception cref="ArgumentException">字段名不存在时抛出。</exception>
    /// <example>
    /// <code>
    /// var dataFields = new Dictionary<string, Func<IEnumerable<object>, object>>
    /// {
    ///     { "Amount", values => values.Sum(v => Convert.ToDecimal(v)) },
    ///     { "Quantity", values => values.Sum(v => Convert.ToInt32(v)) }
    /// };
    /// var pivot = DataTableHelper.AdvancedPivot(
    ///     salesDt,
    ///     new[] { "Year", "Quarter" },
    ///     new[] { "Product", "Region" },
    ///     dataFields
    /// );
    /// </code>
    /// </example>
    public static DataTable AdvancedPivot(
        DataTable dataTable,
        string[] rowFields,
        string[] columnFields,
        Dictionary<string, Func<IEnumerable<object>, object>> dataFields)
    {
        ThrowHelper.ThrowIfNull(dataTable);
        ThrowHelper.ThrowIfNull(rowFields);
        ThrowHelper.ThrowIfNull(columnFields);
        ThrowHelper.ThrowIfNull(dataFields);

        foreach (var field in rowFields.Concat(columnFields).Concat(dataFields.Keys))
        {
            if (!dataTable.Columns.Contains(field))
                throw new ArgumentException($"列 '{field}' 不存在");
        }

        var result = new DataTable();

        foreach (var rowField in rowFields)
        {
            result.Columns.Add(rowField, dataTable.Columns[rowField].DataType);
        }

        var columnCombinations = GetColumnCombinations(dataTable, columnFields);

        foreach (var dataField in dataFields)
        {
            foreach (var combination in columnCombinations)
            {
                var columnName = columnFields.Length > 0
                    ? $"{dataField.Key}_{combination}"
                    : dataField.Key;
                result.Columns.Add(columnName);
            }
        }

        var rowCombinations = dataTable.AsEnumerable()
            .GroupBy(row => rowFields.Select(f => row[f]).ToArray(), new ArrayEqualityComparer())
            .Select(g => g.Key)
            .ToList();

        foreach (var rowCombination in rowCombinations)
        {
            var newRow = result.NewRow();

            for (int i = 0; i < rowFields.Length; i++)
            {
                newRow[rowFields[i]] = rowCombination[i] ?? DBNull.Value;
            }

            foreach (var dataField in dataFields)
            {
                if (columnFields.Length == 0)
                {
                    var filteredData = dataTable.AsEnumerable()
                        .Where(row => rowFields.Select((f, i) => Equals(row[f], rowCombination[i])).All(x => x))
                        .Select(row => row[dataField.Key]);
                    newRow[dataField.Key] = dataField.Value(filteredData) ?? DBNull.Value;
                }
                else
                {
                    foreach (var columnCombination in columnCombinations)
                    {
                        var filterValues = columnCombination.Split('_');
                        var columnName = $"{dataField.Key}_{columnCombination}";

                        var filteredData = dataTable.AsEnumerable()
                            .Where(row =>
                            {
                                for (int i = 0; i < rowFields.Length; i++)
                                {
                                    if (!Equals(row[rowFields[i]], rowCombination[i]))
                                        return false;
                                }

                                for (int i = 0; i < columnFields.Length; i++)
                                {
                                    if (!Equals(row[columnFields[i]]?.ToString(), filterValues[i]))
                                        return false;
                                }

                                return true;
                            })
                            .Select(row => row[dataField.Key]);

                        newRow[columnName] = dataField.Value(filteredData) ?? DBNull.Value;
                    }
                }
            }

            result.Rows.Add(newRow);
        }

        return result;
    }

    /// <summary>
    /// 高级分组聚合操作，支持多个分组列和多个聚合列。
    /// </summary>
    /// <param name="dataTable">源 <see cref="DataTable"/>。</param>
    /// <param name="groupByColumns">分组列名数组。</param>
    /// <param name="aggregateColumns">聚合列配置（结果列名 -> 聚合函数）。</param>
    /// <returns>分组聚合后的 <see cref="DataTable"/>。</returns>
    /// <exception cref="ArgumentNullException">参数为 null 时抛出。</exception>
    /// <example>
    /// <code>
    /// var aggregates = new Dictionary<string, Func<IEnumerable<object>, object>>
    /// {
    ///     { "TotalAmount", values => values.Sum(v => Convert.ToDecimal(v)) },
    ///     { "AvgPrice", values => values.Average(v => Convert.ToDecimal(v)) },
    ///     { "Count", values => values.Count() }
    /// };
    /// var grouped = DataTableHelper.GroupByAdvanced(
    ///     salesDt,
    ///     new[] { "Product", "Region" },
    ///     aggregates
    /// );
    /// </code>
    /// </example>
    public static DataTable GroupByAdvanced(
        DataTable dataTable,
        string[] groupByColumns,
        Dictionary<string, Func<IEnumerable<object>, object>> aggregateColumns)
    {
        ThrowHelper.ThrowIfNull(dataTable);
        ThrowHelper.ThrowIfNull(groupByColumns);
        ThrowHelper.ThrowIfNull(aggregateColumns);

        var result = new DataTable();

        foreach (var col in groupByColumns)
        {
            if (!dataTable.Columns.Contains(col))
                throw new ArgumentException($"分组列 '{col}' 不存在");

            result.Columns.Add(col, dataTable.Columns[col].DataType);
        }

        foreach (var agg in aggregateColumns)
        {
            result.Columns.Add(agg.Key);
        }

        var grouped = dataTable.AsEnumerable()
            .GroupBy(row => groupByColumns.Select(col => row[col]).ToArray(), new ArrayEqualityComparer());

        foreach (var group in grouped)
        {
            var newRow = result.NewRow();

            for (int i = 0; i < groupByColumns.Length; i++)
            {
                newRow[groupByColumns[i]] = group.Key[i] ?? DBNull.Value;
            }

            foreach (var agg in aggregateColumns)
            {
                var columnData = group.Select(row => row[agg.Key]);
                newRow[agg.Key] = agg.Value(columnData) ?? DBNull.Value;
            }

            result.Rows.Add(newRow);
        }

        return result;
    }

    #endregion

    #region 数据分析

    /// <summary>
    /// 计算指定列的完整统计信息。
    /// </summary>
    /// <param name="dataTable">源 <see cref="DataTable"/>。</param>
    /// <param name="columnName">列名。</param>
    /// <returns>统计信息对象。</returns>
    /// <exception cref="ArgumentNullException"><paramref name="dataTable"/> 为 null 时抛出。</exception>
    /// <exception cref="ArgumentException">列名不存在时抛出。</exception>
    /// <example>
    /// <code>
    /// var stats = DataTableHelper.CalculateStatistics(salesDt, "Amount");
    /// Console.WriteLine($"平均值: {stats.Average}, 标准差: {stats.StandardDeviation}");
    /// </code>
    /// </example>
    public static DataStatistics CalculateStatistics(DataTable dataTable, string columnName)
    {
        ThrowHelper.ThrowIfNull(dataTable);

        if (!dataTable.Columns.Contains(columnName))
            throw new ArgumentException($"列 '{columnName}' 不存在", nameof(columnName));

        var values = dataTable.AsEnumerable()
            .Select(row => row[columnName])
            .Where(v => v != null && v != DBNull.Value)
            .Select(v => Convert.ToDouble(v))
            .ToList();

        if (values.Count == 0)
            return new DataStatistics();

        var sortedValues = values.OrderBy(v => v).ToList();
        var mean = values.Average();

        return new DataStatistics
        {
            Count = values.Count,
            Sum = values.Sum(),
            Average = mean,
            Min = values.Min(),
            Max = values.Max(),
            Median = CalculateMedian(sortedValues),
            Mode = CalculateMode(values),
            Variance = CalculateVariance(values, mean),
            StandardDeviation = Math.Sqrt(CalculateVariance(values, mean)),
            Quartile1 = CalculateQuartile(sortedValues, 0.25),
            Quartile3 = CalculateQuartile(sortedValues, 0.75),
            Range = values.Max() - values.Min(),
            Skewness = CalculateSkewness(values, mean),
            Kurtosis = CalculateKurtosis(values, mean)
        };
    }

    /// <summary>
    /// 分析指定列的数据分布情况。
    /// </summary>
    /// <param name="dataTable">源 <see cref="DataTable"/>。</param>
    /// <param name="columnName">列名。</param>
    /// <param name="binCount">分箱数量，默认为 10。</param>
    /// <returns>数据分布信息。</returns>
    /// <exception cref="ArgumentNullException"><paramref name="dataTable"/> 为 null 时抛出。</exception>
    /// <exception cref="ArgumentException">列名不存在时抛出。</exception>
    /// <example>
    /// <code>
    /// var distribution = DataTableHelper.AnalyzeDistribution(salesDt, "Amount", 5);
    /// foreach (var bin in distribution.Bins)
    /// {
    ///     Console.WriteLine($"[{bin.Min:F2} - {bin.Max:F2}]: {bin.Count} ({bin.Percentage:F1}%)");
    /// }
    /// </code>
    /// </example>
    public static DataDistribution AnalyzeDistribution(DataTable dataTable, string columnName, int binCount = 10)
    {
        ThrowHelper.ThrowIfNull(dataTable);

        if (!dataTable.Columns.Contains(columnName))
            throw new ArgumentException($"列 '{columnName}' 不存在", nameof(columnName));

        if (binCount < 1)
            binCount = 10;

        var values = dataTable.AsEnumerable()
            .Select(row => row[columnName])
            .Where(v => v != null && v != DBNull.Value)
            .Select(v => Convert.ToDouble(v))
            .OrderBy(v => v)
            .ToList();

        if (values.Count == 0)
            return new DataDistribution();

        var min = values.Min();
        var max = values.Max();
        var range = max - min;
        var binWidth = range / binCount;

        if (binWidth == 0)
            binWidth = 1;

        var bins = new List<HistogramBin>();
        for (int i = 0; i < binCount; i++)
        {
            var binMin = min + i * binWidth;
            var binMax = i == binCount - 1 ? max : min + (i + 1) * binWidth;

            var count = i == binCount - 1
                ? values.Count(v => v >= binMin && v <= binMax)
                : values.Count(v => v >= binMin && v < binMax);

            bins.Add(new HistogramBin
            {
                Min = binMin,
                Max = binMax,
                Count = count,
                Percentage = values.Count > 0 ? (double)count / values.Count * 100 : 0
            });
        }

        var mean = values.Average();
        return new DataDistribution
        {
            Bins = bins,
            Skewness = CalculateSkewness(values, mean),
            Kurtosis = CalculateKurtosis(values, mean),
            Percentiles = CalculatePercentiles(values)
        };
    }

    /// <summary>
    /// 计算两列之间的皮尔逊相关系数。
    /// </summary>
    /// <param name="dataTable">源 <see cref="DataTable"/>。</param>
    /// <param name="column1">第一列名。</param>
    /// <param name="column2">第二列名。</param>
    /// <returns>相关系数，范围 [-1, 1]，1 表示完全正相关，-1 表示完全负相关，0 表示不相关。</returns>
    /// <exception cref="ArgumentNullException"><paramref name="dataTable"/> 为 null 时抛出。</exception>
    /// <exception cref="ArgumentException">列名不存在时抛出。</exception>
    /// <example>
    /// <code>
    /// var correlation = DataTableHelper.CalculateCorrelation(salesDt, "Price", "Quantity");
    /// Console.WriteLine($"价格与销量的相关系数: {correlation:F4}");
    /// </code>
    /// </example>
    public static double CalculateCorrelation(DataTable dataTable, string column1, string column2)
    {
        ThrowHelper.ThrowIfNull(dataTable);

        if (!dataTable.Columns.Contains(column1))
            throw new ArgumentException($"列 '{column1}' 不存在", nameof(column1));
        if (!dataTable.Columns.Contains(column2))
            throw new ArgumentException($"列 '{column2}' 不存在", nameof(column2));

        var pairs = dataTable.AsEnumerable()
            .Where(row => row[column1] != DBNull.Value && row[column2] != DBNull.Value)
            .Select(row => new
            {
                X = Convert.ToDouble(row[column1]),
                Y = Convert.ToDouble(row[column2])
            })
            .ToList();

        if (pairs.Count < 2)
            return 0;

        var meanX = pairs.Average(p => p.X);
        var meanY = pairs.Average(p => p.Y);

        var numerator = pairs.Sum(p => (p.X - meanX) * (p.Y - meanY));
        var denominator = Math.Sqrt(
            pairs.Sum(p => Math.Pow(p.X - meanX, 2)) *
            pairs.Sum(p => Math.Pow(p.Y - meanY, 2)));

        return denominator == 0 ? 0 : numerator / denominator;
    }

    #endregion

    #region 数据导出

    /// <summary>
    /// 将 <see cref="DataTable"/> 导出为 HTML 表格字符串。
    /// </summary>
    /// <param name="dataTable">源 <see cref="DataTable"/>。</param>
    /// <param name="tableClass">表格 CSS 类名，默认为 "data-table"。</param>
    /// <param name="includeIndex">是否包含行索引列，默认为 false。</param>
    /// <param name="tableId">表格 ID，可选。</param>
    /// <returns>HTML 表格字符串。</returns>
    /// <exception cref="ArgumentNullException"><paramref name="dataTable"/> 为 null 时抛出。</exception>
    /// <example>
    /// <code>
    /// var html = DataTableHelper.ExportToHtml(dt, "table table-striped", true, "salesTable");
    /// File.WriteAllText("output.html", html);
    /// </code>
    /// </example>
    public static string ExportToHtml(
        DataTable dataTable,
        string tableClass = "data-table",
        bool includeIndex = false,
        string tableId = null)
    {
        ThrowHelper.ThrowIfNull(dataTable);

        var html = new StringBuilder();
        var idAttr = string.IsNullOrEmpty(tableId) ? "" : $" id=\"{tableId}\"";
        html.Append($"<table{idAttr} class=\"{tableClass}\">");

        html.Append("<thead><tr>");
        if (includeIndex)
            html.Append("<th>#</th>");

        foreach (DataColumn column in dataTable.Columns)
        {
            html.Append($"<th>{EscapeHtml(column.ColumnName)}</th>");
        }
        html.Append("</tr></thead>");

        html.Append("<tbody>");
        for (int i = 0; i < dataTable.Rows.Count; i++)
        {
            html.Append("<tr>");
            if (includeIndex)
                html.Append($"<td>{i + 1}</td>");

            foreach (DataColumn column in dataTable.Columns)
            {
                var value = dataTable.Rows[i][column];
                html.Append($"<td>{EscapeHtml(value?.ToString() ?? "")}</td>");
            }
            html.Append("</tr>");
        }
        html.Append("</tbody>");

        html.Append("</table>");
        return html.ToString();
    }

    /// <summary>
    /// 将 <see cref="DataTable"/> 导出为 Excel XML 格式（可被 Excel 打开）。
    /// </summary>
    /// <param name="dataTable">源 <see cref="DataTable"/>。</param>
    /// <param name="sheetName">工作表名称，默认为 "Sheet1"。</param>
    /// <returns>Excel XML 字符串。</returns>
    /// <exception cref="ArgumentNullException"><paramref name="dataTable"/> 为 null 时抛出。</exception>
    /// <example>
    /// <code>
    /// var excelXml = DataTableHelper.ExportToExcelXml(dt, "销售数据");
    /// File.WriteAllText("output.xml", excelXml);
    /// </code>
    /// </example>
    public static string ExportToExcelXml(DataTable dataTable, string sheetName = "Sheet1")
    {
        ThrowHelper.ThrowIfNull(dataTable);

        var xml = new StringBuilder();
        xml.Append("<?xml version=\"1.0\" encoding=\"utf-8\"?>");
        xml.Append("<?mso-application progid=\"Excel.Sheet\"?>");
        xml.Append("<Workbook xmlns=\"urn:schemas-microsoft-com:office:spreadsheet\"");
        xml.Append(" xmlns:ss=\"urn:schemas-microsoft-com:office:spreadsheet\">");
        xml.Append($"<Worksheet ss:Name=\"{EscapeXml(sheetName)}\">");
        xml.Append("<Table>");

        xml.Append("<Row ss:StyleID=\"Header\">");
        foreach (DataColumn column in dataTable.Columns)
        {
            xml.Append($"<Cell><Data ss:Type=\"String\">{EscapeXml(column.ColumnName)}</Data></Cell>");
        }
        xml.Append("</Row>");

        foreach (DataRow row in dataTable.Rows)
        {
            xml.Append("<Row>");
            foreach (var item in row.ItemArray)
            {
                var (type, value) = GetExcelCellTypeAndValue(item);
                xml.Append($"<Cell><Data ss:Type=\"{type}\">{value}</Data></Cell>");
            }
            xml.Append("</Row>");
        }

        xml.Append("</Table>");
        xml.Append("</Worksheet>");
        xml.Append("</Workbook>");

        return xml.ToString();
    }

    /// <summary>
    /// 将 <see cref="DataTable"/> 导出为 Markdown 表格格式。
    /// </summary>
    /// <param name="dataTable">源 <see cref="DataTable"/>。</param>
    /// <param name="includeIndex">是否包含行索引列，默认为 false。</param>
    /// <returns>Markdown 表格字符串。</returns>
    /// <exception cref="ArgumentNullException"><paramref name="dataTable"/> 为 null 时抛出。</exception>
    /// <example>
    /// <code>
    /// var markdown = DataTableHelper.ExportToMarkdown(dt, true);
    /// File.WriteAllText("output.md", markdown);
    /// </code>
    /// </example>
    public static string ExportToMarkdown(DataTable dataTable, bool includeIndex = false)
    {
        ThrowHelper.ThrowIfNull(dataTable);

        var md = new StringBuilder();

        var headers = dataTable.Columns.Cast<DataColumn>()
            .Select(c => c.ColumnName)
            .ToList();

        if (includeIndex)
            headers.Insert(0, "#");

        md.Append("| ").Append(string.Join(" | ", headers)).AppendLine(" |");

        var separators = headers.Select(_ => "---").ToList();
        md.Append("| ").Append(string.Join(" | ", separators)).AppendLine(" |");

        for (int i = 0; i < dataTable.Rows.Count; i++)
        {
            var values = dataTable.Columns.Cast<DataColumn>()
                .Select(c => EscapeMarkdown(dataTable.Rows[i][c]?.ToString() ?? ""))
                .ToList();

            if (includeIndex)
                values.Insert(0, (i + 1).ToString());

            md.Append("| ").Append(string.Join(" | ", values)).AppendLine(" |");
        }

        return md.ToString();
    }

    #endregion

    #region 数据清洗

    /// <summary>
    /// 验证 <see cref="DataTable"/> 中的数据。
    /// </summary>
    /// <param name="dataTable">源 <see cref="DataTable"/>。</param>
    /// <param name="validationRules">验证规则字典（列名 -> 验证函数）。</param>
    /// <returns>验证结果列表。</returns>
    /// <exception cref="ArgumentNullException">参数为 null 时抛出。</exception>
    /// <example>
    /// <code>
    /// var rules = new Dictionary<string, Func<object, bool>>
    /// {
    ///     { "Age", value => value != null &amp;&amp; Convert.ToInt32(value) >= 0 &amp;&amp; Convert.ToInt32(value) <= 150 },
    ///     { "Email", value => value != null &amp;&amp; Regex.IsMatch(value.ToString(), @"^[\w-\.]+@[\w-]+\.[a-z]{2,}$") }
    /// };
    /// var results = DataTableHelper.ValidateData(usersDt, rules);
    /// foreach (var error in results.Where(r => !r.IsValid))
    /// {
    ///     Console.WriteLine($"行 {error.RowIndex}: {string.Join(", ", error.Errors)}");
    /// }
    /// </code>
    /// </example>
    public static List<DataValidationResult> ValidateData(
        DataTable dataTable,
        Dictionary<string, Func<object, bool>> validationRules)
    {
        ThrowHelper.ThrowIfNull(dataTable);
        ThrowHelper.ThrowIfNull(validationRules);

        var results = new List<DataValidationResult>();

        for (int rowIndex = 0; rowIndex < dataTable.Rows.Count; rowIndex++)
        {
            var row = dataTable.Rows[rowIndex];

            foreach (DataColumn column in dataTable.Columns)
            {
                if (!validationRules.TryGetValue(column.ColumnName, out var rule))
                    continue;

                var value = row[column];
                try
                {
                    var isValid = rule(value);
                    if (!isValid)
                    {
                        results.Add(new DataValidationResult
                        {
                            IsValid = false,
                            RowIndex = rowIndex,
                            ColumnName = column.ColumnName,
                            Value = value,
                            Errors = new List<string> { $"行 {rowIndex + 1} 列 '{column.ColumnName}' 验证失败" }
                        });
                    }
                }
                catch (Exception ex)
                {
                    results.Add(new DataValidationResult
                    {
                        IsValid = false,
                        RowIndex = rowIndex,
                        ColumnName = column.ColumnName,
                        Value = value,
                        Errors = new List<string> { $"行 {rowIndex + 1} 列 '{column.ColumnName}' 验证异常: {ex.Message}" }
                    });
                }
            }
        }

        return results;
    }

    /// <summary>
    /// 清洗 <see cref="DataTable"/> 中的数据。
    /// </summary>
    /// <param name="dataTable">源 <see cref="DataTable"/>。</param>
    /// <param name="cleaningRules">清洗规则字典（列名 -> 清洗函数）。</param>
    /// <returns>清洗后的新 <see cref="DataTable"/>。</returns>
    /// <exception cref="ArgumentNullException">参数为 null 时抛出。</exception>
    /// <example>
    /// <code>
    /// var rules = new Dictionary<string, Func<object, object>>
    /// {
    ///     { "Name", value => value?.ToString().Trim() },
    ///     { "Phone", value => Regex.Replace(value?.ToString() ?? "", @"[^\d]", "") }
    /// };
    /// var cleanedDt = DataTableHelper.CleanData(usersDt, rules);
    /// </code>
    /// </example>
    public static DataTable CleanData(
        DataTable dataTable,
        Dictionary<string, Func<object, object>> cleaningRules)
    {
        ThrowHelper.ThrowIfNull(dataTable);
        ThrowHelper.ThrowIfNull(cleaningRules);

        var result = dataTable.Copy();

        foreach (DataRow row in result.Rows)
        {
            foreach (DataColumn column in result.Columns)
            {
                if (!cleaningRules.TryGetValue(column.ColumnName, out var rule))
                    continue;

                try
                {
                    var originalValue = row[column];
                    var cleanedValue = rule(originalValue);
                    row[column] = cleanedValue ?? DBNull.Value;
                }
                catch
                {
                    // 清洗失败时保留原值
                }
            }
        }

        return result;
    }

    /// <summary>
    /// 自动填充 <see cref="DataTable"/> 中每一列的缺失值。
    /// 填充策略：优先向下取第一个非空值，若下方均为空则沿用上方最近的非空值。
    /// 返回新表，不修改原表。
    /// </summary>
    /// <param name="dataTable">源 <see cref="DataTable"/>。</param>
    /// <returns>填充后的新 <see cref="DataTable"/>。</returns>
    /// <exception cref="ArgumentNullException"><paramref name="dataTable"/> 为 null 时抛出。</exception>
    /// <example>
    /// <code>
    /// var dt = new DataTable();
    /// dt.Columns.Add("Name", typeof(string));
    /// dt.Rows.Add("张三");
    /// dt.Rows.Add(DBNull.Value);
    /// dt.Rows.Add("李四");
    /// 
    /// var filledDt = DataTableHelper.FillMissingValues(dt);
    /// // filledDt.Rows[1]["Name"] => "张三"（沿用上方非空值）
    /// // dt 原表不变
    /// </code>
    /// </example>
    public static DataTable FillMissingValues(DataTable dataTable)
    {
        ThrowHelper.ThrowIfNull(dataTable);

        var result = dataTable.Copy();

        if (result.Rows.Count == 0)
            return result;

        int rowCount = result.Rows.Count;
        int colCount = result.Columns.Count;

        for (int col = 0; col < colCount; col++)
        {
            // 从下往上：预计算每个空值位置下方最近的非空值
            var nextNonNull = new object[rowCount];
            object nextValue = null;
            for (int row = rowCount - 1; row >= 0; row--)
            {
                var cellValue = result.Rows[row][col];
                if (IsCellNullOrEmpty(cellValue))
                {
                    nextNonNull[row] = nextValue;
                }
                else
                {
                    nextValue = cellValue;
                    nextNonNull[row] = null;
                }
            }

            // 从上往下：优先用下方非空值填充，否则用上方最近非空值
            object lastValue = null;
            for (int row = 0; row < rowCount; row++)
            {
                var cellValue = result.Rows[row][col];
                if (IsCellNullOrEmpty(cellValue))
                {
                    if (nextNonNull[row] != null)
                    {
                        result.Rows[row][col] = nextNonNull[row];
                        lastValue = nextNonNull[row];
                    }
                    else if (lastValue != null)
                    {
                        result.Rows[row][col] = lastValue;
                    }
                }
                else
                {
                    lastValue = cellValue;
                }
            }
        }

        return result;
    }

    /// <summary>
    /// 按自定义规则填充 <see cref="DataTable"/> 中的缺失值。
    /// </summary>
    /// <param name="dataTable">源 <see cref="DataTable"/>。</param>
    /// <param name="fillingRules">填充规则字典（列名 -> 填充函数，函数接收当前表和行索引，返回填充值）。</param>
    /// <returns>填充后的新 <see cref="DataTable"/>。</returns>
    /// <exception cref="ArgumentNullException">参数为 null 时抛出。</exception>
    /// <example>
    /// <code>
    /// var rules = new Dictionary<string, Func<DataTable, int, object>>
    /// {
    ///     { "Age", (dt, rowIdx) => dt.AsEnumerable().Average(r => Convert.ToDouble(r["Age"])) },
    ///     { "City", (dt, rowIdx) => "未知" }
    /// };
    /// var filledDt = DataTableHelper.FillMissingValues(usersDt, rules);
    /// </code>
    /// </example>
    public static DataTable FillMissingValues(
        DataTable dataTable,
        Dictionary<string, Func<DataTable, int, object>> fillingRules)
    {
        ThrowHelper.ThrowIfNull(dataTable);
        ThrowHelper.ThrowIfNull(fillingRules);

        var result = dataTable.Copy();

        foreach (DataColumn column in result.Columns)
        {
            if (!fillingRules.TryGetValue(column.ColumnName, out var rule))
                continue;

            for (int i = 0; i < result.Rows.Count; i++)
            {
                if (result.Rows[i][column] == DBNull.Value || result.Rows[i][column] == null)
                {
                    try
                    {
                        var filledValue = rule(result, i);
                        result.Rows[i][column] = filledValue ?? DBNull.Value;
                    }
                    catch
                    {
                        // 填充失败时保留原值
                    }
                }
            }
        }

        return result;
    }

    /// <summary>
    /// 用常量值填充指定列的缺失值。
    /// </summary>
    /// <param name="dataTable">源 <see cref="DataTable"/>。</param>
    /// <param name="columnName">列名。</param>
    /// <param name="fillValue">填充值。</param>
    /// <returns>填充后的新 <see cref="DataTable"/>。</returns>
    /// <exception cref="ArgumentNullException"><paramref name="dataTable"/> 为 null 时抛出。</exception>
    /// <exception cref="ArgumentException">列名不存在时抛出。</exception>
    /// <example>
    /// <code>
    /// var filledDt = DataTableHelper.FillMissingValues(dt, "City", "未知");
    /// </code>
    /// </example>
    public static DataTable FillMissingValues(DataTable dataTable, string columnName, object fillValue)
    {
        ThrowHelper.ThrowIfNull(dataTable);

        if (!dataTable.Columns.Contains(columnName))
            throw new ArgumentException($"列 '{columnName}' 不存在", nameof(columnName));

        var result = dataTable.Copy();
        var column = result.Columns[columnName]!;

        for (int i = 0; i < result.Rows.Count; i++)
        {
            if (IsCellNullOrEmpty(result.Rows[i][column]))
            {
                result.Rows[i][column] = fillValue ?? DBNull.Value;
            }
        }

        return result;
    }

    /// <summary>
    /// 用指定策略填充指定列的缺失值。
    /// </summary>
    /// <param name="dataTable">源 <see cref="DataTable"/>。</param>
    /// <param name="columnName">列名。</param>
    /// <param name="fillMethod">填充策略。</param>
    /// <returns>填充后的新 <see cref="DataTable"/>。</returns>
    /// <exception cref="ArgumentNullException"><paramref name="dataTable"/> 为 null 时抛出。</exception>
    /// <exception cref="ArgumentException">列名不存在时抛出。</exception>
    /// <example>
    /// <code>
    /// var filledDt = DataTableHelper.FillMissingValues(dt, "Age", FillMethod.Previous);
    /// var filledDt2 = DataTableHelper.FillMissingValues(dt, "Score", FillMethod.Mean);
    /// var filledDt3 = DataTableHelper.FillMissingValues(dt, "City", FillMethod.Mode);
    /// </code>
    /// </example>
    public static DataTable FillMissingValues(DataTable dataTable, string columnName, FillMethod fillMethod)
    {
        ThrowHelper.ThrowIfNull(dataTable);

        if (!dataTable.Columns.Contains(columnName))
            throw new ArgumentException($"列 '{columnName}' 不存在", nameof(columnName));

        var result = dataTable.Copy();
        var column = result.Columns[columnName]!;

        switch (fillMethod)
        {
            case FillMethod.Previous:
                FillWithPrevious(result, column);
                break;
            case FillMethod.Next:
                FillWithNext(result, column);
                break;
            case FillMethod.Mean:
                FillWithMean(result, column);
                break;
            case FillMethod.Median:
                FillWithMedian(result, column);
                break;
            case FillMethod.Mode:
                FillWithMode(result, column);
                break;
        }

        return result;
    }

    /// <summary>
    /// 检测数值列中的异常值。
    /// </summary>
    /// <param name="dataTable">源 <see cref="DataTable"/>。</param>
    /// <param name="columnName">列名。</param>
    /// <param name="method">检测方法，默认为 IQR 方法。</param>
    /// <param name="threshold">阈值（IQR 方法默认 1.5，Z-Score 方法默认 3.0）。</param>
    /// <returns>包含异常值的行索引列表。</returns>
    /// <exception cref="ArgumentNullException"><paramref name="dataTable"/> 为 null 时抛出。</exception>
    /// <exception cref="ArgumentException">列名不存在时抛出。</exception>
    /// <example>
    /// <code>
    /// // 使用 IQR 方法检测异常值
    /// var outliers = DataTableHelper.DetectOutliers(salesDt, "Amount", OutlierDetectionMethod.IQR);
    /// 
    /// // 使用 Z-Score 方法检测异常值
    /// var outliers = DataTableHelper.DetectOutliers(salesDt, "Amount", OutlierDetectionMethod.ZScore, 2.5);
    /// </code>
    /// </example>
    public static List<int> DetectOutliers(
        DataTable dataTable,
        string columnName,
        OutlierDetectionMethod method = OutlierDetectionMethod.IQR,
        double threshold = 1.5)
    {
        ThrowHelper.ThrowIfNull(dataTable);

        if (!dataTable.Columns.Contains(columnName))
            throw new ArgumentException($"列 '{columnName}' 不存在", nameof(columnName));

        var valuesWithIndex = dataTable.AsEnumerable()
            .Select((row, index) => new { Value = row[columnName], Index = index })
            .Where(x => x.Value != null && x.Value != DBNull.Value)
            .Select(x => new { Value = Convert.ToDouble(x.Value), x.Index })
            .ToList();

        if (valuesWithIndex.Count < 4)
            return new List<int>();

        var values = valuesWithIndex.Select(x => x.Value).OrderBy(v => v).ToList();
        var outlierIndices = new List<int>();

        switch (method)
        {
            case OutlierDetectionMethod.IQR:
                {
                    var q1 = CalculateQuartile(values, 0.25);
                    var q3 = CalculateQuartile(values, 0.75);
                    var iqr = q3 - q1;
                    var lowerBound = q1 - threshold * iqr;
                    var upperBound = q3 + threshold * iqr;

                    outlierIndices.AddRange(valuesWithIndex
                        .Where(v => v.Value < lowerBound || v.Value > upperBound)
                        .Select(v => v.Index));
                }
                break;

            case OutlierDetectionMethod.ZScore:
                {
                    if (threshold <= 0) threshold = 3.0;
                    var mean = values.Average();
                    var variance = values.Sum(v => Math.Pow(v - mean, 2)) / values.Count;
                    var stdDev = Math.Sqrt(variance);

                    if (stdDev > 0)
                    {
                        outlierIndices.AddRange(valuesWithIndex
                            .Where(v => Math.Abs((v.Value - mean) / stdDev) > threshold)
                            .Select(v => v.Index));
                    }
                }
                break;
        }

        return outlierIndices;
    }

    /// <summary>
    /// 移除 <see cref="DataTable"/> 中的重复行。
    /// </summary>
    /// <param name="dataTable">源 <see cref="DataTable"/>。</param>
    /// <param name="keyColumns">用于判断重复的列名数组，null 表示所有列。</param>
    /// <returns>去重后的新 <see cref="DataTable"/>。</returns>
    /// <exception cref="ArgumentNullException"><paramref name="dataTable"/> 为 null 时抛出。</exception>
    /// <example>
    /// <code>
    /// // 基于所有列去重
    /// var distinctDt = DataTableHelper.RemoveDuplicates(dt);
    /// 
    /// // 基于指定列去重
    /// var distinctDt = DataTableHelper.RemoveDuplicates(dt, new[] { "UserId", "OrderId" });
    /// </code>
    /// </example>
    public static DataTable RemoveDuplicates(DataTable dataTable, string[] keyColumns = null)
    {
        ThrowHelper.ThrowIfNull(dataTable);

        var result = dataTable.Clone();

        if (keyColumns == null || keyColumns.Length == 0)
            keyColumns = dataTable.Columns.Cast<DataColumn>().Select(c => c.ColumnName).ToArray();

        var seen = new HashSet<string>();
        foreach (DataRow row in dataTable.Rows)
        {
            var key = string.Join("|", keyColumns.Select(c => row[c]?.ToString() ?? "\0"));
            if (seen.Add(key))
                result.ImportRow(row);
        }

        return result;
    }

    #endregion

    #region 数据连接

    /// <summary>
    /// 内连接两个 <see cref="DataTable"/>。
    /// </summary>
    /// <param name="leftTable">左表。</param>
    /// <param name="rightTable">右表。</param>
    /// <param name="leftKey">左表连接键。</param>
    /// <param name="rightKey">右表连接键。</param>
    /// <returns>连接后的 <see cref="DataTable"/>。</returns>
    /// <exception cref="ArgumentNullException">参数为 null 时抛出。</exception>
    /// <example>
    /// <code>
    /// var result = DataTableHelper.InnerJoin(
    ///     ordersDt, 
    ///     customersDt, 
    ///     "CustomerId", 
    ///     "Id"
    /// );
    /// </code>
    /// </example>
    public static DataTable InnerJoin(
        DataTable leftTable,
        DataTable rightTable,
        string leftKey,
        string rightKey)
    {
        return InnerJoin(leftTable, rightTable, new[] { leftKey }, new[] { rightKey });
    }

    /// <summary>
    /// 内连接两个 <see cref="DataTable"/>（支持多键）。
    /// </summary>
    /// <param name="leftTable">左表。</param>
    /// <param name="rightTable">右表。</param>
    /// <param name="leftKeys">左表连接键数组。</param>
    /// <param name="rightKeys">右表连接键数组。</param>
    /// <returns>连接后的 <see cref="DataTable"/>。</returns>
    /// <exception cref="ArgumentNullException">参数为 null 时抛出。</exception>
    public static DataTable InnerJoin(
        DataTable leftTable,
        DataTable rightTable,
        string[] leftKeys,
        string[] rightKeys)
    {
        ThrowHelper.ThrowIfNull(leftTable);
        ThrowHelper.ThrowIfNull(rightTable);
        ThrowHelper.ThrowIfNull(leftKeys);
        ThrowHelper.ThrowIfNull(rightKeys);

        var result = CreateJoinResultTable(leftTable, rightTable);

        var leftLookup = leftTable.AsEnumerable()
            .ToLookup(row => leftKeys.Select(key => row[key]).ToArray(), new ArrayEqualityComparer());

        var rightLookup = rightTable.AsEnumerable()
            .ToLookup(row => rightKeys.Select(key => row[key]).ToArray(), new ArrayEqualityComparer());

        var commonKeys = leftLookup.Select(g => g.Key)
            .Intersect(rightLookup.Select(g => g.Key), new ArrayEqualityComparer());

        foreach (var key in commonKeys)
        {
            var leftRows = leftLookup[key];
            var rightRows = rightLookup[key];

            foreach (var leftRow in leftRows)
            {
                foreach (var rightRow in rightRows)
                {
                    var newRow = CreateJoinedRow(result, leftTable, rightTable, leftRow, rightRow, leftKeys, rightKeys);
                    result.Rows.Add(newRow);
                }
            }
        }

        return result;
    }

    /// <summary>
    /// 左连接两个 <see cref="DataTable"/>。
    /// </summary>
    /// <param name="leftTable">左表。</param>
    /// <param name="rightTable">右表。</param>
    /// <param name="leftKey">左表连接键。</param>
    /// <param name="rightKey">右表连接键。</param>
    /// <returns>连接后的 <see cref="DataTable"/>。</returns>
    /// <exception cref="ArgumentNullException">参数为 null 时抛出。</exception>
    /// <example>
    /// <code>
    /// var result = DataTableHelper.LeftJoin(
    ///     ordersDt, 
    ///     customersDt, 
    ///     "CustomerId", 
    ///     "Id"
    /// );
    /// </code>
    /// </example>
    public static DataTable LeftJoin(
        DataTable leftTable,
        DataTable rightTable,
        string leftKey,
        string rightKey)
    {
        return LeftJoin(leftTable, rightTable, new[] { leftKey }, new[] { rightKey });
    }

    /// <summary>
    /// 左连接两个 <see cref="DataTable"/>（支持多键）。
    /// </summary>
    /// <param name="leftTable">左表。</param>
    /// <param name="rightTable">右表。</param>
    /// <param name="leftKeys">左表连接键数组。</param>
    /// <param name="rightKeys">右表连接键数组。</param>
    /// <returns>连接后的 <see cref="DataTable"/>。</returns>
    /// <exception cref="ArgumentNullException">参数为 null 时抛出。</exception>
    public static DataTable LeftJoin(
        DataTable leftTable,
        DataTable rightTable,
        string[] leftKeys,
        string[] rightKeys)
    {
        ThrowHelper.ThrowIfNull(leftTable);
        ThrowHelper.ThrowIfNull(rightTable);
        ThrowHelper.ThrowIfNull(leftKeys);
        ThrowHelper.ThrowIfNull(rightKeys);

        var result = CreateJoinResultTable(leftTable, rightTable);

        var rightLookup = rightTable.AsEnumerable()
            .ToLookup(row => rightKeys.Select(key => row[key]).ToArray(), new ArrayEqualityComparer());

        foreach (DataRow leftRow in leftTable.Rows)
        {
            var key = leftKeys.Select(key => leftRow[key]).ToArray();
            var rightRows = rightLookup[key].ToList();

            if (rightRows.Count > 0)
            {
                foreach (var rightRow in rightRows)
                {
                    var newRow = CreateJoinedRow(result, leftTable, rightTable, leftRow, rightRow, leftKeys, rightKeys);
                    result.Rows.Add(newRow);
                }
            }
            else
            {
                var newRow = CreateJoinedRow(result, leftTable, rightTable, leftRow, null, leftKeys, rightKeys);
                result.Rows.Add(newRow);
            }
        }

        return result;
    }

    /// <summary>
    /// 全连接两个 <see cref="DataTable"/>。
    /// </summary>
    /// <param name="leftTable">左表。</param>
    /// <param name="rightTable">右表。</param>
    /// <param name="leftKey">左表连接键。</param>
    /// <param name="rightKey">右表连接键。</param>
    /// <returns>连接后的 <see cref="DataTable"/>。</returns>
    /// <exception cref="ArgumentNullException">参数为 null 时抛出。</exception>
    /// <example>
    /// <code>
    /// var result = DataTableHelper.FullJoin(
    ///     ordersDt, 
    ///     customersDt, 
    ///     "CustomerId", 
    ///     "Id"
    /// );
    /// </code>
    /// </example>
    public static DataTable FullJoin(
        DataTable leftTable,
        DataTable rightTable,
        string leftKey,
        string rightKey)
    {
        return FullJoin(leftTable, rightTable, new[] { leftKey }, new[] { rightKey });
    }

    /// <summary>
    /// 全连接两个 <see cref="DataTable"/>（支持多键）。
    /// </summary>
    /// <param name="leftTable">左表。</param>
    /// <param name="rightTable">右表。</param>
    /// <param name="leftKeys">左表连接键数组。</param>
    /// <param name="rightKeys">右表连接键数组。</param>
    /// <returns>连接后的 <see cref="DataTable"/>。</returns>
    /// <exception cref="ArgumentNullException">参数为 null 时抛出。</exception>
    public static DataTable FullJoin(
        DataTable leftTable,
        DataTable rightTable,
        string[] leftKeys,
        string[] rightKeys)
    {
        ThrowHelper.ThrowIfNull(leftTable);
        ThrowHelper.ThrowIfNull(rightTable);
        ThrowHelper.ThrowIfNull(leftKeys);
        ThrowHelper.ThrowIfNull(rightKeys);

        var result = CreateJoinResultTable(leftTable, rightTable);

        var leftLookup = leftTable.AsEnumerable()
            .ToLookup(row => leftKeys.Select(key => row[key]).ToArray(), new ArrayEqualityComparer());

        var rightLookup = rightTable.AsEnumerable()
            .ToLookup(row => rightKeys.Select(key => row[key]).ToArray(), new ArrayEqualityComparer());

        var allKeys = leftLookup.Select(g => g.Key)
            .Union(rightLookup.Select(g => g.Key), new ArrayEqualityComparer());

        foreach (var key in allKeys)
        {
            var leftRows = leftLookup[key].ToList();
            var rightRows = rightLookup[key].ToList();

            if (leftRows.Count > 0 && rightRows.Count > 0)
            {
                foreach (var leftRow in leftRows)
                {
                    foreach (var rightRow in rightRows)
                    {
                        var newRow = CreateJoinedRow(result, leftTable, rightTable, leftRow, rightRow, leftKeys, rightKeys);
                        result.Rows.Add(newRow);
                    }
                }
            }
            else if (leftRows.Count > 0)
            {
                foreach (var leftRow in leftRows)
                {
                    var newRow = CreateJoinedRow(result, leftTable, rightTable, leftRow, null, leftKeys, rightKeys);
                    result.Rows.Add(newRow);
                }
            }
            else
            {
                foreach (var rightRow in rightRows)
                {
                    var newRow = CreateJoinedRow(result, leftTable, rightTable, null, rightRow, leftKeys, rightKeys);
                    result.Rows.Add(newRow);
                }
            }
        }

        return result;
    }

    /// <summary>
    /// 交叉连接两个 <see cref="DataTable"/>（笛卡尔积）。
    /// </summary>
    /// <param name="leftTable">左表。</param>
    /// <param name="rightTable">右表。</param>
    /// <returns>连接后的 <see cref="DataTable"/>。</returns>
    /// <exception cref="ArgumentNullException">参数为 null 时抛出。</exception>
    /// <example>
    /// <code>
    /// var result = DataTableHelper.CrossJoin(colorsDt, sizesDt);
    /// // 结果包含所有颜色和尺寸的组合
    /// </code>
    /// </example>
    public static DataTable CrossJoin(DataTable leftTable, DataTable rightTable)
    {
        ThrowHelper.ThrowIfNull(leftTable);
        ThrowHelper.ThrowIfNull(rightTable);

        var result = CreateJoinResultTable(leftTable, rightTable);

        foreach (DataRow leftRow in leftTable.Rows)
        {
            foreach (DataRow rightRow in rightTable.Rows)
            {
                var newRow = CreateJoinedRow(result, leftTable, rightTable, leftRow, rightRow, Array.Empty<string>(), Array.Empty<string>());
                result.Rows.Add(newRow);
            }
        }

        return result;
    }

    /// <summary>
    /// 垂直合并多个 <see cref="DataTable"/>（UNION ALL）。
    /// </summary>
    /// <param name="tables">要合并的 <see cref="DataTable"/> 数组。</param>
    /// <returns>合并后的 <see cref="DataTable"/>。</returns>
    /// <exception cref="ArgumentNullException"><paramref name="tables"/> 为 null 时抛出。</exception>
    /// <example>
    /// <code>
    /// var result = DataTableHelper.UnionAll(januaryDt, februaryDt, marchDt);
    /// </code>
    /// </example>
    public static DataTable UnionAll(params DataTable[] tables)
    {
        ThrowHelper.ThrowIfNull(tables);

        if (tables.Length == 0)
            return new DataTable();

        var result = tables[0].Clone();

        foreach (var table in tables)
        {
            if (table == null) continue;

            foreach (DataRow row in table.Rows)
            {
                result.ImportRow(row);
            }
        }

        return result;
    }

    /// <summary>
    /// 水平合并两个 <see cref="DataTable"/>。
    /// </summary>
    /// <param name="leftTable">左表。</param>
    /// <param name="rightTable">右表。</param>
    /// <param name="joinColumn">连接列名（用于对齐行），可选。如果不指定，则按行索引对齐。</param>
    /// <returns>合并后的 <see cref="DataTable"/>。</returns>
    /// <exception cref="ArgumentNullException">参数为 null 时抛出。</exception>
    /// <example>
    /// <code>
    /// // 按行索引对齐
    /// var result = DataTableHelper.MergeHorizontally(basicInfoDt, extendedInfoDt);
    /// 
    /// // 按指定列对齐
    /// var result = DataTableHelper.MergeHorizontally(ordersDt, paymentsDt, "OrderId");
    /// </code>
    /// </example>
    public static DataTable MergeHorizontally(
        DataTable leftTable,
        DataTable rightTable,
        string joinColumn = null)
    {
        ThrowHelper.ThrowIfNull(leftTable);
        ThrowHelper.ThrowIfNull(rightTable);

        var result = new DataTable();

        foreach (DataColumn column in leftTable.Columns)
        {
            result.Columns.Add($"Left_{column.ColumnName}", column.DataType);
        }

        foreach (DataColumn column in rightTable.Columns)
        {
            var columnName = $"Right_{column.ColumnName}";
            if (!result.Columns.Contains(columnName))
            {
                result.Columns.Add(columnName, column.DataType);
            }
        }

        if (!string.IsNullOrEmpty(joinColumn) &&
            leftTable.Columns.Contains(joinColumn) &&
            rightTable.Columns.Contains(joinColumn))
        {
            var rightLookup = rightTable.AsEnumerable()
                .ToLookup(row => row[joinColumn], new ObjectEqualityComparer());

            foreach (DataRow leftRow in leftTable.Rows)
            {
                var key = leftRow[joinColumn];
                var rightRows = rightLookup[key].ToList();

                if (rightRows.Count > 0)
                {
                    foreach (var rightRow in rightRows)
                    {
                        var newRow = CreateHorizontalMergedRow(result, leftTable, rightTable, leftRow, rightRow);
                        result.Rows.Add(newRow);
                    }
                }
                else
                {
                    var newRow = CreateHorizontalMergedRow(result, leftTable, rightTable, leftRow, null);
                    result.Rows.Add(newRow);
                }
            }
        }
        else
        {
            var maxRows = Math.Max(leftTable.Rows.Count, rightTable.Rows.Count);

            for (int i = 0; i < maxRows; i++)
            {
                var leftRow = i < leftTable.Rows.Count ? leftTable.Rows[i] : null;
                var rightRow = i < rightTable.Rows.Count ? rightTable.Rows[i] : null;

                var newRow = CreateHorizontalMergedRow(result, leftTable, rightTable, leftRow, rightRow);
                result.Rows.Add(newRow);
            }
        }

        return result;
    }

    #endregion

    #region 高级查询

    /// <summary>
    /// 执行 SQL 风格查询。
    /// </summary>
    /// <param name="dataTable">源 <see cref="DataTable"/>。</param>
    /// <param name="whereClause">WHERE 子句（如 "Age > 18 AND City = 'Beijing'"）。</param>
    /// <param name="orderByClause">ORDER BY 子句（如 "Age DESC, Name ASC"）。</param>
    /// <param name="selectColumns">SELECT 列名数组，null 表示选择所有列。</param>
    /// <returns>查询结果。</returns>
    /// <exception cref="ArgumentNullException"><paramref name="dataTable"/> 为 null 时抛出。</exception>
    /// <example>
    /// <code>
    /// var result = DataTableHelper.Query(
    ///     usersDt,
    ///     "Age > 18 AND City = 'Beijing'",
    ///     "Age DESC, Name ASC",
    ///     new[] { "Name", "Age", "City" }
    /// );
    /// </code>
    /// </example>
    public static DataTable Query(
        DataTable dataTable,
        string whereClause = null,
        string orderByClause = null,
        string[] selectColumns = null)
    {
        ThrowHelper.ThrowIfNull(dataTable);

        var result = dataTable.Clone();

        IEnumerable<DataRow> rows = dataTable.AsEnumerable();

        if (!string.IsNullOrWhiteSpace(whereClause))
        {
            rows = rows.Where(row => EvaluateCondition(row, whereClause));
        }

        if (!string.IsNullOrWhiteSpace(orderByClause))
        {
            rows = ApplyOrderBy(rows, orderByClause);
        }

        foreach (var row in rows)
        {
            result.ImportRow(row);
        }

        if (selectColumns != null && selectColumns.Length > 0)
        {
            var columnsToRemove = result.Columns.Cast<DataColumn>()
                .Where(column => !selectColumns.Contains(column.ColumnName))
                .ToList();

            foreach (var column in columnsToRemove)
            {
                result.Columns.Remove(column);
            }
        }

        return result;
    }

    /// <summary>
    /// 在 <see cref="DataTable"/> 中执行全文搜索。
    /// </summary>
    /// <param name="dataTable">源 <see cref="DataTable"/>。</param>
    /// <param name="searchText">搜索文本。</param>
    /// <param name="searchColumns">搜索列名数组，null 表示搜索所有列。</param>
    /// <param name="caseSensitive">是否区分大小写，默认为 false。</param>
    /// <returns>搜索结果。</returns>
    /// <exception cref="ArgumentNullException"><paramref name="dataTable"/> 为 null 时抛出。</exception>
    /// <example>
    /// <code>
    /// // 在所有列中搜索
    /// var result = DataTableHelper.Search(usersDt, "张三");
    /// 
    /// // 在指定列中搜索
    /// var result = DataTableHelper.Search(usersDt, "张三", new[] { "Name", "Address" });
    /// </code>
    /// </example>
    public static DataTable Search(
        DataTable dataTable,
        string searchText,
        string[] searchColumns = null,
        bool caseSensitive = false)
    {
        ThrowHelper.ThrowIfNull(dataTable);

        if (string.IsNullOrEmpty(searchText))
            return dataTable.Clone();

        var result = dataTable.Clone();
        var comparison = caseSensitive ? StringComparison.Ordinal : StringComparison.OrdinalIgnoreCase;

        var columns = searchColumns ?? dataTable.Columns.Cast<DataColumn>().Select(c => c.ColumnName).ToArray();

        var rows = dataTable.AsEnumerable().Where(row =>
            columns.Any(column =>
            {
                var value = row[column]?.ToString();
                return value != null && value.IndexOf(searchText, comparison) >= 0;
            }));

        foreach (var row in rows)
        {
            result.ImportRow(row);
        }

        return result;
    }

    /// <summary>
    /// 使用正则表达式筛选 <see cref="DataTable"/> 中的数据。
    /// </summary>
    /// <param name="dataTable">源 <see cref="DataTable"/>。</param>
    /// <param name="columnName">列名。</param>
    /// <param name="pattern">正则表达式模式。</param>
    /// <param name="options">正则表达式选项，默认为 None。</param>
    /// <returns>筛选结果。</returns>
    /// <exception cref="ArgumentNullException">参数为 null 时抛出。</exception>
    /// <exception cref="ArgumentException">列名不存在时抛出。</exception>
    /// <example>
    /// <code>
    /// // 筛选邮箱格式正确的记录
    /// var result = DataTableHelper.RegexFilter(usersDt, "Email", @"^[\w-\.]+@[\w-]+\.[a-z]{2,}$");
    /// 
    /// // 筛选手机号格式正确的记录
    /// var result = DataTableHelper.RegexFilter(usersDt, "Phone", @"^1[3-9]\d{9}$");
    /// </code>
    /// </example>
    public static DataTable RegexFilter(
        DataTable dataTable,
        string columnName,
        string pattern,
        RegexOptions options = RegexOptions.None)
    {
        ThrowHelper.ThrowIfNull(dataTable);

        if (!dataTable.Columns.Contains(columnName))
            throw new ArgumentException($"列 '{columnName}' 不存在", nameof(columnName));

        if (string.IsNullOrEmpty(pattern))
            return dataTable.Clone();

        var result = dataTable.Clone();
        var regex = new Regex(pattern, options);

        var rows = dataTable.AsEnumerable().Where(row =>
        {
            var value = row[columnName]?.ToString();
            return value != null && regex.IsMatch(value);
        });

        foreach (var row in rows)
        {
            result.ImportRow(row);
        }

        return result;
    }

    #endregion

    #region 私有辅助方法

    /// <summary>
    /// 将 <see cref="JsonElement"/> 转换为 CLR 对象，保留原始类型。
    /// </summary>
    /// <param name="element">需要转换的 <see cref="JsonElement"/>。</param>
    /// <returns>转换后的对象；若值为 null 则返回 <see cref="DBNull.Value"/>。</returns>
    private static object ConvertJsonElementToObject(JsonElement element)
    {
        return element.ValueKind switch
        {
            JsonValueKind.Number => element.TryGetInt64(out var longVal) ? longVal : element.GetDouble(),
            JsonValueKind.True => true,
            JsonValueKind.False => false,
            JsonValueKind.String => (object?)element.GetString() ?? DBNull.Value,
            JsonValueKind.Null => DBNull.Value,
            _ => element.ToString()
        };
    }

    private static void FillWithPrevious(DataTable table, DataColumn column)
    {
        object lastValue = DBNull.Value;
        for (int i = 0; i < table.Rows.Count; i++)
        {
            if (IsCellNullOrEmpty(table.Rows[i][column]))
            {
                if (lastValue != DBNull.Value)
                    table.Rows[i][column] = lastValue;
            }
            else
            {
                lastValue = table.Rows[i][column];
            }
        }
    }

    private static void FillWithNext(DataTable table, DataColumn column)
    {
        object nextValue = DBNull.Value;
        for (int i = table.Rows.Count - 1; i >= 0; i--)
        {
            if (IsCellNullOrEmpty(table.Rows[i][column]))
            {
                if (nextValue != DBNull.Value)
                    table.Rows[i][column] = nextValue;
            }
            else
            {
                nextValue = table.Rows[i][column];
            }
        }
    }

    private static void FillWithMean(DataTable table, DataColumn column)
    {
        var values = new List<double>();
        foreach (DataRow row in table.Rows)
        {
            if (!IsCellNullOrEmpty(row[column]) && double.TryParse(row[column].ToString(), out var v))
                values.Add(v);
        }

        if (values.Count == 0) return;
        var mean = values.Average();

        for (int i = 0; i < table.Rows.Count; i++)
        {
            if (IsCellNullOrEmpty(table.Rows[i][column]))
                table.Rows[i][column] = mean;
        }
    }

    private static void FillWithMedian(DataTable table, DataColumn column)
    {
        var values = new List<double>();
        foreach (DataRow row in table.Rows)
        {
            if (!IsCellNullOrEmpty(row[column]) && double.TryParse(row[column].ToString(), out var v))
                values.Add(v);
        }

        if (values.Count == 0) return;
        values.Sort();
        var median = values.Count % 2 == 1
            ? values[values.Count / 2]
            : (values[values.Count / 2 - 1] + values[values.Count / 2]) / 2.0;

        for (int i = 0; i < table.Rows.Count; i++)
        {
            if (IsCellNullOrEmpty(table.Rows[i][column]))
                table.Rows[i][column] = median;
        }
    }

    private static void FillWithMode(DataTable table, DataColumn column)
    {
        var freq = new Dictionary<object, int>();
        foreach (DataRow row in table.Rows)
        {
            if (IsCellNullOrEmpty(row[column])) continue;
            var key = row[column];
            freq[key] = freq.TryGetValue(key, out var c) ? c + 1 : 1;
        }

        if (freq.Count == 0) return;
        var mode = freq.OrderByDescending(kv => kv.Value).First().Key;

        for (int i = 0; i < table.Rows.Count; i++)
        {
            if (IsCellNullOrEmpty(table.Rows[i][column]))
                table.Rows[i][column] = mode;
        }
    }

    /// <summary>
    /// 判断单元格值是否为 null、<see cref="DBNull"/> 或空白字符串。
    /// </summary>
    private static bool IsCellNullOrEmpty(object value)
    {
        return value == null || value == DBNull.Value || (value is string s && string.IsNullOrWhiteSpace(s));
    }

    private static object DefaultSumAggregate(IEnumerable<object> values)
    {
        var numericValues = values
            .Where(v => v != null && v != DBNull.Value)
            .Select(v =>
            {
                if (v is decimal d) return d;
                if (v is double db) return (decimal)db;
                if (v is float f) return (decimal)f;
                if (v is int i) return (decimal)i;
                if (v is long l) return (decimal)l;
                return TryConvertToDecimal(v);
            })
            .Where(v => v.HasValue)
            .Select(v => v.Value)
            .ToList();

        return numericValues.Count > 0 ? numericValues.Sum() : 0m;
    }

    private static decimal? TryConvertToDecimal(object value)
    {
        try
        {
            return Convert.ToDecimal(value);
        }
        catch
        {
            return null;
        }
    }

    private static List<string> GetColumnCombinations(DataTable dataTable, string[] columnFields)
    {
        if (columnFields.Length == 0)
            return new List<string> { "" };

        var uniqueValues = new List<List<string>>();
        foreach (var columnField in columnFields)
        {
            var values = dataTable.AsEnumerable()
                .Select(row => row[columnField]?.ToString() ?? "NULL")
                .Distinct()
                .ToList();
            uniqueValues.Add(values);
        }

        var combinations = new List<string>();
        GenerateCombinations(uniqueValues, 0, new List<string>(), combinations);

        return combinations;
    }

    private static void GenerateCombinations(
        List<List<string>> uniqueValues,
        int index,
        List<string> current,
        List<string> combinations)
    {
        if (index == uniqueValues.Count)
        {
            combinations.Add(string.Join("_", current));
            return;
        }

        foreach (var value in uniqueValues[index])
        {
            current.Add(value);
            GenerateCombinations(uniqueValues, index + 1, current, combinations);
            current.RemoveAt(current.Count - 1);
        }
    }

    private static double CalculateMedian(List<double> sortedValues)
    {
        int count = sortedValues.Count;
        if (count == 0) return 0;

        if (count % 2 == 0)
            return (sortedValues[count / 2 - 1] + sortedValues[count / 2]) / 2;
        else
            return sortedValues[count / 2];
    }

    private static double? CalculateMode(List<double> values)
    {
        if (values.Count == 0) return null;

        var frequencyMap = new Dictionary<double, int>();
        foreach (var value in values)
        {
            if (frequencyMap.ContainsKey(value))
                frequencyMap[value]++;
            else
                frequencyMap[value] = 1;
        }

        var maxFrequency = frequencyMap.Values.Max();
        if (maxFrequency == 1) return null;

        return frequencyMap.First(kvp => kvp.Value == maxFrequency).Key;
    }

    private static double CalculateVariance(List<double> values, double mean)
    {
        if (values.Count <= 1) return 0;
        return values.Sum(v => Math.Pow(v - mean, 2)) / (values.Count - 1);
    }

    private static double CalculateQuartile(List<double> sortedValues, double quartile)
    {
        if (sortedValues.Count == 0) return 0;

        var position = quartile * (sortedValues.Count - 1);
        var index = (int)Math.Floor(position);
        var fraction = position - index;

        if (index + 1 < sortedValues.Count)
            return sortedValues[index] + fraction * (sortedValues[index + 1] - sortedValues[index]);
        else
            return sortedValues[index];
    }

    private static double CalculateSkewness(List<double> values, double mean)
    {
        if (values.Count < 3) return 0;

        var variance = CalculateVariance(values, mean);
        var stdDev = Math.Sqrt(variance);

        if (stdDev == 0) return 0;

        var sumCubedDeviations = values.Sum(v => Math.Pow(v - mean, 3));
        return sumCubedDeviations / (values.Count * Math.Pow(stdDev, 3));
    }

    private static double CalculateKurtosis(List<double> values, double mean)
    {
        if (values.Count < 4) return 0;

        var variance = CalculateVariance(values, mean);

        if (variance == 0) return 0;

        var sumFourthDeviations = values.Sum(v => Math.Pow(v - mean, 4));
        return sumFourthDeviations / (values.Count * Math.Pow(variance, 2)) - 3;
    }

    private static Dictionary<int, double> CalculatePercentiles(List<double> sortedValues)
    {
        var percentiles = new Dictionary<int, double>();
        var percentilePositions = new[] { 5, 10, 25, 50, 75, 90, 95 };

        foreach (var p in percentilePositions)
        {
            percentiles[p] = CalculateQuartile(sortedValues, p / 100.0);
        }

        return percentiles;
    }

    private static string EscapeHtml(string value)
    {
        return value
            .Replace("&", "&amp;")
            .Replace("<", "<")
            .Replace(">", ">")
            .Replace("\"", "&quot;")
            .Replace("'", "&#39;");
    }

    private static string EscapeXml(string value)
    {
        return value
            .Replace("&", "&amp;")
            .Replace("<", "<")
            .Replace(">", ">")
            .Replace("\"", "&quot;")
            .Replace("'", "&apos;");
    }

    private static string EscapeMarkdown(string value)
    {
        return value.Replace("|", "\\|").Replace("\n", " ").Replace("\r", "");
    }

    private static (string Type, string Value) GetExcelCellTypeAndValue(object value)
    {
        if (value == null || value == DBNull.Value)
            return ("String", "");

        switch (value)
        {
            case int:
            case long:
            case short:
            case byte:
            case decimal:
            case double:
            case float:
                return ("Number", value.ToString());
            case bool b:
                return ("Boolean", b.ToString().ToLower());
            case DateTime dt:
                return ("DateTime", dt.ToString("yyyy-MM-ddTHH:mm:ss"));
            default:
                return ("String", EscapeXml(value.ToString()));
        }
    }

    private static DataTable CreateJoinResultTable(DataTable leftTable, DataTable rightTable)
    {
        var result = new DataTable();

        foreach (DataColumn column in leftTable.Columns)
        {
            result.Columns.Add(column.ColumnName, column.DataType);
        }

        foreach (DataColumn column in rightTable.Columns)
        {
            if (!result.Columns.Contains(column.ColumnName))
            {
                result.Columns.Add(column.ColumnName, column.DataType);
            }
        }

        return result;
    }

    private static DataRow CreateJoinedRow(
        DataTable resultTable,
        DataTable leftTable,
        DataTable rightTable,
        DataRow leftRow,
        DataRow rightRow,
        string[] leftKeys,
        string[] rightKeys)
    {
        var newRow = resultTable.NewRow();

        if (leftRow != null)
        {
            foreach (DataColumn column in leftTable.Columns)
            {
                newRow[column.ColumnName] = leftRow[column] ?? DBNull.Value;
            }
        }
        else
        {
            foreach (DataColumn column in leftTable.Columns)
            {
                newRow[column.ColumnName] = DBNull.Value;
            }
        }

        if (rightRow != null)
        {
            foreach (DataColumn column in rightTable.Columns)
            {
                if (!leftTable.Columns.Contains(column.ColumnName) || leftKeys.Contains(column.ColumnName))
                {
                    if (!resultTable.Columns.Contains(column.ColumnName))
                        continue;

                    newRow[column.ColumnName] = rightRow[column] ?? DBNull.Value;
                }
            }
        }
        else
        {
            foreach (DataColumn column in rightTable.Columns)
            {
                if (!leftTable.Columns.Contains(column.ColumnName) || leftKeys.Contains(column.ColumnName))
                {
                    if (!resultTable.Columns.Contains(column.ColumnName))
                        continue;

                    newRow[column.ColumnName] = DBNull.Value;
                }
            }
        }

        return newRow;
    }

    private static DataRow CreateHorizontalMergedRow(
        DataTable resultTable,
        DataTable leftTable,
        DataTable rightTable,
        DataRow leftRow,
        DataRow rightRow)
    {
        var newRow = resultTable.NewRow();

        if (leftRow != null)
        {
            foreach (DataColumn column in leftTable.Columns)
            {
                newRow[$"Left_{column.ColumnName}"] = leftRow[column] ?? DBNull.Value;
            }
        }
        else
        {
            foreach (DataColumn column in leftTable.Columns)
            {
                newRow[$"Left_{column.ColumnName}"] = DBNull.Value;
            }
        }

        if (rightRow != null)
        {
            foreach (DataColumn column in rightTable.Columns)
            {
                newRow[$"Right_{column.ColumnName}"] = rightRow[column] ?? DBNull.Value;
            }
        }
        else
        {
            foreach (DataColumn column in rightTable.Columns)
            {
                newRow[$"Right_{column.ColumnName}"] = DBNull.Value;
            }
        }

        return newRow;
    }

    private static bool EvaluateCondition(DataRow row, string condition)
    {
        try
        {
            var rows = row.Table.Select(condition);
            return rows.Contains(row);
        }
        catch
        {
            return false;
        }
    }

    private static IEnumerable<DataRow> ApplyOrderBy(IEnumerable<DataRow> rows, string orderByClause)
    {
        var parts = orderByClause.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
        if (parts.Length == 0) return rows;

        IOrderedEnumerable<DataRow> orderedRows = null;

        foreach (var part in parts)
        {
            var trimmedPart = part.Trim();
            var desc = trimmedPart.EndsWith(" DESC", StringComparison.OrdinalIgnoreCase);
            var columnName = desc
                ? trimmedPart.Substring(0, trimmedPart.Length - 5).Trim()
                : trimmedPart.EndsWith(" ASC", StringComparison.OrdinalIgnoreCase)
                    ? trimmedPart.Substring(0, trimmedPart.Length - 4).Trim()
                    : trimmedPart;

            if (orderedRows == null)
            {
                orderedRows = desc
                    ? rows.OrderByDescending(row => row[columnName], new ObjectComparer())
                    : rows.OrderBy(row => row[columnName], new ObjectComparer());
            }
            else
            {
                orderedRows = desc
                    ? orderedRows.ThenByDescending(row => row[columnName], new ObjectComparer())
                    : orderedRows.ThenBy(row => row[columnName], new ObjectComparer());
            }
        }

        return orderedRows ?? rows;
    }

    /// <summary>
    /// 安全地将值转换为目标类型，处理 <see cref="DBNull"/>、可空类型及常见类型转换。
    /// 若转换失败则返回目标类型的默认值。
    /// </summary>
    /// <param name="value">要转换的原始值。</param>
    /// <param name="targetType">目标类型。</param>
    /// <returns>转换后的值；若转换失败则返回目标类型的默认值。</returns>
    private static object ConvertValueSafe(object value, Type targetType)
    {
        try
        {
            if (value == null || value == DBNull.Value)
            {
                return targetType.IsValueType && Nullable.GetUnderlyingType(targetType) == null
                    ? Activator.CreateInstance(targetType)
                    : null;
            }

            var underlyingType = Nullable.GetUnderlyingType(targetType);
            if (underlyingType != null)
                targetType = underlyingType;

            if (targetType == typeof(string))
                return value.ToString();

            if (targetType == typeof(DateTime))
                return Convert.ToDateTime(value);

            if (targetType == typeof(bool))
                return Convert.ToBoolean(value);

            if (targetType == typeof(Guid))
                return Guid.Parse(value.ToString());

            if (targetType == typeof(decimal))
                return Convert.ToDecimal(value);

            if (targetType.IsEnum)
                return Enum.Parse(targetType, value.ToString());

            return Convert.ChangeType(value, targetType);
        }
        catch
        {
            return targetType.IsValueType ? Activator.CreateInstance(targetType) : null;
        }
    }

    #endregion
}

#region 辅助类

/// <summary>
/// 数据统计信息，包含数值列的完整统计指标。
/// </summary>
public class DataStatistics
{
    /// <summary>
    /// 数据总数。
    /// </summary>
    public int Count { get; set; }

    /// <summary>
    /// 总和。
    /// </summary>
    public double Sum { get; set; }

    /// <summary>
    /// 平均值。
    /// </summary>
    public double Average { get; set; }

    /// <summary>
    /// 最小值。
    /// </summary>
    public double Min { get; set; }

    /// <summary>
    /// 最大值。
    /// </summary>
    public double Max { get; set; }

    /// <summary>
    /// 中位数。
    /// </summary>
    public double Median { get; set; }

    /// <summary>
    /// 众数，如果没有重复值则为 null。
    /// </summary>
    public double? Mode { get; set; }

    /// <summary>
    /// 样本方差。
    /// </summary>
    public double Variance { get; set; }

    /// <summary>
    /// 标准差。
    /// </summary>
    public double StandardDeviation { get; set; }

    /// <summary>
    /// 第一四分位数（Q1，25% 分位数）。
    /// </summary>
    public double Quartile1 { get; set; }

    /// <summary>
    /// 第三四分位数（Q3，75% 分位数）。
    /// </summary>
    public double Quartile3 { get; set; }

    /// <summary>
    /// 极差（最大值 - 最小值）。
    /// </summary>
    public double Range { get; set; }

    /// <summary>
    /// 偏度，衡量分布的对称性。正值表示右偏，负值表示左偏。
    /// </summary>
    public double Skewness { get; set; }

    /// <summary>
    /// 峰度，衡量分布的尖峭程度。正值表示比正态分布更尖峭，负值表示更平坦。
    /// </summary>
    public double Kurtosis { get; set; }
}

/// <summary>
/// 数据分布信息，包含直方图和分布形态指标。
/// </summary>
public class DataDistribution
{
    /// <summary>
    /// 直方图分箱列表。
    /// </summary>
    public List<HistogramBin> Bins { get; set; } = new();

    /// <summary>
    /// 偏度，衡量分布的对称性。
    /// </summary>
    public double Skewness { get; set; }

    /// <summary>
    /// 峰度，衡量分布的尖峭程度。
    /// </summary>
    public double Kurtosis { get; set; }

    /// <summary>
    /// 百分位数字典（百分位 -> 值）。
    /// </summary>
    public Dictionary<int, double> Percentiles { get; set; } = new();
}

/// <summary>
/// 直方图分箱，表示数据分布直方图中的一个区间。
/// </summary>
public class HistogramBin
{
    /// <summary>
    /// 区间最小值。
    /// </summary>
    public double Min { get; set; }

    /// <summary>
    /// 区间最大值。
    /// </summary>
    public double Max { get; set; }

    /// <summary>
    /// 区间内数据数量。
    /// </summary>
    public int Count { get; set; }

    /// <summary>
    /// 区间内数据占总数的百分比。
    /// </summary>
    public double Percentage { get; set; }
}

/// <summary>
/// 数据验证结果。
/// </summary>
public class DataValidationResult
{
    /// <summary>
    /// 是否验证通过。
    /// </summary>
    public bool IsValid { get; set; }

    /// <summary>
    /// 行索引（从 0 开始）。
    /// </summary>
    public int RowIndex { get; set; }

    /// <summary>
    /// 列名。
    /// </summary>
    public string ColumnName { get; set; }

    /// <summary>
    /// 原始值。
    /// </summary>
    public object Value { get; set; }

    /// <summary>
    /// 错误信息列表。
    /// </summary>
    public List<string> Errors { get; set; } = new();
}

/// <summary>
/// 填充缺失值的策略。
/// </summary>
public enum FillMethod
{
    /// <summary>使用前一个非空值填充。</summary>
    Previous,
    /// <summary>使用后一个非空值填充。</summary>
    Next,
    /// <summary>使用平均值填充（仅数值列）。</summary>
    Mean,
    /// <summary>使用中位数填充（仅数值列）。</summary>
    Median,
    /// <summary>使用众数填充。</summary>
    Mode
}

/// <summary>
/// 异常值检测方法。
/// </summary>
public enum OutlierDetectionMethod
{
    /// <summary>
    /// 四分位距方法（IQR）。基于 Q1 - 1.5*IQR 和 Q3 + 1.5*IQR 来检测异常值。
    /// </summary>
    IQR,

    /// <summary>
    /// Z 分数方法。基于数据点与均值的标准差倍数来检测异常值。
    /// </summary>
    ZScore
}

/// <summary>
/// 对象数组相等比较器。
/// </summary>
internal class ArrayEqualityComparer : IEqualityComparer<object[]>
{
    public bool Equals(object[] x, object[] y)
    {
        if (x == null || y == null) return x == y;
        if (x.Length != y.Length) return false;

        for (int i = 0; i < x.Length; i++)
        {
            if (!Equals(x[i], y[i])) return false;
        }

        return true;
    }

    public int GetHashCode(object[] obj)
    {
        if (obj == null) return 0;

        int hash = 17;
        foreach (var item in obj)
        {
            hash = hash * 31 + (item?.GetHashCode() ?? 0);
        }

        return hash;
    }
}

/// <summary>
/// 对象相等比较器。
/// </summary>
internal class ObjectEqualityComparer : IEqualityComparer<object>
{
    public new bool Equals(object x, object y) => object.Equals(x, y);

    public int GetHashCode(object obj) => obj?.GetHashCode() ?? 0;
}

/// <summary>
/// 对象比较器。
/// </summary>
internal class ObjectComparer : IComparer<object>
{
    public int Compare(object x, object y)
    {
        if (x == null && y == null) return 0;
        if (x == null) return -1;
        if (y == null) return 1;

        if (x is IComparable comparable)
            return comparable.CompareTo(y);

        return string.Compare(x.ToString(), y.ToString(), StringComparison.Ordinal);
    }
}

#endregion