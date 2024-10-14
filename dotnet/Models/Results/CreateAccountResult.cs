public class Credential {
    public string Email { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
    
    public Credential(string email, string password) {
        Email = email;
        Username = email;
        Password = password;
    }
}


public class CreateAccountResult {


    public class AccountSuccessfullyCreatedTrust : StatusObject {
        public Credential Credential { get; set; }
        public string Message { get; set; } 
        public DeviceIdSchema DeviceInfo { get; set; }
        public AccountSuccessfullyCreatedTrust(string email, string password, DeviceIdSchema deviceInfo) : base(StatusCodes.Status200OK) {
            Credential = new Credential(email, password);
            Message = "Account successfully created.";
            DeviceInfo = deviceInfo;
        }
    }


    public class AccountSuccessfullyCreated : StatusObject {
        public Credential Credential { get; set; }
        public string Message { get; set; } 
        public AccountSuccessfullyCreated(string email, string password) : base(StatusCodes.Status200OK) {
            Credential = new Credential(email, password);
            Message = "Account successfully created.";
        }
    }


    public class PasswordConflict : StatusObject {
        public string Message { get; set; }
        public PasswordConflict(string message) : base(StatusCodes.Status409Conflict) {
            Message = message;
        }
    }
}