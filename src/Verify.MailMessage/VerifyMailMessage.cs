using System.Net.Mail;
using EmptyFiles;

namespace VerifyTests;

public static class VerifyMailMessage
{
    public static bool Initialized { get; private set; }

    static List<JsonConverter> converters =
        [
        new ContentDispositionConverter(),
        new ContentTypeConverter(),
        new MailAddressConverter(),
        new MailAttachmentConverter(),
        new MailMessageConverter(),
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
        foreach (var attachment in message.Attachments)
        {
            if (ContentTypes.TryGetExtension(attachment.ContentType.ToString(), out var extension))
            {
                targets.Add(new(extension, attachment.ContentStream, attachment.Name));
            }
        }

        return new(message, targets);
    }
}