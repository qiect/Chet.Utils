using System.Data;
using System.Dynamic;
using System.Text;
using System.Text.RegularExpressions;

namespace Chet.Utils.Helpers
{
    /// <summary>
    /// DataTable高级操作帮助类
    /// 提供复杂的数据处理、转换、分析和导出功能
    /// </summary>
    public static partial class DataTableHelper
    {
        #region 数据转换与映射

        /// <summary>
        /// 将DataTable转换为动态对象列表
        /// </summary>
        /// <param name="dataTable">源DataTable</param>
        /// <returns>动态对象列表</returns>
        public static List<dynamic> ToDynamicList(DataTable dataTable)
        {
            var result = new List<dynamic>();

            foreach (DataRow row in dataTable.Rows)
            {
                dynamic dynamicObject = new ExpandoObject();
                var dict = (IDictionary<string, object>)dynamicObject;

                foreach (DataColumn column in dataTable.Columns)
                {
                    dict[column.ColumnName] = row[column] ?? DBNull.Value;
                }

                result.Add(dynamicObject);
            }

            return result;
        }

        /// <summary>
        /// 将DataTable转换为指定类型的对象列表
        /// </summary>
        /// <typeparam name="T">目标类型</typeparam>
        /// <param name="dataTable">源DataTable</param>
        /// <returns>对象列表</returns>
        public static List<T> ToObjectList<T>(DataTable dataTable) where T : new()
        {
            var result = new List<T>();
            var properties = typeof(T).GetProperties();

            foreach (DataRow row in dataTable.Rows)
            {
                var obj = new T();

                foreach (var prop in properties)
                {
                    if (dataTable.Columns.Contains(prop.Name))
                    {
                        var value = row[prop.Name];
                        if (value != DBNull.Value)
                        {
                            try
                            {
                                var convertedValue = Convert.ChangeType(value, prop.PropertyType);
                                prop.SetValue(obj, convertedValue);
                            }
                            catch
                            {
                                // 转换失败时跳过该属性
                            }
                        }
                    }
                }

                result.Add(obj);
            }

            return result;
        }

        /// <summary>
        /// 将对象列表转换为DataTable
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="objects">对象列表</param>
        /// <returns>DataTable</returns>
        public static DataTable ToDataTable<T>(IEnumerable<T> objects)
        {
            var dataTable = new DataTable(typeof(T).Name);
            var properties = typeof(T).GetProperties();

            // 创建列
            foreach (var prop in properties)
            {
                dataTable.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
            }

            // 填充数据
            foreach (var obj in objects)
            {
                var row = dataTable.NewRow();
                foreach (var prop in properties)
                {
                    row[prop.Name] = prop.GetValue(obj) ?? DBNull.Value;
                }
                dataTable.Rows.Add(row);
            }

            return dataTable;
        }

        /// <summary>
        /// 将JSON字符串转换为DataTable
        /// </summary>
        /// <param name="json">JSON字符串</param>
        /// <returns>DataTable</returns>
        public static DataTable ToDataTableFromJson(string json)
        {
            if (string.IsNullOrEmpty(json))
                throw new ArgumentException("JSON字符串不能为空", nameof(json));

            var dataTable = new DataTable();

            try
            {
                // 解析JSON字符串
                var jsonArray = System.Text.Json.JsonSerializer.Deserialize<List<Dictionary<string, object>>>(json);

                if (jsonArray == null || jsonArray.Count == 0)
                    return dataTable;

                // 获取所有可能的列名
                var columnNames = new HashSet<string>();
                foreach (var item in jsonArray)
                {
                    foreach (var key in item.Keys)
                    {
                        columnNames.Add(key);
                    }
                }

                // 创建列
                foreach (var columnName in columnNames)
                {
                    dataTable.Columns.Add(columnName);
                }

                // 填充数据
                foreach (var item in jsonArray)
                {
                    var row = dataTable.NewRow();
                    foreach (var columnName in columnNames)
                    {
                        if (item.ContainsKey(columnName))
                        {
                            row[columnName] = item[columnName] ?? DBNull.Value;
                        }
                        else
                        {
                            row[columnName] = DBNull.Value;
                        }
                    }
                    dataTable.Rows.Add(row);
                }
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("JSON解析失败", ex);
            }

            return dataTable;
        }

        /// <summary>
        /// 将DataTable转换为字典列表
        /// </summary>
        /// <param name="dataTable">源DataTable</param>
        /// <returns>字典列表</returns>
        public static List<Dictionary<string, object>> ToDictionaryList(DataTable dataTable)
        {
            var result = new List<Dictionary<string, object>>();

            foreach (DataRow row in dataTable.Rows)
            {
                var dict = new Dictionary<string, object>();

                foreach (DataColumn column in dataTable.Columns)
                {
                    dict[column.ColumnName] = row[column];
                }

                result.Add(dict);
            }

            return result;
        }

        /// <summary>
        /// 动态映射DataTable列名
        /// </summary>
        /// <param name="dataTable">源DataTable</param>
        /// <param name="columnMappings">列映射字典（原列名->新列名）</param>
        /// <returns>映射后的DataTable</returns>
        public static DataTable MapColumns(DataTable dataTable, Dictionary<string, string> columnMappings)
        {
            var result = dataTable.Copy();

            foreach (var mapping in columnMappings)
            {
                if (result.Columns.Contains(mapping.Key))
                {
                    result.Columns[mapping.Key].ColumnName = mapping.Value;
                }
            }

            return result;
        }

        #endregion

        #region 数据透视与分组

        /// <summary>
        /// 数据透视操作
        /// </summary>
        /// <param name="dataTable">源DataTable</param>
        /// <param name="rowField">行字段</param>
        /// <param name="columnField">列字段</param>
        /// <param name="dataField">数据字段</param>
        /// <param name="aggregateFunction">聚合函数</param>
        /// <returns>透视后的DataTable</returns>
        public static DataTable Pivot(DataTable dataTable, string rowField, string columnField, string dataField,
            Func<IEnumerable<object>, object> aggregateFunction = null)
        {
            // 默认聚合函数为求和
            aggregateFunction ??= values => values.Where(v => v != null && v != DBNull.Value)
                .Select(v => Convert.ToDecimal(v))
                .DefaultIfEmpty(0)
                .Sum();

            var result = new DataTable();

            // 获取行和列的唯一值
            var rowValues = dataTable.AsEnumerable()
                .Select(row => row.Field<object>(rowField))
                .Distinct()
                .OrderBy(v => v)
                .ToList();

            var columnValues = dataTable.AsEnumerable()
                .Select(row => row.Field<object>(columnField))
                .Distinct()
                .OrderBy(v => v)
                .ToList();

            // 创建列
            result.Columns.Add(rowField);
            foreach (var colValue in columnValues)
            {
                result.Columns.Add(colValue?.ToString() ?? "NULL");
            }

            // 填充数据
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

                    newRow[colValue?.ToString() ?? "NULL"] = aggregateFunction(cellData);
                }

                result.Rows.Add(newRow);
            }

            return result;
        }

        /// <summary>
        /// 数据分组聚合
        /// </summary>
        /// <param name="dataTable">源DataTable</param>
        /// <param name="groupByColumns">分组列</param>
        /// <param name="aggregateColumns">聚合列配置</param>
        /// <returns>分组聚合后的DataTable</returns>
        public static DataTable GroupBy(DataTable dataTable, string[] groupByColumns,
            Dictionary<string, Func<IEnumerable<object>, object>> aggregateColumns)
        {
            var result = new DataTable();

            // 创建分组列
            foreach (var col in groupByColumns)
            {
                result.Columns.Add(col, dataTable.Columns[col].DataType);
            }

            // 创建聚合列
            foreach (var agg in aggregateColumns)
            {
                result.Columns.Add(agg.Key);
            }

            // 执行分组聚合
            var grouped = dataTable.AsEnumerable()
                .GroupBy(row => groupByColumns.Select(col => row[col]).ToArray(), new ArrayEqualityComparer());

            foreach (var group in grouped)
            {
                var newRow = result.NewRow();

                // 设置分组列值
                for (int i = 0; i < groupByColumns.Length; i++)
                {
                    newRow[groupByColumns[i]] = group.Key[i];
                }

                // 设置聚合列值
                foreach (var agg in aggregateColumns)
                {
                    var columnData = group.Select(row => row[agg.Key]).ToList();
                    newRow[agg.Key] = agg.Value(columnData);
                }

                result.Rows.Add(newRow);
            }

            return result;
        }

        /// <summary>
        /// 创建数据透视表（高级版）
        /// </summary>
        /// <param name="dataTable">源DataTable</param>
        /// <param name="rowFields">行字段数组</param>
        /// <param name="columnFields">列字段数组</param>
        /// <param name="dataFields">数据字段配置</param>
        /// <returns>高级透视表</returns>
        public static DataTable AdvancedPivot(DataTable dataTable, string[] rowFields, string[] columnFields,
            Dictionary<string, Func<IEnumerable<object>, object>> dataFields)
        {
            var result = new DataTable();

            // 创建行字段列
            foreach (var rowField in rowFields)
            {
                result.Columns.Add(rowField, dataTable.Columns[rowField].DataType);
            }

            // 获取列组合
            var columnCombinations = GetColumnCombinations(dataTable, columnFields);

            // 创建数据列
            foreach (var dataField in dataFields)
            {
                foreach (var combination in columnCombinations)
                {
                    var columnName = $"{dataField.Key}_{combination}";
                    result.Columns.Add(columnName);
                }
            }

            // 获取行组合
            var rowCombinations = dataTable.AsEnumerable()
                .GroupBy(row => rowFields.Select(f => row[f]).ToArray(), new ArrayEqualityComparer())
                .Select(g => g.Key)
                .ToList();

            // 填充数据
            foreach (var rowCombination in rowCombinations)
            {
                var newRow = result.NewRow();

                // 设置行字段值
                for (int i = 0; i < rowFields.Length; i++)
                {
                    newRow[rowFields[i]] = rowCombination[i];
                }

                // 设置数据字段值
                foreach (var dataField in dataFields)
                {
                    foreach (var columnCombination in columnCombinations)
                    {
                        var filterValues = columnCombination.Split('_');
                        var columnName = $"{dataField.Key}_{columnCombination}";

                        var filteredData = dataTable.AsEnumerable()
                            .Where(row =>
                            {
                                // 检查行字段匹配
                                for (int i = 0; i < rowFields.Length; i++)
                                {
                                    if (!Equals(row[rowFields[i]], rowCombination[i]))
                                        return false;
                                }

                                // 检查列字段匹配
                                for (int i = 0; i < columnFields.Length; i++)
                                {
                                    if (!Equals(row[columnFields[i]], filterValues[i]))
                                        return false;
                                }

                                return true;
                            })
                            .Select(row => row[dataField.Key]);

                        newRow[columnName] = dataField.Value(filteredData);
                    }
                }

                result.Rows.Add(newRow);
            }

            return result;
        }

        /// <summary>
        /// 获取列组合
        /// </summary>
        private static List<string> GetColumnCombinations(DataTable dataTable, string[] columnFields)
        {
            if (columnFields.Length == 0) return new List<string> { "" };

            var uniqueValues = new List<List<object>>();
            foreach (var columnField in columnFields)
            {
                uniqueValues.Add(dataTable.AsEnumerable()
                    .Select(row => row[columnField])
                    .Distinct()
                    .ToList());
            }

            var combinations = new List<string>();
            GenerateCombinations(uniqueValues, 0, new List<object>(), combinations);

            return combinations.Select(c => string.Join("_", c)).ToList();
        }

        /// <summary>
        /// 生成组合
        /// </summary>
        private static void GenerateCombinations(List<List<object>> uniqueValues, int index,
            List<object> current, List<string> combinations)
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

        #endregion

        #region 数据分析与统计

        /// <summary>
        /// 计算数据统计信息
        /// </summary>
        /// <param name="dataTable">源DataTable</param>
        /// <param name="columnName">列名</param>
        /// <returns>统计信息</returns>
        public static DataStatistics CalculateStatistics(DataTable dataTable, string columnName)
        {
            var values = dataTable.AsEnumerable()
                .Select(row => row[columnName])
                .Where(v => v != null && v != DBNull.Value)
                .Select(v => Convert.ToDouble(v))
                .ToList();

            if (values.Count == 0)
                return new DataStatistics();

            var sortedValues = values.OrderBy(v => v).ToList();

            return new DataStatistics
            {
                Count = values.Count,
                Sum = values.Sum(),
                Average = values.Average(),
                Min = values.Min(),
                Max = values.Max(),
                Median = CalculateMedian(sortedValues),
                Mode = CalculateMode(values),
                Variance = CalculateVariance(values),
                StandardDeviation = Math.Sqrt(CalculateVariance(values)),
                Quartile1 = CalculateQuartile(sortedValues, 0.25),
                Quartile3 = CalculateQuartile(sortedValues, 0.75)
            };
        }

        /// <summary>
        /// 计算中位数
        /// </summary>
        private static double CalculateMedian(List<double> sortedValues)
        {
            int count = sortedValues.Count;
            if (count == 0) return 0;

            if (count % 2 == 0)
                return (sortedValues[count / 2 - 1] + sortedValues[count / 2]) / 2;
            else
                return sortedValues[count / 2];
        }

        /// <summary>
        /// 计算众数
        /// </summary>
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
            if (maxFrequency == 1) return null; // 没有重复值

            return frequencyMap.First(kvp => kvp.Value == maxFrequency).Key;
        }

        /// <summary>
        /// 计算方差
        /// </summary>
        private static double CalculateVariance(List<double> values)
        {
            if (values.Count <= 1) return 0;

            var mean = values.Average();
            var sumSquaredDifferences = values.Sum(v => Math.Pow(v - mean, 2));
            return sumSquaredDifferences / (values.Count - 1); // 样本方差
        }

        /// <summary>
        /// 计算四分位数
        /// </summary>
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

        /// <summary>
        /// 数据分布分析
        /// </summary>
        /// <param name="dataTable">源DataTable</param>
        /// <param name="columnName">列名</param>
        /// <param name="binCount">分箱数量</param>
        /// <returns>分布信息</returns>
        public static DataDistribution AnalyzeDistribution(DataTable dataTable, string columnName, int binCount = 10)
        {
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

            var bins = new List<HistogramBin>();
            for (int i = 0; i < binCount; i++)
            {
                var binMin = min + i * binWidth;
                var binMax = i == binCount - 1 ? max : min + (i + 1) * binWidth;

                var count = values.Count(v => v >= binMin && v < binMax + (i == binCount - 1 ? 1e-10 : 0));

                bins.Add(new HistogramBin
                {
                    Min = binMin,
                    Max = binMax,
                    Count = count,
                    Percentage = values.Count > 0 ? (double)count / values.Count * 100 : 0
                });
            }

            return new DataDistribution
            {
                Bins = bins,
                Skewness = CalculateSkewness(values),
                Kurtosis = CalculateKurtosis(values)
            };
        }

        /// <summary>
        /// 计算偏度
        /// </summary>
        private static double CalculateSkewness(List<double> values)
        {
            if (values.Count < 2) return 0;

            var mean = values.Average();
            var stdDev = Math.Sqrt(CalculateVariance(values));

            if (stdDev == 0) return 0;

            var sumCubedDeviations = values.Sum(v => Math.Pow(v - mean, 3));
            return sumCubedDeviations / (values.Count * Math.Pow(stdDev, 3));
        }

        /// <summary>
        /// 计算峰度
        /// </summary>
        private static double CalculateKurtosis(List<double> values)
        {
            if (values.Count < 2) return 0;

            var mean = values.Average();
            var variance = CalculateVariance(values);

            if (variance == 0) return 0;

            var sumFourthDeviations = values.Sum(v => Math.Pow(v - mean, 4));
            return sumFourthDeviations / (values.Count * Math.Pow(variance, 2)) - 3;
        }

        /// <summary>
        /// 相关性分析
        /// </summary>
        /// <param name="dataTable">源DataTable</param>
        /// <param name="column1">列1</param>
        /// <param name="column2">列2</param>
        /// <returns>相关系数</returns>
        public static double CalculateCorrelation(DataTable dataTable, string column1, string column2)
        {
            var pairs = dataTable.AsEnumerable()
                .Where(row => row[column1] != DBNull.Value && row[column2] != DBNull.Value)
                .Select(row => new
                {
                    X = Convert.ToDouble(row[column1]),
                    Y = Convert.ToDouble(row[column2])
                })
                .ToList();

            if (pairs.Count < 2) return 0;

            var meanX = pairs.Average(p => p.X);
            var meanY = pairs.Average(p => p.Y);

            var numerator = pairs.Sum(p => (p.X - meanX) * (p.Y - meanY));
            var denominator = Math.Sqrt(
                pairs.Sum(p => Math.Pow(p.X - meanX, 2)) *
                pairs.Sum(p => Math.Pow(p.Y - meanY, 2)));

            return denominator == 0 ? 0 : numerator / denominator;
        }

        #endregion

        #region 数据导出与序列化

        /// <summary>
        /// 导出DataTable为CSV格式
        /// </summary>
        /// <param name="dataTable">源DataTable</param>
        /// <param name="separator">分隔符</param>
        /// <param name="includeHeaders">是否包含表头</param>
        /// <returns>CSV字符串</returns>
        public static string ExportToCsv(DataTable dataTable, string separator = ",", bool includeHeaders = true)
        {
            var csv = new StringBuilder();

            if (includeHeaders)
            {
                var header = string.Join(separator, dataTable.Columns.Cast<DataColumn>().Select(column => EscapeCsvField(column.ColumnName)));
                csv.AppendLine(header);
            }

            foreach (DataRow row in dataTable.Rows)
            {
                var fields = row.ItemArray.Select(field => EscapeCsvField(field?.ToString() ?? ""));
                csv.AppendLine(string.Join(separator, fields));
            }

            return csv.ToString();
        }

        /// <summary>
        /// 转义CSV字段
        /// </summary>
        private static string EscapeCsvField(string field)
        {
            if (string.IsNullOrEmpty(field))
                return "\"\"";

            if (field.Contains(",") || field.Contains("\"") || field.Contains("\n") || field.Contains("\r"))
            {
                return "\"" + field.Replace("\"", "\"\"") + "\"";
            }

            return field;
        }

        /// <summary>
        /// 导出DataTable为JSON格式
        /// </summary>
        /// <param name="dataTable">源DataTable</param>
        /// <returns>JSON字符串</returns>
        public static string ExportToJson(DataTable dataTable)
        {
            var json = new StringBuilder();
            json.Append("[");

            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                if (i > 0) json.Append(",");

                json.Append("{");
                var first = true;

                foreach (DataColumn column in dataTable.Columns)
                {
                    if (!first) json.Append(",");
                    first = false;

                    var value = dataTable.Rows[i][column];
                    json.Append($"\"{column.ColumnName}\":{FormatJsonValue(value)}");
                }

                json.Append("}");
            }

            json.Append("]");
            return json.ToString();
        }

        /// <summary>
        /// 格式化JSON值
        /// </summary>
        private static string FormatJsonValue(object value)
        {
            if (value == null || value == DBNull.Value)
                return "null";

            switch (value)
            {
                case string s:
                    return "\"" + s.Replace("\\", "\\\\").Replace("\"", "\\\"") + "\"";
                case bool b:
                    return b.ToString().ToLower();
                case DateTime dt:
                    return "\"" + dt.ToString("yyyy-MM-ddTHH:mm:ss") + "\"";
                default:
                    return value.ToString();
            }
        }

        /// <summary>
        /// 导出DataTable为HTML表格
        /// </summary>
        /// <param name="dataTable">源DataTable</param>
        /// <param name="tableClass">表格CSS类名</param>
        /// <param name="includeIndex">是否包含行索引</param>
        /// <returns>HTML表格字符串</returns>
        public static string ExportToHtml(DataTable dataTable, string tableClass = "data-table", bool includeIndex = false)
        {
            var html = new StringBuilder();
            html.Append($"<table class=\"{tableClass}\">");

            // 表头
            html.Append("<thead><tr>");
            if (includeIndex)
                html.Append("<th>#</th>");

            foreach (DataColumn column in dataTable.Columns)
            {
                html.Append($"<th>{column.ColumnName}</th>");
            }
            html.Append("</tr></thead>");

            // 表体
            html.Append("<tbody>");
            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                html.Append("<tr>");
                if (includeIndex)
                    html.Append($"<td>{i + 1}</td>");

                foreach (DataColumn column in dataTable.Columns)
                {
                    html.Append($"<td>{dataTable.Rows[i][column]?.ToString() ?? ""}</td>");
                }
                html.Append("</tr>");
            }
            html.Append("</tbody>");

            html.Append("</table>");
            return html.ToString();
        }

        /// <summary>
        /// 导出DataTable为Excel XML格式
        /// </summary>
        /// <param name="dataTable">源DataTable</param>
        /// <returns>Excel XML字符串</returns>
        public static string ExportToExcelXml(DataTable dataTable)
        {
            var xml = new StringBuilder();
            xml.Append("<?xml version=\"1.0\"?>");
            xml.Append("<Workbook xmlns=\"urn:schemas-microsoft-com:office:spreadsheet\">");
            xml.Append("<Worksheet ss:Name=\"Sheet1\">");
            xml.Append("<Table>");

            // 表头
            xml.Append("<Row>");
            foreach (DataColumn column in dataTable.Columns)
            {
                xml.Append($"<Cell><Data ss:Type=\"String\">{column.ColumnName}</Data></Cell>");
            }
            xml.Append("</Row>");

            // 数据行
            foreach (DataRow row in dataTable.Rows)
            {
                xml.Append("<Row>");
                foreach (var item in row.ItemArray)
                {
                    var type = GetExcelCellType(item);
                    var value = item?.ToString() ?? "";
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
        /// 获取Excel单元格类型
        /// </summary>
        private static string GetExcelCellType(object value)
        {
            if (value == null || value == DBNull.Value)
                return "String";

            if (value is int || value is long || value is short || value is byte)
                return "Number";

            if (value is decimal || value is double || value is float)
                return "Number";

            if (value is DateTime)
                return "DateTime";

            if (value is bool)
                return "Boolean";

            return "String";
        }

        #endregion

        #region 数据验证与清洗

        /// <summary>
        /// 数据验证结果
        /// </summary>
        public class ValidationResult
        {
            public bool IsValid { get; set; }
            public List<string> Errors { get; set; } = new List<string>();
            public int RowIndex { get; set; }
            public int ColumnIndex { get; set; }
        }

        /// <summary>
        /// 验证DataTable数据
        /// </summary>
        /// <param name="dataTable">源DataTable</param>
        /// <param name="validationRules">验证规则字典</param>
        /// <returns>验证结果列表</returns>
        public static List<ValidationResult> ValidateData(DataTable dataTable,
            Dictionary<string, Func<object, bool>> validationRules)
        {
            var results = new List<ValidationResult>();

            for (int rowIndex = 0; rowIndex < dataTable.Rows.Count; rowIndex++)
            {
                var row = dataTable.Rows[rowIndex];

                foreach (DataColumn column in dataTable.Columns)
                {
                    if (validationRules.ContainsKey(column.ColumnName))
                    {
                        var value = row[column];
                        var isValid = validationRules[column.ColumnName](value);

                        if (!isValid)
                        {
                            results.Add(new ValidationResult
                            {
                                IsValid = false,
                                Errors = new List<string> { $"行 {rowIndex + 1} 列 '{column.ColumnName}' 验证失败" },
                                RowIndex = rowIndex,
                                ColumnIndex = column.Ordinal
                            });
                        }
                    }
                }
            }

            return results;
        }

        /// <summary>
        /// 清洗DataTable数据
        /// </summary>
        /// <param name="dataTable">源DataTable</param>
        /// <param name="cleaningRules">清洗规则字典</param>
        /// <returns>清洗后的DataTable</returns>
        public static DataTable CleanData(DataTable dataTable,
            Dictionary<string, Func<object, object>> cleaningRules)
        {
            var result = dataTable.Copy();

            foreach (DataRow row in result.Rows)
            {
                foreach (DataColumn column in result.Columns)
                {
                    if (cleaningRules.ContainsKey(column.ColumnName))
                    {
                        var value = row[column];
                        row[column] = cleaningRules[column.ColumnName](value);
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// 去除重复行
        /// </summary>
        /// <param name="dataTable">源DataTable</param>
        /// <param name="keyColumns">用于判断重复的列，null表示所有列</param>
        /// <returns>去重后的DataTable</returns>
        public static DataTable RemoveDuplicates(DataTable dataTable, string[] keyColumns = null)
        {
            var result = dataTable.Clone();

            var distinctRows = keyColumns == null
                ? dataTable.AsEnumerable().Distinct(DataRowComparer.Default)
                : dataTable.AsEnumerable().GroupBy(row => keyColumns.Select(col => row[col]).ToArray(), new ArrayEqualityComparer())
                    .Select(g => g.First());

            foreach (var row in distinctRows)
            {
                result.ImportRow(row);
            }

            return result;
        }

        /// <summary>
        /// 填充缺失值
        /// </summary>
        /// <param name="dataTable">源DataTable</param>
        /// <param name="fillingRules">填充规则字典</param>
        /// <returns>填充后的DataTable</returns>
        public static DataTable FillMissingValues(DataTable dataTable,
            Dictionary<string, Func<DataTable, string, object>> fillingRules)
        {
            var result = dataTable.Copy();

            foreach (DataColumn column in result.Columns)
            {
                if (fillingRules.ContainsKey(column.ColumnName))
                {
                    for (int i = 0; i < result.Rows.Count; i++)
                    {
                        if (result.Rows[i][column] == DBNull.Value)
                        {
                            result.Rows[i][column] = fillingRules[column.ColumnName](result, column.ColumnName);
                        }
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// 检测异常值
        /// </summary>
        /// <param name="dataTable">源DataTable</param>
        /// <param name="columnName">列名</param>
        /// <param name="method">检测方法</param>
        /// <returns>包含异常值的行索引列表</returns>
        public static List<int> DetectOutliers(DataTable dataTable, string columnName, OutlierDetectionMethod method = OutlierDetectionMethod.IQR)
        {
            var values = dataTable.AsEnumerable()
                .Select((row, index) => new { Value = row[columnName], Index = index })
                .Where(x => x.Value != DBNull.Value)
                .Select(x => new { Value = Convert.ToDouble(x.Value), Index = x.Index })
                .OrderBy(x => x.Value)
                .ToList();

            if (values.Count == 0) return new List<int>();

            var outlierIndices = new List<int>();

            switch (method)
            {
                case OutlierDetectionMethod.IQR:
                    var q1 = CalculateQuartile(values.Select(v => v.Value).ToList(), 0.25);
                    var q3 = CalculateQuartile(values.Select(v => v.Value).ToList(), 0.75);
                    var iqr = q3 - q1;
                    var lowerBound = q1 - 1.5 * iqr;
                    var upperBound = q3 + 1.5 * iqr;

                    outlierIndices.AddRange(values
                        .Where(v => v.Value < lowerBound || v.Value > upperBound)
                        .Select(v => v.Index));
                    break;

                case OutlierDetectionMethod.ZScore:
                    var mean = values.Average(v => v.Value);
                    var stdDev = Math.Sqrt(values.Sum(v => Math.Pow(v.Value - mean, 2)) / values.Count);
                    var threshold = 3.0;

                    outlierIndices.AddRange(values
                        .Where(v => Math.Abs((v.Value - mean) / stdDev) > threshold)
                        .Select(v => v.Index));
                    break;
            }

            return outlierIndices;
        }

        #endregion

        #region 数据连接与合并

        /// <summary>
        /// 内连接两个DataTable
        /// </summary>
        /// <param name="leftTable">左表</param>
        /// <param name="rightTable">右表</param>
        /// <param name="joinKey">连接键</param>
        /// <returns>连接后的DataTable</returns>
        public static DataTable InnerJoin(DataTable leftTable, DataTable rightTable, string joinKey)
        {
            return InnerJoin(leftTable, rightTable, new[] { joinKey }, new[] { joinKey });
        }

        /// <summary>
        /// 内连接两个DataTable（多键）
        /// </summary>
        /// <param name="leftTable">左表</param>
        /// <param name="rightTable">右表</param>
        /// <param name="leftKeys">左表连接键</param>
        /// <param name="rightKeys">右表连接键</param>
        /// <returns>连接后的DataTable</returns>
        public static DataTable InnerJoin(DataTable leftTable, DataTable rightTable, string[] leftKeys, string[] rightKeys)
        {
            var result = new DataTable();

            // 创建结果表的列
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

            // 执行连接
            var leftLookup = leftTable.AsEnumerable()
                .ToLookup(row => leftKeys.Select(key => row[key]).ToArray(), new ArrayEqualityComparer());

            var rightLookup = rightTable.AsEnumerable()
                .ToLookup(row => rightKeys.Select(key => row[key]).ToArray(), new ArrayEqualityComparer());

            foreach (var key in leftLookup.Select(g => g.Key).Intersect(rightLookup.Select(g => g.Key), new ArrayEqualityComparer()))
            {
                var leftRows = leftLookup[key];
                var rightRows = rightLookup[key];

                foreach (var leftRow in leftRows)
                {
                    foreach (var rightRow in rightRows)
                    {
                        var newRow = result.NewRow();

                        // 复制左表数据
                        foreach (DataColumn column in leftTable.Columns)
                        {
                            newRow[column.ColumnName] = leftRow[column];
                        }

                        // 复制右表数据
                        foreach (DataColumn column in rightTable.Columns)
                        {
                            if (!leftKeys.Contains(column.ColumnName)) // 避免重复复制连接键
                            {
                                newRow[column.ColumnName] = rightRow[column];
                            }
                        }

                        result.Rows.Add(newRow);
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// 左连接两个DataTable
        /// </summary>
        /// <param name="leftTable">左表</param>
        /// <param name="rightTable">右表</param>
        /// <param name="joinKey">连接键</param>
        /// <returns>连接后的DataTable</returns>
        public static DataTable LeftJoin(DataTable leftTable, DataTable rightTable, string joinKey)
        {
            return LeftJoin(leftTable, rightTable, new[] { joinKey }, new[] { joinKey });
        }

        /// <summary>
        /// 左连接两个DataTable（多键）
        /// </summary>
        /// <param name="leftTable">左表</param>
        /// <param name="rightTable">右表</param>
        /// <param name="leftKeys">左表连接键</param>
        /// <param name="rightKeys">右表连接键</param>
        /// <returns>连接后的DataTable</returns>
        public static DataTable LeftJoin(DataTable leftTable, DataTable rightTable, string[] leftKeys, string[] rightKeys)
        {
            var result = new DataTable();

            // 创建结果表的列
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

            // 执行连接
            var leftLookup = leftTable.AsEnumerable()
                .ToLookup(row => leftKeys.Select(key => row[key]).ToArray(), new ArrayEqualityComparer());

            var rightLookup = rightTable.AsEnumerable()
                .ToLookup(row => rightKeys.Select(key => row[key]).ToArray(), new ArrayEqualityComparer());

            foreach (DataRow leftRow in leftTable.Rows)
            {
                var key = leftKeys.Select(key => leftRow[key]).ToArray();
                var rightRows = rightLookup[key].ToList();

                if (rightRows.Any())
                {
                    foreach (var rightRow in rightRows)
                    {
                        var newRow = result.NewRow();

                        // 复制左表数据
                        foreach (DataColumn column in leftTable.Columns)
                        {
                            newRow[column.ColumnName] = leftRow[column];
                        }

                        // 复制右表数据
                        foreach (DataColumn column in rightTable.Columns)
                        {
                            if (!rightKeys.Contains(column.ColumnName)) // 避免重复复制连接键
                            {
                                newRow[column.ColumnName] = rightRow[column];
                            }
                        }

                        result.Rows.Add(newRow);
                    }
                }
                else
                {
                    var newRow = result.NewRow();

                    // 复制左表数据
                    foreach (DataColumn column in leftTable.Columns)
                    {
                        newRow[column.ColumnName] = leftRow[column];
                    }

                    // 右表字段填充DBNull
                    foreach (DataColumn column in rightTable.Columns)
                    {
                        if (!rightKeys.Contains(column.ColumnName))
                        {
                            newRow[column.ColumnName] = DBNull.Value;
                        }
                    }

                    result.Rows.Add(newRow);
                }
            }

            return result;
        }

        /// <summary>
        /// 合并多个DataTable（垂直合并）
        /// </summary>
        /// <param name="tables">要合并的DataTable数组</param>
        /// <returns>合并后的DataTable</returns>
        public static DataTable UnionAll(params DataTable[] tables)
        {
            if (tables == null || tables.Length == 0)
                return new DataTable();

            var result = tables[0].Clone();

            foreach (var table in tables)
            {
                foreach (DataRow row in table.Rows)
                {
                    result.ImportRow(row);
                }
            }

            return result;
        }

        /// <summary>
        /// 合并多个DataTable（水平合并）
        /// </summary>
        /// <param name="leftTable">左表</param>
        /// <param name="rightTable">右表</param>
        /// <param name="joinColumn">连接列（用于对齐行）</param>
        /// <returns>合并后的DataTable</returns>
        public static DataTable MergeHorizontally(DataTable leftTable, DataTable rightTable, string joinColumn = null)
        {
            var result = new DataTable();

            // 添加左表所有列
            foreach (DataColumn column in leftTable.Columns)
            {
                result.Columns.Add($"{leftTable.TableName}_{column.ColumnName}", column.DataType);
            }

            // 添加右表所有列（避免重复）
            foreach (DataColumn column in rightTable.Columns)
            {
                var columnName = $"{rightTable.TableName}_{column.ColumnName}";
                if (!result.Columns.Contains(columnName))
                {
                    result.Columns.Add(columnName, column.DataType);
                }
            }

            // 如果指定了连接列，则按该列对齐数据
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

                    if (rightRows.Any())
                    {
                        foreach (var rightRow in rightRows)
                        {
                            var newRow = result.NewRow();

                            // 复制左表数据
                            foreach (DataColumn column in leftTable.Columns)
                            {
                                newRow[$"{leftTable.TableName}_{column.ColumnName}"] = leftRow[column];
                            }

                            // 复制右表数据
                            foreach (DataColumn column in rightTable.Columns)
                            {
                                newRow[$"{rightTable.TableName}_{column.ColumnName}"] = rightRow[column];
                            }

                            result.Rows.Add(newRow);
                        }
                    }
                    else
                    {
                        var newRow = result.NewRow();

                        // 复制左表数据
                        foreach (DataColumn column in leftTable.Columns)
                        {
                            newRow[$"{leftTable.TableName}_{column.ColumnName}"] = leftRow[column];
                        }

                        // 右表数据填充DBNull
                        foreach (DataColumn column in rightTable.Columns)
                        {
                            newRow[$"{rightTable.TableName}_{column.ColumnName}"] = DBNull.Value;
                        }

                        result.Rows.Add(newRow);
                    }
                }
            }
            else
            {
                // 简单合并（按行索引对齐）
                var maxRows = Math.Max(leftTable.Rows.Count, rightTable.Rows.Count);

                for (int i = 0; i < maxRows; i++)
                {
                    var newRow = result.NewRow();

                    // 复制左表数据
                    if (i < leftTable.Rows.Count)
                    {
                        foreach (DataColumn column in leftTable.Columns)
                        {
                            newRow[$"{leftTable.TableName}_{column.ColumnName}"] = leftTable.Rows[i][column];
                        }
                    }

                    // 复制右表数据
                    if (i < rightTable.Rows.Count)
                    {
                        foreach (DataColumn column in rightTable.Columns)
                        {
                            newRow[$"{rightTable.TableName}_{column.ColumnName}"] = rightTable.Rows[i][column];
                        }
                    }

                    result.Rows.Add(newRow);
                }
            }

            return result;
        }

        #endregion

        #region 高级查询与筛选

        /// <summary>
        /// 执行SQL风格查询
        /// </summary>
        /// <param name="dataTable">源DataTable</param>
        /// <param name="whereClause">WHERE子句</param>
        /// <param name="orderByClause">ORDER BY子句</param>
        /// <param name="selectColumns">SELECT列</param>
        /// <returns>查询结果</returns>
        public static DataTable Query(DataTable dataTable, string whereClause = null, string orderByClause = null, string[] selectColumns = null)
        {
            var result = dataTable.Clone();

            // 应用WHERE筛选
            IEnumerable<DataRow> rows = dataTable.AsEnumerable();
            if (!string.IsNullOrEmpty(whereClause))
            {
                rows = rows.Where(row => EvaluateWhereClause(row, whereClause));
            }

            // 应用ORDER BY排序
            if (!string.IsNullOrEmpty(orderByClause))
            {
                rows = ApplyOrderBy(rows, orderByClause);
            }

            // 导入筛选和排序后的行
            foreach (var row in rows)
            {
                result.ImportRow(row);
            }

            // 应用SELECT列筛选
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
        /// 评估WHERE子句
        /// </summary>
        private static bool EvaluateWhereClause(DataRow row, string whereClause)
        {
            // 简化的WHERE子句解析（实际应用中建议使用表达式树或脚本引擎）
            try
            {
                // 移除WHERE关键字
                whereClause = whereClause.TrimStart().Substring(5).Trim();

                // 简单处理等于比较
                if (whereClause.Contains("="))
                {
                    var parts = whereClause.Split('=');
                    if (parts.Length == 2)
                    {
                        var columnName = parts[0].Trim().Trim('\'', '"');
                        var value = parts[1].Trim().Trim('\'', '"');

                        if (row.Table.Columns.Contains(columnName))
                        {
                            var rowValue = row[columnName]?.ToString() ?? "";
                            return rowValue.Equals(value, StringComparison.OrdinalIgnoreCase);
                        }
                    }
                }

                // 简单处理IN操作
                if (whereClause.Contains(" IN "))
                {
                    var match = Regex.Match(whereClause, @"(\w+)\s+IN\s+\(([^)]+)\)", RegexOptions.IgnoreCase);
                    if (match.Success)
                    {
                        var columnName = match.Groups[1].Value.Trim();
                        var values = match.Groups[2].Value.Split(',')
                            .Select(v => v.Trim().Trim('\'', '"'))
                            .ToList();

                        if (row.Table.Columns.Contains(columnName))
                        {
                            var rowValue = row[columnName]?.ToString() ?? "";
                            return values.Contains(rowValue, StringComparer.OrdinalIgnoreCase);
                        }
                    }
                }

                return true; // 默认返回true
            }
            catch
            {
                return false; // 发生错误时返回false
            }
        }

        /// <summary>
        /// 应用ORDER BY排序
        /// </summary>
        private static IEnumerable<DataRow> ApplyOrderBy(IEnumerable<DataRow> rows, string orderByClause)
        {
            // 移除ORDER BY关键字
            orderByClause = orderByClause.TrimStart().Substring(8).Trim();

            var parts = orderByClause.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            if (parts.Length == 0) return rows;

            IOrderedEnumerable<DataRow> orderedRows = null;

            foreach (var part in parts)
            {
                var trimmedPart = part.Trim();
                var desc = trimmedPart.EndsWith(" DESC", StringComparison.OrdinalIgnoreCase);
                var columnName = desc ? trimmedPart.Substring(0, trimmedPart.Length - 5).Trim() : trimmedPart;

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
        /// 执行复杂筛选
        /// </summary>
        /// <param name="dataTable">源DataTable</param>
        /// <param name="filterExpression">筛选表达式</param>
        /// <returns>筛选结果</returns>
        public static DataTable Filter(DataTable dataTable, Func<DataRow, bool> filterExpression)
        {
            var result = dataTable.Clone();

            var filteredRows = dataTable.AsEnumerable().Where(filterExpression);
            foreach (var row in filteredRows)
            {
                result.ImportRow(row);
            }

            return result;
        }

        /// <summary>
        /// 执行全文搜索
        /// </summary>
        /// <param name="dataTable">源DataTable</param>
        /// <param name="searchText">搜索文本</param>
        /// <param name="searchColumns">搜索列，null表示所有列</param>
        /// <param name="caseSensitive">是否区分大小写</param>
        /// <returns>搜索结果</returns>
        public static DataTable Search(DataTable dataTable, string searchText, string[] searchColumns = null, bool caseSensitive = false)
        {
            if (string.IsNullOrEmpty(searchText))
                return dataTable.Clone();

            var result = dataTable.Clone();
            var comparison = caseSensitive ? StringComparison.Ordinal : StringComparison.OrdinalIgnoreCase;

            var rows = dataTable.AsEnumerable().Where(row =>
            {
                var columns = searchColumns ?? dataTable.Columns.Cast<DataColumn>().Select(c => c.ColumnName).ToArray();

                return columns.Any(column =>
                    row[column]?.ToString().IndexOf(searchText, comparison) >= 0);
            });

            foreach (var row in rows)
            {
                result.ImportRow(row);
            }

            return result;
        }

        /// <summary>
        /// 执行正则表达式筛选
        /// </summary>
        /// <param name="dataTable">源DataTable</param>
        /// <param name="columnName">列名</param>
        /// <param name="pattern">正则表达式模式</param>
        /// <returns>筛选结果</returns>
        public static DataTable RegexFilter(DataTable dataTable, string columnName, string pattern)
        {
            if (!dataTable.Columns.Contains(columnName))
                return dataTable.Clone();

            var result = dataTable.Clone();
            var regex = new Regex(pattern);

            var rows = dataTable.AsEnumerable().Where(row =>
                regex.IsMatch(row[columnName]?.ToString() ?? ""));

            foreach (var row in rows)
            {
                result.ImportRow(row);
            }

            return result;
        }

        #endregion
    }

    #region 辅助类和枚举

    /// <summary>
    /// 数据统计信息
    /// 包含数据的基本统计指标，如均值、中位数、标准差等
    /// </summary>
    public class DataStatistics
    {
        /// <summary>
        /// 数据总数
        /// </summary>
        public int Count { get; set; }

        /// <summary>
        /// 总和
        /// </summary>
        public double Sum { get; set; }

        /// <summary>
        /// 平均值
        /// </summary>
        public double Average { get; set; }

        /// <summary>
        /// 最小值
        /// </summary>
        public double Min { get; set; }

        /// <summary>
        /// 最大值
        /// </summary>
        public double Max { get; set; }

        /// <summary>
        /// 中位数
        /// </summary>
        public double Median { get; set; }

        /// <summary>
        /// 众数
        /// </summary>
        public double? Mode { get; set; }

        /// <summary>
        /// 方差
        /// </summary>
        public double Variance { get; set; }

        /// <summary>
        /// 标准差
        /// </summary>
        public double StandardDeviation { get; set; }

        /// <summary>
        /// 第一四分位数(Q1)
        /// </summary>
        public double Quartile1 { get; set; }

        /// <summary>
        /// 第三四分位数(Q3)
        /// </summary>
        public double Quartile3 { get; set; }
    }

    /// <summary>
    /// 数据分布信息
    /// 包含数据分布的直方图信息和分布形态指标
    /// </summary>
    public class DataDistribution
    {
        /// <summary>
        /// 直方图箱列表
        /// </summary>
        public List<HistogramBin> Bins { get; set; } = new List<HistogramBin>();

        /// <summary>
        /// 偏度，衡量分布的对称性
        /// </summary>
        public double Skewness { get; set; }

        /// <summary>
        /// 峰度，衡量分布的尖峭程度
        /// </summary>
        public double Kurtosis { get; set; }
    }

    /// <summary>
    /// 直方图箱
    /// 用于表示数据分布直方图中的一个区间
    /// </summary>
    public class HistogramBin
    {
        /// <summary>
        /// 区间最小值
        /// </summary>
        public double Min { get; set; }

        /// <summary>
        /// 区间最大值
        /// </summary>
        public double Max { get; set; }

        /// <summary>
        /// 区间内数据数量
        /// </summary>
        public int Count { get; set; }

        /// <summary>
        /// 区间内数据占总数的百分比
        /// </summary>
        public double Percentage { get; set; }
    }

    /// <summary>
    /// 异常值检测方法枚举
    /// 定义不同的异常值检测算法
    /// </summary>
    public enum OutlierDetectionMethod
    {
        /// <summary>
        /// 四分位距方法(IQR)
        /// 基于第一四分位数和第三四分位数的间距来检测异常值
        /// </summary>
        IQR,

        /// <summary>
        /// Z分数方法
        /// 基于数据点与均值的标准差倍数来检测异常值
        /// </summary>
        ZScore
    }

    /// <summary>
    /// 数组相等比较器
    /// 用于比较两个对象数组是否相等
    /// </summary>
    internal class ArrayEqualityComparer : IEqualityComparer<object[]>
    {
        /// <summary>
        /// 比较两个对象数组是否相等
        /// </summary>
        /// <param name="x">第一个数组</param>
        /// <param name="y">第二个数组</param>
        /// <returns>如果两个数组相等则返回true，否则返回false</returns>
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

        /// <summary>
        /// 计算对象数组的哈希码
        /// </summary>
        /// <param name="obj">对象数组</param>
        /// <returns>哈希码</returns>
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
    /// 对象相等比较器
    /// 用于比较两个对象是否相等
    /// </summary>
    internal class ObjectEqualityComparer : IEqualityComparer<object>
    {
        /// <summary>
        /// 比较两个对象是否相等
        /// </summary>
        /// <param name="x">第一个对象</param>
        /// <param name="y">第二个对象</param>
        /// <returns>如果两个对象相等则返回true，否则返回false</returns>
        public new bool Equals(object x, object y)
        {
            return object.Equals(x, y);
        }

        /// <summary>
        /// 计算对象的哈希码
        /// </summary>
        /// <param name="obj">对象</param>
        /// <returns>哈希码</returns>
        public int GetHashCode(object obj)
        {
            return obj?.GetHashCode() ?? 0;
        }
    }

    /// <summary>
    /// 对象比较器
    /// 用于比较两个对象的大小关系
    /// </summary>
    internal class ObjectComparer : IComparer<object>
    {
        /// <summary>
        /// 比较两个对象的大小
        /// </summary>
        /// <param name="x">第一个对象</param>
        /// <param name="y">第二个对象</param>
        /// <returns>如果x小于y返回负数，如果x等于y返回0，如果x大于y返回正数</returns>
        public int Compare(object x, object y)
        {
            if (x == null && y == null) return 0;
            if (x == null) return -1;
            if (y == null) return 1;

            // 尝试转换为可比较类型
            if (x is IComparable comparable)
            {
                return comparable.CompareTo(y);
            }

            return string.Compare(x.ToString(), y.ToString(), StringComparison.Ordinal);
        }
    }

    /// <summary>
    /// DataRow比较器
    /// 用于比较两个DataRow是否相等
    /// </summary>
    internal class DataRowComparer : IEqualityComparer<DataRow>
    {
        /// <summary>
        /// 默认比较器实例
        /// </summary>
        public static readonly DataRowComparer Default = new DataRowComparer();

        /// <summary>
        /// 比较两个DataRow是否相等
        /// </summary>
        /// <param name="x">第一个DataRow</param>
        /// <param name="y">第二个DataRow</param>
        /// <returns>如果两个DataRow相等则返回true，否则返回false</returns>
        public bool Equals(DataRow x, DataRow y)
        {
            if (x == null || y == null) return x == y;
            if (x.Table != y.Table) return false;

            for (int i = 0; i < x.Table.Columns.Count; i++)
            {
                if (!object.Equals(x[i], y[i]))
                    return false;
            }

            return true;
        }

        /// <summary>
        /// 计算DataRow的哈希码
        /// </summary>
        /// <param name="obj">DataRow对象</param>
        /// <returns>哈希码</returns>
        public int GetHashCode(DataRow obj)
        {
            if (obj == null) return 0;

            int hash = 17;
            foreach (var item in obj.ItemArray)
            {
                hash = hash * 31 + (item?.GetHashCode() ?? 0);
            }

            return hash;
        }
    }

    #endregion
}