namespace BettingManager.Logic.Models
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using Common.Constants;
    using Contracts.Models;

    public class BetfairAccount : IAccount
    {
        private decimal startBalance;
        private string name = string.Empty;
        private decimal balance;
        private decimal currentRake;
        private CurrencyType currency;
        private List<IBetLine> bettingLines;

        public BetfairAccount(string name, decimal balance, CurrencyType currency, decimal currentRake)
        {
            this.Name = name;
            this.Balance = balance;
            this.StartBalance = balance;
            this.Currency = currency;
            this.CurrentRake = currentRake;

            this.BettingLines = new List<IBetLine>();
        }
        
        public decimal Balance
        {
            get
            {
                return this.balance;
            }

            set
            {
                if (value < 0)
                {
                    throw new ArgumentException(string.Format(EngineConstants.SomeValueCannotBeNegativeErrorMessage, "Balance"));
                }

                this.balance = value;
            }
        }

        public List<IBetLine> BettingLines
        {
            get
            {
                return this.bettingLines;
            }

            private set
            {
                this.bettingLines = value;
            }
        }

        public CurrencyType Currency
        {
            get
            {
                return this.currency;
            }

            private set
            {
                this.currency = value;
            }
        }

        public decimal CurrentRake
        {
            get
            {
                return this.currentRake;
            }

            set
            {
                if (value < 0m || value > 0.10m)
                {
                    throw new ArgumentException(EngineConstants.IncorrectRakeValueErrorMessage);
                }

                this.currentRake = value;
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
                    throw new ArgumentNullException(string.Format(EngineConstants.ObjectCannotBeNullErrorMessage, "Name"));
                }

                this.name = value;
            }
        }

        public decimal StartBalance
        {
            get
            {
                return this.startBalance;
            }

            private set
            {
                this.startBalance = value;
            }
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(string.Format("Account Name {0} with balance: {1:F2} {3} and rake: {2}", this.Name, this.Balance, this.CurrentRake, this.Currency));
            return sb.ToString();
        }

        public void WithdrawBalance(decimal value)
        {
            if (value < 0)
            {
                throw new ArgumentException(string.Format(EngineConstants.SomeValueCannotBeNegativeErrorMessage, "Withdraw value"));
            }

            this.Balance -= value;
        }

        public void DepositBalance(decimal value)
        {
            if (value < 0)
            {
                throw new ArgumentException(string.Format(EngineConstants.SomeValueCannotBeNegativeErrorMessage, "Deposit value"));
            }

            this.Balance += value;
        }

        public void AddBettingLine(IBetLine betLine)
        {
            if (betLine == null)
            {
                throw new ArgumentNullException(string.Format(EngineConstants.ObjectCannotBeNullErrorMessage, "Line"));
            }

            if (this.BettingLines.Contains(betLine))
            {
                throw new ArgumentException(EngineConstants.SameLinesToAnAcountErrorMessage);
            }

            this.bettingLines.Add(betLine);
        }

        public void RemoveBettingLine(IBetLine betLine)
        {
            if (betLine == null)
            {
                throw new ArgumentNullException(string.Format(EngineConstants.ObjectCannotBeNullErrorMessage, "Line"));
            }

            if (this.BettingLines.Contains(betLine))
            {
                this.BettingLines.Remove(betLine);
            }
        }
    }
}
