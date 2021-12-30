using Newtonsoft.Json;

namespace CipherPark.CryptioTools.Kraken.Models.Websockets
{
    public class WSUnsubscribeRequest : WSSubscribeRequest
    {
        [JsonProperty("channelID")]
        public string ChannelId { get; set; }
    }
}
