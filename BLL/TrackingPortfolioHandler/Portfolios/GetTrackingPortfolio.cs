using DAL;
using DATA.Portfolios;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BLL.TrackingPortfolioHandler.Portfolios
{
    public class GetTrackingPortfolio
    {
        public class Query : IRequest<TrackingPortfolio>
        {
            public int PortfolioID { get; set; }
        }

        public class Handler : IRequestHandler<Query, TrackingPortfolio>
        {
            private readonly MainDataContext _context;

            public Handler(MainDataContext context)
            {
                _context = context;
            }
            public async Task<TrackingPortfolio> Handle(Query request, CancellationToken cancellationToken)
            {
                var trackingInstrument = await _context.TrackingInstruments.Where(pi => pi.Portfolio.PortfolioID == request.PortfolioID)
                    .ToListAsync(cancellationToken: cancellationToken);

                return new TrackingPortfolio
                {
                    PortfolioID = request.PortfolioID,
                    Instruments = trackingInstrument
                };
            }
        }
    }
}
