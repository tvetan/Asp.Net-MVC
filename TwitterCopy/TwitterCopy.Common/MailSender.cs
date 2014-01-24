﻿namespace TwitterCopy.Common
{
    using System.Collections.Generic;
    using System.Net;
    using System.Net.Mail;
    using System.Text;

    public sealed class MailSender
    {
        private const string SendFrom = "krasi286@gmail.com";
        private const string SendFromName = "TwitterCopy";
        private const string Password = "__PASSWORD__";

        private const string ServerAddress = "smtp.gmail.com";
        private const int ServerPort = 587;

        private static readonly object syncRoot = new object();

        private static MailSender instance;
        private readonly SmtpClient mailClient;

        private MailSender()
        {

            this.mailClient = new SmtpClient
            {
                Credentials = new NetworkCredential(SendFrom, Password),
                Port = ServerPort,
                Host = ServerAddress,
                EnableSsl = true,
            };

            //this.mailClient = new SmtpClient()
            //{
            //    DeliveryMethod = SmtpDeliveryMethod.PickupDirectoryFromIis
            //};
        }

        public static MailSender Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                        {
                            instance = new MailSender();
                        }
                    }
                }

                return instance;
            }
        }

        public void SendMailAsync(string recipient, string subject, string messageBody, IEnumerable<string> bccRecipients = null)
        {
            var message = this.PrepareMessage(recipient, subject, messageBody, bccRecipients);
            this.mailClient.SendAsync(message, null);
        }

        public void SendMail(string recipient, string subject, string messageBody, IEnumerable<string> bccRecipients = null)
        {
            var message = this.PrepareMessage(recipient, subject, messageBody, bccRecipients);
            this.mailClient.Send(message);
        }

        private MailMessage PrepareMessage(string recipient, string subject, string messageBody, IEnumerable<string> bccRecipients)
        {
            var mailTo = new MailAddress(recipient);
            var mailFrom = new MailAddress(SendFrom, SendFromName);
            var message = new MailMessage(mailFrom, mailTo)
            {
                Body = messageBody,
                BodyEncoding = Encoding.UTF8,
                IsBodyHtml = true,
                Subject = subject,
                SubjectEncoding = Encoding.UTF8
            };

            if (bccRecipients != null)
            {
                foreach (var bccRecipient in bccRecipients)
                {
                    message.Bcc.Add(bccRecipient);
                }
            }

            return message;
        }
    }
}