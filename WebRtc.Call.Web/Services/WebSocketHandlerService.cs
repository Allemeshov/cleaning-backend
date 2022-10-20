using System.Collections.Concurrent;
using System.Net.WebSockets;
using System.Text;
using chores.BLL;

namespace WebRtc.Call.Web.Services;

public class WebSocketHandlerService
{
    private ConcurrentDictionary<long, WebSocket> _clients;

    public WebSocketHandlerService()
    {
        _clients = new ConcurrentDictionary<long, WebSocket>();
    }

    public async Task LoopReceive(long id, WebSocket webSocket, CancellationToken cancellationToken)
    {
        if (_clients.TryGetValue(id, out var ws))
        {
            await ws.CloseAsync(WebSocketCloseStatus.NormalClosure, "Another connection", cancellationToken);
            _clients.TryRemove(id, out _);
        }

        if (!_clients.TryAdd(id, webSocket))
        {
            throw new BusinessException($"Failed to save Web Socket of (id: {id}) client");
        }

        byte[] buffer = new byte[8192];
        
        while (webSocket.State == WebSocketState.Open)
        {
            var receiveResult = await webSocket.ReceiveAsync(buffer, cancellationToken);

            if (receiveResult.MessageType == WebSocketMessageType.Text)
            {
                string request = Encoding.UTF8.GetString(buffer, 0, receiveResult.Count);

                await Send(id, $"ECHO: {request}", cancellationToken);
            }
        }
    }

    public async Task Send(long id, string text, CancellationToken cancellationToken)
    {
        if (_clients.TryGetValue(id, out var ws))
        {
            await ws.SendAsync(Encoding.UTF8.GetBytes(text), WebSocketMessageType.Text, WebSocketMessageFlags.EndOfMessage, cancellationToken);
        }
    }
}