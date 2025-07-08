using Newtonsoft.Json;
using System;

namespace Marathon.Helpers.Converters
{
    public class ByteArrayConverter : JsonConverter
    {
        public override bool CanConvert(Type in_type)
        {
            return in_type == typeof(byte[]);
        }

        public override object ReadJson(JsonReader in_reader, Type in_type, object in_existingValue, JsonSerializer in_serialiser)
        {
            if (in_reader.TokenType == JsonToken.String)
            {
                string hex = in_serialiser.Deserialize<string>(in_reader);

                if (!string.IsNullOrEmpty(hex))
                    return BinaryHelper.TransformHexStringToByteArray(hex);
            }

            return Array.Empty<byte>();
        }

        public override void WriteJson(JsonWriter in_writer, object in_value, JsonSerializer in_serialiser)
        {
            in_serialiser.Serialize(in_writer, BitConverter.ToString((byte[])in_value).Replace("-", " "));
        }
    }
}
