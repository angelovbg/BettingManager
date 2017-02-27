using System.Collections.Generic;

using BettingManager.Logic.Common.Constants;
using BettingManager.Logic.Contracts;
using BettingManager.Logic.Contracts.Factories;

namespace BettingManager.Logic.Commands
{
    public class CreateTipsterCommand : ICommand
    { 
        private int currentTipsterId = 1;
        private readonly ITipsterFactory tipsterFactory;
        private readonly IAddTipster addTipster;

        public CreateTipsterCommand(ITipsterFactory tipsterFactory, IAddTipster addTipster)
        {
            this.tipsterFactory = tipsterFactory;
            this.addTipster = addTipster;
        }

        public string Execute(IList<string> parameters)
        {
            var name = parameters[0];
            var company = TipsterCompany.Myself;

            var tipster = tipsterFactory.CreateTipster(name, company);
            this.addTipster.AddTipster(currentTipsterId, tipster);

            return string.Format("Tipster named {0} has been added with ID {1}.", tipster.Name, this.currentTipsterId++);
        }
    }
}
