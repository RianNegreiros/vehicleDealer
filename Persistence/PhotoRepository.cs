using Microsoft.EntityFrameworkCore;
using vehicle_retailer.Core.Interfaces;
using vehicle_retailer.Core.Models;

namespace vehicle_retailer.Persistence
{
  public class PhotoRepository : IPhotoRepository
  {
    private readonly ApiDbContext _context;
    public PhotoRepository(ApiDbContext context)
    {
      _context = context;
    }
    public async Task<IEnumerable<Photo>> GetPhotos(int vehicleId)
    {
      return await _context.Photos
        .Where(p => p.VehicleId == vehicleId)
        .ToListAsync();
    }
  }
}