using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using vehicle_retailer.Models;
using vehicle_retailer.Persistence;

namespace vehicle_retailer.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class FeatureController : ControllerBase
  {
    private readonly ApiDbContext _context;

    public FeatureController(ApiDbContext context)
    {
      _context = context;
    }

    [HttpGet("/features")]
    public async Task<IEnumerable<Feature>> GetFeatures()
    {
      return await _context.Features.ToListAsync();
    }
  }
}