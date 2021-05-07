using System;
using System.Threading.Tasks;
using System.Net;
using Newtonsoft.Json;
using CipherPark.ExchangeTools.CoinbasePro.Models;
using CipherPark.ExchangeTools.CoinbasePro.Common;
using CipherPark.ExchangeTools.Utility;

namespace CipherPark.ExchangeTools.CoinbasePro.Api
{
    public class CoinbaseFeed : ExchangeFeed
    {
        public string EndPoint { get; }
        public string Key { get; }
        public string Secret { get; }
        public string Passphrase { get; }
        public WebProxy Proxy { get; }
        public WSChannel[] Channels { get; }

        public static readonly WSChannel[] DefaultChannels = new[]
        {
                new WSChannel() { Name = WSChannelNames.User },
                new WSChannel() { Name = WSChannelNames.Heartbeat },
                new WSChannel() { Name = WSChannelNames.Level2 },
                new WSChannel() { Name = WSChannelNames.Ticker }
        };

        public CoinbaseFeed(string endPoint, string key, string secret, string passPhrase, WSChannel[] channels = null, WebProxy proxy = null)
        {
            Key = key;
            Secret = secret;
            Passphrase = passPhrase;
            EndPoint = endPoint;
            Proxy = proxy;
            Channels = channels ?? DefaultChannels;
        }

        public async Task OpenAsync(string[] products)
        {                     
            string request = CreateSubscriptionRequest(Key, Secret, Passphrase, products, Channels);           
            await OpenAsync(EndPoint, request, Proxy);            
        }

        protected override void OnRawMessageReceived(string response)
        {
            WSChannelMessage message = JsonConvert.DeserializeObject<WSChannelMessage>(response);
            OnMessageReceived(message);
        }

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
    }
}
