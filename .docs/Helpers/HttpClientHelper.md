# HttpClientHelper 帮助类

## 概述

HttpClientHelper 是一个功能强大的 HTTP 客户端封装类，提供了丰富的 HTTP 请求方法、重试机制、超时控制、认证支持、文件上传下载等功能。该类实现了 IHttpClientHelper 接口，旨在简化 HTTP 通信的复杂性，提高开发效率和代码可维护性。

## 主要特性

- 支持基础 HTTP 方法：GET、POST、PUT、DELETE 等
- 提供重试机制和超时控制
- 支持多种认证方式（Basic Auth 等）
- 内置 JSON 序列化和反序列化
- 文件上传和下载功能，支持进度监控
- 批量请求处理（并行和顺序）
- 自定义请求头和默认配置
- 完整的异常处理和日志记录
- IDisposable 接口实现，支持资源释放

## 类定义

```csharp
public class HttpClientHelper : IHttpClientHelper, IDisposable
{
    // 实现 IHttpClientHelper 接口的所有方法
    // 提供 HTTP 客户端的各种功能
}
```

## 接口定义

### IHttpClientHelper 接口

```csharp
public interface IHttpClientHelper : IDisposable
{
    // 基础 HTTP 请求方法
    Task<T> GetAsync<T>(string url, Dictionary<string, string> headers = null);
    Task<string> GetStringAsync(string url, Dictionary<string, string> headers = null);
    Task<HttpResponseMessage> GetHttpResponseAsync(string url, Dictionary<string, string> headers = null);
    Task<T> PostAsync<T>(string url, HttpContent content, Dictionary<string, string> headers = null);
    Task<T> PostJsonAsync<T>(string url, object data, Dictionary<string, string> headers = null);
    Task<T> PostFormAsync<T>(string url, Dictionary<string, string> formData, Dictionary<string, string> headers = null);
    Task<T> PutAsync<T>(string url, HttpContent content, Dictionary<string, string> headers = null);
    Task<T> PutJsonAsync<T>(string url, object data, Dictionary<string, string> headers = null);
    Task<T> DeleteAsync<T>(string url, Dictionary<string, string> headers = null);
    Task<T> SendAsync<T>(HttpRequestMessage request);
    Task<HttpResponseMessage> SendAsync(HttpRequestMessage request);

    // 高级 HTTP 请求方法
    Task<T> GetWithRetryAsync<T>(string url, int retryCount = 3, Dictionary<string, string> headers = null);
    Task<T> PostWithRetryAsync<T>(string url, HttpContent content, int retryCount = 3, Dictionary<string, string> headers = null);
    Task<T> GetWithTimeoutAsync<T>(string url, TimeSpan timeout, Dictionary<string, string> headers = null);
    Task<T> PostWithTimeoutAsync<T>(string url, HttpContent content, TimeSpan timeout, Dictionary<string, string> headers = null);
    Task<T> GetWithAuthAsync<T>(string url, string username, string password, Dictionary<string, string> headers = null);
    Task<T> GetWithBasicAuthAsync<T>(string url, string username, string password, Dictionary<string, string> headers = null);

    // 文件处理方法
    Task<DownloadResult> DownloadFileAsync(string url, string savePath, Action<DownloadProgress> progressCallback = null, Dictionary<string, string> headers = null);
    Task<UploadResult> UploadFileAsync(string url, string filePath, string fileName = null, Dictionary<string, string> headers = null);

    // 配置和工具方法
    void SetDefaultHeaders(Dictionary<string, string> headers);
    void SetDefaultTimeout(TimeSpan timeout);
    void ClearDefaultHeaders();
    HttpClient GetHttpClient();
    CookieContainer GetCookieContainer();
    void AddHeaders(HttpRequestMessage request, Dictionary<string, string> headers);
    string SerializeToJson(object obj);
    T DeserializeFromJson<T>(string json);

    // 批量请求处理
    Task<List<BatchResponse>> SendBatchAsync(List<BatchRequest> requests);
    Task<List<BatchResponse>> SendSequentialAsync(List<BatchRequest> requests);

    // 监控和统计
    HttpClientStatistics GetStatistics();
    void EnableLogging(bool enable = true);
}
```

## 构造函数

```csharp
/// <summary>
/// 初始化 HttpClientHelper 类的新实例
/// </summary>
public HttpClientHelper();

/// <summary>
/// 初始化 HttpClientHelper 类的新实例，并指定超时时间
/// </summary>
/// <param name="timeoutMilliseconds">超时时间（毫秒）</param>
public HttpClientHelper(int timeoutMilliseconds);

/// <summary>
/// 初始化 HttpClientHelper 类的新实例，并指定超时时间和是否使用默认凭证
/// </summary>
/// <param name="timeoutMilliseconds">超时时间（毫秒）</param>
/// <param name="useDefaultCredentials">是否使用默认凭证</param>
public HttpClientHelper(int timeoutMilliseconds, bool useDefaultCredentials);

/// <summary>
/// 初始化 HttpClientHelper 类的新实例，并指定配置选项
/// </summary>
/// <param name="options">HTTP 客户端配置选项</param>
public HttpClientHelper(HttpClientOptions options);
```

## 属性

该类没有公共属性。

## 方法

### 基础 HTTP 请求方法

#### GetAsync

```csharp
/// <summary>
/// 发送 GET 请求并将响应反序列化为指定类型
/// </summary>
/// <typeparam name="T">响应数据类型</typeparam>
/// <param name="url">请求 URL</param>
/// <param name="headers">自定义请求头（可选）</param>
/// <returns>反序列化后的响应对象</returns>
public async Task<T> GetAsync<T>(string url, Dictionary<string, string> headers = null);
```

#### GetStringAsync

```csharp
/// <summary>
/// 发送 GET 请求并获取字符串响应
/// </summary>
/// <param name="url">请求 URL</param>
/// <param name="headers">自定义请求头（可选）</param>
/// <returns>响应内容字符串</returns>
public async Task<string> GetStringAsync(string url, Dictionary<string, string> headers = null);
```

#### PostJsonAsync

```csharp
/// <summary>
/// 发送 POST 请求，将对象序列化为 JSON 并作为请求体
/// </summary>
/// <typeparam name="T">响应数据类型</typeparam>
/// <param name="url">请求 URL</param>
/// <param name="data">要序列化的对象</param>
/// <param name="headers">自定义请求头（可选）</param>
/// <returns>反序列化后的响应对象</returns>
public async Task<T> PostJsonAsync<T>(string url, object data, Dictionary<string, string> headers = null);
```

### 高级 HTTP 请求方法

#### GetWithRetryAsync

```csharp
/// <summary>
/// 发送 GET 请求并自动重试指定次数
/// </summary>
/// <typeparam name="T">响应数据类型</typeparam>
/// <param name="url">请求 URL</param>
/// <param name="retryCount">重试次数，默认为 3</param>
/// <param name="headers">自定义请求头（可选）</param>
/// <returns>反序列化后的响应对象</returns>
public async Task<T> GetWithRetryAsync<T>(string url, int retryCount = 3, Dictionary<string, string> headers = null);
```

#### GetWithTimeoutAsync

```csharp
/// <summary>
/// 发送 GET 请求并设置特定超时时间
/// </summary>
/// <typeparam name="T">响应数据类型</typeparam>
/// <param name="url">请求 URL</param>
/// <param name="timeout">超时时间</param>
/// <param name="headers">自定义请求头（可选）</param>
/// <returns>反序列化后的响应对象</returns>
public async Task<T> GetWithTimeoutAsync<T>(string url, TimeSpan timeout, Dictionary<string, string> headers = null);
```

#### GetWithBasicAuthAsync

```csharp
/// <summary>
/// 发送带有 Basic 认证的 GET 请求
/// </summary>
/// <typeparam name="T">响应数据类型</typeparam>
/// <param name="url">请求 URL</param>
/// <param name="username">用户名</param>
/// <param name="password">密码</param>
/// <param name="headers">自定义请求头（可选）</param>
/// <returns>反序列化后的响应对象</returns>
public async Task<T> GetWithBasicAuthAsync<T>(string url, string username, string password, Dictionary<string, string> headers = null);
```

### 文件处理方法

#### DownloadFileAsync

```csharp
/// <summary>
/// 下载文件并保存到指定路径
/// </summary>
/// <param name="url">文件 URL</param>
/// <param name="savePath">保存路径</param>
/// <param name="progressCallback">进度回调函数（可选）</param>
/// <param name="headers">自定义请求头（可选）</param>
/// <returns>下载结果</returns>
public async Task<DownloadResult> DownloadFileAsync(string url, string savePath, Action<DownloadProgress> progressCallback = null, Dictionary<string, string> headers = null);
```

#### UploadFileAsync

```csharp
/// <summary>
/// 上传文件到指定 URL
/// </summary>
/// <param name="url">上传 URL</param>
/// <param name="filePath">本地文件路径</param>
/// <param name="fileName">上传文件名（可选，默认为原文件名）</param>
/// <param name="headers">自定义请求头（可选）</param>
/// <returns>上传结果</returns>
public async Task<UploadResult> UploadFileAsync(string url, string filePath, string fileName = null, Dictionary<string, string> headers = null);
```

### 配置和工具方法

#### SetDefaultHeaders

```csharp
/// <summary>
/// 设置默认请求头
/// </summary>
/// <param name="headers">请求头字典</param>
public void SetDefaultHeaders(Dictionary<string, string> headers);
```

#### SetDefaultTimeout

```csharp
/// <summary>
/// 设置默认超时时间
/// </summary>
/// <param name="timeout">超时时间</param>
public void SetDefaultTimeout(TimeSpan timeout);
```

### 批量请求处理

#### SendBatchAsync

```csharp
/// <summary>
/// 并行发送批量请求
/// </summary>
/// <param name="requests">请求列表</param>
/// <returns>响应列表</returns>
public async Task<List<BatchResponse>> SendBatchAsync(List<BatchRequest> requests);
```

#### SendSequentialAsync

```csharp
/// <summary>
/// 顺序发送批量请求
/// </summary>
/// <param name="requests">请求列表</param>
/// <returns>响应列表</returns>
public async Task<List<BatchResponse>> SendSequentialAsync(List<BatchRequest> requests);
```

### 监控和统计

#### GetStatistics

```csharp
/// <summary>
/// 获取 HTTP 客户端统计信息
/// </summary>
/// <returns>统计信息</returns>
public HttpClientStatistics GetStatistics();
```

## 数据结构

### DownloadProgress

```csharp
/// <summary>
/// 下载进度信息
/// </summary>
public class DownloadProgress
{
    /// <summary>
    /// 总字节数
    /// </summary>
    public long TotalBytes { get; set; }

    /// <summary>
    /// 已下载字节数
    /// </summary>
    public long DownloadedBytes { get; set; }

    /// <summary>
    /// 进度百分比
    /// </summary>
    public int ProgressPercentage { get; set; }
}
```

### UploadProgress

```csharp
/// <summary>
/// 上传进度信息
/// </summary>
public class UploadProgress
{
    /// <summary>
    /// 总字节数
    /// </summary>
    public long TotalBytes { get; set; }

    /// <summary>
    /// 已上传字节数
    /// </summary>
    public long UploadedBytes { get; set; }

    /// <summary>
    /// 进度百分比
    /// </summary>
    public int ProgressPercentage { get; set; }
}
```

### BatchRequest

```csharp
/// <summary>
/// 批量请求
/// </summary>
public class BatchRequest
{
    /// <summary>
    /// HTTP 方法
    /// </summary>
    public HttpMethod Method { get; set; }

    /// <summary>
    /// URL
    /// </summary>
    public string Url { get; set; }

    /// <summary>
    /// 请求内容
    /// </summary>
    public HttpContent Content { get; set; }

    /// <summary>
    /// 请求头
    /// </summary>
    public Dictionary<string, string> Headers { get; set; } = new Dictionary<string, string>();
}
```

### BatchResponse

```csharp
/// <summary>
/// 批量响应
/// </summary>
public class BatchResponse
{
    /// <summary>
    /// 请求
    /// </summary>
    public BatchRequest Request { get; set; }

    /// <summary>
    /// HTTP 响应
    /// </summary>
    public HttpResponseMessage Response { get; set; }

    /// <summary>
    /// 响应内容
    /// </summary>
    public string Content { get; set; }

    /// <summary>
    /// 是否成功
    /// </summary>
    public bool Success { get; set; }

    /// <summary>
    /// 错误信息
    /// </summary>
    public string ErrorMessage { get; set; }
}
```

## 使用示例

### 基础 GET 请求

```csharp
var httpClientHelper = new HttpClientHelper();
var result = await httpClientHelper.GetAsync<UserInfo>("https://api.example.com/users/1");
```

### 带 JSON 数据的 POST 请求

```csharp
var userData = new { Name = "John Doe", Email = "john@example.com" };
var result = await httpClientHelper.PostJsonAsync<CreateUserResponse>("https://api.example.com/users", userData);
```

### 带重试机制的请求

```csharp
var result = await httpClientHelper.GetWithRetryAsync<DataModel>("https://api.example.com/data", retryCount: 5);
```

### 带认证的请求

```csharp
var result = await httpClientHelper.GetWithBasicAuthAsync<ProtectedData>(
    "https://api.example.com/protected", "username", "password");
```

### 文件下载

```csharp
var result = await httpClientHelper.DownloadFileAsync(
    "https://example.com/file.pdf", 
    @"C:\Downloads\file.pdf",
    progress => Console.WriteLine($"下载进度: {progress.ProgressPercentage}%")
);
```

### 批量请求

```csharp
var requests = new List<BatchRequest>
{
    new BatchRequest { Method = HttpMethod.Get, Url = "https://api.example.com/users/1" },
    new BatchRequest { Method = HttpMethod.Get, Url = "https://api.example.com/products/2" }
};

var responses = await httpClientHelper.SendBatchAsync(requests);
foreach (var response in responses)
{
    Console.WriteLine($"URL: {response.Request.Url}, Success: {response.Success}");
}
```

## 最佳实践

1. **单例模式**：推荐在应用程序中使用 HttpClientHelper 的单例实例，避免频繁创建和销毁 HttpClient 对象。

2. **使用 using 语句**：在不再需要 HttpClientHelper 实例时，使用 using 语句或手动调用 Dispose() 方法释放资源。

```csharp
using (var httpClientHelper = new HttpClientHelper())
{
    // 使用 httpClientHelper 发送请求
}
```

3. **配置默认超时**：根据实际需求设置合理的默认超时时间。

```csharp
var httpClientHelper = new HttpClientHelper();
httpClientHelper.SetDefaultTimeout(TimeSpan.FromSeconds(30));
```

4. **处理异常**：所有异步方法都可能抛出异常，请确保在调用时进行适当的异常处理。

```csharp
try
{
    var result = await httpClientHelper.GetAsync<Data>(url);
    // 处理结果
}
catch (HttpRequestException ex)
{
    // 处理 HTTP 请求异常
}
catch (TaskCanceledException ex)
{
    // 处理超时异常
}
catch (Exception ex)
{
    // 处理其他异常
}
```

5. **设置默认请求头**：为所有请求设置通用的请求头，如 Content-Type、Accept 等。

```csharp
httpClientHelper.SetDefaultHeaders(new Dictionary<string, string>
{
    { "Content-Type", "application/json" },
    { "Accept", "application/json" },
    { "User-Agent", "MyApplication/1.0" }
});
```

## 注意事项

- **线程安全**：HttpClientHelper 实例本身不是线程安全的。在多线程环境中使用时，请确保适当的同步机制，或者为每个线程创建单独的实例。

- **连接池**：HttpClientHelper 内部使用的 HttpClient 实例会管理连接池，无需手动管理连接。

- **Cookie 处理**：默认启用 Cookie 处理，可以通过 GetCookieContainer() 方法访问和操作 Cookie。

- **日志记录**：类内部使用 ILogger 接口进行日志记录，建议在实际应用中提供合适的日志记录器实现。

- **重试策略**：重试机制适用于临时网络问题，但不应该过度依赖，应确保 API 端点是幂等的。

## 版本兼容性

- .NET Framework 4.6.1 及以上版本
- .NET Core 2.0 及以上版本
- .NET 5.0 及以上版本

## 故障排除

1. **请求超时**：如果请求经常超时，可以增加默认超时时间或为特定请求设置更长的超时时间。

2. **认证失败**：检查用户名和密码是否正确，以及认证方式是否与 API 要求一致。

3. **网络连接问题**：检查网络连接状态，以及是否存在防火墙或代理服务器阻止了请求。

4. **服务器错误**：检查响应状态码和响应内容，分析服务器返回的错误信息。

5. **序列化/反序列化问题**：确保 JSON 数据结构与目标类型匹配，检查是否需要自定义序列化设置。