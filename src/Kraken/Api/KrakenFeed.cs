using System;
using System.Threading.Tasks;
using System.Net;
using Newtonsoft.Json;
using CipherPark.ExchangeTools.Utility;

namespace CipherPark.ExchangeTools.CoinbasePro.Api
{
    public class KrakenFeed : ExchangeFeed
    {
        public string EndPoint { get; }
        public string Token { get; }
        public WebProxy Proxy { get; }

        public KrakenFeed(string endPoint, string token, WebProxy proxy = null)
        {
            EndPoint = endPoint;
            Token = token;
            Proxy = proxy;
        }

        public async Task PingAsync(int? correlationId = null)
        {            
            WSRequest request = new WSRequest
            {
                Event = "ping",
                ReqId = correlationId,
            };
            var content = JsonConvert.SerializeObject(request);
            await SendContentAsync(content);
        }

        private async Task SendContentAsync(string content)
        {
            if (!IsOpen)
                await OpenAsync(EndPoint, content, Proxy);
            else
                await SendAsync(content);
        }

        protected override void OnRawMessageReceived(string response)
        {
            /*
            WSChannelMessage message = JsonConvert.DeserializeObject<WSChannelMessage>(response);
            OnMessageReceived(message);
            */
        }

        /*
        protected void OnMessageReceived(WSChannelMessage message)
        {
            this.MessageReceived?.Invoke(this, message);
        }     

        public event Action<object, WSChannelMessage> MessageReceived;

        private static string CreateSubscriptionRequest(string key, string secret, string passphrase, string[] products, WSChannel[] channels)
        {
            if (key != null && secret != null && passphrase != null)
            {
                string timestamp = DateTime.UtcNow.ToUnixSeconds().ToString();
                string requestPath = "/users/self/verify";
                return JsonConvert.SerializeObject(
                    new WSAuthenticatedRequest
                    {
                        Type = WSRequestTypes.Subscribe,
                        ProductIds = products,
                        Channels = channels,
                        Key = key,
                        Passphrase = passphrase,
                        Timestamp = timestamp,
                        Signature = CoinbaseRequestSignature.Generate(timestamp, requestPath, null, secret, HttpMethodNames.GET)
                    });
            }
            else
            {
                return JsonConvert.SerializeObject(
                    new WSRequest
                    {
                        Type = WSRequestTypes.Subscribe,
                        ProductIds = products,
                        Channels = channels,
                    });
            }
        }
        */
    }

    public class WSMessage
    {
        public string Event { get; set; }
        public int? ReqId { get; set; }
    }    

    public class WSRequest : WSMessage  {  }

    public class WSSystemStatusMessage : WSMessage
    {
        [JsonProperty("connectionID")]
        public int ConnectionID { get; set; }
        public string Status { get; set; }
        public string Version { get; set; }
    }

    public class WSSubscribeRequest : WSRequest
    {   
        public string[] Pair { get; set; }
        public Subscription Subscription { get; set; }       
    }

    public class Subscription
    {
        public int Depth { get; set; }
        public int Interval { get; set; }
        public string Name { get; set; }
        public bool RateCounter { get; set; }
        public bool Snapshot { get; set; }
        public string Token { get; set; }        
    }

    public class WSUnsubscribeRequest : WSSubscribeRequest
    {
        [JsonProperty("channelID")]
        public string ChannelId { get; set; }
    }

    public class WSSubscriptionStatusMessage
    {
        [JsonProperty("channelName")]
        public string ChannelName { get; set; }
        public string[] Pair { get; set; }
        public string Status { get; set; }
        public Subscription Subscription { get; set; }
        public OneOf OneOf { get; set; }
    }

    public class OneOf
    {
        [JsonProperty("errorMessage")]
        public string ErrorMessage { get; set; }
        [JsonProperty("channelID")]
        public string ChannelId { get; set; }
    }

    public class WSTickerMessage
    {
        [JsonProperty("channelID")]
        public string ChannelId { get; set; }
    }
}
