using BLL.Core;
using BLL.MainPortfolio.Validators;
using DAL;
using DATA.Enums;
using DATA.Instruments;
using DATA.Portfolios;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BLL.TrackingPortfolioHandler.TrackingInstruments
{
    public class CreateTrackingInstrument
    {
        public class Command : IRequest<Result<TrackingInstrument>>
        {
            public Guid PortfolioId { get; set; }
            public TrackingInstrument Instrument { get; set; }
        }
        public class Handler : IRequestHandler<Command, Result<TrackingInstrument>>
        {
            private readonly MainDataContext _context;
            private readonly ICommandValidator<TrackingInstrument> _validator;

            public Handler(MainDataContext context, ICommandValidator<TrackingInstrument> validator)
            {
                _context = context;
                _validator = validator;

            }
            public async Task<Result<TrackingInstrument>> Handle(Command request, CancellationToken cancellationToken)
            {
                var errors = _validator.ValidateCommand(request.Instrument);
                if (errors.Count > 0)
                {
                    return Result<TrackingInstrument>.Failure(errors);
                }

                var portfolio = await _context.TrackingPortfolios.FindAsync(new object[] { request.PortfolioId }, cancellationToken: cancellationToken);
                if (portfolio == null)
                {
                    errors.Add(ErrorMessage.PortfolioNotFoundError);
                    return Result<TrackingInstrument>.Failure(errors);
                }
                var instrument = request.Instrument;
                instrument.Symbol = instrument.Symbol.ToUpper();

                if (ValidateInstrumentIsNotAlreadyExists(portfolio, instrument))
                {
                    portfolio.Instruments.Add(instrument);
                    var result = await _context.SaveChangesAsync(cancellationToken) > 0;
                    if (!result)
                    {
                        return Result<TrackingInstrument>.Failure(ErrorMessage.DatabaseAddRecordError);
                    }

                    return Result<TrackingInstrument>.Success(request.Instrument);
                }
                errors.Add(ErrorMessage.InstrumentExistsError);
                return Result<TrackingInstrument>.Failure(errors);
            }
            private bool ValidateInstrumentIsNotAlreadyExists(TrackingPortfolio portfolio, TrackingInstrument instrument)
            {
                var portfolioInstrumentsList = portfolio.Instruments.ToList();
                return portfolioInstrumentsList.Find(ins => ins.Symbol == instrument.Symbol) == null;
            }
        }
    }
}
