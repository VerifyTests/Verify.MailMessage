namespace VerifyTests;

public static partial class VerifyMailMessage
{
    static IEnumerable<Target> GetTargets(AlternateView view, string viewName)
    {
        if (TryGetTarget(view, viewName, out var viewTarget))
        {
            yield return viewTarget.Value;
        }

        for (var resourceIndex = 0; resourceIndex < view.LinkedResources.Count; resourceIndex++)
        {
            var resource = view.LinkedResources[resourceIndex];
            if (TryGetTarget(resource, $"{viewName}_LinkedResources{resourceIndex + 1}", out var resourceTarget))
            {
                yield return resourceTarget.Value;
            }
        }
    }

    static ConversionResult ConvertView(AlternateView view, IReadOnlyDictionary<string, object> context) =>
        new(view, GetTargets(view, "View").ToList());
}