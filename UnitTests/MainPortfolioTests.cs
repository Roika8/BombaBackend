using BLL.Core;
using BLL.MainPortfolio.Validators;
using BLL.PortfolioInstruments;
using BLL.Services;
using BombaAPI.Controllers;
using BombaRestAPI.Properties.DTOs;
using DAL;
using DATA.Enums;
using DATA.Instruments;
using DATA.Portfolios;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;

namespace UnitTests
{
    public class MainPortfolioTests : BaseTests
    {

        #region Add instrument 


        [Test]
        public async Task AddingNewPortfolioInstrument_InvalidArgument_GetErrorsList([Values] bool validAveragePrice, [Values] bool validSymbol,
            [Values] bool validUnits, [Values] bool validStopLoss, [Values] bool validTakeProfit, [Values] bool validChartPattern)
        {
            // Arrange
            var portfolioInstrument = new PortfolioInstrument
            {
                AvgPrice = validAveragePrice ? 10 : -1,
                ChartPattern = validChartPattern ? (int)ChartPattern.Flag : (int)ChartPattern.Flag + 1,
                StopLoss = validStopLoss ? 10 : -1,
                TakeProfit = validTakeProfit ? 10 : -1,
                Symbol = validSymbol ? "SMBL" : "TooLongSymbol",
                Units = validUnits ? 3 : -1
            };

            var command = new CreatePortfolioInstrument.Command { PortfolioId = Guid.NewGuid(), Instrument = portfolioInstrument };
            var handler = new CreatePortfolioInstrument.Handler(_dbContext, _validator);
            var result = await handler.Handle(command, CancellationToken.None);

            Assert.Multiple(() =>
            {

                if (validAveragePrice)
                {
                    Assert.That(result.Errors, Does.Not.Contain(ErrorConvertor.ConvertError(ErrorMessage.PriceError)));
                }
                else
                {
                    Assert.That(result.Errors, Does.Contain(ErrorConvertor.ConvertError(ErrorMessage.PriceError)));
                }
                if (validSymbol)
                {
                    Assert.That(result.Errors, Does.Not.Contain(ErrorConvertor.ConvertError(ErrorMessage.SymboLengthError)));
                    Assert.That(result.Errors, Does.Not.Contain(ErrorConvertor.ConvertError(ErrorMessage.SymbolEmptyError)));
                    Assert.That(result.Errors, Does.Not.Contain(ErrorConvertor.ConvertError(ErrorMessage.SymbolFormatError)));
                }
                else
                {
                    Assert.That(result.Errors, Does.Contain(ErrorConvertor.ConvertError(ErrorMessage.SymboLengthError)));
                }
                if (validUnits)
                {
                    Assert.That(result.Errors, Does.Not.Contain(ErrorConvertor.ConvertError(ErrorMessage.UnitsError)));
                }
                else
                {
                    Assert.That(result.Errors, Does.Contain(ErrorConvertor.ConvertError(ErrorMessage.UnitsError)));
                }
                if (validStopLoss)
                {
                    Assert.That(result.Errors, Does.Not.Contain(ErrorConvertor.ConvertError(ErrorMessage.StopLossError)));
                }
                else
                {
                    Assert.That(result.Errors, Does.Contain(ErrorConvertor.ConvertError(ErrorMessage.StopLossError)));
                }
                if (validTakeProfit)
                {
                    Assert.That(result.Errors, Does.Not.Contain(ErrorConvertor.ConvertError(ErrorMessage.TakeProfitError)));
                }
                else
                {
                    Assert.That(result.Errors, Does.Contain(ErrorConvertor.ConvertError(ErrorMessage.TakeProfitError)));
                }
                if (validChartPattern)
                {
                    Assert.That(result.Errors, Does.Not.Contain(ErrorConvertor.ConvertError(ErrorMessage.ChartPatternError)));
                }
                else
                {
                    Assert.That(result.Errors, Does.Contain(ErrorConvertor.ConvertError(ErrorMessage.ChartPatternError)));
                }

            });
        }



        [Test]
        [TestCase("")]
        [TestCase(" ")]
        [TestCase("EG#")]
        [TestCase("EGEG#")]
        [TestCase("EGGEEG")]
        [TestCase("EG G")]

        public async Task AddingNewPortfolioInstrument_InvalidSymbol_GetErrorsList(string testedSymbol)
        {
            //Arrange
            var portfolioInstrument = new PortfolioInstrument
            {
                AvgPrice = 10,
                ChartPattern = (int)ChartPattern.Flag,
                StopLoss = 10,
                TakeProfit = 10,
                Symbol = testedSymbol,
                Units = 3
            };

            //Action
            var command = new CreatePortfolioInstrument.Command { PortfolioId = Guid.NewGuid(), Instrument = portfolioInstrument };
            var handler = new CreatePortfolioInstrument.Handler(_dbContext, _validator);
            var result = await handler.Handle(command, CancellationToken.None);

            //Assert
            if (string.IsNullOrEmpty(testedSymbol))
            {
                Assert.That(result.Errors, Does.Contain(ErrorConvertor.ConvertError(ErrorMessage.SymbolEmptyError)));
            }
            else if (testedSymbol.Count() > 4)
            {
                Assert.That(result.Errors, Does.Contain(ErrorConvertor.ConvertError(ErrorMessage.SymboLengthError)));
            }
            else
            {
                Assert.That(result.Errors, Does.Contain(ErrorConvertor.ConvertError(ErrorMessage.SymbolFormatError)));
            }

        }

        [Test]
        public async Task AddingNewPortfolioInstrument_PortfolioNotExists_PortfolioNotFoundError()
        {
            //Arrange
            var portfolioInstrument = new PortfolioInstrument
            {
                AvgPrice = 10,
                ChartPattern = (int)ChartPattern.Flag,
                StopLoss = 10,
                TakeProfit = 10,
                Symbol = "TSLA",
                Units = 30
            };
            var portfolioId = Guid.NewGuid();
            var portfolioList = new List<Portfolio>()
            {
                new Portfolio
                {
                  PortfolioID = portfolioId,
                  Instruments = new List<PortfolioInstrument>
                  {
                    portfolioInstrument
                  }
                }
            };
            _dbContext.Portfolios.AddRange(CreateDbSet(portfolioList));


            var command = new CreatePortfolioInstrument.Command { PortfolioId = Guid.NewGuid(), Instrument = portfolioInstrument };
            var handler = new CreatePortfolioInstrument.Handler(_dbContext, _validator);

            //Action
            var result = await handler.Handle(command, CancellationToken.None);

            //Assert
            Assert.That(result.Errors, Does.Contain(ErrorConvertor.ConvertError(ErrorMessage.PortfolioNotFoundError)));
        }

        [Test]
        public async Task AddingNewPortfolioInstrument_InstrumentSymbolAlreadyExists_InstrumentExistsError()
        {
            //Arrange
            var requestedPortfolioInstrument = new PortfolioInstrument
            {
                AvgPrice = 10,
                ChartPattern = (int)ChartPattern.Flag,
                StopLoss = 10,
                TakeProfit = 10,
                Symbol = "TSLA",
                Units = 30
            };

            var existsPortfolioInstrument = new PortfolioInstrument
            {
                AvgPrice = 7,
                ChartPattern = (int)ChartPattern.Pennant,
                StopLoss = 6,
                TakeProfit = 10,
                Symbol = "TSLA",
                Units = 500
            };


            var portfolioId = Guid.NewGuid();
            var portfolioList = new List<Portfolio>()
            {
                new Portfolio
                {
                  PortfolioID = portfolioId,
                  Instruments = new List<PortfolioInstrument>
                  {
                    existsPortfolioInstrument
                  }
                }
            };

            _dbContext.Portfolios.AddRange(CreateDbSet(portfolioList));


            var command = new CreatePortfolioInstrument.Command { PortfolioId = portfolioId, Instrument = requestedPortfolioInstrument };
            var handler = new CreatePortfolioInstrument.Handler(_dbContext, _validator);

            //Action
            var result = await handler.Handle(command, CancellationToken.None);

            //Assert
            Assert.That(result.Errors, Does.Contain(ErrorConvertor.ConvertError(ErrorMessage.InstrumentExistsError)));
        }
        #endregion

        #region Delete instrument 
        [Test]
        public async Task DeletePortfolioInstrument_InstrumentIdExists_InstrumentRemoved()
        {
            //Arrange
            const int instrumentID = 12345;
            var portfolioInstrument = new PortfolioInstrument
            {
                AvgPrice = 10,
                ChartPattern = (int)ChartPattern.Flag,
                StopLoss = 10,
                TakeProfit = 10,
                Symbol = "TSLA",
                Units = 30,
                InstrumentId = instrumentID
            };
            var portfolioId = Guid.NewGuid();
            var portfolioList = new List<Portfolio>()
            {
                new Portfolio
                {
                  PortfolioID = portfolioId,
                  Instruments = new List<PortfolioInstrument>
                  {
                    portfolioInstrument
                  }
                }
            };
            _dbContext.Portfolios.AddRange(CreateDbSet(portfolioList));


            var command = new DeletePortfolioInstrument.Command { InstrumentID = instrumentID };
            var handler = new DeletePortfolioInstrument.Handler(_dbContext);

            //Action
            var result = await handler.Handle(command, CancellationToken.None);

            //Assert
            Assert.That(result.Value, Is.EqualTo(Unit.Value));
        }

        [Test]
        public async Task DeletePortfolioInstrument_InstrumentIdNotExists_InstrumentRemoved()
        {
            //Arrange
            const int instrumentID = 12345;
            var portfolioInstrument = new PortfolioInstrument
            {
                AvgPrice = 10,
                ChartPattern = (int)ChartPattern.Flag,
                StopLoss = 10,
                TakeProfit = 10,
                Symbol = "TSLA",
                Units = 30,
                InstrumentId = instrumentID
            };
            var portfolioId = Guid.NewGuid();
            var portfolioList = new List<Portfolio>()
            {
                new Portfolio
                {
                  PortfolioID = portfolioId,
                  Instruments = new List<PortfolioInstrument>
                  {
                    portfolioInstrument
                  }
                }
            };
            _dbContext.Portfolios.AddRange(CreateDbSet(portfolioList));


            var command = new DeletePortfolioInstrument.Command { InstrumentID = instrumentID + 1 };
            var handler = new DeletePortfolioInstrument.Handler(_dbContext);

            //Action
            var result = await handler.Handle(command, CancellationToken.None);

            //Assert
            Assert.That(result.Errors, Does.Contain(ErrorConvertor.ConvertError(ErrorMessage.InstrumentNotFoundError)));
        }
        #endregion
    }
}