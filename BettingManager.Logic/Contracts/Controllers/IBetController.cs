namespace BettingManager.Logic.Contracts.Controllers
{
    using System.Collections.Generic;

    using Models;

    public interface IBetController
    {
        void SetupCheckedBets(IList<IBet> bets);
    }
}
