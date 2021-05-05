using CipherPark.ExchangeTools.Utility;

namespace CipherPark.ExchangeTools.Kraken.Models
{
    public class AssetsRequest
    {
        [UrlParameter("info")]
        public string Info { get; set; }
        [UrlParameter("aclass")]
        public string AClass { get; set; }
        [UrlParameter("asset")]
        public string Asset { get; set; }
    }
}
