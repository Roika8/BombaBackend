using BLL.Core;
using BLL.PortfolioInstruments;
using BLL.Services;
using BombaAPI.Controllers;
using BombaRestAPI.Properties.DTOs;
using DATA.Enums;
using DATA.Instruments;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace UnitTests
{
    public class Tests
    {

        [SetUp]
        public void Setup()
        {
        }
        [Test]
        public async Task AddingNewPortfolioInstrument_InvalidArgument_GetErrorsList([Values] bool validAveragePrice, [Values] bool validSymbol, [Values] bool validUnits,
            [Values] bool validStopLoss, [Values] bool validTakeProfit, [Values] bool validChartPattern)
        {
            // Arrange
            var portfolioInstrumentDto = new PortfolioInstrumentDto
            {
                AvgPrice = validAveragePrice ? 10 : -1,
                ChartPattern = validChartPattern ? (int)ChartPattern.Flag : (int)ChartPattern.Flag + 1,
                StopLoss = validStopLoss ? 10 : -1,
                TakeProfit = validTakeProfit ? 10 : -1,
                Symbol = validSymbol ? "SMBL" : "TooLongSymbol",
                Units = validUnits ? 3 : -1
            };
            var errorsList = new List<ErrorMessage>
            {
              ErrorMessage.StopLossError,
              ErrorMessage.ChartPatternError
            };
            var expectedCommandResult = Result<PortfolioInstrument>.Failure(errorsList);
            var mediatorMock = new Mock<IMediator>();

            mediatorMock.Setup(m => m.Send(It.IsAny<CreatePortfolioInstrument.Command>(), default)).
                ReturnsAsync(expectedCommandResult);

            var portfolioController = new MainPortfolioController(mediatorMock.Object);



            // Act
            var result = await portfolioController.AddPortfolioInstrument(Guid.NewGuid(), portfolioInstrumentDto);

            // Assert
            Assert.IsInstanceOf<BadRequestResult>(result);
            var contentResult = result as ContentResult;

            if (!validAveragePrice)
            {
                StringAssert.Contains(ErrorConvertor.ConvertError(ErrorMessage.AveragePriceError), contentResult.Content, "Average price error not found");
            }
        }
    }
}