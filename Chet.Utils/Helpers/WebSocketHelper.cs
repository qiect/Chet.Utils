using System.Collections.Concurrent;
using System.Net;
using System.Net.WebSockets;
using System.Text;
using System.Buffers;

namespace Chet.Utils.Helpers;

/// <summary>
/// WebSocket客户端帮助类，提供WebSocket连接管理、消息发送、心跳检测、自动重连等功能。
/// </summary>
/// <remarks>
/// <para>支持的功能：</para>
/// <list type="bullet">
///   <item><description>连接管理：自动连接、断线重连、连接状态监控</description></item>
///   <item><description>消息发送：文本消息、二进制消息、批量发送、消息队列</description></item>
///   <item><description>心跳检测：可配置心跳间隔、自动检测连接状态</description></item>
///   <item><description>错误处理：完善的错误事件、异常重试机制</description></item>
///   <item><description>SSL/TLS：支持安全连接、证书验证</description></item>
/// </list>
/// </remarks>
/// <example>
/// <code>
/// // 创建客户端
/// var client = new WebSocketHelper(
///     heartbeatInterval: 30000,
///     maxReconnectAttempts: 5,
///     reconnectDelay: 5000);
/// 
/// // 订阅事件
/// client.OnConnected += (sender, args) => Console.WriteLine("已连接");
/// client.OnMessageReceived += (sender, message) => Console.WriteLine($"收到消息: {message}");
/// client.OnDisconnected += (sender, status) => Console.WriteLine($"已断开: {status}");
/// client.OnError += (sender, ex) => Console.WriteLine($"错误: {ex.Message}");
/// 
/// // 连接服务器
/// await client.ConnectAsync(new Uri("ws://localhost:8080"));
/// 
/// // 发送消息
/// await client.SendMessageAsync("Hello, World!");
/// 
/// // 断开连接
/// await client.DisconnectAsync();
/// </code>
/// </example>
public class WebSocketHelper : IDisposable
{
    #region 私有字段

    private ClientWebSocket _webSocket;
    private WebSocketState _state;
    private readonly int _heartbeatInterval;
    private readonly int _maxReconnectAttempts;
    private readonly int _reconnectDelay;
    private readonly int _receiveBufferSize;
    private readonly int _sendBufferSize;
    private readonly TimeSpan _connectionTimeout;
    private CancellationTokenSource _heartbeatCts;
    private CancellationTokenSource _receiveCts;
    private readonly object _connectLock = new object();
    private int _reconnectAttempts;
    private bool _hasDisconnected;
    private Uri _lastConnectedUri;
    private readonly ConcurrentQueue<string> _messageQueue;
    private readonly SemaphoreSlim _sendLock = new SemaphoreSlim(1, 1);
    private bool _isDisposed;
    private readonly Action<ClientWebSocketOptions> _configureOptions;

    #endregion

    #region 公共属性

    /// <summary>
    /// 获取当前WebSocket连接状态。
    /// </summary>
    public WebSocketState State => _state;

    /// <summary>
    /// 获取是否已连接。
    /// </summary>
    public bool IsConnected => _webSocket?.State == WebSocketState.Open;

    /// <summary>
    /// 获取当前连接的URI。
    /// </summary>
    public Uri ConnectedUri => _lastConnectedUri;

    /// <summary>
    /// 获取或设置是否启用自动重连。
    /// </summary>
    public bool AutoReconnect { get; set; } = true;

    /// <summary>
    /// 获取或设置是否启用消息队列（在断开连接时缓存消息）。
    /// </summary>
    public bool EnableMessageQueue { get; set; } = true;

    /// <summary>
    /// 获取消息队列中的消息数量。
    /// </summary>
    public int QueuedMessageCount => _messageQueue.Count;

    #endregion

    #region 事件定义

    /// <summary>
    /// 连接成功事件。
    /// </summary>
    public event EventHandler<WebSocketConnectedEventArgs> OnConnected;

    /// <summary>
    /// 连接断开事件。
    /// </summary>
    public event EventHandler<WebSocketDisconnectedEventArgs> OnDisconnected;

    /// <summary>
    /// 接收到文本消息事件。
    /// </summary>
    public event EventHandler<WebSocketMessageReceivedEventArgs> OnMessageReceived;

    /// <summary>
    /// 接收到二进制消息事件。
    /// </summary>
    public event EventHandler<WebSocketBinaryReceivedEventArgs> OnBinaryReceived;

    /// <summary>
    /// 发生错误事件。
    /// </summary>
    public event EventHandler<WebSocketErrorEventArgs> OnError;

    /// <summary>
    /// 心跳检测事件。
    /// </summary>
    public event EventHandler<WebSocketHeartbeatEventArgs> OnHeartbeat;

    /// <summary>
    /// 重连事件。
    /// </summary>
    public event EventHandler<WebSocketReconnectingEventArgs> OnReconnecting;

    /// <summary>
    /// 消息发送完成事件。
    /// </summary>
    public event EventHandler<WebSocketMessageSentEventArgs> OnMessageSent;

    #endregion

    #region 构造函数

    /// <summary>
    /// 初始化WebSocketHelper实例。
    /// </summary>
    /// <param name="heartbeatInterval">心跳检测间隔（毫秒），默认30秒，0表示禁用。</param>
    /// <param name="maxReconnectAttempts">最大重连次数，默认5次，0表示无限重连。</param>
    /// <param name="reconnectDelay">重连延迟时间（毫秒），默认5秒。</param>
    /// <param name="receiveBufferSize">接收缓冲区大小（字节），默认4KB。</param>
    /// <param name="sendBufferSize">发送缓冲区大小（字节），默认4KB。</param>
    /// <param name="connectionTimeout">连接超时时间，默认30秒。</param>
    /// <param name="configureOptions">WebSocket选项配置委托。</param>
    public WebSocketHelper(
        int heartbeatInterval = 30000,
        int maxReconnectAttempts = 5,
        int reconnectDelay = 5000,
        int receiveBufferSize = 4096,
        int sendBufferSize = 4096,
        TimeSpan? connectionTimeout = null,
        Action<ClientWebSocketOptions> configureOptions = null)
    {
        _heartbeatInterval = heartbeatInterval;
        _maxReconnectAttempts = maxReconnectAttempts;
        _reconnectDelay = reconnectDelay;
        _receiveBufferSize = receiveBufferSize;
        _sendBufferSize = sendBufferSize;
        _connectionTimeout = connectionTimeout ?? TimeSpan.FromSeconds(30);
        _configureOptions = configureOptions;
        _state = WebSocketState.None;
        _messageQueue = new ConcurrentQueue<string>();
    }

    #endregion

    #region 公共方法 - 连接管理

    /// <summary>
    /// 连接到WebSocket服务器。
    /// </summary>
    /// <param name="uri">WebSocket服务器地址。</param>
    /// <param name="cancellationToken">取消令牌。</param>
    /// <returns>连接任务。</returns>
    /// <exception cref="ArgumentNullException">uri为null时抛出。</exception>
    /// <exception cref="InvalidOperationException">已经在连接中时抛出。</exception>
    public async Task ConnectAsync(Uri uri, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(uri);

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

            ResetDisconnectionState();
            _webSocket = new ClientWebSocket();
            _state = WebSocketState.Connecting;
        }

        try
        {
            _configureOptions?.Invoke(_webSocket.Options);

            using var cts = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);
            cts.CancelAfter(_connectionTimeout);

            await _webSocket.ConnectAsync(uri, cts.Token);
            _state = WebSocketState.Open;
            _reconnectAttempts = 0;
            _lastConnectedUri = uri;

            _receiveCts = new CancellationTokenSource();
            _heartbeatCts = new CancellationTokenSource();

            _ = Task.Run(() => ReceiveMessagesAsync(_receiveCts.Token), _receiveCts.Token);
            
            if (_heartbeatInterval > 0)
            {
                _ = Task.Run(() => HeartbeatAsync(_heartbeatCts.Token), _heartbeatCts.Token);
            }

            OnConnected?.Invoke(this, new WebSocketConnectedEventArgs(uri));

            if (EnableMessageQueue && !_messageQueue.IsEmpty)
            {
                _ = Task.Run(() => SendQueuedMessagesAsync(), CancellationToken.None);
            }
        }
        catch (OperationCanceledException) when (cancellationToken.IsCancellationRequested)
        {
            _state = WebSocketState.Closed;
            throw;
        }
        catch (Exception ex)
        {
            _state = WebSocketState.Closed;
            OnError?.Invoke(this, new WebSocketErrorEventArgs(null, ex));

            if (AutoReconnect && ShouldReconnect())
            {
                _ = Task.Run(() => ReconnectAsync(uri, cancellationToken), cancellationToken);
            }
        }
    }

    /// <summary>
    /// 断开WebSocket连接。
    /// </summary>
    /// <param name="closeStatus">关闭状态。</param>
    /// <param name="statusDescription">状态描述。</param>
    /// <param name="cancellationToken">取消令牌。</param>
    /// <returns>断开连接任务。</returns>
    public async Task DisconnectAsync(
        WebSocketCloseStatus closeStatus = WebSocketCloseStatus.NormalClosure,
        string statusDescription = "Normal closure",
        CancellationToken cancellationToken = default)
    {
        if (_webSocket == null || _state == WebSocketState.Closed || _hasDisconnected)
        {
            return;
        }

        try
        {
            if (_webSocket.State != WebSocketState.Closed && 
                _webSocket.State != WebSocketState.Aborted)
            {
                await _webSocket.CloseAsync(closeStatus, statusDescription, cancellationToken);
            }
        }
        catch (Exception ex)
        {
            OnError?.Invoke(this, new WebSocketErrorEventArgs(null, ex));
        }
        finally
        {
            Cleanup();
            if (!_hasDisconnected)
            {
                _hasDisconnected = true;
                OnDisconnected?.Invoke(this, new WebSocketDisconnectedEventArgs(closeStatus, statusDescription));
            }
        }
    }

    /// <summary>
    /// 重新连接到WebSocket服务器。
    /// </summary>
    /// <param name="cancellationToken">取消令牌。</param>
    /// <returns>重连任务。</returns>
    public async Task ReconnectAsync(CancellationToken cancellationToken = default)
    {
        if (_lastConnectedUri == null)
        {
            throw new InvalidOperationException("没有可重连的URI");
        }

        await ReconnectAsync(_lastConnectedUri, cancellationToken);
    }

    /// <summary>
    /// 重新连接到指定的WebSocket服务器。
    /// </summary>
    /// <param name="uri">WebSocket服务器地址。</param>
    /// <param name="cancellationToken">取消令牌。</param>
    /// <returns>重连任务。</returns>
    private async Task ReconnectAsync(Uri uri, CancellationToken cancellationToken)
    {
        while (ShouldReconnect())
        {
            _reconnectAttempts++;
            
            OnReconnecting?.Invoke(this, new WebSocketReconnectingEventArgs(_reconnectAttempts, _maxReconnectAttempts));

            await Task.Delay(_reconnectDelay, cancellationToken);

            try
            {
                await ConnectAsync(uri, cancellationToken);
                return;
            }
            catch (Exception ex)
            {
                OnError?.Invoke(this, new WebSocketErrorEventArgs(null, ex));
            }
        }

        await DisconnectAsync(WebSocketCloseStatus.InternalServerError, "Max reconnect attempts reached");
    }

    #endregion

    #region 公共方法 - 消息发送

    /// <summary>
    /// 发送文本消息。
    /// </summary>
    /// <param name="message">要发送的消息。</param>
    /// <param name="cancellationToken">取消令牌。</param>
    /// <returns>发送任务。</returns>
    /// <exception cref="ArgumentNullException">message为null时抛出。</exception>
    /// <exception cref="InvalidOperationException">未连接时抛出。</exception>
    public async Task SendMessageAsync(string message, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(message);

        if (!IsConnected)
        {
            if (EnableMessageQueue)
            {
                _messageQueue.Enqueue(message);
                return;
            }
            throw new InvalidOperationException("WebSocket未连接");
        }

        await _sendLock.WaitAsync(cancellationToken);
        try
        {
            var buffer = Encoding.UTF8.GetBytes(message);
            await _webSocket.SendAsync(
                new ArraySegment<byte>(buffer),
                WebSocketMessageType.Text,
                true,
                cancellationToken);

            OnMessageSent?.Invoke(this, new WebSocketMessageSentEventArgs(message, false));
        }
        finally
        {
            _sendLock.Release();
        }
    }

    /// <summary>
    /// 发送二进制消息。
    /// </summary>
    /// <param name="data">要发送的二进制数据。</param>
    /// <param name="cancellationToken">取消令牌。</param>
    /// <returns>发送任务。</returns>
    /// <exception cref="ArgumentNullException">data为null时抛出。</exception>
    /// <exception cref="InvalidOperationException">未连接时抛出。</exception>
    public async Task SendBinaryAsync(byte[] data, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(data);

        if (!IsConnected)
        {
            throw new InvalidOperationException("WebSocket未连接");
        }

        await _sendLock.WaitAsync(cancellationToken);
        try
        {
            await _webSocket.SendAsync(
                new ArraySegment<byte>(data),
                WebSocketMessageType.Binary,
                true,
                cancellationToken);

            OnMessageSent?.Invoke(this, new WebSocketMessageSentEventArgs("[Binary Data]", true));
        }
        finally
        {
            _sendLock.Release();
        }
    }

    /// <summary>
    /// 批量发送文本消息。
    /// </summary>
    /// <param name="messages">要发送的消息列表。</param>
    /// <param name="cancellationToken">取消令牌。</param>
    /// <returns>发送任务。</returns>
    /// <exception cref="ArgumentNullException">messages为null时抛出。</exception>
    public async Task SendMessagesAsync(IEnumerable<string> messages, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(messages);

        foreach (var message in messages)
        {
            await SendMessageAsync(message, cancellationToken);
        }
    }

    /// <summary>
    /// 清空消息队列。
    /// </summary>
    public void ClearMessageQueue()
    {
        while (_messageQueue.TryDequeue(out _)) { }
    }

    #endregion

    #region 私有方法

    /// <summary>
    /// 接收消息循环。
    /// </summary>
    private async Task ReceiveMessagesAsync(CancellationToken cancellationToken)
    {
        var buffer = ArrayPool<byte>.Shared.Rent(_receiveBufferSize);
        var messageBuilder = new StringBuilder();
        var binaryBuffer = new List<byte>();

        try
        {
            while (!cancellationToken.IsCancellationRequested && 
                   _webSocket?.State == WebSocketState.Open)
            {
                var result = await _webSocket.ReceiveAsync(
                    new ArraySegment<byte>(buffer),
                    cancellationToken);

                if (result.MessageType == WebSocketMessageType.Close)
                {
                    await HandleCloseMessageAsync(result, cancellationToken);
                    break;
                }

                if (result.MessageType == WebSocketMessageType.Text)
                {
                    var chunk = Encoding.UTF8.GetString(buffer, 0, result.Count);
                    messageBuilder.Append(chunk);

                    if (result.EndOfMessage)
                    {
                        var message = messageBuilder.ToString();
                        messageBuilder.Clear();
                        OnMessageReceived?.Invoke(this, new WebSocketMessageReceivedEventArgs(message));
                    }
                }
                else if (result.MessageType == WebSocketMessageType.Binary)
                {
                    binaryBuffer.AddRange(new ArraySegment<byte>(buffer, 0, result.Count));

                    if (result.EndOfMessage)
                    {
                        var data = binaryBuffer.ToArray();
                        binaryBuffer.Clear();
                        OnBinaryReceived?.Invoke(this, new WebSocketBinaryReceivedEventArgs(data));
                    }
                }
            }
        }
        catch (OperationCanceledException)
        {
        }
        catch (Exception ex)
        {
            OnError?.Invoke(this, new WebSocketErrorEventArgs(null, ex));

            if (!cancellationToken.IsCancellationRequested && AutoReconnect && _lastConnectedUri != null)
            {
                _ = ReconnectAsync(_lastConnectedUri, CancellationToken.None);
            }
        }
        finally
        {
            ArrayPool<byte>.Shared.Return(buffer);
        }
    }

    /// <summary>
    /// 处理关闭消息。
    /// </summary>
    private async Task HandleCloseMessageAsync(WebSocketReceiveResult result, CancellationToken cancellationToken)
    {
        var closeStatus = result.CloseStatus ?? WebSocketCloseStatus.NormalClosure;
        var closeDescription = result.CloseStatusDescription ?? "Server closed";

        try
        {
            if (_webSocket.State != WebSocketState.Closed && 
                _webSocket.State != WebSocketState.Aborted)
            {
                await _webSocket.CloseAsync(
                    closeStatus,
                    closeDescription,
                    cancellationToken);
            }
        }
        catch (Exception ex)
        {
            OnError?.Invoke(this, new WebSocketErrorEventArgs(null, ex));
        }
        finally
        {
            Cleanup();
            if (!_hasDisconnected)
            {
                _hasDisconnected = true;
                OnDisconnected?.Invoke(this, new WebSocketDisconnectedEventArgs(closeStatus, closeDescription));
            }
        }
    }

    /// <summary>
    /// 心跳检测循环。
    /// </summary>
    private async Task HeartbeatAsync(CancellationToken cancellationToken)
    {
        try
        {
            while (!cancellationToken.IsCancellationRequested && 
                   _webSocket?.State == WebSocketState.Open)
            {
                await Task.Delay(_heartbeatInterval, cancellationToken);

                if (cancellationToken.IsCancellationRequested || !IsConnected)
                {
                    break;
                }

                try
                {
                    await _sendLock.WaitAsync(cancellationToken);
                    try
                    {
                        await _webSocket.SendAsync(
                            Array.Empty<byte>(),
                            WebSocketMessageType.Binary,
                            true,
                            cancellationToken);
                    }
                    finally
                    {
                        _sendLock.Release();
                    }

                    OnHeartbeat?.Invoke(this, new WebSocketHeartbeatEventArgs(true, DateTime.Now));
                }
                catch (Exception ex)
                {
                    OnHeartbeat?.Invoke(this, new WebSocketHeartbeatEventArgs(false, DateTime.Now, ex.Message));
                    OnError?.Invoke(this, new WebSocketErrorEventArgs(null, ex));
                }
            }
        }
        catch (OperationCanceledException)
        {
        }
    }

    /// <summary>
    /// 发送队列中的消息。
    /// </summary>
    private async Task SendQueuedMessagesAsync()
    {
        while (_messageQueue.TryDequeue(out var message))
        {
            if (!IsConnected)
            {
                _messageQueue.Enqueue(message);
                break;
            }

            try
            {
                await SendMessageAsync(message);
            }
            catch
            {
                _messageQueue.Enqueue(message);
                break;
            }
        }
    }

    /// <summary>
    /// 是否应该重连。
    /// </summary>
    private bool ShouldReconnect()
    {
        return _maxReconnectAttempts == 0 || _reconnectAttempts < _maxReconnectAttempts;
    }

    /// <summary>
    /// 清理资源。
    /// </summary>
    private void Cleanup()
    {
        try
        {
            _heartbeatCts?.Cancel();
            _heartbeatCts?.Dispose();
            _heartbeatCts = null;

            _receiveCts?.Cancel();
            _receiveCts?.Dispose();
            _receiveCts = null;
        }
        catch
        {
        }

        _webSocket?.Dispose();
        _webSocket = null;
        _state = WebSocketState.Closed;
    }

    /// <summary>
    /// 重置断开连接状态标志。
    /// </summary>
    private void ResetDisconnectionState()
    {
        _hasDisconnected = false;
    }

    #endregion

    #region IDisposable实现

    /// <summary>
    /// 释放资源。
    /// </summary>
    public void Dispose()
    {
        if (_isDisposed)
        {
            return;
        }

        _isDisposed = true;

        Cleanup();
        _sendLock?.Dispose();
        ClearMessageQueue();

        GC.SuppressFinalize(this);
    }

    /// <summary>
    /// 析构函数。
    /// </summary>
    ~WebSocketHelper()
    {
        Dispose();
    }

    #endregion
}

/// <summary>
/// WebSocket服务器帮助类，提供WebSocket服务器功能、连接管理、消息广播等。
/// </summary>
/// <remarks>
/// <para>支持的功能：</para>
/// <list type="bullet">
///   <item><description>服务器管理：启动、停止、状态监控</description></item>
///   <item><description>客户端管理：连接、断开、心跳检测、超时断开</description></item>
///   <item><description>消息处理：接收、广播、单播、组播</description></item>
///   <item><description>事件驱动：连接事件、断开事件、消息事件、错误事件</description></item>
/// </list>
/// </remarks>
/// <example>
/// <code>
/// // 创建服务器
/// var server = new WebSocketServerHelper(port: 8080, heartbeatIntervalSec: 30);
/// 
/// // 订阅事件
/// server.OnClientConnected += (sender, args) => 
///     Console.WriteLine($"客户端连接: {args.ClientInfo.ClientId}");
/// server.OnMessageReceived += (sender, args) => 
///     Console.WriteLine($"收到消息: {args.Message}");
/// 
/// // 启动服务器
/// await server.StartServerAsync("http://localhost:8080/");
/// 
/// // 广播消息
/// await server.BroadcastMessageAsync("Hello, everyone!");
/// 
/// // 停止服务器
/// server.StopServer();
/// </code>
/// </example>
public class WebSocketServerHelper : IDisposable
{
    #region 私有字段

    private HttpListener _httpListener;
    private readonly ConcurrentDictionary<string, WebSocket> _connectedClients;
    private readonly ConcurrentDictionary<string, ClientInfo> _clientInfos;
    private readonly ConcurrentDictionary<string, HashSet<string>> _groups;
    private bool _isRunning;
    private readonly int _heartbeatInterval;
    private Task _heartbeatTask;
    private CancellationTokenSource _cts;
    private int _clientIdCounter;
    private readonly object _clientIdLock = new object();
    private bool _isDisposed;

    #endregion

    #region 公共属性

    /// <summary>
    /// 获取当前连接的客户端数量。
    /// </summary>
    public int ConnectedClientCount => _connectedClients.Count;

    /// <summary>
    /// 获取服务器是否正在运行。
    /// </summary>
    public bool IsRunning => _isRunning;

    /// <summary>
    /// 获取所有已连接的客户端ID列表。
    /// </summary>
    public IEnumerable<string> ConnectedClientIds => _connectedClients.Keys;

    /// <summary>
    /// 获取所有组名称。
    /// </summary>
    public IEnumerable<string> GroupNames => _groups.Keys;

    #endregion

    #region 事件定义

    /// <summary>
    /// 服务器启动事件。
    /// </summary>
    public event EventHandler OnServerStarted;

    /// <summary>
    /// 服务器停止事件。
    /// </summary>
    public event EventHandler OnServerStopped;

    /// <summary>
    /// 客户端连接事件。
    /// </summary>
    public event EventHandler<ClientConnectedEventArgs> OnClientConnected;

    /// <summary>
    /// 客户端断开连接事件。
    /// </summary>
    public event EventHandler<ClientDisconnectedEventArgs> OnClientDisconnected;

    /// <summary>
    /// 收到消息事件。
    /// </summary>
    public event EventHandler<MessageReceivedEventArgs> OnMessageReceived;

    /// <summary>
    /// 发送消息事件。
    /// </summary>
    public event EventHandler<MessageSentEventArgs> OnMessageSent;

    /// <summary>
    /// 心跳检测事件。
    /// </summary>
    public event EventHandler<HeartbeatEventArgs> OnHeartbeat;

    /// <summary>
    /// 发生错误事件。
    /// </summary>
    public event EventHandler<WebSocketErrorEventArgs> OnError;

    #endregion

    #region 构造函数

    /// <summary>
    /// 初始化WebSocketServerHelper实例。
    /// </summary>
    /// <param name="heartbeatIntervalSec">心跳检测间隔（秒），默认30秒，0表示禁用心跳。</param>
    public WebSocketServerHelper(int heartbeatIntervalSec = 30)
    {
        _heartbeatInterval = heartbeatIntervalSec * 1000;
        _connectedClients = new ConcurrentDictionary<string, WebSocket>();
        _clientInfos = new ConcurrentDictionary<string, ClientInfo>();
        _groups = new ConcurrentDictionary<string, HashSet<string>>();
        _cts = new CancellationTokenSource();
    }

    /// <summary>
    /// 初始化WebSocketServerHelper实例。
    /// </summary>
    /// <param name="port">服务器端口（仅用于标识，实际端口由prefixes决定）。</param>
    /// <param name="heartbeatIntervalSec">心跳检测间隔（秒），默认30秒，0表示禁用心跳。</param>
    public WebSocketServerHelper(int port, int heartbeatIntervalSec = 30) : this(heartbeatIntervalSec)
    {
    }

    #endregion

    #region 公共方法 - 服务器管理

    /// <summary>
    /// 启动WebSocket服务器。
    /// </summary>
    /// <param name="prefixes">监听的URL前缀列表，例如：http://localhost:8080/</param>
    /// <returns>启动任务。</returns>
    /// <exception cref="InvalidOperationException">服务器已经在运行时抛出。</exception>
    /// <exception cref="ArgumentException">prefixes为空时抛出。</exception>
    public async Task StartServerAsync(IEnumerable<string> prefixes)
    {
        if (_isRunning)
        {
            throw new InvalidOperationException("服务器已经在运行中");
        }

        var prefixList = prefixes?.ToList();
        if (prefixList == null || prefixList.Count == 0)
        {
            throw new ArgumentException("必须提供至少一个监听前缀", nameof(prefixes));
        }

        try
        {
            _httpListener = new HttpListener();

            foreach (var prefix in prefixList)
            {
                _httpListener.Prefixes.Add(prefix);
            }

            _httpListener.Start();
            _isRunning = true;

            OnServerStarted?.Invoke(this, EventArgs.Empty);

            if (_heartbeatInterval > 0)
            {
                _heartbeatTask = Task.Run(() => HeartbeatLoopAsync(_cts.Token));
            }

            await AcceptWebSocketConnectionsAsync(_cts.Token);
        }
        catch (Exception ex)
        {
            if (!(_cts.Token.IsCancellationRequested && ex is OperationCanceledException))
            {
                OnError?.Invoke(this, new WebSocketErrorEventArgs(null, ex));
            }
            StopServer();
            throw;
        }
    }

    /// <summary>
    /// 启动WebSocket服务器。
    /// </summary>
    /// <param name="prefix">监听的URL前缀，例如：http://localhost:8080/</param>
    /// <returns>启动任务。</returns>
    public Task StartServerAsync(string prefix)
    {
        return StartServerAsync(new[] { prefix });
    }

    /// <summary>
    /// 停止WebSocket服务器。
    /// </summary>
    public void StopServer()
    {
        if (!_isRunning)
        {
            return;
        }

        try
        {
            _cts.Cancel();

            foreach (var clientId in _connectedClients.Keys.ToList())
            {
                DisconnectClient(clientId, WebSocketCloseStatus.NormalClosure, "Server shutting down");
            }

            if (_httpListener != null && _httpListener.IsListening)
            {
                _httpListener.Stop();
                _httpListener.Close();
            }

            _isRunning = false;

            OnServerStopped?.Invoke(this, EventArgs.Empty);
        }
        catch (Exception ex)
        {
            OnError?.Invoke(this, new WebSocketErrorEventArgs(null, ex));
        }
        finally
        {
            _connectedClients.Clear();
            _clientInfos.Clear();
            _groups.Clear();
            _cts.Dispose();
            _cts = new CancellationTokenSource();
        }
    }

    #endregion

    #region 公共方法 - 消息发送

    /// <summary>
    /// 向指定客户端发送消息。
    /// </summary>
    /// <param name="clientId">客户端ID。</param>
    /// <param name="message">消息内容。</param>
    /// <param name="cancellationToken">取消令牌。</param>
    /// <returns>发送任务。</returns>
    /// <exception cref="ArgumentException">客户端不存在时抛出。</exception>
    /// <exception cref="InvalidOperationException">客户端连接状态异常时抛出。</exception>
    public async Task SendMessageToClientAsync(string clientId, string message, CancellationToken cancellationToken = default)
    {
        ArgumentException.ThrowIfNullOrEmpty(clientId);
        ArgumentException.ThrowIfNullOrEmpty(message);

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
            await webSocket.SendAsync(
                new ArraySegment<byte>(buffer),
                WebSocketMessageType.Text,
                true,
                cancellationToken);

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
    /// 向指定客户端发送二进制消息。
    /// </summary>
    /// <param name="clientId">客户端ID。</param>
    /// <param name="data">二进制数据。</param>
    /// <param name="cancellationToken">取消令牌。</param>
    /// <returns>发送任务。</returns>
    public async Task SendBinaryToClientAsync(string clientId, byte[] data, CancellationToken cancellationToken = default)
    {
        ArgumentException.ThrowIfNullOrEmpty(clientId);
        ArgumentNullException.ThrowIfNull(data);

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
            await webSocket.SendAsync(
                new ArraySegment<byte>(data),
                WebSocketMessageType.Binary,
                true,
                cancellationToken);

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
    /// 广播消息给所有连接的客户端。
    /// </summary>
    /// <param name="message">消息内容。</param>
    /// <param name="cancellationToken">取消令牌。</param>
    /// <returns>广播任务。</returns>
    public Task BroadcastMessageAsync(string message, CancellationToken cancellationToken = default)
    {
        return BroadcastMessageAsync(message, null, cancellationToken);
    }

    /// <summary>
    /// 广播消息给所有连接的客户端，除了指定的客户端。
    /// </summary>
    /// <param name="message">消息内容。</param>
    /// <param name="excludeClientId">要排除的客户端ID。</param>
    /// <param name="cancellationToken">取消令牌。</param>
    /// <returns>广播任务。</returns>
    public async Task BroadcastMessageAsync(string message, string excludeClientId, CancellationToken cancellationToken = default)
    {
        var tasks = _connectedClients.Keys
            .Where(id => id != excludeClientId)
            .Select(clientId => SendMessageToClientAsync(clientId, message, cancellationToken)
                .ContinueWith(t =>
                {
                    if (t.IsFaulted && t.Exception != null)
                    {
                        OnError?.Invoke(this, new WebSocketErrorEventArgs(clientId, t.Exception.InnerException));
                    }
                }));

        await Task.WhenAll(tasks);
    }

    /// <summary>
    /// 向指定组广播消息。
    /// </summary>
    /// <param name="groupName">组名称。</param>
    /// <param name="message">消息内容。</param>
    /// <param name="cancellationToken">取消令牌。</param>
    /// <returns>广播任务。</returns>
    public async Task BroadcastToGroupAsync(string groupName, string message, CancellationToken cancellationToken = default)
    {
        ArgumentException.ThrowIfNullOrEmpty(groupName);

        if (!_groups.TryGetValue(groupName, out var clientIds))
        {
            return;
        }

        var tasks = clientIds
            .Where(id => _connectedClients.ContainsKey(id))
            .Select(clientId => SendMessageToClientAsync(clientId, message, cancellationToken)
                .ContinueWith(t =>
                {
                    if (t.IsFaulted && t.Exception != null)
                    {
                        OnError?.Invoke(this, new WebSocketErrorEventArgs(clientId, t.Exception.InnerException));
                    }
                }));

        await Task.WhenAll(tasks);
    }

    #endregion

    #region 公共方法 - 客户端管理

    /// <summary>
    /// 断开指定客户端连接。
    /// </summary>
    /// <param name="clientId">客户端ID。</param>
    /// <param name="closeStatus">关闭状态。</param>
    /// <param name="statusDescription">状态描述。</param>
    public void DisconnectClient(
        string clientId,
        WebSocketCloseStatus closeStatus = WebSocketCloseStatus.NormalClosure,
        string statusDescription = "Normal closure")
    {
        ArgumentException.ThrowIfNullOrEmpty(clientId);

        if (!_connectedClients.TryRemove(clientId, out var webSocket))
        {
            return;
        }

        try
        {
            if (webSocket.State == WebSocketState.Open || 
                webSocket.State == WebSocketState.CloseReceived)
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
            _clientInfos.TryRemove(clientId, out _);

            foreach (var group in _groups.Values)
            {
                group.Remove(clientId);
            }

            OnClientDisconnected?.Invoke(this, new ClientDisconnectedEventArgs(clientId, closeStatus, statusDescription));
        }
    }

    /// <summary>
    /// 获取客户端信息。
    /// </summary>
    /// <param name="clientId">客户端ID。</param>
    /// <returns>客户端信息，如果客户端不存在则返回null。</returns>
    public ClientInfo GetClientInfo(string clientId)
    {
        ArgumentException.ThrowIfNullOrEmpty(clientId);
        _clientInfos.TryGetValue(clientId, out var clientInfo);
        return clientInfo;
    }

    /// <summary>
    /// 设置客户端自定义信息。
    /// </summary>
    /// <param name="clientId">客户端ID。</param>
    /// <param name="customInfo">自定义信息。</param>
    public void SetClientCustomInfo(string clientId, object customInfo)
    {
        ArgumentException.ThrowIfNullOrEmpty(clientId);

        if (_clientInfos.TryGetValue(clientId, out var clientInfo))
        {
            clientInfo.CustomInfo = customInfo;
        }
    }

    /// <summary>
    /// 将客户端添加到组。
    /// </summary>
    /// <param name="clientId">客户端ID。</param>
    /// <param name="groupName">组名称。</param>
    public void AddClientToGroup(string clientId, string groupName)
    {
        ArgumentException.ThrowIfNullOrEmpty(clientId);
        ArgumentException.ThrowIfNullOrEmpty(groupName);

        if (!_connectedClients.ContainsKey(clientId))
        {
            return;
        }

        var group = _groups.GetOrAdd(groupName, _ => new HashSet<string>());
        lock (group)
        {
            group.Add(clientId);
        }
    }

    /// <summary>
    /// 将客户端从组中移除。
    /// </summary>
    /// <param name="clientId">客户端ID。</param>
    /// <param name="groupName">组名称。</param>
    public void RemoveClientFromGroup(string clientId, string groupName)
    {
        ArgumentException.ThrowIfNullOrEmpty(clientId);
        ArgumentException.ThrowIfNullOrEmpty(groupName);

        if (_groups.TryGetValue(groupName, out var group))
        {
            lock (group)
            {
                group.Remove(clientId);
            }
        }
    }

    /// <summary>
    /// 获取组中的所有客户端ID。
    /// </summary>
    /// <param name="groupName">组名称。</param>
    /// <returns>客户端ID列表。</returns>
    public IEnumerable<string> GetClientsInGroup(string groupName)
    {
        ArgumentException.ThrowIfNullOrEmpty(groupName);

        if (_groups.TryGetValue(groupName, out var group))
        {
            lock (group)
            {
                return group.ToList();
            }
        }

        return Enumerable.Empty<string>();
    }

    #endregion

    #region 私有方法

    /// <summary>
    /// 接受WebSocket连接的循环。
    /// </summary>
    private async Task AcceptWebSocketConnectionsAsync(CancellationToken cancellationToken)
    {
        try
        {
            while (!cancellationToken.IsCancellationRequested && _httpListener.IsListening)
            {
                try
                {
                    var context = await _httpListener.GetContextAsync();

                    if (context.Request.IsWebSocketRequest)
                    {
                        _ = Task.Run(() => HandleWebSocketRequestAsync(context, cancellationToken), cancellationToken);
                    }
                    else
                    {
                        context.Response.StatusCode = 400;
                        context.Response.Close();
                    }
                }
                catch (Exception ex)
                {
                    OnError?.Invoke(this, new WebSocketErrorEventArgs(null, ex));
                }
            }
        }
        catch (OperationCanceledException)
        {
        }
    }

    /// <summary>
    /// 处理WebSocket请求。
    /// </summary>
    private async Task HandleWebSocketRequestAsync(HttpListenerContext context, CancellationToken cancellationToken)
    {
        WebSocketContext webSocketContext = null;
        WebSocket webSocket = null;
        string clientId = null;

        try
        {
            webSocketContext = await context.AcceptWebSocketAsync(null);
            webSocket = webSocketContext.WebSocket;

            clientId = GenerateClientId();

            var clientInfo = new ClientInfo
            {
                ClientId = clientId,
                ConnectedTime = DateTime.Now,
                RemoteEndPoint = context.Request.RemoteEndPoint,
                UserAgent = context.Request.UserAgent,
                LastHeartbeatTime = DateTime.Now
            };

            _connectedClients.TryAdd(clientId, webSocket);
            _clientInfos.TryAdd(clientId, clientInfo);

            OnClientConnected?.Invoke(this, new ClientConnectedEventArgs(clientInfo));

            await HandleClientMessagesAsync(clientId, webSocket, cancellationToken);
        }
        catch (Exception ex)
        {
            OnError?.Invoke(this, new WebSocketErrorEventArgs(clientId, ex));
        }
        finally
        {
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
    /// 处理客户端消息。
    /// </summary>
    private async Task HandleClientMessagesAsync(string clientId, WebSocket webSocket, CancellationToken cancellationToken)
    {
        var buffer = new byte[4096];

        try
        {
            while (!cancellationToken.IsCancellationRequested && webSocket.State == WebSocketState.Open)
            {
                var result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), cancellationToken);

                if (_clientInfos.TryGetValue(clientId, out var clientInfo))
                {
                    clientInfo.LastHeartbeatTime = DateTime.Now;
                }

                if (result.MessageType == WebSocketMessageType.Close)
                {
                    await webSocket.CloseAsync(
                        result.CloseStatus ?? WebSocketCloseStatus.NormalClosure,
                        result.CloseStatusDescription ?? "Client closed",
                        CancellationToken.None);
                    break;
                }

                if (result.MessageType == WebSocketMessageType.Text)
                {
                    var message = Encoding.UTF8.GetString(buffer, 0, result.Count);
                    OnMessageReceived?.Invoke(this, new MessageReceivedEventArgs(clientId, message));
                }
                else if (result.MessageType == WebSocketMessageType.Binary)
                {
                    if (result.Count == 0)
                    {
                        OnHeartbeat?.Invoke(this, new HeartbeatEventArgs(clientId, true));
                    }
                    else
                    {
                        var binaryData = new byte[result.Count];
                        Array.Copy(buffer, 0, binaryData, 0, result.Count);
                        OnMessageReceived?.Invoke(this, new MessageReceivedEventArgs(clientId, binaryData));
                    }
                }
            }
        }
        catch (OperationCanceledException)
        {
        }
        catch (Exception ex)
        {
            OnError?.Invoke(this, new WebSocketErrorEventArgs(clientId, ex));
        }
    }

    /// <summary>
    /// 心跳检测循环。
    /// </summary>
    private async Task HeartbeatLoopAsync(CancellationToken cancellationToken)
    {
        try
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                await Task.Delay(_heartbeatInterval, cancellationToken);
                CheckClientHeartbeats();
            }
        }
        catch (OperationCanceledException)
        {
        }
        catch (Exception ex)
        {
            OnError?.Invoke(this, new WebSocketErrorEventArgs(null, ex));
        }
    }

    /// <summary>
    /// 检查客户端心跳。
    /// </summary>
    private void CheckClientHeartbeats()
    {
        var timeoutThreshold = DateTime.Now.AddMilliseconds(-_heartbeatInterval * 3);
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

        foreach (var clientId in clientsToDisconnect)
        {
            DisconnectClient(clientId, WebSocketCloseStatus.EndpointUnavailable, "Heartbeat timeout");
        }
    }

    /// <summary>
    /// 生成客户端ID。
    /// </summary>
    private string GenerateClientId()
    {
        lock (_clientIdLock)
        {
            _clientIdCounter++;
            return $"client_{_clientIdCounter}_{Guid.NewGuid().ToString("N").Substring(0, 8)}";
        }
    }

    #endregion

    #region IDisposable实现

    /// <summary>
    /// 释放资源。
    /// </summary>
    public void Dispose()
    {
        if (_isDisposed)
        {
            return;
        }

        _isDisposed = true;
        StopServer();
        GC.SuppressFinalize(this);
    }

    /// <summary>
    /// 析构函数。
    /// </summary>
    ~WebSocketServerHelper()
    {
        Dispose();
    }

    #endregion
}

#region 事件参数类

/// <summary>
/// 客户端信息类。
/// </summary>
public class ClientInfo
{
    /// <summary>
    /// 获取客户端ID。
    /// </summary>
    public string ClientId { get; internal init; }

    /// <summary>
    /// 获取连接时间。
    /// </summary>
    public DateTime ConnectedTime { get; internal init; }

    /// <summary>
    /// 获取远程端点。
    /// </summary>
    public EndPoint RemoteEndPoint { get; internal init; }

    /// <summary>
    /// 获取User-Agent。
    /// </summary>
    public string UserAgent { get; internal init; }

    /// <summary>
    /// 获取或设置最后心跳时间。
    /// </summary>
    public DateTime LastHeartbeatTime { get; internal set; }

    /// <summary>
    /// 获取或设置自定义信息。
    /// </summary>
    public object CustomInfo { get; set; }

    /// <summary>
    /// 获取连接持续时间。
    /// </summary>
    public TimeSpan ConnectionDuration => DateTime.Now - ConnectedTime;
}

/// <summary>
/// WebSocket连接成功事件参数。
/// </summary>
public class WebSocketConnectedEventArgs : EventArgs
{
    /// <summary>
    /// 获取连接的URI。
    /// </summary>
    public Uri Uri { get; }

    /// <summary>
    /// 获取连接时间。
    /// </summary>
    public DateTime ConnectedTime { get; }

    /// <summary>
    /// 初始化WebSocketConnectedEventArgs实例。
    /// </summary>
    /// <param name="uri">连接的URI。</param>
    public WebSocketConnectedEventArgs(Uri uri)
    {
        Uri = uri;
        ConnectedTime = DateTime.Now;
    }
}

/// <summary>
/// WebSocket断开连接事件参数。
/// </summary>
public class WebSocketDisconnectedEventArgs : EventArgs
{
    /// <summary>
    /// 获取关闭状态。
    /// </summary>
    public WebSocketCloseStatus CloseStatus { get; }

    /// <summary>
    /// 获取状态描述。
    /// </summary>
    public string StatusDescription { get; }

    /// <summary>
    /// 获取断开时间。
    /// </summary>
    public DateTime DisconnectedTime { get; }

    /// <summary>
    /// 初始化WebSocketDisconnectedEventArgs实例。
    /// </summary>
    /// <param name="closeStatus">关闭状态。</param>
    /// <param name="statusDescription">状态描述。</param>
    public WebSocketDisconnectedEventArgs(WebSocketCloseStatus closeStatus, string statusDescription)
    {
        CloseStatus = closeStatus;
        StatusDescription = statusDescription;
        DisconnectedTime = DateTime.Now;
    }
}

/// <summary>
/// WebSocket消息接收事件参数。
/// </summary>
public class WebSocketMessageReceivedEventArgs : EventArgs
{
    /// <summary>
    /// 获取消息内容。
    /// </summary>
    public string Message { get; }

    /// <summary>
    /// 获取接收时间。
    /// </summary>
    public DateTime ReceivedTime { get; }

    /// <summary>
    /// 初始化WebSocketMessageReceivedEventArgs实例。
    /// </summary>
    /// <param name="message">消息内容。</param>
    public WebSocketMessageReceivedEventArgs(string message)
    {
        Message = message;
        ReceivedTime = DateTime.Now;
    }
}

/// <summary>
/// WebSocket二进制消息接收事件参数。
/// </summary>
public class WebSocketBinaryReceivedEventArgs : EventArgs
{
    /// <summary>
    /// 获取二进制数据。
    /// </summary>
    public byte[] Data { get; }

    /// <summary>
    /// 获取接收时间。
    /// </summary>
    public DateTime ReceivedTime { get; }

    /// <summary>
    /// 初始化WebSocketBinaryReceivedEventArgs实例。
    /// </summary>
    /// <param name="data">二进制数据。</param>
    public WebSocketBinaryReceivedEventArgs(byte[] data)
    {
        Data = data;
        ReceivedTime = DateTime.Now;
    }
}

/// <summary>
/// WebSocket错误事件参数。
/// </summary>
public class WebSocketErrorEventArgs : EventArgs
{
    /// <summary>
    /// 获取客户端ID（可能为null）。
    /// </summary>
    public string ClientId { get; }

    /// <summary>
    /// 获取异常信息。
    /// </summary>
    public Exception Exception { get; }

    /// <summary>
    /// 获取发生时间。
    /// </summary>
    public DateTime ErrorTime { get; }

    /// <summary>
    /// 初始化WebSocketErrorEventArgs实例。
    /// </summary>
    /// <param name="clientId">客户端ID。</param>
    /// <param name="exception">异常信息。</param>
    public WebSocketErrorEventArgs(string clientId, Exception exception)
    {
        ClientId = clientId;
        Exception = exception;
        ErrorTime = DateTime.Now;
    }
}

/// <summary>
/// WebSocket心跳事件参数。
/// </summary>
public class WebSocketHeartbeatEventArgs : EventArgs
{
    /// <summary>
    /// 获取是否成功。
    /// </summary>
    public bool IsSuccess { get; }

    /// <summary>
    /// 获取心跳时间。
    /// </summary>
    public DateTime HeartbeatTime { get; }

    /// <summary>
    /// 获取错误消息（如果有）。
    /// </summary>
    public string ErrorMessage { get; }

    /// <summary>
    /// 初始化WebSocketHeartbeatEventArgs实例。
    /// </summary>
    /// <param name="isSuccess">是否成功。</param>
    /// <param name="heartbeatTime">心跳时间。</param>
    /// <param name="errorMessage">错误消息。</param>
    public WebSocketHeartbeatEventArgs(bool isSuccess, DateTime heartbeatTime, string errorMessage = null)
    {
        IsSuccess = isSuccess;
        HeartbeatTime = heartbeatTime;
        ErrorMessage = errorMessage;
    }
}

/// <summary>
/// WebSocket重连事件参数。
/// </summary>
public class WebSocketReconnectingEventArgs : EventArgs
{
    /// <summary>
    /// 获取当前重连次数。
    /// </summary>
    public int Attempt { get; }

    /// <summary>
    /// 获取最大重连次数。
    /// </summary>
    public int MaxAttempts { get; }

    /// <summary>
    /// 获取重连时间。
    /// </summary>
    public DateTime ReconnectingTime { get; }

    /// <summary>
    /// 初始化WebSocketReconnectingEventArgs实例。
    /// </summary>
    /// <param name="attempt">当前重连次数。</param>
    /// <param name="maxAttempts">最大重连次数。</param>
    public WebSocketReconnectingEventArgs(int attempt, int maxAttempts)
    {
        Attempt = attempt;
        MaxAttempts = maxAttempts;
        ReconnectingTime = DateTime.Now;
    }
}

/// <summary>
/// WebSocket消息发送完成事件参数。
/// </summary>
public class WebSocketMessageSentEventArgs : EventArgs
{
    /// <summary>
    /// 获取消息内容。
    /// </summary>
    public string Message { get; }

    /// <summary>
    /// 获取是否是二进制消息。
    /// </summary>
    public bool IsBinary { get; }

    /// <summary>
    /// 获取发送时间。
    /// </summary>
    public DateTime SentTime { get; }

    /// <summary>
    /// 初始化WebSocketMessageSentEventArgs实例。
    /// </summary>
    /// <param name="message">消息内容。</param>
    /// <param name="isBinary">是否是二进制消息。</param>
    public WebSocketMessageSentEventArgs(string message, bool isBinary)
    {
        Message = message;
        IsBinary = isBinary;
        SentTime = DateTime.Now;
    }
}

/// <summary>
/// 客户端连接事件参数。
/// </summary>
public class ClientConnectedEventArgs : EventArgs
{
    /// <summary>
    /// 获取客户端信息。
    /// </summary>
    public ClientInfo ClientInfo { get; }

    /// <summary>
    /// 初始化ClientConnectedEventArgs实例。
    /// </summary>
    /// <param name="clientInfo">客户端信息。</param>
    public ClientConnectedEventArgs(ClientInfo clientInfo)
    {
        ClientInfo = clientInfo;
    }
}

/// <summary>
/// 客户端断开连接事件参数。
/// </summary>
public class ClientDisconnectedEventArgs : EventArgs
{
    /// <summary>
    /// 获取客户端ID。
    /// </summary>
    public string ClientId { get; }

    /// <summary>
    /// 获取关闭状态。
    /// </summary>
    public WebSocketCloseStatus CloseStatus { get; }

    /// <summary>
    /// 获取状态描述。
    /// </summary>
    public string StatusDescription { get; }

    /// <summary>
    /// 初始化ClientDisconnectedEventArgs实例。
    /// </summary>
    /// <param name="clientId">客户端ID。</param>
    /// <param name="closeStatus">关闭状态。</param>
    /// <param name="statusDescription">状态描述。</param>
    public ClientDisconnectedEventArgs(string clientId, WebSocketCloseStatus closeStatus, string statusDescription)
    {
        ClientId = clientId;
        CloseStatus = closeStatus;
        StatusDescription = statusDescription;
    }
}

/// <summary>
/// 消息接收事件参数。
/// </summary>
public class MessageReceivedEventArgs : EventArgs
{
    /// <summary>
    /// 获取客户端ID。
    /// </summary>
    public string ClientId { get; }

    /// <summary>
    /// 获取消息内容。
    /// </summary>
    public string Message { get; }

    /// <summary>
    /// 获取二进制数据。
    /// </summary>
    public byte[] BinaryData { get; }

    /// <summary>
    /// 获取是否是二进制消息。
    /// </summary>
    public bool IsBinary => BinaryData != null;

    /// <summary>
    /// 获取接收时间。
    /// </summary>
    public DateTime ReceivedTime { get; }

    /// <summary>
    /// 初始化MessageReceivedEventArgs实例（文本消息）。
    /// </summary>
    /// <param name="clientId">客户端ID。</param>
    /// <param name="message">消息内容。</param>
    public MessageReceivedEventArgs(string clientId, string message)
    {
        ClientId = clientId;
        Message = message;
        ReceivedTime = DateTime.Now;
    }

    /// <summary>
    /// 初始化MessageReceivedEventArgs实例（二进制消息）。
    /// </summary>
    /// <param name="clientId">客户端ID。</param>
    /// <param name="binaryData">二进制数据。</param>
    public MessageReceivedEventArgs(string clientId, byte[] binaryData)
    {
        ClientId = clientId;
        BinaryData = binaryData;
        ReceivedTime = DateTime.Now;
    }
}

/// <summary>
/// 消息发送事件参数。
/// </summary>
public class MessageSentEventArgs : EventArgs
{
    /// <summary>
    /// 获取客户端ID。
    /// </summary>
    public string ClientId { get; }

    /// <summary>
    /// 获取消息内容。
    /// </summary>
    public string Message { get; }

    /// <summary>
    /// 获取是否是二进制消息。
    /// </summary>
    public bool IsBinary { get; }

    /// <summary>
    /// 获取发送时间。
    /// </summary>
    public DateTime SentTime { get; }

    /// <summary>
    /// 初始化MessageSentEventArgs实例。
    /// </summary>
    /// <param name="clientId">客户端ID。</param>
    /// <param name="message">消息内容。</param>
    /// <param name="isBinary">是否是二进制消息。</param>
    public MessageSentEventArgs(string clientId, string message, bool isBinary = false)
    {
        ClientId = clientId;
        Message = message;
        IsBinary = isBinary;
        SentTime = DateTime.Now;
    }
}

/// <summary>
/// 心跳事件参数。
/// </summary>
public class HeartbeatEventArgs : EventArgs
{
    /// <summary>
    /// 获取客户端ID。
    /// </summary>
    public string ClientId { get; }

    /// <summary>
    /// 获取是否正常心跳。
    /// </summary>
    public bool IsNormal { get; }

    /// <summary>
    /// 获取心跳消息。
    /// </summary>
    public string Message { get; }

    /// <summary>
    /// 获取心跳时间。
    /// </summary>
    public DateTime HeartbeatTime { get; }

    /// <summary>
    /// 初始化HeartbeatEventArgs实例。
    /// </summary>
    /// <param name="clientId">客户端ID。</param>
    /// <param name="isNormal">是否正常心跳。</param>
    /// <param name="message">心跳消息。</param>
    public HeartbeatEventArgs(string clientId, bool isNormal, string message = null)
    {
        ClientId = clientId;
        IsNormal = isNormal;
        Message = message;
        HeartbeatTime = DateTime.Now;
    }
}

#endregion
