using System;
using System.Collections.Generic;
using System.Text;
using FluentAssertions;
using Xunit;
using CipherPark.ExchangeTools.Utility;

namespace Utility.UnitTests
{   
    public class UrlQueryStringSerializerTest
    {
        [Fact]
        public void WhenBothPropertiesPopulated_ThenQueryStringHasBothFields()
        {
            //Arrange
            const string ProductId = "1111";
            const string OrderId = "2222";
           
            var testData = new { product_id = ProductId, order_id = OrderId };
            var expectedString = $"{nameof(testData.product_id)}={ProductId}&{nameof(testData.order_id)}={OrderId}";

            //Act
            var queryString = UrlQueryStringSerializer.SerializeObject(testData);

            //Assert
            queryString.Should().Be(expectedString);
        }

        [Fact]
        public void WhenOnePropertyIsPopulated_ThenQueryStringHasOneField()
        {
            //Arrange
            const string ProductId = "1111";
            const string OrderId = null;

            var testData = new { product_id = ProductId, order_id = OrderId };
            var expectedString = $"{nameof(testData.product_id)}={ProductId}";

            //Act
            var queryString = UrlQueryStringSerializer.SerializeObject(testData);

            //Assert
            queryString.Should().Be(expectedString);
        }

        [Fact]
        public void WhenNoPropertyIsPopulated_ThenQueryStringHasNoFields()
        {
            //Arrange
            const string ProductId = null;
            const string OrderId = null;

            var testData = new { product_id = ProductId, order_id = OrderId };            

            //Act
            var queryString = UrlQueryStringSerializer.SerializeObject(testData);

            //Assert
            queryString.Should().BeNullOrEmpty();
        }
    }
}
