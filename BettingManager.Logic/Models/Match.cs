namespace BettingManager.Logic.Models
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Common.Constants;
    using Contracts.Models;
 
    public class Match : IMatch
    {
        private static int lastUsedId = 0;
        private int id;
        private string homeTeam;
        private string visitorTeam;
        private DateTime date;
        private SportType sport;
        private League league;

        private List<IBet> bets;
        private Dictionary<ResultType, IResult> results;
       
        public Match(SportType sport, League league, DateTime dateTime, string homeTeam, string visitorTeam)
        {
            /*
            var dateToBeChecked = dateTime.AddHours(2);
        
            if (DateTime.Now > dateToBeChecked)
            {
                throw new ArgumentException("Cannot add match that is expired.");
            }
            */

            if (homeTeam == visitorTeam)
            {
                throw new ArgumentException(EngineConstants.SameTeamsInMatchErrorMessage);
            }

            this.Sport = sport;
            this.League = league;
            this.Date = dateTime;
            this.HomeTeam = homeTeam;
            this.VisitorTeam = visitorTeam;

            this.Id = lastUsedId + 1;
            lastUsedId = this.Id;

            this.Bets = new List<IBet>();
            this.Results = new Dictionary<ResultType, IResult>();     
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

        public DateTime Date
        {
            get
            {
                return this.date;
            }

            private set
            {
                this.date = value;
            }
        }

        public string HomeTeam
        {
            get
            {
                return this.homeTeam;
            }

            set
            {
                this.homeTeam = value;
            }
        }

        public int Id
        {
            get
            {
                return this.id;
            }

            private set
            {
                this.id = value;
            }
        }

        public League League
        {
            get
            {
                return this.league;
            }

            private set
            {
                this.league = value;
            }
        }

        public Dictionary<ResultType, IResult> Results
        {
            get
            {
                return this.results;
            }

            private set
            {
                this.results = value;
            }
        }

        public string VisitorTeam
        {
            get
            {
                return this.visitorTeam;
            }

            set
            {
                this.visitorTeam = value;
            }
        }

        public SportType Sport
        {
            get
            {
                return this.sport;
            }

            private set
            {
                this.sport = value;
            }
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(string.Format("{0} MatchID: {5} {1} - {2} / {3} {4}", this.Date, this.HomeTeam, this.VisitorTeam, this.Sport, this.League, this.Id));

            sb.AppendLine();
            if (this.Bets.Count > 0)
            {              
                sb.AppendLine(string.Format("---BETS---"));
                foreach (var bet in this.bets)
                {
                    sb.AppendLine(bet.ToString());
                }
            }
            else
            {
                sb.AppendLine(string.Format("---NO BETS---"));
            }

            if (this.results.Count > 0)
            {
                sb.Append(string.Format("---RESULTS---"));
                foreach (var result in this.results)
                {
                    sb.AppendLine();
                    sb.Append(string.Format("Bet type: {0} - {1}", result.Key, this.results[result.Key].ToString()));
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
                throw new ArgumentException(EngineConstants.SameBetsForAMatchErrorMessage);
            }

            this.bets.Add(bet);
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

        public void AddResult(ResultType resultType, IResult result)
        {
            if (result == null)
            {
                throw new ArgumentNullException(string.Format(EngineConstants.ObjectCannotBeNullErrorMessage, "Result"));
            }

            if (this.Results.ContainsValue(result))
            {
                throw new ArgumentException(EngineConstants.SameBetResultssForAMatchErrorMessage);
            }

            this.results.Add(resultType, result);
        }

        public void RemoveResult(ResultType resultType, IResult result)
        {
            if (result == null)
            {
                throw new ArgumentNullException(string.Format(EngineConstants.ObjectCannotBeNullErrorMessage, "Result"));
            }

            if (this.Results.ContainsKey(resultType) && this.Results.ContainsValue(result))
            {
                this.Results.Remove(resultType);
            }
        }
    }
}
