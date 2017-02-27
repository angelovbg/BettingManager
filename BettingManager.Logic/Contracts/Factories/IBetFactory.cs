using BettingManager.Logic.Contracts.Models;

namespace BettingManager.Logic.Contracts.Factories
{
    public interface IBetFactory
    {
        IBet GetRegularBet(IMatch match, IBetLine line, string mark, decimal amount, decimal coefficient, ITipster tipster);

        IBet GetDoubleSignBet(IMatch match, IBetLine line, string mark, decimal amount, decimal coefficient, ITipster tipster);

        IBet GetUnderOverBet(IMatch match, IBetLine line, string mark, decimal amount, decimal coefficient, ITipster tipster);
    }
}
