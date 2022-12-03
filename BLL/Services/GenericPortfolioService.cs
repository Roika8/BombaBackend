using AutoMapper;
using BLL.Interfaces;
using DAL;
using DAL.Interfaces;
using DATA.Instruments;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace BLL
{
    public class GenericPortfolioService : IGenericPortfolioService
    {
        private readonly MainDataContext _context;
        private readonly IMapper _mapper;
        public GenericPortfolioService(MainDataContext dataContext, IMapper mapper)
        {
            _context = dataContext;
            _mapper = mapper;
        }

        public Task<bool> AddInstrumentToPortfolio<T>(T instrument)
        {
            switch (typeof(T))
            {
                case PortfolioInstrument:
                    break;
                default:
                    break;
            }
        }

        public Task<bool> DeletePortfolioInstrument<T>(T instrument)
        {
            throw new NotImplementedException();
        }

        public Task<bool> EditPortfolioInstrument<T>(T instrument)
        {
            throw new NotImplementedException();
        }

        public Task<T> GetPortfolioInstrument<T>(int instrumentId)
        {
            throw new NotImplementedException();
        }
    }
}
