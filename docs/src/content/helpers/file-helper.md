---
title: "FileHelper"
description: "是一个静态帮助类，为文件操作提供了丰富的高级功能，包括压缩解压、安全写入、流式读取、INI 操作、文件比较、文件监控等，旨在补充 FileExtensions 中未包含的高级文件操作功能，提高文件处理的效率和安全性。"
namespace: "Chet.Utils.Helpers"
className: "FileHelper"
category: "Helpers"
order: 3
---

# FileHelper 帮助类

## 概述

[FileHelper](../../Chet.Utils/Helpers/FileHelper.cs) 是一个静态帮助类，为文件操作提供了丰富的高级功能，包括压缩解压、安全写入、流式读取、INI 操作、文件比较、文件监控等，旨在补充 FileExtensions 中未包含的高级文件操作功能，提高文件处理的效率和安全性。

## 功能模块

### 1. 压缩与解压

提供 ZIP 和 GZip 格式的文件压缩解压功能。

#### ZipFile

将文件压缩为 ZIP 文件。

```csharp
public static void ZipFile(
    string sourceFilePath,
    string zipFilePath,
    CompressionLevel compressionLevel = CompressionLevel.Optimal,
    bool overwrite = true)
```

**参数：**
- `sourceFilePath`: 源文件路径
- `zipFilePath`: ZIP 文件路径
- `compressionLevel`: 压缩级别，默认为 Optimal
- `overwrite`: 是否覆盖已存在的 ZIP 文件

**示例：**
```csharp
FileHelper.ZipFile("C:\\test.txt", "C:\\test.zip");
FileHelper.ZipFile("C:\\test.txt", "C:\\test.zip", CompressionLevel.Maximum);
```

#### ZipDirectory

将目录压缩为 ZIP 文件。

```csharp
public static void ZipDirectory(
    string sourceDirectoryPath,
    string zipFilePath,
    CompressionLevel compressionLevel = CompressionLevel.Optimal,
    bool includeBaseDirectory = false,
    bool overwrite = true)
```

**参数：**
- `sourceDirectoryPath`: 源目录路径
- `zipFilePath`: ZIP 文件路径
- `compressionLevel`: 压缩级别
- `includeBaseDirectory`: 是否包含基础目录
- `overwrite`: 是否覆盖已存在的 ZIP 文件

**示例：**
```csharp
FileHelper.ZipDirectory("C:\\MyFolder", "C:\\MyFolder.zip");
FileHelper.ZipDirectory("C:\\MyFolder", "C:\\MyFolder.zip", includeBaseDirectory: true);
```

#### UnzipFile

解压 ZIP 文件到指定目录。

```csharp
public static void UnzipFile(
    string zipFilePath,
    string destDirectoryPath,
    bool overwrite = true)
```

**示例：**
```csharp
FileHelper.UnzipFile("C:\\test.zip", "C:\\Extracted");
```

#### GZipCompress / GZipDecompress

使用 GZip 格式压缩/解压文件。

```csharp
public static void GZipCompress(string sourceFilePath, string destFilePath, bool overwrite = true)
public static void GZipDecompress(string sourceFilePath, string destFilePath, bool overwrite = true)
```

**示例：**
```csharp
FileHelper.GZipCompress("C:\\test.txt", "C:\\test.txt.gz");
FileHelper.GZipDecompress("C:\\test.txt.gz", "C:\\test.txt");
```

---

### 2. 安全写入

提供原子写入功能，确保写入过程中断不会导致文件损坏。

#### SafeWriteTextFile

安全写入文本文件（原子写入：先写入临时文件，成功后替换原文件）。

```csharp
public static void SafeWriteTextFile(
    string filePath,
    string content,
    Encoding encoding = null)
```

**参数：**
- `filePath`: 文件路径
- `content`: 文件内容
- `encoding`: 编码格式，默认为 UTF-8

**示例：**
```csharp
FileHelper.SafeWriteTextFile("C:\\config.json", jsonContent);
FileHelper.SafeWriteTextFile("C:\\data.txt", content, Encoding.UTF8);
```

#### SafeWriteTextFileAsync

异步安全写入文本文件。

```csharp
public static async Task SafeWriteTextFileAsync(
    string filePath,
    string content,
    Encoding encoding = null,
    CancellationToken cancellationToken = default)
```

#### SafeWriteBinaryFile

安全写入二进制文件。

```csharp
public static void SafeWriteBinaryFile(string filePath, byte[] bytes)
```

**示例：**
```csharp
byte[] data = Encoding.UTF8.GetBytes("Hello World");
FileHelper.SafeWriteBinaryFile("C:\\data.bin", data);
```

---

### 3. 流式读取

提供大文件的流式读取功能，避免一次性加载到内存。

#### ReadFileInChunks

流式读取大文件（分块读取，延迟执行）。

```csharp
public static IEnumerable<byte[]> ReadFileInChunks(
    string filePath,
    int bufferSize = 8192)
```

**参数：**
- `filePath`: 文件路径
- `bufferSize`: 缓冲区大小（字节），默认为 8192

**返回值：** 文件数据块枚举

**示例：**
```csharp
foreach (var chunk in FileHelper.ReadFileInChunks("C:\\largefile.bin", 4096))
{
    ProcessChunk(chunk);
}
```

#### ReadFileInChunksAsync

异步流式读取大文件。

```csharp
public static async IAsyncEnumerable<byte[]> ReadFileInChunksAsync(
    string filePath,
    int bufferSize = 8192,
    CancellationToken cancellationToken = default)
```

#### ReadLargeTextFile

逐行读取大文件（延迟执行，适用于大文本文件）。

```csharp
public static IEnumerable<string> ReadLargeTextFile(
    string filePath,
    Encoding encoding = null)
```

**示例：**
```csharp
foreach (var line in FileHelper.ReadLargeTextFile("C:\\largefile.log"))
{
    if (line.Contains("ERROR"))
    {
        Console.WriteLine(line);
    }
}
```

#### ReadLargeTextFileAsync

异步逐行读取大文件。

```csharp
public static async IAsyncEnumerable<string> ReadLargeTextFileAsync(
    string filePath,
    Encoding encoding = null,
    CancellationToken cancellationToken = default)
```

---

### 4. 目录操作

#### ClearDirectory

清空目录内容（保留目录本身）。

```csharp
public static void ClearDirectory(
    string directoryPath,
    bool deleteSubdirectories = true)
```

**参数：**
- `directoryPath`: 目录路径
- `deleteSubdirectories`: 是否删除子目录，默认为 true

**示例：**
```csharp
FileHelper.ClearDirectory("C:\\Temp");
FileHelper.ClearDirectory("C:\\Temp", deleteSubdirectories: false);
```

#### EnsureDirectoryExists

确保目录存在（如果不存在则创建）。

```csharp
public static DirectoryInfo EnsureDirectoryExists(string directoryPath)
```

**示例：**
```csharp
FileHelper.EnsureDirectoryExists("C:\\MyApp\\Data");
FileHelper.EnsureDirectoryExists("C:\\MyApp\\Logs\\2024\\01");
```

---

### 5. 校验计算

#### GetFileCrc32

获取文件的 CRC32 校验和。

```csharp
public static uint GetFileCrc32(string filePath)
```

**返回值：** CRC32 校验和（32位无符号整数）

**示例：**
```csharp
uint crc = FileHelper.GetFileCrc32("C:\\test.bin");
Console.WriteLine($"CRC32: {crc:X8}");
```

#### VerifyFileHash

验证文件哈希值是否匹配。

```csharp
public static bool VerifyFileHash(
    string filePath,
    string expectedHash,
    string hashAlgorithm = "MD5")
```

**参数：**
- `filePath`: 文件路径
- `expectedHash`: 期望的哈希值（小写十六进制字符串）
- `hashAlgorithm`: 哈希算法，默认为 MD5，支持 MD5、SHA1、SHA256、SHA384、SHA512

**示例：**
```csharp
bool isValid = FileHelper.VerifyFileHash("C:\\test.bin", "d41d8cd98f00b204e9800998ecf8427e");
bool isValidSha256 = FileHelper.VerifyFileHash("C:\\test.bin", expectedHash, "SHA256");
```

---

### 6. INI 文件操作

提供 INI 配置文件的读写功能，支持 Windows API 和跨平台托管实现。

#### ReadIniValue

读取 INI 文件中的值。

```csharp
public static string ReadIniValue(
    string filePath,
    string section,
    string key,
    string defaultValue = "")
```

**参数：**
- `filePath`: INI 文件路径
- `section`: 节名称
- `key`: 键名称
- `defaultValue`: 默认值

**示例：**
```csharp
var server = FileHelper.ReadIniValue("C:\\config.ini", "Database", "Server", "localhost");
var port = FileHelper.ReadIniValue("C:\\config.ini", "Database", "Port", "3306");
```

#### WriteIniValue

写入值到 INI 文件。

```csharp
public static void WriteIniValue(
    string filePath,
    string section,
    string key,
    string value)
```

**示例：**
```csharp
FileHelper.WriteIniValue("C:\\config.ini", "Database", "Server", "192.168.1.100");
FileHelper.WriteIniValue("C:\\config.ini", "Database", "Port", "3306");
```

---

### 7. 文件比较

#### CompareFiles

比较两个文件是否相同（通过哈希值比较）。

```csharp
public static bool CompareFiles(string filePath1, string filePath2)
```

**示例：**
```csharp
bool same = FileHelper.CompareFiles("C:\\file1.txt", "C:\\file2.txt");
if (same)
{
    Console.WriteLine("文件内容相同");
}
```

#### CompareFileContent

比较两个文件的内容差异（逐字节比较）。

```csharp
public static List<FileDifference> CompareFileContent(string filePath1, string filePath2)
```

**返回值：** 差异信息列表

**示例：**
```csharp
var differences = FileHelper.CompareFileContent("C:\\file1.bin", "C:\\file2.bin");
foreach (var diff in differences)
{
    Console.WriteLine($"位置 {diff.Position}: 文件1={diff.Value1:X2}, 文件2={diff.Value2:X2}");
}
```

**FileDifference 类：**
```csharp
public class FileDifference
{
    public long Position { get; set; }      // 差异位置（字节偏移）
    public byte Value1 { get; set; }        // 第一个文件的字节值
    public byte Value2 { get; set; }        // 第二个文件的字节值
}
```

---

### 8. 文件版本信息

#### GetFileVersionInfo

获取文件版本信息。

```csharp
public static FileVersionInfo GetFileVersionInfo(string filePath)
```

**示例：**
```csharp
var versionInfo = FileHelper.GetFileVersionInfo("C:\\Program Files\\MyApp\\app.exe");
if (versionInfo != null)
{
    Console.WriteLine($"版本: {versionInfo.FileVersion}");
    Console.WriteLine($"产品: {versionInfo.ProductName}");
    Console.WriteLine($"公司: {versionInfo.CompanyName}");
}
```

#### GetFileVersion

获取文件版本字符串。

```csharp
public static string GetFileVersion(string filePath)
```

---

### 9. 临时文件

#### CreateTempFile

创建临时文件并返回路径。

```csharp
public static string CreateTempFile(
    string extension = ".tmp",
    string prefix = "")
```

**参数：**
- `extension`: 文件扩展名（带点），如 ".txt"
- `prefix`: 文件名前缀

**示例：**
```csharp
var tempFile = FileHelper.CreateTempFile(".txt", "myapp_");
File.WriteAllText(tempFile, "临时内容");
// 使用完毕后删除
File.Delete(tempFile);
```

#### CreateTempDirectory

创建临时目录并返回路径。

```csharp
public static string CreateTempDirectory(string prefix = "")
```

**示例：**
```csharp
var tempDir = FileHelper.CreateTempDirectory("myapp_");
// 使用临时目录...
Directory.Delete(tempDir, true); // 清理
```

---

### 10. 文件监控

#### WatchFileChanges

创建文件系统监视器。

```csharp
public static FileSystemWatcher WatchFileChanges(
    string path,
    string filter = "*.*",
    bool includeSubdirectories = true)
```

**示例：**
```csharp
using var watcher = FileHelper.WatchFileChanges("C:\\Logs", "*.log");
watcher.Changed += (s, e) => Console.WriteLine($"文件已更改: {e.FullPath}");
watcher.Created += (s, e) => Console.WriteLine($"文件已创建: {e.FullPath}");
watcher.Deleted += (s, e) => Console.WriteLine($"文件已删除: {e.FullPath}");
watcher.EnableRaisingEvents = true;

// 保持运行
Console.ReadLine();
```

#### WaitForFileCreation

等待文件创建完成（用于处理正在写入的文件）。

```csharp
public static bool WaitForFileCreation(string filePath, TimeSpan timeout)
```

**示例：**
```csharp
if (FileHelper.WaitForFileCreation("C:\\upload\\file.pdf", TimeSpan.FromSeconds(30)))
{
    // 文件已就绪，可以处理
    ProcessFile("C:\\upload\\file.pdf");
}
```

---

### 11. 文件锁定

#### IsFileLocked

判断文件是否被锁定。

```csharp
public static bool IsFileLocked(string filePath)
```

**示例：**
```csharp
if (!FileHelper.IsFileLocked("C:\\data.xlsx"))
{
    // 文件未被锁定，可以安全操作
    ProcessExcelFile("C:\\data.xlsx");
}
```

#### WaitForFileUnlock

等待文件解锁。

```csharp
public static bool WaitForFileUnlock(string filePath, TimeSpan timeout)
```

**示例：**
```csharp
if (FileHelper.WaitForFileUnlock("C:\\data.xlsx", TimeSpan.FromSeconds(10)))
{
    // 文件已解锁，可以安全操作
    ProcessExcelFile("C:\\data.xlsx");
}
else
{
    Console.WriteLine("文件仍被锁定，无法访问");
}
```

---

### 12. 文件搜索

#### SearchFiles

搜索文件（延迟执行，适用于大量文件）。

```csharp
public static IEnumerable<string> SearchFiles(
    string directoryPath,
    string searchPattern = "*.*",
    SearchOption searchOption = SearchOption.AllDirectories)
```

**示例：**
```csharp
var logFiles = FileHelper.SearchFiles("C:\\Logs", "*.log");
foreach (var file in logFiles)
{
    Console.WriteLine(file);
}
```

---

## 与 FileExtensions 的区别

| 功能 | FileHelper | FileExtensions |
|------|------------|----------------|
| 压缩解压 | ✅ Zip/GZip | ❌ |
| 安全写入 | ✅ 原子写入 | ❌ |
| 流式读取 | ✅ 分块读取 | ❌ |
| INI 操作 | ✅ 读写 INI | ❌ |
| 文件比较 | ✅ 哈希/字节比较 | ❌ |
| 文件监控 | ✅ FileSystemWatcher | ❌ |
| 文件锁定检测 | ✅ IsFileLocked | ❌ |
| 临时文件创建 | ✅ CreateTempFile | ❌ |
| 基础文件操作 | ❌ | ✅ 读写、复制、移动等 |
| 目录基础操作 | ✅ ClearDirectory | ✅ 创建、删除等 |

**建议：** 基础文件操作使用 `FileExtensions`，高级功能使用 `FileHelper`。

---

## 最佳实践

### 1. 大文件处理

```csharp
// 使用流式读取处理大文件
foreach (var chunk in FileHelper.ReadFileInChunks("large_file.bin", 65536))
{
    await ProcessChunkAsync(chunk);
}

// 或逐行读取大文本文件
foreach (var line in FileHelper.ReadLargeTextFile("large_log.txt"))
{
    if (line.Contains("ERROR"))
    {
        await ProcessErrorLogAsync(line);
    }
}
```

### 2. 安全写入配置文件

```csharp
// 使用原子写入避免配置文件损坏
var config = JsonSerializer.Serialize(appSettings);
FileHelper.SafeWriteTextFile("config.json", config);
```

### 3. 文件完整性验证

```csharp
// 下载后验证文件完整性
var downloadedHash = FileHelper.GetFileCrc32("downloaded_file.zip");
if (downloadedHash == expectedHash)
{
    Console.WriteLine("文件完整性验证通过");
}
else
{
    Console.WriteLine("文件可能已损坏");
    File.Delete("downloaded_file.zip");
}
```

### 4. 处理被锁定的文件

```csharp
// 等待文件解锁后处理
if (FileHelper.WaitForFileUnlock("report.xlsx", TimeSpan.FromSeconds(30)))
{
    var data = ReadExcelFile("report.xlsx");
    ProcessData(data);
}
else
{
    Console.WriteLine("文件被占用，请稍后重试");
}
```

---

## 异常处理

所有方法都会在参数无效或操作失败时抛出相应的异常：

- `FileNotFoundException`: 文件不存在
- `DirectoryNotFoundException`: 目录不存在
- `ArgumentException`: 参数无效
- `IOException`: I/O 操作失败
- `UnauthorizedAccessException`: 没有访问权限

**建议使用 try-catch 处理异常：**

```csharp
try
{
    FileHelper.ZipFile(sourceFile, zipFile);
}
catch (FileNotFoundException ex)
{
    Console.WriteLine($"源文件不存在: {ex.FileName}");
}
catch (IOException ex)
{
    Console.WriteLine($"压缩失败: {ex.Message}");
}
```

---

## 相关文档

- [FileExtensions](../Extensions/FileExtensions.md) - 文件扩展方法
- [StreamExtensions](../Extensions/StreamExtensions.md) - 流扩展方法
