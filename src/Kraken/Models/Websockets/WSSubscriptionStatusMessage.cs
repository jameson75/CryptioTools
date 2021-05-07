using Newtonsoft.Json;

namespace CipherPark.ExchangeTools.Kraken.Models.Websockets
{
    public class WSSubscriptionStatusMessage
    {
        [JsonProperty("channelName")]
        public string ChannelName { get; set; }
        public string[] Pair { get; set; }
        public string Status { get; set; }
        public Subscription Subscription { get; set; }
        public OneOf OneOf { get; set; }
    }
}
