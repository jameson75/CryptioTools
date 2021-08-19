using System;
using System.Collections.Generic;
using System.Linq;
using CipherPark.ExchangeTools.Utility;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace CipherPark.ExchangeTools.Kraken.Models
{
    public class OHLCResult
    {
        [JsonExtensionData(ReadData = true)]
        private Dictionary<string, JToken> _raw = null;
        public long Last { get; set; }
        public Dictionary<string, object[][]> Data
        {
            get { return _raw.Take(1).ToDictionary(k => k.Key, v => ((JToken)v.Value).ToObject<object[][]>()); }
        }
    }
    
    public class OHLCResponse : Response<OHLCResult> { }

    public static class OHLCValueIndex
    {
        public const int Time = 0;       
        public const int Open = 1;
        public const int High = 2;
        public const int Low = 3;
        public const int Close = 4;
        public const int VWAP = 5;
        public const int Volume = 6;
        public const int Count = 7;
    }

    public static class OHLCResponseExtensions
    {
        public static DateTime Time(this OHLCResult result, int index)
        {
            return UnixTimestampConverter.FromUnixSeconds((long)result.Data.First().Value[index][OHLCValueIndex.Time]);
        }

        public static double Open(this OHLCResult result, int index)
        {
            return Convert.ToDouble(result.Data.First().Value[index][OHLCValueIndex.Open]);
        }

        public static double High(this OHLCResult result, int index)
        {
            return Convert.ToDouble(result.Data.First().Value[index][OHLCValueIndex.High]);
        }

        public static double Low(this OHLCResult result, int index)
        {
            return Convert.ToDouble(result.Data.First().Value[index][OHLCValueIndex.Low]);
        }

        public static double Close(this OHLCResult result, int index)
        {
            return Convert.ToDouble(result.Data.First().Value[index][OHLCValueIndex.Close]);
        }

        public static int Count(this OHLCResult result)
        {
            return result.Data.First().Value.Length;
        }
    }

    public static class OHLCInterval
    {
        public const int OnMinute = 1;
        public const int FiveMinutes = 5;
        public const int FifteenMinutes = 15;
        public const int HalfHour = 30;
        public const int OneHour = 60;
        public const int FourHours = 240;
        public const int OneDay = 1440;
        public const int SevenDays = 10080;
        public const int FifteenDays = 21600;
    }
}
