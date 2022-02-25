namespace Marathon.Helpers.Converters
{
    public class ByteArrayConverter : JsonConverter
    {
		public override bool CanConvert(Type objectType) => objectType == typeof(byte[]);

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			if (reader.TokenType == JsonToken.String)
			{
				string hex = serializer.Deserialize<string>(reader);

				if (!string.IsNullOrEmpty(hex))
					return BinaryHelper.StringToByteArray(hex);
			}

			return Array.Empty<byte>();
        }

		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
			=> serializer.Serialize(writer, BitConverter.ToString((byte[])value).Replace("-", " "));
    }
}
