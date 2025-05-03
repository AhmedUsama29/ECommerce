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
        Expression<Func<T, bool>> Criteria { get; } //For Filtring

        List<Expression<Func<T, object>>> IncludeExpressions { get; } //For Eager Loading

    }
}
