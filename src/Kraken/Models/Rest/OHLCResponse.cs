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
        public static DateTime Time(this OHLCResponse response, int index)
        {
            return UnixTimestampConverter.FromUnixSeconds((long)response.Result.Data.First().Value[index][OHLCValueIndex.Time]);
        }

        public static double Open(this OHLCResponse response, int index)
        {
            return Convert.ToDouble(response.Result.Data.First().Value[index][OHLCValueIndex.Open]);
        }

        public static double High(this OHLCResponse response, int index)
        {
            return Convert.ToDouble(response.Result.Data.First().Value[index][OHLCValueIndex.High]);
        }

        public static double Low(this OHLCResponse response, int index)
        {
            return Convert.ToDouble(response.Result.Data.First().Value[index][OHLCValueIndex.Low]);
        }

        public static double Close(this OHLCResponse response, int index)
        {
            return Convert.ToDouble(response.Result.Data.First().Value[index][OHLCValueIndex.Close]);
        }
    }        
}
