using DAL;
using DATA.Instruments;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace BLL.TrackingPortfolioHandler.TrackingInstruments
{
    public class GetTrackingInstrument
    {
        public class Query : IRequest<TrackingInstrument>
        {
            public int InstrumentID { get; set; }
        }
        public class Handler : IRequestHandler<Query, TrackingInstrument>
        {
            private readonly MainDataContext _context;

            public Handler(MainDataContext context)
            {
                _context = context;
            }
            public async Task<TrackingInstrument> Handle(Query request, CancellationToken cancellationToken)
            {
                return await _context.TrackingInstruments.FindAsync(new object[] { request.InstrumentID }, cancellationToken: cancellationToken);
            }
        }
    }
}
