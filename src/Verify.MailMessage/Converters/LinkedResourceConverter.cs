class LinkedResourceConverter :
    WriteOnlyJsonConverter<LinkedResource>
{
    public override void Write(VerifyJsonWriter writer, LinkedResource resource)
    {
        writer.WriteStartObject();

        writer.WriteMember(resource, resource.ContentType, "ContentType");
        writer.WriteMember(resource, resource.ContentId, "ContentId");
        writer.WriteMember(resource, resource.ContentLink, "ContentLink");

        if (resource.TransferEncoding != TransferEncoding.Base64)
        {
            writer.WriteMember(resource, resource.TransferEncoding, "TransferEncoding");
        }

        if (resource.IsAttachmentAtEnd())
        {
            if (resource.TryGetContent(out var content))
            {
                writer.WriteMember(resource, content, "Content");
            }
            else
            {
                writer.WriteMember(resource, "binary", "Content");
            }
        }

        writer.WriteEndObject();
    }
}