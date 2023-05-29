using Microsoft.EntityFrameworkCore;
using vehicle_retailer.Models;

namespace vehicle_retailer.Persistence
{
  public class ApiDbContext : DbContext
  {
    public DbSet<Vehicle> Vehicles { get; set; }
    public DbSet<Make> Makes { get; set; }
    public DbSet<Model> Models { get; set; }
    public DbSet<Feature> Features { get; set; }

    public ApiDbContext(DbContextOptions<ApiDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      modelBuilder.Entity<VehicleFeature>().HasKey(vf =>
        new { vf.VehicleId, vf.FeatureId });
    }
  }
}