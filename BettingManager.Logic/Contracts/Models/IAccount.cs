namespace BettingManager.Logic.Contracts.Models
{
    using System.Collections.Generic;

    using Common.Constants;

    public interface IAccount
    {
        decimal Balance { get; set; }

        decimal CurrentRake { get; set; }

        CurrencyType Currency { get; }

        string Name { get; }

        List<IBetLine> BettingLines { get; }

        decimal StartBalance { get; }

        void WithdrawBalance(decimal value);

        void DepositBalance(decimal value);

        void AddBettingLine(IBetLine betLine);

        void RemoveBettingLine(IBetLine betLine);
    }
}
