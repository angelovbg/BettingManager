using System;

using BettingManager.Logic.Common.Constants;
using BettingManager.Logic.Contracts.Models;

namespace BettingManager.Logic.Contracts.Factories
{
    public interface IMatchFactory
    {
        IMatch CreateMatch(SportType sport, League league, DateTime dateTime, string homeTeam, string visitorTeam);
    }
}
