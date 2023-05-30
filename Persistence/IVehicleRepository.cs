using vehicle_retailer.Models;

namespace vehicle_retailer.Persistence
{
  public interface IVehicleRepository
  {
    Task<Vehicle?> GetVehicle(int id, bool includeRelated = true);
    public void Add(Vehicle vehicle);
    public void Remove(Vehicle vehicle);
  }
}