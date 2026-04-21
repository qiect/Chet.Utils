using System.Data;
using System.Text.Json;
using Xunit;

namespace Chet.Utils.Helpers.Tests
{
    public class DataTableHelperTests
    {
        #region 辅助方法

        private DataTable CreateTestDataTable()
        {
            var dt = new DataTable("TestTable");
            dt.Columns.Add("Id", typeof(int));
            dt.Columns.Add("Name", typeof(string));
            dt.Columns.Add("Age", typeof(int));
            dt.Columns.Add("Salary", typeof(decimal));

            dt.Rows.Add(1, "张三", 25, 5000m);
            dt.Rows.Add(2, "李四", 30, 6000m);
            dt.Rows.Add(3, "王五", 28, 5500m);
            dt.Rows.Add(4, "赵六", 35, 7000m);
            dt.Rows.Add(5, "钱七", 25, 5000m);

            return dt;
        }

        private DataTable CreateSalesDataTable()
        {
            var dt = new DataTable("Sales");
            dt.Columns.Add("Year", typeof(int));
            dt.Columns.Add("Product", typeof(string));
            dt.Columns.Add("Region", typeof(string));
            dt.Columns.Add("Amount", typeof(decimal));

            dt.Rows.Add(2023, "产品A", "华东", 10000m);
            dt.Rows.Add(2023, "产品A", "华北", 8000m);
            dt.Rows.Add(2023, "产品B", "华东", 15000m);
            dt.Rows.Add(2023, "产品B", "华北", 12000m);
            dt.Rows.Add(2024, "产品A", "华东", 12000m);
            dt.Rows.Add(2024, "产品A", "华北", 9000m);
            dt.Rows.Add(2024, "产品B", "华东", 18000m);
            dt.Rows.Add(2024, "产品B", "华北", 14000m);

            return dt;
        }

        private DataTable CreateDataTableWithNulls()
        {
            var dt = new DataTable("NullTable");
            dt.Columns.Add("Id", typeof(int));
            dt.Columns.Add("Name", typeof(string));
            dt.Columns.Add("Age", typeof(int));
            dt.Columns.Add("City", typeof(string));

            dt.Rows.Add(1, "张三", 25, "北京");
            dt.Rows.Add(2, DBNull.Value, 30, "上海");
            dt.Rows.Add(3, "王五", DBNull.Value, DBNull.Value);
            dt.Rows.Add(4, "赵六", 35, "广州");

            return dt;
        }

        #endregion

        #region 数据转换测试

        [Fact]
        public void ToDynamicList_ValidDataTable_ReturnsCorrectList()
        {
            var dt = CreateTestDataTable();

            var result = DataTableHelper.ToDynamicList(dt);

            Assert.Equal(5, result.Count);
            Assert.Equal(1, result[0].Id);
            Assert.Equal("张三", result[0].Name);
            Assert.Equal(25, result[0].Age);
            Assert.Equal(5000m, result[0].Salary);
        }

        [Fact]
        public void ToDynamicList_NullDataTable_ThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => DataTableHelper.ToDynamicList(null!));
        }

        [Fact]
        public void ToDynamicList_EmptyDataTable_ReturnsEmptyList()
        {
            var dt = new DataTable();

            var result = DataTableHelper.ToDynamicList(dt);

            Assert.Empty(result);
        }

        [Fact]
        public void ToDynamicList_WithNullValues_ConvertsNullCorrectly()
        {
            var dt = CreateDataTableWithNulls();

            var result = DataTableHelper.ToDynamicList(dt);

            Assert.Equal(4, result.Count);
            Assert.Null(result[1].Name);
            Assert.Null(result[2].Age);
            Assert.Null(result[2].City);
        }

        [Fact]
        public void ToDataTableFromJson_ValidJson_ReturnsCorrectDataTable()
        {
            var json = "[{\"Id\":1,\"Name\":\"张三\"},{\"Id\":2,\"Name\":\"李四\"}]";

            var result = DataTableHelper.ToDataTableFromJson(json);

            Assert.Equal(2, result.Rows.Count);
            Assert.Equal(2, result.Columns.Count);
            Assert.Equal("1", result.Rows[0]["Id"]);
            Assert.Equal("张三", result.Rows[0]["Name"]);
        }

        [Fact]
        public void ToDataTableFromJson_EmptyJson_ReturnsEmptyDataTable()
        {
            var json = "[]";

            var result = DataTableHelper.ToDataTableFromJson(json);

            Assert.Equal(0, result.Rows.Count);
        }

        [Fact]
        public void ToDataTableFromJson_NullJson_ThrowsArgumentException()
        {
            Assert.Throws<ArgumentException>(() => DataTableHelper.ToDataTableFromJson(null!));
        }

        [Fact]
        public void ToDataTableFromJson_EmptyJsonString_ThrowsArgumentException()
        {
            Assert.Throws<ArgumentException>(() => DataTableHelper.ToDataTableFromJson(""));
        }

        [Fact]
        public void ToDataTableFromJson_InvalidJson_ThrowsInvalidOperationException()
        {
            Assert.Throws<InvalidOperationException>(() => DataTableHelper.ToDataTableFromJson("invalid json"));
        }

        [Fact]
        public void MapColumns_ValidMappings_ReturnsMappedDataTable()
        {
            var dt = CreateTestDataTable();
            var mappings = new Dictionary<string, string>
            {
                { "Id", "编号" },
                { "Name", "姓名" }
            };

            var result = DataTableHelper.MapColumns(dt, mappings);

            Assert.True(result.Columns.Contains("编号"));
            Assert.True(result.Columns.Contains("姓名"));
            Assert.True(result.Columns.Contains("Age"));
            Assert.False(result.Columns.Contains("Id"));
            Assert.False(result.Columns.Contains("Name"));
        }

        [Fact]
        public void MapColumns_NullDataTable_ThrowsArgumentNullException()
        {
            var mappings = new Dictionary<string, string> { { "Id", "编号" } };

            Assert.Throws<ArgumentNullException>(() => DataTableHelper.MapColumns(null!, mappings));
        }

        [Fact]
        public void MapColumns_NullMappings_ThrowsArgumentNullException()
        {
            var dt = CreateTestDataTable();

            Assert.Throws<ArgumentNullException>(() => DataTableHelper.MapColumns(dt, null!));
        }

        [Fact]
        public void MapColumns_NonExistentColumn_IgnoresMapping()
        {
            var dt = CreateTestDataTable();
            var mappings = new Dictionary<string, string>
            {
                { "NonExistent", "新列名" }
            };

            var result = DataTableHelper.MapColumns(dt, mappings);

            Assert.Equal(dt.Columns.Count, result.Columns.Count);
        }

        [Fact]
        public void ToDataTableFromObjects_ValidList_ReturnsCorrectDataTable()
        {
            var users = new List<TestUser>
            {
                new TestUser { Id = 1, Name = "张三", Age = 25 },
                new TestUser { Id = 2, Name = "李四", Age = 30 }
            };

            var result = DataTableHelper.ToDataTableFromObjects(users);

            Assert.Equal(2, result.Rows.Count);
            Assert.Equal(3, result.Columns.Count);
            Assert.Equal(1, result.Rows[0]["Id"]);
            Assert.Equal("张三", result.Rows[0]["Name"]);
        }

        [Fact]
        public void ToDataTableFromObjects_EmptyList_ReturnsEmptyDataTable()
        {
            var users = new List<TestUser>();

            var result = DataTableHelper.ToDataTableFromObjects(users);

            Assert.Equal(0, result.Rows.Count);
            Assert.Equal(3, result.Columns.Count);
        }

        [Fact]
        public void ToDataTableFromObjects_NullList_ThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => DataTableHelper.ToDataTableFromObjects<TestUser>(null!));
        }

        #endregion

        #region 数据透视测试

        [Fact]
        public void Pivot_ValidData_ReturnsPivotTable()
        {
            var dt = CreateSalesDataTable();

            var result = DataTableHelper.Pivot(dt, "Year", "Product", "Amount");

            Assert.True(result.Columns.Contains("Year"));
            Assert.True(result.Columns.Contains("产品A"));
            Assert.True(result.Columns.Contains("产品B"));
            Assert.Equal(2, result.Rows.Count);
        }

        [Fact]
        public void Pivot_NullDataTable_ThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => 
                DataTableHelper.Pivot(null!, "Year", "Product", "Amount"));
        }

        [Fact]
        public void Pivot_NonExistentRowField_ThrowsArgumentException()
        {
            var dt = CreateSalesDataTable();

            Assert.Throws<ArgumentException>(() => 
                DataTableHelper.Pivot(dt, "NonExistent", "Product", "Amount"));
        }

        [Fact]
        public void Pivot_NonExistentColumnField_ThrowsArgumentException()
        {
            var dt = CreateSalesDataTable();

            Assert.Throws<ArgumentException>(() => 
                DataTableHelper.Pivot(dt, "Year", "NonExistent", "Amount"));
        }

        [Fact]
        public void Pivot_NonExistentDataField_ThrowsArgumentException()
        {
            var dt = CreateSalesDataTable();

            Assert.Throws<ArgumentException>(() => 
                DataTableHelper.Pivot(dt, "Year", "Product", "NonExistent"));
        }

        [Fact]
        public void Pivot_WithCustomAggregate_UsesCustomAggregate()
        {
            var dt = CreateSalesDataTable();

            var result = DataTableHelper.Pivot(dt, "Year", "Product", "Amount",
                values => values.Count());

            Assert.True(result.Rows.Count > 0);
        }

        [Fact]
        public void AdvancedPivot_ValidData_ReturnsAdvancedPivotTable()
        {
            var dt = CreateSalesDataTable();
            var dataFields = new Dictionary<string, Func<IEnumerable<object>, object>>
            {
                { "Amount", values => values.Sum(v => Convert.ToDecimal(v)) }
            };

            var result = DataTableHelper.AdvancedPivot(
                dt,
                new[] { "Year" },
                new[] { "Product" },
                dataFields);

            Assert.True(result.Columns.Contains("Year"));
            Assert.Equal(2, result.Rows.Count);
        }

        [Fact]
        public void AdvancedPivot_MultipleRowFields_ReturnsCorrectStructure()
        {
            var dt = CreateSalesDataTable();
            var dataFields = new Dictionary<string, Func<IEnumerable<object>, object>>
            {
                { "Amount", values => values.Sum(v => Convert.ToDecimal(v)) }
            };

            var result = DataTableHelper.AdvancedPivot(
                dt,
                new[] { "Year", "Product" },
                new[] { "Region" },
                dataFields);

            Assert.True(result.Columns.Contains("Year"));
            Assert.True(result.Columns.Contains("Product"));
        }

        [Fact]
        public void GroupByAdvanced_ValidData_ReturnsGroupedTable()
        {
            var dt = CreateSalesDataTable();
            var aggregates = new Dictionary<string, Func<IEnumerable<object>, object>>
            {
                { "Amount", values => values.Sum(v => Convert.ToDecimal(v)) }
            };

            var result = DataTableHelper.GroupByAdvanced(dt, new[] { "Year" }, aggregates);

            Assert.Equal(2, result.Rows.Count);
            Assert.True(result.Columns.Contains("Year"));
            Assert.True(result.Columns.Contains("Amount"));
        }

        [Fact]
        public void GroupByAdvanced_MultipleGroupColumns_ReturnsCorrectGroups()
        {
            var dt = CreateSalesDataTable();
            var aggregates = new Dictionary<string, Func<IEnumerable<object>, object>>
            {
                { "Amount", values => values.Sum(v => Convert.ToDecimal(v)) }
            };

            var result = DataTableHelper.GroupByAdvanced(dt, new[] { "Year", "Product" }, aggregates);

            Assert.Equal(4, result.Rows.Count);
        }

        #endregion

        #region 数据分析测试

        [Fact]
        public void CalculateStatistics_ValidColumn_ReturnsCorrectStatistics()
        {
            var dt = CreateTestDataTable();

            var result = DataTableHelper.CalculateStatistics(dt, "Age");

            Assert.Equal(5, result.Count);
            Assert.Equal(28.6, result.Average, 1);
            Assert.Equal(25, result.Min);
            Assert.Equal(35, result.Max);
            Assert.True(result.StandardDeviation > 0);
        }

        [Fact]
        public void CalculateStatistics_NullDataTable_ThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => 
                DataTableHelper.CalculateStatistics(null!, "Age"));
        }

        [Fact]
        public void CalculateStatistics_NonExistentColumn_ThrowsArgumentException()
        {
            var dt = CreateTestDataTable();

            Assert.Throws<ArgumentException>(() => 
                DataTableHelper.CalculateStatistics(dt, "NonExistent"));
        }

        [Fact]
        public void CalculateStatistics_EmptyColumn_ReturnsEmptyStatistics()
        {
            var dt = new DataTable();
            dt.Columns.Add("Value", typeof(int));

            var result = DataTableHelper.CalculateStatistics(dt, "Value");

            Assert.Equal(0, result.Count);
        }

        [Fact]
        public void AnalyzeDistribution_ValidColumn_ReturnsCorrectDistribution()
        {
            var dt = CreateTestDataTable();

            var result = DataTableHelper.AnalyzeDistribution(dt, "Age", 3);

            Assert.Equal(3, result.Bins.Count);
            Assert.True(result.Bins.Sum(b => b.Count) == 5);
        }

        [Fact]
        public void AnalyzeDistribution_NullDataTable_ThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => 
                DataTableHelper.AnalyzeDistribution(null!, "Age"));
        }

        [Fact]
        public void AnalyzeDistribution_NonExistentColumn_ThrowsArgumentException()
        {
            var dt = CreateTestDataTable();

            Assert.Throws<ArgumentException>(() => 
                DataTableHelper.AnalyzeDistribution(dt, "NonExistent"));
        }

        [Fact]
        public void CalculateCorrelation_ValidColumns_ReturnsCorrectCorrelation()
        {
            var dt = new DataTable();
            dt.Columns.Add("X", typeof(int));
            dt.Columns.Add("Y", typeof(int));

            for (int i = 1; i <= 10; i++)
            {
                dt.Rows.Add(i, i * 2);
            }

            var result = DataTableHelper.CalculateCorrelation(dt, "X", "Y");

            Assert.True(result > 0.99);
        }

        [Fact]
        public void CalculateCorrelation_UnrelatedColumns_ReturnsLowCorrelation()
        {
            var dt = new DataTable();
            dt.Columns.Add("X", typeof(int));
            dt.Columns.Add("Y", typeof(int));

            dt.Rows.Add(1, 10);
            dt.Rows.Add(2, 5);
            dt.Rows.Add(3, 8);
            dt.Rows.Add(4, 3);
            dt.Rows.Add(5, 7);

            var result = DataTableHelper.CalculateCorrelation(dt, "X", "Y");

            Assert.True(Math.Abs(result) < 0.8);
        }

        [Fact]
        public void CalculateCorrelation_NullDataTable_ThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => 
                DataTableHelper.CalculateCorrelation(null!, "X", "Y"));
        }

        [Fact]
        public void CalculateCorrelation_NonExistentColumn_ThrowsArgumentException()
        {
            var dt = CreateTestDataTable();

            Assert.Throws<ArgumentException>(() => 
                DataTableHelper.CalculateCorrelation(dt, "NonExistent", "Age"));
        }

        #endregion

        #region 数据导出测试

        [Fact]
        public void ExportToHtml_ValidDataTable_ReturnsValidHtml()
        {
            var dt = CreateTestDataTable();

            var result = DataTableHelper.ExportToHtml(dt);

            Assert.Contains("<table", result);
            Assert.Contains("</table>", result);
            Assert.Contains("<thead>", result);
            Assert.Contains("<tbody>", result);
            Assert.Contains("张三", result);
        }

        [Fact]
        public void ExportToHtml_WithIndex_IncludesIndexColumn()
        {
            var dt = CreateTestDataTable();

            var result = DataTableHelper.ExportToHtml(dt, includeIndex: true);

            Assert.Contains("<th>#</th>", result);
            Assert.Contains("<td>1</td>", result);
        }

        [Fact]
        public void ExportToHtml_WithCustomClass_AppliesClass()
        {
            var dt = CreateTestDataTable();

            var result = DataTableHelper.ExportToHtml(dt, "custom-table");

            Assert.Contains("class=\"custom-table\"", result);
        }

        [Fact]
        public void ExportToHtml_NullDataTable_ThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => DataTableHelper.ExportToHtml(null!));
        }

        [Fact]
        public void ExportToExcelXml_ValidDataTable_ReturnsValidXml()
        {
            var dt = CreateTestDataTable();

            var result = DataTableHelper.ExportToExcelXml(dt);

            Assert.Contains("<?xml", result);
            Assert.Contains("<Workbook", result);
            Assert.Contains("<Worksheet", result);
            Assert.Contains("<Table>", result);
        }

        [Fact]
        public void ExportToExcelXml_WithSheetName_UsesSheetName()
        {
            var dt = CreateTestDataTable();

            var result = DataTableHelper.ExportToExcelXml(dt, "销售数据");

            Assert.Contains("销售数据", result);
        }

        [Fact]
        public void ExportToExcelXml_NullDataTable_ThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => DataTableHelper.ExportToExcelXml(null!));
        }

        [Fact]
        public void ExportToMarkdown_ValidDataTable_ReturnsValidMarkdown()
        {
            var dt = CreateTestDataTable();

            var result = DataTableHelper.ExportToMarkdown(dt);

            Assert.Contains("| Id | Name | Age | Salary |", result);
            Assert.Contains("| --- | --- | --- | --- |", result);
            Assert.Contains("| 张三 |", result);
        }

        [Fact]
        public void ExportToMarkdown_WithIndex_IncludesIndexColumn()
        {
            var dt = CreateTestDataTable();

            var result = DataTableHelper.ExportToMarkdown(dt, includeIndex: true);

            Assert.Contains("| # |", result);
        }

        [Fact]
        public void ExportToMarkdown_NullDataTable_ThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => DataTableHelper.ExportToMarkdown(null!));
        }

        #endregion

        #region 数据清洗测试

        [Fact]
        public void ValidateData_ValidRules_ReturnsValidationResults()
        {
            var dt = CreateTestDataTable();
            var rules = new Dictionary<string, Func<object, bool>>
            {
                { "Age", value => value != null && Convert.ToInt32(value) >= 18 },
                { "Name", value => !string.IsNullOrEmpty(value?.ToString()) }
            };

            var result = DataTableHelper.ValidateData(dt, rules);

            Assert.All(result.Where(r => r.IsValid), r => Assert.True(r.IsValid));
        }

        [Fact]
        public void ValidateData_InvalidData_ReturnsFailedResults()
        {
            var dt = new DataTable();
            dt.Columns.Add("Age", typeof(int));
            dt.Rows.Add(15);
            dt.Rows.Add(25);

            var rules = new Dictionary<string, Func<object, bool>>
            {
                { "Age", value => Convert.ToInt32(value) >= 18 }
            };

            var result = DataTableHelper.ValidateData(dt, rules);

            Assert.Single(result.Where(r => !r.IsValid));
        }

        [Fact]
        public void ValidateData_NullDataTable_ThrowsArgumentNullException()
        {
            var rules = new Dictionary<string, Func<object, bool>>();

            Assert.Throws<ArgumentNullException>(() => DataTableHelper.ValidateData(null!, rules));
        }

        [Fact]
        public void CleanData_ValidRules_ReturnsCleanedDataTable()
        {
            var dt = new DataTable();
            dt.Columns.Add("Name", typeof(string));
            dt.Rows.Add("  张三  ");
            dt.Rows.Add("李四");

            var rules = new Dictionary<string, Func<object, object>>
            {
                { "Name", value => value?.ToString().Trim() }
            };

            var result = DataTableHelper.CleanData(dt, rules);

            Assert.Equal("张三", result.Rows[0]["Name"]);
        }

        [Fact]
        public void CleanData_NullDataTable_ThrowsArgumentNullException()
        {
            var rules = new Dictionary<string, Func<object, object>>();

            Assert.Throws<ArgumentNullException>(() => DataTableHelper.CleanData(null!, rules));
        }

        [Fact]
        public void FillMissingValues_ValidRules_FillsMissingValues()
        {
            var dt = CreateDataTableWithNulls();
            var rules = new Dictionary<string, Func<DataTable, int, object>>
            {
                { "City", (table, idx) => "未知" }
            };

            var result = DataTableHelper.FillMissingValues(dt, rules);

            Assert.Equal("未知", result.Rows[2]["City"]);
        }

        [Fact]
        public void FillMissingValues_NullDataTable_ThrowsArgumentNullException()
        {
            var rules = new Dictionary<string, Func<DataTable, int, object>>();

            Assert.Throws<ArgumentNullException>(() => DataTableHelper.FillMissingValues(null!, rules));
        }

        [Fact]
        public void DetectOutliers_IQRMethod_DetectsOutliers()
        {
            var dt = new DataTable();
            dt.Columns.Add("Value", typeof(int));
            dt.Rows.Add(10);
            dt.Rows.Add(12);
            dt.Rows.Add(11);
            dt.Rows.Add(13);
            dt.Rows.Add(100);

            var result = DataTableHelper.DetectOutliers(dt, "Value", OutlierDetectionMethod.IQR);

            Assert.Contains(4, result);
        }

        [Fact]
        public void DetectOutliers_ZScoreMethod_DetectsOutliers()
        {
            var dt = new DataTable();
            dt.Columns.Add("Value", typeof(int));
            dt.Rows.Add(10);
            dt.Rows.Add(11);
            dt.Rows.Add(12);
            dt.Rows.Add(11);
            dt.Rows.Add(10);
            dt.Rows.Add(200);

            var result = DataTableHelper.DetectOutliers(dt, "Value", OutlierDetectionMethod.ZScore, 2.0);

            Assert.Contains(5, result);
        }

        [Fact]
        public void DetectOutliers_NullDataTable_ThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => 
                DataTableHelper.DetectOutliers(null!, "Value"));
        }

        [Fact]
        public void DetectOutliers_NonExistentColumn_ThrowsArgumentException()
        {
            var dt = CreateTestDataTable();

            Assert.Throws<ArgumentException>(() => 
                DataTableHelper.DetectOutliers(dt, "NonExistent"));
        }

        [Fact]
        public void RemoveDuplicates_AllColumns_RemovesDuplicateRows()
        {
            var dt = new DataTable();
            dt.Columns.Add("Id", typeof(int));
            dt.Columns.Add("Name", typeof(string));
            dt.Rows.Add(1, "张三");
            dt.Rows.Add(1, "张三");
            dt.Rows.Add(2, "李四");

            var result = DataTableHelper.RemoveDuplicates(dt);

            Assert.Equal(2, result.Rows.Count);
        }

        [Fact]
        public void RemoveDuplicates_SpecificColumns_RemovesDuplicatesByKeyColumns()
        {
            var dt = new DataTable();
            dt.Columns.Add("Id", typeof(int));
            dt.Columns.Add("Name", typeof(string));
            dt.Rows.Add(1, "张三");
            dt.Rows.Add(1, "李四");
            dt.Rows.Add(2, "王五");

            var result = DataTableHelper.RemoveDuplicates(dt, new[] { "Id" });

            Assert.Equal(2, result.Rows.Count);
        }

        [Fact]
        public void RemoveDuplicates_NullDataTable_ThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => DataTableHelper.RemoveDuplicates(null!));
        }

        #endregion

        #region 数据连接测试

        [Fact]
        public void InnerJoin_ValidTables_ReturnsJoinedTable()
        {
            var leftTable = new DataTable();
            leftTable.Columns.Add("Id", typeof(int));
            leftTable.Columns.Add("Name", typeof(string));
            leftTable.Rows.Add(1, "张三");
            leftTable.Rows.Add(2, "李四");

            var rightTable = new DataTable();
            rightTable.Columns.Add("UserId", typeof(int));
            rightTable.Columns.Add("OrderNo", typeof(string));
            rightTable.Rows.Add(1, "ORD001");
            rightTable.Rows.Add(1, "ORD002");
            rightTable.Rows.Add(3, "ORD003");

            var result = DataTableHelper.InnerJoin(leftTable, rightTable, "Id", "UserId");

            Assert.Equal(2, result.Rows.Count);
        }

        [Fact]
        public void InnerJoin_NoMatchingRows_ReturnsEmptyTable()
        {
            var leftTable = new DataTable();
            leftTable.Columns.Add("Id", typeof(int));
            leftTable.Rows.Add(1);

            var rightTable = new DataTable();
            rightTable.Columns.Add("UserId", typeof(int));
            rightTable.Rows.Add(2);

            var result = DataTableHelper.InnerJoin(leftTable, rightTable, "Id", "UserId");

            Assert.Equal(0, result.Rows.Count);
        }

        [Fact]
        public void InnerJoin_NullLeftTable_ThrowsArgumentNullException()
        {
            var rightTable = new DataTable();

            Assert.Throws<ArgumentNullException>(() => 
                DataTableHelper.InnerJoin(null!, rightTable, "Id", "UserId"));
        }

        [Fact]
        public void LeftJoin_ValidTables_ReturnsLeftJoinedTable()
        {
            var leftTable = new DataTable();
            leftTable.Columns.Add("Id", typeof(int));
            leftTable.Columns.Add("Name", typeof(string));
            leftTable.Rows.Add(1, "张三");
            leftTable.Rows.Add(2, "李四");
            leftTable.Rows.Add(3, "王五");

            var rightTable = new DataTable();
            rightTable.Columns.Add("UserId", typeof(int));
            rightTable.Columns.Add("OrderNo", typeof(string));
            rightTable.Rows.Add(1, "ORD001");
            rightTable.Rows.Add(2, "ORD002");

            var result = DataTableHelper.LeftJoin(leftTable, rightTable, "Id", "UserId");

            Assert.Equal(3, result.Rows.Count);
        }

        [Fact]
        public void LeftJoin_NoMatchingRows_KeepsAllLeftRows()
        {
            var leftTable = new DataTable();
            leftTable.Columns.Add("Id", typeof(int));
            leftTable.Rows.Add(1);

            var rightTable = new DataTable();
            rightTable.Columns.Add("UserId", typeof(int));
            rightTable.Rows.Add(2);

            var result = DataTableHelper.LeftJoin(leftTable, rightTable, "Id", "UserId");

            Assert.Equal(1, result.Rows.Count);
        }

        [Fact]
        public void FullJoin_ValidTables_ReturnsFullJoinedTable()
        {
            var leftTable = new DataTable();
            leftTable.Columns.Add("Id", typeof(int));
            leftTable.Rows.Add(1);
            leftTable.Rows.Add(2);

            var rightTable = new DataTable();
            rightTable.Columns.Add("UserId", typeof(int));
            rightTable.Rows.Add(2);
            rightTable.Rows.Add(3);

            var result = DataTableHelper.FullJoin(leftTable, rightTable, "Id", "UserId");

            Assert.Equal(3, result.Rows.Count);
        }

        [Fact]
        public void CrossJoin_ValidTables_ReturnsCartesianProduct()
        {
            var leftTable = new DataTable();
            leftTable.Columns.Add("Color", typeof(string));
            leftTable.Rows.Add("Red");
            leftTable.Rows.Add("Blue");

            var rightTable = new DataTable();
            rightTable.Columns.Add("Size", typeof(string));
            rightTable.Rows.Add("S");
            rightTable.Rows.Add("M");

            var result = DataTableHelper.CrossJoin(leftTable, rightTable);

            Assert.Equal(4, result.Rows.Count);
        }

        [Fact]
        public void CrossJoin_NullTable_ThrowsArgumentNullException()
        {
            var table = new DataTable();

            Assert.Throws<ArgumentNullException>(() => DataTableHelper.CrossJoin(null!, table));
            Assert.Throws<ArgumentNullException>(() => DataTableHelper.CrossJoin(table, null!));
        }

        [Fact]
        public void UnionAll_ValidTables_ReturnsCombinedTable()
        {
            var table1 = new DataTable();
            table1.Columns.Add("Id", typeof(int));
            table1.Rows.Add(1);
            table1.Rows.Add(2);

            var table2 = new DataTable();
            table2.Columns.Add("Id", typeof(int));
            table2.Rows.Add(3);
            table2.Rows.Add(4);

            var result = DataTableHelper.UnionAll(table1, table2);

            Assert.Equal(4, result.Rows.Count);
        }

        [Fact]
        public void UnionAll_NullTables_ThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => DataTableHelper.UnionAll(null!));
        }

        [Fact]
        public void MergeHorizontally_ValidTables_ReturnsMergedTable()
        {
            var leftTable = new DataTable();
            leftTable.Columns.Add("Name", typeof(string));
            leftTable.Rows.Add("张三");
            leftTable.Rows.Add("李四");

            var rightTable = new DataTable();
            rightTable.Columns.Add("Age", typeof(int));
            rightTable.Rows.Add(25);
            rightTable.Rows.Add(30);

            var result = DataTableHelper.MergeHorizontally(leftTable, rightTable);

            Assert.Equal(2, result.Rows.Count);
            Assert.True(result.Columns.Contains("Left_Name"));
            Assert.True(result.Columns.Contains("Right_Age"));
        }

        [Fact]
        public void MergeHorizontally_NullTables_ThrowsArgumentNullException()
        {
            var table = new DataTable();

            Assert.Throws<ArgumentNullException>(() => DataTableHelper.MergeHorizontally(null!, table));
            Assert.Throws<ArgumentNullException>(() => DataTableHelper.MergeHorizontally(table, null!));
        }

        #endregion

        #region 高级查询测试

        [Fact]
        public void Query_WithWhereClause_ReturnsFilteredTable()
        {
            var dt = CreateTestDataTable();

            var result = DataTableHelper.Query(dt, "Age > 28");

            Assert.Equal(2, result.Rows.Count);
        }

        [Fact]
        public void Query_WithOrderByClause_ReturnsSortedTable()
        {
            var dt = CreateTestDataTable();

            var result = DataTableHelper.Query(dt, null, "Age DESC");

            Assert.Equal(35, result.Rows[0]["Age"]);
            Assert.Equal(25, result.Rows[result.Rows.Count - 1]["Age"]);
        }

        [Fact]
        public void Query_WithSelectColumns_ReturnsSelectedColumns()
        {
            var dt = CreateTestDataTable();

            var result = DataTableHelper.Query(dt, null, null, new[] { "Id", "Name" });

            Assert.Equal(2, result.Columns.Count);
            Assert.True(result.Columns.Contains("Id"));
            Assert.True(result.Columns.Contains("Name"));
            Assert.False(result.Columns.Contains("Age"));
        }

        [Fact]
        public void Query_NullDataTable_ThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => DataTableHelper.Query(null!));
        }

        [Fact]
        public void Search_ValidSearchText_ReturnsMatchingRows()
        {
            var dt = CreateTestDataTable();

            var result = DataTableHelper.Search(dt, "张");

            Assert.Single(result.Rows);
            Assert.Equal("张三", result.Rows[0]["Name"]);
        }

        [Fact]
        public void Search_CaseInsensitive_ReturnsMatchingRows()
        {
            var dt = new DataTable();
            dt.Columns.Add("Name", typeof(string));
            dt.Rows.Add("ZHANGSAN");
            dt.Rows.Add("lisi");

            var result = DataTableHelper.Search(dt, "zhang");

            Assert.Single(result.Rows);
        }

        [Fact]
        public void Search_EmptySearchText_ReturnsEmptyTable()
        {
            var dt = CreateTestDataTable();

            var result = DataTableHelper.Search(dt, "");

            Assert.Equal(0, result.Rows.Count);
        }

        [Fact]
        public void Search_NullDataTable_ThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => DataTableHelper.Search(null!, "test"));
        }

        [Fact]
        public void RegexFilter_ValidPattern_ReturnsMatchingRows()
        {
            var dt = new DataTable();
            dt.Columns.Add("Email", typeof(string));
            dt.Rows.Add("test@example.com");
            dt.Rows.Add("invalid-email");
            dt.Rows.Add("user@domain.org");

            var result = DataTableHelper.RegexFilter(dt, "Email", @"^[\w-\.]+@[\w-]+\.[a-z]{2,}$");

            Assert.Equal(2, result.Rows.Count);
        }

        [Fact]
        public void RegexFilter_EmptyPattern_ReturnsEmptyTable()
        {
            var dt = CreateTestDataTable();

            var result = DataTableHelper.RegexFilter(dt, "Name", "");

            Assert.Equal(0, result.Rows.Count);
        }

        [Fact]
        public void RegexFilter_NullDataTable_ThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => DataTableHelper.RegexFilter(null!, "Name", "pattern"));
        }

        [Fact]
        public void RegexFilter_NonExistentColumn_ThrowsArgumentException()
        {
            var dt = CreateTestDataTable();

            Assert.Throws<ArgumentException>(() => DataTableHelper.RegexFilter(dt, "NonExistent", "pattern"));
        }

        #endregion
    }

    public class TestUser
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int Age { get; set; }
    }
}
