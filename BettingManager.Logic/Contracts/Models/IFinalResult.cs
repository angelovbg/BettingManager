namespace BettingManager.Logic.Contracts.Models
{
    public interface IFinalResult : IResult
    {
        int TotalGoalsScored { get; }

        string[] GetDoubleMark();

        string GetMark();
    }
}
