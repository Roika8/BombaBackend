using BLL.Core;
using DAL;
using DATA.Enums;
using DATA.Instruments;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace BLL.PortfolioInstruments
{
    public class DeletePortfolioInstrument
    {
        public class Command : IRequest<Result<Unit>>
        {
            public int InstrumentID { get; set; }
        }
        public class Handler : IRequestHandler<Command, Result<Unit>>
        {
            private readonly MainDataContext _context;

            public Handler(MainDataContext context)
            {
                _context = context;
            }
            public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
            {
                var portfolioInstrument = await _context.PortfolioInstruments.FindAsync(new object[] { request.InstrumentID }, cancellationToken: cancellationToken);

                if (portfolioInstrument == null)
                {
                    return Result<Unit>.Failure(new List<ErrorMessage> { ErrorMessage.InstrumentNotFoundError });
                }
                _context.Remove(portfolioInstrument);
                await _context.SaveChangesAsync(cancellationToken);
                return Result<Unit>.Success(Unit.Value);
            }

        }
    }
}
