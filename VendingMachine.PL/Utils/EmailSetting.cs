using System.Net.Mail;
using System.Net;
using Microsoft.AspNetCore.Identity.UI.Services;

namespace VendingMachine.PL.Utils
{
    public class EmailSetting : IEmailSender 
    {
        private readonly IConfiguration _configuration;

        public EmailSetting(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            var password = _configuration["GmailPassword"];

            var client = new SmtpClient("smtp.gmail.com", 587)
            {
                EnableSsl = true,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential("lama.rafatali@gmail.com", password)
            };

            return client.SendMailAsync(
                new MailMessage(from: "lama.rafatali@gmail.com",
                                to: email,
                                subject,
                                htmlMessage
                                )
                { IsBodyHtml = true }
                );
        }
    }
}
