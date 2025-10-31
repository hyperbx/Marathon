using Newtonsoft.Json;
using System;

namespace Marathon.Helpers.Converters
{
    public class ByteArrayToHexStringConverter : JsonConverter<byte[]>
    {
        public override byte[] ReadJson(JsonReader in_reader, Type in_type, byte[] in_existingValue, bool in_hasExistingValue, JsonSerializer in_serialiser)
        {
            if (in_reader.TokenType == JsonToken.String)
            {
                var hexStr = in_serialiser.Deserialize<string>(in_reader);

                if (!string.IsNullOrEmpty(hexStr))
                    return BinaryHelper.TransformHexStringToByteArray(hexStr);
            }

            return [];
        }

        public override void WriteJson(JsonWriter in_writer, byte[] in_value, JsonSerializer in_serialiser)
        {
            in_serialiser.Serialize(in_writer, BitConverter.ToString(in_value).Replace("-", " "));
        }
    }
}
