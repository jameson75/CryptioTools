using Xunit;
using System.Threading;
using CipherPark.ExchangeTools.Kraken.Api;
using FluentAssertions;

namespace CipherPark.ExchangeTools.Kraken.IntegrationTests
{
    public class KrakenFeedTest
    {
        [Fact]
        public void WhenPingSent_ThenPongReceived()
        {
            //Arrange                        
            KrakenFeed sut = KrakenFactory.CreateFeed();
            ManualResetEvent messageReceivedEvent = new ManualResetEvent(false);
            int timeoutLength = 3000; //ms;          

            //Act
            sut.PongReceived += (s, m) =>
            {
                messageReceivedEvent.Set();
            };
            sut.PingAsync().GetAwaiter().GetResult();
            bool messageReceivedEventSet = messageReceivedEvent.WaitOne(timeoutLength);

            //Assert           
            messageReceivedEventSet.Should().BeTrue();
        }

        [Theory]
        [InlineData("book")]
        [InlineData("ohlc")]
        [InlineData("openOrders")]
        [InlineData("ownTrades")]
        [InlineData("spread")]
        [InlineData("ticker")]
        [InlineData("trade")]
        public void WhenSubscriptionSent_ThenSubscriptionStatusReceived(string channelName)
        {
            //Arrange                        
            KrakenFeed sut = KrakenFactory.CreateAuthenticatedFeed();
            ManualResetEvent messageReceivedEvent = new ManualResetEvent(false);
            int timeoutLength = 3000; //ms;          

            //Act
            sut.SubscriptionStatusReceived += (s, m) =>
            {
                messageReceivedEvent.Set();
            };
            sut.SubscribeAsync(channelName).GetAwaiter().GetResult();
            bool messageReceivedEventSet = messageReceivedEvent.WaitOne(timeoutLength);

            //Assert           
            messageReceivedEventSet.Should().BeTrue();
        }
    }
}
