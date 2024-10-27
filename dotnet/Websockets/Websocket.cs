using System.Collections.Concurrent;
using System.Net.WebSockets;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;

public class Websocket {
    private readonly RequestDelegate _next;
    private readonly IServiceScopeFactory _serviceScopeFactory;
    protected readonly ConcurrentDictionary<string, ConcurrentDictionary<string, WebSocket>> _websockets = new();
    protected readonly byte[] _byte = new byte[1024 * 4];

    public Websocket(RequestDelegate next, IServiceScopeFactory serviceScopeFactory) {
        _next = next;
        _serviceScopeFactory = serviceScopeFactory;
    }

    public async Task StoreWebsocket(string userid, string wsid, WebSocket ws) {

        await ws.ReceiveAsync(new ArraySegment<byte>(_byte), CancellationToken.None);

        _websockets.TryGetValue(userid, out var value);
        value!.TryRemove(wsid, out _);
        if(value.Count == 0) {
            _websockets.TryRemove(userid, out _);
        }

        await ws.CloseAsync(WebSocketCloseStatus.NormalClosure, "Closing", CancellationToken.None);
        Console.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(_websockets, Newtonsoft.Json.Formatting.Indented));
    }

    public async Task InvokeAsync(HttpContext context) {

        if (context.WebSockets.IsWebSocketRequest) {

            using var scope = _serviceScopeFactory.CreateScope();
            var authorizationService = scope.ServiceProvider.GetRequiredService<IAuthorizationService>();

            var authorization = await authorizationService.AuthorizeAsync(context.User, null, UserPolicy._policy);
            if(!authorization.Succeeded) {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                return;
            }

            if (context.Request.Path == "/chat") {

                var ws = await context.WebSockets.AcceptWebSocketAsync();
                if(ws.State != WebSocketState.Open) {
                    context.Response.StatusCode = StatusCodes.Status409Conflict;
                    return;
                }

                var userid = context.User.Claims.FirstOrDefault(f => f.Type == ClaimTypes.NameIdentifier)!.Value;
                var wsid = Guid.NewGuid() + "-" + DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();

                _websockets.AddOrUpdate(
                    userid,
                    new ConcurrentDictionary<string, WebSocket>(new[] { new KeyValuePair<string, WebSocket>(wsid, ws) }),
                    (key, websockets) => {
                        websockets.AddOrUpdate(wsid, ws, (wsKey, existws) => ws);
                        return websockets;
                    }
                );

                Console.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(_websockets, Newtonsoft.Json.Formatting.Indented));
                await StoreWebsocket(userid, wsid, ws);
            }

        } else {

            await _next(context);
        }
    }
}
