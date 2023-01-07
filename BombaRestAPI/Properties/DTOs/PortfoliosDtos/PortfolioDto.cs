using System;
using System.Collections;
using System.Collections.Generic;

namespace BombaRestAPI.Properties.DTOs.PortfoliosDtos
{
    public class PortfolioDto
    {
        public Guid PortfolioID { get; set; }
        public Guid UserID { get; set; }

        public IEnumerable<PortfolioInstrumentDto> PortfolioInstruments { get; set; }

    }
}
