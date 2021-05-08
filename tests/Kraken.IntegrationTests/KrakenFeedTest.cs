using System;
using System.Threading;
using Xunit;
using FluentAssertions;
using CipherPark.ExchangeTools.Kraken.Api;

namespace CipherPark.ExchangeTools.Kraken.IntegrationTests
{
    public class KrakenFeedTest
    {
        private const int Timeout = 3000;

        [Fact]
        public void WhenPingSent_ThenPongReceived()
        {
            //Arrange    
            KrakenFeed sut = KrakenFactory.CreateFeed();
            ManualResetEvent messageReceivedEvent = new ManualResetEvent(false);               

            //Act
            sut.PongReceived += (s, m) =>
            {
                messageReceivedEvent.Set();
            };
            sut.PingAsync().GetAwaiter().GetResult();
            bool messageReceivedEventSet = messageReceivedEvent.WaitOne(Timeout);
            sut.Dispose();

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
           
            //Act
            sut.SubscriptionStatusReceived += (s, m) =>
            {
                messageReceivedEvent.Set();
            };
            sut.SubscribeAsync(channelName).GetAwaiter().GetResult();
            bool messageReceivedEventSet = messageReceivedEvent.WaitOne(Timeout);
            sut.Dispose();

            //Assert           
            messageReceivedEventSet.Should().BeTrue();           
        }

        [Fact]
        public void WhenTickerSubscribedTo_ThenTickerMessageReceived()
        {
            //Arrange                
            KrakenFeed sut = KrakenFactory.CreateFeed();
            ManualResetEvent messageReceivedEvent = new ManualResetEvent(false);                      

            //Act
            sut.TickerReceived += (s, m) =>
            {
                messageReceivedEvent.Set();
            };
            sut.SubscribeAsync("ticker", new[] { "BTC/USD" }).GetAwaiter().GetResult();
            bool messageReceivedEventSet = messageReceivedEvent.WaitOne(Timeout);
            sut.Dispose();

            //Assert           
            messageReceivedEventSet.Should().BeTrue();
        }
    }
}
