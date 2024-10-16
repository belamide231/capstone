using dotenv.net;

public class EnvHelper {

    public static string? _ServerUrl;
    public static string? _MongoUrl;
    public static string? _RedisUrl;
    public static string? _JwtKey;
    public static string? _JwtDuration;
    public static string? _CacheDuration;
    public static string? _GmailUsername;
    public static string? _GmailPassword;

    public EnvHelper() {

        DotEnv.Load();

        _ServerUrl = Environment.GetEnvironmentVariable("SERVER_URL");
        _MongoUrl = Environment.GetEnvironmentVariable("MONGO_URL");
        _RedisUrl = Environment.GetEnvironmentVariable("REDIS_URL");
        _JwtKey = Environment.GetEnvironmentVariable("JWT_KEY");
        _JwtDuration = Environment.GetEnvironmentVariable("JWT_DURATION");
        _CacheDuration = Environment.GetEnvironmentVariable("CACHE_DURATION");
        _GmailUsername = Environment.GetEnvironmentVariable("GMAIL_USERNAME");
        _GmailPassword = Environment.GetEnvironmentVariable("GMAIL_PASSWORD");
    }
}