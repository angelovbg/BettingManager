namespace BettingManager.Logic.Models
{
    using System;
    using System.Text;

    using Common.Constants;
    using Contracts.Models;

    public abstract class Result : IResult
    {       
        private int homeTeamScore;
        private int visitorTeamScore;
        private IMatch match;
        private ResultType resultType;

        public Result(IMatch match, int homeTeamScore, int visitorTeamScore, ResultType resultType)
        {
            this.Match = match;
            this.HomeTeamScore = homeTeamScore;
            this.VisitorTeamScore = visitorTeamScore;

            this.match.AddResult(resultType, this);
        }

        public int HomeTeamScore
        {
            get
            {
                return this.homeTeamScore;
            }

            set
            {
                if (value < 0)
                {
                    throw new ArgumentException(string.Format(EngineConstants.SomeValueCannotBeNegativeErrorMessage, "Home score"));
                }

                this.homeTeamScore = value;
            }
        }

        public int VisitorTeamScore
        {
            get
            {
                return this.visitorTeamScore;
            }

            set
            {
                if (value < 0)
                {
                    throw new ArgumentException(string.Format(EngineConstants.SomeValueCannotBeNegativeErrorMessage, "Visitor score"));
                }

                this.visitorTeamScore = value;
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
                if (value == null)
                {
                    throw new ArgumentNullException(string.Format(EngineConstants.ObjectCannotBeNullErrorMessage, "Match"));
                }

                this.match = value;
            }

        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine(string.Format("MatchId = {0} Result {1} : {2}", this.match.Id, this.HomeTeamScore, this.VisitorTeamScore));

            return sb.ToString();
        }
    }
}
