# StreamExtensions 类功能文档

## 概述

[StreamExtensions](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\StreamExtensions.cs#L8-L255) 是一个静态扩展类，为 `Stream` 类型提供了丰富的扩展方法。该类包含流的读取、写入、转换、判断、操作等多种功能，旨在简化流处理操作，提高代码的安全性和可读性，特别适用于需要处理各种流类型的日常开发场景。

## 主要功能模块

### 1. 流状态判断方法

提供流属性和状态检查的便捷方法。

**主要方法：**
- [CanReadSafe()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\StreamExtensions.cs#L13-L13) - 判断流是否可读
- [CanWriteSafe()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\StreamExtensions.cs#L19-L19) - 判断流是否可写
- [CanSeekSafe()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\StreamExtensions.cs#L25-L25) - 判断流是否可查找（支持 Seek）
- [IsNullOrEmpty()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\StreamExtensions.cs#L31-L31) - 判断流是否为空或长度为零
- [IsMemoryStream()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\StreamExtensions.cs#L209-L209) - 判断流是否为 MemoryStream
- [IsFileStream()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\StreamExtensions.cs#L215-L215) - 判断流是否为 FileStream
- [IsNullStream()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\StreamExtensions.cs#L221-L221) - 判断流是否为空流（Stream.Null）

### 2. 流转换方法

提供流与其他数据类型之间的相互转换功能。

**主要方法：**
- [ToBytes()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\StreamExtensions.cs#L37-L52) - 将流内容读取为字节数组
- [ToText()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\StreamExtensions.cs#L58-L62) - 将流内容读取为字符串（默认 UTF8 编码）
- [ToStream()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\StreamExtensions.cs#L121-L125) - 将文件内容读取为流
- [ToStream()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\StreamExtensions.cs#L131-L136) - 将字符串转换为流（默认 UTF8 编码）
- [ToStream()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\StreamExtensions.cs#L142-L146) - 将字节数组转换为流

### 3. 流写入方法

提供向流写入数据的功能。

**主要方法：**
- [WriteText()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\StreamExtensions.cs#L68-L77) - 将字符串写入流（覆盖原内容，默认 UTF8 编码）
- [WriteBytes()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\StreamExtensions.cs#L83-L91) - 将字节数组写入流（覆盖原内容）

### 4. 流文件操作方法

提供流与文件系统之间的交互功能。

**主要方法：**
- [SaveToFile()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\StreamExtensions.cs#L97-L103) - 将流内容保存到文件（覆盖）

### 5. 流复制方法

提供流之间的数据复制功能。

**主要方法：**
- [CopyToStream()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\StreamExtensions.cs#L152-L159) - 将流内容复制到另一个流

### 6. 流位置操作方法

提供流位置控制和部分读取功能。

**主要方法：**
- [ResetPosition()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\StreamExtensions.cs#L165-L168) - 重置流到起始位置
- [ReadBytes()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\StreamExtensions.cs#L174-L191) - 读取流的部分内容为字节数组
- [ReadText()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\StreamExtensions.cs#L197-L203) - 读取流的部分内容为字符串（默认 UTF8 编码）
- [GetPosition()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\StreamExtensions.cs#L233-L233) - 获取流的当前位置
- [SetPosition()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\StreamExtensions.cs#L239-L244) - 设置流的当前位置

### 7. 流信息获取方法

提供流元数据获取功能。

**主要方法：**
- [GetLength()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\StreamExtensions.cs#L227-L227) - 获取流的长度（字节）

### 8. 流释放方法

提供安全的流关闭和释放功能。

**主要方法：**
- [CloseSafe()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\StreamExtensions.cs#L250-L254) - 关闭并释放流

## 使用场景

1. **文件处理** - 读写文件流、保存流到文件、从文件创建流
2. **网络通信** - 处理网络数据流的读取和写入
3. **数据转换** - 流与字符串、字节数组之间的相互转换
4. **内存操作** - MemoryStream 的创建和操作
5. **数据传输** - 流之间的数据复制和传输
6. **数据解析** - 读取流的部分内容进行解析处理
7. **编码处理** - 不同编码格式的文本流处理
8. **资源管理** - 安全地关闭和释放流资源

## 注意事项

1. 所有方法都是扩展方法，需要通过 `Stream` 实例调用
2. 带有 "Safe" 后缀的方法都对 null 值进行了安全处理，避免抛出异常
3. [ToBytes()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\StreamExtensions.cs#L37-L52) 方法针对 MemoryStream 进行了优化，直接调用 ToArray() 方法提高性能
4. 读取方法在操作前后会保存和恢复流的原始位置
5. 写入方法会先清空流内容（SetLength(0)），然后写入新数据
6. [ReadBytes()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\StreamExtensions.cs#L174-L191) 方法处理了读取不足的情况，返回实际读取的数据
7. [SetPosition()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\StreamExtensions.cs#L239-L244) 方法对位置参数进行了边界检查，确保在有效范围内
8. 所有编码相关方法默认使用 UTF8 编码，支持自定义编码
9. 文件操作方法使用 using 语句确保资源正确释放
10. 流复制方法会自动重置源流位置到起始位置