namespace Game.Business;
public interface IServerConnection
{
    public Task ConnectAsync();
    public Task SendAsync(string message);
    public Task<string> ReceiveAsync();
    public Task DisconnectAsync();

}