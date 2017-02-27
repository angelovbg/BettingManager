using System.Collections.Generic;

using BettingManager.Logic.Contracts;
using BettingManager.Logic.Contracts.Factories;

namespace BettingManager.Logic.Commands
{
    public class WithdrawAccountCommand : ICommand
    {
        private readonly IGetAccountById getAccount;

        public WithdrawAccountCommand(IGetAccountById getAccount)
        {
            this.getAccount = getAccount;
        }

        public string Execute(IList<string> parameters)
        {
            var accountId = int.Parse(parameters[0]);
            var amount = decimal.Parse(parameters[1]);

            var account = this.getAccount.PickAccountById(accountId);
            account.WithdrawBalance(amount);

            return string.Format("{0} {1} has been withdrawed from account {2}", amount, account.Currency, account.Name);
        }
    }
}
