using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Net;
using System.Net.Mail;

namespace WebQLNhaTro.Models
{
    public class Email
    {
       public void sendmail(string toEmail,string subject,string content)
        {
            var fromEmail = ConfigurationManager.AppSettings["fromEmailaddress"].ToString();
            var fromEmailDisPlay = ConfigurationManager.AppSettings["fromEmailDisplayName"].ToString();
            var fromEmailPass = ConfigurationManager.AppSettings["fromEmailPassword"].ToString();
            var smtpHost = ConfigurationManager.AppSettings["SMTPHost"].ToString();
            var smtpPort = ConfigurationManager.AppSettings["SMTPPort"].ToString();

            bool enabledSsl = bool.Parse(ConfigurationManager.AppSettings["EnabledSSL"].ToString());

            string body = content;
            MailMessage message = new MailMessage(new MailAddress(fromEmail, fromEmailDisPlay),new MailAddress(toEmail));
            message.Subject = subject;
            message.IsBodyHtml = true;
            message.Body = body;

            var client = new SmtpClient();
            client.Credentials = new NetworkCredential(fromEmail, fromEmailPass);
            client.Host = smtpHost;
            client.EnableSsl = enabledSsl;
            client.Port = !string.IsNullOrEmpty(smtpPort) ? Convert.ToInt32(smtpPort) : 0;
            client.Send(message);
        }
    }
}