using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using WLV.LMS.Common.Logging;

namespace WLV.LMS.Common.MailService
{
    public class EmailManager
    {
        /// <summary>
        /// The email thread lock
        /// </summary>
        private static object emailThreadLock = new object();

        /// <summary>
        /// Initializes the <see cref="EmailManager"/> class.
        /// </summary>
        public EmailManager()
        {
        }

        /// <summary>
        /// Sends the email.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <returns></returns>
        public static bool SendEmail(EmailMessage message, OutgoingMailSetting outgoingMailSetting)
        {
            var result = false;

            try
            {
                var htmlBody = string.Empty;
                if (message.MessageBody != null)
                    result = Send(message, outgoingMailSetting);
            }
            catch (Exception ex)
            {
                LogManager.Log(LogSeverity.Error, LogModule.Common, ex.Message,ex);
                return result;
            }

            return result;
        }

        /// <summary>
        /// Determines whether [is valid email] [the specified email].
        /// </summary>
        /// <param name="email">The email.</param>
        /// <returns></returns>
        private static bool IsValidEmail(string email)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(email))
                    email = email.Trim();

                var mailAddress = new MailAddress(email);
                return mailAddress.Address == email;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Sends the specified message.  
        /// </summary>
        /// <param name="message">The message.</param>
        private static bool Send(EmailMessage message, OutgoingMailSetting outgoingMailSetting)
        {
            var result = false;

            lock (emailThreadLock)
            {
                try
                {
                    var mail = new MailMessage();

                    if (!string.IsNullOrWhiteSpace(message.To))
                        mail.To.Add(ValidateEamilAddresses(message.To));

                    if (!string.IsNullOrWhiteSpace(message.CC))
                        mail.CC.Add(ValidateEamilAddresses(message.CC));

                    if (!string.IsNullOrWhiteSpace(message.BCC))
                        mail.Bcc.Add(ValidateEamilAddresses(message.BCC));

                    mail.From = new MailAddress(outgoingMailSetting.MailAddress, outgoingMailSetting.DisplayName);
                    mail.Subject = message.Subject;
                    mail.IsBodyHtml = true;
                    mail.Body = message.MessageBody;

                    if (message.Attachments != null)
                        foreach (var attachment in message.Attachments)
                        {
                            MemoryStream ms = new MemoryStream(attachment.Content);
                            mail.Attachments.Add(new Attachment(ms, attachment.AttachmentName));
                        }

                    if (message.InlineConents != null)
                        foreach (var content in message.InlineConents)
                        {
                            var ms = new MemoryStream(content.Content);
                            var attachment = new Attachment(ms, content.ContentId);

                            if (message.IsHtml)
                            {
                                attachment.ContentId = content.ContentId;
                                attachment.ContentDisposition.Inline = true;
                                attachment.ContentDisposition.DispositionType = DispositionTypeNames.Inline;
                            }

                            mail.Attachments.Add(attachment);
                        }

                    SmtpClient smtpClient = new SmtpClient(outgoingMailSetting.SmtpServer);
                    smtpClient.Port = outgoingMailSetting.SmtpPort;

                    // If password is not provided default email authentication
                    if (string.IsNullOrEmpty(outgoingMailSetting.Password))
                        smtpClient.UseDefaultCredentials = true;
                    else
                    {
                        smtpClient.UseDefaultCredentials = false;
                        smtpClient.EnableSsl = true;
                        smtpClient.Credentials = new NetworkCredential(outgoingMailSetting.MailAddress, outgoingMailSetting.Password);
                    }

                    smtpClient.Send(mail);
                    result = true;
                }
                catch (Exception ex)
                {
                    result = false;
                    LogManager.Log(LogSeverity.Error, LogModule.Common, string.Format("Failed to send the email. Error Message: {0}", ex.Message), ex);
                }

                return result;
            }
        }

        /// <summary>
        /// Validates the eamil addresses.
        /// </summary>
        /// <param name="emailAddressString">The email address string.</param>
        /// <returns></returns>
        private static string ValidateEamilAddresses(string emailAddressString)
        {
            var emailAddress = new List<string>();
            try
            {
                var addresses = emailAddressString.Split(";,".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                foreach (var address in addresses)
                {
                    if (IsValidEmail(address))
                        emailAddress.Add(address);
                }
            }
            catch (Exception ex)
            {
                LogManager.Log(LogSeverity.Error, LogModule.Common, ex.Message);
            }

            return string.Join(",", emailAddress.ToArray());
        }

        private static MailAddress ToMailAddresses(string emailAddressString)
        {
            var mailAddress = new MailAddress(emailAddressString);
            return mailAddress;
        }
    }
}
