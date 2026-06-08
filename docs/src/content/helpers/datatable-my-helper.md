---
title: "DataTableMyHelper"
description: "是 DataTableHelper 的补充帮助类，为 DataTable 提供了缺失值补充、JSON 数组转 DataTable、数据表映射等实用功能，旨在补充 DataTableHelper 中未包含的特定业务场景功能。"
namespace: "Chet.Utils.Helpers"
className: "DataTableMyHelper"
category: "Helpers"
order: 11
---

# DataTableMyHelper 帮助类

## 概述

[DataTableMyHelper](../../Chet.Utils/Helpers/DataTableMyHelper.cs) 是 DataTableHelper 的补充帮助类，为 DataTable 提供了缺失值补充、JSON 数组转 DataTable、数据表映射等实用功能，旨在补充 DataTableHelper 中未包含的特定业务场景功能。

## 类定义

```csharp
public static partial class DataTableHelper
```

**注意：** DataTableMyHelper 中的方法以 `partial class` 的形式定义在 `DataTableHelper` 中，通过 `DataTableHelper` 类名调用。

---

## 方法详解

### FillMissingValues

补充 DataTable 中每一列的缺失值（先向下查找补充，若后续都为空则延续上方最近的非空值）。

```csharp
public static void FillMissingValues(DataTable table)
```

**参数：**
- `table`: 需要处理的 DataTable

**描述：**

该方法遍历 DataTable 的每一列，对每一列中的缺失值（null、DBNull 或空字符串）进行智能填充：

1. 首先向下查找第一个非空值，如果找到则使用该值填充当前缺失值
2. 如果向下未找到非空值，但存在上方最近的非空值，则使用上方的值填充
3. 如果上下均无非空值，则保持为空

**示例：**

```csharp
using Chet.Utils.Helpers;

// 创建包含缺失值的 DataTable
var table = new DataTable();
table.Columns.Add("Name", typeof(string));
table.Columns.Add("Age", typeof(int));

table.Rows.Add("张三", 25);
table.Rows.Add(DBNull.Value, DBNull.Value);  // 缺失值
table.Rows.Add("李四", 30);
table.Rows.Add(DBNull.Value, DBNull.Value);  // 缺失值
table.Rows.Add(DBNull.Value, DBNull.Value);  // 缺失值

// 填充缺失值
DataTableHelper.FillMissingValues(table);

// 结果：
// 行1: Name="张三", Age=25
// 行2: Name="李四", Age=30  (向下找到"李四"和30)
// 行3: Name="李四", Age=30
// 行4: Name="李四", Age=30  (向下未找到，使用上方最近的值)
// 行5: Name="李四", Age=30
```

---

### JsonToDataTableChunked

将 JSON 数组转换为 DataTable。

```csharp
public static DataTable JsonToDataTableChunked(JArray jsonArray)
```

**参数：**
- `jsonArray`: Newtonsoft.Json.Linq.JArray 类型的 JSON 数组

**返回值：**
- 转换后的 DataTable，所有列的类型为 `object`

**描述：**

该方法将 JArray 格式的 JSON 数据转换为 DataTable：

1. 从第一个 JSON 对象中提取列信息
2. 所有列的数据类型设置为 `object`
3. 使用 `BeginLoadData` / `EndLoadData` 优化批量数据加载性能
4. 自动处理 JToken 到 .NET 类型的转换（整数→long、浮点数→double、布尔→bool、字符串→string、日期→DateTime、null→DBNull）

**类型转换规则：**

| JTokenType | 目标类型 |
|------------|---------|
| Integer | long |
| Float | double |
| Boolean | bool |
| String | string |
| Null | DBNull.Value |
| Date | DateTime |
| 其他 | string（ToString） |

**示例：**

```csharp
using Chet.Utils.Helpers;
using Newtonsoft.Json.Linq;

// 从 JSON 字符串创建 JArray
string json = @"[
    { ""Name"": ""张三"", ""Age"": 25, ""Score"": 89.5 },
    { ""Name"": ""李四"", ""Age"": 30, ""Score"": 95.0 },
    { ""Name"": ""王五"", ""Age"": 28, ""Score"": 76.3 }
]";

JArray jsonArray = JArray.Parse(json);

// 转换为 DataTable
DataTable table = DataTableHelper.JsonToDataTableChunked(jsonArray);

// 遍历结果
foreach (DataRow row in table.Rows)
{
    Console.WriteLine($"姓名: {row["Name"]}, 年龄: {row["Age"]}, 成绩: {row["Score"]}");
}
```

---

### MapToEntities<T>

将 DataTable 中的数据映射为指定类型的实体对象列表。映射关系通过字段映射字典指定，仅处理 DataTable 中存在且目标类型中也存在的属性。

```csharp
public static List<T> MapToEntities<T>(DataTable dataTable, Dictionary<string, string> fieldMapping) where T : new()
```

**参数：**
- `dataTable`: 源数据表，包含待转换的行数据
- `fieldMapping`: 字段映射字典，键为 DataTable 列名，值为目标类型的属性名

**返回值：**
- 转换后的实体对象列表

**类型约束：**
- `T`: 目标实体类型，必须具有无参构造函数

**描述：**

该方法通过字段映射字典将 DataTable 的数据转换为强类型的实体对象列表：

1. 验证 DataTable 和映射字典的有效性
2. 过滤出有效的映射关系：源列必须存在于 DataTable 中，且目标属性必须存在于类型 T 中
3. 如果没有有效的映射关系，返回空列表并输出警告
4. 遍历每一行数据，创建实体对象并赋值
5. 自动处理类型转换，包括可空类型、DateTime、bool 等常见类型
6. 转换失败时使用目标类型的默认值，不会抛出异常

**类型转换支持：**

- `string`: 使用 ToString() 转换
- `DateTime`: 使用 Convert.ToDateTime() 转换
- `bool`: 使用 Convert.ToBoolean() 转换
- 可空类型：自动提取基础类型进行转换
- 其他类型：使用 Convert.ChangeType() 通用转换

**示例：**

```csharp
using Chet.Utils.Helpers;

// 定义实体类
public class UserDto
{
    public string UserName { get; set; }
    public int UserAge { get; set; }
    public string Email { get; set; }
}

// 创建 DataTable
var table = new DataTable();
table.Columns.Add("Name", typeof(string));
table.Columns.Add("Age", typeof(int));
table.Columns.Add("Email", typeof(string));

table.Rows.Add("张三", 25, "zhangsan@example.com");
table.Rows.Add("李四", 30, "lisi@example.com");
table.Rows.Add("王五", 28, "wangwu@example.com");

// 定义字段映射（DataTable 列名 -> 实体属性名）
var mapping = new Dictionary<string, string>
{
    { "Name", "UserName" },
    { "Age", "UserAge" },
    { "Email", "Email" }
};

// 映射为实体列表
var users = DataTableHelper.MapToEntities<UserDto>(table, mapping);

foreach (var user in users)
{
    Console.WriteLine($"用户名: {user.UserName}, 年龄: {user.UserAge}, 邮箱: {user.Email}");
}
```

---

## 使用场景

1. **数据清洗** - 使用 `FillMissingValues` 补充 Excel 导入数据中的空值
2. **JSON 数据转换** - 使用 `JsonToDataTableChunked` 将 API 返回的 JSON 数据转换为 DataTable 进行后续处理
3. **数据映射** - 使用 `MapToEntities<T>` 将 DataTable 数据转换为 DTO 对象，用于业务逻辑处理
4. **报表处理** - 处理报表中的合并单元格导致的缺失值问题

## 注意事项

1. `FillMissingValues` 方法会直接修改传入的 DataTable，不会创建新的副本
2. `JsonToDataTableChunked` 方法需要 Newtonsoft.Json 包的支持
3. `MapToEntities<T>` 方法在处理单行出错时会记录日志但继续处理其他行
4. `MapToEntities<T>` 中的类型转换失败时返回目标类型的默认值，不会抛出异常
5. 这些方法作为 `DataTableHelper` 的 partial class 实现，通过 `DataTableHelper` 类名调用
