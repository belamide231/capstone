using System.Collections.Concurrent;
using System.Net.WebSockets;
using System.Text;
using System.Text.Json;
using Microsoft.VisualBasic;
using MongoDB.Bson;
using MongoDB.Driver;
using Newtonsoft.Json;
using Org.BouncyCastle.Crypto.Digests;

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
    private readonly ConcurrentDictionary<string, CancellationTokenSource> _conversations = new ConcurrentDictionary<string, CancellationTokenSource>();
    private readonly byte[] _buffer = new byte[1024 * 4];
    private readonly TimeSpan _duration = TimeSpan.FromSeconds(5);
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
                ConversationSchema myConversation; 
                var conversationId = "";

                if(conversationList.Count == 0) {
                    var newConversation = new ConversationSchema(audience);
                    await _mongo.ConversationCollection().InsertOneAsync(newConversation);
                    conversationId = newConversation.ConversationId!.ToString();
                    myConversation = newConversation;
                } else {
                    conversationId = conversationList[0].ConversationId!.ToString();
                    myConversation = conversationList[0];
                }

                // var userData = await _mongo.UserDataCollection().Find(
                //     Builders<UsersDataSchema>.Filter.In(f => f.UserId, audience)
                // ).ToListAsync();
                // Console.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(userData, Formatting.Indented));

                // QUEUEING THE CONVERSATION
                var tasks = audience.Select(userDataId => Task.Run(async () => {
                    var usersData = await _mongo.UserDataCollection().FindAsync(
                        Builders<UsersDataSchema>.Filter.Eq(f => f.UserId, userDataId)
                    );
                    var usersDataList = usersData.ToList();

                    Console.WriteLine(usersDataList.Count());

                    // CREATING USER DATA FOR QUEUEING CONVERSATIONS IF DID NOT EXIST
                    if(usersDataList!.Count == 0) {
                        var newUserData = new UsersDataSchema(userDataId);
                        await _mongo.UserDataCollection().InsertOneAsync(newUserData);

                    // IF EXISTING PULLNG THE CONVERSATION
                    } else {
                        await _mongo.UserDataCollection().FindOneAndUpdateAsync(
                            Builders<UsersDataSchema>.Filter.Eq(f => f.UserId, userDataId),
                            Builders<UsersDataSchema>.Update.Pull(f => f.StacksOfConversations, conversationId)
                        );
                    }

                    // PUSHING IT TO THE LATEST
                    await _mongo.UserDataCollection().FindOneAndUpdateAsync(
                        Builders<UsersDataSchema>.Filter.Eq(f => f.UserId, userDataId),
                        Builders<UsersDataSchema>.Update.Push(f => f.StacksOfConversations, conversationId)
                    );                
                }));
                await Task.WhenAll(tasks);

                // CREATING MESSAGE
                var newMessage = new MessageSchema(conversationId!, userid, messageModel.Message!);
                var serializedMessage = Newtonsoft.Json.JsonConvert.SerializeObject(newMessage, Newtonsoft.Json.Formatting.Indented);

                // STORING MESSAGE IN REDIS
                var stored = await _redis.Conversations().ListLeftPushAsync(conversationId, serializedMessage);

                // CANCELLING THE EXISTING DURATION THE TASK THAT IF THE DURATION ENDS IT STORES IN MONGO 
                if(_conversations.TryGetValue(conversationId!, out var cancellationToken)) {
                    cancellationToken.Cancel();
                    _conversations!.Remove(conversationId, out _);
                }

                // CREATING A NEW DURATION FOR STORING CONVERSATION IN MONGO IF DURATION ENDS
                var newCancellationTokenSource = new CancellationTokenSource();
                _ = Task.Run(async() => {

                    await Task.Delay(_duration, newCancellationTokenSource.Token);
                    var listOfSerializedMessages = await _redis.Conversations().ListRangeAsync(conversationId);
                    var listOfMessages = listOfSerializedMessages.Select(element => Newtonsoft.Json.JsonConvert.DeserializeObject<MessageSchema>(element!)).ToList();
                    
                    // STORING IN MONGO
                    await _mongo.ConversationCollection().FindOneAndUpdateAsync(
                        Builders<ConversationSchema>.Filter.Eq(f => f.ConversationId, conversationId),
                        Builders<ConversationSchema>.Update.PushEach(f => f.Messages, listOfMessages)
                    );

                    // DELETING CONVERSATION IN REDIS
                    await _redis.Conversations().KeyDeleteAsync(conversationId);

                }, newCancellationTokenSource.Token);
                _conversations.TryAdd(conversationId!, newCancellationTokenSource);

                // NOTIFYING SENDER THAT MESSAGE IS ALREADY BEING SENT
                var sentModel = new SentModel(conversationId, newMessage.MessageId!);
                var serializedSentModel = Newtonsoft.Json.JsonConvert.SerializeObject(sentModel, Formatting.Indented);
                var byteSerializedSentModel = Encoding.ASCII.GetBytes("sent;" + serializedMessage);
                await ws.SendAsync(new ArraySegment<byte>(byteSerializedSentModel), WebSocketMessageType.Text, true, CancellationToken.None);

                // NOTIFYING RECEIVERS
                // foreach(var receiver in myConversation.Audience!) {
                //     Console.WriteLine(receiver);
                // }

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