using Domain.Contracs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories
{
    public static class SpecificationEvaluator
    {

        public static IQueryable<T> CreateQuery<T>(IQueryable<T> inputQuery, ISpecifications<T> specifications) where T : class
        {
            
            var query = inputQuery;
            
            if(specifications.Criteria is not null)
            {
                query = query.Where(specifications.Criteria);
            }

            if (specifications.OrderBy is not null)
            {
                query = query.OrderBy(specifications.OrderBy);
            }else if (specifications.OrderByDescending is not null)
            {
                query = query.OrderByDescending(specifications.OrderByDescending);
            }

            if (specifications.IsPagingEnabled)
            {
                query = query.Skip(specifications.Skip).Take(specifications.Take);
            }

            foreach (var includeExpression in specifications.IncludeExpressions)
            {
                query = query.Include(includeExpression);
            }

            //  Another way
            //query = specifications.IncludeExpressions.Aggregate(query, (current, includeExpression) => current.Include(includeExpression));

            return query;

        }

    }
}
