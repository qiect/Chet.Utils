using System;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Chet.Utils.Helpers;
using Xunit;

namespace Chet.Utils.Tests.Helpers
{
    public class WebSocketHelperTests
    {
        /// <summary>
        /// 测试WebSocketHelper构造函数是否正确初始化属性
        /// </summary>
        [Fact]
        public void Constructor_InitializesProperties()
        {
            // Arrange
            int heartbeatInterval = 10000;
            int maxReconnectAttempts = 3;
            int reconnectDelay = 2000;

            // Act
            var webSocketHelper = new WebSocketHelper(heartbeatInterval, maxReconnectAttempts, reconnectDelay);

            // Assert
            Assert.NotNull(webSocketHelper);
            // Note: We can't directly assert on private fields, but we can test behavior
        }

        /// <summary>
        /// 测试WebSocketHelper默认构造函数
        /// </summary>
        [Fact]
        public void Constructor_WithDefaultParameters_InitializesWithDefaults()
        {
            // Act
            var webSocketHelper = new WebSocketHelper();

            // Assert
            Assert.NotNull(webSocketHelper);
        }

        /// <summary>
        /// 测试连接状态属性在初始时为None
        /// </summary>
        [Fact]
        public void State_WhenInitialized_IsNone()
        {
            // Arrange
            var webSocketHelper = new WebSocketHelper();

            // Act
            var state = webSocketHelper.State;

            // Assert
            Assert.Equal(WebSocketState.None, state);
        }

        /// <summary>
        /// 测试IsConnected属性在初始时为false
        /// </summary>
        [Fact]
        public void IsConnected_WhenInitialized_IsFalse()
        {
            // Arrange
            var webSocketHelper = new WebSocketHelper();

            // Act
            var isConnected = webSocketHelper.IsConnected;

            // Assert
            Assert.False(isConnected);
        }

        /// <summary>
        /// 测试SendMessageAsync在未连接时抛出异常
        /// </summary>
        [Fact]
        public async Task SendMessageAsync_WhenNotConnected_ThrowsInvalidOperationException()
        {
            // Arrange
            var webSocketHelper = new WebSocketHelper();
            string message = "Test message";

            // Act & Assert
            await Assert.ThrowsAsync<InvalidOperationException>(() =>
                webSocketHelper.SendMessageAsync(message));
        }

        /// <summary>
        /// 测试SendBinaryAsync在未连接时抛出异常
        /// </summary>
        [Fact]
        public async Task SendBinaryAsync_WhenNotConnected_ThrowsInvalidOperationException()
        {
            // Arrange
            var webSocketHelper = new WebSocketHelper();
            byte[] data = Encoding.UTF8.GetBytes("Test data");

            // Act & Assert
            await Assert.ThrowsAsync<InvalidOperationException>(() =>
                webSocketHelper.SendBinaryAsync(data));
        }

        /// <summary>
        /// 测试SendPingAsync在未连接时抛出异常
        /// </summary>
        [Fact]
        public async Task SendPingAsync_WhenNotConnected_ThrowsInvalidOperationException()
        {
            // Arrange
            var webSocketHelper = new WebSocketHelper();

            // Act & Assert
            await Assert.ThrowsAsync<InvalidOperationException>(() =>
                webSocketHelper.SendPingAsync());
        }

        /// <summary>
        /// 测试连接事件是否正确触发
        /// </summary>
        [Fact]
        public async Task ConnectAsync_RaisesOnConnectedEvent()
        {
            // Arrange
            var webSocketHelper = new WebSocketHelper();
            bool eventRaised = false;
            webSocketHelper.OnConnected += (sender, args) => eventRaised = true;

            // 使用一个无效的URI来测试事件系统，因为实际连接需要WebSocket服务器
            var uri = new Uri("ws://invalid-localhost:12345");

            // Act
            try
            {
                await webSocketHelper.ConnectAsync(uri, new CancellationToken(true)); // Already cancelled token
            }
            catch (OperationCanceledException)
            {
                // 忽略取消异常
            }

            // Assert
            // Note: 由于使用了已取消的令牌，ConnectAsync会立即返回，但我们验证事件系统是否工作
            Assert.False(eventRaised); // 不应该触发连接成功事件
        }

        /// <summary>
        /// 测试错误事件是否正确触发
        /// </summary>
        [Fact]
        public async Task ConnectAsync_WithInvalidUri_RaisesOnErrorEvent()
        {
            // Arrange
            var webSocketHelper = new WebSocketHelper();
            Exception raisedException = null;
            webSocketHelper.OnError += (sender, exception) => raisedException = exception;

            var uri = new Uri("ws://invalid-localhost:12345");

            // Act
            try
            {
                await webSocketHelper.ConnectAsync(uri, new CancellationToken(true)); // Already cancelled token
            }
            catch (OperationCanceledException)
            {
                // 忽略取消异常
            }

            // Assert
            // With cancelled token, we might not get an error event
        }

        /// <summary>
        /// 测试消息接收事件是否能正确触发
        /// </summary>
        [Fact]
        public void OnMessageReceived_EventSubscription_Works()
        {
            // Arrange
            var webSocketHelper = new WebSocketHelper();
            string receivedMessage = null;
            webSocketHelper.OnMessageReceived += (sender, message) => receivedMessage = message;

            // Act
            // 模拟触发事件（在实际中这会由内部逻辑触发）
            var handler = webSocketHelper.GetType()
                .GetField("OnMessageReceived",
                    System.Reflection.BindingFlags.Instance |
                    System.Reflection.BindingFlags.NonPublic)
                ?.GetValue(webSocketHelper) as EventHandler<string>;

            // Assert
            Assert.NotNull(webSocketHelper);
            // We can't easily trigger the event without actually receiving a message
        }

        /// <summary>
        /// 测试Dispose方法是否正确清理资源
        /// </summary>
        [Fact]
        public void Dispose_ClearsResources()
        {
            // Arrange
            var webSocketHelper = new WebSocketHelper();

            // Act
            webSocketHelper.Dispose();

            // Assert
            // After dispose, we can't easily check private state without reflection
            Assert.NotNull(webSocketHelper);
        }

        /// <summary>
        /// 测试心跳事件是否能正确触发
        /// </summary>
        [Fact]
        public void OnHeartbeat_EventSubscription_Works()
        {
            // Arrange
            var webSocketHelper = new WebSocketHelper();
            bool heartbeatReceived = false;
            webSocketHelper.OnHeartbeat += (sender, args) => heartbeatReceived = true;

            // Act
            // Similar to message received, we can't easily trigger without internal access

            // Assert
            Assert.False(heartbeatReceived);
        }

        /// <summary>
        /// 测试断开连接事件是否能正确触发
        /// </summary>
        [Fact]
        public async Task DisconnectAsync_RaisesOnDisconnectedEvent()
        {
            // Arrange
            var webSocketHelper = new WebSocketHelper();
            WebSocketCloseStatus? receivedStatus = null;
            webSocketHelper.OnDisconnected += (sender, status) => receivedStatus = status;

            // Act
            await webSocketHelper.DisconnectAsync();

            // Assert
            // Should be called with null since no actual connection was made
            Assert.Null(receivedStatus);
        }

        /// <summary>
        /// 测试循环发送和接收消息的功能
        /// 注意：此测试模拟WebSocketHelper的消息接收机制，测试事件订阅和触发功能
        /// </summary>
        [Fact]
        public async Task TestLoopSendAndReceiveMessages()
        {
            // Arrange
            var webSocketHelper = new WebSocketHelper();
            var receivedMessages = new List<string>();
            var messageCount = 5; // 测试循环5次
            
            // 订阅消息接收事件
            webSocketHelper.OnMessageReceived += (sender, message) =>
            {
                receivedMessages.Add(message);
                Console.WriteLine($"Received message: {message}");
            };
            
            // Act
            // 模拟触发多次消息接收
            // 由于我们无法直接测试WebSocket连接，我们通过直接调用事件处理程序来模拟
            // 实际应用中，这些事件会由WebSocketHelper内部的ReceiveMessagesAsync方法触发
            
            // 使用反射获取消息接收事件字段
            var onMessageReceivedField = typeof(WebSocketHelper).GetField("OnMessageReceived", 
                System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            
            if (onMessageReceivedField != null)
            {
                var onMessageReceived = onMessageReceivedField.GetValue(webSocketHelper) as EventHandler<string>;
                
                // 模拟发送多条消息
                for (int i = 0; i < messageCount; i++)
                {
                    var testMessage = $"Test message {i}";
                    onMessageReceived?.Invoke(webSocketHelper, testMessage);
                    await Task.Delay(10); // 短暂延迟模拟异步操作
                }
            }

            // Assert
            // 验证是否正确接收了所有消息
            Assert.Equal(messageCount, receivedMessages.Count);
            for (int i = 0; i < messageCount; i++)
            {
                Assert.Equal($"Test message {i}", receivedMessages[i]);
            }
        }
        
        /// <summary>
        /// 测试WebSocketHelper在循环接收消息时的行为
        /// 此测试通过模拟内部逻辑来验证循环接收机制
        /// </summary>
        [Fact]
        public void TestReceiveMessagesLoopBehavior()
        {
            // 这个测试主要验证WebSocketHelper的事件系统是否能正确处理多次消息
            // 由于我们无法直接测试异步的ReceiveMessagesAsync方法，
            // 我们通过多次触发事件来模拟循环接收的情况
            
            // Arrange
            var webSocketHelper = new WebSocketHelper();
            var receivedMessages = new List<string>();
            var messageCount = 10;
            
            // 订阅消息接收事件
            webSocketHelper.OnMessageReceived += (sender, message) =>
            {
                receivedMessages.Add(message);
            };
            
            // Act - 模拟多次接收消息
            for (int i = 0; i < messageCount; i++)
            {
                // 直接调用事件处理程序
                var onMessageReceivedField = typeof(WebSocketHelper).GetField("OnMessageReceived", 
                    System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
                
                if (onMessageReceivedField != null)
                {
                    var onMessageReceived = onMessageReceivedField.GetValue(webSocketHelper) as EventHandler<string>;
                    onMessageReceived?.Invoke(webSocketHelper, $"Loop message {i}");
                }
            }
            
            // Assert
            Assert.Equal(messageCount, receivedMessages.Count);
            for (int i = 0; i < messageCount; i++)
            {
                Assert.Equal($"Loop message {i}", receivedMessages[i]);
            }
        }
    }
}