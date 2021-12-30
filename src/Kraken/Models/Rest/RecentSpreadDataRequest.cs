using CipherPark.CryptioTools.Utility;
namespace CipherPark.CryptioTools.Kraken.Models
{

    public class RecentSpreadDataRequest
    {
        [UrlParameter("pair")]
        public string Pair { get; set; }
        [UrlParameter("since")]
        public long? Since { get; set; }
    }
}
