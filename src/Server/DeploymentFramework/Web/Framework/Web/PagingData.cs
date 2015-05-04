using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;

namespace Baud.Deployment.Web.Framework.Web
{
    public class PagingData
    {
        public static readonly string PageNumberPropertyName;
        public static readonly string SortColumnPropertyName;
        public static readonly string SortDirectionPropertyName;

        private static int _defaultPageSize = 30;
        public static int DefaultPageSize
        {
            get { return _defaultPageSize; }
            set { _defaultPageSize = value; }
        }

        static PagingData()
        {
            Expression<Func<PagingData, int>> pageNumberExpression = x => x.PageNumber;
            PageNumberPropertyName = ExpressionHelper.GetExpressionText(pageNumberExpression);

            Expression<Func<PagingData, string>> sortColumnExpression = x => x.SortColumn;
            SortColumnPropertyName = ExpressionHelper.GetExpressionText(sortColumnExpression);

            Expression<Func<PagingData, SortingDirection>> sortingDirectionExpression = x => x.SortDirection;
            SortDirectionPropertyName = ExpressionHelper.GetExpressionText(sortingDirectionExpression);
        }

        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public string SortColumn { get; set; }
        public SortingDirection SortDirection { get; set; }

        public PagingData()
        {
            PageNumber = 1;
            PageSize = DefaultPageSize;
        }
    }
}