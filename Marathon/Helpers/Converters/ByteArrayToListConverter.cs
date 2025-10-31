using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Marathon.Helpers.Converters
{
    public class ByteArrayToListConverter : JsonConverter<byte[]>
    {
        public override byte[] ReadJson(JsonReader in_reader, Type in_type, byte[] in_existingValue, bool in_hasExistingValue, JsonSerializer in_serialiser)
        {
            if (in_reader.TokenType == JsonToken.Null)
                return null;

            var buffer = new List<byte>();

            while (in_reader.Read())
            {
                if (in_reader.TokenType == JsonToken.EndArray)
                    break;

                if (in_reader.TokenType == JsonToken.Integer)
                    buffer.Add(Convert.ToByte(in_reader.Value));
            }

            return [.. buffer];
        }

        public override void WriteJson(JsonWriter in_writer, byte[] in_value, JsonSerializer in_serialiser)
        {
            if (in_value == null)
            {
                in_writer.WriteNull();
                return;
            }

            in_writer.WriteStartArray();

            foreach (var b in in_value)
                in_writer.WriteValue(b);

            in_writer.WriteEndArray();
        }
    }
}
