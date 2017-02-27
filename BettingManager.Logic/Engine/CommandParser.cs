namespace BettingManager.Logic.Engine
{
    using System;
    using System.Collections.Generic;

    using Contracts;

    public class CommandParser : ICommandParser
    {
        private const char SplitCommandSymbol = ' ';

        private string name;
        private IList<string> parameters;

        private CommandParser(string input)
        {
            this.TranslateInput(input);
        }

        public string Name
        {
            get
            {
                return this.name;
            }

            private set
            {
                if (value.Length < 4)
                {
                    throw new ArgumentException("Command name is too short.");
                }

                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentNullException("Name cannot be null or empty.");
                }

                this.name = value;
            }
        }

        public IList<string> Parameters
        {
            get
            {
                return this.parameters;
            }

            private set
            {
                /*
                if (value == null)
                {
                    throw new ArgumentNullException("List of strings cannot be null.");
                }

                if (value.Count == 0)
                {
                    throw new NullReferenceException("Parameters cannot be empty.");
                }
                */
                this.parameters = value;
            }
        }

        /// <exception cref="System.ArgumentException">Throw when name is empty.</exception>
        /// <exception cref="InvalidOperationException">Throw when name is incorrect or empty.</exception>
        public static CommandParser Parse(string input)
        {
            return new CommandParser(input);
        }

        private void TranslateInput(string input)
        {
            var indexOfFirstSeparator = input.IndexOf(SplitCommandSymbol);
            
            if (indexOfFirstSeparator >= 0)
            {
                this.Name = input.Substring(0, indexOfFirstSeparator);
                this.Parameters = input.Substring(indexOfFirstSeparator + 1).Split(
                    new[] { SplitCommandSymbol }, StringSplitOptions.RemoveEmptyEntries);
            }
            else
            {
                this.Name = input;
            }
        }
    }
}
