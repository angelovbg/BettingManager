using System;
using System.Collections.Generic;

using BettingManager.Logic.Contracts;
using BettingManager.Logic.Contracts.Factories;

namespace BettingManager.Logic.Commands
{
    public class ShowAccountCommand : ICommand
    {
        private readonly IGetAccountById getAccount;

        public  ShowAccountCommand(IGetAccountById getAccount)
        {
            this.getAccount = getAccount;
        }

        public string Execute(IList<string> parameters)
        {
            var accountId = int.Parse(parameters[0]);

            var account = this.getAccount.PickAccountById(accountId);

            return account.ToString();
        }
    }
}
