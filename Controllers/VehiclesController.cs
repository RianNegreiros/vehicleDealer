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

    public VehiclesController(ApiDbContext context, IMapper mapper)
    {
      _context = context;
      _mapper = mapper;
    }

    [HttpPost]
    public async Task<IActionResult> CreateVehicle([FromBody] VehicleResource vehicleResource)
    {
      if (!ModelState.IsValid)
        return BadRequest(ModelState);

      var vehicle = _mapper.Map<VehicleResource, Vehicle>(vehicleResource);
      vehicle.LastUpdate = DateTime.Now;

      _context.Vehicles.Add(vehicle);
      await _context.SaveChangesAsync();

      var result = _mapper.Map<Vehicle, VehicleResource>(vehicle);

      return Ok(result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateVehicle(int id, [FromBody] VehicleResource vehicleResource)
    {
      if (!ModelState.IsValid)
        return BadRequest(ModelState);

      var vehicle = await _context.Vehicles.Include(v => v.Features).SingleOrDefaultAsync(v => v.Id == id);

      if (vehicle == null)
        return NotFound();

      _mapper.Map<VehicleResource, Vehicle>(vehicleResource, vehicle);
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
  }
}