namespace BettingManager.Logic.Models.Abstractions
{
    using Common.Constants;
    using Contracts.Models;

    public abstract class BetForFinalResult : Bet
    {
        public BetForFinalResult(IMatch match, IBetLine line, string mark, decimal amount, decimal coefficient, ITipster tipster) : base(match, line, mark.ToString(), amount, coefficient, tipster)
        {
            this.ResultType = ResultType.Final;
        }

        public override bool IsCheckedBet()
        {
            this.IsChecked = false;
            if (this.Match.Results == null)
            {
                return false;
            }

            if (this.Match.Results.ContainsKey(ResultType.Final))
            {
                this.IsChecked = true;
                return true;
            }

            return false;
        }

    }
}
