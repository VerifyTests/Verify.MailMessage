namespace VerifyTests;

public static partial class VerifyMailMessage
{
    static bool TryGetTarget(LinkedResource resource, string name, [NotNullWhen(true)] out Target? target)
    {
        if (!resource.TryGetExtension(out var extension))
        {
            target = null;
            return false;
        }

        target = AttachmentToTarget(extension, resource, name);
        return true;
    }

    static ConversionResult ConvertResource(LinkedResource resource, IReadOnlyDictionary<string, object> context)
    {
        var targets = new List<Target>();
        if (TryGetTarget(resource, "LinkedResource", out var target))
        {
            targets.Add(target.Value);
        }

        return new(resource, targets);
    }
}