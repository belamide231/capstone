public class VerifyResults {


    public class EmailAlreadyTaken : StatusObject {
        public string Message { get; set; }
        public EmailAlreadyTaken() : base(StatusCodes.Status403Forbidden) {
            Message = "Email is already taken.";
        }
    }


    public class EmailIsLocked : StatusObject {
        public string Message { get; set; }
        public EmailIsLocked() : base(StatusCodes.Status403Forbidden) { 
            Message = "Email is locked.";
        }
    }


    public class InternalServerProblem : StatusObject {
        public string Message { get; set; }
        public InternalServerProblem() : base(StatusCodes.Status500InternalServerError) {
            Message = "Something went wrong to the server.";
        }
    }


    public class EmailIsValid : StatusObject {
        public string Message { get; set; }
        public string Code { get; set; }
        public EmailIsValid(string code) : base(StatusCodes.Status200OK) {
            Message = "We sent the code to your email.";
            Code = code;
        }
    }


    public class VerificationCodeSentAlready : StatusObject {
        public string Message { get; set; }
        public string Code { get; set; }
        public VerificationCodeSentAlready(string code) : base(StatusCodes.Status200OK) {
            Message = "We sent the code already.";
            Code = code;
        }
    }
}