using AutoMapper;
using DATA.Instruments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Core
{
    public class Mapping : Profile
    {
        public Mapping()
        {
            //Doesnt need to change the portfolio proprties and instrument Symbol
            CreateMap<PortfolioInstrument, PortfolioInstrument>()
                .ForMember(instrument => instrument.Portfolio, action => action.Ignore())
                .ForMember(instrument => instrument.PortfolioId, action => action.Ignore())
                .ForMember(instrument => instrument.Symbol, action => action.Ignore());

        }
    }
}
