using FileSharingApp.SharedData;
using System.Net;
using System.Net.Mail;

namespace FileSharingApp.Services.Mail
{
    public class EmailSender :IEmailSender
    {
        private readonly IConfiguration config;
        public string Host { get { return config.GetSection("SmtpConfiguration").GetValue<string>("Host"); } }
        public int Port { get { return config.GetSection("SmtpConfiguration").GetValue<int>("Port"); } }
        public string Email { get { return config.GetSection("SmtpConfiguration").GetValue<string>("ServerMail"); } }
        public string Password { get { return config.GetSection("SmtpConfiguration").GetValue<string>("Password"); } }
        public EmailSender(IConfiguration config)
        {
            this.config = config;
        }
        public async Task SendEmailAsync(Contact model)
        { 
            var credentials = new NetworkCredential(Email, Password);

            var client = new SmtpClient
            {
                Host = Host,
                Port = Port,
                EnableSsl = true,
                Credentials = credentials
            };

            var msg = new MailMessage
            {
                From = new MailAddress(model.Email),
                Subject = model.Subject,
                Body = model.Message,
                IsBodyHtml = false
            };

            msg.To.Add(new MailAddress(Email));

            await client.SendMailAsync(msg);

        }
        public async Task SendEmailAsync(MailMessage msg)
        {
            var creadentials = new NetworkCredential(Email, Password);

            var client = new SmtpClient
            {
                Host = Host,
                Port = Port,
                Credentials = creadentials,
                EnableSsl = true
            };

            msg.From = new MailAddress(Email);

            await client.SendMailAsync(msg);

        }

    }
}
