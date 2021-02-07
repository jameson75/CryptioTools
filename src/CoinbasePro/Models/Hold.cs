using System;
using Newtonsoft.Json;
using CipherPark.ExchangeTools.CoinbasePro.Common;

namespace CipherPark.ExchangeTools.CoinbasePro.Models
{
    public class Hold
    {
        public string Id { get; set; }
        [JsonProperty("account_id")]
        public string AccountId { get; set; }
        [JsonProperty("created_at")]
        public DateTime CreatedAt { get; set; }
        [JsonProperty("updated_at")]
        public DateTime UpdatedAt { get; set; }
        public double Amount { get; set; }
        public HoldType Type { get; set; }
        public string Ref { get; set; }
    }
}