using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using Xunit;
using FluentAssertions;
using CipherPark.ExchangeTools.CoinbasePro.Api;
using CipherPark.ExchangeTools.CoinbasePro.Common;
using CipherPark.ExchangeTools.CoinbasePro.Models;
using CipherPark.ExchangeTools.Utility.Credentials;

namespace CipherPark.ExchangeTools.CoinbasePro.IntegrationTests
{
    public class CoinbaseApiTest
    {
        private const string CoinbaseRestEndpoint = "https://api-public.sandbox.pro.coinbase.com";       

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
            products.IsUniqueOn(x => x.Id);
        }

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
            var credentials = manager.GetCredentials();
           
            var api = new CoinbaseApi(CoinbaseRestEndpoint,
                                   credentials.ApiKey,
                                   credentials.ApiSecret,
                                   credentials.ApiPassPhrase);
            
            return api;
        }
    }   
}
