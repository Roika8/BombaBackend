using DATA;
using DATA.Enums;
using DATA.Instruments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BLL.MainPortfolio.Validators
{
    public class TrackingInstrumentValidator : ICommandValidator<TrackingInstrument>
    {
        public List<ErrorMessage> ValidateCommand(TrackingInstrument model)
        {
            var errorsList = new List<ErrorMessage>()
            {
                ValidatePrices(model.TrackingPrices),
                ValidateStockSymbol(model.Symbol),
            };

            errorsList.RemoveAll(err => err == ErrorMessage.NoErrors);
            return errorsList;
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

        private ErrorMessage ValidatePrices(ICollection<TrackingInstrumentPrice> trackingPrices)
        {
            if (!trackingPrices.Any())
            {
                return ErrorMessage.PriceError;
            }
            foreach (var price in trackingPrices)
            {
                if (price.Price <= 0)
                {
                    return ErrorMessage.PriceError;
                }
            }

            return ErrorMessage.NoErrors;
        }
    }
}
