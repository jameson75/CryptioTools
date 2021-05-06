using System.Collections.Generic;
using Newtonsoft.Json;

namespace CipherPark.ExchangeTools.Kraken.Models
{    
    public class Asset
    {
        public string AltName { get; set; }
        public string AClass { get; set; }
        public int Decimals { get; set; }
        [JsonProperty(PropertyName = "display_decimals")]
        public int DisplayDecimals { get; set; }
    }
    
    public class AssetsResult : Dictionary<string, Asset> { }

    public class AssetsResponse : Response<AssetsResult> { }
}
