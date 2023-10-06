class AlternateViewConverter :
    WriteOnlyJsonConverter<AlternateView>
{
    public override void Write(VerifyJsonWriter writer, AlternateView attachment)
    {
        writer.WriteStartObject();

        writer.WriteMember(attachment, attachment.BaseUri, "BaseUri");
        writer.WriteMember(attachment, attachment.ContentType, "ContentType");
        writer.WriteMember(attachment, attachment.ContentId, "ContentId");

        if (attachment.TransferEncoding != TransferEncoding.Base64)
        {
            writer.WriteMember(attachment, attachment.TransferEncoding, "TransferEncoding");
        }

        //TODO: linked resources

        if (attachment.TryGetContent(out var content))
        {
            writer.WriteMember(attachment, content, "Content");
        }
        else
        {
            writer.WriteMember(attachment, "binary", "Content");
        }

        writer.WriteEndObject();
    }
}