using System.Text;

namespace Chet.Utils
{
    /// <summary>
    /// Stream 扩展方法类，提供常用的读取、写入、转换、判断、操作等功能。
    /// </summary>
    public static class StreamExtensions
    {
        /// <summary>
        /// 判断流是否可读。
        /// </summary>
        /// <param name="stream">待判断的流。</param>
        public static bool CanReadSafe(this Stream stream) => stream != null && stream.CanRead;

        /// <summary>
        /// 判断流是否可写。
        /// </summary>
        /// <param name="stream">待判断的流。</param>
        public static bool CanWriteSafe(this Stream stream) => stream != null && stream.CanWrite;

        /// <summary>
        /// 判断流是否可查找（支持 Seek）。
        /// </summary>
        /// <param name="stream">待判断的流。</param>
        public static bool CanSeekSafe(this Stream stream) => stream != null && stream.CanSeek;

        /// <summary>
        /// 判断流是否为空或长度为零。
        /// </summary>
        /// <param name="stream">待判断的流。</param>
        public static bool IsNullOrEmpty(this Stream stream) => stream == null || stream.Length == 0;

        /// <summary>
        /// 将流内容读取为字节数组。
        /// </summary>
        /// <param name="stream">待读取的流。</param>
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
        public static string ToText(this Stream stream, Encoding encoding = null)
        {
            encoding ??= Encoding.UTF8;
            var bytes = stream.ToBytes();
            return encoding.GetString(bytes);
        }

        /// <summary>
        /// 将字符串写入流（覆盖原内容，默认 UTF8 编码）。
        /// </summary>
        /// <param name="stream">目标流。</param>
        /// <param name="text">写入内容。</param>
        /// <param name="encoding">编码，默认 UTF8。</param>
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
        /// 将流内容保存到文件（覆盖）。
        /// </summary>
        /// <param name="stream">源流。</param>
        /// <param name="filePath">目标文件路径。</param>
        public static void SaveToFile(this Stream stream, string filePath)
        {
            if (stream == null || string.IsNullOrEmpty(filePath)) return;
            using var file = new FileStream(filePath, FileMode.Create, FileAccess.Write);
            stream.Position = 0;
            stream.CopyTo(file);
        }

        /// <summary>
        /// 将文件内容读取为流。
        /// </summary>
        /// <param name="filePath">文件路径。</param>
        public static Stream ToStream(this string filePath)
        {
            if (string.IsNullOrEmpty(filePath) || !File.Exists(filePath)) return Stream.Null;
            return new FileStream(filePath, FileMode.Open, FileAccess.Read);
        }

        /// <summary>
        /// 将字符串转换为流（默认 UTF8 编码）。
        /// </summary>
        /// <param name="text">字符串内容。</param>
        /// <param name="encoding">编码，默认 UTF8。</param>
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
        public static Stream ToStream(this byte[] bytes)
        {
            if (bytes == null) return Stream.Null;
            return new MemoryStream(bytes);
        }

        /// <summary>
        /// 将流内容复制到另一个流。
        /// </summary>
        /// <param name="stream">源流。</param>
        /// <param name="target">目标流。</param>
        public static void CopyToStream(this Stream stream, Stream target)
        {
            if (stream == null || target == null) return;
            stream.Position = 0;
            stream.CopyTo(target);
            target.Flush();
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
        /// 读取流的部分内容为字节数组。
        /// </summary>
        /// <param name="stream">源流。</param>
        /// <param name="offset">起始偏移。</param>
        /// <param name="count">读取长度。</param>
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
        public static string ReadText(this Stream stream, int offset, int count, Encoding encoding = null)
        {
            encoding ??= Encoding.UTF8;
            var bytes = stream.ReadBytes(offset, count);
            return encoding.GetString(bytes);
        }

        /// <summary>
        /// 判断流是否为 MemoryStream。
        /// </summary>
        /// <param name="stream">待判断的流。</param>
        public static bool IsMemoryStream(this Stream stream) => stream is MemoryStream;

        /// <summary>
        /// 判断流是否为 FileStream。
        /// </summary>
        /// <param name="stream">待判断的流。</param>
        public static bool IsFileStream(this Stream stream) => stream is FileStream;

        /// <summary>
        /// 判断流是否为空流（Stream.Null）。
        /// </summary>
        /// <param name="stream">待判断的流。</param>
        public static bool IsNullStream(this Stream stream) => stream == Stream.Null;

        /// <summary>
        /// 获取流的长度（字节）。
        /// </summary>
        /// <param name="stream">待处理的流。</param>
        public static long GetLength(this Stream stream) => stream?.Length ?? 0;

        /// <summary>
        /// 获取流的当前位置。
        /// </summary>
        /// <param name="stream">待处理的流。</param>
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
        /// 关闭并释放流。
        /// </summary>
        /// <param name="stream">待处理的流。</param>
        public static void CloseSafe(this Stream stream)
        {
            if (stream != null)
            {
                stream.Close();
                stream.Dispose();
            }
        }
    }
}