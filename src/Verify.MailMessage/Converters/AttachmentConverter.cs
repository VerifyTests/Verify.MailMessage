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

        var stream = attachment.ContentStream;
        // An attachment already written to a file due to a type converter will have position at end.
        if (stream.Position != stream.Length)
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