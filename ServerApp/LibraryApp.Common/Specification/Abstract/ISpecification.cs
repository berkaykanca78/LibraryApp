using LibraryApp.Common.Specification.Concrete;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace LibraryApp.Common.Specification.Abstract
{
    public interface ISpecification<T>
    {
        Expression<Func<T, bool>> Criteria { get; }
        List<Expression<Func<T, object>>> Includes { get; }
        List<string> IncludeStrings { get; }
        Expression<Func<T, object>> OrderByExpression { get; }
        Expression<Func<T, object>> OrderByDescendingExpression { get; }
    }
}
