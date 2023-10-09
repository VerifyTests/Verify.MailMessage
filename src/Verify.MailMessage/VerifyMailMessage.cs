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
        new AlternateViewConverter(),
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
            if (!TryGetExtension(attachment, out var extension))
            {
                continue;
            }

            var name = attachment.Name ?? $"Attachment{index + 1}";
            targets.Add(AttachmentToTarget(extension, attachment, name));
        }


        return new(message, targets);
    }

    static Target AttachmentToTarget(string extension, AttachmentBase attachment, string name)
    {
        if (FileExtensions.IsText(extension))
        {
            var reader = new StreamReader(attachment.ContentStream);
            return new(extension, reader.ReadToEnd(), name);
        }

        return new(extension, attachment.ContentStream, name);
    }

    static bool TryGetExtension(Attachment attachment, [NotNullWhen(true)] out string? extension)
    {
        if (attachment.Name != null)
        {
            extension = Path.GetExtension(attachment.Name);
            if (!string.IsNullOrWhiteSpace(extension) && extension.Length == 3)
            {
                return true;
            }
        }

        return ContentTypes.TryGetExtension(attachment.ContentType.MediaType, out extension);
    }
}