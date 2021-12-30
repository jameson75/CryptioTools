using Newtonsoft.Json;

namespace CipherPark.CryptioTools.CoinbasePro.Models
{
    public class WSRequest
    {
        [JsonProperty("type")]
        public string Type { get; set; }
        [JsonProperty(PropertyName = "product_ids", NullValueHandling = NullValueHandling.Ignore)]
        public string[] ProductIds { get; set; }
        [JsonProperty(PropertyName = "channels", NullValueHandling = NullValueHandling.Ignore)]
        public WSChannel[] Channels { get; set; }
    }
}