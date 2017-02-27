using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using BettingManager.Logic.Contracts;
using BettingManager.Logic.Contracts.Factories;

namespace BettingManager.Logic.Commands
{
    public class ShowAllMatchesWithoutResultsCommand : ICommand
    {
        private readonly IGetMatchesWithoutResults matches;

        public ShowAllMatchesWithoutResultsCommand(IGetMatchesWithoutResults matches)
        {
            this.matches = matches;
        }

        public string Execute(IList<string> parameters)
        {
            var matchesWithoutResults = this.matches.PickMatchesWithoutResults(DateTime.Now);
           
            if (matchesWithoutResults.ToList().Count > 0)
            {
                var result = new StringBuilder();

                foreach (var match in matchesWithoutResults)
                {
                    result.Append(match.ToString());
                }

                return result.ToString();
            }
            else
            {
                return string.Format("There are no past matches without result");
            }
        }
    }
}
