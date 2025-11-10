using Xunit;

namespace Chet.Utils.Tests.Extensions
{
    public class EnumerableExtendTests
    {
        [Fact]
        public void IsNullOrEmpty_IEnumerable_NullOrEmpty_ReturnsTrue()
        {
            IEnumerable<int> nullList = null;
            IEnumerable<int> emptyList = new List<int>();
            Assert.True(nullList.IsNullOrEmpty());
            Assert.True(emptyList.IsNullOrEmpty());
        }

        [Fact]
        public void IsNullOrEmpty_IEnumerable_NotEmpty_ReturnsFalse()
        {
            IEnumerable<int> list = new List<int> { 1 };
            Assert.False(list.IsNullOrEmpty());
        }

        [Fact]
        public void IsNotEmpty_IEnumerable_NullOrEmpty_ReturnsFalse()
        {
            IEnumerable<int> nullList = null;
            IEnumerable<int> emptyList = new List<int>();
            Assert.False(nullList.IsNotEmpty());
            Assert.False(emptyList.IsNotEmpty());
        }

        [Fact]
        public void IsNotEmpty_IEnumerable_NotEmpty_ReturnsTrue()
        {
            IEnumerable<int> list = new List<int> { 1 };
            Assert.True(list.IsNotEmpty());
        }

        [Fact]
        public void SafeCount_NullOrEmpty_ReturnsZero()
        {
            IEnumerable<int> nullList = null;
            IEnumerable<int> emptyList = new List<int>();
            Assert.Equal(0, nullList.SafeCount());
            Assert.Equal(0, emptyList.SafeCount());
        }

        [Fact]
        public void SafeCount_NotEmpty_ReturnsCount()
        {
            IEnumerable<int> list = new List<int> { 1, 2, 3 };
            Assert.Equal(3, list.SafeCount());
        }

        [Fact]
        public void FirstOrDefaultSafe_NullOrEmpty_ReturnsDefault()
        {
            IEnumerable<int> nullList = null;
            IEnumerable<int> emptyList = new List<int>();
            Assert.Equal(0, nullList.FirstOrDefaultSafe());
            Assert.Equal(0, emptyList.FirstOrDefaultSafe());
        }

        [Fact]
        public void FirstOrDefaultSafe_NotEmpty_ReturnsFirst()
        {
            IEnumerable<int> list = new List<int> { 5, 6 };
            Assert.Equal(5, list.FirstOrDefaultSafe());
        }

        [Fact]
        public void LastOrDefaultSafe_NullOrEmpty_ReturnsDefault()
        {
            IEnumerable<int> nullList = null;
            IEnumerable<int> emptyList = new List<int>();
            Assert.Equal(0, nullList.LastOrDefaultSafe());
            Assert.Equal(0, emptyList.LastOrDefaultSafe());
        }

        [Fact]
        public void LastOrDefaultSafe_NotEmpty_ReturnsLast()
        {
            IEnumerable<int> list = new List<int> { 5, 6 };
            Assert.Equal(6, list.LastOrDefaultSafe());
        }

        [Fact]
        public void ContainsSafe_Null_ReturnsFalse()
        {
            IEnumerable<int> nullList = null;
            Assert.False(nullList.ContainsSafe(1));
        }

        [Fact]
        public void ContainsSafe_Contains_ReturnsTrue()
        {
            IEnumerable<int> list = new List<int> { 1, 2, 3 };
            Assert.True(list.ContainsSafe(2));
        }

        [Fact]
        public void ToListSafe_Null_ReturnsEmptyList()
        {
            IEnumerable<int> nullList = null;
            var result = nullList.ToListSafe();
            Assert.NotNull(result);
            Assert.Empty(result);
        }

        [Fact]
        public void ToListSafe_NotNull_ReturnsList()
        {
            IEnumerable<int> list = new[] { 1, 2 };
            var result = list.ToListSafe();
            Assert.Equal(list, result);
        }

        [Fact]
        public void ToArraySafe_Null_ReturnsEmptyArray()
        {
            IEnumerable<int> nullList = null;
            var result = nullList.ToArraySafe();
            Assert.NotNull(result);
            Assert.Empty(result);
        }

        [Fact]
        public void ToArraySafe_NotNull_ReturnsArray()
        {
            IEnumerable<int> list = new[] { 1, 2 };
            var result = list.ToArraySafe();
            Assert.Equal(list, result);
        }

        [Fact]
        public void DistinctSafe_Null_ReturnsEmpty()
        {
            IEnumerable<int> nullList = null;
            var result = nullList.DistinctSafe();
            Assert.Empty(result);
        }

        [Fact]
        public void DistinctSafe_NotNull_ReturnsDistinct()
        {
            IEnumerable<int> list = new[] { 1, 1, 2 };
            var result = list.DistinctSafe();
            Assert.Equal(new[] { 1, 2 }, result);
        }

        [Fact]
        public void WhereSafe_Null_ReturnsEmpty()
        {
            IEnumerable<int> nullList = null;
            var result = nullList.WhereSafe(x => x > 0);
            Assert.Empty(result);
        }

        [Fact]
        public void WhereSafe_PredicateNull_ReturnsAll()
        {
            IEnumerable<int> list = new[] { 1, 2 };
            var result = list.WhereSafe(null);
            Assert.Equal(list, result);
        }

        [Fact]
        public void SelectSafe_Null_ReturnsEmpty()
        {
            IEnumerable<int> nullList = null;
            var result = nullList.SelectSafe(x => x.ToString());
            Assert.Empty(result);
        }

        [Fact]
        public void GroupBySafe_Null_ReturnsEmpty()
        {
            IEnumerable<int> nullList = null;
            var result = nullList.GroupBySafe(x => x);
            Assert.Empty(result);
        }

        [Fact]
        public void OrderBySafe_Null_ReturnsEmpty()
        {
            IEnumerable<int> nullList = null;
            var result = nullList.OrderBySafe(x => x);
            Assert.Empty(result);
        }

        [Fact]
        public void OrderByDescendingSafe_Null_ReturnsEmpty()
        {
            IEnumerable<int> nullList = null;
            var result = nullList.OrderByDescendingSafe(x => x);
            Assert.Empty(result);
        }

        [Fact]
        public void Page_NullOrInvalid_ReturnsEmpty()
        {
            IEnumerable<int> nullList = null;
            var result1 = nullList.Page(0, 1);
            var result2 = new[] { 1, 2 }.Page(-1, 1);
            var result3 = new[] { 1, 2 }.Page(0, 0);
            Assert.Empty(result1);
            Assert.Empty(result2);
            Assert.Empty(result3);
        }

        [Fact]
        public void Page_Valid_ReturnsPage()
        {
            IEnumerable<int> list = new[] { 1, 2, 3, 4 };
            var result = list.Page(1, 2);
            Assert.Equal(new[] { 3, 4 }, result);
        }

        [Fact]
        public void SumSafe_NullOrEmpty_ReturnsZero()
        {
            IEnumerable<int> nullList = null;
            IEnumerable<int> emptyList = new int[0];
            Assert.Equal(0, nullList.SumSafe());
            Assert.Equal(0, emptyList.SumSafe());
        }

        [Fact]
        public void SumSafe_Int_ReturnsSum()
        {
            IEnumerable<int> list = new[] { 1, 2, 3 };
            Assert.Equal(6, list.SumSafe());
        }

        [Fact]
        public void AverageSafe_NullOrEmpty_ReturnsZero()
        {
            IEnumerable<int> nullList = null;
            IEnumerable<int> emptyList = new int[0];
            Assert.Equal(0, nullList.AverageSafe());
            Assert.Equal(0, emptyList.AverageSafe());
        }

        [Fact]
        public void AverageSafe_Int_ReturnsAverage()
        {
            IEnumerable<int> list = new[] { 2, 4 };
            Assert.Equal(3, list.AverageSafe());
        }

        [Fact]
        public void MaxSafe_NullOrEmpty_ReturnsZero()
        {
            IEnumerable<int> nullList = null;
            IEnumerable<int> emptyList = new int[0];
            Assert.Equal(0, nullList.MaxSafe());
            Assert.Equal(0, emptyList.MaxSafe());
        }

        [Fact]
        public void MaxSafe_Int_ReturnsMax()
        {
            IEnumerable<int> list = new[] { 2, 4 };
            Assert.Equal(4, list.MaxSafe());
        }

        [Fact]
        public void MinSafe_NullOrEmpty_ReturnsZero()
        {
            IEnumerable<int> nullList = null;
            IEnumerable<int> emptyList = new int[0];
            Assert.Equal(0, nullList.MinSafe());
            Assert.Equal(0, emptyList.MinSafe());
        }

        [Fact]
        public void MinSafe_Int_ReturnsMin()
        {
            IEnumerable<int> list = new[] { 2, 4 };
            Assert.Equal(2, list.MinSafe());
        }

        [Fact]
        public void RemoveNulls_Null_ReturnsEmpty()
        {
            IEnumerable<string> nullList = null;
            var result = nullList.RemoveNulls();
            Assert.Empty(result);
        }

        [Fact]
        public void RemoveNulls_Valid_RemovesNulls()
        {
            IEnumerable<string> list = new[] { "a", null, "b" };
            var result = list.RemoveNulls();
            Assert.Equal(new[] { "a", "b" }, result);
        }

        [Fact]
        public void ToDictionarySafe_Null_ReturnsEmptyDictionary()
        {
            IEnumerable<string> nullList = null;
            var result = nullList.ToDictionarySafe(x => x, x => x);
            Assert.Empty(result);
        }

        [Fact]
        public void ToDictionarySafe_SelectorNull_UsesDefault()
        {
            IEnumerable<string> list = new[] { "a" };
            var result = list.ToDictionarySafe(x => x, y => y);
            Assert.Single(result);
        }

        [Fact]
        public void ToHashSetSafe_Null_ReturnsEmptyHashSet()
        {
            IEnumerable<int> nullList = null;
            var result = nullList.ToHashSetSafe();
            Assert.Empty(result);
        }

        [Fact]
        public void ToHashSetSafe_Valid_ReturnsHashSet()
        {
            IEnumerable<int> list = new[] { 1, 2, 2 };
            var result = list.ToHashSetSafe();
            Assert.Equal(new HashSet<int> { 1, 2 }, result);
        }

        [Fact]
        public void ForEachSafe_NullOrActionNull_DoesNothing()
        {
            IEnumerable<int> nullList = null;
            int count = 0;
            nullList.ForEachSafe(x => count++);
            new[] { 1, 2 }.ForEachSafe(null);
            Assert.Equal(0, count);
        }

        [Fact]
        public void ForEachSafe_Valid_ExecutesAction()
        {
            IEnumerable<int> list = new[] { 1, 2 };
            int sum = 0;
            list.ForEachSafe(x => sum += x);
            Assert.Equal(3, sum);
        }

        [Fact]
        public void Chunk_NullOrInvalidSize_ReturnsEmpty()
        {
            IEnumerable<int> nullList = null;
            var result1 = nullList.Chunk(2);
            var result2 = new[] { 1, 2 }.Chunk(0);
            Assert.Empty(result1);
            Assert.Empty(result2);
        }

        [Fact]
        public void Chunk_Valid_ReturnsChunks()
        {
            IEnumerable<int> list = new[] { 1, 2, 3, 4, 5 };
            var result = list.Chunk(2).ToList();
            Assert.Equal(3, result.Count);
            Assert.Equal(new[] { 1, 2 }, result[0]);
            Assert.Equal(new[] { 3, 4 }, result[1]);
            Assert.Equal(new[] { 5 }, result[2]);
        }

        [Fact]
        public void AllSafe_NullOrPredicateNull_ReturnsFalse()
        {
            IEnumerable<int> nullList = null;
            Assert.False(nullList.AllSafe(x => x > 0));
            Assert.False(new[] { 1, 2 }.AllSafe(null));
        }

        [Fact]
        public void AllSafe_Valid_ReturnsTrueOrFalse()
        {
            IEnumerable<int> list = new[] { 1, 2, 3 };
            Assert.True(list.AllSafe(x => x > 0));
            Assert.False(list.AllSafe(x => x > 2));
        }

        [Fact]
        public void AnySafe_NullOrPredicateNull_ReturnsFalse()
        {
            IEnumerable<int> nullList = null;
            Assert.False(nullList.AnySafe(x => x > 0));
            Assert.False(new[] { 1, 2 }.AnySafe(null));
        }

        [Fact]
        public void AnySafe_Valid_ReturnsTrueOrFalse()
        {
            IEnumerable<int> list = new[] { 1, 2, 3 };
            Assert.True(list.AnySafe(x => x > 2));
            Assert.False(list.AnySafe(x => x > 3));
        }

        [Fact]
        public void ReverseSafe_Null_ReturnsEmpty()
        {
            IEnumerable<int> nullList = null;
            var result = nullList.ReverseSafe();
            Assert.Empty(result);
        }

        [Fact]
        public void ReverseSafe_Valid_ReturnsReversed()
        {
            IEnumerable<int> list = new[] { 1, 2, 3 };
            var result = list.ReverseSafe();
            Assert.Equal(new[] { 3, 2, 1 }, result);
        }

        [Fact]
        public void ToDataTable_Null_ReturnsEmptyTable()
        {
            IEnumerable<TestClass> nullList = null;
            var dt = nullList.ToDataTable();
            Assert.NotNull(dt);
            Assert.Equal("TestClass", dt.TableName);
            Assert.Empty(dt.Rows);
        }

        [Fact]
        public void ToDataTable_Valid_ReturnsTable()
        {
            var list = new[] { new TestClass { Id = 1, Name = "A" }, new TestClass { Id = 2, Name = "B" } };
            var dt = list.ToDataTable();
            Assert.Equal(2, dt.Rows.Count);
            Assert.Equal("Id", dt.Columns[0].ColumnName);
            Assert.Equal("Name", dt.Columns[1].ColumnName);
            Assert.Equal(1, dt.Rows[0][0]);
            Assert.Equal("A", dt.Rows[0][1]);
        }

        public class TestClass
        {
            public int Id { get; set; }
            public string Name { get; set; }
        }

        [Fact]
        public void ToConcurrentDictionary_KV_Null_ReturnsEmpty()
        {
            IEnumerable<KeyValuePair<int, string>> nullList = null;
            var dict = nullList.ToConcurrentDictionary();
            Assert.Empty(dict);
        }

        [Fact]
        public void ToConcurrentDictionary_KV_Valid_ReturnsDict()
        {
            var list = new[] { new KeyValuePair<int, string>(1, "A"), new KeyValuePair<int, string>(2, "B") };
            var dict = list.ToConcurrentDictionary();
            Assert.Equal("A", dict[1]);
            Assert.Equal("B", dict[2]);
        }

        [Fact]
        public void ToConcurrentDictionary_Custom_Null_ReturnsEmpty()
        {
            IEnumerable<string> nullList = null;
            var dict = nullList.ToConcurrentDictionary(x => x, x => x);
            Assert.Empty(dict);
        }

        [Fact]
        public void ToConcurrentDictionary_Custom_Valid_ReturnsDict()
        {
            var list = new[] { "A", "B" };
            var dict = list.ToConcurrentDictionary(x => x, x => x.ToLower());
            Assert.Equal("a", dict["A"]);
            Assert.Equal("b", dict["B"]);
        }

        [Fact]
        public void IsNullOrEmpty_ICollection_NullOrEmpty_ReturnsTrue()
        {
            ICollection<int> nullCol = null;
            ICollection<int> emptyCol = new List<int>();
            Assert.True(nullCol.IsNullOrEmpty());
            Assert.True(emptyCol.IsNullOrEmpty());
        }

        [Fact]
        public void IsNullOrEmpty_ICollection_NotEmpty_ReturnsFalse()
        {
            ICollection<int> col = new List<int> { 1 };
            Assert.False(col.IsNullOrEmpty());
        }

        [Fact]
        public void AddSafe_Null_DoesNothing()
        {
            ICollection<int> nullCol = null;
            nullCol.AddSafe(1); // no exception
        }

        [Fact]
        public void AddSafe_Valid_AddsItem()
        {
            ICollection<int> col = new List<int>();
            col.AddSafe(1);
            Assert.Contains(1, col);
        }

        [Fact]
        public void RemoveSafe_NullOrNotFound_DoesNothing()
        {
            ICollection<int> nullCol = null;
            nullCol.RemoveSafe(1); // no exception
            ICollection<int> col = new List<int> { 2 };
            col.RemoveSafe(1);
            Assert.Contains(2, col);
        }

        [Fact]
        public void RemoveSafe_Valid_RemovesItem()
        {
            ICollection<int> col = new List<int> { 1, 2 };
            col.RemoveSafe(1);
            Assert.DoesNotContain(1, col);
        }

        [Fact]
        public void ClearSafe_Null_DoesNothing()
        {
            ICollection<int> nullCol = null;
            nullCol.ClearSafe(); // no exception
        }

        [Fact]
        public void ClearSafe_Valid_ClearsCollection()
        {
            ICollection<int> col = new List<int> { 1, 2 };
            col.ClearSafe();
            Assert.Empty(col);
        }

        [Fact]
        public void AddRangeSafe_NullOrItemsNull_DoesNothing()
        {
            ICollection<int> nullCol = null;
            nullCol.AddRangeSafe(new[] { 1, 2 }); // no exception
            ICollection<int> col = new List<int>();
            col.AddRangeSafe(null); // no exception
            Assert.Empty(col);
        }

        [Fact]
        public void AddRangeSafe_Valid_AddsItems()
        {
            ICollection<int> col = new List<int>();
            col.AddRangeSafe(new[] { 1, 2 });
            Assert.Contains(1, col);
            Assert.Contains(2, col);
        }

        [Fact]
        public void RemoveRangeSafe_NullOrItemsNull_DoesNothing()
        {
            ICollection<int> nullCol = null;
            nullCol.RemoveRangeSafe(new[] { 1, 2 }); // no exception
            ICollection<int> col = new List<int> { 1, 2 };
            col.RemoveRangeSafe(null); // no exception
            Assert.Equal(2, col.Count);
        }

        [Fact]
        public void RemoveRangeSafe_Valid_RemovesItems()
        {
            ICollection<int> col = new List<int> { 1, 2, 3 };
            col.RemoveRangeSafe(new[] { 1, 3 });
            Assert.DoesNotContain(1, col);
            Assert.DoesNotContain(3, col);
            Assert.Contains(2, col);
        }
    }
}