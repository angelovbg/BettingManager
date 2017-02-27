using BettingManager.Logic.Contracts.Models;

namespace BettingManager.Logic.Contracts.Factories
{
    public interface IGetLineById
    {
        IBetLine PickLineById(int lineId);
    }
}
