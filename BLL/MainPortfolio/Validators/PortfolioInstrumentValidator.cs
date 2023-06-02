using DATA.Enums;
using DATA.Instruments;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace BLL.MainPortfolio.Validators
{
    public class PortfolioInstrumentValidator : ICommandValidator<PortfolioInstrument>
    {
        public List<ErrorMessage> ValidateCommand(PortfolioInstrument model)
        {
            var errorsList = new List<ErrorMessage>
            {
                ValidateStockSymbol(model.Symbol),
                ValidateTakeProfit(model.TakeProfit),
                ValidateStopLoss(model.StopLoss),
                ValidateChartPattern(model.ChartPattern),
                ValidateAveragePrice(model.AvgPrice),
                ValidateUnits(model.Units)
            };
            errorsList.RemoveAll(err => err == ErrorMessage.NoErrors);
            return errorsList;
        }

        private ErrorMessage ValidateUnits(decimal units)
        {
            if (units < 1)
            {
                return ErrorMessage.UnitsError;
            }
            return ErrorMessage.NoErrors;
        }

        private ErrorMessage ValidateAveragePrice(decimal avgPrice)
        {
            if (avgPrice < 0)
            {
                return ErrorMessage.PriceError;
            }
            return ErrorMessage.NoErrors;
        }

        private ErrorMessage ValidateChartPattern(int? chartPattern)
        {
            if (chartPattern != null)
            {
                if (!Enum.IsDefined(typeof(ChartPattern), chartPattern))
                {
                    return ErrorMessage.ChartPatternError;
                }
            }
            return ErrorMessage.NoErrors;
        }
        private ErrorMessage ValidateStopLoss(decimal? stopLoss)
        {
            if (stopLoss < 0)
            {
                return ErrorMessage.StopLossError;
            }
            return ErrorMessage.NoErrors;
        }

        private ErrorMessage ValidateTakeProfit(decimal? takeProfit)
        {
            if (takeProfit < 0)
            {
                return ErrorMessage.TakeProfitError;
            }
            return ErrorMessage.NoErrors;
        }

        private ErrorMessage ValidateStockSymbol(string symbol)
        {
            if (string.IsNullOrEmpty(symbol))
            {
                return ErrorMessage.SymbolEmptyError;
            }
            if (symbol.Length > 4)
            {
                return ErrorMessage.SymboLengthError;
            }
            if (string.IsNullOrWhiteSpace(symbol) || !Regex.IsMatch(symbol, "^[a-zA-Z]*$"))
            {
                return ErrorMessage.SymbolFormatError;
            }
            return ErrorMessage.NoErrors;
        }
    }
}
