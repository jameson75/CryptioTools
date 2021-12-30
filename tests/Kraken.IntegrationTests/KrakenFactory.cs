using Microsoft.Extensions.Configuration;
using CipherPark.CryptioTools.Kraken.Api;
using CipherPark.CryptioTools.Utility.Credentials;
using FluentAssertions;

namespace CipherPark.CryptioTools.Kraken.IntegrationTests
{
    public class KrakenFactory
    {
        private const string RestProductionEndPoint = "https://api.kraken.com";
        private const string WebSocketsProductionEndPoint = "wss://ws.kraken.com";
        private const string WebSocketsAuthProductionEndPoint = "wss://ws-auth.kraken.com";

        /// <summary>
        /// Creates a new instance of the CoinbaseApi class initialized with credentials stored in environment variables.
        /// </summary>
        /// <returns></returns>
        public static KrakenFeed CreateFeed()
        {
            KrakenApi api = CreateApi();
            var feed = new KrakenFeed(WebSocketsProductionEndPoint, null, null);
            return feed;
        }

        /// <summary>
        /// Creates a new instance of the CoinbaseApi class initialized with credentials stored in environment variables.
        /// </summary>
        /// <returns></returns>
        public static KrakenFeed CreateAuthenticatedFeed()
        {
            KrakenApi api = CreateApi();
            var tokenResponse = api.GetWebSocketsToken();
            tokenResponse.Result.Should().NotBeNull();
            var token = tokenResponse.Result.Token;
            var feed = new KrakenFeed(WebSocketsAuthProductionEndPoint, token, null);
            return feed;
        }

        public static KrakenApi CreateApi()
        {
            ConfigurationBuilder builder = new ConfigurationBuilder();
            builder.AddEnvironmentVariables();
            var config = builder.Build();

            ExchangeCredentialsManager manager = new ExchangeCredentialsManager(config);
            var credentials = manager.GetCredentials(ExchangeCredentialsStore.Kraken);

            return new KrakenApi(RestProductionEndPoint,
                                 credentials.ApiKey,
                                 credentials.ApiSecret);
        }
    }
}
