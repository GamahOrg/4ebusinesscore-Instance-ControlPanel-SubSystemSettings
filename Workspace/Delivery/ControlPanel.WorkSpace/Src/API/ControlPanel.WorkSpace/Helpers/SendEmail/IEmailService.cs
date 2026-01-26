namespace UserManagement.Helpers.SendEmail
{
    public interface IEmailService
    {
        Task Send(EmailMessage emailMessage);

        // Templates
        Task ResetPasswordEmail(string email, string validToken);
    }
}
