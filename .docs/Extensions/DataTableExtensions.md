# DataTableExtensions 类功能文档

## 概述

[DataTableExtensions](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\DataTableExtensions.cs#L8-L168) 是一个静态扩展类，为 `DataTable` 类型提供了丰富的扩展方法。该类包含数据转换、查询筛选、结构操作、数据处理等多种功能，旨在简化 DataTable 的常用操作，提高数据处理的效率和代码可读性。

## 主要功能模块

### 1. 状态判断方法

提供 DataTable 状态检查的便捷方法。

**主要方法：**
- [IsNullOrEmpty()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\DataTableExtensions.cs#L13-L15) - 判断 DataTable 是否为 null 或无行数据

### 2. 数据转换方法

提供 DataTable 到其他数据结构的转换功能。

**主要方法：**
- [ToList<T>()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\DataTableExtensions.cs#L23-L27) - 将 DataTable 转换为泛型集合
- [ToDictionaryList()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\DataTableExtensions.cs#L51-L66) - 获取 DataTable 的所有行数据（每行为字典）
- [AsEnumerableDictionary()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\DataTableExtensions.cs#L107-L120) - DataTable 转为一行一字典的枚举（便于遍历）
- [ToArray()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\DataTableExtensions.cs#L159-L167) - DataTable 转为二维数组

### 3. 元数据获取方法

提供获取 DataTable 结构信息的功能。

**主要方法：**
- [GetColumnNames()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\DataTableExtensions.cs#L35-L39) - 获取 DataTable 的所有列名

### 4. 数据筛选与排序方法

提供数据查询和排序功能。

**主要方法：**
- [Filter()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\DataTableExtensions.cs#L74-L83) - DataTable 按条件筛选，返回新 DataTable
- [Sort()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\DataTableExtensions.cs#L91-L100) - DataTable 按指定列排序，返回新 DataTable

### 5. 结构操作方法

提供 DataTable 结构克隆和复制功能。

**主要方法：**
- [CopyAll()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\DataTableExtensions.cs#L127-L130) - DataTable 克隆结构并复制所有数据
- [CloneStructure()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\DataTableExtensions.cs#L137-L140) - DataTable 克隆结构但不复制数据

### 6. 数据操作方法

提供行级数据操作功能。

**主要方法：**
- [AddRow()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\DataTableExtensions.cs#L147-L153) - DataTable 添加一行数据
- [ClearRows()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\DataTableExtensions.cs#L159-L161) - DataTable 删除所有行

## 使用场景

1. **数据转换** - 将 DataTable 转换为对象集合、字典列表或其他数据结构
2. **数据查询** - 对 DataTable 进行筛选和排序操作
3. **数据导出** - 将 DataTable 数据导出为数组或其他格式
4. **数据处理** - 遍历 DataTable 行数据进行批量处理
5. **Web API** - 将 DataTable 转换为 JSON 格式返回
6. **报表生成** - 处理报表数据源的 DataTable 对象
7. **数据迁移** - 在不同数据结构间转换数据
8. **单元测试** - 简化测试中对 DataTable 数据的验证和操作

## 注意事项

1. 所有方法都是扩展方法，需要通过 `DataTable` 实例调用
2. 大部分方法都对 null 值进行了安全处理，避免抛出异常
3. [ToList<T>()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\DataTableExtensions.cs#L23-L27) 方法需要提供行转换委托来指定转换逻辑
4. [Filter()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\DataTableExtensions.cs#L74-L83) 和 [Sort()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\DataTableExtensions.cs#L91-L100) 方法使用 DataTable 内置的 Select 方法，支持标准的表达式语法
5. [AsEnumerableDictionary()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\DataTableExtensions.cs#L107-L120) 方法返回延迟执行的枚举器，适合大数据量处理
6. [AddRow()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\DataTableExtensions.cs#L147-L153) 方法按列顺序添加值，需要注意参数顺序与列顺序匹配
7. [ToArray()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\DataTableExtensions.cs#L159-L167) 方法返回 object 类型的二维数组，使用时可能需要类型转换
8. 克隆和复制方法基于 DataTable 内置的 Copy 和 Clone 方法实现