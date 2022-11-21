using DATA;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Diagnostics.CodeAnalysis;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using System.Reflection.Emit;

namespace DAL
{
    public class MainDataContext : DbContext
    {
        public MainDataContext([NotNull] DbContextOptions<MainDataContext> options) : base(options)
        {
           
        }
      
   
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Portfolio>()
                   .HasMany(c => c.Instruments)
                   .WithOne(e => e.Portfolio);

            base.OnModelCreating(builder);

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
