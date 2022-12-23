using System;

namespace BombaRestAPI.Properties.DTOs.InstumentsDtos
{
    public class HistoryInstrumentDto
    {
        public decimal ActionOccuredPrice { get; set; }
        public decimal Units { get; set; }
        public decimal ProfitLoss { get; set; }
        public DateTime RequestOccured { get; set; }
        public string Symbol { get; set; }
    }
}
