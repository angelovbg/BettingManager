using System.Collections.Generic;

using BettingManager.Logic.Contracts;
using BettingManager.Logic.Contracts.Factories;
using BettingManager.Logic.Contracts.Controllers;

namespace BettingManager.Logic.Commands
{
    public class CreateFinalResultCommand : ICommand
    {
        private int currentResultId = 1;
        private readonly IGetMatchById getMatch;
        private readonly IBetController betController;
        private readonly IResultFactory resultFactory;
        private readonly IAddResult addResult;

        public CreateFinalResultCommand(IResultFactory resultFactory, IAddResult addResult, IGetMatchById getMatch, IBetController betController)
        {
            this.getMatch = getMatch;
            this.betController = betController;
            this.resultFactory = resultFactory;
            this.addResult = addResult;
        }

        public string Execute(IList<string> parameters)
        {
            var matchId = int.Parse(parameters[0]);

            var match = getMatch.PickMatchById(matchId);

            var homeTeamScore = int.Parse(parameters[1]);
            var visitorTeamScore = int.Parse(parameters[2]);

            var result = this.resultFactory.CreateResult(match, homeTeamScore, visitorTeamScore);
            //var result = new FinalResult(match, homeTeamScore, visitorTeamScore);
            this.addResult.AddResult(currentResultId, result);

            betController.SetupCheckedBets(match.Bets);

            return string.Format("Result for match {0} has been added with ID {1}.", match.Id, this.currentResultId++);
        }
    }
}
