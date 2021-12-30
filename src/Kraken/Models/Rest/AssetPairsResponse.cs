using System.Collections.Generic;
using Newtonsoft.Json;

namespace CipherPark.CryptioTools.Kraken.Models
{
    public class AssetPair
    {
        public string AltName { get; set; }
        [JsonProperty(PropertyName = "aclass_base")]
        public string AClassBase { get; set; }
        public string Base { get; set; }
        [JsonProperty(PropertyName = "aclass_quote")]
        public string AClassQuote { get; set; }
        public string Quote { get; set; }
        public string Lot { get; set; }
        [JsonProperty(PropertyName = "pair_decimals")]
        public int PairDecimals { get; set; }
        [JsonProperty(PropertyName = "lot_decimals")]
        public int LotDecimals { get; set; }
        [JsonProperty(PropertyName = "lot_multiplier")]
        public int LotMultiplier { get; set; }
        [JsonProperty(PropertyName = "leverage_buy")]
        public double[] LeverageBuy { get; set; }
        [JsonProperty(PropertyName = "leverage_sell")]
        public double[] LeverageSell { get; set; }
        public double[][] Fees { get; set; }
        [JsonProperty(PropertyName = "fees_maker")]
        public double[][] FeesMaker { get; set; }
        [JsonProperty(PropertyName = "fee_volume_currency")]
        public string FeeVolumeCurrency { get; set; }
        [JsonProperty(PropertyName = "margin_call")]
        public double MarginCall { get; set; }
        [JsonProperty(PropertyName = "margin_stop")]
        public double MarginStop { get; set; }
    }

    public class AssetPairsResult : Dictionary<string, AssetPair> { }

    public class AssetPairsResponse : Response<AssetPairsResult> { }

}
