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
  public class MakesController : ControllerBase
  {
    private readonly ApiDbContext _context;
    private readonly IMapper _mapper;
    public MakesController(ApiDbContext context, IMapper mapper)
    {
      _mapper = mapper;
      _context = context;
    }

    /// <summary>
    /// Get all makes
    /// </summary>
    /// <returns>A list of makes</returns>
    /// <response code="200">Returns a list or a empty list of makes</response>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<MakeResource>), 200)]
    public async Task<IEnumerable<MakeResource>> GetMakes()
    {
      var makes = await _context.Makes.Include(m => m.Models).ToListAsync();

      return _mapper.Map<List<Make>, List<MakeResource>>(makes);
    }
  }
}