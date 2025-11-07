# WebSocketHelper 帮助类文档

## 概述

WebSocketHelper 是一个功能全面的 WebSocket 客户端辅助类，封装了 WebSocket 连接管理、消息收发、心跳检测、自动重连等常用功能。它提供了事件驱动的编程模型，使 WebSocket 的使用更加简单和可靠。

## 主要特性

- 🔄 自动连接管理和重连机制
- 💓 内置心跳检测功能
- 📨 支持文本和二进制消息收发
- ⚡ 事件驱动的异步编程模型
- 🔒 线程安全的操作
- 🧹 自动资源管理和清理

## 类定义

public class WebSocketHelper : IDisposable

## 构造函数

WebSocketHelper(int, int, int)

初始化 WebSocketHelper 实例

### 参数

| 参数名 | 类型 | 描述 |
|--------|------|------|
| heartbeatInterval | int | 心跳检测间隔（毫秒），默认 30000ms |
| maxReconnectAttempts | int | 最大重连次数，默认 5 次 |
| reconnectDelay | int | 重连延迟时间（毫秒），默认 5000ms |

### 示例

```csharp
// 使用默认配置
var wsHelper = new WebSocketHelper();

// 自定义配置
var wsHelper = new WebSocketHelper(heartbeatInterval: 10000, maxReconnectAttempts: 3, reconnectDelay: 2000);
```

## 属性

| 属性名 | 类型 | 描述 |
|--------|------|------|
| State | WebSocketState | 获取当前 WebSocket 连接状态 |
| IsConnected | bool | 获取是否已建立连接 |

事件

### OnConnected

连接成功时触发

```csharp
public event EventHandler OnConnected;
```

### OnDisconnected

连接断开时触发

```csharp
public event EventHandler<WebSocketCloseStatus?> OnDisconnected;
```

### OnMessageReceived

接收到消息时触发

```csharp
public event EventHandler<string> OnMessageReceived;
```

### OnError

发生错误时触发

```csharp
public event EventHandler<Exception> OnError;
```

### OnHeartbeat

心跳检测时触发

```csharp
public event EventHandler OnHeartbeat;
```

## 方法

### ConnectAsync(Uri, CancellationToken)

连接到 WebSocket 服务器

#### 参数

| 参数名 | 类型 | 描述 |
|--------|------|------|
| uri | Uri | WebSocket 服务器地址 |
| cancellationToken | CancellationToken | 取消令牌 |

#### 返回值

Task: 连接任务

#### 异常

- ArgumentNullException: URI 为空时抛出

#### 示例

```csharp
try
{
    await wsHelper.ConnectAsync(new Uri("wss://echo.websocket.org"));
    Console.WriteLine("Connected successfully");
}
catch (Exception ex)
{
    Console.WriteLine($"Connection failed: {ex.Message}");
}
```

### DisconnectAsync(WebSocketCloseStatus, string)

断开 WebSocket 连接

#### 参数

| 参数名 | 类型 | 描述 |
|--------|------|------|
| closeStatus | WebSocketCloseStatus | 关闭状态，默认 NormalClosure |
| statusDescription | string | 状态描述，默认 "Normal closure" |

#### 返回值

Task: 断开连接任务

#### 示例

```csharp
await wsHelper.DisconnectAsync();
```

### SendMessageAsync(string, CancellationToken)

发送文本消息

#### 参数

| 参数名 | 类型 | 描述 |
|--------|------|------|
| message | string | 要发送的消息 |
| cancellationToken | CancellationToken | 取消令牌 |

#### 返回值

Task: 发送任务

#### 异常

- InvalidOperationException: 未连接时抛出

#### 示例

```csharp
await wsHelper.SendMessageAsync("Hello WebSocket Server!");
```

### SendBinaryAsync(byte[], CancellationToken)

发送二进制消息

#### 参数

| 参数名 | 类型 | 描述 |
|--------|------|------|
| data | byte[] | 要发送的二进制数据 |
| cancellationToken | CancellationToken | 取消令牌 |

#### 返回值

Task: 发送任务

#### 异常

- InvalidOperationException: 未连接时抛出

#### 示例

```csharp
byte[] binaryData = Encoding.UTF8.GetBytes("Binary data");
await wsHelper.SendBinaryAsync(binaryData);
```

### SendPingAsync(CancellationToken)

发送心跳包(Ping帧)

#### 参数

| 参数名 | 类型 | 描述 |
|--------|------|------|
| cancellationToken | CancellationToken | 取消令牌 |

#### 返回值

Task: 发送任务

#### 异常

- InvalidOperationException: 未连接时抛出

## 使用示例

### 基本使用

```csharp
using System;
using System.Threading.Tasks;

class Program
{
    static async Task Main(string[] args)
    {
        var wsHelper = new WebSocketHelper();
        
        // 订阅事件
        wsHelper.OnConnected += (sender, e) => Console.WriteLine("Connected to server");
        wsHelper.OnDisconnected += (sender, e) => Console.WriteLine($"Disconnected: {e}");
        wsHelper.OnMessageReceived += (sender, message) => Console.WriteLine($"Received: {message}");
        wsHelper.OnError += (sender, ex) => Console.WriteLine($"Error: {ex.Message}");
        wsHelper.OnHeartbeat += (sender, e) => Console.WriteLine("Heartbeat sent");
        
        try
        {
            // 连接到WebSocket服务器
            await wsHelper.ConnectAsync(new Uri("wss://echo.websocket.org"));
            
            // 发送消息
            await wsHelper.SendMessageAsync("Hello WebSocket!");
            
            // 等待一段时间以接收响应
            await Task.Delay(5000);
            
            // 断开连接
            await wsHelper.DisconnectAsync();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Exception: {ex.Message}");
        }
    }
}
```

### 高级配置使用

```csharp
// 配置快速心跳和有限重连
var wsHelper = new WebSocketHelper(
    heartbeatInterval: 10000,     // 10秒心跳
    maxReconnectAttempts: 3,      // 最多重连3次
    reconnectDelay: 2000          // 2秒后重连
);

// 订阅所有事件
wsHelper.OnConnected += (sender, e) => {
    Console.WriteLine("WebSocket connected");
};

wsHelper.OnDisconnected += (sender, status) => {
    Console.WriteLine($"WebSocket disconnected with status: {status}");
};

wsHelper.OnMessageReceived += async (sender, message) => {
    Console.WriteLine($"Message received: {message}");
    
    // 回复消息
    var helper = sender as WebSocketHelper;
    await helper.SendMessageAsync($"Echo: {message}");
};

wsHelper.OnError += (sender, ex) => {
    Console.WriteLine($"WebSocket error: {ex.Message}");
};

wsHelper.OnHeartbeat += (sender, e) => {
    Console.WriteLine("Heartbeat keep-alive signal sent");
};
```

## 最佳实践

### 1. 正确处理生命周期

```csharp
public class WebSocketService
{
    private WebSocketHelper _webSocketHelper;
    
    public async Task StartAsync()
    {
        _webSocketHelper = new WebSocketHelper();
        // 配置事件处理...
        await _webSocketHelper.ConnectAsync(new Uri("wss://your-websocket-server"));
    }
    
    public async Task StopAsync()
    {
        if (_webSocketHelper != null)
        {
            await _webSocketHelper.DisconnectAsync();
            _webSocketHelper.Dispose();
            _webSocketHelper = null;
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
catch (Exception ex)
{
    Console.WriteLine($"General error: {ex.Message}");
}
```

### 3. 消息处理

```csharp
wsHelper.OnMessageReceived += async (sender, message) => {
    try
    {
        // 解析消息
        var data = JsonSerializer.Deserialize<YourDataType>(message);
        
        // 处理业务逻辑
        await ProcessMessageAsync(data);
    }
    catch (JsonException jsonEx)
    {
        Console.WriteLine($"Failed to parse message: {jsonEx.Message}");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error processing message: {ex.Message}");
    }
};
```

## 注意事项

1. **线程安全**: WebSocketHelper 设计为线程安全，可以在多个线程中同时调用其方法。
2. **资源管理**: 使用完毕后请调用 Dispose() 方法释放资源，或者使用 using 语句。
3. **异常处理**: 网络操作可能会抛出各种异常，建议始终使用 try-catch 包装关键操作。
4. **自动重连**: 类内置自动重连机制，但不会无限重试，达到最大重连次数后会彻底断开连接。
5. **心跳检测**: 默认每30秒发送一次心跳包，可以通过构造函数自定义间隔。

## 版本兼容性

此类基于 .NET Standard 2.0+ 实现，兼容以下平台：
- .NET Core 2.0+
- .NET Framework 4.6.1+
- Mono 5.4+
- Xamarin.iOS 10.14+
- Xamarin.Mac 3.8+
- Xamarin.Android 8.0+

## 故障排除

### 连接失败

1. 检查 URI 格式是否正确（应以 ws:// 或 wss:// 开头）
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