using System;
using Newtonsoft.Json;

namespace CipherPark.ExchangeTools.CoinbasePro.Models
{
    public class TickerResult
    {
        [JsonProperty("trade_id")]
        public long TradeId { get; set; }
        public double Price { get; set; }
        public double Size { get; set; }
        public double Bid { get; set; }
        public double Ask { get; set; }
        public double Volume { get; set; }
        public DateTime Time { get; set; }
    }
}