using DAL;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BLL.TrackingPortfolioHandler.TrackingInstruments
{
    public class DeleteTrackingInstrument
    {
        public class Command : IRequest
        {
            public int InstrumentID { get; set; }
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
                var trackingInstrument = await _context.TrackingInstruments.FindAsync(new object[] { request.InstrumentID }, cancellationToken: cancellationToken);
                _context.Remove(trackingInstrument);
                await _context.SaveChangesAsync(cancellationToken);
                return Unit.Value;
            }
        }
    }
}
