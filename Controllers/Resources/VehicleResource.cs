using System.Collections.ObjectModel;

namespace vehicle_retailer.Controllers.Resources
{
  public class VehicleResource
  {
    public int Id { get; set; }
    public KeyValuePairResource Model { get; set; }
    public MakeResource Make { get; set; }
    public bool IsRegistered { get; set; }
    public ContactResource Contact { get; set; }
    public DateTime LastUpdate { get; set; }
    public ICollection<KeyValuePairResource> Features { get; set; }

    public VehicleResource()
    {
      Features = new Collection<KeyValuePairResource>();
    }
  }
}