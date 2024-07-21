using System.Net.WebSockets;
using Game.Business;

namespace GameTests;

public class ServerConnectionTests
{
    [Fact]
    public async void ConnectAsync_WhenCalled_ConnectsToWebSocketServer()
    {
        // Arrange
        var webSocketUrl = "ws://localhost:5000";
        var serverConnection = new ServerConnection(webSocketUrl);

        // Act
        await serverConnection.ConnectAsync();

        // Assert
        Assert.Equal(WebSocketState.Open, serverConnection._webSocket.State);
    }

}