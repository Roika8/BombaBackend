using DAL;
using DATA.Instruments;
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
    public class HistoryPortfolioGetter
    {
        public class Query : IRequest<List<HistoryInstrument>>
        {
            public int PortfolioID { get; set; }
        }

        public class Handler : IRequestHandler<Query, List<HistoryInstrument>>
        {
            private readonly MainDataContext _context;

            public Handler(MainDataContext context)
            {
                _context = context;
            }
            public async Task<List<HistoryInstrument>> Handle(Query request, CancellationToken cancellationToken)
            {
                return await _context.HistoryInstuments.Where(pi => pi.Portfolio.PortfolioID == request.PortfolioID).ToListAsync(cancellationToken: cancellationToken);
            }
        }
    }
}
