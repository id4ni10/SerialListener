using System;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace SerialListener
{
    public static class Email
    {
        public async static void send(string destination, string temperature = "Setup Completo!")
        {
            try
            {
                var remetenteEmail = "sistemas@portalimap.org.br";

                var body = "<html><style>div { float: left; } hr { clear: both; } p { clear: both; }</style><div>";

                var format = string.Format("<div><h2>{0}</h2><h3>{1} - {2}</h3></div></div><br /><hr /><p>{3}</p></html>", "Sistemas", remetenteEmail, Environment.MachineName, temperature);

                body += format;

                var mail = new MailMessage
                {
                    From = new MailAddress(remetenteEmail, "IMAP", Encoding.UTF8),
                    Subject = "Heat",
                    SubjectEncoding = Encoding.UTF8,
                    Body = body,
                    BodyEncoding = Encoding.UTF8,
                    IsBodyHtml = true,
                    Priority = MailPriority.High,
                };

                mail.To.Add(destination);

                var client = new SmtpClient
                {
                    Credentials = new NetworkCredential(remetenteEmail, "515t3M45"),
                    Port = 587,
                    Host = "smtp.gmail.com",
                    EnableSsl = true,
                };

                await client.SendMailAsync(mail);
            }
            catch (Exception faliure)
            {
                throw faliure;
            }
        }
    }
}
