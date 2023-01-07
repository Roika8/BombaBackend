using BLL.MainPortfolio;
using BLL.MainPortfolio.Portfolios;
using BLL.PortfolioInstruments;
using BombaRestAPI.Controllers;
using BombaRestAPI.Properties.DTOs;
using BombaRestAPI.Properties.DTOs.PortfoliosDtos;
using DATA.Enums;
using DATA.Instruments;
using DATA.Portfolios;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
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

        [HttpGet("GetPortfolio/{portfolioID}")]
        public async Task<ActionResult<PortfolioDto>> GetPortfolio(Guid portfolioID, CancellationToken cancellationToken)
        {
            var portfolio = await Mediator.Send(new GetMainPortfolio.Query { PortfolioID = portfolioID }, cancellationToken);
            var portfolioInstrumentsDto = new List<PortfolioInstrumentDto>();

            portfolio.Instruments.ToList().ForEach(instrument => portfolioInstrumentsDto.Add(new PortfolioInstrumentDto
            {
                AvgPrice = instrument.AvgPrice,
                ChartPattern = instrument.ChartPattern,
                StopLoss = instrument.StopLoss,
                Symbol = instrument.Symbol,
                TakeProfit = instrument.TakeProfit,
                Units = instrument.Units
            }));

            return new PortfolioDto
            {
                PortfolioID = portfolioID,
                UserID = portfolio.UserID,
                PortfolioInstruments = portfolioInstrumentsDto
            };
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
            try
            {
                var res = await Mediator.Send(new DeletePortfolioInstrument.Command { InstrumentID = instrumentID });
                return Ok(res);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

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
                InstrumentId = instrumentID,
            };
            var res = await Mediator.Send(new EditPortfolioInstrument.Command { PortfolioInstrument = portfolioInstrument });
            return Ok(res);
        }



        [HttpGet("GetSingleInstrument/{instrumentID}")]
        public async Task<ActionResult<PortfolioInstrumentDto>> GetSinglePortfolioInstrument(int instrumentID)
        {
            var instrumentData = await Mediator.Send(new GetPortfolioInstrument.Query { InstrumentID = instrumentID });
            return new PortfolioInstrumentDto
            {
                AvgPrice = instrumentData.AvgPrice,
                ChartPattern = instrumentData.ChartPattern,
                StopLoss = instrumentData.StopLoss,
                Symbol = instrumentData.Symbol,
                TakeProfit = instrumentData.TakeProfit,
                Units = instrumentData.Units
            };
        }

        [Route("AddInstrumentToPortfolio")]
        [HttpPost]
        public async Task<IActionResult> AddPortfolioInstrument(Guid portfolioID, PortfolioInstrumentDto portfolioInstrumentDto)
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
            return Ok(await Mediator.Send(new CreatePortfolioInstrument.Command { Instrument = portfolioInstrument, PortfolioId = portfolioID }));
        }
        #endregion

    }
}
