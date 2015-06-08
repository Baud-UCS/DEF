using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic;
using System.Linq.Expressions;
using System.Web;
using Baud.Deployment.Web.Framework.Web;
using PagedList;

namespace Baud.Deployment.Web
{
    public static class Extensions
    {
        public static string GetPropertyName<TModel, TValue>(this Expression<Func<TModel, TValue>> expression)
        {
            var expressionBody = expression.Body.ToString().TrimEnd(')');

            var dotInExpression = expressionBody.LastIndexOf('.');
            return dotInExpression > 0 ? expressionBody.Substring(dotInExpression + 1) : expressionBody;
        }

        public static IPagedList<T> ToPagedList<T>(this IQueryable<T> superset, PagingData paging)
        {
            var query = superset;
            if (!string.IsNullOrEmpty(paging.SortColumn))
            {
                string ordering = paging.SortColumn;
                if (paging.SortDirection == SortingDirection.Descending)
                {
                    ordering += " DESC";
                }

                try
                {
                    query = query.OrderBy(ordering);
                }
                catch (ParseException)
                {
                }
            }

            return query.ToPagedList(paging.PageNumber, paging.PageSize);
        }
    }
}