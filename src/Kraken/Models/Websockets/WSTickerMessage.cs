using Newtonsoft.Json;

namespace CipherPark.ExchangeTools.Kraken.Models.Websockets
{
    public class WSTickerMessage
    {
        [JsonProperty("channelID")]
        public string ChannelId { get; set; }
    }
}
