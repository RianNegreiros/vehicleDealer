using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using vehicleDealer.Controllers.Resources;
using vehicleDealer.Core.Interfaces;
using vehicleDealer.Core.Models;

namespace vehicleDealer.Controllers
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

    /// <summary>
    /// Create a new vehicle
    /// </summary>
    /// <param name="vehicleResource">Vehicle data to be created</param>
    /// <returns>A specific vehicle</returns>
    /// <response code="200">Returns a specific vehicle</response>
    [HttpPost]
    [Authorize]
    [ProducesResponseType(typeof(VehicleResource), 200)]
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

    /// <summary>
    /// Update a specific vehicle
    /// </summary>
    /// <param name="id">Vehicle Identifier</param>
    /// <param name="vehicleResource">Vehicle data to be updated</param>
    /// <returns>A specific vehicle</returns>
    /// <response code="200">Returns a specific vehicle</response>
    /// <response code="404">If the vehicle is not found</response>
    [HttpPut("{id}")]
    [Authorize]
    [ProducesResponseType(typeof(VehicleResource), 200)]
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

    /// <summary>
    /// Delete a specific vehicle
    /// </summary>
    /// <param name="id">Vehicle Identifier</param>
    /// <returns>A specific vehicle</returns>
    /// <response code="200">Returns a specific vehicle</response>
    /// <response code="404">If the vehicle is not found</response>
    [HttpDelete("{id}")]
    [Authorize]
    [ProducesResponseType(typeof(int), 200)]
    public async Task<IActionResult> DeleteVehicle(int id)
    {
      var vehicle = await _repository.GetVehicle(id, includeRelated: false);

      if (vehicle == null)
        return NotFound();

      _repository.Remove(vehicle);
      await _unitOfWork.CompleteAsync();

      return Ok(id);
    }

    /// <summary>
    /// Get a specific vehicle
    /// </summary>
    /// <param name="id">Vehicle Identifier</param>
    /// <returns>A specific vehicle</returns>
    /// <response code="200">Returns a specific vehicle</response>
    /// <response code="404">If the vehicle is not found</response>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(VehicleResource), 200)]
    public async Task<IActionResult> GetVehicle(int id)
    {
      var vehicle = await _repository.GetVehicle(id);

      if (vehicle == null)
        return NotFound();

      var vehicleResource = _mapper.Map<Vehicle, VehicleResource>(vehicle);

      return Ok(vehicleResource);
    }

    /// <summary>
    /// Get all vehicles
    /// </summary>
    /// <param name="filterResource">Filter to be applied</param>
    /// <returns>A list of vehicles</returns>
    /// <response code="200">Returns a list or a empty list of vehicles</response>
    [HttpGet]
    [ProducesResponseType(typeof(QueryResultResource<VehicleResource>), 200)]
    public async Task<QueryResultResource<VehicleResource>> GetVehicles(VehicleQueryResource filterResource)
    {
      var filter = _mapper.Map<VehicleQueryResource, VehicleQuery>(filterResource);
      var queryResult = await _repository.GetVehicles(filter);

      return _mapper.Map<QueryResult<Vehicle>, QueryResultResource<VehicleResource>>(queryResult);
    }
  }
}