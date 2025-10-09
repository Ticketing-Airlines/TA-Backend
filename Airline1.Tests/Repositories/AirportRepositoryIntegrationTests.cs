using Xunit;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Airline1.Data;
using Airline1.Models;
using Airline1.Repositories.Implementations;
using System.Threading.Tasks;

namespace Airline1.Tests.Repositories
{
    public class AirportRepositoryIntegrationTests
    {
        private readonly AppDbContext _dbContext;
        private readonly AirportRepository _airportRepository;

        public AirportRepositoryIntegrationTests()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase("AirportTestDb")
                .Options;

            _dbContext = new AppDbContext(options);
            _airportRepository = new AirportRepository(_dbContext);
        }

        [Fact]
        public async Task AddAsync_ShouldSaveAirport()
        {
            // Arrange
            var airport = new Airport
            {
                IataCode = "CEB",
                IcaoCode = "RPVM",
                Name = "Mactan-Cebu International Airport",
                City = "Cebu",
                Country = "Philippines",
                Latitude = 10.3076,
                Longitude = 123.9794,
                TimeZone = "Asia/Manila",
                Terminals = 2,
                IsActive = true
            };

            // Act
            var result = await _airportRepository.AddAsync(airport);
            await _dbContext.SaveChangesAsync();

            // Assert
            var saved = await _dbContext.Airports.FirstOrDefaultAsync(a => a.IataCode == "CEB");
            saved.Should().NotBeNull();
            saved!.Name.Should().Be("Mactan-Cebu International Airport");
            saved.IcaoCode.Should().Be("RPVM");
        }
    }
}
