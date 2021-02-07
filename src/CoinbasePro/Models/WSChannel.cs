using Newtonsoft.Json;

namespace CipherPark.ExchangeTools.CoinbasePro.Models
{
    public class WSChannel
    {
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty(PropertyName = "product_ids", NullValueHandling = NullValueHandling.Ignore)]
        public string[] ProductIds { get; set; }
    }
}