using DATA.Enums;
using DATA.Instruments;
using System;
using System.Collections.Generic;

namespace BLL.MainPortfolio.Validators
{
    public class HistoryInstrumentValidator : ICommandValidator<HistoryInstrument>
    {
        public decimal ActionOccuredPrice { get; set; }
        public decimal Units { get; set; }
        public decimal ProfitLoss { get; set; }
        public DateTime RequestOccured { get; set; }

        public List<ErrorMessage> ValidateCommand(HistoryInstrument model)
        {
            var errorsList = new List<ErrorMessage>
            {
                ValidateUnits(model.Units),
                ValidateActionOccuredPrice(model.ActionOccuredPrice),
                ValidateRequestOccured(model.RequestOccured)
            };
            errorsList.RemoveAll(err => err == ErrorMessage.NoErrors);
            return errorsList;
        }

        private ErrorMessage ValidateRequestOccured(DateTime requestOccured)
        {
            return requestOccured.Date > DateTime.Now
                ? ErrorMessage.HistoryDatetimeError
                : ErrorMessage.NoErrors;
        }


        private ErrorMessage ValidateActionOccuredPrice(decimal actionOccuredPrice)
        {
            if (actionOccuredPrice < 1)
            {
                return ErrorMessage.PriceError;
            }
            return ErrorMessage.NoErrors;
        }

        private ErrorMessage ValidateUnits(decimal units)
        {
            if (units < 1)
            {
                return ErrorMessage.UnitsError;
            }
            return ErrorMessage.NoErrors;
        }
    }
}
