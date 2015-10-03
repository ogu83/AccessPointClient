using System.Net.Mail;

namespace Common
{
    public class MailHelper
    {
        public static void Send(string from, string to, string subject, string body)
        {
            var mail = new MailMessage(from, to, subject, body);
            var client = new SmtpClient("smtp.oguzkoroglu.net", 587);
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.UseDefaultCredentials = false;
            client.Send(mail);
        }
    }
}
