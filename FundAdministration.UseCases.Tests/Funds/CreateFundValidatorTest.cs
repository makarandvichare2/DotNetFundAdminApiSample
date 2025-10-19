using Ardalis.SharedKernel;
using FluentAssertions;
using FluentValidation.TestHelper;
using FundAdministration.Core.Funds;
using FundAdministration.Infrastructure.Data.Config;
using FundAdministration.UseCases.Funds.Create;
using FundAdministration.UseCases.Funds.Validators;
using FundAdministration.UseCases.Specifications;
using NSubstitute;

namespace FundAdministration.UseCases.Tests.Funds
{
    public class CreateFundValidatorTest
    {
        private readonly IReadRepository<Fund> _repository = Substitute.For<IReadRepository<Fund>>();
        private CreateFundValidator _validator;

        public CreateFundValidatorTest()
        {
            _validator = new CreateFundValidator(_repository);
        }
        [Fact]
        public async Task Handle_WithValidInput_ReturnSuccess()
        {
            //Arrange
            var command = ValidCommand();

            //Act
            var result = await _validator.TestValidateAsync(command);

            //Assert
            result.IsValid.Should().BeTrue();
        }

        [Fact]
        public async Task Handle_WhenFundNameIsNull_ReturnError()
        {
            //Arrange
            var command = ValidCommand() with { fundName = null };

            //Act
            var result = await _validator.TestValidateAsync(command);

            //Assert
            result.Errors.Should().ContainSingle(e =>
            e.PropertyName == nameof(CreateFundCommand.fundName) &&
            e.ErrorMessage == "Fund Name is required.");
        }

        [Fact]
        public async Task Handle_WhenFundNameExceedMaxLength_ReturnError()
        {
            //Arrange
            var command = ValidCommand() with { fundName = new string('A', DataSchemaConstants.LENGTH_100 + 1) };

            //Act
            var result = await _validator.TestValidateAsync(command);

            //Assert
            result.Errors.Should().ContainSingle(e =>
            e.PropertyName == nameof(CreateFundCommand.fundName) &&
            e.ErrorMessage == "The length of 'fund Name' must be 100 characters or fewer. You entered 101 characters.");
        }

        [Fact]
        public async Task Handle_WhenFundNameAlreadyExists_ReturnError()
        {
            //Arrange
            var command = ValidCommand();

            _repository.CountAsync( Arg.Any<FundByNameSpec>(), Arg.Any<CancellationToken>()) .Returns(Task.FromResult(1));

            //Act
            var result = await _validator.TestValidateAsync(command);

            //Assert
            result.Errors.Should().ContainSingle(e =>
            e.PropertyName == nameof(CreateFundCommand.fundName) &&
            e.ErrorMessage == "Fund name already exists.");
        }

        [Fact]
        public async Task Handle_WhenCurrencyCodeEmpty_ReturnError()
        {
            //Arrange
            var command = ValidCommand() with { currencyCode = null };

            //Act
            var result = await _validator.TestValidateAsync(command);

            //Assert
            result.Errors.Should().ContainSingle(e =>
            e.PropertyName == nameof(CreateFundCommand.currencyCode) &&
            e.ErrorMessage == "Currency Code is required.");
        }

        [Fact]
        public async Task Handle_WhenCurrencyCodeLessThanMinLength_ReturnError()
        {
            //Arrange
            var command = ValidCommand() with { currencyCode = "E" };

            //Act
            var result = await _validator.TestValidateAsync(command);

            //Assert
            result.Errors.Should().ContainSingle(e =>
            e.PropertyName == nameof(CreateFundCommand.currencyCode) &&
            e.ErrorMessage == "The length of 'currency Code' must be at least 3 characters. You entered 1 characters.");
        }

        [Fact]
        public async Task Handle_WhenCurrencyCodeGreaterThanMaxLength_ReturnError()
        {
            //Arrange
            var command = ValidCommand() with { currencyCode = "EASW" };

            //Act
            var result = await _validator.TestValidateAsync(command);

            //Assert
            result.Errors.Should().ContainSingle(e =>
            e.PropertyName == nameof(CreateFundCommand.currencyCode) &&
            e.ErrorMessage == "The length of 'currency Code' must be 3 characters or fewer. You entered 4 characters.");
        }

        [Fact]
        public async Task Handle_WhenLaunchDateIdDefault_ReturnError()
        {
            //Arrange
            var command = ValidCommand() with { launchDate = default(DateTime) };

            //Act
            var result = await _validator.TestValidateAsync(command);

            //Assert
            result.Errors.Should().ContainSingle(e =>
            e.PropertyName == nameof(CreateFundCommand.launchDate) &&
            e.ErrorMessage == "Launch Date is not valid.");
        }


        #region private methods
        private CreateFundCommand ValidCommand()
        {
            return new CreateFundCommand("Fund Name 1", "EUR", DateTime.Now);
        }
        #endregion
    }
}
