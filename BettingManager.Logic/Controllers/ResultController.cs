namespace BettingManager.Logic.Controllers
{
    using System;
    using System.Collections.Generic;

    using Contracts.Controllers;
    using Contracts.Models;
    using Models;

    public class ResultController : IResultController
    {
        /*
        public IResult AddFinalResult(IMatch match, IList<string> parameters)
        {
            var homeTeamScore = int.Parse(parameters[1]);
            var visitorTeamScore = int.Parse(parameters[2]);

            var result = new FinalResult(match, homeTeamScore, visitorTeamScore);

            return result;
        }
        */
        public IResult AddFinalResult(IMatch match, IList<string> parameters)
        {
            throw new NotImplementedException();
        }
    }
}
