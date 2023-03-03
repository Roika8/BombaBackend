﻿

using System.ComponentModel;

namespace DATA.Enums
{
    public enum ErrorMessage
    {
        NoErrors,

        [Description("Units cannot be less then 1")]
        UnitsError,

        [Description("Average Price cannot be less then 0")]
        AveragePriceError,

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
    }
}