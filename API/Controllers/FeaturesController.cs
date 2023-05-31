using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using vehicleDealer.Controllers.Resources;
using vehicleDealer.Core.Models;
using vehicleDealer.Persistence;

namespace vehicleDealer.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class FeaturesController : ControllerBase
  {
    private readonly ApiDbContext _context;
    private readonly IMapper _mapper;

    public FeaturesController(ApiDbContext context, IMapper mapper)
    {
      _context = context;
      _mapper = mapper;
    }

    [HttpGet]
    public async Task<IEnumerable<KeyValuePairResource>> GetFeatures()
    {
      var features = await _context.Features.ToListAsync();

      return _mapper.Map<List<Feature>, List<KeyValuePairResource>>(features);
    }
  }
}