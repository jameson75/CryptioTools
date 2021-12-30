using Newtonsoft.Json;

namespace CipherPark.CryptioTools.CoinbasePro.Models
{
    public class ProductResult
    {
        public string Id { get; set; }
        [JsonProperty("base_currency")]
        public string BaseCurrency { get; set; }
        [JsonProperty("quote_currency")]
        public string QuoteCurrency { get; set; }
        [JsonProperty("base_min_size")]
        public double BaseMinSize { get; set; }
        [JsonProperty("base_max_size")]
        public double BaseMaxSize { get; set; }
        [JsonProperty("quote_increment")]
        public double QuoteIncrement { get; set; }
    }
}