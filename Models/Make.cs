using System.Collections.ObjectModel;

namespace vehicle_retailer.Models
{
  public class Make
  {
    public int Id { get; set; }
    public string Name { get; set; }
    public ICollection<Model> Models { get; set; }

    public Make()
    {
      Models = new Collection<Model>();
    }
  }
}