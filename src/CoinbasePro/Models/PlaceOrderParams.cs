using Newtonsoft.Json;
using CipherPark.ExchangeTools.CoinbasePro.Common;

namespace CipherPark.ExchangeTools.CoinbasePro.Models
{
    public class PlaceOrderParams
    {
        /// <summary>
        /// Amount of coin/product to buy or sell
        /// </summary>
        [JsonProperty("size")]
        public double? Size { get; set; }
        /// <summary>
        /// Price per coin/product (required only for limit orders).
        /// </summary>
        [JsonProperty("price")]
        public double? Price { get; set; }
        /// <summary>
        /// [required] buy or sell
        /// </summary>
        [JsonProperty("side")]
        public string Side { get; set; }
        /// <summary>
        /// [required] A valid product id (ie: BTC-USD)
        /// </summary>
        [JsonProperty("product_id")]
        public string ProductId { get; set; }
        /// <summary>
        /// [optional] limit or market (default is limit)
        /// </summary>
        [JsonProperty("type")]
        public string Type { get; set; }
        /// <summary>
        /// [optional] Order ID selected by you to identify your order
        /// </summary>
        [JsonProperty("client_oid")]
        public string ClientOid { get; set; }
        /// <summary>
        /// [optional]* Either loss or entry. 
        /// *Requires stop_price to be defined
        /// </summary>
        [JsonProperty("stop")]
        public string Stop { get; set; }
        /// <summary>
        /// [optional] Only if stop is defined. Sets trigger price for stop order.
        /// </summary>
        [JsonProperty("stop_price")]
        public double? StopPrice { get; set; }
        /// <summary>
        /// [optional]* Desired amount of quote currency to use. 
        /// *Required only for limit orders. 
        /// *One of size or funds is required.
        /// </summary>
        [JsonProperty("funds")]
        public double? Funds { get; set; }
        /// <summary>
        /// [optional]* Post only flag.
        /// *Required only for limit orders.
        /// *Invalid when time_in_force is IOC or FOK.
        /// </summary>
        [JsonProperty("post_only")]
        public bool? PostOnly { get; set; }
        /// <summary>
        /// [optional]* Self-trade prevention flag         
        /// </summary>
        [JsonProperty("stp")]
        public string SelfTradePrevention { get; set; }
        /// <summary>
        /// [optional]* GTC, GTT, IOC, or FOK (default is GTC)
        /// *Applies only to limit orders.
        /// </summary>
        [JsonProperty("time_in_force")]
        public string TimeInForce { get; set; }
        /// <summary>
        /// [optional]* min, hour, day
        /// * Requires time_in_force to be GTT
        /// </summary>
        [JsonProperty("cancel_after")]
        public string CancelAfter { get; set; }
    }
}