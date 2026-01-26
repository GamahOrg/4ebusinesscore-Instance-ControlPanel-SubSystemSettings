
namespace UserManagement.Helpers.SendEmail
{
    public class EmailMessage
    {
        public EmailMessage()
        {
            ToAddresses = new List<EmailAddress>();
        }

        public List<EmailAddress> ToAddresses { get; set; }
        public List<EmailAddress> CcAddresses { get; set; }
        public string Subject { get; set; }
        public MimeEntity Body { get; set; }
        public string BodyContent
        {
            set
            {
                this.Body = (new BodyBuilder()
                {
                    TextBody = value
                }).ToMessageBody();
            }
        }
    }

}
