namespace VerifyTests;

public static partial class VerifyMailMessage
{
    static IEnumerable<Target> GetTargets(AlternateView view, string viewName)
    {
        if (!view.TryGetExtension(out var extension))
        {
            yield break;
        }

        yield return AttachmentToTarget(extension, view, viewName);

        for (var resourceIndex = 0; resourceIndex < view.LinkedResources.Count; resourceIndex++)
        {
            var resource = view.LinkedResources[resourceIndex];
            var resourcesName = $"{viewName}_LinkedResources{resourceIndex + 1}";
            if (resource.TryGetExtension(out var resourceExtension))
            {
                yield return AttachmentToTarget(resourceExtension, resource, resourcesName);
            }
        }
    }
}