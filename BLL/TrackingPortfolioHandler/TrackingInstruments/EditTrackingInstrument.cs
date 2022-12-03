using AutoMapper;
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
    public class EditTrackingInstrument
    {
        public class Command : IRequest
        {
            public TrackingInstrument TrackingInstrument { get; set; }
        }

        public class Handler : IRequestHandler<Command>
        {
            private readonly MainDataContext _context;
            private readonly IMapper _mapper;

            public Handler(MainDataContext dataContext, IMapper mapper)
            {
                _context = dataContext;
                _mapper = mapper;
            }
            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                TrackingInstrument instrument = await _context.TrackingInstruments.FindAsync(new object[] { request.TrackingInstrument.InstrumentID }, cancellationToken);

                _mapper.Map(request.TrackingInstrument, instrument);

                await _context.SaveChangesAsync(cancellationToken);
                return Unit.Value;
            }
        }
    }
}
