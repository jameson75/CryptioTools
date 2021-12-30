using CipherPark.CryptioTools.Utility;

namespace CipherPark.CryptioTools.Kraken.Models
{
    public class OrderBookRequest
    {
        [UrlParameter("pair")]
        public string Pair { get; set; }
        [UrlParameter("count")]
        public int? Count { get; set; }
    }
}
