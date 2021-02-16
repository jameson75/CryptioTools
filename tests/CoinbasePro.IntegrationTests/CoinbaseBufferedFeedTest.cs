using System.Linq;
using System.Threading;
using Microsoft.Extensions.Configuration;
using Xunit;
using FluentAssertions;
using CipherPark.ExchangeTools.CoinbasePro.Api;
using CipherPark.ExchangeTools.CoinbasePro.Common;
using CipherPark.ExchangeTools.CoinbasePro.Models;
using CipherPark.ExchangeTools.Utility.Credentials;

namespace CipherPark.ExchangeTools.CoinbasePro.IntegrationTests
{
    public class CoinbaseBufferedFeedTest
    {
        private const string CoinbaseWebsocketEndpoint = "wss://ws-feed-public.sandbox.pro.coinbase.com";

        [Fact]       
        public void WhenFeedOpenedOnProduct_ThenHeartbeatReceived()
        {
            //Arrange            
            CoinbaseBufferedFeed sut = CreateFeed();
            string expectedProductId = "BTC-USD";
            ManualResetEvent messageReceivedEvent = new ManualResetEvent(false);
            int timeoutLength = 300; //ms;
            string messageProductId = null;

            //Act
            sut.MessageReceived += (s, m) =>
            {
                if (messageProductId == null && m.Type == WSChannelNames.Ticker)
                {
                    messageProductId = m.ProductId;
                    messageReceivedEvent.Set();
                }
            };

            sut.OpenAsync(new[] { expectedProductId }).GetAwaiter().GetResult();
            bool messageReceivedEventSet = messageReceivedEvent.WaitOne(timeoutLength);

            //Assert           
            messageReceivedEventSet.Should().BeTrue();
            messageProductId.Should().Be(expectedProductId);
        }

        [Theory]
        [InlineData(WSMessageTypes.Ticker)]
        [InlineData(WSMessageTypes.Level2Snapshot)]
        public void WhenFeedOpenedOnProduct_ThenChannelMessageForProductReceived(string messageType)
        {
            //Arrange            
            CoinbaseBufferedFeed sut = CreateFeed();
            string expectedProductId = "BTC-USD";
            ManualResetEvent messageReceivedEvent = new ManualResetEvent(false);
            int timeoutLength = 300; //ms;
            string messageProductId = null;

            //Act
            sut.MessageReceived += (s, m) =>
            {
                if (messageProductId == null && m.Type == messageType)
                {
                    messageProductId = m.ProductId;
                    messageReceivedEvent.Set();
                }
            };

            sut.OpenAsync(new[] { expectedProductId }).GetAwaiter().GetResult();
            bool messageReceivedEventSet = messageReceivedEvent.WaitOne(timeoutLength);

            //Assert           
            messageReceivedEventSet.Should().BeTrue();
            messageProductId.Should().Be(expectedProductId);
        }       

        /// <summary>
        /// Creates a new instance of the CoinbaseApi class initialized with credentials stored in environment variables.
        /// </summary>
        /// <returns></returns>
        private static CoinbaseBufferedFeed CreateFeed(WSChannel[] channels = null)
        {
            ConfigurationBuilder builder = new ConfigurationBuilder();
            builder.AddEnvironmentVariables();
            var config = builder.Build();

            ExchangeCredentialsManager manager = new ExchangeCredentialsManager(config);
            var credentials = manager.GetCredentials();

            var feed = new CoinbaseBufferedFeed(CoinbaseWebsocketEndpoint,
                                   credentials.ApiKey,
                                   credentials.ApiSecret,
                                   credentials.ApiPassPhrase,
                                   channels);

            return feed;
        }
    }
}
