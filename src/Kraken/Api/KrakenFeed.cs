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

        public async Task OpenAsync(string[] products)
        {
            string wsUrl = EndPoint;
            string request = null;
            await OpenAsync(wsUrl, request, Proxy);            
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
}
