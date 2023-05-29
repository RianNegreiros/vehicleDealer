using Microsoft.EntityFrameworkCore;

namespace vehicle_retailer.Persistence
{
  public class ApiDbContext : DbContext
  {
    public ApiDbContext(DbContextOptions<ApiDbContext> options)
        : base(options)
    {

    }
  }
}