using Microsoft.EntityFrameworkCore;
using PruebaTecnica.Models;

namespace PruebaTecnica.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions <AppDbContext> options) : base(options) { }

        public DbSet<Product> productos { get; set; } = null!;
        public DbSet<cliente> Clientes { get; set; } = null!;
        public DbSet<Sale> ventas { get; set; } = null!;
        public DbSet<saleDetail> detalleVenta { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>().HasKey(p => p.productoId);
            modelBuilder.Entity<cliente>().HasKey(c => c.clienteId);
            modelBuilder.Entity<Sale>().HasKey(v => v.ventaId);
            modelBuilder.Entity<saleDetail>().HasKey(dv => dv.detalleId);

            modelBuilder.Entity<saleDetail>()
                .HasOne(d => d.producto)
                .WithMany()
                .HasForeignKey(d => d.productoId);

            base.OnModelCreating(modelBuilder);

        }   
    }
}
