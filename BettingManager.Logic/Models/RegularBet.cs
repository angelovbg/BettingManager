using System;

using BettingManager.Logic.Models.Abstractions;
using BettingManager.Logic.Common.Constants;
using BettingManager.Logic.Contracts.Models;

namespace BettingManager.Logic.Models
{
    public class RegularBet : BetForFinalResult
    {
        public RegularBet(IMatch match, IBetLine line, string mark, decimal amount, decimal coefficient, ITipster tipster) : base(match, line, mark, amount, coefficient, tipster)
        {
            this.CurrentBetType = BetType.Regular;
        }

        public override void IsWinningBet()
        {
            var result = this.Match.Results[ResultType.Final] as IFinalResult;
            if (result.GetMark()  == Mark.ToString())
            {
                this.IsWin = true;
            }
            else
            {
                this.IsWin = false;
            }
        }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}
