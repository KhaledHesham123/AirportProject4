using AirportProject4.Project.core.Entities.main;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace AirportProject4.Shared.Specification
{
    public class BaseSpecification<TEntitty> : ISpecification<TEntitty> where TEntitty : BaseEntity
    {
        public Expression<Func<TEntitty, bool>> _Criteria { get; set; } = null;
        public List<Expression<Func<TEntitty, object>>> _includes { get; set; } = new List<Expression<Func<TEntitty, object>>>();
        public List<Func<IQueryable<TEntitty>, IIncludableQueryable<TEntitty, object>>> _Thenincludes { get; set; } = new List<Func<IQueryable<TEntitty>, IIncludableQueryable<TEntitty, object>>>();

        public BaseSpecification(Expression<Func<TEntitty, bool>> Criteria)
        {
            _Criteria = Criteria;
        }

        public BaseSpecification()
        {
            
        }

    }
}
