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

    /// <summary>
    /// Get all features
    /// </summary>
    /// <returns>A list of features</returns>
    /// <response code="200">Returns a list or a empty list of features</response>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<KeyValuePairResource>), 200)]
    public async Task<IEnumerable<KeyValuePairResource>> GetFeatures()
    {
      var features = await _context.Features.ToListAsync();

      return _mapper.Map<List<Feature>, List<KeyValuePairResource>>(features);
    }
  }
}