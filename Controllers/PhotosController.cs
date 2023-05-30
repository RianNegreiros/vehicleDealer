using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using vehicleDealer.Controllers.Resources;
using vehicleDealer.Core.Interfaces;
using vehicleDealer.Core.Models;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

namespace vehicleDealer.Controllers
{
  [ApiController]
  [Route("api/vehicles/{vehicleId}/[controller]")]
  public class PhotosController : ControllerBase
  {
    private readonly IHostingEnvironment _host;
    private readonly IVehicleRepository _vehicleRepository;
    private readonly IPhotoRepository _photoRepository;
    private readonly IMapper _mapper;
    private readonly PhotoSettings _photoSettings;
    private readonly IPhotoService _photoService;

    public PhotosController(IHostingEnvironment host, IVehicleRepository vehicleRepository, IPhotoRepository photoRepository, IMapper mapper, IOptionsSnapshot<PhotoSettings> options, IPhotoService photoService)
    {
      _photoService = photoService;
      _photoSettings = options.Value;
      _mapper = mapper;
      _vehicleRepository = vehicleRepository;
      _photoRepository = photoRepository;
      _host = host;
    }

    [HttpGet]
    public async Task<IEnumerable<PhotoResource>> GetPhotos(int vehicleId)
    {
      var photos = await _photoRepository.GetPhotos(vehicleId);

      return _mapper.Map<IEnumerable<Photo>, IEnumerable<PhotoResource>>(photos);
    }

    [HttpPost]
    public async Task<IActionResult> Upload(int vehicleId, IFormFile file)
    {
      var vehicle = await _vehicleRepository.GetVehicle(vehicleId, includeRelated: false);
      if (vehicle == null)
        return NotFound();

      if (file == null) return BadRequest("Null file");
      if (file.Length == 0) return BadRequest("Empty file");
      if (file.Length > _photoSettings.MaxBytes) return BadRequest("Max file size exceeded");
      if (!_photoSettings.IsSupported(file.FileName)) return BadRequest("Invalid file type.");

      var uploadsFolderPath = Path.Combine(_host.WebRootPath, "uploads");
      var photo = await _photoService.UploadPhoto(vehicle, file, uploadsFolderPath);

      return Ok(_mapper.Map<Photo, PhotoResource>(photo));
    }
  }
}