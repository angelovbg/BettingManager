using BettingManager.Logic.Contracts.Models;

namespace BettingManager.Logic.Contracts.Factories
{
    public interface IGetTipsterById
    {
        ITipster PickTipsterById(int tipsterId);
    }
}
