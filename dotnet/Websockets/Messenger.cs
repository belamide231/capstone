using System.Collections.Concurrent;
using System.Net.WebSockets;
using System.Text;
using MongoDB.Bson;
using MongoDB.Driver;

public class IdAndEmailPair {
    public string Email { get; set; }
    public string Id { get; set; }

    public IdAndEmailPair(string email, string id) {
        Email = email;
        Id = id;
    }
}

public class Messenger {
    private readonly RequestDelegate _next;
    private readonly ConcurrentDictionary<string, ConcurrentDictionary<string, WebSocket>> _websockets = new();
    private readonly ConcurrentDictionary<string, IdAndEmailPair> _users = new ConcurrentDictionary<string, IdAndEmailPair>();
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
            var eventType = serializedObject.Split(";")[0];
            serializedObject = serializedObject.Split(";")[1];

            if(eventType == "message") {
                
                var messageModel = Newtonsoft.Json.JsonConvert.DeserializeObject<MessageModel>(serializedObject);
                var audience = messageModel!.Receivers;
                audience!.Add(messageModel!.Sender!);

                // FINDING CONVERSATION BY AUDIENCE
                var conversation = await _mongo.ConversationCollection().FindAsync(
                    Builders<ConversationSchema>.Filter.And(
                        Builders<ConversationSchema>.Filter.Size(f => f.Audience, audience.Count),
                        Builders<ConversationSchema>.Filter.All(f => f.Audience, audience)
                    )
                );
                
                var conversationList = await conversation.ToListAsync();
                dynamic conversationId = null!;

                if(conversationList.Count == 0) {
                    var newConversation = new ConversationSchema(audience);
                    await _mongo.ConversationCollection().InsertOneAsync(newConversation);
                    conversationId = newConversation.ConversationId!;
                } else {
                    conversationId = conversationList[0].ConversationId!;
                }
                
                // CREATING MESSAGE

                // STORING MESSAGE IN REDIS

                // SETTING TIMEOUT
            }

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

            if(string.IsNullOrEmpty(context.Request.Query["id"]) && string.IsNullOrEmpty(context.Request.Query["email"])) {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                return;
            }

            if (context.Request.Path == "/chat") {

                var ws = await context.WebSockets.AcceptWebSocketAsync();
                if(ws.State != WebSocketState.Open) {
                    context.Response.StatusCode = StatusCodes.Status409Conflict;
                    return;
                }

                var userid = context.Request.Query["id"];
                var userEmail = context.Request.Query["email"];
                var wsid = context.Request.Headers["Sec-WebSocket-Key"];
                
                _websockets.AddOrUpdate(
                    userid!,
                    new ConcurrentDictionary<string, WebSocket>(new[] { new KeyValuePair<string, WebSocket>(wsid!, ws) }),
                    (key, websockets) => {
                        websockets.AddOrUpdate(wsid!, ws, (wsKey, existws) => ws);
                        return websockets;
                    }
                );

                if(!_users.TryGetValue(userid!, out _)) {
                    _users.TryAdd(userid!, new IdAndEmailPair(userEmail!, userid!));
                    var totalSerializedUser = Newtonsoft.Json.JsonConvert.SerializeObject(_users, Newtonsoft.Json.Formatting.Indented);
                    foreach(var userids in _websockets) {
                        foreach(var websocketholder in userids.Value) {
                            await websocketholder.Value.SendAsync(new ArraySegment<byte>(Encoding.ASCII.GetBytes("actives;" + totalSerializedUser)), WebSocketMessageType.Text, true, CancellationToken.None);                    
                        }
                    }
                }
                await StoreWebsocket(userid!, wsid!, ws);
            }

        } else {

            await _next(context);
        }
    }
}