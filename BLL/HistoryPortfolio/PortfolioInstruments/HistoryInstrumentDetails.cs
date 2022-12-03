using DAL;
using DATA.Instruments;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BLL.HistorytPortfolio.PortfolioInstruments
{
    public class HistoryInstrumentDetails
    {
        public class Query : IRequest<HistoryInstrument>
        {
            public int InstrumentID { get; set; }
        }
        public class Handler : IRequestHandler<Query, HistoryInstrument>
        {
            private readonly MainDataContext _context;

            public Handler(MainDataContext context)
            {
                _context = context;
            }
            public async Task<HistoryInstrument> Handle(Query request, CancellationToken cancellationToken)
            {
                return await _context.HistoryInstuments.FindAsync(new object[] { request.InstrumentID }, cancellationToken: cancellationToken);
            }
        }
    }
}
