using CipherPark.ExchangeTools.Utility;

namespace CipherPark.ExchangeTools.Kraken.Models
{
    public class AssetPairsRequest
    {
        [UrlParameter("info")]
        public string Info { get; set; }
        [UrlParameter("pair")]
        public string Pair { get; set; }
    }
}
