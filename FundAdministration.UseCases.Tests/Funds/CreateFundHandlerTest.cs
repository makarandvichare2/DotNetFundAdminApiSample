using Ardalis.Result;
using FluentAssertions;
using FluentValidation;
using FundAdministration.Core.Funds;
using FundAdministration.Infrastructure.Data;
using FundAdministration.UseCases.Funds.Create;
using NSubstitute;
using NSubstitute.ExceptionExtensions;

namespace FundAdministration.UseCases.Tests.Funds
{
    public class CreateFundHandlerTest
    {
        private readonly ISoftDeleteRepository<Fund> _repository = Substitute.For<ISoftDeleteRepository<Fund>>();
        private readonly IValidator<CreateFundCommand> _validator = Substitute.For<IValidator<CreateFundCommand>>();
        private CreateFundHandler _handler;

        public CreateFundHandlerTest()
        {
            _handler = new CreateFundHandler(_repository, _validator);
        }
        [Fact]
        public async Task Handle_WithValidInput_CreateFund()
        {
            //Arrange
            var inputData = ValidInputs();
            var fund = CreateFund(
                 inputData.fundName,
                 inputData.currency,
                 inputData.launchDate
                 );

            var command = new CreateFundCommand(
            inputData.fundName,
            inputData.currency,
            inputData.launchDate
            );
            _repository.AddAsync(Arg.Any<Fund>(), Arg.Any<CancellationToken>())
            .Returns(Task.FromResult(fund));

            //Act
            var result = await _handler.Handle(command, CancellationToken.None);

            //Assert
            result.IsSuccess.Should().BeTrue();
        }

        [Fact]
        public async Task Handle_WhenValidationFails_ReturnError()
        {
            //Arrange
            var inputData = ValidInputs();
            var fund = CreateFund(
                 inputData.fundName,
                 inputData.currency,
                 inputData.launchDate
                 );

            var command = new CreateFundCommand(
            inputData.fundName,
            inputData.currency,
            inputData.launchDate
            );

            _validator.WhenForAnyArgs(x => x.ValidateAndThrowAsync(command))
            .Throw(new ValidationException(string.Empty));

            //Act
            var result = await _handler.Handle(command, CancellationToken.None);

            //Assert
            result.Status.Should().Be(ResultStatus.Invalid);

        }

        [Fact]
        public async Task Handle_WhenCreateThrowException_ReturnError()
        {
            //Arrange
            var inputData = ValidInputs();

            var command = new CreateFundCommand(
            inputData.fundName,
            inputData.currency,
            inputData.launchDate
            );

            _repository.AddAsync(Arg.Any<Fund>(), Arg.Any<CancellationToken>())
            .ThrowsAsync(new Exception());

            //Act
            var result = await _handler.Handle(command, CancellationToken.None);

            //Assert
            result.IsSuccess.Should().BeFalse();
            result.Errors.Should().NotBeEmpty();
        }

        #region private methods
        private (string fundName, string currency, DateTime launchDate) ValidInputs()
        {
            return (
            fundName: "Fund Name 1",
            currency: "EUR",
            launchDate: DateTime.Now
             );
        }

        private Fund CreateFund(
            string fundName,
            string currencyCode,
            DateTime launchDate
            )
        {
            return new Fund(
               fundName,
               new Currency(currencyCode),
               launchDate);
        }
        #endregion
    }
}