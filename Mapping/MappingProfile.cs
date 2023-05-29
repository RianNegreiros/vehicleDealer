using AutoMapper;
using vehicle_retailer.Controllers.Resources;
using vehicle_retailer.Models;

namespace vehicle_retailer.Mapping
{
  public class MappingProfile : Profile
  {
    public MappingProfile()
    {
      // Domain to API Resource
      CreateMap<Make, MakeResource>();
      CreateMap<Model, ModelResource>();
      CreateMap<Feature, FeatureResource>();
    }
  }
}