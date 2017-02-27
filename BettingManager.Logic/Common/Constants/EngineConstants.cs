namespace BettingManager.Logic.Common.Constants
{
    public static class EngineConstants
    {
        /*
        // Commands
        internal const string CreateCompanyCommand = "CreateCompany";
        internal const string AddFurnitureToCompanyCommand = "AddFurnitureToCompany";
        internal const string RemoveFurnitureFromCompanyCommand = "RemoveFurnitureFromCompany";
        internal const string FindFurnitureFromCompanyCommand = "FindFurnitureFromCompany";
        internal const string ShowCompanyCatalogCommand = "ShowCompanyCatalog";
        internal const string CreateTableCommand = "CreateTable";
        internal const string CreateChairCommand = "CreateChair";
        internal const string SetChairHeight = "SetChairHeight";
        internal const string ConvertChair = "ConvertChair";
        */

        // Error messages
        public const string ObjectCannotBeNullErrorMessage = "{0} cannot be null.";
        public const string BetAmountCannotBeNegativeErrorMessage = "Bet amount cannot be zero or negative.";
        public const string BetCoefficientCannotBeLessThenOneErrorMessage = "Coefficient cannot be less then 1.";
        public const string BetAmountIsTooBigErrorMessage = "Bet amount is too big.";
        public const string SameTeamsInMatchErrorMessage = "Team names cannot be the same.";
        public const string SameBetsForAMatchErrorMessage = "Cannot add same bet for a match!";
        public const string SameLinesToAnAcountErrorMessage = "Cannot add same line for an account!";
        public const string SameBetToALineErrorMessage = "Cannot add same bet for a line!";
        public const string SameBetResultssForAMatchErrorMessage = "Cannot add same result for a match!";
        public const string IncorrectRakeValueErrorMessage = "Current rake value is not correct!";
        public const string InvalidMarkTypeErrorMessage = "Mark type is not correct!";
        public const string InvalidTotalGoalsErrorMessage = "Invalid total goals!";
        public const string InvalidParamsDoubleResultErrorMessage = "Invalid params for double result mark!";
        public const string CannotAddBetToMatchWithResultErrorMessage = "Cannot add a bet to match, that already had result!";
        public const string AlreadyCompletedLineErrorMessage = "This line is already completly betted!";
        public const string BetIsGreaterThenNeededErrorMessage = "Bet is much greater than needed!";
        public const string NotEnoughAccountBalanceForBetErrorMessage = "Not enough resource in account for this bet!";
        public const string NotCheckedBetErrorMessage = "This bet is not checked for result.";
        public const string SomeValueCannotBeNegativeErrorMessage = "{0} cannot be negative!";
        /*
        // Success messages
        internal const string CompanyCreatedSuccessMessage = "Company {0} created";
        internal const string FurnitureAddedSuccessMessage = "Furniture {0} added to company {1}";
        internal const string FurnitureRemovedSuccessMessage = "Furniture {0} removed from company {1}";
        internal const string TableCreatedSuccessMessage = "Table {0} created";
        internal const string ChairCreatedSuccessMessage = "Chair {0} created";
        internal const string ChairHeightAdjustedSuccessMessage = "Chair {0} adjusted to height {1}";
        internal const string ChairStateConvertedSuccessMessage = "Chair {0} converted";
        */
    }
}
