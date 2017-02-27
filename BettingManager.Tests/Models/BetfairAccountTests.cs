namespace BettingManager.Tests.Models
{
    using Logic.Models;
    using Logic.Common.Constants;
    using Logic.Contracts.Models;
    using Moq;
    using NUnit.Framework;

    [TestFixture]
    public class BetfairAccountTests
    {
        
        private string accountName = "Angel";
        private decimal balance = 500m;
        private CurrencyType currency = CurrencyType.EUR;
        private decimal rake = 0.07m;
        private decimal decreasingStepValue = 2m;

        [Test]
        public void Constructor_ShouldPass_WithValidParams()
        {
            var account = new BetfairAccount(this.accountName, this.balance, this.currency, this.rake);

            Assert.IsInstanceOf<BetfairAccount>(account);
        }

        [Test]
        public void Constructor_ShouldThrowArgumentNullException_WithNullName()
        {
            Assert.That(
                () => new BetfairAccount(null, this.balance, this.currency, this.rake),
                Throws.ArgumentNullException.With.Message.Contains(string.Format(EngineConstants.ObjectCannotBeNullErrorMessage, "Name")));
        }

        [TestCase(-1)]
        [TestCase(0.2)]
        [TestCase(1)]
        [TestCase(10)]
        public void Constructor_ShouldThrowArgumentException_WithIncorectRake(decimal inputRake)
        {
            Assert.That(
                () => new BetfairAccount(this.accountName, this.balance, this.currency, inputRake),
                Throws.ArgumentException.With.Message.Contains(EngineConstants.IncorrectRakeValueErrorMessage));
        }

        [Test]
        public void GetName_CorrectlySetNameByConstructor()
        {
            var account = new BetfairAccount(this.accountName, this.balance, this.currency, this.rake);

            Assert.AreEqual(this.accountName, account.Name);
        }

        [Test]
        public void GetBalance_CorrectlySetBalanceByConstructor()
        {
            var account = new BetfairAccount(this.accountName, this.balance, this.currency, this.rake);

            Assert.AreEqual(this.balance, account.Balance);
        }

        [Test]
        public void GetCurrency_CorrectlySetCurrencyByConstructor()
        {
            var account = new BetfairAccount(this.accountName, this.balance, this.currency, this.rake);

            Assert.AreEqual(this.currency, account.Currency);
        }

        [Test]
        public void GetRake_CorrectlySetRakeByConstructor()
        {
            var account = new BetfairAccount(this.accountName, this.balance, this.currency, this.rake);

            Assert.AreEqual(this.rake, account.CurrentRake);
        }

        [Test]
        public void GetName_CorrectlySetName()
        {
            var name = "Changed";
            var account = new BetfairAccount(this.accountName, this.balance, this.currency, this.rake);
           
            account.Name = name;

            Assert.AreEqual(name, account.Name);
        }

        [Test]
        public void GetBalance_CorrectlySetBalance()
        {
            var balance = 230m;
            var account = new BetfairAccount(this.accountName, this.balance, this.currency, this.rake);

            account.Balance = balance;

            Assert.AreEqual(balance, account.Balance);
        }

        [Test]
        public void GetStartBalance_CorrectlySetBalance()
        {
            var balance = 230m;
            var account = new BetfairAccount(this.accountName, balance, this.currency, this.rake);
      
            Assert.AreEqual(balance, account.StartBalance);
        }

        [Test]
        public void GetRake_CorrectlySetRake()
        {
            var changedRake = 0.04m;
            var account = new BetfairAccount(this.accountName, this.balance, this.currency, this.rake);

            account.CurrentRake = changedRake;

            Assert.AreEqual(changedRake, account.CurrentRake);
        }

        [Test]
        public void Constructor_ShouldThrowArgumentException_NegativeBalance()
        {
            Assert.That(
                () => new BetfairAccount(this.accountName, -20, this.currency, this.rake),
                Throws.ArgumentException.With.Message.Contains(string.Format(EngineConstants.SomeValueCannotBeNegativeErrorMessage, "Balance")));
        }

        [Test]
        public void AddBettingLine_ShouldThrowNullArgumentException_WithNullLine()
        {
            var account = new BetfairAccount(this.accountName, this.balance, this.currency, this.rake);

            Assert.That(
                () => account.AddBettingLine(null),
                Throws.ArgumentNullException.With.Message.Contains(string.Format(EngineConstants.ObjectCannotBeNullErrorMessage, "Line")));
        }

        [Test]
        public void AddBettingLine_ShouldThrowArgumentException_WhenTryingToAddSameLineTwice()
        {
            var mockedLine = new Mock<IBetLine>();
            var account = new BetfairAccount(this.accountName, this.balance, this.currency, this.rake);
            account.BettingLines.Add(mockedLine.Object);
        
            Assert.That(
                () => account.AddBettingLine(mockedLine.Object),
                Throws.ArgumentException.With.Message.Contains(EngineConstants.SameLinesToAnAcountErrorMessage));
        }

        [Test]
        public void RemoveBettingLine_ShouldThrowNullArgumentException_WithNullLine()
        {
            var account = new BetfairAccount(this.accountName, this.balance, this.currency, this.rake);

            Assert.That(
                () => account.RemoveBettingLine(null),
                Throws.ArgumentNullException.With.Message.Contains(string.Format(EngineConstants.ObjectCannotBeNullErrorMessage, "Line")));
        }

        [Test]
        public void RemoveBettingLine_ShouldThrowArgumentException_WhenTryingToRemoveExistingLine()
        {
            var mockedLine = new Mock<IBetLine>();
            var account = new BetfairAccount(this.accountName, this.balance, this.currency, this.rake);
            account.BettingLines.Add(mockedLine.Object);

            Assert.AreEqual(1, account.BettingLines.Count);

            account.RemoveBettingLine(mockedLine.Object);

            Assert.AreEqual(0, account.BettingLines.Count);
        }

        [Test]
        public void DepositBalance_ShouldAddCorrectlyAmountToAccountBalance()
        {
            var account = new BetfairAccount(this.accountName, this.balance, this.currency, this.rake);
            var depositedValue = 100m;

            account.DepositBalance(depositedValue);

            Assert.AreEqual(this.balance + depositedValue, account.Balance);
        }

        [Test]
        public void Deposit_ShouldThrowArgumentException_WithNegativeAmount()
        {
            var account = new BetfairAccount(this.accountName, this.balance, this.currency, this.rake);
            var depositValue = -10m;

            Assert.That(
             () => account.DepositBalance(depositValue),
             Throws.ArgumentException.With.Message.Contains(string.Format(EngineConstants.SomeValueCannotBeNegativeErrorMessage, "Deposit value")));
        }

        [Test]
        public void WithdrawBalance_ShouldSubtractCorrectlyAmountToAccountBalance()
        {
            var account = new BetfairAccount(this.accountName, this.balance, this.currency, this.rake);
            var withdrawValue = 50m;

            account.WithdrawBalance(withdrawValue);

            Assert.AreEqual(this.balance - withdrawValue, account.Balance);
        }

        [Test]
        public void Withdraw_ShouldThrowArgumentException_WithNegativeAmount()
        {
            var account = new BetfairAccount(this.accountName, this.balance, this.currency, this.rake);
            var withdrawValue = -10m;

            Assert.That(
             () => account.WithdrawBalance(withdrawValue),
             Throws.ArgumentException.With.Message.Contains(string.Format(EngineConstants.SomeValueCannotBeNegativeErrorMessage, "Withdraw value")));
        }

        [Test]
        public void WithdrawBalance_ShouldThrowArgumentException_WithGreaterAmountToBeWithdrawFromAccount()
        {
            var account = new BetfairAccount(this.accountName, this.balance, this.currency, this.rake);
            var withdrawValue = 5000m;

            Assert.That(
             () => account.WithdrawBalance(withdrawValue),
             Throws.ArgumentException.With.Message.Contains(string.Format(EngineConstants.SomeValueCannotBeNegativeErrorMessage, "Balance")));
        }
        
    }
}
