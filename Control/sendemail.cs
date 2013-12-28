using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Mail;

namespace RGDZY.control
{
    public class SendEmail : IDisposable
    {
        private MailMessage msg = null;

        public SendEmail()
        {
            msg = new System.Net.Mail.MailMessage();
        }

        public bool send(string to, string sub, string content, string cc=null) 
        {
            msg.To.Add(to);
            if(cc != null) msg.CC.Add(cc);
            msg.From = new MailAddress("admin@labsystemsjtu.com", "ADMIN", System.Text.Encoding.UTF8);
            msg.Subject = sub;
            msg.SubjectEncoding = System.Text.Encoding.UTF8;
            msg.Body = content;
            msg.BodyEncoding = System.Text.Encoding.UTF8;
            msg.IsBodyHtml = false;
            msg.Priority = MailPriority.High;

            SmtpClient client = new SmtpClient();
            client.Credentials = new System.Net.NetworkCredential("labsystemsjtu@gmail.com", "meiyoushenayijiumeiyourgdzy");

            client.Port = 587;
            client.Host = "smtp.gmail.com";
            client.EnableSsl = true;

            object userState = msg;
            try
            {
                client.Send(msg);
                return true;
            }
            catch (System.Net.Mail.SmtpException e)
            {
                Console.WriteLine(e.ToString());
                return false;
            }
        }

        public virtual void Dispose()
        {
            msg.Dispose();
        }
    }
}