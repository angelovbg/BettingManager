using System;

using BettingManager.Logic.Contracts;

namespace BettingManager.Logic.Provider
{
    public class ConsoleWriterProvider : IWriter
    {
        public void Write(string message)
        {
            Console.Write(message);
        }

        public void WriteLine(string message)
        {
            Console.WriteLine(message);
        }
    }
}
