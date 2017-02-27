using BettingManager.Logic.Contracts.Models;

namespace BettingManager.Logic.Contracts.Factories
{
    public interface IAddMatch
    {
        void AddMatch(int matchId, IMatch match);
    }
}
