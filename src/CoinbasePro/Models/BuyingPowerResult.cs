using Newtonsoft.Json;

namespace CipherPark.ExchangeTools.CoinbasePro.Models
{
    public class BuyingPowerResult
    {
        [JsonProperty("buying_power")]
        public double BuyingPower { get; set; }

        [JsonProperty("selling_power")]
        public double SellingPower { get; set; }

        [JsonProperty("buying_power_explanation")]
        public string BuyingPowerExplanation { get; set; }
    }
}

