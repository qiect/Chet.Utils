using Newtonsoft.Json.Linq;
using System.Data;

namespace Chet.Utils.Helpers
{
    /// <summary>
    /// DataTable帮助类
    /// </summary>
    public static partial class DataTableHelper
    {
        #region 补充缺失值
        /// <summary>
        /// 补充DataTable中每一列的缺失值（先向下查找补充，若后续都为空则延续上方最近的非空值）。
        /// </summary>
        /// <param name="table">需要处理的DataTable</param>
        public static void FillMissingValues(DataTable table)
        {
            if (table == null || table.Rows.Count == 0)
                return;

            int rowCount = table.Rows.Count;
            int colCount = table.Columns.Count;

            // 遍历每一列
            for (int col = 0; col < colCount; col++)
            {
                object lastValue = null;
                // 遍历每一行，处理当前列的缺失值
                for (int row = 0; row < rowCount; row++)
                {
                    var value = table.Rows[row][col];
                    if (IsNullOrEmpty(value))
                    {
                        // 向下查找第一个非空值
                        object nextValue = null;
                        for (int nextRow = row + 1; nextRow < rowCount; nextRow++)
                        {
                            var temp = table.Rows[nextRow][col];
                            if (!IsNullOrEmpty(temp))
                            {
                                nextValue = temp;
                                break;
                            }
                        }
                        // 如果找到向下的非空值，则使用该值填充
                        if (nextValue != null)
                        {
                            table.Rows[row][col] = nextValue;
                            lastValue = nextValue;
                        }
                        // 如果向下未找到非空值，但存在上方最近的非空值，则使用上方的值填充
                        else if (lastValue != null)
                        {
                            table.Rows[row][col] = lastValue;
                        }
                        // 如果lastValue也为null，则保持为空
                    }
                    else
                    {
                        // 记录当前非空值，用于后续缺失值的填充
                        lastValue = value;
                    }
                }
            }
        }

        /// <summary>
        /// 判断对象是否为null、DBNull或者空字符串
        /// </summary>
        /// <param name="value">需要判断的对象</param>
        /// <returns>如果对象为null、DBNull或者空字符串则返回true，否则返回false</returns>
        private static bool IsNullOrEmpty(object value)
        {
            return value == null || value == DBNull.Value || (value is string s && string.IsNullOrWhiteSpace(s));
        }
        #endregion

        #region JsonToData
        /// <summary>
        /// Json数组转DataTable
        /// </summary>
        /// <param name="jsonArray"></param>
        /// <returns></returns>
        public static DataTable JsonToDataTableChunked(JArray jsonArray)
        {
            var dataTable = new DataTable();

            if (jsonArray.Count == 0) return dataTable;

            // 获取列信息
            var firstItem = (JObject)jsonArray[0];
            var columns = firstItem.Properties().Select(p => p.Name).ToArray();

            foreach (var column in columns)
            {
                dataTable.Columns.Add(column, typeof(object));
            }

            dataTable.BeginLoadData();

            // 顺序处理，保持数据顺序
            foreach (var item in jsonArray)
            {
                var jObject = (JObject)item;
                var rowValues = new object[columns.Length];

                for (int j = 0; j < columns.Length; j++)
                {
                    var value = jObject[columns[j]];
                    rowValues[j] = ConvertJTokenToObject(value);
                }

                dataTable.Rows.Add(rowValues);
            }

            dataTable.EndLoadData();

            return dataTable;
        }

        private static object ConvertJTokenToObject(JToken token)
        {
            if (token == null) return DBNull.Value;

            return token.Type switch
            {
                JTokenType.Integer => token.ToObject<long>(),
                JTokenType.Float => token.ToObject<double>(),
                JTokenType.Boolean => token.ToObject<bool>(),
                JTokenType.String => token.ToString(),
                JTokenType.Null => DBNull.Value,
                JTokenType.Date => token.ToObject<DateTime>(),
                _ => token.ToString()
            };
        }
        #endregion

        #region 数据表映射
        /// <summary>
        /// 将 DataTable 中的数据映射为指定类型的实体对象列表。
        /// 映射关系通过字段映射字典指定，仅处理 DataTable 中存在且目标类型中也存在的属性。
        /// </summary>
        /// <typeparam name="T">目标实体类型，必须具有无参构造函数。</typeparam>
        /// <param name="dataTable">源数据表，包含待转换的行数据。</param>
        /// <param name="fieldMapping">字段映射字典，键为 DataTable 列名，值为目标类型的属性名。</param>
        /// <returns>转换后的实体对象列表。</returns>
        public static List<T> MapToEntities<T>(DataTable dataTable, Dictionary<string, string> fieldMapping) where T : new()
        {
            if (dataTable == null || dataTable.Rows.Count == 0)
                return new List<T>();

            var result = new List<T>(dataTable.Rows.Count);

            // 获取 DataTable 中实际存在的列名集合，用于后续映射校验
            var existingColumns = new HashSet<string>(dataTable.Columns.Cast<DataColumn>().Select(c => c.ColumnName));

            // 过滤出有效的映射关系：源列存在且目标属性也存在
            var validMapping = fieldMapping
                .Where(m => existingColumns.Contains(m.Key) &&
                           typeof(T).GetProperty(m.Value) != null)
                .ToDictionary(m => m.Key, m => m.Value);

            if (validMapping.Count == 0)
            {
                Console.WriteLine("警告: 没有有效的映射关系");
                return result;
            }

            foreach (DataRow row in dataTable.Rows)
            {
                try
                {
                    var entity = new T();
                    var entityType = typeof(T);

                    // 遍历有效映射，将数据行中的值赋给实体属性
                    foreach (var mapping in validMapping)
                    {
                        string sourceColumn = mapping.Key;
                        string targetProperty = mapping.Value;

                        var value = row[sourceColumn];
                        var property = entityType.GetProperty(targetProperty);

                        if (value != DBNull.Value && property != null && property.CanWrite)
                        {
                            property.SetValue(entity, ConvertValueSafe(value, property.PropertyType));
                        }
                    }

                    result.Add(entity);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"处理行时出错: {ex.Message}");
                    // 出错时记录日志但继续处理其他行
                }
            }

            return result;
        }

        /// <summary>
        /// 安全地将值转换为目标类型，处理 DBNull、可空类型及常见类型转换。
        /// 若转换失败则返回目标类型的默认值。
        /// </summary>
        /// <param name="value">要转换的原始值。</param>
        /// <param name="targetType">目标类型。</param>
        /// <returns>转换后的值或默认值。</returns>
        private static object ConvertValueSafe(object value, Type targetType)
        {
            try
            {
                if (value == null || value == DBNull.Value)
                {
                    // 如果目标类型是值类型且不可空，则返回其默认实例；否则返回 null
                    return targetType.IsValueType && Nullable.GetUnderlyingType(targetType) == null
                        ? Activator.CreateInstance(targetType)
                        : null;
                }

                // 处理可空类型，提取其基础类型进行转换
                if (targetType.IsGenericType && targetType.GetGenericTypeDefinition() == typeof(Nullable<>))
                {
                    targetType = Nullable.GetUnderlyingType(targetType);
                }

                // 特殊处理常见类型
                if (targetType == typeof(string))
                {
                    return value.ToString();
                }

                if (targetType == typeof(DateTime))
                {
                    return Convert.ToDateTime(value);
                }

                if (targetType == typeof(bool))
                {
                    return Convert.ToBoolean(value);
                }

                // 使用通用类型转换
                return Convert.ChangeType(value, targetType);
            }
            catch
            {
                // 转换失败时返回默认值
                return targetType.IsValueType ? Activator.CreateInstance(targetType) : null;
            }
        }
        #endregion
    }
}
