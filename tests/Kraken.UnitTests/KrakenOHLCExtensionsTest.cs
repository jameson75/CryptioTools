using System;
using Xunit;
using CipherPark.ExchangeTools.Kraken.Models;
using Moq;
using FluentAssertions;
using Newtonsoft.Json;

namespace Kraken.UnitTests
{
    public class KrakenOHLCExtensionsTest
    {
        [Fact]
        public void WhenHOHLCReponsePopulated_ThenExtensionMethodsLookupCorrectValues()
        {
            //Arrange
            var jsonResult = "{\"error\":[],\"result\":{\"XLTCZUSD\":[[1559088000,\"114.51\",\"118.20\",\"108.88\",\"114.66\",\"114.85\",\"13316.28846765\",1096],[1559174400,\"115.35\",\"121.00\",\"101.89\",\"108.48\",\"111.71\",\"23709.71171712\",1947],[1559260800,\"107.86\",\"114.78\",\"104.54\",\"114.64\",\"108.47\",\"14296.17391353\",1066],[1559347200,\"114.68\",\"115.76\",\"110.83\",\"112.51\",\"113.20\",\"9241.21366575\",662],[1559433600,\"112.61\",\"116.77\",\"111.85\",\"114.75\",\"114.60\",\"5264.77294620\",588],[1559520000,\"114.51\",\"116.07\",\"104.80\",\"105.73\",\"110.02\",\"39258.99879483\",3237],[1559606400,\"105.73\",\"106.28\",\"98.00\",\"101.90\",\"102.98\",\"89398.49801357\",7911],[1559692800,\"102.12\",\"105.20\",\"100.17\",\"103.91\",\"102.81\",\"28818.27982673\",1995],[1559779200,\"103.49\",\"112.51\",\"101.20\",\"111.31\",\"106.11\",\"35379.21843737\",2516],[1559865600,\"111.78\",\"121.57\",\"109.83\",\"117.14\",\"115.93\",\"45625.44698274\",3921],[1559952000,\"116.90\",\"119.98\",\"114.03\",\"118.52\",\"117.14\",\"19270.45071876\",1771],[1560038400,\"118.82\",\"119.36\",\"111.51\",\"114.67\",\"115.20\",\"31973.64493268\",2219],[1560124800,\"114.41\",\"130.50\",\"112.78\",\"130.04\",\"123.41\",\"49972.18617081\",4160],[1560211200,\"129.57\",\"138.82\",\"125.14\",\"136.26\",\"131.85\",\"40960.95719725\",3466]]\r\n}\r\n}";
            var sut = JsonConvert.DeserializeObject<OHLCResponse>(jsonResult);

            //Act
            var open = sut.Open(0);
            var high = sut.High(0);
            var low = sut.Low(1);
            var close = sut.Close(1);
            var time = sut.Time(1);

            //Assert
            open.Should().Be(114.51);
            high.Should().Be(118.20);
            low.Should().Be(101.89);
            close.Should().Be(108.48);
            time.Should().Be(DateTime.Parse("5/30/2019 12:00:00 AM"));
        }
    }
}