using System;
using System.Collections.Generic;

using BettingManager.Logic.Contracts.Factories;
using BettingManager.Logic.Contracts.Models;
using System.Linq;

namespace BettingManager.Logic.Models
{
    public class BettingManagerData : IBettingManager
    {
        private readonly IDictionary<int, IAccount> accounts;
        private readonly IDictionary<int, IBetLine> lines;
        private readonly IDictionary<string, IBetLine> linesData;
        private readonly IDictionary<int, IMatch> matches;
        private readonly IDictionary<int, IResult> results;
        private readonly IDictionary<int, IBet> bets;
        private readonly IDictionary<int, ITipster> tipsters;

        public BettingManagerData()
        {
            this.accounts = new Dictionary<int, IAccount>();
            this.lines = new Dictionary<int, IBetLine>();
            this.linesData = new Dictionary<string, IBetLine>();
            this.matches = new Dictionary<int, IMatch>();
            this.results = new Dictionary<int, IResult>();
            this.bets = new Dictionary<int, IBet>();
            this.tipsters = new Dictionary<int, ITipster>();
        }

        public void AddAccount(int accountId, IAccount account)
        {
            this.accounts.Add(accountId, account);
        }

        public void AddLine(int lineId, IBetLine line)
        {
            this.lines.Add(lineId, line);
            this.linesData.Add(line.Name, line);
        }

        public void AddMatch(int matchId, IMatch match)
        {
            this.matches.Add(matchId, match);
        }

        public IEnumerable<IMatch> PickAllMacthesAfterDate(DateTime date)
        {
            var sortedIncommingMatches = from t in this.matches.Values
                                         where t.Date > date
                                         orderby t.Date
                                         select t;

            return sortedIncommingMatches.ToList();
        }

        public IAccount PickAccountById(int accountId)
        {
            return this.accounts[accountId];
        }

        public IBetLine PickLineById(int lineId)
        {
            return this.lines[lineId];
        }

        public IBetLine PickLineByName(string lineName)
        {
            return this.linesData[lineName];
        }

        public IMatch PickMatchById(int matchId)
        {
            return this.matches[matchId];
        }

        public IEnumerable<IMatch> PickMatchesWithoutResults(DateTime date)
        {
            var matchesWithoutResults = from t in this.matches.Values
                                        where t.Date < date && t.Results.Count == 0
                                        orderby t.Date
                                        select t;

            return matchesWithoutResults.ToList();
        }

        public void AddResult(int resultId, IResult result)
        {
            this.results.Add(resultId, result);
        }

        public void AddBet(int betId, IBet bet)
        {
            this.bets.Add(betId, bet);
        }

        public void AddTipster(int tipsterId, ITipster tipster)
        {
            this.tipsters.Add(tipsterId, tipster);
        }

        public ITipster PickTipsterById(int tipsterId)
        {
            return this.tipsters[tipsterId];
        }

        public IBet GetBetById(int betId)
        {
            return this.bets[betId];
        }
    }
}
