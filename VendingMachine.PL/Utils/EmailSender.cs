using Microsoft.AspNetCore.Identity.UI.Services;
using System.Net;
using System.Net.Mail;

namespace VendingMachine.PL.Utils
{
    public class EmailSender : IEmailSender
    {
        private readonly IConfiguration _configuration;
        public EmailSender(
            IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            var host = _configuration["SmtpSettings:Host"];
            var port = int.Parse(_configuration["SmtpSettings:Port"]);
            var enableSsl = bool.Parse(_configuration["SmtpSettings:EnableSsl"]);
            var smtpEmail = _configuration["SmtpSettings:Email"];
            var password = _configuration["SmtpSettings:Password"];


            var client = new SmtpClient(host, port)
            {
                EnableSsl = enableSsl,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(smtpEmail, password)
            };

            return client.SendMailAsync(
                new MailMessage(from: smtpEmail,
                                to: email,
                                subject,
                                htmlMessage)
                { IsBodyHtml = true });
        }
    }
}
