using System;
using Newtonsoft.Json;
using CipherPark.ExchangeTools.Utility;

namespace CipherPark.ExchangeTools.Kraken.Models
{
    public class TimeResult
    {
        [JsonConverter(typeof(JsonTimestampConverter))]
        public DateTime UnixTime { get; set; }
        public string RFC1123 { get; set; }
    }

    public class TimeResponse : Response<TimeResult> { }
}
