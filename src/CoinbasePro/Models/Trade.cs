using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace CipherPark.ExchangeTools.CoinbasePro.Models
{
    public class Trade
    {
        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("time")]
        public DateTime Time { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("trade_id")]
        public string TradeId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("price")]
        public double Price { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("size")]
        public double Size { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("side")]
        public string Side { get; set; }       
    }
}
