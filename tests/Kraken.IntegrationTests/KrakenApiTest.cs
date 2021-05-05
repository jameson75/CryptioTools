using System;
using Xunit;
using Microsoft.Extensions.Configuration;
using CipherPark.ExchangeTools.Kraken.Api;
using CipherPark.ExchangeTools.Kraken.Models;
using CipherPark.ExchangeTools.Utility.Credentials;
using FluentAssertions;

namespace Kraken.IntegrationTests
{
    public class KrakenApiTest
    {
        private const string KrakenRestEndpoint = "https://api.kraken.com";        

        [Fact]
        public void WhenTimeRequested_ThenResponseHasTime()
        {
            var api = CreateApi();

            var reponse = api.GetTime();

            reponse.Result.UnixTime.Should().BeAfter(DateTime.Now.AddMinutes(-1));
        }

        [Fact]
        public void WhenTradesHistory_ThenResponseHasTradeHistory()
        {
            var api = CreateApi();

            var reponse = api.GetTradesHistory(new TradesHistoryRequest
            {
                Type = "all"
            });

            reponse.Should().NotBeNull();
        }

        private static KrakenApi CreateApi()
        {
            ConfigurationBuilder builder = new ConfigurationBuilder();
            builder.AddEnvironmentVariables();
            var config = builder.Build();

            ExchangeCredentialsManager manager = new ExchangeCredentialsManager(config);
            var credentials = manager.GetCredentials(ExchangeCredentialsStore.Kraken);

            return new KrakenApi(KrakenRestEndpoint,
                                 credentials.ApiKey,
                                 credentials.ApiSecret);                                 
        }
    }
}
