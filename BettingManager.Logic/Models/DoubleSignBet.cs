namespace BettingManager.Logic.Models
{
    using Abstractions;
    using Common.Constants;
    using Contracts.Models;
    
    public class DoubleSignBet : BetForFinalResult
    {
        public DoubleSignBet(IMatch match, IBetLine line, string mark, decimal amount, decimal coefficient, ITipster tipster) : base(match, line, mark, amount, coefficient, tipster)
        {
            this.CurrentBetType = BetType.DoubleSign;
        }

        public override void IsWinningBet()
        {
            var result = this.Match.Results[ResultType.Final] as IFinalResult;
            var results = result.GetDoubleMark();

            this.IsWin = false;

            foreach (var singleResult in results)
            {
                if (this.Mark == singleResult)
                {
                    this.IsWin = true;
                }
            }
        }
    }
}
