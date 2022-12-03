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

namespace BLL.TrackingPortfolioHandlers.Portfolios
{
    public class CreateTrackingPortfolio
    {
        public class Command : IRequest
        {

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
                TrackingPortfolio portfolio = new()
                {
                    Instruments = new List<TrackingInstrument>(),
                    UserID = Guid.NewGuid()
                };
                _context.TrackingPortfolios.Add(portfolio);
                await _context.SaveChangesAsync(cancellationToken);
                return Unit.Value;
            }
        }
    }
}
