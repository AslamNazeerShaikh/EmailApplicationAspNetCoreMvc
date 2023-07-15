using EmailApplication.Services.Helpers;
using EmailApplication.Services.Interfaces;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailApplication.Services.Classes
{
    public class EmailService : IEmailService
    {
        private readonly EmailSettings _emailSettings;

        public EmailService(IOptions<EmailSettings> options)
        {
            _emailSettings = options.Value;
        }

        public async Task SendEmailAsync(EmailRequest emailRequest)
        {
            var email = new MimeMessage
            {
                Sender = MailboxAddress.Parse(_emailSettings.Email)
            };
            email.To.Add(MailboxAddress.Parse(emailRequest.ToEmail));
            email.Subject = emailRequest.Subject;

            var emailBodyBuilder = new BodyBuilder
            {
                HtmlBody = emailRequest.Body
            };
            email.Body = emailBodyBuilder.ToMessageBody();

            using var smtp = new SmtpClient();
            smtp.Connect(_emailSettings.Host, _emailSettings.Port, SecureSocketOptions.StartTls);
            smtp.Authenticate(_emailSettings.Email, _emailSettings.Password);

            await smtp.SendAsync(email);

            smtp.Disconnect(true);
        }
    }
}
