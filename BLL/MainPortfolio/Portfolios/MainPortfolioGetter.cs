using DAL;
using DATA.Instruments;
using DATA.Portfolios;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BLL.MainPortfolio.Portfolios
{
    public class MainPortfolioGetter
    {
        public class Query : IRequest<Portfolio>
        {
            public int PortfolioID { get; set; }
        }

        public class Handler : IRequestHandler<Query, Portfolio>
        {
            private readonly MainDataContext _context;

            public Handler(MainDataContext context)
            {
                _context = context;
            }
            public async Task<Portfolio> Handle(Query request, CancellationToken cancellationToken)
            {
                var portfolioInstruments = await _context.PortfolioInstruments.Where(pi => pi.Portfolio.PortfolioID == request.PortfolioID)
                    .ToListAsync(cancellationToken: cancellationToken);

                return new Portfolio
                {
                    PortfolioID = request.PortfolioID,
                    Instruments = portfolioInstruments
                };
            }
        }
    }
}
