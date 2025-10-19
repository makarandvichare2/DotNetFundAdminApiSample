using FluentAssertions;
using FundAdministration.Core.Funds;
namespace FundAdministration.Core.Tests
{
    public class FundTests
    {
        [Fact]
        public void Constructor_WithValidInputs_CreateFundInstance()
        {
            //Arrange
            var inputData = ValidInputs();

            //Act
            var fund = CreateFund(
                inputData.fundName,
                inputData.currency,
                inputData.launchDate
                ); 

            //Assert
            inputData.fundName.Should().Be(fund.FundName);
            inputData.currency.Should().Be(fund.Currency.CurrencyCode);
            inputData.launchDate.Should().Be(fund.LaunchDate);

        }

        [Fact]
        public void Constructor_WithDefaultLaunchDate_ThrowException()
        {
            //Arrange
             var inputData = ValidInputs();
            inputData.launchDate = default(DateTime);

            //Act
            Action act = () => CreateFund(
                inputData.fundName,
                inputData.currency,
                inputData.launchDate
                );

            //assert
            ArgumentException exception = Assert.Throws<ArgumentException>(act);
            exception.Message.Should().Be("Parameter [launchDate] is default value for type DateTime (Parameter 'launchDate')");
        }


        [Fact]
        public void Constructor_WithEmptyFundName_ThrowException()
        {
            //Arrange
            var inputData = ValidInputs();
            inputData.fundName = string.Empty;

            //Act
            Action act = () => CreateFund(
                inputData.fundName,
                inputData.currency,
                inputData.launchDate
                );

            //assert
            ArgumentException exception = Assert.Throws<ArgumentException>(act);
            exception.Message.Should().Be("Required input fundName was empty. (Parameter 'fundName')");
        }

        [Fact]
        public void Constructor_WithSpaceFundName_ThrowException()
        {
            //Arrange
            var inputData = ValidInputs();
            inputData.fundName = " ";

            //Act
            Action act = () => CreateFund(
                inputData.fundName,
                inputData.currency,
                inputData.launchDate
                );

            //assert
            ArgumentException exception = Assert.Throws<ArgumentException>(act);
            exception.Message.Should().Be("Required input fundName was empty. (Parameter 'fundName')");
        }

        [Fact]
        public void Constructor_WithNullFundName_ThrowException()
        {
            //Arrange
            var inputData = ValidInputs();
            inputData.fundName = null;

            //Act
            Action act = () => CreateFund(
                inputData.fundName,
                inputData.currency,
                inputData.launchDate
                );

            //assert
            ArgumentException exception = Assert.Throws<ArgumentNullException>(act);
            exception.Message.Should().Be("Value cannot be null. (Parameter 'fundName')");
        }

        [Fact]
        public void Constructor_WithInvalidCurrency_ThrowException()
        {
            //Arrange
            var inputData = ValidInputs();
            inputData.currency = string.Empty;

            //Act
            Action act = () => CreateFund(
                inputData.fundName,
                inputData.currency,
                inputData.launchDate
                );

            //assert
            ArgumentException exception = Assert.Throws<ArgumentException>(act);
            exception.Message.Should().Be("Required input currencyCode was empty. (Parameter 'currencyCode')");
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