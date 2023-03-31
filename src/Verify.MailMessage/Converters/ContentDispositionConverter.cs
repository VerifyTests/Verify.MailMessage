using System.Net.Mime;

class ContentDispositionConverter :
    WriteOnlyJsonConverter<ContentDisposition>
{
    public override void Write(VerifyJsonWriter writer, ContentDisposition disposition)
    {
        writer.WriteStartObject();

        writer.WriteMember(disposition, disposition.DispositionType, "DispositionType");
        writer.WriteMember(disposition, disposition.FileName, "FileName");
        if (disposition.CreationDate != DateTime.MinValue)
        {
            writer.WriteMember(disposition, disposition.CreationDate, "CreationDate");
        }

        if (disposition.ModificationDate != DateTime.MinValue)
        {
            writer.WriteMember(disposition, disposition.ModificationDate, "ModificationDate");
        }

        if (disposition.ReadDate != DateTime.MinValue)
        {
            writer.WriteMember(disposition, disposition.ReadDate, "ReadDate");
        }

        if (disposition.Size != -1)
        {
            writer.WriteMember(disposition, disposition.Size, "Size");
        }

        if (disposition.Parameters != null)
        {
            var parameters = new Dictionary<string, string?>();
            foreach (string key in disposition.Parameters.Keys)
            {
                var notNullKey = key!;
                if (notNullKey is
                    "size" or
                    "read-date" or
                    "filename" or
                    "tispositiontype" or
                    "creation-date" or
                    "modification-date")
                {
                    continue;
                }

                parameters[notNullKey] = disposition.Parameters[notNullKey];
            }

            writer.WriteMember(disposition, parameters, "Parameters");
        }

        writer.WriteEndObject();
    }
}