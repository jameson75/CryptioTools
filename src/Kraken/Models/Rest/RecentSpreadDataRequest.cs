using CipherPark.ExchangeTools.Utility;
namespace CipherPark.ExchangeTools.Kraken.Models
{

    public class RecentSpreadDataRequest
    {
        [UrlParameter("pair")]
        public string Pair { get; set; }
        [UrlParameter("since")]
        public long? Since { get; set; }
    }
}
