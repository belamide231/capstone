public class VerifyResults {


    public class EmailAlreadyTaken : StatusModel {
        public string Message { get; set; }
        public EmailAlreadyTaken() : base(StatusCodes.Status403Forbidden) {
            Message = "Email is already taken";
        }
    }


    public class EmailIsLocked : StatusModel {
        public string Message { get; set; }
        public EmailIsLocked() : base(StatusCodes.Status403Forbidden) { 
            Message = "Email is temporarily locked";
        }
    }


    public class InternalServerProblem : StatusModel {
        public string Message { get; set; }
        public InternalServerProblem() : base(StatusCodes.Status500InternalServerError) {
            Message = "Something went wrong to the server";
        }
    }


    public class EmailIsValid : StatusModel {
        public string Message { get; set; }
        public string Code { get; set; }
        public EmailIsValid(string code) : base(StatusCodes.Status200OK) {
            Message = "We sent the code to your email";
            Code = code;
        }
    }


    public class VerificationCodeSentAlready : StatusModel {
        public string Message { get; set; }
        public string Code { get; set; }
        public VerificationCodeSentAlready(string code) : base(StatusCodes.Status200OK) {
            Message = "We sent the code already";
            Code = code;
        }
    }
}