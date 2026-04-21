using System.Text;

namespace Chet.Utils;

/// <summary>
/// File 扩展方法类，提供常用的读写、判断、信息获取、操作等功能。
/// </summary>
/// <remarks>
/// <para>本类提供丰富的文件扩展方法，涵盖以下功能：</para>
/// <list type="bullet">
///   <item><description>文件判断：Exists、IsFile、IsDirectory、IsReadOnly、IsHidden</description></item>
///   <item><description>同步读取：ReadAllText、ReadAllLines、ReadAllBytes、ReadLines</description></item>
///   <item><description>异步读取：ReadAllTextAsync、ReadAllLinesAsync、ReadAllBytesAsync</description></item>
///   <item><description>同步写入：WriteAllText、WriteAllLines、WriteAllBytes、AppendText、AppendLines</description></item>
///   <item><description>异步写入：WriteAllTextAsync、WriteAllLinesAsync、WriteAllBytesAsync</description></item>
///   <item><description>文件操作：DeleteFile、CopyTo、MoveTo、Rename、Replace</description></item>
///   <item><description>文件信息：GetFileSize、GetCreationTime、GetLastWriteTime、GetLastAccessTime</description></item>
///   <item><description>文件属性：SetReadOnly、UnsetReadOnly、SetHidden、UnsetHidden、SetAttributes</description></item>
///   <item><description>哈希计算：GetFileMd5、GetFileSha256、GetFileHash</description></item>
///   <item><description>路径处理：GetExtension、GetFileName、GetDirectoryName、CombinePaths</description></item>
///   <item><description>路径判断：IsAbsolute、IsRelative、IsUncPath、HasInvalidChars</description></item>
///   <item><description>目录操作：CreateDirectory、DeleteDirectory、GetFiles、GetDirectories</description></item>
/// </list>
/// </remarks>
public static class FileExtensions
{
    #region 文件判断

    /// <summary>
    /// 判断文件是否存在。
    /// </summary>
    /// <param name="filePath">文件路径。</param>
    /// <returns>如果文件存在返回 true；否则返回 false。</returns>
    /// <example>
    /// <code>
    /// var exists = "C:\\test.txt".Exists(); // false
    /// </code>
    /// </example>
    public static bool Exists(this string filePath) => File.Exists(filePath);

    /// <summary>
    /// 判断路径是否为文件。
    /// </summary>
    /// <param name="path">待判断的路径。</param>
    /// <returns>如果路径为文件返回 true；否则返回 false。</returns>
    public static bool IsFile(this string path) =>
        !string.IsNullOrEmpty(path) && File.Exists(path);

    /// <summary>
    /// 判断路径是否为目录。
    /// </summary>
    /// <param name="path">待判断的路径。</param>
    /// <returns>如果路径为目录返回 true；否则返回 false。</returns>
    public static bool IsDirectory(this string path) =>
        !string.IsNullOrEmpty(path) && Directory.Exists(path);

    /// <summary>
    /// 判断路径的文件或目录是否存在。
    /// </summary>
    /// <param name="path">待判断的路径。</param>
    /// <returns>如果文件或目录存在返回 true；否则返回 false。</returns>
    public static bool ExistsPath(this string path) =>
        !string.IsNullOrEmpty(path) && (File.Exists(path) || Directory.Exists(path));

    /// <summary>
    /// 判断文件是否为只读。
    /// </summary>
    /// <param name="filePath">文件路径。</param>
    /// <returns>如果文件为只读返回 true；否则返回 false。</returns>
    public static bool IsReadOnly(this string filePath) =>
        File.Exists(filePath) && new FileInfo(filePath).IsReadOnly;

    /// <summary>
    /// 判断文件是否为隐藏文件。
    /// </summary>
    /// <param name="filePath">文件路径。</param>
    /// <returns>如果文件为隐藏文件返回 true；否则返回 false。</returns>
    public static bool IsHidden(this string filePath)
    {
        if (!File.Exists(filePath)) return false;
        var attr = File.GetAttributes(filePath);
        return (attr & FileAttributes.Hidden) == FileAttributes.Hidden;
    }

    /// <summary>
    /// 判断文件是否为系统文件。
    /// </summary>
    /// <param name="filePath">文件路径。</param>
    /// <returns>如果文件为系统文件返回 true；否则返回 false。</returns>
    public static bool IsSystemFile(this string filePath)
    {
        if (!File.Exists(filePath)) return false;
        var attr = File.GetAttributes(filePath);
        return (attr & FileAttributes.System) == FileAttributes.System;
    }

    /// <summary>
    /// 判断文件是否为临时文件。
    /// </summary>
    /// <param name="filePath">文件路径。</param>
    /// <returns>如果文件为临时文件返回 true；否则返回 false。</returns>
    public static bool IsTemporary(this string filePath)
    {
        if (!File.Exists(filePath)) return false;
        var attr = File.GetAttributes(filePath);
        return (attr & FileAttributes.Temporary) == FileAttributes.Temporary;
    }

    #endregion

    #region 同步读取

    /// <summary>
    /// 读取文件所有文本内容。
    /// </summary>
    /// <param name="filePath">文件路径。</param>
    /// <returns>文件文本内容，如果文件不存在返回空字符串。</returns>
    /// <example>
    /// <code>
    /// var content = "C:\\test.txt".ReadAllText();
    /// </code>
    /// </example>
    public static string ReadAllText(this string filePath) =>
        File.Exists(filePath) ? File.ReadAllText(filePath) : string.Empty;

    /// <summary>
    /// 读取文件所有文本内容（指定编码）。
    /// </summary>
    /// <param name="filePath">文件路径。</param>
    /// <param name="encoding">文件编码。</param>
    /// <returns>文件文本内容，如果文件不存在返回空字符串。</returns>
    public static string ReadAllText(this string filePath, Encoding encoding) =>
        File.Exists(filePath) ? File.ReadAllText(filePath, encoding) : string.Empty;

    /// <summary>
    /// 读取文件所有行内容。
    /// </summary>
    /// <param name="filePath">文件路径。</param>
    /// <returns>文件所有行的数组，如果文件不存在返回空数组。</returns>
    public static string[] ReadAllLines(this string filePath) =>
        File.Exists(filePath) ? File.ReadAllLines(filePath) : Array.Empty<string>();

    /// <summary>
    /// 读取文件所有行内容（指定编码）。
    /// </summary>
    /// <param name="filePath">文件路径。</param>
    /// <param name="encoding">文件编码。</param>
    /// <returns>文件所有行的数组，如果文件不存在返回空数组。</returns>
    public static string[] ReadAllLines(this string filePath, Encoding encoding) =>
        File.Exists(filePath) ? File.ReadAllLines(filePath, encoding) : Array.Empty<string>();

    /// <summary>
    /// 读取文件所有字节内容。
    /// </summary>
    /// <param name="filePath">文件路径。</param>
    /// <returns>文件字节数组，如果文件不存在返回空数组。</returns>
    public static byte[] ReadAllBytes(this string filePath) =>
        File.Exists(filePath) ? File.ReadAllBytes(filePath) : Array.Empty<byte>();

    /// <summary>
    /// 逐行读取文件内容（延迟执行）。
    /// </summary>
    /// <param name="filePath">文件路径。</param>
    /// <returns>文件行的枚举。</returns>
    public static IEnumerable<string> ReadLines(this string filePath)
    {
        if (!File.Exists(filePath)) yield break;
        foreach (var line in File.ReadLines(filePath))
        {
            yield return line;
        }
    }

    /// <summary>
    /// 逐行读取文件内容（指定编码，延迟执行）。
    /// </summary>
    /// <param name="filePath">文件路径。</param>
    /// <param name="encoding">文件编码。</param>
    /// <returns>文件行的枚举。</returns>
    public static IEnumerable<string> ReadLines(this string filePath, Encoding encoding)
    {
        if (!File.Exists(filePath)) yield break;
        foreach (var line in File.ReadLines(filePath, encoding))
        {
            yield return line;
        }
    }

    #endregion

    #region 异步读取

    /// <summary>
    /// 异步读取文件所有文本内容。
    /// </summary>
    /// <param name="filePath">文件路径。</param>
    /// <param name="cancellationToken">取消令牌。</param>
    /// <returns>文件文本内容，如果文件不存在返回空字符串。</returns>
    /// <example>
    /// <code>
    /// var content = await "C:\\test.txt".ReadAllTextAsync();
    /// </code>
    /// </example>
    public static async Task<string> ReadAllTextAsync(this string filePath, CancellationToken cancellationToken = default)
    {
        if (!File.Exists(filePath)) return string.Empty;
        using var reader = new StreamReader(filePath);
        cancellationToken.ThrowIfCancellationRequested();
        return await reader.ReadToEndAsync();
    }

    /// <summary>
    /// 异步读取文件所有文本内容（指定编码）。
    /// </summary>
    /// <param name="filePath">文件路径。</param>
    /// <param name="encoding">文件编码。</param>
    /// <param name="cancellationToken">取消令牌。</param>
    /// <returns>文件文本内容，如果文件不存在返回空字符串。</returns>
    public static async Task<string> ReadAllTextAsync(this string filePath, Encoding encoding, CancellationToken cancellationToken = default)
    {
        if (!File.Exists(filePath)) return string.Empty;
        using var reader = new StreamReader(filePath, encoding);
        cancellationToken.ThrowIfCancellationRequested();
        return await reader.ReadToEndAsync();
    }

    /// <summary>
    /// 异步读取文件所有行内容。
    /// </summary>
    /// <param name="filePath">文件路径。</param>
    /// <param name="cancellationToken">取消令牌。</param>
    /// <returns>文件所有行的列表，如果文件不存在返回空列表。</returns>
    public static async Task<List<string>> ReadAllLinesAsync(this string filePath, CancellationToken cancellationToken = default)
    {
        if (!File.Exists(filePath)) return new List<string>();
        var lines = new List<string>();
        using var reader = new StreamReader(filePath);
        while (!reader.EndOfStream)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var line = await reader.ReadLineAsync();
            if (line != null)
                lines.Add(line);
        }
        return lines;
    }

    /// <summary>
    /// 异步读取文件所有行内容（指定编码）。
    /// </summary>
    /// <param name="filePath">文件路径。</param>
    /// <param name="encoding">文件编码。</param>
    /// <param name="cancellationToken">取消令牌。</param>
    /// <returns>文件所有行的列表，如果文件不存在返回空列表。</returns>
    public static async Task<List<string>> ReadAllLinesAsync(this string filePath, Encoding encoding, CancellationToken cancellationToken = default)
    {
        if (!File.Exists(filePath)) return new List<string>();
        var lines = new List<string>();
        using var reader = new StreamReader(filePath, encoding);
        while (!reader.EndOfStream)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var line = await reader.ReadLineAsync();
            if (line != null)
                lines.Add(line);
        }
        return lines;
    }

    /// <summary>
    /// 异步读取文件所有字节内容。
    /// </summary>
    /// <param name="filePath">文件路径。</param>
    /// <param name="cancellationToken">取消令牌。</param>
    /// <returns>文件字节数组，如果文件不存在返回空数组。</returns>
    public static async Task<byte[]> ReadAllBytesAsync(this string filePath, CancellationToken cancellationToken = default)
    {
        if (!File.Exists(filePath)) return Array.Empty<byte>();
        using var stream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
        var buffer = new byte[stream.Length];
        await stream.ReadAsync(buffer, 0, buffer.Length, cancellationToken);
        return buffer;
    }

    #endregion

    #region 同步写入

    /// <summary>
    /// 写入文本内容到文件（覆盖）。
    /// </summary>
    /// <param name="filePath">文件路径。</param>
    /// <param name="content">写入内容。</param>
    /// <example>
    /// <code>
    /// "C:\\test.txt".WriteAllText("Hello World");
    /// </code>
    /// </example>
    public static void WriteAllText(this string filePath, string content)
    {
        EnsureDirectoryExists(filePath);
        File.WriteAllText(filePath, content ?? string.Empty);
    }

    /// <summary>
    /// 写入文本内容到文件（覆盖，指定编码）。
    /// </summary>
    /// <param name="filePath">文件路径。</param>
    /// <param name="content">写入内容。</param>
    /// <param name="encoding">文件编码。</param>
    public static void WriteAllText(this string filePath, string content, Encoding encoding)
    {
        EnsureDirectoryExists(filePath);
        File.WriteAllText(filePath, content ?? string.Empty, encoding);
    }

    /// <summary>
    /// 写入多行内容到文件（覆盖）。
    /// </summary>
    /// <param name="filePath">文件路径。</param>
    /// <param name="lines">写入行内容。</param>
    public static void WriteAllLines(this string filePath, IEnumerable<string> lines)
    {
        EnsureDirectoryExists(filePath);
        File.WriteAllLines(filePath, lines ?? Array.Empty<string>());
    }

    /// <summary>
    /// 写入多行内容到文件（覆盖，指定编码）。
    /// </summary>
    /// <param name="filePath">文件路径。</param>
    /// <param name="lines">写入行内容。</param>
    /// <param name="encoding">文件编码。</param>
    public static void WriteAllLines(this string filePath, IEnumerable<string> lines, Encoding encoding)
    {
        EnsureDirectoryExists(filePath);
        File.WriteAllLines(filePath, lines ?? Array.Empty<string>(), encoding);
    }

    /// <summary>
    /// 写入字节内容到文件（覆盖）。
    /// </summary>
    /// <param name="filePath">文件路径。</param>
    /// <param name="bytes">写入字节内容。</param>
    public static void WriteAllBytes(this string filePath, byte[] bytes)
    {
        EnsureDirectoryExists(filePath);
        File.WriteAllBytes(filePath, bytes ?? Array.Empty<byte>());
    }

    /// <summary>
    /// 追加文本内容到文件。
    /// </summary>
    /// <param name="filePath">文件路径。</param>
    /// <param name="content">追加内容。</param>
    public static void AppendText(this string filePath, string content)
    {
        EnsureDirectoryExists(filePath);
        File.AppendAllText(filePath, content ?? string.Empty);
    }

    /// <summary>
    /// 追加文本内容到文件（指定编码）。
    /// </summary>
    /// <param name="filePath">文件路径。</param>
    /// <param name="content">追加内容。</param>
    /// <param name="encoding">文件编码。</param>
    public static void AppendText(this string filePath, string content, Encoding encoding)
    {
        EnsureDirectoryExists(filePath);
        File.AppendAllText(filePath, content ?? string.Empty, encoding);
    }

    /// <summary>
    /// 追加多行内容到文件。
    /// </summary>
    /// <param name="filePath">文件路径。</param>
    /// <param name="lines">追加行内容。</param>
    public static void AppendLines(this string filePath, IEnumerable<string> lines)
    {
        EnsureDirectoryExists(filePath);
        File.AppendAllLines(filePath, lines ?? Array.Empty<string>());
    }

    /// <summary>
    /// 追加多行内容到文件（指定编码）。
    /// </summary>
    /// <param name="filePath">文件路径。</param>
    /// <param name="lines">追加行内容。</param>
    /// <param name="encoding">文件编码。</param>
    public static void AppendLines(this string filePath, IEnumerable<string> lines, Encoding encoding)
    {
        EnsureDirectoryExists(filePath);
        File.AppendAllLines(filePath, lines ?? Array.Empty<string>(), encoding);
    }

    /// <summary>
    /// 追加一行内容到文件。
    /// </summary>
    /// <param name="filePath">文件路径。</param>
    /// <param name="line">追加行内容。</param>
    public static void AppendLine(this string filePath, string line)
    {
        EnsureDirectoryExists(filePath);
        File.AppendAllText(filePath, (line ?? string.Empty) + Environment.NewLine);
    }

    #endregion

    #region 异步写入

    /// <summary>
    /// 异步写入文本内容到文件（覆盖）。
    /// </summary>
    /// <param name="filePath">文件路径。</param>
    /// <param name="content">写入内容。</param>
    /// <param name="cancellationToken">取消令牌。</param>
    /// <example>
    /// <code>
    /// await "C:\\test.txt".WriteAllTextAsync("Hello World");
    /// </code>
    /// </example>
    public static async Task WriteAllTextAsync(this string filePath, string content, CancellationToken cancellationToken = default)
    {
        EnsureDirectoryExists(filePath);
        using var writer = new StreamWriter(filePath);
        cancellationToken.ThrowIfCancellationRequested();
        await writer.WriteAsync(content ?? string.Empty);
    }

    /// <summary>
    /// 异步写入文本内容到文件（覆盖，指定编码）。
    /// </summary>
    /// <param name="filePath">文件路径。</param>
    /// <param name="content">写入内容。</param>
    /// <param name="encoding">文件编码。</param>
    /// <param name="cancellationToken">取消令牌。</param>
    public static async Task WriteAllTextAsync(this string filePath, string content, Encoding encoding, CancellationToken cancellationToken = default)
    {
        EnsureDirectoryExists(filePath);
        using var writer = new StreamWriter(filePath, false, encoding);
        cancellationToken.ThrowIfCancellationRequested();
        await writer.WriteAsync(content ?? string.Empty);
    }

    /// <summary>
    /// 异步写入多行内容到文件（覆盖）。
    /// </summary>
    /// <param name="filePath">文件路径。</param>
    /// <param name="lines">写入行内容。</param>
    /// <param name="cancellationToken">取消令牌。</param>
    public static async Task WriteAllLinesAsync(this string filePath, IEnumerable<string> lines, CancellationToken cancellationToken = default)
    {
        EnsureDirectoryExists(filePath);
        using var writer = new StreamWriter(filePath);
        foreach (var line in lines ?? Array.Empty<string>())
        {
            cancellationToken.ThrowIfCancellationRequested();
            await writer.WriteLineAsync(line);
        }
    }

    /// <summary>
    /// 异步写入多行内容到文件（覆盖，指定编码）。
    /// </summary>
    /// <param name="filePath">文件路径。</param>
    /// <param name="lines">写入行内容。</param>
    /// <param name="encoding">文件编码。</param>
    /// <param name="cancellationToken">取消令牌。</param>
    public static async Task WriteAllLinesAsync(this string filePath, IEnumerable<string> lines, Encoding encoding, CancellationToken cancellationToken = default)
    {
        EnsureDirectoryExists(filePath);
        using var writer = new StreamWriter(filePath, false, encoding);
        foreach (var line in lines ?? Array.Empty<string>())
        {
            cancellationToken.ThrowIfCancellationRequested();
            await writer.WriteLineAsync(line);
        }
    }

    /// <summary>
    /// 异步写入字节内容到文件（覆盖）。
    /// </summary>
    /// <param name="filePath">文件路径。</param>
    /// <param name="bytes">写入字节内容。</param>
    /// <param name="cancellationToken">取消令牌。</param>
    public static async Task WriteAllBytesAsync(this string filePath, byte[] bytes, CancellationToken cancellationToken = default)
    {
        EnsureDirectoryExists(filePath);
        using var stream = new FileStream(filePath, FileMode.Create, FileAccess.Write);
        await stream.WriteAsync(bytes ?? Array.Empty<byte>(), 0, (bytes ?? Array.Empty<byte>()).Length, cancellationToken);
    }

    /// <summary>
    /// 异步追加文本内容到文件。
    /// </summary>
    /// <param name="filePath">文件路径。</param>
    /// <param name="content">追加内容。</param>
    /// <param name="cancellationToken">取消令牌。</param>
    public static async Task AppendTextAsync(this string filePath, string content, CancellationToken cancellationToken = default)
    {
        EnsureDirectoryExists(filePath);
        using var writer = new StreamWriter(filePath, true);
        cancellationToken.ThrowIfCancellationRequested();
        await writer.WriteAsync(content ?? string.Empty);
    }

    /// <summary>
    /// 异步追加多行内容到文件。
    /// </summary>
    /// <param name="filePath">文件路径。</param>
    /// <param name="lines">追加行内容。</param>
    /// <param name="cancellationToken">取消令牌。</param>
    public static async Task AppendLinesAsync(this string filePath, IEnumerable<string> lines, CancellationToken cancellationToken = default)
    {
        EnsureDirectoryExists(filePath);
        using var writer = new StreamWriter(filePath, true);
        foreach (var line in lines ?? Array.Empty<string>())
        {
            cancellationToken.ThrowIfCancellationRequested();
            await writer.WriteLineAsync(line);
        }
    }

    #endregion

    #region 文件操作

    /// <summary>
    /// 删除文件。
    /// </summary>
    /// <param name="filePath">文件路径。</param>
    /// <example>
    /// <code>
    /// "C:\\test.txt".DeleteFile();
    /// </code>
    /// </example>
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
        {
            EnsureDirectoryExists(destPath);
            File.Copy(filePath, destPath, overwrite);
        }
    }

    /// <summary>
    /// 异步复制文件到目标路径（可覆盖）。
    /// </summary>
    /// <param name="filePath">源文件路径。</param>
    /// <param name="destPath">目标文件路径。</param>
    /// <param name="overwrite">是否覆盖目标文件。</param>
    /// <param name="cancellationToken">取消令牌。</param>
    public static async Task CopyToAsync(this string filePath, string destPath, bool overwrite = true, CancellationToken cancellationToken = default)
    {
        if (!File.Exists(filePath)) return;
        EnsureDirectoryExists(destPath);
        if (overwrite && File.Exists(destPath))
            File.Delete(destPath);
        using var sourceStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
        using var destStream = new FileStream(destPath, FileMode.Create, FileAccess.Write);
        await sourceStream.CopyToAsync(destStream, cancellationToken);
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
        EnsureDirectoryExists(destPath);
        if (overwrite && File.Exists(destPath))
            File.Delete(destPath);
        File.Move(filePath, destPath);
    }

    /// <summary>
    /// 重命名文件。
    /// </summary>
    /// <param name="filePath">原文件路径。</param>
    /// <param name="newName">新文件名（不含路径）。</param>
    public static void Rename(this string filePath, string newName)
    {
        if (!File.Exists(filePath) || string.IsNullOrEmpty(newName)) return;
        var directory = Path.GetDirectoryName(filePath);
        var newPath = string.IsNullOrEmpty(directory) ? newName : Path.Combine(directory, newName);
        File.Move(filePath, newPath);
    }

    /// <summary>
    /// 替换文件内容。
    /// </summary>
    /// <param name="filePath">目标文件路径。</param>
    /// <param name="sourceFilePath">源文件路径。</param>
    /// <param name="backupFilePath">备份文件路径（可选）。</param>
    public static void Replace(this string filePath, string sourceFilePath, string backupFilePath = null)
    {
        if (!File.Exists(filePath) || !File.Exists(sourceFilePath)) return;
        if (string.IsNullOrEmpty(backupFilePath))
            File.Replace(sourceFilePath, filePath, null);
        else
            File.Replace(sourceFilePath, filePath, backupFilePath);
    }

    /// <summary>
    /// 创建文件（如果不存在）。
    /// </summary>
    /// <param name="filePath">文件路径。</param>
    /// <returns>创建的文件流。</returns>
    public static FileStream CreateFile(this string filePath)
    {
        EnsureDirectoryExists(filePath);
        return File.Create(filePath);
    }

    /// <summary>
    /// 清空文件内容。
    /// </summary>
    /// <param name="filePath">文件路径。</param>
    public static void Clear(this string filePath)
    {
        if (File.Exists(filePath))
            File.WriteAllText(filePath, string.Empty);
    }

    #endregion

    #region 文件信息

    /// <summary>
    /// 获取文件大小（字节）。
    /// </summary>
    /// <param name="filePath">文件路径。</param>
    /// <returns>文件大小（字节），如果文件不存在返回 0。</returns>
    public static long GetFileSize(this string filePath) =>
        File.Exists(filePath) ? new FileInfo(filePath).Length : 0;

    /// <summary>
    /// 获取文件的友好大小描述。
    /// </summary>
    /// <param name="filePath">文件路径。</param>
    /// <returns>友好大小描述，如 "1.5 MB"。</returns>
    public static string GetFriendlyFileSize(this string filePath)
    {
        var size = filePath.GetFileSize();
        return size.GetFriendlyFileSizeString();
    }

    /// <summary>
    /// 获取文件创建时间。
    /// </summary>
    /// <param name="filePath">文件路径。</param>
    /// <returns>文件创建时间，如果文件不存在返回 DateTime.MinValue。</returns>
    public static DateTime GetCreationTime(this string filePath) =>
        File.Exists(filePath) ? File.GetCreationTime(filePath) : DateTime.MinValue;

    /// <summary>
    /// 获取文件最后修改时间。
    /// </summary>
    /// <param name="filePath">文件路径。</param>
    /// <returns>文件最后修改时间，如果文件不存在返回 DateTime.MinValue。</returns>
    public static DateTime GetLastWriteTime(this string filePath) =>
        File.Exists(filePath) ? File.GetLastWriteTime(filePath) : DateTime.MinValue;

    /// <summary>
    /// 获取文件最后访问时间。
    /// </summary>
    /// <param name="filePath">文件路径。</param>
    /// <returns>文件最后访问时间，如果文件不存在返回 DateTime.MinValue。</returns>
    public static DateTime GetLastAccessTime(this string filePath) =>
        File.Exists(filePath) ? File.GetLastAccessTime(filePath) : DateTime.MinValue;

    /// <summary>
    /// 获取文件扩展名（带点）。
    /// </summary>
    /// <param name="filePath">文件路径。</param>
    /// <returns>文件扩展名。</returns>
    public static string GetExtension(this string filePath) =>
        Path.GetExtension(filePath);

    /// <summary>
    /// 获取文件名（不含路径）。
    /// </summary>
    /// <param name="filePath">文件路径。</param>
    /// <returns>文件名。</returns>
    public static string GetFileName(this string filePath) =>
        Path.GetFileName(filePath);

    /// <summary>
    /// 获取文件名（不含扩展名）。
    /// </summary>
    /// <param name="filePath">文件路径。</param>
    /// <returns>不含扩展名的文件名。</returns>
    public static string GetFileNameWithoutExtension(this string filePath) =>
        Path.GetFileNameWithoutExtension(filePath);

    /// <summary>
    /// 获取文件所在目录路径。
    /// </summary>
    /// <param name="filePath">文件路径。</param>
    /// <returns>目录路径。</returns>
    public static string GetDirectoryName(this string filePath) =>
        Path.GetDirectoryName(filePath);

    /// <summary>
    /// 获取文件属性。
    /// </summary>
    /// <param name="filePath">文件路径。</param>
    /// <returns>文件属性，如果文件不存在返回 FileAttributes.Normal。</returns>
    public static FileAttributes GetFileAttributes(this string filePath) =>
        File.Exists(filePath) ? File.GetAttributes(filePath) : FileAttributes.Normal;

    #endregion

    #region 文件属性设置

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

    /// <summary>
    /// 设置文件属性。
    /// </summary>
    /// <param name="filePath">文件路径。</param>
    /// <param name="attributes">文件属性。</param>
    public static void SetFileAttributes(this string filePath, FileAttributes attributes)
    {
        if (File.Exists(filePath))
            File.SetAttributes(filePath, attributes);
    }

    /// <summary>
    /// 设置文件创建时间。
    /// </summary>
    /// <param name="filePath">文件路径。</param>
    /// <param name="time">创建时间。</param>
    public static void SetCreationTime(this string filePath, DateTime time)
    {
        if (File.Exists(filePath))
            File.SetCreationTime(filePath, time);
    }

    /// <summary>
    /// 设置文件最后修改时间。
    /// </summary>
    /// <param name="filePath">文件路径。</param>
    /// <param name="time">最后修改时间。</param>
    public static void SetLastWriteTime(this string filePath, DateTime time)
    {
        if (File.Exists(filePath))
            File.SetLastWriteTime(filePath, time);
    }

    /// <summary>
    /// 设置文件最后访问时间。
    /// </summary>
    /// <param name="filePath">文件路径。</param>
    /// <param name="time">最后访问时间。</param>
    public static void SetLastAccessTime(this string filePath, DateTime time)
    {
        if (File.Exists(filePath))
            File.SetLastAccessTime(filePath, time);
    }

    #endregion

    #region 哈希计算

    /// <summary>
    /// 获取文件的 MD5 值（32位小写）。
    /// </summary>
    /// <param name="filePath">文件路径。</param>
    /// <returns>MD5 哈希值，如果文件不存在返回空字符串。</returns>
    public static string GetFileMd5(this string filePath)
    {
        if (!File.Exists(filePath)) return string.Empty;
        using var md5 = System.Security.Cryptography.MD5.Create();
        using var stream = File.OpenRead(filePath);
        var hash = md5.ComputeHash(stream);
        return BitConverter.ToString(hash).Replace("-", "").ToLowerInvariant();
    }

    /// <summary>
    /// 异步获取文件的 MD5 值（32位小写）。
    /// </summary>
    /// <param name="filePath">文件路径。</param>
    /// <param name="cancellationToken">取消令牌。</param>
    /// <returns>MD5 哈希值，如果文件不存在返回空字符串。</returns>
    public static async Task<string> GetFileMd5Async(this string filePath, CancellationToken cancellationToken = default)
    {
        if (!File.Exists(filePath)) return string.Empty;
        using var md5 = System.Security.Cryptography.MD5.Create();
        using var stream = File.OpenRead(filePath);
        var hash = await md5.ComputeHashAsync(stream, cancellationToken);
        return BitConverter.ToString(hash).Replace("-", "").ToLowerInvariant();
    }

    /// <summary>
    /// 获取文件的 SHA256 值（64位小写）。
    /// </summary>
    /// <param name="filePath">文件路径。</param>
    /// <returns>SHA256 哈希值，如果文件不存在返回空字符串。</returns>
    public static string GetFileSha256(this string filePath)
    {
        if (!File.Exists(filePath)) return string.Empty;
        using var sha = System.Security.Cryptography.SHA256.Create();
        using var stream = File.OpenRead(filePath);
        var hash = sha.ComputeHash(stream);
        return BitConverter.ToString(hash).Replace("-", "").ToLowerInvariant();
    }

    /// <summary>
    /// 异步获取文件的 SHA256 值（64位小写）。
    /// </summary>
    /// <param name="filePath">文件路径。</param>
    /// <param name="cancellationToken">取消令牌。</param>
    /// <returns>SHA256 哈希值，如果文件不存在返回空字符串。</returns>
    public static async Task<string> GetFileSha256Async(this string filePath, CancellationToken cancellationToken = default)
    {
        if (!File.Exists(filePath)) return string.Empty;
        using var sha = System.Security.Cryptography.SHA256.Create();
        using var stream = File.OpenRead(filePath);
        var hash = await sha.ComputeHashAsync(stream, cancellationToken);
        return BitConverter.ToString(hash).Replace("-", "").ToLowerInvariant();
    }

    /// <summary>
    /// 获取文件的指定算法哈希值。
    /// </summary>
    /// <param name="filePath">文件路径。</param>
    /// <param name="hashAlgorithm">哈希算法实例。</param>
    /// <returns>哈希值（小写十六进制字符串）。</returns>
    public static string GetFileHash(this string filePath, System.Security.Cryptography.HashAlgorithm hashAlgorithm)
    {
        if (!File.Exists(filePath) || hashAlgorithm == null) return string.Empty;
        using var stream = File.OpenRead(filePath);
        var hash = hashAlgorithm.ComputeHash(stream);
        return BitConverter.ToString(hash).Replace("-", "").ToLowerInvariant();
    }

    /// <summary>
    /// 打开文件并返回文件流。
    /// </summary>
    /// <param name="filePath">文件路径。</param>
    /// <param name="fileMode">文件打开模式，默认为 Open。</param>
    /// <param name="fileAccess">文件访问模式，默认为 Read。</param>
    /// <param name="fileShare">文件共享模式，默认为 Read。</param>
    /// <returns>文件流，如果文件不存在返回 null。</returns>
    public static FileStream OpenAsFileStream(this string filePath, FileMode fileMode = FileMode.Open, FileAccess fileAccess = FileAccess.Read, FileShare fileShare = FileShare.Read)
    {
        if (!File.Exists(filePath)) return null;
        return new FileStream(filePath, fileMode, fileAccess, fileShare);
    }

    /// <summary>
    /// 打开文件并返回流（用于读取）。
    /// </summary>
    /// <param name="filePath">文件路径。</param>
    /// <returns>文件流，如果文件不存在返回 Stream.Null。</returns>
    public static Stream OpenAsStream(this string filePath)
    {
        if (!File.Exists(filePath)) return Stream.Null;
        return File.OpenRead(filePath);
    }

    #endregion

    #region 路径处理

    /// <summary>
    /// 判断路径是否为绝对路径。
    /// </summary>
    /// <param name="path">待判断的路径。</param>
    /// <returns>如果为绝对路径返回 true；否则返回 false。</returns>
    public static bool IsAbsolute(this string path) =>
        !string.IsNullOrEmpty(path) && Path.IsPathRooted(path);

    /// <summary>
    /// 判断路径是否为相对路径。
    /// </summary>
    /// <param name="path">待判断的路径。</param>
    /// <returns>如果为相对路径返回 true；否则返回 false。</returns>
    public static bool IsRelative(this string path) =>
        !string.IsNullOrEmpty(path) && !Path.IsPathRooted(path);

    /// <summary>
    /// 判断路径是否为 UNC 路径。
    /// </summary>
    /// <param name="path">待判断的路径。</param>
    /// <returns>如果为 UNC 路径返回 true；否则返回 false。</returns>
    public static bool IsUncPath(this string path) =>
        !string.IsNullOrEmpty(path) && path.StartsWith(@"\\") && Path.IsPathRooted(path);

    /// <summary>
    /// 判断路径是否包含无效字符。
    /// </summary>
    /// <param name="path">待判断的路径。</param>
    /// <returns>如果包含无效字符返回 true；否则返回 false。</returns>
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
    /// <returns>如果包含无效字符返回 true；否则返回 false。</returns>
    public static bool FileNameHasInvalidChars(this string fileName)
    {
        if (string.IsNullOrEmpty(fileName)) return false;
        var invalidChars = Path.GetInvalidFileNameChars();
        return fileName.IndexOfAny(invalidChars) >= 0;
    }

    /// <summary>
    /// 合并多个路径为一个完整路径。
    /// </summary>
    /// <param name="paths">路径数组。</param>
    /// <returns>合并后的路径。</returns>
    public static string CombinePaths(params string[] paths) =>
        Path.Combine(paths);

    /// <summary>
    /// 获取路径的根目录。
    /// </summary>
    /// <param name="path">待处理的路径。</param>
    /// <returns>根目录路径。</returns>
    public static string GetPathRoot(this string path) =>
        string.IsNullOrWhiteSpace(path) ? path : Path.GetPathRoot(path);

    /// <summary>
    /// 获取路径的完整规范路径（绝对路径）。
    /// </summary>
    /// <param name="path">待处理的路径。</param>
    /// <returns>完整规范路径。</returns>
    public static string GetFullPath(this string path) =>
        Path.GetFullPath(path);

    /// <summary>
    /// 获取临时文件路径。
    /// </summary>
    /// <returns>临时文件路径。</returns>
    public static string GetTempFilePath() => Path.GetTempFileName();

    /// <summary>
    /// 获取临时目录路径。
    /// </summary>
    /// <returns>临时目录路径。</returns>
    public static string GetTempDirectory() => Path.GetTempPath();

    /// <summary>
    /// 更改路径的扩展名。
    /// </summary>
    /// <param name="path">原始路径。</param>
    /// <param name="extension">新扩展名（带点）。</param>
    /// <returns>更改扩展名后的路径。</returns>
    public static string ChangeExtension(this string path, string extension) =>
        Path.ChangeExtension(path, extension);

    /// <summary>
    /// 获取路径的各级目录（分解为数组）。
    /// </summary>
    /// <param name="path">待处理的路径。</param>
    /// <returns>各级目录数组。</returns>
    public static string[] SplitDirectories(this string path)
    {
        if (string.IsNullOrEmpty(path)) return Array.Empty<string>();
        return path.Split(new[] { Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar }, StringSplitOptions.RemoveEmptyEntries);
    }

    /// <summary>
    /// 获取路径的父目录路径。
    /// </summary>
    /// <param name="path">待处理的路径。</param>
    /// <returns>父目录路径。</returns>
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
    /// <returns>如果为根目录返回 true；否则返回 false。</returns>
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
    /// <returns>规范化路径。</returns>
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
    /// <returns>相对路径。</returns>
    public static string GetRelativePath(this string path, string basePath)
    {
        if (string.IsNullOrEmpty(path) || string.IsNullOrEmpty(basePath)) return path;
        return Path.GetRelativePath(basePath, path);
    }

    #endregion

    #region 目录操作

    /// <summary>
    /// 创建目录（如果不存在）。
    /// </summary>
    /// <param name="directoryPath">目录路径。</param>
    /// <returns>创建的目录信息。</returns>
    public static DirectoryInfo CreateDirectory(this string directoryPath)
    {
        if (string.IsNullOrEmpty(directoryPath)) return null;
        return Directory.CreateDirectory(directoryPath);
    }

    /// <summary>
    /// 删除目录。
    /// </summary>
    /// <param name="directoryPath">目录路径。</param>
    /// <param name="recursive">是否递归删除子目录和文件。</param>
    public static void DeleteDirectory(this string directoryPath, bool recursive = true)
    {
        if (Directory.Exists(directoryPath))
            Directory.Delete(directoryPath, recursive);
    }

    /// <summary>
    /// 获取目录下的所有文件。
    /// </summary>
    /// <param name="directoryPath">目录路径。</param>
    /// <param name="searchPattern">搜索模式，默认 "*"。</param>
    /// <param name="searchOption">搜索选项。</param>
    /// <returns>文件路径数组。</returns>
    public static string[] GetFiles(this string directoryPath, string searchPattern = "*", SearchOption searchOption = SearchOption.TopDirectoryOnly)
    {
        if (!Directory.Exists(directoryPath)) return Array.Empty<string>();
        return Directory.GetFiles(directoryPath, searchPattern, searchOption);
    }

    /// <summary>
    /// 获取目录下的所有子目录。
    /// </summary>
    /// <param name="directoryPath">目录路径。</param>
    /// <param name="searchPattern">搜索模式，默认 "*"。</param>
    /// <param name="searchOption">搜索选项。</param>
    /// <returns>目录路径数组。</returns>
    public static string[] GetDirectories(this string directoryPath, string searchPattern = "*", SearchOption searchOption = SearchOption.TopDirectoryOnly)
    {
        if (!Directory.Exists(directoryPath)) return Array.Empty<string>();
        return Directory.GetDirectories(directoryPath, searchPattern, searchOption);
    }

    /// <summary>
    /// 获取目录大小（字节）。
    /// </summary>
    /// <param name="directoryPath">目录路径。</param>
    /// <param name="searchPattern">搜索模式，默认 "*"。</param>
    /// <returns>目录大小（字节）。</returns>
    public static long GetDirectorySize(this string directoryPath, string searchPattern = "*")
    {
        if (!Directory.Exists(directoryPath)) return 0;
        var dirInfo = new DirectoryInfo(directoryPath);
        return dirInfo.EnumerateFiles(searchPattern, SearchOption.AllDirectories).Sum(file => file.Length);
    }

    /// <summary>
    /// 判断目录是否为空。
    /// </summary>
    /// <param name="directoryPath">目录路径。</param>
    /// <returns>如果目录为空返回 true；否则返回 false。</returns>
    public static bool IsDirectoryEmpty(this string directoryPath)
    {
        if (!Directory.Exists(directoryPath)) return true;
        return !Directory.EnumerateFileSystemEntries(directoryPath).Any();
    }

    /// <summary>
    /// 复制目录。
    /// </summary>
    /// <param name="sourcePath">源目录路径。</param>
    /// <param name="destPath">目标目录路径。</param>
    /// <param name="overwrite">是否覆盖已存在的文件。</param>
    public static void CopyDirectory(this string sourcePath, string destPath, bool overwrite = true)
    {
        if (!Directory.Exists(sourcePath)) return;
        Directory.CreateDirectory(destPath);
        foreach (var file in Directory.GetFiles(sourcePath))
        {
            var fileName = Path.GetFileName(file);
            var destFile = Path.Combine(destPath, fileName);
            File.Copy(file, destFile, overwrite);
        }
        foreach (var dir in Directory.GetDirectories(sourcePath))
        {
            var dirName = Path.GetFileName(dir);
            var destDir = Path.Combine(destPath, dirName);
            CopyDirectory(dir, destDir, overwrite);
        }
    }

    /// <summary>
    /// 移动目录。
    /// </summary>
    /// <param name="sourcePath">源目录路径。</param>
    /// <param name="destPath">目标目录路径。</param>
    public static void MoveDirectory(this string sourcePath, string destPath)
    {
        if (!Directory.Exists(sourcePath)) return;
        Directory.Move(sourcePath, destPath);
    }

    #endregion

    #region 辅助方法

    /// <summary>
    /// 确保文件所在目录存在。
    /// </summary>
    /// <param name="filePath">文件路径。</param>
    private static void EnsureDirectoryExists(string filePath)
    {
        if (string.IsNullOrEmpty(filePath)) return;
        var directory = Path.GetDirectoryName(filePath);
        if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
            Directory.CreateDirectory(directory);
    }

    /// <summary>
    /// 获取友好的文件大小描述。
    /// </summary>
    /// <param name="bytes">字节数。</param>
    /// <returns>友好大小描述。</returns>
    public static string GetFriendlyFileSizeString(this long bytes)
    {
        string[] sizes = { "B", "KB", "MB", "GB", "TB", "PB" };
        int order = 0;
        double size = bytes;
        while (size >= 1024 && order < sizes.Length - 1)
        {
            order++;
            size /= 1024;
        }
        return $"{size:0.##} {sizes[order]}";
    }

    #endregion
}
