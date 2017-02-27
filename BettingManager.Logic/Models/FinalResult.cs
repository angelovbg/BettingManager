namespace BettingManager.Logic.Models
{
    using System;
    using Common.Constants;
    using Contracts.Models;

    public class FinalResult : Result, IFinalResult
    {
        private const ResultType FinalResultType = ResultType.Final;
        private bool isHomeWin;
        private bool isVisitorWin;
        private bool isDraw;
        private int totalGoalsScored = -1;

        public FinalResult(IMatch match, int homeTeamScore, int visitorTeamScore) : base(match, homeTeamScore, visitorTeamScore, FinalResultType)
        {
            this.isHomeWin = homeTeamScore > visitorTeamScore;
            this.isVisitorWin = homeTeamScore < visitorTeamScore;
            this.isDraw = homeTeamScore == visitorTeamScore;
            this.TotalGoalsScored = homeTeamScore + visitorTeamScore;
        }

        public int TotalGoalsScored
        {
            get
            {
                return this.totalGoalsScored;
            }

            private set
            {
                if (this.HomeTeamScore + this.VisitorTeamScore != value)
                {
                    throw new ArgumentException(EngineConstants.InvalidTotalGoalsErrorMessage);
                }

                this.totalGoalsScored = value;
            }
        }

        public string[] GetDoubleMark()
        {
            if (this.isHomeWin)
            {
                return new string[] { FinalResultDouble._1X.ToString(), FinalResultDouble._12.ToString() };
            }
            else if (this.isVisitorWin)
            {
                return new string[] { FinalResultDouble.X2.ToString(), FinalResultDouble._12.ToString() };
                
            }
            else if (this.isDraw)
            {
                return new string[] { FinalResultDouble._1X.ToString(), FinalResultDouble.X2.ToString() };
            }
            else
            {
                throw new ArgumentException("Invalid params for double result mark!");
            }
        }

        public string GetMark()
        {
            if (this.isHomeWin)
            {
                return FinalResultMarks._1.ToString();
            }
            else if (this.isVisitorWin)
            {
                return FinalResultMarks._2.ToString();
            }
            else if (this.isDraw)
            {
                return FinalResultMarks.X.ToString();
            }
            else
            {
                throw new ArgumentException("Invalid params for result mark?!");
            }
        }
    }
}
