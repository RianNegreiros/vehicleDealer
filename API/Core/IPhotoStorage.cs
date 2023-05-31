namespace vehicleDealer.Core
{
  public interface IPhotoStorage
  {
    Task<string> StorePhoto(string uploadsFolderPath, IFormFile file);
  }
}