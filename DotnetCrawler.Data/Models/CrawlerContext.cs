using Microsoft.EntityFrameworkCore;

namespace DotnetCrawler.Data.Models
{
    public class CrawlerContext : DbContext
    {
        public CrawlerContext() { }

        public CrawlerContext(DbContextOptions<CrawlerContext> options)
            : base(options)
        { }

        public DbSet<Apartment> Apartments { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=KKERPE\\SQLEXPRESS;Database=Microsoft.eShopOnWeb.CatalogDb;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Apartment>()
                .HasKey(b => b.Id);

            modelBuilder.Entity<Apartment>(entity =>
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
