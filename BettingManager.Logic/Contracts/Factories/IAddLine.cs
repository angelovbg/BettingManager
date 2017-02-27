using BettingManager.Logic.Contracts.Models;

namespace BettingManager.Logic.Contracts.Factories
{
    public interface IAddLine
    {
        void AddLine(int lineId, IBetLine line);
    }
}
