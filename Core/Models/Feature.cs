using System.ComponentModel.DataAnnotations;

namespace vehicleDealer.Core.Models
{
  public class Feature
  {
    public int Id { get; set; }

    [Required]
    [StringLength(255)]
    public string Name { get; set; }
  }
}