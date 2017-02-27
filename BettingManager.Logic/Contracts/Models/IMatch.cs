namespace BettingManager.Logic.Contracts.Models
{    
    using System;
    using System.Collections.Generic;
    using Common.Constants;

    public interface IMatch
    {
        int Id { get; }

        List<IBet> Bets { get; }

        DateTime Date { get; }

        Dictionary<ResultType, IResult> Results { get; }

        void AddBet(IBet bet);

        void RemoveBet(IBet bet);

        void AddResult(ResultType resultType, IResult result);

        void RemoveResult(ResultType resultType, IResult result);
    }
}
