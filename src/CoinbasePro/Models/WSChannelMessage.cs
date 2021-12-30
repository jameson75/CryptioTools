using System;
using Newtonsoft.Json;

namespace CipherPark.CryptioTools.CoinbasePro.Models
{
    public class WSChannelMessage
    {
        public string Type { get; set; }
        [JsonProperty("trade_id")]
        public long? TradeId { get; set; }
        public long Sequence { get; set; }
        [JsonProperty("product_id")]
        public string ProductId { get; set; }
        public DateTime? Time { get; set; }
        public double? Price { get; set; }
        public string Side { get; set; }
        [JsonProperty("last_size")]
        public double? LastSize { get; set; }
        [JsonProperty("best_bid")]
        public double? BestBid { get; set; }
        [JsonProperty("best_ask")]
        public double BestAsk { get; set; }
        public double[][] Bids { get; set; }
        public double[][] Asks { get; set; }
        public object[][] Changes { get; set; }
        [JsonProperty("order_id")]
        public string OrderId { get; set; }
        public double? Funds { get; set; }
        [JsonProperty("order_type")]
        public string OrderType { get; set; }
        [JsonProperty("remaining_size")]
        public double? RemainingSize { get; set; }
        public string Reason { get; set; }
        [JsonProperty("open_24h")]
        public double? Open24Hours { get; set; }
        [JsonProperty("high_24h")]
        public double? High24Hours { get; set; }
        [JsonProperty("low_24h")]
        public double? Low24Hours { get; set; }
        [JsonProperty("volume_24h")]
        public double? Volume24Hours { get; set; }
        [JsonProperty("taker_user_id")]
        public string TakerUserId { get; set; }
        [JsonProperty("user_id")]
        public string UserId { get; set; }
        [JsonProperty("taker_profile_id")]
        public string TakerProfileId { get; set; }
        [JsonProperty("profile_id")]
        public string ProfileId { get; set; }
    }
}