using DATA;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;
using DATA.Portfolios;
using DATA.Instruments;

namespace DAL
{
    public class MainDataContext : DbContext
    {
        public MainDataContext([NotNull] DbContextOptions<MainDataContext> options) : base(options)
        {

        }
        public MainDataContext()
        {

        }


        protected override void OnModelCreating(ModelBuilder builder)
        {

            builder.Entity<HistoryInstrument>()
                 .HasOne(h => h.Portfolio)
                 .WithMany(hp => hp.Instruments)
                 .HasForeignKey(hi => hi.PortfolioId);


            builder.Entity<PortfolioInstrument>()
                 .HasOne(h => h.Portfolio)
                 .WithMany(hp => hp.Instruments)
                 .HasForeignKey(hi => hi.PortfolioId);

            builder.Entity<TrackingInstrument>()
             .HasOne(h => h.Portfolio)
             .WithMany(hp => hp.Instruments)
             .HasForeignKey(hi => hi.PortfolioId);


            builder.Entity<TrackingInstrument>()
                .HasMany(c => c.TrackingPrices)
                .WithOne(tp => tp.Instrument)
                .HasForeignKey(tp => tp.InstrumentID);

            base.OnModelCreating(builder);

        }
        public virtual DbSet<Portfolio> Portfolios { get; set; }
        public DbSet<PortfolioInstrument> PortfolioInstruments { get; set; }
        public DbSet<CashData> CashDatas { get; set; }

        public DbSet<HistoryInstrument> HistoryInstuments { get; set; }
        public DbSet<HistoryPortfolio> HistoryPortfolios { get; set; }

        public DbSet<TrackingInstrument> TrackingInstruments { get; set; }
        public DbSet<TrackingPortfolio> TrackingPortfolios { get; set; }
        public DbSet<TrackingInstrumentPrice> TrackingInstumentsPrice { get; set; }
    }
}
