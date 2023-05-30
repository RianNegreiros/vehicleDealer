using vehicle_retailer.Core.Models;

namespace vehicle_retailer.Core.Interfaces
{
  public interface IPhotoRepository
  {
    Task<IEnumerable<Photo>> GetPhotos(int vehicleId);
  }
}