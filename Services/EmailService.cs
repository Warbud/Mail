using System.Net;
using System.Net.Mail;
using System.Text;
using Warbud.Mail.Models;

namespace Warbud.Mail.Services;

public interface IEmailService
{
    Task SendAsync(User user, WarbudMailMessage message);
    Task SendAsync(IEnumerable<User> users, WarbudMailMessage message);
}

public class EmailService : IEmailService
{
    private static readonly SmtpClient Client = new ("smtp.gmail.com", 587)
    {
        EnableSsl = true,
        
    };

    private readonly MailAddress _from;

    public EmailService(IConfiguration configuration)
    {
        var (senderEmail, appPassword) = configuration.GetOptions<EmailOptions>("EmailOptions");
        _from = new MailAddress(senderEmail);
        Client.Credentials = new NetworkCredential(senderEmail, appPassword);
    }

    public async Task SendAsync(User user, WarbudMailMessage message)
    {
        var (subject, body) = message;
        var messageToSend = new MailMessage(_from, new MailAddress(user.Email)) 
        {
            Body = body, 
            Subject = subject,
            BodyEncoding = Encoding.Unicode,
            IsBodyHtml = true
        };
        await Client.SendMailAsync(messageToSend);
    }

    public async Task SendAsync(IEnumerable<User> users, WarbudMailMessage message)
    {
        var (subject, body) = message;
        var messageToSend = new MailMessage(_from.Address, string.Join(',', users.Select(x => x.Email))) 
        {
            Body = body, 
            Subject = subject,
            BodyEncoding = Encoding.Unicode,
            IsBodyHtml = true
        };
        await Client.SendMailAsync(messageToSend);
    }
}