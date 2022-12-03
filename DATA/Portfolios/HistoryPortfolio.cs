using DATA.Instruments;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace DATA.Portfolios
{
    public class HistoryPortfolio : BasePortfolio<HistoryInstument>
    { }
}