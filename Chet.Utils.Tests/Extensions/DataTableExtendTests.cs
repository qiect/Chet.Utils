using System.Data;
using Xunit;

namespace Chet.Utils.Tests.Extensions
{
    public class DataTableExtendTests
    {
        [Fact]
        public void IsNullOrEmpty_ReturnsTrue_WhenNullOrNoRows()
        {
            DataTable dtNull = null;
            var dtEmpty = new DataTable();
            Assert.True(dtNull.IsNullOrEmpty());
            Assert.True(dtEmpty.IsNullOrEmpty());
        }

        [Fact]
        public void IsNullOrEmpty_ReturnsFalse_WhenHasRows()
        {
            var dt = new DataTable();
            dt.Columns.Add("A");
            dt.Rows.Add("1");
            Assert.False(dt.IsNullOrEmpty());
        }

        [Fact]
        public void ToList_ReturnsEmpty_WhenNullOrNoRows()
        {
            DataTable dtNull = null;
            Assert.Empty(dtNull.ToList(r => r));
            var dt = new DataTable();
            Assert.Empty(dt.ToList(r => r));
        }

        [Fact]
        public void ToList_ConvertsRows()
        {
            var dt = new DataTable();
            dt.Columns.Add("A", typeof(int));
            dt.Rows.Add(1);
            dt.Rows.Add(2);
            var list = dt.ToList(r => (int)r["A"]);
            Assert.Equal(new[] { 1, 2 }, list);
        }

        [Fact]
        public void GetColumnNames_ReturnsEmpty_WhenNullOrNoColumns()
        {
            DataTable dtNull = null;
            Assert.Empty(dtNull.GetColumnNames());
            var dt = new DataTable();
            Assert.Empty(dt.GetColumnNames());
        }

        [Fact]
        public void GetColumnNames_ReturnsNames()
        {
            var dt = new DataTable();
            dt.Columns.Add("A");
            dt.Columns.Add("B");
            var names = dt.GetColumnNames();
            Assert.Equal(new[] { "A", "B" }, names);
        }

        [Fact]
        public void ToDictionaryList_ReturnsEmpty_WhenNullOrNoRows()
        {
            DataTable dtNull = null;
            Assert.Empty(dtNull.ToDictionaryList());
            var dt = new DataTable();
            Assert.Empty(dt.ToDictionaryList());
        }

        [Fact]
        public void ToDictionaryList_ReturnsRowDictionaries()
        {
            var dt = new DataTable();
            dt.Columns.Add("A");
            dt.Columns.Add("B");
            dt.Rows.Add("1", "2");
            var list = dt.ToDictionaryList();
            Assert.Single(list);
            Assert.Equal("1", list[0]["A"]);
            Assert.Equal("2", list[0]["B"]);
        }

        [Fact]
        public void Filter_ReturnsSelfOrClone_WhenNullOrNoMatch()
        {
            DataTable dtNull = null;
            Assert.Null(dtNull.Filter("A=1"));
            var dt = new DataTable();
            dt.Columns.Add("A", typeof(int));
            var filtered = dt.Filter("A=1");
            Assert.Empty(filtered.Rows);
            Assert.Equal(dt.Columns.Count, filtered.Columns.Count);
        }

        [Fact]
        public void Filter_ReturnsFilteredRows()
        {
            var dt = new DataTable();
            dt.Columns.Add("A", typeof(int));
            dt.Rows.Add(1);
            dt.Rows.Add(2);
            var filtered = dt.Filter("A=2");
            Assert.Single(filtered.Rows);
            Assert.Equal(2, filtered.Rows[0]["A"]);
        }

        [Fact]
        public void Sort_ReturnsSelfOrClone_WhenNullOrNoRows()
        {
            DataTable dtNull = null;
            Assert.Null(dtNull.Sort("A DESC"));
            var dt = new DataTable();
            dt.Columns.Add("A", typeof(int));
            var sorted = dt.Sort("A DESC");
            Assert.Empty(sorted.Rows);
            Assert.Equal(dt.Columns.Count, sorted.Columns.Count);
        }

        [Fact]
        public void Sort_ReturnsSortedRows()
        {
            var dt = new DataTable();
            dt.Columns.Add("A", typeof(int));
            dt.Rows.Add(2);
            dt.Rows.Add(1);
            var sorted = dt.Sort("A ASC");
            Assert.Equal(1, sorted.Rows[0]["A"]);
            Assert.Equal(2, sorted.Rows[1]["A"]);
        }

        [Fact]
        public void AsEnumerableDictionary_ReturnsEmpty_WhenNullOrNoRows()
        {
            DataTable dtNull = null;
            Assert.Empty(dtNull.AsEnumerableDictionary());
            var dt = new DataTable();
            Assert.Empty(dt.AsEnumerableDictionary());
        }

        [Fact]
        public void AsEnumerableDictionary_ReturnsRowDictionaries()
        {
            var dt = new DataTable();
            dt.Columns.Add("A");
            dt.Rows.Add("x");
            var list = dt.AsEnumerableDictionary().ToList();
            Assert.Single(list);
            Assert.Equal("x", list[0]["A"]);
        }

        [Fact]
        public void CopyAll_ReturnsNull_WhenNull()
        {
            DataTable dtNull = null;
            Assert.Null(dtNull.CopyAll());
        }

        [Fact]
        public void CopyAll_ReturnsDeepCopy()
        {
            var dt = new DataTable();
            dt.Columns.Add("A");
            dt.Rows.Add("1");
            var copy = dt.CopyAll();
            Assert.Equal(dt.Rows.Count, copy.Rows.Count);
            Assert.Equal(dt.Columns.Count, copy.Columns.Count);
            Assert.NotSame(dt, copy);
        }

        [Fact]
        public void CloneStructure_ReturnsNull_WhenNull()
        {
            DataTable dtNull = null;
            Assert.Null(dtNull.CloneStructure());
        }

        [Fact]
        public void CloneStructure_ReturnsStructureOnly()
        {
            var dt = new DataTable();
            dt.Columns.Add("A");
            dt.Rows.Add("1");
            var clone = dt.CloneStructure();
            Assert.Equal(dt.Columns.Count, clone.Columns.Count);
            Assert.Empty(clone.Rows);
        }

        [Fact]
        public void AddRow_DoesNotThrow_WhenNullOrValuesNull()
        {
            DataTable dtNull = null;
            var dt = new DataTable();
            dt.Columns.Add("A");
            Exception ex1 = Record.Exception(() => dtNull.AddRow(1, 2));
            Exception ex2 = Record.Exception(() => dt.AddRow(null));
            Assert.Null(ex1);
            Assert.Null(ex2);
        }

        [Fact]
        public void AddRow_AddsRow()
        {
            var dt = new DataTable();
            dt.Columns.Add("A");
            dt.Columns.Add("B");
            dt.AddRow(1, 2, 3); // 超出列数应忽略
            Assert.Single(dt.Rows);
            Assert.Equal(1, Convert.ToInt32(dt.Rows[0]["A"]));
            Assert.Equal(2, Convert.ToInt32(dt.Rows[0]["B"]));
        }

        [Fact]
        public void ClearRows_DoesNotThrow_WhenNullOrNormal()
        {
            DataTable dtNull = null;
            var dt = new DataTable();
            dt.Columns.Add("A");
            dt.Rows.Add("1");
            Exception ex1 = Record.Exception(() => dtNull.ClearRows());
            Exception ex2 = Record.Exception(() => dt.ClearRows());
            Assert.Null(ex1);
            Assert.Null(ex2);
            Assert.Empty(dt.Rows);
        }

        [Fact]
        public void ToArray_ReturnsEmpty_WhenNullOrNoRows()
        {
            DataTable dtNull = null;
            Assert.Equal(0, dtNull.ToArray().Length);
            var dt = new DataTable();
            Assert.Equal(0, dt.ToArray().Length);
        }

        [Fact]
        public void ToArray_Returns2DArray()
        {
            var dt = new DataTable();
            dt.Columns.Add("A");
            dt.Columns.Add("B");
            dt.Rows.Add(1, 2);
            dt.Rows.Add(3, 4);
            var arr = dt.ToArray();
            Assert.Equal(2, arr.GetLength(0));
            Assert.Equal(2, arr.GetLength(1));
            Assert.Equal(1, Convert.ToInt32(arr[0, 0]));
            Assert.Equal(4, Convert.ToInt32(arr[1, 1]));
        }
    }
}