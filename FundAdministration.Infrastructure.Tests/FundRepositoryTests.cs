using FundAdministration.Core.Funds;
using FundAdministration.Infrastructure.Data;
using FundAdministration.Infrastructure.Tests.Base;
using FluentAssertions;


namespace FundAdministration.Infrastructure.Tests
{
    public class FundRepositoryTests : BaseRepositoryTest
    {
        [Fact]
        public async Task GetByIdAsync_WhenIdExists_ReturnFund()
        {
            // Arrange
            string databaseName = Guid.NewGuid().ToString();
            var launchDate = DateTime.Now;
            var currencyCode = "EUR";
            var fundName = "My Test Fund";
            Guid fundId = Guid.Empty;

            using var seedContext = CreateContext(databaseName);

            var newFund = new Fund(fundName, new Currency(currencyCode), launchDate);
            seedContext.Funds.Add(newFund);
            await seedContext.SaveChangesAsync();
            fundId = newFund.Id;

            using var testContext = CreateContext(databaseName);

            var repository = new SoftDeleteRepository<Fund>(testContext);

            // Act
            var result = await repository.GetByIdAsync(fundId);

            // Assert
            result.Should().NotBeNull();
            result.FundName.Should().Be(fundName);

        }

        [Fact]
        public async Task GetByIdAsync_WhenIdNotExists_ReturnNull()
        {
            // Arrange
            string databaseName = Guid.NewGuid().ToString();
            Guid notExistingId = Guid.NewGuid();

            using var testContext = CreateContext(databaseName) ;

            var repository = new SoftDeleteRepository<Fund>(testContext);

            // Act
            var result = await repository.GetByIdAsync(notExistingId);

            // Assert
            result.Should().BeNull();
        }

        [Fact]
        public async Task ListAsync_WhenFundsExists_ReturnFunds()
        {
            // Arrange
            string databaseName = Guid.NewGuid().ToString();

            using var seedContext = CreateContext(databaseName);

            seedContext.Funds.Add(new Fund("My Test Fund1", new Currency("EUR"), DateTime.Now));
            seedContext.Funds.Add(new Fund("My Test Fund2", new Currency("USD"), DateTime.Now));
            await seedContext.SaveChangesAsync();

            using var testContext = CreateContext(databaseName);

            var repository = new SoftDeleteRepository<Fund>(testContext);

            // Act
            var result = await repository.ListAsync();

            // Assert
            result.Count.Should().Be(2);

        }

        [Fact]
        public async Task ListAsync_WhenIdNotExists_ReturnNull()
        {
            // Arrange
            string databaseName = Guid.NewGuid().ToString();
            Guid notExistingId = Guid.NewGuid();

            using var testContext = CreateContext(databaseName);

            var repository = new SoftDeleteRepository<Fund>(testContext);

            // Act
            var result = await repository.ListAsync();

            // Assert
            result.Count.Should().Be(0);

        }

        [Fact]
        public async Task AddAsync_WhenValidInput_CreateNewFund()
        {
            // Arrange
            string databaseName = Guid.NewGuid().ToString();
            var launchDate = DateTime.Now;
            var currencyCode = "EUR";
            var fundName = "My Test Fund";

            using var testContext = CreateContext(databaseName);

            var repository = new SoftDeleteRepository<Fund>(testContext);
            var newFund = new Fund(fundName, new Currency(currencyCode), launchDate);

            // Act
            _ = await repository.AddAsync(newFund);

            // Assert
            var result = await repository.GetByIdAsync(newFund.Id);
            result.Should().NotBeNull();
            result.FundName.Should().Be(fundName);

        }

        [Fact]
        public async Task UpdateAsync_WhenValidInput_UpdateFund()
        {
            // Arrange
            string databaseName = Guid.NewGuid().ToString();
            var launchDate = DateTime.Now;
            var currencyCode = "EUR";
            var fundName = "My Test Fund";

            using var testContext = CreateContext(databaseName);

            var repository = new SoftDeleteRepository<Fund>(testContext);
            var newFund = new Fund(fundName, new Currency(currencyCode), launchDate);
             await repository.AddAsync(newFund);

            // Act
            newFund.UpdateFundName("My Test Fund 1");
            await repository.UpdateAsync(newFund);

            // Assert
            var result = await repository.GetByIdAsync(newFund.Id);
            result.Should().NotBeNull();
            result.FundName.Should().Be("My Test Fund 1");

        }

        [Fact]
        public async Task DeleteAsync_WhenValidInput_DeleteFund()
        {
            // Arrange
            string databaseName = Guid.NewGuid().ToString();
            var launchDate = DateTime.Now;
            var currencyCode = "EUR";
            var fundName = "My Test Fund";

            using var testContext = CreateContext(databaseName);

            var repository = new SoftDeleteRepository<Fund>(testContext);
            var newFund = new Fund(fundName, new Currency(currencyCode), launchDate);
            await repository.AddAsync(newFund);

            // Act
            await repository.DeleteAsync(newFund);

            // Assert
            var result = await repository.GetByIdAsync(newFund.Id);
            result.Should().BeNull();
        }
    }
}
