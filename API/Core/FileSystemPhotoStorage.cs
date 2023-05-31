namespace vehicleDealer.Core
{
  public class FileSystemPhotoStorage : IPhotoStorage
  {
    public async Task<string> StorePhoto(string uploadsFolderPath, IFormFile file)
    {
      if (!Directory.Exists(uploadsFolderPath))
        Directory.CreateDirectory(uploadsFolderPath);

      var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
      var filePath = Path.Combine(uploadsFolderPath, fileName);

      using (var stream = new FileStream(filePath, FileMode.Create))
      {
        await file.CopyToAsync(stream);
      }

      return fileName;
    }
  }
}