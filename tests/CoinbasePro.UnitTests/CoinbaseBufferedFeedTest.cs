using Microsoft.Extensions.Configuration;
using FluentAssertions;
using Xunit;
using CipherPark.CryptioTools.CoinbasePro.Api;
using CipherPark.CryptioTools.Utility.Credentials;

namespace CoinbasePro.UnitTests
{
    public class CoinbaseBufferedFeedTest
    {
        [Fact]
        public void WhenNoChannelsProvided_FeedHasDefaultChannels()
        {
            //Arrange
            const string endPoint = "https://dummy-api.com";
            var credentials = GetCredentials();

            //Act
            CoinbaseBufferedFeed sut = new CoinbaseBufferedFeed(endPoint, credentials.ApiKey, credentials.ApiSecret, credentials.ApiPassPhrase);
            
            //Assert
            sut.InternalFeed.Channels.Should().BeEquivalentTo(CoinbaseFeed.DefaultChannels);
        }

        /// <summary>
        /// Creates a new instance of credentials stored in environment variables.
        /// </summary>
        /// <returns></returns>
        private static ExchangeCredentials GetCredentials()
        {
            ConfigurationBuilder builder = new ConfigurationBuilder();
            builder.AddEnvironmentVariables();
            var config = builder.Build();

            ExchangeCredentialsManager manager = new ExchangeCredentialsManager(config);
            var credentials = manager.GetCredentials(ExchangeCredentialsStore.CoinbaseProSandbox);

            return credentials;
        }
    }
}
