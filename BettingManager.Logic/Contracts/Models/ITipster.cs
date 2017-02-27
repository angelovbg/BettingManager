namespace BettingManager.Logic.Contracts.Models
{
    using System.Collections.Generic;

    using Common.Constants;
  
    public interface ITipster
    {
        IList<IBet> Bets { get; }

        TipsterCompany Company { get; }

        string Name { get; set; }

        void AddBet(IBet bet);
    }
}
