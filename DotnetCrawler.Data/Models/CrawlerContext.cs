using DotnetCrawler.Data.Models.Clarteys;
using DotnetCrawler.Data.Models.EIzsoles;
using Microsoft.EntityFrameworkCore;

namespace DotnetCrawler.Data.Models
{
    public class CrawlerContext : DbContext
    {
        public CrawlerContext() { }

        public CrawlerContext(DbContextOptions<CrawlerContext> options)
            : base(options)
        { }

        public DbSet<ClarteysApartment> ClarteysApartments { get; set; }
        public DbSet<EIzsolesThing> EIzsolesThings { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=KKERPE\\SQLEXPRESS;Database=Crawler;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ClarteysApartment>()
                .HasKey(b => b.Id);

            modelBuilder.Entity<ClarteysApartment>(entity =>
            {
                entity.Property(e => e.StartingPrice)
                    .IsRequired()
                    .HasMaxLength(10);

                entity.Property(e => e.Address)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Rooms)
                    .IsRequired()
                    .HasMaxLength(10);

                entity.Property(e => e.Area)
                    .IsRequired()
                    .HasMaxLength(10);
            });
        }
    }
}
