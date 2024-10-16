public class CredentialVerificationResult: StatusObject {

    public string Token { get; set; }

    public CredentialVerificationResult(string token, int statusCode) : base(statusCode) {
        Token = token;
    }
}