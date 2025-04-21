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
            Client.Credentials = new NetworkCredential("abdulrahman.e.f22@gmail.com", "hmcqvnwmdorhyyff");
            Client.Send("abdulrahman.e.f22@gmail.com", email.To, email.Subject, email.Body);


        }
    }
}
