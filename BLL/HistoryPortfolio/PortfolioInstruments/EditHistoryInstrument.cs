using AutoMapper;
using DAL;
using DATA.Instruments;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace BLL.HistorytPortfolio.PortfolioInstruments
{
    public class EditHistoryInstrument
    {
        public class Command : IRequest
        {
            public HistoryInstrument HistoryInstrument { get; set; }
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
                HistoryInstrument instrument = await _context.HistoryInstuments.FindAsync(new object[] { request.HistoryInstrument.InstrumentId }, cancellationToken);

                _mapper.Map(request.HistoryInstrument, instrument);

                await _context.SaveChangesAsync(cancellationToken);
                return Unit.Value;
            }
        }
    }
}
