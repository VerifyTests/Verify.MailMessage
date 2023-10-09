namespace VerifyTests;

public static partial class VerifyMailMessage
{
    static bool TryGetExtension(this AttachmentBase view,[NotNullWhen(true)] out string? extension) =>
        ContentTypes.TryGetExtension(view.ContentType.MediaType, out extension);

    static Target AttachmentToTarget(string extension, AttachmentBase attachment, string name)
    {
        if (FileExtensions.IsText(extension))
        {
            var reader = new StreamReader(attachment.ContentStream);
            return new(extension, reader.ReadToEnd(), name);
        }

        return new(extension, attachment.ContentStream, name);
    }

    internal static bool IsAttachmentAtEnd(this AttachmentBase attachment)
    {
        var stream = attachment.ContentStream;
        // An attachment already written to a file due to a type converter will have position at end.
        return stream.Position != stream.Length;
    }
}