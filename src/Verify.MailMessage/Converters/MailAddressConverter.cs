using System.Net.Mail;

class MailAddressConverter :
    WriteOnlyJsonConverter<MailAddress>
{
    public override void Write(VerifyJsonWriter writer, MailAddress address)
    {
        if (address.DisplayName == "")
        {
            writer.WriteRawValueWithScrubbers(address.Address);
        }
        else
        {
            writer.WriteRawValueWithScrubbers($"{address.DisplayName} <{address.Address}>");
        }
    }
}