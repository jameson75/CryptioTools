using System;
using Newtonsoft.Json;

namespace CipherPark.CryptioTools.CoinbasePro.Models
{
    public class FillResult
    {
        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("trade_id")]
        public string TradeId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("product_id")]
        public string ProductId { get; set; }
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
        [JsonProperty("order_id")]
        public string OrderId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("created_at")]
        public DateTime CreateAt { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("liquidity")]
        public string Liquidity { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("fee")]
        public double Fee { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("settled")]
        public bool Settled { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("side")]
        public string Side { get; set; }
    }
}