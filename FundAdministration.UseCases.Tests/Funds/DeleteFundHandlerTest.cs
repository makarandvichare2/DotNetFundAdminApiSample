using Ardalis.Result;
using FluentAssertions;
using FluentValidation;
using FundAdministration.Core.Funds;
using FundAdministration.Infrastructure.Data;
using FundAdministration.UseCases.Funds.Create;
using FundAdministration.UseCases.Funds.Delete;
using NSubstitute;
using NSubstitute.ExceptionExtensions;

namespace FundAdministration.UseCases.Tests.Funds
{
    public class DeleteFundHandlerTest
    {
        private readonly ISoftDeleteRepository<Fund> _repository = Substitute.For<ISoftDeleteRepository<Fund>>();
        private readonly IValidator<DeleteFundCommand> _validator = Substitute.For<IValidator<DeleteFundCommand>>();
        private DeleteFundHandler _handler;

        public DeleteFundHandlerTest()
        {
            _handler = new DeleteFundHandler(_repository, _validator);
        }
        [Fact]
        public async Task Handle_WithValidInput_DeleteFund()
        {
            //Arrange
            var inputData = ValidInputs();
            var fund = UpdateFund(
                 inputData.fundName,
                 inputData.currency,
                 inputData.launchDate
                 );

            var command = new DeleteFundCommand(inputData.id);

            _repository.GetByIdAsync(Arg.Any<Guid>(), Arg.Any<CancellationToken>())
            .Returns(Task.FromResult(fund));

            _repository.UpdateAsync(Arg.Any<Fund>(), Arg.Any<CancellationToken>())
            .Returns(Task.FromResult(true));

            //Act
            var result = await _handler.Handle(command, CancellationToken.None);

            //Assert
            result.IsSuccess.Should().BeTrue();
        }

        [Fact]
        public async Task Handle_WhenValidationFails_ReturnError()
        {
            //Arrange
            var fundId = Guid.NewGuid();

            var command = new DeleteFundCommand(fundId);

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
            var fund = UpdateFund(
                 inputData.fundName,
                 inputData.currency,
                 inputData.launchDate
                 );

            var command = new DeleteFundCommand(inputData.id);

            _repository.GetByIdAsync(Arg.Any<Guid>(), Arg.Any<CancellationToken>())
            .Returns(Task.FromResult(fund));

            _repository.UpdateAsync(Arg.Any<Fund>(), Arg.Any<CancellationToken>())
            .ThrowsAsync(new Exception());

            //Act
            var result = await _handler.Handle(command, CancellationToken.None);

            //Assert
            result.IsSuccess.Should().BeFalse();
            result.Errors.Should().NotBeEmpty();
        }

        #region private methods
        private (Guid id, string fundName, string currency, DateTime launchDate) ValidInputs()
        {
            return (
                id: Guid.NewGuid(),
                fundName: "Fund Name 1",
                currency: "EUR",
                launchDate: DateTime.Now
             );
        }

        private Fund UpdateFund(
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