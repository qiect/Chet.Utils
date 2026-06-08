---
title: "FileExtensions"
description: "为文件系统相关类型（string、FileInfo、DirectoryInfo）提供了丰富的扩展方法，涵盖文件读写、路径处理、信息获取、属性操作等功能，旨在简化文件系统操作，提高代码的安全性和可读性，特别适用于各种文件处理场景。"
targetType: "FileInfo"
namespace: "Chet.Utils"
className: "FileExtensions"
category: "Extensions"
order: 8
---

# FileExtensions 功能文档

## 概述

[FileExtensions](../../Chet.Utils/Extensions/FileExtensions.cs) 是一个静态扩展类，为文件系统相关类型（string、FileInfo、DirectoryInfo）提供了丰富的扩展方法，涵盖文件读写、路径处理、信息获取、属性操作等功能，旨在简化文件系统操作，提高代码的安全性和可读性，特别适用于各种文件处理场景。

## 主要功能模块

### 1. 文件判断

#### Exists
判断文件是否存在。
```csharp
public static bool Exists(this string filePath)
```
**示例：**
```csharp
var exists = "C:\\test.txt".Exists(); // false
```

#### IsFile
判断路径是否为文件。
```csharp
public static bool IsFile(this string path)
```

#### IsDirectory
判断路径是否为目录。
```csharp
public static bool IsDirectory(this string path)
```

#### ExistsPath
判断路径的文件或目录是否存在。
```csharp
public static bool ExistsPath(this string path)
```

#### IsReadOnly
判断文件是否为只读。
```csharp
public static bool IsReadOnly(this string filePath)
```

#### IsHidden
判断文件是否为隐藏文件。
```csharp
public static bool IsHidden(this string filePath)
```

#### IsSystemFile
判断文件是否为系统文件。
```csharp
public static bool IsSystemFile(this string filePath)
```

#### IsTemporary
判断文件是否为临时文件。
```csharp
public static bool IsTemporary(this string filePath)
```

---

### 2. 同步读取

#### ReadAllText
读取文件所有文本内容。
```csharp
public static string ReadAllText(this string filePath)
public static string ReadAllText(this string filePath, Encoding encoding)
```
**示例：**
```csharp
var content = "C:\\test.txt".ReadAllText();
var contentUtf8 = "C:\\test.txt".ReadAllText(Encoding.UTF8);
```

#### ReadAllLines
读取文件所有行内容。
```csharp
public static string[] ReadAllLines(this string filePath)
public static string[] ReadAllLines(this string filePath, Encoding encoding)
```

#### ReadAllBytes
读取文件所有字节内容。
```csharp
public static byte[] ReadAllBytes(this string filePath)
```

#### ReadLines
逐行读取文件内容（延迟执行）。
```csharp
public static IEnumerable<string> ReadLines(this string filePath)
public static IEnumerable<string> ReadLines(this string filePath, Encoding encoding)
```

---

### 3. 异步读取

#### ReadAllTextAsync
异步读取文件所有文本内容。
```csharp
public static async Task<string> ReadAllTextAsync(this string filePath, CancellationToken cancellationToken = default)
public static async Task<string> ReadAllTextAsync(this string filePath, Encoding encoding, CancellationToken cancellationToken = default)
```
**示例：**
```csharp
var content = await "C:\\test.txt".ReadAllTextAsync();
```

#### ReadAllLinesAsync
异步读取文件所有行内容。
```csharp
public static async Task<List<string>> ReadAllLinesAsync(this string filePath, CancellationToken cancellationToken = default)
public static async Task<List<string>> ReadAllLinesAsync(this string filePath, Encoding encoding, CancellationToken cancellationToken = default)
```

#### ReadAllBytesAsync
异步读取文件所有字节内容。
```csharp
public static async Task<byte[]> ReadAllBytesAsync(this string filePath, CancellationToken cancellationToken = default)
```

---

### 4. 同步写入

#### WriteAllText
写入文本内容到文件（覆盖）。
```csharp
public static void WriteAllText(this string filePath, string content)
public static void WriteAllText(this string filePath, string content, Encoding encoding)
```
**示例：**
```csharp
"C:\\test.txt".WriteAllText("Hello World");
```

#### WriteAllLines
写入多行内容到文件（覆盖）。
```csharp
public static void WriteAllLines(this string filePath, IEnumerable<string> lines)
public static void WriteAllLines(this string filePath, IEnumerable<string> lines, Encoding encoding)
```

#### WriteAllBytes
写入字节内容到文件（覆盖）。
```csharp
public static void WriteAllBytes(this string filePath, byte[] bytes)
```

#### AppendText
追加文本内容到文件。
```csharp
public static void AppendText(this string filePath, string content)
public static void AppendText(this string filePath, string content, Encoding encoding)
```

#### AppendLines
追加多行内容到文件。
```csharp
public static void AppendLines(this string filePath, IEnumerable<string> lines)
public static void AppendLines(this string filePath, IEnumerable<string> lines, Encoding encoding)
```

#### AppendLine
追加一行内容到文件。
```csharp
public static void AppendLine(this string filePath, string line)
```

---

### 5. 异步写入

#### WriteAllTextAsync
异步写入文本内容到文件（覆盖）。
```csharp
public static async Task WriteAllTextAsync(this string filePath, string content, CancellationToken cancellationToken = default)
public static async Task WriteAllTextAsync(this string filePath, string content, Encoding encoding, CancellationToken cancellationToken = default)
```
**示例：**
```csharp
await "C:\\test.txt".WriteAllTextAsync("Hello World");
```

#### WriteAllLinesAsync
异步写入多行内容到文件（覆盖）。
```csharp
public static async Task WriteAllLinesAsync(this string filePath, IEnumerable<string> lines, CancellationToken cancellationToken = default)
public static async Task WriteAllLinesAsync(this string filePath, IEnumerable<string> lines, Encoding encoding, CancellationToken cancellationToken = default)
```

#### WriteAllBytesAsync
异步写入字节内容到文件（覆盖）。
```csharp
public static async Task WriteAllBytesAsync(this string filePath, byte[] bytes, CancellationToken cancellationToken = default)
```

#### AppendTextAsync
异步追加文本内容到文件。
```csharp
public static async Task AppendTextAsync(this string filePath, string content, CancellationToken cancellationToken = default)
```

#### AppendLinesAsync
异步追加多行内容到文件。
```csharp
public static async Task AppendLinesAsync(this string filePath, IEnumerable<string> lines, CancellationToken cancellationToken = default)
```

---

### 6. 文件操作

#### DeleteFile
删除文件。
```csharp
public static void DeleteFile(this string filePath)
```
**示例：**
```csharp
"C:\\test.txt".DeleteFile();
```

#### CopyTo
复制文件到目标路径。
```csharp
public static void CopyTo(this string sourcePath, string destPath, bool overwrite = true)
```

#### MoveTo
移动文件到目标路径。
```csharp
public static void MoveTo(this string sourcePath, string destPath, bool overwrite = true)
```

#### Rename
重命名文件。
```csharp
public static void Rename(this string filePath, string newName)
```

#### Replace
替换文件内容。
```csharp
public static void Replace(this string sourcePath, string destPath, string backupPath = null)
```

---

### 7. 文件信息

#### GetFileSize
获取文件大小（字节）。
```csharp
public static long GetFileSize(this string filePath)
```
**示例：**
```csharp
var size = "C:\\test.txt".GetFileSize();
```

#### GetCreationTime
获取文件创建时间。
```csharp
public static DateTime GetCreationTime(this string filePath)
```

#### GetLastWriteTime
获取文件最后修改时间。
```csharp
public static DateTime GetLastWriteTime(this string filePath)
```

#### GetLastAccessTime
获取文件最后访问时间。
```csharp
public static DateTime GetLastAccessTime(this string filePath)
```

#### GetFileSizeFriendly
获取文件大小的友好字符串（如 "1.23 MB"）。
```csharp
public static string GetFileSizeFriendly(this string filePath)
```

---

### 8. 文件属性

#### SetReadOnly
设置文件为只读。
```csharp
public static void SetReadOnly(this string filePath)
```

#### UnsetReadOnly
取消文件只读属性。
```csharp
public static void UnsetReadOnly(this string filePath)
```

#### SetHidden
设置文件为隐藏文件。
```csharp
public static void SetHidden(this string filePath)
```

#### UnsetHidden
取消文件隐藏属性。
```csharp
public static void UnsetHidden(this string filePath)
```

#### SetAttributes
设置文件属性。
```csharp
public static void SetAttributes(this string filePath, FileAttributes attributes)
```

#### GetAttributes
获取文件属性。
```csharp
public static FileAttributes GetAttributes(this string filePath)
```

---

### 9. 哈希计算

#### GetFileMd5
获取文件的 MD5 值。
```csharp
public static string GetFileMd5(this string filePath)
```
**示例：**
```csharp
var md5 = "C:\\test.txt".GetFileMd5();
```

#### GetFileSha256
获取文件的 SHA256 值。
```csharp
public static string GetFileSha256(this string filePath)
```

#### GetFileHash
获取文件的指定算法哈希值。
```csharp
public static string GetFileHash(this string filePath, HashAlgorithm algorithm)
```

---

### 10. 路径处理

#### GetExtension
获取文件扩展名（带点）。
```csharp
public static string GetExtension(this string filePath)
```

#### GetFileName
获取文件名（带路径）。
```csharp
public static string GetFileName(this string filePath)
```

#### GetFileNameWithoutExtension
获取文件名（不带扩展名）。
```csharp
public static string GetFileNameWithoutExtension(this string filePath)
```

#### GetDirectoryName
获取文件所在目录路径。
```csharp
public static string GetDirectoryName(this string filePath)
```

#### CombinePaths
合并多个路径为一个完整路径。
```csharp
public static string CombinePaths(this string path, params string[] paths)
```
**示例：**
```csharp
var fullPath = "C:\\Base".CombinePaths("SubDir", "file.txt");
// "C:\\Base\\SubDir\\file.txt"
```

#### GetFullPath
获取路径的完整规范路径（相对路径转绝对路径）。
```csharp
public static string GetFullPath(this string relativePath)
```

#### GetPathRoot
获取路径的根目录。
```csharp
public static string GetPathRoot(this string path)
```

#### GetParentDirectory
获取路径的父目录路径。
```csharp
public static string GetParentDirectory(this string path)
```

#### ChangeExtension
更改路径的扩展名。
```csharp
public static string ChangeExtension(this string path, string newExtension)
```

#### NormalizePath
获取路径的规范化形式。
```csharp
public static string NormalizePath(this string path)
```

#### GetRelativePath
获取路径的相对路径。
```csharp
public static string GetRelativePath(this string path, string basePath)
```

---

### 11. 路径判断

#### IsAbsolute
判断路径是否为绝对路径。
```csharp
public static bool IsAbsolute(this string path)
```

#### IsRelative
判断路径是否为相对路径。
```csharp
public static bool IsRelative(this string path)
```

#### IsUncPath
判断路径是否为 UNC 路径。
```csharp
public static bool IsUncPath(this string path)
```

#### HasInvalidChars
判断路径是否包含无效字符。
```csharp
public static bool HasInvalidChars(this string path)
```

#### IsRootDirectory
判断路径是否为根目录。
```csharp
public static bool IsRootDirectory(this string path)
```

---

### 12. 目录操作

#### CreateDirectory
创建目录。
```csharp
public static void CreateDirectory(this string directoryPath)
```

#### DeleteDirectory
删除目录。
```csharp
public static void DeleteDirectory(this string directoryPath, bool recursive = true)
```

#### GetFiles
获取目录下的所有文件。
```csharp
public static string[] GetFiles(this string directoryPath, string searchPattern = "*.*", bool recursive = false)
```

#### GetDirectories
获取目录下的所有子目录。
```csharp
public static string[] GetDirectories(this string directoryPath, string searchPattern = "*", bool recursive = false)
```

#### GetTempFilePath
获取临时文件路径。
```csharp
public static string GetTempFilePath()
```

#### GetTempDirectory
获取临时目录路径。
```csharp
public static string GetTempDirectory()
```

---

## 使用场景

1. **文件管理** - 读写文件、复制移动文件、获取文件信息等操作
2. **路径处理** - 路径拼接、分解、规范化等
3. **文件系统遍历** - 创建删除、移动目录和文件
4. **文件验证** - 检查文件是否存在、验证路径有效性
5. **文件属性管理** - 设置文件只读、隐藏等属性
6. **文件校验** - 计算文件哈希值进行完整性校验
7. **目录遍历** - 获取目录下的文件和子目录信息
8. **临时文件管理** - 创建和管理临时文件及目录

## 注意事项

1. 所有方法都是扩展方法，需要通过相应实例来调用
2. 带有 "Safe" 后缀的方法对 null 值进行了安全处理，不会抛出异常
3. 大部分文件操作方法会先检查文件是否存在，避免操作不存在的文件
4. 路径处理方法支持跨平台路径分隔符处理
5. 文件哈希计算方法使用 using 确保资源正确释放
6. 递归方法提供可选参数控制递归深度
7. 属性操作方法在操作前会检查文件是否存在
8. 所有读写方法对 null 参数进行了安全处理
9. 路径规范化方法使用 Uri 进行跨平台兼容处理
