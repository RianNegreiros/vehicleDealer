using vehicle_retailer.Core.Interfaces;

namespace vehicle_retailer.Persistence
{
  public class UnitOfWork : IUnitOfWork
  {
    private readonly ApiDbContext _context;

    public UnitOfWork(ApiDbContext context)
    {
      _context = context;
    }

    public async Task CompleteAsync()
    {
      await _context.SaveChangesAsync();
    }
  }
}