using BettingManager.Logic.Contracts.Models;

namespace BettingManager.Logic.Contracts.Factories
{
    public interface IGetAccountById
    {
        IAccount PickAccountById(int accountId);
    }
}
