namespace VerifyTests;

public static partial class VerifyMailMessage
{
    static bool TryGetTarget(AttachmentBase attachment, string name, [NotNullWhen(true)] out Target? target)
    {
        if (!ContentTypes.TryGetExtension(attachment.ContentType.MediaType, out var extension) ||
            extension == "bin")
        {
            target = null;
            return false;
        }

        if (FileExtensions.IsText(extension))
        {
            var reader = new StreamReader(attachment.ContentStream);
            target = new Target(extension, reader.ReadToEnd(), name);
        }
        else
        {
            target = new Target(extension, attachment.ContentStream, name);
        }

        return true;
    }

    internal static bool IsAttachmentAtEnd(this AttachmentBase attachment)
    {
        var stream = attachment.ContentStream;
        // An attachment already written to a file due to a type converter will have position at end.
        return stream.Position != stream.Length;
    }
}