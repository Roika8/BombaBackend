using DATA.Instruments;
using DATA.Portfolios;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Threading;
using BLL.HistorytPortfolio.Portfolios;
using BLL.HistorytPortfolio.PortfolioInstruments;
using BombaRestAPI.Properties.DTOs.InstumentsDtos;
using System;

namespace BombaRestAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HistoryPortfolioController : BaseApiController
    {
        #region History Portfolio

        [HttpGet("GetPortfolio/{id}")]
        public async Task<ActionResult<HistoryPortfolio>> GetPortfolio(Guid portfolioId, CancellationToken cancellationToken)
        {
            return await Mediator.Send(new GetHistoryPortfolio.Query { PortfolioID = portfolioId }, cancellationToken);
        }

        [Route("AddPortfolio")]
        [HttpPost]
        public async Task<IActionResult> AddNewPortfolio()
        {
            return Ok(await Mediator.Send(new CreateHistoryPortfolio.Command()));
        }
        #endregion



        #region Instruments in  history portfolio

        [HttpDelete("DeleteInstrument/{instrumentID}")]
        public async Task<ActionResult<DeleteHistoryInstrument>> DeleteHistoryInstrument(int instrumentID)
        {
            return Ok(await Mediator.Send(new DeleteHistoryInstrument.Command { InstrumentID = instrumentID }));
        }

        [HttpPut("EditInstrument/{instrumentID}")]
        public async Task<ActionResult<EditHistoryInstrument>> EditHistoryInstrument(int instrumentID, HistoryInstrumentDto portfolioInstrumentDto)
        {
            HistoryInstrument historyInstrument = new()
            {
                ActionOccuredPrice = portfolioInstrumentDto.ActionOccuredPrice,
                ProfitLoss = portfolioInstrumentDto.ProfitLoss,
                RequestOccured = portfolioInstrumentDto.RequestOccured,
                Units = portfolioInstrumentDto.Units,
                InstrumentId = instrumentID
            };
            return Ok(await Mediator.Send(new EditHistoryInstrument.Command { HistoryInstrument = historyInstrument }));
        }

        [HttpGet("GetSingleInstrument/{instrumentID}")]
        public async Task<ActionResult<HistoryInstrument>> GetSingleHistoryInstrument(int instrumentID)
        {
            return await Mediator.Send(new GetHistoryInstrument.Query { InstrumentID = instrumentID });
        }

        [Route("AddInstrumentToHistory")]
        [HttpPost]
        public async Task<IActionResult> AddHistoryInstrument(Guid portfolioID, HistoryInstrumentDto historyInstrumentDto, CancellationToken cancellationToke = default)
        {
            HistoryInstrument historyInstrument = new()
            {
                ActionOccuredPrice = historyInstrumentDto.ActionOccuredPrice,
                ProfitLoss = historyInstrumentDto.ProfitLoss,
                RequestOccured = historyInstrumentDto.RequestOccured,
                Units = historyInstrumentDto.Units,
                Symbol = historyInstrumentDto.Symbol,
            };

            return Ok(await Mediator.Send(new CreateHistoryInstrument.Command { Instrument = historyInstrument, PortfolioId = portfolioID }));
        }
        #endregion

    }
}
