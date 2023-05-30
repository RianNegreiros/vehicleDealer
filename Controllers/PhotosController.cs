using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using vehicle_retailer.Controllers.Resources;
using vehicle_retailer.Core.Interfaces;
using vehicle_retailer.Core.Models;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

namespace vehicle_retailer.Controllers
{
  [ApiController]
  [Route("api/vehicles/{vehicleId}/[controller]")]
  public class PhotosController : ControllerBase
  {
    private readonly IHostingEnvironment _host;
    private readonly IVehicleRepository _repository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly PhotoSettings _photoSettings;
    public PhotosController(IHostingEnvironment host, IVehicleRepository repository, IUnitOfWork unitOfWork, IMapper mapper, IOptionsSnapshot<PhotoSettings> options)
    {
      _host = host;
      _repository = repository;
      _unitOfWork = unitOfWork;
      _mapper = mapper;
      _photoSettings = options.Value;
    }
    [HttpPost]
    public async Task<IActionResult> Upload(int vehicleId, IFormFile file)
    {
      var vehicle = await _repository.GetVehicle(vehicleId, includeRelated: false);
      if (vehicle == null)
        return NotFound();

      if (file == null) return BadRequest("Null file");
      if (file.Length == 0) return BadRequest("Empty file");
      if (file.Length > _photoSettings.MaxBytes) return BadRequest("Max file size exceeded");
      if (!_photoSettings.IsSupported(file.FileName)) return BadRequest("Invalid file type.");

      var uploadsFolderPath = Path.Combine(_host.WebRootPath, "uploads");
      if (!Directory.Exists(uploadsFolderPath))
        Directory.CreateDirectory(uploadsFolderPath);

      var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
      var filePath = Path.Combine(uploadsFolderPath, fileName);

      using (var stream = new FileStream(filePath, FileMode.Create))
      {
        await file.CopyToAsync(stream);
      }

      var photo = new Photo { FileName = fileName };
      vehicle.Photos.Add(photo);
      await _unitOfWork.CompleteAsync();

      return Ok(_mapper.Map<Photo, PhotoResource>(photo));
    }
  }
}