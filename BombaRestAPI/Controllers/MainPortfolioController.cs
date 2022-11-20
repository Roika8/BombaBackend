using BLL.PortfolioInstruments;
using BombaRestAPI.Controllers;
using DATA;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BombaAPI.Controllers
{
    [AllowAnonymous] //Change it after
    [ApiController]
    [Route("api/[controller]")]
    public class MainPortfolioController : BaseApiController
    {
        private readonly ILogger<MainPortfolioController> _logger;



        [Route("AddInstrument")]
        [HttpPost]
        public async Task<ActionResult<PortfolioInstrument>> AddInstrumentToPortfolio([FromBody] PortfolioInstrument portfolioInstrument)
        {
            try
            {
                return Ok();


            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return BadRequest(e.Message);
            }
        }

        [HttpGet("GetSingleInstrument/{id}")]
        public async Task<ActionResult<PortfolioInstrument>> GetPortfolio(int id)
        {
            return await Mediator.Send(new InstrumentDetails.Query { InstrumentID = id });
        }

        [HttpGet("GetAllPortfolioInstruments")]
        public async Task<ActionResult<List<PortfolioInstrument>>> GetAllPortfolioInstruments()
        {
            return await Mediator.Send(new List.Query());
        }

    }
}
