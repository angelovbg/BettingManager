using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using BettingManager.Logic.Contracts;
using BettingManager.Logic.Contracts.Factories;


namespace BettingManager.Logic.Commands
{
    public class ShowIncommingMatchesCommand : ICommand
    {
        private readonly IGetAllMacthesAfterDate matches;

        public ShowIncommingMatchesCommand(IGetAllMacthesAfterDate matches)
        {
            this.matches = matches;
        }

        public string Execute(IList<string> parameters)
        {
            var sortedIncommingMatches = this.matches.PickAllMacthesAfterDate(DateTime.Now);

            if (sortedIncommingMatches.ToList().Count > 0)
            {
                var result = new StringBuilder();

                foreach (var match in sortedIncommingMatches)
                {
                    result.Append(match.ToString());
                   
                }

                return result.ToString();
            }
            else
            {
                return string.Format("There is no one incomming match");
            }
        }
    }
}
