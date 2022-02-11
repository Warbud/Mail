namespace Warbud.Mail.Models;

public class EmailOptions
{
    public string SenderEmail { get; set; }
    public string AppPassword { get; set; }

    public void Deconstruct(out string senderEmail, out string appPassword)
    {
        senderEmail = SenderEmail;
        appPassword = AppPassword;
    }
}