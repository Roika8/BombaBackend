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
    public class List
    {
        public class Query : IRequest<List<PortfolioInstrument>>
        {
            public int PortfolioID { get; set; }
        }

        public class Handler : IRequestHandler<Query, List<PortfolioInstrument>>
        {
            private readonly MainDataContext _context;

            public Handler(MainDataContext context)
            {
                _context = context;
            }
            public async Task<List<PortfolioInstrument>> Handle(Query request, CancellationToken cancellationToken)
            {
                return await _context.PortfolioInstruments.Where(pi => pi.Portfolio.PortfolioID == request.PortfolioID).ToListAsync();
            }
        }
    }
}
