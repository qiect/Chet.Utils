# DataTableExtensions 功能文档

## 概述

[DataTableExtensions](../../Chet.Utils/Extensions/DataTableExtensions.cs) 是一个静态扩展类，为 `DataTable` 类型提供了丰富的扩展方法，涵盖数据转换、查询筛选、聚合统计、数据操作等功能，旨在简化 DataTable 的常用操作，提高数据处理效率和代码可读性。

## 主要功能模块

### 1. 基础判断

提供 DataTable 状态相关的数据判断方法。

#### IsNullOrEmpty

判断 DataTable 是否为 null 或无行数据。

```csharp
public static bool IsNullOrEmpty(this DataTable dt)
```

**参数：**

- `dt`: 待判断的 DataTable

**返回值：**

- 如果为 null 或无行数据返回 true；否则返回 false

**示例：**

```csharp
DataTable dt1 = null;
DataTable dt2 = new DataTable();
DataTable dt3 = new DataTable();
dt3.Columns.Add("Name");
dt3.Rows.Add("张三");

bool result1 = dt1.IsNullOrEmpty(); // true
bool result2 = dt2.IsNullOrEmpty(); // true
bool result3 = dt3.IsNullOrEmpty(); // false
```

#### HasRows

判断 DataTable 是否有数据行。

```csharp
public static bool HasRows(this DataTable dt)
```

**参数：**

- `dt`: 待判断的 DataTable

**返回值：**

- 如果有数据行返回 true；否则返回 false

**示例：**

```csharp
DataTable dt = new DataTable();
dt.Columns.Add("Name");
dt.Rows.Add("张三");

bool result = dt.HasRows(); // true
```

#### HasColumns

判断 DataTable 是否有列。

```csharp
public static bool HasColumns(this DataTable dt)
```

**参数：**

- `dt`: 待判断的 DataTable

**返回值：**

- 如果有列返回 true；否则返回 false

**示例：**

```csharp
DataTable dt1 = new DataTable();
DataTable dt2 = new DataTable();
dt2.Columns.Add("Name");

bool result1 = dt1.HasColumns(); // false
bool result2 = dt2.HasColumns(); // true
```

#### ContainsColumn

判断 DataTable 是否包含指定列名。

```csharp
public static bool ContainsColumn(this DataTable dt, string columnName)
```

**参数：**

- `dt`: 待判断的 DataTable
- `columnName`: 列名

**返回值：**

- 如果包含指定列返回 true；否则返回 false

**示例：**

```csharp
DataTable dt = new DataTable();
dt.Columns.Add("Name");
dt.Columns.Add("Age");

bool result1 = dt.ContainsColumn("Name"); // true
bool result2 = dt.ContainsColumn("Address"); // false
```

#### ContainsRow

判断 DataTable 是否包含指定行。

```csharp
public static bool ContainsRow(this DataTable dt, int rowIndex)
```

**参数：**

- `dt`: 待判断的 DataTable
- `rowIndex`: 行索引

**返回值：**

- 如果包含指定行返回 true；否则返回 false

**示例：**

```csharp
DataTable dt = new DataTable();
dt.Columns.Add("Name");
dt.Rows.Add("张三");
dt.Rows.Add("李四");

bool result1 = dt.ContainsRow(0); // true
bool result2 = dt.ContainsRow(5); // false
```

#### GetRowCount

获取 DataTable 的行数。

```csharp
public static int GetRowCount(this DataTable dt)
```

**参数：**

- `dt`: 待处理的 DataTable

**返回值：**

- 行数，如果为 null 返回 0

**示例：**

```csharp
DataTable dt = new DataTable();
dt.Columns.Add("Name");
dt.Rows.Add("张三");
dt.Rows.Add("李四");

int count = dt.GetRowCount(); // 2
```

#### GetColumnCount

获取 DataTable 的列数。

```csharp
public static int GetColumnCount(this DataTable dt)
```

**参数：**

- `dt`: 待处理的 DataTable

**返回值：**

- 列数，如果为 null 返回 0

**示例：**

```csharp
DataTable dt = new DataTable();
dt.Columns.Add("Name");
dt.Columns.Add("Age");

int count = dt.GetColumnCount(); // 2
```

### 2. 转换操作

提供 DataTable 与其他数据结构之间的转换功能。

#### ToList<T> (使用转换器)

将 DataTable 转换为泛型集合。

```csharp
public static List<T> ToList<T>(this DataTable dt, Func<DataRow, T> converter)
```

**参数：**

- `dt`: 待转换的 DataTable
- `converter`: 行转换委托

**返回值：**

- 转换后的泛型集合

**示例：**

```csharp
DataTable dt = new DataTable();
dt.Columns.Add("Name", typeof(string));
dt.Columns.Add("Age", typeof(int));
dt.Rows.Add("张三", 25);
dt.Rows.Add("李四", 30);

var list = dt.ToList(row => new Person 
{ 
    Name = row["Name"].ToString(), 
    Age = Convert.ToInt32(row["Age"]) 
});
```

#### ToList<T> (自动映射)

将 DataTable 转换为泛型集合（自动映射）。

```csharp
public static List<T> ToList<T>(this DataTable dt) where T : new()
```

**参数：**

- `dt`: 待转换的 DataTable

**返回值：**

- 转换后的泛型集合

**示例：**

```csharp
DataTable dt = new DataTable();
dt.Columns.Add("Name", typeof(string));
dt.Columns.Add("Age", typeof(int));
dt.Rows.Add("张三", 25);
dt.Rows.Add("李四", 30);

var list = dt.ToList<Person>();
```

#### GetColumnNames

获取 DataTable 的所有列名。

```csharp
public static List<string> GetColumnNames(this DataTable dt)
```

**参数：**

- `dt`: 待处理的 DataTable

**返回值：**

- 列名列表

**示例：**

```csharp
DataTable dt = new DataTable();
dt.Columns.Add("Name");
dt.Columns.Add("Age");

var names = dt.GetColumnNames(); // ["Name", "Age"]
```

#### GetColumnTypes

获取 DataTable 的所有列类型。

```csharp
public static Dictionary<string, Type> GetColumnTypes(this DataTable dt)
```

**参数：**

- `dt`: 待处理的 DataTable

**返回值：**

- 列类型字典（列名 -> 类型）

**示例：**

```csharp
DataTable dt = new DataTable();
dt.Columns.Add("Name", typeof(string));
dt.Columns.Add("Age", typeof(int));

var types = dt.GetColumnTypes(); 
// { "Name": typeof(string), "Age": typeof(int) }
```

#### ToDictionaryList

获取 DataTable 的所有行数据（每行为字典）。

```csharp
public static List<Dictionary<string, object>> ToDictionaryList(this DataTable dt)
```

**参数：**

- `dt`: 待处理的 DataTable

**返回值：**

- 字典列表

**示例：**

```csharp
DataTable dt = new DataTable();
dt.Columns.Add("Name", typeof(string));
dt.Columns.Add("Age", typeof(int));
dt.Rows.Add("张三", 25);

var list = dt.ToDictionaryList();
// [{ "Name": "张三", "Age": 25 }]
```

#### ToArray

DataTable 转为二维数组。

```csharp
public static object[,] ToArray(this DataTable dt)
```

**参数：**

- `dt`: 待转换的 DataTable

**返回值：**

- 二维数组

**示例：**

```csharp
DataTable dt = new DataTable();
dt.Columns.Add("Name", typeof(string));
dt.Columns.Add("Age", typeof(int));
dt.Rows.Add("张三", 25);
dt.Rows.Add("李四", 30);

var arr = dt.ToArray();
// arr[0, 0] = "张三", arr[0, 1] = 25
// arr[1, 0] = "李四", arr[1, 1] = 30
```

#### ToCsv

DataTable 转为 CSV 格式字符串。

```csharp
public static string ToCsv(this DataTable dt, bool includeHeader = true, char separator = ',')
```

**参数：**

- `dt`: 待转换的 DataTable
- `includeHeader`: 是否包含列头，默认 true
- `separator`: 分隔符，默认逗号

**返回值：**

- CSV 格式字符串

**示例：**

```csharp
DataTable dt = new DataTable();
dt.Columns.Add("Name", typeof(string));
dt.Columns.Add("Age", typeof(int));
dt.Rows.Add("张三", 25);

string csv = dt.ToCsv();
// "Name,Age\n张三,25\n"

string csvNoHeader = dt.ToCsv(includeHeader: false);
// "张三,25\n"
```

#### ToJson

DataTable 转为 JSON 格式字符串。

```csharp
public static string ToJson(this DataTable dt)
```

**参数：**

- `dt`: 待转换的 DataTable

**返回值：**

- JSON 格式字符串

**示例：**

```csharp
DataTable dt = new DataTable();
dt.Columns.Add("Name", typeof(string));
dt.Columns.Add("Age", typeof(int));
dt.Rows.Add("张三", 25);

string json = dt.ToJson();
// [{"Name":"张三","Age":25}]
```

### 3. 查询筛选

提供数据查询和筛选功能。

#### Filter

DataTable 按条件筛选，返回新 DataTable。

```csharp
public static DataTable Filter(this DataTable dt, string filter)
```

**参数：**

- `dt`: 待筛选的 DataTable
- `filter`: 筛选表达式，如 "Age > 18 AND Name LIKE '张%'"

**返回值：**

- 筛选后的新 DataTable

**示例：**

```csharp
DataTable dt = new DataTable();
dt.Columns.Add("Name", typeof(string));
dt.Columns.Add("Age", typeof(int));
dt.Rows.Add("张三", 25);
dt.Rows.Add("李四", 17);
dt.Rows.Add("王五", 30);

var filtered = dt.Filter("Age > 18");
// 只包含张三和王五的行
```

#### Sort

DataTable 按指定列排序，返回新 DataTable。

```csharp
public static DataTable Sort(this DataTable dt, string sort)
```

**参数：**

- `dt`: 待排序的 DataTable
- `sort`: 排序表达式，如 "Age DESC, Name ASC"

**返回值：**

- 排序后的新 DataTable

**示例：**

```csharp
DataTable dt = new DataTable();
dt.Columns.Add("Name", typeof(string));
dt.Columns.Add("Age", typeof(int));
dt.Rows.Add("张三", 25);
dt.Rows.Add("李四", 30);
dt.Rows.Add("王五", 20);

var sorted = dt.Sort("Age DESC");
// 按年龄降序排列
```

#### FilterAndSort

DataTable 按条件筛选并排序，返回新 DataTable。

```csharp
public static DataTable FilterAndSort(this DataTable dt, string filter, string sort)
```

**参数：**

- `dt`: 待处理的 DataTable
- `filter`: 筛选表达式
- `sort`: 排序表达式

**返回值：**

- 处理后的新 DataTable

**示例：**

```csharp
DataTable dt = new DataTable();
dt.Columns.Add("Name", typeof(string));
dt.Columns.Add("Age", typeof(int));
dt.Rows.Add("张三", 25);
dt.Rows.Add("李四", 17);
dt.Rows.Add("王五", 30);

var result = dt.FilterAndSort("Age > 18", "Age DESC");
// 筛选年龄大于18岁并按年龄降序排列
```

#### SelectRows

获取满足条件的 DataRow 数组。

```csharp
public static DataRow[] SelectRows(this DataTable dt, string filter)
```

**参数：**

- `dt`: 待查询的 DataTable
- `filter`: 筛选表达式

**返回值：**

- 满足条件的 DataRow 数组

**示例：**

```csharp
DataTable dt = new DataTable();
dt.Columns.Add("Name", typeof(string));
dt.Columns.Add("Age", typeof(int));
dt.Rows.Add("张三", 25);
dt.Rows.Add("李四", 17);

DataRow[] rows = dt.SelectRows("Age >= 18");
```

#### Distinct

DataTable 按指定列去重，返回新 DataTable。

```csharp
public static DataTable Distinct(this DataTable dt, params string[] columnNames)
```

**参数：**

- `dt`: 待去重的 DataTable
- `columnNames`: 去重依据的列名数组

**返回值：**

- 去重后的新 DataTable

**示例：**

```csharp
DataTable dt = new DataTable();
dt.Columns.Add("Name", typeof(string));
dt.Columns.Add("Age", typeof(int));
dt.Rows.Add("张三", 25);
dt.Rows.Add("张三", 25);
dt.Rows.Add("李四", 30);

var distinct = dt.Distinct("Name", "Age");
// 只保留两行：张三/25 和 李四/30
```

#### Top

获取 DataTable 前 N 行数据。

```csharp
public static DataTable Top(this DataTable dt, int count)
```

**参数：**

- `dt`: 待处理的 DataTable
- `count`: 行数

**返回值：**

- 前 N 行的新 DataTable

**示例：**

```csharp
DataTable dt = new DataTable();
dt.Columns.Add("Name", typeof(string));
dt.Rows.Add("张三");
dt.Rows.Add("李四");
dt.Rows.Add("王五");

var top2 = dt.Top(2); // 只包含张三和李四
```

#### Bottom

获取 DataTable 后 N 行数据。

```csharp
public static DataTable Bottom(this DataTable dt, int count)
```

**参数：**

- `dt`: 待处理的 DataTable
- `count`: 行数

**返回值：**

- 后 N 行的新 DataTable

**示例：**

```csharp
DataTable dt = new DataTable();
dt.Columns.Add("Name", typeof(string));
dt.Rows.Add("张三");
dt.Rows.Add("李四");
dt.Rows.Add("王五");

var bottom2 = dt.Bottom(2); // 只包含李四和王五
```

#### Range

获取 DataTable 指定范围的行数据。

```csharp
public static DataTable Range(this DataTable dt, int startIndex, int count)
```

**参数：**

- `dt`: 待处理的 DataTable
- `startIndex`: 起始索引（从 0 开始）
- `count`: 行数

**返回值：**

- 指定范围的新 DataTable

**示例：**

```csharp
DataTable dt = new DataTable();
dt.Columns.Add("Name", typeof(string));
dt.Rows.Add("张三");
dt.Rows.Add("李四");
dt.Rows.Add("王五");
dt.Rows.Add("赵六");

var range = dt.Range(1, 2); // 只包含李四和王五
```

### 4. 聚合统计

提供数据聚合和统计功能。

#### Sum

计算指定列的总和。

```csharp
public static decimal Sum(this DataTable dt, string columnName, string filter = null)
```

**参数：**

- `dt`: 待计算的 DataTable
- `columnName`: 列名
- `filter`: 筛选条件（可选）

**返回值：**

- 总和

**示例：**

```csharp
DataTable dt = new DataTable();
dt.Columns.Add("Name", typeof(string));
dt.Columns.Add("Amount", typeof(decimal));
dt.Rows.Add("张三", 100m);
dt.Rows.Add("李四", 200m);
dt.Rows.Add("王五", 150m);

decimal total = dt.Sum("Amount"); // 450
decimal filteredSum = dt.Sum("Amount", "Name LIKE '张%'"); // 100
```

#### Average

计算指定列的平均值。

```csharp
public static decimal Average(this DataTable dt, string columnName, string filter = null)
```

**参数：**

- `dt`: 待计算的 DataTable
- `columnName`: 列名
- `filter`: 筛选条件（可选）

**返回值：**

- 平均值

**示例：**

```csharp
DataTable dt = new DataTable();
dt.Columns.Add("Name", typeof(string));
dt.Columns.Add("Amount", typeof(decimal));
dt.Rows.Add("张三", 100m);
dt.Rows.Add("李四", 200m);

decimal avg = dt.Average("Amount"); // 150
```

#### Min

获取指定列的最小值。

```csharp
public static object Min(this DataTable dt, string columnName, string filter = null)
```

**参数：**

- `dt`: 待处理的 DataTable
- `columnName`: 列名
- `filter`: 筛选条件（可选）

**返回值：**

- 最小值

**示例：**

```csharp
DataTable dt = new DataTable();
dt.Columns.Add("Name", typeof(string));
dt.Columns.Add("Age", typeof(int));
dt.Rows.Add("张三", 25);
dt.Rows.Add("李四", 30);

object min = dt.Min("Age"); // 25
```

#### Max

获取指定列的最大值。

```csharp
public static object Max(this DataTable dt, string columnName, string filter = null)
```

**参数：**

- `dt`: 待处理的 DataTable
- `columnName`: 列名
- `filter`: 筛选条件（可选）

**返回值：**

- 最大值

**示例：**

```csharp
DataTable dt = new DataTable();
dt.Columns.Add("Name", typeof(string));
dt.Columns.Add("Age", typeof(int));
dt.Rows.Add("张三", 25);
dt.Rows.Add("李四", 30);

object max = dt.Max("Age"); // 30
```

#### Count

计算满足条件的行数。

```csharp
public static int Count(this DataTable dt, string filter = null)
```

**参数：**

- `dt`: 待计算的 DataTable
- `filter`: 筛选条件（可选）

**返回值：**

- 行数

**示例：**

```csharp
DataTable dt = new DataTable();
dt.Columns.Add("Name", typeof(string));
dt.Columns.Add("Age", typeof(int));
dt.Rows.Add("张三", 25);
dt.Rows.Add("李四", 17);
dt.Rows.Add("王五", 30);

int total = dt.Count(); // 3
int adults = dt.Count("Age >= 18"); // 2
```

#### CountNonNull

计算指定列的非空值数量。

```csharp
public static int CountNonNull(this DataTable dt, string columnName, string filter = null)
```

**参数：**

- `dt`: 待计算的 DataTable
- `columnName`: 列名
- `filter`: 筛选条件（可选）

**返回值：**

- 非空值数量

**示例：**

```csharp
DataTable dt = new DataTable();
dt.Columns.Add("Name", typeof(string));
dt.Columns.Add("Email", typeof(string));
dt.Rows.Add("张三", "zhangsan@example.com");
dt.Rows.Add("李四", DBNull.Value);
dt.Rows.Add("王五", "wangwu@example.com");

int count = dt.CountNonNull("Email"); // 2
```

#### GetStatistics

获取聚合统计结果。

```csharp
public static Dictionary<string, object> GetStatistics(this DataTable dt, string columnName, string filter = null)
```

**参数：**

- `dt`: 待处理的 DataTable
- `columnName`: 列名
- `filter`: 筛选条件（可选）

**返回值：**

- 包含 Sum、Avg、Min、Max、Count 的字典

**示例：**

```csharp
DataTable dt = new DataTable();
dt.Columns.Add("Name", typeof(string));
dt.Columns.Add("Amount", typeof(decimal));
dt.Rows.Add("张三", 100m);
dt.Rows.Add("李四", 200m);
dt.Rows.Add("王五", 150m);

var stats = dt.GetStatistics("Amount");
// stats["Sum"] = 450
// stats["Avg"] = 150
// stats["Min"] = 100
// stats["Max"] = 200
// stats["Count"] = 3
```

### 5. 行列操作

提供行和列的增删改操作。

#### AddRow (参数数组)

DataTable 添加一行数据。

```csharp
public static void AddRow(this DataTable dt, params object[] values)
```

**参数：**

- `dt`: 目标 DataTable
- `values`: 各列的值，顺序需与列一致

**示例：**

```csharp
DataTable dt = new DataTable();
dt.Columns.Add("Name", typeof(string));
dt.Columns.Add("Age", typeof(int));

dt.AddRow("张三", 25);
dt.AddRow("李四", 30);
```

#### AddRow (字典)

DataTable 添加一行数据（使用字典）。

```csharp
public static void AddRow(this DataTable dt, Dictionary<string, object> data)
```

**参数：**

- `dt`: 目标 DataTable
- `data`: 列名和值的字典

**示例：**

```csharp
DataTable dt = new DataTable();
dt.Columns.Add("Name", typeof(string));
dt.Columns.Add("Age", typeof(int));

dt.AddRow(new Dictionary<string, object> { { "Name", "张三" }, { "Age", 25 } });
```

#### AddColumn

DataTable 添加一列。

```csharp
public static void AddColumn(this DataTable dt, string columnName, Type type = null, object defaultValue = null)
```

**参数：**

- `dt`: 目标 DataTable
- `columnName`: 列名
- `type`: 列类型，默认 typeof(string)
- `defaultValue`: 默认值

**示例：**

```csharp
DataTable dt = new DataTable();
dt.AddColumn("Name");
dt.AddColumn("Age", typeof(int));
dt.AddColumn("IsActive", typeof(bool), true);
```

#### RemoveColumn

DataTable 删除指定列。

```csharp
public static void RemoveColumn(this DataTable dt, string columnName)
```

**参数：**

- `dt`: 目标 DataTable
- `columnName`: 列名

**示例：**

```csharp
DataTable dt = new DataTable();
dt.Columns.Add("Name");
dt.Columns.Add("Temp");

dt.RemoveColumn("Temp");
```

#### RenameColumn

DataTable 重命名列。

```csharp
public static void RenameColumn(this DataTable dt, string oldName, string newName)
```

**参数：**

- `dt`: 目标 DataTable
- `oldName`: 原列名
- `newName`: 新列名

**示例：**

```csharp
DataTable dt = new DataTable();
dt.Columns.Add("OldName");

dt.RenameColumn("OldName", "NewName");
```

#### ClearRows

DataTable 删除所有行。

```csharp
public static void ClearRows(this DataTable dt)
```

**参数：**

- `dt`: 待清空的 DataTable

**示例：**

```csharp
DataTable dt = new DataTable();
dt.Columns.Add("Name");
dt.Rows.Add("张三");
dt.Rows.Add("李四");

dt.ClearRows(); // 清空所有行，保留结构
```

#### DeleteRows

DataTable 删除满足条件的行。

```csharp
public static void DeleteRows(this DataTable dt, string filter)
```

**参数：**

- `dt`: 目标 DataTable
- `filter`: 筛选表达式

**示例：**

```csharp
DataTable dt = new DataTable();
dt.Columns.Add("Name", typeof(string));
dt.Columns.Add("Age", typeof(int));
dt.Rows.Add("张三", 25);
dt.Rows.Add("李四", 17);
dt.Rows.Add("王五", 30);

dt.DeleteRows("Age < 18"); // 删除年龄小于18的行
```

#### SetValue

设置指定单元格的值。

```csharp
public static void SetValue(this DataTable dt, int rowIndex, string columnName, object value)
```

**参数：**

- `dt`: 目标 DataTable
- `rowIndex`: 行索引
- `columnName`: 列名
- `value`: 值

**示例：**

```csharp
DataTable dt = new DataTable();
dt.Columns.Add("Name", typeof(string));
dt.Rows.Add("张三");

dt.SetValue(0, "Name", "李四");
```

#### GetValue

获取指定单元格的值。

```csharp
public static object GetValue(this DataTable dt, int rowIndex, string columnName)
```

**参数：**

- `dt`: 目标 DataTable
- `rowIndex`: 行索引
- `columnName`: 列名

**返回值：**

- 单元格值，如果不存在返回 null

**示例：**

```csharp
DataTable dt = new DataTable();
dt.Columns.Add("Name", typeof(string));
dt.Rows.Add("张三");

object value = dt.GetValue(0, "Name"); // "张三"
```

#### GetValue<T>

获取指定单元格的值并转换为指定类型。

```csharp
public static T GetValue<T>(this DataTable dt, int rowIndex, string columnName, T defaultValue = default)
```

**参数：**

- `dt`: 目标 DataTable
- `rowIndex`: 行索引
- `columnName`: 列名
- `defaultValue`: 默认值

**返回值：**

- 转换后的值

**示例：**

```csharp
DataTable dt = new DataTable();
dt.Columns.Add("Age", typeof(int));
dt.Rows.Add(25);

int age = dt.GetValue<int>(0, "Age"); // 25
int missing = dt.GetValue<int>(0, "Missing", -1); // -1
```

### 6. 数据操作

提供数据复制、合并等操作。

#### CopyAll

DataTable 克隆结构并复制所有数据。

```csharp
public static DataTable CopyAll(this DataTable dt)
```

**参数：**

- `dt`: 待复制的 DataTable

**返回值：**

- 复制的 DataTable

**示例：**

```csharp
DataTable dt = new DataTable();
dt.Columns.Add("Name");
dt.Rows.Add("张三");

DataTable copy = dt.CopyAll();
```

#### CloneStructure

DataTable 克隆结构但不复制数据。

```csharp
public static DataTable CloneStructure(this DataTable dt)
```

**参数：**

- `dt`: 待克隆的 DataTable

**返回值：**

- 克隆结构的 DataTable

**示例：**

```csharp
DataTable dt = new DataTable();
dt.Columns.Add("Name");
dt.Rows.Add("张三");

DataTable clone = dt.CloneStructure(); // 只有结构，无数据
```

#### Merge

合并两个 DataTable。

```csharp
public static DataTable Merge(this DataTable dt, DataTable other, bool preserveChanges = true)
```

**参数：**

- `dt`: 目标 DataTable
- `other`: 待合并的 DataTable
- `preserveChanges`: 是否保留现有更改

**返回值：**

- 合并后的 DataTable

**示例：**

```csharp
DataTable dt1 = new DataTable();
dt1.Columns.Add("Name");
dt1.Rows.Add("张三");

DataTable dt2 = new DataTable();
dt2.Columns.Add("Name");
dt2.Rows.Add("李四");

DataTable merged = dt1.Merge(dt2); // 包含张三和李四
```

#### AsEnumerableDictionary

DataTable 转为一行一字典的枚举（便于遍历）。

```csharp
public static IEnumerable<IDictionary<string, object>> AsEnumerableDictionary(this DataTable dt)
```

**参数：**

- `dt`: 待处理的 DataTable

**返回值：**

- 字典枚举

**示例：**

```csharp
DataTable dt = new DataTable();
dt.Columns.Add("Name", typeof(string));
dt.Columns.Add("Age", typeof(int));
dt.Rows.Add("张三", 25);
dt.Rows.Add("李四", 30);

foreach (var row in dt.AsEnumerableDictionary())
{
    Console.WriteLine($"Name: {row["Name"]}, Age: {row["Age"]}");
}
```

#### ConvertDbNullToNull

将 DBNull 值转换为 null。

```csharp
public static void ConvertDbNullToNull(this DataTable dt)
```

**参数：**

- `dt`: 目标 DataTable

**示例：**

```csharp
DataTable dt = new DataTable();
dt.Columns.Add("Name", typeof(string));
dt.Rows.Add(DBNull.Value);

dt.ConvertDbNullToNull();
// DBNull.Value 被转换为 null
```

### 7. 分组操作

提供数据分组和聚合功能。

#### GroupBy

DataTable 按指定列分组。

```csharp
public static Dictionary<object, List<DataRow>> GroupBy(this DataTable dt, string groupColumn)
```

**参数：**

- `dt`: 待分组的 DataTable
- `groupColumn`: 分组列名

**返回值：**

- 分组字典（分组键 -> 行列表）

**示例：**

```csharp
DataTable dt = new DataTable();
dt.Columns.Add("Department", typeof(string));
dt.Columns.Add("Name", typeof(string));
dt.Rows.Add("研发部", "张三");
dt.Rows.Add("研发部", "李四");
dt.Rows.Add("市场部", "王五");

var groups = dt.GroupBy("Department");
// groups["研发部"] = [张三, 李四]
// groups["市场部"] = [王五]
```

#### GroupBySum

DataTable 按指定列分组并求和。

```csharp
public static DataTable GroupBySum(this DataTable dt, string groupColumn, string sumColumn)
```

**参数：**

- `dt`: 待分组的 DataTable
- `groupColumn`: 分组列名
- `sumColumn`: 求和列名

**返回值：**

- 分组求和结果 DataTable

**示例：**

```csharp
DataTable dt = new DataTable();
dt.Columns.Add("Department", typeof(string));
dt.Columns.Add("Amount", typeof(decimal));
dt.Rows.Add("研发部", 100m);
dt.Rows.Add("研发部", 200m);
dt.Rows.Add("市场部", 150m);

var result = dt.GroupBySum("Department", "Amount");
// 研发部: 300
// 市场部: 150
```

#### GroupByCount

DataTable 按指定列分组并计数。

```csharp
public static DataTable GroupByCount(this DataTable dt, string groupColumn)
```

**参数：**

- `dt`: 待分组的 DataTable
- `groupColumn`: 分组列名

**返回值：**

- 分组计数结果 DataTable

**示例：**

```csharp
DataTable dt = new DataTable();
dt.Columns.Add("Department", typeof(string));
dt.Columns.Add("Name", typeof(string));
dt.Rows.Add("研发部", "张三");
dt.Rows.Add("研发部", "李四");
dt.Rows.Add("市场部", "王五");

var result = dt.GroupByCount("Department");
// 研发部: 2
// 市场部: 1
```

### 8. 分页操作

提供数据分页功能。

#### GetPage

获取 DataTable 指定页的数据。

```csharp
public static DataTable GetPage(this DataTable dt, int pageIndex, int pageSize)
```

**参数：**

- `dt`: 待分页的 DataTable
- `pageIndex`: 页索引（从 1 开始）
- `pageSize`: 每页大小

**返回值：**

- 指定页的 DataTable

**示例：**

```csharp
DataTable dt = new DataTable();
dt.Columns.Add("Name");
for (int i = 0; i < 25; i++)
    dt.Rows.Add($"用户{i}");

var page1 = dt.GetPage(1, 10); // 前10条
var page2 = dt.GetPage(2, 10); // 第11-20条
var page3 = dt.GetPage(3, 10); // 第21-25条
```

#### GetPagedData

获取 DataTable 分页数据信息。

```csharp
public static PagedDataTable GetPagedData(this DataTable dt, int pageIndex, int pageSize)
```

**参数：**

- `dt`: 待分页的 DataTable
- `pageIndex`: 页索引（从 1 开始）
- `pageSize`: 每页大小

**返回值：**

- 包含分页信息和数据的对象

**示例：**

```csharp
DataTable dt = new DataTable();
dt.Columns.Add("Name");
for (int i = 0; i < 25; i++)
    dt.Rows.Add($"用户{i}");

var paged = dt.GetPagedData(1, 10);
// paged.PageIndex = 1
// paged.PageSize = 10
// paged.TotalCount = 25
// paged.TotalPages = 3
// paged.HasPreviousPage = false
// paged.HasNextPage = true
// paged.Data = 前10条数据
```

### 9. 实体转换

提供 DataTable 与实体之间的转换功能。

#### ToEntity<T>

将 DataTable 的第一行转换为实体。

```csharp
public static T ToEntity<T>(this DataTable dt) where T : new()
```

**参数：**

- `dt`: 待转换的 DataTable

**返回值：**

- 实体对象，如果无数据返回默认值

**示例：**

```csharp
DataTable dt = new DataTable();
dt.Columns.Add("Name", typeof(string));
dt.Columns.Add("Age", typeof(int));
dt.Rows.Add("张三", 25);

Person person = dt.ToEntity<Person>();
// person.Name = "张三", person.Age = 25
```

#### ToEntityList<T>

将 DataTable 转换为实体列表。

```csharp
public static List<T> ToEntityList<T>(this DataTable dt) where T : new()
```

**参数：**

- `dt`: 待转换的 DataTable

**返回值：**

- 实体列表

**示例：**

```csharp
DataTable dt = new DataTable();
dt.Columns.Add("Name", typeof(string));
dt.Columns.Add("Age", typeof(int));
dt.Rows.Add("张三", 25);
dt.Rows.Add("李四", 30);

List<Person> list = dt.ToEntityList<Person>();
```

#### FromEntityList<T>

从实体列表创建 DataTable。

```csharp
public static DataTable FromEntityList<T>(this IEnumerable<T> entities, string tableName = "Table")
```

**参数：**

- `entities`: 实体列表
- `tableName`: 表名

**返回值：**

- 创建的 DataTable

**示例：**

```csharp
var persons = new List<Person>
{
    new Person { Name = "张三", Age = 25 },
    new Person { Name = "李四", Age = 30 }
};

DataTable dt = persons.FromEntityList("Persons");
```

## PagedDataTable 类

分页 DataTable 结果类，包含分页信息和数据。

### 属性

| 属性              | 类型        | 说明            |
| --------------- | --------- | ------------- |
| PageIndex       | int       | 当前页索引（从 1 开始） |
| PageSize        | int       | 每页大小          |
| TotalCount      | int       | 总记录数          |
| TotalPages      | int       | 总页数           |
| Data            | DataTable | 当前页数据         |
| HasPreviousPage | bool      | 是否有上一页        |
| HasNextPage     | bool      | 是否有下一页        |

## 使用场景

1. **数据转换** - 将 DataTable 转换为对象集合、字典列表、数组等数据结构
2. **数据查询** - 对 DataTable 进行筛选、排序、分页
3. **数据导出** - 将 DataTable 数据导出为 CSV、JSON 等格式
4. **数据处理** - 对 DataTable 数据进行聚合统计、分组汇总
5. **Web API** - 将 DataTable 转换为 JSON 格式返回
6. **批量操作** - 批量添加行、列，批量更新数据
7. **数据迁移** - 在不同数据结构间转换数据
8. **单元测试** - 简化测试中对 DataTable 数据的验证和操作

## 注意事项

1. 所有方法都是扩展方法，需要通过 `DataTable` 实例调用
2. 大部分方法对 null 值进行了安全处理，不会抛出异常
3. `ToList<T>()` 自动映射方法要求实体属性名与 DataTable 列名一致
4. `Filter()` 和 `Sort()` 方法使用 DataTable 原生的 Select 语法，支持标准的表达式语法
5. `AsEnumerableDictionary()` 方法返回延迟执行的枚举，适合大数据量处理
6. `AddRow()` 方法参数顺序需要与列顺序匹配
7. `ToArray()` 方法返回 object 类型的二维数组，使用时可能需要类型转换
8. 克隆和复制方法基于 DataTable 原生的 Copy 和 Clone 方法实现

