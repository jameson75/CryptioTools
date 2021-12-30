using CipherPark.CryptioTools.Utility;

namespace CipherPark.CryptioTools.Kraken.Models
{
    public class OHLCRequest
    {
        [UrlParameter("pair")]
        public string Pair { get; set; }
        [UrlParameter("interval")]
        public int? Interval { get; set; }
        [UrlParameter("since")]
        public long? Since { get; set; }
    }
}
