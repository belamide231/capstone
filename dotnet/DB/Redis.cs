using StackExchange.Redis;

public class Redis {

    private readonly ConnectionMultiplexer _redis;
    public Redis() => _redis = ConnectionMultiplexer.Connect("localhost:6379");
    public IDatabase VerificationCodes() => _redis.GetDatabase(0);
    public IDatabase Conversations() => _redis.GetDatabase(5);
}