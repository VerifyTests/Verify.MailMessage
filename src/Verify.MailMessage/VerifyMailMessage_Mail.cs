namespace VerifyTests;

public static partial class VerifyMailMessage
{
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