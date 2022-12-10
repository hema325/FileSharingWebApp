using FileSharingApp.SharedData;
using System.Net.Mail;

namespace FileSharingApp.Services.Mail
{
    public interface IEmailSender
    {
        Task SendEmailAsync(Contact entity);
        Task SendEmailAsync(MailMessage msg);
    }
}
