using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using ECommerce.Business.Abstract;

namespace ECommerce.Business.Concrete
{
    public class EmailManager : IEmailService
    {
        public async Task SendMail(string subject, string body, string to)
        {
            try
            {
                Smtp model = new Smtp();

                var smtpClient = new SmtpClient
                {
                    Host = model.SmtpHost,
                    Port = model.Port,
                    EnableSsl = true,
                    Credentials = new NetworkCredential(model.Email, model.Password)
                };
                var message = new MailMessage();
                message.To.Add(to);
                message.From = new MailAddress(model.Email);
                message.Subject = subject;
                message.Body = body;
                message.IsBodyHtml = true;

                await smtpClient.SendMailAsync(message);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
    public class Smtp
    {
        public Smtp()
        {
            Email = "inffffffooooooooooooo@hotmail.com";
            Password = "testEmail.55";
            SmtpHost = "smtp.live.com";
            Port = 587;
            Ssl = true;
        }
        public string Email { get; set; }
        public string Password { get; set; }
        public string SmtpHost { get; set; }
        public int Port { get; set; }
        public bool Ssl { get; set; }
    }
}

