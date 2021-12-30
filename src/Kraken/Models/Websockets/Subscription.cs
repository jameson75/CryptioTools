using Newtonsoft.Json;

namespace CipherPark.CryptioTools.Kraken.Models.Websockets
{
    public class Subscription
    {
        [JsonProperty("depth")]
        public int? Depth { get; set; }
        [JsonProperty("interval")]
        public int? Interval { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("ratecounter")]
        public bool? RateCounter { get; set; }
        [JsonProperty("snapshot")]
        public bool? Snapshot { get; set; }
        [JsonProperty("token")]
        public string Token { get; set; }
    }
}
