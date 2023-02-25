using BLL.Core;
using BLL.MainPortfolio.Validators;
using BLL.PortfolioInstruments;
using BLL.Services;
using BombaAPI.Controllers;
using BombaRestAPI.Properties.DTOs;
using DAL;
using DATA.Enums;
using DATA.Instruments;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace UnitTests
{
    public class Tests
    {
        private MainDataContext _dbContext;
        private ICommandValidator<PortfolioInstrument> _validator;
        public Tests()
        {
            DbContextOptionsBuilder dbOptions = new DbContextOptionsBuilder<MainDataContext>().UseInMemoryDatabase(Guid.NewGuid().ToString());
            _dbContext = new MainDataContext((DbContextOptions<MainDataContext>)dbOptions.Options);
            _validator = new EditPortfolioInstrumentValidator();
        }

        [Test]
        public async Task AddingNewPortfolioInstrument_InvalidArgument_GetErrorsList([Values] bool validAveragePrice, [Values] bool validSymbol, [Values] bool validUnits,
            [Values] bool validStopLoss, [Values] bool validTakeProfit, [Values] bool validChartPattern)
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
            var errorsList = new List<ErrorMessage>();


            if (!validAveragePrice)
            {
                errorsList.Add(ErrorMessage.AveragePriceError);
            }
            if (!validSymbol)
            {
                errorsList.Add(ErrorMessage.SymbolError);
            }
            if (!validUnits)
            {
                errorsList.Add(ErrorMessage.UnitsError);
            }
            if (!validStopLoss)
            {
                errorsList.Add(ErrorMessage.StopLossError);
            }
            if (!validTakeProfit)
            {
                errorsList.Add(ErrorMessage.ChartPatternError);
            }
            if (!validChartPattern)
            {
                errorsList.Add(ErrorMessage.ChartPatternError);
            }

            //var expectedCommandResult = Result<PortfolioInstrument>.Failure(errorsList);
            //var mediatorMock = new Mock<IMediator>();

            //mediatorMock.Setup(m => m.Send(It.IsAny<CreatePortfolioInstrument.Command>(), default)).
            //    ReturnsAsync(expectedCommandResult);

            //var portfolioController = new MainPortfolioController(mediatorMock.Object);

            var command = new CreatePortfolioInstrument.Command { PortfolioId = Guid.NewGuid(), Instrument = portfolioInstrument };
            var handler = new CreatePortfolioInstrument.Handler(_dbContext, _validator);
            var result = await handler.Handle(command, CancellationToken.None);

            // Act
            //var result = await portfolioController.AddPortfolioInstrument(Guid.NewGuid(), portfolioInstrumentDto);

            //// Assert
            //Assert.IsInstanceOf<BadRequestResult>(result);
            //var contentResult = result as ContentResult;

            //if (!validAveragePrice)
            //{
            //    StringAssert.Contains(ErrorConvertor.ConvertError(ErrorMessage.AveragePriceError), contentResult.Content, "Average price error not found");
            //}
        }
    }
}