using CipherPark.CryptioTools.Utility;

namespace CipherPark.CryptioTools.Kraken.Models
{
    public class TickerRequest
    {
        [UrlParameter("pair")]
        public string Pair { get; set; }
    }
}
