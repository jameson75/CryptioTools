using System;
using Newtonsoft.Json;
using CipherPark.CryptioTools.CoinbasePro.Common;

namespace CipherPark.CryptioTools.CoinbasePro.Models
{
    public class AccountHistory
    {
        public string Id { get; set; }
        [JsonProperty("created_at")]
        public DateTime CreateAt { get; set; }
        public double Amount { get; set; }
        public double Balance { get; set; }
        public EntryType Type { get; set; }
        public AccountHistoryDetails Details { get; set; }
    }
}