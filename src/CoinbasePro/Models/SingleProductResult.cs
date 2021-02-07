using System;
using Newtonsoft.Json;

namespace CipherPark.ExchangeTools.CoinbasePro.Models
{
    public class SingleProductResult
    {
        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("id")]
        public string Id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("display_name")]
        public string DisplayName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("base_currency")]
        public string BaseCurrency { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("quote_currency")]
        public string QuoteCurrency { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("base_increment")]
        public double BaseIncrement { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("quote_increment")]
        public double Quote_Increment { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("base_min_size")]
        public double BaseMinSize { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("base_max_size")]
        public double BaseMaxSize { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("min_market_funds")]
        public double MinMarketFunds { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("max_market_funds")]
        public double MaxMarketFunds { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("status")]
        public string Online { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("status_message")]
        public string StatusMessage { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("cancel_only")]
        public bool CancelOnly { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("limit_only")]
        public bool LimitOnly { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("post_only")]
        public bool PostOnly { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("trading_disabled")]
        public bool TradingDisabled { get; set; }
    }
}