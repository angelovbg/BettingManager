using BettingManager.Logic.Contracts.Models;

namespace BettingManager.Logic.Contracts.Factories
{
    public interface IAddTipster
    {
        void AddTipster(int tipsterId, ITipster tipster);
    }
}
