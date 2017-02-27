using System.Collections.Generic;

using BettingManager.Logic.Contracts;
using BettingManager.Logic.Contracts.Factories;

namespace BettingManager.Logic.Commands
{
    public class CheckNextBetCommand : ICommand
    {
        private readonly IGetLineByName getLine;

        public CheckNextBetCommand(IGetLineByName getLine)
        {
            this.getLine = getLine;
        }

        public string Execute(IList<string> parameters)
        {
            //var lineId = int.Parse(parameters[0]);
            //var line = this.getLine.PickLineById(lineId);
            var lineName = parameters[0];
            var line = this.getLine.PickLineByName(lineName);
            var coefficient = decimal.Parse(parameters[1]);

            var valueToBet = line.ChechValueToBeBet(coefficient);

            return string.Format("Bet value at coefficiet {1:F2} must be: {0:F2} - Line: {2}", valueToBet, coefficient, line.Name);
        }
    }
}
