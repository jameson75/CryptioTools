using Newtonsoft.Json;

namespace CipherPark.ExchangeTools.Kraken.Models.Websockets
{
    public class WSSubscriptionStatusMessage : WSMessage
    {        
        public string ChannelName { get; set; }
        public string Pair { get; set; }
        public string Status { get; set; }
        public Subscription Subscription { get; set; }
        public string ChannelId { get; set; }
        public string ErrorMessage { get; set; }
    }
}
