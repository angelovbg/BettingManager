using System.Collections.Generic;

using BettingManager.Logic.Contracts;
using BettingManager.Logic.Contracts.Factories;

namespace BettingManager.Logic.Commands
{
    public class CreateLineCommand : ICommand
    {
        private readonly IBetLineFactory lineFactory;
        private readonly IAddLine lines;
        private readonly IGetAccountById getAccountById;
        private int currentLineId = 1;

        public CreateLineCommand(IBetLineFactory lineFactory, IAddLine addLine, IGetAccountById getAccountById)
        {
            this.lineFactory = lineFactory;
            this.lines = addLine;
            this.getAccountById = getAccountById;
        }

        public string Execute(IList<string> parameters)
        {
            var account = this.getAccountById.PickAccountById(int.Parse(parameters[0]));
            string name = parameters[1];
            decimal stepAmount = decimal.Parse(parameters[2]);
            decimal decreasingStepValue = decimal.Parse(parameters[3]);

            var line = this.lineFactory.AddLine(account, name, stepAmount, decreasingStepValue);
            this.lines.AddLine(currentLineId, line);
     
            return string.Format("Line {0} has been added with ID {1}.", line.Name, this.currentLineId++);
         }
    }
}
