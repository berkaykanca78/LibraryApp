using LibraryApp.Common.Specification.Abstract;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace LibraryApp.Common.Specification.Concrete
{
    public abstract class BaseSpecification<T> : ISpecification<T>
    {
        public Expression<Func<T, bool>> Criteria { get; set; }
        public List<Expression<Func<T, object>>> Includes { get; } = new List<Expression<Func<T, object>>>();
        public List<string> IncludeStrings { get; } = new List<string>();
        public Expression<Func<T, object>> OrderByExpression { get; private set; }
        public Expression<Func<T, object>> OrderByDescendingExpression { get; private set; }

        protected virtual void AddInclude(Expression<Func<T, object>> includeExpression)
        {
            Includes.Add(includeExpression);
        }

        protected virtual void AddInclude(string includeString)
        {
            IncludeStrings.Add(includeString);
        }

        protected void AddOrderBy(Expression<Func<T, object>> orderExpression) => OrderByExpression = orderExpression;
        protected void AddOrderByDescending(Expression<Func<T, object>> orderByDescendingExpression) => OrderByDescendingExpression = orderByDescendingExpression;
    }
}
