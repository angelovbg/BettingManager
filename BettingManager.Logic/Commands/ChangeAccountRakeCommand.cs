using System.Collections.Generic;

using BettingManager.Logic.Contracts;
using BettingManager.Logic.Contracts.Factories;

namespace BettingManager.Logic.Commands
{
    public class ChangeAccountRakeCommand : ICommand
    {
        private readonly IGetAccountById getAccount;

        public ChangeAccountRakeCommand(IGetAccountById getAccount)
        {
            this.getAccount = getAccount;
        }

        public string Execute(IList<string> parameters)
        {
            var accountId = int.Parse(parameters[0]);
            var rake = decimal.Parse(parameters[1]);

            var account = this.getAccount.PickAccountById(accountId);
            account.CurrentRake = rake;

            return string.Format("Rake for account {0} has been changed to {1}.", account.Name, account.CurrentRake);
        }
    }
}
