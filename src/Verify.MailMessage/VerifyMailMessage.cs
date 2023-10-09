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
        VerifierSettings.RegisterFileConverter<Attachment>(ConvertAttachment);
        VerifierSettings.RegisterFileConverter<AlternateView>(ConvertView);
        VerifierSettings.RegisterFileConverter<LinkedResource>(ConvertResource);
    }
}