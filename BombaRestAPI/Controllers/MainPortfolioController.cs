using BLL.MainPortfolio;
using BLL.PortfolioInstruments;
using BombaRestAPI.Controllers;
using DATA;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace BombaAPI.Controllers
{
    [AllowAnonymous] //Change it after
    [ApiController]
    [Route("api/[controller]")]
    public class MainPortfolioController : BaseApiController
    {
        private readonly ILogger<MainPortfolioController> _logger;


        [HttpGet("GetPortfolio/{id}")]
        public async Task<ActionResult<Portfolio>> GetPortfolio(int id)
        {
            return await Mediator.Send(new MainPortfolioDetails.Query { PortfolioID = id });
        }
        [Route("AddPortfolio")]
        [HttpPost]
        public async Task<IActionResult> AddNewPortfolio()
        {
            return Ok(await Mediator.Send(new CreatePortfolio.Command()));
        }


        [HttpGet("GetSingleInstrument/{id}")]
        public async Task<ActionResult<PortfolioInstrument>> GetPortfolioInstrument(int id)
        {
            return await Mediator.Send(new InstrumentDetails.Query { InstrumentID = id });
        }

        [HttpGet("GetAllPortfolioInstruments")]
        public async Task<ActionResult<List<PortfolioInstrument>>> GetAllPortfolioInstruments()
        {
            return await Mediator.Send(new List.Query());
        }


        [Route("AddInstrument")]
        [HttpPost]
        public async Task<IActionResult> AddPortfolioInstrument(PortfolioInstrument portfolioInstrument)
        {
            return Ok(await Mediator.Send(new Create.Command { Instrument = portfolioInstrument }));
        }

    }
}
