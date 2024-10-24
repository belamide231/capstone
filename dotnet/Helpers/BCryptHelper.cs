public class BCryptHelper {

    public static string Hash(string text) => BCrypt.Net.BCrypt.HashPassword(text);
    public static bool Verify(string text, string hash) => BCrypt.Net.BCrypt.Verify(text, hash);
}