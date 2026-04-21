# StreamExtensions 功能文档

## 概述

[StreamExtensions](../../Chet.Utils/Extensions/StreamExtensions.cs) 是一个静态扩展类，为 `Stream` 类型提供了丰富的扩展方法，涵盖流的读取、写入、转换、判断、操作等功能，旨在简化流操作，提高代码的安全性和可读性，特别适用于需要处理各种流类型的日常开发场景。

## 主要功能模块

### 1. 基础判断

#### CanReadSafe
判断流是否可读。
```csharp
public static bool CanReadSafe(this Stream stream)
```
**示例：**
```csharp
using var stream = new MemoryStream();
var canRead = stream.CanReadSafe(); // true
```

#### CanWriteSafe
判断流是否可写。
```csharp
public static bool CanWriteSafe(this Stream stream)
```

#### CanSeekSafe
判断流是否可查找（支持 Seek）。
```csharp
public static bool CanSeekSafe(this Stream stream)
```

#### IsNullOrEmpty
判断流是否为空或长度为零。
```csharp
public static bool IsNullOrEmpty(this Stream stream)
```

#### IsNotNullOrEmpty
判断流是否不为空且长度大于零。
```csharp
public static bool IsNotNullOrEmpty(this Stream stream)
```

#### IsNullStream
判断流是否为空流（Stream.Null）。
```csharp
public static bool IsNullStream(this Stream stream)
```

---

### 2. 类型判断

#### IsMemoryStream
判断流是否为 MemoryStream。
```csharp
public static bool IsMemoryStream(this Stream stream)
```

#### IsFileStream
判断流是否为 FileStream。
```csharp
public static bool IsFileStream(this Stream stream)
```

#### IsNetworkStream
判断流是否为 NetworkStream。
```csharp
public static bool IsNetworkStream(this Stream stream)
```

#### IsBufferedStream
判断流是否为 BufferedStream。
```csharp
public static bool IsBufferedStream(this Stream stream)
```

---

### 3. 同步读取

#### ToBytes
将流内容读取为字节数组。
```csharp
public static byte[] ToBytes(this Stream stream)
```
**示例：**
```csharp
using var stream = new MemoryStream(Encoding.UTF8.GetBytes("Hello World"));
var bytes = stream.ToBytes();
```

#### ToText
将流内容读取为字符串（默认 UTF8 编码）。
```csharp
public static string ToText(this Stream stream, Encoding encoding = null)
```
**示例：**
```csharp
using var stream = new MemoryStream(Encoding.UTF8.GetBytes("Hello World"));
var text = stream.ToText(); // "Hello World"
```

#### ReadBytes
读取流的部分内容为字节数组。
```csharp
public static byte[] ReadBytes(this Stream stream, int offset, int count)
```

#### ReadText
读取流的部分内容为字符串（默认 UTF8 编码）。
```csharp
public static string ReadText(this Stream stream, int offset, int count, Encoding encoding = null)
```

#### ReadLine
读取流的一行内容（按换行符分割）。
```csharp
public static string ReadLine(this Stream stream, Encoding encoding = null)
```

#### ReadAllLines
读取流的所有行内容。
```csharp
public static List<string> ReadAllLines(this Stream stream, Encoding encoding = null)
```

---

### 4. 异步读取

#### ToBytesAsync
异步将流内容读取为字节数组。
```csharp
public static async Task<byte[]> ToBytesAsync(this Stream stream, CancellationToken cancellationToken = default)
```
**示例：**
```csharp
using var stream = new MemoryStream(Encoding.UTF8.GetBytes("Hello World"));
var bytes = await stream.ToBytesAsync();
```

#### ToTextAsync
异步将流内容读取为字符串（默认 UTF8 编码）。
```csharp
public static async Task<string> ToTextAsync(this Stream stream, Encoding encoding = null, CancellationToken cancellationToken = default)
```

#### ReadBytesAsync
异步读取流的部分内容为字节数组。
```csharp
public static async Task<byte[]> ReadBytesAsync(this Stream stream, int offset, int count, CancellationToken cancellationToken = default)
```

#### ReadTextAsync
异步读取流的部分内容为字符串（默认 UTF8 编码）。
```csharp
public static async Task<string> ReadTextAsync(this Stream stream, int offset, int count, Encoding encoding = null, CancellationToken cancellationToken = default)
```

#### ReadLineAsync
异步读取流的一行内容（按换行符分割）。
```csharp
public static async Task<string> ReadLineAsync(this Stream stream, Encoding encoding = null, CancellationToken cancellationToken = default)
```

#### ReadAllLinesAsync
异步读取流的所有行内容。
```csharp
public static async Task<List<string>> ReadAllLinesAsync(this Stream stream, Encoding encoding = null, CancellationToken cancellationToken = default)
```

---

### 5. 同步写入

#### WriteText
将字符串写入流（覆盖原内容，默认 UTF8 编码）。
```csharp
public static void WriteText(this Stream stream, string text, Encoding encoding = null)
```
**示例：**
```csharp
using var stream = new MemoryStream();
stream.WriteText("Hello World");
```

#### WriteBytes
将字节数组写入流（覆盖原内容）。
```csharp
public static void WriteBytes(this Stream stream, byte[] bytes)
```

#### AppendText
向流追加写入字符串（默认 UTF8 编码）。
```csharp
public static void AppendText(this Stream stream, string text, Encoding encoding = null)
```

#### AppendBytes
向流追加写入字节数组。
```csharp
public static void AppendBytes(this Stream stream, byte[] bytes)
```

#### WriteLine
写入一行文本（带换行符）。
```csharp
public static void WriteLine(this Stream stream, string text, Encoding encoding = null)
```

#### WriteLines
写入多行文本。
```csharp
public static void WriteLines(this Stream stream, IEnumerable<string> lines, Encoding encoding = null)
```

#### WriteStream
将另一个流的内容写入当前流。
```csharp
public static void WriteStream(this Stream stream, Stream source)
```

---

### 6. 异步写入

#### WriteTextAsync
异步将字符串写入流（覆盖原内容，默认 UTF8 编码）。
```csharp
public static async Task WriteTextAsync(this Stream stream, string text, Encoding encoding = null, CancellationToken cancellationToken = default)
```
**示例：**
```csharp
using var stream = new MemoryStream();
await stream.WriteTextAsync("Hello World");
```

#### WriteBytesAsync
异步将字节数组写入流（覆盖原内容）。
```csharp
public static async Task WriteBytesAsync(this Stream stream, byte[] bytes, CancellationToken cancellationToken = default)
```

#### AppendTextAsync
异步向流追加写入字符串（默认 UTF8 编码）。
```csharp
public static async Task AppendTextAsync(this Stream stream, string text, Encoding encoding = null, CancellationToken cancellationToken = default)
```

#### AppendBytesAsync
异步向流追加写入字节数组。
```csharp
public static async Task AppendBytesAsync(this Stream stream, byte[] bytes, CancellationToken cancellationToken = default)
```

#### WriteLineAsync
异步写入一行文本（带换行符）。
```csharp
public static async Task WriteLineAsync(this Stream stream, string text, Encoding encoding = null, CancellationToken cancellationToken = default)
```

#### WriteLinesAsync
异步写入多行文本。
```csharp
public static async Task WriteLinesAsync(this Stream stream, IEnumerable<string> lines, Encoding encoding = null, CancellationToken cancellationToken = default)
```

#### WriteStreamAsync
异步将另一个流的内容写入当前流。
```csharp
public static async Task WriteStreamAsync(this Stream stream, Stream source, CancellationToken cancellationToken = default)
```

---

### 7. 文件操作

#### SaveToFile
将流内容保存到文件（覆盖）。
```csharp
public static void SaveToFile(this Stream stream, string filePath)
```
**示例：**
```csharp
using var stream = new MemoryStream(Encoding.UTF8.GetBytes("Hello World"));
stream.SaveToFile("C:\\output.txt");
```

#### SaveToFileAsync
异步将流内容保存到文件（覆盖）。
```csharp
public static async Task SaveToFileAsync(this Stream stream, string filePath, CancellationToken cancellationToken = default)
```

#### ToFileStream
从文件数据读取为流。
```csharp
public static Stream ToFileStream(this string filePath)
```

---

### 8. 流操作

#### CopyToStream
将流数据复制到另一个流。
```csharp
public static void CopyToStream(this Stream source, Stream destination)
```
**示例：**
```csharp
using var source = new MemoryStream(Encoding.UTF8.GetBytes("Hello World"));
using var destination = new MemoryStream();
source.CopyToStream(destination);
```

#### CopyToStreamAsync
异步将流数据复制到另一个流。
```csharp
public static async Task CopyToStreamAsync(this Stream source, Stream destination, CancellationToken cancellationToken = default)
```

#### ResetPosition
重置流到起始位置。
```csharp
public static void ResetPosition(this Stream stream)
```

#### SetPosition
设置流的当前位置。
```csharp
public static void SetPosition(this Stream stream, long position)
```

#### GetPosition
获取流的当前位置。
```csharp
public static long GetPosition(this Stream stream)
```

#### GetLength
获取流的长度（字节）。
```csharp
public static long GetLength(this Stream stream)
```

#### SeekToStart
定位流到起始位置。
```csharp
public static void SeekToStart(this Stream stream)
```

#### SeekToEnd
定位流到结束位置。
```csharp
public static void SeekToEnd(this Stream stream)
```

---

### 9. 转换操作

#### ToStream (string)
将字符串转换为流（默认 UTF8 编码）。
```csharp
public static Stream ToStream(this string text, Encoding encoding = null)
```
**示例：**
```csharp
var stream = "Hello World".ToStream();
```

#### ToStream (byte[])
将字节数组转换为流。
```csharp
public static Stream ToStream(this byte[] bytes)
```
**示例：**
```csharp
var bytes = Encoding.UTF8.GetBytes("Hello World");
var stream = bytes.ToStream();
```

#### ToMemoryStream
将流转换为 MemoryStream。
```csharp
public static MemoryStream ToMemoryStream(this Stream stream)
```

---

### 10. 哈希计算

#### ComputeMd5
计算流的 MD5 哈希值。
```csharp
public static string ComputeMd5(this Stream stream)
```
**示例：**
```csharp
using var stream = new MemoryStream(Encoding.UTF8.GetBytes("Hello World"));
var md5 = stream.ComputeMd5();
```

#### ComputeSha256
计算流的 SHA256 哈希值。
```csharp
public static string ComputeSha256(this Stream stream)
```

#### ComputeHash
计算流的指定算法哈希值。
```csharp
public static string ComputeHash(this Stream stream, HashAlgorithm algorithm)
```

---

### 11. 压缩解压

#### Compress
压缩流数据（GZip）。
```csharp
public static byte[] Compress(this Stream stream)
```
**示例：**
```csharp
using var stream = new MemoryStream(Encoding.UTF8.GetBytes("Hello World"));
var compressed = stream.Compress();
```

#### Decompress
解压流数据（GZip）。
```csharp
public static byte[] Decompress(this Stream stream)
```

#### CompressAsync
异步压缩流数据（GZip）。
```csharp
public static async Task<byte[]> CompressAsync(this Stream stream, CancellationToken cancellationToken = default)
```

#### DecompressAsync
异步解压流数据（GZip）。
```csharp
public static async Task<byte[]> DecompressAsync(this Stream stream, CancellationToken cancellationToken = default)
```

---

## 使用场景

1. **文件处理** - 读写文件流、保存流到文件、从文件创建流
2. **网络通信** - 网络流数据的读取和写入
3. **数据转换** - 流与字符串、字节数组之间的相互转换
4. **内存处理** - MemoryStream 的创建和操作
5. **数据传输** - 流之间的数据复制和传输
6. **数据解析** - 读取流的部分数据进行解析处理
7. **编码处理** - 不同编码格式的文本流处理
8. **资源管理** - 安全关闭和释放流资源
9. **数据校验** - 计算流数据的哈希值进行完整性校验
10. **数据压缩** - 压缩和解压流数据

## 注意事项

1. 所有方法都是扩展方法，需要通过 `Stream` 实例来调用
2. 带有 "Safe" 后缀的方法对 null 值进行了安全处理，不会抛出异常
3. `ToBytes` 方法对 MemoryStream 进行了优化，直接调用 ToArray() 方法
4. 读取方法内部会保存和恢复流的原始位置
5. 写入方法会清空原数据（SetLength(0)）后再写入新内容
6. `ReadBytes` 方法返回实际读取的字节数组长度
7. `SetPosition` 方法设置位置时会检查边界，确保在有效范围内
8. 所有编码方法默认使用 UTF8 编码，支持自定义编码
9. 文件保存方法使用 using 确保资源正确释放
10. 复制方法会自动将源流位置重置到起始位置
11. 哈希计算方法会重置流位置，计算完成后恢复原位置
12. 压缩解压方法使用 GZip 算法
