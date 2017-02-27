using BettingManager.Logic.Contracts.Models;

namespace BettingManager.Logic.Contracts.Factories
{
    public interface IAddAccount
    {
        void AddAccount(int accountId, IAccount account);
    }
}
