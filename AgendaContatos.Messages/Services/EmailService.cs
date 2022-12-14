using AgendaContatos.Messages.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace AgendaContatos.Messages.Services
{
    //Classe para implementação do envio de emails
    public class EmailService
    {
        public void Send(string mailTo, string subject, string body)
        {
            #region Montar a mensagem

            var mailMessage = new MailMessage(EmailSettings.Conta, mailTo);
            mailMessage.Subject = subject;
            mailMessage.Body = body;
            mailMessage.IsBodyHtml = true;

            #endregion

            #region Enviando a mensagem

            var smtpClient = new SmtpClient(EmailSettings.Smtp, EmailSettings.Porta);
            smtpClient.Credentials = new NetworkCredential(EmailSettings.Conta, EmailSettings.Senha);
            smtpClient.EnableSsl = true;
            smtpClient.Send(mailMessage);

            #endregion
        }
    }
}
