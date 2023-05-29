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
  public class FeatureController : ControllerBase
  {
    private readonly ApiDbContext _context;
    private readonly IMapper _mapper;

    public FeatureController(ApiDbContext context, IMapper mapper)
    {
      _context = context;
      _mapper = mapper;
    }

    [HttpGet("/features")]
    public async Task<IEnumerable<FeatureResource>> GetFeatures()
    {
      var features = await _context.Features.ToListAsync();

      return _mapper.Map<List<Feature>, List<FeatureResource>>(features);
    }
  }
}