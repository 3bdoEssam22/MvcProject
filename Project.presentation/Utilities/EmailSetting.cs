using System.Net;
using System.Net.Mail;

namespace Project.presentation.Utilities
{
    public static class EmailSetting
    {
        public static void SendEmail(Email email)
        {
            var Client = new SmtpClient("smtp.gmail.com", 587);
            Client.EnableSsl = true;
            Client.Credentials = new NetworkCredential("abdulrahman@gmail.com", "123");
            Client.Send("abdulrahman@gmail.com", email.To, email.Subject, email.Body);


        }
    }
}
