using BLL.Core;
using BLL.MainPortfolio.Validators;
using DAL;
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
                var x = errors.Length;
                if (!string.IsNullOrEmpty(errors))
                {
                    return Result<PortfolioInstrument>.Failure(errors);
                }

                var portfolio = await _context.Portfolios.FindAsync(new object[] { request.PortfolioId }, cancellationToken: cancellationToken);
                if (portfolio == null)
                {
                    return Result<PortfolioInstrument>.Failure("Couldnt find portfolio");
                }
                var instrument = request.Instrument;
                instrument.Symbol = instrument.Symbol.ToUpper();


                if (ValidateInstrumentIsNotAlreadyExists(portfolio, instrument))
                {
                    await _context.SaveChangesAsync(cancellationToken);
                    return Result<PortfolioInstrument>.Success(request.Instrument);
                }
                return Result<PortfolioInstrument>.Failure("InstrumentAlready exists in portfolio");
            }
            private bool ValidateInstrumentIsNotAlreadyExists(Portfolio portfolio, PortfolioInstrument instrument)
            {
                var portfolioInstrumentsList = portfolio.Instruments.ToList();
                return portfolioInstrumentsList.Find(ins => ins.Symbol == instrument.Symbol) == null;
            }
        }
    }
}
