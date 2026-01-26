namespace UserManagement.Helpers.SendEmail
{
    public class SendEmailWithErrorConfiguration : ISendEmailWithErrorConfiguration
    {
        public bool AllowSend { get; set; }
        public string ToEmails { get; set; }
    }
}
