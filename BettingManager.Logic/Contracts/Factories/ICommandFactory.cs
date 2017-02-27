using System;

namespace BettingManager.Logic.Contracts.Factories
{
    public interface ICommandFactory
    {
        ICommand GetCommand(Type type);
    }
}
