using System.ComponentModel;
using Xunit;

namespace Chet.Utils.Tests.Extensions
{
    public class EnumExtendTests
    {
        private enum TestEnum
        {
            [Description("无")]
            None = 0,
            [Description("第一个")]
            First = 1,
            [Description("第二个")]
            Second = 2,
            Third = 3
        }

        [Flags]
        private enum FlagEnum
        {
            None = 0,
            A = 1,
            B = 2,
            C = 4
        }

        [Fact]
        public void IsDefined_Test()
        {
            Assert.True(TestEnum.First.IsDefined());
            Assert.False(((TestEnum)100).IsDefined());
        }

        [Fact]
        public void GetValues_Test()
        {
            var values = EnumExtensions.GetValues<TestEnum>();
            Assert.Contains(TestEnum.First, values);
            Assert.Contains(TestEnum.Second, values);
            Assert.Contains(TestEnum.Third, values);
        }

        [Fact]
        public void GetNames_Test()
        {
            var names = EnumExtensions.GetNames<TestEnum>();
            Assert.Contains("First", names);
            Assert.Contains("Second", names);
            Assert.Contains("Third", names);
        }

        [Fact]
        public void ToInt_ToLong_Test()
        {
            Assert.Equal(1, TestEnum.First.ToInt());
            Assert.Equal(2L, TestEnum.Second.ToLong());
        }

        [Fact]
        public void ToStringValue_Test()
        {
            Assert.Equal("First", TestEnum.First.ToStringValue());
        }

        [Fact]
        public void GetDescription_Test()
        {
            Assert.Equal("第一个", TestEnum.First.GetDescription());
            Assert.Equal("Third", TestEnum.Third.GetDescription());
        }

        [Fact]
        public void FromDescription_Test()
        {
            Assert.Equal(TestEnum.First, "第一个".FromDescription(TestEnum.None));
            Assert.Equal(TestEnum.Third, "Third".FromDescription(TestEnum.None));
            Assert.Equal(TestEnum.None, "不存在".FromDescription(TestEnum.None));
        }

        [Fact]
        public void Parse_Test()
        {
            Assert.Equal(TestEnum.First, "First".Parse(TestEnum.None));
            Assert.Equal(TestEnum.Second, "second".Parse(TestEnum.None));
            Assert.Equal(TestEnum.None, "".Parse(TestEnum.None));
            Assert.Equal(TestEnum.None, "notfound".Parse(TestEnum.None));
        }

        [Fact]
        public void ToEnum_Int_Long_Test()
        {
            Assert.Equal(TestEnum.Second, 2.ToEnum(TestEnum.None));
            Assert.Equal(TestEnum.Third, 3L.ToEnum(TestEnum.None));
            Assert.Equal(TestEnum.None, 100.ToEnum(TestEnum.None));
        }

        [Fact]
        public void GetValueDescriptionDict_Test()
        {
            var dict = EnumExtensions.GetValueDescriptionDict<TestEnum>();
            Assert.Equal("第一个", dict[TestEnum.First]);
            Assert.Equal("无", dict[TestEnum.None]);
            Assert.Equal("Third", dict[TestEnum.Third]);
        }

        [Fact]
        public void GetNameDescriptionDict_Test()
        {
            var dict = EnumExtensions.GetNameDescriptionDict<TestEnum>();
            Assert.Equal("第一个", dict["First"]);
            Assert.Equal("无", dict["None"]);
            Assert.Equal("Third", dict["Third"]);
        }

        [Fact]
        public void HasFlag_AddFlag_RemoveFlag_Test()
        {
            var value = FlagEnum.A.AddFlag(FlagEnum.B);
            Assert.True(value.HasFlag(FlagEnum.A));
            Assert.True(value.HasFlag(FlagEnum.B));
            Assert.False(value.HasFlag(FlagEnum.C));

            value = value.AddFlag(FlagEnum.C);
            Assert.True(value.HasFlag(FlagEnum.C));

            value = value.RemoveFlag(FlagEnum.B);
            Assert.False(value.HasFlag(FlagEnum.B));
            Assert.True(value.HasFlag(FlagEnum.A));
            Assert.True(value.HasFlag(FlagEnum.C));
        }

        [Fact]
        public void GetUnderlyingType_Test()
        {
            Assert.Equal(typeof(int), EnumExtensions.GetUnderlyingType<TestEnum>());
            Assert.Equal(typeof(int), EnumExtensions.GetUnderlyingType<FlagEnum>());
        }
    }
}