using Xunit;
using Moq;
using FluentAssertions;
using Airline1.Services.Interfaces;
using Airline1.Services.Implementations;
using Airline1.Repositories.Interfaces;
using Airline1.Models;
using System.Threading.Tasks;

namespace Airline1.Tests.Services
{
    public class AirportServiceTests
    {
        private readonly Mock<IAirportRepository> _airportRepositoryMock;
        private readonly IAirportService _airportService;

        public AirportServiceTests()
        {
            _airportRepositoryMock = new Mock<IAirportRepository>();
            _airportService = new AirportService(_airportRepositoryMock.Object);
        }

        [Fact]
        public async Task AddAirport_ShouldReturnAirport_WhenValid()
        {
            // Arrange
            var airport = new Airport
            {
                Id = 1,
                IataCode = "MNL",
                IcaoCode = "RPLL",
                Name = "Ninoy Aquino International Airport",
                City = "Manila",
                Country = "Philippines",
                Latitude = 14.5086,
                Longitude = 121.0198,
                TimeZone = "Asia/Manila",
                Terminals = 4,
                IsActive = true
            };

            _airportRepositoryMock
                .Setup(repo => repo.AddAsync(It.IsAny<Airport>()))
                .ReturnsAsync(airport);

            // Act
            var result = await _airportService.AddAirportAsync(airport);

            // Assert
            result.Should().NotBeNull();
            result.IataCode.Should().Be("MNL");
            result.IcaoCode.Should().Be("RPLL");
            result.Name.Should().Be("Ninoy Aquino International Airport");
        }
    }
}
