using DAL;
using DATA;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BLL.PortfolioInstruments
{
    public class InstrumentDetails
    {
        public class Query : IRequest<PortfolioInstrument>
        {
            public int InstrumentID { get; set; }
        }
        public class Handler : IRequestHandler<Query, PortfolioInstrument>
        {
            private readonly MainDataContext _context;

            public Handler(MainDataContext context)
            {
                _context = context;
            }
            public async Task<PortfolioInstrument> Handle(Query request, CancellationToken cancellationToken)
            {
                return await _context.PortfolioInstruments.FindAsync(request.InstrumentID);
            }
        }
    }
}
