using StackExchange.Redis;

public class Redis {

    private readonly ConnectionMultiplexer _redis;
    public Redis() {

        // LOCAL
        _redis = ConnectionMultiplexer.Connect("localhost:6379");

        // CLOUD
        // var options = ConfigurationOptions.Parse("discrete-cowbird-36026.upstash.io:6379"); 
        // options.Password = "AYy6AAIjcDE2ZTVkMzliNDZkMzI0ZTJlOTlmMTgxYzFmNGU0Y2QyZXAxMA";
        // options.Ssl = true; 
        // _redis = ConnectionMultiplexer.Connect(options);
    }
    public IDatabase VerificationCodes() => _redis.GetDatabase(0);
    public IDatabase Conversations() => _redis.GetDatabase(5);
}