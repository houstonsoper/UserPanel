using System.Net.Mail;

namespace userpanel.api.Services;

public interface IEmailSender
{
    void SendRegistrationEmail(string toEmail);
    void SendPasswordResetEmail(string toEmail, Guid token);
    SmtpClient SetupSmtpClient();
}