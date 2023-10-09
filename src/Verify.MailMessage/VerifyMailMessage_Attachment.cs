using System.Diagnostics.CodeAnalysis;

namespace VerifyTests;

public static partial class VerifyMailMessage
{
    static bool TryGetTarget(Attachment attachment, string name, [NotNullWhen(true)] out Target? target)
    {
        if (!attachment.TryGetExtension(out var extension))
        {
            target = null;
            return false;
        }

        target = AttachmentToTarget(extension, attachment, name);
        return true;
    }
}