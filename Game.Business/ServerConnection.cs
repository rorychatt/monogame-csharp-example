using System.Net.WebSockets;
using System.Text;

namespace Game.Business;
public class ServerConnection : IServerConnection
{
    private readonly string _webSocketUrl;
    public ClientWebSocket _webSocket { get; init; }

    public ServerConnection(string webSocketUrl)
    {
        _webSocketUrl = webSocketUrl;
        _webSocket = new ClientWebSocket();
    }

    public async Task ConnectAsync()
    {
        try
        {
            await _webSocket.ConnectAsync(new Uri(_webSocketUrl), CancellationToken.None);
            Console.WriteLine("Connected to the WebSocket server.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Failed to connect: {ex.Message}");
        }
    }

    public async Task SendAsync(string message)
    {
        if (_webSocket.State == WebSocketState.Open)
        {
            var buffer = Encoding.UTF8.GetBytes(message);
            await _webSocket.SendAsync(new ArraySegment<byte>(buffer), WebSocketMessageType.Text, true, CancellationToken.None);
            Console.WriteLine("Message sent to the server.");
        }
        else
        {
            Console.WriteLine("WebSocket connection is not open.");
        }
    }

    public async Task<string> ReceiveAsync()
    {
        if (_webSocket.State == WebSocketState.Open)
        {
            var buffer = new byte[1024];
            var result = await _webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
            return Encoding.UTF8.GetString(buffer, 0, result.Count);
        }
        else
        {
            return "WebSocket connection is not open.";
        }
    }

    public async Task DisconnectAsync()
    {
        if (_webSocket != null && _webSocket.State == WebSocketState.Open)
        {
            await _webSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, string.Empty, CancellationToken.None);
            Console.WriteLine("Disconnected from the WebSocket server.");
        }
    }
}