using AirportProject4.Project.core.Entities.main;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace AirportProject4.Shared.Specification
{
    public interface ISpecification<TEntitty> where TEntitty : BaseEntity
    {
        public Expression<Func<TEntitty, bool>> _Criteria { get; set; }

        public List<Expression<Func<TEntitty, object>>> _includes { get; set; }

        List<Func<IQueryable<TEntitty>, IIncludableQueryable<TEntitty, object>>> _Thenincludes { get; set; }
    }
}
