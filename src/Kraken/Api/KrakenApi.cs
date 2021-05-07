using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using Newtonsoft.Json;
using CipherPark.ExchangeTools.Kraken.Models;
using CipherPark.ExchangeTools.Utility;

namespace CipherPark.ExchangeTools.Kraken.Api
{
    public class KrakenApi
    {
        private const string DefaultVersion = "0";
        private static class ApiRoute
        {
            public const string Time = "Time";
            public const string Assets = "Assets";
            public const string AssetPairs = "AssetPairs";
            public const string Ticker = "Ticker";
            public const string OHLC = "OHLC";
            public const string OrderBook = "Depth";
            public const string RecentTrades = "Trades";
            public const string RecentSpreadData = "Spread";
            public const string AddOrder = "AddOrder";
            public const string CancelOrder = "CancelOrder";
            public const string TradesHistory = "TradesHistory";
            public const string ClosedOrders = "ClosedOrders";
            public const string OpenOrders = "OpenOrders";
            public const string QueryOrders = "QueryOrders";
            public const string QueryTrades = "QueryTrades";
            public const string OpenPositions = "OpenPositions";
            public const string Ledgers = "Ledgers";
            public const string QueryLedgers = "QueryLedgers";
            public const string Volume = "TradeVolume";
            public const string AddExport = "AddExport";
            public const string ExportStatus = "ExportStatus";
            public const string RetrieveExport = "RetrieveExport";
            public const string RemoveExport = "RemoveExport";
            public const string CancelAll = "CancelAll";
            public const string CancelAllOrdersAfter = "CancelAllOrdersAfter";
            public const string DepositMethods = "DepositMethods";
            public const string DepositAdddresses = "DepositAddresses";
            public const string DepositStatus = "DepositStatus";
            public const string WithdrawInfo = "WithdrawInfo";
            public const string Withdraw = "Withdraw";
            public const string WithdrawStatus = "WithdrawStatus";
            public const string WithdrawCancel = "WithdrawCancel";
            public const string WalletTransfer = "WalletTransfer";
            public const string GetWebSocketsToken = "GetWebSocketsToken";
        }
        
        public string ApiKey { get; }
        public string Secret { get; }
        public string ApiDomain { get; }
        public string ApiVersion { get; }

        public KrakenApi(string endPoint, string key, string secret, string version = DefaultVersion, WebProxy proxy = null)
        {
            ApiDomain = endPoint;
            ApiKey = key;
            Secret = secret;
            ApiVersion = version;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public TimeResponse GetTime()
        {
            var jsonResult = SendRequest(null, ApiRoute.Time);
            return JsonConvert.DeserializeObject<TimeResponse>(jsonResult);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public AssetsResponse GetAssets(AssetsRequest request)
        {
            var jsonResult = SendRequest(UrlQueryStringSerializer.SerializeObject(request), ApiRoute.Assets);
            return JsonConvert.DeserializeObject<AssetsResponse>(jsonResult);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public AssetPairsResponse GetAssetPairs(AssetPairsRequest request)
        {
            var jsonResult = SendRequest(UrlQueryStringSerializer.SerializeObject(request), ApiRoute.AssetPairs);
            return JsonConvert.DeserializeObject<AssetPairsResponse>(jsonResult);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public TickerResponse GetTickerInfo(TickerRequest request)
        {
            var jsonResult = SendRequest(UrlQueryStringSerializer.SerializeObject(request), ApiRoute.Ticker);
            return JsonConvert.DeserializeObject<TickerResponse>(jsonResult);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public OHLCResponse GetOHLC(OHLCRequest request)
        {
            var jsonResult = SendRequest(UrlQueryStringSerializer.SerializeObject(request), ApiRoute.OHLC);
            return JsonConvert.DeserializeObject<OHLCResponse>(jsonResult);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public OrderBookResponse GetOrderBook(OrderBookRequest request)
        {
            var jsonResult = SendRequest(UrlQueryStringSerializer.SerializeObject(request), ApiRoute.OrderBook);
            return JsonConvert.DeserializeObject<OrderBookResponse>(jsonResult);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public RecentTradesResponse GetRecentTrades(RecentTradesRequest request)
        {
            var jsonResult = SendRequest(UrlQueryStringSerializer.SerializeObject(request), ApiRoute.RecentTrades);
            return JsonConvert.DeserializeObject<RecentTradesResponse>(jsonResult);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public RecentSpreadDataResponse GetRecentSpreadData(RecentSpreadDataRequest request)
        {
            var jsonResult = SendRequest(UrlQueryStringSerializer.SerializeObject(request), ApiRoute.RecentSpreadData);
            return JsonConvert.DeserializeObject<RecentSpreadDataResponse>(jsonResult);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public StandardOrderResponse AddStandardOrder(StandardOrderRequest request)
        {
            var jsonResult = SendPrivateRequest(UrlQueryStringSerializer.SerializeObject(request), ApiRoute.AddOrder);
            return JsonConvert.DeserializeObject<StandardOrderResponse>(jsonResult);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public CancelOrderResponse CancelOrder(CancelOrderRequest request)
        {
            var jsonResult = SendPrivateRequest(UrlQueryStringSerializer.SerializeObject(request), ApiRoute.CancelOrder);
            return JsonConvert.DeserializeObject<CancelOrderResponse>(jsonResult);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public TradesHistoryResponse GetTradesHistory(TradesHistoryRequest request)
        {
            var jsonResult = SendPrivateRequest(UrlQueryStringSerializer.SerializeObject(request), ApiRoute.TradesHistory);
            return JsonConvert.DeserializeObject<TradesHistoryResponse>(jsonResult);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public WebSocketsTokenResponse GetWebSocketsToken()
        {
            var jsonResult = SendPrivateRequest(null, ApiRoute.GetWebSocketsToken);
            return JsonConvert.DeserializeObject<WebSocketsTokenResponse>(jsonResult);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="nameValues"></param>
        /// <param name="endPoint"></param>
        /// <returns></returns>
        private string SendPrivateRequest(string nameValues, string endPoint)
        {
            return SendRequest(nameValues, endPoint, true, HttpMethodNames.POST);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="nameValues"></param>
        /// <param name="apiRoute"></param>
        /// <param name="isPrivateEndPoint"></param>
        /// <param name="method"></param>
        /// <returns></returns>
        private string SendRequest(string nameValues, string apiRoute, bool isPrivateEndPoint = false, string method = HttpMethodNames.GET)
        {
            string responseData = null;
            string url;
            string apiAccess = isPrivateEndPoint ? "private" : "public";
            string endPoint = $"{ApiDomain}/{ApiVersion}/{apiAccess}/{apiRoute}";

            if (isPrivateEndPoint && method != HttpMethodNames.POST)
                throw new ArgumentException("Private endpoint invoked with http method other than POST", nameof(method));

            if (isPrivateEndPoint && (ApiKey == null || Secret == null))
                throw new InvalidOperationException("Private endpoint invoked with no API Key and/or Secret specified");

            url = string.IsNullOrEmpty(nameValues) && method == HttpMethodNames.GET ? endPoint + "?" + nameValues : endPoint;

            HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(url);
            webRequest.Method = method;
            webRequest.Accept = "application/json";
            if (method == HttpMethodNames.POST)
            {
                string body = null;
                webRequest.ContentType = "application/x-www-form-urlencoded";
                if (isPrivateEndPoint)
                {
                    long nonce = DateTime.UtcNow.ToUnixMilliseconds();
                    body = "nonce=" + nonce;
                    if (nameValues != null)
                    {
                        body += "&" + nameValues;
                    }
                    string signature = CreateSignature(Secret, nonce, body, $"/{ApiVersion}/{apiAccess}/{apiRoute}");
                    webRequest.Headers.Add("API-Key", ApiKey);
                    webRequest.Headers.Add("API-Sign", signature);
                }
                else
                    body = nameValues;

                if (body != null)
                {
                    using (Stream requestStream = webRequest.GetRequestStream())
                    {
                        requestStream.Write(System.Text.Encoding.ASCII.GetBytes(body), 0, body.Length);
                    }
                }
            }
            HttpWebResponse webResponse = (HttpWebResponse)webRequest.GetResponse();
            using (Stream responseStream = webResponse.GetResponseStream())
            {
                using (StreamReader reader = new StreamReader(responseStream, Encoding.Default))
                {
                    responseData = reader.ReadToEnd();
                }
            }

            return responseData;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="secret"></param>
        /// <param name="nonce"></param>
        /// <param name="postData"></param>
        /// <param name="requestPath"></param>
        /// <returns></returns>
        private static string CreateSignature(string secret, long nonce, string postData, string requestPath)
        {
            byte[] decodedSecret = Convert.FromBase64String(secret);
            var np = nonce + Convert.ToChar(0) + postData;
            var path = requestPath;
            var pathBytes = Encoding.UTF8.GetBytes(path);
            var ncBytes = new SHA256Managed().ComputeHash(Encoding.UTF8.GetBytes(np));
            var phBytes = new byte[pathBytes.Count() + ncBytes.Count()];
            pathBytes.CopyTo(phBytes, 0);
            ncBytes.CopyTo(phBytes, pathBytes.Count());
            var signature = Convert.ToBase64String(new HMACSHA512(decodedSecret).ComputeHash(phBytes));
            return signature;
        }
    }
}

