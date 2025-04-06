using Microsoft.EntityFrameworkCore;
using OnlineShop.Models;

namespace OnlineShop.Context
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Villa>()
                .HasMany(v => v.Details)
                .WithOne(d => d.Villa)
                .HasForeignKey(d => d.VillaId);
        }

        public DbSet<Villa> Villas { get; set; }
        public DbSet<Detail> Details { get; set; }
        public DbSet<Customer> Customers { get; set; }
    }
}
