public class SentModel {

    public DateTime TimeSent { get; set; }
    public string ConversationId { get; set; }

    public string MessageId { get; set; }

    public SentModel(string conversationId, string messageId) {
        TimeZoneInfo philippineTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Asia/Manila");
        DateTime philippineTime = TimeZoneInfo.ConvertTime(DateTime.UtcNow, philippineTimeZone);

        TimeSent = philippineTime;
        ConversationId = conversationId;
        MessageId = messageId;
    } 
}