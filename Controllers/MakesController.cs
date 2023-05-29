using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using vehicle_retailer.Models;
using vehicle_retailer.Persistence;

namespace vehicle_retailer.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class MakesController : ControllerBase
  {
    private readonly ApiDbContext _context;
    public MakesController(ApiDbContext context)
    {
      _context = context;
    }

    [HttpGet("/makes")]
    public async Task<IEnumerable<Make>> GetMakes()
    {
      return await _context.Makes.Include(m => m.Models).ToListAsync();
    }
  }
}