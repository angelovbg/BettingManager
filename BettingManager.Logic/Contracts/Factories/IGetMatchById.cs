using BettingManager.Logic.Contracts.Models;

namespace BettingManager.Logic.Contracts.Factories
{
    public interface IGetMatchById
    {
        IMatch PickMatchById(int matchId);
    }
}
