using System;
using System.Collections.Generic;
using System.Threading;
using System.Text.Json;
using System.Collections.Concurrent;
using Chet.Utils.Helpers;

namespace Chet.Utils.Tests.Module.ServerApp
{
    class Program
    {
        private static WebSocketServerHelper? _server;
        // 存储客户端信息
        private static ConcurrentDictionary<string, ClientInfo> _connectedClients = new ConcurrentDictionary<string, ClientInfo>();
        private static int _messageCount = 0;
        private static bool _isRunning = true;

        static async Task Main(string[] args)
        {
            PrintHeader("WebSocket服务器测试工具");
            Console.WriteLine("命令说明：");
            Console.WriteLine("  b - 广播消息给所有客户端");
            Console.WriteLine("  l - 显示当前连接的客户端列表");
            Console.WriteLine("  s - 停止服务器");
            Console.WriteLine("  ESC - 停止服务器并退出");
            PrintSeparator();

            // 初始化服务器
            _server = new WebSocketServerHelper(8080);
            _connectedClients = new ConcurrentDictionary<string, ClientInfo>();
            _messageCount = 0;

            // 注册事件处理器
            RegisterEventHandlers();

            // 启动服务器
            try
            {
                LogInfo("正在启动WebSocket服务器...");
                var port = 8080;
                await _server.StartServerAsync($"http://localhost:{port}/");
                LogInfo($"服务器已启动，监听: http://localhost:{port}/");
            }
            catch (Exception ex)
            {
                LogError("启动失败", ex.Message, ex.GetType().Name);
                return;
            }

            try
            {
                // 命令处理循环
                while (_isRunning)
                {
                    if (Console.KeyAvailable)
                    {
                        var key = Console.ReadKey(true).Key;
                        await ProcessCommandAsync(key);
                    }
                    await Task.Delay(50);
                }
            }
            finally
            {
                // 停止服务器
                if (_server != null && _server.IsRunning)
                {
                    LogInfo("正在停止服务器...");
                    _server.StopServer();
                    LogEvent("服务器已停止", "");
                }
                LogInfo("服务器程序已退出");
            }
        }

        private static void RegisterEventHandlers()
        {
            if (_server == null) return;

            // 服务器启动事件
            _server.OnServerStarted += (sender, e) =>
            {
                LogEvent("服务器已启动", "");
            };

            // 服务器停止事件
            _server.OnServerStopped += (sender, e) =>
            {
                LogEvent("服务器已停止", "");
                // 清理客户端列表
                _connectedClients.Clear();
            };

            // 客户端连接事件
            _server.OnClientConnected += (sender, e) =>
            {
                string clientId = e.ClientInfo.ClientId;
                if (!string.IsNullOrEmpty(clientId))
                {
                    _connectedClients.TryAdd(clientId, new ClientInfo { ClientId = clientId, ConnectedTime = DateTime.Now });
                    LogEvent("客户端已连接", clientId);
                }
                LogInfo($"当前连接数: {_connectedClients.Count}");
            };

            // 客户端断开事件
            _server.OnClientDisconnected += (sender, e) =>
            {
                string clientId = e.ClientId;
                if (_connectedClients.TryRemove(clientId, out _))
                {
                    LogEvent("客户端已断开", clientId);
                    LogInfo($"当前连接数: {_connectedClients.Count}");
                }
            };

            // 消息接收事件
            _server.OnMessageReceived += async (sender, e) =>
            {
                _messageCount++;
                try
                {
                    // 从事件参数中获取客户端ID和消息内容
                    string clientId = e.ClientId;
                    string messageContent = e.Message; // 假设e有Message属性包含实际消息内容
                    
                    // 如果没有Message属性，则尝试使用e.ToString()
                    if (string.IsNullOrEmpty(messageContent))
                    {
                        messageContent = e.ToString();
                    }
                    
                    LogInfo($"收到消息: {messageContent}, 客户端ID: {clientId}");

                    // 确保客户端ID有效且不为WebSocketServerHelper
                    if (!string.IsNullOrEmpty(clientId) && !clientId.Contains("WebSocketServerHelper"))
                    {
                        // 创建ServerMessage对象并处理消息
                        var serverMessage = new ServerMessage
                        {
                            ClientId = clientId,
                            Message = messageContent,
                            IsJson = messageContent.Trim().StartsWith('{') && messageContent.Trim().EndsWith('}')
                        };
                        await HandleMessage(serverMessage);
                    }
                    else
                    {
                        LogWarning("收到无效的客户端ID，忽略消息处理");
                    }
                }
                catch (Exception ex)
                {
                    LogError("处理消息异常", ex.Message);
                }
            };

            // 错误事件
            _server.OnError += (sender, e) =>
            {
                LogError("服务器错误", e.ToString(), "WebSocket错误");
            };
        }

        private static async Task ProcessCommandAsync(ConsoleKey key)
        {
            switch (key)
            {
                case ConsoleKey.B:
                    await BroadcastMessageAsync();
                    break;

                case ConsoleKey.L:
                    ListConnectedClients();
                    break;

                case ConsoleKey.S:
                    _isRunning = false;
                    LogInfo("准备停止服务器...");
                    break;

                case ConsoleKey.Escape:
                    _isRunning = false;
                    LogInfo("准备停止服务器并退出...");
                    break;

                default:
                    LogWarning("未知命令，请参考命令说明");
                    break;
            }
        }

        private static async Task HandleMessage(ServerMessage? message)
        {
            if (message == null)
            {
                LogWarning("收到空消息，忽略处理");
                return;
            }

            string clientId = message.ClientId;

            try
            {
                if (message.IsJson)
                {
                    var jsonMessage = JsonSerializer.Deserialize<JsonMessage>(message.Message);
                    if (jsonMessage != null)
                    {
                        switch (jsonMessage.Type)
                        {
                            case "client_info":
                                await HandleClientInfo(clientId, jsonMessage);
                                break;
                            case "test":
                                await HandleTestMessage(clientId, jsonMessage);
                                break;
                            case "ping":
                                await HandlePingMessage(clientId, jsonMessage);
                                break;
                            default:
                                LogInfo($"收到未知类型消息: {jsonMessage.Type}，客户端: {clientId}");
                                break;
                        }
                    }
                }
                else
                {
                    // 处理非JSON消息
                    LogInfo($"收到非JSON消息: {message.Message}，客户端: {clientId}");
                    // 简单回显
                    await SendResponse(clientId, "echo", "收到消息: " + message.Message);
                }
            }
            catch (Exception ex)
            {
                LogError("处理消息失败", ex.Message, $"客户端: {clientId}");
            }
        }

        private static async Task HandleClientInfo(string clientId, JsonMessage message)
        {
            // 更新客户端信息
            if (_connectedClients.TryGetValue(clientId, out var clientInfo))
            {
                clientInfo.ClientName = message.ClientName ?? "Unknown Client";
                clientInfo.ClientVersion = message.ClientVersion ?? "1.0.0";
                _connectedClients[clientId] = clientInfo;
            }

            LogInfo($"收到客户端信息: 客户端ID={clientId}, 名称={clientInfo?.ClientName}, 版本={clientInfo?.ClientVersion}");

            // 发送欢迎消息
            await SendWelcomeMessage(clientId);
            // 发送确认消息
            await SendResponse(clientId, "info_ack", "客户端信息已接收");
        }

        private static async Task HandleTestMessage(string clientId, JsonMessage message)
        {
            string content = message.Content ?? "[无内容]";
            LogInfo($"收到测试消息: 内容={content}, 客户端={clientId}");

            // 回显消息给客户端
            await SendResponse(clientId, "test_ack", $"已收到: {content}");
        }

        private static async Task HandlePingMessage(string clientId, JsonMessage message)
        {
            LogEvent("收到心跳", $"客户端: {clientId}");

            // 回复pong消息
            await SendResponse(clientId, "pong", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"));
        }

        private static async Task SendWelcomeMessage(string clientId)
        {
            var welcome = new JsonMessage
            {
                Type = "welcome",
                Content = "欢迎连接到WebSocket测试服务器",
                ServerTime = DateTime.Now,
                ServerVersion = "1.0.0"
            };
            await SendJsonMessage(clientId, welcome);
        }

        private static async Task SendResponseMessage(string clientId, string type, string content)
        {
            var response = new JsonMessage
            {
                Type = type,
                Content = content,
                ServerTime = DateTime.Now
            };
            await SendJsonMessage(clientId, response);
        }

        private static async Task SendResponse(string clientId, string type, string content)
        {
            await SendResponseMessage(clientId, type, content);
        }

        private static async Task SendJsonMessage(string clientId, JsonMessage message)
        {
            try
            {
                if (_server == null) return;
                string json = JsonSerializer.Serialize(message);
                await _server.SendMessageToClientAsync(clientId, json);
            }
            catch (Exception ex)
            {
                LogError("发送消息失败", ex.Message, $"目标客户端: {clientId}");
            }
        }

        private static async Task BroadcastMessageAsync()
        {
            Console.Write("请输入要广播的消息: ");
            string message = Console.ReadLine() ?? "";

            if (string.IsNullOrWhiteSpace(message))
            {
                LogWarning("广播消息不能为空");
                return;
            }

            var broadcastMsg = new JsonMessage
            {
                Type = "broadcast",
                Content = message,
                ServerTime = DateTime.Now,
                Sender = "SERVER"
            };

            try
            {
                if (_server == null) return;
                string json = JsonSerializer.Serialize(broadcastMsg);
                await _server.BroadcastMessageAsync(json);
                LogInfo($"广播成功，已发送到 {_connectedClients.Count} 个客户端");
            }
            catch (Exception ex)
            {
                LogError("广播失败", ex.Message);
            }
        }

        private static void ListConnectedClients()
        {
            PrintSeparator();
            Console.WriteLine($"当前连接的客户端列表 (总数: {_connectedClients.Count})");
            PrintSeparator();

            foreach (var kvp in _connectedClients)
            {
                var client = kvp.Value;
                Console.WriteLine($"客户端ID: {client.ClientId}");
                Console.WriteLine($"  名称: {client.ClientName ?? "未知"}");
                Console.WriteLine($"  版本: {client.ClientVersion ?? "未知"}");
                Console.WriteLine($"  连接时间: {client.ConnectedTime:yyyy-MM-dd HH:mm:ss.fff}");
                PrintSeparator();
            }
        }

        private static void BroadcastServerShutdownMessage()
        {
            var shutdownMsg = new JsonMessage
            {
                Type = "server_shutdown",
                Content = "服务器即将关闭",
                ServerTime = DateTime.Now
            };

            try
            {
                if (_server == null) return;
                string json = JsonSerializer.Serialize(shutdownMsg);
                _server.BroadcastMessageAsync(json).Wait(1000);
            }
            catch (Exception ex)
            {
                LogError("广播关闭消息失败", ex.Message);
            }
        }

        #region 辅助方法

        private static void PrintHeader(string title)
        {
            PrintSeparator();
            Console.WriteLine(title);
            PrintSeparator();
        }

        private static void PrintSeparator()
        {
            Console.WriteLine("========================================");
        }

        private static void LogInfo(string message)
        {
            Console.WriteLine($"[信息] {DateTime.Now:HH:mm:ss.fff} - {message}");
        }

        private static void LogWarning(string message)
        {
            Console.WriteLine($"[警告] {DateTime.Now:HH:mm:ss.fff} - {message}");
        }

        private static void LogError(string title, string message, string? details = null)
        {
            Console.WriteLine($"[错误] {DateTime.Now:HH:mm:ss.fff} - {title}: {message}");
            if (!string.IsNullOrEmpty(details))
            {
                Console.WriteLine($"      详情: {details}");
            }
        }

        private static void LogEvent(string eventType, string message)
        {
            Console.WriteLine($"[事件] {DateTime.Now:HH:mm:ss.fff} - {eventType}: {message}");
        }

        #endregion

        #region 数据模型

        private class ClientInfo
        {
            public string ClientId { get; set; } = string.Empty;
            public string? ClientName { get; set; }
            public string? ClientVersion { get; set; }
            public DateTime ConnectedTime { get; set; }
        }

        private class JsonMessage
        {
            public string Type { get; set; } = "test";
            public string? Content { get; set; }
            public DateTime ServerTime { get; set; }
            public string? ServerVersion { get; set; }
            public string? Sender { get; set; }
            public string? ClientName { get; set; }
            public string? ClientVersion { get; set; }
        }

        private class ServerMessage
        {
            public string ClientId { get; set; } = string.Empty;
            public string Message { get; set; } = string.Empty;
            public bool IsJson { get; set; } = false;
        }

        #endregion
    }
}