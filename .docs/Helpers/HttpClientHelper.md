# HttpClientHelper 类功能文档

## 概述

[HttpClientHelper](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\HttpClientHelper.cs#L11-L894) 是一个静态工具类，提供了丰富的 HTTP 请求功能。该类封装了 .NET 的 `HttpClient`，提供了基础的 HTTP 请求方法、高级特性（如重试机制、超时控制、认证等）、文件上传下载、批量请求处理等功能，旨在简化 HTTP 通信操作。

## 主要功能模块

### 1. 基础 HTTP 请求方法

提供标准的 HTTP 请求方法，包括 GET、POST、PUT、DELETE 等，支持多种数据格式。

**主要方法：**
- [GetAsync()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\HttpClientHelper.cs#L36-L42) - 发送 GET 请求
- [GetStringAsync()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\HttpClientHelper.cs#L52-L57) - 发送 GET 请求并返回字符串内容
- [GetByteArrayAsync()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\HttpClientHelper.cs#L67-L72) - 发送 GET 请求并返回字节数组内容
- [PostAsync()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\HttpClientHelper.cs#L83-L91) - 发送 POST 请求
- [PostJsonAsync<T>()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\HttpClientHelper.cs#L101-L106) - 发送 POST 请求（JSON 数据）
- [PostFormAsync()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\HttpClientHelper.cs#L116-L120) - 发送 POST 请求（表单数据）
- [PutAsync()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\HttpClientHelper.cs#L130-L138) - 发送 PUT 请求
- [PutJsonAsync<T>()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\HttpClientHelper.cs#L148-L153) - 发送 PUT 请求（JSON 数据）
- [DeleteAsync()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\HttpClientHelper.cs#L162-L167) - 发送 DELETE 请求
- [SendAsync()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\HttpClientHelper.cs#L178-L186) - 发送自定义 HTTP 请求

### 2. 高级 HTTP 请求方法

提供增强功能的 HTTP 请求方法，包括重试机制、超时控制、认证、文件上传下载等。

**主要方法：**
- [GetWithRetryAsync()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\HttpClientHelper.cs#L198-L202) - 发送带重试机制的 GET 请求
- [PostWithRetryAsync()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\HttpClientHelper.cs#L213-L218) - 发送带重试机制的 POST 请求
- [GetWithTimeoutAsync()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\HttpClientHelper.cs#L228-L233) - 发送带超时控制的 GET 请求
- [PostWithTimeoutAsync()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\HttpClientHelper.cs#L244-L249) - 发送带超时控制的 POST 请求
- [GetWithAuthAsync()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\HttpClientHelper.cs#L260-L265) - 发送带认证的 GET 请求
- [GetWithBasicAuthAsync()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\HttpClientHelper.cs#L277-L282) - 发送带基本认证的 GET 请求
- [DownloadFileAsync()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\HttpClientHelper.cs#L294-L344) - 下载文件
- [UploadFileAsync()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\HttpClientHelper.cs#L356-L403) - 上传文件

### 3. 配置和工具方法

提供 HTTP 客户端配置和常用的工具方法。

**主要方法：**
- [SetDefaultHeaders()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\HttpClientHelper.cs#L413-L420) - 设置默认请求头
- [SetDefaultTimeout()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\HttpClientHelper.cs#L426-L429) - 设置默认超时时间
- [SetAutomaticDecompression()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\HttpClientHelper.cs#L435-L439) - 启用或禁用自动解压缩
- [SetCookieContainer()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\HttpClientHelper.cs#L445-L448) - 设置 Cookie 容器
- [GetCookieContainer()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\HttpClientHelper.cs#L454-L457) - 获取 Cookie 容器
- [ClearDefaultHeaders()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\HttpClientHelper.cs#L462-L465) - 清除默认请求头
- [SerializeToJson<T>()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\HttpClientHelper.cs#L509-L516) - 序列化对象为 JSON
- [DeserializeFromJson<T>()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\HttpClientHelper.cs#L524-L531) - 反序列化 JSON 为对象
- [GetHttpClient()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\HttpClientHelper.cs#L537-L540) - 获取 HTTP 客户端实例

### 4. 批量请求处理

提供批量处理 HTTP 请求的功能，支持并行和顺序执行。

**主要方法：**
- [SendBatchAsync()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\HttpClientHelper.cs#L551-L599) - 并行发送多个 HTTP 请求
- [SendSequentialAsync()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\HttpClientHelper.cs#L609-L644) - 顺序发送多个 HTTP 请求

### 5. 监控和统计

提供 HTTP 客户端的监控和统计功能。

**主要方法：**
- [GetStatistics()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\HttpClientHelper.cs#L654-L666) - 获取 HTTP 客户端统计信息
- [EnableLogging()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\HttpClientHelper.cs#L673-L677) - 启用请求日志记录

## 数据结构

### 结果类
- [DownloadProgress](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\HttpClientHelper.cs#L700-L717) - 下载进度信息
- [DownloadResult](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\HttpClientHelper.cs#L722-L744) - 下载结果
- [UploadProgress](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\HttpClientHelper.cs#L749-L766) - 上传进度信息
- [UploadResult](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\HttpClientHelper.cs#L771-L793) - 上传结果
- [BatchRequest](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\HttpClientHelper.cs#L798-L819) - 批量请求
- [BatchResponse](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\HttpClientHelper.cs#L824-L850) - 批量响应
- [HttpClientStatistics](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\HttpClientHelper.cs#L855-L892) - HTTP 客户端统计信息

## 使用场景

1. **Web API 调用** - 简化与 RESTful API 的交互
2. **文件传输** - 实现文件的上传和下载功能
3. **批量处理** - 并行或顺序处理多个 HTTP 请求
4. **安全通信** - 支持各种认证机制的 HTTP 请求
5. **可靠通信** - 通过重试机制提高请求成功率
6. **性能优化** - 通过配置优化 HTTP 客户端性能

## 注意事项

1. 该类使用静态 `HttpClient` 实例，遵循 .NET 最佳实践
2. 默认启用了 GZip 和 Deflate 自动解压缩
3. 默认超时时间为 30 秒
4. 重试机制会避免对某些状态码（如 400、401、403）进行重试
5. 文件上传和下载支持进度回调
6. 批量请求支持并发控制
7. JSON 序列化/反序列化使用 camelCase 命名策略
8. 部分高级功能（如日志记录）需要额外实现