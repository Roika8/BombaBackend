using BLL.Core;
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
    public class GetMainPortfolio
    {
        public class Query : IRequest<Result<Portfolio>>
        {
            public Guid PortfolioID { get; set; }
        }

        public class Handler : IRequestHandler<Query, Result<Portfolio>>
        {
            private readonly MainDataContext _context;

            public Handler(MainDataContext context)
            {
                _context = context;
            }
            public async Task<Result<Portfolio>> Handle(Query request, CancellationToken cancellationToken)
            {
                var result = await _context.Portfolios.FirstOrDefaultAsync(portfolio =>
                                   portfolio.PortfolioID == request.PortfolioID, cancellationToken);

                //var portfolio = new Portfolio
                //{
                //    PortfolioID = request.PortfolioID,
                //    Instruments = result.Instruments,
                //    UserID = result.UserID
                //    //Todo Add here the UserID from auth        
                //};
                return Result<Portfolio>.Success(result);
            }
        }
    }
}
