namespace BettingManager.Logic.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using Common.Constants;
    using Contracts.Models;
   
    public class BetLine : IBetLine
    {
        private IAccount account;
        private List<IBet> bets;
        private decimal balance;
        private decimal stepAmount = 2m;
        private bool hasCompleteBet;
        private string name;
        private decimal totalLosingStreak = 0;
        private decimal totalStepAmountStreak = 0;
        private decimal biggestLossingAmount = 0;
        private decimal decreasingStepValue = 0;
        private decimal defaultStepAmount = 0;

        public BetLine(IAccount account, string name, decimal stepAmount, decimal decreasingStepValue)
        {
            this.Account = account;
            this.StepAmount = stepAmount;
            this.Balance = 0;
            this.Name = name;
            this.DecreasingStepValue = decreasingStepValue;
            this.DefaultStepAmount = stepAmount;
            this.Bets = new List<IBet>();

            this.Account.AddBettingLine(this);
        }

        public IAccount Account
        {
            get
            {
                return this.account;
            }

            private set
            {
                this.account = value;
            }
        }

        public decimal Balance
        {
            get
            {
                return this.balance;
            }

            set
            {
                this.balance = value;
            }
        }

        public decimal BiggestLossingAmount
        {
            get
            {
                return this.biggestLossingAmount;
            }

            set
            {
                if (value < 0)
                {
                    throw new ArgumentException(string.Format("Value of {0} cannot be negative", "BiggestLossingAmount"));
                }

                this.biggestLossingAmount = value;
            }
        }
        public decimal DecreasingStepValue
        {
            get
            {
                return this.decreasingStepValue;
            }

            set
            {
                if (value < 0)
                {
                    throw new ArgumentException(string.Format("Value of {0} cannot be negative", "DecreasingStepValue"));
                }

                this.decreasingStepValue = value;
            }
        }

        public decimal DefaultStepAmount
        {
            get
            {
                return this.defaultStepAmount;
            }

            set
            {
                this.defaultStepAmount = value;
            }
        }
        

        public decimal StepAmount
        {
            get
            {
                return this.stepAmount;
            }

            set
            {
                this.stepAmount = value;       
            }
        }

        public List<IBet> Bets
        {
            get
            {
                return this.bets;
            }

            private set
            {
                this.bets = value;
            }
        }

        public string Name
        {
            get
            {
                return this.name;
            }

            private set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentNullException(EngineConstants.ObjectCannotBeNullErrorMessage, "Name");
                }

                this.name = value;
            }
        }


        public decimal TotalLosingStreak
        {
            get
            {
                return this.totalLosingStreak;
            }

            set
            {
                if (value < 0)
                {
                    throw new ArgumentException("Total losing steak cannot be null");
                }

                this.totalLosingStreak = value;
                this.BiggestLosingAmountSetup();       
            }
        }

        public decimal TotalStepAmountStreak
        {
            get
            {
                return this.totalStepAmountStreak;
            }

            set
            {
                if (value < 0)
                {
                    throw new ArgumentException("Total step amount streak canno be negative!");
                }

                this.totalStepAmountStreak = value;
            }
        }

        public bool HasCompleteBet
        {
            get
            {
                return this.hasCompleteBet;
            }

            set
            {
                this.hasCompleteBet = value;
            }
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(string.Format("Bet line for Account name: {0} -> Step amount: {1:F2} Balance: {2:F2}, Total losing streak: {3:F2} with total steps amounts {4:F2}", this.account.Name, this.StepAmount, this.Balance, this.TotalLosingStreak, this.TotalStepAmountStreak));

            if (this.Bets.Count > 0)
            {
                sb.AppendLine();
                sb.Append(string.Format("---BETS---"));
                foreach (var bet in this.bets)
                {
                    sb.AppendLine();
                    sb.Append(bet.ToString());
                }
            }

            return sb.ToString();
        }

        public void AddBet(IBet bet)
        {
           if (bet == null)
            {
                throw new ArgumentNullException(string.Format(EngineConstants.ObjectCannotBeNullErrorMessage, "Bet"));
            }

            if (this.Bets.Contains(bet))
            {
                throw new ArgumentException(EngineConstants.SameBetToALineErrorMessage);
            }

            this.Bets.Add(bet);
        }

        public void RemoveBet(IBet bet)
        {
            if (bet == null)
            {
                throw new ArgumentNullException(string.Format(EngineConstants.ObjectCannotBeNullErrorMessage, "Bet"));
            }

            if (this.Bets.Contains(bet))
            {
                this.Bets.Remove(bet);
            }
        }

        public decimal LeftValueToBeBet()
        {
            return this.GetAmountToBeWin() - this.TotalAmountOfActiveBets();
        }

        public void BiggestLosingAmountSetup()
        {
            if (this.TotalLosingStreak > this.BiggestLossingAmount)
            {
                this.BiggestLossingAmount = this.TotalStepAmountStreak;
            }
        }

        public decimal ChechValueToBeBet(decimal coefficient)
        {
            var valueToBeWin = this.GetAmountToBeWin();
            var rakeReduction = this.GetReducedRake();
            var value = valueToBeWin / (coefficient - 1) / rakeReduction;
            value = Math.Round(value, 2);
            var activeBetsValue = this.TotalAmountOfActiveBets();
            value -= activeBetsValue;

            return value;
        }

        public decimal TotalAmountOfActiveBets()
        {
            //var activeBets = from t in this.Bets
            //       where t.IsChecked = true
            //       select t;
            var activeBets = from t in this.Bets
                             where t.IsChecked = false
                             select t;
            var value = 0m;

            if (activeBets.ToList().Count > 0)
            {
                foreach (var bet in activeBets)
                {
                    value += bet.CalculateWinningBetAmount();
                }
            }

            return value;
        }

        public decimal ValueForLineBet(decimal coefficient)
        {
            var valueToBeWin = this.GetAmountToBeWin();
            var rakeReduction = this.GetReducedRake();
            var value = valueToBeWin / (coefficient - 1) / rakeReduction;

            return value;
        }

        public decimal GetAmountToBeWin()
        {
            return this.TotalLosingStreak + this.StepAmount + this.TotalStepAmountStreak;
        }

        public decimal GetReducedRake()
        {
            return 1 - this.Account.CurrentRake;
        }

        public void DecreaseStepAmount()
        {
            if (this.StepAmount - this.DecreasingStepValue > 0)
            {
                this.StepAmount -= this.DecreasingStepValue;
            }
            else
            {
                this.StepAmount = 0;
            }
            
        }

        public void RestoreStepAmount()
        {
            this.StepAmount = this.DefaultStepAmount;
        }
    }
}