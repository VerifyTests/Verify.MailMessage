class MailAttachmentConverter :
    WriteOnlyJsonConverter<Attachment>
{
    public override void Write(VerifyJsonWriter writer, Attachment attachment)
    {
        writer.WriteStartObject();

        writer.WriteMember(attachment, attachment.Name, "Name");
        writer.WriteMember(attachment, attachment.NameEncoding, "NameEncoding");
        writer.WriteMember(attachment, attachment.ContentType, "ContentType");

        if (attachment.TransferEncoding != TransferEncoding.Base64)
        {
            writer.WriteMember(attachment, attachment.TransferEncoding, "TransferEncoding");
        }

        if (ContentTypes.IsText(attachment.ContentType.MediaType, out _))
        {
            using var reader = new StreamReader(attachment.ContentStream);
            writer.WriteMember(attachment, reader.ReadToEnd(), "Content");
        }

        writer.WriteEndObject();
    }
}