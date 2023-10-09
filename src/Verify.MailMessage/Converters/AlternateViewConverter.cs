class AlternateViewConverter :
    WriteOnlyJsonConverter<AlternateView>
{
    public override void Write(VerifyJsonWriter writer, AlternateView view)
    {
        writer.WriteStartObject();

        writer.WriteMember(view, view.BaseUri, "BaseUri");
        writer.WriteMember(view, view.ContentType, "ContentType");
        writer.WriteMember(view, view.ContentId, "ContentId");

        if (view.TransferEncoding != TransferEncoding.Base64)
        {
            writer.WriteMember(view, view.TransferEncoding, "TransferEncoding");
        }

        if (view.IsAttachmentAtEnd())
        {
            if (view.TryGetContent(out var content))
            {
                writer.WriteMember(view, content, "Content");
            }
            else
            {
                writer.WriteMember(view, "binary", "Content");
            }
        }

        writer.WriteMember(view, view.LinkedResources, "LinkedResources");
        writer.WriteEndObject();
    }
}