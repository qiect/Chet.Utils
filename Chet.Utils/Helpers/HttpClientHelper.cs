using System.Net;
using System.Text;
using System.Text.Json;

namespace Chet.Utils.Helpers
{
    /// <summary>
    /// HttpClient帮助类
    /// 提供丰富的HTTP请求功能，包括重试机制、超时控制、认证、缓存等高级特性
    /// </summary>
    public static class HttpClientHelper
    {
        #region 静态HttpClient实例

        private static readonly HttpClient _httpClient;
        private static readonly HttpClientHandler _httpClientHandler;

        static HttpClientHelper()
        {
            _httpClientHandler = new HttpClientHandler
            {
                AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate,
                UseCookies = true
            };

            _httpClient = new HttpClient(_httpClientHandler)
            {
                Timeout = TimeSpan.FromSeconds(30)
            };

            // 设置默认请求头
            _httpClient.DefaultRequestHeaders.Add("User-Agent", "Chet.HttpClientHelper/1.0");
            _httpClient.DefaultRequestHeaders.Add("Accept", "application/json");
        }

        #endregion

        #region 基础HTTP请求方法

        /// <summary>
        /// 发送GET请求
        /// </summary>
        /// <param name="url">请求URL</param>
        /// <param name="headers">请求头</param>
        /// <param name="cancellationToken">取消令牌</param>
        /// <returns>HTTP响应</returns>
        public static async Task<HttpResponseMessage> GetAsync(string url, Dictionary<string, string> headers = null, CancellationToken cancellationToken = default)
        {
            using var request = new HttpRequestMessage(HttpMethod.Get, url);
            AddHeaders(request, headers);
            return await _httpClient.SendAsync(request, cancellationToken);
        }

        /// <summary>
        /// 发送GET请求并返回字符串内容
        /// </summary>
        /// <param name="url">请求URL</param>
        /// <param name="headers">请求头</param>
        /// <param name="cancellationToken">取消令牌</param>
        /// <returns>响应内容</returns>
        public static async Task<string> GetStringAsync(string url, Dictionary<string, string> headers = null, CancellationToken cancellationToken = default)
        {
            var response = await GetAsync(url, headers, cancellationToken);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync(cancellationToken);
        }

        /// <summary>
        /// 发送GET请求并返回字节数组内容
        /// </summary>
        /// <param name="url">请求URL</param>
        /// <param name="headers">请求头</param>
        /// <param name="cancellationToken">取消令牌</param>
        /// <returns>响应内容</returns>
        public static async Task<byte[]> GetByteArrayAsync(string url, Dictionary<string, string> headers = null, CancellationToken cancellationToken = default)
        {
            var response = await GetAsync(url, headers, cancellationToken);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsByteArrayAsync(cancellationToken);
        }

        /// <summary>
        /// 发送POST请求
        /// </summary>
        /// <param name="url">请求URL</param>
        /// <param name="content">请求内容</param>
        /// <param name="headers">请求头</param>
        /// <param name="cancellationToken">取消令牌</param>
        /// <returns>HTTP响应</returns>
        public static async Task<HttpResponseMessage> PostAsync(string url, HttpContent content, Dictionary<string, string> headers = null, CancellationToken cancellationToken = default)
        {
            using var request = new HttpRequestMessage(HttpMethod.Post, url)
            {
                Content = content
            };
            AddHeaders(request, headers);
            return await _httpClient.SendAsync(request, cancellationToken);
        }

        /// <summary>
        /// 发送POST请求（JSON数据）
        /// </summary>
        /// <param name="url">请求URL</param>
        /// <param name="data">请求数据</param>
        /// <param name="headers">请求头</param>
        /// <param name="cancellationToken">取消令牌</param>
        /// <returns>HTTP响应</returns>
        public static async Task<HttpResponseMessage> PostJsonAsync<T>(string url, T data, Dictionary<string, string> headers = null, CancellationToken cancellationToken = default)
        {
            var json = JsonSerializer.Serialize(data);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            return await PostAsync(url, content, headers, cancellationToken);
        }

        /// <summary>
        /// 发送POST请求（表单数据）
        /// </summary>
        /// <param name="url">请求URL</param>
        /// <param name="formData">表单数据</param>
        /// <param name="headers">请求头</param>
        /// <param name="cancellationToken">取消令牌</param>
        /// <returns>HTTP响应</returns>
        public static async Task<HttpResponseMessage> PostFormAsync(string url, Dictionary<string, string> formData, Dictionary<string, string> headers = null, CancellationToken cancellationToken = default)
        {
            var content = new FormUrlEncodedContent(formData);
            return await PostAsync(url, content, headers, cancellationToken);
        }

        /// <summary>
        /// 发送PUT请求
        /// </summary>
        /// <param name="url">请求URL</param>
        /// <param name="content">请求内容</param>
        /// <param name="headers">请求头</param>
        /// <param name="cancellationToken">取消令牌</param>
        /// <returns>HTTP响应</returns>
        public static async Task<HttpResponseMessage> PutAsync(string url, HttpContent content, Dictionary<string, string> headers = null, CancellationToken cancellationToken = default)
        {
            using var request = new HttpRequestMessage(HttpMethod.Put, url)
            {
                Content = content
            };
            AddHeaders(request, headers);
            return await _httpClient.SendAsync(request, cancellationToken);
        }

        /// <summary>
        /// 发送PUT请求（JSON数据）
        /// </summary>
        /// <param name="url">请求URL</param>
        /// <param name="data">请求数据</param>
        /// <param name="headers">请求头</param>
        /// <param name="cancellationToken">取消令牌</param>
        /// <returns>HTTP响应</returns>
        public static async Task<HttpResponseMessage> PutJsonAsync<T>(string url, T data, Dictionary<string, string> headers = null, CancellationToken cancellationToken = default)
        {
            var json = JsonSerializer.Serialize(data);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            return await PutAsync(url, content, headers, cancellationToken);
        }

        /// <summary>
        /// 发送DELETE请求
        /// </summary>
        /// <param name="url">请求URL</param>
        /// <param name="headers">请求头</param>
        /// <param name="cancellationToken">取消令牌</param>
        /// <returns>HTTP响应</returns>
        public static async Task<HttpResponseMessage> DeleteAsync(string url, Dictionary<string, string> headers = null, CancellationToken cancellationToken = default)
        {
            using var request = new HttpRequestMessage(HttpMethod.Delete, url);
            AddHeaders(request, headers);
            return await _httpClient.SendAsync(request, cancellationToken);
        }

        /// <summary>
        /// 发送自定义HTTP请求
        /// </summary>
        /// <param name="method">HTTP方法</param>
        /// <param name="url">请求URL</param>
        /// <param name="content">请求内容</param>
        /// <param name="headers">请求头</param>
        /// <param name="cancellationToken">取消令牌</param>
        /// <returns>HTTP响应</returns>
        public static async Task<HttpResponseMessage> SendAsync(HttpMethod method, string url, HttpContent content = null, Dictionary<string, string> headers = null, CancellationToken cancellationToken = default)
        {
            using var request = new HttpRequestMessage(method, url)
            {
                Content = content
            };
            AddHeaders(request, headers);
            return await _httpClient.SendAsync(request, cancellationToken);
        }

        #endregion

        #region 高级HTTP请求方法

        /// <summary>
        /// 发送带重试机制的GET请求
        /// </summary>
        /// <param name="url">请求URL</param>
        /// <param name="maxRetries">最大重试次数</param>
        /// <param name="retryDelay">重试延迟时间</param>
        /// <param name="headers">请求头</param>
        /// <param name="cancellationToken">取消令牌</param>
        /// <returns>HTTP响应</returns>
        public static async Task<HttpResponseMessage> GetWithRetryAsync(string url, int maxRetries = 3, TimeSpan? retryDelay = null, Dictionary<string, string> headers = null, CancellationToken cancellationToken = default)
        {
            return await SendWithRetryAsync(() => GetAsync(url, headers, cancellationToken), maxRetries, retryDelay ?? TimeSpan.FromSeconds(1), cancellationToken);
        }

        /// <summary>
        /// 发送带重试机制的POST请求
        /// </summary>
        /// <param name="url">请求URL</param>
        /// <param name="content">请求内容</param>
        /// <param name="maxRetries">最大重试次数</param>
        /// <param name="retryDelay">重试延迟时间</param>
        /// <param name="headers">请求头</param>
        /// <param name="cancellationToken">取消令牌</param>
        /// <returns>HTTP响应</returns>
        public static async Task<HttpResponseMessage> PostWithRetryAsync(string url, HttpContent content, int maxRetries = 3, TimeSpan? retryDelay = null, Dictionary<string, string> headers = null, CancellationToken cancellationToken = default)
        {
            return await SendWithRetryAsync(() => PostAsync(url, content, headers, cancellationToken), maxRetries, retryDelay ?? TimeSpan.FromSeconds(1), cancellationToken);
        }

        /// <summary>
        /// 发送带超时控制的GET请求
        /// </summary>
        /// <param name="url">请求URL</param>
        /// <param name="timeout">超时时间</param>
        /// <param name="headers">请求头</param>
        /// <param name="cancellationToken">取消令牌</param>
        /// <returns>HTTP响应</returns>
        public static async Task<HttpResponseMessage> GetWithTimeoutAsync(string url, TimeSpan timeout, Dictionary<string, string> headers = null, CancellationToken cancellationToken = default)
        {
            using var cts = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);
            cts.CancelAfter(timeout);
            return await GetAsync(url, headers, cts.Token);
        }

        /// <summary>
        /// 发送带超时控制的POST请求
        /// </summary>
        /// <param name="url">请求URL</param>
        /// <param name="content">请求内容</param>
        /// <param name="timeout">超时时间</param>
        /// <param name="headers">请求头</param>
        /// <param name="cancellationToken">取消令牌</param>
        /// <returns>HTTP响应</returns>
        public static async Task<HttpResponseMessage> PostWithTimeoutAsync(string url, HttpContent content, TimeSpan timeout, Dictionary<string, string> headers = null, CancellationToken cancellationToken = default)
        {
            using var cts = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);
            cts.CancelAfter(timeout);
            return await PostAsync(url, content, headers, cts.Token);
        }

        /// <summary>
        /// 发送带认证的GET请求
        /// </summary>
        /// <param name="url">请求URL</param>
        /// <param name="authToken">认证令牌</param>
        /// <param name="authScheme">认证方案（默认Bearer）</param>
        /// <param name="headers">请求头</param>
        /// <param name="cancellationToken">取消令牌</param>
        /// <returns>HTTP响应</returns>
        public static async Task<HttpResponseMessage> GetWithAuthAsync(string url, string authToken, string authScheme = "Bearer", Dictionary<string, string> headers = null, CancellationToken cancellationToken = default)
        {
            var authHeaders = headers ?? new Dictionary<string, string>();
            authHeaders["Authorization"] = $"{authScheme} {authToken}";
            return await GetAsync(url, authHeaders, cancellationToken);
        }

        /// <summary>
        /// 发送带基本认证的GET请求
        /// </summary>
        /// <param name="url">请求URL</param>
        /// <param name="username">用户名</param>
        /// <param name="password">密码</param>
        /// <param name="headers">请求头</param>
        /// <param name="cancellationToken">取消令牌</param>
        /// <returns>HTTP响应</returns>
        public static async Task<HttpResponseMessage> GetWithBasicAuthAsync(string url, string username, string password, Dictionary<string, string> headers = null, CancellationToken cancellationToken = default)
        {
            var credentials = Convert.ToBase64String(Encoding.ASCII.GetBytes($"{username}:{password}"));
            var authHeaders = headers ?? new Dictionary<string, string>();
            authHeaders["Authorization"] = $"Basic {credentials}";
            return await GetAsync(url, authHeaders, cancellationToken);
        }

        /// <summary>
        /// 下载文件
        /// </summary>
        /// <param name="url">文件URL</param>
        /// <param name="filePath">保存路径</param>
        /// <param name="headers">请求头</param>
        /// <param name="progressCallback">进度回调</param>
        /// <param name="cancellationToken">取消令牌</param>
        /// <returns>下载结果</returns>
        public static async Task<DownloadResult> DownloadFileAsync(string url, string filePath, Dictionary<string, string> headers = null, Action<DownloadProgress> progressCallback = null, CancellationToken cancellationToken = default)
        {
            try
            {
                var response = await GetAsync(url, headers, cancellationToken);
                response.EnsureSuccessStatusCode();

                var totalBytes = response.Content.Headers.ContentLength ?? -1;
                var downloadedBytes = 0L;

                using var contentStream = await response.Content.ReadAsStreamAsync(cancellationToken);
                using var fileStream = new FileStream(filePath, FileMode.Create, FileAccess.Write, FileShare.None, 8192, true);

                var buffer = new byte[8192];
                var bytesRead = 0;

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

                return new DownloadResult
                {
                    Success = true,
                    FilePath = filePath,
                    FileSize = downloadedBytes
                };
            }
            catch (Exception ex)
            {
                return new DownloadResult
                {
                    Success = false,
                    ErrorMessage = ex.Message
                };
            }
        }

        /// <summary>
        /// 上传文件
        /// </summary>
        /// <param name="url">上传URL</param>
        /// <param name="filePath">文件路径</param>
        /// <param name="fieldName">表单字段名</param>
        /// <param name="additionalData">附加表单数据</param>
        /// <param name="headers">请求头</param>
        /// <param name="progressCallback">进度回调</param>
        /// <param name="cancellationToken">取消令牌</param>
        /// <returns>上传结果</returns>
        public static async Task<UploadResult> UploadFileAsync(string url, string filePath, string fieldName = "file", Dictionary<string, string> additionalData = null, Dictionary<string, string> headers = null, Action<UploadProgress> progressCallback = null, CancellationToken cancellationToken = default)
        {
            try
            {
                using var content = new MultipartFormDataContent();

                // 添加附加数据
                if (additionalData != null)
                {
                    foreach (var item in additionalData)
                    {
                        content.Add(new StringContent(item.Value), item.Key);
                    }
                }

                // 添加文件
                var fileContent = new StreamContent(new FileStream(filePath, FileMode.Open, FileAccess.Read));
                var fileName = Path.GetFileName(filePath);
                content.Add(fileContent, fieldName, fileName);

                using var request = new HttpRequestMessage(HttpMethod.Post, url)
                {
                    Content = content
                };
                AddHeaders(request, headers);

                var response = await _httpClient.SendAsync(request, cancellationToken);
                response.EnsureSuccessStatusCode();

                var responseContent = await response.Content.ReadAsStringAsync(cancellationToken);

                return new UploadResult
                {
                    Success = true,
                    ResponseContent = responseContent
                };
            }
            catch (Exception ex)
            {
                return new UploadResult
                {
                    Success = false,
                    ErrorMessage = ex.Message
                };
            }
        }

        #endregion

        #region 配置和工具方法

        /// <summary>
        /// 设置默认请求头
        /// </summary>
        /// <param name="headers">请求头字典</param>
        public static void SetDefaultHeaders(Dictionary<string, string> headers)
        {
            foreach (var header in headers)
            {
                _httpClient.DefaultRequestHeaders.Remove(header.Key);
                _httpClient.DefaultRequestHeaders.Add(header.Key, header.Value);
            }
        }

        /// <summary>
        /// 设置默认超时时间
        /// </summary>
        /// <param name="timeout">超时时间</param>
        public static void SetDefaultTimeout(TimeSpan timeout)
        {
            _httpClient.Timeout = timeout;
        }

        /// <summary>
        /// 启用或禁用自动解压缩
        /// </summary>
        /// <param name="enable">是否启用</param>
        public static void SetAutomaticDecompression(bool enable)
        {
            _httpClientHandler.AutomaticDecompression = enable ?
                DecompressionMethods.GZip | DecompressionMethods.Deflate :
                DecompressionMethods.None;
        }

        /// <summary>
        /// 设置Cookie容器
        /// </summary>
        /// <param name="cookieContainer">Cookie容器</param>
        public static void SetCookieContainer(CookieContainer cookieContainer)
        {
            _httpClientHandler.CookieContainer = cookieContainer;
        }

        /// <summary>
        /// 获取Cookie容器
        /// </summary>
        /// <returns>Cookie容器</returns>
        public static CookieContainer GetCookieContainer()
        {
            return _httpClientHandler.CookieContainer;
        }

        /// <summary>
        /// 清除默认请求头
        /// </summary>
        public static void ClearDefaultHeaders()
        {
            _httpClient.DefaultRequestHeaders.Clear();
        }

        /// <summary>
        /// 添加请求头
        /// </summary>
        /// <param name="request">HTTP请求消息</param>
        /// <param name="headers">请求头字典</param>
        private static void AddHeaders(HttpRequestMessage request, Dictionary<string, string> headers)
        {
            if (headers != null)
            {
                foreach (var header in headers)
                {
                    request.Headers.Add(header.Key, header.Value);
                }
            }
        }

        /// <summary>
        /// 带重试机制的请求发送
        /// </summary>
        /// <param name="requestFunc">请求函数</param>
        /// <param name="maxRetries">最大重试次数</param>
        /// <param name="retryDelay">重试延迟</param>
        /// <param name="cancellationToken">取消令牌</param>
        /// <returns>HTTP响应</returns>
        private static async Task<HttpResponseMessage> SendWithRetryAsync(Func<Task<HttpResponseMessage>> requestFunc, int maxRetries, TimeSpan retryDelay, CancellationToken cancellationToken)
        {
            for (int i = 0; i <= maxRetries; i++)
            {
                try
                {
                    var response = await requestFunc();

                    // 对于某些状态码不进行重试
                    if (response.IsSuccessStatusCode ||
                        response.StatusCode == HttpStatusCode.BadRequest ||
                        response.StatusCode == HttpStatusCode.Unauthorized ||
                        response.StatusCode == HttpStatusCode.Forbidden)
                    {
                        return response;
                    }

                    if (i < maxRetries)
                    {
                        await Task.Delay(retryDelay, cancellationToken);
                    }
                }
                catch (HttpRequestException) when (i < maxRetries)
                {
                    await Task.Delay(retryDelay, cancellationToken);
                }
                catch (TaskCanceledException) when (i < maxRetries && !cancellationToken.IsCancellationRequested)
                {
                    await Task.Delay(retryDelay, cancellationToken);
                }
            }

            // 最后一次尝试
            return await requestFunc();
        }

        /// <summary>
        /// 序列化对象为JSON
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="obj">对象实例</param>
        /// <returns>JSON字符串</returns>
        public static string SerializeToJson<T>(T obj)
        {
            return JsonSerializer.Serialize(obj, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });
        }

        /// <summary>
        /// 反序列化JSON为对象
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="json">JSON字符串</param>
        /// <returns>对象实例</returns>
        public static T DeserializeFromJson<T>(string json)
        {
            return JsonSerializer.Deserialize<T>(json, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });
        }

        /// <summary>
        /// 获取HTTP客户端实例
        /// </summary>
        /// <returns>HTTP客户端实例</returns>
        public static HttpClient GetHttpClient()
        {
            return _httpClient;
        }

        #endregion

        #region 批量请求处理

        /// <summary>
        /// 并行发送多个HTTP请求
        /// </summary>
        /// <param name="requests">请求列表</param>
        /// <param name="maxConcurrency">最大并发数</param>
        /// <param name="cancellationToken">取消令牌</param>
        /// <returns>响应列表</returns>
        public static async Task<List<BatchResponse>> SendBatchAsync(List<BatchRequest> requests, int maxConcurrency = 5, CancellationToken cancellationToken = default)
        {
            using var semaphore = new SemaphoreSlim(maxConcurrency);
            var tasks = new List<Task<BatchResponse>>();

            foreach (var request in requests)
            {
                await semaphore.WaitAsync(cancellationToken);

                var task = Task.Run(async () =>
                {
                    try
                    {
                        var response = await SendAsync(request.Method, request.Url, request.Content, request.Headers, cancellationToken);
                        var content = await response.Content.ReadAsStringAsync(cancellationToken);

                        return new BatchResponse
                        {
                            Request = request,
                            Response = response,
                            Content = content,
                            Success = response.IsSuccessStatusCode
                        };
                    }
                    catch (Exception ex)
                    {
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

        /// <summary>
        /// 顺序发送多个HTTP请求
        /// </summary>
        /// <param name="requests">请求列表</param>
        /// <param name="cancellationToken">取消令牌</param>
        /// <returns>响应列表</returns>
        public static async Task<List<BatchResponse>> SendSequentialAsync(List<BatchRequest> requests, CancellationToken cancellationToken = default)
        {
            var responses = new List<BatchResponse>();

            foreach (var request in requests)
            {
                if (cancellationToken.IsCancellationRequested)
                    break;

                try
                {
                    var response = await SendAsync(request.Method, request.Url, request.Content, request.Headers, cancellationToken);
                    var content = await response.Content.ReadAsStringAsync(cancellationToken);

                    responses.Add(new BatchResponse
                    {
                        Request = request,
                        Response = response,
                        Content = content,
                        Success = response.IsSuccessStatusCode
                    });
                }
                catch (Exception ex)
                {
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

        /// <summary>
        /// 获取HTTP客户端统计信息
        /// </summary>
        /// <returns>统计信息</returns>
        public static HttpClientStatistics GetStatistics()
        {
            // 注意：HttpClient本身不提供内置统计信息
            // 这里可以结合自定义中间件或包装器来实现
            return new HttpClientStatistics
            {
                DefaultTimeout = _httpClient.Timeout,
                DefaultHeadersCount = _httpClient.DefaultRequestHeaders.Count(),
                SupportsAutomaticDecompression = _httpClientHandler.AutomaticDecompression != DecompressionMethods.None,
                UseCookies = _httpClientHandler.UseCookies
            };
        }

        /// <summary>
        /// 启用请求日志记录
        /// </summary>
        /// <param name="enable">是否启用</param>
        public static void EnableLogging(bool enable = true)
        {
            // 这里可以集成日志框架如ILogger等
            // 实际实现需要根据具体日志系统进行调整
        }

        #endregion
    }

    #region 数据结构和枚举

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

    /// <summary>
    /// 下载结果
    /// </summary>
    public class DownloadResult
    {
        /// <summary>
        /// 是否成功
        /// </summary>
        public bool Success { get; set; }

        /// <summary>
        /// 文件路径
        /// </summary>
        public string FilePath { get; set; }

        /// <summary>
        /// 文件大小
        /// </summary>
        public long FileSize { get; set; }

        /// <summary>
        /// 错误信息
        /// </summary>
        public string ErrorMessage { get; set; }
    }

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

    /// <summary>
    /// 上传结果
    /// </summary>
    public class UploadResult
    {
        /// <summary>
        /// 是否成功
        /// </summary>
        public bool Success { get; set; }

        /// <summary>
        /// 响应内容
        /// </summary>
        public string ResponseContent { get; set; }

        /// <summary>
        /// 错误信息
        /// </summary>
        public string ErrorMessage { get; set; }
    }

    /// <summary>
    /// 批量请求
    /// </summary>
    public class BatchRequest
    {
        /// <summary>
        /// HTTP方法
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
        /// HTTP响应
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

    /// <summary>
    /// HTTP客户端统计信息
    /// </summary>
    public class HttpClientStatistics
    {
        /// <summary>
        /// 默认超时时间
        /// </summary>
        public TimeSpan DefaultTimeout { get; set; }

        /// <summary>
        /// 默认请求头数量
        /// </summary>
        public int DefaultHeadersCount { get; set; }

        /// <summary>
        /// 是否支持自动解压缩
        /// </summary>
        public bool SupportsAutomaticDecompression { get; set; }

        /// <summary>
        /// 是否使用Cookie
        /// </summary>
        public bool UseCookies { get; set; }

        /// <summary>
        /// 总请求数
        /// </summary>
        public long TotalRequests { get; set; }

        /// <summary>
        /// 成功请求数
        /// </summary>
        public long SuccessfulRequests { get; set; }

        /// <summary>
        /// 失败请求数
        /// </summary>
        public long FailedRequests { get; set; }

        /// <summary>
        /// 平均响应时间（毫秒）
        /// </summary>
        public double AverageResponseTimeMs { get; set; }
    }

    #endregion
}