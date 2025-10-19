using Ardalis.Result;
using Azure.Core;
using FundAdministration.API.Controllers.Funds;
using FundAdministration.Common.Funds;
using FundAdministration.Core.Funds;
using FundAdministration.UseCases.Funds.Create;
using FundAdministration.UseCases.Funds.Delete;
using FundAdministration.UseCases.Funds.Get;
using FundAdministration.UseCases.Funds.List;
using FundAdministration.UseCases.Funds.Update;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using NSubstitute.ExceptionExtensions;

namespace FundAdministration.API.Tests
{
    public class FundControllerTest
    {
        [Fact]
        public async Task GetListAsync_ReturnOkObjectResult()
        {
            // Arrange
            var mediator = Substitute.For<IMediator>();
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
            var fundResult = Result.Success(funds.AsEnumerable());
            mediator.Send(Arg.Any<ListFundQuery>()).Returns(fundResult);
            var controller = new FundV1Controller(mediator);

            // Act
            var result = await controller.GetListAsync();

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task GetListAsync_ReturnNotFound()
        {
            // Arrange
            var mediator = Substitute.For<IMediator>();
            var funds = new List<FundListDTO> {
            };
            var fundResult = Result.NotFound();
            mediator.Send(Arg.Any<ListFundQuery>()).Returns(fundResult);
            var controller = new FundV1Controller(mediator);

            // Act
            var result = await controller.GetListAsync();

            // Assert
            Assert.IsType<NotFoundObjectResult>(result);
            var notFoundResult = (NotFoundObjectResult)result;
            Assert.IsType<ProblemDetails>(notFoundResult.Value);
            var problemDetails = (ProblemDetails)notFoundResult.Value;
            Assert.Equal(404, problemDetails.Status);
            Assert.Equal("Resouce not found", problemDetails.Title);
        }

        [Fact]
        public async Task GetFundAsync_ReturnOkObjectResult()
        {
            // Arrange
            var mediator = Substitute.For<IMediator>();
            var createFundData = CreateFundData();
            var fundResult = Result.Success(createFundData);
            mediator.Send(Arg.Any<GetFundQuery>()).Returns(fundResult);
            var controller = new FundV1Controller(mediator);
            var fundId = Guid.NewGuid();

            // Act
            var result = await controller.GetFundAsync(fundId);

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task CreateFund_ReturnOkObjectResult()
        {
            // Arrange
            var mediator = Substitute.For<IMediator>();
            var request = CreateFundRequest();
            var fundResult = Result.Success(Guid.NewGuid());
            mediator.Send(Arg.Any<CreateFundCommand>()).Returns(fundResult);
            var controller = new FundV1Controller(mediator);

            // Act
            var result = await controller.CreateFund(request);

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task CreateFund_ReturnBadRequest()
        {
            // Arrange
            var mediator = Substitute.For<IMediator>();
            var request = CreateFundRequest();
            var fundResult = Result.Invalid();
            mediator.Send(Arg.Any<CreateFundCommand>()).Returns(fundResult);
            var controller = new FundV1Controller(mediator);

            // Act
            var result = await controller.CreateFund(request);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
            var badRequestResult = (BadRequestObjectResult)result;
            Assert.IsType<ValidationProblemDetails>(badRequestResult.Value);
            var problemDetails = (ValidationProblemDetails)badRequestResult.Value;
            Assert.Equal(400, problemDetails.Status);
            Assert.Equal("Validation failed", problemDetails.Title);
        }

        [Fact]
        public async Task UpdateFund_ReturnOkObjectResult()
        {
            // Arrange
            var mediator = Substitute.For<IMediator>();
            var request = UpdateFundRequest();
            var fundResult = Result.Success(true);
            mediator.Send(Arg.Any<UpdateFundCommand>()).Returns(fundResult);
            var controller = new FundV1Controller(mediator);

            // Act
            var result = await controller.UpdateFund(request.id,request);

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task UpdateFund_ReturnBadRequest()
        {
            // Arrange
            var mediator = Substitute.For<IMediator>();
            var request = UpdateFundRequest();
            var fundResult = Result.Invalid();
            mediator.Send(Arg.Any<UpdateFundCommand>()).Returns(fundResult);
            var controller = new FundV1Controller(mediator);

            // Act
            var result = await controller.UpdateFund(request.id, request);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
            var badRequestResult = (BadRequestObjectResult)result;
            Assert.IsType<ValidationProblemDetails>(badRequestResult.Value);
            var problemDetails = (ValidationProblemDetails)badRequestResult.Value;
            Assert.Equal(400, problemDetails.Status);
            Assert.Equal("Validation failed", problemDetails.Title);
        }

        [Fact]
        public async Task DeleteFund_ReturnOkObjectResult()
        {
            // Arrange
            var mediator = Substitute.For<IMediator>();
            var id = Guid.NewGuid();
            var fundResult = Result.Success(true);
            mediator.Send(Arg.Any<DeleteFundCommand>()).Returns(fundResult);
            var controller = new FundV1Controller(mediator);

            // Act
            var result = await controller.DeleteFund(id);

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task DeleteFund_ReturnNotFound()
        {
            // Arrange
            var mediator = Substitute.For<IMediator>();
            var id = Guid.NewGuid();
            var fundResult = Result.NotFound();
            mediator.Send(Arg.Any<DeleteFundCommand>()).Returns(fundResult);
            var controller = new FundV1Controller(mediator);

            // Act
            var result = await controller.DeleteFund(id);

            // Assert
            Assert.IsType<NotFoundObjectResult>(result);
            var notFoundResult = (NotFoundObjectResult)result;
            Assert.IsType<ProblemDetails>(notFoundResult.Value);
            var problemDetails = (ProblemDetails)notFoundResult.Value;
            Assert.Equal(404, problemDetails.Status);
            Assert.Equal("Resouce not found", problemDetails.Title);
        }

        [Fact]
        public async Task DeleteFund_ReturnInternalError()
        {
            // Arrange
            var mediator = Substitute.For<IMediator>();

            mediator.Send(Arg.Any<DeleteFundCommand>()).ThrowsAsync(new Exception());
            var controller = new FundV1Controller(mediator);

            // Act
            // Assert
            await Assert.ThrowsAsync<Exception>(
                    () => controller.DeleteFund(Guid.NewGuid())
            );
            
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
        private CreateFundDataDTO CreateFundData()
        {
            CreateFundDTO fundDTO = CreateFundDTO();
            var currencies = new List<string>();


            return new CreateFundDataDTO(fundDTO, currencies);
        }

        private CreateFundDTO CreateFundDTO()
        {
            return new CreateFundDTO("Fund 1","EUR",DateTime.Now);
        }

        private CreateFundRequest CreateFundRequest()
        {
            return new CreateFundRequest("Fund 1", "EUR", DateTime.Now);
        }

        private UpdateFundRequest UpdateFundRequest()
        {
            return new UpdateFundRequest(Guid.NewGuid(),"Fund 1", "EUR", DateTime.Now);
        }
        #endregion
    }
}