using vehicle_retailer.Core.Models;

namespace vehicle_retailer.Core.Interfaces
{
  public interface IVehicleRepository
  {
    Task<Vehicle?> GetVehicle(int id, bool includeRelated = true);
    public void Add(Vehicle vehicle);
    public void Remove(Vehicle vehicle);
    public Task<IEnumerable<Vehicle>> GetVehicles(VehicleQuery queryObj);
  }
}