using Microsoft.EntityFrameworkCore;
using vehicle_retailer.Models;

namespace vehicle_retailer.Persistence
{
  public class ApiDbContext : DbContext
  {
    public ApiDbContext(DbContextOptions<ApiDbContext> options)
        : base(options)
    {
    }

    public DbSet<Make> Makes { get; set; }
  }
}