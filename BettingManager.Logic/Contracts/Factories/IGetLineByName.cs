using BettingManager.Logic.Contracts.Models;

namespace BettingManager.Logic.Contracts.Factories
{
    public interface IGetLineByName
    {
        IBetLine PickLineByName(string lineName);
    }
}
