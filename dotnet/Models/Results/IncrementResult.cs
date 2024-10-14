public class IncrementResult : StatusObject {

    
    public string Message { get; set; }
    public IncrementResult() : base(StatusCodes.Status200OK) {
        Message = "Incrementing fail attempt success.";
    }
}