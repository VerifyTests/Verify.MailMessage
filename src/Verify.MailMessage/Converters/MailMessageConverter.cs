using System.Net.Mail;
using System.Net.Mime;

class MailMessageConverter :
    WriteOnlyJsonConverter<MailMessage>
{
    public override void Write(VerifyJsonWriter writer, MailMessage mail)
    {
        writer.WriteStartObject();

        writer.WriteMember(mail, mail.From, "From");
        writer.WriteMember(mail, mail.Sender, "Sender");
        var to = mail.To;
        if (to.Any())
        {
            if (to.Count > 1)
            {
                writer.WriteMember(mail, to, "To");
            }
            else
            {
                writer.WriteMember(mail, to[0], "To");
            }
        }

        var cc = mail.CC;
        if (cc.Any())
        {
            if (cc.Count > 1)
            {
                writer.WriteMember(mail, cc, "Cc");
            }
            else
            {
                writer.WriteMember(mail, cc[0], "Cc");
            }
        }

        var bcc = mail.Bcc;
        if (bcc.Any())
        {
            if (bcc.Count > 1)
            {
                writer.WriteMember(mail, bcc, "Bcc");
            }
            else
            {
                writer.WriteMember(mail, bcc[0], "Bcc");
            }
        }

        var reply = mail.ReplyToList;
        if (reply.Any())
        {
            if (reply.Count > 1)
            {
                writer.WriteMember(mail, reply, "ReplyTo");
            }
            else
            {
                writer.WriteMember(mail, reply[0], "ReplyTo");
            }
        }

        if (mail.Priority != MailPriority.Normal)
        {
            writer.WriteMember(mail, mail.Priority, "Priority");
        }

        writer.WriteMember(mail, mail.Subject, "Subject");
        writer.WriteMember(mail, mail.SubjectEncoding, "SubjectEncoding");
        writer.WriteMember(mail, mail.Headers, "Headers");
        writer.WriteMember(mail, mail.HeadersEncoding, "HeadersEncoding");
        if (mail.DeliveryNotificationOptions != DeliveryNotificationOptions.None)
        {
            writer.WriteMember(mail, mail.DeliveryNotificationOptions, "DeliveryNotificationOptions");
        }

        if (!Equals(mail.BodyEncoding, Encoding.ASCII))
        {
            writer.WriteMember(mail, mail.BodyEncoding, "BodyEncoding");
        }

        if (mail.BodyTransferEncoding != TransferEncoding.Unknown)
        {
            //TODO: work out why TransferEncoding enum needs a ToString
            writer.WriteMember(mail, mail.BodyTransferEncoding.ToString(), "BodyTransferEncoding");
        }

        writer.WriteMember(mail, mail.IsBodyHtml, "IsBodyHtml");
        writer.WriteMember(mail, mail.Body, "Body");

        writer.WriteMember(mail, mail.Attachments, "Attachments");
        writer.WriteEndObject();
    }
}