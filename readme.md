# <img src="/src/icon.png" height="30px"> Verify.MailMessage

[![Discussions](https://img.shields.io/badge/Verify-Discussions-yellow?svg=true&label=)](https://github.com/orgs/VerifyTests/discussions)
[![Build status](https://ci.appveyor.com/api/projects/status/cpmnux3i0euge195?svg=true)](https://ci.appveyor.com/project/SimonCropp/verify-mailmessage)
[![NuGet Status](https://img.shields.io/nuget/v/Verify.MailMessage.svg)](https://www.nuget.org/packages/Verify.MailMessage/)

Extends [Verify](https://github.com/VerifyTests/Verify) to allow verification of [MailMessage](https://learn.microsoft.com/en-us/dotnet/api/system.net.mail.mailmessage) and related types.

**See [Milestones](../../milestones?state=closed) for release notes.**


## Sponsors

include: zzz


## NuGet

 * https://nuget.org/packages/Verify.MailMessage


## Usage

<!-- snippet: Enable -->
<a id='snippet-Enable'></a>
```cs
[ModuleInitializer]
public static void Initialize() =>
    VerifyMailMessage.Initialize();
```
<sup><a href='/src/Tests/ModuleInitializer.cs#L3-L9' title='Snippet source file'>snippet source</a> | <a href='#snippet-Enable' title='Start of snippet'>anchor</a></sup>
<!-- endSnippet -->


### ContentDisposition

<!-- snippet: ContentDisposition -->
<a id='snippet-ContentDisposition'></a>
```cs
[Fact]
public Task ContentDisposition()
{
    var content = new ContentDisposition("attachment; filename=\"filename.jpg\"");
    return Verify(content);
}
```
<sup><a href='/src/Tests/Tests.cs#L3-L12' title='Snippet source file'>snippet source</a> | <a href='#snippet-ContentDisposition' title='Start of snippet'>anchor</a></sup>
<!-- endSnippet -->

Results in: 

<!-- snippet: Tests.ContentDisposition.verified.txt -->
<a id='snippet-Tests.ContentDisposition.verified.txt'></a>
```txt
{
  DispositionType: attachment,
  FileName: filename.jpg
}
```
<sup><a href='/src/Tests/Tests.ContentDisposition.verified.txt#L1-L4' title='Snippet source file'>snippet source</a> | <a href='#snippet-Tests.ContentDisposition.verified.txt' title='Start of snippet'>anchor</a></sup>
<!-- endSnippet -->


### ContentType

<!-- snippet: ContentType -->
<a id='snippet-ContentType'></a>
```cs
[Fact]
public Task ContentType()
{
    var content = new ContentType("text/html; charset=utf-8")
    {
        Name = "name.txt"
    };
    return Verify(content);
}
```
<sup><a href='/src/Tests/Tests.cs#L36-L48' title='Snippet source file'>snippet source</a> | <a href='#snippet-ContentType' title='Start of snippet'>anchor</a></sup>
<!-- endSnippet -->

Results in: 

<!-- snippet: Tests.ContentType.verified.txt -->
<a id='snippet-Tests.ContentType.verified.txt'></a>
```txt
{
  MediaType: text/html,
  Name: name.txt,
  CharSet: utf-8
}
```
<sup><a href='/src/Tests/Tests.ContentType.verified.txt#L1-L5' title='Snippet source file'>snippet source</a> | <a href='#snippet-Tests.ContentType.verified.txt' title='Start of snippet'>anchor</a></sup>
<!-- endSnippet -->


### Attachment

<!-- snippet: Attachment -->
<a id='snippet-Attachment'></a>
```cs
[Fact]
public Task Attachment()
{
    var attachment = new Attachment(
        new MemoryStream("file content"u8.ToArray()),
        new ContentType("text/html; charset=utf-8"))
    {
        Name = "name.txt"
    };
    return Verify(attachment);
}
```
<sup><a href='/src/Tests/Tests.cs#L67-L81' title='Snippet source file'>snippet source</a> | <a href='#snippet-Attachment' title='Start of snippet'>anchor</a></sup>
<!-- endSnippet -->

Results in: 

<!-- snippet: Tests.Attachment.verified.txt -->
<a id='snippet-Tests.Attachment.verified.txt'></a>
```txt
{
  Name: name.txt,
  ContentType: {
    MediaType: text/html,
    Name: name.txt,
    CharSet: utf-8
  },
  ContentId: Guid_1,
  ContentDisposition: {
    DispositionType: attachment
  }
}
```
<sup><a href='/src/Tests/Tests.Attachment.verified.txt#L1-L12' title='Snippet source file'>snippet source</a> | <a href='#snippet-Tests.Attachment.verified.txt' title='Start of snippet'>anchor</a></sup>
<!-- endSnippet -->


### MailMessage

<!-- snippet: MailMessage -->
<a id='snippet-MailMessage'></a>
```cs
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
```
<sup><a href='/src/Tests/Tests.cs#L188-L201' title='Snippet source file'>snippet source</a> | <a href='#snippet-MailMessage' title='Start of snippet'>anchor</a></sup>
<!-- endSnippet -->

Results in: 

<!-- snippet: Tests.MailMessage.verified.txt -->
<a id='snippet-Tests.MailMessage.verified.txt'></a>
```txt
{
  From: from@mail.com,
  To: to@mail.com,
  Subject: The subject,
  IsBodyHtml: false,
  Body: The body
}
```
<sup><a href='/src/Tests/Tests.MailMessage.verified.txt#L1-L7' title='Snippet source file'>snippet source</a> | <a href='#snippet-Tests.MailMessage.verified.txt' title='Start of snippet'>anchor</a></sup>
<!-- endSnippet -->


## Icon

[Mail](https://thenounproject.com/icon/mail-5633084/)  from [The Noun Project](https://thenounproject.com).
