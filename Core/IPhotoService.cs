using vehicleDealer.Core.Models;

namespace vehicleDealer.Core.Interfaces
{
  public interface IPhotoService
  {
    Task<Photo> UploadPhoto(Vehicle vehicle, IFormFile file, string uploadsFolderPath);
  }
}