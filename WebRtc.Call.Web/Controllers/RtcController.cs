using System.Net.WebSockets;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using WebRtc.Call.Web.Services;

namespace WebRtc.Call.Web.Controllers;

[Controller]
[Route("[controller]/[action]")]
public class RtcController : Controller
{
    private WebSocketHandlerService _webSocketHandlerService;

    public RtcController(WebSocketHandlerService webSocketHandlerService)
    {
        _webSocketHandlerService = webSocketHandlerService;
    }

    [HttpGet("{id:long}")]
    [SwaggerOperation("вебсокет (id пользователя, пока без проверок на реальность)")]
    public async Task Ws(long id, CancellationToken cancellationToken)
    {
        if (HttpContext.WebSockets.IsWebSocketRequest)
        {
            using var webSocket = await HttpContext.WebSockets.AcceptWebSocketAsync();

            await _webSocketHandlerService.LoopReceive(id, webSocket, cancellationToken);

            await webSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, "Normal Closure", cancellationToken);
        }
        else
        {
            HttpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
        }
    }
}