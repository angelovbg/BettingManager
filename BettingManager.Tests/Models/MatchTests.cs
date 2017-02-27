namespace BettingManager.Tests.Models
{
    using System;

    using Logic.Models;
    using Logic.Common.Constants;
    using Logic.Contracts.Models;

    using Moq;
    using NUnit.Framework;

    [TestFixture]
    public class MatchTests
    {
        private SportType sport = SportType.Football;
        private League league = League.PremierLeague;
        private DateTime date = DateTime.Now;
        private string homeTeam = "Chelsea";
        private string visitorTeam = "Arsenal";
        private IBet mockedBet = new Mock<IBet>().Object;

        [Test]
        public void GetId_ShouldSetCorrectlyUniqueIds_withValidParams()
        {
            var mockedBet = new Mock<IBet>();

            var firstMatch = new Logic.Models.Match(this.sport, this.league, this.date.AddDays(4), this.homeTeam, this.visitorTeam);
            var secondMatch = new Logic.Models.Match(this.sport, this.league, this.date.AddDays(9), this.homeTeam, this.visitorTeam);
            var thirdMatch = new Logic.Models.Match(this.sport, this.league, this.date.AddDays(14), this.homeTeam, this.visitorTeam);

            var startId = firstMatch.Id;

            Assert.AreEqual(startId, firstMatch.Id);
            Assert.AreEqual(startId + 1, secondMatch.Id);
            Assert.AreEqual(startId + 2, thirdMatch.Id);
        }
        
        [Test]
        public void Controller_ShouldPass_WithValidParams()
        {
            var match = new Logic.Models.Match(this.sport, this.league, this.date.AddDays(5), this.homeTeam, this.visitorTeam);

            Assert.IsInstanceOf<Logic.Models.Match>(match);
        }

        [Test]
        public void GetSport_CorrectlySetSportByConstructor()
        {
            var match = new Logic.Models.Match(this.sport, this.league, this.date.AddDays(5), this.homeTeam, this.visitorTeam);

            Assert.AreEqual(this.sport, match.Sport);
        }

        [Test]
        public void GetLeague_CorrectlySetLeagueByConstructor()
        {
            var match = new Logic.Models.Match(this.sport, this.league, this.date.AddDays(5), this.homeTeam, this.visitorTeam);

            Assert.AreEqual(this.league, match.League);
        }
    
        [Test]
        public void GetDate_CorrectlySetDateByConstructor()
        {
            var match = new Logic.Models.Match(this.sport, this.league, this.date, this.homeTeam, this.visitorTeam);

            Assert.AreEqual(this.date, match.Date);
        }

        [Test]
        public void GetHomeTeam_CorrectlySetHomeTeamByConstructor()
        {
            var match = new Logic.Models.Match(this.sport, this.league, this.date.AddDays(5), this.homeTeam, this.visitorTeam);

            Assert.AreEqual(this.homeTeam, match.HomeTeam);
        }

        [Test]
        public void GetVisitorTeam_CorrectlySetVisitorTeamByConstructor()
        {
            var match = new Logic.Models.Match(this.sport, this.league, this.date.AddDays(5), this.homeTeam, this.visitorTeam);

            Assert.AreEqual(this.visitorTeam, match.VisitorTeam);
        }

        [Test]
        public void Constructor_ShouldThrowArgumentException_withSameTeams()
        {
            var homeTeam = "Chelsea";
            var visitorTeam = "Chelsea";

            Assert.That(
                () => new Logic.Models.Match(this.sport, this.league, this.date.AddDays(5), homeTeam, visitorTeam),
                Throws.ArgumentException.With.Message.Contains(EngineConstants.SameTeamsInMatchErrorMessage));
        }

        [Test]
        public void AddBet_ShouldThrowArgumentNullException_withNullBet()
        {
            var match = new Logic.Models.Match(this.sport, this.league, this.date.AddDays(5), this.homeTeam, this.visitorTeam);

            Assert.That(
                () => match.AddBet(null),
                Throws.ArgumentNullException.With.Message.Contains(string.Format(EngineConstants.ObjectCannotBeNullErrorMessage, "Bet")));
        }

        [Test]
        public void AddBet_ShouldThrowArgumentException_withBetSameBetsAdded()
        {
            var match = new Logic.Models.Match(this.sport, this.league, this.date.AddDays(5), this.homeTeam, this.visitorTeam);
            var mockedBet = new Mock<IBet>();

            match.AddBet(mockedBet.Object);

            Assert.That(
                () => match.AddBet(mockedBet.Object),
                Throws.ArgumentException.With.Message.Contains(EngineConstants.SameBetsForAMatchErrorMessage));
        }

        [Test]
        public void AddBet_ShouldAddCorrectToCollection_withValidParams()
        {
            var mockedBet = new Mock<IBet>();
            var match = new Logic.Models.Match(this.sport, this.league, this.date.AddDays(5), this.homeTeam, this.visitorTeam);

            match.AddBet(mockedBet.Object);

            Assert.IsTrue(match.Bets.Contains(mockedBet.Object));
        }

        [Test]
        public void AddResult_ShouldThrowArgumentNullException_withNullResult()
        {
            var match = new Logic.Models.Match(this.sport, this.league, this.date.AddDays(5), this.homeTeam, this.visitorTeam);

            Assert.That(
                () => match.AddResult(ResultType.Final, null),
                Throws.ArgumentNullException.With.Message.Contains(string.Format(EngineConstants.ObjectCannotBeNullErrorMessage, "Result")));
        }

        [Test]
        public void AddResult_ShouldThrowArgumentException_withTwoSameResults()
        {
            var mockedResult = new Mock<IResult>();
            var resultType = ResultType.Final;

            var match = new Logic.Models.Match(this.sport, this.league, this.date.AddDays(5), this.homeTeam, this.visitorTeam);      
            match.AddResult(resultType, mockedResult.Object);

            Assert.That(
                () => match.AddResult(resultType, mockedResult.Object),
                Throws.ArgumentException.With.Message.Contains(EngineConstants.SameBetResultssForAMatchErrorMessage));
        }

        [Test]
        public void AddResult_ShouldAddCorrectToCollection_withValidParams()
        {
            var mockedResult = new Mock<IResult>();

            var match = new Logic.Models.Match(this.sport, this.league, this.date.AddDays(5), this.homeTeam, this.visitorTeam);
            match.AddResult(ResultType.Final, mockedResult.Object);

            Assert.IsTrue(match.Results.ContainsValue(mockedResult.Object));
        }

        [Test]
        public void RemoveBet_ShouldThrowNullArgumentException_WithNullBet()
        {
            var match = new Logic.Models.Match(this.sport, this.league, this.date.AddDays(5), this.homeTeam, this.visitorTeam);

            Assert.That(
                () => match.RemoveBet(null),
                Throws.ArgumentNullException.With.Message.Contains(string.Format(EngineConstants.ObjectCannotBeNullErrorMessage, "Bet")));
        }

        [Test]
        public void RemoveBet_ShouldThrowArgumentException_WhenTryingToRemoveExistingBet()
        {
            var match = new Logic.Models.Match(this.sport, this.league, this.date.AddDays(5), this.homeTeam, this.visitorTeam);
            match.Bets.Add(this.mockedBet);

            Assert.AreEqual(1, match.Bets.Count);

            match.RemoveBet(this.mockedBet);

            Assert.AreEqual(0, match.Bets.Count);
        }
    }
}
