using CryptoArbitrageLibrary.Model.Kraken.Deserialization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace CryptoArbitrageLibrary.Repository.Kraken
{
    public class KrakenSymbolJsonConverter : JsonConverter<Dictionary<string, KrakenSymbolDetail>>
    {

        public override Dictionary<string, KrakenSymbolDetail> Read(
            ref Utf8JsonReader reader,
            Type typeToConvert,
            JsonSerializerOptions options)
        {
            var propertyNameString = string.Empty;

            var dictionary = new Dictionary<string, KrakenSymbolDetail>();

            if (reader.TokenType != JsonTokenType.StartObject)
            {
                throw new JsonException("Expected StartObject token");
            }

            while (reader.Read())
            {
                if (reader.TokenType == JsonTokenType.EndObject)
                {
                    return dictionary;
                }

                if (reader.TokenType != JsonTokenType.PropertyName)
                {
                    throw new JsonException("Expected PropertyName token");
                }

                var propertyName = reader.GetString();

                
                reader.Read();
                var symbolDetail = JsonSerializer.Deserialize<KrakenSymbolDetail>(ref reader, options);

               
                if (symbolDetail != null)
                {
                    dictionary[propertyName?? string.Empty] = symbolDetail;
                }
            }

            throw new JsonException("Unexpected end of JSON");
        }



        public override void Write(
            Utf8JsonWriter writer,
            Dictionary<string, KrakenSymbolDetail> value,
            JsonSerializerOptions options)
        {
            throw new NotImplementedException("Serialization not implemented.");
        }


    }
}
