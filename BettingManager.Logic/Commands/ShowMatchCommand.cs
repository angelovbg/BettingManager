using System.Collections.Generic;

using BettingManager.Logic.Contracts;
using BettingManager.Logic.Contracts.Factories;

namespace BettingManager.Logic.Commands
{
    public class ShowMatchCommand : ICommand
    {
        private readonly IGetMatchById getMatch;

        public ShowMatchCommand(IGetMatchById getMatch)
        {
            this.getMatch = getMatch;
        }

        public string Execute(IList<string> parameters)
        {
            var matchId = int.Parse(parameters[0]);

            var match = this.getMatch.PickMatchById(matchId);

            return match.ToString();
        }
    }
}
