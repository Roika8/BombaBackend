using BLL.MainPortfolio.Portfolios;
using BLL.PortfolioInstruments;
using BombaRestAPI.Properties.DTOs;
using DATA.Enums;
using DATA.Instruments;
using DATA.Portfolios;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Threading;
using BLL.HistorytPortfolio.Portfolios;
using BLL.HistorytPortfolio.PortfolioInstruments;

namespace BombaRestAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HistoryPortfolioController : BaseApiController
    {
        #region History Portfolio

        [HttpGet("GetPortfolio/{id}")]
        public async Task<ActionResult<HistoryPortfolio>> GetPortfolio(int id, CancellationToken cancellationToken)
        {
            return await Mediator.Send(new HistoryPortfolioGetter.Query { PortfolioID = id }, cancellationToken);
        }

        [Route("AddPortfolio")]
        [HttpPost]
        public async Task<IActionResult> AddNewPortfolio()
        {
            return Ok(await Mediator.Send(new CreateHistoryPortfolio.Command()));
        }

        //Edit portfolio . delete instruemnt
        //Edit instrument. edit data
        #endregion



        #region Instruments in portfolio

        [HttpDelete("DeleteInstrument/{instrumentID}")]
        public async Task<ActionResult<DeleteHistoryInstrument>> DeletePortfolioInstrument(int instrumentID)
        {
            return Ok(await Mediator.Send(new DeleteHistoryInstrument.Command { InstrumentID = instrumentID }));
        }

        [HttpPut("EditPortfolioInstrument/{instrumentID}")]
        public async Task<ActionResult<PortfolioInstrument>> EditPortfolioInstrument(int instrumentID, PortfolioInstrumentDto portfolioInstrumentDto)
        {
            PortfolioInstrument portfolioInstrument = new()
            {
                AvgPrice = portfolioInstrumentDto.AvgPrice,
                ChartPattern = (ChartPattern)portfolioInstrumentDto.ChartPattern,
                StopLoss = portfolioInstrumentDto.StopLoss,
                Symbol = portfolioInstrumentDto.Symbol,
                TakeProfit = portfolioInstrumentDto.TakeProfit,
                Units = portfolioInstrumentDto.Units,
                InstrumentID = instrumentID
            };
            return Ok(await Mediator.Send(new EditPortfolioInstrument.Command { PortfolioInstrument = portfolioInstrument }));
        }



        [HttpGet("GetSingleInstrument/{instrumentID}")]
        public async Task<ActionResult<PortfolioInstrument>> GetSinglePortfolioInstrument(int instrumentID)
        {
            return await Mediator.Send(new PortfolioInstrumentGetter.Query { InstrumentID = instrumentID });
        }

        [Route("AddInstrumentToPortfolio")]
        [HttpPost]
        public async Task<IActionResult> AddPortfolioInstrument(PortfolioInstrumentDto portfolioInstrumentDto)
        {
            PortfolioInstrument portfolioInstrument = new()
            {
                AvgPrice = portfolioInstrumentDto.AvgPrice,
                ChartPattern = (ChartPattern)portfolioInstrumentDto.ChartPattern,
                StopLoss = portfolioInstrumentDto.StopLoss,
                Symbol = portfolioInstrumentDto.Symbol,
                TakeProfit = portfolioInstrumentDto.TakeProfit,
                Units = portfolioInstrumentDto.Units,
            };
            portfolioInstrument.Portfolio.PortfolioID = portfolioInstrumentDto.PortfolioID;


            return Ok(await Mediator.Send(new CreatePortfolioInstrument.Command { Instrument = portfolioInstrument }));
        }
        #endregion

    }
}
