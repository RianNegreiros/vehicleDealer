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

    public async Task<Vehicle?> GetVehicle(int id, bool includeRelated = true)
    {
      if (!includeRelated)
        return await _context.Vehicles.FindAsync(id);

      return await _context.Vehicles
          .Include(v => v.Features)
              .ThenInclude(vf => vf.Feature)
          .Include(v => v.Model)
              .ThenInclude(m => m.Make)
          .SingleOrDefaultAsync(v => v.Id == id);
    }

    public void Add(Vehicle vehicle)
    {
      _context.Vehicles.Add(vehicle);
    }

    public void Remove(Vehicle vehicle)
    {
      _context.Vehicles.Remove(vehicle);
    }
  }
}