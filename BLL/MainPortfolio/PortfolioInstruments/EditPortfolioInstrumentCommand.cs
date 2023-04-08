using AutoMapper;
using BLL.Core;
using BLL.MainPortfolio.Validators;
using DAL;
using DATA.Enums;
using DATA.Instruments;
using FluentValidation;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace BLL.PortfolioInstruments
{
    public class EditPortfolioInstrumentCommand
    {
        public class Command : IRequest<Result<Unit>>
        {
            public PortfolioInstrument PortfolioInstrument { get; set; }
        }


        public class Handler : IRequestHandler<Command, Result<Unit>>
        {
            private readonly MainDataContext _context;
            private readonly IMapper _mapper;
            private readonly ICommandValidator<PortfolioInstrument> _validator;

            public Handler(MainDataContext dataContext, IMapper mapper, ICommandValidator<PortfolioInstrument> validator)
            {
                _context = dataContext;
                _mapper = mapper;
                _validator = validator;
            }
            public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
            {
                var instrument = await _context.PortfolioInstruments.FindAsync(new object[] { request.PortfolioInstrument.InstrumentId }, cancellationToken);
                if (instrument == null)
                {
                    return Result<Unit>.Failure(ErrorMessage.InstrumentNotFoundError);
                }
                request.PortfolioInstrument.Symbol = instrument.Symbol;
                var errors = _validator.ValidateCommand(request.PortfolioInstrument);
                if (errors.Count > 0)
                {
                    return Result<Unit>.Failure(errors);
                }

               
                _mapper.Map(request.PortfolioInstrument, instrument);

                var result = await _context.SaveChangesAsync(cancellationToken) > 0;
                if (!result)
                {
                    return Result<Unit>.Failure(ErrorMessage.DatabaseEditRecordError);
                }

                return Result<Unit>.Success(Unit.Value);
            }
        }
    }
}
