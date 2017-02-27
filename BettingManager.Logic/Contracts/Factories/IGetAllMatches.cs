using System.Collections.Generic;

using BettingManager.Logic.Contracts.Models;

namespace BettingManager.Logic.Contracts.Factories
{
    public interface IGetAllMatches
    {
        ICollection<IMatch> AllMatches();
    }
}
