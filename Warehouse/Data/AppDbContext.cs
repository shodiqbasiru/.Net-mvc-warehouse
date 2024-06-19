using Microsoft.EntityFrameworkCore;
using Warehouse.Models;

namespace Warehouse.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }
    
    public DbSet<Gudang> Gudang { get; set; }
    public DbSet<Barang> Barang { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Gudang>()
            .ToTable("tb_gudang");
        
        modelBuilder.Entity<Barang>()
            .ToTable("tb_barang");
        
        modelBuilder.Entity<Gudang>()
            .HasKey(gudang => gudang.Id);

        modelBuilder.Entity<Barang>()
            .HasKey(barang => barang.Id);

        modelBuilder.Entity<Barang>()
            .HasOne(barang => barang.Gudang)
            .WithMany(barang => barang.ListBarang)
            .HasForeignKey(barang => barang.GudangId);
    }
}