using DAL;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace BLL.HistorytPortfolio.PortfolioInstruments
{
    public class DeleteHistoryInstrument
    {
        public class Command : IRequest
        {
            public int InstrumentID { get; set; }
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
                var historyInstrument = await _context.HistoryInstuments.FindAsync(new object[] { request.InstrumentID }, cancellationToken: cancellationToken);
                _context.Remove(historyInstrument);
                await _context.SaveChangesAsync(cancellationToken);
                return Unit.Value;
            }
        }
    }
}
