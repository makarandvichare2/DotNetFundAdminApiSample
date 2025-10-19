using FluentAssertions;
using FundAdministration.Core.Investors;
namespace FundAdministration.Core.Tests
{
    public class InvestorTests
    {
        [Fact]
        public void Constructor_WithValidInputs_CreateInvestorInstance()
        {
            //Arrange
            var inputData = ValidInputs();

            //Act
            var investor = CreateInvestor(
                inputData.fullName,
                inputData.email,
                inputData.fundId
                ); 

            //Assert
            inputData.fullName.Should().Be(investor.FullName);
            inputData.email.Should().Be(investor.Email.EmailId);
            inputData.fundId.Should().Be(investor.FundId);

        }

        [Fact]
        public void Constructor_WithDefaultFundId_ThrowException()
        {
            //Arrange
             var inputData = ValidInputs();
            inputData.fundId = default(Guid);

            //Act
            Action act = () => CreateInvestor(
                inputData.fullName,
                inputData.email,
                inputData.fundId
                );

            //assert
            ArgumentException exception = Assert.Throws<ArgumentException>(act);
            exception.Message.Should().Be("Parameter [fundId] is default value for type Guid (Parameter 'fundId')");
        }


        [Fact]
        public void Constructor_WithEmptyFullName_ThrowException()
        {
            //Arrange
            var inputData = ValidInputs();
            inputData.fullName = string.Empty;

            //Act
            Action act = () => CreateInvestor(
                inputData.fullName,
                inputData.email,
                inputData.fundId
                );

            //assert
            ArgumentException exception = Assert.Throws<ArgumentException>(act);
            exception.Message.Should().Be("Required input fullName was empty. (Parameter 'fullName')");
        }

        [Fact]
        public void Constructor_WithSpaceFullName_ThrowException()
        {
            //Arrange
            var inputData = ValidInputs();
            inputData.fullName = " ";

            //Act
            Action act = () => CreateInvestor(
                inputData.fullName,
                inputData.email,
                inputData.fundId
                );

            //assert
            ArgumentException exception = Assert.Throws<ArgumentException>(act);
            exception.Message.Should().Be("Required input fullName was empty. (Parameter 'fullName')");
        }

        [Fact]
        public void Constructor_WithNullFullName_ThrowException()
        {
            //Arrange
            var inputData = ValidInputs();
            inputData.fullName = null;

            //Act
            Action act = () => CreateInvestor(
                inputData.fullName,
                inputData.email,
                inputData.fundId
                );

            //assert
            ArgumentException exception = Assert.Throws<ArgumentNullException>(act);
            exception.Message.Should().Be("Value cannot be null. (Parameter 'fullName')");
        }

        [Fact]
        public void Constructor_WithInvalidEmail_ThrowException()
        {
            //Arrange
            var inputData = ValidInputs();
            inputData.email = string.Empty;

            //Act
            Action act = () => CreateInvestor(
                inputData.fullName,
                inputData.email,
                inputData.fundId
                );

            //assert
            ArgumentException exception = Assert.Throws<ArgumentException>(act);
            exception.Message.Should().Be("Required input emailId was empty. (Parameter 'emailId')");
        }


        #region private methods
        private (string fullName, string email, Guid fundId) ValidInputs()
        {
            return (
            fullName: "Full Name 1",
            email: "mak@mak.com",
            fundId: Guid.NewGuid()
             );
        }

        private Investor CreateInvestor(
            string fullName,
            string email,
            Guid fundId
            )
        {
            return new Investor(
               fullName,
               new Email(email),
               fundId);
        }
        #endregion
    }

}