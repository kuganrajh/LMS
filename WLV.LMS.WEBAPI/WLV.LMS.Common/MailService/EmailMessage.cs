using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WLV.LMS.Common.MailService
{
    public class EmailMessage
    {
        /// <summary>
        /// Gets or sets to.
        /// </summary>
        public string To { get; set; }

        /// <summary>
        /// Gets or sets the cc.
        /// </summary>
        public string CC { get; set; }

        /// <summary>
        /// Gets or sets the BCC.
        /// </summary>
        public string BCC { get; set; }

        /// <summary>
        /// Gets or sets the subject.
        /// </summary>
        public string Subject { get; set; }

        /// <summary>
        /// Gets or sets the message body.
        /// </summary>
        /// <value>
        /// The message body.
        /// </value>
        public string MessageBody { get; set; }

        /// <summary>
        /// Gets or sets the keywords.
        /// </summary>
        /// <value>
        /// The keywords.
        /// </value>
        public IReadOnlyDictionary<string, string> Keywords { get; set; }

        /// <summary>
        /// Gets or sets the attachments.
        /// </summary>
        /// <value>
        /// The attachments.
        /// </value>
        public IList<MailAttachment> Attachments { get; set; }

        /// <summary>
        /// Gets or sets the inline conents.
        /// </summary>
        /// <value>
        /// The inline conents.
        /// </value>
        public IList<InlineContent> InlineConents { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is HTML.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is HTML; otherwise, <c>false</c>.
        /// </value>
        public bool IsHtml { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="EmailMessage"/> class.
        /// </summary>
        public EmailMessage()
        {
            To = string.Empty;
            CC = string.Empty;
            BCC = string.Empty;
            Subject = string.Empty;
            MessageBody = string.Empty;
            Attachments = new List<MailAttachment>();
            InlineConents = new List<InlineContent>();
            IsHtml = true;
        }
    }

    public class InlineContent
    {
        public string ContentId { get; set; }

        public byte[] Content { get; set; }
    }

    public class MailAttachment
    {
        public byte[] Content { get; set; }

        public string AttachmentName { get; set; }
    }

    public class OutgoingMailSetting
    {
        public string DisplayName { get; set; }

        public string MailAddress { get; set; }

        public string Password { get; set; }

        public string SmtpServer { get; set; }

        public int SmtpPort { get; set; }

        public OutgoingMailSetting()
        {
            DisplayName = string.Empty;
            MailAddress = string.Empty;
            Password = string.Empty;
            SmtpServer = string.Empty;
        }
    }
}
