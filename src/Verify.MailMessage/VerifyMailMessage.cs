namespace VerifyTests;

public static partial class VerifyMailMessage
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
            if (TryGetTarget(attachment, $"Attachment{index + 1}", out var target))
            {
                targets.Add(target.Value);
            }
        }

        for (var index = 0; index < message.AlternateViews.Count; index++)
        {
            var view = message.AlternateViews[index];
            targets.AddRange(GetTargets(view, $"AlternateView{index + 1}"));
        }

        return new(message, targets);
    }
}