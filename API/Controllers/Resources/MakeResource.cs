using System.Collections.ObjectModel;

namespace vehicleDealer.Controllers.Resources
{
  public class MakeResource : KeyValuePairResource
  {
    public ICollection<KeyValuePairResource> Models { get; set; }

    public MakeResource()
    {
      Models = new Collection<KeyValuePairResource>();
    }
  }
}