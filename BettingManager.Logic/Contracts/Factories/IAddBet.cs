using BettingManager.Logic.Contracts.Models;

namespace BettingManager.Logic.Contracts.Factories
{
    public interface IAddBet
    {
        void AddBet(int betId, IBet bet);
    }
}
