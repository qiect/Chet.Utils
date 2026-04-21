using System.Diagnostics;
using System.IO.Compression;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;

namespace Chet.Utils.Helpers;

/// <summary>
/// 文件操作帮助类，提供 <see cref="FileExtensions"/> 中未包含的高级文件操作功能。
/// </summary>
/// <remarks>
/// <para>本类提供以下 FileExtensions 中未包含的功能：</para>
/// <list type="bullet">
///   <item><description>压缩解压：Zip 压缩/解压、GZip 压缩/解压</description></item>
///   <item><description>安全写入：SafeWriteTextFile（原子写入）</description></item>
///   <item><description>流式读取：ReadFileInChunks（大文件分块读取）</description></item>
///   <item><description>目录操作：ClearDirectory（清空目录）</description></item>
///   <item><description>校验计算：GetFileCrc32、VerifyFileHash</description></item>
///   <item><description>INI 操作：ReadIniValue、WriteIniValue</description></item>
///   <item><description>文件比较：CompareFiles、CompareFileContent</description></item>
///   <item><description>版本信息：GetFileVersionInfo</description></item>
///   <item><description>临时文件：CreateTempFile、CreateTempDirectory</description></item>
///   <item><description>文件监控：WatchFileChanges</description></item>
///   <item><description>文件锁定：IsFileLocked、WaitForFileUnlock</description></item>
///   <item><description>文件搜索：SearchFiles（延迟执行）</description></item>
/// </list>
/// <para>基础功能请使用 <see cref="FileExtensions"/> 扩展方法。</para>
/// </remarks>
public static class FileHelper
{
    #region 压缩与解压

    /// <summary>
    /// 将文件压缩为 ZIP 文件。
    /// </summary>
    /// <param name="sourceFilePath">源文件路径。</param>
    /// <param name="zipFilePath">ZIP 文件路径。</param>
    /// <param name="compressionLevel">压缩级别，默认为 Optimal。</param>
    /// <param name="overwrite">是否覆盖已存在的 ZIP 文件。</param>
    /// <exception cref="FileNotFoundException">源文件不存在时抛出。</exception>
    /// <example>
    /// <code>
    /// FileHelper.ZipFile("C:\\test.txt", "C:\\test.zip");
    /// </code>
    /// </example>
    public static void ZipFile(string sourceFilePath, string zipFilePath, CompressionLevel compressionLevel = CompressionLevel.Optimal, bool overwrite = true)
    {
        if (!File.Exists(sourceFilePath))
            throw new FileNotFoundException($"源文件未找到: {sourceFilePath}", sourceFilePath);

        var directory = Path.GetDirectoryName(zipFilePath);
        if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
            Directory.CreateDirectory(directory);

        if (overwrite && File.Exists(zipFilePath))
            File.Delete(zipFilePath);

        using var archive = System.IO.Compression.ZipFile.Open(zipFilePath, ZipArchiveMode.Create);
        archive.CreateEntryFromFile(sourceFilePath, Path.GetFileName(sourceFilePath), compressionLevel);
    }

    /// <summary>
    /// 将目录压缩为 ZIP 文件。
    /// </summary>
    /// <param name="sourceDirectoryPath">源目录路径。</param>
    /// <param name="zipFilePath">ZIP 文件路径。</param>
    /// <param name="compressionLevel">压缩级别，默认为 Optimal。</param>
    /// <param name="includeBaseDirectory">是否包含基础目录。</param>
    /// <param name="overwrite">是否覆盖已存在的 ZIP 文件。</param>
    /// <exception cref="DirectoryNotFoundException">源目录不存在时抛出。</exception>
    /// <example>
    /// <code>
    /// FileHelper.ZipDirectory("C:\\MyFolder", "C:\\MyFolder.zip");
    /// </code>
    /// </example>
    public static void ZipDirectory(string sourceDirectoryPath, string zipFilePath, CompressionLevel compressionLevel = CompressionLevel.Optimal, bool includeBaseDirectory = false, bool overwrite = true)
    {
        if (!Directory.Exists(sourceDirectoryPath))
            throw new DirectoryNotFoundException($"源目录未找到: {sourceDirectoryPath}");

        var directory = Path.GetDirectoryName(zipFilePath);
        if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
            Directory.CreateDirectory(directory);

        if (overwrite && File.Exists(zipFilePath))
            File.Delete(zipFilePath);

        System.IO.Compression.ZipFile.CreateFromDirectory(sourceDirectoryPath, zipFilePath, compressionLevel, includeBaseDirectory);
    }

    /// <summary>
    /// 解压 ZIP 文件到指定目录。
    /// </summary>
    /// <param name="zipFilePath">ZIP 文件路径。</param>
    /// <param name="destDirectoryPath">目标目录路径。</param>
    /// <param name="overwrite">是否覆盖已存在的文件。</param>
    /// <exception cref="FileNotFoundException">ZIP 文件不存在时抛出。</exception>
    /// <example>
    /// <code>
    /// FileHelper.UnzipFile("C:\\test.zip", "C:\\Extracted");
    /// </code>
    /// </example>
    public static void UnzipFile(string zipFilePath, string destDirectoryPath, bool overwrite = true)
    {
        if (!File.Exists(zipFilePath))
            throw new FileNotFoundException($"ZIP 文件未找到: {zipFilePath}", zipFilePath);

        if (!Directory.Exists(destDirectoryPath))
            Directory.CreateDirectory(destDirectoryPath);

        System.IO.Compression.ZipFile.ExtractToDirectory(zipFilePath, destDirectoryPath, overwrite);
    }

    /// <summary>
    /// 使用 GZip 压缩文件。
    /// </summary>
    /// <param name="sourceFilePath">源文件路径。</param>
    /// <param name="destFilePath">目标文件路径（通常以 .gz 结尾）。</param>
    /// <param name="overwrite">是否覆盖已存在的目标文件。</param>
    /// <exception cref="FileNotFoundException">源文件不存在时抛出。</exception>
    /// <example>
    /// <code>
    /// FileHelper.GZipCompress("C:\\test.txt", "C:\\test.txt.gz");
    /// </code>
    /// </example>
    public static void GZipCompress(string sourceFilePath, string destFilePath, bool overwrite = true)
    {
        if (!File.Exists(sourceFilePath))
            throw new FileNotFoundException($"源文件未找到: {sourceFilePath}", sourceFilePath);

        var directory = Path.GetDirectoryName(destFilePath);
        if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
            Directory.CreateDirectory(directory);

        if (overwrite && File.Exists(destFilePath))
            File.Delete(destFilePath);

        using var sourceStream = File.OpenRead(sourceFilePath);
        using var destStream = File.Create(destFilePath);
        using var gzipStream = new GZipStream(destStream, CompressionMode.Compress);
        sourceStream.CopyTo(gzipStream);
    }

    /// <summary>
    /// 使用 GZip 解压文件。
    /// </summary>
    /// <param name="sourceFilePath">源文件路径（.gz 文件）。</param>
    /// <param name="destFilePath">目标文件路径。</param>
    /// <param name="overwrite">是否覆盖已存在的目标文件。</param>
    /// <exception cref="FileNotFoundException">源文件不存在时抛出。</exception>
    /// <example>
    /// <code>
    /// FileHelper.GZipDecompress("C:\\test.txt.gz", "C:\\test.txt");
    /// </code>
    /// </example>
    public static void GZipDecompress(string sourceFilePath, string destFilePath, bool overwrite = true)
    {
        if (!File.Exists(sourceFilePath))
            throw new FileNotFoundException($"源文件未找到: {sourceFilePath}", sourceFilePath);

        var directory = Path.GetDirectoryName(destFilePath);
        if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
            Directory.CreateDirectory(directory);

        if (overwrite && File.Exists(destFilePath))
            File.Delete(destFilePath);

        using var sourceStream = File.OpenRead(sourceFilePath);
        using var destStream = File.Create(destFilePath);
        using var gzipStream = new GZipStream(sourceStream, CompressionMode.Decompress);
        gzipStream.CopyTo(destStream);
    }

    #endregion

    #region 安全写入

    /// <summary>
    /// 安全写入文本文件（原子写入：先写入临时文件，成功后替换原文件）。
    /// </summary>
    /// <param name="filePath">文件路径。</param>
    /// <param name="content">文件内容。</param>
    /// <param name="encoding">编码格式，默认为 UTF-8。</param>
    /// <remarks>
    /// <para>此方法确保写入操作的原子性，避免写入过程中断导致文件损坏。</para>
    /// <para>写入流程：创建临时文件 -> 写入内容 -> 替换原文件。</para>
    /// </remarks>
    /// <example>
    /// <code>
    /// FileHelper.SafeWriteTextFile("C:\\config.json", jsonContent);
    /// </code>
    /// </example>
    public static void SafeWriteTextFile(string filePath, string content, Encoding encoding = null)
    {
        encoding ??= Encoding.UTF8;
        var tempFilePath = filePath + ".tmp." + Guid.NewGuid().ToString("N");

        try
        {
            var directory = Path.GetDirectoryName(filePath);
            if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
                Directory.CreateDirectory(directory);

            File.WriteAllText(tempFilePath, content ?? string.Empty, encoding);
            File.Replace(tempFilePath, filePath, null);
        }
        catch
        {
            if (File.Exists(tempFilePath))
                File.Delete(tempFilePath);
            throw;
        }
    }

    /// <summary>
    /// 异步安全写入文本文件（原子写入）。
    /// </summary>
    /// <param name="filePath">文件路径。</param>
    /// <param name="content">文件内容。</param>
    /// <param name="encoding">编码格式，默认为 UTF-8。</param>
    /// <param name="cancellationToken">取消令牌。</param>
    /// <returns>异步任务。</returns>
    public static async Task SafeWriteTextFileAsync(string filePath, string content, Encoding encoding = null, CancellationToken cancellationToken = default)
    {
        encoding ??= Encoding.UTF8;
        var tempFilePath = filePath + ".tmp." + Guid.NewGuid().ToString("N");

        try
        {
            var directory = Path.GetDirectoryName(filePath);
            if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
                Directory.CreateDirectory(directory);

            await File.WriteAllTextAsync(tempFilePath, content ?? string.Empty, encoding, cancellationToken);
            File.Replace(tempFilePath, filePath, null);
        }
        catch
        {
            if (File.Exists(tempFilePath))
                File.Delete(tempFilePath);
            throw;
        }
    }

    /// <summary>
    /// 安全写入二进制文件（原子写入）。
    /// </summary>
    /// <param name="filePath">文件路径。</param>
    /// <param name="bytes">字节数组。</param>
    /// <example>
    /// <code>
    /// FileHelper.SafeWriteBinaryFile("C:\\data.bin", bytes);
    /// </code>
    /// </example>
    public static void SafeWriteBinaryFile(string filePath, byte[] bytes)
    {
        var tempFilePath = filePath + ".tmp." + Guid.NewGuid().ToString("N");

        try
        {
            var directory = Path.GetDirectoryName(filePath);
            if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
                Directory.CreateDirectory(directory);

            File.WriteAllBytes(tempFilePath, bytes ?? Array.Empty<byte>());
            File.Replace(tempFilePath, filePath, null);
        }
        catch
        {
            if (File.Exists(tempFilePath))
                File.Delete(tempFilePath);
            throw;
        }
    }

    #endregion

    #region 流式读取

    /// <summary>
    /// 流式读取大文件（分块读取，延迟执行）。
    /// </summary>
    /// <param name="filePath">文件路径。</param>
    /// <param name="bufferSize">缓冲区大小（字节），默认为 8192。</param>
    /// <returns>文件数据块枚举。</returns>
    /// <exception cref="FileNotFoundException">文件不存在时抛出。</exception>
    /// <remarks>
    /// <para>此方法适用于读取大文件，避免一次性加载到内存。</para>
    /// <para>使用 foreach 遍历返回的枚举来逐块处理文件内容。</para>
    /// </remarks>
    /// <example>
    /// <code>
    /// foreach (var chunk in FileHelper.ReadFileInChunks("C:\\largefile.bin", 4096))
    /// {
    ///     ProcessChunk(chunk);
    /// }
    /// </code>
    /// </example>
    public static IEnumerable<byte[]> ReadFileInChunks(string filePath, int bufferSize = 8192)
    {
        if (!File.Exists(filePath))
            throw new FileNotFoundException($"文件未找到: {filePath}", filePath);

        using var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read);
        var buffer = new byte[bufferSize];
        int bytesRead;

        while ((bytesRead = fileStream.Read(buffer, 0, buffer.Length)) > 0)
        {
            var chunk = new byte[bytesRead];
            Array.Copy(buffer, chunk, bytesRead);
            yield return chunk;
        }
    }

    /// <summary>
    /// 异步流式读取大文件（分块读取）。
    /// </summary>
    /// <param name="filePath">文件路径。</param>
    /// <param name="bufferSize">缓冲区大小（字节），默认为 8192。</param>
    /// <param name="cancellationToken">取消令牌。</param>
    /// <returns>文件数据块异步枚举。</returns>
    public static async IAsyncEnumerable<byte[]> ReadFileInChunksAsync(string filePath, int bufferSize = 8192, [System.Runtime.CompilerServices.EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        if (!File.Exists(filePath))
            throw new FileNotFoundException($"文件未找到: {filePath}", filePath);

        using var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read);
        var buffer = new byte[bufferSize];
        int bytesRead;

        while ((bytesRead = await fileStream.ReadAsync(buffer, cancellationToken)) > 0)
        {
            var chunk = new byte[bytesRead];
            Array.Copy(buffer, chunk, bytesRead);
            yield return chunk;
        }
    }

    /// <summary>
    /// 逐行读取大文件（延迟执行，适用于大文本文件）。
    /// </summary>
    /// <param name="filePath">文件路径。</param>
    /// <param name="encoding">编码格式，默认为 UTF-8。</param>
    /// <returns>文件行枚举。</returns>
    /// <exception cref="FileNotFoundException">文件不存在时抛出。</exception>
    /// <example>
    /// <code>
    /// foreach (var line in FileHelper.ReadLargeTextFile("C:\\largefile.log"))
    /// {
    ///     ProcessLine(line);
    /// }
    /// </code>
    /// </example>
    public static IEnumerable<string> ReadLargeTextFile(string filePath, Encoding encoding = null)
    {
        if (!File.Exists(filePath))
            throw new FileNotFoundException($"文件未找到: {filePath}", filePath);

        encoding ??= Encoding.UTF8;
        using var reader = new StreamReader(filePath, encoding);
        while (!reader.EndOfStream)
        {
            yield return reader.ReadLine();
        }
    }

    /// <summary>
    /// 异步逐行读取大文件。
    /// </summary>
    /// <param name="filePath">文件路径。</param>
    /// <param name="encoding">编码格式，默认为 UTF-8。</param>
    /// <param name="cancellationToken">取消令牌。</param>
    /// <returns>文件行异步枚举。</returns>
    public static async IAsyncEnumerable<string> ReadLargeTextFileAsync(string filePath, Encoding encoding = null, [System.Runtime.CompilerServices.EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        if (!File.Exists(filePath))
            throw new FileNotFoundException($"文件未找到: {filePath}", filePath);

        encoding ??= Encoding.UTF8;
        using var reader = new StreamReader(filePath, encoding);
        while (!reader.EndOfStream)
        {
            cancellationToken.ThrowIfCancellationRequested();
            yield return await reader.ReadLineAsync();
        }
    }

    #endregion

    #region 目录操作

    /// <summary>
    /// 清空目录内容（保留目录本身）。
    /// </summary>
    /// <param name="directoryPath">目录路径。</param>
    /// <param name="deleteSubdirectories">是否删除子目录，默认为 true。</param>
    /// <exception cref="DirectoryNotFoundException">目录不存在时抛出。</exception>
    /// <example>
    /// <code>
    /// FileHelper.ClearDirectory("C:\\Temp");
    /// </code>
    /// </example>
    public static void ClearDirectory(string directoryPath, bool deleteSubdirectories = true)
    {
        if (!Directory.Exists(directoryPath))
            throw new DirectoryNotFoundException($"目录未找到: {directoryPath}");

        foreach (var filePath in Directory.GetFiles(directoryPath))
        {
            File.Delete(filePath);
        }

        if (deleteSubdirectories)
        {
            foreach (var subDirectoryPath in Directory.GetDirectories(directoryPath))
            {
                Directory.Delete(subDirectoryPath, true);
            }
        }
    }

    /// <summary>
    /// 确保目录存在（如果不存在则创建）。
    /// </summary>
    /// <param name="directoryPath">目录路径。</param>
    /// <returns>目录信息对象。</returns>
    /// <example>
    /// <code>
    /// FileHelper.EnsureDirectoryExists("C:\\MyApp\\Data");
    /// </code>
    /// </example>
    public static DirectoryInfo EnsureDirectoryExists(string directoryPath)
    {
        if (string.IsNullOrEmpty(directoryPath))
            return null;

        return Directory.CreateDirectory(directoryPath);
    }

    #endregion

    #region 校验计算

    /// <summary>
    /// 获取文件的 CRC32 校验和。
    /// </summary>
    /// <param name="filePath">文件路径。</param>
    /// <returns>CRC32 校验和（32位无符号整数）。</returns>
    /// <exception cref="FileNotFoundException">文件不存在时抛出。</exception>
    /// <example>
    /// <code>
    /// uint crc = FileHelper.GetFileCrc32("C:\\test.bin");
    /// Console.WriteLine($"CRC32: {crc:X8}");
    /// </code>
    /// </example>
    public static uint GetFileCrc32(string filePath)
    {
        if (!File.Exists(filePath))
            throw new FileNotFoundException($"文件未找到: {filePath}", filePath);

        using var stream = File.OpenRead(filePath);
        return CalculateCrc32(stream);
    }

    /// <summary>
    /// 验证文件哈希值是否匹配。
    /// </summary>
    /// <param name="filePath">文件路径。</param>
    /// <param name="expectedHash">期望的哈希值（小写十六进制字符串）。</param>
    /// <param name="hashAlgorithm">哈希算法，默认为 MD5。</param>
    /// <returns>如果哈希值匹配返回 true；否则返回 false。</returns>
    /// <example>
    /// <code>
    /// bool isValid = FileHelper.VerifyFileHash("C:\\test.bin", "d41d8cd98f00b204e9800998ecf8427e");
    /// </code>
    /// </example>
    public static bool VerifyFileHash(string filePath, string expectedHash, string hashAlgorithm = "MD5")
    {
        if (!File.Exists(filePath) || string.IsNullOrEmpty(expectedHash))
            return false;

        using var stream = File.OpenRead(filePath);
        using HashAlgorithm algorithm = hashAlgorithm.ToUpperInvariant() switch
        {
            "MD5" => MD5.Create(),
            "SHA1" => SHA1.Create(),
            "SHA256" => SHA256.Create(),
            "SHA384" => SHA384.Create(),
            "SHA512" => SHA512.Create(),
            _ => throw new ArgumentException($"不支持的哈希算法: {hashAlgorithm}", nameof(hashAlgorithm))
        };

        var hashBytes = algorithm.ComputeHash(stream);
        var hashString = BitConverter.ToString(hashBytes).Replace("-", "").ToLowerInvariant();
        return string.Equals(hashString, expectedHash.ToLowerInvariant(), StringComparison.OrdinalIgnoreCase);
    }

    /// <summary>
    /// 计算流的 CRC32 校验和。
    /// </summary>
    private static uint CalculateCrc32(Stream stream)
    {
        uint crc = 0xFFFFFFFF;
        var buffer = new byte[8192];
        int bytesRead;

        while ((bytesRead = stream.Read(buffer, 0, buffer.Length)) > 0)
        {
            for (int i = 0; i < bytesRead; i++)
            {
                crc ^= buffer[i];
                for (int j = 0; j < 8; j++)
                {
                    crc = (crc >> 1) ^ (crc & 1) * 0xEDB88320;
                }
            }
        }

        return ~crc;
    }

    #endregion

    #region INI 文件操作

    /// <summary>
    /// 读取 INI 文件中的值。
    /// </summary>
    /// <param name="filePath">INI 文件路径。</param>
    /// <param name="section">节名称。</param>
    /// <param name="key">键名称。</param>
    /// <param name="defaultValue">默认值。</param>
    /// <returns>读取到的值，如果不存在则返回默认值。</returns>
    /// <remarks>
    /// <para>此方法使用 Windows API 读取 INI 文件，仅支持 Windows 平台。</para>
    /// <para>在非 Windows 平台上，将使用纯 C# 实现解析。</para>
    /// </remarks>
    /// <example>
    /// <code>
    /// var server = FileHelper.ReadIniValue("C:\\config.ini", "Database", "Server", "localhost");
    /// </code>
    /// </example>
    public static string ReadIniValue(string filePath, string section, string key, string defaultValue = "")
    {
        if (!File.Exists(filePath))
            return defaultValue;

        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        {
            var sb = new StringBuilder(255);
            GetPrivateProfileString(section, key, defaultValue, sb, 255, filePath);
            return sb.ToString();
        }
        else
        {
            return ReadIniValueManaged(filePath, section, key, defaultValue);
        }
    }

    /// <summary>
    /// 写入值到 INI 文件。
    /// </summary>
    /// <param name="filePath">INI 文件路径。</param>
    /// <param name="section">节名称。</param>
    /// <param name="key">键名称。</param>
    /// <param name="value">要写入的值。</param>
    /// <remarks>
    /// <para>此方法使用 Windows API 写入 INI 文件，仅支持 Windows 平台。</para>
    /// <para>在非 Windows 平台上，将使用纯 C# 实现写入。</para>
    /// </remarks>
    /// <example>
    /// <code>
    /// FileHelper.WriteIniValue("C:\\config.ini", "Database", "Server", "192.168.1.100");
    /// </code>
    /// </example>
    public static void WriteIniValue(string filePath, string section, string key, string value)
    {
        var directory = Path.GetDirectoryName(filePath);
        if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
            Directory.CreateDirectory(directory);

        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        {
            WritePrivateProfileString(section, key, value, filePath);
        }
        else
        {
            WriteIniValueManaged(filePath, section, key, value);
        }
    }

    [DllImport("kernel32", CharSet = CharSet.Unicode, SetLastError = true)]
    private static extern int GetPrivateProfileString(string section, string key, string defaultValue, StringBuilder retVal, int size, string filePath);

    [DllImport("kernel32", CharSet = CharSet.Unicode, SetLastError = true)]
    private static extern int WritePrivateProfileString(string section, string key, string value, string filePath);

    /// <summary>
    /// 托管方式读取 INI 值（跨平台）。
    /// </summary>
    private static string ReadIniValueManaged(string filePath, string section, string key, string defaultValue)
    {
        if (!File.Exists(filePath))
            return defaultValue;

        var lines = File.ReadAllLines(filePath);
        var currentSection = string.Empty;

        foreach (var line in lines)
        {
            var trimmedLine = line.Trim();
            if (trimmedLine.StartsWith("[") && trimmedLine.EndsWith("]"))
            {
                currentSection = trimmedLine[1..^1].Trim();
            }
            else if (string.Equals(currentSection, section, StringComparison.OrdinalIgnoreCase))
            {
                var separatorIndex = trimmedLine.IndexOf('=');
                if (separatorIndex > 0)
                {
                    var currentKey = trimmedLine[..separatorIndex].Trim();
                    if (string.Equals(currentKey, key, StringComparison.OrdinalIgnoreCase))
                    {
                        return trimmedLine[(separatorIndex + 1)..].Trim();
                    }
                }
            }
        }

        return defaultValue;
    }

    /// <summary>
    /// 托管方式写入 INI 值（跨平台）。
    /// </summary>
    private static void WriteIniValueManaged(string filePath, string section, string key, string value)
    {
        var lines = File.Exists(filePath) ? File.ReadAllLines(filePath).ToList() : new List<string>();
        var currentSection = string.Empty;
        var sectionIndex = -1;
        var keyIndex = -1;

        for (int i = 0; i < lines.Count; i++)
        {
            var trimmedLine = lines[i].Trim();
            if (trimmedLine.StartsWith("[") && trimmedLine.EndsWith("]"))
            {
                currentSection = trimmedLine[1..^1].Trim();
                if (string.Equals(currentSection, section, StringComparison.OrdinalIgnoreCase) && sectionIndex < 0)
                {
                    sectionIndex = i;
                }
            }
            else if (string.Equals(currentSection, section, StringComparison.OrdinalIgnoreCase))
            {
                var separatorIndex = trimmedLine.IndexOf('=');
                if (separatorIndex > 0)
                {
                    var currentKey = trimmedLine[..separatorIndex].Trim();
                    if (string.Equals(currentKey, key, StringComparison.OrdinalIgnoreCase))
                    {
                        keyIndex = i;
                        break;
                    }
                }
            }
        }

        if (keyIndex >= 0)
        {
            lines[keyIndex] = $"{key}={value}";
        }
        else if (sectionIndex >= 0)
        {
            lines.Insert(sectionIndex + 1, $"{key}={value}");
        }
        else
        {
            lines.Add(string.Empty);
            lines.Add($"[{section}]");
            lines.Add($"{key}={value}");
        }

        File.WriteAllLines(filePath, lines);
    }

    #endregion

    #region 文件比较

    /// <summary>
    /// 比较两个文件是否相同（通过哈希值比较）。
    /// </summary>
    /// <param name="filePath1">第一个文件路径。</param>
    /// <param name="filePath2">第二个文件路径。</param>
    /// <returns>如果两个文件内容相同返回 true；否则返回 false。</returns>
    /// <example>
    /// <code>
    /// bool same = FileHelper.CompareFiles("C:\\file1.txt", "C:\\file2.txt");
    /// </code>
    /// </example>
    public static bool CompareFiles(string filePath1, string filePath2)
    {
        if (!File.Exists(filePath1) || !File.Exists(filePath2))
            return false;

        var info1 = new FileInfo(filePath1);
        var info2 = new FileInfo(filePath2);

        if (info1.Length != info2.Length)
            return false;

        using var md5 = MD5.Create();
        using var stream1 = File.OpenRead(filePath1);
        using var stream2 = File.OpenRead(filePath2);

        var hash1 = md5.ComputeHash(stream1);
        var hash2 = md5.ComputeHash(stream2);

        return hash1.SequenceEqual(hash2);
    }

    /// <summary>
    /// 比较两个文件的内容差异（逐字节比较）。
    /// </summary>
    /// <param name="filePath1">第一个文件路径。</param>
    /// <param name="filePath2">第二个文件路径。</param>
    /// <returns>差异信息列表，包含差异位置和两个文件的字节值。</returns>
    /// <example>
    /// <code>
    /// var differences = FileHelper.CompareFileContent("C:\\file1.bin", "C:\\file2.bin");
    /// foreach (var diff in differences)
    /// {
    ///     Console.WriteLine($"位置 {diff.Position}: 文件1={diff.Value1:X2}, 文件2={diff.Value2:X2}");
    /// }
    /// </code>
    /// </example>
    public static List<FileDifference> CompareFileContent(string filePath1, string filePath2)
    {
        var differences = new List<FileDifference>();

        if (!File.Exists(filePath1) || !File.Exists(filePath2))
            return differences;

        using var stream1 = File.OpenRead(filePath1);
        using var stream2 = File.OpenRead(filePath2);

        long position = 0;
        int byte1, byte2;

        while ((byte1 = stream1.ReadByte()) != -1 && (byte2 = stream2.ReadByte()) != -1)
        {
            if (byte1 != byte2)
            {
                differences.Add(new FileDifference { Position = position, Value1 = (byte)byte1, Value2 = (byte)byte2 });
            }
            position++;
        }

        while (stream1.ReadByte() != -1 || stream2.ReadByte() != -1)
        {
            differences.Add(new FileDifference { Position = position, Value1 = 0, Value2 = 0 });
            position++;
        }

        return differences;
    }

    /// <summary>
    /// 文件差异信息。
    /// </summary>
    public class FileDifference
    {
        /// <summary>
        /// 差异位置（字节偏移）。
        /// </summary>
        public long Position { get; set; }

        /// <summary>
        /// 第一个文件的字节值。
        /// </summary>
        public byte Value1 { get; set; }

        /// <summary>
        /// 第二个文件的字节值。
        /// </summary>
        public byte Value2 { get; set; }
    }

    #endregion

    #region 文件版本信息

    /// <summary>
    /// 获取文件版本信息。
    /// </summary>
    /// <param name="filePath">文件路径。</param>
    /// <returns>文件版本信息，如果文件不存在或无版本信息返回 null。</returns>
    /// <example>
    /// <code>
    /// var versionInfo = FileHelper.GetFileVersionInfo("C:\\Program Files\\MyApp\\app.exe");
    /// if (versionInfo != null)
    /// {
    ///     Console.WriteLine($"版本: {versionInfo.FileVersion}");
    ///     Console.WriteLine($"产品: {versionInfo.ProductName}");
    /// }
    /// </code>
    /// </example>
    public static FileVersionInfo GetFileVersionInfo(string filePath)
    {
        if (!File.Exists(filePath))
            return null;

        return FileVersionInfo.GetVersionInfo(filePath);
    }

    /// <summary>
    /// 获取文件版本字符串。
    /// </summary>
    /// <param name="filePath">文件路径。</param>
    /// <returns>文件版本字符串，如果文件不存在或无版本信息返回空字符串。</returns>
    public static string GetFileVersion(string filePath)
    {
        var info = GetFileVersionInfo(filePath);
        return info?.FileVersion ?? string.Empty;
    }

    #endregion

    #region 临时文件

    /// <summary>
    /// 创建临时文件并返回路径。
    /// </summary>
    /// <param name="extension">文件扩展名（带点），如 ".txt"。</param>
    /// <param name="prefix">文件名前缀。</param>
    /// <returns>临时文件路径。</returns>
    /// <example>
    /// <code>
    /// var tempFile = FileHelper.CreateTempFile(".txt", "myapp_");
    /// File.WriteAllText(tempFile, "临时内容");
    /// </code>
    /// </example>
    public static string CreateTempFile(string extension = ".tmp", string prefix = "")
    {
        var tempPath = Path.GetTempPath();
        var fileName = $"{prefix}{Guid.NewGuid():N}{extension}";
        var filePath = Path.Combine(tempPath, fileName);
        File.WriteAllText(filePath, string.Empty);
        return filePath;
    }

    /// <summary>
    /// 创建临时目录并返回路径。
    /// </summary>
    /// <param name="prefix">目录名前缀。</param>
    /// <returns>临时目录路径。</returns>
    /// <example>
    /// <code>
    /// var tempDir = FileHelper.CreateTempDirectory("myapp_");
    /// // 使用临时目录...
    /// Directory.Delete(tempDir, true); // 清理
    /// </code>
    /// </example>
    public static string CreateTempDirectory(string prefix = "")
    {
        var tempPath = Path.GetTempPath();
        var dirName = $"{prefix}{Guid.NewGuid():N}";
        var dirPath = Path.Combine(tempPath, dirName);
        Directory.CreateDirectory(dirPath);
        return dirPath;
    }

    #endregion

    #region 文件监控

    /// <summary>
    /// 创建文件系统监视器。
    /// </summary>
    /// <param name="path">要监视的路径。</param>
    /// <param name="filter">文件筛选器，默认为 "*.*"。</param>
    /// <param name="includeSubdirectories">是否包含子目录。</param>
    /// <returns>文件系统监视器实例。</returns>
    /// <remarks>
    /// <para>返回的 FileSystemWatcher 需要由调用方管理生命周期（启用、禁用、释放）。</para>
    /// <para>使用示例：</para>
    /// <code>
    /// using var watcher = FileHelper.WatchFileChanges("C:\\Logs", "*.log");
    /// watcher.Changed += (s, e) => Console.WriteLine($"文件已更改: {e.FullPath}");
    /// watcher.EnableRaisingEvents = true;
    /// </code>
    /// </remarks>
    public static FileSystemWatcher WatchFileChanges(string path, string filter = "*.*", bool includeSubdirectories = true)
    {
        return new FileSystemWatcher
        {
            Path = path,
            Filter = filter,
            IncludeSubdirectories = includeSubdirectories,
            NotifyFilter = NotifyFilters.FileName | NotifyFilters.DirectoryName | NotifyFilters.LastWrite | NotifyFilters.Size
        };
    }

    /// <summary>
    /// 等待文件创建完成（用于处理正在写入的文件）。
    /// </summary>
    /// <param name="filePath">文件路径。</param>
    /// <param name="timeout">超时时间。</param>
    /// <returns>如果文件创建完成返回 true；超时返回 false。</returns>
    /// <example>
    /// <code>
    /// if (FileHelper.WaitForFileCreation("C:\\upload\\file.pdf", TimeSpan.FromSeconds(30)))
    /// {
    ///     // 文件已就绪，可以处理
    /// }
    /// </code>
    /// </example>
    public static bool WaitForFileCreation(string filePath, TimeSpan timeout)
    {
        var startTime = DateTime.Now;
        while (!File.Exists(filePath))
        {
            if (DateTime.Now - startTime > timeout)
                return false;
            Thread.Sleep(100);
        }
        return true;
    }

    #endregion

    #region 文件锁定

    /// <summary>
    /// 判断文件是否被锁定。
    /// </summary>
    /// <param name="filePath">文件路径。</param>
    /// <returns>如果文件被锁定返回 true；否则返回 false。</returns>
    /// <example>
    /// <code>
    /// if (!FileHelper.IsFileLocked("C:\\data.xlsx"))
    /// {
    ///     // 文件未被锁定，可以安全操作
    /// }
    /// </code>
    /// </example>
    public static bool IsFileLocked(string filePath)
    {
        if (!File.Exists(filePath))
            return false;

        try
        {
            using var stream = File.Open(filePath, FileMode.Open, FileAccess.ReadWrite, FileShare.None);
            return false;
        }
        catch (IOException)
        {
            return true;
        }
    }

    /// <summary>
    /// 等待文件解锁。
    /// </summary>
    /// <param name="filePath">文件路径。</param>
    /// <param name="timeout">超时时间。</param>
    /// <returns>如果文件解锁成功返回 true；超时返回 false。</returns>
    /// <example>
    /// <code>
    /// if (FileHelper.WaitForFileUnlock("C:\\data.xlsx", TimeSpan.FromSeconds(10)))
    /// {
    ///     // 文件已解锁，可以安全操作
    /// }
    /// </code>
    /// </example>
    public static bool WaitForFileUnlock(string filePath, TimeSpan timeout)
    {
        var startTime = DateTime.Now;
        while (IsFileLocked(filePath))
        {
            if (DateTime.Now - startTime > timeout)
                return false;
            Thread.Sleep(100);
        }
        return true;
    }

    #endregion

    #region 文件搜索

    /// <summary>
    /// 搜索文件（延迟执行，适用于大量文件）。
    /// </summary>
    /// <param name="directoryPath">搜索目录。</param>
    /// <param name="searchPattern">搜索模式，支持多个模式（用分号分隔）。</param>
    /// <param name="searchOption">搜索选项。</param>
    /// <returns>匹配的文件路径枚举。</returns>
    /// <example>
    /// <code>
    /// // 搜索所有 .txt 和 .log 文件
    /// foreach (var file in FileHelper.SearchFiles("C:\\Logs", "*.txt;*.log"))
    /// {
    ///     Console.WriteLine(file);
    /// }
    /// </code>
    /// </example>
    public static IEnumerable<string> SearchFiles(string directoryPath, string searchPattern = "*", SearchOption searchOption = SearchOption.TopDirectoryOnly)
    {
        if (!Directory.Exists(directoryPath))
            yield break;

        var patterns = searchPattern.Split(';', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);

        foreach (var pattern in patterns)
        {
            var files = Directory.EnumerateFiles(directoryPath, pattern, searchOption);
            foreach (var file in files)
            {
                yield return file;
            }
        }
    }

    /// <summary>
    /// 搜索目录（延迟执行）。
    /// </summary>
    /// <param name="directoryPath">搜索目录。</param>
    /// <param name="searchPattern">搜索模式。</param>
    /// <param name="searchOption">搜索选项。</param>
    /// <returns>匹配的目录路径枚举。</returns>
    public static IEnumerable<string> SearchDirectories(string directoryPath, string searchPattern = "*", SearchOption searchOption = SearchOption.TopDirectoryOnly)
    {
        if (!Directory.Exists(directoryPath))
            yield break;

        var directories = Directory.EnumerateDirectories(directoryPath, searchPattern, searchOption);
        foreach (var dir in directories)
        {
            yield return dir;
        }
    }

    #endregion

    #region 文件编码检测

    /// <summary>
    /// 检测文件的编码格式。
    /// </summary>
    /// <param name="filePath">文件路径。</param>
    /// <returns>检测到的编码格式，默认返回 UTF-8。</returns>
    /// <remarks>
    /// <para>此方法通过检测文件 BOM（字节顺序标记）来判断编码。</para>
    /// <para>如果文件没有 BOM，则返回 UTF-8 作为默认编码。</para>
    /// </remarks>
    /// <example>
    /// <code>
    /// var encoding = FileHelper.DetectFileEncoding("C:\\data.txt");
    /// Console.WriteLine($"文件编码: {encoding.EncodingName}");
    /// </code>
    /// </example>
    public static Encoding DetectFileEncoding(string filePath)
    {
        if (!File.Exists(filePath))
            return Encoding.UTF8;

        using var stream = File.OpenRead(filePath);
        var bom = new byte[4];
        var bytesRead = stream.Read(bom, 0, 4);

        if (bytesRead >= 3 && bom[0] == 0xEF && bom[1] == 0xBB && bom[2] == 0xBF)
            return Encoding.UTF8;

        if (bytesRead >= 4 && bom[0] == 0x00 && bom[1] == 0x00 && bom[2] == 0xFE && bom[3] == 0xFF)
            return Encoding.BigEndianUnicode;

        if (bytesRead >= 4 && bom[0] == 0xFF && bom[1] == 0xFE && bom[2] == 0x00 && bom[3] == 0x00)
            return Encoding.UTF32;

        if (bytesRead >= 2 && bom[0] == 0xFF && bom[1] == 0xFE)
            return Encoding.Unicode;

        if (bytesRead >= 2 && bom[0] == 0xFE && bom[1] == 0xFF)
            return Encoding.BigEndianUnicode;

        if (bytesRead >= 3 && bom[0] == 0x2B && bom[1] == 0x2F && bom[2] == 0x76)
            return Encoding.UTF7;

        return Encoding.UTF8;
    }

    #endregion
}
