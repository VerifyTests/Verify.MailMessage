class AttachmentConverter :
    WriteOnlyJsonConverter<Attachment>
{
    public override void Write(VerifyJsonWriter writer, Attachment attachment)
    {
        writer.WriteStartObject();

        writer.WriteMember(attachment, attachment.Name, "Name");
        writer.WriteMember(attachment, attachment.NameEncoding, "NameEncoding");
        writer.WriteMember(attachment, attachment.ContentType, "ContentType");
        writer.WriteMember(attachment, attachment.ContentId, "ContentId");

        if (attachment.TransferEncoding != TransferEncoding.Base64)
        {
            writer.WriteMember(attachment, attachment.TransferEncoding, "TransferEncoding");
        }

        writer.WriteMember(attachment, attachment.ContentDisposition, "ContentDisposition");

        if (attachment.IsAttachmentAtEnd())
        {
            if (attachment.TryGetContent(out var content))
            {
                writer.WriteMember(attachment, content, "Content");
            }
            else
            {
                writer.WriteMember(attachment, "binary", "Content");
            }
        }

        writer.WriteEndObject();
    }
}