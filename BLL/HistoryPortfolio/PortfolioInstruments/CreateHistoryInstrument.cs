using BLL.Core;
using BLL.MainPortfolio.Validators;
using DAL;
using DATA.Enums;
using DATA.Instruments;
using DATA.Portfolios;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BLL.HistorytPortfolio.PortfolioInstruments
{
    public class CreateHistoryInstrument
    {
        public class Command : IRequest<Result<HistoryInstrument>>
        {
            public Guid PortfolioId { get; set; }

            public HistoryInstrument Instrument { get; set; }
        }
        public class Handler : IRequestHandler<Command, Result<HistoryInstrument>>
        {
            private readonly MainDataContext _context;
            private readonly ICommandValidator<HistoryInstrument> _validator;


            public Handler(MainDataContext context, ICommandValidator<HistoryInstrument> validator)
            {
                _context = context;
                _validator = validator;
            }
            public async Task<Result<HistoryInstrument>> Handle(Command request, CancellationToken cancellationToken)
            {
                var errors = _validator.ValidateCommand(request.Instrument);

                if (errors.Count > 0)
                {
                    return Result<HistoryInstrument>.Failure(errors);
                }

                var historyPortfolio = await _context.HistoryPortfolios.FindAsync(new object[] { request.PortfolioId }, cancellationToken: cancellationToken);

                if (historyPortfolio == null)
                {
                    errors.Add(ErrorMessage.PortfolioNotFoundError);
                    return Result<HistoryInstrument>.Failure(errors);
                }
                var instrument = request.Instrument;
                instrument.Symbol = instrument.Symbol.ToUpper();

                if (ValidateInstrumentIsNotAlreadyExists(historyPortfolio, instrument))
                {
                    historyPortfolio.Instruments.Add(instrument);
                    var result = await _context.SaveChangesAsync(cancellationToken) > 0;
                    if (!result)
                    {
                        return Result<HistoryInstrument>.Failure(ErrorMessage.DatabaseAddRecordError);
                    }

                    return Result<HistoryInstrument>.Success(request.Instrument);
                }
                errors.Add(ErrorMessage.InstrumentExistsError);
                return Result<HistoryInstrument>.Failure(errors);
            }

            private bool ValidateInstrumentIsNotAlreadyExists(HistoryPortfolio portfolio, HistoryInstrument instrument)
            {
                var portfolioInstrumentsList = portfolio.Instruments.ToList();
                return portfolioInstrumentsList.Find(ins => ins.Symbol == instrument.Symbol) == null;
            }
        }
    }
}
