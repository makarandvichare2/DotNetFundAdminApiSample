using Ardalis.Result;
using Ardalis.Specification;
using FluentAssertions;
using FluentValidation;
using FluentValidation.Results;
using FundAdministration.Core.Funds;
using FundAdministration.Infrastructure.Data;
using FundAdministration.UseCases.Funds.Update;
using NSubstitute;
using NSubstitute.ExceptionExtensions;

namespace FundAdministration.UseCases.Tests.Funds
{
    public class UpdateFundHandlerTest
    {
        private readonly ISoftDeleteRepository<Fund> _repository = Substitute.For<ISoftDeleteRepository<Fund>>();
        private readonly IValidator<UpdateFundCommand> _validator = Substitute.For<IValidator<UpdateFundCommand>>();
        private UpdateFundHandler _handler;

        public UpdateFundHandlerTest()
        {
            _handler = new UpdateFundHandler(_repository, _validator);
        }
        [Fact]
        public async Task Handle_WithValidInput_CreateFund()
        {
            //Arrange
            var inputData = ValidInputs();
            var fund = UpdateFund(
                 inputData.fundName,
                 inputData.currency,
                 inputData.launchDate
                 );

            var command = new UpdateFundCommand(
            inputData.id,
            inputData.fundName,
            inputData.currency,
            inputData.launchDate
            );

            _repository.GetByIdAsync(Arg.Any<Guid>(), Arg.Any<CancellationToken>())
            .Returns(Task.FromResult(fund));

            _repository.UpdateAsync(Arg.Any<Fund>(), Arg.Any<CancellationToken>())
            .Returns(Task.CompletedTask);

            //Act
            var result = await _handler.Handle(command, CancellationToken.None);

            //Assert
            result.IsSuccess.Should().BeTrue();
            result.Errors.Should().BeEmpty();
        }

        [Fact]
        public async Task Handle_WhenFundIdNotExist_ReturnError()
        {
            //Arrange
            var inputData = ValidInputs();

            var command = new UpdateFundCommand(
            inputData.id,
            inputData.fundName,
            inputData.currency,
            inputData.launchDate
            );

            //Act
            var result = await _handler.Handle(command, CancellationToken.None);

            //Assert
            result.IsSuccess.Should().BeFalse();
            result.Errors.Should().NotBeEmpty();
        }

        [Fact]
        public async Task Handle_WhenUpdateThrowException_ReturnError()
        {
            //Arrange
            var inputData = ValidInputs();

            var fund = UpdateFund(
                 inputData.fundName,
                 inputData.currency,
                 inputData.launchDate
                 );

            var command = new UpdateFundCommand(
            inputData.id,
            inputData.fundName,
            inputData.currency,
            inputData.launchDate
            );

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

        [Fact]
        public async Task Handle_WhenValidationFails_ReturnError()
        {
            //Arrange
            var inputData = ValidInputs();

            var command = new UpdateFundCommand(
            inputData.id,
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