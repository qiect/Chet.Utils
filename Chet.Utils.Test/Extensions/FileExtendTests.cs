using System.Text;
using Xunit;

namespace Chet.Utils.Tests.Extensions
{
    public class FileExtendTests : IDisposable
    {
        private readonly string tempDir;
        private readonly string tempFile;
        private readonly string tempFile2;

        public FileExtendTests()
        {
            tempDir = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
            Directory.CreateDirectory(tempDir);
            tempFile = Path.Combine(tempDir, "test.txt");
            tempFile2 = Path.Combine(tempDir, "test2.txt");
            File.WriteAllText(tempFile, "Hello\nWorld\n");
        }

        public void Dispose()
        {
            if (File.Exists(tempFile)) File.Delete(tempFile);
            if (File.Exists(tempFile2)) File.Delete(tempFile2);
            if (Directory.Exists(tempDir)) Directory.Delete(tempDir, true);
        }

        [Fact]
        public void Exists_FileExists_ReturnsTrue()
        {
            Assert.True(tempFile.Exists());
            Assert.False("nofile.txt".Exists());
        }

        [Fact]
        public void ReadAllText_FileExists_ReturnsContent()
        {
            Assert.Equal("Hello\nWorld\n", tempFile.ReadAllText());
        }

        [Fact]
        public void ReadAllText_FileNotExists_ReturnsEmpty()
        {
            Assert.Equal(string.Empty, "nofile.txt".ReadAllText());
        }

        [Fact]
        public void ReadAllLines_FileExists_ReturnsLines()
        {
            var lines = tempFile.ReadAllLines();
            Assert.Equal(2, lines.Length);
            Assert.Equal("Hello", lines[0]);
        }

        [Fact]
        public void ReadAllLines_FileNotExists_ReturnsEmptyArray()
        {
            var lines = "nofile.txt".ReadAllLines();
            Assert.Empty(lines);
        }

        [Fact]
        public void ReadAllBytes_FileExists_ReturnsBytes()
        {
            var bytes = tempFile.ReadAllBytes();
            Assert.True(bytes.Length > 0);
        }

        [Fact]
        public void ReadAllBytes_FileNotExists_ReturnsEmptyArray()
        {
            var bytes = "nofile.txt".ReadAllBytes();
            Assert.Empty(bytes);
        }

        [Fact]
        public void WriteAllText_OverwritesFile()
        {
            tempFile.WriteAllText("NewContent");
            Assert.Equal("NewContent", tempFile.ReadAllText());
        }

        [Fact]
        public void WriteAllLines_OverwritesFile()
        {
            tempFile.WriteAllLines(new[] { "A", "B" });
            var lines = tempFile.ReadAllLines();
            Assert.Equal(2, lines.Length);
            Assert.Equal("A", lines[0]);
        }

        [Fact]
        public void WriteAllBytes_OverwritesFile()
        {
            var data = Encoding.UTF8.GetBytes("123");
            tempFile.WriteAllBytes(data);
            var bytes = tempFile.ReadAllBytes();
            Assert.Equal(data.Length, bytes.Length);
        }

        [Fact]
        public void AppendText_AppendsContent()
        {
            tempFile.AppendText("!");
            Assert.EndsWith("!", tempFile.ReadAllText());
        }

        [Fact]
        public void AppendLines_AppendsLines()
        {
            tempFile.AppendLines(new[] { "C", "D" });
            var lines = tempFile.ReadAllLines();
            Assert.True(lines.Length >= 4);
        }

        [Fact]
        public void DeleteFile_RemovesFile()
        {
            tempFile.DeleteFile();
            Assert.False(tempFile.Exists());
        }

        [Fact]
        public void CopyTo_CopiesFile()
        {
            tempFile.CopyTo(tempFile2);
            Assert.True(tempFile2.Exists());
            Assert.Equal(tempFile.ReadAllText(), tempFile2.ReadAllText());
        }

        [Fact]
        public void MoveTo_MovesFile()
        {
            tempFile.MoveTo(tempFile2);
            Assert.False(tempFile.Exists());
            Assert.True(tempFile2.Exists());
        }

        [Fact]
        public void GetFileSize_ReturnsSize()
        {
            Assert.Equal(new FileInfo(tempFile).Length, tempFile.GetFileSize());
        }

        [Fact]
        public void GetCreationTime_ReturnsTime()
        {
            var t = tempFile.GetCreationTime();
            Assert.NotEqual(DateTime.MinValue, t);
        }

        [Fact]
        public void GetLastWriteTime_ReturnsTime()
        {
            var t = tempFile.GetLastWriteTime();
            Assert.NotEqual(DateTime.MinValue, t);
        }

        [Fact]
        public void GetLastAccessTime_ReturnsTime()
        {
            var t = tempFile.GetLastAccessTime();
            Assert.NotEqual(DateTime.MinValue, t);
        }

        [Fact]
        public void GetExtension_ReturnsExtension()
        {
            Assert.Equal(".txt", tempFile.GetExtension());
        }

        [Fact]
        public void GetFileName_ReturnsFileName()
        {
            Assert.Equal("test.txt", tempFile.GetFileName());
        }

        [Fact]
        public void GetFileNameWithoutExtension_ReturnsName()
        {
            Assert.Equal("test", tempFile.GetFileNameWithoutExtension());
        }

        [Fact]
        public void GetDirectoryName_ReturnsDirectory()
        {
            Assert.Equal(tempDir, tempFile.GetDirectoryName());
        }

        [Fact]
        public void IsReadOnly_SetAndUnset()
        {
            tempFile.SetReadOnly();
            Assert.True(tempFile.IsReadOnly());
            tempFile.UnsetReadOnly();
            Assert.False(tempFile.IsReadOnly());
        }

        [Fact]
        public void GetFileMd5_ReturnsMd5()
        {
            var md5 = tempFile.GetFileMd5();
            Assert.True(md5.Length == 32);
        }

        [Fact]
        public void GetFileSha256_ReturnsSha256()
        {
            var sha = tempFile.GetFileSha256();
            Assert.True(sha.Length == 64);
        }

        [Fact]
        public void IsHidden_SetAndUnset()
        {
            tempFile.SetHidden();
            Assert.True(tempFile.IsHidden());
            tempFile.UnsetHidden();
            Assert.False(tempFile.IsHidden());
        }
    }
}