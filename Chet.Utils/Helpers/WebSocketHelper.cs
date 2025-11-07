using System.Net.WebSockets;
using System.Text;
namespace Chet.Utils.Helpers
{
    /// <summary>
    /// WebSocket帮助类，提供WebSocket连接管理、消息发送、心跳检测等功能
    /// </summary>
    public class WebSocketHelper
    {
        #region 私有字段

        /// <summary>
        /// WebSocket客户端实例
        /// </summary>
        private ClientWebSocket _webSocket;

        /// <summary>
        /// 连接状态
        /// </summary>
        private WebSocketState _state;

        /// <summary>
        /// 心跳检测间隔（毫秒）
        /// </summary>
        private readonly int _heartbeatInterval;

        /// <summary>
        /// 心跳检测CancellationTokenSource
        /// </summary>
        private CancellationTokenSource _heartbeatCts;

        /// <summary>
        /// 接收消息CancellationTokenSource
        /// </summary>
        private CancellationTokenSource _receiveCts;

        /// <summary>
        /// 连接锁，确保同一时间只有一个连接操作
        /// </summary>
        private readonly object _connectLock = new object();

        /// <summary>
        /// 重连次数计数器
        /// </summary>
        private int _reconnectAttempts = 0;

        /// <summary>
        /// 最大重连次数
        /// </summary>
        private readonly int _maxReconnectAttempts;

        /// <summary>
        /// 重连延迟时间（毫秒）
        /// </summary>
        private readonly int _reconnectDelay;

        #endregion

        #region 公共属性

        /// <summary>
        /// 获取当前WebSocket连接状态
        /// </summary>
        public WebSocketState State => _state;

        /// <summary>
        /// 获取是否已连接
        /// </summary>
        public bool IsConnected => _webSocket?.State == WebSocketState.Open;

        #endregion

        #region 事件定义

        /// <summary>
        /// 连接成功事件
        /// </summary>
        public event EventHandler OnConnected;

        /// <summary>
        /// 连接断开事件
        /// </summary>
        public event EventHandler<WebSocketCloseStatus?> OnDisconnected;

        /// <summary>
        /// 接收到消息事件
        /// </summary>
        public event EventHandler<string> OnMessageReceived;

        /// <summary>
        /// 发生错误事件
        /// </summary>
        public event EventHandler<Exception> OnError;

        /// <summary>
        /// 心跳检测事件
        /// </summary>
        public event EventHandler OnHeartbeat;

        #endregion

        #region 构造函数

        /// <summary>
        /// 初始化WebSocketHelper实例
        /// </summary>
        /// <param name="heartbeatInterval">心跳检测间隔（毫秒），默认30秒</param>
        /// <param name="maxReconnectAttempts">最大重连次数，默认5次</param>
        /// <param name="reconnectDelay">重连延迟时间（毫秒），默认5秒</param>
        public WebSocketHelper(
            int heartbeatInterval = 30000,
            int maxReconnectAttempts = 5,
            int reconnectDelay = 5000)
        {
            _heartbeatInterval = heartbeatInterval;
            _maxReconnectAttempts = maxReconnectAttempts;
            _reconnectDelay = reconnectDelay;
            _state = WebSocketState.None;
        }

        #endregion

        #region 公共方法

        /// <summary>
        /// 连接到WebSocket服务器
        /// </summary>
        /// <param name="uri">WebSocket服务器地址</param>
        /// <param name="cancellationToken">取消令牌</param>
        /// <returns>连接任务</returns>
        public async Task ConnectAsync(Uri uri, CancellationToken cancellationToken = default)
        {
            if (_webSocket != null && _webSocket.State == WebSocketState.Open)
            {
                return;
            }

            lock (_connectLock)
            {
                if (_webSocket != null && _webSocket.State == WebSocketState.Connecting)
                {
                    return;
                }

                _webSocket = new ClientWebSocket();
                _state = WebSocketState.Connecting;
            }

            try
            {
                await _webSocket.ConnectAsync(uri, cancellationToken);
                _state = WebSocketState.Open;
                _reconnectAttempts = 0;

                // 启动接收消息和心跳检测
                _receiveCts = new CancellationTokenSource();
                _heartbeatCts = new CancellationTokenSource();

                _ = Task.Run(() => ReceiveMessagesAsync(_receiveCts.Token));
                _ = Task.Run(() => HeartbeatAsync(_heartbeatCts.Token));

                OnConnected?.Invoke(this, EventArgs.Empty);
            }
            catch (Exception ex)
            {
                _state = WebSocketState.Closed;
                OnError?.Invoke(this, ex);

                // 尝试重连
                if (_reconnectAttempts < _maxReconnectAttempts)
                {
                    _reconnectAttempts++;
                    await Task.Delay(_reconnectDelay, cancellationToken);
                    await ConnectAsync(uri, cancellationToken);
                }
            }
        }

        /// <summary>
        /// 断开WebSocket连接
        /// </summary>
        /// <param name="closeStatus">关闭状态</param>
        /// <param name="statusDescription">状态描述</param>
        /// <returns>断开连接任务</returns>
        public async Task DisconnectAsync(WebSocketCloseStatus closeStatus = WebSocketCloseStatus.NormalClosure, string statusDescription = "Normal closure")
        {
            if (_webSocket == null)
                return;

            try
            {
                if (_webSocket.State != WebSocketState.Closed && _webSocket.State != WebSocketState.Aborted)
                {
                    await _webSocket.CloseAsync(closeStatus, statusDescription, CancellationToken.None);
                }
            }
            catch (Exception ex)
            {
                OnError?.Invoke(this, ex);
            }
            finally
            {
                Cleanup();
                OnDisconnected?.Invoke(this, closeStatus);
            }
        }

        /// <summary>
        /// 发送文本消息
        /// </summary>
        /// <param name="message">要发送的消息</param>
        /// <param name="cancellationToken">取消令牌</param>
        /// <returns>发送任务</returns>
        public async Task SendMessageAsync(string message, CancellationToken cancellationToken = default)
        {
            if (_webSocket?.State != WebSocketState.Open)
            {
                throw new InvalidOperationException("WebSocket is not connected");
            }

            var buffer = Encoding.UTF8.GetBytes(message);
            await _webSocket.SendAsync(new ArraySegment<byte>(buffer), WebSocketMessageType.Text, true, cancellationToken);
        }

        /// <summary>
        /// 发送二进制消息
        /// </summary>
        /// <param name="data">要发送的二进制数据</param>
        /// <param name="cancellationToken">取消令牌</param>
        /// <returns>发送任务</returns>
        public async Task SendBinaryAsync(byte[] data, CancellationToken cancellationToken = default)
        {
            if (_webSocket?.State != WebSocketState.Open)
            {
                throw new InvalidOperationException("WebSocket is not connected");
            }

            await _webSocket.SendAsync(new ArraySegment<byte>(data), WebSocketMessageType.Binary, true, cancellationToken);
        }

        /// <summary>
        /// 发送心跳包（Ping帧）
        /// </summary>
        /// <param name="cancellationToken">取消令牌</param>
        /// <returns>发送任务</returns>
        public async Task SendPingAsync(CancellationToken cancellationToken = default)
        {
            if (_webSocket?.State != WebSocketState.Open)
            {
                throw new InvalidOperationException("WebSocket is not connected");
            }

            await _webSocket.SendAsync(new ArraySegment<byte>(Array.Empty<byte>()), WebSocketMessageType.Binary, true, cancellationToken);
        }

        #endregion

        #region 私有方法

        /// <summary>
        /// 接收消息循环
        /// </summary>
        /// <param name="cancellationToken">取消令牌</param>
        /// <returns>接收任务</returns>
        private async Task ReceiveMessagesAsync(CancellationToken cancellationToken)
        {
            var buffer = new byte[1024 * 4]; // 4KB缓冲区

            try
            {
                while (!cancellationToken.IsCancellationRequested && _webSocket?.State == WebSocketState.Open)
                {
                    var result = await _webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), cancellationToken);

                    if (result.MessageType == WebSocketMessageType.Close)
                    {
                        await DisconnectAsync(result.CloseStatus ?? WebSocketCloseStatus.NormalClosure, result.CloseStatusDescription ?? "Server closed");
                        break;
                    }

                    if (result.MessageType == WebSocketMessageType.Text)
                    {
                        var message = Encoding.UTF8.GetString(buffer, 0, result.Count);
                        OnMessageReceived?.Invoke(this, message);
                    }
                }
            }
            catch (OperationCanceledException)
            {
                // 正常取消，无需处理
            }
            catch (Exception ex)
            {
                OnError?.Invoke(this, ex);

                // 如果不是主动断开连接，则尝试重连
                if (_webSocket?.State != WebSocketState.Closed)
                {
                    await HandleReconnectAsync();
                }
            }
        }

        /// <summary>
        /// 心跳检测循环
        /// </summary>
        /// <param name="cancellationToken">取消令牌</param>
        /// <returns>心跳检测任务</returns>
        private async Task HeartbeatAsync(CancellationToken cancellationToken)
        {
            try
            {
                while (!cancellationToken.IsCancellationRequested && _webSocket?.State == WebSocketState.Open)
                {
                    await Task.Delay(_heartbeatInterval, cancellationToken);

                    if (cancellationToken.IsCancellationRequested)
                        break;

                    await SendPingAsync(cancellationToken);
                    OnHeartbeat?.Invoke(this, EventArgs.Empty);
                }
            }
            catch (OperationCanceledException)
            {
                // 正常取消，无需处理
            }
            catch (Exception ex)
            {
                OnError?.Invoke(this, ex);
            }
        }

        /// <summary>
        /// 处理重连逻辑
        /// </summary>
        /// <returns>重连任务</returns>
        private async Task HandleReconnectAsync()
        {
            if (_reconnectAttempts < _maxReconnectAttempts)
            {
                _reconnectAttempts++;
                await Task.Delay(_reconnectDelay);
                // 注意：这里需要外部提供URI，实际使用中可能需要保存URI
                // await ConnectAsync(_uri);
            }
            else
            {
                await DisconnectAsync(WebSocketCloseStatus.InternalServerError, "Max reconnect attempts reached");
            }
        }

        /// <summary>
        /// 清理资源
        /// </summary>
        private void Cleanup()
        {
            try
            {
                _heartbeatCts?.Cancel();
                _receiveCts?.Cancel();
            }
            catch
            {
                // 忽略取消时的异常
            }

            _webSocket?.Dispose();
            _webSocket = null;
            _state = WebSocketState.Closed;
        }

        #endregion

        #region 析构函数和IDisposable实现

        /// <summary>
        /// 析构函数，确保资源被释放
        /// </summary>
        ~WebSocketHelper()
        {
            Cleanup();
        }

        /// <summary>
        /// 释放资源
        /// </summary>
        public void Dispose()
        {
            Cleanup();
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}