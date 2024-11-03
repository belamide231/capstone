using System.Collections.Concurrent;
using System.Net.WebSockets;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Org.BouncyCastle.Ocsp;

public class Messenger {
    private readonly RequestDelegate _next;
    private readonly ConcurrentDictionary<string, ConcurrentDictionary<string, WebSocket>> _websockets = new();
    private readonly ConcurrentDictionary<string, string> _users = new ConcurrentDictionary<string, string>();
    private readonly byte[] _buffer = new byte[1024 * 4];
    private readonly Redis _redis;
    private readonly Mongo _mongo;    

    public Messenger(RequestDelegate next, Redis redis, Mongo mongo) {
        _next = next;
        _redis = redis;
        _mongo = mongo;
    }

    public async Task StoreWebsocket(string userid, string wsid, WebSocket ws) {

        var result = await ws.ReceiveAsync(new ArraySegment<byte>(_buffer), CancellationToken.None);
        while(!result.CloseStatus.HasValue) {
            var serializedObject = Encoding.UTF8.GetString(_buffer, 0, result.Count);

            Console.WriteLine(serializedObject);

            // var eventType = serializedObject.Split(";")[0];
            // serializedObject = serializedObject.Split(";")[1];

            // if(eventType == "message") {

            //     var messageObject = Newtonsoft.Json.JsonConvert.DeserializeObject<MessageSchema>(serializedObject);
            //     var stored = await _redis.Messages().ListLeftPushAsync(messageObject!.conversationId, serializedObject);

            //     foreach(var receiver in messageObject.receivers!) {

            //         if(_websockets.TryGetValue(receiver, out var websockets)) {

            //             foreach(var websocket in websockets) {

            //                 await websocket.Value.SendAsync(new ArraySegment<byte>(Encoding.UTF8.GetBytes(serializedObject)), WebSocketMessageType.Text, result.EndOfMessage, CancellationToken.None);
            //             }
            //         }
            //     }

            // } else {

            // } 

            // await ws.SendAsync(new ArraySegment<byte>(_buffer), WebSocketMessageType.Text, result.EndOfMessage, CancellationToken.None);
            result = await ws.ReceiveAsync(new ArraySegment<byte>(_buffer), CancellationToken.None);
        }

        _websockets.TryGetValue(userid, out var value);
        value!.TryRemove(wsid, out _);
        if(value.Count == 0) {
            _websockets.TryRemove(userid, out _);
            _users.Remove(userid, out _);

            var totalSerializedUser = Newtonsoft.Json.JsonConvert.SerializeObject(_users, Newtonsoft.Json.Formatting.Indented);
            foreach(var userids in _websockets) {
                foreach(var websocketholder in userids.Value) {
                    await websocketholder.Value.SendAsync(new ArraySegment<byte>(Encoding.ASCII.GetBytes("actives;" + totalSerializedUser)), WebSocketMessageType.Text, true, CancellationToken.None);                    
                }
            }
        }

        await ws.CloseAsync(result.CloseStatus.Value, result.CloseStatusDescription, CancellationToken.None);
    }

    public async Task InvokeAsync(HttpContext context) {

        if (context.WebSockets.IsWebSocketRequest) {

            // WE NEED TO TAKE THIS OUT
            var UserManager = context.RequestServices.GetRequiredService<UserManager<ApplicationUser>>();
            
            if(string.IsNullOrEmpty(context.Request.Query["token"])) {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                return;
            }

            var authorization = context.Request.Query["token"];
            var claims = JwtHelper.Decode(authorization!);

            if(string.IsNullOrEmpty(claims["nameid"])) {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                return;
            }

            if (context.Request.Path == "/chat") {

                var ws = await context.WebSockets.AcceptWebSocketAsync();
                if(ws.State != WebSocketState.Open) {
                    context.Response.StatusCode = StatusCodes.Status409Conflict;
                    return;
                }

                var userid = claims["nameid"];
                var wsid = Guid.NewGuid() + "-" + DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
                

                _websockets.AddOrUpdate(
                    userid,
                    new ConcurrentDictionary<string, WebSocket>(new[] { new KeyValuePair<string, WebSocket>(wsid, ws) }),
                    (key, websockets) => {
                        websockets.AddOrUpdate(wsid, ws, (wsKey, existws) => ws);
                        return websockets;
                    }
                );

                var user = await UserManager.FindByIdAsync(userid);
                var userEmail = user!.Email;

                if(!_users.TryGetValue(userid, out _)) {
                    _users.TryAdd(userid, userEmail!);
                    var totalSerializedUser = Newtonsoft.Json.JsonConvert.SerializeObject(_users, Newtonsoft.Json.Formatting.Indented);
                    foreach(var userids in _websockets) {
                        foreach(var websocketholder in userids.Value) {
                            await websocketholder.Value.SendAsync(new ArraySegment<byte>(Encoding.ASCII.GetBytes("actives;" + totalSerializedUser)), WebSocketMessageType.Text, true, CancellationToken.None);                    
                        }
                    }
                }

                await StoreWebsocket(userid, wsid, ws);
            }

        } else {

            await _next(context);
        }
    }
}