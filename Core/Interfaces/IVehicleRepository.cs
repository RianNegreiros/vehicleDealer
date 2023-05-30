using vehicleDealer.Core.Models;

namespace vehicleDealer.Core.Interfaces
{
  public interface IVehicleRepository
  {
    Task<Vehicle?> GetVehicle(int id, bool includeRelated = true);
    public void Add(Vehicle vehicle);
    public void Remove(Vehicle vehicle);
    public Task<QueryResult<Vehicle>> GetVehicles(VehicleQuery queryObj);
  }
}