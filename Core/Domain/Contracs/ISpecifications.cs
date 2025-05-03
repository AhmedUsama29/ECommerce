using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Contracs
{
    public interface ISpecifications<T> where T : class
    {

        Expression<Func<T,object>> OrderBy { get; } //For Sorting [Ascending]
        Expression<Func<T, object>> OrderByDescending { get; } //For Sorting [Descending]

        Expression<Func<T, bool>> Criteria { get; } //For Filtring

        List<Expression<Func<T, object>>> IncludeExpressions { get; } //For Eager Loading

        public int Skip { get; }
        public int Take { get; }
        public bool IsPagingEnabled { get; } //For Pagination

    }
}
