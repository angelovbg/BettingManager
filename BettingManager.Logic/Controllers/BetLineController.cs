namespace BettingManager.Logic.Controllers
{
    using System;
    using System.Collections.Generic;

    using Contracts.Controllers;
    using Contracts.Models;
    using Models;

    public class BetLineController : IBetLineController
    {
        /*
        public IBetLine AddLine(IList<string> input, IAccount account)
        {
            string name = input[1];
            decimal stepAmount = decimal.Parse(input[2]);
            var line = new BetLine(account, name, stepAmount);

            return line;
        }
        */
        public IBetLine AddLine(IList<string> input, IAccount account)
        {
            throw new NotImplementedException();
        }
    }
}
