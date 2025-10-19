using FluentAssertions;
using FundAdministration.Common.Funds;
using FundAdministration.Infrastructure.Data.Queries.Funds;
using FundAdministration.UseCases.Funds.List;
using NSubstitute;

namespace FundAdministration.UseCases.Tests.Funds
{
    public class ListFundHandlerTest
    {
        private readonly IListFundQueryService _service = Substitute.For<IListFundQueryService>();
        private ListFundHandler _handler;

        public ListFundHandlerTest()
        {
            _handler = new ListFundHandler(_service);
        }
        [Fact]
        public async Task Handle_WithValidInput_ReturnFunds()
        {
            //Arrange
            var query = new ListFundQuery();
            var fundsData = FundsData();

            var funds = new List<FundListDTO> {
                new FundListDTO(
                   fundsData[0].id,
                   fundsData[0].fundName,
                   fundsData[0].currency,
                   fundsData[0].launchDate
                    ),
                new FundListDTO(
                    fundsData[1].id,
                   fundsData[1].fundName,
                   fundsData[1].currency,
                   fundsData[1].launchDate
                    )
            };

            _service.ListAsync().Returns(Task.FromResult(funds.AsEnumerable()));

            //Act
            var result = await _handler.Handle(query, CancellationToken.None);

            //Assert
            result.Value.Should().BeEquivalentTo(funds);
        }

        #region private methods
        private List<(Guid id, string fundName, string currency, DateTime launchDate)> FundsData()
        {
            var list = new List<(Guid id, string fundName, string currency, DateTime launchDate)>()
            {
                (   id: Guid.NewGuid(),
                    fundName: "Fund Name 1",
                    currency: "EUR",
                    launchDate: DateTime.Now
                ),
                (id: Guid.NewGuid(),
                fundName: "Fund Name 2",
                    currency: "USD",
                    launchDate: DateTime.Now
                )
            };

            return list;
        }
        #endregion
    }
}