using BettingManager.Logic.Common.Constants;
using BettingManager.Logic.Contracts.Models;

namespace BettingManager.Logic.Contracts.Factories
{
    public interface IAccountFactory
    {      
        IAccount CreateAccount(string name, decimal balance, CurrencyType currency, decimal currentRake);
    }
}
