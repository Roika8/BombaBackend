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

        [Route("AddPortfolio")]
        [HttpPost]
        public async Task<IActionResult> AddPortfolio([FromBody] Portfolio portfolio)
        {
            try
            {
                bool res = await _mainPortfolioService.AddPortfolio(new Portfolio
                {
                    Instruments = portfolio.Instruments,
                    User = portfolio.User,
                });
                return res ? Ok() : BadRequest();
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return BadRequest(e.Message);
            }
        }
    }
}
