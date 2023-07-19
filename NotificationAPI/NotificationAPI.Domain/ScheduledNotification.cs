namespace NotificationAPI.Domain
{
    public class ScheduledNotification
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string SendToEmail { get; set; }
        public DateTime SendAt { get; set; }
        public string Subject { get; set; }
        public string Message { get; set; }
        public ScheduledNotification(string sendToEmail, DateTime sendAt, string subject, string message)
        {
            SendToEmail = sendToEmail;
            SendAt = sendAt;
            Subject = subject;
            Message = message;
        }
    }
}
