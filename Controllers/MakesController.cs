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

    [HttpGet]
    public async Task<IEnumerable<MakeResource>> GetMakes()
    {
      var makes = await _context.Makes.Include(m => m.Models).ToListAsync();

      return _mapper.Map<List<Make>, List<MakeResource>>(makes);
    }
  }
}