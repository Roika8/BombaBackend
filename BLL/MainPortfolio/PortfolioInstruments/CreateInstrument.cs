using DAL;
using DATA.Instruments;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace BLL.PortfolioInstruments
{
    public class CreateHistoryInstrument
    {
        public class Command : IRequest
        {
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
                var portfolio = await _context.Portfolios.FindAsync(request.Instrument.Portfolio.PortfolioID);
                portfolio.Instruments.Add(request.Instrument);
                await _context.SaveChangesAsync();
                return Unit.Value;
            }
        }
    }
}
