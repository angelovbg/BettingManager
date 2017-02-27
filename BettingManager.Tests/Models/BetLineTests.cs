namespace BettingManager.Tests.Models
{
    using Logic.Models;
    using Logic.Common.Constants;
    using Logic.Contracts.Models;
    using Moq;
    using NUnit.Framework;

    [TestFixture]
    public class BetLineTests
    {     
        private IAccount account = new Mock<IAccount>().Object;
        private string lineName = "First";
        private decimal stepAmount = 2m;
        private IBet mockedBet = new Mock<IBet>().Object;
        private decimal decreasingStepValue = 3;

        [Test]
        public void Constructor_ShouldPass_WithValidParams()
        {
            var betLine = new BetLine(this.account, this.lineName, this.stepAmount, this.decreasingStepValue);

            Assert.IsInstanceOf<BetLine>(betLine);
        }

        [Test]
        public void AddBet_ShouldThrowNullArgumentException_WithNullBet()
        {
            var betLine = new BetLine(this.account, this.lineName, this.stepAmount, this.decreasingStepValue);

            Assert.That(
                () => betLine.AddBet(null),
                Throws.ArgumentNullException.With.Message.Contains(string.Format(EngineConstants.ObjectCannotBeNullErrorMessage, "Bet")));
        }

        [Test]
        public void AddBet_ShouldThrowArgumentException_WhenTryingToAddSameBetTwice()
        {
            var betLine = new BetLine(this.account, this.lineName, this.stepAmount, this.decreasingStepValue);
            betLine.Bets.Add(this.mockedBet);

            Assert.That(
                () => betLine.AddBet(this.mockedBet),
                Throws.ArgumentException.With.Message.Contains(EngineConstants.SameBetToALineErrorMessage));
        }

        [Test]
        public void RemoveBet_ShouldThrowNullArgumentException_WithNullBet()
        {
            var betLine = new BetLine(this.account, this.lineName, this.stepAmount, this.decreasingStepValue);

            Assert.That(
                () => betLine.RemoveBet(null),
                Throws.ArgumentNullException.With.Message.Contains(string.Format(EngineConstants.ObjectCannotBeNullErrorMessage, "Bet")));
        }

        [Test]
        public void RemoveBet_ShouldThrowArgumentException_WhenTryingToRemoveExistingBet()
        {
            var betLine = new BetLine(this.account, this.lineName, this.stepAmount, this.decreasingStepValue);
            betLine.Bets.Add(this.mockedBet);

            Assert.AreEqual(1, betLine.Bets.Count);

            betLine.RemoveBet(this.mockedBet);

            Assert.AreEqual(0, betLine.Bets.Count);
        }
        
    }
}
