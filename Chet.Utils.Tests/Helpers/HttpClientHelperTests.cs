using Microsoft.Extensions.Logging;
using Moq;
using Moq.Protected;
using System.Net;
using System.Text;
using Xunit;

namespace Chet.Utils.Helpers.Tests
{
    public class HttpClientHelperTests
    {
        #region 构造函数测试

        [Fact]
        public void Constructor_WithLogger_CreatesInstance()
        {
            // Arrange
            var loggerMock = new Mock<ILogger<HttpClientHelper>>();

            // Act
            var httpClientHelper = new HttpClientHelper(loggerMock.Object);

            // Assert
            Assert.NotNull(httpClientHelper);
        }

        [Fact]
        public void Constructor_WithoutLogger_CreatesInstance()
        {
            // Act
            var httpClientHelper = new HttpClientHelper();

            // Assert
            Assert.NotNull(httpClientHelper);
        }

        [Fact]
        public void Constructor_WithHttpClient_CreatesInstance()
        {
            // Arrange
            var httpClient = new HttpClient();

            // Act
            var httpClientHelper = new HttpClientHelper(httpClient);

            // Assert
            Assert.NotNull(httpClientHelper);
        }

        [Fact]
        public void Constructor_WithNullHttpClient_ThrowsArgumentNullException()
        {
            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => new HttpClientHelper((HttpClient)null));
        }

        #endregion

        #region 基础HTTP请求方法测试

        [Fact]
        public async Task GetAsync_ValidUrl_ReturnsHttpResponseMessage()
        {
            // Arrange
            var mockHandler = new Mock<HttpMessageHandler>();
            var expectedResponse = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent("Success")
            };

            mockHandler.Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(expectedResponse);

            var httpClient = new HttpClient(mockHandler.Object);
            var httpClientHelper = new HttpClientHelper(httpClient);

            // Act
            var response = await httpClientHelper.GetAsync("https://example.com");

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Equal("Success", await response.Content.ReadAsStringAsync());
        }

        [Fact]
        public async Task GetStringAsync_ValidUrl_ReturnsStringContent()
        {
            // Arrange
            var mockHandler = new Mock<HttpMessageHandler>();
            var expectedContent = "Test response content";
            var expectedResponse = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(expectedContent)
            };

            mockHandler.Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(expectedResponse);

            var httpClient = new HttpClient(mockHandler.Object);
            var httpClientHelper = new HttpClientHelper(httpClient);

            // Act
            var content = await httpClientHelper.GetStringAsync("https://example.com");

            // Assert
            Assert.Equal(expectedContent, content);
        }

        [Fact]
        public async Task GetStringAsync_NonSuccessStatusCode_ThrowsHttpRequestException()
        {
            // Arrange
            var mockHandler = new Mock<HttpMessageHandler>();
            var expectedResponse = new HttpResponseMessage(HttpStatusCode.NotFound);

            mockHandler.Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(expectedResponse);

            var httpClient = new HttpClient(mockHandler.Object);
            var httpClientHelper = new HttpClientHelper(httpClient);

            // Act & Assert
            await Assert.ThrowsAsync<HttpRequestException>(() => httpClientHelper.GetStringAsync("https://example.com"));
        }

        [Fact]
        public async Task PostJsonAsync_ValidData_ReturnsHttpResponseMessage()
        {
            // Arrange
            var mockHandler = new Mock<HttpMessageHandler>();
            var expectedResponse = new HttpResponseMessage(HttpStatusCode.Created);
            var testData = new TestObject { Id = 1, Name = "Test" };

            mockHandler.Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(expectedResponse)
                .Callback<HttpRequestMessage, CancellationToken>((request, ct) =>
                {
                    Assert.Equal(HttpMethod.Post, request.Method);
                    Assert.Equal("https://example.com/", request.RequestUri.ToString());
                });

            var httpClient = new HttpClient(mockHandler.Object);
            var httpClientHelper = new HttpClientHelper(httpClient);

            // Act
            var response = await httpClientHelper.PostJsonAsync("https://example.com", testData);

            // Assert
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        }

        [Fact]
        public async Task PutJsonAsync_ValidData_ReturnsHttpResponseMessage()
        {
            // Arrange
            var mockHandler = new Mock<HttpMessageHandler>();
            var expectedResponse = new HttpResponseMessage(HttpStatusCode.OK);
            var testData = new TestObject { Id = 1, Name = "Test" };

            mockHandler.Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(expectedResponse)
                .Callback<HttpRequestMessage, CancellationToken>((request, ct) =>
                {
                    Assert.Equal(HttpMethod.Put, request.Method);
                    Assert.Equal("https://example.com/", request.RequestUri.ToString());
                });

            var httpClient = new HttpClient(mockHandler.Object);
            var httpClientHelper = new HttpClientHelper(httpClient);

            // Act
            var response = await httpClientHelper.PutJsonAsync("https://example.com", testData);

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task DeleteAsync_ValidUrl_ReturnsHttpResponseMessage()
        {
            // Arrange
            var mockHandler = new Mock<HttpMessageHandler>();
            var expectedResponse = new HttpResponseMessage(HttpStatusCode.NoContent);

            mockHandler.Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(expectedResponse)
                .Callback<HttpRequestMessage, CancellationToken>((request, ct) =>
                {
                    Assert.Equal(HttpMethod.Delete, request.Method);
                    Assert.Equal("https://example.com/", request.RequestUri.ToString());
                });

            var httpClient = new HttpClient(mockHandler.Object);
            var httpClientHelper = new HttpClientHelper(httpClient);

            // Act
            var response = await httpClientHelper.DeleteAsync("https://example.com");

            // Assert
            Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
        }

        #endregion

        #region 高级HTTP请求方法测试

        [Fact]
        public async Task GetWithRetryAsync_SuccessOnFirstTry_ReturnsResponse()
        {
            // Arrange
            var mockHandler = new Mock<HttpMessageHandler>();
            var expectedResponse = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent("Success")
            };

            mockHandler.Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(expectedResponse);

            var httpClient = new HttpClient(mockHandler.Object);
            var httpClientHelper = new HttpClientHelper(httpClient);

            // Act
            var response = await httpClientHelper.GetWithRetryAsync("https://example.com", maxRetries: 2);

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task GetWithRetryAsync_SuccessAfterRetry_ReturnsResponse()
        {
            // Arrange
            var mockHandler = new Mock<HttpMessageHandler>();
            var expectedResponse = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent("Success")
            };

            var callCount = 0;
            mockHandler.Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(() =>
                {
                    callCount++;
                    if (callCount == 1)
                    {
                        return new HttpResponseMessage(HttpStatusCode.RequestTimeout);
                    }
                    return expectedResponse;
                });

            var httpClient = new HttpClient(mockHandler.Object);
            var httpClientHelper = new HttpClientHelper(httpClient);

            // Act
            var response = await httpClientHelper.GetWithRetryAsync("https://example.com", maxRetries: 2);

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Equal(2, callCount);
        }

        [Fact]
        public async Task GetWithAuthAsync_ValidToken_AddsAuthorizationHeader()
        {
            // Arrange
            var mockHandler = new Mock<HttpMessageHandler>();
            var expectedResponse = new HttpResponseMessage(HttpStatusCode.OK);
            var token = "test-token";

            mockHandler.Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(expectedResponse)
                .Callback<HttpRequestMessage, CancellationToken>((request, ct) =>
                {
                    Assert.True(request.Headers.Contains("Authorization"));
                    Assert.Contains($"Bearer {token}", request.Headers.GetValues("Authorization"));
                });

            var httpClient = new HttpClient(mockHandler.Object);
            var httpClientHelper = new HttpClientHelper(httpClient);

            // Act
            var response = await httpClientHelper.GetWithAuthAsync("https://example.com", token);

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task GetWithBasicAuthAsync_ValidCredentials_AddsAuthorizationHeader()
        {
            // Arrange
            var mockHandler = new Mock<HttpMessageHandler>();
            var expectedResponse = new HttpResponseMessage(HttpStatusCode.OK);
            var username = "testuser";
            var password = "testpass";

            mockHandler.Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(expectedResponse)
                .Callback<HttpRequestMessage, CancellationToken>((request, ct) =>
                {
                    Assert.True(request.Headers.Contains("Authorization"));
                    // Basic base64(username:password)
                    var expectedAuth = "Basic " + Convert.ToBase64String(Encoding.ASCII.GetBytes($"{username}:{password}"));
                    Assert.Contains(expectedAuth, request.Headers.GetValues("Authorization"));
                });

            var httpClient = new HttpClient(mockHandler.Object);
            var httpClientHelper = new HttpClientHelper(httpClient);

            // Act
            var response = await httpClientHelper.GetWithBasicAuthAsync("https://example.com", username, password);

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task GetWithTimeoutAsync_ValidTimeout_AppliesCancellationToken()
        {
            // Arrange
            var mockHandler = new Mock<HttpMessageHandler>();
            var expectedResponse = new HttpResponseMessage(HttpStatusCode.OK);

            mockHandler.Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(expectedResponse);

            var httpClient = new HttpClient(mockHandler.Object);
            var httpClientHelper = new HttpClientHelper(httpClient);

            // Act
            var response = await httpClientHelper.GetWithTimeoutAsync("https://example.com", TimeSpan.FromMilliseconds(5000));

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        #endregion

        #region 工具方法测试

        [Fact]
        public void SerializeToJson_ValidObject_ReturnsJsonString()
        {
            // Arrange
            var httpClientHelper = new HttpClientHelper();
            var testData = new TestObject { Id = 1, Name = "Test" };

            // Act
            var json = httpClientHelper.SerializeToJson(testData);

            // Assert
            Assert.Contains("\"id\":1", json);
            Assert.Contains("\"name\":\"Test\"", json);
        }

        [Fact]
        public void DeserializeFromJson_ValidJson_ReturnsObject()
        {
            // Arrange
            var httpClientHelper = new HttpClientHelper();
            var json = "{\"id\":1,\"name\":\"Test\"}";

            // Act
            var obj = httpClientHelper.DeserializeFromJson<TestObject>(json);

            // Assert
            Assert.NotNull(obj);
            Assert.Equal(1, obj.Id);
            Assert.Equal("Test", obj.Name);
        }

        [Fact]
        public void SetDefaultHeaders_ValidHeaders_AddsHeaders()
        {
            // Arrange
            var mockHandler = new Mock<HttpMessageHandler>();
            mockHandler.Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage(HttpStatusCode.OK));

            var httpClient = new HttpClient(mockHandler.Object);
            var httpClientHelper = new HttpClientHelper(httpClient);
            var headers = new Dictionary<string, string>
            {
                { "X-Custom-Header", "CustomValue" }
            };

            // Act
            httpClientHelper.SetDefaultHeaders(headers);

            // Assert
            Assert.True(httpClient.DefaultRequestHeaders.Contains("X-Custom-Header"));
        }

        [Fact]
        public void SetDefaultTimeout_ValidTimeout_SetsTimeout()
        {
            // Arrange
            var httpClientHelper = new HttpClientHelper();
            var newTimeout = TimeSpan.FromMinutes(1);

            // Act
            httpClientHelper.SetDefaultTimeout(newTimeout);

            // Assert
            var httpClient = httpClientHelper.GetHttpClient();
            Assert.Equal(newTimeout, httpClient.Timeout);
        }

        [Fact]
        public void GetHttpClient_ReturnsHttpClientInstance()
        {
            // Arrange
            var httpClientHelper = new HttpClientHelper();

            // Act
            var httpClient = httpClientHelper.GetHttpClient();

            // Assert
            Assert.NotNull(httpClient);
        }

        #endregion

        #region 批量请求处理测试

        [Fact]
        public async Task SendBatchAsync_MultipleRequests_ReturnsResponses()
        {
            // Arrange
            var mockHandler = new Mock<HttpMessageHandler>();
            var expectedResponse = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent("Success")
            };

            var callCount = 0;
            mockHandler.Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(expectedResponse)
                .Callback(() => callCount++);

            var httpClient = new HttpClient(mockHandler.Object);
            var httpClientHelper = new HttpClientHelper(httpClient);

            var requests = new List<BatchRequest>
            {
                new BatchRequest
                {
                    Method = HttpMethod.Get,
                    Url = "https://example.com/test1"
                },
                new BatchRequest
                {
                    Method = HttpMethod.Post,
                    Url = "https://example.com/test2",
                    Content = new StringContent("test data")
                }
            };

            // Act
            var responses = await httpClientHelper.SendBatchAsync(requests);

            // Assert
            Assert.Equal(2, responses.Count);
            Assert.Equal(2, callCount);
            Assert.All(responses, r => Assert.True(r.Success));
        }

        [Fact]
        public async Task SendSequentialAsync_MultipleRequests_ReturnsResponses()
        {
            // Arrange
            var mockHandler = new Mock<HttpMessageHandler>();
            var expectedResponse = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent("Success")
            };

            var callCount = 0;
            mockHandler.Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(expectedResponse)
                .Callback(() => callCount++);

            var httpClient = new HttpClient(mockHandler.Object);
            var httpClientHelper = new HttpClientHelper(httpClient);

            var requests = new List<BatchRequest>
            {
                new BatchRequest
                {
                    Method = HttpMethod.Get,
                    Url = "https://example.com/test1"
                },
                new BatchRequest
                {
                    Method = HttpMethod.Post,
                    Url = "https://example.com/test2",
                    Content = new StringContent("test data")
                }
            };

            // Act
            var responses = await httpClientHelper.SendSequentialAsync(requests);

            // Assert
            Assert.Equal(2, responses.Count);
            Assert.Equal(2, callCount);
            Assert.All(responses, r => Assert.True(r.Success));
        }

        #endregion

        #region 文件操作测试

        [Fact]
        public async Task DownloadFileAsync_ValidUrl_DownloadsFile()
        {
            // Arrange
            var mockHandler = new Mock<HttpMessageHandler>();
            var fileContent = "Test file content";
            var expectedResponse = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(fileContent)
            };

            mockHandler.Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(expectedResponse);

            var httpClient = new HttpClient(mockHandler.Object);
            var httpClientHelper = new HttpClientHelper(httpClient);
            var tempPath = Path.GetTempFileName();

            try
            {
                // Act
                var result = await httpClientHelper.DownloadFileAsync("https://example.com/file.txt", tempPath);

                // Assert
                Assert.True(result.Success);
                Assert.True(File.Exists(tempPath));
                Assert.Equal(fileContent.Length, result.FileSize);

                var downloadedContent = await File.ReadAllTextAsync(tempPath);
                Assert.Equal(fileContent, downloadedContent);
            }
            finally
            {
                // Cleanup
                if (File.Exists(tempPath))
                {
                    File.Delete(tempPath);
                }
            }
        }

        [Fact]
        public async Task UploadFileAsync_ValidFile_UploadsFile()
        {
            // Arrange
            var mockHandler = new Mock<HttpMessageHandler>();
            var expectedResponse = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent("{\"status\":\"success\"}")
            };

            mockHandler.Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(expectedResponse);

            var httpClient = new HttpClient(mockHandler.Object);
            var httpClientHelper = new HttpClientHelper(httpClient);

            // Create a temporary file to upload
            var tempPath = Path.GetTempFileName();
            await File.WriteAllTextAsync(tempPath, "Test file content");

            try
            {
                // Act
                var result = await httpClientHelper.UploadFileAsync("https://example.com/upload", tempPath);

                // Assert
                Assert.True(result.Success);
                Assert.NotNull(result.ResponseContent);
            }
            finally
            {
                // Cleanup
                if (File.Exists(tempPath))
                {
                    File.Delete(tempPath);
                }
            }
        }

        #endregion

        #region 数据模型测试

        [Fact]
        public void DownloadProgress_Properties_WorkCorrectly()
        {
            // Arrange
            var progress = new DownloadProgress
            {
                TotalBytes = 1000,
                DownloadedBytes = 500,
                ProgressPercentage = 50
            };

            // Assert
            Assert.Equal(1000, progress.TotalBytes);
            Assert.Equal(500, progress.DownloadedBytes);
            Assert.Equal(50, progress.ProgressPercentage);
        }

        [Fact]
        public void DownloadResult_Properties_WorkCorrectly()
        {
            // Arrange
            var result = new DownloadResult
            {
                Success = true,
                FilePath = "/path/to/file.txt",
                FileSize = 1024,
                ErrorMessage = null
            };

            // Assert
            Assert.True(result.Success);
            Assert.Equal("/path/to/file.txt", result.FilePath);
            Assert.Equal(1024, result.FileSize);
            Assert.Null(result.ErrorMessage);
        }

        [Fact]
        public void UploadResult_Properties_WorkCorrectly()
        {
            // Arrange
            var result = new UploadResult
            {
                Success = false,
                ResponseContent = null,
                ErrorMessage = "Upload failed"
            };

            // Assert
            Assert.False(result.Success);
            Assert.Null(result.ResponseContent);
            Assert.Equal("Upload failed", result.ErrorMessage);
        }

        [Fact]
        public void BatchRequest_Properties_WorkCorrectly()
        {
            // Arrange
            var request = new BatchRequest
            {
                Method = HttpMethod.Get,
                Url = "https://example.com",
                Content = new StringContent("test"),
                Headers = new Dictionary<string, string> { { "X-Test", "value" } }
            };

            // Assert
            Assert.Equal(HttpMethod.Get, request.Method);
            Assert.Equal("https://example.com", request.Url);
            Assert.NotNull(request.Content);
            Assert.Single(request.Headers);
            Assert.Equal("value", request.Headers["X-Test"]);
        }

        [Fact]
        public void BatchResponse_Properties_WorkCorrectly()
        {
            // Arrange
            var response = new BatchResponse
            {
                Request = new BatchRequest { Url = "https://example.com" },
                Response = new HttpResponseMessage(HttpStatusCode.OK),
                Content = "response content",
                Success = true,
                ErrorMessage = null
            };

            // Assert
            Assert.NotNull(response.Request);
            Assert.NotNull(response.Response);
            Assert.Equal("response content", response.Content);
            Assert.True(response.Success);
            Assert.Null(response.ErrorMessage);
        }

        #endregion

        #region 统计信息测试

        [Fact]
        public void GetStatistics_ReturnsStatisticsObject()
        {
            // Arrange
            var httpClientHelper = new HttpClientHelper();

            // Act
            var statistics = httpClientHelper.GetStatistics();

            // Assert
            Assert.NotNull(statistics);
            Assert.Equal(TimeSpan.FromSeconds(30), statistics.DefaultTimeout);
            Assert.True(statistics.SupportsAutomaticDecompression);
            Assert.True(statistics.UseCookies);
        }

        #endregion
    }

    #region 辅助类

    public class TestObject
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    #endregion
}