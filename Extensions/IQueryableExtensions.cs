using System.Linq.Expressions;

namespace vehicle_retailer.Extensions
{
  public static class IQueryableExtensions
  {
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