using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using SolarWatch;
using SolarWatch.Controllers;
using SolarWatch.Repository;
using SolarWatch.Services.OpeningApis;
using SolarWatch.Services.Processors;
using SolarWatch.Services.Provider;
using System;

namespace SolarWatchTest
{
    [TestFixture]
    public class SolarWatchControllerTests
    {
        private Mock<ILogger<SolarWatchController>> _loggerMock;
        private Mock<ICordinatesProcessor> _processorMock;
        private Mock<IOpenGeocodingApi> _geocodingApiMock;
        private Mock<IOpenSunsetAndSunriseApi> _sunsetAndSunriseApi;
        private Mock<ISolarWatchProvider> _providerMock;
        private Mock<ICityRepository> _cityRepositoryMock;
        private Mock<ISunsetSunriseRepository> _sunsetSunriseRepositoryMock;
        private SolarWatchController _controller;

        [SetUp]
        public void Setup()
        {
            _loggerMock = new Mock<ILogger<SolarWatchController>>();
            _processorMock = new Mock<ICordinatesProcessor>();
            _geocodingApiMock = new Mock<IOpenGeocodingApi>();
            _sunsetAndSunriseApi = new Mock<IOpenSunsetAndSunriseApi>();
            _providerMock = new Mock<ISolarWatchProvider>();
            _cityRepositoryMock = new Mock<ICityRepository>();
            _sunsetSunriseRepositoryMock = new Mock<ISunsetSunriseRepository>();
            _controller = new SolarWatchController(_loggerMock.Object, _processorMock.Object, _geocodingApiMock.Object, _providerMock.Object, _sunsetAndSunriseApi.Object, _cityRepositoryMock.Object, _sunsetSunriseRepositoryMock.Object);
        }

        [Test]
        public async Task GetTwilightReturnsNotFoundResultIfOpenGeocodingApiFails()
        {
            // Arrange
            var city = "{}";
            var date = new DateOnly();
            _geocodingApiMock.Setup(p => p.GetGeocodingDataAsync(city)).Throws(new Exception());

            // Act
            var result = await _controller.GetTwilight(city, date);

            // Assert
            Assert.IsInstanceOf(typeof(NotFoundObjectResult), result.Result);
        }

        [Test]
        public async Task GetTwilightReturnsNotFoundResultIfGeocodingApiDataIsInvalid()
        {
            // Arrange
            var city = "Budapest";
            var date = new DateOnly();
            var url = "{}";
            _geocodingApiMock.Setup(p => p.GetGeocodingDataAsync(city)).ReturnsAsync(url);
            _processorMock.Setup(p => p.ProcessCordinates(url)).Throws<Exception>();

            // Act
            var result = await _controller.GetTwilight(city, date);

            // Assert
            Assert.IsInstanceOf(typeof(NotFoundObjectResult), result.Result);
        }

        [Test]
        public async Task GetTwilightReturnsNotFoundResultIfSunsetAndSunriseApiDataIsInvalid()
        {
            // Arrange
            var city = "Budapest";
            var date = new DateOnly();
            var url = "{}";
            _geocodingApiMock.Setup(p => p.GetGeocodingDataAsync(city)).ReturnsAsync(url);
            _processorMock.Setup(p => p.ProcessCordinates(url)).Returns(It.IsAny<double[]>());
            _sunsetAndSunriseApi.Setup(p => p.GetSunsetAndSunriseDataAsync(It.IsAny<double[]>(), date)).Throws<Exception>();

            // Act
            var result = await _controller.GetTwilight(city, date);

            // Assert
            Assert.IsInstanceOf(typeof(NotFoundObjectResult), result.Result);
        }

        [Test]
        public async Task GetTwilightReturnsNotFoundResultIfSolarWatchProviderIsInvalid()
        {
            // Arrange
            var city = "Budapest";
            var date = new DateOnly();
            var url = "{}";
            _geocodingApiMock.Setup(p => p.GetGeocodingDataAsync(city)).ReturnsAsync(url);
            _processorMock.Setup(p => p.ProcessCordinates(url)).Throws<Exception>();
            _sunsetAndSunriseApi.Setup(p => p.GetSunsetAndSunriseDataAsync(It.IsAny<double[]>(), date)).ReturnsAsync(url);
            _providerMock.Setup(p => p.Process(url, city)).Throws<Exception>();

            // Act
            var result = await _controller.GetTwilight(city, date);

            // Assert
            Assert.IsInstanceOf(typeof(NotFoundObjectResult), result.Result);
        }

        [Test]
        public async Task GetTwilightReturnsNotFoundResultIfSolarWatchProviderIsValid()
        {
            // Arrange
            var city = "Budapest";
            var date = new DateOnly();
            var url = "{}";
            var expectedSolarWatch = new Twilight();
            _geocodingApiMock.Setup(p => p.GetGeocodingDataAsync(city)).ReturnsAsync(url);
            _processorMock.Setup(p => p.ProcessCordinates(url)).Returns(It.IsAny<double[]>());
            _sunsetAndSunriseApi.Setup(p => p.GetSunsetAndSunriseDataAsync(It.IsAny<double[]>(), date)).ReturnsAsync(url);
            _providerMock.Setup(p => p.Process(url, city)).Returns(expectedSolarWatch);

            // Act
            var result = await _controller.GetTwilight(city, date);

            // Assert
            Assert.IsInstanceOf(typeof(OkObjectResult), result.Result);
            Assert.That(((OkObjectResult)result.Result).Value, Is.EqualTo(expectedSolarWatch));
        }
    }
}