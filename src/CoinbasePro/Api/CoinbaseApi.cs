using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Net;
using Newtonsoft.Json;
using CipherPark.ExchangeTools.CoinbasePro.Models;
using CipherPark.ExchangeTools.Utility;

namespace CipherPark.ExchangeTools.CoinbasePro.Api
{
    public class CoinbaseApi
    {
        public string Key { get; }
        public string Secret { get; }
        public string Passphrase { get; }
        public string EndPoint { get; }
        public WebProxy Proxy { get; }

        public CoinbaseApi(string endPoint, string key, string secret, string passPhrase, WebProxy proxy = null)
        {
            Key = key;
            Secret = secret;
            Passphrase = passPhrase;
            EndPoint = endPoint;
            Proxy = proxy;
        }

        #region Accounts

        public Account[] GetAllAccounts()
        {
            string requestPath = "accounts";
            var jsonResult = SendRequest(null, requestPath, HttpMethodNames.GET);
            return JsonConvert.DeserializeObject<Account[]>(jsonResult);
        }

        public Account GetAccount(string accountId)
        {
            string requestPath = $"accounts/{accountId}";
            var jsonResult = SendRequest(null, requestPath, HttpMethodNames.GET);
            return JsonConvert.DeserializeObject<Account>(jsonResult);
        }

        public AccountHistoryPage GetAccountHistory(string accountId, string before, string after)
        {
            string requestPath = $"accounts/{accountId}/ledger";
            var jsonResult = SendRequest(null, requestPath, HttpMethodNames.GET, out string _before, out string _after);
            return new AccountHistoryPage
            {
                History = JsonConvert.DeserializeObject<AccountHistory[]>(jsonResult),
                Before = _before,
                After = _after
            };
        }

        public HoldsPage GetHolds(string accountId, string before = null, string after = null)
        {
            string requestPath = UrlQueryStringAppender.Append($"accounts/{accountId}/holds", new { before, after });
            var jsonResult = SendRequest(null, requestPath, HttpMethodNames.GET, out string _before, out string _after);
            return new HoldsPage
            {
                Holds = JsonConvert.DeserializeObject<Hold[]>(jsonResult),
                Before = _before,
                After = _after
            };
        }

        #endregion      

        #region Orders

        public OrderResult PlaceOrder(PlaceOrderParams order)
        {
            string requestPath = "orders";
            string content = JsonConvert.SerializeObject(order);
            var jsonResult = SendRequest(content, requestPath, HttpMethodNames.POST);
            var orderResult = JsonConvert.DeserializeObject<OrderResult>(jsonResult);
            return orderResult;
        }

        public string CancelOrder(string orderId)
        {
            string requestPath = $"orders/{orderId}";            
            var jsonResult = SendRequest(null, requestPath, HttpMethodNames.DELETE);
            return jsonResult;
        }

        public string[] CancelAll()
        {
            string requestPath = "orders";
            var jsonResult = SendRequest(null, requestPath, HttpMethodNames.DELETE);
            return JsonConvert.DeserializeObject<string[]>(jsonResult);
        }

        public OrderResult[] ListOrders(ListOrderParams filter)
        {
            string requestPath = UrlQueryStringAppender.Append("orders", filter);            
            var jsonResult = SendRequest(null, requestPath, HttpMethodNames.GET);
            return JsonConvert.DeserializeObject<OrderResult[]>(jsonResult);
        }

        public string CancelOrder (string orderId, bool isClientId = false)
        {
            string requestPath = isClientId ? $"orders/client:{orderId}" : $"orders/{orderId}";
            var jsonResult = SendRequest(null, requestPath, HttpMethodNames.DELETE);
            return jsonResult;
        }

        #endregion

        #region Fills
        public FillResult[] GetFills(FillParams filter)
        {
            string requestPath = UrlQueryStringAppender.Append("fills", filter);           
            var jsonResult = SendRequest(null, requestPath, HttpMethodNames.GET);
            return JsonConvert.DeserializeObject<FillResult[]>(jsonResult);
        }
        #endregion

        #region Limits       
        public string GetLimits()
        {
            string requestPath = "/users/self/exchange-limits";            
            var jsonResult = SendRequest(null, requestPath, HttpMethodNames.GET);
            return jsonResult;
        }
        #endregion

        #region Deposits
        #endregion

        #region Withdrawls
        #endregion

        #region Stablecoin Conversions
        #endregion

        #region Payment Methods
        #endregion

        #region Coinbase Accounts
        #endregion

        #region Fees
        #endregion

        #region Reports
        #endregion

        #region Profiles
        #endregion

        #region UserAccount
        #endregion

        #region Margin
        public string GetMarginProfile()
        {
            string requestPath = "/margin/profile_information";
            var jsonResult = SendRequest(null, requestPath, HttpMethodNames.GET);
            return jsonResult;
        }
        #endregion

        #region Oracle
        #endregion

        #region Market Data

        public ProductResult[] GetProducts()
        {
            string requestPath = "products";
            var jsonResult = SendRequest(null, requestPath, HttpMethodNames.GET);
            return JsonConvert.DeserializeObject<ProductResult[]>(jsonResult);
        }

        public SingleProductResult GetSingleProduct(string productId)
        {
            string requestPath = $"products/{productId}";
            var jsonResult = SendRequest(null, requestPath, HttpMethodNames.GET);
            return JsonConvert.DeserializeObject<SingleProductResult>(jsonResult);
        }

        public TickerResult GetProductTicker(string productId)
        {
            string requestPath = $"products/{productId}/ticker";
            var jsonResult = SendRequest(null, requestPath, HttpMethodNames.GET);
            return JsonConvert.DeserializeObject<TickerResult>(jsonResult);
        }

        public HistoricRate[] GetHistoricRates(string productId, HistoricRateParams hrp)
        {
            string requestPath = UrlQueryStringAppender.Append($"products/{productId}/candles", hrp);                        
            var jsonResult = SendRequest(null, requestPath, HttpMethodNames.GET);
            var tempResults = JsonConvert.DeserializeObject<object[][]>(jsonResult);
            return tempResults.Select(x => new HistoricRate
            {
                Time = UnixTimestampConverter.FromUnixSeconds(Convert.ToInt64(x[0])),
                Low = Convert.ToDouble(x[1]),
                High = Convert.ToDouble(x[2]),
                Open = Convert.ToDouble(x[3]),
                Close = Convert.ToDouble(x[4]),
                Volume = Convert.ToDouble(x[5])
            }).ToArray();
        }

        public OrderBookResult GetProductOrderBook(string productId, int? level = null)
        {
            string requestPath = $"products/{productId}/book";
            UrlQueryStringAppender.Append(requestPath, new { level });
            var jsonResult = SendRequest(null, requestPath, HttpMethodNames.GET);
            return JsonConvert.DeserializeObject<OrderBookResult>(jsonResult);
        }

        public TradesPage GetTrades(string productId, string before = null, string after = null)
        {
            string requestPath = UrlQueryStringAppender.Append($"products/{productId}/trades", new { before, after });
            var jsonResult = SendRequest(null, requestPath, HttpMethodNames.GET, out string _before, out string _after);
            return new TradesPage
            {
                Trades = JsonConvert.DeserializeObject<Trade[]>(jsonResult),
                Before = _before,
                After = _after
            };
        }

        public StatsResult Get24HourStats(string productId)
        {
            string requestPath = $"/products/{productId}/stats";
            var jsonResult = SendRequest(null, requestPath, HttpMethodNames.GET);
            return JsonConvert.DeserializeObject<StatsResult>(jsonResult);
        }

        #endregion

        #region Connectivity
        private string SendRequest(string content, string requestPath, string method = HttpMethodNames.POST)
        {
            return SendRequest(content, requestPath, method, out string _, out string _);
        }

        private string SendRequest(string content, string requestPath, string method, out string before, out string after)
        {
            string responseData = null;
            string endPoint = EndPoint;
            string url = $"{endPoint}/{requestPath}";
            string timeStamp = DateTime.UtcNow.ToUnixSeconds().ToString();
            string signature = CoinbaseRequestSignature.Generate(timeStamp, "/" + requestPath, content, Secret, method);

            try
            {
                HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(url);
                webRequest.UserAgent = "Cipher Park GDAX Api Client";
                webRequest.ContentType = "application/json";
                webRequest.Method = method;
                webRequest.Accept = "application/json";
                webRequest.Headers.Add("CB-ACCESS-KEY", Key);
                webRequest.Headers.Add("CB-ACCESS-SIGN", signature);
                webRequest.Headers.Add("CB-ACCESS-TIMESTAMP", timeStamp);
                webRequest.Headers.Add("CB-ACCESS-PASSPHRASE", Passphrase);

                if (Proxy != null)
                    webRequest.Proxy = Proxy;

                if (content != null)
                {
                    using Stream requestStream = webRequest.GetRequestStream();
                    requestStream.Write(System.Text.Encoding.ASCII.GetBytes(content), 0, content.Length);
                }

                HttpWebResponse webResponse = (HttpWebResponse)webRequest.GetResponse();
                using (Stream responseStream = webResponse.GetResponseStream())
                {
                    using StreamReader reader = new StreamReader(responseStream, Encoding.Default);
                    responseData = reader.ReadToEnd();
                }

                before = webResponse.Headers.Get("CB-BEFORE");
                after = webResponse.Headers.Get("CB-AFTER");
            }

            catch (WebException webEx)
            {
                if (webEx.Response != null)
                {
                    using Stream responseStream = webEx.Response.GetResponseStream();
                    using StreamReader reader = new StreamReader(responseStream, Encoding.Default);
                    responseData = reader.ReadToEnd();
                }
                throw new CoinbaseApiException(responseData, webEx);
            }

            return responseData;
        }
        #endregion
    }
}
