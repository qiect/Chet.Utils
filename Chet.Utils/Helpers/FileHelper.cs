using System.IO.Compression;
using System.Security.Cryptography;
using System.Text;

namespace Chet.Utils.Helpers
{
    /// <summary>
    /// 文件操作帮助类，基础操作、读写操作、属性、目录操作、压缩解压、安全校验等功能集成
    /// </summary>
    public static class FileHelper
    {
        #region 文件基础操作

        /// <summary>
        /// 检查文件是否存在
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <returns>是否存在</returns>
        public static bool FileExists(string filePath)
        {
            return File.Exists(filePath);
        }

        /// <summary>
        /// 检查目录是否存在
        /// </summary>
        /// <param name="directoryPath">目录路径</param>
        /// <returns>是否存在</returns>
        public static bool DirectoryExists(string directoryPath)
        {
            return Directory.Exists(directoryPath);
        }

        /// <summary>
        /// 创建目录
        /// </summary>
        /// <param name="directoryPath">目录路径</param>
        public static void CreateDirectory(string directoryPath)
        {
            if (!DirectoryExists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }
        }

        /// <summary>
        /// 删除文件
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <param name="ignoreIfNotExists">如果文件不存在是否忽略错误</param>
        public static void DeleteFile(string filePath, bool ignoreIfNotExists = true)
        {
            if (FileExists(filePath))
            {
                File.Delete(filePath);
            }
            else if (!ignoreIfNotExists)
            {
                throw new FileNotFoundException($"文件未找到: {filePath}");
            }
        }

        /// <summary>
        /// 删除目录及其所有内容
        /// </summary>
        /// <param name="directoryPath">目录路径</param>
        /// <param name="recursive">是否递归删除子目录</param>
        public static void DeleteDirectory(string directoryPath, bool recursive = true)
        {
            if (DirectoryExists(directoryPath))
            {
                Directory.Delete(directoryPath, recursive);
            }
        }

        /// <summary>
        /// 复制文件
        /// </summary>
        /// <param name="sourceFilePath">源文件路径</param>
        /// <param name="destFilePath">目标文件路径</param>
        /// <param name="overwrite">是否覆盖已存在的文件</param>
        public static void CopyFile(string sourceFilePath, string destFilePath, bool overwrite = true)
        {
            if (!FileExists(sourceFilePath))
            {
                throw new FileNotFoundException($"源文件未找到: {sourceFilePath}");
            }

            // 确保目标目录存在
            CreateDirectory(Path.GetDirectoryName(destFilePath));
            File.Copy(sourceFilePath, destFilePath, overwrite);
        }

        /// <summary>
        /// 移动文件
        /// </summary>
        /// <param name="sourceFilePath">源文件路径</param>
        /// <param name="destFilePath">目标文件路径</param>
        /// <param name="overwrite">是否覆盖已存在的文件</param>
        public static void MoveFile(string sourceFilePath, string destFilePath, bool overwrite = true)
        {
            if (!FileExists(sourceFilePath))
            {
                throw new FileNotFoundException($"源文件未找到: {sourceFilePath}");
            }

            if (FileExists(destFilePath))
            {
                if (overwrite)
                {
                    DeleteFile(destFilePath);
                }
                else
                {
                    throw new IOException($"目标文件已存在: {destFilePath}");
                }
            }

            // 确保目标目录存在
            CreateDirectory(Path.GetDirectoryName(destFilePath));
            File.Move(sourceFilePath, destFilePath);
        }

        /// <summary>
        /// 获取文件信息
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <returns>FileInfo对象</returns>
        public static FileInfo GetFileInfo(string filePath)
        {
            return new FileInfo(filePath);
        }

        /// <summary>
        /// 获取目录信息
        /// </summary>
        /// <param name="directoryPath">目录路径</param>
        /// <returns>DirectoryInfo对象</returns>
        public static DirectoryInfo GetDirectoryInfo(string directoryPath)
        {
            return new DirectoryInfo(directoryPath);
        }

        /// <summary>
        /// 获取文件大小（字节）
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <returns>文件大小（字节）</returns>
        public static long GetFileSize(string filePath)
        {
            if (!FileExists(filePath))
            {
                throw new FileNotFoundException($"文件未找到: {filePath}");
            }

            return new FileInfo(filePath).Length;
        }

        #endregion

        #region 文件读写操作

        /// <summary>
        /// 读取文本文件内容
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <param name="encoding">编码格式，默认UTF-8</param>
        /// <returns>文件内容</returns>
        public static string ReadTextFile(string filePath, Encoding encoding = null)
        {
            if (!FileExists(filePath))
            {
                throw new FileNotFoundException($"文件未找到: {filePath}");
            }

            encoding = encoding ?? Encoding.UTF8;
            return File.ReadAllText(filePath, encoding);
        }

        /// <summary>
        /// 逐行读取文本文件内容
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <param name="encoding">编码格式，默认UTF-8</param>
        /// <returns>文件内容行集合</returns>
        public static IEnumerable<string> ReadLines(string filePath, Encoding encoding = null)
        {
            if (!FileExists(filePath))
            {
                throw new FileNotFoundException($"文件未找到: {filePath}");
            }

            encoding = encoding ?? Encoding.UTF8;
            return File.ReadLines(filePath, encoding);
        }

        /// <summary>
        /// 读取二进制文件内容
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <returns>文件字节数组</returns>
        public static byte[] ReadBinaryFile(string filePath)
        {
            if (!FileExists(filePath))
            {
                throw new FileNotFoundException($"文件未找到: {filePath}");
            }

            return File.ReadAllBytes(filePath);
        }

        /// <summary>
        /// 写入文本到文件
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <param name="content">文本内容</param>
        /// <param name="encoding">编码格式，默认UTF-8</param>
        /// <param name="append">是否追加模式</param>
        public static void WriteTextFile(string filePath, string content, Encoding encoding = null, bool append = false)
        {
            encoding = encoding ?? Encoding.UTF8;

            // 确保目录存在
            CreateDirectory(Path.GetDirectoryName(filePath));

            if (append)
            {
                File.AppendAllText(filePath, content, encoding);
            }
            else
            {
                File.WriteAllText(filePath, content, encoding);
            }
        }

        /// <summary>
        /// 逐行写入文本到文件
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <param name="lines">文本行集合</param>
        /// <param name="encoding">编码格式，默认UTF-8</param>
        /// <param name="append">是否追加模式</param>
        public static void WriteLines(string filePath, IEnumerable<string> lines, Encoding encoding = null, bool append = false)
        {
            encoding = encoding ?? Encoding.UTF8;

            // 确保目录存在
            CreateDirectory(Path.GetDirectoryName(filePath));

            if (append)
            {
                File.AppendAllLines(filePath, lines, encoding);
            }
            else
            {
                File.WriteAllLines(filePath, lines, encoding);
            }
        }

        /// <summary>
        /// 写入二进制数据到文件
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <param name="data">二进制数据</param>
        public static void WriteBinaryFile(string filePath, byte[] data)
        {
            // 确保目录存在
            CreateDirectory(Path.GetDirectoryName(filePath));
            File.WriteAllBytes(filePath, data);
        }

        /// <summary>
        /// 安全写入文件（先写入临时文件，再替换原文件）
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <param name="content">文件内容</param>
        /// <param name="encoding">编码格式</param>
        public static void SafeWriteTextFile(string filePath, string content, Encoding encoding = null)
        {
            encoding = encoding ?? Encoding.UTF8;
            var tempFilePath = filePath + ".tmp";

            try
            {
                WriteTextFile(tempFilePath, content, encoding);
                MoveFile(tempFilePath, filePath, true);
            }
            catch
            {
                // 如果出错，删除临时文件
                if (FileExists(tempFilePath))
                {
                    DeleteFile(tempFilePath);
                }
                throw;
            }
        }

        /// <summary>
        /// 追加文本到文件
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <param name="content">要追加的内容</param>
        /// <param name="encoding">编码格式</param>
        public static void AppendText(string filePath, string content, Encoding encoding = null)
        {
            WriteTextFile(filePath, content, encoding, true);
        }

        /// <summary>
        /// 读取文件的部分内容
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <param name="offset">偏移量</param>
        /// <param name="count">读取字节数</param>
        /// <returns>文件部分内容</returns>
        public static byte[] ReadPartialFile(string filePath, int offset, int count)
        {
            using (var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {
                fileStream.Seek(offset, SeekOrigin.Begin);
                var buffer = new byte[count];
                var bytesRead = fileStream.Read(buffer, 0, count);
                Array.Resize(ref buffer, bytesRead);
                return buffer;
            }
        }

        /// <summary>
        /// 流式读取大文件
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <param name="bufferSize">缓冲区大小</param>
        /// <returns>文件数据块枚举</returns>
        public static IEnumerable<byte[]> ReadFileInChunks(string filePath, int bufferSize = 8192)
        {
            using (var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {
                var buffer = new byte[bufferSize];
                int bytesRead;

                while ((bytesRead = fileStream.Read(buffer, 0, buffer.Length)) > 0)
                {
                    var chunk = new byte[bytesRead];
                    Array.Copy(buffer, chunk, bytesRead);
                    yield return chunk;
                }
            }
        }

        #endregion

        #region 文件属性与元数据

        /// <summary>
        /// 获取文件创建时间
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <returns>创建时间</returns>
        public static DateTime GetFileCreationTime(string filePath)
        {
            return new FileInfo(filePath).CreationTime;
        }

        /// <summary>
        /// 获取文件最后访问时间
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <returns>最后访问时间</returns>
        public static DateTime GetFileLastAccessTime(string filePath)
        {
            return new FileInfo(filePath).LastAccessTime;
        }

        /// <summary>
        /// 获取文件最后修改时间
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <returns>最后修改时间</returns>
        public static DateTime GetFileLastWriteTime(string filePath)
        {
            return new FileInfo(filePath).LastWriteTime;
        }

        /// <summary>
        /// 设置文件创建时间
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <param name="creationTime">创建时间</param>
        public static void SetFileCreationTime(string filePath, DateTime creationTime)
        {
            new FileInfo(filePath).CreationTime = creationTime;
        }

        /// <summary>
        /// 设置文件最后访问时间
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <param name="lastAccessTime">最后访问时间</param>
        public static void SetFileLastAccessTime(string filePath, DateTime lastAccessTime)
        {
            new FileInfo(filePath).LastAccessTime = lastAccessTime;
        }

        /// <summary>
        /// 设置文件最后修改时间
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <param name="lastWriteTime">最后修改时间</param>
        public static void SetFileLastWriteTime(string filePath, DateTime lastWriteTime)
        {
            new FileInfo(filePath).LastWriteTime = lastWriteTime;
        }

        /// <summary>
        /// 获取文件属性
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <returns>文件属性</returns>
        public static FileAttributes GetFileAttributes(string filePath)
        {
            return File.GetAttributes(filePath);
        }

        /// <summary>
        /// 设置文件属性
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <param name="attributes">文件属性</param>
        public static void SetFileAttributes(string filePath, FileAttributes attributes)
        {
            File.SetAttributes(filePath, attributes);
        }

        /// <summary>
        /// 获取文件扩展名
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <returns>文件扩展名</returns>
        public static string GetFileExtension(string filePath)
        {
            return Path.GetExtension(filePath);
        }

        /// <summary>
        /// 获取不带扩展名的文件名
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <returns>不带扩展名的文件名</returns>
        public static string GetFileNameWithoutExtension(string filePath)
        {
            return Path.GetFileNameWithoutExtension(filePath);
        }

        #endregion

        #region 目录操作

        /// <summary>
        /// 获取目录下的所有文件
        /// </summary>
        /// <param name="directoryPath">目录路径</param>
        /// <param name="searchPattern">搜索模式</param>
        /// <param name="searchOption">搜索选项</param>
        /// <returns>文件路径数组</returns>
        public static string[] GetFiles(string directoryPath, string searchPattern = "*", SearchOption searchOption = SearchOption.TopDirectoryOnly)
        {
            return Directory.GetFiles(directoryPath, searchPattern, searchOption);
        }

        /// <summary>
        /// 获取目录下的所有子目录
        /// </summary>
        /// <param name="directoryPath">目录路径</param>
        /// <param name="searchPattern">搜索模式</param>
        /// <param name="searchOption">搜索选项</param>
        /// <returns>子目录路径数组</returns>
        public static string[] GetDirectories(string directoryPath, string searchPattern = "*", SearchOption searchOption = SearchOption.TopDirectoryOnly)
        {
            return Directory.GetDirectories(directoryPath, searchPattern, searchOption);
        }

        /// <summary>
        /// 获取目录下的所有文件和子目录
        /// </summary>
        /// <param name="directoryPath">目录路径</param>
        /// <returns>文件和子目录路径数组</returns>
        public static string[] GetFileSystemEntries(string directoryPath)
        {
            return Directory.GetFileSystemEntries(directoryPath);
        }

        /// <summary>
        /// 计算目录大小
        /// </summary>
        /// <param name="directoryPath">目录路径</param>
        /// <param name="includeSubdirectories">是否包含子目录</param>
        /// <returns>目录总大小（字节）</returns>
        public static long GetDirectorySize(string directoryPath, bool includeSubdirectories = true)
        {
            if (!DirectoryExists(directoryPath))
            {
                throw new DirectoryNotFoundException($"目录未找到: {directoryPath}");
            }

            var searchOption = includeSubdirectories ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly;
            var files = Directory.GetFiles(directoryPath, "*", searchOption);

            return files.Sum(file => new FileInfo(file).Length);
        }

        /// <summary>
        /// 复制目录及其所有内容
        /// </summary>
        /// <param name="sourceDirectoryPath">源目录路径</param>
        /// <param name="targetDirectoryPath">目标目录路径</param>
        /// <param name="overwrite">是否覆盖已存在的文件</param>
        public static void CopyDirectory(string sourceDirectoryPath, string targetDirectoryPath, bool overwrite = true)
        {
            if (!DirectoryExists(sourceDirectoryPath))
            {
                throw new DirectoryNotFoundException($"源目录未找到: {sourceDirectoryPath}");
            }

            CreateDirectory(targetDirectoryPath);

            // 复制文件
            foreach (var filePath in GetFiles(sourceDirectoryPath))
            {
                var fileName = Path.GetFileName(filePath);
                var targetFilePath = Path.Combine(targetDirectoryPath, fileName);
                CopyFile(filePath, targetFilePath, overwrite);
            }

            // 递归复制子目录
            foreach (var directoryPath in GetDirectories(sourceDirectoryPath))
            {
                var directoryName = Path.GetFileName(directoryPath);
                var targetDirectoryPathRecursive = Path.Combine(targetDirectoryPath, directoryName);
                CopyDirectory(directoryPath, targetDirectoryPathRecursive, overwrite);
            }
        }

        /// <summary>
        /// 清空目录内容
        /// </summary>
        /// <param name="directoryPath">目录路径</param>
        /// <param name="deleteSubdirectories">是否删除子目录</param>
        public static void ClearDirectory(string directoryPath, bool deleteSubdirectories = true)
        {
            if (!DirectoryExists(directoryPath))
            {
                throw new DirectoryNotFoundException($"目录未找到: {directoryPath}");
            }

            // 删除所有文件
            foreach (var filePath in GetFiles(directoryPath))
            {
                DeleteFile(filePath);
            }

            // 删除所有子目录
            if (deleteSubdirectories)
            {
                foreach (var subDirectoryPath in GetDirectories(directoryPath))
                {
                    DeleteDirectory(subDirectoryPath, true);
                }
            }
        }

        /// <summary>
        /// 获取父目录路径
        /// </summary>
        /// <param name="path">路径</param>
        /// <returns>父目录路径</returns>
        public static string GetParentDirectory(string path)
        {
            return Directory.GetParent(path)?.FullName;
        }

        /// <summary>
        /// 获取目录名称
        /// </summary>
        /// <param name="directoryPath">目录路径</param>
        /// <returns>目录名称</returns>
        public static string GetDirectoryName(string directoryPath)
        {
            return Path.GetDirectoryName(directoryPath);
        }

        /// <summary>
        /// 获取程序当前运行目录
        /// </summary>
        /// <returns>当前运行目录</returns>
        public static string GetCurrentDirectory()
        {
            return Directory.GetCurrentDirectory();
        }

        /// <summary>
        /// 设置当前工作目录
        /// </summary>
        /// <param name="directoryPath">目录路径</param>
        public static void SetCurrentDirectory(string directoryPath)
        {
            Directory.SetCurrentDirectory(directoryPath);
        }

        #endregion

        #region 文件压缩与解压

        /// <summary>
        /// 压缩文件
        /// </summary>
        /// <param name="sourceFilePath">源文件路径</param>
        /// <param name="compressedFilePath">压缩文件路径</param>
        public static void CompressFile(string sourceFilePath, string compressedFilePath)
        {
            if (!FileExists(sourceFilePath))
            {
                throw new FileNotFoundException($"源文件未找到: {sourceFilePath}");
            }

            using (var sourceStream = new FileStream(sourceFilePath, FileMode.Open, FileAccess.Read))
            using (var compressedStream = new FileStream(compressedFilePath, FileMode.Create, FileAccess.Write))
            using (var compressionStream = new GZipStream(compressedStream, CompressionMode.Compress))
            {
                sourceStream.CopyTo(compressionStream);
            }
        }

        /// <summary>
        /// 解压文件
        /// </summary>
        /// <param name="compressedFilePath">压缩文件路径</param>
        /// <param name="destinationFilePath">解压后文件路径</param>
        public static void DecompressFile(string compressedFilePath, string destinationFilePath)
        {
            if (!FileExists(compressedFilePath))
            {
                throw new FileNotFoundException($"压缩文件未找到: {compressedFilePath}");
            }

            using (var compressedStream = new FileStream(compressedFilePath, FileMode.Open, FileAccess.Read))
            using (var decompressionStream = new GZipStream(compressedStream, CompressionMode.Decompress))
            using (var destinationStream = new FileStream(destinationFilePath, FileMode.Create, FileAccess.Write))
            {
                decompressionStream.CopyTo(destinationStream);
            }
        }

        /// <summary>
        /// 压缩目录
        /// </summary>
        /// <param name="sourceDirectoryPath">源目录路径</param>
        /// <param name="archiveFilePath">压缩包文件路径</param>
        public static void CompressDirectory(string sourceDirectoryPath, string archiveFilePath)
        {
            if (!DirectoryExists(sourceDirectoryPath))
            {
                throw new DirectoryNotFoundException($"源目录未找到: {sourceDirectoryPath}");
            }

            ZipFile.CreateFromDirectory(sourceDirectoryPath, archiveFilePath);
        }

        /// <summary>
        /// 解压目录
        /// </summary>
        /// <param name="archiveFilePath">压缩包文件路径</param>
        /// <param name="destinationDirectoryPath">解压目标目录路径</param>
        public static void DecompressDirectory(string archiveFilePath, string destinationDirectoryPath)
        {
            if (!FileExists(archiveFilePath))
            {
                throw new FileNotFoundException($"压缩包文件未找到: {archiveFilePath}");
            }

            CreateDirectory(destinationDirectoryPath);
            ZipFile.ExtractToDirectory(archiveFilePath, destinationDirectoryPath);
        }

        /// <summary>
        /// 向ZIP压缩包添加文件
        /// </summary>
        /// <param name="archiveFilePath">压缩包文件路径</param>
        /// <param name="filePathToAdd">要添加的文件路径</param>
        /// <param name="entryName">在压缩包中的条目名称</param>
        public static void AddFileToZip(string archiveFilePath, string filePathToAdd, string entryName = null)
        {
            using (var archive = ZipFile.Open(archiveFilePath, ZipArchiveMode.Update))
            {
                archive.CreateEntryFromFile(filePathToAdd, entryName ?? Path.GetFileName(filePathToAdd));
            }
        }

        /// <summary>
        /// 从ZIP压缩包提取文件
        /// </summary>
        /// <param name="archiveFilePath">压缩包文件路径</param>
        /// <param name="entryName">要提取的条目名称</param>
        /// <param name="destinationFilePath">提取后的文件路径</param>
        public static void ExtractFileFromZip(string archiveFilePath, string entryName, string destinationFilePath)
        {
            using (var archive = ZipFile.OpenRead(archiveFilePath))
            {
                var entry = archive.GetEntry(entryName);
                if (entry == null)
                {
                    throw new FileNotFoundException($"在压缩包中未找到条目: {entryName}");
                }

                CreateDirectory(Path.GetDirectoryName(destinationFilePath));
                entry.ExtractToFile(destinationFilePath, true);
            }
        }

        /// <summary>
        /// 列出ZIP压缩包中的所有条目
        /// </summary>
        /// <param name="archiveFilePath">压缩包文件路径</param>
        /// <returns>条目名称列表</returns>
        public static List<string> ListZipEntries(string archiveFilePath)
        {
            var entries = new List<string>();

            using (var archive = ZipFile.OpenRead(archiveFilePath))
            {
                foreach (var entry in archive.Entries)
                {
                    entries.Add(entry.FullName);
                }
            }

            return entries;
        }

        /// <summary>
        /// 压缩文本内容
        /// </summary>
        /// <param name="text">要压缩的文本</param>
        /// <param name="encoding">编码格式</param>
        /// <returns>压缩后的字节数组</returns>
        public static byte[] CompressText(string text, Encoding encoding = null)
        {
            encoding = encoding ?? Encoding.UTF8;
            var textBytes = encoding.GetBytes(text);

            using (var input = new MemoryStream(textBytes))
            using (var output = new MemoryStream())
            {
                using (var compressor = new GZipStream(output, CompressionMode.Compress))
                {
                    input.CopyTo(compressor);
                }
                return output.ToArray();
            }
        }

        /// <summary>
        /// 解压文本内容
        /// </summary>
        /// <param name="compressedData">压缩的数据</param>
        /// <param name="encoding">编码格式</param>
        /// <returns>解压后的文本</returns>
        public static string DecompressText(byte[] compressedData, Encoding encoding = null)
        {
            encoding = encoding ?? Encoding.UTF8;

            using (var input = new MemoryStream(compressedData))
            using (var output = new MemoryStream())
            {
                using (var decompressor = new GZipStream(input, CompressionMode.Decompress))
                {
                    decompressor.CopyTo(output);
                }

                return encoding.GetString(output.ToArray());
            }
        }

        /// <summary>
        /// 创建ZIP压缩包并添加多个文件
        /// </summary>
        /// <param name="filePaths">要添加的文件路径列表</param>
        /// <param name="archiveFilePath">压缩包文件路径</param>
        public static void CreateZipFromFiles(IEnumerable<string> filePaths, string archiveFilePath)
        {
            using (var archive = ZipFile.Open(archiveFilePath, ZipArchiveMode.Create))
            {
                foreach (var filePath in filePaths)
                {
                    if (FileExists(filePath))
                    {
                        archive.CreateEntryFromFile(filePath, Path.GetFileName(filePath));
                    }
                }
            }
        }

        #endregion

        #region 文件安全与校验

        /// <summary>
        /// 计算文件MD5哈希值
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <returns>MD5哈希值</returns>
        public static string CalculateMD5(string filePath)
        {
            if (!FileExists(filePath))
            {
                throw new FileNotFoundException($"文件未找到: {filePath}");
            }

            using (var md5 = MD5.Create())
            using (var stream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {
                var hash = md5.ComputeHash(stream);
                return BitConverter.ToString(hash).Replace("-", "").ToLowerInvariant();
            }
        }

        /// <summary>
        /// 计算文件SHA1哈希值
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <returns>SHA1哈希值</returns>
        public static string CalculateSHA1(string filePath)
        {
            if (!FileExists(filePath))
            {
                throw new FileNotFoundException($"文件未找到: {filePath}");
            }

            using (var sha1 = SHA1.Create())
            using (var stream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {
                var hash = sha1.ComputeHash(stream);
                return BitConverter.ToString(hash).Replace("-", "").ToLowerInvariant();
            }
        }

        /// <summary>
        /// 计算文件SHA256哈希值
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <returns>SHA256哈希值</returns>
        public static string CalculateSHA256(string filePath)
        {
            if (!FileExists(filePath))
            {
                throw new FileNotFoundException($"文件未找到: {filePath}");
            }

            using (var sha256 = SHA256.Create())
            using (var stream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {
                var hash = sha256.ComputeHash(stream);
                return BitConverter.ToString(hash).Replace("-", "").ToLowerInvariant();
            }
        }

        /// <summary>
        /// 比较两个文件是否相同
        /// </summary>
        /// <param name="filePath1">第一个文件路径</param>
        /// <param name="filePath2">第二个文件路径</param>
        /// <returns>是否相同</returns>
        public static bool CompareFiles(string filePath1, string filePath2)
        {
            if (!FileExists(filePath1) || !FileExists(filePath2))
            {
                return false;
            }

            // 先比较文件大小
            if (GetFileSize(filePath1) != GetFileSize(filePath2))
            {
                return false;
            }

            // 再比较哈希值
            return CalculateMD5(filePath1) == CalculateMD5(filePath2);
        }

        /// <summary>
        /// 加密文件
        /// </summary>
        /// <param name="sourceFilePath">源文件路径</param>
        /// <param name="encryptedFilePath">加密后文件路径</param>
        /// <param name="password">密码</param>
        public static void EncryptFile(string sourceFilePath, string encryptedFilePath, string password)
        {
            if (!FileExists(sourceFilePath))
            {
                throw new FileNotFoundException($"源文件未找到: {sourceFilePath}");
            }

            using (var aes = Aes.Create())
            {
                var key = Encoding.UTF8.GetBytes(password.PadRight(32, '0').Substring(0, 32));
                var iv = Encoding.UTF8.GetBytes(password.PadRight(16, '0').Substring(0, 16));

                aes.Key = key;
                aes.IV = iv;

                using (var sourceStream = new FileStream(sourceFilePath, FileMode.Open, FileAccess.Read))
                using (var destinationStream = new FileStream(encryptedFilePath, FileMode.Create, FileAccess.Write))
                using (var encryptor = aes.CreateEncryptor())
                using (var cryptoStream = new CryptoStream(destinationStream, encryptor, CryptoStreamMode.Write))
                {
                    sourceStream.CopyTo(cryptoStream);
                }
            }
        }

        /// <summary>
        /// 解密文件
        /// </summary>
        /// <param name="encryptedFilePath">加密文件路径</param>
        /// <param name="decryptedFilePath">解密后文件路径</param>
        /// <param name="password">密码</param>
        public static void DecryptFile(string encryptedFilePath, string decryptedFilePath, string password)
        {
            if (!FileExists(encryptedFilePath))
            {
                throw new FileNotFoundException($"加密文件未找到: {encryptedFilePath}");
            }

            using (var aes = Aes.Create())
            {
                var key = Encoding.UTF8.GetBytes(password.PadRight(32, '0').Substring(0, 32));
                var iv = Encoding.UTF8.GetBytes(password.PadRight(16, '0').Substring(0, 16));

                aes.Key = key;
                aes.IV = iv;

                using (var sourceStream = new FileStream(encryptedFilePath, FileMode.Open, FileAccess.Read))
                using (var destinationStream = new FileStream(decryptedFilePath, FileMode.Create, FileAccess.Write))
                using (var decryptor = aes.CreateDecryptor())
                using (var cryptoStream = new CryptoStream(sourceStream, decryptor, CryptoStreamMode.Read))
                {
                    cryptoStream.CopyTo(destinationStream);
                }
            }
        }

        /// <summary>
        /// 计算文件CRC32校验值
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <returns>CRC32校验值</returns>
        public static uint CalculateCRC32(string filePath)
        {
            if (!FileExists(filePath))
            {
                throw new FileNotFoundException($"文件未找到: {filePath}");
            }

            var table = GenerateCRC32Table();
            uint crc = 0xFFFFFFFF;

            using (var stream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {
                byte[] buffer = new byte[4096];
                int bytesRead;

                while ((bytesRead = stream.Read(buffer, 0, buffer.Length)) > 0)
                {
                    for (int i = 0; i < bytesRead; i++)
                    {
                        crc = (crc >> 8) ^ table[(crc & 0xFF) ^ buffer[i]];
                    }
                }
            }

            return crc ^ 0xFFFFFFFF;
        }

        /// <summary>
        /// 生成CRC32表
        /// </summary>
        /// <returns>CRC32表</returns>
        private static uint[] GenerateCRC32Table()
        {
            var table = new uint[256];
            for (uint i = 0; i < 256; i++)
            {
                var temp = i;
                for (int j = 8; j > 0; j--)
                {
                    if ((temp & 1) == 1)
                    {
                        temp = (temp >> 1) ^ 0xEDB88320;
                    }
                    else
                    {
                        temp >>= 1;
                    }
                }
                table[i] = temp;
            }
            return table;
        }

        /// <summary>
        /// 隐藏文件
        /// </summary>
        /// <param name="filePath">文件路径</param>
        public static void HideFile(string filePath)
        {
            if (FileExists(filePath))
            {
                var attributes = File.GetAttributes(filePath);
                attributes |= FileAttributes.Hidden;
                File.SetAttributes(filePath, attributes);
            }
        }

        /// <summary>
        /// 取消文件隐藏
        /// </summary>
        /// <param name="filePath">文件路径</param>
        public static void UnhideFile(string filePath)
        {
            if (FileExists(filePath))
            {
                var attributes = File.GetAttributes(filePath);
                attributes &= ~FileAttributes.Hidden;
                File.SetAttributes(filePath, attributes);
            }
        }

        /// <summary>
        /// 检查文件是否为只读
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <returns>是否为只读</returns>
        public static bool IsReadOnly(string filePath)
        {
            if (!FileExists(filePath))
            {
                throw new FileNotFoundException($"文件未找到: {filePath}");
            }

            var attributes = File.GetAttributes(filePath);
            return attributes.HasFlag(FileAttributes.ReadOnly);
        }

        #endregion
    }
}