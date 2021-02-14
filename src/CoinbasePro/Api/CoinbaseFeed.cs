using System;
using System.Net.WebSockets;
using System.Threading;
using System.Threading.Tasks;
using System.Net;
using Newtonsoft.Json;
using CipherPark.ExchangeTools.CoinbasePro.Models;
using CipherPark.ExchangeTools.CoinbasePro.Common;
using CipherPark.ExchangeTools.Utility;

namespace CipherPark.ExchangeTools.CoinbasePro.Api
{
    public class CoinbaseFeed
    {
        public string EndPoint { get; }
        public string Key { get; }
        public string Secret { get; }
        public string Passphrase { get; }
        public WebProxy Proxy { get; }

        public CoinbaseFeed(string endPoint, string key, string secret, string passPhrase, WebProxy proxy = null)
        {
            Key = key;
            Secret = secret;
            Passphrase = passPhrase;
            EndPoint = endPoint;
            Proxy = proxy;
        }

        public async Task OpenAsync(string endPoint, string[] products)
        {
            string wsUrl = endPoint;

            WebSocket client = new ClientWebSocket();
            if (Proxy != null)
                ((ClientWebSocket)client).Options.Proxy = Proxy;           
            await ((ClientWebSocket)client).ConnectAsync(new Uri(wsUrl), CancellationToken.None);
            string request = CreateSubscriptionRequest(Key, Secret, Passphrase, products);
            var payLoad = new ArraySegment<byte>(System.Text.Encoding.UTF8.GetBytes(request));
            await client.SendAsync(payLoad, WebSocketMessageType.Text, true, CancellationToken.None);
            new Thread(async () =>
            {
                while (true)
                {
                    byte[] responseBuffer = Array.Empty<byte>();
                    while (true)
                    {
                        var chunkBuffer = new ArraySegment<byte>(new byte[10]);
                        WebSocketReceiveResult result = await client.ReceiveAsync(chunkBuffer, CancellationToken.None);
                        if (result.Count > 0)
                        {
                            Array.Resize(ref responseBuffer, responseBuffer.Length + result.Count);
                            Array.Copy(chunkBuffer.Array, 0, responseBuffer, responseBuffer.Length - result.Count, result.Count);
                        }
                        if (result.EndOfMessage)
                            break;
                    }
                    string response = System.Text.Encoding.UTF8.GetString(responseBuffer);
                    OnRawMessageReceived(response);
                    WSChannelMessage message = JsonConvert.DeserializeObject<WSChannelMessage>(response);
                    OnMessageReceived(message);
                }
            }).Start();
        }   

        protected void OnRawMessageReceived(string response)
        {
            this.RawMessageRecieved?.Invoke(this, response);
        }

        protected void OnMessageReceived(WSChannelMessage message)
        {
            this.MessageReceived?.Invoke(this, message);
        }

        public event Action<object, string> RawMessageRecieved;

        public event Action<object, WSChannelMessage> MessageReceived;

        private static string CreateSubscriptionRequest(string key, string secret, string passphrase, string[] products)
        {
            var channels = new[]
            {
                new WSChannel() { Name = WSChannelNames.User },
                new WSChannel() { Name = WSChannelNames.Heartbeat },
                new WSChannel() { Name = WSChannelNames.Level2 },
                new WSChannel() { Name = WSChannelNames.Ticker }
            };          

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
