using System;
using System.Collections.Generic;

using BettingManager.Logic.Contracts.Models;

namespace BettingManager.Logic.Contracts.Factories
{
    public interface IGetAllMacthesAfterDate
    {
        IEnumerable<IMatch> PickAllMacthesAfterDate(DateTime date);
    }
}
