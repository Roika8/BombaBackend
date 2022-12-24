using DAL;
using DATA.Instruments;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BLL.TrackingPortfolioHandler.TrackingInstruments
{
    public class CreateTrackingInstrument
    {
        public class Command : IRequest
        {
            public TrackingInstrument Instrument { get; set; }
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
                var portfolio = await _context.TrackingPortfolios.FindAsync(new object[] { request.Instrument.Portfolio.PortfolioID }, cancellationToken: cancellationToken);
                portfolio.Instruments.Add(request.Instrument);
                await _context.SaveChangesAsync(cancellationToken);
                return Unit.Value;
            }
        }
    }
}
