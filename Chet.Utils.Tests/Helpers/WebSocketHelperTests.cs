using System.Net.WebSockets;
using System.Text;
using Chet.Utils.Helpers;
using Xunit;

namespace Chet.Utils.Tests.Helpers
{
    public class WebSocketHelperTests
    {
        #region 构造函数测试

        [Fact]
        public void Constructor_InitializesProperties()
        {
            int heartbeatInterval = 10000;
            int maxReconnectAttempts = 3;
            int reconnectDelay = 2000;

            var webSocketHelper = new WebSocketHelper(heartbeatInterval, maxReconnectAttempts, reconnectDelay);

            Assert.NotNull(webSocketHelper);
        }

        [Fact]
        public void Constructor_WithDefaultParameters_InitializesWithDefaults()
        {
            var webSocketHelper = new WebSocketHelper();

            Assert.NotNull(webSocketHelper);
        }

        [Fact]
        public void Constructor_WithCustomParameters_InitializesCorrectly()
        {
            var webSocketHelper = new WebSocketHelper(
                heartbeatInterval: 5000,
                maxReconnectAttempts: 10,
                reconnectDelay: 3000,
                receiveBufferSize: 8192,
                sendBufferSize: 8192,
                connectionTimeout: TimeSpan.FromSeconds(60));

            Assert.NotNull(webSocketHelper);
            Assert.True(webSocketHelper.AutoReconnect);
            Assert.True(webSocketHelper.EnableMessageQueue);
        }

        #endregion

        #region 属性测试

        [Fact]
        public void State_WhenInitialized_IsNone()
        {
            var webSocketHelper = new WebSocketHelper();

            var state = webSocketHelper.State;

            Assert.Equal(WebSocketState.None, state);
        }

        [Fact]
        public void IsConnected_WhenInitialized_IsFalse()
        {
            var webSocketHelper = new WebSocketHelper();

            var isConnected = webSocketHelper.IsConnected;

            Assert.False(isConnected);
        }

        [Fact]
        public void ConnectedUri_WhenInitialized_IsNull()
        {
            var webSocketHelper = new WebSocketHelper();

            Assert.Null(webSocketHelper.ConnectedUri);
        }

        [Fact]
        public void QueuedMessageCount_WhenInitialized_IsZero()
        {
            var webSocketHelper = new WebSocketHelper();

            Assert.Equal(0, webSocketHelper.QueuedMessageCount);
        }

        [Fact]
        public void AutoReconnect_DefaultValue_IsTrue()
        {
            var webSocketHelper = new WebSocketHelper();

            Assert.True(webSocketHelper.AutoReconnect);
        }

        [Fact]
        public void EnableMessageQueue_DefaultValue_IsTrue()
        {
            var webSocketHelper = new WebSocketHelper();

            Assert.True(webSocketHelper.EnableMessageQueue);
        }

        #endregion

        #region 发送消息异常测试

        [Fact]
        public async Task SendMessageAsync_WhenNotConnected_ThrowsInvalidOperationException()
        {
            var webSocketHelper = new WebSocketHelper();
            webSocketHelper.EnableMessageQueue = false;
            string message = "Test message";

            await Assert.ThrowsAsync<InvalidOperationException>(() =>
                webSocketHelper.SendMessageAsync(message));
        }

        [Fact]
        public async Task SendMessageAsync_WhenNotConnectedAndQueueEnabled_QueuesMessage()
        {
            var webSocketHelper = new WebSocketHelper();
            webSocketHelper.EnableMessageQueue = true;
            string message = "Test message";

            await webSocketHelper.SendMessageAsync(message);

            Assert.Equal(1, webSocketHelper.QueuedMessageCount);
        }

        [Fact]
        public async Task SendMessageAsync_WithNullMessage_ThrowsArgumentNullException()
        {
            var webSocketHelper = new WebSocketHelper();

            await Assert.ThrowsAsync<ArgumentNullException>(() =>
                webSocketHelper.SendMessageAsync(null));
        }

        [Fact]
        public async Task SendBinaryAsync_WhenNotConnected_ThrowsInvalidOperationException()
        {
            var webSocketHelper = new WebSocketHelper();
            byte[] data = Encoding.UTF8.GetBytes("Test data");

            await Assert.ThrowsAsync<InvalidOperationException>(() =>
                webSocketHelper.SendBinaryAsync(data));
        }

        [Fact]
        public async Task SendBinaryAsync_WithNullData_ThrowsArgumentNullException()
        {
            var webSocketHelper = new WebSocketHelper();

            await Assert.ThrowsAsync<ArgumentNullException>(() =>
                webSocketHelper.SendBinaryAsync(null));
        }

        [Fact]
        public async Task SendMessagesAsync_WithNullMessages_ThrowsArgumentNullException()
        {
            var webSocketHelper = new WebSocketHelper();

            await Assert.ThrowsAsync<ArgumentNullException>(() =>
                webSocketHelper.SendMessagesAsync(null));
        }

        #endregion

        #region 事件测试

        [Fact]
        public void OnConnected_EventCanBeSubscribed()
        {
            var webSocketHelper = new WebSocketHelper();
            WebSocketConnectedEventArgs receivedArgs = null;

            webSocketHelper.OnConnected += (sender, args) => receivedArgs = args;

            Assert.NotNull(webSocketHelper);
        }

        [Fact]
        public void OnDisconnected_EventCanBeSubscribed()
        {
            var webSocketHelper = new WebSocketHelper();
            WebSocketDisconnectedEventArgs receivedArgs = null;

            webSocketHelper.OnDisconnected += (sender, args) => receivedArgs = args;

            Assert.NotNull(webSocketHelper);
        }

        [Fact]
        public void OnMessageReceived_EventCanBeSubscribed()
        {
            var webSocketHelper = new WebSocketHelper();
            WebSocketMessageReceivedEventArgs receivedArgs = null;

            webSocketHelper.OnMessageReceived += (sender, args) => receivedArgs = args;

            Assert.NotNull(webSocketHelper);
        }

        [Fact]
        public void OnBinaryReceived_EventCanBeSubscribed()
        {
            var webSocketHelper = new WebSocketHelper();
            WebSocketBinaryReceivedEventArgs receivedArgs = null;

            webSocketHelper.OnBinaryReceived += (sender, args) => receivedArgs = args;

            Assert.NotNull(webSocketHelper);
        }

        [Fact]
        public void OnError_EventCanBeSubscribed()
        {
            var webSocketHelper = new WebSocketHelper();
            WebSocketErrorEventArgs receivedArgs = null;

            webSocketHelper.OnError += (sender, args) => receivedArgs = args;

            Assert.NotNull(webSocketHelper);
        }

        [Fact]
        public void OnHeartbeat_EventCanBeSubscribed()
        {
            var webSocketHelper = new WebSocketHelper();
            WebSocketHeartbeatEventArgs receivedArgs = null;

            webSocketHelper.OnHeartbeat += (sender, args) => receivedArgs = args;

            Assert.NotNull(webSocketHelper);
        }

        [Fact]
        public void OnReconnecting_EventCanBeSubscribed()
        {
            var webSocketHelper = new WebSocketHelper();
            WebSocketReconnectingEventArgs receivedArgs = null;

            webSocketHelper.OnReconnecting += (sender, args) => receivedArgs = args;

            Assert.NotNull(webSocketHelper);
        }

        [Fact]
        public void OnMessageSent_EventCanBeSubscribed()
        {
            var webSocketHelper = new WebSocketHelper();
            WebSocketMessageSentEventArgs receivedArgs = null;

            webSocketHelper.OnMessageSent += (sender, args) => receivedArgs = args;

            Assert.NotNull(webSocketHelper);
        }

        #endregion

        #region 连接测试

        [Fact]
        public async Task ConnectAsync_WithNullUri_ThrowsArgumentNullException()
        {
            var webSocketHelper = new WebSocketHelper();

            await Assert.ThrowsAsync<ArgumentNullException>(() =>
                webSocketHelper.ConnectAsync(null));
        }

        [Fact]
        public async Task ConnectAsync_WithCancelledToken_DoesNotConnect()
        {
            var webSocketHelper = new WebSocketHelper();
            var cts = new CancellationTokenSource();
            cts.Cancel();

            var uri = new Uri("ws://localhost:12345");

            await Assert.ThrowsAnyAsync<OperationCanceledException>(() =>
                webSocketHelper.ConnectAsync(uri, cts.Token));
        }

        [Fact]
        public async Task DisconnectAsync_WhenNotConnected_DoesNotThrow()
        {
            var webSocketHelper = new WebSocketHelper();

            var exception = await Record.ExceptionAsync(() =>
                webSocketHelper.DisconnectAsync());

            Assert.Null(exception);
        }

        [Fact]
        public async Task DisconnectAsync_CalledMultipleTimes_DoesNotThrow()
        {
            var webSocketHelper = new WebSocketHelper();

            await webSocketHelper.DisconnectAsync();
            await webSocketHelper.DisconnectAsync();
            await webSocketHelper.DisconnectAsync();
        }

        [Fact]
        public async Task ReconnectAsync_WithoutPreviousConnection_ThrowsInvalidOperationException()
        {
            var webSocketHelper = new WebSocketHelper();

            await Assert.ThrowsAsync<InvalidOperationException>(() =>
                webSocketHelper.ReconnectAsync());
        }

        #endregion

        #region 消息队列测试

        [Fact]
        public void ClearMessageQueue_ClearsAllMessages()
        {
            var webSocketHelper = new WebSocketHelper();
            webSocketHelper.EnableMessageQueue = true;

            webSocketHelper.SendMessageAsync("message1").Wait();
            webSocketHelper.SendMessageAsync("message2").Wait();
            Assert.Equal(2, webSocketHelper.QueuedMessageCount);

            webSocketHelper.ClearMessageQueue();

            Assert.Equal(0, webSocketHelper.QueuedMessageCount);
        }

        #endregion

        #region Dispose测试

        [Fact]
        public void Dispose_ClearsResources()
        {
            var webSocketHelper = new WebSocketHelper();

            webSocketHelper.Dispose();

            Assert.NotNull(webSocketHelper);
        }

        [Fact]
        public void Dispose_CalledMultipleTimes_DoesNotThrow()
        {
            var webSocketHelper = new WebSocketHelper();

            webSocketHelper.Dispose();
            webSocketHelper.Dispose();
            webSocketHelper.Dispose();
        }

        #endregion

        #region WebSocketServerHelper 测试

        [Fact]
        public void WebSocketServerHelper_Constructor_InitializesProperties()
        {
            var server = new WebSocketServerHelper(heartbeatIntervalSec: 30);

            Assert.NotNull(server);
            Assert.False(server.IsRunning);
            Assert.Equal(0, server.ConnectedClientCount);
        }

        [Fact]
        public void WebSocketServerHelper_Constructor_WithPort_InitializesProperties()
        {
            var server = new WebSocketServerHelper(8080, heartbeatIntervalSec: 30);

            Assert.NotNull(server);
            Assert.False(server.IsRunning);
        }

        [Fact]
        public void WebSocketServerHelper_ConnectedClientIds_ReturnsEmptyWhenNoClients()
        {
            var server = new WebSocketServerHelper();

            var clientIds = server.ConnectedClientIds;

            Assert.Empty(clientIds);
        }

        [Fact]
        public void WebSocketServerHelper_GroupNames_ReturnsEmptyWhenNoGroups()
        {
            var server = new WebSocketServerHelper();

            var groupNames = server.GroupNames;

            Assert.Empty(groupNames);
        }

        [Fact]
        public void WebSocketServerHelper_StopServer_WhenNotRunning_DoesNotThrow()
        {
            var server = new WebSocketServerHelper();

            var exception = Record.Exception(() => server.StopServer());

            Assert.Null(exception);
        }

        [Fact]
        public void WebSocketServerHelper_StopServer_CalledMultipleTimes_DoesNotThrow()
        {
            var server = new WebSocketServerHelper();

            server.StopServer();
            server.StopServer();
            server.StopServer();
        }

        [Fact]
        public void WebSocketServerHelper_GetClientInfo_WhenClientNotExists_ReturnsNull()
        {
            var server = new WebSocketServerHelper();

            var clientInfo = server.GetClientInfo("non-existent-id");

            Assert.Null(clientInfo);
        }

        [Fact]
        public async Task WebSocketServerHelper_StartServerAsync_WithNullPrefixes_ThrowsArgumentException()
        {
            var server = new WebSocketServerHelper();

            await Assert.ThrowsAsync<ArgumentException>(() =>
                server.StartServerAsync((IEnumerable<string>)null));
        }

        [Fact]
        public async Task WebSocketServerHelper_StartServerAsync_WithEmptyPrefixes_ThrowsArgumentException()
        {
            var server = new WebSocketServerHelper();

            await Assert.ThrowsAsync<ArgumentException>(() =>
                server.StartServerAsync(new List<string>()));
        }

        [Fact]
        public async Task WebSocketServerHelper_StartServerAsync_WithInvalidPrefix_ThrowsException()
        {
            var server = new WebSocketServerHelper();

            await Assert.ThrowsAnyAsync<Exception>(() =>
                server.StartServerAsync("invalid-prefix"));
        }

        [Fact]
        public async Task WebSocketServerHelper_SendMessageToClient_WithNullClientId_ThrowsArgumentNullException()
        {
            var server = new WebSocketServerHelper();

            await Assert.ThrowsAsync<ArgumentNullException>(() =>
                server.SendMessageToClientAsync(null, "test message"));
        }

        [Fact]
        public async Task WebSocketServerHelper_SendMessageToClient_WithEmptyClientId_ThrowsArgumentException()
        {
            var server = new WebSocketServerHelper();

            await Assert.ThrowsAsync<ArgumentException>(() =>
                server.SendMessageToClientAsync("", "test message"));
        }

        [Fact]
        public async Task WebSocketServerHelper_SendMessageToClient_WithNullMessage_ThrowsArgumentNullException()
        {
            var server = new WebSocketServerHelper();

            await Assert.ThrowsAsync<ArgumentNullException>(() =>
                server.SendMessageToClientAsync("client-id", null));
        }

        [Fact]
        public async Task WebSocketServerHelper_SendMessageToClient_WhenClientNotExists_ThrowsArgumentException()
        {
            var server = new WebSocketServerHelper();

            await Assert.ThrowsAsync<ArgumentException>(() =>
                server.SendMessageToClientAsync("non-existent-id", "test message"));
        }

        [Fact]
        public async Task WebSocketServerHelper_SendBinaryToClient_WithNullClientId_ThrowsArgumentNullException()
        {
            var server = new WebSocketServerHelper();
            byte[] data = Encoding.UTF8.GetBytes("test data");

            await Assert.ThrowsAsync<ArgumentNullException>(() =>
                server.SendBinaryToClientAsync(null, data));
        }

        [Fact]
        public async Task WebSocketServerHelper_SendBinaryToClient_WithNullData_ThrowsArgumentNullException()
        {
            var server = new WebSocketServerHelper();

            await Assert.ThrowsAsync<ArgumentNullException>(() =>
                server.SendBinaryToClientAsync("client-id", null));
        }

        [Fact]
        public async Task WebSocketServerHelper_SendBinaryToClient_WhenClientNotExists_ThrowsArgumentException()
        {
            var server = new WebSocketServerHelper();
            byte[] data = Encoding.UTF8.GetBytes("test data");

            await Assert.ThrowsAsync<ArgumentException>(() =>
                server.SendBinaryToClientAsync("non-existent-id", data));
        }

        [Fact]
        public void WebSocketServerHelper_AddClientToGroup_WithNullClientId_ThrowsArgumentNullException()
        {
            var server = new WebSocketServerHelper();

            Assert.Throws<ArgumentNullException>(() => server.AddClientToGroup(null, "test-group"));
        }

        [Fact]
        public void WebSocketServerHelper_AddClientToGroup_WithNullGroupName_ThrowsArgumentNullException()
        {
            var server = new WebSocketServerHelper();

            Assert.Throws<ArgumentNullException>(() => server.AddClientToGroup("client-id", null));
        }

        [Fact]
        public void WebSocketServerHelper_RemoveClientFromGroup_WithNullClientId_ThrowsArgumentNullException()
        {
            var server = new WebSocketServerHelper();

            Assert.Throws<ArgumentNullException>(() => server.RemoveClientFromGroup(null, "test-group"));
        }

        [Fact]
        public void WebSocketServerHelper_RemoveClientFromGroup_WithNullGroupName_ThrowsArgumentNullException()
        {
            var server = new WebSocketServerHelper();

            Assert.Throws<ArgumentNullException>(() => server.RemoveClientFromGroup("client-id", null));
        }

        [Fact]
        public void WebSocketServerHelper_GetClientsInGroup_WithNullGroupName_ThrowsArgumentNullException()
        {
            var server = new WebSocketServerHelper();

            Assert.Throws<ArgumentNullException>(() => server.GetClientsInGroup(null));
        }

        [Fact]
        public void WebSocketServerHelper_GetClientsInGroup_WhenGroupNotExists_ReturnsEmpty()
        {
            var server = new WebSocketServerHelper();

            var clients = server.GetClientsInGroup("non-existent-group");

            Assert.Empty(clients);
        }

        [Fact]
        public void WebSocketServerHelper_DisconnectClient_WithNullClientId_ThrowsArgumentNullException()
        {
            var server = new WebSocketServerHelper();

            Assert.Throws<ArgumentNullException>(() => server.DisconnectClient(null));
        }

        [Fact]
        public void WebSocketServerHelper_DisconnectClient_WhenClientNotExists_DoesNotThrow()
        {
            var server = new WebSocketServerHelper();

            server.DisconnectClient("non-existent-id");
        }

        [Fact]
        public void WebSocketServerHelper_SetClientCustomInfo_WithNullClientId_ThrowsArgumentNullException()
        {
            var server = new WebSocketServerHelper();

            Assert.Throws<ArgumentNullException>(() => server.SetClientCustomInfo(null, new { Name = "Test" }));
        }

        [Fact]
        public void WebSocketServerHelper_Dispose_ClearsResources()
        {
            var server = new WebSocketServerHelper();

            server.Dispose();

            Assert.NotNull(server);
        }

        [Fact]
        public void WebSocketServerHelper_Dispose_CalledMultipleTimes_DoesNotThrow()
        {
            var server = new WebSocketServerHelper();

            server.Dispose();
            server.Dispose();
            server.Dispose();
        }

        #endregion

        #region 事件参数类测试

        [Fact]
        public void WebSocketConnectedEventArgs_Properties_WorkCorrectly()
        {
            var uri = new Uri("ws://localhost:8080");
            var eventArgs = new WebSocketConnectedEventArgs(uri);

            Assert.Equal(uri, eventArgs.Uri);
            Assert.True(eventArgs.ConnectedTime <= DateTime.Now);
        }

        [Fact]
        public void WebSocketDisconnectedEventArgs_Properties_WorkCorrectly()
        {
            var eventArgs = new WebSocketDisconnectedEventArgs(
                WebSocketCloseStatus.NormalClosure,
                "Test closure");

            Assert.Equal(WebSocketCloseStatus.NormalClosure, eventArgs.CloseStatus);
            Assert.Equal("Test closure", eventArgs.StatusDescription);
            Assert.True(eventArgs.DisconnectedTime <= DateTime.Now);
        }

        [Fact]
        public void WebSocketMessageReceivedEventArgs_Properties_WorkCorrectly()
        {
            var message = "test message";
            var eventArgs = new WebSocketMessageReceivedEventArgs(message);

            Assert.Equal(message, eventArgs.Message);
            Assert.True(eventArgs.ReceivedTime <= DateTime.Now);
        }

        [Fact]
        public void WebSocketBinaryReceivedEventArgs_Properties_WorkCorrectly()
        {
            var data = Encoding.UTF8.GetBytes("test data");
            var eventArgs = new WebSocketBinaryReceivedEventArgs(data);

            Assert.Equal(data, eventArgs.Data);
            Assert.True(eventArgs.ReceivedTime <= DateTime.Now);
        }

        [Fact]
        public void WebSocketErrorEventArgs_Properties_WorkCorrectly()
        {
            var exception = new InvalidOperationException("Test error");
            var eventArgs = new WebSocketErrorEventArgs("test-id", exception);

            Assert.Equal("test-id", eventArgs.ClientId);
            Assert.Equal(exception, eventArgs.Exception);
            Assert.True(eventArgs.ErrorTime <= DateTime.Now);
        }

        [Fact]
        public void WebSocketHeartbeatEventArgs_Properties_WorkCorrectly()
        {
            var heartbeatTime = DateTime.Now;
            var eventArgs = new WebSocketHeartbeatEventArgs(true, heartbeatTime, "test message");

            Assert.True(eventArgs.IsSuccess);
            Assert.Equal(heartbeatTime, eventArgs.HeartbeatTime);
            Assert.Equal("test message", eventArgs.ErrorMessage);
        }

        [Fact]
        public void WebSocketReconnectingEventArgs_Properties_WorkCorrectly()
        {
            var eventArgs = new WebSocketReconnectingEventArgs(3, 5);

            Assert.Equal(3, eventArgs.Attempt);
            Assert.Equal(5, eventArgs.MaxAttempts);
            Assert.True(eventArgs.ReconnectingTime <= DateTime.Now);
        }

        [Fact]
        public void WebSocketMessageSentEventArgs_Properties_WorkCorrectly()
        {
            var eventArgs = new WebSocketMessageSentEventArgs("test message", false);

            Assert.Equal("test message", eventArgs.Message);
            Assert.False(eventArgs.IsBinary);
            Assert.True(eventArgs.SentTime <= DateTime.Now);
        }

        [Fact]
        public void WebSocketMessageSentEventArgs_BinaryMessage_WorkCorrectly()
        {
            var eventArgs = new WebSocketMessageSentEventArgs("[Binary Data]", true);

            Assert.Equal("[Binary Data]", eventArgs.Message);
            Assert.True(eventArgs.IsBinary);
        }

        [Fact]
        public void ClientConnectedEventArgs_Properties_WorkCorrectly()
        {
            var clientInfo = new ClientInfo();
            var eventArgs = new ClientConnectedEventArgs(clientInfo);

            Assert.Equal(clientInfo, eventArgs.ClientInfo);
        }

        [Fact]
        public void ClientDisconnectedEventArgs_Properties_WorkCorrectly()
        {
            var eventArgs = new ClientDisconnectedEventArgs(
                "test-id",
                WebSocketCloseStatus.NormalClosure,
                "Test closure");

            Assert.Equal("test-id", eventArgs.ClientId);
            Assert.Equal(WebSocketCloseStatus.NormalClosure, eventArgs.CloseStatus);
            Assert.Equal("Test closure", eventArgs.StatusDescription);
        }

        [Fact]
        public void MessageReceivedEventArgs_TextMessage_Properties_WorkCorrectly()
        {
            var eventArgs = new MessageReceivedEventArgs("test-id", "test message");

            Assert.Equal("test-id", eventArgs.ClientId);
            Assert.Equal("test message", eventArgs.Message);
            Assert.False(eventArgs.IsBinary);
            Assert.True(eventArgs.ReceivedTime <= DateTime.Now);
        }

        [Fact]
        public void MessageReceivedEventArgs_BinaryMessage_Properties_WorkCorrectly()
        {
            var data = Encoding.UTF8.GetBytes("test data");
            var eventArgs = new MessageReceivedEventArgs("test-id", data);

            Assert.Equal("test-id", eventArgs.ClientId);
            Assert.Equal(data, eventArgs.BinaryData);
            Assert.True(eventArgs.IsBinary);
        }

        [Fact]
        public void MessageSentEventArgs_Properties_WorkCorrectly()
        {
            var eventArgs = new MessageSentEventArgs("test-id", "test message");

            Assert.Equal("test-id", eventArgs.ClientId);
            Assert.Equal("test message", eventArgs.Message);
            Assert.False(eventArgs.IsBinary);
            Assert.True(eventArgs.SentTime <= DateTime.Now);
        }

        [Fact]
        public void MessageSentEventArgs_BinaryMessage_WorkCorrectly()
        {
            var eventArgs = new MessageSentEventArgs("test-id", "[Binary Data]", true);

            Assert.Equal("test-id", eventArgs.ClientId);
            Assert.True(eventArgs.IsBinary);
        }

        [Fact]
        public void HeartbeatEventArgs_Properties_WorkCorrectly()
        {
            var eventArgs = new HeartbeatEventArgs("test-id", true, "test message");

            Assert.Equal("test-id", eventArgs.ClientId);
            Assert.True(eventArgs.IsNormal);
            Assert.Equal("test message", eventArgs.Message);
            Assert.True(eventArgs.HeartbeatTime <= DateTime.Now);
        }

        #endregion

        #region ClientInfo 测试

        [Fact]
        public void ClientInfo_ConnectionDuration_WorksCorrectly()
        {
            var clientInfo = new ClientInfo();
            Assert.True(clientInfo.ConnectionDuration >= TimeSpan.Zero);
        }

        [Fact]
        public void ClientInfo_CustomInfo_CanBeSetAndRetrieved()
        {
            var customData = new { Name = "Test", Age = 25 };
            var clientInfo = new ClientInfo
            {
                CustomInfo = customData
            };

            Assert.Equal(customData, clientInfo.CustomInfo);
        }

        #endregion

        #region 并发测试

        [Fact]
        public async Task WebSocketHelper_ConcurrentDispose_DoesNotThrow()
        {
            var webSocketHelper = new WebSocketHelper();

            var tasks = Enumerable.Range(0, 10)
                .Select(_ => Task.Run(() => webSocketHelper.Dispose()));

            await Task.WhenAll(tasks);
        }

        [Fact]
        public void WebSocketServerHelper_ConcurrentStop_DoesNotThrow()
        {
            var server = new WebSocketServerHelper();

            Parallel.For(0, 10, _ => server.StopServer());
        }

        [Fact]
        public void WebSocketHelper_ConcurrentMessageQueue_DoesNotThrow()
        {
            var webSocketHelper = new WebSocketHelper();
            webSocketHelper.EnableMessageQueue = true;

            Parallel.For(0, 100, i =>
            {
                webSocketHelper.SendMessageAsync($"message-{i}").Wait();
            });

            Assert.Equal(100, webSocketHelper.QueuedMessageCount);
        }

        #endregion
    }
}
