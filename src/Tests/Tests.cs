using System.Collections.Specialized;
using System.Net.Mail;
using System.Net.Mime;

[UsesVerify]
public class Tests
{
    #region ContentDisposition

    [Fact]
    public Task ContentDisposition()
    {
        var content = new ContentDisposition("attachment; filename=\"filename.jpg\"");
        return Verify(content);
    }

    #endregion

    [Fact]
    public Task ContentDispositionFull()
    {
        var content = new ContentDisposition("inline; filename=\"filename.jpg\"")
        {
            CreationDate = DateTime.Now,
            ModificationDate = DateTime.Now,
            ReadDate = DateTime.Now,
            Size = 2,
            Parameters =
            {
                {
                    "key", "value"
                }
            }
        };
        return Verify(content);
    }

    #region ContentType

    [Fact]
    public Task ContentType()
    {
        var content = new ContentType("text/html; charset=utf-8")
        {
            Name = "name.txt"
        };
        return Verify(content);
    }

    #endregion

    [Fact]
    public Task ContentTypeFull()
    {
        var content = new ContentType("text/html; charset=utf-8")
        {
            Name = "name.txt",
            Boundary = "the boundary",
            Parameters =
            {
                {
                    "key", "value"
                }
            }
        };
        return Verify(content);
    }

    #region MailAttachment

    [Fact]
    public Task MailAttachment()
    {
        var attachment = new Attachment(
            new MemoryStream("file content"u8.ToArray()),
            new ContentType("text/html; charset=utf-8"))
        {
            Name = "name.txt"
        };
        return Verify(attachment);
    }

    #endregion

    [Fact]
    public Task MailAttachmentFull()
    {
        var attachment = new Attachment(
            new MemoryStream("file content"u8.ToArray()),
            new ContentType("text/html; charset=utf-8"))
        {
            Name = "name.txt",
            TransferEncoding = TransferEncoding.EightBit,
        };
        return Verify(attachment);
    }

    #region AlternateView

    [Fact]
    public Task AlternateView()
    {
        var view = new AlternateView(
            new MemoryStream("file content"u8.ToArray()),
            new ContentType("text/html; charset=utf-8"))
        {
            ContentId = "the content id"
        };
        return Verify(view);
    }

    #endregion

    [Fact]
    public Task AlternateViewFull()
    {
        var view = new AlternateView(
            new MemoryStream("file content"u8.ToArray()),
            new ContentType("text/html; charset=utf-8"))
        {
            ContentId = "the content id",
            BaseUri = new("http://url"),
            TransferEncoding = TransferEncoding.EightBit,
        };
        return Verify(view);
    }

    #region MailMessage

    [Fact]
    public Task MailMessage()
    {
        var mail = new MailMessage(
            from: "from@mail.com",
            to: "to@mail.com",
            subject: "The subject",
            body: "The body");
        return Verify(mail);
    }

    #endregion

    [Fact]
    public Task MailMessageFull()
    {
        var mail = BuildMail();
        return Verify(mail);
    }

    [Fact]
    public Task MailMessageFullNested()
    {
        var mail = BuildMail();
        return Verify(new
        {
            mail
        });
    }

    static MailMessage BuildMail() =>
        new(
            from: new("from@mail.com", "the from"),
            to: new MailAddress("sender@mail.com", "the to"))
        {
            Subject = "The subject",
            Body = "The body",
            Headers =
            {
                new NameValueCollection
                {
                    {
                        "key", "value"
                    }
                }
            },
            Priority = MailPriority.High,
            Sender = new("sender@mail.com", "the sender"),
            Bcc =
            {
                new MailAddress("bcc@mail.com", "the bcc")
            },
            CC =
            {
                new MailAddress("cc@mail.com", "the ccc")
            },
            BodyEncoding = Encoding.BigEndianUnicode,
            SubjectEncoding = Encoding.UTF32,
            HeadersEncoding = Encoding.ASCII,
            BodyTransferEncoding = TransferEncoding.EightBit,
            IsBodyHtml = true,
            ReplyToList =
            {
                new MailAddress("reply@mail.com", "the reply")
            },
            DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure | DeliveryNotificationOptions.Delay,
            Attachments =
            {
                new Attachment(
                    new MemoryStream("file content"u8.ToArray()),
                    new ContentType("text/html; charset=utf-8"))
                {
                    Name = "name.txt"
                }
            }
        };
}