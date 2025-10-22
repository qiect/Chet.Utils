# DataTableHelper 类功能文档

## 概述

[DataTableHelper](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\DataTableHelper.cs#L12-L1893) 是一个静态工具类，提供了丰富的 `DataTable` 数据处理、转换、分析和导出功能。该类旨在简化复杂的数据操作，包括数据转换映射、透视分组、统计分析、数据导出、数据验证清洗、数据连接合并以及高级查询筛选等。

## 主要功能模块

### 1. 数据转换与映射

提供多种数据格式之间的转换功能，支持将 `DataTable` 转换为动态对象、指定类型对象、字典列表等，也支持反向转换和列名映射。

**主要方法：**
- [ToDynamicList()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\DataTableHelper.cs#L24-L43) - 将 `DataTable` 转换为动态对象列表
- [ToObjectList<T>()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\DataTableHelper.cs#L52-L88) - 将 `DataTable` 转换为指定类型的对象列表
- [ToDataTable<T>()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\DataTableHelper.cs#L97-L125) - 将对象列表转换为 `DataTable`
- [ToDataTableFromJson()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\DataTableHelper.cs#L134-L183) - 将 JSON 字符串转换为 `DataTable`
- [ToDictionaryList()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\DataTableHelper.cs#L192-L210) - 将 `DataTable` 转换为字典列表
- [MapColumns()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\DataTableHelper.cs#L219-L234) - 动态映射 `DataTable` 列名

### 2. 数据透视与分组

提供数据透视和分组聚合功能，支持创建复杂的交叉表和汇总报表。

**主要方法：**
- [Pivot()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\DataTableHelper.cs#L246-L304) - 数据透视操作
- [GroupBy()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\DataTableHelper.cs#L314-L357) - 数据分组聚合
- [AdvancedPivot()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\DataTableHelper.cs#L367-L452) - 创建高级数据透视表

### 3. 数据分析与统计

提供数据统计分析功能，包括基本统计指标计算、分布分析和相关性分析。

**主要方法：**
- [CalculateStatistics()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\DataTableHelper.cs#L484-L508) - 计算数据统计信息
- [AnalyzeDistribution()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\DataTableHelper.cs#L585-L630) - 数据分布分析
- [CalculateCorrelation()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\DataTableHelper.cs#L690-L719) - 相关性分析

### 4. 数据导出与序列化

支持将 `DataTable` 导出为多种格式，包括 CSV、JSON、HTML 和 Excel XML。

**主要方法：**
- [ExportToCsv()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\DataTableHelper.cs#L731-L751) - 导出为 CSV 格式
- [ExportToJson()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\DataTableHelper.cs#L762-L794) - 导出为 JSON 格式
- [ExportToHtml()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\DataTableHelper.cs#L805-L835) - 导出为 HTML 表格
- [ExportToExcelXml()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\DataTableHelper.cs#L845-L882) - 导出为 Excel XML 格式

### 5. 数据验证与清洗

提供数据质量控制功能，包括数据验证、清洗、去重、缺失值填充和异常值检测。

**主要方法：**
- [ValidateData()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\DataTableHelper.cs#L905-L935) - 验证 `DataTable` 数据
- [CleanData()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\DataTableHelper.cs#L945-L962) - 清洗 `DataTable` 数据
- [RemoveDuplicates()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\DataTableHelper.cs#L971-L988) - 去除重复行
- [FillMissingValues()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\DataTableHelper.cs#L997-L1018) - 填充缺失值
- [DetectOutliers()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\DataTableHelper.cs#L1027-L1074) - 检测异常值

### 6. 数据连接与合并

支持多种数据表连接和合并操作，包括内连接、左连接和垂直/水平合并。

**主要方法：**
- [InnerJoin()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\DataTableHelper.cs#L1086-L1136) - 内连接两个 `DataTable`
- [LeftJoin()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\DataTableHelper.cs#L1147-L1213) - 左连接两个 `DataTable`
- [UnionAll()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\DataTableHelper.cs#L1223-L1237) - 合并多个 `DataTable`（垂直合并）
- [MergeHorizontally()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\DataTableHelper.cs#L1247-L1331) - 合并多个 `DataTable`（水平合并）

### 7. 高级查询与筛选

提供类似 SQL 的查询功能和高级筛选能力。

**主要方法：**
- [Query()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\DataTableHelper.cs#L1343-L1382) - 执行 SQL 风格查询
- [Filter()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\DataTableHelper.cs#L1459-L1471) - 执行复杂筛选
- [Search()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\DataTableHelper.cs#L1482-L1507) - 执行全文搜索
- [RegexFilter()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\DataTableHelper.cs#L1517-L1535) - 执行正则表达式筛选

## 数据结构

### 统计分析类
- [DataStatistics](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\DataTableHelper.cs#L1549-L1615) - 数据统计信息
- [DataDistribution](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\DataTableHelper.cs#L1620-L1637) - 数据分布信息
- [HistogramBin](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\DataTableHelper.cs#L1642-L1663) - 直方图箱

### 验证与异常处理类
- [ValidationResult](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\DataTableHelper.cs#L891-L896) - 数据验证结果

### 枚举类
- [OutlierDetectionMethod](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\DataTableHelper.cs#L1668-L1681) - 异常值检测方法（IQR、ZScore）

### 内部辅助类
- [ArrayEqualityComparer](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\DataTableHelper.cs#L1686-L1722) - 数组相等比较器
- [ObjectEqualityComparer](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\DataTableHelper.cs#L1727-L1747) - 对象相等比较器
- [ObjectComparer](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\DataTableHelper.cs#L1752-L1777) - 对象比较器
- [DataRowComparer](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\DataTableHelper.cs#L1782-L1816) - DataRow 比较器

## 使用场景

1. **数据转换** - 在不同数据格式之间进行转换
2. **报表生成** - 创建透视表和汇总报表
3. **数据分析** - 进行统计分析和数据挖掘
4. **数据导出** - 将数据导出为各种格式供其他系统使用
5. **数据清洗** - 提高数据质量，处理异常值和缺失值
6. **数据集成** - 合并来自不同数据源的数据
7. **数据查询** - 对内存中的数据表执行复杂查询

## 注意事项

1. 部分方法可能需要大量内存处理大数据集
2. 某些统计分析方法要求数据列为数值类型
3. 正则表达式筛选可能影响性能
4. JSON 解析依赖于 `System.Text.Json` 库
5. 高级查询功能的 WHERE 子句解析较为简单，复杂表达式可能不支持