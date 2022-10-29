using BLL.Interfaces;
using DATA;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BombaAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MainPortfolioController : ControllerBase
    {
        private readonly ILogger<MainPortfolioController> _logger;
        private readonly IMainPortfolioService _mainPortfolioService;

        public MainPortfolioController(ILogger<MainPortfolioController> logger, IMainPortfolioService mainPortfolio)
        {
            _logger = logger;
            _mainPortfolioService = mainPortfolio;
        }

        [Route("AddInstrument")]
        [HttpPost]
        public async Task<IActionResult> AddInstrumentToPortfolio(PortfolioInstrument portfolioInstrument, Guid userID)
        {
            try
            {
                bool res = await _mainPortfolioService.AddInstrumentToPortfolio(portfolioInstrument, userID);
                return res ? Ok() : BadRequest();
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return BadRequest(e.Message);
            }
        }


        [Route("GetPortfolio")]
        [HttpGet]
        public async Task<IActionResult> GetPortfolio()
        {
             return Ok($"This is portfolio for : ");
        }
    }
}
