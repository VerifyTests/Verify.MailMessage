class MailMessageConverter :
    WriteOnlyJsonConverter<MailMessage>
{
    public override void Write(VerifyJsonWriter writer, MailMessage mail)
    {
        writer.WriteStartObject();

        writer.WriteMember(mail, mail.From, "From");
        writer.WriteMember(mail, mail.Sender, "Sender");
        WriteAddresses(writer, mail, mail.To, "To");
        WriteAddresses(writer, mail, mail.CC, "Cc");
        WriteAddresses(writer, mail, mail.Bcc, "Bcc");
        WriteAddresses(writer, mail, mail.ReplyToList, "ReplyTo");

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

    static void WriteAddresses(VerifyJsonWriter writer, MailMessage mail, MailAddressCollection addresses, string name)
    {
        if (!addresses.Any())
        {
            return;
        }

        if (addresses.Count == 1)
        {
            writer.WriteMember(mail, addresses[0], name);
        }
        else
        {
            writer.WriteMember(mail, addresses, name);
        }
    }
}