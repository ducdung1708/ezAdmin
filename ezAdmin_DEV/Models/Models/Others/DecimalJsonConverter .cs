using Newtonsoft.Json;
using System.Globalization;

namespace Models.Models.Others
{
    public class DecimalJsonConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(decimal);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null)
            {
                return null;
            }
            else
            {
                return Convert.ToDecimal(reader.Value, CultureInfo.InvariantCulture);
            }
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            writer.WriteRawValue(((decimal)value).ToString("G29", CultureInfo.InvariantCulture));
        }
    }
}
