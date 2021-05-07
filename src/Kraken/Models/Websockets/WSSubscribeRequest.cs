using Newtonsoft.Json;

namespace CipherPark.ExchangeTools.Kraken.Models.Websockets
{
    public class WSSubscribeRequest : WSRequest
    {
        [JsonProperty("pair")]
        public string[] Pair { get; set; }
        [JsonProperty("subscription")]
        public Subscription Subscription { get; set; }
    }
}