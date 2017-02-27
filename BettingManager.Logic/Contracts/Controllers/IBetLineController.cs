namespace BettingManager.Logic.Contracts.Controllers
{
    using System.Collections.Generic;

    using Models;

    public interface IBetLineController
    {
        IBetLine AddLine(IList<string> input, IAccount account);   
    }
}