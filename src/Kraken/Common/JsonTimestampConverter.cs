using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace CipherPark.CryptioTools.Utility
{
    public class JsonTimestampConverter : CustomCreationConverter<DateTime>
    {
        public override DateTime Create(Type objectType)
        {
            return new DateTime();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null)
            {
                if (IsNullable(objectType))
                {
                    throw new JsonSerializationException(string.Format("Cannot convert null value to {0}.", objectType));
                }

                return null;
            }

            if (reader.TokenType != JsonToken.Integer)
            {
                throw new JsonSerializationException(string.Format("Unexpected token parsing date. Expected Integer, got {0}.", reader.TokenType));
            }

            long seconds = (long)reader.Value;

            return UnixTimestampConverter.FromUnixSeconds(seconds).ToLocalTime();
        }

        private static bool IsNullable(Type type)
        {
            return (!type.IsValueType && type != typeof(Enum)) || (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>));
        }
    }
}
