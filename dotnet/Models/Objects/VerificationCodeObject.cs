using System;

public class VerificationObject {


    public static string VerifyingEmailCode = "Verifying email code";
    public static string RegisteringAccount = "Registering an account";
    public static string VerifyingCredential = "Verifying the credential";
    public static string RecoveryOfAccount = "Verying to recover account";
    public static string AccountNewPassword = "Account new password";


    public string Code { get; set; }
    public int FailCount { get; set; }
    public string Event { get; set; } 
    public VerificationObject(string _event) {
        Code = new Random().Next(100000, 1000000).ToString();
        FailCount = 0;                
        Event = _event;
    }
}