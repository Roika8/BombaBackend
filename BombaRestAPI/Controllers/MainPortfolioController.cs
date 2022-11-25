using BLL.MainPortfolio;
using BLL.PortfolioInstruments;
using BombaRestAPI.Controllers;
using BombaRestAPI.Properties.DTOs;
using DATA;
using DATA.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
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
            return await Mediator.Send(new MainPortfolioDetails.Query { PortfolioID = id }, cancellationToken);
        }

        [Route("AddPortfolio")]
        [HttpPost]
        public async Task<IActionResult> AddNewPortfolio()
        {
            return Ok(await Mediator.Send(new CreatePortfolio.Command()));
        }
        //Edit portfolio . delete instruemnt
        //Edit instrument. edit data
        #endregion
        #region Instruments in portfolio

        [HttpDelete("DeleteInstrument/{instrumentID}")]
        public async Task<ActionResult<PortfolioInstrument>> DeletePortfolioInstrument(int instrumentID)
        {
            return Ok(await Mediator.Send(new DeleteInstrument.Command { InstrumentID = instrumentID }));
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
            return Ok(await Mediator.Send(new EditInstrument.Command { PortfolioInstrument = portfolioInstrument }));
        }



        [HttpGet("GetSingleInstrument/{instrumentID}")]
        public async Task<ActionResult<PortfolioInstrument>> GetSinglePortfolioInstrument(int instrumentID)
        {
            return await Mediator.Send(new InstrumentDetails.Query { InstrumentID = instrumentID });
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


            return Ok(await Mediator.Send(new Create.Command { Instrument = portfolioInstrument }));
        }
        #endregion

    }
}
