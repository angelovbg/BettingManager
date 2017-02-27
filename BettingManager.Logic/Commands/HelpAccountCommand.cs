using System.Collections.Generic;

using BettingManager.Logic.Contracts;

namespace BettingManager.Logic.Commands
{
    public class HelpAccountCommand : ICommand
    {
        public string Execute(IList<string> parameters)
        {
            return string.Format("CreateAccount id balance rake currency");
        }
    }
}
