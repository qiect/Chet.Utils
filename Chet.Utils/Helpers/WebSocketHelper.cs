using System.Collections.Concurrent;
using System.Net;
using System.Net.WebSockets;
using System.Text;
namespace Chet.Utils.Helpers
{
    /// <summary>
    /// WebSocket客户端帮助类，提供WebSocket连接管理、消息发送、心跳检测等功能
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

        /// <summary>
        /// 标志位，表示是否是主动断开连接
        /// 用于避免在DisconnectAsync和ReceiveMessagesAsync之间重复触发OnDisconnected事件
        /// </summary>
        private bool _hasDisconnected = false;

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

                // 重置断开连接状态标志
                ResetDisconnectionState();

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
            if (_webSocket == null || _state == WebSocketState.Closed || _hasDisconnected)
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
                // 清理资源
                Cleanup();
                // 触发断开连接事件
                if (!_hasDisconnected)
                {
                    _hasDisconnected = true;
                    OnDisconnected?.Invoke(this, closeStatus);
                }
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
                        // 当接收到服务器的关闭消息时，确保只触发一次断开连接事件
                        var closeStatus = result.CloseStatus ?? WebSocketCloseStatus.NormalClosure;
                        try
                        {
                            // 确认关闭
                            if (_webSocket.State != WebSocketState.Closed && _webSocket.State != WebSocketState.Aborted)
                            {
                                await _webSocket.CloseAsync(closeStatus, result.CloseStatusDescription ?? "Server closed", CancellationToken.None);
                            }
                        }
                        catch (Exception ex)
                        {
                            OnError?.Invoke(this, ex);
                        }
                        finally
                        {
                            Cleanup();
                            // 确保只触发一次断开连接事件
                            if (!_hasDisconnected)
                            {
                                _hasDisconnected = true;
                                OnDisconnected?.Invoke(this, closeStatus);
                            }
                        }
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

        /// <summary>
        /// 重置断开连接状态标志，在重新连接前调用
        /// </summary>
        private void ResetDisconnectionState()
        {
            _hasDisconnected = false;
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

    /// <summary>
    /// WebSocket服务器帮助类，提供WebSocket服务器功能、连接管理、消息广播等
    /// </summary>
    public class WebSocketServerHelper : IDisposable
    {
        #region 私有字段

        /// <summary>
        /// HTTP监听器
        /// </summary>
        private HttpListener _httpListener;

        /// <summary>
        /// 已连接的客户端字典，键为客户端ID
        /// </summary>
        private readonly ConcurrentDictionary<string, WebSocket> _connectedClients;

        /// <summary>
        /// 客户端信息字典，存储额外的客户端信息
        /// </summary>
        private readonly ConcurrentDictionary<string, ClientInfo> _clientInfos;

        /// <summary>
        /// 服务器状态
        /// </summary>
        private bool _isRunning;

        /// <summary>
        /// 心跳检测间隔（毫秒）
        /// </summary>
        private readonly int _heartbeatInterval;

        /// <summary>
        /// 默认服务器端口
        /// </summary>
        private int _defaultPort = 8080;

        /// <summary>
        /// 心跳检测任务
        /// </summary>
        private Task _heartbeatTask;

        /// <summary>
        /// 取消令牌源
        /// </summary>
        private CancellationTokenSource _cts;

        /// <summary>
        /// 客户端ID生成器
        /// </summary>
        private int _clientIdCounter;

        /// <summary>
        /// 客户端ID锁
        /// </summary>
        private readonly object _clientIdLock = new object();

        #endregion

        #region 公共属性

        /// <summary>
        /// 获取当前连接的客户端数量
        /// </summary>
        public int ConnectedClientCount => _connectedClients.Count;

        /// <summary>
        /// 获取服务器是否正在运行
        /// </summary>
        public bool IsRunning => _isRunning;

        /// <summary>
        /// 获取所有已连接的客户端ID列表
        /// </summary>
        public IEnumerable<string> ConnectedClientIds => _connectedClients.Keys;

        #endregion

        #region 事件定义

        /// <summary>
        /// 服务器启动事件
        /// </summary>
        public event EventHandler OnServerStarted;

        /// <summary>
        /// 服务器停止事件
        /// </summary>
        public event EventHandler OnServerStopped;

        /// <summary>
        /// 客户端连接事件
        /// </summary>
        public event EventHandler<ClientConnectedEventArgs> OnClientConnected;

        /// <summary>
        /// 客户端断开连接事件
        /// </summary>
        public event EventHandler<ClientDisconnectedEventArgs> OnClientDisconnected;

        /// <summary>
        /// 收到消息事件
        /// </summary>
        public event EventHandler<MessageReceivedEventArgs> OnMessageReceived;

        /// <summary>
        /// 发送消息事件
        /// </summary>
        public event EventHandler<MessageSentEventArgs> OnMessageSent;

        /// <summary>
        /// 心跳检测事件
        /// </summary>
        public event EventHandler<HeartbeatEventArgs> OnHeartbeat;

        /// <summary>
        /// 发生错误事件
        /// </summary>
        public event EventHandler<WebSocketErrorEventArgs> OnError;

        #endregion

        #region 构造函数

        /// <summary>
        /// 初始化WebSocketServerHelper实例
        /// </summary>
        /// <param name="heartbeatInterval">心跳检测间隔（毫秒），默认30秒，0表示禁用心跳</param>
        public WebSocketServerHelper(int heartbeatInterval = 30000)
        {
            _heartbeatInterval = heartbeatInterval;
            _connectedClients = new ConcurrentDictionary<string, WebSocket>();
            _clientInfos = new ConcurrentDictionary<string, ClientInfo>();
            _cts = new CancellationTokenSource();
        }

        /// <summary>
        /// 初始化WebSocketServerHelper实例
        /// </summary>
        /// <param name="port">服务器端口</param>
        /// <param name="heartbeatIntervalSec">心跳检测间隔（秒），默认30秒，0表示禁用心跳</param>
        public WebSocketServerHelper(int port, int heartbeatIntervalSec = 30)
        {
            _defaultPort = port;
            _heartbeatInterval = heartbeatIntervalSec * 1000;
            _connectedClients = new ConcurrentDictionary<string, WebSocket>();
            _clientInfos = new ConcurrentDictionary<string, ClientInfo>();
            _cts = new CancellationTokenSource();
        }

        #endregion

        #region 公共方法

        /// <summary>
        /// 启动WebSocket服务器
        /// </summary>
        /// <param name="prefixes">监听的URL前缀列表，例如：http://localhost:8080/</param>
        /// <returns>启动任务</returns>
        public async Task StartServerAsync(IEnumerable<string> prefixes)
        {
            if (_isRunning)
            {
                throw new InvalidOperationException("服务器已经在运行中");
            }

            try
            {
                _httpListener = new HttpListener();

                foreach (var prefix in prefixes)
                {
                    _httpListener.Prefixes.Add(prefix);
                }

                _httpListener.Start();
                _isRunning = true;

                // 触发服务器启动事件
                OnServerStarted?.Invoke(this, EventArgs.Empty);

                // 启动心跳检测（如果启用）
                if (_heartbeatInterval > 0)
                {
                    _heartbeatTask = Task.Run(() => HeartbeatLoopAsync(_cts.Token));
                }

                // 开始接受客户端连接
                await AcceptWebSocketConnectionsAsync(_cts.Token);
            }
            catch (Exception ex)
            {
                // 如果不是取消操作，则触发错误事件
                if (!(_cts.Token.IsCancellationRequested && ex is OperationCanceledException))
                {
                    OnError?.Invoke(this, new WebSocketErrorEventArgs(null, ex));
                }
                StopServer();
                throw;
            }
        }

        /// <summary>
        /// 启动WebSocket服务器
        /// </summary>
        /// <param name="prefix">监听的URL前缀，例如：http://localhost:8080/</param>
        /// <returns>启动任务</returns>
        public Task StartServerAsync(string prefix)
        {
            return StartServerAsync(new[] { prefix });
        }

        /// <summary>
        /// 停止WebSocket服务器
        /// </summary>
        public void StopServer()
        {
            if (!_isRunning)
                return;

            try
            {
                // 取消所有任务
                _cts.Cancel();

                // 关闭所有客户端连接
                foreach (var clientId in _connectedClients.Keys)
                {
                    DisconnectClient(clientId, WebSocketCloseStatus.NormalClosure, "Server shutting down");
                }

                // 停止HTTP监听器
                if (_httpListener != null && _httpListener.IsListening)
                {
                    _httpListener.Stop();
                    _httpListener.Close();
                }

                _isRunning = false;

                // 触发服务器停止事件
                OnServerStopped?.Invoke(this, EventArgs.Empty);
            }
            catch (Exception ex)
            {
                OnError?.Invoke(this, new WebSocketErrorEventArgs(null, ex));
            }
            finally
            {
                // 清理资源
                _connectedClients.Clear();
                _clientInfos.Clear();
                _cts.Dispose();
                _cts = new CancellationTokenSource();
            }
        }

        /// <summary>
        /// 向指定客户端发送消息
        /// </summary>
        /// <param name="clientId">客户端ID</param>
        /// <param name="message">消息内容</param>
        /// <returns>发送任务</returns>
        public async Task SendMessageToClientAsync(string clientId, string message)
        {
            if (!_connectedClients.TryGetValue(clientId, out var webSocket))
            {
                throw new ArgumentException($"客户端ID {clientId} 不存在或已断开连接");
            }

            if (webSocket.State != WebSocketState.Open)
            {
                throw new InvalidOperationException($"客户端 {clientId} 连接状态异常: {webSocket.State}");
            }

            try
            {
                var buffer = Encoding.UTF8.GetBytes(message);
                await webSocket.SendAsync(new ArraySegment<byte>(buffer), WebSocketMessageType.Text, true, _cts.Token);

                // 触发消息发送事件
                OnMessageSent?.Invoke(this, new MessageSentEventArgs(clientId, message));
            }
            catch (Exception ex)
            {
                OnError?.Invoke(this, new WebSocketErrorEventArgs(clientId, ex));
                DisconnectClient(clientId);
                throw;
            }
        }

        /// <summary>
        /// 向指定客户端发送二进制消息
        /// </summary>
        /// <param name="clientId">客户端ID</param>
        /// <param name="data">二进制数据</param>
        /// <returns>发送任务</returns>
        public async Task SendBinaryToClientAsync(string clientId, byte[] data)
        {
            if (!_connectedClients.TryGetValue(clientId, out var webSocket))
            {
                throw new ArgumentException($"客户端ID {clientId} 不存在或已断开连接");
            }

            if (webSocket.State != WebSocketState.Open)
            {
                throw new InvalidOperationException($"客户端 {clientId} 连接状态异常: {webSocket.State}");
            }

            try
            {
                await webSocket.SendAsync(new ArraySegment<byte>(data), WebSocketMessageType.Binary, true, _cts.Token);

                // 触发消息发送事件
                OnMessageSent?.Invoke(this, new MessageSentEventArgs(clientId, "[Binary Data]", true));
            }
            catch (Exception ex)
            {
                OnError?.Invoke(this, new WebSocketErrorEventArgs(clientId, ex));
                DisconnectClient(clientId);
                throw;
            }
        }

        /// <summary>
        /// 广播消息给所有连接的客户端
        /// </summary>
        /// <param name="message">消息内容</param>
        /// <returns>广播任务</returns>
        public Task BroadcastMessageAsync(string message)
        {
            return BroadcastMessageAsync(message, null);
        }

        /// <summary>
        /// 广播消息给所有连接的客户端，除了指定的客户端
        /// </summary>
        /// <param name="message">消息内容</param>
        /// <param name="excludeClientId">要排除的客户端ID</param>
        /// <returns>广播任务</returns>
        public async Task BroadcastMessageAsync(string message, string excludeClientId)
        {
            var tasks = new List<Task>();

            foreach (var clientId in _connectedClients.Keys)
            {
                if (clientId == excludeClientId)
                    continue;

                tasks.Add(SendMessageToClientAsync(clientId, message).ContinueWith(t =>
                {
                    if (t.IsFaulted && t.Exception != null)
                    {
                        OnError?.Invoke(this, new WebSocketErrorEventArgs(clientId, t.Exception.InnerException));
                    }
                }));
            }

            await Task.WhenAll(tasks);
        }

        /// <summary>
        /// 断开指定客户端连接
        /// </summary>
        /// <param name="clientId">客户端ID</param>
        /// <param name="closeStatus">关闭状态</param>
        /// <param name="statusDescription">状态描述</param>
        public void DisconnectClient(string clientId, WebSocketCloseStatus closeStatus = WebSocketCloseStatus.NormalClosure, string statusDescription = "Normal closure")
        {
            if (!_connectedClients.TryRemove(clientId, out var webSocket))
                return;

            try
            {
                if (webSocket.State == WebSocketState.Open || webSocket.State == WebSocketState.CloseReceived)
                {
                    webSocket.CloseAsync(closeStatus, statusDescription, CancellationToken.None).Wait();
                }
                webSocket.Dispose();
            }
            catch (Exception ex)
            {
                OnError?.Invoke(this, new WebSocketErrorEventArgs(clientId, ex));
            }
            finally
            {
                // 移除客户端信息
                _clientInfos.TryRemove(clientId, out _);

                // 触发客户端断开连接事件
                OnClientDisconnected?.Invoke(this, new ClientDisconnectedEventArgs(clientId, closeStatus, statusDescription));
            }
        }

        /// <summary>
        /// 获取客户端信息
        /// </summary>
        /// <param name="clientId">客户端ID</param>
        /// <returns>客户端信息，如果客户端不存在则返回null</returns>
        public ClientInfo GetClientInfo(string clientId)
        {
            _clientInfos.TryGetValue(clientId, out var clientInfo);
            return clientInfo;
        }

        /// <summary>
        /// 设置客户端自定义信息
        /// </summary>
        /// <param name="clientId">客户端ID</param>
        /// <param name="customInfo">自定义信息</param>
        public void SetClientCustomInfo(string clientId, object customInfo)
        {
            if (_clientInfos.TryGetValue(clientId, out var clientInfo))
            {
                clientInfo.CustomInfo = customInfo;
            }
        }

        #endregion

        #region 私有方法

        /// <summary>
        /// 接受WebSocket连接的循环
        /// </summary>
        /// <param name="cancellationToken">取消令牌</param>
        /// <returns>任务</returns>
        private async Task AcceptWebSocketConnectionsAsync(CancellationToken cancellationToken)
        {
            try
            {
                while (!cancellationToken.IsCancellationRequested && _httpListener.IsListening)
                {
                    try
                    {
                        var context = await _httpListener.GetContextAsync();

                        // 检查是否是WebSocket请求
                        if (context.Request.IsWebSocketRequest)
                        {
                            _ = Task.Run(() => HandleWebSocketRequestAsync(context, cancellationToken));
                        }
                        else
                        {
                            context.Response.StatusCode = 400;
                            context.Response.Close();
                        }
                    }
                    catch (Exception ex)
                    {
                        // 忽略单个连接的错误，继续接受其他连接
                        OnError?.Invoke(this, new WebSocketErrorEventArgs(null, ex));
                    }
                }
            }
            catch (OperationCanceledException)
            {
                // 正常取消，无需处理
            }
        }

        /// <summary>
        /// 处理WebSocket请求
        /// </summary>
        /// <param name="context">HTTP上下文</param>
        /// <param name="cancellationToken">取消令牌</param>
        /// <returns>任务</returns>
        private async Task HandleWebSocketRequestAsync(HttpListenerContext context, CancellationToken cancellationToken)
        {
            WebSocketContext webSocketContext = null;
            WebSocket webSocket = null;
            string clientId = null;

            try
            {
                // 接受WebSocket连接
                webSocketContext = await context.AcceptWebSocketAsync(null);
                webSocket = webSocketContext.WebSocket;

                // 生成客户端ID
                clientId = GenerateClientId();

                // 创建客户端信息
                var clientInfo = new ClientInfo
                {
                    ClientId = clientId,
                    ConnectedTime = DateTime.Now,
                    RemoteEndPoint = context.Request.RemoteEndPoint,
                    UserAgent = context.Request.UserAgent,
                    LastHeartbeatTime = DateTime.Now
                };

                // 添加到客户端列表
                _connectedClients.TryAdd(clientId, webSocket);
                _clientInfos.TryAdd(clientId, clientInfo);

                // 触发客户端连接事件
                OnClientConnected?.Invoke(this, new ClientConnectedEventArgs(clientInfo));

                // 处理客户端消息
                await HandleClientMessagesAsync(clientId, webSocket, cancellationToken);
            }
            catch (Exception ex)
            {
                OnError?.Invoke(this, new WebSocketErrorEventArgs(clientId, ex));
            }
            finally
            {
                // 确保断开连接并清理资源
                if (clientId != null)
                {
                    DisconnectClient(clientId, WebSocketCloseStatus.InternalServerError, "Error occurred");
                }
                else if (webSocket != null)
                {
                    try
                    {
                        webSocket.Dispose();
                    }
                    catch { }
                }
            }
        }

        /// <summary>
        /// 处理客户端消息
        /// </summary>
        /// <param name="clientId">客户端ID</param>
        /// <param name="webSocket">WebSocket连接</param>
        /// <param name="cancellationToken">取消令牌</param>
        /// <returns>任务</returns>
        private async Task HandleClientMessagesAsync(string clientId, WebSocket webSocket, CancellationToken cancellationToken)
        {
            var buffer = new byte[1024 * 4]; // 4KB缓冲区

            try
            {
                while (!cancellationToken.IsCancellationRequested && webSocket.State == WebSocketState.Open)
                {
                    var result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), cancellationToken);

                    // 更新心跳时间
                    if (_clientInfos.TryGetValue(clientId, out var clientInfo))
                    {
                        clientInfo.LastHeartbeatTime = DateTime.Now;
                    }

                    // 处理关闭请求
                    if (result.MessageType == WebSocketMessageType.Close)
                    {
                        await webSocket.CloseAsync(result.CloseStatus ?? WebSocketCloseStatus.NormalClosure,
                            result.CloseStatusDescription ?? "Client closed", CancellationToken.None);
                        break;
                    }

                    // 处理文本消息
                    if (result.MessageType == WebSocketMessageType.Text)
                    {
                        var message = Encoding.UTF8.GetString(buffer, 0, result.Count);

                        // 触发消息接收事件
                        OnMessageReceived?.Invoke(this, new MessageReceivedEventArgs(clientId, message));
                    }

                    // 处理二进制消息（可以是心跳包）
                    if (result.MessageType == WebSocketMessageType.Binary)
                    {
                        // 如果是心跳包（空二进制消息）
                        if (result.Count == 0)
                        {
                            OnHeartbeat?.Invoke(this, new HeartbeatEventArgs(clientId, true));
                        }
                        else
                        {
                            // 处理其他二进制消息
                            var binaryData = new byte[result.Count];
                            Array.Copy(buffer, 0, binaryData, 0, result.Count);

                            // 触发二进制消息接收事件
                            OnMessageReceived?.Invoke(this, new MessageReceivedEventArgs(clientId, binaryData));
                        }
                    }
                }
            }
            catch (OperationCanceledException)
            {
                // 正常取消，无需处理
            }
            catch (Exception ex)
            {
                OnError?.Invoke(this, new WebSocketErrorEventArgs(clientId, ex));
            }
        }

        /// <summary>
        /// 心跳检测循环
        /// </summary>
        /// <param name="cancellationToken">取消令牌</param>
        /// <returns>任务</returns>
        private async Task HeartbeatLoopAsync(CancellationToken cancellationToken)
        {
            try
            {
                while (!cancellationToken.IsCancellationRequested)
                {
                    await Task.Delay(_heartbeatInterval, cancellationToken);

                    // 检查客户端心跳
                    CheckClientHeartbeats();
                }
            }
            catch (OperationCanceledException)
            {
                // 正常取消，无需处理
            }
            catch (Exception ex)
            {
                OnError?.Invoke(this, new WebSocketErrorEventArgs(null, ex));
            }
        }

        /// <summary>
        /// 检查客户端心跳，断开超时的客户端
        /// </summary>
        private void CheckClientHeartbeats()
        {
            var timeoutThreshold = DateTime.Now.AddMilliseconds(-_heartbeatInterval * 3); // 允许3倍心跳间隔的超时
            var clientsToDisconnect = new List<string>();

            foreach (var clientId in _clientInfos.Keys)
            {
                if (_clientInfos.TryGetValue(clientId, out var clientInfo))
                {
                    if (clientInfo.LastHeartbeatTime < timeoutThreshold)
                    {
                        clientsToDisconnect.Add(clientId);
                        OnHeartbeat?.Invoke(this, new HeartbeatEventArgs(clientId, false, "Heartbeat timeout"));
                    }
                }
            }

            // 断开超时的客户端
            foreach (var clientId in clientsToDisconnect)
            {
                DisconnectClient(clientId, WebSocketCloseStatus.EndpointUnavailable, "Heartbeat timeout");
            }
        }

        /// <summary>
        /// 生成客户端ID
        /// </summary>
        /// <returns>唯一的客户端ID</returns>
        private string GenerateClientId()
        {
            lock (_clientIdLock)
            {
                _clientIdCounter++;
                return $"client_{_clientIdCounter}_{Guid.NewGuid().ToString().Substring(0, 8)}";
            }
        }

        #endregion

        #region IDisposable实现

        /// <summary>
        /// 释放资源
        /// </summary>
        public void Dispose()
        {
            StopServer();
        }

        #endregion
    }

    #region 事件参数类

    /// <summary>
    /// 客户端信息类
    /// </summary>
    public class ClientInfo
    {
        /// <summary>
        /// 客户端ID
        /// </summary>
        public string ClientId { get; internal set; }

        /// <summary>
        /// 连接时间
        /// </summary>
        public DateTime ConnectedTime { get; internal set; }

        /// <summary>
        /// 远程端点
        /// </summary>
        public EndPoint RemoteEndPoint { get; internal set; }

        /// <summary>
        /// User-Agent
        /// </summary>
        public string UserAgent { get; internal set; }

        /// <summary>
        /// 最后心跳时间
        /// </summary>
        public DateTime LastHeartbeatTime { get; internal set; }

        /// <summary>
        /// 自定义信息
        /// </summary>
        public object CustomInfo { get; set; }
    }

    /// <summary>
    /// 客户端连接事件参数
    /// </summary>
    public class ClientConnectedEventArgs : EventArgs
    {
        /// <summary>
        /// 客户端信息
        /// </summary>
        public ClientInfo ClientInfo { get; }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="clientInfo">客户端信息</param>
        public ClientConnectedEventArgs(ClientInfo clientInfo)
        {
            ClientInfo = clientInfo;
        }
    }

    /// <summary>
    /// 客户端断开连接事件参数
    /// </summary>
    public class ClientDisconnectedEventArgs : EventArgs
    {
        /// <summary>
        /// 客户端ID
        /// </summary>
        public string ClientId { get; }

        /// <summary>
        /// 关闭状态
        /// </summary>
        public WebSocketCloseStatus CloseStatus { get; }

        /// <summary>
        /// 状态描述
        /// </summary>
        public string StatusDescription { get; }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="clientId">客户端ID</param>
        /// <param name="closeStatus">关闭状态</param>
        /// <param name="statusDescription">状态描述</param>
        public ClientDisconnectedEventArgs(string clientId, WebSocketCloseStatus closeStatus, string statusDescription)
        {
            ClientId = clientId;
            CloseStatus = closeStatus;
            StatusDescription = statusDescription;
        }
    }

    /// <summary>
    /// 消息接收事件参数
    /// </summary>
    public class MessageReceivedEventArgs : EventArgs
    {
        /// <summary>
        /// 客户端ID
        /// </summary>
        public string ClientId { get; }

        /// <summary>
        /// 消息内容
        /// </summary>
        public string Message { get; }

        /// <summary>
        /// 二进制数据
        /// </summary>
        public byte[] BinaryData { get; }

        /// <summary>
        /// 是否是二进制消息
        /// </summary>
        public bool IsBinary => BinaryData != null;

        /// <summary>
        /// 构造函数（文本消息）
        /// </summary>
        /// <param name="clientId">客户端ID</param>
        /// <param name="message">消息内容</param>
        public MessageReceivedEventArgs(string clientId, string message)
        {
            ClientId = clientId;
            Message = message;
        }

        /// <summary>
        /// 构造函数（二进制消息）
        /// </summary>
        /// <param name="clientId">客户端ID</param>
        /// <param name="binaryData">二进制数据</param>
        public MessageReceivedEventArgs(string clientId, byte[] binaryData)
        {
            ClientId = clientId;
            BinaryData = binaryData;
        }
    }

    /// <summary>
    /// 消息发送事件参数
    /// </summary>
    public class MessageSentEventArgs : EventArgs
    {
        /// <summary>
        /// 客户端ID
        /// </summary>
        public string ClientId { get; }

        /// <summary>
        /// 消息内容
        /// </summary>
        public string Message { get; }

        /// <summary>
        /// 是否是二进制消息
        /// </summary>
        public bool IsBinary { get; }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="clientId">客户端ID</param>
        /// <param name="message">消息内容</param>
        /// <param name="isBinary">是否是二进制消息</param>
        public MessageSentEventArgs(string clientId, string message, bool isBinary = false)
        {
            ClientId = clientId;
            Message = message;
            IsBinary = isBinary;
        }
    }

    /// <summary>
    /// 心跳事件参数
    /// </summary>
    public class HeartbeatEventArgs : EventArgs
    {
        /// <summary>
        /// 客户端ID
        /// </summary>
        public string ClientId { get; }

        /// <summary>
        /// 是否正常心跳
        /// </summary>
        public bool IsNormal { get; }

        /// <summary>
        /// 心跳消息
        /// </summary>
        public string Message { get; }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="clientId">客户端ID</param>
        /// <param name="isNormal">是否正常心跳</param>
        /// <param name="message">心跳消息</param>
        public HeartbeatEventArgs(string clientId, bool isNormal, string message = null)
        {
            ClientId = clientId;
            IsNormal = isNormal;
            Message = message;
        }
    }

    /// <summary>
    /// WebSocket错误事件参数
    /// </summary>
    public class WebSocketErrorEventArgs : EventArgs
    {
        /// <summary>
        /// 客户端ID（可能为null）
        /// </summary>
        public string ClientId { get; }

        /// <summary>
        /// 异常信息
        /// </summary>
        public Exception Exception { get; }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="clientId">客户端ID</param>
        /// <param name="exception">异常信息</param>
        public WebSocketErrorEventArgs(string clientId, Exception exception)
        {
            ClientId = clientId;
            Exception = exception;
        }
    }

    #endregion
}