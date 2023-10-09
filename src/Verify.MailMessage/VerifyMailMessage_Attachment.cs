namespace VerifyTests;

public static partial class VerifyMailMessage
{
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