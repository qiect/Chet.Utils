using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using System.Diagnostics;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.Json;

namespace Chet.Utils.Helpers
{
    /// <summary>
    /// HTTP客户端帮助类接口
    /// 提供全面的HTTP请求功能，包括重试机制、超时控制、认证、拦截器等高级特性
    /// </summary>
    /// <remarks>
    /// <para>该接口定义了完整的HTTP客户端操作，支持：</para>
    /// <list type="bullet">
    ///   <item><description>所有标准HTTP方法（GET、POST、PUT、DELETE、PATCH、HEAD、OPTIONS）</description></item>
    ///   <item><description>泛型返回类型的自动反序列化</description></item>
    ///   <item><description>灵活的重试策略（支持指数退避）</description></item>
    ///   <item><description>多种认证方式（Bearer Token、Basic Auth、API Key）</description></item>
    ///   <item><description>请求/响应拦截器机制</description></item>
    ///   <item><description>文件上传下载（支持进度回调）</description></item>
    ///   <item><description>批量请求处理（并行/顺序）</description></item>
    ///   <item><description>完整的统计和监控功能</description></item>
    /// </list>
    /// </remarks>
    public interface IHttpClientHelper : IDisposable
    {
        #region 基础HTTP请求方法

        /// <summary>
        /// 发送GET请求并返回HTTP响应消息
        /// </summary>
        /// <param name="url">请求URL，必须是有效的HTTP或HTTPS地址</param>
        /// <param name="headers">可选的请求头字典</param>
        /// <param name="cancellationToken">取消令牌，用于取消请求</param>
        /// <returns>HTTP响应消息对象</returns>
        /// <exception cref="ArgumentNullException">当URL为空时抛出</exception>
        /// <exception cref="ArgumentException">当URL格式无效时抛出</exception>
        /// <exception cref="HttpRequestException">当请求失败时抛出</exception>
        Task<HttpResponseMessage> GetAsync(string url, Dictionary<string, string>? headers = null, CancellationToken cancellationToken = default);

        /// <summary>
        /// 发送GET请求并将响应反序列化为指定类型
        /// </summary>
        /// <typeparam name="T">响应数据的类型</typeparam>
        /// <param name="url">请求URL，必须是有效的HTTP或HTTPS地址</param>
        /// <param name="headers">可选的请求头字典</param>
        /// <param name="cancellationToken">取消令牌</param>
        /// <returns>反序列化后的响应对象</returns>
        /// <exception cref="ArgumentNullException">当URL为空时抛出</exception>
        /// <exception cref="ArgumentException">当URL格式无效时抛出</exception>
        /// <exception cref="HttpRequestException">当请求失败或响应状态码表示错误时抛出</exception>
        /// <exception cref="JsonException">当JSON反序列化失败时抛出</exception>
        Task<T> GetAsync<T>(string url, Dictionary<string, string>? headers = null, CancellationToken cancellationToken = default);

        /// <summary>
        /// 发送GET请求并返回字符串响应内容
        /// </summary>
        /// <param name="url">请求URL</param>
        /// <param name="headers">可选的请求头字典</param>
        /// <param name="cancellationToken">取消令牌</param>
        /// <returns>响应内容的字符串</returns>
        /// <exception cref="HttpRequestException">当响应状态码表示错误时抛出</exception>
        Task<string> GetStringAsync(string url, Dictionary<string, string>? headers = null, CancellationToken cancellationToken = default);

        /// <summary>
        /// 发送GET请求并返回字节数组响应内容
        /// </summary>
        /// <param name="url">请求URL</param>
        /// <param name="headers">可选的请求头字典</param>
        /// <param name="cancellationToken">取消令牌</param>
        /// <returns>响应内容的字节数组</returns>
        Task<byte[]> GetByteArrayAsync(string url, Dictionary<string, string>? headers = null, CancellationToken cancellationToken = default);

        /// <summary>
        /// 发送POST请求
        /// </summary>
        /// <param name="url">请求URL</param>
        /// <param name="content">请求内容</param>
        /// <param name="headers">可选的请求头字典</param>
        /// <param name="cancellationToken">取消令牌</param>
        /// <returns>HTTP响应消息</returns>
        Task<HttpResponseMessage> PostAsync(string url, HttpContent content, Dictionary<string, string>? headers = null, CancellationToken cancellationToken = default);

        /// <summary>
        /// 发送POST请求并将响应反序列化为指定类型
        /// </summary>
        /// <typeparam name="T">响应数据的类型</typeparam>
        /// <param name="url">请求URL</param>
        /// <param name="content">请求内容</param>
        /// <param name="headers">可选的请求头字典</param>
        /// <param name="cancellationToken">取消令牌</param>
        /// <returns>反序列化后的响应对象</returns>
        Task<T> PostAsync<T>(string url, HttpContent content, Dictionary<string, string>? headers = null, CancellationToken cancellationToken = default);

        /// <summary>
        /// 发送JSON格式的POST请求
        /// </summary>
        /// <typeparam name="T">请求数据的类型</typeparam>
        /// <param name="url">请求URL</param>
        /// <param name="data">要发送的数据对象</param>
        /// <param name="headers">可选的请求头字典</param>
        /// <param name="cancellationToken">取消令牌</param>
        /// <returns>HTTP响应消息</returns>
        Task<HttpResponseMessage> PostJsonAsync<T>(string url, T data, Dictionary<string, string>? headers = null, CancellationToken cancellationToken = default);

        /// <summary>
        /// 发送JSON格式的POST请求并将响应反序列化为指定类型
        /// </summary>
        /// <typeparam name="TRequest">请求数据的类型</typeparam>
        /// <typeparam name="TResponse">响应数据的类型</typeparam>
        /// <param name="url">请求URL</param>
        /// <param name="data">要发送的数据对象</param>
        /// <param name="headers">可选的请求头字典</param>
        /// <param name="cancellationToken">取消令牌</param>
        /// <returns>反序列化后的响应对象</returns>
        Task<TResponse> PostJsonAsync<TRequest, TResponse>(string url, TRequest data, Dictionary<string, string>? headers = null, CancellationToken cancellationToken = default);

        /// <summary>
        /// 发送表单格式的POST请求
        /// </summary>
        /// <param name="url">请求URL</param>
        /// <param name="formData">表单数据字典</param>
        /// <param name="headers">可选的请求头字典</param>
        /// <param name="cancellationToken">取消令牌</param>
        /// <returns>HTTP响应消息</returns>
        Task<HttpResponseMessage> PostFormAsync(string url, Dictionary<string, string> formData, Dictionary<string, string>? headers = null, CancellationToken cancellationToken = default);

        /// <summary>
        /// 发送PUT请求
        /// </summary>
        Task<HttpResponseMessage> PutAsync(string url, HttpContent content, Dictionary<string, string>? headers = null, CancellationToken cancellationToken = default);

        /// <summary>
        /// 发送PUT请求并将响应反序列化为指定类型
        /// </summary>
        Task<T> PutAsync<T>(string url, HttpContent content, Dictionary<string, string>? headers = null, CancellationToken cancellationToken = default);

        /// <summary>
        /// 发送JSON格式的PUT请求
        /// </summary>
        Task<HttpResponseMessage> PutJsonAsync<T>(string url, T data, Dictionary<string, string>? headers = null, CancellationToken cancellationToken = default);

        /// <summary>
        /// 发送JSON格式的PUT请求并将响应反序列化为指定类型
        /// </summary>
        Task<TResponse> PutJsonAsync<TRequest, TResponse>(string url, TRequest data, Dictionary<string, string>? headers = null, CancellationToken cancellationToken = default);

        /// <summary>
        /// 发送DELETE请求
        /// </summary>
        Task<HttpResponseMessage> DeleteAsync(string url, Dictionary<string, string>? headers = null, CancellationToken cancellationToken = default);

        /// <summary>
        /// 发送DELETE请求并将响应反序列化为指定类型
        /// </summary>
        Task<T> DeleteAsync<T>(string url, Dictionary<string, string>? headers = null, CancellationToken cancellationToken = default);

        /// <summary>
        /// 发送PATCH请求（部分更新）
        /// </summary>
        /// <param name="url">请求URL</param>
        /// <param name="content">请求内容，通常包含部分更新的数据</param>
        /// <param name="headers">可选的请求头字典</param>
        /// <param name="cancellationToken">取消令牌</param>
        /// <returns>HTTP响应消息</returns>
        Task<HttpResponseMessage> PatchAsync(string url, HttpContent content, Dictionary<string, string>? headers = null, CancellationToken cancellationToken = default);

        /// <summary>
        /// 发送PATCH请求并将响应反序列化为指定类型
        /// </summary>
        Task<T> PatchAsync<T>(string url, HttpContent content, Dictionary<string, string>? headers = null, CancellationToken cancellationToken = default);

        /// <summary>
        /// 发送HEAD请求（只获取响应头，不获取响应体）
        /// </summary>
        /// <param name="url">请求URL</param>
        /// <param name="headers">可选的请求头字典</param>
        /// <param name="cancellationToken">取消令牌</param>
        /// <returns>HTTP响应消息（只包含头部信息）</returns>
        Task<HttpResponseMessage> HeadAsync(string url, Dictionary<string, string>? headers = null, CancellationToken cancellationToken = default);

        /// <summary>
        /// 发送OPTIONS请求（获取服务器支持的HTTP方法）
        /// </summary>
        /// <param name="url">请求URL</param>
        /// <param name="headers">可选的请求头字典</param>
        /// <param name="cancellationToken">取消令牌</param>
        /// <returns>HTTP响应消息，通常在Allow头中包含支持的HTTP方法</returns>
        Task<HttpResponseMessage> OptionsAsync(string url, Dictionary<string, string>? headers = null, CancellationToken cancellationToken = default);

        /// <summary>
        /// 发送自定义HTTP方法的请求
        /// </summary>
        /// <param name="method">HTTP方法（如GET、POST、PUT、DELETE等）</param>
        /// <param name="url">请求URL</param>
        /// <param name="content">可选的请求内容</param>
        /// <param name="headers">可选的请求头字典</param>
        /// <param name="cancellationToken">取消令牌</param>
        /// <returns>HTTP响应消息</returns>
        Task<HttpResponseMessage> SendAsync(HttpMethod method, string url, HttpContent? content = null, Dictionary<string, string>? headers = null, CancellationToken cancellationToken = default);

        /// <summary>
        /// 发送自定义HTTP方法的请求并将响应反序列化为指定类型
        /// </summary>
        Task<T> SendAsync<T>(HttpMethod method, string url, HttpContent? content = null, Dictionary<string, string>? headers = null, CancellationToken cancellationToken = default);

        #endregion

        #region 高级HTTP请求方法

        /// <summary>
        /// 发送带重试机制的GET请求
        /// </summary>
        /// <param name="url">请求URL</param>
        /// <param name="maxRetries">最大重试次数，默认为3次</param>
        /// <param name="retryDelay">重试延迟时间，如果为null则使用指数退避策略</param>
        /// <param name="headers">可选的请求头字典</param>
        /// <param name="cancellationToken">取消令牌</param>
        /// <returns>HTTP响应消息</returns>
        /// <remarks>
        /// <para>重试策略说明：</para>
        /// <list type="bullet">
        ///   <item><description>当响应状态码为5xx或请求超时时会自动重试</description></item>
        ///   <item><description>对于4xx错误（如400、401、403、404）不会重试</description></item>
        ///   <item><description>如果未指定retryDelay，将使用指数退避策略（1秒、2秒、4秒...）</description></item>
        /// </list>
        /// </remarks>
        Task<HttpResponseMessage> GetWithRetryAsync(string url, int maxRetries = 3, TimeSpan? retryDelay = null, Dictionary<string, string>? headers = null, CancellationToken cancellationToken = default);

        /// <summary>
        /// 发送带重试机制的GET请求并将响应反序列化为指定类型
        /// </summary>
        Task<T> GetWithRetryAsync<T>(string url, int maxRetries = 3, TimeSpan? retryDelay = null, Dictionary<string, string>? headers = null, CancellationToken cancellationToken = default);

        /// <summary>
        /// 发送带重试机制的POST请求
        /// </summary>
        Task<HttpResponseMessage> PostWithRetryAsync(string url, HttpContent content, int maxRetries = 3, TimeSpan? retryDelay = null, Dictionary<string, string>? headers = null, CancellationToken cancellationToken = default);

        /// <summary>
        /// 发送带重试机制的POST请求并将响应反序列化为指定类型
        /// </summary>
        Task<T> PostWithRetryAsync<T>(string url, HttpContent content, int maxRetries = 3, TimeSpan? retryDelay = null, Dictionary<string, string>? headers = null, CancellationToken cancellationToken = default);

        /// <summary>
        /// 发送带超时控制的GET请求
        /// </summary>
        /// <param name="url">请求URL</param>
        /// <param name="timeout">请求超时时间</param>
        /// <param name="headers">可选的请求头字典</param>
        /// <param name="cancellationToken">取消令牌</param>
        /// <returns>HTTP响应消息</returns>
        /// <exception cref="TaskCanceledException">当请求超时时抛出</exception>
        Task<HttpResponseMessage> GetWithTimeoutAsync(string url, TimeSpan timeout, Dictionary<string, string>? headers = null, CancellationToken cancellationToken = default);

        /// <summary>
        /// 发送带超时控制的GET请求并将响应反序列化为指定类型
        /// </summary>
        Task<T> GetWithTimeoutAsync<T>(string url, TimeSpan timeout, Dictionary<string, string>? headers = null, CancellationToken cancellationToken = default);

        /// <summary>
        /// 发送带超时控制的POST请求
        /// </summary>
        Task<HttpResponseMessage> PostWithTimeoutAsync(string url, HttpContent content, TimeSpan timeout, Dictionary<string, string>? headers = null, CancellationToken cancellationToken = default);

        /// <summary>
        /// 发送带超时控制的POST请求并将响应反序列化为指定类型
        /// </summary>
        Task<T> PostWithTimeoutAsync<T>(string url, HttpContent content, TimeSpan timeout, Dictionary<string, string>? headers = null, CancellationToken cancellationToken = default);

        /// <summary>
        /// 发送带Bearer Token认证的GET请求
        /// </summary>
        /// <param name="url">请求URL</param>
        /// <param name="authToken">认证令牌</param>
        /// <param name="authScheme">认证方案，默认为"Bearer"</param>
        /// <param name="headers">可选的请求头字典</param>
        /// <param name="cancellationToken">取消令牌</param>
        /// <returns>HTTP响应消息</returns>
        Task<HttpResponseMessage> GetWithAuthAsync(string url, string authToken, string authScheme = "Bearer", Dictionary<string, string>? headers = null, CancellationToken cancellationToken = default);

        /// <summary>
        /// 发送带Bearer Token认证的GET请求并将响应反序列化为指定类型
        /// </summary>
        Task<T> GetWithAuthAsync<T>(string url, string authToken, string authScheme = "Bearer", Dictionary<string, string>? headers = null, CancellationToken cancellationToken = default);

        /// <summary>
        /// 发送带基本认证（Basic Auth）的GET请求
        /// </summary>
        /// <param name="url">请求URL</param>
        /// <param name="username">用户名</param>
        /// <param name="password">密码</param>
        /// <param name="headers">可选的请求头字典</param>
        /// <param name="cancellationToken">取消令牌</param>
        /// <returns>HTTP响应消息</returns>
        Task<HttpResponseMessage> GetWithBasicAuthAsync(string url, string username, string password, Dictionary<string, string>? headers = null, CancellationToken cancellationToken = default);

        /// <summary>
        /// 发送带基本认证的GET请求并将响应反序列化为指定类型
        /// </summary>
        Task<T> GetWithBasicAuthAsync<T>(string url, string username, string password, Dictionary<string, string>? headers = null, CancellationToken cancellationToken = default);

        /// <summary>
        /// 发送带API Key认证的GET请求
        /// </summary>
        /// <param name="url">请求URL</param>
        /// <param name="apiKey">API密钥</param>
        /// <param name="headerName">API Key的请求头名称，默认为"X-API-Key"</param>
        /// <param name="headers">可选的请求头字典</param>
        /// <param name="cancellationToken">取消令牌</param>
        /// <returns>HTTP响应消息</returns>
        Task<HttpResponseMessage> GetWithApiKeyAsync(string url, string apiKey, string headerName = "X-API-Key", Dictionary<string, string>? headers = null, CancellationToken cancellationToken = default);

        /// <summary>
        /// 发送带API Key认证的GET请求并将响应反序列化为指定类型
        /// </summary>
        Task<T> GetWithApiKeyAsync<T>(string url, string apiKey, string headerName = "X-API-Key", Dictionary<string, string>? headers = null, CancellationToken cancellationToken = default);

        /// <summary>
        /// 下载文件（支持进度回调）
        /// </summary>
        /// <param name="url">文件URL</param>
        /// <param name="filePath">本地保存路径</param>
        /// <param name="headers">可选的请求头字典</param>
        /// <param name="progressCallback">进度回调函数，接收<see cref="DownloadProgress"/>参数</param>
        /// <param name="cancellationToken">取消令牌</param>
        /// <returns>下载结果对象</returns>
        /// <remarks>
        /// <para>如果目标目录不存在，会自动创建</para>
        /// <para>支持大文件下载，使用流式处理避免内存溢出</para>
        /// </remarks>
        Task<DownloadResult> DownloadFileAsync(string url, string filePath, Dictionary<string, string>? headers = null, Action<DownloadProgress>? progressCallback = null, CancellationToken cancellationToken = default);

        /// <summary>
        /// 上传文件（支持进度回调）
        /// </summary>
        /// <param name="url">上传URL</param>
        /// <param name="filePath">本地文件路径</param>
        /// <param name="fieldName">表单字段名，默认为"file"</param>
        /// <param name="additionalData">附加的表单数据</param>
        /// <param name="headers">可选的请求头字典</param>
        /// <param name="progressCallback">进度回调函数，接收<see cref="UploadProgress"/>参数</param>
        /// <param name="cancellationToken">取消令牌</param>
        /// <returns>上传结果对象</returns>
        Task<UploadResult> UploadFileAsync(string url, string filePath, string fieldName = "file", Dictionary<string, string>? additionalData = null, Dictionary<string, string>? headers = null, Action<UploadProgress>? progressCallback = null, CancellationToken cancellationToken = default);

        #endregion

        #region 配置和工具方法

        /// <summary>
        /// 批量设置默认请求头
        /// </summary>
        /// <param name="headers">请求头字典</param>
        void SetDefaultHeaders(Dictionary<string, string> headers);

        /// <summary>
        /// 添加单个默认请求头
        /// </summary>
        /// <param name="key">请求头名称</param>
        /// <param name="value">请求头值</param>
        void AddDefaultHeader(string key, string value);

        /// <summary>
        /// 移除指定的默认请求头
        /// </summary>
        /// <param name="key">请求头名称</param>
        void RemoveDefaultHeader(string key);

        /// <summary>
        /// 设置默认超时时间
        /// </summary>
        /// <param name="timeout">超时时间，必须大于零或设置为 Timeout.InfiniteTimeSpan</param>
        /// <remarks>
        /// <para>
        /// <b>建议：</b>保持默认的无限超时，使用 CancellationToken 控制具体请求的超时。
        /// 这种设计提供了更灵活的超时管理，允许每个请求有独立的超时时间。
        /// </para>
        /// <para>
        /// 如果需要设置全局超时作为兜底保护，可以设置一个较大的值（如 5 分钟）。
        /// </para>
        /// </remarks>
        void SetDefaultTimeout(TimeSpan timeout);

        /// <summary>
        /// 启用或禁用自动解压缩（GZip/Deflate）
        /// </summary>
        /// <param name="enable">true启用，false禁用</param>
        void SetAutomaticDecompression(bool enable);

        /// <summary>
        /// 设置Cookie容器
        /// </summary>
        /// <param name="cookieContainer">Cookie容器实例</param>
        void SetCookieContainer(CookieContainer cookieContainer);

        /// <summary>
        /// 获取当前的Cookie容器
        /// </summary>
        /// <returns>Cookie容器实例，如果未设置则返回null</returns>
        CookieContainer? GetCookieContainer();

        /// <summary>
        /// 清除所有默认请求头
        /// </summary>
        void ClearDefaultHeaders();

        /// <summary>
        /// 序列化对象为JSON字符串
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="obj">要序列化的对象</param>
        /// <returns>JSON字符串</returns>
        string SerializeToJson<T>(T obj);

        /// <summary>
        /// 反序列化JSON字符串为对象
        /// </summary>
        /// <typeparam name="T">目标类型</typeparam>
        /// <param name="json">JSON字符串</param>
        /// <returns>反序列化后的对象</returns>
        T DeserializeFromJson<T>(string json);

        /// <summary>
        /// 获取内部HttpClient实例
        /// </summary>
        /// <returns>HttpClient实例</returns>
        /// <remarks>直接操作HttpClient可能绕过拦截器和统计功能，请谨慎使用</remarks>
        HttpClient GetHttpClient();

        /// <summary>
        /// 设置代理服务器
        /// </summary>
        /// <param name="proxy">代理配置</param>
        void SetProxy(IWebProxy? proxy);

        /// <summary>
        /// 设置每服务器最大连接数
        /// </summary>
        /// <param name="maxConnections">最大连接数，必须大于零</param>
        void SetMaxConnectionsPerServer(int maxConnections);

        /// <summary>
        /// 配置JSON序列化选项
        /// </summary>
        /// <param name="configure">配置委托</param>
        void ConfigureJsonOptions(Action<JsonSerializerOptions> configure);

        /// <summary>
        /// 添加请求拦截器
        /// </summary>
        /// <param name="interceptor">拦截器委托，接收HttpRequestMessage参数</param>
        /// <remarks>
        /// <para>拦截器在请求发送前执行，可用于：</para>
        /// <list type="bullet">
        ///   <item><description>添加自定义请求头</description></item>
        ///   <item><description>修改请求内容</description></item>
        ///   <item><description>记录请求日志</description></item>
        ///   <item><description>实现自定义认证逻辑</description></item>
        /// </list>
        /// </remarks>
        void AddRequestInterceptor(Func<HttpRequestMessage, Task> interceptor);

        /// <summary>
        /// 添加响应拦截器
        /// </summary>
        /// <param name="interceptor">拦截器委托，接收HttpResponseMessage参数</param>
        /// <remarks>
        /// <para>拦截器在收到响应后执行，可用于：</para>
        /// <list type="bullet">
        ///   <item><description>记录响应日志</description></item>
        ///   <item><description>处理错误响应</description></item>
        ///   <item><description>提取响应头信息</description></item>
        /// </list>
        /// </remarks>
        void AddResponseInterceptor(Func<HttpResponseMessage, Task> interceptor);

        /// <summary>
        /// 清除所有请求和响应拦截器
        /// </summary>
        void ClearInterceptors();

        #endregion

        #region 批量请求处理

        /// <summary>
        /// 并行发送多个HTTP请求
        /// </summary>
        /// <param name="requests">请求列表</param>
        /// <param name="maxConcurrency">最大并发数，默认为5</param>
        /// <param name="cancellationToken">取消令牌</param>
        /// <returns>响应列表，顺序与请求列表对应</returns>
        /// <remarks>
        /// <para>并行执行可以提高批量请求的效率</para>
        /// <para>通过maxConcurrency参数控制并发数，避免对目标服务器造成过大压力</para>
        /// </remarks>
        Task<List<BatchResponse>> SendBatchAsync(List<BatchRequest> requests, int maxConcurrency = 5, CancellationToken cancellationToken = default);

        /// <summary>
        /// 顺序发送多个HTTP请求
        /// </summary>
        /// <param name="requests">请求列表</param>
        /// <param name="cancellationToken">取消令牌</param>
        /// <returns>响应列表，顺序与请求列表对应</returns>
        /// <remarks>
        /// <para>顺序执行适用于需要按特定顺序处理请求的场景</para>
        /// <para>如果某个请求失败，会继续执行后续请求</para>
        /// </remarks>
        Task<List<BatchResponse>> SendSequentialAsync(List<BatchRequest> requests, CancellationToken cancellationToken = default);

        #endregion

        #region 监控和统计

        /// <summary>
        /// 获取HTTP客户端统计信息
        /// </summary>
        /// <returns>统计信息对象</returns>
        HttpClientStatistics GetStatistics();

        /// <summary>
        /// 启用或禁用日志记录
        /// </summary>
        /// <param name="enable">true启用，false禁用</param>
        void EnableLogging(bool enable = true);

        /// <summary>
        /// 重置统计数据
        /// </summary>
        void ResetStatistics();

        #endregion
    }

    /// <summary>
    /// HTTP客户端帮助类实现
    /// 提供全面的HTTP请求功能，包括重试机制、超时控制、认证、拦截器等高级特性
    /// </summary>
    /// <remarks>
    /// <example>
    /// 基本使用示例：
    /// <code>
    /// // 创建实例
    /// using var httpClient = new HttpClientHelper();
    /// 
    /// // GET请求
    /// var user = await httpClient.GetAsync&lt;User&gt;("https://api.example.com/users/1");
    /// 
    /// // POST请求
    /// var result = await httpClient.PostJsonAsync&lt;CreateUserRequest, CreateUserResponse&gt;(
    ///     "https://api.example.com/users", 
    ///     new CreateUserRequest { Name = "John" });
    /// 
    /// // 带重试的请求
    /// var data = await httpClient.GetWithRetryAsync&lt;Data&gt;(
    ///     "https://api.example.com/data", 
    ///     maxRetries: 3);
    /// </code>
    /// </example>
    /// </remarks>
    public class HttpClientHelper : IHttpClientHelper
    {
        #region 私有字段

        /// <summary>
        /// 内部使用的HttpClient实例，用于发送HTTP请求
        /// </summary>
        private readonly HttpClient _httpClient;

        /// <summary>
        /// HTTP消息处理器，用于配置代理、Cookie、SSL等
        /// </summary>
        private readonly HttpClientHandler? _httpClientHandler;

        /// <summary>
        /// 日志记录器，用于记录请求和响应信息
        /// </summary>
        private readonly ILogger<HttpClientHelper> _logger;

        /// <summary>
        /// 标识实例是否已被释放
        /// </summary>
        private bool _disposed;

        /// <summary>
        /// 标识是否启用日志记录
        /// </summary>
        private bool _loggingEnabled = true;

        /// <summary>
        /// 用于统计数据的线程同步锁
        /// </summary>
        private readonly object _statsLock = new();

        /// <summary>
        /// 总请求数统计
        /// </summary>
        private long _totalRequests;

        /// <summary>
        /// 成功请求数统计
        /// </summary>
        private long _successfulRequests;

        /// <summary>
        /// 失败请求数统计
        /// </summary>
        private long _failedRequests;

        /// <summary>
        /// 总响应时间（毫秒）
        /// </summary>
        private long _totalResponseTimeMs;

        /// <summary>
        /// JSON序列化选项配置
        /// </summary>
        private JsonSerializerOptions _jsonOptions;

        /// <summary>
        /// 请求拦截器列表，在发送请求前执行
        /// </summary>
        private readonly List<Func<HttpRequestMessage, Task>> _requestInterceptors = new();

        /// <summary>
        /// 响应拦截器列表，在收到响应后执行
        /// </summary>
        private readonly List<Func<HttpResponseMessage, Task>> _responseInterceptors = new();

        /// <summary>
        /// PATCH HTTP方法的静态实例
        /// </summary>
        private static readonly HttpMethod PatchMethod = new("PATCH");

        #endregion

        #region 构造函数

        /// <summary>
        /// 初始化HttpClientHelper实例
        /// </summary>
        /// <param name="logger">可选的日志记录器</param>
        /// <remarks>
        /// <para>默认配置：</para>
        /// <list type="bullet">
        ///   <item><description>超时时间：无限（由CancellationToken控制具体超时）</description></item>
        ///   <item><description>自动解压缩：GZip和Deflate</description></item>
        ///   <item><description>每服务器最大连接数：100</description></item>
        ///   <item><description>默认User-Agent：Chet.HttpClientHelper/2.0</description></item>
        /// </list>
        /// <para>
        /// <b>超时设计说明：</b>HttpClient.Timeout 设置为无限，所有超时控制由调用方通过 CancellationToken 控制。
        /// 这种设计提供了更灵活的超时管理，允许每个请求有独立的超时时间。
        /// </para>
        /// </remarks>
        public HttpClientHelper(ILogger<HttpClientHelper>? logger = null)
        {
            _logger = logger ?? NullLogger<HttpClientHelper>.Instance;
            _jsonOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                PropertyNameCaseInsensitive = true,
                WriteIndented = false
            };

            _httpClientHandler = new HttpClientHandler
            {
                AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate,
                UseCookies = true,
                MaxConnectionsPerServer = 100
            };

            _httpClient = new HttpClient(_httpClientHandler)
            {
                Timeout = Timeout.InfiniteTimeSpan
            };

            _httpClient.DefaultRequestHeaders.Add("User-Agent", "Chet.HttpClientHelper/2.0");
            _httpClient.DefaultRequestHeaders.Add("Accept", "application/json");
        }

        /// <summary>
        /// 使用自定义HttpClient实例初始化HttpClientHelper
        /// </summary>
        /// <param name="httpClient">自定义的HttpClient实例，不能为null</param>
        /// <param name="logger">可选的日志记录器</param>
        /// <exception cref="ArgumentNullException">当httpClient为null时抛出</exception>
        /// <remarks>
        /// <para>使用此构造函数时：</para>
        /// <list type="bullet">
        ///   <item><description>不会创建新的HttpClientHandler，因此代理、Cookie、SSL等配置需要在外部设置</description></item>
        ///   <item><description>SetProxy、SetCookieContainer、SetAutomaticDecompression等方法将无效</description></item>
        ///   <item><description>适用于需要精细控制HttpClient配置的场景</description></item>
        /// </list>
        /// </remarks>
        public HttpClientHelper(HttpClient httpClient, ILogger<HttpClientHelper> logger = null)
        {
            _logger = logger ?? NullLogger<HttpClientHelper>.Instance;
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            _httpClientHandler = null;
            _jsonOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                PropertyNameCaseInsensitive = true
            };
        }

        /// <summary>
        /// 使用自定义配置选项初始化HttpClientHelper
        /// </summary>
        /// <param name="options">HTTP客户端配置选项，为null时使用默认配置</param>
        /// <param name="logger">可选的日志记录器</param>
        /// <remarks>
        /// <para>配置选项说明：</para>
        /// <list type="bullet">
        ///   <item><description>Timeout：请求超时时间，默认无限（建议使用CancellationToken控制超时）</description></item>
        ///   <item><description>DefaultHeaders：默认请求头字典</description></item>
        ///   <item><description>EnableAutomaticDecompression：是否启用自动解压缩，默认true</description></item>
        ///   <item><description>EnableCookies：是否启用Cookie，默认true</description></item>
        ///   <item><description>MaxConnectionsPerServer：每服务器最大连接数，默认100</description></item>
        ///   <item><description>Proxy：代理服务器配置</description></item>
        ///   <item><description>UseDefaultCredentials：是否使用默认凭证</description></item>
        ///   <item><description>ServerCertificateValidationCallback：SSL证书验证回调</description></item>
        ///   <item><description>JsonOptions：JSON序列化选项</description></item>
        /// </list>
        /// <para>
        /// <b>超时设计说明：</b>建议将 Timeout 设置为无限或较大值，使用 CancellationToken 控制具体请求的超时。
        /// 这种设计提供了更灵活的超时管理，允许每个请求有独立的超时时间。
        /// </para>
        /// </remarks>
        public HttpClientHelper(HttpClientOptions options, ILogger<HttpClientHelper> logger = null)
        {
            _logger = logger ?? NullLogger<HttpClientHelper>.Instance;
            _jsonOptions = options?.JsonOptions ?? new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                PropertyNameCaseInsensitive = true
            };

            _httpClientHandler = new HttpClientHandler
            {
                AutomaticDecompression = options?.EnableAutomaticDecompression != false
                    ? DecompressionMethods.GZip | DecompressionMethods.Deflate
                    : DecompressionMethods.None,
                UseCookies = options?.EnableCookies ?? true,
                MaxConnectionsPerServer = options?.MaxConnectionsPerServer ?? 100,
                Proxy = options?.Proxy,
                UseDefaultCredentials = options?.UseDefaultCredentials ?? false,
                ServerCertificateCustomValidationCallback = options?.ServerCertificateValidationCallback
            };

            _httpClient = new HttpClient(_httpClientHandler)
            {
                Timeout = options?.Timeout ?? Timeout.InfiniteTimeSpan
            };

            if (options?.DefaultHeaders != null)
            {
                foreach (var header in options.DefaultHeaders)
                {
                    _httpClient.DefaultRequestHeaders.Add(header.Key, header.Value);
                }
            }
            else
            {
                _httpClient.DefaultRequestHeaders.Add("User-Agent", "Chet.HttpClientHelper/2.0");
                _httpClient.DefaultRequestHeaders.Add("Accept", "application/json");
            }
        }

        #endregion

        #region 基础HTTP请求方法

        /// <inheritdoc/>
        public async Task<HttpResponseMessage> GetAsync(string url, Dictionary<string, string> headers = null, CancellationToken cancellationToken = default)
        {
            ValidateUrl(url);
            using var request = new HttpRequestMessage(HttpMethod.Get, url);
            AddHeaders(request, headers);
            return await SendRequestAsync(request, cancellationToken);
        }

        /// <inheritdoc/>
        public async Task<T> GetAsync<T>(string url, Dictionary<string, string> headers = null, CancellationToken cancellationToken = default)
        {
            var response = await GetAsync(url, headers, cancellationToken);
            return await HandleResponseAsync<T>(response);
        }

        /// <inheritdoc/>
        public async Task<string> GetStringAsync(string url, Dictionary<string, string> headers = null, CancellationToken cancellationToken = default)
        {
            var response = await GetAsync(url, headers, cancellationToken);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync(cancellationToken);
        }

        /// <inheritdoc/>
        public async Task<byte[]> GetByteArrayAsync(string url, Dictionary<string, string> headers = null, CancellationToken cancellationToken = default)
        {
            var response = await GetAsync(url, headers, cancellationToken);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsByteArrayAsync(cancellationToken);
        }

        /// <inheritdoc/>
        public async Task<HttpResponseMessage> PostAsync(string url, HttpContent content, Dictionary<string, string> headers = null, CancellationToken cancellationToken = default)
        {
            ValidateUrl(url);
            using var request = new HttpRequestMessage(HttpMethod.Post, url) { Content = content };
            AddHeaders(request, headers);
            return await SendRequestAsync(request, cancellationToken);
        }

        /// <inheritdoc/>
        public async Task<T> PostAsync<T>(string url, HttpContent content, Dictionary<string, string> headers = null, CancellationToken cancellationToken = default)
        {
            var response = await PostAsync(url, content, headers, cancellationToken);
            return await HandleResponseAsync<T>(response);
        }

        /// <inheritdoc/>
        public async Task<HttpResponseMessage> PostJsonAsync<T>(string url, T data, Dictionary<string, string> headers = null, CancellationToken cancellationToken = default)
        {
            var json = JsonSerializer.Serialize(data, _jsonOptions);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            return await PostAsync(url, content, headers, cancellationToken);
        }

        /// <inheritdoc/>
        public async Task<TResult> PostJsonAsync<T, TResult>(string url, T data, Dictionary<string, string> headers = null, CancellationToken cancellationToken = default)
        {
            var response = await PostJsonAsync<T>(url, data, headers, cancellationToken);
            return await HandleResponseAsync<TResult>(response);
        }

        /// <inheritdoc/>
        public async Task<HttpResponseMessage> PostFormAsync(string url, Dictionary<string, string> formData, Dictionary<string, string> headers = null, CancellationToken cancellationToken = default)
        {
            if (formData == null) throw new ArgumentNullException(nameof(formData));
            var content = new FormUrlEncodedContent(formData);
            return await PostAsync(url, content, headers, cancellationToken);
        }

        /// <inheritdoc/>
        public async Task<HttpResponseMessage> PutAsync(string url, HttpContent content, Dictionary<string, string> headers = null, CancellationToken cancellationToken = default)
        {
            ValidateUrl(url);
            using var request = new HttpRequestMessage(HttpMethod.Put, url) { Content = content };
            AddHeaders(request, headers);
            return await SendRequestAsync(request, cancellationToken);
        }

        /// <inheritdoc/>
        public async Task<T> PutAsync<T>(string url, HttpContent content, Dictionary<string, string> headers = null, CancellationToken cancellationToken = default)
        {
            var response = await PutAsync(url, content, headers, cancellationToken);
            return await HandleResponseAsync<T>(response);
        }

        /// <inheritdoc/>
        public async Task<HttpResponseMessage> PutJsonAsync<T>(string url, T data, Dictionary<string, string> headers = null, CancellationToken cancellationToken = default)
        {
            var json = JsonSerializer.Serialize(data, _jsonOptions);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            return await PutAsync(url, content, headers, cancellationToken);
        }

        /// <inheritdoc/>
        public async Task<TResult> PutJsonAsync<T, TResult>(string url, T data, Dictionary<string, string> headers = null, CancellationToken cancellationToken = default)
        {
            var response = await PutJsonAsync<T>(url, data, headers, cancellationToken);
            return await HandleResponseAsync<TResult>(response);
        }

        /// <inheritdoc/>
        public async Task<HttpResponseMessage> DeleteAsync(string url, Dictionary<string, string> headers = null, CancellationToken cancellationToken = default)
        {
            ValidateUrl(url);
            using var request = new HttpRequestMessage(HttpMethod.Delete, url);
            AddHeaders(request, headers);
            return await SendRequestAsync(request, cancellationToken);
        }

        /// <inheritdoc/>
        public async Task<T> DeleteAsync<T>(string url, Dictionary<string, string> headers = null, CancellationToken cancellationToken = default)
        {
            var response = await DeleteAsync(url, headers, cancellationToken);
            return await HandleResponseAsync<T>(response);
        }

        /// <inheritdoc/>
        public async Task<HttpResponseMessage> PatchAsync(string url, HttpContent content, Dictionary<string, string> headers = null, CancellationToken cancellationToken = default)
        {
            ValidateUrl(url);
            using var request = new HttpRequestMessage(PatchMethod, url) { Content = content };
            AddHeaders(request, headers);
            return await SendRequestAsync(request, cancellationToken);
        }

        /// <inheritdoc/>
        public async Task<T> PatchAsync<T>(string url, HttpContent content, Dictionary<string, string> headers = null, CancellationToken cancellationToken = default)
        {
            var response = await PatchAsync(url, content, headers, cancellationToken);
            return await HandleResponseAsync<T>(response);
        }

        /// <inheritdoc/>
        public async Task<HttpResponseMessage> HeadAsync(string url, Dictionary<string, string> headers = null, CancellationToken cancellationToken = default)
        {
            ValidateUrl(url);
            using var request = new HttpRequestMessage(HttpMethod.Head, url);
            AddHeaders(request, headers);
            return await SendRequestAsync(request, cancellationToken);
        }

        /// <inheritdoc/>
        public async Task<HttpResponseMessage> OptionsAsync(string url, Dictionary<string, string> headers = null, CancellationToken cancellationToken = default)
        {
            ValidateUrl(url);
            using var request = new HttpRequestMessage(HttpMethod.Options, url);
            AddHeaders(request, headers);
            return await SendRequestAsync(request, cancellationToken);
        }

        /// <inheritdoc/>
        public async Task<HttpResponseMessage> SendAsync(HttpMethod method, string url, HttpContent content = null, Dictionary<string, string> headers = null, CancellationToken cancellationToken = default)
        {
            ValidateUrl(url);
            using var request = new HttpRequestMessage(method, url) { Content = content };
            AddHeaders(request, headers);
            return await SendRequestAsync(request, cancellationToken);
        }

        /// <inheritdoc/>
        public async Task<T> SendAsync<T>(HttpMethod method, string url, HttpContent content = null, Dictionary<string, string> headers = null, CancellationToken cancellationToken = default)
        {
            var response = await SendAsync(method, url, content, headers, cancellationToken);
            return await HandleResponseAsync<T>(response);
        }

        #endregion

        #region 高级HTTP请求方法

        /// <inheritdoc/>
        public async Task<HttpResponseMessage> GetWithRetryAsync(string url, int maxRetries = 3, TimeSpan? retryDelay = null, Dictionary<string, string> headers = null, CancellationToken cancellationToken = default)
        {
            return await SendWithRetryAsync(
                () => GetAsync(url, headers, cancellationToken),
                maxRetries,
                retryDelay,
                cancellationToken);
        }

        /// <inheritdoc/>
        public async Task<T> GetWithRetryAsync<T>(string url, int maxRetries = 3, TimeSpan? retryDelay = null, Dictionary<string, string> headers = null, CancellationToken cancellationToken = default)
        {
            var response = await GetWithRetryAsync(url, maxRetries, retryDelay, headers, cancellationToken);
            return await HandleResponseAsync<T>(response);
        }

        /// <inheritdoc/>
        public async Task<HttpResponseMessage> PostWithRetryAsync(string url, HttpContent content, int maxRetries = 3, TimeSpan? retryDelay = null, Dictionary<string, string> headers = null, CancellationToken cancellationToken = default)
        {
            return await SendWithRetryAsync(
                () => PostAsync(url, content, headers, cancellationToken),
                maxRetries,
                retryDelay,
                cancellationToken);
        }

        /// <inheritdoc/>
        public async Task<T> PostWithRetryAsync<T>(string url, HttpContent content, int maxRetries = 3, TimeSpan? retryDelay = null, Dictionary<string, string> headers = null, CancellationToken cancellationToken = default)
        {
            var response = await PostWithRetryAsync(url, content, maxRetries, retryDelay, headers, cancellationToken);
            return await HandleResponseAsync<T>(response);
        }

        /// <inheritdoc/>
        public async Task<HttpResponseMessage> GetWithTimeoutAsync(string url, TimeSpan timeout, Dictionary<string, string> headers = null, CancellationToken cancellationToken = default)
        {
            using var cts = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);
            cts.CancelAfter(timeout);
            return await GetAsync(url, headers, cts.Token);
        }

        /// <inheritdoc/>
        public async Task<T> GetWithTimeoutAsync<T>(string url, TimeSpan timeout, Dictionary<string, string> headers = null, CancellationToken cancellationToken = default)
        {
            var response = await GetWithTimeoutAsync(url, timeout, headers, cancellationToken);
            return await HandleResponseAsync<T>(response);
        }

        /// <inheritdoc/>
        public async Task<HttpResponseMessage> PostWithTimeoutAsync(string url, HttpContent content, TimeSpan timeout, Dictionary<string, string> headers = null, CancellationToken cancellationToken = default)
        {
            using var cts = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);
            cts.CancelAfter(timeout);
            return await PostAsync(url, content, headers, cts.Token);
        }

        /// <inheritdoc/>
        public async Task<T> PostWithTimeoutAsync<T>(string url, HttpContent content, TimeSpan timeout, Dictionary<string, string> headers = null, CancellationToken cancellationToken = default)
        {
            var response = await PostWithTimeoutAsync(url, content, timeout, headers, cancellationToken);
            return await HandleResponseAsync<T>(response);
        }

        /// <inheritdoc/>
        public async Task<HttpResponseMessage> GetWithAuthAsync(string url, string authToken, string authScheme = "Bearer", Dictionary<string, string> headers = null, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrEmpty(authToken)) throw new ArgumentNullException(nameof(authToken));
            var authHeaders = headers != null ? new Dictionary<string, string>(headers) : new Dictionary<string, string>();
            authHeaders["Authorization"] = $"{authScheme} {authToken}";
            return await GetAsync(url, authHeaders, cancellationToken);
        }

        /// <inheritdoc/>
        public async Task<T> GetWithAuthAsync<T>(string url, string authToken, string authScheme = "Bearer", Dictionary<string, string> headers = null, CancellationToken cancellationToken = default)
        {
            var response = await GetWithAuthAsync(url, authToken, authScheme, headers, cancellationToken);
            return await HandleResponseAsync<T>(response);
        }

        /// <inheritdoc/>
        public async Task<HttpResponseMessage> GetWithBasicAuthAsync(string url, string username, string password, Dictionary<string, string> headers = null, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrEmpty(username)) throw new ArgumentNullException(nameof(username));
            var credentials = Convert.ToBase64String(Encoding.UTF8.GetBytes($"{username}:{password}"));
            var authHeaders = headers != null ? new Dictionary<string, string>(headers) : new Dictionary<string, string>();
            authHeaders["Authorization"] = $"Basic {credentials}";
            return await GetAsync(url, authHeaders, cancellationToken);
        }

        /// <inheritdoc/>
        public async Task<T> GetWithBasicAuthAsync<T>(string url, string username, string password, Dictionary<string, string> headers = null, CancellationToken cancellationToken = default)
        {
            var response = await GetWithBasicAuthAsync(url, username, password, headers, cancellationToken);
            return await HandleResponseAsync<T>(response);
        }

        /// <inheritdoc/>
        public async Task<HttpResponseMessage> GetWithApiKeyAsync(string url, string apiKey, string headerName = "X-API-Key", Dictionary<string, string> headers = null, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrEmpty(apiKey)) throw new ArgumentNullException(nameof(apiKey));
            var authHeaders = headers != null ? new Dictionary<string, string>(headers) : new Dictionary<string, string>();
            authHeaders[headerName] = apiKey;
            return await GetAsync(url, authHeaders, cancellationToken);
        }

        /// <inheritdoc/>
        public async Task<T> GetWithApiKeyAsync<T>(string url, string apiKey, string headerName = "X-API-Key", Dictionary<string, string> headers = null, CancellationToken cancellationToken = default)
        {
            var response = await GetWithApiKeyAsync(url, apiKey, headerName, headers, cancellationToken);
            return await HandleResponseAsync<T>(response);
        }

        /// <inheritdoc/>
        public async Task<DownloadResult> DownloadFileAsync(string url, string filePath, Dictionary<string, string> headers = null, Action<DownloadProgress> progressCallback = null, CancellationToken cancellationToken = default)
        {
            ValidateUrl(url);
            if (string.IsNullOrEmpty(filePath)) throw new ArgumentNullException(nameof(filePath));

            var directory = Path.GetDirectoryName(filePath);
            if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            var stopwatch = Stopwatch.StartNew();

            try
            {
                var response = await GetAsync(url, headers, cancellationToken);
                response.EnsureSuccessStatusCode();

                var totalBytes = response.Content.Headers.ContentLength ?? -1;
                var downloadedBytes = 0L;

                using var contentStream = await response.Content.ReadAsStreamAsync(cancellationToken);
                using var fileStream = new FileStream(filePath, FileMode.Create, FileAccess.Write, FileShare.None, 8192, true);

                var buffer = new byte[8192];
                int bytesRead;

                while ((bytesRead = await contentStream.ReadAsync(buffer, 0, buffer.Length, cancellationToken)) > 0)
                {
                    await fileStream.WriteAsync(buffer, 0, bytesRead, cancellationToken);
                    downloadedBytes += bytesRead;

                    if (progressCallback != null && totalBytes > 0)
                    {
                        var progress = new DownloadProgress
                        {
                            TotalBytes = totalBytes,
                            DownloadedBytes = downloadedBytes,
                            ProgressPercentage = (int)((double)downloadedBytes / totalBytes * 100)
                        };
                        progressCallback(progress);
                    }
                }

                stopwatch.Stop();
                UpdateStatistics(true, stopwatch.ElapsedMilliseconds);

                if (_loggingEnabled)
                {
                    _logger.LogInformation("File downloaded successfully to {FilePath} ({FileSize} bytes, {Duration}ms)",
                        filePath, downloadedBytes, stopwatch.ElapsedMilliseconds);
                }

                return new DownloadResult
                {
                    Success = true,
                    FilePath = filePath,
                    FileSize = downloadedBytes,
                    Duration = stopwatch.Elapsed
                };
            }
            catch (Exception ex)
            {
                stopwatch.Stop();
                UpdateStatistics(false, stopwatch.ElapsedMilliseconds);

                if (_loggingEnabled)
                {
                    _logger.LogError(ex, "Error downloading file from {Url}", url);
                }

                return new DownloadResult
                {
                    Success = false,
                    ErrorMessage = ex.Message,
                    Duration = stopwatch.Elapsed
                };
            }
        }

        /// <inheritdoc/>
        public async Task<UploadResult> UploadFileAsync(string url, string filePath, string fieldName = "file", Dictionary<string, string> additionalData = null, Dictionary<string, string> headers = null, Action<UploadProgress> progressCallback = null, CancellationToken cancellationToken = default)
        {
            ValidateUrl(url);
            if (string.IsNullOrEmpty(filePath)) throw new ArgumentNullException(nameof(filePath));
            if (!File.Exists(filePath)) throw new FileNotFoundException("File not found", filePath);

            var stopwatch = Stopwatch.StartNew();
            var fileInfo = new FileInfo(filePath);
            var totalBytes = fileInfo.Length;

            try
            {
                using var content = new MultipartFormDataContent();

                if (additionalData != null)
                {
                    foreach (var item in additionalData)
                    {
                        content.Add(new StringContent(item.Value), item.Key);
                    }
                }

                var progressStream = new ProgressStream(filePath, progressCallback, totalBytes);
                var fileContent = new StreamContent(progressStream);
                var fileName = Path.GetFileName(filePath);
                content.Add(fileContent, fieldName, fileName);

                using var request = new HttpRequestMessage(HttpMethod.Post, url) { Content = content };
                AddHeaders(request, headers);

                var response = await _httpClient.SendAsync(request, cancellationToken);
                response.EnsureSuccessStatusCode();

                var responseContent = await response.Content.ReadAsStringAsync(cancellationToken);

                stopwatch.Stop();
                UpdateStatistics(true, stopwatch.ElapsedMilliseconds);

                if (_loggingEnabled)
                {
                    _logger.LogInformation("File uploaded successfully to {Url} ({FileSize} bytes, {Duration}ms)",
                        url, totalBytes, stopwatch.ElapsedMilliseconds);
                }

                return new UploadResult
                {
                    Success = true,
                    ResponseContent = responseContent,
                    FileSize = totalBytes,
                    Duration = stopwatch.Elapsed
                };
            }
            catch (Exception ex)
            {
                stopwatch.Stop();
                UpdateStatistics(false, stopwatch.ElapsedMilliseconds);

                if (_loggingEnabled)
                {
                    _logger.LogError(ex, "Error uploading file to {Url}", url);
                }

                return new UploadResult
                {
                    Success = false,
                    ErrorMessage = ex.Message,
                    Duration = stopwatch.Elapsed
                };
            }
        }

        #endregion

        #region 配置和工具方法

        /// <inheritdoc/>
        public void SetDefaultHeaders(Dictionary<string, string> headers)
        {
            if (headers == null) throw new ArgumentNullException(nameof(headers));
            foreach (var header in headers)
            {
                _httpClient.DefaultRequestHeaders.Remove(header.Key);
                _httpClient.DefaultRequestHeaders.TryAddWithoutValidation(header.Key, header.Value);
            }
        }

        /// <inheritdoc/>
        public void AddDefaultHeader(string key, string value)
        {
            if (string.IsNullOrEmpty(key)) throw new ArgumentNullException(nameof(key));
            _httpClient.DefaultRequestHeaders.Remove(key);
            _httpClient.DefaultRequestHeaders.TryAddWithoutValidation(key, value);
        }

        /// <inheritdoc/>
        public void RemoveDefaultHeader(string key)
        {
            if (string.IsNullOrEmpty(key)) throw new ArgumentNullException(nameof(key));
            _httpClient.DefaultRequestHeaders.Remove(key);
        }

        /// <inheritdoc/>
        public void SetDefaultTimeout(TimeSpan timeout)
        {
            if (timeout <= TimeSpan.Zero && timeout != Timeout.InfiniteTimeSpan)
                throw new ArgumentOutOfRangeException(nameof(timeout), "Timeout must be greater than zero or Timeout.InfiniteTimeSpan");
            _httpClient.Timeout = timeout;
        }

        /// <inheritdoc/>
        public void SetAutomaticDecompression(bool enable)
        {
            if (_httpClientHandler != null)
            {
                _httpClientHandler.AutomaticDecompression = enable
                    ? DecompressionMethods.GZip | DecompressionMethods.Deflate
                    : DecompressionMethods.None;
            }
        }

        /// <inheritdoc/>
        public void SetCookieContainer(CookieContainer cookieContainer)
        {
            if (_httpClientHandler != null)
            {
                _httpClientHandler.CookieContainer = cookieContainer;
            }
        }

        /// <inheritdoc/>
        public CookieContainer GetCookieContainer()
        {
            return _httpClientHandler?.CookieContainer;
        }

        /// <inheritdoc/>
        public void ClearDefaultHeaders()
        {
            _httpClient.DefaultRequestHeaders.Clear();
        }

        /// <inheritdoc/>
        public string SerializeToJson<T>(T obj)
        {
            return JsonSerializer.Serialize(obj, _jsonOptions);
        }

        /// <inheritdoc/>
        public T DeserializeFromJson<T>(string json)
        {
            if (string.IsNullOrEmpty(json)) throw new ArgumentNullException(nameof(json));
            return JsonSerializer.Deserialize<T>(json, _jsonOptions);
        }

        /// <inheritdoc/>
        public HttpClient GetHttpClient()
        {
            return _httpClient;
        }

        /// <inheritdoc/>
        public void SetProxy(IWebProxy proxy)
        {
            if (_httpClientHandler != null)
            {
                _httpClientHandler.Proxy = proxy;
            }
        }

        /// <inheritdoc/>
        public void SetMaxConnectionsPerServer(int maxConnections)
        {
            if (maxConnections <= 0) throw new ArgumentOutOfRangeException(nameof(maxConnections), "Max connections must be greater than zero");
            if (_httpClientHandler != null)
            {
                _httpClientHandler.MaxConnectionsPerServer = maxConnections;
            }
        }

        /// <inheritdoc/>
        public void ConfigureJsonOptions(Action<JsonSerializerOptions> configure)
        {
            configure?.Invoke(_jsonOptions);
        }

        /// <inheritdoc/>
        public void AddRequestInterceptor(Func<HttpRequestMessage, Task> interceptor)
        {
            if (interceptor != null)
            {
                _requestInterceptors.Add(interceptor);
            }
        }

        /// <inheritdoc/>
        public void AddResponseInterceptor(Func<HttpResponseMessage, Task> interceptor)
        {
            if (interceptor != null)
            {
                _responseInterceptors.Add(interceptor);
            }
        }

        /// <inheritdoc/>
        public void ClearInterceptors()
        {
            _requestInterceptors.Clear();
            _responseInterceptors.Clear();
        }

        #endregion

        #region 批量请求处理

        /// <inheritdoc/>
        public async Task<List<BatchResponse>> SendBatchAsync(List<BatchRequest> requests, int maxConcurrency = 5, CancellationToken cancellationToken = default)
        {
            if (requests == null) throw new ArgumentNullException(nameof(requests));
            if (maxConcurrency <= 0) throw new ArgumentOutOfRangeException(nameof(maxConcurrency));

            using var semaphore = new SemaphoreSlim(maxConcurrency);
            var tasks = new List<Task<BatchResponse>>();

            foreach (var request in requests)
            {
                await semaphore.WaitAsync(cancellationToken);

                var task = Task.Run(async () =>
                {
                    try
                    {
                        var content = request.Content;
                        if (content != null)
                        {
                            await content.LoadIntoBufferAsync();
                        }

                        var response = await SendAsync(request.Method, request.Url, request.Content, request.Headers, cancellationToken);
                        var responseContent = await response.Content.ReadAsStringAsync(cancellationToken);

                        return new BatchResponse
                        {
                            Request = request,
                            Response = response,
                            Content = responseContent,
                            Success = response.IsSuccessStatusCode
                        };
                    }
                    catch (Exception ex)
                    {
                        if (_loggingEnabled)
                        {
                            _logger.LogError(ex, "Error processing batch request to {Url}", request.Url);
                        }
                        return new BatchResponse
                        {
                            Request = request,
                            Success = false,
                            ErrorMessage = ex.Message
                        };
                    }
                    finally
                    {
                        semaphore.Release();
                    }
                }, cancellationToken);

                tasks.Add(task);
            }

            return new List<BatchResponse>(await Task.WhenAll(tasks));
        }

        /// <inheritdoc/>
        public async Task<List<BatchResponse>> SendSequentialAsync(List<BatchRequest> requests, CancellationToken cancellationToken = default)
        {
            if (requests == null) throw new ArgumentNullException(nameof(requests));

            var responses = new List<BatchResponse>();

            foreach (var request in requests)
            {
                if (cancellationToken.IsCancellationRequested)
                    break;

                try
                {
                    var content = request.Content;
                    if (content != null)
                    {
                        await content.LoadIntoBufferAsync();
                    }

                    var response = await SendAsync(request.Method, request.Url, request.Content, request.Headers, cancellationToken);
                    var responseContent = await response.Content.ReadAsStringAsync(cancellationToken);

                    responses.Add(new BatchResponse
                    {
                        Request = request,
                        Response = response,
                        Content = responseContent,
                        Success = response.IsSuccessStatusCode
                    });
                }
                catch (Exception ex)
                {
                    if (_loggingEnabled)
                    {
                        _logger.LogError(ex, "Error processing sequential request to {Url}", request.Url);
                    }
                    responses.Add(new BatchResponse
                    {
                        Request = request,
                        Success = false,
                        ErrorMessage = ex.Message
                    });
                }
            }

            return responses;
        }

        #endregion

        #region 监控和统计

        /// <inheritdoc/>
        public HttpClientStatistics GetStatistics()
        {
            lock (_statsLock)
            {
                return new HttpClientStatistics
                {
                    DefaultTimeout = _httpClient.Timeout,
                    DefaultHeadersCount = _httpClient.DefaultRequestHeaders.Count(),
                    SupportsAutomaticDecompression = _httpClientHandler?.AutomaticDecompression != DecompressionMethods.None,
                    UseCookies = _httpClientHandler?.UseCookies ?? false,
                    TotalRequests = _totalRequests,
                    SuccessfulRequests = _successfulRequests,
                    FailedRequests = _failedRequests,
                    AverageResponseTimeMs = _totalRequests > 0 ? (double)_totalResponseTimeMs / _totalRequests : 0
                };
            }
        }

        /// <inheritdoc/>
        public void EnableLogging(bool enable = true)
        {
            _loggingEnabled = enable;
        }

        /// <inheritdoc/>
        public void ResetStatistics()
        {
            lock (_statsLock)
            {
                _totalRequests = 0;
                _successfulRequests = 0;
                _failedRequests = 0;
                _totalResponseTimeMs = 0;
            }
        }

        #endregion

        #region 私有方法

        private void ValidateUrl(string url)
        {
            if (string.IsNullOrEmpty(url))
            {
                throw new ArgumentNullException(nameof(url));
            }

            if (!Uri.TryCreate(url, UriKind.Absolute, out var uri) ||
                (uri.Scheme != Uri.UriSchemeHttp && uri.Scheme != Uri.UriSchemeHttps))
            {
                throw new ArgumentException($"Invalid URL: {url}", nameof(url));
            }
        }

        private void AddHeaders(HttpRequestMessage request, Dictionary<string, string> headers)
        {
            if (headers != null)
            {
                foreach (var header in headers)
                {
                    request.Headers.TryAddWithoutValidation(header.Key, header.Value);
                }
            }
        }

        private async Task<HttpResponseMessage> SendRequestAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var stopwatch = Stopwatch.StartNew();

            try
            {
                foreach (var interceptor in _requestInterceptors)
                {
                    await interceptor(request);
                }

                var response = await _httpClient.SendAsync(request, cancellationToken);

                foreach (var interceptor in _responseInterceptors)
                {
                    await interceptor(response);
                }

                stopwatch.Stop();
                UpdateStatistics(response.IsSuccessStatusCode, stopwatch.ElapsedMilliseconds);

                if (_loggingEnabled)
                {
                    _logger.LogDebug("HTTP {Method} {Url} completed with {StatusCode} in {Duration}ms",
                        request.Method, request.RequestUri, response.StatusCode, stopwatch.ElapsedMilliseconds);
                }

                return response;
            }
            catch (Exception ex)
            {
                stopwatch.Stop();
                UpdateStatistics(false, stopwatch.ElapsedMilliseconds);

                if (_loggingEnabled)
                {
                    _logger.LogError(ex, "HTTP {Method} {Url} failed after {Duration}ms",
                        request.Method, request.RequestUri, stopwatch.ElapsedMilliseconds);
                }

                throw;
            }
        }

        private async Task<T> HandleResponseAsync<T>(HttpResponseMessage response)
        {
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            return DeserializeFromJson<T>(content);
        }

        private async Task<HttpResponseMessage> SendWithRetryAsync(
            Func<Task<HttpResponseMessage>> requestFunc,
            int maxRetries,
            TimeSpan? retryDelay,
            CancellationToken cancellationToken)
        {
            var delay = retryDelay ?? TimeSpan.FromSeconds(1);
            var currentDelay = delay;
            Exception lastException = null;

            for (int i = 0; i <= maxRetries; i++)
            {
                try
                {
                    var response = await requestFunc();

                    if (response.IsSuccessStatusCode ||
                        response.StatusCode == HttpStatusCode.BadRequest ||
                        response.StatusCode == HttpStatusCode.Unauthorized ||
                        response.StatusCode == HttpStatusCode.Forbidden ||
                        response.StatusCode == HttpStatusCode.NotFound)
                    {
                        return response;
                    }

                    if (i < maxRetries)
                    {
                        if (_loggingEnabled)
                        {
                            _logger.LogWarning("Request failed with status {StatusCode}, retrying in {Delay}ms (attempt {Attempt} of {MaxRetries})",
                                response.StatusCode, currentDelay.TotalMilliseconds, i + 1, maxRetries);
                        }
                        await Task.Delay(currentDelay, cancellationToken);
                        currentDelay = TimeSpan.FromMilliseconds(currentDelay.TotalMilliseconds * 2);
                    }
                }
                catch (HttpRequestException ex) when (i < maxRetries)
                {
                    lastException = ex;
                    if (_loggingEnabled)
                    {
                        _logger.LogWarning(ex, "Request failed with exception, retrying in {Delay}ms (attempt {Attempt} of {MaxRetries})",
                            currentDelay.TotalMilliseconds, i + 1, maxRetries);
                    }
                    await Task.Delay(currentDelay, cancellationToken);
                    currentDelay = TimeSpan.FromMilliseconds(currentDelay.TotalMilliseconds * 2);
                }
                catch (TaskCanceledException ex) when (i < maxRetries && !cancellationToken.IsCancellationRequested)
                {
                    lastException = ex;
                    if (_loggingEnabled)
                    {
                        _logger.LogWarning(ex, "Request timed out, retrying in {Delay}ms (attempt {Attempt} of {MaxRetries})",
                            currentDelay.TotalMilliseconds, i + 1, maxRetries);
                    }
                    await Task.Delay(currentDelay, cancellationToken);
                    currentDelay = TimeSpan.FromMilliseconds(currentDelay.TotalMilliseconds * 2);
                }
            }

            if (lastException != null)
            {
                throw lastException;
            }

            return await requestFunc();
        }

        private void UpdateStatistics(bool success, long responseTimeMs)
        {
            lock (_statsLock)
            {
                _totalRequests++;
                _totalResponseTimeMs += responseTimeMs;
                if (success)
                {
                    _successfulRequests++;
                }
                else
                {
                    _failedRequests++;
                }
            }
        }

        #endregion

        #region IDisposable实现

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _httpClient?.Dispose();
                    _httpClientHandler?.Dispose();
                }

                _disposed = true;
            }
        }

        #endregion
    }

    #region 数据结构

    /// <summary>
    /// HTTP客户端配置选项
    /// </summary>
    /// <remarks>
    /// <para>用于自定义HttpClientHelper的初始化配置</para>
    /// <para>
    /// <b>超时设计说明：</b>建议将 Timeout 设置为无限（默认值），使用 CancellationToken 控制具体请求的超时。
    /// 这种设计提供了更灵活的超时管理，允许每个请求有独立的超时时间。
    /// </para>
    /// <para>示例：</para>
    /// <code>
    /// var options = new HttpClientOptions
    /// {
    ///     Timeout = Timeout.InfiniteTimeSpan, // 或不设置，使用默认无限超时
    ///     MaxConnectionsPerServer = 200,
    ///     EnableAutomaticDecompression = true
    /// };
    /// var httpClient = new HttpClientHelper(options);
    /// 
    /// // 使用 CancellationToken 控制具体请求的超时
    /// using var cts = new CancellationTokenSource(TimeSpan.FromSeconds(30));
    /// var result = await httpClient.GetAsync&lt;User&gt;("https://api.example.com/users/1", cancellationToken: cts.Token);
    /// </code>
    /// </remarks>
    public class HttpClientOptions
    {
        /// <summary>
        /// 获取或设置请求超时时间
        /// </summary>
        /// <remarks>
        /// <para>默认为无限（Timeout.InfiniteTimeSpan）</para>
        /// <para>
        /// <b>建议：</b>保持默认的无限超时，使用 CancellationToken 控制具体请求的超时。
        /// 这种设计提供了更灵活的超时管理，允许每个请求有独立的超时时间。
        /// </para>
        /// <para>
        /// 如果需要设置全局超时作为兜底保护，可以设置一个较大的值（如 5 分钟）。
        /// </para>
        /// </remarks>
        public TimeSpan? Timeout { get; set; }

        /// <summary>
        /// 获取或设置默认请求头字典
        /// </summary>
        /// <remarks>这些请求头会添加到所有请求中</remarks>
        public Dictionary<string, string> DefaultHeaders { get; set; }

        /// <summary>
        /// 获取或设置是否启用自动解压缩（GZip/Deflate）
        /// </summary>
        /// <remarks>默认为true，建议启用以提高传输效率</remarks>
        public bool EnableAutomaticDecompression { get; set; } = true;

        /// <summary>
        /// 获取或设置是否启用Cookie支持
        /// </summary>
        /// <remarks>默认为true</remarks>
        public bool EnableCookies { get; set; } = true;

        /// <summary>
        /// 获取或设置每服务器最大并发连接数
        /// </summary>
        /// <remarks>
        /// <para>默认为100</para>
        /// <para>在高并发场景下可以适当增大此值</para>
        /// </remarks>
        public int MaxConnectionsPerServer { get; set; } = 100;

        /// <summary>
        /// 获取或设置代理服务器配置
        /// </summary>
        /// <remarks>设置为null表示不使用代理</remarks>
        public IWebProxy Proxy { get; set; }

        /// <summary>
        /// 获取或设置是否使用默认凭证
        /// </summary>
        /// <remarks>用于Windows身份验证场景</remarks>
        public bool UseDefaultCredentials { get; set; }

        /// <summary>
        /// 获取或设置SSL证书验证回调函数
        /// </summary>
        /// <remarks>
        /// <para>可用于自定义证书验证逻辑或忽略证书错误（仅用于开发环境）</para>
        /// <para>警告：在生产环境中忽略证书验证会带来安全风险</para>
        /// </remarks>
        public Func<HttpRequestMessage, X509Certificate2, X509Chain, SslPolicyErrors, bool> ServerCertificateValidationCallback { get; set; }

        /// <summary>
        /// 获取或设置JSON序列化选项
        /// </summary>
        /// <remarks>用于自定义JSON序列化行为，如命名策略、日期格式等</remarks>
        public JsonSerializerOptions JsonOptions { get; set; }
    }

    /// <summary>
    /// 下载进度信息
    /// </summary>
    /// <remarks>用于报告文件下载的实时进度</remarks>
    public class DownloadProgress
    {
        /// <summary>
        /// 获取或设置文件总字节数
        /// </summary>
        /// <remarks>如果服务器未提供Content-Length头，此值可能为-1</remarks>
        public long TotalBytes { get; set; }

        /// <summary>
        /// 获取或设置已下载的字节数
        /// </summary>
        public long DownloadedBytes { get; set; }

        /// <summary>
        /// 获取或设置下载进度百分比（0-100）
        /// </summary>
        public int ProgressPercentage { get; set; }
    }

    /// <summary>
    /// 文件下载结果
    /// </summary>
    /// <remarks>包含下载操作的完整结果信息</remarks>
    public class DownloadResult
    {
        /// <summary>
        /// 获取或设置下载是否成功
        /// </summary>
        public bool Success { get; set; }

        /// <summary>
        /// 获取或设置文件保存路径
        /// </summary>
        public string FilePath { get; set; }

        /// <summary>
        /// 获取或设置下载的文件大小（字节）
        /// </summary>
        public long FileSize { get; set; }

        /// <summary>
        /// 获取或设置错误信息（仅在失败时有值）
        /// </summary>
        public string ErrorMessage { get; set; }

        /// <summary>
        /// 获取或设置下载耗时
        /// </summary>
        public TimeSpan Duration { get; set; }
    }

    /// <summary>
    /// 上传进度信息
    /// </summary>
    /// <remarks>用于报告文件上传的实时进度</remarks>
    public class UploadProgress
    {
        /// <summary>
        /// 获取或设置文件总字节数
        /// </summary>
        public long TotalBytes { get; set; }

        /// <summary>
        /// 获取或设置已上传的字节数
        /// </summary>
        public long UploadedBytes { get; set; }

        /// <summary>
        /// 获取或设置上传进度百分比（0-100）
        /// </summary>
        public int ProgressPercentage { get; set; }
    }

    /// <summary>
    /// 文件上传结果
    /// </summary>
    /// <remarks>包含上传操作的完整结果信息</remarks>
    public class UploadResult
    {
        /// <summary>
        /// 获取或设置上传是否成功
        /// </summary>
        public bool Success { get; set; }

        /// <summary>
        /// 获取或设置服务器响应内容
        /// </summary>
        public string ResponseContent { get; set; }

        /// <summary>
        /// 获取或设置错误信息（仅在失败时有值）
        /// </summary>
        public string ErrorMessage { get; set; }

        /// <summary>
        /// 获取或设置上传的文件大小（字节）
        /// </summary>
        public long FileSize { get; set; }

        /// <summary>
        /// 获取或设置上传耗时
        /// </summary>
        public TimeSpan Duration { get; set; }
    }

    /// <summary>
    /// 批量请求定义
    /// </summary>
    /// <remarks>用于批量请求处理时定义单个请求的参数</remarks>
    public class BatchRequest
    {
        /// <summary>
        /// 获取或设置HTTP方法（GET、POST、PUT、DELETE等）
        /// </summary>
        public HttpMethod Method { get; set; }

        /// <summary>
        /// 获取或设置请求URL
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// 获取或设置请求内容（用于POST、PUT等方法）
        /// </summary>
        public HttpContent Content { get; set; }

        /// <summary>
        /// 获取或设置请求头字典
        /// </summary>
        public Dictionary<string, string> Headers { get; set; } = new Dictionary<string, string>();
    }

    /// <summary>
    /// 批量请求响应
    /// </summary>
    /// <remarks>包含批量请求中单个请求的响应信息</remarks>
    public class BatchResponse
    {
        /// <summary>
        /// 获取或设置对应的请求对象
        /// </summary>
        public BatchRequest Request { get; set; }

        /// <summary>
        /// 获取或设置HTTP响应消息
        /// </summary>
        public HttpResponseMessage Response { get; set; }

        /// <summary>
        /// 获取或设置响应内容字符串
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// 获取或设置请求是否成功
        /// </summary>
        public bool Success { get; set; }

        /// <summary>
        /// 获取或设置错误信息（仅在失败时有值）
        /// </summary>
        public string ErrorMessage { get; set; }
    }

    /// <summary>
    /// HTTP客户端统计信息
    /// </summary>
    /// <remarks>用于监控HTTP客户端的运行状态和性能指标</remarks>
    public class HttpClientStatistics
    {
        /// <summary>
        /// 获取或设置默认超时时间
        /// </summary>
        public TimeSpan DefaultTimeout { get; set; }

        /// <summary>
        /// 获取或设置默认请求头数量
        /// </summary>
        public int DefaultHeadersCount { get; set; }

        /// <summary>
        /// 获取或设置是否支持自动解压缩
        /// </summary>
        public bool SupportsAutomaticDecompression { get; set; }

        /// <summary>
        /// 获取或设置是否使用Cookie
        /// </summary>
        public bool UseCookies { get; set; }

        /// <summary>
        /// 获取或设置总请求数
        /// </summary>
        public long TotalRequests { get; set; }

        /// <summary>
        /// 获取或设置成功请求数
        /// </summary>
        public long SuccessfulRequests { get; set; }

        /// <summary>
        /// 获取或设置失败请求数
        /// </summary>
        public long FailedRequests { get; set; }

        /// <summary>
        /// 获取或设置平均响应时间（毫秒）
        /// </summary>
        public double AverageResponseTimeMs { get; set; }
    }

    /// <summary>
    /// 支持进度报告的文件流
    /// </summary>
    /// <remarks>
    /// <para>内部类，用于在文件上传时报告进度</para>
    /// <para>继承自FileStream，重写ReadAsync方法以实现进度回调</para>
    /// </remarks>
    internal class ProgressStream : FileStream
    {
        /// <summary>
        /// 进度回调委托
        /// </summary>
        private readonly Action<UploadProgress> _progressCallback;

        /// <summary>
        /// 文件总字节数
        /// </summary>
        private readonly long _totalBytes;

        /// <summary>
        /// 已上传字节数
        /// </summary>
        private long _uploadedBytes;

        /// <summary>
        /// 初始化ProgressStream实例
        /// </summary>
        /// <param name="path">文件路径</param>
        /// <param name="progressCallback">进度回调函数</param>
        /// <param name="totalBytes">文件总字节数</param>
        public ProgressStream(string path, Action<UploadProgress> progressCallback, long totalBytes)
            : base(path, FileMode.Open, FileAccess.Read, FileShare.Read, 8192, true)
        {
            _progressCallback = progressCallback;
            _totalBytes = totalBytes;
        }

        /// <summary>
        /// 异步读取文件数据并报告进度
        /// </summary>
        /// <param name="buffer">数据缓冲区</param>
        /// <param name="offset">偏移量</param>
        /// <param name="count">读取数量</param>
        /// <param name="cancellationToken">取消令牌</param>
        /// <returns>实际读取的字节数</returns>
        public override async Task<int> ReadAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
        {
            var bytesRead = await base.ReadAsync(buffer, offset, count, cancellationToken);
            _uploadedBytes += bytesRead;

            if (_progressCallback != null && _totalBytes > 0)
            {
                var progress = new UploadProgress
                {
                    TotalBytes = _totalBytes,
                    UploadedBytes = _uploadedBytes,
                    ProgressPercentage = (int)((double)_uploadedBytes / _totalBytes * 100)
                };
                _progressCallback(progress);
            }

            return bytesRead;
        }
    }

    #endregion
}
