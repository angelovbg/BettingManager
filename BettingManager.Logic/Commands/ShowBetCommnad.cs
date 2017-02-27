using System;
using System.Collections.Generic;

using BettingManager.Logic.Contracts;
using BettingManager.Logic.Contracts.Factories;

namespace BettingManager.Logic.Commands
{
    class ShowBetCommnad : ICommand
    {
        private readonly IGetBetById getBet;

        public ShowBetCommnad(IGetBetById getBet)
        {
            this.getBet = getBet;
        }

        public string Execute(IList<string> parameters)
        {
            var betId = int.Parse(parameters[0]);

            var bet = this.getBet.GetBetById(betId);

            return bet.ToString();
        }
    }
}
