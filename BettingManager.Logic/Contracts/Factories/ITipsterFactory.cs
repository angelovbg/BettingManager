using BettingManager.Logic.Common.Constants;
using BettingManager.Logic.Contracts.Models;

namespace BettingManager.Logic.Contracts.Factories
{
    public interface ITipsterFactory
    {
        ITipster CreateTipster(string name, TipsterCompany company);
    }
}
