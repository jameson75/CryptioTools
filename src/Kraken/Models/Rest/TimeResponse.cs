using System;
using Newtonsoft.Json;
using CipherPark.CryptioTools.Utility;

namespace CipherPark.CryptioTools.Kraken.Models
{
    public class TimeResult
    {
        [JsonConverter(typeof(JsonTimestampConverter))]
        public DateTime UnixTime { get; set; }
        public string RFC1123 { get; set; }
    }

    public class TimeResponse : Response<TimeResult> { }
}
