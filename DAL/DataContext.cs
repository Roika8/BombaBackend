using DATA;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Diagnostics.CodeAnalysis;

namespace DAL
{
    public class DataContext : IdentityDbContext<User>
    {
        public DataContext([NotNull] DbContextOptions options) : base(options)
        {
        }


        public DbSet<Portfolio> Portfolios { get; set; }
        public DbSet<PortfolioInstrument> PortfolioInstruments { get; set; }

        //public DbSet<User> Users { get; set; }
        public DbSet<CashData> CashDatas { get; set; }

        public DbSet<HistoryInstument> HistoryInstuments { get; set; }
        public DbSet<HistoryPortfolio> HistoryPortfolios { get; set; }

        public DbSet<TrackingInstrument> TrackingInstruments { get; set; }
        public DbSet<TrackingPortfolio> TrackingPortfolios { get; set; }
        public DbSet<TrackingInstumentPrice> TrackingInstumentsPrice { get; set; }
    }
}
