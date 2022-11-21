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
    public class MainPortfolioDetails
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
                return await _context.Portfolios.FindAsync(request.PortfolioID);
            }
        }
    }
}
