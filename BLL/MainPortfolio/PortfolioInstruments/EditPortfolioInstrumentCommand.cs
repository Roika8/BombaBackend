using AutoMapper;
using BLL.MainPortfolio.Validators;
using DAL;
using DATA.Instruments;
using FluentValidation;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace BLL.PortfolioInstruments
{
    public class EditPortfolioInstrumentCommand
    {
        public class Command : IRequest
        {
            public PortfolioInstrument PortfolioInstrument { get; set; }
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
                PortfolioInstrument instrument = await _context.PortfolioInstruments.FindAsync(new object[] { request.PortfolioInstrument.InstrumentId }, cancellationToken);

                _mapper.Map(request.PortfolioInstrument, instrument);

                await _context.SaveChangesAsync(cancellationToken);
                return Unit.Value;
            }
        }
    }
}
