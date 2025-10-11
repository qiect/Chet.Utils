using Newtonsoft.Json.Linq;
using System.Data;

namespace Chet.Utils.Helpers
{
    /// <summary>
    /// DataTable帮助类
    /// </summary>
    public static class DataTableHelper
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

            for (int col = 0; col < colCount; col++)
            {
                object lastValue = null;
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
                        if (nextValue != null)
                        {
                            table.Rows[row][col] = nextValue;
                            lastValue = nextValue;
                        }
                        else if (lastValue != null)
                        {
                            table.Rows[row][col] = lastValue;
                        }
                        // 如果lastValue也为null，则保持为空
                    }
                    else
                    {
                        lastValue = value;
                    }
                }
            }
        }

        private static bool IsNullOrEmpty(object value)
        {
            return value == null || value == DBNull.Value || (value is string s && string.IsNullOrWhiteSpace(s));
        }
        #endregion

        #region Json2Data
        /// <summary>
        /// Json数组转DataTable
        /// </summary>
        /// <param name="jsonArray"></param>
        /// <returns></returns>
        public static DataTable Json2DataTableChunked(JArray jsonArray)
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
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dataTable"></param>
        /// <param name="fieldMapping"></param>
        /// <returns></returns>
        public static List<T> MapToEntities<T>(DataTable dataTable, Dictionary<string, string> fieldMapping) where T : new()
        {
            if (dataTable == null || dataTable.Rows.Count == 0)
                return new List<T>();

            var result = new List<T>(dataTable.Rows.Count);

            // 获取DataTable中实际存在的列
            var existingColumns = new HashSet<string>(dataTable.Columns.Cast<DataColumn>().Select(c => c.ColumnName));

            // 过滤有效的映射关系
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
                    // 可以选择记录错误行信息，但继续处理其他行
                }
            }

            return result;
        }

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

                // 处理可空类型
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
