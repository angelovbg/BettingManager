using BettingManager.Logic.Contracts.Models;

namespace BettingManager.Logic.Contracts.Factories
{
    public interface IAddResult
    {
        void AddResult(int resultId, IResult result);
    }
}
