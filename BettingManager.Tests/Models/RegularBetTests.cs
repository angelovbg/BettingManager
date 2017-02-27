namespace BettingManager.Tests.Models
{
    using Logic.Models;
    using Logic.Common.Constants;
    using Logic.Contracts.Models;
    using Moq;
    using NUnit.Framework;

    [TestFixture]
    public class RegularBetTests
    {
        private IMatch mockedMatch = new Mock<IMatch>().Object;
        private Mock<IBetLine> mockedLine = new Mock<IBetLine>();
        private ITipster mockedTipster = new Mock<ITipster>().Object;

        //private FinalResultMarks mark = FinalResultMarks._1;
        private string mark = "_1";
        private decimal amount = 10;
        private decimal coefficient = 3m;
        private decimal lineValueToBeWin = 20m;
        private decimal AccountBalance = 100m;
        private BetType betType = BetType.Regular;

        private void ResetParameters()
        {
            this.mockedLine.Reset();
        }

        [Test]
        public void Constructor_ShouldThrowArgumentNullException_withNullMatch()
        {
            this.ResetParameters();
            Assert.That(
                () => new RegularBet(null, this.mockedLine.Object, this.mark, this.amount, this.coefficient, this.mockedTipster),
                Throws.ArgumentNullException.With.Message.Contains(string.Format(EngineConstants.ObjectCannotBeNullErrorMessage, "Match")));
        }

        [Test]
        public void Constructor_ShouldThrowArgumentNullException_withNullLine()
        {
            this.ResetParameters();
            Assert.That(
                () => new RegularBet(this.mockedMatch, null, this.mark, this.amount, this.coefficient, this.mockedTipster),
                Throws.ArgumentNullException.With.Message.Contains(string.Format(EngineConstants.ObjectCannotBeNullErrorMessage, "Line")));
        }

        [Test]
        public void Constructor_ShouldThrowArgumentNullException_withNullTipster()
        {
            this.ResetParameters();
            Assert.That(
                () => new RegularBet(this.mockedMatch, this.mockedLine.Object, this.mark, this.amount, this.coefficient, null),
                Throws.ArgumentNullException.With.Message.Contains(string.Format(EngineConstants.ObjectCannotBeNullErrorMessage, "Tipster")));
        }

        [Test]
        public void Constructor_ShouldThrowArgumentException_withNegativeAmount()
        {
            this.ResetParameters();
            this.mockedLine.Setup(x => x.Account.Balance).Returns(100);
            
            Assert.That(
                () => new RegularBet(this.mockedMatch, this.mockedLine.Object, this.mark, -20m, this.coefficient, this.mockedTipster),
                Throws.ArgumentException.With.Message.Contains(EngineConstants.BetAmountCannotBeNegativeErrorMessage));
        }

        [Test]
        public void Constructor_ShouldThrowArgumentException_withZeroAmount()
        {
            this.ResetParameters();
            this.mockedLine.Setup(x => x.Account.Balance).Returns(100);

            Assert.That(
                () => new RegularBet(this.mockedMatch, this.mockedLine.Object, this.mark, 0, this.coefficient, this.mockedTipster),
                Throws.ArgumentException.With.Message.Contains(EngineConstants.BetAmountCannotBeNegativeErrorMessage));
        }

        [Test]
        public void Constructor_ShouldThrowArgumentException_WitoVeryBigAmount()
        {
            this.ResetParameters();
            this.mockedLine.Setup(x => x.Account.Balance).Returns(100);

            Assert.That(
                () => new RegularBet(this.mockedMatch, this.mockedLine.Object, this.mark, 100000, this.coefficient, this.mockedTipster),
                Throws.ArgumentException.With.Message.Contains(EngineConstants.BetAmountIsTooBigErrorMessage));
        }

        [Test]
        public void Constructor_ShouldThrowArgumentException_withCoefficientLessThenOne()
        {
            this.ResetParameters();
            this.mockedLine.Setup(x => x.Account.Balance).Returns(100);

            Assert.That(
                () => new RegularBet(this.mockedMatch, this.mockedLine.Object, this.mark, this.amount, 0.6m, this.mockedTipster),
                Throws.ArgumentException.With.Message.Contains(EngineConstants.BetCoefficientCannotBeLessThenOneErrorMessage));
        }

        [Test]
        public void Constructor_ShouldPass_withValidParams()
        {
            this.ResetParameters();
            this.mockedLine.Setup(x => x.Account.Balance).Returns(100m);

            var regularBet = new RegularBet(this.mockedMatch, this.mockedLine.Object, "_1", 1, 1, this.mockedTipster);

            Assert.IsInstanceOf<RegularBet>(regularBet);
        }

        [Test]
        public void Constructor_ShouldThrowArgumentException_WithAlreadyCompleteLine()
        {
            this.ResetParameters();
            this.mockedLine.Setup(x => x.Account.Balance).Returns(100);
            this.mockedLine.Setup(x => x.HasCompleteBet).Returns(true);

            Assert.That(
                () => new RegularBet(this.mockedMatch, this.mockedLine.Object, this.mark, this.amount, this.coefficient, this.mockedTipster),
                Throws.ArgumentException.With.Message.Contains(EngineConstants.AlreadyCompletedLineErrorMessage));
        }

        [Test]
        public void Constructor_ShouldThrowArgumentException_WithGreaterThenNeededBet()
        {
            this.ResetParameters();
            this.mockedLine.Setup(x => x.Account.Balance).Returns(100m);
            this.mockedLine.Setup(x => x.LeftValueToBeBet()).Returns(0m);

            Assert.That(
               () => new RegularBet(this.mockedMatch, this.mockedLine.Object, this.mark, this.amount, this.coefficient, this.mockedTipster),
               Throws.ArgumentException.With.Message.Contains(EngineConstants.BetIsGreaterThenNeededErrorMessage));
        }

        [Test]
        public void Constructor_ShouldThrowArgumentException_NotEnoughAccountBalance()
        {
            this.ResetParameters();
            this.mockedLine.Setup(x => x.Account.Balance).Returns(1m);
            this.mockedLine.Setup(x => x.LeftValueToBeBet()).Returns(this.lineValueToBeWin);

            Assert.That(
               () => new RegularBet(this.mockedMatch, this.mockedLine.Object, this.mark, this.amount, this.coefficient, this.mockedTipster),
               Throws.ArgumentException.With.Message.Contains(EngineConstants.NotEnoughAccountBalanceForBetErrorMessage));
        }

        [Test]
        public void GetAmount_CorrectlySetAmountByConstructor()
        {
            this.ResetParameters();
            this.mockedLine.Setup(x => x.Account.Balance).Returns(100m);
            this.mockedLine.Setup(x => x.LeftValueToBeBet()).Returns(this.lineValueToBeWin);

            var regularBet = new RegularBet(this.mockedMatch, this.mockedLine.Object, this.mark, this.amount, this.coefficient, this.mockedTipster);

            Assert.AreEqual(this.amount, regularBet.Amount);
        }

        [Test]
        public void GetCoefficient_CorrectlySetCoefficientByConstructor()
        {
            this.ResetParameters();
            this.mockedLine.Setup(x => x.Account.Balance).Returns(100m);
            this.mockedLine.Setup(x => x.LeftValueToBeBet()).Returns(this.lineValueToBeWin);

            var regularBet = new RegularBet(this.mockedMatch, this.mockedLine.Object, this.mark, this.amount, this.coefficient, this.mockedTipster);

            Assert.AreEqual(this.coefficient, regularBet.Coefficient);
        }

        [Test]
        public void GetMatch_CorrectlySetMatchByConstructor()
        {
            this.ResetParameters();
            this.mockedLine.Setup(x => x.Account.Balance).Returns(100m);
            this.mockedLine.Setup(x => x.LeftValueToBeBet()).Returns(this.lineValueToBeWin);

            var regularBet = new RegularBet(this.mockedMatch, this.mockedLine.Object, this.mark, this.amount, this.coefficient, this.mockedTipster);

            Assert.AreEqual(this.mockedMatch, regularBet.Match);
        }

        [Test]
        public void GetLine_CorrectlySetLineByConstructor()
        {
            this.ResetParameters();
            this.mockedLine.Setup(x => x.Account.Balance).Returns(100m);
            this.mockedLine.Setup(x => x.LeftValueToBeBet()).Returns(this.lineValueToBeWin);

            var regularBet = new RegularBet(this.mockedMatch, this.mockedLine.Object, this.mark, this.amount, this.coefficient, this.mockedTipster);

            Assert.AreEqual(this.mockedLine.Object, regularBet.Line);
        }

        [Test]
        public void GetCurrentBetType_CorrectlySetCurrentBetTypeByConstructor()
        {
            this.ResetParameters();
            this.mockedLine.Setup(x => x.Account.Balance).Returns(100m);
            this.mockedLine.Setup(x => x.LeftValueToBeBet()).Returns(this.lineValueToBeWin);

            var regularBet = new RegularBet(this.mockedMatch, this.mockedLine.Object, this.mark, this.amount, this.coefficient, this.mockedTipster);

            Assert.AreEqual(regularBet.CurrentBetType, this.betType);
        }

        [Test]
        public void GetMark_CorrectlySetMarkByConstructor()
        {
            this.ResetParameters();
            this.mockedLine.Setup(x => x.Account.Balance).Returns(100m);
            this.mockedLine.Setup(x => x.LeftValueToBeBet()).Returns(this.lineValueToBeWin);

            var regularBet = new RegularBet(this.mockedMatch, this.mockedLine.Object, this.mark, this.amount, this.coefficient, this.mockedTipster);

            Assert.AreEqual(regularBet.Mark, this.mark.ToString());
        }

        [Test]
        public void GetTipster_CorrectlySetTipsterByConstructor()
        {
            this.ResetParameters();
            this.mockedLine.Setup(x => x.Account.Balance).Returns(100m);
            this.mockedLine.Setup(x => x.LeftValueToBeBet()).Returns(this.lineValueToBeWin);

            var regularBet = new RegularBet(this.mockedMatch, this.mockedLine.Object, this.mark, this.amount, this.coefficient, this.mockedTipster);

            Assert.AreEqual(regularBet.Tipster, this.mockedTipster);
        }

        [Test]
        public void GetResultType_CorrectlySetResultTypeByConstructor()
        {
            this.ResetParameters();
            this.mockedLine.Setup(x => x.Account.Balance).Returns(100m);
            this.mockedLine.Setup(x => x.LeftValueToBeBet()).Returns(this.lineValueToBeWin);

            var regularBet = new RegularBet(this.mockedMatch, this.mockedLine.Object, this.mark, this.amount, this.coefficient, this.mockedTipster);

            Assert.AreEqual(regularBet.ResultType, ResultType.Final);
        }

        [Test]
        public void SetupBalance_ShouldThrowArgumentException_WithUncheckedBet()
        {
            this.ResetParameters();
            this.mockedLine.Setup(x => x.Account.Balance).Returns(this.AccountBalance);
            this.mockedLine.Setup(x => x.LeftValueToBeBet()).Returns(this.lineValueToBeWin);

            var regularBet = new RegularBet(this.mockedMatch, this.mockedLine.Object, this.mark, this.amount, this.coefficient, this.mockedTipster);
            regularBet.IsChecked = false;

            Assert.That(
               () => regularBet.SetupBalances(),
               Throws.ArgumentException.With.Message.Contains(EngineConstants.NotCheckedBetErrorMessage));
        }

        //TODO
        [Test]
        public void SetupBalance_ShouldIncreaseAccountBalance_WithWinningBet()
        {
            this.ResetParameters();
            this.mockedLine.Setup(x => x.Account.Balance).Returns(this.AccountBalance);
            this.mockedLine.Setup(x => x.LeftValueToBeBet()).Returns(this.lineValueToBeWin);

            var regularBet = new RegularBet(this.mockedMatch, this.mockedLine.Object, this.mark, this.amount, this.coefficient, this.mockedTipster);
            regularBet.IsWin = true;

            Assert.IsTrue(regularBet.Line.Account.Balance > this.AccountBalance);
        }
    }
}
