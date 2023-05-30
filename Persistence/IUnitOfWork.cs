namespace vehicle_retailer.Persistence
{
  public interface IUnitOfWork
  {
    Task CompleteAsync();
  }
}