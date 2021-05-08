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

    public static class OHLCValueIndex
    {
        public const int Time =     0;
        public const int ETime =    1;
        public const int Open =     2;
        public const int High =     3;
        public const int Low =      4;
        public const int Close =    5;
        public const int VWAP =     6;
        public const int Volume =   7;
        public const int Count =    8;
    }
}
