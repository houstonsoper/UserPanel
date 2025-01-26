using System.Net;
using System.Net.Mail;
using System.Text;

namespace userpanel.api.Services;

public class EmailSender : IEmailSender
{
    public void SendEmail(string toEmail, string subject)
    {
        // Set up SMTP client
        var client = new SmtpClient("smtp.gmail.com", 587);
        client.EnableSsl = true;
        client.UseDefaultCredentials = false;
        client.Credentials = new NetworkCredential("houstonsoperdummyemail@gmail.com", "odsp rihz pgju rdpb");
        // Create email message
        var mailMessage = new MailMessage();
        mailMessage.From = new MailAddress("houstonsoperdummyemail@gmail.com");
        mailMessage.To.Add(toEmail);
        mailMessage.Subject = subject;
        mailMessage.IsBodyHtml = true;
        var mailBody = new StringBuilder();
        mailBody.AppendFormat("<h1>User Registered</h1>");
        mailBody.AppendFormat("<p>Thank you For Registering account</p>");
        mailMessage.Body = mailBody.ToString();

        // Send email
        client.Send(mailMessage);
    }
}