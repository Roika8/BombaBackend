using DATA.Enums;
using DATA.Instruments;
using System;
using System.Linq;
using System.Text;

namespace BLL.MainPortfolio.Validators
{
    public class EditPortfolioInstrumentValidator : ICommandValidator<PortfolioInstrument>
    {
        public string ValidateCommand(PortfolioInstrument model)
        {
            var sb = new StringBuilder();

            sb.AppendLine(ValidateStockSymbol(model.Symbol));
            sb.AppendLine(ValidateTakeProfit(model.TakeProfit));
            sb.AppendLine(ValidateStopLoss(model.StopLoss));
            sb.AppendLine(ValidateChartPattern(model.ChartPattern));
            sb.AppendLine(ValidateAveragePrice(model.AvgPrice));
            sb.AppendLine(ValidateUnits(model.Units));
            return sb.ToString().Trim();
        }

        private string ValidateUnits(decimal units)
        {
            if (units < 1)
            {
                return "Units cannot be less then 1";
            }
            return string.Empty;
        }

        private string ValidateAveragePrice(decimal avgPrice)
        {
            if (avgPrice < 0)
            {
                return "Average Price cannot be less then 0";
            }
            return string.Empty;
        }

        private string ValidateChartPattern(int? chartPattern)
        {
            int minValue = (int)Enum.GetValues(typeof(ChartPattern)).Cast<ChartPattern>().Min();
            int maxValue = (int)Enum.GetValues(typeof(ChartPattern)).Cast<ChartPattern>().Max();

            if (chartPattern != null)
            {
                if (!Enum.IsDefined(typeof(ChartPattern), chartPattern))
                {
                    return $"Invalid chartPattern entered, The range is {minValue} - {maxValue}, the requested value was: {chartPattern}";
                }
            }
            return string.Empty;
        }
        private string ValidateStopLoss(decimal? stopLoss)
        {
            if (stopLoss < 0)
            {
                return "StopLoss cannot be less then 0";
            }
            return string.Empty;
        }

        private string ValidateTakeProfit(decimal? takeProfit)
        {
            if (takeProfit < 0)
            {
                return "TakeProfit cannot be less then 0";
            }
            return string.Empty;
        }

        private string ValidateStockSymbol(string symbol)
        {
            if (string.IsNullOrEmpty(symbol))
            {
                return "Instrument symbol is required.";
            }
            if (symbol.Length > 4)
            {
                return "Symbol cannot be more then 4 digits long";
            }
            if (string.IsNullOrWhiteSpace(symbol))
            {
                return "Symbol cannot have white spaces";
            }
            return string.Empty;
        }
    }
}
