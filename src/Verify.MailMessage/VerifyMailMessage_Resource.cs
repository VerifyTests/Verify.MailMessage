namespace VerifyTests;

public static partial class VerifyMailMessage
{
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