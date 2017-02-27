namespace BettingManager.Logic.Contracts.Models
{
    using System.Collections.Generic;

    public interface IBetLine
    {    
        decimal Balance { get; set; }

        List<IBet> Bets { get; }

        string Name { get; }

        decimal StepAmount { get; set; }

        decimal TotalLosingStreak { get; set; }

        decimal BiggestLossingAmount { get; set; }

        decimal DefaultStepAmount { get; set; }

        decimal TotalStepAmountStreak { get; set; }

        IAccount Account { get; }

        bool HasCompleteBet { get; set; }

        void AddBet(IBet bet);

        void RemoveBet(IBet bet);

        decimal ChechValueToBeBet(decimal coefficient);

        decimal GetAmountToBeWin();

        decimal GetReducedRake();

        decimal TotalAmountOfActiveBets();

        decimal LeftValueToBeBet();

        void DecreaseStepAmount();

        void RestoreStepAmount();
    }
}
