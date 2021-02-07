using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
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
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<Account[]> GetAllAccountsAsync()
        {
            string requestPath = "accounts";
            var result = await SendRequestAsync(null, requestPath, HttpMethodNames.GET);
            return JsonConvert.DeserializeObject<Account[]>(result.Content);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public Account[] GetAllAccounts()
        {
            return GetAllAccountsAsync().GetAwaiter().GetResult();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="accountId"></param>
        /// <returns></returns>
        public async Task<Account> GetAccountAsync(string accountId)
        {
            string requestPath = $"accounts/{accountId}";
            var result = await SendRequestAsync(null, requestPath, HttpMethodNames.GET);
            return JsonConvert.DeserializeObject<Account>(result.Content);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="accountId"></param>
        /// <returns></returns>
        public Account GetAccount(string accountId)
        {
            return GetAccountAsync(accountId).GetAwaiter().GetResult();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="accountId"></param>
        /// <param name="before"></param>
        /// <param name="after"></param>
        /// <returns></returns>
        public async Task<AccountHistoryPage> GetAccountHistoryAsync(string accountId, string before = null, string after = null)
        {
            string requestPath = UrlQueryStringAppender.Append($"accounts/{accountId}/ledger", new { before, after });
            var result = await SendRequestAsync(null, requestPath, HttpMethodNames.GET);
            return new AccountHistoryPage
            {
                History = JsonConvert.DeserializeObject<AccountHistory[]>(result.Content),
                Before = result.Before,
                After = result.After
            };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="accountId"></param>
        /// <param name="before"></param>
        /// <param name="after"></param>
        /// <returns></returns>
        public AccountHistoryPage GetAccountHistory(string accountId, string before = null, string after = null)
        {
            return GetAccountHistoryAsync(accountId, before, after).GetAwaiter().GetResult();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="accountId"></param>
        /// <param name="before"></param>
        /// <param name="after"></param>
        /// <returns></returns>
        public async Task<HoldsPage> GetHoldsAsync(string accountId, string before = null, string after = null)
        {
            string requestPath = UrlQueryStringAppender.Append($"accounts/{accountId}/holds", new { before, after });
            var result = await SendRequestAsync(null, requestPath, HttpMethodNames.GET);
            return new HoldsPage
            {
                Holds = JsonConvert.DeserializeObject<Hold[]>(result.Content),
                Before = result.Before,
                After = result.After
            };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="accountId"></param>
        /// <param name="before"></param>
        /// <param name="after"></param>
        /// <returns></returns>
        public HoldsPage GetHolds(string accountId, string before = null, string after = null)
        {
            return GetHoldsAsync(accountId, before, after).GetAwaiter().GetResult();
        }

        #endregion      

        #region Orders

        public async Task<OrderResult> PlaceOrderAsync(PlaceOrderParams order)
        {
            string requestPath = "orders";
            string content = JsonConvert.SerializeObject(order);
            var result = await SendRequestAsync(content, requestPath, HttpMethodNames.POST);
            var orderResult = JsonConvert.DeserializeObject<OrderResult>(result.Content);
            return orderResult;
        }

        public  OrderResult PlaceOrder(PlaceOrderParams order)
        {
            return PlaceOrderAsync(order).GetAwaiter().GetResult();
        }

        public async Task<string> CancelOrderAsync(string orderId, bool isClientId = false)
        {
            string requestPath = isClientId ? $"orders/client:{orderId}" : $"orders/{orderId}";
            var result = await SendRequestAsync(null, requestPath, HttpMethodNames.DELETE);
            return result.Content;
        }

        public string CancelOrder(string orderId, bool isClientId = false)
        {
            return CancelOrderAsync(orderId, isClientId).GetAwaiter().GetResult();
        }

        public async Task<string[]> CancelAllAsync()
        {
            string requestPath = "orders";
            var result = await SendRequestAsync(null, requestPath, HttpMethodNames.DELETE);
            return JsonConvert.DeserializeObject<string[]>(result.Content);
        }

        public string[] CancelAll()
        {
            return CancelAllAsync().GetAwaiter().GetResult();
        }

        public async Task <OrderResult[]> ListOrdersAsync(ListOrderParams filter)
        {
            string requestPath = UrlQueryStringAppender.Append("orders", filter);
            var result = await SendRequestAsync(null, requestPath, HttpMethodNames.GET);
            return JsonConvert.DeserializeObject<OrderResult[]>(result.Content);        
        }

        public OrderResult[] ListOrders(ListOrderParams filter)
        {
            return ListOrdersAsync(filter).GetAwaiter().GetResult();
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

        public async Task<ProductResult[]> GetProductsAsync()
        {
            string requestPath = "products";
            var result = await SendRequestAsync(null, requestPath, HttpMethodNames.GET);
            return JsonConvert.DeserializeObject<ProductResult[]>(result.Content);
        }

        public ProductResult[] GetProducts()
        {
            return GetProductsAsync().GetAwaiter().GetResult();
        }

        public async Task<SingleProductResult> GetSingleProductAsync(string productId)
        {
            string requestPath = $"products/{productId}";
            var result = await SendRequestAsync(null, requestPath, HttpMethodNames.GET);
            return JsonConvert.DeserializeObject<SingleProductResult>(result.Content);
        }

        public SingleProductResult GetSingleProduct(string productId)
        {
            return GetSingleProductAsync(productId).GetAwaiter().GetResult();
        }

        public async Task<TickerResult> GetProductTickerAsync(string productId)
        {
            string requestPath = $"products/{productId}/ticker";
            var result = await SendRequestAsync(null, requestPath, HttpMethodNames.GET);
            return JsonConvert.DeserializeObject<TickerResult>(result.Content);
        }

        public TickerResult GetProductTicker(string productId)
        {
            return GetProductTickerAsync(productId).GetAwaiter().GetResult();
        }

        public async Task<HistoricRate[]> GetHistoricRatesAsync(string productId, HistoricRateParams hrp)
        {
            string requestPath = UrlQueryStringAppender.Append($"products/{productId}/candles", hrp);                        
            var result = await SendRequestAsync(null, requestPath, HttpMethodNames.GET);
            var tempResults = JsonConvert.DeserializeObject<object[][]>(result.Content);
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

        public HistoricRate[] GetHistoricRates(string productId, HistoricRateParams hrp)
        {
            return GetHistoricRatesAsync(productId, hrp).GetAwaiter().GetResult();
        }

        public async Task<OrderBookResult> GetProductOrderBookAsync(string productId, int? level = null)
        {
            string requestPath = $"products/{productId}/book";
            UrlQueryStringAppender.Append(requestPath, new { level });
            var result = await SendRequestAsync(null, requestPath, HttpMethodNames.GET);
            return JsonConvert.DeserializeObject<OrderBookResult>(result.Content);
        }

        public OrderBookResult GetProductOrderBook(string productId, int? level = null)
        {
            return GetProductOrderBookAsync(productId, level).GetAwaiter().GetResult();
        }

        public async Task<TradesPage> GetTradesAsync(string productId, string before = null, string after = null)
        {
            string requestPath = UrlQueryStringAppender.Append($"products/{productId}/trades", new { before, after });
            var result = await SendRequestAsync(null, requestPath, HttpMethodNames.GET);
            return new TradesPage
            {
                Trades = JsonConvert.DeserializeObject<Trade[]>(result.Content),
                Before = result.Before,
                After = result.After
            };
        }

        public TradesPage GetTrades(string productId, string before = null, string after = null)
        {
            return GetTradesAsync(productId, before, after).GetAwaiter().GetResult();
        }

        public async Task<StatsResult> Get24HourStatsAsync(string productId)
        {
            string requestPath = $"/products/{productId}/stats";
            var result = await SendRequestAsync(null, requestPath, HttpMethodNames.GET);
            return JsonConvert.DeserializeObject<StatsResult>(result.Content);
        }

        public StatsResult Get24HourStats(string productId)
        {
            return Get24HourStatsAsync(productId).GetAwaiter().GetResult();
        }

        #endregion

        #region Connectivity             

        private async Task<AsyncRequestResult> SendRequestAsync(string content, string requestPath, string method)
        {
            string responseData = null;
            string endPoint = EndPoint;
            string url = $"{endPoint}/{requestPath}";
            string timeStamp = DateTime.UtcNow.ToUnixSeconds().ToString();
            string signature = CoinbaseRequestSignature.Generate(timeStamp, "/" + requestPath, content, Secret, method);
            string before = null;
            string after = null;

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
                    using Stream requestStream = await webRequest.GetRequestStreamAsync();
                    requestStream.Write(System.Text.Encoding.ASCII.GetBytes(content), 0, content.Length);
                }

                HttpWebResponse webResponse = (HttpWebResponse) await webRequest.GetResponseAsync();
                using (Stream responseStream = webResponse.GetResponseStream())
                {
                    using StreamReader reader = new StreamReader(responseStream, Encoding.Default);
                    responseData = await reader.ReadToEndAsync();
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

            return new AsyncRequestResult(responseData, before, after);
        }

        private string SendRequest(string content, string requestPath, string method, out string before, out string after)
        {
            var result = SendRequestAsync(content, requestPath, method).GetAwaiter().GetResult();
            before = result.Before;
            after = result.After;
            return result.Content;
        }

        private string SendRequest(string content, string requestPath, string method = HttpMethodNames.POST)
        {
            return SendRequest(content, requestPath, method, out string _, out string _);
        }

        #endregion
    }
}
