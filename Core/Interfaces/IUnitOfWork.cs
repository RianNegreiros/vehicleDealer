namespace vehicle_retailer.Core.Interfaces
{
  public interface IUnitOfWork
  {
    Task CompleteAsync();
  }
}