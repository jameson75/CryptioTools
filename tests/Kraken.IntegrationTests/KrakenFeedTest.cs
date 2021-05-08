using System;
using System.Threading;
using Xunit;
using FluentAssertions;
using CipherPark.ExchangeTools.Kraken.Api;

namespace CipherPark.ExchangeTools.Kraken.IntegrationTests
{
    public class KrakenFeedTest : IDisposable
    {
        private KrakenFeed sut;

        public KrakenFeedTest()
        {
            sut = KrakenFactory.CreateFeed();           
        }

        public void Dispose()
        {
           sut?.Dispose();
        }

        [Fact]
        public void WhenPingSent_ThenPongReceived()
        {
            //Arrange                                  
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
            ManualResetEvent messageReceivedEvent = new ManualResetEvent(false);
            int timeoutLength = 3500; //ms;          

            //Act
            sut.SubscriptionStatusReceived += (s, m) =>
            {
                messageReceivedEvent.Set();
            };
            sut.SubscribeAsync(channelName).GetAwaiter().GetResult();
            bool messageReceivedEventSet = messageReceivedEvent.WaitOne(timeoutLength);
            //sut.Dispose();

            //Assert           
            messageReceivedEventSet.Should().BeTrue();           
        }
    }
}
