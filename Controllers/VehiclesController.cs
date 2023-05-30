using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using vehicle_retailer.Controllers.Resources;
using vehicle_retailer.Models;
using vehicle_retailer.Persistence;

namespace vehicle_retailer.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class VehiclesController : ControllerBase
  {
    private readonly ApiDbContext _context;
    private readonly IMapper _mapper;
    private readonly IVehicleRepository _repository;

    public VehiclesController(ApiDbContext context, IMapper mapper, IVehicleRepository repository)
    {
      _context = context;
      _mapper = mapper;
      _repository = repository;
    }

    [HttpPost]
    public async Task<IActionResult> CreateVehicle([FromBody] SaveVehicleResource vehicleResource)
    {
      if (!ModelState.IsValid)
        return BadRequest(ModelState);

      var vehicle = _mapper.Map<SaveVehicleResource, Vehicle>(vehicleResource);
      vehicle.LastUpdate = DateTime.Now;

      _context.Vehicles.Add(vehicle);
      await _context.SaveChangesAsync();

      vehicle = await _repository.GetVehicle(vehicle.Id);

      var result = _mapper.Map<Vehicle, VehicleResource>(vehicle);

      return Ok(result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateVehicle(int id, [FromBody] SaveVehicleResource vehicleResource)
    {
      if (!ModelState.IsValid)
        return BadRequest(ModelState);

      var vehicle = await _repository.GetVehicle(id);

      if (vehicle == null)
        return NotFound();

      _mapper.Map<SaveVehicleResource, Vehicle>(vehicleResource, vehicle);
      vehicle.LastUpdate = DateTime.Now;

      await _context.SaveChangesAsync();

      var result = _mapper.Map<Vehicle, VehicleResource>(vehicle);

      return Ok(result);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteVehicle(int id)
    {
      var vehicle = await _context.Vehicles.FindAsync(id);

      if (vehicle == null)
        return NotFound();

      _context.Remove(vehicle);
      await _context.SaveChangesAsync();

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
  }
}