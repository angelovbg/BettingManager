namespace BettingManager.Logic.Contracts.Models
{
    using BettingManager.Logic.Common.Constants;

    public interface IBet
    {
        ResultType ResultType { get; }

        decimal Amount { get; }

        decimal Coefficient { get; }
        
        bool IsChecked { get; set; }

        bool IsWin { get; set; }

        IBetLine Line { get; }

        IMatch Match { get; }

        ITipster Tipster { get; }

        bool IsCheckedBet();

        void SetupBalances();

        void IsWinningBet();

        decimal CalculateWinningBetAmount();
    }
}
