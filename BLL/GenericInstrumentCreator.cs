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

namespace BLL
{
    public class GenericInstrumentCreator
    {
        public class Command : IRequest
        {
            public BaseInstrument<Ge> GenericPortfolio;
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
                switch (request)
                {
                    case PortfolioInstrument:
                        Portfolio portfolio = new()
                        {
                            Instruments = new List<PortfolioInstrument>(),
                            UserID = Guid.NewGuid()
                        };
                        _context.Portfolios.Add(portfolio);
                        break;

                    case TrackingPortfolio:
                        TrackingPortfolio trackingPortfolio = new TrackingPortfolio
                        {
                            Instruments = new List<TrackingInstrument>(),
                            UserID = Guid.NewGuid()
                        };
                        _context.TrackingPortfolios.Add(trackingPortfolio);
                        break;

                    case HistoryPortfolio:
                        HistoryPortfolio historyPortfolio = new HistoryPortfolio
                        {
                            Instruments = new List<HistoryInstument>(),
                            UserID = Guid.NewGuid()
                        };
                        break;
                    default:
                        break;
                }


                await _context.SaveChangesAsync();
                return Unit.Value;
            }
        }
    }
}
