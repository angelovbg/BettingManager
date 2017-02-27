namespace BettingManager.Logic.Contracts.Controllers
{
    using System.Collections.Generic;

    using Models;

    public interface IResultController
    {
        IResult AddFinalResult(IMatch match, IList<string> parameters);
    }
}
