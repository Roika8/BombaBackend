using DAL;
using DATA;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BLL.PortfolioInstruments
{
    public class Create
    {
        public class Command : IRequest
        {
            public PortfolioInstrument Instrument { get; set; }
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
                var portfolio = await _context.Portfolios.FindAsync(request.Instrument.Portfolio.PortfolioID);
                portfolio.Instruments ??= new List<PortfolioInstrument>();
                portfolio.Instruments.Add(request.Instrument);
                await _context.SaveChangesAsync();
                return Unit.Value;
            }
        }
    }
}
