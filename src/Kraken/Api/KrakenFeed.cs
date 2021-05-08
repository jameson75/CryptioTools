using System;
using System.Threading.Tasks;
using System.Net;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using CipherPark.ExchangeTools.Utility;
using CipherPark.ExchangeTools.Kraken.Models.Websockets;
using System.Linq;

namespace CipherPark.ExchangeTools.Kraken.Api
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
            await SendRequestAsync(request);
        }

        public async Task SubscribeAsync(string name, 
                                         string[] pairs = null, 
                                         int? depth = null,
                                         int? interval = null, 
                                         bool? rateCounter = null, 
                                         bool? snapShot = null,  
                                         int? correlationId = null)
        {
            WSSubscribeRequest request = new WSSubscribeRequest
            {
                Event = "subscribe",
                ReqId = correlationId,
                Pair = pairs,
                Subscription = new Subscription
                {
                    Depth = depth,
                    Interval = interval,
                    Name = name,
                    RateCounter = rateCounter,
                    Snapshot = snapShot,
                    Token = Token,
                }
            };
            await SendRequestAsync(request);
        }

        public async Task UnsubscribeAsync(string channelId, string[] pairs = null,  int? correlationId = null)
        {
            WSUnsubscribeRequest request = new WSUnsubscribeRequest
            {
                Event = "unsubscribe",
                ReqId = correlationId,
                ChannelId = channelId,
                Pair = pairs
            };
            await SendRequestAsync(request);
        }

        public async Task AddOrderAsync()
        {
            await Task.CompletedTask;
            throw new NotImplementedException();
        }

        public async Task CancelOrderAsync()
        {
            await Task.CompletedTask;
            throw new NotImplementedException();
        }

        public async Task CancelAllAsync()
        {
            await Task.CompletedTask;
            throw new NotImplementedException();
        }

        public async Task CancelAllOrdersAfter()
        {
            await Task.CompletedTask;
            throw new NotImplementedException();
        }

        private async Task SendRequestAsync(WSRequest request)
        {
            string content = JsonConvert.SerializeObject(request, Formatting.None, new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
            });

            if (!IsOpen)
                await OpenAsync(EndPoint, content, Proxy);
            else
                await SendAsync(content);
        }       

        protected override void OnRawMessageReceived(string response)
        {
            string @event;
            if (response[0] == '[')
            {
                var jObjects = JArray.Parse(response).ToArray();
                if (response[1] == '[')
                    @event = jObjects[1].ToObject<string>();
                else
                    @event = jObjects[2].ToObject<string>();
                switch(@event)
                {             
                    case "ticker":
                        var tickerMessage = jObjects[1].ToObject<WSTickerMessage>();
                        tickerMessage.ChannelId = jObjects[0].ToObject<string>();
                        tickerMessage.ChannelName = @event;
                        tickerMessage.Pair = jObjects[3].ToObject<string>();
                        OnTicker(tickerMessage);
                        break;
                    case "ohlc":
                        break;
                    case "trade":
                        break;
                    case "spread":
                        break;
                    case "book":
                        break;
                    case "ownTrades":
                        break;
                    case "openOrders":
                        break;
                }
            }
            else
            {
                JObject reponseObj = JObject.Parse(response);
                @event = reponseObj.Value<string>("event");
                switch (@event)
                {
                    case "pong":
                        var pongMessage = JsonConvert.DeserializeObject<WSMessage>(response);
                        OnPong(pongMessage);
                        break;
                    case "heartbeat":
                        var heartbeatMessage = JsonConvert.DeserializeObject<WSMessage>(response);
                        OnHeartbeat(heartbeatMessage);
                        break;
                    case "systemStatus":
                        var systemStatusMessage = JsonConvert.DeserializeObject<WSSystemStatusMessage>(response);
                        OnSystemStatus(systemStatusMessage);
                        break;
                    case "subscriptionStatus":
                        var subscriptionStatusMessage = JsonConvert.DeserializeObject<WSSubscriptionStatusMessage>(response);
                        OnSubscriptionStatus(subscriptionStatusMessage);
                        break;                               
                    case "addOrderStatus":
                        break;
                    case "cancelOrderStatus":
                        break;
                    case "cancelAllStatus":
                        break;
                    case "cancelAllOrdersAfterStatus":
                        break;
                }
            }
        }

        private void OnPong(WSMessage message)
        {
            PongReceived?.Invoke(this, message);
        }

        private void OnHeartbeat(WSMessage message)
        {
            HeartbeatReceived?.Invoke(this, message);
        }

        private void OnSystemStatus(WSSystemStatusMessage message)
        {
            SystemStatusReceived?.Invoke(this, message);
        }

        private void OnSubscriptionStatus(WSSubscriptionStatusMessage message)
        {
            SubscriptionStatusReceived?.Invoke(this, message);
        }

        private void OnTicker(WSTickerMessage message)
        {
            TickerReceived?.Invoke(this, message);
        }

        public event Action<object, WSMessage> PongReceived;

        public event Action<object, WSMessage> HeartbeatReceived;

        public event Action<object, WSSystemStatusMessage> SystemStatusReceived;

        public event Action<object, WSTickerMessage> TickerReceived;

        public event Action<object, WSSubscriptionStatusMessage> SubscriptionStatusReceived;
    }   
}
