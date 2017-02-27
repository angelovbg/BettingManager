using BettingManager.Logic.Contracts.Models;

namespace BettingManager.Logic.Contracts.Factories
{
    public interface IBetLineFactory
    {
        IBetLine AddLine(IAccount account, string name, decimal stepAmount, decimal decreasingStepValue);
    }
}
