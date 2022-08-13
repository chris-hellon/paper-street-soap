using PaperStreetSoap.Core.Interfaces.Services;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Http;
using ProjectBase.Core.Interfaces.Services;

namespace PaperStreetSoap.Infrastructure.Services
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _configuration;
        private readonly string _username;
        private readonly string _password;
        private readonly string _smtpServer;
        private readonly int _port;

        public EmailService(IConfiguration configuration, IErrorLoggerService errorLoggerService)
        {
            _configuration = configuration;

            _username = _configuration["EmailConfig:Username"];
            _password = _configuration["EmailConfig:Password"];
            _smtpServer = _configuration["EmailConfig:SmtpServer"];
            _port = int.Parse(_configuration["EmailConfig:Port"]);
        }

        public async Task Send(string to, string subject, string html, string? displayName = null, string? from = null)
        {
            try
            {
                var mail = new MimeMessage();
                mail.From.Add(new MailboxAddress(displayName ?? _username, from ?? _username));
                mail.Sender = new MailboxAddress(displayName ?? _username, from ?? _username);
                mail.To.Add(MailboxAddress.Parse(to));

                var body = new BodyBuilder();
                mail.Subject = subject;
                body.HtmlBody = html;
                mail.Body = body.ToMessageBody();

                using var smtp = new SmtpClient();
                await smtp.ConnectAsync(_smtpServer, _port, SecureSocketOptions.SslOnConnect);
                await smtp.AuthenticateAsync(_username, _password);
               

                await smtp.SendAsync(mail);
                await smtp.DisconnectAsync(true);
            }
            catch (Exception e)
            {
                string error = e.Message;
            }
        } 
    }
}

