using DAL;
using DATA.Instruments;
using DATA.Portfolios;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace BLL.HistorytPortfolio.Portfolios
{
    public class CreateHistoryPortfolio
    {
        public class Command : IRequest
        {
            public string UserID { get; set; }

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
                HistoryPortfolio portfolio = new()
                {
                    Instruments = new List<HistoryInstrument>(),
                    UserID = Guid.Parse(request.UserID),
                    PortfolioID = Guid.NewGuid()
                };
                _context.HistoryPortfolios.Add(portfolio);
                await _context.SaveChangesAsync(cancellationToken);
                return Unit.Value;
            }
        }
    }
}
