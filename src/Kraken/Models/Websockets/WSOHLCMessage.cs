using System.Text.Json.Serialization;

namespace CipherPark.ExchangeTools.Kraken.Models.Websockets
{
    public class WSOHLCMessage
    {
        [JsonIgnore]
        public int ChannelId { get; set; }
        [JsonIgnore]
        public double[] Values { get; set; }
        [JsonIgnore]
        public string ChannelName { get; set; }
        [JsonIgnore]
        public string Pair { get; set; }
    }
}
