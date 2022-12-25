using BLL.HistorytPortfolio.PortfolioInstruments;
using BLL.HistorytPortfolio.Portfolios;
using BLL.PortfolioInstruments;
using BombaRestAPI.Properties.DTOs.InstumentsDtos;
using DATA.Instruments;
using DATA.Portfolios;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Threading;
using BLL.TrackingPortfolioHandler.Portfolios;
using BLL.TrackingPortfolioHandler.TrackingInstruments;
using System.Collections.Generic;
using DATA;
using System.Linq;
using BombaRestAPI.Properties.DTOs;
using System;

namespace BombaRestAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TrackingPortfolioController : BaseApiController
    {
        #region Tracking Portfolio

        [HttpGet("GetPortfolio/{id}")]
        public async Task<ActionResult<TrackingPortfolio>> GetPortfolio(Guid portfolioID, CancellationToken cancellationToken)
        {
            return await Mediator.Send(new GetTrackingPortfolio.Query { PortfolioID = portfolioID }, cancellationToken);
        }

        [Route("AddPortfolio")]
        [HttpPost]
        public async Task<IActionResult> AddNewPortfolio()
        {
            return Ok(await Mediator.Send(new CreateTrackingPortfolio.Command()));
        }
        #endregion



        #region Instruments in tracking portfolio

        [HttpDelete("DeleteInstrument/{instrumentID}")]
        public async Task<ActionResult<DeleteTrackingInstrument>> DeleteTrackingInstrument(int instrumentID)
        {
            return Ok(await Mediator.Send(new DeleteTrackingInstrument.Command { InstrumentID = instrumentID }));
        }

        [HttpPut("EditInstrument/{instrumentID}")]
        public async Task<ActionResult<EditTrackingInstrument>> EditTrackingInstrument(Guid instrumentID, TrackingInstrumentDto trackingInstrumentDto)
        {
            var trackingPricesList = trackingInstrumentDto.TrackingPrices.Select(price =>
                                  new TrackingInstumentPrice
                                  {
                                      InstrumentID = instrumentID,
                                      Price = price
                                  }).ToList();

            TrackingInstrument trackingInstrument = new()
            {
                InstrumentID = instrumentID,
                TrackingPrices = trackingPricesList,
            };
            return Ok(await Mediator.Send(new EditTrackingInstrument.Command { TrackingInstrument = trackingInstrument }));
        }

        [HttpGet("GetSingleInstrument/{instrumentID}")]
        public async Task<ActionResult<TrackingInstrument>> GetSingleTrackingInstrument(int instrumentID)
        {
            return await Mediator.Send(new GetTrackingInstrument.Query { InstrumentID = instrumentID });
        }

        [Route("AddInstrumentToTracking")]
        [HttpPost]
        public async Task<IActionResult> AddTrackingInstrument(Guid portfolioID, TrackingInstrumentDto trackingInstrumentDto)
        {
            var trackingPricesList = trackingInstrumentDto.TrackingPrices.Select(price =>
                                  new TrackingInstumentPrice
                                  {
                                      Price = price
                                  }).ToList();

            TrackingInstrument trackingInstrument = new()
            {
                TrackingPrices = trackingPricesList,
                Symbol = trackingInstrumentDto.Symbol
            };


            trackingInstrument.Portfolio.PortfolioID = portfolioID;


            return Ok(await Mediator.Send(new CreateTrackingInstrument.Command { Instrument = trackingInstrument }));
        }
        #endregion
    }
}
