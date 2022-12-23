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

namespace BLL.HistorytPortfolio.Portfolios
{
    public class GetHistoryPortfolio
    {
        public class Query : IRequest<HistoryPortfolio>
        {
            public int PortfolioID { get; set; }
        }

        public class Handler : IRequestHandler<Query, HistoryPortfolio>
        {
            private readonly MainDataContext _context;

            public Handler(MainDataContext context)
            {
                _context = context;
            }
            public async Task<HistoryPortfolio> Handle(Query request, CancellationToken cancellationToken)
            {
                var historyInstuments = await _context.HistoryInstuments.Where(pi => pi.Portfolio.PortfolioID == request.PortfolioID).ToListAsync(cancellationToken: cancellationToken);
                return new HistoryPortfolio
                {
                    PortfolioID = request.PortfolioID,
                    Instruments = historyInstuments
                };
            }
        }
    }
}
