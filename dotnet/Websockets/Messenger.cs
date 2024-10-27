using System.Collections.Concurrent;
using System.Net.WebSockets;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;

public class Messenger {
    private readonly RequestDelegate _next;
    private readonly IServiceScopeFactory _serviceScopeFactory;
    private readonly ConcurrentDictionary<string, ConcurrentDictionary<string, WebSocket>> _websockets = new();
    private readonly byte[] _buffer = new byte[1024 * 4];
    private readonly Redis _redis;
    private readonly Mongo _mongo;
    

    public Messenger(RequestDelegate next, IServiceScopeFactory serviceScopeFactory, Redis redis, Mongo mongo) {
        _next = next;
        _serviceScopeFactory = serviceScopeFactory;
        _redis = redis;
        _mongo = mongo;
    }

    public async Task StoreWebsocket(string userid, string wsid, WebSocket ws) {

        var result = await ws.ReceiveAsync(new ArraySegment<byte>(_buffer), CancellationToken.None);
        while(!result.CloseStatus.HasValue) {
            var serializedObject = Encoding.UTF8.GetString(_buffer, 0, result.Count);
            var eventType = serializedObject.Split("event=")[1].Split(";")[0];
            serializedObject = serializedObject.Split(";")[1];

            if(eventType == "message") {
                var messageObject = Newtonsoft.Json.JsonConvert.DeserializeObject<MessageModel>(serializedObject);
                var stored = await _redis.Messages().StringSetAsync(messageObject!.conversationId, serializedObject);
                if(stored) {
                    foreach(var receiver in messageObject.receiver!) {
                        if(_websockets.TryGetValue(receiver.userId!, out var websockets)) {
                            foreach(var websocket in websockets) {
                                await websocket.Value.SendAsync(new ArraySegment<byte>(Encoding.UTF8.GetBytes(serializedObject)), WebSocketMessageType.Text, result.EndOfMessage, CancellationToken.None);
                            }
                        }
                    }
                }
            } else {

                var statusObject = Newtonsoft.Json.JsonConvert.DeserializeObject<MessageStatusModel>(serializedObject);
                serializedObject = await _redis.Messages().StringGetAsync(statusObject!.conversationId);
                if(!string.IsNullOrEmpty(serializedObject)) {
                    var messageObject = Newtonsoft.Json.JsonConvert.DeserializeObject<MessageModel>(serializedObject!);
                    messageObject!.receiver!.FirstOrDefault(f => f.userId == statusObject.userId)!.status = eventType;
                    serializedObject = Newtonsoft.Json.JsonConvert.SerializeObject(messageObject, Newtonsoft.Json.Formatting.Indented);
                    var stored = await _redis.Messages().StringSetAsync(statusObject.conversationId, serializedObject);
                    if(stored) {
                        if(_websockets.TryGetValue(messageObject.sender!, out var websockets)) {
                            foreach(var websocket in websockets) {
                                await websocket.Value.SendAsync(new ArraySegment<byte>(Encoding.ASCII.GetBytes("event=" + eventType + "; { " + "conversationId: " + messageObject.conversationId + ", userId: " + statusObject.userId + " }")), WebSocketMessageType.Text, result.EndOfMessage, CancellationToken.None);
                            }
                        }
                        foreach(var user in messageObject.receiver!) {
                            if(_websockets.TryGetValue(user.userId!, out websockets)) {
                                foreach(var websocket in websockets) {
                                    await websocket.Value.SendAsync(new ArraySegment<byte>(Encoding.ASCII.GetBytes("event=" + eventType + "; { " + "conversationId: " + messageObject.conversationId + ", userId: " + statusObject.userId + " }")), WebSocketMessageType.Text, result.EndOfMessage, CancellationToken.None);
                                }                            
                            }
                        }
                    }
                }
            } 

            await ws.SendAsync(new ArraySegment<byte>(_buffer), WebSocketMessageType.Text, result.EndOfMessage, CancellationToken.None);
            result = await ws.ReceiveAsync(new ArraySegment<byte>(_buffer), CancellationToken.None);
        }

        _websockets.TryGetValue(userid, out var value);
        value!.TryRemove(wsid, out _);
        if(value.Count == 0) {
            _websockets.TryRemove(userid, out _);
        }

        await ws.CloseAsync(result.CloseStatus.Value, result.CloseStatusDescription, CancellationToken.None);
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