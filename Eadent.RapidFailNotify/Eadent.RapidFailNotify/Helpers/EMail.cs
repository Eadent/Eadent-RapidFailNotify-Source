using Eadent.RapidFailNotify.DataTransferObjects;
using MailKit.Net.Smtp;
using MimeKit;
using Newtonsoft.Json;
using System;
using System.IO;

namespace Eadent.RapidFailNotify.Helpers
{
    public static class EMail
    {
        public static void Send(string toEMailName, string toEMailAddress, string subject, string htmlBody)
        {
            string eMailSettingsFilePath = "App_Data/Confidential/E-Mail.settings.json";  // TODO: Consider obtaining from Configuration.

            string eMailSettingsString = File.ReadAllText(eMailSettingsFilePath);

            EMailSettingsDto eMailSettings = JsonConvert.DeserializeObject<EMailSettingsDto>(eMailSettingsString);

            var mailMessage = new MimeMessage();

            mailMessage.From.Add(new MailboxAddress(eMailSettings.FromEMailName, eMailSettings.FromEMailAddress));
            mailMessage.To.Add(new MailboxAddress(toEMailName, toEMailAddress));
            mailMessage.Subject = subject;
            mailMessage.Body = new TextPart("html")
            {
                Text = htmlBody
            };

            using (var smtpClient = new SmtpClient())
            {
                smtpClient.Connect(eMailSettings.HostName, eMailSettings.HostPort, true);
                smtpClient.Authenticate(eMailSettings.SenderEMailAddress, eMailSettings.SenderPassword);
                smtpClient.Send(mailMessage);
                smtpClient.Disconnect(true);
            }
        }
    }
}
