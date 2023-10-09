using System.Diagnostics.CodeAnalysis;

namespace VerifyTests;

public static class VerifyMailMessage
{
    public static bool Initialized { get; private set; }

    static List<JsonConverter> converters =
        [
        new ContentDispositionConverter(),
        new ContentTypeConverter(),
        new AlternateViewConverter(),
        new AddressConverter(),
        new AttachmentConverter(),
        new LinkedResourceConverter(),
        new MessageConverter(),
        ];

    public static void Initialize()
    {
        if (Initialized)
        {
            throw new("Already Initialized");
        }

        Initialized = true;

        InnerVerifier.ThrowIfVerifyHasBeenRun();
        VerifierSettings.AddExtraSettings(_ => _.Converters.AddRange(converters));
        VerifierSettings.RegisterFileConverter<MailMessage>(ConvertMail);
    }

    static ConversionResult ConvertMail(MailMessage message, IReadOnlyDictionary<string, object> context)
    {
        var targets = new List<Target>();
        for (var index = 0; index < message.Attachments.Count; index++)
        {
            var attachment = message.Attachments[index];
            if (!attachment.TryGetExtension(out var extension))
            {
                continue;
            }

            var name = $"Attachment{index + 1}";
            targets.Add(AttachmentToTarget(extension, attachment, name));
        }

        for (var index = 0; index < message.AlternateViews.Count; index++)
        {
            var view = message.AlternateViews[index];
            if (!view.TryGetExtension(out var extension))
            {
                continue;
            }

            var viewName = $"AlternateView{index + 1}";
            targets.Add(AttachmentToTarget(extension, view, viewName));

            for (var resourceIndex = 0; resourceIndex < view.LinkedResources.Count; resourceIndex++)
            {
                var resource = view.LinkedResources[resourceIndex];
                if (!view.TryGetExtension(out var resourceExtension))
                {
                    continue;
                }

                var resourcesName = $"{viewName}_LinkedResources{resourceIndex + 1}";
                targets.Add(AttachmentToTarget(resourceExtension, resource, resourcesName));
            }
        }

        return new(message, targets);
    }

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