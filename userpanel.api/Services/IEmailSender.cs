namespace userpanel.api.Services;

public interface IEmailSender
{
    void SendEmail(string toEmail, string subject);
}