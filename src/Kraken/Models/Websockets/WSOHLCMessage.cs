using CipherPark.CryptioTools.Utility;
using System;
using System.Text.Json.Serialization;

namespace CipherPark.CryptioTools.Kraken.Models.Websockets
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

    public static class WSOHLCValueIndex
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

    public static class WSOHLCMessageExtensions
    {
        public static double Open(this WSOHLCMessage message)
        {
            return message.Values[WSOHLCValueIndex.Open];
        }

        public static double High(this WSOHLCMessage message)
        {
            return message.Values[WSOHLCValueIndex.High];
        }

        public static double Low(this WSOHLCMessage message)
        {
            return message.Values[WSOHLCValueIndex.Low];
        }

        public static double Close(this WSOHLCMessage message)
        {
            return message.Values[WSOHLCValueIndex.Close];
        }

        public static DateTime Time(this WSOHLCMessage message)
        {
            return UnixTimestampConverter.FromUnixSeconds((long)message.Values[WSOHLCValueIndex.Time]);
        }
    }
}
