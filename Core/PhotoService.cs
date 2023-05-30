using vehicleDealer.Core.Interfaces;
using vehicleDealer.Core.Models;

namespace vehicleDealer.Core
{
  public class PhotoService : IPhotoService
  {
    private readonly IUnitOfWork unitOfWork;
    private readonly IPhotoStorage photoStorage;
    public PhotoService(IUnitOfWork unitOfWork, IPhotoStorage photoStorage)
    {
      this.photoStorage = photoStorage;
      this.unitOfWork = unitOfWork;
    }

    public async Task<Photo> UploadPhoto(Vehicle vehicle, IFormFile file, string uploadsFolderPath)
    {
      var fileName = await photoStorage.StorePhoto(uploadsFolderPath, file);

      var photo = new Photo { FileName = fileName };
      vehicle.Photos.Add(photo);
      await unitOfWork.CompleteAsync();

      return photo;
    }
  }
}