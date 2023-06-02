

using System.ComponentModel;

namespace DATA.Enums
{
    public enum ErrorMessage
    {
        NoErrors,

        [Description("Units cannot be less then 1")]
        UnitsError,

        [Description("Price cannot be less then 0")]
        PriceError,

        [Description("ChartPattern out of range")]
        ChartPatternError,

        [Description("StopLoss cannot be less then 0")]
        StopLossError,

        [Description("TakeProfit cannot be less then 0")]
        TakeProfitError,

        [Description("Instrument symbol is required")]
        SymbolEmptyError,

        [Description("Symbol cannot be more then 4 digits long")]
        SymboLengthError,

        [Description("Symbol cannot have white spaces and must contains letters only")]
        SymbolFormatError,

        [Description("Couldnt find portfolio")]
        PortfolioNotFoundError,

        [Description("Instrument Already exists in portfolio")]
        InstrumentExistsError,

        [Description("Instrument not found in portfolio")]
        InstrumentNotFoundError,

        [Description("Failed to add record from DB")]
        DatabaseAddRecordError,

        [Description("Failed to delete record from DB")]
        DatabaseDeleteRecordError,

        [Description("Failed to edit record from DB")]
        DatabaseEditRecordError,

        [Description("History datetime cannot be in the future")]
        HistoryDatetimeError
    }
}
