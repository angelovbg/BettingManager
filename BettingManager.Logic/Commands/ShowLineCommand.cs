using System;
using System.Collections.Generic;

using BettingManager.Logic.Contracts;
using BettingManager.Logic.Contracts.Factories;

namespace BettingManager.Logic.Commands
{
    public class ShowLineCommand : ICommand
    {
        //private readonly IGetLineById getLine;
        private readonly IGetLineByName getLine;

        //public ShowLineCommand(IGetLineById getLine)
        public ShowLineCommand(IGetLineByName getLine)         
        {
            this.getLine = getLine;
        }

        public string Execute(IList<string> parameters)
        {
            //var lineId = int.Parse(parameters[0]);
            var lineName = parameters[0];

            //var line = this.getLine.PickLineById(lineId);
            var line = this.getLine.PickLineByName(lineName);

            return line.ToString();
        }
    }
}
