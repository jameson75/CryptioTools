using Newtonsoft.Json;

namespace CipherPark.CryptioTools.CoinbasePro.Models
{
    public class HoldsPage : ResultPage
    {
        public Hold[] Holds { get; set; }
    }
}