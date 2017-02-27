namespace BettingManager.Logic.Controllers
{
    using System;
    using System.Collections.Generic;

    using Common.Constants;
    using Contracts.Controllers;
    using Contracts.Models;
    using Models;

    public class MatchController : IMatchController
    {
        //public IMatch AddMatch(IList<string> parameters)
        //{
        //    var stringDateData = parameters[0].Split('-');
        //    var intDateData = new int[5];

        //    for (int i = 0; i < stringDateData.Length; i++)
        //    {
        //        intDateData[i] = int.Parse(stringDateData[i]);
        //    }

        //    var matchDate = new DateTime(intDateData[0], intDateData[1], intDateData[2], intDateData[3], intDateData[4], int.Parse("00"));
        //    var homeTeam = parameters[1];
        //    var visitorTeam = parameters[2];
        //    var sport = parameters[3];
        //    var league = parameters[4];

        //    var sportType = (SportType)Enum.Parse(typeof(SportType), sport);
        //    var leagueType = (League)Enum.Parse(typeof(League), league);

        //    var match = new Match(sportType, leagueType, matchDate, homeTeam, visitorTeam);

        //    return match;
        //}
        public IMatch AddMatch(IList<string> parameters)
        {
            throw new NotImplementedException();
        }
    }
}
