namespace BettingManager.Logic.Models
{
    using System;
    using System.Collections.Generic;
    using Common.Constants;
    using Contracts;
    using Contracts.Models;

    public class Tipster : ITipster
    {
        private IList<IBet> bets;
        private TipsterCompany company;
        private string name;

        public Tipster(string name, TipsterCompany company)
        {
            this.Name = name;
            this.Company = company;

            this.bets = new List<IBet>();
        }

        public IList<IBet> Bets
        {
            get
            {
                return this.bets;
            }
        }

        public TipsterCompany Company
        {
            get
            {
                return this.company;
            }

            private set
            {
                this.company = value;
            }
        }

        public string Name
        {
            get
            {
                return this.name;
            }

            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentNullException(EngineConstants.ObjectCannotBeNullErrorMessage, "Name");
                }

                this.name = value;
            }
        }

        public void AddBet(IBet bet)
        {
            if (bet == null)
            {
                throw new ArgumentNullException(string.Format(EngineConstants.ObjectCannotBeNullErrorMessage, "Bet"));
            }

            if (this.Bets.Contains(bet))
            {
                throw new ArgumentException(EngineConstants.SameBetsForAMatchErrorMessage);
            }

            this.bets.Add(bet);
        }
    }
}
