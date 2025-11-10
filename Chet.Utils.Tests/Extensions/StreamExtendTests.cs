using System.Text;
using Xunit;

namespace Chet.Utils.Tests.Extensions
{
    public class StreamExtendTests
    {
        [Fact]
        public void CanReadSafe_CanWriteSafe_CanSeekSafe_Test()
        {
            using var ms = new MemoryStream();
            Assert.True(ms.CanReadSafe());
            Assert.True(ms.CanWriteSafe());
            Assert.True(ms.CanSeekSafe());
            Stream nullStream = null;
            Assert.False(nullStream.CanReadSafe());
            Assert.False(nullStream.CanWriteSafe());
            Assert.False(nullStream.CanSeekSafe());
        }

        [Fact]
        public void IsNullOrEmpty_Test()
        {
            Stream nullStream = null;
            Assert.True(nullStream.IsNullOrEmpty());
            using var ms = new MemoryStream();
            Assert.True(ms.IsNullOrEmpty());
            ms.WriteByte(1);
            Assert.False(ms.IsNullOrEmpty());
        }

        [Fact]
        public void ToBytes_ToText_Test()
        {
            var text = "hello world";
            using var ms = new MemoryStream(Encoding.UTF8.GetBytes(text));
            var bytes = ms.ToBytes();
            Assert.Equal(Encoding.UTF8.GetBytes(text), bytes);
            Assert.Equal(text, ms.ToText());
        }

        [Fact]
        public void WriteText_WriteBytes_Test()
        {
            using var ms = new MemoryStream();
            ms.WriteText("abc");
            Assert.Equal("abc", ms.ToText());
            ms.WriteBytes(Encoding.UTF8.GetBytes("xyz"));
            Assert.Equal("xyz", ms.ToText());
        }

        [Fact]
        public void SaveToFile_And_ToStream_File_Test()
        {
            var tempFile = Path.GetTempFileName();
            try
            {
                using var ms = new MemoryStream(Encoding.UTF8.GetBytes("filetest"));
                ms.SaveToFile(tempFile);
                using var fs = tempFile.ToStream();
                Assert.True(fs.CanRead);
                Assert.Equal("filetest", fs.ToText());
            }
            finally
            {
                if (File.Exists(tempFile))
                    File.Delete(tempFile);
            }
        }

        [Fact]
        public void StringAndBytes_ToStream_Test()
        {
            var str = "stream";
            using var s1 = str.ToStream(null);
            Assert.Equal(str, s1.ToText());
            var bytes = Encoding.UTF8.GetBytes(str);
            using var s2 = bytes.ToStream();
            Assert.Equal(str, s2.ToText());
        }

        [Fact]
        public void CopyToStream_Test()
        {
            using var src = new MemoryStream(Encoding.UTF8.GetBytes("copy"));
            using var dest = new MemoryStream();
            src.CopyToStream(dest);
            dest.Position = 0;
            Assert.Equal("copy", dest.ToText());
        }

        [Fact]
        public void ResetPosition_Test()
        {
            using var ms = new MemoryStream(Encoding.UTF8.GetBytes("reset"));
            ms.Position = ms.Length;
            ms.ResetPosition();
            Assert.Equal(0, ms.Position);
        }

        [Fact]
        public void ReadBytes_ReadText_Test()
        {
            var text = "abcdef";
            using var ms = new MemoryStream(Encoding.UTF8.GetBytes(text));
            var bytes = ms.ReadBytes(2, 3);
            Assert.Equal(Encoding.UTF8.GetBytes("cde"), bytes);
            var str = ms.ReadText(1, 4);
            Assert.Equal("bcde", str);
        }

        [Fact]
        public void IsMemoryStream_IsFileStream_IsNullStream_Test()
        {
            using var ms = new MemoryStream();
            Assert.True(ms.IsMemoryStream());
            Assert.False(ms.IsFileStream());
            Assert.False(ms.IsNullStream());
            using var fs = new FileStream(Path.GetTempFileName(), FileMode.OpenOrCreate);
            Assert.True(fs.IsFileStream());
            Assert.False(fs.IsMemoryStream());
            fs.Dispose();
            Assert.True(Stream.Null.IsNullStream());
        }

        [Fact]
        public void GetLength_GetPosition_SetPosition_Test()
        {
            using var ms = new MemoryStream(Encoding.UTF8.GetBytes("12345"));
            Assert.Equal(5, ms.GetLength());
            Assert.Equal(0, ms.GetPosition());
            ms.SetPosition(2);
            Assert.Equal(2, ms.Position);
            ms.SetPosition(100); // 超出范围不应改变
            Assert.True(ms.Position <= ms.Length);
        }

        [Fact]
        public void CloseSafe_Test()
        {
            var ms = new MemoryStream();
            ms.CloseSafe();
            // 多次调用不抛异常
            ms.CloseSafe();
        }
    }
}