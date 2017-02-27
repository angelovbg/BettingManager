/*
using BettingManager.Logic.Common.Constants;
using BettingManager.Logic.Contracts.Factories;
using BettingManager.Logic.Contracts.Models;
using BettingManager.Logic.Models;

namespace BettingManager.Logic.Factories
{
    public class BetFactory : IBetFactory
    {
        public IBet CreateRegularBet(IMatch match, IBetLine line, FinalResultMarks mark, decimal amount, decimal coefficient, ITipster tipster)
        {
            return new RegularBet(match, line, mark, amount, coefficient, tipster);
        }

        public IBet CreateDoubleSignBet(IMatch match, IBetLine line, FinalResultDouble mark, decimal amount, decimal coefficient, ITipster tipster)
        {
            return new DoubleSignBet(match, line, mark, amount, coefficient, tipster);
        }

        public IBet CreateUnderOverBet(IMatch match, IBetLine line, FinalResultUnderOver mark, decimal amount, decimal coefficient, ITipster tipster)
        {
            return new UnderOverBet(match, line, mark, amount, coefficient, tipster);
        }
    }
}
*/