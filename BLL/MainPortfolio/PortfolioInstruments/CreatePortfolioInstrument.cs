using BLL.Core;
using BLL.MainPortfolio.Validators;
using BLL.Services;
using DAL;
using DATA.Enums;
using DATA.Instruments;
using DATA.Portfolios;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BLL.PortfolioInstruments
{

    public class CreatePortfolioInstrument
    {
        public class Command : IRequest<Result<PortfolioInstrument>>
        {
            public Guid PortfolioId { get; set; }
            public PortfolioInstrument Instrument { get; set; }
        }
        public class Handler : IRequestHandler<Command, Result<PortfolioInstrument>>
        {
            private readonly MainDataContext _context;
            private readonly ICommandValidator<PortfolioInstrument> _validator;

            public Handler(MainDataContext context, ICommandValidator<PortfolioInstrument> validator)
            {
                _context = context;
                _validator = validator;
            }
            public async Task<Result<PortfolioInstrument>> Handle(Command request, CancellationToken cancellationToken)
            {
                var errors = _validator.ValidateCommand(request.Instrument);

                if (errors.Count > 0)
                {
                    return Result<PortfolioInstrument>.Failure(errors);
                }

                var portfolio = await _context.Portfolios.FindAsync(new object[] { request.PortfolioId }, cancellationToken: cancellationToken);
                if (portfolio == null)
                {
                    errors.Add(ErrorMessage.PortfolioNotFoundError);
                    return Result<PortfolioInstrument>.Failure(errors);
                }
                var instrument = request.Instrument;
                instrument.Symbol = instrument.Symbol.ToUpper();


                if (ValidateInstrumentIsNotAlreadyExists(portfolio, instrument))
                {
                    portfolio.Instruments.Add(instrument);
                    await _context.SaveChangesAsync(cancellationToken);
                    return Result<PortfolioInstrument>.Success(request.Instrument);
                }
                errors.Add(ErrorMessage.InstrumentExistsError);
                return Result<PortfolioInstrument>.Failure(errors);
            }

            private bool ValidateInstrumentIsNotAlreadyExists(Portfolio portfolio, PortfolioInstrument instrument)
            {
                var portfolioInstrumentsList = portfolio.Instruments.ToList();
                return portfolioInstrumentsList.Find(ins => ins.Symbol == instrument.Symbol) == null;
            }

        }
    }
}
