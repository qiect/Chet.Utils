# DataTableHelper 帮助类

## 概述

[DataTableHelper](../../Chet.Utils/Helpers/DataTableHelper.cs) 是一个静态帮助类，为 DataTable 提供了丰富的高级数据处理功能，包括数据转换、数据透视、数据分析、数据导出、数据清洗、数据连接、高级查询等，旨在补充 DataTableExtensions 中未包含的高级功能，提高数据处理的效率和灵活性。

## 主要特性

- 🔄 数据转换（动态对象、JSON 转 DataTable、列名映射）
- 📊 数据透视（透视表、高级透视、分组聚合）
- 📈 数据分析（统计计算、分布分析、相关性分析）
- 📤 数据导出（HTML、Excel XML、Markdown）
- 🧹 数据清洗（验证、清洗、填充缺失值、异常值检测）
- 🔗 数据连接（内连接、左连接、全连接、交叉连接）
- 🔍 高级查询（SQL 风格查询、全文搜索、正则筛选）

## 类定义

```csharp
public static partial class DataTableHelper
```

---

## 数据转换

### ToDynamicList

将 DataTable 转换为动态对象列表。

```csharp
public static List<dynamic> ToDynamicList(DataTable dataTable)
```

**参数：**
- `dataTable`: 源 DataTable

**返回值：** 动态对象列表，每行转换为一个 ExpandoObject

### ToDataTableFromJson

将 JSON 字符串转换为 DataTable。

```csharp
public static DataTable ToDataTableFromJson(string json)
```

**参数：**
- `json`: JSON 字符串，必须是对象数组格式

**返回值：** 转换后的 DataTable

### MapColumns

映射 DataTable 的列名。

```csharp
public static DataTable MapColumns(DataTable dataTable, Dictionary<string, string> columnMappings)
```

**参数：**
- `dataTable`: 源 DataTable
- `columnMappings`: 列映射字典（原列名 -> 新列名）

### ToDataTableFromObjects

将对象列表转换为 DataTable。

```csharp
public static DataTable ToDataTableFromObjects<T>(IEnumerable<T> objects, string tableName = null)
```

---

## 数据透视

### Pivot

创建数据透视表。

```csharp
public static DataTable Pivot(
    DataTable dataTable,
    string rowField,
    string columnField,
    string dataField,
    Func<IEnumerable<object>, object> aggregateFunction = null)
```

**参数：**
- `dataTable`: 源 DataTable
- `rowField`: 行字段名
- `columnField`: 列字段名
- `dataField`: 数据字段名
- `aggregateFunction`: 聚合函数，默认为求和

### AdvancedPivot

创建高级数据透视表，支持多行字段、多列字段和多数据字段。

```csharp
public static DataTable AdvancedPivot(
    DataTable dataTable,
    string[] rowFields,
    string[] columnFields,
    Dictionary<string, Func<IEnumerable<object>, object>> dataFields)
```

### GroupByAdvanced

高级分组聚合操作，支持多个分组列和多个聚合列。

```csharp
public static DataTable GroupByAdvanced(
    DataTable dataTable,
    string[] groupByColumns,
    Dictionary<string, Func<IEnumerable<object>, object>> aggregateColumns)
```

---

## 数据分析

### CalculateStatistics

计算数值列的统计信息。

```csharp
public static Dictionary<string, double> CalculateStatistics(DataTable dataTable, string columnName)
```

**返回值：** 包含 Mean、Median、Mode、Variance、StandardDeviation、Min、Max、Sum、Count 等统计值

### AnalyzeDistribution

分析数据分布情况。

```csharp
public static Dictionary<string, int> AnalyzeDistribution(DataTable dataTable, string columnName, int bins = 10)
```

### CalculateCorrelation

计算两列之间的相关系数。

```csharp
public static double CalculateCorrelation(DataTable dataTable, string column1, string column2)
```

---

## 数据导出

### ExportToHtml

将 DataTable 导出为 HTML 表格。

```csharp
public static string ExportToHtml(DataTable dataTable, string tableId = null, string tableClass = null)
```

### ExportToExcelXml

将 DataTable 导出为 Excel XML 格式。

```csharp
public static string ExportToExcelXml(DataTable dataTable, string sheetName = "Sheet1")
```

### ExportToMarkdown

将 DataTable 导出为 Markdown 表格格式。

```csharp
public static string ExportToMarkdown(DataTable dataTable)
```

---

## 数据清洗

### ValidateData

验证数据完整性。

```csharp
public static List<DataValidationResult> ValidateData(DataTable dataTable, Dictionary<string, Func<object, bool>> validationRules)
```

### CleanData

清洗数据（移除无效行）。

```csharp
public static DataTable CleanData(DataTable dataTable, Func<DataRow, bool> predicate)
```

### FillMissingValues

填充缺失值。

```csharp
public static DataTable FillMissingValues(DataTable dataTable, string columnName, object fillValue)
public static DataTable FillMissingValues(DataTable dataTable, string columnName, FillMethod fillMethod)
```

**FillMethod 枚举：**
- `Previous`: 使用前一个非空值
- `Next`: 使用后一个非空值
- `Mean`: 使用平均值
- `Median`: 使用中位数
- `Mode`: 使用众数

### DetectOutliers

检测异常值（使用 IQR 方法）。

```csharp
public static List<int> DetectOutliers(DataTable dataTable, string columnName, double iqrMultiplier = 1.5)
```

---

## 数据连接

### InnerJoin

内连接两个 DataTable。

```csharp
public static DataTable InnerJoin(DataTable left, DataTable right, string leftColumn, string rightColumn)
```

### LeftJoin

左连接两个 DataTable。

```csharp
public static DataTable LeftJoin(DataTable left, DataTable right, string leftColumn, string rightColumn)
```

### FullJoin

全连接两个 DataTable。

```csharp
public static DataTable FullJoin(DataTable left, DataTable right, string leftColumn, string rightColumn)
```

### CrossJoin

交叉连接两个 DataTable。

```csharp
public static DataTable CrossJoin(DataTable left, DataTable right)
```

### UnionAll

垂直合并多个 DataTable。

```csharp
public static DataTable UnionAll(params DataTable[] dataTables)
```

### MergeHorizontally

水平合并两个 DataTable。

```csharp
public static DataTable MergeHorizontally(DataTable left, DataTable right)
```

---

## 高级查询

### Query

SQL 风格查询。

```csharp
public static DataTable Query(DataTable dataTable, string whereClause = null, string orderBy = null, int? limit = null)
```

### Search

全文搜索。

```csharp
public static DataTable Search(DataTable dataTable, string keyword, string[] searchColumns = null)
```

### RegexFilter

正则表达式筛选。

```csharp
public static DataTable RegexFilter(DataTable dataTable, string columnName, string pattern)
```

---

## 使用示例

### 数据透视示例

```csharp
using Chet.Utils.Helpers;

// 创建销售数据表
var salesDt = new DataTable();
salesDt.Columns.Add("Year", typeof(int));
salesDt.Columns.Add("Product", typeof(string));
salesDt.Columns.Add("Amount", typeof(decimal));

// 添加数据...

// 创建透视表：按年份（行）和产品（列）统计销售额
var pivotTable = DataTableHelper.Pivot(
    salesDt,
    "Year",
    "Product",
    "Amount",
    values => values.Sum(v => Convert.ToDecimal(v))
);
```

### 数据分析示例

```csharp
// 计算统计信息
var stats = DataTableHelper.CalculateStatistics(salesDt, "Amount");
Console.WriteLine($"平均值: {stats["Mean"]}");
Console.WriteLine($"标准差: {stats["StandardDeviation"]}");
Console.WriteLine($"最小值: {stats["Min"]}");
Console.WriteLine($"最大值: {stats["Max"]}");

// 检测异常值
var outlierRows = DataTableHelper.DetectOutliers(salesDt, "Amount");
Console.WriteLine($"发现 {outlierRows.Count} 个异常值");
```

### 数据连接示例

```csharp
// 内连接
var joined = DataTableHelper.InnerJoin(
    customersDt,
    ordersDt,
    "CustomerId",
    "CustomerId"
);

// 左连接
var leftJoined = DataTableHelper.LeftJoin(
    customersDt,
    ordersDt,
    "CustomerId",
    "CustomerId"
);
```

### 数据导出示例

```csharp
// 导出为 HTML
string html = DataTableHelper.ExportToHtml(dataTable, "myTable", "table table-striped");

// 导出为 Markdown
string markdown = DataTableHelper.ExportToMarkdown(dataTable);

// 导出为 Excel XML
string excelXml = DataTableHelper.ExportToExcelXml(dataTable, "Sales Data");
```

---

## 注意事项

1. 本类提供 DataTableExtensions 中未包含的高级功能。
2. 基础功能请使用 DataTableExtensions 扩展方法。
3. 大数据量操作时注意内存使用。
4. 数据连接操作会创建新的 DataTable。

---

## 版本兼容性

- .NET Framework 4.6.1 及以上版本
- .NET Core 2.0 及以上版本
- .NET 5.0 及以上版本
