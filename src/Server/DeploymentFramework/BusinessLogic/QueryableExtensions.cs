using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Baud.Deployment
{
    public static class QueryableExtensions
    {
        public static IQueryable<T> Filter<T>(this IQueryable<T> query, string filterValue, Expression<Func<T, bool>> predicate)
        {
            if (!string.IsNullOrEmpty(filterValue))
            {
                return query.Where(predicate);
            }
            else
            {
                return query;
            }
        }

        public static IQueryable<TItem> Filter<TItem, TStruct>(this IQueryable<TItem> query, IEnumerable<TStruct> filterValue, Expression<Func<TItem, bool>> predicate) where TStruct : struct
        {
            if (filterValue != null && filterValue.Any())
            {
                return query.Where(predicate);
            }
            else
            {
                return query;
            }
        }

        public static IQueryable<TItem> Filter<TItem, TStruct>(this IQueryable<TItem> query, TStruct? filterValue, Expression<Func<TItem, bool>> predicate) where TStruct : struct
        {
            if (filterValue != null)
            {
                return query.Where(predicate);
            }
            else
            {
                return query;
            }
        }
    }
}