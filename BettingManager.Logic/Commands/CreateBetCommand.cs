using System;
using System.Collections.Generic;

using BettingManager.Logic.Common.Constants;
using BettingManager.Logic.Contracts;
using BettingManager.Logic.Contracts.Factories;
using BettingManager.Logic.Contracts.Models;

namespace BettingManager.Logic.Commands
{
    public class CreateBetCommand : ICommand
    {
        private int currentBetId = 1;
        private readonly IBetFactory betFactory;
        private readonly IAddBet addBet;
        private readonly IGetMatchById getMatch;
        private readonly IGetLineByName getLine;
        private readonly IGetTipsterById getTipster;
        private string regularMark = string.Empty;
        private string doubleSignMark = string.Empty;
        private string underOverMark = string.Empty;

        public CreateBetCommand(IBetFactory betFactory, IAddBet addBet, IGetMatchById getMatch, IGetLineByName getLine, IGetTipsterById getTipster)
        {
            this.betFactory = betFactory;
            this.addBet = addBet;
            this.getMatch = getMatch;
            this.getLine = getLine;
            this.getTipster = getTipster;
        }

        public string Execute(IList<string> parameters)
        {
            var matchId = int.Parse(parameters[0]);
            //var lineId = int.Parse(parameters[1]);
            var lineName = parameters[1];
            var tipsterId = int.Parse(parameters[5]);

            var match = getMatch.PickMatchById(matchId);
            //var line = getLine.PickLineById(lineId);
            var line = getLine.PickLineByName(lineName);

            var tipster = this.getTipster.PickTipsterById(tipsterId);

            var inputMark = this.TransformMark(parameters[2]);

            var mark = this.GetCorrectMarkType(inputMark);

            var amount = decimal.Parse(parameters[3]);
            var coefficient = decimal.Parse(parameters[4]);

            IBet bet;

            if(this.regularMark.Length > 0)
            {
                bet = betFactory.GetRegularBet(match, line, mark, amount, coefficient, tipster);
            }
            else if (this.doubleSignMark.Length > 0)
            {
                bet = betFactory.GetDoubleSignBet(match, line, mark, amount, coefficient, tipster);
            }
            else if (this.underOverMark.Length > 0)
            {
                bet = betFactory.GetUnderOverBet(match, line, mark, amount, coefficient, tipster);
            }
            else
            {
                throw new ArgumentException(EngineConstants.InvalidMarkTypeErrorMessage);
            }
            
            addBet.AddBet(currentBetId, bet);

            return string.Format("Bet for match {0} has been added with ID {1}.", bet.Match.Id, this.currentBetId++);
        }

        public string TransformMark(string input)
        {
            if (input == "1")
            {
                return "_1";
            }
            else if (input == "2")
            {
                return "_2";
            }
            else if (input == "1X")
            {
                return "_1X";
            }
            else if (input == "12")
            {
                return "_12";
            }
            else
            {
                return input;
            }
        }

        private string GetCorrectMarkType(string mark)
        {
            this.regularMark = string.Empty;
            this.doubleSignMark = string.Empty;
            this.underOverMark = string.Empty;

            if (mark == FinalResultMarks._1.ToString()
               || mark == FinalResultMarks.X.ToString()
               || mark == FinalResultMarks._2.ToString())
            {
                this.regularMark = mark;
                return mark;
            }
            else if (mark == FinalResultDouble._1X.ToString()
                || mark == FinalResultDouble.X2.ToString()
                || mark == FinalResultDouble._12.ToString())
            {
                this.doubleSignMark = mark;
                return mark;
            }
            else if (mark == FinalResultUnderOver.Over0_5.ToString()
                || mark == FinalResultUnderOver.Over1_5.ToString()
                || mark == FinalResultUnderOver.Over2_5.ToString()
                || mark == FinalResultUnderOver.Over3_5.ToString()
                || mark == FinalResultUnderOver.Over5_5.ToString()
                || mark == FinalResultUnderOver.Under0_5.ToString()
                || mark == FinalResultUnderOver.Under1_5.ToString()
                || mark == FinalResultUnderOver.Under2_5.ToString()
                || mark == FinalResultUnderOver.Under3_5.ToString()
                || mark == FinalResultUnderOver.Under5_5.ToString())
            {
                this.underOverMark = mark;
                return mark;
            }
            else
            {
                throw new ArgumentException(EngineConstants.InvalidMarkTypeErrorMessage);
            }
        }
    }
}
