using System.Net.Mail;

namespace userpanel.api.Services;

public interface IEmailSender
{
    void SendRegistrationEmail(string toEmail, string subject);
    SmtpClient SetupSmtpClient();
}