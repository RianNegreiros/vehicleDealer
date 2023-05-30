using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using vehicle_retailer.Controllers.Resources;
using vehicle_retailer.Core.Interfaces;
using vehicle_retailer.Core.Models;

namespace vehicle_retailer.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class VehiclesController : ControllerBase
  {
    private readonly IMapper _mapper;
    private readonly IVehicleRepository _repository;
    private readonly IUnitOfWork _unitOfWork;

    public VehiclesController(IMapper mapper, IVehicleRepository repository, IUnitOfWork unitOfWork)
    {
      _mapper = mapper;
      _repository = repository;
      _unitOfWork = unitOfWork;
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> CreateVehicle([FromBody] SaveVehicleResource vehicleResource)
    {
      if (!ModelState.IsValid)
        return BadRequest(ModelState);

      var vehicle = _mapper.Map<SaveVehicleResource, Vehicle>(vehicleResource);
      vehicle.LastUpdate = DateTime.Now;

      _repository.Add(vehicle);
      await _unitOfWork.CompleteAsync();

      vehicle = await _repository.GetVehicle(vehicle.Id);

      var result = _mapper.Map<Vehicle, VehicleResource>(vehicle);

      return Ok(result);
    }

    [HttpPut("{id}")]
    [Authorize]
    public async Task<IActionResult> UpdateVehicle(int id, [FromBody] SaveVehicleResource vehicleResource)
    {
      if (!ModelState.IsValid)
        return BadRequest(ModelState);

      var vehicle = await _repository.GetVehicle(id);

      if (vehicle == null)
        return NotFound();

      _mapper.Map<SaveVehicleResource, Vehicle>(vehicleResource, vehicle);
      vehicle.LastUpdate = DateTime.Now;

      await _unitOfWork.CompleteAsync();

      vehicle = await _repository.GetVehicle(vehicle.Id);
      var result = _mapper.Map<Vehicle, VehicleResource>(vehicle);

      return Ok(result);
    }

    [HttpDelete("{id}")]
    [Authorize]
    public async Task<IActionResult> DeleteVehicle(int id)
    {
      var vehicle = await _repository.GetVehicle(id, includeRelated: false);

      if (vehicle == null)
        return NotFound();

      _repository.Remove(vehicle);
      await _unitOfWork.CompleteAsync();

      return Ok(id);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetVehicle(int id)
    {
      var vehicle = await _repository.GetVehicle(id);

      if (vehicle == null)
        return NotFound();

      var vehicleResource = _mapper.Map<Vehicle, VehicleResource>(vehicle);

      return Ok(vehicleResource);
    }

    [HttpGet]
    public async Task<QueryResultResource<VehicleResource>> GetVehicles(VehicleQueryResource filterResource)
    {
      var filter = _mapper.Map<VehicleQueryResource, VehicleQuery>(filterResource);
      var queryResult = await _repository.GetVehicles(filter);

      return _mapper.Map<QueryResult<Vehicle>, QueryResultResource<VehicleResource>>(queryResult);
    }
  }
}