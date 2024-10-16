


using System.Security.Principal;

public class CredentialVerificationResults {

    public class CredentialVerification: StatusObject {

        public string Token { get; set; }

        public CredentialVerification(string token, int statusCode) : base(statusCode) {
            Token = token;
        }
    }


    public class CredentialVerificationWithDeviceInfo : StatusObject {
        public string Token { get; set; }
        public string DeviceIdIdentifier { get; set; }
        public string DeviceId { get; set; }
        public CredentialVerificationWithDeviceInfo(string token, int statusCode, string deviceIdIdentifier, string deviceId) : base(statusCode) {
            Token = token;
            DeviceIdIdentifier = deviceIdIdentifier;
            DeviceId = deviceId;
        }
    }
}