namespace Chet.Utils
{
    /// <summary>
    /// File 扩展方法类，提供常用的读写、判断、信息获取、操作等功能。
    /// </summary>
    public static class FileExtensions
    {
        #region File 扩展方法，提供常用的读写、判断、信息获取、操作等功能。
        /// <summary>
        /// 判断文件是否存在。
        /// </summary>
        /// <param name="filePath">文件路径。</param>
        public static bool Exists(this string filePath) => File.Exists(filePath);

        /// <summary>
        /// 读取文件所有文本内容。
        /// </summary>
        /// <param name="filePath">文件路径。</param>
        public static string ReadAllText(this string filePath) =>
            File.Exists(filePath) ? File.ReadAllText(filePath) : string.Empty;

        /// <summary>
        /// 读取文件所有行内容。
        /// </summary>
        /// <param name="filePath">文件路径。</param>
        public static string[] ReadAllLines(this string filePath) =>
            File.Exists(filePath) ? File.ReadAllLines(filePath) : Array.Empty<string>();

        /// <summary>
        /// 读取文件所有字节内容。
        /// </summary>
        /// <param name="filePath">文件路径。</param>
        public static byte[] ReadAllBytes(this string filePath) =>
            File.Exists(filePath) ? File.ReadAllBytes(filePath) : Array.Empty<byte>();

        /// <summary>
        /// 写入文本内容到文件（覆盖）。
        /// </summary>
        /// <param name="filePath">文件路径。</param>
        /// <param name="content">写入内容。</param>
        public static void WriteAllText(this string filePath, string content) =>
            File.WriteAllText(filePath, content ?? string.Empty);

        /// <summary>
        /// 写入多行内容到文件（覆盖）。
        /// </summary>
        /// <param name="filePath">文件路径。</param>
        /// <param name="lines">写入行内容。</param>
        public static void WriteAllLines(this string filePath, IEnumerable<string> lines) =>
            File.WriteAllLines(filePath, lines ?? Array.Empty<string>());

        /// <summary>
        /// 写入字节内容到文件（覆盖）。
        /// </summary>
        /// <param name="filePath">文件路径。</param>
        /// <param name="bytes">写入字节内容。</param>
        public static void WriteAllBytes(this string filePath, byte[] bytes) =>
            File.WriteAllBytes(filePath, bytes ?? Array.Empty<byte>());

        /// <summary>
        /// 追加文本内容到文件。
        /// </summary>
        /// <param name="filePath">文件路径。</param>
        /// <param name="content">追加内容。</param>
        public static void AppendText(this string filePath, string content) =>
            File.AppendAllText(filePath, content ?? string.Empty);

        /// <summary>
        /// 追加多行内容到文件。
        /// </summary>
        /// <param name="filePath">文件路径。</param>
        /// <param name="lines">追加行内容。</param>
        public static void AppendLines(this string filePath, IEnumerable<string> lines) =>
            File.AppendAllLines(filePath, lines ?? Array.Empty<string>());

        /// <summary>
        /// 删除文件。
        /// </summary>
        /// <param name="filePath">文件路径。</param>
        public static void DeleteFile(this string filePath)
        {
            if (File.Exists(filePath))
                File.Delete(filePath);
        }

        /// <summary>
        /// 复制文件到目标路径（可覆盖）。
        /// </summary>
        /// <param name="filePath">源文件路径。</param>
        /// <param name="destPath">目标文件路径。</param>
        /// <param name="overwrite">是否覆盖目标文件。</param>
        public static void CopyTo(this string filePath, string destPath, bool overwrite = true)
        {
            if (File.Exists(filePath))
                File.Copy(filePath, destPath, overwrite);
        }

        /// <summary>
        /// 移动文件到目标路径（可覆盖）。
        /// </summary>
        /// <param name="filePath">源文件路径。</param>
        /// <param name="destPath">目标文件路径。</param>
        /// <param name="overwrite">是否覆盖目标文件。</param>
        public static void MoveTo(this string filePath, string destPath, bool overwrite = true)
        {
            if (!File.Exists(filePath)) return;
            if (overwrite && File.Exists(destPath))
                File.Delete(destPath);
            File.Move(filePath, destPath);
        }

        /// <summary>
        /// 获取文件大小（字节）。
        /// </summary>
        /// <param name="filePath">文件路径。</param>
        public static long GetFileSize(this string filePath) =>
            File.Exists(filePath) ? new FileInfo(filePath).Length : 0;

        /// <summary>
        /// 获取文件创建时间。
        /// </summary>
        /// <param name="filePath">文件路径。</param>
        public static DateTime GetCreationTime(this string filePath) =>
            File.Exists(filePath) ? File.GetCreationTime(filePath) : DateTime.MinValue;

        /// <summary>
        /// 获取文件最后修改时间。
        /// </summary>
        /// <param name="filePath">文件路径。</param>
        public static DateTime GetLastWriteTime(this string filePath) =>
            File.Exists(filePath) ? File.GetLastWriteTime(filePath) : DateTime.MinValue;

        /// <summary>
        /// 获取文件最后访问时间。
        /// </summary>
        /// <param name="filePath">文件路径。</param>
        public static DateTime GetLastAccessTime(this string filePath) =>
            File.Exists(filePath) ? File.GetLastAccessTime(filePath) : DateTime.MinValue;

        /// <summary>
        /// 判断文件是否为只读。
        /// </summary>
        /// <param name="filePath">文件路径。</param>
        public static bool IsReadOnly(this string filePath) =>
            File.Exists(filePath) && new FileInfo(filePath).IsReadOnly;

        /// <summary>
        /// 设置文件为只读。
        /// </summary>
        /// <param name="filePath">文件路径。</param>
        public static void SetReadOnly(this string filePath)
        {
            if (File.Exists(filePath))
                new FileInfo(filePath).IsReadOnly = true;
        }

        /// <summary>
        /// 取消文件只读属性。
        /// </summary>
        /// <param name="filePath">文件路径。</param>
        public static void UnsetReadOnly(this string filePath)
        {
            if (File.Exists(filePath))
                new FileInfo(filePath).IsReadOnly = false;
        }

        /// <summary>
        /// 获取文件的 MD5 值（32位小写）。
        /// </summary>
        /// <param name="filePath">文件路径。</param>
        public static string GetFileMd5(this string filePath)
        {
            if (!File.Exists(filePath)) return string.Empty;
            using var md5 = System.Security.Cryptography.MD5.Create();
            using var stream = File.OpenRead(filePath);
            var hash = md5.ComputeHash(stream);
            return BitConverter.ToString(hash).Replace("-", "").ToLowerInvariant();
        }

        /// <summary>
        /// 获取文件的 SHA256 值（64位小写）。
        /// </summary>
        /// <param name="filePath">文件路径。</param>
        public static string GetFileSha256(this string filePath)
        {
            if (!File.Exists(filePath)) return string.Empty;
            using var sha = System.Security.Cryptography.SHA256.Create();
            using var stream = File.OpenRead(filePath);
            var hash = sha.ComputeHash(stream);
            return BitConverter.ToString(hash).Replace("-", "").ToLowerInvariant();
        }

        /// <summary>
        /// 判断文件是否为隐藏文件。
        /// </summary>
        /// <param name="filePath">文件路径。</param>
        public static bool IsHidden(this string filePath)
        {
            if (!File.Exists(filePath)) return false;
            var attr = File.GetAttributes(filePath);
            return (attr & FileAttributes.Hidden) == FileAttributes.Hidden;
        }

        /// <summary>
        /// 设置文件为隐藏文件。
        /// </summary>
        /// <param name="filePath">文件路径。</param>
        public static void SetHidden(this string filePath)
        {
            if (File.Exists(filePath))
            {
                var attr = File.GetAttributes(filePath);
                File.SetAttributes(filePath, attr | FileAttributes.Hidden);
            }
        }

        /// <summary>
        /// 取消文件隐藏属性。
        /// </summary>
        /// <param name="filePath">文件路径。</param>
        public static void UnsetHidden(this string filePath)
        {
            if (File.Exists(filePath))
            {
                var attr = File.GetAttributes(filePath);
                File.SetAttributes(filePath, attr & ~FileAttributes.Hidden);
            }
        }
        #endregion

        #region Path 扩展方法，提供常用的路径处理、判断、组合、分解等功能。

        /// <summary>
        /// 判断路径是否为绝对路径。
        /// </summary>
        /// <param name="path">待判断的路径。</param>
        public static bool IsAbsolute(this string path) =>
            !string.IsNullOrEmpty(path) && Path.IsPathRooted(path);

        /// <summary>
        /// 判断路径是否为相对路径。
        /// </summary>
        /// <param name="path">待判断的路径。</param>
        public static bool IsRelative(this string path) =>
            !string.IsNullOrEmpty(path) && !Path.IsPathRooted(path);

        /// <summary>
        /// 获取文件扩展名（带点）。
        /// </summary>
        /// <param name="filePath">文件路径。</param>
        public static string GetExtension(this string filePath) =>
            Path.GetExtension(filePath);

        /// <summary>
        /// 获取文件名（不含路径）。
        /// </summary>
        /// <param name="filePath">文件路径。</param>
        public static string GetFileName(this string filePath) =>
            Path.GetFileName(filePath);

        /// <summary>
        /// 获取文件名（不含扩展名）。
        /// </summary>
        /// <param name="filePath">文件路径。</param>
        public static string GetFileNameWithoutExtension(this string filePath) =>
            Path.GetFileNameWithoutExtension(filePath);

        /// <summary>
        /// 获取文件所在目录路径。
        /// </summary>
        /// <param name="filePath">文件路径。</param>
        public static string GetDirectoryName(this string filePath) =>
            Path.GetDirectoryName(filePath);

        /// <summary>
        /// 合并多个路径为一个完整路径。
        /// </summary>
        /// <param name="paths">路径数组。</param>
        public static string CombinePaths(params string[] paths) =>
            Path.Combine(paths);

        /// <summary>
        /// 获取路径的根目录。
        /// </summary>
        /// <param name="path">待处理的路径。</param>
        public static string GetPathRoot(this string path) =>
            string.IsNullOrWhiteSpace(path) ? path : Path.GetPathRoot(path);

        /// <summary>
        /// 获取路径的完整规范路径（绝对路径）。
        /// </summary>
        /// <param name="path">待处理的路径。</param>
        public static string GetFullPath(this string path) =>
            Path.GetFullPath(path);

        /// <summary>
        /// 获取临时文件路径。
        /// </summary>
        public static string GetTempFilePath() => Path.GetTempFileName();

        /// <summary>
        /// 获取临时目录路径。
        /// </summary>
        public static string GetTempDirectory() => Path.GetTempPath();

        /// <summary>
        /// 更改路径的扩展名。
        /// </summary>
        /// <param name="path">原始路径。</param>
        /// <param name="extension">新扩展名（带点）。</param>
        public static string ChangeExtension(this string path, string extension) =>
            Path.ChangeExtension(path, extension);

        /// <summary>
        /// 判断路径是否包含无效字符。
        /// </summary>
        /// <param name="path">待判断的路径。</param>
        public static bool HasInvalidChars(this string path)
        {
            if (string.IsNullOrEmpty(path)) return false;
            var invalidChars = Path.GetInvalidPathChars();
            return path.IndexOfAny(invalidChars) >= 0;
        }

        /// <summary>
        /// 判断文件名是否包含无效字符。
        /// </summary>
        /// <param name="fileName">待判断的文件名。</param>
        public static bool FileNameHasInvalidChars(this string fileName)
        {
            if (string.IsNullOrEmpty(fileName)) return false;
            var invalidChars = Path.GetInvalidFileNameChars();
            return fileName.IndexOfAny(invalidChars) >= 0;
        }

        /// <summary>
        /// 获取路径分隔符。
        /// </summary>
        public static char GetDirectorySeparator() => Path.DirectorySeparatorChar;

        /// <summary>
        /// 获取备用路径分隔符。
        /// </summary>
        public static char GetAltDirectorySeparator() => Path.AltDirectorySeparatorChar;

        /// <summary>
        /// 获取路径分隔符字符串。
        /// </summary>
        public static string GetDirectorySeparatorString() => Path.DirectorySeparatorChar.ToString();

        /// <summary>
        /// 获取路径卷分隔符。
        /// </summary>
        public static char GetVolumeSeparator() => Path.VolumeSeparatorChar;

        /// <summary>
        /// 判断路径是否为 UNC 路径。
        /// </summary>
        /// <param name="path">待判断的路径。</param>
        public static bool IsUncPath(this string path) =>
            !string.IsNullOrEmpty(path) && path.StartsWith(@"\\") && Path.IsPathRooted(path);

        /// <summary>
        /// 获取路径的各级目录（分解为数组）。
        /// </summary>
        /// <param name="path">待处理的路径。</param>
        public static string[] SplitDirectories(this string path)
        {
            if (string.IsNullOrEmpty(path)) return Array.Empty<string>();
            return path.Split(new[] { Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar }, StringSplitOptions.RemoveEmptyEntries);
        }

        /// <summary>
        /// 获取路径的父目录路径。
        /// </summary>
        /// <param name="path">待处理的路径。</param>
        public static string GetParentDirectory(this string path)
        {
            if (string.IsNullOrEmpty(path)) return string.Empty;
            var dir = Path.GetDirectoryName(path);
            return string.IsNullOrEmpty(dir) ? string.Empty : dir;
        }

        /// <summary>
        /// 判断路径是否为根目录。
        /// </summary>
        /// <param name="path">待判断的路径。</param>
        public static bool IsRootDirectory(this string path)
        {
            if (string.IsNullOrEmpty(path)) return false;
            var root = Path.GetPathRoot(path);
            return string.Equals(path.TrimEnd('\\', '/'), root?.TrimEnd('\\', '/'), StringComparison.OrdinalIgnoreCase);
        }

        /// <summary>
        /// 获取路径的规范化形式（去除冗余分隔符等）。
        /// </summary>
        /// <param name="path">待处理的路径。</param>
        public static string NormalizePath(this string path)
        {
            if (string.IsNullOrEmpty(path)) return string.Empty;
            return Path.GetFullPath(new Uri(path, UriKind.RelativeOrAbsolute).LocalPath)
                .TrimEnd(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar);
        }

        /// <summary>
        /// 获取路径的相对路径（相对于 basePath）。
        /// </summary>
        /// <param name="path">目标路径。</param>
        /// <param name="basePath">基准路径。</param>
        public static string GetRelativePath(this string path, string basePath)
        {
            if (string.IsNullOrEmpty(path) || string.IsNullOrEmpty(basePath)) return path;
            return Path.GetRelativePath(basePath, path);
        }

        /// <summary>
        /// 判断路径是否为文件。
        /// </summary>
        /// <param name="path">待判断的路径。</param>
        public static bool IsFile(this string path) =>
            !string.IsNullOrEmpty(path) && File.Exists(path);

        /// <summary>
        /// 判断路径是否为目录。
        /// </summary>
        /// <param name="path">待判断的路径。</param>
        public static bool IsDirectory(this string path) =>
            !string.IsNullOrEmpty(path) && Directory.Exists(path);


        /// <summary>
        /// 获取路径的文件或目录是否存在。
        /// </summary>
        /// <param name="path">待判断的路径。</param>
        public static bool ExistsPath(this string path) =>
            !string.IsNullOrEmpty(path) && (File.Exists(path) || Directory.Exists(path));

        #endregion

        #region FileInfo 扩展方法，提供常用的判断、信息获取、操作等功能。
        /// <summary>
        /// 判断文件是否存在。
        /// </summary>
        /// <param name="file">FileInfo 实例。</param>
        public static bool ExistsSafe(this FileInfo file) => file != null && file.Exists;

        /// <summary>
        /// 获取文件大小（字节）。
        /// </summary>
        /// <param name="file">FileInfo 实例。</param>
        public static long GetSize(this FileInfo file) => file?.Exists == true ? file.Length : 0;

        /// <summary>
        /// 获取文件扩展名（带点）。
        /// </summary>
        /// <param name="file">FileInfo 实例。</param>
        public static string GetExtension(this FileInfo file) => file?.Extension ?? string.Empty;

        /// <summary>
        /// 获取文件名（不含路径）。
        /// </summary>
        /// <param name="file">FileInfo 实例。</param>
        public static string GetFileName(this FileInfo file) => file?.Name ?? string.Empty;

        /// <summary>
        /// 获取文件所在目录路径。
        /// </summary>
        /// <param name="file">FileInfo 实例。</param>
        public static string GetDirectoryName(this FileInfo file) => file?.DirectoryName ?? string.Empty;

        /// <summary>
        /// 获取文件创建时间。
        /// </summary>
        /// <param name="file">FileInfo 实例。</param>
        public static DateTime GetCreationTime(this FileInfo file) => file?.Exists == true ? file.CreationTime : DateTime.MinValue;

        /// <summary>
        /// 获取文件最后修改时间。
        /// </summary>
        /// <param name="file">FileInfo 实例。</param>
        public static DateTime GetLastWriteTime(this FileInfo file) => file?.Exists == true ? file.LastWriteTime : DateTime.MinValue;

        /// <summary>
        /// 获取文件最后访问时间。
        /// </summary>
        /// <param name="file">FileInfo 实例。</param>
        public static DateTime GetLastAccessTime(this FileInfo file) => file?.Exists == true ? file.LastAccessTime : DateTime.MinValue;

        /// <summary>
        /// 判断文件是否为只读。
        /// </summary>
        /// <param name="file">FileInfo 实例。</param>
        public static bool IsReadOnly(this FileInfo file) => file?.Exists == true && file.IsReadOnly;

        /// <summary>
        /// 设置文件为只读。
        /// </summary>
        /// <param name="file">FileInfo 实例。</param>
        public static void SetReadOnly(this FileInfo file)
        {
            if (file?.Exists == true)
                file.IsReadOnly = true;
        }

        /// <summary>
        /// 取消文件只读属性。
        /// </summary>
        /// <param name="file">FileInfo 实例。</param>
        public static void UnsetReadOnly(this FileInfo file)
        {
            if (file?.Exists == true)
                file.IsReadOnly = false;
        }

        /// <summary>
        /// 删除文件。
        /// </summary>
        /// <param name="file">FileInfo 实例。</param>
        public static void DeleteFile(this FileInfo file)
        {
            if (file?.Exists == true)
                file.Delete();
        }

        /// <summary>
        /// 复制文件到目标路径（可覆盖）。
        /// </summary>
        /// <param name="file">FileInfo 实例。</param>
        /// <param name="destPath">目标文件路径。</param>
        /// <param name="overwrite">是否覆盖目标文件。</param>
        public static void CopyTo(this FileInfo file, string destPath, bool overwrite = true)
        {
            if (file?.Exists == true)
                file.CopyTo(destPath, overwrite);
        }

        /// <summary>
        /// 移动文件到目标路径（可覆盖）。
        /// </summary>
        /// <param name="file">FileInfo 实例。</param>
        /// <param name="destPath">目标文件路径。</param>
        /// <param name="overwrite">是否覆盖目标文件。</param>
        public static void MoveTo(this FileInfo file, string destPath, bool overwrite = true)
        {
            if (file?.Exists != true) return;
            if (overwrite && File.Exists(destPath))
                File.Delete(destPath);
            file.MoveTo(destPath);
        }

        /// <summary>
        /// 获取文件的 MD5 值（32位小写）。
        /// </summary>
        /// <param name="file">FileInfo 实例。</param>
        public static string GetMd5(this FileInfo file)
        {
            if (file?.Exists != true) return string.Empty;
            using var md5 = System.Security.Cryptography.MD5.Create();
            using var stream = file.OpenRead();
            var hash = md5.ComputeHash(stream);
            return BitConverter.ToString(hash).Replace("-", "").ToLowerInvariant();
        }

        /// <summary>
        /// 获取文件的 SHA256 值（64位小写）。
        /// </summary>
        /// <param name="file">FileInfo 实例。</param>
        public static string GetSha256(this FileInfo file)
        {
            if (file?.Exists != true) return string.Empty;
            using var sha = System.Security.Cryptography.SHA256.Create();
            using var stream = file.OpenRead();
            var hash = sha.ComputeHash(stream);
            return BitConverter.ToString(hash).Replace("-", "").ToLowerInvariant();
        }

        /// <summary>
        /// 判断文件是否为隐藏文件。
        /// </summary>
        /// <param name="file">FileInfo 实例。</param>
        public static bool IsHidden(this FileInfo file)
        {
            if (file?.Exists != true) return false;
            var attr = file.Attributes;
            return (attr & FileAttributes.Hidden) == FileAttributes.Hidden;
        }

        /// <summary>
        /// 设置文件为隐藏文件。
        /// </summary>
        /// <param name="file">FileInfo 实例。</param>
        public static void SetHidden(this FileInfo file)
        {
            if (file?.Exists == true)
                file.Attributes |= FileAttributes.Hidden;
        }

        /// <summary>
        /// 取消文件隐藏属性。
        /// </summary>
        /// <param name="file">FileInfo 实例。</param>
        public static void UnsetHidden(this FileInfo file)
        {
            if (file?.Exists == true)
                file.Attributes &= ~FileAttributes.Hidden;
        }
        #endregion

        #region DirectoryInfo 扩展方法，提供常用的判断、信息获取、操作等功能。
        /// <summary>
        /// 判断目录是否存在。
        /// </summary>
        /// <param name="dir">DirectoryInfo 实例。</param>
        public static bool ExistsSafe(this DirectoryInfo dir) => dir != null && dir.Exists;

        /// <summary>
        /// 获取目录名。
        /// </summary>
        /// <param name="dir">DirectoryInfo 实例。</param>
        public static string GetDirectoryName(this DirectoryInfo dir) => dir?.Name ?? string.Empty;

        /// <summary>
        /// 获取目录完整路径。
        /// </summary>
        /// <param name="dir">DirectoryInfo 实例。</param>
        public static string GetFullPath(this DirectoryInfo dir) => dir?.FullName ?? string.Empty;

        /// <summary>
        /// 获取目录创建时间。
        /// </summary>
        /// <param name="dir">DirectoryInfo 实例。</param>
        public static DateTime GetCreationTime(this DirectoryInfo dir) => dir?.Exists == true ? dir.CreationTime : DateTime.MinValue;

        /// <summary>
        /// 获取目录最后修改时间。
        /// </summary>
        /// <param name="dir">DirectoryInfo 实例。</param>
        public static DateTime GetLastWriteTime(this DirectoryInfo dir) => dir?.Exists == true ? dir.LastWriteTime : DateTime.MinValue;

        /// <summary>
        /// 获取目录最后访问时间。
        /// </summary>
        /// <param name="dir">DirectoryInfo 实例。</param>
        public static DateTime GetLastAccessTime(this DirectoryInfo dir) => dir?.Exists == true ? dir.LastAccessTime : DateTime.MinValue;

        /// <summary>
        /// 获取目录下所有文件（可递归）。
        /// </summary>
        /// <param name="dir">DirectoryInfo 实例。</param>
        /// <param name="searchPattern">搜索模式，默认 "*"。</param>
        /// <param name="searchOption">搜索选项，默认 TopDirectoryOnly。</param>
        public static FileInfo[] GetFilesSafe(this DirectoryInfo dir, string searchPattern = "*", SearchOption searchOption = SearchOption.TopDirectoryOnly) =>
            dir?.Exists == true ? dir.GetFiles(searchPattern, searchOption) : Array.Empty<FileInfo>();

        /// <summary>
        /// 获取目录下所有子目录（可递归）。
        /// </summary>
        /// <param name="dir">DirectoryInfo 实例。</param>
        /// <param name="searchPattern">搜索模式，默认 "*"。</param>
        /// <param name="searchOption">搜索选项，默认 TopDirectoryOnly。</param>
        public static DirectoryInfo[] GetDirectoriesSafe(this DirectoryInfo dir, string searchPattern = "*", SearchOption searchOption = SearchOption.TopDirectoryOnly) =>
            dir?.Exists == true ? dir.GetDirectories(searchPattern, searchOption) : Array.Empty<DirectoryInfo>();

        /// <summary>
        /// 创建目录（如果不存在）。
        /// </summary>
        /// <param name="dir">DirectoryInfo 实例。</param>
        public static void CreateSafe(this DirectoryInfo dir)
        {
            if (dir == null) return;
            if (!dir.Exists) dir.Create();
        }

        /// <summary>
        /// 删除目录（可递归）。
        /// </summary>
        /// <param name="dir">DirectoryInfo 实例。</param>
        /// <param name="recursive">是否递归删除。</param>
        public static void DeleteSafe(this DirectoryInfo dir, bool recursive = false)
        {
            if (dir?.Exists == true)
                dir.Delete(recursive);
        }

        /// <summary>
        /// 移动目录到目标路径。
        /// </summary>
        /// <param name="dir">DirectoryInfo 实例。</param>
        /// <param name="destPath">目标目录路径。</param>
        public static void MoveTo(this DirectoryInfo dir, string destPath)
        {
            if (dir?.Exists == true)
                dir.MoveTo(destPath);
        }

        /// <summary>
        /// 获取目录下所有文件路径（可递归）。
        /// </summary>
        /// <param name="dir">DirectoryInfo 实例。</param>
        /// <param name="searchPattern">搜索模式，默认 "*"。</param>
        /// <param name="searchOption">搜索选项，默认 TopDirectoryOnly。</param>
        public static List<string> GetFilePaths(this DirectoryInfo dir, string searchPattern = "*", SearchOption searchOption = SearchOption.TopDirectoryOnly)
        {
            if (dir?.Exists != true) return new List<string>();
            var files = dir.GetFiles(searchPattern, searchOption);
            var list = new List<string>();
            foreach (var file in files)
                list.Add(file.FullName);
            return list;
        }

        /// <summary>
        /// 获取目录下所有子目录路径（可递归）。
        /// </summary>
        /// <param name="dir">DirectoryInfo 实例。</param>
        /// <param name="searchPattern">搜索模式，默认 "*"。</param>
        /// <param name="searchOption">搜索选项，默认 TopDirectoryOnly。</param>
        public static List<string> GetDirectoryPaths(this DirectoryInfo dir, string searchPattern = "*", SearchOption searchOption = SearchOption.TopDirectoryOnly)
        {
            if (dir?.Exists != true) return new List<string>();
            var dirs = dir.GetDirectories(searchPattern, searchOption);
            var list = new List<string>();
            foreach (var d in dirs)
                list.Add(d.FullName);
            return list;
        }

        /// <summary>
        /// 判断目录是否为隐藏目录。
        /// </summary>
        /// <param name="dir">DirectoryInfo 实例。</param>
        public static bool IsHidden(this DirectoryInfo dir)
        {
            if (dir?.Exists != true) return false;
            var attr = dir.Attributes;
            return (attr & FileAttributes.Hidden) == FileAttributes.Hidden;
        }

        /// <summary>
        /// 设置目录为隐藏目录。
        /// </summary>
        /// <param name="dir">DirectoryInfo 实例。</param>
        public static void SetHidden(this DirectoryInfo dir)
        {
            if (dir?.Exists == true)
                dir.Attributes |= FileAttributes.Hidden;
        }

        /// <summary>
        /// 取消目录隐藏属性。
        /// </summary>
        /// <param name="dir">DirectoryInfo 实例。</param>
        public static void UnsetHidden(this DirectoryInfo dir)
        {
            if (dir?.Exists == true)
                dir.Attributes &= ~FileAttributes.Hidden;
        }
        #endregion
    }

}