using Newtonsoft.Json;

namespace CipherPark.ExchangeTools.CoinbasePro.Models
{
    public class StatsResult
    {
        [JsonProperty("open")]
        public double Open { get; set; }
        [JsonProperty("high")]
        public double High { get; set; }
        [JsonProperty("low")]
        public double Low { get; set; }
        [JsonProperty("volume")]
        public double Volume { get; set; }
        [JsonProperty("last")]
        public double Last { get; set; }
        [JsonProperty("volume_30day")]
        public double Volume30Day { get; set; }
    }
}
