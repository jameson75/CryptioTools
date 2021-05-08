using Newtonsoft.Json;

namespace CipherPark.ExchangeTools.Kraken.Models.Websockets
{
    public class WSTickerMessage
    {
        [JsonProperty("a")]
        public double[] Ask { get; set; }
        [JsonProperty("b")]
        public double[] Bid { get; set; }
        [JsonProperty("c")]
        public double[] Close { get; set; }
        [JsonProperty("h")]
        public double[] High { get; set; }
        [JsonProperty("l")]
        public double[] Low { get; set; }
        [JsonProperty("o")]
        public double[] Open { get; set; }
        [JsonProperty("v")]
        public double[] Volume { get; set; }
        [JsonProperty("p")]
        public double[] Vwap { get; set; }
        [JsonProperty("t")]
        public int[] TradeCount { get; set; }
        [JsonIgnore]
        public string ChannelId { get; set; }
        [JsonIgnore]
        public string Pair { get; set; }
        [JsonIgnore]
        public string ChannelName { get; set; }
    }
}
