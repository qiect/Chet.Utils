# WebSocketHelper 帮助类

## 概述

[WebSocketHelper](../../Chet.Utils/Helpers/WebSocketHelper.cs) 是一个 WebSocket 客户端和服务器辅助类，为 WebSocket 通信提供了丰富的功能，包括连接管理、消息收发、心跳检测、自动重连、消息队列、事件驱动模型、线程安全操作、自动资源管理等，旨在封装 WebSocket 的复杂性，使 WebSocket 的使用更加简单和可靠。

## 主要特性

- 🔄 自动连接管理和重连机制
- 💓 内置心跳检测功能
- 📨 支持文本和二进制消息收发
- 📦 消息队列支持（断线时缓存消息）
- ⚡ 事件驱动的异步编程模型
- 🔒 线程安全的操作
- 🧹 自动资源管理和清理
- 🖥️ 内置 WebSocket 服务器支持

***

## WebSocketHelper 客户端

### 类定义

```csharp
public class WebSocketHelper : IDisposable
```

### 构造函数

```csharp
public WebSocketHelper(
    int heartbeatInterval = 30000,
    int maxReconnectAttempts = 5,
    int reconnectDelay = 5000,
    int receiveBufferSize = 4096,
    int sendBufferSize = 4096,
    TimeSpan? connectionTimeout = null,
    Action<ClientWebSocketOptions> configureOptions = null)
```

#### 参数

| 参数名                  | 类型                             | 描述                           |
| -------------------- | ------------------------------ | ---------------------------- |
| heartbeatInterval    | int                            | 心跳检测间隔（毫秒），默认 30000ms，0 表示禁用 |
| maxReconnectAttempts | int                            | 最大重连次数，默认 5 次，0 表示无限重连       |
| reconnectDelay       | int                            | 重连延迟时间（毫秒），默认 5000ms         |
| receiveBufferSize    | int                            | 接收缓冲区大小（字节），默认 4096          |
| sendBufferSize       | int                            | 发送缓冲区大小（字节），默认 4096          |
| connectionTimeout    | TimeSpan?                      | 连接超时时间，默认 30 秒               |
| configureOptions     | Action<ClientWebSocketOptions> | WebSocket 选项配置委托             |

#### 示例

```csharp
// 使用默认配置
var wsHelper = new WebSocketHelper();

// 自定义配置
var wsHelper = new WebSocketHelper(
    heartbeatInterval: 10000,
    maxReconnectAttempts: 3,
    reconnectDelay: 2000,
    receiveBufferSize: 8192,
    sendBufferSize: 8192,
    connectionTimeout: TimeSpan.FromSeconds(60));

// 使用配置委托设置 SSL
var wsHelper = new WebSocketHelper(configureOptions: options =>
{
    options.RemoteCertificateValidationCallback = (sender, cert, chain, errors) => true;
});
```

### 属性

| 属性名                | 类型             | 描述                    |
| ------------------ | -------------- | --------------------- |
| State              | WebSocketState | 获取当前 WebSocket 连接状态   |
| IsConnected        | bool           | 获取是否已建立连接             |
| ConnectedUri       | Uri            | 获取当前连接的 URI           |
| AutoReconnect      | bool           | 获取或设置是否启用自动重连，默认 true |
| EnableMessageQueue | bool           | 获取或设置是否启用消息队列，默认 true |
| QueuedMessageCount | int            | 获取消息队列中的消息数量          |

### 事件

#### OnConnected

连接成功时触发

```csharp
public event EventHandler<WebSocketConnectedEventArgs> OnConnected;
```

#### OnDisconnected

连接断开时触发

```csharp
public event EventHandler<WebSocketDisconnectedEventArgs> OnDisconnected;
```

#### OnMessageReceived

接收到文本消息时触发

```csharp
public event EventHandler<WebSocketMessageReceivedEventArgs> OnMessageReceived;
```

#### OnBinaryReceived

接收到二进制消息时触发

```csharp
public event EventHandler<WebSocketBinaryReceivedEventArgs> OnBinaryReceived;
```

#### OnError

发生错误时触发

```csharp
public event EventHandler<WebSocketErrorEventArgs> OnError;
```

#### OnHeartbeat

心跳检测时触发

```csharp
public event EventHandler<WebSocketHeartbeatEventArgs> OnHeartbeat;
```

#### OnReconnecting

重连时触发

```csharp
public event EventHandler<WebSocketReconnectingEventArgs> OnReconnecting;
```

#### OnMessageSent

消息发送完成时触发

```csharp
public event EventHandler<WebSocketMessageSentEventArgs> OnMessageSent;
```

### 方法

#### ConnectAsync

连接到 WebSocket 服务器

```csharp
public Task ConnectAsync(Uri uri, CancellationToken cancellationToken = default)
```

| 参数名               | 类型                | 描述              |
| ----------------- | ----------------- | --------------- |
| uri               | Uri               | WebSocket 服务器地址 |
| cancellationToken | CancellationToken | 取消令牌            |

**异常：**

- ArgumentNullException: URI 为空时抛出
- InvalidOperationException: 已经在连接中时抛出

#### DisconnectAsync

断开 WebSocket 连接

```csharp
public Task DisconnectAsync(
    WebSocketCloseStatus closeStatus = WebSocketCloseStatus.NormalClosure,
    string statusDescription = "Normal closure",
    CancellationToken cancellationToken = default)
```

#### ReconnectAsync

重新连接到 WebSocket 服务器

```csharp
public Task ReconnectAsync(CancellationToken cancellationToken = default)
```

**异常：**

- InvalidOperationException: 没有可重连的 URI 时抛出

#### SendMessageAsync

发送文本消息

```csharp
public Task SendMessageAsync(string message, CancellationToken cancellationToken = default)
```

**注意：** 如果未连接且启用了消息队列，消息将被缓存到队列中。

#### SendBinaryAsync

发送二进制消息

```csharp
public Task SendBinaryAsync(byte[] data, CancellationToken cancellationToken = default)
```

#### SendMessagesAsync

批量发送文本消息

```csharp
public Task SendMessagesAsync(IEnumerable<string> messages, CancellationToken cancellationToken = default)
```

#### ClearMessageQueue

清空消息队列

```csharp
public void ClearMessageQueue()
```

***

## WebSocketServerHelper 服务器

### 类定义

```csharp
public class WebSocketServerHelper : IDisposable
```

### 构造函数

```csharp
public WebSocketServerHelper(int heartbeatIntervalSec = 30)
public WebSocketServerHelper(int port, int heartbeatIntervalSec = 30)
```

| 参数名                  | 类型  | 描述                       |
| -------------------- | --- | ------------------------ |
| port                 | int | 服务器端口（仅用于标识）             |
| heartbeatIntervalSec | int | 心跳检测间隔（秒），默认 30 秒，0 表示禁用 |

### 属性

| 属性名                  | 类型                   | 描述                |
| -------------------- | -------------------- | ----------------- |
| ConnectedClientCount | int                  | 获取当前连接的客户端数量      |
| IsRunning            | bool                 | 获取服务器是否正在运行       |
| ConnectedClientIds   | IEnumerable\<string> | 获取所有已连接的客户端 ID 列表 |
| GroupNames           | IEnumerable\<string> | 获取所有组名称           |

### 事件

#### OnServerStarted

服务器启动时触发

```csharp
public event EventHandler OnServerStarted;
```

#### OnServerStopped

服务器停止时触发

```csharp
public event EventHandler OnServerStopped;
```

#### OnClientConnected

客户端连接时触发

```csharp
public event EventHandler<ClientConnectedEventArgs> OnClientConnected;
```

#### OnClientDisconnected

客户端断开连接时触发

```csharp
public event EventHandler<ClientDisconnectedEventArgs> OnClientDisconnected;
```

#### OnMessageReceived

收到消息时触发

```csharp
public event EventHandler<MessageReceivedEventArgs> OnMessageReceived;
```

#### OnMessageSent

发送消息时触发

```csharp
public event EventHandler<MessageSentEventArgs> OnMessageSent;
```

#### OnHeartbeat

心跳检测时触发

```csharp
public event EventHandler<HeartbeatEventArgs> OnHeartbeat;
```

#### OnError

发生错误时触发

```csharp
public event EventHandler<WebSocketErrorEventArgs> OnError;
```

### 方法

#### StartServerAsync

启动 WebSocket 服务器

```csharp
public Task StartServerAsync(IEnumerable<string> prefixes)
public Task StartServerAsync(string prefix)
```

| 参数名      | 类型                   | 描述           |
| -------- | -------------------- | ------------ |
| prefixes | IEnumerable\<string> | 监听的 URL 前缀列表 |
| prefix   | string               | 监听的 URL 前缀   |

**示例：**

```csharp
var server = new WebSocketServerHelper(heartbeatIntervalSec: 30);
await server.StartServerAsync("http://localhost:8080/");
```

#### StopServer

停止 WebSocket 服务器

```csharp
public void StopServer()
```

#### SendMessageToClientAsync

向指定客户端发送消息

```csharp
public Task SendMessageToClientAsync(string clientId, string message, CancellationToken cancellationToken = default)
```

#### SendBinaryToClientAsync

向指定客户端发送二进制消息

```csharp
public Task SendBinaryToClientAsync(string clientId, byte[] data, CancellationToken cancellationToken = default)
```

#### BroadcastMessageAsync

广播消息给所有连接的客户端

```csharp
public Task BroadcastMessageAsync(string message, CancellationToken cancellationToken = default)
public Task BroadcastMessageAsync(string message, string excludeClientId, CancellationToken cancellationToken = default)
```

#### BroadcastToGroupAsync

向指定组广播消息

```csharp
public Task BroadcastToGroupAsync(string groupName, string message, CancellationToken cancellationToken = default)
```

#### DisconnectClient

断开指定客户端连接

```csharp
public void DisconnectClient(string clientId, WebSocketCloseStatus closeStatus = WebSocketCloseStatus.NormalClosure, string statusDescription = "Normal closure")
```

#### GetClientInfo

获取客户端信息

```csharp
public ClientInfo GetClientInfo(string clientId)
```

#### SetClientCustomInfo

设置客户端自定义信息

```csharp
public void SetClientCustomInfo(string clientId, object customInfo)
```

#### AddClientToGroup

将客户端添加到组

```csharp
public void AddClientToGroup(string clientId, string groupName)
```

#### RemoveClientFromGroup

将客户端从组中移除

```csharp
public void RemoveClientFromGroup(string clientId, string groupName)
```

#### GetClientsInGroup

获取组中的所有客户端 ID

```csharp
public IEnumerable<string> GetClientsInGroup(string groupName)
```

***

## 事件参数类

### WebSocketConnectedEventArgs

| 属性名           | 类型       | 描述      |
| ------------- | -------- | ------- |
| Uri           | Uri      | 连接的 URI |
| ConnectedTime | DateTime | 连接时间    |

### WebSocketDisconnectedEventArgs

| 属性名               | 类型                   | 描述   |
| ----------------- | -------------------- | ---- |
| CloseStatus       | WebSocketCloseStatus | 关闭状态 |
| StatusDescription | string               | 状态描述 |
| DisconnectedTime  | DateTime             | 断开时间 |

### WebSocketMessageReceivedEventArgs

| 属性名          | 类型       | 描述   |
| ------------ | -------- | ---- |
| Message      | string   | 消息内容 |
| ReceivedTime | DateTime | 接收时间 |

### WebSocketBinaryReceivedEventArgs

| 属性名          | 类型       | 描述    |
| ------------ | -------- | ----- |
| Data         | byte\[]  | 二进制数据 |
| ReceivedTime | DateTime | 接收时间  |

### WebSocketErrorEventArgs

| 属性名       | 类型        | 描述               |
| --------- | --------- | ---------------- |
| ClientId  | string    | 客户端 ID（可能为 null） |
| Exception | Exception | 异常信息             |
| ErrorTime | DateTime  | 发生时间             |

### WebSocketHeartbeatEventArgs

| 属性名           | 类型       | 描述        |
| ------------- | -------- | --------- |
| IsSuccess     | bool     | 是否成功      |
| HeartbeatTime | DateTime | 心跳时间      |
| ErrorMessage  | string   | 错误消息（如果有） |

### WebSocketReconnectingEventArgs

| 属性名              | 类型       | 描述     |
| ---------------- | -------- | ------ |
| Attempt          | int      | 当前重连次数 |
| MaxAttempts      | int      | 最大重连次数 |
| ReconnectingTime | DateTime | 重连时间   |

### WebSocketMessageSentEventArgs

| 属性名      | 类型       | 描述       |
| -------- | -------- | -------- |
| Message  | string   | 消息内容     |
| IsBinary | bool     | 是否是二进制消息 |
| SentTime | DateTime | 发送时间     |

### ClientInfo

| 属性名                | 类型       | 描述         |
| ------------------ | -------- | ---------- |
| ClientId           | string   | 客户端 ID     |
| ConnectedTime      | DateTime | 连接时间       |
| RemoteEndPoint     | EndPoint | 远程端点       |
| UserAgent          | string   | User-Agent |
| LastHeartbeatTime  | DateTime | 最后心跳时间     |
| CustomInfo         | object   | 自定义信息      |
| ConnectionDuration | TimeSpan | 连接持续时间     |

***

## 使用示例

### 客户端基本使用

```csharp
using System;
using System.Threading.Tasks;
using Chet.Utils.Helpers;

class Program
{
    static async Task Main(string[] args)
    {
        var wsHelper = new WebSocketHelper();
        
        // 订阅事件
        wsHelper.OnConnected += (sender, e) => 
            Console.WriteLine($"Connected to {e.Uri} at {e.ConnectedTime}");
        
        wsHelper.OnDisconnected += (sender, e) => 
            Console.WriteLine($"Disconnected: {e.CloseStatus} - {e.StatusDescription}");
        
        wsHelper.OnMessageReceived += (sender, e) => 
            Console.WriteLine($"Received: {e.Message}");
        
        wsHelper.OnBinaryReceived += (sender, e) => 
            Console.WriteLine($"Received binary data: {e.Data.Length} bytes");
        
        wsHelper.OnError += (sender, e) => 
            Console.WriteLine($"Error: {e.Exception.Message}");
        
        wsHelper.OnHeartbeat += (sender, e) => 
            Console.WriteLine($"Heartbeat: {(e.IsSuccess ? "Success" : e.ErrorMessage)}");
        
        wsHelper.OnReconnecting += (sender, e) => 
            Console.WriteLine($"Reconnecting: attempt {e.Attempt}/{e.MaxAttempts}");
        
        try
        {
            await wsHelper.ConnectAsync(new Uri("wss://echo.websocket.org"));
            await wsHelper.SendMessageAsync("Hello WebSocket!");
            await Task.Delay(5000);
            await wsHelper.DisconnectAsync();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Exception: {ex.Message}");
        }
        finally
        {
            wsHelper.Dispose();
        }
    }
}
```

### 服务器基本使用

```csharp
using System;
using System.Threading.Tasks;
using Chet.Utils.Helpers;

class Program
{
    static async Task Main(string[] args)
    {
        var server = new WebSocketServerHelper(heartbeatIntervalSec: 30);
        
        // 订阅事件
        server.OnServerStarted += (sender, e) => 
            Console.WriteLine("Server started");
        
        server.OnClientConnected += (sender, e) => 
            Console.WriteLine($"Client connected: {e.ClientInfo.ClientId}");
        
        server.OnClientDisconnected += (sender, e) => 
            Console.WriteLine($"Client disconnected: {e.ClientId}");
        
        server.OnMessageReceived += async (sender, e) =>
        {
            Console.WriteLine($"Message from {e.ClientId}: {e.Message}");
            await server.SendMessageToClientAsync(e.ClientId, $"Echo: {e.Message}");
        };
        
        server.OnError += (sender, e) => 
            Console.WriteLine($"Error: {e.Exception.Message}");
        
        try
        {
            await server.StartServerAsync("http://localhost:8080/");
            Console.WriteLine("Press any key to stop...");
            Console.ReadKey();
        }
        finally
        {
            server.Dispose();
        }
    }
}
```

### 消息队列使用

```csharp
var wsHelper = new WebSocketHelper();
wsHelper.EnableMessageQueue = true;

// 即使未连接，消息也会被缓存
await wsHelper.SendMessageAsync("Message 1");
await wsHelper.SendMessageAsync("Message 2");

Console.WriteLine($"Queued messages: {wsHelper.QueuedMessageCount}"); // 输出: 2

// 连接后自动发送队列中的消息
await wsHelper.ConnectAsync(new Uri("wss://example.com"));
```

### 组播消息

```csharp
var server = new WebSocketServerHelper();

server.OnClientConnected += (sender, e) =>
{
    // 将客户端添加到特定组
    server.AddClientToGroup(e.ClientInfo.ClientId, "chat-room");
};

// 向组广播消息
await server.BroadcastToGroupAsync("chat-room", "Hello everyone in the chat room!");
```

***

## 最佳实践

### 1. 正确处理生命周期

```csharp
public class WebSocketService : IDisposable
{
    private WebSocketHelper _webSocketHelper;
    private bool _disposed;
    
    public async Task StartAsync()
    {
        _webSocketHelper = new WebSocketHelper();
        _webSocketHelper.OnMessageReceived += OnMessageReceived;
        await _webSocketHelper.ConnectAsync(new Uri("wss://server"));
    }
    
    public async Task StopAsync()
    {
        if (_webSocketHelper != null)
        {
            await _webSocketHelper.DisconnectAsync();
            _webSocketHelper.OnMessageReceived -= OnMessageReceived;
            _webSocketHelper.Dispose();
            _webSocketHelper = null;
        }
    }
    
    public void Dispose()
    {
        if (!_disposed)
        {
            StopAsync().GetAwaiter().GetResult();
            _disposed = true;
        }
    }
}
```

### 2. 异常处理

```csharp
try
{
    await wsHelper.ConnectAsync(new Uri("wss://server"));
}
catch (WebSocketException wsex)
{
    Console.WriteLine($"WebSocket error: {wsex.Message}");
}
catch (OperationCanceledException)
{
    Console.WriteLine("Connection cancelled");
}
catch (Exception ex)
{
    Console.WriteLine($"General error: {ex.Message}");
}
```

### 3. 消息处理

```csharp
wsHelper.OnMessageReceived += async (sender, e) =>
{
    try
    {
        var data = JsonSerializer.Deserialize<YourDataType>(e.Message);
        await ProcessMessageAsync(data);
    }
    catch (JsonException jsonEx)
    {
        Console.WriteLine($"Failed to parse message: {jsonEx.Message}");
    }
};
```

***

## 注意事项

1. **线程安全**: WebSocketHelper 设计为线程安全，可以在多个线程中同时调用其方法。
2. **资源管理**: 使用完毕后请调用 Dispose() 方法释放资源，或者使用 using 语句。
3. **异常处理**: 网络操作可能会抛出各种异常，建议始终使用 try-catch 包装关键操作。
4. **自动重连**: 类内置自动重连机制，但不会无限重试，达到最大重连次数后会彻底断开连接。
5. **心跳检测**: 默认每 30 秒发送一次心跳包，可以通过构造函数自定义间隔或禁用。
6. **消息队列**: 启用消息队列后，断线时的消息会被缓存，重连后自动发送。

***

## 版本兼容性

此类基于 .NET Standard 2.0+ 实现，兼容以下平台：

- .NET Core 2.0+
- .NET Framework 4.6.1+
- .NET 5.0+

***

## 故障排除

### 连接失败

1. 检查 URI 格式是否正确（应以 ws\:// 或 wss\:// 开头）
2. 确认网络连接正常
3. 检查防火墙设置
4. 验证服务器是否可用

### 消息无法发送

1. 确认连接状态为 Open
2. 检查消息大小是否超出限制
3. 查看是否有异常事件被触发

### 心跳超时

1. 检查网络延迟
2. 调整心跳间隔时间
3. 确认服务器支持 Ping/Pong 帧

### 服务器启动失败

1. 确保端口未被占用
2. 检查 URL 前缀格式（必须以 / 结尾）
3. 确保有足够的权限监听指定端口

