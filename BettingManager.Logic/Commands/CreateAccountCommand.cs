using System;
using System.Collections.Generic;

using BettingManager.Logic.Common.Constants;
using BettingManager.Logic.Contracts;
using BettingManager.Logic.Contracts.Factories;

namespace BettingManager.Logic.Commands
{
    public class CreateAccountCommand : ICommand
    {
        private readonly IAddAccount accounts;
        private readonly IAccountFactory accountFactory;
        private int currentAccountId = 1;
                   
        public CreateAccountCommand(IAccountFactory accountFactory, IAddAccount accounts)
        {
            this.accounts = accounts;
            this.accountFactory = accountFactory;
        }

        public string Execute(IList<string> parameters)
        {
            var name = parameters[0];
            var balance = decimal.Parse(parameters[1]);
            var currentRake = decimal.Parse(parameters[2]);
            var currencyName = parameters[3];

            CurrencyType currency = (CurrencyType)Enum.Parse(typeof(CurrencyType), currencyName);

            var account = this.accountFactory.CreateAccount(name, balance, currency, currentRake);
            this.accounts.AddAccount(currentAccountId, account);
            
            return string.Format("Account {0} has been added with ID {1}.", account.Name, this.currentAccountId++);
        }
    }
}
