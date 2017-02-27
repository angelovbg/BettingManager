using BettingManager.Logic.Contracts.Models;

namespace BettingManager.Logic.Contracts.Factories
{
    public interface IGetBetById
    {
        IBet GetBetById(int betId);
    }
}
