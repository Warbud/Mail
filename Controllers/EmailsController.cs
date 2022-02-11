using Microsoft.AspNetCore.Mvc;
using Warbud.Mail.Models;
using Warbud.Mail.Services;

namespace Warbud.Mail.Controllers;

public record SendEmail(User User, WarbudMailMessage Message);
public record SendEmails(List<User> Users, WarbudMailMessage Message);

public class EmailsController : ControllerBase
{
    private readonly IEmailService _emailService;
    public EmailsController(IEmailService emailService)
    {
        _emailService = emailService;
    }
    
    [HttpPost("send-email")]
    public ActionResult Post([FromBody] SendEmail email)
    {
        var (user, mailMessage) = email;
        _emailService.SendAsync(user, mailMessage);
        return Accepted();
    }
    
    [HttpPost("send-emails")]
    public ActionResult Post([FromBody] SendEmails emails)
    {
        var (users, mailMessage) = emails;
        _emailService.SendAsync(users, mailMessage);
        return Accepted();
    }
}

