using LibraryApp.Common.Specification.Abstract;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace LibraryApp.Common.Extensions
{
    public static class QuerySpecificationExtensions
    {
        public static IQueryable<T> Specify<T>(this IQueryable<T> query, ISpecification<T> spec) where T : class
        {
            // fetch a Queryable that includes all expression-based includes
            var queryableResultWithIncludes = spec.Includes
                .Aggregate(query,
                    (current, include) => current.Include(include));

            // modify the IQueryable to include any string-based include statements
            var secondaryResult = spec.IncludeStrings
                .Aggregate(queryableResultWithIncludes,
                    (current, include) => current.Include(include));

            // return the result of the query using the specification's criteria expression
            if (spec.Criteria != null)
                return secondaryResult.Where(spec.Criteria);

            if(spec.OrderByExpression != null)
                return secondaryResult.OrderBy(spec.OrderByExpression);

            if (spec.OrderByDescendingExpression != null)
                return secondaryResult.OrderByDescending(spec.OrderByDescendingExpression);

            return secondaryResult;
        }
    }
}
