using Newtonsoft.Json;

namespace CipherPark.ExchangeTools.CoinbasePro.Models
{
    public class HoldsPage : ResultPage
    {
        public Hold[] Holds { get; set; }
    }
}