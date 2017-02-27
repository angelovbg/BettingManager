using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BettingManager.Logic.Contracts
{
    public interface IWriter
    {
        /// <summary>
        /// Writes data to a given source.
        /// </summary>
        /// <param name="message">The data to be written.</param>
        void Write(string message);

        /// <summary>
        /// Writes data to a given source and appends new line at the end.
        /// </summary>
        /// <param name="message">The data to be written.</param>
        void WriteLine(string message);
    }
}
