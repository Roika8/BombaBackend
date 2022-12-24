using BLL.MainPortfolio;
using BLL.MainPortfolio.Portfolios;
using BLL.PortfolioInstruments;
using BombaRestAPI.Controllers;
using BombaRestAPI.Properties.DTOs;
using BombaRestAPI.Properties.DTOs.InstumentsDtos;
using DATA.Enums;
using DATA.Instruments;
using DATA.Portfolios;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading;
using System.Threading.Tasks;

namespace BombaAPI.Controllers
{
    [AllowAnonymous] //Change it after
    [ApiController]
    [Route("api/[controller]")]
    public class MainPortfolioController : BaseApiController
    {

        #region Portfolio

        [HttpGet("GetPortfolio/{id}")]
        public async Task<ActionResult<Portfolio>> GetPortfolio(int id, CancellationToken cancellationToken)
        {
            return await Mediator.Send(new GetMainPortfolio.Query { PortfolioID = id }, cancellationToken);
        }

        [Route("AddPortfolio")]
        [HttpPost]
        public async Task<IActionResult> AddNewPortfolio()
        {
            return Ok(await Mediator.Send(new CreatePortfolio.Command()));
        }
        #endregion



        #region Instruments in portfolio

        [HttpDelete("DeleteInstrument/{instrumentID}")]
        public async Task<ActionResult<PortfolioInstrument>> DeletePortfolioInstrument(int instrumentID)
        {
            return Ok(await Mediator.Send(new DeletePortfolioInstrument.Command { InstrumentID = instrumentID }));
        }

        [HttpPut("EditPortfolioInstrument/{instrumentID}")]
        public async Task<ActionResult<PortfolioInstrument>> EditPortfolioInstrument(int instrumentID, PortfolioInstrumentDto portfolioInstrumentDto)
        {
            PortfolioInstrument portfolioInstrument = new()
            {
                AvgPrice = portfolioInstrumentDto.AvgPrice,
                ChartPattern = (ChartPattern)portfolioInstrumentDto.ChartPattern,
                StopLoss = portfolioInstrumentDto.StopLoss,
                TakeProfit = portfolioInstrumentDto.TakeProfit,
                Units = portfolioInstrumentDto.Units,
                InstrumentID = instrumentID,
            };
            return Ok(await Mediator.Send(new EditPortfolioInstrument.Command { PortfolioInstrument = portfolioInstrument }));
        }



        [HttpGet("GetSingleInstrument/{instrumentID}")]
        public async Task<ActionResult<PortfolioInstrument>> GetSinglePortfolioInstrument(int instrumentID)
        {
            return await Mediator.Send(new GetPortfolioInstrument.Query { InstrumentID = instrumentID });
        }

        [Route("AddInstrumentToPortfolio")]
        [HttpPost]
        public async Task<IActionResult> AddPortfolioInstrument(int portfolioID, PortfolioInstrumentDto portfolioInstrumentDto)
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
            portfolioInstrument.Portfolio.PortfolioID = portfolioID;


            return Ok(await Mediator.Send(new CreatePortfolioInstrument.Command { Instrument = portfolioInstrument }));
        }
        #endregion

    }
}
