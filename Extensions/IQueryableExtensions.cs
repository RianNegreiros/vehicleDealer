using System.Linq.Expressions;
using vehicleDealer.Core.Models;

namespace vehicleDealer.Extensions
{
  public static class IQueryableExtensions
  {
    public static IQueryable<Vehicle> ApplyFiltering(this IQueryable<Vehicle> query, VehicleQuery queryObj)
    {
      if (queryObj.MakeId.HasValue)
        query = query.Where(v => v.Model.MakeId == queryObj.MakeId.Value);

      if (queryObj.ModelId.HasValue)
        query = query.Where(v => v.ModelId == queryObj.ModelId.Value);

      return query;
    }

    public static IQueryable<T> ApplyOrdering<T>(this IQueryable<T> query, IQueryObject queryObject, Dictionary<string, Expression<Func<T, object>>> columnsMap)
    {
      if (String.IsNullOrWhiteSpace(queryObject.SortBy) || !columnsMap.ContainsKey(queryObject.SortBy))
        return query;

      if (queryObject.IsSortAscending)
        return query.OrderBy(columnsMap[queryObject.SortBy]);
      else
        return query.OrderByDescending(columnsMap[queryObject.SortBy]);
    }

    public static IQueryable<T> ApplyPaging<T>(this IQueryable<T> query, IQueryObject queryObj)
    {
      if (queryObj.Page <= 0)
        queryObj.Page = 1;

      if (queryObj.PageSize <= 0)
        queryObj.PageSize = 10;

      return query.Skip((queryObj.Page - 1) * queryObj.PageSize).Take(queryObj.PageSize);
    }
  }
}