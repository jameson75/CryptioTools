using Newtonsoft.Json;

namespace CipherPark.ExchangeTools.CoinbasePro.Models
{
    public class AccountHistoryDetails
    {
        [JsonProperty("order_id")]
        public string OrderId { get; set; }
        [JsonProperty("trade_id")]
        public string TradeId { get; set; }
        [JsonProperty("product_id")]
        public string ProductId { get; set; }
    }
}