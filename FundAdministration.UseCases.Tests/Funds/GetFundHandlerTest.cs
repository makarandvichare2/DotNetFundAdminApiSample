using FluentAssertions;
using FundAdministration.Common.Funds;
using FundAdministration.Infrastructure.Data.Queries.Funds;
using FundAdministration.UseCases.Funds.Get;
using FundAdministration.UseCases.Funds.List;
using NSubstitute;
using NSubstitute.ExceptionExtensions;

namespace FundAdministration.UseCases.Tests.Funds
{
    public class GetFundHandlerTest
    {
        private readonly IGetFundQueryService _service = Substitute.For<IGetFundQueryService>();
        private GetFundHandler _handler;

        public GetFundHandlerTest()
        {
            _handler = new GetFundHandler(_service);
        }
        [Fact]
        public async Task Handle_WithValidInput_ReturnFund()
        {
            //Arrange
            var fundDto = FundData();
            var fundId = Guid.NewGuid();
            var query = new GetFundQuery(fundId);
            var fundCreateDataDto = new CreateFundDataDTO(fundDto, new List<string> { });

            _service.FundDataAsync(fundId).Returns(Task.FromResult(fundCreateDataDto));

            //Act
            var result = await _handler.Handle(query, CancellationToken.None);

            //Assert
            result.IsSuccess.Should().BeTrue();
            result.Value.Should().BeEquivalentTo(fundCreateDataDto);
        }

        [Fact]
        public async Task Handle_WhenFundDataAsyncThrowException_ReturnError()
        {
            //Arrange
            var fundDto = FundData();
            var fundId = Guid.NewGuid();
            var query = new GetFundQuery(fundId);
            var fundCreateDataDto = new CreateFundDataDTO(fundDto, new List<string> { });

            _service.FundDataAsync(fundId).ThrowsAsync(new Exception());

            //Act
            var result = await _handler.Handle(query, CancellationToken.None);

            //Assert
            result.IsSuccess.Should().BeFalse();
            result.Errors.Should().NotBeEmpty();
        }

        #region private methods
        private CreateFundDTO FundData()
        {
            return new CreateFundDTO(
                    fundName: "Fund Name 1",
                    currency: "EUR",
                    launchDate: DateTime.Now
                );
        }
        #endregion
    }
}