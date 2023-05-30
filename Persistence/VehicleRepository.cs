using Microsoft.EntityFrameworkCore;
using vehicle_retailer.Models;

namespace vehicle_retailer.Persistence
{
  public class VehicleRepository : IVehicleRepository
  {
    private readonly ApiDbContext _context;
    public VehicleRepository(ApiDbContext context)
    {
      _context = context;
    }

    public async Task<Vehicle?> GetVehicle(int id)
    {
      return await _context.Vehicles
          .Include(v => v.Features)
              .ThenInclude(vf => vf.Feature)
          .Include(v => v.Model)
              .ThenInclude(m => m.Make)
          .SingleOrDefaultAsync(v => v.Id == id);
    }
  }
}