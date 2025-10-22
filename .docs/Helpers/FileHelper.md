# FileHelper 类功能文档

## 概述

[FileHelper](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\FileHelper.cs#L11-L1054) 是一个静态工具类，提供了全面的文件和目录操作功能。该类集成了文件基础操作、读写操作、属性管理、目录操作、压缩解压、安全校验等多种功能，旨在简化 .NET 中的文件系统操作。

## 主要功能模块

### 1. 文件基础操作

提供基本的文件和目录操作功能，包括检查存在性、创建、删除、复制、移动等。

**主要方法：**
- [FileExists()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\FileHelper.cs#L19-L22) - 检查文件是否存在
- [DirectoryExists()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\FileHelper.cs#L28-L31) - 检查目录是否存在
- [CreateDirectory()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\FileHelper.cs#L37-L42) - 创建目录
- [DeleteFile()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\FileHelper.cs#L49-L58) - 删除文件
- [DeleteDirectory()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\FileHelper.cs#L65-L70) - 删除目录及其所有内容
- [CopyFile()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\FileHelper.cs#L77-L87) - 复制文件
- [MoveFile()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\FileHelper.cs#L95-L114) - 移动文件
- [GetFileInfo()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\FileHelper.cs#L120-L123) - 获取文件信息
- [GetDirectoryInfo()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\FileHelper.cs#L129-L132) - 获取目录信息
- [GetFileSize()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\FileHelper.cs#L139-L148) - 获取文件大小（字节）

### 2. 文件读写操作

提供文本和二进制文件的读写功能，支持多种编码格式和安全写入方式。

**主要方法：**
- [ReadTextFile()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\FileHelper.cs#L158-L168) - 读取文本文件内容
- [ReadLines()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\FileHelper.cs#L177-L187) - 逐行读取文本文件内容
- [ReadBinaryFile()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\FileHelper.cs#L195-L204) - 读取二进制文件内容
- [WriteTextFile()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\FileHelper.cs#L213-L227) - 写入文本到文件
- [WriteLines()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\FileHelper.cs#L237-L251) - 逐行写入文本到文件
- [WriteBinaryFile()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\FileHelper.cs#L259-L265) - 写入二进制数据到文件
- [SafeWriteTextFile()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\FileHelper.cs#L274-L291) - 安全写入文件（先写入临时文件，再替换原文件）
- [AppendText()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\FileHelper.cs#L299-L302) - 追加文本到文件
- [ReadPartialFile()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\FileHelper.cs#L311-L322) - 读取文件的部分内容
- [ReadFileInChunks()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\FileHelper.cs#L330-L348) - 流式读取大文件

### 3. 文件属性与元数据

提供文件属性和元数据的获取与设置功能。

**主要方法：**
- [GetFileCreationTime()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\FileHelper.cs#L357-L360) - 获取文件创建时间
- [GetFileLastAccessTime()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\FileHelper.cs#L367-L370) - 获取文件最后访问时间
- [GetFileLastWriteTime()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\FileHelper.cs#L377-L380) - 获取文件最后修改时间
- [SetFileCreationTime()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\FileHelper.cs#L387-L390) - 设置文件创建时间
- [SetFileLastAccessTime()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\FileHelper.cs#L397-L400) - 设置文件最后访问时间
- [SetFileLastWriteTime()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\FileHelper.cs#L407-L410) - 设置文件最后修改时间
- [GetFileAttributes()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\FileHelper.cs#L416-L419) - 获取文件属性
- [SetFileAttributes()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\FileHelper.cs#L426-L429) - 设置文件属性
- [GetFileExtension()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\FileHelper.cs#L435-L438) - 获取文件扩展名
- [GetFileNameWithoutExtension()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\FileHelper.cs#L445-L448) - 获取不带扩展名的文件名

### 4. 目录操作

提供目录相关的操作功能，包括文件和子目录的枚举、目录大小计算、复制等。

**主要方法：**
- [GetFiles()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\FileHelper.cs#L459-L462) - 获取目录下的所有文件
- [GetDirectories()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\FileHelper.cs#L472-L475) - 获取目录下的所有子目录
- [GetFileSystemEntries()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\FileHelper.cs#L483-L486) - 获取目录下的所有文件和子目录
- [GetDirectorySize()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\FileHelper.cs#L495-L507) - 计算目录大小
- [CopyDirectory()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\FileHelper.cs#L516-L542) - 复制目录及其所有内容
- [ClearDirectory()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\FileHelper.cs#L550-L574) - 清空目录内容
- [GetParentDirectory()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\FileHelper.cs#L580-L583) - 获取父目录路径
- [GetDirectoryName()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\FileHelper.cs#L589-L592) - 获取目录名称
- [GetCurrentDirectory()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\FileHelper.cs#L598-L601) - 获取程序当前运行目录
- [SetCurrentDirectory()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\FileHelper.cs#L607-L610) - 设置当前工作目录

### 5. 文件压缩与解压

提供文件和目录的压缩与解压功能，支持 GZip 和 ZIP 格式。

**主要方法：**
- [CompressFile()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\FileHelper.cs#L619-L632) - 压缩文件
- [DecompressFile()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\FileHelper.cs#L641-L655) - 解压文件
- [CompressDirectory()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\FileHelper.cs#L663-L672) - 压缩目录
- [DecompressDirectory()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\FileHelper.cs#L680-L690) - 解压目录
- [AddFileToZip()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\FileHelper.cs#L699-L706) - 向ZIP压缩包添加文件
- [ExtractFileFromZip()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\FileHelper.cs#L715-L729) - 从ZIP压缩包提取文件
- [ListZipEntries()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\FileHelper.cs#L737-L750) - 列出ZIP压缩包中的所有条目
- [CompressText()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\FileHelper.cs#L759-L774) - 压缩文本内容
- [DecompressText()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\FileHelper.cs#L783-L802) - 解压文本内容
- [CreateZipFromFiles()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\FileHelper.cs#L810-L823) - 创建ZIP压缩包并添加多个文件

### 6. 文件安全与校验

提供文件安全和完整性校验功能，包括哈希计算、文件比较、加密解密等。

**主要方法：**
- [CalculateMD5()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\FileHelper.cs#L832-L844) - 计算文件MD5哈希值
- [CalculateSHA1()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\FileHelper.cs#L852-L864) - 计算文件SHA1哈希值
- [CalculateSHA256()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\FileHelper.cs#L872-L884) - 计算文件SHA256哈希值
- [CompareFiles()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\FileHelper.cs#L892-L906) - 比较两个文件是否相同
- [EncryptFile()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\FileHelper.cs#L915-L940) - 加密文件
- [DecryptFile()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\FileHelper.cs#L950-L977) - 解密文件
- [CalculateCRC32()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\FileHelper.cs#L985-L1008) - 计算文件CRC32校验值
- [HideFile()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\FileHelper.cs#L1025-L1033) - 隐藏文件
- [UnhideFile()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\FileHelper.cs#L1040-L1048) - 取消文件隐藏
- [IsReadOnly()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\FileHelper.cs#L1050-L1053) - 检查文件是否为只读

## 使用场景

1. **文件管理** - 简化应用程序中的文件和目录操作
2. **数据持久化** - 提供文本和二进制数据的读写功能
3. **文件安全** - 实现文件加密、哈希校验等安全功能
4. **数据压缩** - 减少存储空间和传输带宽
5. **批量处理** - 处理大量文件和目录操作
6. **备份与同步** - 实现文件复制、移动和比较功能

## 注意事项

1. 部分方法在文件或目录不存在时会抛出异常
2. 加密功能使用 AES 算法，密钥和初始化向量基于提供的密码生成
3. 大文件操作建议使用流式处理方法以避免内存问题
4. 压缩和解压功能依赖于 .NET 的 GZipStream 和 ZipFile 类
5. 安全写入功能通过临时文件机制确保文件写入的原子性
6. 部分文件属性操作在不同操作系统上可能有不同的行为