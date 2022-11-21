using DAL;
using DATA;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BLL.MainPortfolio
{
    public class CreatePortfolio
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
                Portfolio portfolio = new Portfolio
                {
                    Instruments = new List<PortfolioInstrument>(),
                    UserID = Guid.NewGuid()
                };
                _context.Portfolios.Add(portfolio);
                await _context.SaveChangesAsync();
                return Unit.Value;
            }
        }
    }
}
