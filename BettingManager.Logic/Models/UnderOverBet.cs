namespace BettingManager.Logic.Models
{
    using System;

    using Abstractions; 
    using Common.Constants;
    using Contracts.Models;   

    public class UnderOverBet : BetForFinalResult
    {
        public UnderOverBet(IMatch match, IBetLine line, string mark, decimal amount, decimal coefficient, ITipster tipster) : base(match, line, mark, amount, coefficient, tipster)
        {
            this.CurrentBetType = BetType.UnderOver;
        }

        public override void IsWinningBet()
        {
            var result = this.Match.Results[ResultType.Final] as IFinalResult;
            int goals = result.TotalGoalsScored;

            if (goals >= 0 && this.IsChecked)
            {
                this.IsWin = false;

                if (this.Mark == FinalResultUnderOver.Over0_5.ToString())
                {
                    if (goals > 0.5)
                    {
                        this.IsWin = true;
                    }                 
                }
                else if (this.Mark == FinalResultUnderOver.Over1_5.ToString())
                {
                    if (goals > 1.5)
                    {
                        this.IsWin = true;
                    }
                }
                else if (this.Mark == FinalResultUnderOver.Over2_5.ToString())
                {
                    if (goals > 2.5)
                    {
                        this.IsWin = true;
                    }
                }
                else if (this.Mark == FinalResultUnderOver.Over3_5.ToString())
                {
                    if (goals > 3.5)
                    {
                        this.IsWin = true;
                    }
                }
                else if (this.Mark == FinalResultUnderOver.Over5_5.ToString())
                {
                    if (goals > 5.5)
                    {
                        this.IsWin = true;
                    }
                }
                else if (this.Mark == FinalResultUnderOver.Under0_5.ToString())
                {
                    if (goals < 0.5)
                    {
                        this.IsWin = true;
                    }
                }
                else if (this.Mark == FinalResultUnderOver.Under1_5.ToString())
                {
                    if (goals < 1.5)
                    {
                        this.IsWin = true;
                    }
                }
                else if (this.Mark == FinalResultUnderOver.Under2_5.ToString())
                {
                    if (goals < 2.5)
                    {
                        this.IsWin = true;
                    }
                }
                else if (this.Mark == FinalResultUnderOver.Under3_5.ToString())
                {
                    if (goals < 3.5)
                    {
                        this.IsWin = true;
                    }
                }
                else if (this.Mark == FinalResultUnderOver.Under5_5.ToString())
                {
                    if (goals < 5.5)
                    {
                        this.IsWin = true;
                    }
                }
                else
                {
                    throw new ArgumentException("Invalid params for under/over mark!");
                }
            }
            else
            {
                throw new ArgumentException("This bet is not checked!");
            }
        }
    }
}
