using System;
using System.Collections.Generic;

using BettingManager.Logic.Contracts.Models;

namespace BettingManager.Logic.Contracts.Factories
{
    public interface IGetMatchesWithoutResults
    {
        IEnumerable<IMatch> PickMatchesWithoutResults(DateTime date);
    }
}
