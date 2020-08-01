using Microsoft.EntityFrameworkCore;
using P03_SalesDatabase.Data.Models;

namespace P03_SalesDatabase.Data
{
    public class SalesContext : DbContext
    {
        public SalesContext()
        {

        }

        public SalesContext(DbContextOptions options)
            : base(options)
        {

        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Sale> Sales { get; set; }
        public DbSet<Store> Stores { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder
                    .UseSqlServer(Configuration.ConnectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>(entity =>
            {
                entity
                    .Property(c => c.Name)
                    .IsUnicode(true);

                entity
                    .Property(c => c.Email)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity
                    .Property(p => p.Name)
                    .IsUnicode(true);
            });

            modelBuilder.Entity<Store>(entity =>
            {
                entity
                    .Property(s => s.Name)
                    .IsUnicode(true);
            });

            modelBuilder.Entity<Sale>(entity =>
            {
            });
        }
    }
}
