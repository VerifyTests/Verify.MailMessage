namespace VerifyTests;

public static partial class VerifyMailMessage
{
    static bool TryGetTarget(Attachment attachment, string name, [NotNullWhen(true)] out Target? target)
    {
        if (!attachment.TryGetExtension(out var extension))
        {
            target = null;
            return false;
        }

        target = AttachmentToTarget(extension, attachment, name);
        return true;
    }

    static ConversionResult ConvertAttachment(Attachment attachment, IReadOnlyDictionary<string, object> context)
    {
        var targets = new List<Target>();
        if (TryGetTarget(attachment, "Attachment", out var target))
        {
            targets.Add(target.Value);
        }

        return new(attachment, targets);
    }
}