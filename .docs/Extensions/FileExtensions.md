# FileExtensions 类功能文档

## 概述

[FileExtensions](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\FileExtensions.cs#L7-L760) 是一个静态扩展类，为文件系统相关类型（string、FileInfo、DirectoryInfo）提供了丰富的扩展方法。该类包含文件读写、路径处理、信息获取、属性操作等多种功能，旨在简化文件系统操作，提高代码的安全性和可读性，适用于各种文件处理场景。

## 主要功能模块

### 1. 文件操作扩展方法（string 类型）

提供基于文件路径字符串的文件操作功能。

#### 文件状态判断方法
- [Exists()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\FileExtensions.cs#L12-L12) - 判断文件是否存在

#### 文件读取方法
- [ReadAllText()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\FileExtensions.cs#L18-L19) - 读取文件所有文本内容
- [ReadAllLines()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\FileExtensions.cs#L25-L26) - 读取文件所有行内容
- [ReadAllBytes()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\FileExtensions.cs#L32-L33) - 读取文件所有字节内容

#### 文件写入方法
- [WriteAllText()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\FileExtensions.cs#L39-L40) - 写入文本内容到文件（覆盖）
- [WriteAllLines()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\FileExtensions.cs#L46-L47) - 写入多行内容到文件（覆盖）
- [WriteAllBytes()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\FileExtensions.cs#L53-L54) - 写入字节内容到文件（覆盖）
- [AppendText()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\FileExtensions.cs#L60-L61) - 追加文本内容到文件
- [AppendLines()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\FileExtensions.cs#L67-L68) - 追加多行内容到文件

#### 文件操作方法
- [DeleteFile()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\FileExtensions.cs#L74-L77) - 删除文件
- [CopyTo()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\FileExtensions.cs#L83-L87) - 复制文件到目标路径
- [MoveTo()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\FileExtensions.cs#L94-L100) - 移动文件到目标路径

#### 文件信息获取方法
- [GetFileSize()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\FileExtensions.cs#L106-L107) - 获取文件大小（字节）
- [GetCreationTime()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\FileExtensions.cs#L113-L114) - 获取文件创建时间
- [GetLastWriteTime()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\FileExtensions.cs#L120-L121) - 获取文件最后修改时间
- [GetLastAccessTime()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\FileExtensions.cs#L127-L128) - 获取文件最后访问时间

#### 文件属性操作方法
- [IsReadOnly()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\FileExtensions.cs#L134-L135) - 判断文件是否为只读
- [SetReadOnly()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\FileExtensions.cs#L141-L144) - 设置文件为只读
- [UnsetReadOnly()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\FileExtensions.cs#L150-L153) - 取消文件只读属性
- [IsHidden()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\FileExtensions.cs#L186-L191) - 判断文件是否为隐藏文件
- [SetHidden()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\FileExtensions.cs#L197-L203) - 设置文件为隐藏文件
- [UnsetHidden()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\FileExtensions.cs#L209-L215) - 取消文件隐藏属性

#### 文件哈希值方法
- [GetFileMd5()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\FileExtensions.cs#L159-L166) - 获取文件的 MD5 值
- [GetFileSha256()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\FileExtensions.cs#L172-L179) - 获取文件的 SHA256 值

### 2. 路径操作扩展方法（string 类型）

提供路径处理和分析功能。

#### 路径判断方法
- [IsAbsolute()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\FileExtensions.cs#L225-L226) - 判断路径是否为绝对路径
- [IsRelative()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\FileExtensions.cs#L232-L233) - 判断路径是否为相对路径
- [IsUncPath()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\FileExtensions.cs#L315-L316) - 判断路径是否为 UNC 路径
- [IsRootDirectory()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\FileExtensions.cs#L352-L357) - 判断路径是否为根目录
- [IsFile()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\FileExtensions.cs#L378-L379) - 判断路径是否为文件
- [IsDirectory()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\FileExtensions.cs#L385-L386) - 判断路径是否为目录
- [ExistsPath()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\FileExtensions.cs#L393-L394) - 判断路径的文件或目录是否存在

#### 路径分解方法
- [GetExtension()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\FileExtensions.cs#L240-L241) - 获取文件扩展名（带点）
- [GetFileName()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\FileExtensions.cs#L247-L248) - 获取文件名（不含路径）
- [GetFileNameWithoutExtension()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\FileExtensions.cs#L254-L255) - 获取文件名（不含扩展名）
- [GetDirectoryName()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\FileExtensions.cs#L261-L262) - 获取文件所在目录路径
- [GetPathRoot()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\FileExtensions.cs#L275-L276) - 获取路径的根目录
- [GetParentDirectory()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\FileExtensions.cs#L344-L349) - 获取路径的父目录路径
- [SplitDirectories()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\FileExtensions.cs#L323-L327) - 获取路径的各级目录（分解为数组）

#### 路径操作方法
- [CombinePaths()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\FileExtensions.cs#L268-L269) - 合并多个路径为一个完整路径
- [GetFullPath()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\FileExtensions.cs#L282-L283) - 获取路径的完整规范路径（绝对路径）
- [ChangeExtension()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\FileExtensions.cs#L296-L297) - 更改路径的扩展名
- [NormalizePath()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\FileExtensions.cs#L363-L367) - 获取路径的规范化形式
- [GetRelativePath()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\FileExtensions.cs#L372-L375) - 获取路径的相对路径

#### 路径验证方法
- [HasInvalidChars()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\FileExtensions.cs#L303-L308) - 判断路径是否包含无效字符
- [FileNameHasInvalidChars()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\FileExtensions.cs#L310-L313) - 判断文件名是否包含无效字符

#### 系统路径获取方法
- [GetTempFilePath()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\FileExtensions.cs#L286-L286) - 获取临时文件路径
- [GetTempDirectory()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\FileExtensions.cs#L289-L289) - 获取临时目录路径
- [GetDirectorySeparator()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\FileExtensions.cs#L318-L318) - 获取路径分隔符
- [GetAltDirectorySeparator()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\FileExtensions.cs#L320-L320) - 获取备用路径分隔符
- [GetVolumeSeparator()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\FileExtensions.cs#L322-L322) - 获取路径卷分隔符

### 3. FileInfo 扩展方法

提供基于 FileInfo 对象的文件操作功能。

#### 文件状态判断方法
- [ExistsSafe()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\FileExtensions.cs#L403-L403) - 判断文件是否存在

#### 文件信息获取方法
- [GetSize()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\FileExtensions.cs#L409-L409) - 获取文件大小（字节）
- [GetExtension()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\FileExtensions.cs#L415-L415) - 获取文件扩展名（带点）
- [GetFileName()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\FileExtensions.cs#L421-L421) - 获取文件名（不含路径）
- [GetDirectoryName()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\FileExtensions.cs#L427-L427) - 获取文件所在目录路径
- [GetCreationTime()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\FileExtensions.cs#L433-L433) - 获取文件创建时间
- [GetLastWriteTime()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\FileExtensions.cs#L439-L439) - 获取文件最后修改时间
- [GetLastAccessTime()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\FileExtensions.cs#L445-L445) - 获取文件最后访问时间

#### 文件属性操作方法
- [IsReadOnly()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\FileExtensions.cs#L451-L451) - 判断文件是否为只读
- [SetReadOnly()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\FileExtensions.cs#L458-L461) - 设置文件为只读
- [UnsetReadOnly()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\FileExtensions.cs#L467-L470) - 取消文件只读属性
- [IsHidden()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\FileExtensions.cs#L539-L544) - 判断文件是否为隐藏文件
- [SetHidden()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\FileExtensions.cs#L550-L553) - 设置文件为隐藏文件
- [UnsetHidden()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\FileExtensions.cs#L559-L562) - 取消文件隐藏属性

#### 文件操作方法
- [DeleteFile()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\FileExtensions.cs#L476-L479) - 删除文件
- [CopyTo()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\FileExtensions.cs#L485-L488) - 复制文件到目标路径
- [MoveTo()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\FileExtensions.cs#L495-L501) - 移动文件到目标路径

#### 文件哈希值方法
- [GetMd5()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\FileExtensions.cs#L507-L514) - 获取文件的 MD5 值
- [GetSha256()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\FileExtensions.cs#L520-L527) - 获取文件的 SHA256 值

### 4. DirectoryInfo 扩展方法

提供基于 DirectoryInfo 对象的目录操作功能。

#### 目录状态判断方法
- [ExistsSafe()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\FileExtensions.cs#L571-L571) - 判断目录是否存在
- [IsHidden()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\FileExtensions.cs#L651-L656) - 判断目录是否为隐藏目录

#### 目录信息获取方法
- [GetDirectoryName()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\FileExtensions.cs#L577-L577) - 获取目录名
- [GetFullPath()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\FileExtensions.cs#L583-L583) - 获取目录完整路径
- [GetCreationTime()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\FileExtensions.cs#L589-L589) - 获取目录创建时间
- [GetLastWriteTime()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\FileExtensions.cs#L595-L595) - 获取目录最后修改时间
- [GetLastAccessTime()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\FileExtensions.cs#L601-L601) - 获取目录最后访问时间

#### 目录内容获取方法
- [GetFilesSafe()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\FileExtensions.cs#L608-L609) - 获取目录下所有文件（可递归）
- [GetDirectoriesSafe()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\FileExtensions.cs#L616-L618) - 获取目录下所有子目录（可递归）
- [GetFilePaths()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\FileExtensions.cs#L640-L648) - 获取目录下所有文件路径（可递归）
- [GetDirectoryPaths()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\FileExtensions.cs#L658-L666) - 获取目录下所有子目录路径（可递归）

#### 目录操作方法
- [CreateSafe()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\FileExtensions.cs#L624-L628) - 创建目录（如果不存在）
- [DeleteSafe()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\FileExtensions.cs#L634-L637) - 删除目录（可递归）
- [MoveTo()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\FileExtensions.cs#L672-L675) - 移动目录到目标路径
- [SetHidden()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\FileExtensions.cs#L679-L682) - 设置目录为隐藏目录
- [UnsetHidden()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\FileExtensions.cs#L688-L691) - 取消目录隐藏属性

## 使用场景

1. **文件处理** - 读写文件、复制移动文件、获取文件信息等操作
2. **路径管理** - 路径组合、分解、规范化处理
3. **文件系统操作** - 创建、删除、移动目录和文件
4. **文件验证** - 检查文件是否存在、验证路径有效性
5. **文件属性管理** - 设置文件只读、隐藏等属性
6. **文件校验** - 计算文件哈希值进行完整性校验
7. **目录遍历** - 获取目录下所有文件和子目录信息
8. **临时文件处理** - 创建和管理临时文件及目录

## 注意事项

1. 所有方法都是扩展方法，需要通过相应的实例调用
2. 带有 "Safe" 后缀的方法都对 null 值进行了安全处理，避免抛出异常
3. 大部分文件操作方法都检查文件是否存在，避免操作不存在的文件
4. 路径操作方法支持跨平台路径分隔符处理
5. 文件哈希计算方法使用 using 语句确保资源正确释放
6. 递归操作方法提供搜索选项参数控制递归深度
7. 属性操作方法在操作前检查对象是否存在
8. 所有写入操作方法都对 null 输入进行了安全处理
9. 路径规范化方法使用 Uri 类进行跨平台兼容处理