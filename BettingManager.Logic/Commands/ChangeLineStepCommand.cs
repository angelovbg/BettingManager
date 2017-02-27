using System;
using System.Collections.Generic;
using BettingManager.Logic.Contracts;
using BettingManager.Logic.Contracts.Factories;

namespace BettingManager.Logic.Commands
{
    public class ChangeLineStepCommand : ICommand
    {
        private readonly IGetLineByName getLine;

        public ChangeLineStepCommand(IGetLineByName getLine)
        {
            this.getLine = getLine;
        }

        public string Execute(IList<string> parameters)
        {
            //var lineId = int.Parse(parameters[0]);
            var lineName = parameters[0];
            var step = decimal.Parse(parameters[1]);

            //var line = this.getLine.PickLineById(lineId);
            var line = this.getLine.PickLineByName(lineName);
            line.StepAmount = step;
            line.DefaultStepAmount = step;
            return string.Format("Step for Line {0} has been changed to {1:F2}", line.Name, line.StepAmount);
        }
    }
}
