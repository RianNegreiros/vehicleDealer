using vehicle_retailer.Models;

namespace vehicle_retailer.Persistence
{
  public interface IVehicleRepository
  {
    Task<Vehicle?> GetVehicle(int id);
  }
}