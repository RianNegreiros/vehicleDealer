namespace vehicleDealer.Core.Interfaces
{
  public interface IUnitOfWork
  {
    Task CompleteAsync();
  }
}