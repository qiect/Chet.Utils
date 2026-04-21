using System.Text;

namespace Chet.Utils;

/// <summary>
/// Stream 扩展方法类，提供常用的读取、写入、转换、判断、操作等功能。
/// </summary>
/// <remarks>
/// <para>本类提供丰富的流扩展方法，涵盖以下功能：</para>
/// <list type="bullet">
///   <item><description>基础判断：CanReadSafe、CanWriteSafe、CanSeekSafe、IsNullOrEmpty、IsNullStream</description></item>
///   <item><description>类型判断：IsMemoryStream、IsFileStream、IsNetworkStream</description></item>
///   <item><description>同步读取：ToBytes、ToText、ReadBytes、ReadText、ReadLine</description></item>
///   <item><description>异步读取：ToBytesAsync、ToTextAsync、ReadBytesAsync、ReadTextAsync</description></item>
///   <item><description>同步写入：WriteText、WriteBytes、WriteLine、WriteStream</description></item>
///   <item><description>异步写入：WriteTextAsync、WriteBytesAsync、WriteLineAsync、WriteStreamAsync</description></item>
///   <item><description>文件操作：SaveToFile、SaveToFileAsync、ToFileStream</description></item>
///   <item><description>流操作：CopyToStream、CopyToStreamAsync、ResetPosition、SetPosition</description></item>
///   <item><description>转换操作：ToStream（字符串/字节数组转流）、ToMemoryStream</description></item>
///   <item><description>位置操作：GetPosition、GetLength、SeekToStart、SeekToEnd</description></item>
///   <item><description>哈希计算：ComputeMd5、ComputeSha256、ComputeHash</description></item>
///   <item><description>压缩解压：Compress、Decompress、CompressAsync、DecompressAsync</description></item>
/// </list>
/// </remarks>
public static class StreamExtensions
{
    #region 基础判断

    /// <summary>
    /// 判断流是否可读。
    /// </summary>
    /// <param name="stream">待判断的流。</param>
    /// <returns>如果流可读返回 true；否则返回 false。</returns>
    /// <example>
    /// <code>
    /// using var stream = new MemoryStream();
    /// var canRead = stream.CanReadSafe(); // true
    /// </code>
    /// </example>
    public static bool CanReadSafe(this Stream stream) => stream != null && stream.CanRead;

    /// <summary>
    /// 判断流是否可写。
    /// </summary>
    /// <param name="stream">待判断的流。</param>
    /// <returns>如果流可写返回 true；否则返回 false。</returns>
    public static bool CanWriteSafe(this Stream stream) => stream != null && stream.CanWrite;

    /// <summary>
    /// 判断流是否可查找（支持 Seek）。
    /// </summary>
    /// <param name="stream">待判断的流。</param>
    /// <returns>如果流可查找返回 true；否则返回 false。</returns>
    public static bool CanSeekSafe(this Stream stream) => stream != null && stream.CanSeek;

    /// <summary>
    /// 判断流是否为空或长度为零。
    /// </summary>
    /// <param name="stream">待判断的流。</param>
    /// <returns>如果流为空或长度为零返回 true；否则返回 false。</returns>
    public static bool IsNullOrEmpty(this Stream stream) => stream == null || stream.Length == 0;

    /// <summary>
    /// 判断流是否不为空且长度大于零。
    /// </summary>
    /// <param name="stream">待判断的流。</param>
    /// <returns>如果流不为空且长度大于零返回 true；否则返回 false。</returns>
    public static bool IsNotNullOrEmpty(this Stream stream) => stream != null && stream.Length > 0;

    /// <summary>
    /// 判断流是否为空流（Stream.Null）。
    /// </summary>
    /// <param name="stream">待判断的流。</param>
    /// <returns>如果流为空流返回 true；否则返回 false。</returns>
    public static bool IsNullStream(this Stream stream) => stream == Stream.Null;

    #endregion

    #region 类型判断

    /// <summary>
    /// 判断流是否为 MemoryStream。
    /// </summary>
    /// <param name="stream">待判断的流。</param>
    /// <returns>如果流为 MemoryStream 返回 true；否则返回 false。</returns>
    public static bool IsMemoryStream(this Stream stream) => stream is MemoryStream;

    /// <summary>
    /// 判断流是否为 FileStream。
    /// </summary>
    /// <param name="stream">待判断的流。</param>
    /// <returns>如果流为 FileStream 返回 true；否则返回 false。</returns>
    public static bool IsFileStream(this Stream stream) => stream is FileStream;

    /// <summary>
    /// 判断流是否为 NetworkStream。
    /// </summary>
    /// <param name="stream">待判断的流。</param>
    /// <returns>如果流为 NetworkStream 返回 true；否则返回 false。</returns>
    public static bool IsNetworkStream(this Stream stream) => stream is System.Net.Sockets.NetworkStream;

    /// <summary>
    /// 判断流是否为 BufferedStream。
    /// </summary>
    /// <param name="stream">待判断的流。</param>
    /// <returns>如果流为 BufferedStream 返回 true；否则返回 false。</returns>
    public static bool IsBufferedStream(this Stream stream) => stream is BufferedStream;

    #endregion

    #region 同步读取

    /// <summary>
    /// 将流内容读取为字节数组。
    /// </summary>
    /// <param name="stream">待读取的流。</param>
    /// <returns>流内容的字节数组。</returns>
    /// <example>
    /// <code>
    /// using var stream = new MemoryStream(Encoding.UTF8.GetBytes("Hello World"));
    /// var bytes = stream.ToBytes();
    /// </code>
    /// </example>
    public static byte[] ToBytes(this Stream stream)
    {
        if (stream == null) return Array.Empty<byte>();
        if (stream is MemoryStream ms)
            return ms.ToArray();
        long originalPosition = 0;
        if (stream.CanSeek)
        {
            originalPosition = stream.Position;
            stream.Position = 0;
        }
        using var memory = new MemoryStream();
        stream.CopyTo(memory);
        if (stream.CanSeek)
            stream.Position = originalPosition;
        return memory.ToArray();
    }

    /// <summary>
    /// 将流内容读取为字符串（默认 UTF8 编码）。
    /// </summary>
    /// <param name="stream">待读取的流。</param>
    /// <param name="encoding">编码，默认 UTF8。</param>
    /// <returns>流内容的字符串。</returns>
    /// <example>
    /// <code>
    /// using var stream = new MemoryStream(Encoding.UTF8.GetBytes("Hello World"));
    /// var text = stream.ToText(); // "Hello World"
    /// </code>
    /// </example>
    public static string ToText(this Stream stream, Encoding encoding = null)
    {
        encoding ??= Encoding.UTF8;
        var bytes = stream.ToBytes();
        return encoding.GetString(bytes);
    }

    /// <summary>
    /// 读取流的部分内容为字节数组。
    /// </summary>
    /// <param name="stream">源流。</param>
    /// <param name="offset">起始偏移。</param>
    /// <param name="count">读取长度。</param>
    /// <returns>读取的字节数组。</returns>
    public static byte[] ReadBytes(this Stream stream, int offset, int count)
    {
        if (stream == null || !stream.CanRead || offset < 0 || count <= 0 || offset >= stream.Length)
            return Array.Empty<byte>();
        var buffer = new byte[count];
        long originalPosition = stream.CanSeek ? stream.Position : 0;
        if (stream.CanSeek) stream.Position = offset;
        int read = stream.Read(buffer, 0, count);
        if (stream.CanSeek) stream.Position = originalPosition;
        if (read < count)
        {
            var result = new byte[read];
            Array.Copy(buffer, result, read);
            return result;
        }
        return buffer;
    }

    /// <summary>
    /// 读取流的部分内容为字符串（默认 UTF8 编码）。
    /// </summary>
    /// <param name="stream">源流。</param>
    /// <param name="offset">起始偏移。</param>
    /// <param name="count">读取长度。</param>
    /// <param name="encoding">编码，默认 UTF8。</param>
    /// <returns>读取的字符串。</returns>
    public static string ReadText(this Stream stream, int offset, int count, Encoding encoding = null)
    {
        encoding ??= Encoding.UTF8;
        var bytes = stream.ReadBytes(offset, count);
        return encoding.GetString(bytes);
    }

    /// <summary>
    /// 读取流的一行内容（按换行符分割）。
    /// </summary>
    /// <param name="stream">源流。</param>
    /// <param name="encoding">编码，默认 UTF8。</param>
    /// <returns>读取的一行内容。</returns>
    public static string ReadLine(this Stream stream, Encoding encoding = null)
    {
        encoding ??= Encoding.UTF8;
        using var reader = new StreamReader(stream, encoding, leaveOpen: true);
        return reader.ReadLine();
    }

    /// <summary>
    /// 读取流的所有行内容。
    /// </summary>
    /// <param name="stream">源流。</param>
    /// <param name="encoding">编码，默认 UTF8。</param>
    /// <returns>所有行的列表。</returns>
    public static List<string> ReadAllLines(this Stream stream, Encoding encoding = null)
    {
        encoding ??= Encoding.UTF8;
        var lines = new List<string>();
        using var reader = new StreamReader(stream, encoding, leaveOpen: true);
        while (!reader.EndOfStream)
        {
            var line = reader.ReadLine();
            if (line != null)
                lines.Add(line);
        }
        return lines;
    }

    #endregion

    #region 异步读取

    /// <summary>
    /// 异步将流内容读取为字节数组。
    /// </summary>
    /// <param name="stream">待读取的流。</param>
    /// <param name="cancellationToken">取消令牌。</param>
    /// <returns>流内容的字节数组。</returns>
    /// <example>
    /// <code>
    /// using var stream = new MemoryStream(Encoding.UTF8.GetBytes("Hello World"));
    /// var bytes = await stream.ToBytesAsync();
    /// </code>
    /// </example>
    public static async Task<byte[]> ToBytesAsync(this Stream stream, CancellationToken cancellationToken = default)
    {
        if (stream == null) return Array.Empty<byte>();
        if (stream is MemoryStream ms)
            return ms.ToArray();
        long originalPosition = 0;
        if (stream.CanSeek)
        {
            originalPosition = stream.Position;
            stream.Position = 0;
        }
        using var memory = new MemoryStream();
        await stream.CopyToAsync(memory, cancellationToken);
        if (stream.CanSeek)
            stream.Position = originalPosition;
        return memory.ToArray();
    }

    /// <summary>
    /// 异步将流内容读取为字符串（默认 UTF8 编码）。
    /// </summary>
    /// <param name="stream">待读取的流。</param>
    /// <param name="encoding">编码，默认 UTF8。</param>
    /// <param name="cancellationToken">取消令牌。</param>
    /// <returns>流内容的字符串。</returns>
    public static async Task<string> ToTextAsync(this Stream stream, Encoding encoding = null, CancellationToken cancellationToken = default)
    {
        encoding ??= Encoding.UTF8;
        var bytes = await stream.ToBytesAsync(cancellationToken);
        return encoding.GetString(bytes);
    }

    /// <summary>
    /// 异步读取流的部分内容为字节数组。
    /// </summary>
    /// <param name="stream">源流。</param>
    /// <param name="offset">起始偏移。</param>
    /// <param name="count">读取长度。</param>
    /// <param name="cancellationToken">取消令牌。</param>
    /// <returns>读取的字节数组。</returns>
    public static async Task<byte[]> ReadBytesAsync(this Stream stream, int offset, int count, CancellationToken cancellationToken = default)
    {
        if (stream == null || !stream.CanRead || offset < 0 || count <= 0 || offset >= stream.Length)
            return Array.Empty<byte>();
        var buffer = new byte[count];
        long originalPosition = stream.CanSeek ? stream.Position : 0;
        if (stream.CanSeek) stream.Position = offset;
        int read = await stream.ReadAsync(buffer, 0, count, cancellationToken);
        if (stream.CanSeek) stream.Position = originalPosition;
        if (read < count)
        {
            var result = new byte[read];
            Array.Copy(buffer, result, read);
            return result;
        }
        return buffer;
    }

    /// <summary>
    /// 异步读取流的部分内容为字符串（默认 UTF8 编码）。
    /// </summary>
    /// <param name="stream">源流。</param>
    /// <param name="offset">起始偏移。</param>
    /// <param name="count">读取长度。</param>
    /// <param name="encoding">编码，默认 UTF8。</param>
    /// <param name="cancellationToken">取消令牌。</param>
    /// <returns>读取的字符串。</returns>
    public static async Task<string> ReadTextAsync(this Stream stream, int offset, int count, Encoding encoding = null, CancellationToken cancellationToken = default)
    {
        encoding ??= Encoding.UTF8;
        var bytes = await stream.ReadBytesAsync(offset, count, cancellationToken);
        return encoding.GetString(bytes);
    }

    /// <summary>
    /// 异步读取流的一行内容（按换行符分割）。
    /// </summary>
    /// <param name="stream">源流。</param>
    /// <param name="encoding">编码，默认 UTF8。</param>
    /// <param name="cancellationToken">取消令牌。</param>
    /// <returns>读取的一行内容。</returns>
    public static async Task<string> ReadLineAsync(this Stream stream, Encoding encoding = null, CancellationToken cancellationToken = default)
    {
        encoding ??= Encoding.UTF8;
        using var reader = new StreamReader(stream, encoding, leaveOpen: true);
        return await reader.ReadLineAsync();
    }

    /// <summary>
    /// 异步读取流的所有行内容。
    /// </summary>
    /// <param name="stream">源流。</param>
    /// <param name="encoding">编码，默认 UTF8。</param>
    /// <param name="cancellationToken">取消令牌。</param>
    /// <returns>所有行的列表。</returns>
    public static async Task<List<string>> ReadAllLinesAsync(this Stream stream, Encoding encoding = null, CancellationToken cancellationToken = default)
    {
        encoding ??= Encoding.UTF8;
        var lines = new List<string>();
        using var reader = new StreamReader(stream, encoding, leaveOpen: true);
        while (!reader.EndOfStream)
        {
            var line = await reader.ReadLineAsync();
            if (line != null)
                lines.Add(line);
            cancellationToken.ThrowIfCancellationRequested();
        }
        return lines;
    }

    #endregion

    #region 同步写入

    /// <summary>
    /// 将字符串写入流（覆盖原内容，默认 UTF8 编码）。
    /// </summary>
    /// <param name="stream">目标流。</param>
    /// <param name="text">写入内容。</param>
    /// <param name="encoding">编码，默认 UTF8。</param>
    /// <example>
    /// <code>
    /// using var stream = new MemoryStream();
    /// stream.WriteText("Hello World");
    /// </code>
    /// </example>
    public static void WriteText(this Stream stream, string text, Encoding encoding = null)
    {
        if (stream == null || !stream.CanWrite) return;
        encoding ??= Encoding.UTF8;
        var bytes = encoding.GetBytes(text ?? string.Empty);
        stream.SetLength(0);
        stream.Write(bytes, 0, bytes.Length);
        stream.Flush();
    }

    /// <summary>
    /// 将字节数组写入流（覆盖原内容）。
    /// </summary>
    /// <param name="stream">目标流。</param>
    /// <param name="bytes">写入字节数组。</param>
    public static void WriteBytes(this Stream stream, byte[] bytes)
    {
        if (stream == null || !stream.CanWrite || bytes == null) return;
        stream.SetLength(0);
        stream.Write(bytes, 0, bytes.Length);
        stream.Flush();
    }

    /// <summary>
    /// 向流追加写入字符串（默认 UTF8 编码）。
    /// </summary>
    /// <param name="stream">目标流。</param>
    /// <param name="text">追加内容。</param>
    /// <param name="encoding">编码，默认 UTF8。</param>
    public static void AppendText(this Stream stream, string text, Encoding encoding = null)
    {
        if (stream == null || !stream.CanWrite) return;
        encoding ??= Encoding.UTF8;
        var bytes = encoding.GetBytes(text ?? string.Empty);
        if (stream.CanSeek)
            stream.Position = stream.Length;
        stream.Write(bytes, 0, bytes.Length);
        stream.Flush();
    }

    /// <summary>
    /// 向流追加写入字节数组。
    /// </summary>
    /// <param name="stream">目标流。</param>
    /// <param name="bytes">追加字节数组。</param>
    public static void AppendBytes(this Stream stream, byte[] bytes)
    {
        if (stream == null || !stream.CanWrite || bytes == null) return;
        if (stream.CanSeek)
            stream.Position = stream.Length;
        stream.Write(bytes, 0, bytes.Length);
        stream.Flush();
    }

    /// <summary>
    /// 写入一行文本（带换行符）。
    /// </summary>
    /// <param name="stream">目标流。</param>
    /// <param name="text">写入内容。</param>
    /// <param name="encoding">编码，默认 UTF8。</param>
    public static void WriteLine(this Stream stream, string text, Encoding encoding = null)
    {
        encoding ??= Encoding.UTF8;
        stream.AppendText(text + Environment.NewLine, encoding);
    }

    /// <summary>
    /// 写入多行文本。
    /// </summary>
    /// <param name="stream">目标流。</param>
    /// <param name="lines">写入行内容。</param>
    /// <param name="encoding">编码，默认 UTF8。</param>
    public static void WriteLines(this Stream stream, IEnumerable<string> lines, Encoding encoding = null)
    {
        if (lines == null) return;
        foreach (var line in lines)
        {
            stream.WriteLine(line, encoding);
        }
    }

    /// <summary>
    /// 将另一个流的内容写入当前流。
    /// </summary>
    /// <param name="stream">目标流。</param>
    /// <param name="source">源流。</param>
    public static void WriteStream(this Stream stream, Stream source)
    {
        if (stream == null || !stream.CanWrite || source == null || !source.CanRead) return;
        if (source.CanSeek)
            source.Position = 0;
        source.CopyTo(stream);
        stream.Flush();
    }

    #endregion

    #region 异步写入

    /// <summary>
    /// 异步将字符串写入流（覆盖原内容，默认 UTF8 编码）。
    /// </summary>
    /// <param name="stream">目标流。</param>
    /// <param name="text">写入内容。</param>
    /// <param name="encoding">编码，默认 UTF8。</param>
    /// <param name="cancellationToken">取消令牌。</param>
    public static async Task WriteTextAsync(this Stream stream, string text, Encoding encoding = null, CancellationToken cancellationToken = default)
    {
        if (stream == null || !stream.CanWrite) return;
        encoding ??= Encoding.UTF8;
        var bytes = encoding.GetBytes(text ?? string.Empty);
        stream.SetLength(0);
        await stream.WriteAsync(bytes, 0, bytes.Length, cancellationToken);
        await stream.FlushAsync(cancellationToken);
    }

    /// <summary>
    /// 异步将字节数组写入流（覆盖原内容）。
    /// </summary>
    /// <param name="stream">目标流。</param>
    /// <param name="bytes">写入字节数组。</param>
    /// <param name="cancellationToken">取消令牌。</param>
    public static async Task WriteBytesAsync(this Stream stream, byte[] bytes, CancellationToken cancellationToken = default)
    {
        if (stream == null || !stream.CanWrite || bytes == null) return;
        stream.SetLength(0);
        await stream.WriteAsync(bytes, 0, bytes.Length, cancellationToken);
        await stream.FlushAsync(cancellationToken);
    }

    /// <summary>
    /// 异步向流追加写入字符串（默认 UTF8 编码）。
    /// </summary>
    /// <param name="stream">目标流。</param>
    /// <param name="text">追加内容。</param>
    /// <param name="encoding">编码，默认 UTF8。</param>
    /// <param name="cancellationToken">取消令牌。</param>
    public static async Task AppendTextAsync(this Stream stream, string text, Encoding encoding = null, CancellationToken cancellationToken = default)
    {
        if (stream == null || !stream.CanWrite) return;
        encoding ??= Encoding.UTF8;
        var bytes = encoding.GetBytes(text ?? string.Empty);
        if (stream.CanSeek)
            stream.Position = stream.Length;
        await stream.WriteAsync(bytes, 0, bytes.Length, cancellationToken);
        await stream.FlushAsync(cancellationToken);
    }

    /// <summary>
    /// 异步向流追加写入字节数组。
    /// </summary>
    /// <param name="stream">目标流。</param>
    /// <param name="bytes">追加字节数组。</param>
    /// <param name="cancellationToken">取消令牌。</param>
    public static async Task AppendBytesAsync(this Stream stream, byte[] bytes, CancellationToken cancellationToken = default)
    {
        if (stream == null || !stream.CanWrite || bytes == null) return;
        if (stream.CanSeek)
            stream.Position = stream.Length;
        await stream.WriteAsync(bytes, 0, bytes.Length, cancellationToken);
        await stream.FlushAsync(cancellationToken);
    }

    /// <summary>
    /// 异步写入一行文本（带换行符）。
    /// </summary>
    /// <param name="stream">目标流。</param>
    /// <param name="text">写入内容。</param>
    /// <param name="encoding">编码，默认 UTF8。</param>
    /// <param name="cancellationToken">取消令牌。</param>
    public static async Task WriteLineAsync(this Stream stream, string text, Encoding encoding = null, CancellationToken cancellationToken = default)
    {
        encoding ??= Encoding.UTF8;
        await stream.AppendTextAsync(text + Environment.NewLine, encoding, cancellationToken);
    }

    /// <summary>
    /// 异步写入多行文本。
    /// </summary>
    /// <param name="stream">目标流。</param>
    /// <param name="lines">写入行内容。</param>
    /// <param name="encoding">编码，默认 UTF8。</param>
    /// <param name="cancellationToken">取消令牌。</param>
    public static async Task WriteLinesAsync(this Stream stream, IEnumerable<string> lines, Encoding encoding = null, CancellationToken cancellationToken = default)
    {
        if (lines == null) return;
        foreach (var line in lines)
        {
            await stream.WriteLineAsync(line, encoding, cancellationToken);
            cancellationToken.ThrowIfCancellationRequested();
        }
    }

    /// <summary>
    /// 异步将另一个流的内容写入当前流。
    /// </summary>
    /// <param name="stream">目标流。</param>
    /// <param name="source">源流。</param>
    /// <param name="cancellationToken">取消令牌。</param>
    public static async Task WriteStreamAsync(this Stream stream, Stream source, CancellationToken cancellationToken = default)
    {
        if (stream == null || !stream.CanWrite || source == null || !source.CanRead) return;
        if (source.CanSeek)
            source.Position = 0;
        await source.CopyToAsync(stream, cancellationToken);
        await stream.FlushAsync(cancellationToken);
    }

    #endregion

    #region 文件操作

    /// <summary>
    /// 将流内容保存到文件（覆盖）。
    /// </summary>
    /// <param name="stream">源流。</param>
    /// <param name="filePath">目标文件路径。</param>
    /// <example>
    /// <code>
    /// using var stream = new MemoryStream(Encoding.UTF8.GetBytes("Hello World"));
    /// stream.SaveToFile("output.txt");
    /// </code>
    /// </example>
    public static void SaveToFile(this Stream stream, string filePath)
    {
        if (stream == null || string.IsNullOrEmpty(filePath)) return;
        var directory = Path.GetDirectoryName(filePath);
        if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
            Directory.CreateDirectory(directory);
        using var file = new FileStream(filePath, FileMode.Create, FileAccess.Write);
        if (stream.CanSeek)
            stream.Position = 0;
        stream.CopyTo(file);
    }

    /// <summary>
    /// 异步将流内容保存到文件（覆盖）。
    /// </summary>
    /// <param name="stream">源流。</param>
    /// <param name="filePath">目标文件路径。</param>
    /// <param name="cancellationToken">取消令牌。</param>
    public static async Task SaveToFileAsync(this Stream stream, string filePath, CancellationToken cancellationToken = default)
    {
        if (stream == null || string.IsNullOrEmpty(filePath)) return;
        var directory = Path.GetDirectoryName(filePath);
        if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
            Directory.CreateDirectory(directory);
        using var file = new FileStream(filePath, FileMode.Create, FileAccess.Write);
        if (stream.CanSeek)
            stream.Position = 0;
        await stream.CopyToAsync(file, cancellationToken);
    }

    /// <summary>
    /// 将流内容追加到文件。
    /// </summary>
    /// <param name="stream">源流。</param>
    /// <param name="filePath">目标文件路径。</param>
    public static void AppendToFile(this Stream stream, string filePath)
    {
        if (stream == null || string.IsNullOrEmpty(filePath)) return;
        var directory = Path.GetDirectoryName(filePath);
        if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
            Directory.CreateDirectory(directory);
        using var file = new FileStream(filePath, FileMode.Append, FileAccess.Write);
        if (stream.CanSeek)
            stream.Position = 0;
        stream.CopyTo(file);
    }

    /// <summary>
    /// 异步将流内容追加到文件。
    /// </summary>
    /// <param name="stream">源流。</param>
    /// <param name="filePath">目标文件路径。</param>
    /// <param name="cancellationToken">取消令牌。</param>
    public static async Task AppendToFileAsync(this Stream stream, string filePath, CancellationToken cancellationToken = default)
    {
        if (stream == null || string.IsNullOrEmpty(filePath)) return;
        var directory = Path.GetDirectoryName(filePath);
        if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
            Directory.CreateDirectory(directory);
        using var file = new FileStream(filePath, FileMode.Append, FileAccess.Write);
        if (stream.CanSeek)
            stream.Position = 0;
        await stream.CopyToAsync(file, cancellationToken);
    }

    /// <summary>
    /// 将文件内容读取为流。
    /// </summary>
    /// <param name="filePath">文件路径。</param>
    /// <returns>文件内容的流。</returns>
    public static Stream ToFileStream(this string filePath)
    {
        if (string.IsNullOrEmpty(filePath) || !File.Exists(filePath)) return Stream.Null;
        return new FileStream(filePath, FileMode.Open, FileAccess.Read);
    }

    /// <summary>
    /// 异步将文件内容读取为流。
    /// </summary>
    /// <param name="filePath">文件路径。</param>
    /// <param name="cancellationToken">取消令牌。</param>
    /// <returns>文件内容的流。</returns>
    public static async Task<Stream> ToFileStreamAsync(this string filePath, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrEmpty(filePath) || !File.Exists(filePath)) return Stream.Null;
        return await Task.Run(() => new FileStream(filePath, FileMode.Open, FileAccess.Read), cancellationToken);
    }

    #endregion

    #region 流操作

    /// <summary>
    /// 将流内容复制到另一个流。
    /// </summary>
    /// <param name="stream">源流。</param>
    /// <param name="target">目标流。</param>
    public static void CopyToStream(this Stream stream, Stream target)
    {
        if (stream == null || target == null) return;
        if (stream.CanSeek)
            stream.Position = 0;
        stream.CopyTo(target);
        target.Flush();
    }

    /// <summary>
    /// 异步将流内容复制到另一个流。
    /// </summary>
    /// <param name="stream">源流。</param>
    /// <param name="target">目标流。</param>
    /// <param name="cancellationToken">取消令牌。</param>
    public static async Task CopyToStreamAsync(this Stream stream, Stream target, CancellationToken cancellationToken = default)
    {
        if (stream == null || target == null) return;
        if (stream.CanSeek)
            stream.Position = 0;
        await stream.CopyToAsync(target, cancellationToken);
        await target.FlushAsync(cancellationToken);
    }

    /// <summary>
    /// 重置流到起始位置。
    /// </summary>
    /// <param name="stream">待重置的流。</param>
    public static void ResetPosition(this Stream stream)
    {
        if (stream != null && stream.CanSeek)
            stream.Position = 0;
    }

    /// <summary>
    /// 获取流的当前位置。
    /// </summary>
    /// <param name="stream">待处理的流。</param>
    /// <returns>当前位置，如果流不可查找则返回 0。</returns>
    public static long GetPosition(this Stream stream) => stream?.CanSeek == true ? stream.Position : 0;

    /// <summary>
    /// 设置流的当前位置。
    /// </summary>
    /// <param name="stream">待处理的流。</param>
    /// <param name="position">目标位置。</param>
    public static void SetPosition(this Stream stream, long position)
    {
        if (stream != null && stream.CanSeek && position >= 0 && position <= stream.Length)
            stream.Position = position;
    }

    /// <summary>
    /// 获取流的长度（字节）。
    /// </summary>
    /// <param name="stream">待处理的流。</param>
    /// <returns>流的长度，如果流为空则返回 0。</returns>
    public static long GetLength(this Stream stream) => stream?.Length ?? 0;

    /// <summary>
    /// 将流定位到起始位置。
    /// </summary>
    /// <param name="stream">待处理的流。</param>
    public static void SeekToStart(this Stream stream)
    {
        if (stream != null && stream.CanSeek)
            stream.Seek(0, SeekOrigin.Begin);
    }

    /// <summary>
    /// 将流定位到结束位置。
    /// </summary>
    /// <param name="stream">待处理的流。</param>
    public static void SeekToEnd(this Stream stream)
    {
        if (stream != null && stream.CanSeek)
            stream.Seek(0, SeekOrigin.End);
    }

    /// <summary>
    /// 将流定位到指定偏移位置。
    /// </summary>
    /// <param name="stream">待处理的流。</param>
    /// <param name="offset">偏移量。</param>
    /// <param name="origin">定位起点。</param>
    public static void SeekTo(this Stream stream, long offset, SeekOrigin origin = SeekOrigin.Begin)
    {
        if (stream != null && stream.CanSeek)
            stream.Seek(offset, origin);
    }

    #endregion

    #region 转换操作

    /// <summary>
    /// 将字符串转换为流（默认 UTF8 编码）。
    /// </summary>
    /// <param name="text">字符串内容。</param>
    /// <param name="encoding">编码，默认 UTF8。</param>
    /// <returns>包含字符串内容的流。</returns>
    /// <example>
    /// <code>
    /// var stream = "Hello World".ToStream();
    /// </code>
    /// </example>
    public static Stream ToStream(this string text, Encoding encoding = null)
    {
        encoding ??= Encoding.UTF8;
        var bytes = encoding.GetBytes(text ?? string.Empty);
        return new MemoryStream(bytes);
    }

    /// <summary>
    /// 将字节数组转换为流。
    /// </summary>
    /// <param name="bytes">字节数组。</param>
    /// <returns>包含字节数组内容的流。</returns>
    public static Stream ToStream(this byte[] bytes)
    {
        if (bytes == null) return Stream.Null;
        return new MemoryStream(bytes);
    }

    /// <summary>
    /// 将流转换为 MemoryStream。
    /// </summary>
    /// <param name="stream">源流。</param>
    /// <returns>MemoryStream 实例。</returns>
    public static MemoryStream ToMemoryStream(this Stream stream)
    {
        if (stream == null) return new MemoryStream();
        if (stream is MemoryStream ms) return ms;
        var bytes = stream.ToBytes();
        return new MemoryStream(bytes);
    }

    /// <summary>
    /// 将流转换为 BufferedStream。
    /// </summary>
    /// <param name="stream">源流。</param>
    /// <param name="bufferSize">缓冲区大小，默认 4096。</param>
    /// <returns>BufferedStream 实例。</returns>
    public static BufferedStream ToBufferedStream(this Stream stream, int bufferSize = 4096)
    {
        if (stream == null) return new BufferedStream(new MemoryStream(), bufferSize);
        return new BufferedStream(stream, bufferSize);
    }

    #endregion

    #region 哈希计算

    /// <summary>
    /// 计算流的 MD5 哈希值。
    /// </summary>
    /// <param name="stream">源流。</param>
    /// <returns>MD5 哈希值（32位小写十六进制字符串）。</returns>
    public static string ComputeMd5(this Stream stream)
    {
        if (stream == null) return string.Empty;
        using var md5 = System.Security.Cryptography.MD5.Create();
        if (stream.CanSeek)
            stream.Position = 0;
        var hash = md5.ComputeHash(stream);
        return BitConverter.ToString(hash).Replace("-", "").ToLowerInvariant();
    }

    /// <summary>
    /// 异步计算流的 MD5 哈希值。
    /// </summary>
    /// <param name="stream">源流。</param>
    /// <param name="cancellationToken">取消令牌。</param>
    /// <returns>MD5 哈希值（32位小写十六进制字符串）。</returns>
    public static async Task<string> ComputeMd5Async(this Stream stream, CancellationToken cancellationToken = default)
    {
        if (stream == null) return string.Empty;
        using var md5 = System.Security.Cryptography.MD5.Create();
        if (stream.CanSeek)
            stream.Position = 0;
        var hash = await md5.ComputeHashAsync(stream, cancellationToken);
        return BitConverter.ToString(hash).Replace("-", "").ToLowerInvariant();
    }

    /// <summary>
    /// 计算流的 SHA256 哈希值。
    /// </summary>
    /// <param name="stream">源流。</param>
    /// <returns>SHA256 哈希值（64位小写十六进制字符串）。</returns>
    public static string ComputeSha256(this Stream stream)
    {
        if (stream == null) return string.Empty;
        using var sha = System.Security.Cryptography.SHA256.Create();
        if (stream.CanSeek)
            stream.Position = 0;
        var hash = sha.ComputeHash(stream);
        return BitConverter.ToString(hash).Replace("-", "").ToLowerInvariant();
    }

    /// <summary>
    /// 异步计算流的 SHA256 哈希值。
    /// </summary>
    /// <param name="stream">源流。</param>
    /// <param name="cancellationToken">取消令牌。</param>
    /// <returns>SHA256 哈希值（64位小写十六进制字符串）。</returns>
    public static async Task<string> ComputeSha256Async(this Stream stream, CancellationToken cancellationToken = default)
    {
        if (stream == null) return string.Empty;
        using var sha = System.Security.Cryptography.SHA256.Create();
        if (stream.CanSeek)
            stream.Position = 0;
        var hash = await sha.ComputeHashAsync(stream, cancellationToken);
        return BitConverter.ToString(hash).Replace("-", "").ToLowerInvariant();
    }

    /// <summary>
    /// 计算流的指定算法哈希值。
    /// </summary>
    /// <param name="stream">源流。</param>
    /// <param name="hashAlgorithm">哈希算法实例。</param>
    /// <returns>哈希值（小写十六进制字符串）。</returns>
    public static string ComputeHash(this Stream stream, System.Security.Cryptography.HashAlgorithm hashAlgorithm)
    {
        if (stream == null || hashAlgorithm == null) return string.Empty;
        if (stream.CanSeek)
            stream.Position = 0;
        var hash = hashAlgorithm.ComputeHash(stream);
        return BitConverter.ToString(hash).Replace("-", "").ToLowerInvariant();
    }

    /// <summary>
    /// 异步计算流的指定算法哈希值。
    /// </summary>
    /// <param name="stream">源流。</param>
    /// <param name="hashAlgorithm">哈希算法实例。</param>
    /// <param name="cancellationToken">取消令牌。</param>
    /// <returns>哈希值（小写十六进制字符串）。</returns>
    public static async Task<string> ComputeHashAsync(this Stream stream, System.Security.Cryptography.HashAlgorithm hashAlgorithm, CancellationToken cancellationToken = default)
    {
        if (stream == null || hashAlgorithm == null) return string.Empty;
        if (stream.CanSeek)
            stream.Position = 0;
        var hash = await hashAlgorithm.ComputeHashAsync(stream, cancellationToken);
        return BitConverter.ToString(hash).Replace("-", "").ToLowerInvariant();
    }

    #endregion

    #region 压缩解压

    /// <summary>
    /// 使用 GZip 压缩流。
    /// </summary>
    /// <param name="stream">源流。</param>
    /// <returns>压缩后的流。</returns>
    public static Stream Compress(this Stream stream)
    {
        if (stream == null) return Stream.Null;
        var bytes = stream.ToBytes();
        using var output = new MemoryStream();
        using (var gzip = new System.IO.Compression.GZipStream(output, System.IO.Compression.CompressionMode.Compress, leaveOpen: true))
        {
            gzip.Write(bytes, 0, bytes.Length);
        }
        return new MemoryStream(output.ToArray());
    }

    /// <summary>
    /// 异步使用 GZip 压缩流。
    /// </summary>
    /// <param name="stream">源流。</param>
    /// <param name="cancellationToken">取消令牌。</param>
    /// <returns>压缩后的流。</returns>
    public static async Task<Stream> CompressAsync(this Stream stream, CancellationToken cancellationToken = default)
    {
        if (stream == null) return Stream.Null;
        var bytes = await stream.ToBytesAsync(cancellationToken);
        using var output = new MemoryStream();
        using (var gzip = new System.IO.Compression.GZipStream(output, System.IO.Compression.CompressionMode.Compress, leaveOpen: true))
        {
            await gzip.WriteAsync(bytes, 0, bytes.Length, cancellationToken);
        }
        return new MemoryStream(output.ToArray());
    }

    /// <summary>
    /// 使用 GZip 解压流。
    /// </summary>
    /// <param name="stream">压缩流。</param>
    /// <returns>解压后的流。</returns>
    public static Stream Decompress(this Stream stream)
    {
        if (stream == null) return Stream.Null;
        if (stream.CanSeek)
            stream.Position = 0;
        using var gzip = new System.IO.Compression.GZipStream(stream, System.IO.Compression.CompressionMode.Decompress, leaveOpen: true);
        using var output = new MemoryStream();
        gzip.CopyTo(output);
        return new MemoryStream(output.ToArray());
    }

    /// <summary>
    /// 异步使用 GZip 解压流。
    /// </summary>
    /// <param name="stream">压缩流。</param>
    /// <param name="cancellationToken">取消令牌。</param>
    /// <returns>解压后的流。</returns>
    public static async Task<Stream> DecompressAsync(this Stream stream, CancellationToken cancellationToken = default)
    {
        if (stream == null) return Stream.Null;
        if (stream.CanSeek)
            stream.Position = 0;
        using var gzip = new System.IO.Compression.GZipStream(stream, System.IO.Compression.CompressionMode.Decompress, leaveOpen: true);
        using var output = new MemoryStream();
        await gzip.CopyToAsync(output, cancellationToken);
        return new MemoryStream(output.ToArray());
    }

    #endregion

    #region 其他操作

    /// <summary>
    /// 关闭并释放流。
    /// </summary>
    /// <param name="stream">待处理的流。</param>
    public static void CloseSafe(this Stream stream)
    {
        stream?.Close();
    }

    /// <summary>
    /// 尝试关闭并释放流（不抛出异常）。
    /// </summary>
    /// <param name="stream">待处理的流。</param>
    public static void DisposeSafe(this Stream stream)
    {
        try
        {
            stream?.Dispose();
        }
        catch
        {
        }
    }

    /// <summary>
    /// 创建流的副本。
    /// </summary>
    /// <param name="stream">源流。</param>
    /// <returns>流的副本。</returns>
    public static Stream Clone(this Stream stream)
    {
        if (stream == null) return Stream.Null;
        var bytes = stream.ToBytes();
        return new MemoryStream(bytes);
    }

    /// <summary>
    /// 判断两个流内容是否相等。
    /// </summary>
    /// <param name="stream1">第一个流。</param>
    /// <param name="stream2">第二个流。</param>
    /// <returns>如果内容相等返回 true；否则返回 false。</returns>
    public static bool ContentEquals(this Stream stream1, Stream stream2)
    {
        if (stream1 == null && stream2 == null) return true;
        if (stream1 == null || stream2 == null) return false;
        if (stream1.Length != stream2.Length) return false;
        var bytes1 = stream1.ToBytes();
        var bytes2 = stream2.ToBytes();
        return bytes1.SequenceEqual(bytes2);
    }

    /// <summary>
    /// 获取流的友好大小描述。
    /// </summary>
    /// <param name="stream">源流。</param>
    /// <returns>友好大小描述，如 "1.5 MB"。</returns>
    public static string GetFriendlySize(this Stream stream)
    {
        var length = stream?.Length ?? 0;
        return length.GetFriendlySize();
    }

    #endregion
}

/// <summary>
/// 数值扩展方法辅助类（用于 StreamExtensions）。
/// </summary>
internal static class NumericHelper
{
    /// <summary>
    /// 获取友好的文件大小描述。
    /// </summary>
    public static string GetFriendlySize(this long bytes)
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
}
