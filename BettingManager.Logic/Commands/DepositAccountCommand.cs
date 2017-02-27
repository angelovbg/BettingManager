using System.Collections.Generic;
using BettingManager.Logic.Contracts;
using BettingManager.Logic.Contracts.Factories;

namespace BettingManager.Logic.Commands
{
    public class DepositAccountCommand : ICommand
    {
        private readonly IGetAccountById getAccount;

        public DepositAccountCommand(IGetAccountById getAccount)
        {
            this.getAccount = getAccount;
        }

        public string Execute(IList<string> parameters)
        {
            var accountId = int.Parse(parameters[0]);
            var amount = decimal.Parse(parameters[1]);

            var account = this.getAccount.PickAccountById(accountId);
            account.DepositBalance(amount);

            return string.Format("{0} {1} has been deposited to account {2}", amount, account.Currency, account.Name);
        }
    }
}
