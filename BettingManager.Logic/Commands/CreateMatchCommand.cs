using System;
using System.Collections.Generic;

using BettingManager.Logic.Common.Constants;
using BettingManager.Logic.Contracts;
using BettingManager.Logic.Contracts.Factories;
using BettingManager.Logic.Models;

namespace BettingManager.Logic.Commands
{
    public class CreateMatchCommand : ICommand
    {
        private int currentMatchId = 1;
        private readonly IAddMatch addMatch;

        public CreateMatchCommand(IAddMatch addMatch)
        {
            this.addMatch = addMatch;
        }

        public string Execute(IList<string> parameters)
        {
            var stringDateData = parameters[0].Split('-');
            var intDateData = new int[5];

            for (int i = 0; i < stringDateData.Length; i++)
            {
                intDateData[i] = int.Parse(stringDateData[i]);
            }

            var matchDate = new DateTime(intDateData[0], intDateData[1], intDateData[2], intDateData[3], intDateData[4], int.Parse("00"));
            var homeTeam = parameters[1];
            var visitorTeam = parameters[2];
            var sport = parameters[3];
            var league = parameters[4];

            var sportType = (SportType)Enum.Parse(typeof(SportType), sport);
            var leagueType = (League)Enum.Parse(typeof(League), league);

            var match = new Match(sportType, leagueType, matchDate, homeTeam, visitorTeam);

            this.addMatch.AddMatch(match.Id, match);

            return string.Format("Match {0} - {1} has been added with ID {2}.", match.HomeTeam, match.VisitorTeam, this.currentMatchId++);
        }
    }
}
