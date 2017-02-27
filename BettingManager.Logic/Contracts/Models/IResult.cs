namespace BettingManager.Logic.Contracts.Models
{
    public interface IResult
    {
        int HomeTeamScore { get; }

        int VisitorTeamScore { get; }

        IMatch Match { get; }
    }
}
