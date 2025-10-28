using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;

namespace AirportProject4.Shared
{
    public class BaseQueryHandler
    {
        protected void verifypagination(int pageNumber, int pageSize)
        {
            if (pageNumber <= 0)
                throw new ArgumentException("Page number must be greater than 0.");
            if (pageSize <= 0 || pageSize > 100)
                throw new ArgumentException("Page size must be between 1 and 100.");

           
        }

        protected IQueryable<T> ApplySearch<T>(IQueryable<T> query, string? search, Func<T, string> selector)
        {
            if (!string.IsNullOrWhiteSpace(search))
            {
                var term = search.Trim().ToLower();
                query = query.Where(x => selector(x).ToLower().Contains(term));
            }
            return query;
        }

        protected IQueryable<T> ApplySorting<T, TKey>(
       IQueryable<T> query,
       string sortBy,
       string sortDir,
       Expression<Func<T, TKey>> keySelectorDefault,
       Dictionary<string, Expression<Func<T, TKey>>>? customSelectors = null)
        {
            bool asc = string.Equals(sortDir, "asc", StringComparison.OrdinalIgnoreCase);

            if (customSelectors != null && customSelectors.ContainsKey(sortBy.ToLower()))
            {
                var selector = customSelectors[sortBy.ToLower()];
                return asc ? query.OrderBy(selector) : query.OrderByDescending(selector);
            }

            return asc ? query.OrderBy(keySelectorDefault) : query.OrderByDescending(keySelectorDefault);
        }


        protected IQueryable<T> Applypagination<T>(IQueryable<T> query, int pageNumber, int pageSize)
        {
            verifypagination(pageNumber, pageSize);
            return query.Skip((pageNumber - 1) * pageSize).Take(pageSize);
        }
    }
}
