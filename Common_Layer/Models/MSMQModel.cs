using Experimental.System.Messaging;
using System;
using System.Net;
using System.Net.Mail;

namespace Common_Layer.Models
{
    public class MSMQModel
    {
        MessageQueue messageQueue = new MessageQueue();
        private string recieverEmailAddr;
        private string recieverName;

        //Message To Send Token Using MessageQueue And Delegate

        public void SendMessage(string token, string Email, string name)
        {
            recieverEmailAddr = Email;
            recieverName = name;
            messageQueue.Path = @".\Private$\Token";
            try
            {
                if (!MessageQueue.Exists(messageQueue.Path)) 
                {
                    MessageQueue.Create(messageQueue.Path);
                }
                messageQueue.Formatter = new XmlMessageFormatter(new Type[] { typeof(string) });
                messageQueue.ReceiveCompleted += MessageQueue_ReceiveCompleted;
                messageQueue.Send(token);
                messageQueue.BeginReceive();
                messageQueue.Close();
            }
            catch (Exception ex )
            {
                throw ex; 
            }
        }

        //Delagate To Send Token As Message To the Sender EmailId using Smtp And MailMessage

        private void MessageQueue_ReceiveCompleted(object sender, ReceiveCompletedEventArgs e)
        {
            try
            {
                var msg = messageQueue.EndReceive(e.AsyncResult);
                string token = msg.Body.ToString();
                MailMessage mailMessage = new MailMessage();
                SmtpClient smtpClient = new SmtpClient("smtp.gmail.com")
                {
                    Port = 587,
                    EnableSsl = true,
                    Credentials = new NetworkCredential("adityawaghmare31@gmail.com", "gwos uqtj hkgb yqut"),
                };
                mailMessage.From = new MailAddress("adityawaghmare31@gmail.com");
                mailMessage.To.Add(new MailAddress(recieverEmailAddr));
                string mailBody = $"<!DOCTYPE html>" +
                                  $"<html>" +
                                  $"<style>" +
                                  $".blink" +
                                  $"</style>" +
                                    $"<body style=\"background-color:#DBFF73;text-align:center;padding:5px;\">" +
                                    $"<h1 style=\"color:#6A8D02;border-bottom:3px solid #84AF08;margin-top: 5px;\">" +
                                    $"<h3 style=\"color:#8AB411;\">For Resetting Password The Below Link Is Is Issued </h3>" +
                                    $"<h3 style=\"color:#8AB411;\">Please Click The Link Below To Reset Your Password </h3>" +
                                    $"<a style=\"color:#00802b; text-decoration: none; font-size:20px;\"href='http://localhost:4200/resetpassword/{token}'>Click me</a>\n" +
                                    $"<h3 style=\"color:#8AB411;margin-bottom:5px;\"><blink>This Token Will be Valid For Next 6 Hours<blink></h3>" +
                                    $"</body>" +
                                    $"</html>";
                mailMessage.Body = mailBody;
                mailMessage.IsBodyHtml = true;
                mailMessage.Subject = "fundoo Notes Password Reset Link";
                smtpClient.Send(mailMessage);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}
