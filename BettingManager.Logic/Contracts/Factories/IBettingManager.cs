namespace BettingManager.Logic.Contracts.Factories
{
    public interface IBettingManager : IAddAccount, IGetAccountById, IAddLine, 
        IGetLineById, IAddMatch, IGetMatchById, IGetAllMacthesAfterDate, IGetMatchesWithoutResults,
        IAddResult, IAddBet, IAddTipster, IGetTipsterById, IGetBetById, IGetLineByName
    {

    }
}
