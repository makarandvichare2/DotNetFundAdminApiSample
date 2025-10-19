using FluentAssertions;
using FundAdministration.Common.Enum;
using FundAdministration.Core.Transactions;

namespace FundAdministration.Core.Tests
{
    public class TransactionTests
    {
        [Fact]
        public void Constructor_WithValidInputs_CreateFundInstance()
        {
            //Arrange
            var inputData = ValidInputs();

            //Act
            var transaction = CreateTransaction(
                inputData.investorId,
                inputData.transactionType,
                inputData.amount,
                inputData.transactionDate
                ); ; 

            //Assert
            inputData.investorId.Should().Be(transaction.InvestorId);
            inputData.transactionType.Should().Be(transaction.TransactionType);
            inputData.amount.Should().Be(transaction.Amount);
            inputData.transactionDate.Should().Be(transaction.TransactionDate);

        }

        [Fact]
        public void Constructor_WithDefaultTransactionDateDate_ThrowException()
        {
            //Arrange
             var inputData = ValidInputs();
            inputData.transactionDate = default(DateTime);

            //Act
            Action act = () => CreateTransaction(
                inputData.investorId,
                inputData.transactionType,
                inputData.amount,
                inputData.transactionDate
                );

            //assert
            ArgumentException exception = Assert.Throws<ArgumentException>(act);
            exception.Message.Should().Be("Parameter [transactionDate] is default value for type DateTime (Parameter 'transactionDate')");
        }

        [Fact]
        public void Constructor_WithDefaultInvestorId_ThrowException()
        {
            //Arrange
            var inputData = ValidInputs();
            inputData.investorId = default(Guid);

            //Act
            Action act = () => CreateTransaction(
                inputData.investorId,
                inputData.transactionType,
                inputData.amount,
                inputData.transactionDate
                );

            //assert
            ArgumentException exception = Assert.Throws<ArgumentException>(act);
            exception.Message.Should().Be("Parameter [investorId] is default value for type Guid (Parameter 'investorId')");
        }


        #region private methods
        private (Guid investorId,
            TransactionType transactionType,
            decimal amount,
            DateTime transactionDate) ValidInputs()
        {
            return (
                investorId: Guid.NewGuid(),
                transactionType: TransactionType.Redemption,
                amount:2300,
                transactionDate: DateTime.Now
             );
        }

        private Transaction CreateTransaction(
            Guid investorId,
            TransactionType transactionType,
            decimal amount,
            DateTime transactionDate
            )
        {
            return new Transaction(
               investorId,
               transactionType,
               amount,
               transactionDate);
        }
        #endregion
    }

}