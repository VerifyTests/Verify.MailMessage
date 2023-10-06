class ContentTypeConverter :
    WriteOnlyJsonConverter<ContentType>
{
    public override void Write(VerifyJsonWriter writer, ContentType contentType)
    {
        writer.WriteStartObject();

        writer.WriteMember(contentType, contentType.MediaType, "MediaType");
        writer.WriteMember(contentType, contentType.Name, "Name");
        writer.WriteMember(contentType, contentType.CharSet, "CharSet");
        writer.WriteMember(contentType, contentType.Boundary, "Boundary");
        // ReSharper disable once ConditionIsAlwaysTrueOrFalseAccordingToNullableAPIContract
        if (contentType.Parameters != null)
        {
            var parameters = new Dictionary<string, string?>();
            foreach (string key in contentType.Parameters.Keys)
            {
                var notNullKey = key!;
                if (notNullKey is
                    "name" or
                    "boundary" or
                    "charset")
                {
                  continue;
                }

                parameters[notNullKey] = contentType.Parameters[notNullKey];
            }
            writer.WriteMember(contentType, parameters, "Parameters");
        }

        writer.WriteEndObject();
    }
}