using System.Linq;
using Microsoft.Extensions.Configuration;
using Xunit;
using FluentAssertions;
using CipherPark.CryptioTools.CoinbasePro.Api;
using CipherPark.CryptioTools.CoinbasePro.Common;
using CipherPark.CryptioTools.CoinbasePro.Models;
using CipherPark.CryptioTools.Utility.Credentials;

namespace CipherPark.CryptioTools.CoinbasePro.IntegrationTests
{
    public class CoinbaseApiTest
    {
        private const string CoinbaseRestEndpoint = "https://api-public.sandbox.pro.coinbase.com";

        #region Market Data Tests
        [Fact]
        public void WhenSingleProductRequested_ThenResponseHasProduct()
        {
            //Arrange            
            CoinbaseApi sut = CreateApi();
            string expectedBase = "BTC";
            string expectedQuote = "USD";
            string expectedId = $"{expectedBase}-{expectedQuote}";

            //Act
            var product = sut.GetSingleProduct(expectedId);

            //Assert           
            product.Id.Should().Be(expectedId);
            product.BaseCurrency.Should().Be(expectedBase);
            product.QuoteCurrency.Should().Be(expectedQuote);
        }

        [Fact]
        public void WhenAllProductsRequested_ThenResponseHasProducts()
        {
            //Arrange            
            CoinbaseApi sut = CreateApi();

            //Act
            var products = sut.GetProducts();

            //Assert
            products.Should().NotBeEmpty();
            products.IsUniqueOn(x => x.Id).Should().BeTrue();
        }

        [Fact]
        public void WhenFirstTradesPageRequested_ThenResponseHasTrades()
        {
            //Arrange
            CoinbaseApi sut = CreateApi();

            //Act
            var page = sut.GetTrades("BTC-USD");

            //Assert
            page.Trades.Should().NotBeNullOrEmpty();
            page.Trades.IsUniqueOn(x => x.TradeId).Should().BeTrue();
            page.Trades.All(x => x.Price > 0).Should().BeTrue();
            page.Trades.All(x => x.Size > 0).Should().BeTrue();
        }
        #endregion

        #region Account Tests
        [Fact]
        public void WhenAllAccountsRequested_ThenResponseHasAccounts()
        {
            //Arrange            
            CoinbaseApi sut = CreateApi();

            //Act
            var allAccounts = sut.GetAllAccounts();

            //Assert
            allAccounts.Should().NotBeEmpty();
            allAccounts.IsUniqueOn(x => x.Id).Should().BeTrue();           
        }

        [Fact]
        public void WhenAllHoldsRequested_ThenResponseHasHolds()
        {
            //Arrange            
            CoinbaseApi sut = CreateApi();

            //Act
            var id = sut.GetAllAccounts().First().Id;
            var holds = sut.GetHolds(id);

            //Assert            
            holds.Holds.IsUniqueOn(x => x.Id).Should().BeTrue();
        }
        #endregion

        #region Orders Tests
        [Fact]
        public void WhenOrderPlaced_ThenResponseHasOrderId()
        {
            //Arrange            
            CoinbaseApi sut = CreateApi();

            //Act
            var orderResult = sut.PlaceOrder(new PlaceOrderParams
            {
                Side = Sides.Buy,
                ProductId = "BTC-USD",
                Price = 10000,
                Size = 1               
            });

            //Assert            
            orderResult.Id.Should().NotBeNullOrEmpty();

            //Tear down
            TeardownOrders(sut, new[] { orderResult.Id });
        }

        /// <remarks>
        /// It's assumed that the sandbox environment contains at least one order.
        /// </remarks>
        [Fact]
        public void WhenOrdersQueried_ThenResponseHasOrders()
        {
            //Arrange            
            CoinbaseApi sut = CreateApi();

            //Act
            var orders = sut.ListOrders(new ListOrderParams
            {
               Status = OrderStatusFilter.all,
            });

            //Assert          
            orders.Should().NotBeEmpty();
        }
        #endregion

        #region Fills Tests
        /// <remarks>
        /// It's assumed that the sandbox environment contains at least one fill in BTC-USD
        /// </remarks>
        [Fact]
        public void WhenFillsQueried_ThenResponseHasFills()
        {
            //Arrange            
            CoinbaseApi sut = CreateApi();

            //Act
            var fills = sut.GetFills(new FillParams { ProductId = "BTC-USD" } );

            //Assert          
            fills.Should().NotBeEmpty();
        }
        #endregion

        #region Margin Tests
        /* Coinbase has disabled margin trading. These tests will fail with 404 - Route not found.
        [Fact]
        public void WhenMarginProfileQueried_ThenResponseHasProfile()
        {
            //Arrange            
            CoinbaseApi sut = CreateApi();
            const string productId = "BTC-USD";

            //Act
            var profile = sut.GetMarginProfile(productId);

            //Assert          
            profile.Should().NotBeNull();      
        }

        [Fact]
        public void WhenMarginBuyingPowerQueried_ThenResponseHasBuyingPower()
        {
            //Arrange            
            CoinbaseApi sut = CreateApi();
            const string productId = "BTC-USD";

            //Act            
            var power = sut.GetBuyingPower(productId);

            //Assert       
            power.Should().NotBeNull();
        }
        */
        #endregion

        /// <summary>
        /// Tears down temporary orders created during test.
        /// </summary>
        /// <param name="orders"></param>
        private static void TeardownOrders(CoinbaseApi api, string[] orders)
        {           
            foreach (var orderId in orders)
                api.CancelOrder(orderId);
        }
      
        /// <summary>
        /// Creates a new instance of the CoinbaseApi class initialized with credentials stored in environment variables.
        /// </summary>
        /// <returns></returns>
        private static CoinbaseApi CreateApi()
        {
            ConfigurationBuilder builder = new ConfigurationBuilder();
            builder.AddEnvironmentVariables();
            var config = builder.Build();
            
            ExchangeCredentialsManager manager = new ExchangeCredentialsManager(config);
            var credentials = manager.GetCredentials(ExchangeCredentialsStore.CoinbaseProSandbox);
           
            var api = new CoinbaseApi(CoinbaseRestEndpoint,
                                   credentials.ApiKey,
                                   credentials.ApiSecret,
                                   credentials.ApiPassPhrase);
            
            return api;
        }
    }   
}
