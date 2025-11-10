using Chet.Utils.Helpers;
using System.Net.WebSockets;
using System.Text.Json;
using System.Threading.Tasks;

namespace ClientApp
{
    class Program
    {
        private static WebSocketHelper _webSocketHelper;
        private static bool _isRunning = true;
        private static int _messageCount = 0;
        private static DateTime _lastHeartbeatTime;
        private static bool _autoTestMode = false;
        private static Task _autoTestTask;
        private static CancellationTokenSource _autoTestCts;

        static async Task Main(string[] args)
        {
            PrintHeader("WebSocket客户端测试工具");
            Console.WriteLine("命令说明：");
            Console.WriteLine("  s - 发送测试消息");
            Console.WriteLine("  d - 断开连接");
            Console.WriteLine("  c - 重新连接");
            Console.WriteLine("  t - 启动/停止自动测试模式");
            Console.WriteLine("  q - 退出程序");
            PrintSeparator();

            // 初始化WebSocketHelper
            _webSocketHelper = new WebSocketHelper(
                heartbeatInterval: 15000,  // 15秒心跳
                maxReconnectAttempts: 3,   // 最多重连3次
                reconnectDelay: 3000       // 重连延迟3秒
            );

            // 注册事件处理器
            RegisterEventHandlers();

            try
            {
                // 初始连接
                await ConnectToServer();

                // 启动命令处理循环
                await HandleUserCommands();
            }
            finally
            {
                // 停止自动测试（如果正在运行）
                if (_autoTestCts != null)
                {
                    _autoTestCts.Cancel();
                    if (_autoTestTask != null)
                    {
                        await Task.WhenAny(_autoTestTask, Task.Delay(2000));
                    }
                    _autoTestCts.Dispose();
                }

                // 清理资源
                if (_webSocketHelper != null)
                {
                    await _webSocketHelper.DisconnectAsync();
                    _webSocketHelper.Dispose();
                }
                LogInfo("客户端已退出");
            }
        }

        private static void RegisterEventHandlers()
        {
            _webSocketHelper.OnConnected += (sender, e) =>
            {
                LogEvent("连接成功", "成功连接到服务器！");
                // 发送连接信息给服务器
                SendClientInfo();
            };

            _webSocketHelper.OnDisconnected += (sender, closeStatus) =>
            {
                LogEvent("连接断开", $"与服务器断开连接，关闭状态: {closeStatus}");
            };

            _webSocketHelper.OnMessageReceived += (sender, message) =>
            {
                // 尝试解析JSON消息
                if (TryParseJsonMessage(message, out var jsonMessage))
                {
                    LogEvent("接收消息", $"类型: {jsonMessage.Type}, 内容: {jsonMessage.Content}");
                }
                else
                {
                    LogEvent("接收消息", message);
                }
            };

            _webSocketHelper.OnError += (sender, exception) =>
            {
                LogError("发生错误", exception.Message, exception.GetType().Name);
            };

            _webSocketHelper.OnHeartbeat += (sender, e) =>
            {
                _lastHeartbeatTime = DateTime.Now;
                LogEvent("心跳发送", $"{_lastHeartbeatTime:HH:mm:ss.fff}");
            };
        }

        private static async Task ConnectToServer()
        {
            try
            {
                LogInfo("正在连接到服务器...");
                await _webSocketHelper.ConnectAsync(new Uri("ws://localhost:8080"));
            }
            catch (Exception ex)
            {
                LogError("连接失败", ex.Message, ex.GetType().Name);
            }
        }

        private static async Task HandleUserCommands()
        {
            // 检查是否有标准输入（用于管道输入）
            bool hasStdIn = !Console.IsInputRedirected;
            
            // 处理管道输入的单个命令
            if (!hasStdIn && Console.In.Peek() != -1)
            {
                string input = Console.ReadLine();
                if (!string.IsNullOrEmpty(input))
                {
                    char key = input.Trim().ToLower()[0];
                    await ProcessCommandAsync(key);
                }
                return;
            }
            
            // 标准控制台模式 - 持续监听按键
            while (_isRunning && hasStdIn)
            {
                // 使用KeyAvailable检查以避免阻塞
                if (Console.KeyAvailable)
                {
                    var key = Console.ReadKey(true).Key;
                    await ProcessCommandAsync(key);
                }

                // 避免CPU占用过高
                await Task.Delay(50);
            }
        }
        
        private static async Task ProcessCommandAsync(ConsoleKey key)
        {
            switch (key)
            {
                case ConsoleKey.S:
                    if (_webSocketHelper.IsConnected)
                    {
                        await SendTestMessage();
                    }
                    else
                    {
                        LogWarning("未连接到服务器，请先连接");
                    }
                    break;

                case ConsoleKey.D:
                    if (_webSocketHelper.IsConnected)
                    {
                        LogInfo("正在断开连接...");
                        await _webSocketHelper.DisconnectAsync();
                    }
                    else
                    {
                        LogWarning("未连接到服务器");
                    }
                    break;

                case ConsoleKey.C:
                    if (!_webSocketHelper.IsConnected)
                    {
                        await ConnectToServer();
                    }
                    else
                    {
                        LogWarning("已经连接到服务器");
                    }
                    break;

                case ConsoleKey.T:
                    ToggleAutoTestMode();
                    break;

                case ConsoleKey.Q:
                    _isRunning = false;
                    LogInfo("正在退出...");
                    break;

                default:
                    LogWarning("未知命令，请参考命令说明");
                    break;
            }
        }
        
        private static async Task ProcessCommandAsync(char keyChar)
        {
            switch (char.ToLower(keyChar))
            {
                case 's':
                    await ProcessCommandAsync(ConsoleKey.S);
                    break;
                case 'd':
                    await ProcessCommandAsync(ConsoleKey.D);
                    break;
                case 'c':
                    await ProcessCommandAsync(ConsoleKey.C);
                    break;
                case 't':
                    await ProcessCommandAsync(ConsoleKey.T);
                    break;
                case 'q':
                    await ProcessCommandAsync(ConsoleKey.Q);
                    break;
                default:
                    Console.WriteLine("未知命令，请参考命令说明");
                    break;
            }
        }

        private static void ToggleAutoTestMode()
        {
            _autoTestMode = !_autoTestMode;

            if (_autoTestMode)
            {
                PrintHeader("自动测试模式");
                Console.WriteLine("将进行以下测试：");
                Console.WriteLine("1. 发送10条测试消息（每2秒一条）");
                Console.WriteLine("2. 断开并重新连接");
                Console.WriteLine("3. 再次发送5条测试消息");
                PrintSeparator();

                _autoTestCts = new CancellationTokenSource();
                _autoTestTask = RunAutoTest(_autoTestCts.Token);
            }
            else
            {
                if (_autoTestCts != null)
                {
                    _autoTestCts.Cancel();
                    _autoTestCts.Dispose();
                    _autoTestCts = null;
                }
                LogInfo("已停止自动测试模式");
            }
        }

        private static async Task RunAutoTest(CancellationToken cancellationToken)
        {
            try
            {
                // 等待连接建立
                if (!_webSocketHelper.IsConnected)
                {
                    LogInfo("自动测试等待连接建立...");
                    await ConnectToServer();
                    await Task.Delay(1000, cancellationToken);
                }

                // 第一阶段：发送10条测试消息
                LogInfo("开始第一阶段测试：发送多条消息");
                for (int i = 1; i <= 10 && !cancellationToken.IsCancellationRequested; i++)
                {
                    await SendTestMessage($"自动测试 - 消息 #{i}");
                    await Task.Delay(2000, cancellationToken);
                }

                if (cancellationToken.IsCancellationRequested) return;

                // 第二阶段：断开并重新连接
                LogInfo("开始第二阶段测试：断开并重新连接");
                await _webSocketHelper.DisconnectAsync();
                await Task.Delay(2000, cancellationToken);
                await ConnectToServer();
                await Task.Delay(1000, cancellationToken);

                if (cancellationToken.IsCancellationRequested) return;

                // 第三阶段：再次发送5条测试消息
                LogInfo("开始第三阶段测试：重连后发送消息");
                for (int i = 1; i <= 5 && !cancellationToken.IsCancellationRequested; i++)
                {
                    await SendTestMessage($"重连后测试 - 消息 #{i}");
                    await Task.Delay(2000, cancellationToken);
                }

                if (!cancellationToken.IsCancellationRequested)
                {
                    PrintSeparator();
                    LogInfo("自动测试完成！");
                    PrintSeparator();
                }
            }
            catch (OperationCanceledException)
            {
                LogInfo("自动测试已取消");
            }
            catch (Exception ex)
            {
                LogError("自动测试出错", ex.Message);
            }
            finally
            {
                _autoTestMode = false;
            }
        }

        private static async Task SendTestMessage()
        {
            _messageCount++;
            await SendTestMessage($"手动发送 - 消息 #{_messageCount} - {DateTime.Now:HH:mm:ss.fff}");
        }

        private static async Task SendTestMessage(string message)
        {
            try
            {
                if (_webSocketHelper.IsConnected)
                {
                    // 创建结构化消息
                    var structuredMessage = new ClientMessage
                    {
                        Type = "test",
                        Content = message,
                        Timestamp = DateTime.Now,
                        Sequence = _messageCount
                    };

                    string jsonMessage = JsonSerializer.Serialize(structuredMessage);
                    await _webSocketHelper.SendMessageAsync(jsonMessage);
                    LogInfo($"发送消息: {message}");
                }
                else
                {
                    LogWarning("发送失败：未连接到服务器");
                }
            }
            catch (Exception ex)
            {
                LogError("发送消息失败", ex.Message);
            }
        }

        private static void SendClientInfo()
        {
            try
            {
                var clientInfo = new ClientMessage
                {
                    Type = "client_info",
                    Content = $"客户端连接",
                    Timestamp = DateTime.Now,
                    ClientName = "WebSocket测试客户端",
                    ClientVersion = "1.0.0",
                    Sequence = 0
                };

                string jsonMessage = JsonSerializer.Serialize(clientInfo);
                _webSocketHelper.SendMessageAsync(jsonMessage);
            }
            catch (Exception ex)
            {
                LogError("发送客户端信息失败", ex.Message);
            }
        }

        private static bool TryParseJsonMessage(string message, out ClientMessage? jsonMessage)
        {
            jsonMessage = null;
            try
            {
                jsonMessage = JsonSerializer.Deserialize<ClientMessage>(message);
                return jsonMessage != null;
            }
            catch
            {
                return false;
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

        private class ClientMessage
        {
            public string Type { get; set; } = "test";
            public string Content { get; set; } = string.Empty;
            public DateTime Timestamp { get; set; } = DateTime.Now;
            public int Sequence { get; set; } = 0;
            public string? ClientName { get; set; }
            public string? ClientVersion { get; set; }
        }

        #endregion
    }
}