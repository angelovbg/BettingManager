using System;

using BettingManager.Logic.Contracts;

namespace BettingManager.Logic.Provider
{
    public class ConsoleReaderProvider : IReader
    {
        public string ReadLine()
        {
            return Console.ReadLine();
        }
    }
}
