namespace BettingManager.Tests.Controllers
{
    using Logic.Controllers;
    using Logic.Models;
    using Logic.Common.Constants;
    using Logic.Contracts.Models;

    using Moq;
    using NUnit.Framework;
    using System.Collections.Generic;

    [TestFixture]
    public class BetControllerTests
    {
        
        private decimal amount = 8m;
        private decimal coefficient = 3m;
        private decimal startAccountValue = 100m;
        private decimal lineValueToBeWin = 20m;
        private FinalResultMarks mark = FinalResultMarks._1;
        private Mock<IMatch> mockedMatch = new Mock<IMatch>();
        private Mock<IBetLine> mockedLine = new Mock<IBetLine>();
        private Mock<ITipster> mockedTipster = new Mock<ITipster>();
        private List<string> parameters = new List<string>()
            {
                "",
                "",
                "_1",
                "10",
                "2.5"
            };

        private string accountName = "Test";
        private decimal accountBalance = 100m;
        private CurrencyType accountCurrency = CurrencyType.EUR;
        private decimal accountRake = 0.04m;

        private string lineName = "First";
        private decimal lineStepAmount = 20m;
        private decimal decreasingStepValue = 2m;

        private void ResetParams()
        {
            this.mockedMatch.Reset();
        }

        //[TestCase("_1")]
        //[TestCase("1")]
        //[TestCase("X")]
        //[TestCase("2")]
        //[TestCase("_2")]
        //public void AddRegularBet_ShouldGenerateCorrecBet_WithValidParams(string input)
        //{
        //    this.ResetParams();
        //    this.parameters[2] = input;
        //    this.mockedLine.Setup(x => x.Account.Balance).Returns(100m);
        //    this.mockedLine.Setup(x => x.LeftValueToBeBet()).Returns(lineValueToBeWin);

        //    var betController = new BetController();

        //    var bet = betController.AddRegularBet(parameters, this.mockedMatch.Object, this.mockedLine.Object, this.mockedTipster.Object);
        //    Assert.AreEqual(typeof(RegularBet), bet.GetType());
        //}

        //[TestCase("-1")]
        //[TestCase("x")]
        //[TestCase("H")]
        //[TestCase("Х")]
        //[TestCase("!2")]
        //public void AddRegularBet_ShouldThrowArgumentException_WithInvalidMark(string input)
        //{
        //    this.ResetParams();
        //    this.parameters[2] = input;
        //    this.mockedLine.Setup(x => x.Account.Balance).Returns(100m);
        //    this.mockedLine.Setup(x => x.LeftValueToBeBet()).Returns(lineValueToBeWin);

        //    var betController = new BetController();

        //    Assert.That(
        //        () => betController.AddRegularBet(this.parameters, this.mockedMatch.Object, this.mockedLine.Object, this.mockedTipster.Object),
        //        Throws.ArgumentException.With.Message.Contains(EngineConstants.InvalidMarkTypeErrorMessage));
        //}


        //[Test]
        //public void AddRegularBet_ShouldThrowSystemFormatException_WithInvalidAmountType()
        //{
        //    this.ResetParams();
        //    this.parameters[2] = "_1";
        //    this.parameters[3] = "wrong param";
        //    this.mockedLine.Setup(x => x.Account.Balance).Returns(100m);
        //    this.mockedLine.Setup(x => x.LeftValueToBeBet()).Returns(lineValueToBeWin);

        //    var betController = new BetController();

        //    Assert.Throws<System.FormatException>(() => betController.AddRegularBet(this.parameters, this.mockedMatch.Object, this.mockedLine.Object, this.mockedTipster.Object));
        //}

        //[Test]
        //public void AddRegularBet_ShouldThrowSystemFormatException_WithInvalidCoefficientType()
        //{
        //    this.ResetParams();
        //    this.parameters[2] = "_1";
        //    this.parameters[4] = "wrong param";
        //    this.mockedLine.Setup(x => x.Account.Balance).Returns(100m);
        //    this.mockedLine.Setup(x => x.LeftValueToBeBet()).Returns(lineValueToBeWin);

        //    var betController = new BetController();

        //    Assert.Throws<System.FormatException>(() => betController.AddRegularBet(this.parameters, this.mockedMatch.Object, this.mockedLine.Object, this.mockedTipster.Object));
        //}

        [Test]
        public void SetupCheckedBets_ShouldSetBetCheckedPropertyFalse_WithMatchWithoutResults()
        {
            this.ResetParams();
            this.mockedLine.Setup(x => x.Account.Balance).Returns(100m);
            this.mockedLine.Setup(x => x.LeftValueToBeBet()).Returns(this.lineValueToBeWin);

            var firstBet = new RegularBet(this.mockedMatch.Object, this.mockedLine.Object, mark.ToString(), this.amount, this.coefficient, this.mockedTipster.Object);

            var collectionOfBets = new List<IBet>() { firstBet };

            var betController = new BetController();
            betController.SetupCheckedBets(collectionOfBets);

            Assert.IsFalse(firstBet.IsChecked);
        }

        [Test]
        public void SetupCheckedBets_ShouldSetBetCheckedProperty_WithValidParams()
        {
            this.ResetParams();
            this.mockedLine.Setup(x => x.Account.Balance).Returns(100m);
            this.mockedLine.Setup(x => x.LeftValueToBeBet()).Returns(this.lineValueToBeWin);

            var bet = new RegularBet(this.mockedMatch.Object, this.mockedLine.Object, mark.ToString(), this.amount, this.coefficient, this.mockedTipster.Object);

            var mockedResult = new Mock<IFinalResult>();
            mockedResult.Setup(r => r.GetMark()).Returns("_1");

            this.mockedMatch.Setup(m => m.Results).Returns(new Dictionary<ResultType, IResult>() { [ResultType.Final] = mockedResult.Object });

            var collectionOfBets = new List<IBet>() { bet };

            var betController = new BetController();
            betController.SetupCheckedBets(collectionOfBets);

            Assert.IsTrue(bet.IsChecked);
        }

        [TestCase("_1", FinalResultMarks._1)]
        [TestCase("X", FinalResultMarks.X)]
        [TestCase("_2", FinalResultMarks._2)]
        public void SetupCheckedBets_ShouldSetWinningBet_WithValidParams(string inputString, FinalResultMarks finalResultMark)
        {
            this.ResetParams();
            this.mockedLine.Setup(x => x.Account.Balance).Returns(100m);

            var bet = new RegularBet(this.mockedMatch.Object, this.mockedLine.Object, finalResultMark.ToString(), this.amount, this.coefficient, this.mockedTipster.Object);

            var mockedResult = new Mock<IFinalResult>();
            mockedResult.Setup(r => r.GetMark()).Returns(inputString);

            this.mockedMatch.Setup(m => m.Results).Returns(new Dictionary<ResultType, IResult>() { [ResultType.Final] = mockedResult.Object });

            var collectionOfBets = new List<IBet>() { bet };

            var betController = new BetController();
            betController.SetupCheckedBets(collectionOfBets);

            Assert.IsTrue(bet.IsWin);
        }

        [TestCase("_1", FinalResultMarks.X)]
        [TestCase("_1", FinalResultMarks._2)]
        [TestCase("X", FinalResultMarks._1)]
        [TestCase("X", FinalResultMarks._2)]
        [TestCase("_2", FinalResultMarks.X)]
        [TestCase("_2", FinalResultMarks._1)]
        public void SetupCheckedBets_ShouldSetWinningBetFalse_WithValidParams(string inputString, FinalResultMarks finalResultMark)
        {
            this.ResetParams();
            this.mockedLine.Setup(x => x.Account.Balance).Returns(100m);

            var bet = new RegularBet(this.mockedMatch.Object, this.mockedLine.Object, finalResultMark.ToString(), this.amount, this.coefficient, this.mockedTipster.Object);

            var mockedResult = new Mock<IFinalResult>();
            mockedResult.Setup(r => r.GetMark()).Returns(inputString);

            this.mockedMatch.Setup(m => m.Results).Returns(new Dictionary<ResultType, IResult>() { [ResultType.Final] = mockedResult.Object });

            var collectionOfBets = new List<IBet>() { bet };

            var betController = new BetController();
            betController.SetupCheckedBets(collectionOfBets);

            Assert.IsFalse(bet.IsWin);
        }

        [TestCase("_1", FinalResultMarks._1)]
        [TestCase("X", FinalResultMarks.X)]
        [TestCase("_2", FinalResultMarks._2)]
        public void SetupCheckedBets_ShouldSetCorrectWinningBetAccountBalance_WithValidParams(string inputString, FinalResultMarks finalResultMark)
        {
            this.ResetParams();
            var account = new BetfairAccount(this.accountName, this.accountBalance, this.accountCurrency, this.accountRake);
            var line = new BetLine(account, this.lineName, this.lineStepAmount, this.decreasingStepValue);
            var bet = new RegularBet(this.mockedMatch.Object, line, finalResultMark.ToString(), this.amount, this.coefficient, this.mockedTipster.Object);

            var mockedResult = new Mock<IFinalResult>();
            mockedResult.Setup(r => r.GetMark()).Returns(inputString);

            this.mockedMatch.Setup(m => m.Results).Returns(new Dictionary<ResultType, IResult>() { [ResultType.Final] = mockedResult.Object });

            var collectionOfBets = new List<IBet>() { bet };

            var betController = new BetController();
            betController.SetupCheckedBets(collectionOfBets);

            Assert.IsTrue(bet.Line.Account.Balance > 110m);
        }

        [TestCase("_1", FinalResultMarks.X)]
        [TestCase("_1", FinalResultMarks._2)]
        [TestCase("X", FinalResultMarks._1)]
        [TestCase("X", FinalResultMarks._2)]
        [TestCase("_2", FinalResultMarks.X)]
        [TestCase("_2", FinalResultMarks._1)]
        public void SetupCheckedBets_ShouldSetCorrectLosingBetAccountBalance_WithValidParams(string inputString, FinalResultMarks finalResultMark)
        {
            this.ResetParams();
            var account = new BetfairAccount(this.accountName, this.accountBalance, this.accountCurrency, this.accountRake);
            var line = new BetLine(account, this.lineName, this.lineStepAmount, this.decreasingStepValue);
            var bet = new RegularBet(this.mockedMatch.Object, line, finalResultMark.ToString(), this.amount, this.coefficient, this.mockedTipster.Object);

            var mockedResult = new Mock<IFinalResult>();
            mockedResult.Reset();
            mockedResult.Setup(r => r.GetMark()).Returns(inputString);

            
            this.mockedMatch.Setup(m => m.Results).Returns(new Dictionary<ResultType, IResult>() { [ResultType.Final] = mockedResult.Object });

            var collectionOfBets = new List<IBet>() { bet };

            var betController = new BetController();
            betController.SetupCheckedBets(collectionOfBets);

            Assert.AreEqual(startAccountValue - this.amount, bet.Line.Account.Balance);
        }

        [Test]
        public void Constructor_ShouldThrowArgumentException_WhenAddRegularBetWithMatchWithResult()
        {
            this.ResetParams();
            this.mockedLine.Setup(x => x.Account.Balance).Returns(100m);
            this.mockedLine.Setup(x => x.LeftValueToBeBet()).Returns(this.lineValueToBeWin);

            var mockedResult = new Mock<IResult>();
            this.mockedMatch.Setup(m => m.Results).Returns(new Dictionary<ResultType, IResult>() { [ResultType.Final] = mockedResult.Object });

            Assert.That(
                        () => new RegularBet(this.mockedMatch.Object, this.mockedLine.Object, mark.ToString(), this.amount, this.coefficient, this.mockedTipster.Object),
                        Throws.ArgumentException.With.Message.Contains(EngineConstants.CannotAddBetToMatchWithResultErrorMessage));
        }

        [Test]
        public void Constructor_ShouldAddRegularBet_WhenAddRegularBetWithMatchWithHalfTimeResult()
        {
            this.ResetParams();
            this.mockedLine.Setup(x => x.Account.Balance).Returns(100m);
            this.mockedLine.Setup(x => x.LeftValueToBeBet()).Returns(this.lineValueToBeWin);

            var mockedResult = new Mock<IResult>();
            this.mockedMatch.Setup(m => m.Results).Returns(new Dictionary<ResultType, IResult>() { [ResultType.FirstHalf] = mockedResult.Object });

            var regularBet = new RegularBet(this.mockedMatch.Object, this.mockedLine.Object, mark.ToString(), this.amount, this.coefficient, this.mockedTipster.Object);

            Assert.IsInstanceOf<RegularBet>(regularBet);
        }     
    }
}