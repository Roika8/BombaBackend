using DAL;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace BLL.PortfolioInstruments
{
    public class DeletePortfolioInstrument
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
                var portfolioInstrument = await _context.PortfolioInstruments.FindAsync(new object[] { request.InstrumentID }, cancellationToken: cancellationToken);
                _context.Remove(portfolioInstrument);
                await _context.SaveChangesAsync(cancellationToken);
                return Unit.Value;
            }
        }
    }
}
