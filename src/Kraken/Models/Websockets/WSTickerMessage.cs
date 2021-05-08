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
        public int ChannelId { get; set; }
        [JsonIgnore]
        public string Pair { get; set; }
        [JsonIgnore]
        public string ChannelName { get; set; }
    }
    
    public static class TickerBidAskValueIndex
    {
        public const int BestPrice =        0;
        public const int WholeLotVolume =   1;
        public const int LotVolume =        2;
    }

    public static class TickerCloseValueIndex
    {
        public const int Price =        0;
        public const int LotVolume =    1;
    }

    public static class TickerOHLVWTIndex
    {
        public const int ValueToday =   0;
        public const int ValueLast24 =  1;
    }   
}
