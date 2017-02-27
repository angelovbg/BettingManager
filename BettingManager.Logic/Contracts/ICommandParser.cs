namespace BettingManager.Logic.Contracts
{
    using System.Collections.Generic;

    public interface ICommandParser
    {
        string Name { get; }

        IList<string> Parameters { get; }
    }
}
