using ContasApp.Messagens.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace ContasApp.Messagens.Services
{
    public static class EmailMessageService
    {
        private static string _conta = "ContasAppSuporte@outlook.com";
        private static string _senha = "contasApp@2023";
        private static string _smtp = "smtp-mail.outlook.com";
        private static int _porta = 587;

        public static void Send(EmailMessageModel model)
        {
            var mailMessage = new MailMessage(_conta, model.EmailDestinatario);
            mailMessage.Subject = model.Assunto;
            mailMessage.Body = model.Corpo;

            var smtpClient = new SmtpClient(_smtp, _porta);
            smtpClient.UseDefaultCredentials = false;
            smtpClient.EnableSsl = true;
            smtpClient.Credentials = new NetworkCredential(_conta, _senha);
            smtpClient.Send(mailMessage);
        }
    }
} 
