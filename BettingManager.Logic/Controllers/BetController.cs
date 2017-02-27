namespace BettingManager.Logic.Controllers
{
    using System;
    using System.Collections.Generic;

    using Common.Constants;
    using Contracts.Controllers;
    using Contracts.Models;
    using Models;

    public class BetController : IBetController
    {

        public void SetupCheckedBets(IList<IBet> bets)
        {
            foreach (var bet in bets)
            {
                if (bet.IsCheckedBet())
                {
                    bet.IsWinningBet();
                    bet.SetupBalances();
                    bet.Line.HasCompleteBet = false;
                }          
            }         
        }

        public string TransformMark(string input)
        {
            if (input == "1")
            {
                return "_1";
            }
            else if (input == "2")
            {
                return "_2";
            }
            else if (input == "1X")
            {
                return "_1X";
            }
            else if (input == "12")
            {
                return "_12";
            }
            else
            {
                return input;
            }
        }
    }
}
