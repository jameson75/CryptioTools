using System;
using Xunit;
using CipherPark.ExchangeTools.Kraken.Models;
using FluentAssertions;

namespace CipherPark.ExchangeTools.Kraken.IntegrationTests
{
    public class KrakenApiTest
    {
        [Fact]
        public void WhenTimeRequested_ThenResponseHasTime()
        {
            var api = KrakenFactory.CreateApi();

            var reponse = api.GetTime();

            reponse.Result.UnixTime.Should().BeAfter(DateTime.Now.AddMinutes(-1));
        }

        [Fact]
        public void WhenTradesHistory_ThenResponseHasTradeHistory()
        {
            var api = KrakenFactory.CreateApi();

            var reponse = api.GetTradesHistory(new TradesHistoryRequest
            {
                Type = "all"
            });

            reponse.Result.Should().NotBeNull();
        }

        [Fact]
        public void WhenGetWebSocketsToken_ThenResponseHasToken()
        {
            var api = KrakenFactory.CreateApi();

            var reponse = api.GetWebSocketsToken();

            reponse.Result.Should().NotBeNull();
        }        
    }
}