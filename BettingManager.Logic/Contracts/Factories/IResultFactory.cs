using BettingManager.Logic.Contracts.Models;

namespace BettingManager.Logic.Contracts.Factories
{
    public interface IResultFactory
    {
        IResult CreateResult(IMatch match, int homeTeamScore, int visitorTeamScore);
    }
}
