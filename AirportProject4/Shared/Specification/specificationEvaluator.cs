using AirportProject4.Project.core.Entities.main;
using Microsoft.EntityFrameworkCore;

namespace AirportProject4.Shared.Specification
{
    public static class specificationEvaluator<T> where T : BaseEntity
    {
        public static IQueryable<T> GetQuery(IQueryable<T> inputQuery, ISpecification<T> spec) 
        {
            var query = inputQuery;

            if (spec._Criteria is not null)
            {
                query = query.Where(spec._Criteria);

            }

            //if (spec._OrderBy is not null)
            //{
            //    query = query.OrderBy(spec._OrderBy);
            //}

            //if (spec._OrderByDesc is not null)
            //{
            //    query = query.OrderByDescending(spec._OrderByDesc);
            //}

            query = spec._includes.Aggregate(query, (CurrentQuery, includequery) => CurrentQuery.Include(includequery));
            query = spec._Thenincludes.Aggregate(query, (current, include) => include(current)); // ✅ كده صح

           

            return query;
        }
    }
}
