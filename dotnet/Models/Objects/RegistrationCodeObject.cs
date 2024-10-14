using System;

public class RegistrationCodeObject {


    public string Code { get; set; }
    public int FailCount { get; set; } 
    public RegistrationCodeObject() {
        Code = new Random().Next(100000, 1000000).ToString();
        FailCount = 0;                
    }
}