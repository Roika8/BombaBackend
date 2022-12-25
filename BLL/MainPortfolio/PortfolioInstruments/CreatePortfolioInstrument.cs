using DAL;
using DATA.Instruments;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace BLL.PortfolioInstruments
{
    public class CreatePortfolioInstrument
    {
        public class Command : IRequest
        {
            public Guid PortfolioId { get; set; }
            public PortfolioInstrument Instrument { get; set; }
        }
        public class Handler : IRequestHandler<Command>
        {
            private readonly MainDataContext _context;

            public Handler(MainDataContext context)
            {
                _context = context;
            }
            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                var portfolio = await _context.Portfolios.FindAsync(new object[] { request.PortfolioId }, cancellationToken: cancellationToken);
                var instrument = request.Instrument;
                instrument.Symbol = instrument.Symbol.ToUpper();

                portfolio.Instruments.Add(request.Instrument);
                await _context.SaveChangesAsync(cancellationToken);
                return Unit.Value;
            }
        }
    }
}
