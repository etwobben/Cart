using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Data
{
    public class DatabaseContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<CartLine> CartLines { get; set; }

        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>().HasData(new Product() { Id = 1, Name = "Stoel", Description = "Een stoel met maar liefst 4 poten", Price = 10 });
            modelBuilder.Entity<Product>().HasData(new Product() { Id = 2, Name = "Tafel", Description = "Een tafel met 1 hele grote poot", Price = 799.95 });
            modelBuilder.Entity<Product>().HasData(new Product() { Id = 3, Name = "Lamp", Description = "Een hele bijzondere die mooi paars licht geeft", Price = 16.95 });
        }
    }
}
