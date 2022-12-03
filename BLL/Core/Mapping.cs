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
            CreateMap<PortfolioInstrument, PortfolioInstrument>();
        }
    }
}
