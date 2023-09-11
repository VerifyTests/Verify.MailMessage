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
        var converters = Argon.DefaultContractResolver.Converters;
        converters.Add(new ContentDispositionConverter());
        converters.Add(new ContentTypeConverter());
        converters.Add(new MailAddressConverter());
        converters.Add(new MailAttachmentConverter());
        converters.Add(new MailMessageConverter());
    }
}
