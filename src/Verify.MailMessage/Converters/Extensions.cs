static class Extensions
{
    public static bool TryGetContent(this AttachmentBase attachment, [NotNullWhen(true)] out string? content)
    {
        if (ContentTypes.IsText(attachment.ContentType.MediaType, out _))
        {
            using var reader = new StreamReader(attachment.ContentStream);
            content = reader.ReadToEnd();
            return true;
        }

        content = null;
        return false;
    }
}