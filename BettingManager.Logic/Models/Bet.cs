namespace BettingManager.Logic.Models
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Common.Constants;
    using Contracts.Models;

    public abstract class Bet : IBet
    {
        private IMatch match;
        private decimal amount = 0;
        private decimal coefficient;
        private BetType currentBetType;
        private string mark;
        private bool isChecked;
        private bool isWin; 
        private IBetLine line;
        private ITipster tipster;
        private ResultType resultType; 

        public Bet(IMatch match, IBetLine line, string mark, decimal amount, decimal coefficient, ITipster tipster)
        {
            if (match == null)
            {
                throw new ArgumentNullException(string.Format(EngineConstants.ObjectCannotBeNullErrorMessage, "Match"));
            }

            if (line == null)
            {
                throw new ArgumentNullException(string.Format(EngineConstants.ObjectCannotBeNullErrorMessage, "Line"));
            }

            if (tipster == null)
            {
                throw new ArgumentNullException(string.Format(EngineConstants.ObjectCannotBeNullErrorMessage, "Tipster"));
            }

            this.Line = line;          
            this.Match = match;      
            this.Amount = amount;
            this.Coefficient = coefficient;
            this.CurrentBetType = currentBetType;
            this.Mark = mark;     
            this.isChecked = false;
            this.Tipster = tipster;

            if (this.Match.Results != null && this.Match.Results.ContainsKey(resultType))
            {
                throw new ArgumentException(EngineConstants.CannotAddBetToMatchWithResultErrorMessage);
            }

            if (this.Line.HasCompleteBet)
            {
                throw new ArgumentException(EngineConstants.AlreadyCompletedLineErrorMessage);
            }

            if (this.CalculateWinningBetAmount() > (this.Line.LeftValueToBeBet() + 0.3m))
            {
                throw new ArgumentException(EngineConstants.BetIsGreaterThenNeededErrorMessage);
            }

           this.Line.HasCompleteBet = this.IsReadyToBeComplete();

            match.AddBet(this);
            line.AddBet(this);
        }

        public decimal Amount
        {
            get
            {
                return this.amount;
            }

            private set
            {         
                if (value <= 0)
                {
                    throw new ArgumentException(EngineConstants.BetAmountCannotBeNegativeErrorMessage);
                }

                if (value > 10000)
                {
                    throw new ArgumentException(EngineConstants.BetAmountIsTooBigErrorMessage);
                }

                if (value > this.Line.Account.Balance)
                {
                    throw new ArgumentException(EngineConstants.NotEnoughAccountBalanceForBetErrorMessage);
                }
                
                this.amount = value;
            }
        }

        public BetType CurrentBetType
        {
            get
            {
                return this.currentBetType;
            }

            protected set
            {
                this.currentBetType = value;
            }
        }

        public decimal Coefficient
        {
            get
            {
                return this.coefficient;
            }

            private set
            {
                if (value < 1)
                {
                    throw new ArgumentException(EngineConstants.BetCoefficientCannotBeLessThenOneErrorMessage);
                }

                this.coefficient = value;
            }
        }

        public string Mark
        {
            get
            {
                return this.mark;
            }

            private set
            {
                this.mark = value;
            }
        }

        public IMatch Match
        {
            get
            {
                return this.match;
            }

            private set
            {
                this.match = value;
            }
        }

        public bool IsChecked
        {
            get
            {
                return this.isChecked;
            }

            set
            {
                this.isChecked = value;
            }
        }

        public bool IsWin
        {
            get
            {
                return this.isWin;
            }

            set
            {
                this.isWin = value;
            }
        }

        public IBetLine Line
        {
            get
            {
                return this.line;
            }

            private set
            {
                this.line = value;
            }
        }

        public ITipster Tipster
        {
            get
            {
                return this.tipster;
            }

            private set
            {
                this.tipster = value;
            }
        }

        public ResultType ResultType
        {
            get
            {
                return this.resultType;
            }

            protected set
            {
                this.resultType = value;
            }
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            var isCheckedBetData = string.Empty;

            if (this.IsChecked)
            {
                isCheckedBetData += string.Format("{0}", this.IsWin ? "Win" : "Lost");
            }
            else
            {
                isCheckedBetData += string.Format("No result added.");
            }
            
            sb.Append(string.Format("Amount: {0:F2} Coef: {1:F2} MatchID: {2} Mark: {3} BetType: {4}. {5}", this.Amount, this.Coefficient, this.Match.Id, this.Mark, this.CurrentBetType, isCheckedBetData));
            return sb.ToString();
        }

        public virtual void SetupBalances()
        {
            if (!this.IsChecked)
            {
                throw new ArgumentException(EngineConstants.NotCheckedBetErrorMessage);
            }

            if (this.IsWin)
            {
                this.UpdateWinningBetBalances();
            }
            else
            {
                this.UpdateLosingBetBalances();
            }
        }

        public bool IsReadyToBeComplete()
        {
            var absoluteValue = Math.Abs(this.CalculateWinningBetAmount() - this.Line.LeftValueToBeBet());
            if (absoluteValue < 0.2m)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public virtual decimal CalculateWinningBetAmount()
        {
            return Math.Round(this.amount * (this.coefficient - 1)  * (1 - this.Line.Account.CurrentRake), 2);
        }

        public virtual decimal CalculateLosingBetAmount()
        {
            return this.amount;
        }

        public void UpdateWinningBetBalances()
        {
            decimal winningAmount = this.CalculateWinningBetAmount();
            this.Line.Balance += winningAmount;
            this.Line.Account.Balance += winningAmount;

            this.Line.RestoreStepAmount();

            this.Line.TotalLosingStreak = 0;
            this.Line.TotalStepAmountStreak = 0;
        }

        public void UpdateLosingBetBalances()
        {
            decimal losingAmount = this.CalculateLosingBetAmount();
            this.Line.Balance -= losingAmount;
            this.Line.Account.Balance -= losingAmount;

            this.Line.DecreaseStepAmount();

            this.Line.TotalLosingStreak += losingAmount;
            this.line.TotalStepAmountStreak += this.Line.StepAmount;
        }

        public abstract bool IsCheckedBet();

        public abstract void IsWinningBet();

    }
}
