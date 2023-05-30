using Microsoft.EntityFrameworkCore;
using vehicleDealer.Core.Interfaces;
using vehicleDealer.Core.Models;

namespace vehicleDealer.Persistence
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