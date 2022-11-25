using DAL;
using DATA;
using MediatR;
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
                var x = await _context.Portfolios.FindAsync(new object[] { request.PortfolioID }, cancellationToken);
                return x;
            }
        }
    }
}
