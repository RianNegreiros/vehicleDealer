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

    /// <summary>
    /// Get all photos from a specific vehicle
    /// </summary>
    /// <param name="vehicleId">Vehicle Identifier</param>
    /// <returns>A list of photos</returns>
    /// <response code="200">Returns a list or a empty list of photos</response>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<PhotoResource>), 200)]
    public async Task<IEnumerable<PhotoResource>> GetPhotos(int vehicleId)
    {
      var photos = await _photoRepository.GetPhotos(vehicleId);

      return _mapper.Map<IEnumerable<Photo>, IEnumerable<PhotoResource>>(photos);
    }

    /// <summary>
    /// Upload a photo to a specific vehicle
    /// </summary>
    /// <param name="vehicleId">Vehicle Identifier</param>
    /// <param name="file">Photo file</param>
    /// <returns>A specific photo</returns>
    /// <response code="200">Returns a specific photo</response>
    /// <response code="404">If the vehicle is not found</response>
    /// <response code="400">If the file is null, empty, exceeds the max file size or is not supported</response>
    [HttpPost]
    [ProducesResponseType(typeof(PhotoResource), 200)]
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