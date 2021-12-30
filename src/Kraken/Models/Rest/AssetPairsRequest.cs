using CipherPark.CryptioTools.Utility;

namespace CipherPark.CryptioTools.Kraken.Models
{
    public class AssetPairsRequest
    {
        [UrlParameter("info")]
        public string Info { get; set; }
        [UrlParameter("pair")]
        public string Pair { get; set; }
    }
}
