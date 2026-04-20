using Microsoft.Extensions.Logging;
using Moq;
using Moq.Protected;
using System.Net;
using System.Text;
using System.Text.Json;
using Xunit;

namespace Chet.Utils.Helpers.Tests
{
    public class HttpClientHelperTests
    {
        #region 构造函数测试

        [Fact]
        public void Constructor_WithLogger_CreatesInstance()
        {
            var loggerMock = new Mock<ILogger<HttpClientHelper>>();
            var httpClientHelper = new HttpClientHelper(loggerMock.Object);
            Assert.NotNull(httpClientHelper);
        }

        [Fact]
        public void Constructor_WithoutLogger_CreatesInstance()
        {
            var httpClientHelper = new HttpClientHelper();
            Assert.NotNull(httpClientHelper);
        }

        [Fact]
        public void Constructor_WithHttpClient_CreatesInstance()
        {
            var httpClient = new HttpClient();
            var httpClientHelper = new HttpClientHelper(httpClient);
            Assert.NotNull(httpClientHelper);
        }

        [Fact]
        public void Constructor_WithNullHttpClient_ThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => new HttpClientHelper((HttpClient)null));
        }

        [Fact]
        public void Constructor_WithOptions_CreatesInstance()
        {
            var options = new HttpClientOptions
            {
                Timeout = TimeSpan.FromSeconds(60),
                MaxConnectionsPerServer = 50
            };
            var httpClientHelper = new HttpClientHelper(options);
            Assert.NotNull(httpClientHelper);
        }

        [Fact]
        public void Constructor_WithOptionsAndHeaders_CreatesInstance()
        {
            var options = new HttpClientOptions
            {
                DefaultHeaders = new Dictionary<string, string>
                {
                    { "X-Custom-Header", "TestValue" }
                }
            };
            var httpClientHelper = new HttpClientHelper(options);
            Assert.NotNull(httpClientHelper);
        }

        #endregion

        #region URL验证测试

        [Fact]
        public async Task GetAsync_InvalidUrl_ThrowsArgumentException()
        {
            var httpClientHelper = new HttpClientHelper();
            await Assert.ThrowsAsync<ArgumentException>(() => httpClientHelper.GetAsync("invalid-url"));
        }

        [Fact]
        public async Task GetAsync_EmptyUrl_ThrowsArgumentNullException()
        {
            var httpClientHelper = new HttpClientHelper();
            await Assert.ThrowsAsync<ArgumentNullException>(() => httpClientHelper.GetAsync(""));
        }

        [Fact]
        public async Task GetAsync_NullUrl_ThrowsArgumentNullException()
        {
            var httpClientHelper = new HttpClientHelper();
            await Assert.ThrowsAsync<ArgumentNullException>(() => httpClientHelper.GetAsync(null));
        }

        [Fact]
        public async Task GetAsync_FtpUrl_ThrowsArgumentException()
        {
            var httpClientHelper = new HttpClientHelper();
            await Assert.ThrowsAsync<ArgumentException>(() => httpClientHelper.GetAsync("ftp://example.com/file"));
        }

        #endregion

        #region 基础HTTP请求方法测试

        [Fact]
        public async Task GetAsync_ValidUrl_ReturnsHttpResponseMessage()
        {
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

            var response = await httpClientHelper.GetAsync("https://example.com");

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Equal("Success", await response.Content.ReadAsStringAsync());
        }

        [Fact]
        public async Task GetAsyncT_ValidUrl_ReturnsDeserializedObject()
        {
            var mockHandler = new Mock<HttpMessageHandler>();
            var testData = new TestObject { Id = 1, Name = "Test" };
            var json = JsonSerializer.Serialize(testData);
            var expectedResponse = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(json, Encoding.UTF8, "application/json")
            };

            mockHandler.Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(expectedResponse);

            var httpClient = new HttpClient(mockHandler.Object);
            var httpClientHelper = new HttpClientHelper(httpClient);

            var result = await httpClientHelper.GetAsync<TestObject>("https://example.com");

            Assert.NotNull(result);
            Assert.Equal(1, result.Id);
            Assert.Equal("Test", result.Name);
        }

        [Fact]
        public async Task GetStringAsync_ValidUrl_ReturnsStringContent()
        {
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

            var content = await httpClientHelper.GetStringAsync("https://example.com");

            Assert.Equal(expectedContent, content);
        }

        [Fact]
        public async Task GetStringAsync_NonSuccessStatusCode_ThrowsHttpRequestException()
        {
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

            await Assert.ThrowsAsync<HttpRequestException>(() => httpClientHelper.GetStringAsync("https://example.com"));
        }

        [Fact]
        public async Task GetByteArrayAsync_ValidUrl_ReturnsByteArray()
        {
            var mockHandler = new Mock<HttpMessageHandler>();
            var expectedBytes = new byte[] { 1, 2, 3, 4, 5 };
            var expectedResponse = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new ByteArrayContent(expectedBytes)
            };

            mockHandler.Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(expectedResponse);

            var httpClient = new HttpClient(mockHandler.Object);
            var httpClientHelper = new HttpClientHelper(httpClient);

            var result = await httpClientHelper.GetByteArrayAsync("https://example.com");

            Assert.Equal(expectedBytes, result);
        }

        [Fact]
        public async Task PostAsync_ValidUrl_ReturnsHttpResponseMessage()
        {
            var mockHandler = new Mock<HttpMessageHandler>();
            var expectedResponse = new HttpResponseMessage(HttpStatusCode.Created);
            var content = new StringContent("test data");

            mockHandler.Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(expectedResponse)
                .Callback<HttpRequestMessage, CancellationToken>((request, ct) =>
                {
                    Assert.Equal(HttpMethod.Post, request.Method);
                });

            var httpClient = new HttpClient(mockHandler.Object);
            var httpClientHelper = new HttpClientHelper(httpClient);

            var response = await httpClientHelper.PostAsync("https://example.com", content);

            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        }

        [Fact]
        public async Task PostJsonAsync_ValidData_ReturnsHttpResponseMessage()
        {
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
                });

            var httpClient = new HttpClient(mockHandler.Object);
            var httpClientHelper = new HttpClientHelper(httpClient);

            var response = await httpClientHelper.PostJsonAsync("https://example.com", testData);

            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        }

        [Fact]
        public async Task PostJsonAsyncT_ValidData_ReturnsDeserializedObject()
        {
            var mockHandler = new Mock<HttpMessageHandler>();
            var testData = new TestObject { Id = 1, Name = "Test" };
            var responseData = new TestObject { Id = 2, Name = "Response" };
            var responseJson = JsonSerializer.Serialize(responseData);
            var expectedResponse = new HttpResponseMessage(HttpStatusCode.Created)
            {
                Content = new StringContent(responseJson, Encoding.UTF8, "application/json")
            };

            mockHandler.Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(expectedResponse);

            var httpClient = new HttpClient(mockHandler.Object);
            var httpClientHelper = new HttpClientHelper(httpClient);

            var result = await httpClientHelper.PostJsonAsync<TestObject, TestObject>("https://example.com", testData);

            Assert.NotNull(result);
            Assert.Equal(2, result.Id);
            Assert.Equal("Response", result.Name);
        }

        [Fact]
        public async Task PostFormAsync_ValidFormData_ReturnsHttpResponseMessage()
        {
            var mockHandler = new Mock<HttpMessageHandler>();
            var expectedResponse = new HttpResponseMessage(HttpStatusCode.OK);
            var formData = new Dictionary<string, string>
            {
                { "username", "testuser" },
                { "password", "testpass" }
            };

            mockHandler.Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(expectedResponse);

            var httpClient = new HttpClient(mockHandler.Object);
            var httpClientHelper = new HttpClientHelper(httpClient);

            var response = await httpClientHelper.PostFormAsync("https://example.com", formData);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task PostFormAsync_NullFormData_ThrowsArgumentNullException()
        {
            var httpClientHelper = new HttpClientHelper();
            await Assert.ThrowsAsync<ArgumentNullException>(() => httpClientHelper.PostFormAsync("https://example.com", null));
        }

        [Fact]
        public async Task PutJsonAsync_ValidData_ReturnsHttpResponseMessage()
        {
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
                });

            var httpClient = new HttpClient(mockHandler.Object);
            var httpClientHelper = new HttpClientHelper(httpClient);

            var response = await httpClientHelper.PutJsonAsync("https://example.com", testData);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task DeleteAsync_ValidUrl_ReturnsHttpResponseMessage()
        {
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
                });

            var httpClient = new HttpClient(mockHandler.Object);
            var httpClientHelper = new HttpClientHelper(httpClient);

            var response = await httpClientHelper.DeleteAsync("https://example.com");

            Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
        }

        [Fact]
        public async Task DeleteAsyncT_ValidUrl_ReturnsDeserializedObject()
        {
            var mockHandler = new Mock<HttpMessageHandler>();
            var responseData = new TestObject { Id = 1, Name = "Deleted" };
            var responseJson = JsonSerializer.Serialize(responseData);
            var expectedResponse = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(responseJson, Encoding.UTF8, "application/json")
            };

            mockHandler.Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(expectedResponse);

            var httpClient = new HttpClient(mockHandler.Object);
            var httpClientHelper = new HttpClientHelper(httpClient);

            var result = await httpClientHelper.DeleteAsync<TestObject>("https://example.com");

            Assert.NotNull(result);
            Assert.Equal(1, result.Id);
        }

        #endregion

        #region PATCH/HEAD/OPTIONS方法测试

        [Fact]
        public async Task PatchAsync_ValidUrl_ReturnsHttpResponseMessage()
        {
            var mockHandler = new Mock<HttpMessageHandler>();
            var expectedResponse = new HttpResponseMessage(HttpStatusCode.OK);
            var content = new StringContent("{\"name\":\"updated\"}", Encoding.UTF8, "application/json");

            mockHandler.Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(expectedResponse)
                .Callback<HttpRequestMessage, CancellationToken>((request, ct) =>
                {
                    Assert.Equal("PATCH", request.Method.Method);
                });

            var httpClient = new HttpClient(mockHandler.Object);
            var httpClientHelper = new HttpClientHelper(httpClient);

            var response = await httpClientHelper.PatchAsync("https://example.com", content);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task HeadAsync_ValidUrl_ReturnsHttpResponseMessage()
        {
            var mockHandler = new Mock<HttpMessageHandler>();
            var expectedResponse = new HttpResponseMessage(HttpStatusCode.OK);

            mockHandler.Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(expectedResponse)
                .Callback<HttpRequestMessage, CancellationToken>((request, ct) =>
                {
                    Assert.Equal(HttpMethod.Head, request.Method);
                });

            var httpClient = new HttpClient(mockHandler.Object);
            var httpClientHelper = new HttpClientHelper(httpClient);

            var response = await httpClientHelper.HeadAsync("https://example.com");

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task OptionsAsync_ValidUrl_ReturnsHttpResponseMessage()
        {
            var mockHandler = new Mock<HttpMessageHandler>();
            var expectedResponse = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent("")
            };
            expectedResponse.Content.Headers.Allow.Add("GET");
            expectedResponse.Content.Headers.Allow.Add("POST");
            expectedResponse.Content.Headers.Allow.Add("PUT");
            expectedResponse.Content.Headers.Allow.Add("DELETE");

            mockHandler.Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(expectedResponse)
                .Callback<HttpRequestMessage, CancellationToken>((request, ct) =>
                {
                    Assert.Equal(HttpMethod.Options, request.Method);
                });

            var httpClient = new HttpClient(mockHandler.Object);
            var httpClientHelper = new HttpClientHelper(httpClient);

            var response = await httpClientHelper.OptionsAsync("https://example.com");

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.True(response.Content.Headers.Allow.Count > 0);
        }

        #endregion

        #region 高级HTTP请求方法测试

        [Fact]
        public async Task GetWithRetryAsync_SuccessOnFirstTry_ReturnsResponse()
        {
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

            var response = await httpClientHelper.GetWithRetryAsync("https://example.com", maxRetries: 2);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task GetWithRetryAsync_SuccessAfterRetry_ReturnsResponse()
        {
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

            var response = await httpClientHelper.GetWithRetryAsync("https://example.com", maxRetries: 2, retryDelay: TimeSpan.FromMilliseconds(10));

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Equal(2, callCount);
        }

        [Fact]
        public async Task GetWithRetryAsyncT_SuccessAfterRetry_ReturnsDeserializedObject()
        {
            var mockHandler = new Mock<HttpMessageHandler>();
            var responseData = new TestObject { Id = 1, Name = "Test" };
            var responseJson = JsonSerializer.Serialize(responseData);
            var expectedResponse = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(responseJson, Encoding.UTF8, "application/json")
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
                        return new HttpResponseMessage(HttpStatusCode.InternalServerError);
                    }
                    return expectedResponse;
                });

            var httpClient = new HttpClient(mockHandler.Object);
            var httpClientHelper = new HttpClientHelper(httpClient);

            var result = await httpClientHelper.GetWithRetryAsync<TestObject>("https://example.com", maxRetries: 2, retryDelay: TimeSpan.FromMilliseconds(10));

            Assert.NotNull(result);
            Assert.Equal(1, result.Id);
            Assert.Equal(2, callCount);
        }

        [Fact]
        public async Task GetWithAuthAsync_ValidToken_AddsAuthorizationHeader()
        {
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

            var response = await httpClientHelper.GetWithAuthAsync("https://example.com", token);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task GetWithAuthAsync_NullToken_ThrowsArgumentNullException()
        {
            var httpClientHelper = new HttpClientHelper();
            await Assert.ThrowsAsync<ArgumentNullException>(() => httpClientHelper.GetWithAuthAsync("https://example.com", null));
        }

        [Fact]
        public async Task GetWithBasicAuthAsync_ValidCredentials_AddsAuthorizationHeader()
        {
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
                    var expectedAuth = "Basic " + Convert.ToBase64String(Encoding.UTF8.GetBytes($"{username}:{password}"));
                    Assert.Contains(expectedAuth, request.Headers.GetValues("Authorization"));
                });

            var httpClient = new HttpClient(mockHandler.Object);
            var httpClientHelper = new HttpClientHelper(httpClient);

            var response = await httpClientHelper.GetWithBasicAuthAsync("https://example.com", username, password);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task GetWithApiKeyAsync_ValidApiKey_AddsHeader()
        {
            var mockHandler = new Mock<HttpMessageHandler>();
            var expectedResponse = new HttpResponseMessage(HttpStatusCode.OK);
            var apiKey = "test-api-key";

            mockHandler.Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(expectedResponse)
                .Callback<HttpRequestMessage, CancellationToken>((request, ct) =>
                {
                    Assert.True(request.Headers.Contains("X-API-Key"));
                    Assert.Equal(apiKey, request.Headers.GetValues("X-API-Key").First());
                });

            var httpClient = new HttpClient(mockHandler.Object);
            var httpClientHelper = new HttpClientHelper(httpClient);

            var response = await httpClientHelper.GetWithApiKeyAsync("https://example.com", apiKey);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task GetWithApiKeyAsync_CustomHeaderName_AddsCorrectHeader()
        {
            var mockHandler = new Mock<HttpMessageHandler>();
            var expectedResponse = new HttpResponseMessage(HttpStatusCode.OK);
            var apiKey = "test-api-key";
            var headerName = "X-Custom-Key";

            mockHandler.Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(expectedResponse)
                .Callback<HttpRequestMessage, CancellationToken>((request, ct) =>
                {
                    Assert.True(request.Headers.Contains(headerName));
                    Assert.Equal(apiKey, request.Headers.GetValues(headerName).First());
                });

            var httpClient = new HttpClient(mockHandler.Object);
            var httpClientHelper = new HttpClientHelper(httpClient);

            var response = await httpClientHelper.GetWithApiKeyAsync("https://example.com", apiKey, headerName);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task GetWithTimeoutAsync_ValidTimeout_AppliesCancellationToken()
        {
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

            var response = await httpClientHelper.GetWithTimeoutAsync("https://example.com", TimeSpan.FromMilliseconds(5000));

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        #endregion

        #region 工具方法测试

        [Fact]
        public void SerializeToJson_ValidObject_ReturnsJsonString()
        {
            var httpClientHelper = new HttpClientHelper();
            var testData = new TestObject { Id = 1, Name = "Test" };

            var json = httpClientHelper.SerializeToJson(testData);

            Assert.Contains("\"id\":1", json);
            Assert.Contains("\"name\":\"Test\"", json);
        }

        [Fact]
        public void DeserializeFromJson_ValidJson_ReturnsObject()
        {
            var httpClientHelper = new HttpClientHelper();
            var json = "{\"id\":1,\"name\":\"Test\"}";

            var obj = httpClientHelper.DeserializeFromJson<TestObject>(json);

            Assert.NotNull(obj);
            Assert.Equal(1, obj.Id);
            Assert.Equal("Test", obj.Name);
        }

        [Fact]
        public void DeserializeFromJson_EmptyJson_ThrowsArgumentNullException()
        {
            var httpClientHelper = new HttpClientHelper();
            Assert.Throws<ArgumentNullException>(() => httpClientHelper.DeserializeFromJson<TestObject>(""));
        }

        [Fact]
        public void SetDefaultHeaders_ValidHeaders_AddsHeaders()
        {
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

            httpClientHelper.SetDefaultHeaders(headers);

            Assert.True(httpClient.DefaultRequestHeaders.Contains("X-Custom-Header"));
        }

        [Fact]
        public void SetDefaultHeaders_NullHeaders_ThrowsArgumentNullException()
        {
            var httpClientHelper = new HttpClientHelper();
            Assert.Throws<ArgumentNullException>(() => httpClientHelper.SetDefaultHeaders(null));
        }

        [Fact]
        public void AddDefaultHeader_ValidHeader_AddsHeader()
        {
            var httpClientHelper = new HttpClientHelper();

            httpClientHelper.AddDefaultHeader("X-Test-Header", "TestValue");

            var httpClient = httpClientHelper.GetHttpClient();
            Assert.True(httpClient.DefaultRequestHeaders.Contains("X-Test-Header"));
        }

        [Fact]
        public void RemoveDefaultHeader_ExistingHeader_RemovesHeader()
        {
            var httpClientHelper = new HttpClientHelper();
            httpClientHelper.AddDefaultHeader("X-Test-Header", "TestValue");

            httpClientHelper.RemoveDefaultHeader("X-Test-Header");

            var httpClient = httpClientHelper.GetHttpClient();
            Assert.False(httpClient.DefaultRequestHeaders.Contains("X-Test-Header"));
        }

        [Fact]
        public void SetDefaultTimeout_ValidTimeout_SetsTimeout()
        {
            var httpClientHelper = new HttpClientHelper();
            var newTimeout = TimeSpan.FromMinutes(1);

            httpClientHelper.SetDefaultTimeout(newTimeout);

            var httpClient = httpClientHelper.GetHttpClient();
            Assert.Equal(newTimeout, httpClient.Timeout);
        }

        [Fact]
        public void SetDefaultTimeout_ZeroTimeout_ThrowsArgumentOutOfRangeException()
        {
            var httpClientHelper = new HttpClientHelper();
            Assert.Throws<ArgumentOutOfRangeException>(() => httpClientHelper.SetDefaultTimeout(TimeSpan.Zero));
        }

        [Fact]
        public void GetHttpClient_ReturnsHttpClientInstance()
        {
            var httpClientHelper = new HttpClientHelper();
            var httpClient = httpClientHelper.GetHttpClient();
            Assert.NotNull(httpClient);
        }

        [Fact]
        public void ConfigureJsonOptions_ValidAction_UpdatesOptions()
        {
            var httpClientHelper = new HttpClientHelper();
            var testData = new TestObject { Id = 1, Name = "Test" };

            httpClientHelper.ConfigureJsonOptions(options =>
            {
                options.WriteIndented = true;
            });

            var json = httpClientHelper.SerializeToJson(testData);
            Assert.Contains("\n", json);
        }

        #endregion

        #region 拦截器测试

        [Fact]
        public async Task AddRequestInterceptor_InterceptorIsCalled()
        {
            var mockHandler = new Mock<HttpMessageHandler>();
            var expectedResponse = new HttpResponseMessage(HttpStatusCode.OK);
            var interceptorCalled = false;

            mockHandler.Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(expectedResponse);

            var httpClient = new HttpClient(mockHandler.Object);
            var httpClientHelper = new HttpClientHelper(httpClient);

            httpClientHelper.AddRequestInterceptor(async request =>
            {
                interceptorCalled = true;
                request.Headers.Add("X-Interceptor-Header", "Added");
                await Task.CompletedTask;
            });

            await httpClientHelper.GetAsync("https://example.com");

            Assert.True(interceptorCalled);
        }

        [Fact]
        public async Task AddResponseInterceptor_InterceptorIsCalled()
        {
            var mockHandler = new Mock<HttpMessageHandler>();
            var expectedResponse = new HttpResponseMessage(HttpStatusCode.OK);
            var interceptorCalled = false;

            mockHandler.Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(expectedResponse);

            var httpClient = new HttpClient(mockHandler.Object);
            var httpClientHelper = new HttpClientHelper(httpClient);

            httpClientHelper.AddResponseInterceptor(async response =>
            {
                interceptorCalled = true;
                await Task.CompletedTask;
            });

            await httpClientHelper.GetAsync("https://example.com");

            Assert.True(interceptorCalled);
        }

        [Fact]
        public void ClearInterceptors_RemovesAllInterceptors()
        {
            var httpClientHelper = new HttpClientHelper();

            httpClientHelper.AddRequestInterceptor(async _ => await Task.CompletedTask);
            httpClientHelper.AddResponseInterceptor(async _ => await Task.CompletedTask);

            httpClientHelper.ClearInterceptors();

            var httpClient = httpClientHelper.GetHttpClient();
            Assert.NotNull(httpClient);
        }

        #endregion

        #region 批量请求处理测试

        [Fact]
        public async Task SendBatchAsync_MultipleRequests_ReturnsResponses()
        {
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

            var responses = await httpClientHelper.SendBatchAsync(requests);

            Assert.Equal(2, responses.Count);
            Assert.All(responses, r => Assert.True(r.Success));
        }

        [Fact]
        public async Task SendBatchAsync_NullRequests_ThrowsArgumentNullException()
        {
            var httpClientHelper = new HttpClientHelper();
            await Assert.ThrowsAsync<ArgumentNullException>(() => httpClientHelper.SendBatchAsync(null));
        }

        [Fact]
        public async Task SendBatchAsync_InvalidMaxConcurrency_ThrowsArgumentOutOfRangeException()
        {
            var httpClientHelper = new HttpClientHelper();
            var requests = new List<BatchRequest>();
            await Assert.ThrowsAsync<ArgumentOutOfRangeException>(() => httpClientHelper.SendBatchAsync(requests, 0));
        }

        [Fact]
        public async Task SendSequentialAsync_MultipleRequests_ReturnsResponses()
        {
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

            var responses = await httpClientHelper.SendSequentialAsync(requests);

            Assert.Equal(2, responses.Count);
            Assert.All(responses, r => Assert.True(r.Success));
        }

        [Fact]
        public async Task SendSequentialAsync_NullRequests_ThrowsArgumentNullException()
        {
            var httpClientHelper = new HttpClientHelper();
            await Assert.ThrowsAsync<ArgumentNullException>(() => httpClientHelper.SendSequentialAsync(null));
        }

        #endregion

        #region 统计功能测试

        [Fact]
        public void GetStatistics_InitialState_ReturnsZeroCounts()
        {
            var httpClientHelper = new HttpClientHelper();

            var stats = httpClientHelper.GetStatistics();

            Assert.Equal(0, stats.TotalRequests);
            Assert.Equal(0, stats.SuccessfulRequests);
            Assert.Equal(0, stats.FailedRequests);
        }

        [Fact]
        public async Task GetStatistics_AfterRequests_ReturnsCorrectCounts()
        {
            var mockHandler = new Mock<HttpMessageHandler>();
            var successResponse = new HttpResponseMessage(HttpStatusCode.OK);

            mockHandler.Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(successResponse);

            var httpClient = new HttpClient(mockHandler.Object);
            var httpClientHelper = new HttpClientHelper(httpClient);

            await httpClientHelper.GetAsync("https://example.com");
            await httpClientHelper.GetAsync("https://example.com");

            var stats = httpClientHelper.GetStatistics();

            Assert.Equal(2, stats.TotalRequests);
            Assert.Equal(2, stats.SuccessfulRequests);
            Assert.Equal(0, stats.FailedRequests);
        }

        [Fact]
        public void ResetStatistics_ClearsAllCounts()
        {
            var httpClientHelper = new HttpClientHelper();

            httpClientHelper.ResetStatistics();

            var stats = httpClientHelper.GetStatistics();
            Assert.Equal(0, stats.TotalRequests);
        }

        [Fact]
        public void EnableLogging_DisablesLogging()
        {
            var loggerMock = new Mock<ILogger<HttpClientHelper>>();
            var httpClientHelper = new HttpClientHelper(loggerMock.Object);

            httpClientHelper.EnableLogging(false);

            var httpClient = httpClientHelper.GetHttpClient();
            Assert.NotNull(httpClient);
        }

        #endregion

        #region 文件操作测试

        [Fact]
        public async Task DownloadFileAsync_ValidUrl_DownloadsFile()
        {
            var mockHandler = new Mock<HttpMessageHandler>();
            var fileContent = "Test file content";
            var expectedResponse = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(fileContent)
            };
            expectedResponse.Content.Headers.ContentLength = fileContent.Length;

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
                var result = await httpClientHelper.DownloadFileAsync("https://example.com/file.txt", tempPath);

                Assert.True(result.Success);
                Assert.Equal(tempPath, result.FilePath);
                Assert.True(File.Exists(tempPath));
                Assert.Equal(fileContent, File.ReadAllText(tempPath));
            }
            finally
            {
                if (File.Exists(tempPath))
                {
                    File.Delete(tempPath);
                }
            }
        }

        [Fact]
        public async Task DownloadFileAsync_WithProgressCallback_ReportsProgress()
        {
            var mockHandler = new Mock<HttpMessageHandler>();
            var fileContent = "Test file content for progress test";
            var expectedResponse = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(fileContent)
            };
            expectedResponse.Content.Headers.ContentLength = fileContent.Length;

            mockHandler.Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(expectedResponse);

            var httpClient = new HttpClient(mockHandler.Object);
            var httpClientHelper = new HttpClientHelper(httpClient);

            var tempPath = Path.GetTempFileName();
            var progressReports = new List<DownloadProgress>();

            try
            {
                var result = await httpClientHelper.DownloadFileAsync(
                    "https://example.com/file.txt",
                    tempPath,
                    progressCallback: progress => progressReports.Add(progress));

                Assert.True(result.Success);
                Assert.NotEmpty(progressReports);
            }
            finally
            {
                if (File.Exists(tempPath))
                {
                    File.Delete(tempPath);
                }
            }
        }

        [Fact]
        public async Task DownloadFileAsync_EmptyFilePath_ThrowsArgumentNullException()
        {
            var httpClientHelper = new HttpClientHelper();
            await Assert.ThrowsAsync<ArgumentNullException>(() => httpClientHelper.DownloadFileAsync("https://example.com", ""));
        }

        [Fact]
        public async Task UploadFileAsync_FileNotFound_ThrowsFileNotFoundException()
        {
            var httpClientHelper = new HttpClientHelper();
            await Assert.ThrowsAsync<FileNotFoundException>(() => httpClientHelper.UploadFileAsync("https://example.com", "nonexistent.txt"));
        }

        [Fact]
        public async Task UploadFileAsync_ValidFile_UploadsFile()
        {
            var mockHandler = new Mock<HttpMessageHandler>();
            var expectedResponse = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent("{\"success\":true}")
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
            await File.WriteAllTextAsync(tempPath, "Test upload content");

            try
            {
                var result = await httpClientHelper.UploadFileAsync("https://example.com/upload", tempPath);

                Assert.True(result.Success);
                Assert.Equal("{\"success\":true}", result.ResponseContent);
            }
            finally
            {
                if (File.Exists(tempPath))
                {
                    File.Delete(tempPath);
                }
            }
        }

        [Fact]
        public async Task UploadFileAsync_WithProgressCallback_ReportsProgress()
        {
            var mockHandler = new Mock<HttpMessageHandler>();
            var expectedResponse = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent("{\"success\":true}")
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
            await File.WriteAllTextAsync(tempPath, "Test upload content with progress");
            var progressReports = new List<UploadProgress>();

            try
            {
                var result = await httpClientHelper.UploadFileAsync(
                    "https://example.com/upload",
                    tempPath,
                    progressCallback: progress => progressReports.Add(progress));

                Assert.True(result.Success);
            }
            finally
            {
                if (File.Exists(tempPath))
                {
                    File.Delete(tempPath);
                }
            }
        }

        #endregion

        #region 配置方法测试

        [Fact]
        public void SetMaxConnectionsPerServer_ValidValue_SetsValue()
        {
            var httpClientHelper = new HttpClientHelper();

            httpClientHelper.SetMaxConnectionsPerServer(50);

            var httpClient = httpClientHelper.GetHttpClient();
            Assert.NotNull(httpClient);
        }

        [Fact]
        public void SetMaxConnectionsPerServer_InvalidValue_ThrowsArgumentOutOfRangeException()
        {
            var httpClientHelper = new HttpClientHelper();
            Assert.Throws<ArgumentOutOfRangeException>(() => httpClientHelper.SetMaxConnectionsPerServer(0));
        }

        [Fact]
        public void SetAutomaticDecompression_Enabled_SetsDecompression()
        {
            var httpClientHelper = new HttpClientHelper();

            httpClientHelper.SetAutomaticDecompression(true);

            var httpClient = httpClientHelper.GetHttpClient();
            Assert.NotNull(httpClient);
        }

        [Fact]
        public void SetCookieContainer_ValidContainer_SetsContainer()
        {
            var httpClientHelper = new HttpClientHelper();
            var cookieContainer = new CookieContainer();

            httpClientHelper.SetCookieContainer(cookieContainer);

            var result = httpClientHelper.GetCookieContainer();
            Assert.NotNull(result);
        }

        #endregion

        #region IDisposable测试

        [Fact]
        public void Dispose_CalledOnce_DisposesResources()
        {
            var httpClientHelper = new HttpClientHelper();

            httpClientHelper.Dispose();

            var httpClient = httpClientHelper.GetHttpClient();
            Assert.Throws<ObjectDisposedException>(() => httpClient.Send(new HttpRequestMessage(HttpMethod.Get, "https://example.com")));
        }

        [Fact]
        public void Dispose_CalledMultipleTimes_DoesNotThrow()
        {
            var httpClientHelper = new HttpClientHelper();

            httpClientHelper.Dispose();
            httpClientHelper.Dispose();
        }

        #endregion
    }

    public class TestObject
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
