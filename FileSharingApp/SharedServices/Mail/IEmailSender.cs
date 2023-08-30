using FileSharingApp.Models;
using System.Net.Mail;

namespace FileSharingApp.Services.Mail
{
    public interface IEmailSender
    {
        Task SendEmailAsync(Contact entity);
        Task SendEmailAsync(MailMessage msg);
    }
}
