namespace BettingManager.Logic.Contracts.Controllers
{
    using System.Collections.Generic;

    using Models;

    public interface IMatchController
    {
        IMatch AddMatch(IList<string> parameters);
    }
}
