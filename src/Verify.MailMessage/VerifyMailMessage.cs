namespace VerifyTests;

public static class VerifyMailMessage
{
    public static bool Initialized { get; private set; }

    public static void Initialize()
    {
        if (Initialized)
        {
            throw new("Already Initialized");
        }

        Initialized = true;

        InnerVerifier.ThrowIfVerifyHasBeenRun();
        VerifierSettings.AddExtraSettings(_ =>
        {
            _.Converters.Add(new ContentDispositionConverter());
            _.Converters.Add(new ContentTypeConverter());
            _.Converters.Add(new AddressConverter());
            _.Converters.Add(new AttachmentConverter());
            _.Converters.Add(new AlternateViewConverter());
            _.Converters.Add(new MessageConverter());
        });
    }
}
