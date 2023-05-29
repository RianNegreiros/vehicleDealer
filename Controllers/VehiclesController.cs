using AutoMapper;
using Microsoft.AspNetCore.Mvc;
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
      var vehicle = _mapper.Map<VehicleResource, Vehicle>(vehicleResource);
      vehicle.LastUpdate = DateTime.Now;
      _context.Vehicles.Add(vehicle);
      await _context.SaveChangesAsync();

      var result = _mapper.Map<Vehicle, VehicleResource>(vehicle);
      return Ok(result);
    }
  }
}