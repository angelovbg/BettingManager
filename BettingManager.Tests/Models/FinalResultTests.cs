namespace BettingManager.Tests.Models
{
    using Logic.Models;
    using Logic.Common.Constants;
    using Logic.Contracts.Models;
    using Moq;
    using NUnit.Framework;

    [TestFixture]
    public class FinalResultTests
    {
        private IMatch mockedMatch = new Mock<IMatch>().Object;
        private int homeTeamResult = 3;
        private int visitorTeamResult = 1;

        [Test]
        public void Constructor_ShouldPass_WithValidParams()
        {
            var finalResult = new FinalResult(this.mockedMatch, this.homeTeamResult, this.visitorTeamResult);

            Assert.IsInstanceOf<FinalResult>(finalResult);
        }

        [Test]
        public void Constructor_ShouldThrowArgumentNullException_WithNullMatch()
        {
            Assert.That(
               () => new FinalResult(null, this.homeTeamResult, this.visitorTeamResult),
               Throws.ArgumentNullException.With.Message.Contains(string.Format(EngineConstants.ObjectCannotBeNullErrorMessage, "Match")));
        }

        [Test]
        public void Constructor_ShouldThrowArgumentException_WithNegativeHomeScore()
        {
            Assert.That(
               () => new FinalResult(this.mockedMatch, -1, this.visitorTeamResult),
               Throws.ArgumentException.With.Message.Contains(string.Format(EngineConstants.SomeValueCannotBeNegativeErrorMessage, "Home score")));
        }

        [Test]
        public void Constructor_ShouldThrowArgumentException_WithNegativeVisitorScore()
        {
            Assert.That(
               () => new FinalResult(this.mockedMatch, this.homeTeamResult, -1),
               Throws.ArgumentException.With.Message.Contains(string.Format(EngineConstants.SomeValueCannotBeNegativeErrorMessage, "Visitor score")));
        }

        [Test]
        public void GetMatch_CorrectlySetMatchByConstructor()
        {
            var finalResult = new FinalResult(this.mockedMatch, this.homeTeamResult, this.visitorTeamResult);

            Assert.AreEqual(this.mockedMatch, finalResult.Match);
        }

        [Test]
        public void GetHomeScore_CorrectlySetHomeScoreByConstructor()
        {
            var finalResult = new FinalResult(this.mockedMatch, this.homeTeamResult, this.visitorTeamResult);

            Assert.AreEqual(this.homeTeamResult, finalResult.HomeTeamScore);
        }

        [Test]
        public void GetVisitorScore_CorrectlySetVisitorScoreByConstructor()
        {
            var finalResult = new FinalResult(this.mockedMatch, this.homeTeamResult, this.visitorTeamResult);

            Assert.AreEqual(this.visitorTeamResult, finalResult.VisitorTeamScore);
        }

        [TestCase(4, 1)]
        [TestCase(3, 2)]
        [TestCase(2, 1)]
        [TestCase(1, 3)]
        [TestCase(0, 0)]
        [TestCase(1, 1)]
        [TestCase(3, 1)]
        [TestCase(2, 2)]
        public void GetTotalGoalsScored_CorrectlySetTotalGoalsScoredByConstructor(int homeScore, int visitorScore)
        {
            var finalResult = new FinalResult(this.mockedMatch, homeScore, visitorScore);

            Assert.AreEqual(homeScore + visitorScore, finalResult.TotalGoalsScored);
        }

        [TestCase(4, 1)]
        [TestCase(3, 2)]
        [TestCase(2, 1)]
        [TestCase(1, 0)]
        public void GetDoubleMark_ShouldReturnValidResults_WithHomeWinResult(int homeScore, int visitorScore)
        {
            var finalResult = new FinalResult(this.mockedMatch, homeScore, visitorScore);
            var results = finalResult.GetDoubleMark();

            Assert.AreEqual("_1X", results[0]);
            Assert.AreEqual("_12", results[1]);
        }

        [TestCase(0, 0)]
        [TestCase(1, 1)]
        [TestCase(2, 2)]
        public void GetDoubleMark_ShouldReturnValidResults_WithDrawResult(int homeScore, int visitorScore)
        {
            var finalResult = new FinalResult(this.mockedMatch, homeScore, visitorScore);
            var results = finalResult.GetDoubleMark();

            Assert.AreEqual("_1X", results[0]);
            Assert.AreEqual("X2", results[1]);
        }

        [TestCase(0, 1)]
        [TestCase(2, 4)]
        [TestCase(1, 3)]
        [TestCase(2, 3)]
        public void GetDoubleMark_ShouldReturnValidResults_WithVisitorWinResult(int homeScore, int visitorScore)
        {
            var finalResult = new FinalResult(this.mockedMatch, homeScore, visitorScore);
            var results = finalResult.GetDoubleMark();

            Assert.AreEqual("X2", results[0]);
            Assert.AreEqual("_12", results[1]);
        }     
    }
}
