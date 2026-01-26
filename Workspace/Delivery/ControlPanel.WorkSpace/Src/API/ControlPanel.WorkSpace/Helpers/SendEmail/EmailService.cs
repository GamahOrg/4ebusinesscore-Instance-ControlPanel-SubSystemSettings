

namespace UserManagement.Helpers.SendEmail
{
    public class EmailService : IEmailService
    {

        private readonly IEmailConfiguration _emailConfiguration;
        private readonly IConfiguration _configuration;
        private readonly string _frontEndURL;
        public EmailService(IEmailConfiguration emailConfiguration, IConfiguration configuration)
        {
            _emailConfiguration = emailConfiguration;
            _configuration = configuration;

            // front url
            _frontEndURL = _configuration["ClientSubscriber:Url"];
        }


        public async Task Send(EmailMessage emailMessage)
        {
            try
            {
                var message = new MimeMessage();

                // Prepare the email object settings [to, cc, from]
                message.To.AddRange(emailMessage.ToAddresses.Select(x => new MailboxAddress(x.Name, x.Address)));

                if (emailMessage.CcAddresses != null && emailMessage.CcAddresses.Count > 0)
                {
                    message.Cc.AddRange(emailMessage.CcAddresses.Select(x => new MailboxAddress(x.Name, x.Address)));
                }

                message.From.Add(new MailboxAddress(_emailConfiguration.FromEmail, _emailConfiguration.FromEmail));

                // Prepare email subject
                message.Subject = emailMessage.Subject;

                // Prepare email content
                message.Body = emailMessage.Body;

                // Authenticate then send email
                using (var emailClient = new SmtpClient() { })
                {
                    emailClient.ServerCertificateValidationCallback = (s, c, h, e) => true;


                    // Connect to server with account credentials and ssl settings
                    emailClient.Connect(_emailConfiguration.SmtpServer, _emailConfiguration.SmtpPort);
                    //   emailClient();

                    //Remove any OAuth functionality as we won't be using it.
                    emailClient.AuthenticationMechanisms.Remove("XOAUTH2");

                    // Authenticate the connection to server
                    emailClient.Authenticate(_emailConfiguration.SmtpUsername, _emailConfiguration.SmtpPassword);

                    // Send email
                    await emailClient.SendAsync(message);

                    // Disconnect the object
                    emailClient.Disconnect(true);
                }
            }
            catch (Exception ex)
            {
                throw new Exception((ex != null ? ex.Message : ""));
            }
        }


        /*
         * Helper Function
        */
        private string GetTemplatePath(string templateName)
        {
            //var pathToFile = _webHostEnvironment.WebRootPath
            //        + Path.DirectorySeparatorChar.ToString()
            //        + "EmailTemplates"
            //        + Path.DirectorySeparatorChar.ToString()
            //        + templateName;
            return "";
        }


        /*
         * Templates
        */
        public async Task ResetPasswordEmail(string email, string validToken)
        {
            var redirectPage = $"{_frontEndURL}/auth/reset-password/{email}/{validToken}";

            // Get TemplateFile located at wwwroot/EmailTemplates/ResetPasswordTemplate.html
            var pathToFile = GetTemplatePath("ResetPasswordTemplate.html");

            var builder = new BodyBuilder();
            //var image = builder.LinkedResources.Add(companyLogoPath);
            //image.ContentId = MimeUtils.GenerateMessageId();

            using (StreamReader SourceReader = File.OpenText(pathToFile))
            {
                builder.HtmlBody = string.Format(SourceReader.ReadToEnd(),
                        redirectPage
                    );
            }

            var subject = $"Reset Password";
            var emailMessage = new EmailMessage()
            {
                Body = builder.ToMessageBody(),
                Subject = subject,
                ToAddresses = new List<EmailAddress>()
                {
                    new EmailAddress() { Address = email, Name = email }
                }
            };

            await Send(emailMessage);
        }

        public async Task AfterRegistirationEmail(string email, string tempPassword, string companyName, string companyLogoPath)
        {
            var redirectPage = $"{_frontEndURL}/auth/login";

            // Get TemplateFile located at wwwroot/EmailTemplates/AfterRegistirationTemplate.html
            var pathToFile = GetTemplatePath("AfterRegistirationTemplate.html");

            var builder = new BodyBuilder();
            var image = builder.LinkedResources.Add(companyLogoPath);
            image.ContentId = MimeUtils.GenerateMessageId();

            using (StreamReader SourceReader = File.OpenText(pathToFile))
            {
                builder.HtmlBody = string.Format(SourceReader.ReadToEnd(),
                        redirectPage,
                        companyName,
                        email,
                        tempPassword
                    );
            }

            var subject = $"Registration Success.";
            var emailMessage = new EmailMessage()
            {
                Body = builder.ToMessageBody(),
                Subject = subject,
                ToAddresses = new List<EmailAddress>()
                {
                    new EmailAddress() { Address = email, Name = email }
                }
            };

            await Send(emailMessage);
        }

        public async Task AfterPasswordChangeEmail(string email)
        {
            var redirectPage = $"{_frontEndURL}/auth/login";

            // Get TemplateFile located at wwwroot/EmailTemplates/AfterPasswordChange.html
            var pathToFile = GetTemplatePath("AfterPasswordChange.html");

            var builder = new BodyBuilder();
            //var image = builder.LinkedResources.Add(companyLogoPath);
            //image.ContentId = MimeUtils.GenerateMessageId();

            using (StreamReader SourceReader = File.OpenText(pathToFile))
            {
                builder.HtmlBody = string.Format(SourceReader.ReadToEnd(),
                        redirectPage
                    );
            }

            var subject = $"Password Changes";
            var emailMessage = new EmailMessage()
            {
                Body = builder.ToMessageBody(),
                Subject = subject,
                ToAddresses = new List<EmailAddress>()
                {
                    new EmailAddress() { Address = email, Name = email }
                }
            };

            await Send(emailMessage);
        }
    }
}
