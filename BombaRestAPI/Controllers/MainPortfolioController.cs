using BLL.Core;
using BLL.MainPortfolio;
using BLL.MainPortfolio.Portfolios;
using BLL.PortfolioInstruments;
using BombaRestAPI.Controllers;
using BombaRestAPI.Properties.DTOs;
using BombaRestAPI.Properties.DTOs.PortfoliosDtos;
using DATA;
using DATA.Enums;
using DATA.Instruments;
using DATA.Portfolios;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

namespace BombaAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MainPortfolioController : BaseApiController
    {
        private readonly SignInManager<User> _signInManager;

        public MainPortfolioController(SignInManager<User> signInManager)
        {
            _signInManager = signInManager;
        }

        #region Portfolio

        [HttpGet("GetPortfolio/{portfolioID}")]
        public async Task<ActionResult<PortfolioDto>> GetPortfolio(Guid portfolioID, CancellationToken cancellationToken)
        {
            var result = await Mediator.Send(new GetMainPortfolio.Query { PortfolioID = portfolioID }, cancellationToken);
            if (result.IsSuccess && result.Value != null)
            {
                var portfolio = result.Value;
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

                return Ok(new PortfolioDto
                {
                    PortfolioID = portfolioID,
                    UserID = portfolio.UserID,
                    PortfolioInstruments = portfolioInstrumentsDto
                });
            }
            if (result.IsSuccess && result.Value == null)
            {
                return NotFound();
            }
            return BadRequest();
        }

        [Route("AddPortfolio")]
        [HttpPost]
        public async Task<IActionResult> AddNewPortfolio()
        {
            var foundUser = await _signInManager.UserManager.FindByEmailAsync(User.FindFirstValue(ClaimTypes.Email));

            return Ok(await Mediator.Send(new CreatePortfolio.Command { UserID = foundUser.Id }));
        }
        #endregion



        #region Instruments in portfolio

        [HttpDelete("DeleteInstrument/{instrumentID}")]
        public async Task<ActionResult<PortfolioInstrument>> DeletePortfolioInstrument(int instrumentID, CancellationToken cancellationToken = default)
        {
            try
            {
                var result = await Mediator.Send(new DeletePortfolioInstrument.Command { InstrumentID = instrumentID }, cancellationToken);

                if (result.IsSuccess)
                {
                    return Ok();
                }

                if (!result.IsSuccess)
                {
                    return NotFound(result.Errors);
                }
                return BadRequest(result.Errors);
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
                ChartPattern = portfolioInstrumentDto.ChartPattern,
                StopLoss = portfolioInstrumentDto.StopLoss,
                TakeProfit = portfolioInstrumentDto.TakeProfit,
                Units = portfolioInstrumentDto.Units,
                InstrumentId = instrumentID,
            };
            var res = await Mediator.Send(new EditPortfolioInstrumentCommand.Command { PortfolioInstrument = portfolioInstrument });
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
        public async Task<IActionResult> AddPortfolioInstrument(Guid portfolioID, PortfolioInstrumentDto portfolioInstrumentDto, CancellationToken cancellationToke = default)
        {
            try
            {
                PortfolioInstrument portfolioInstrument = new()
                {
                    AvgPrice = portfolioInstrumentDto.AvgPrice,
                    ChartPattern = portfolioInstrumentDto.ChartPattern,
                    StopLoss = portfolioInstrumentDto.StopLoss,
                    Symbol = portfolioInstrumentDto.Symbol,
                    TakeProfit = portfolioInstrumentDto.TakeProfit,
                    Units = portfolioInstrumentDto.Units,
                };
                var result = await Mediator.Send(new CreatePortfolioInstrument.Command { Instrument = portfolioInstrument, PortfolioId = portfolioID }, cancellationToke);
                if (result.IsSuccess && result.Value != null)
                {
                    return Ok();
                }
                if (result.IsSuccess && result.Value == null)
                {
                    return NotFound(result.Errors);
                }
                return BadRequest(result.Errors);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }

        #endregion

    }
}
