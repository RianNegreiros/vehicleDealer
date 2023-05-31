using vehicleDealer.Core.Models;

namespace vehicleDealer.Core.Interfaces
{
  public interface IPhotoRepository
  {
    Task<IEnumerable<Photo>> GetPhotos(int vehicleId);
  }
}