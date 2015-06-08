using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq.Expressions;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.WebPages;
using Baud.Deployment.Web.Framework.Security;
using HtmlTags;

namespace Baud.Deployment.Web.Framework.Web
{
    public static class WebExtensions
    {
        public static string LocalResources(this WebViewPage page, string key)
        {
            return page.ViewContext.HttpContext.GetLocalResourceObject(page.VirtualPath, key) as string;
        }

        public static string Action(this UrlHelper url, ActionResult action, string tabName)
        {
            return string.Concat(url.Action(action), '#', tabName);
        }

        public static ActionResult WithCurrentQueryString(this ActionResult result, WebPageRenderingBase page)
        {
            return result.AddRouteValues(page.Request.QueryString);
        }

        #region Security

        public static bool IsActionAllowed(this UrlHelper helper, ActionResult action)
        {
            return IsActionAllowed(helper.RequestContext.HttpContext, action);
        }

        public static MvcHtmlString GuardedActionLink(this HtmlHelper htmlHelper, string linkText, ActionResult result)
        {
            return IsActionAllowed(htmlHelper.ViewContext.HttpContext, result) ? T4Extensions.ActionLink(htmlHelper, linkText, result) : MvcHtmlString.Empty;
        }

        public static MvcHtmlString GuardedActionLink(this HtmlHelper htmlHelper, string linkText, ActionResult result, IDictionary<string, object> htmlAttributes)
        {
            return IsActionAllowed(htmlHelper.ViewContext.HttpContext, result) ? T4Extensions.ActionLink(htmlHelper, linkText, result, htmlAttributes) : MvcHtmlString.Empty;
        }

        public static MvcHtmlString GuardedActionLink(this HtmlHelper htmlHelper, string linkText, ActionResult result, object htmlAttributes)
        {
            return IsActionAllowed(htmlHelper.ViewContext.HttpContext, result) ? T4Extensions.ActionLink(htmlHelper, linkText, result, htmlAttributes) : MvcHtmlString.Empty;
        }

        public static MvcHtmlString PermissionsAwareActionLink(this HtmlHelper htmlHelper, string linkText, ActionResult result)
        {
            return IsActionAllowed(htmlHelper.ViewContext.HttpContext, result) ? T4Extensions.ActionLink(htmlHelper, linkText, result) : MvcHtmlString.Create(linkText);
        }

        public static MvcHtmlString PermissionsAwareActionLink(this HtmlHelper htmlHelper, string linkText, ActionResult result, IDictionary<string, object> htmlAttributes)
        {
            return IsActionAllowed(htmlHelper.ViewContext.HttpContext, result) ? T4Extensions.ActionLink(htmlHelper, linkText, result, htmlAttributes) : MvcHtmlString.Create(linkText);
        }

        public static MvcHtmlString PermissionsAwareActionLink(this HtmlHelper htmlHelper, string linkText, ActionResult result, object htmlAttributes)
        {
            return IsActionAllowed(htmlHelper.ViewContext.HttpContext, result) ? T4Extensions.ActionLink(htmlHelper, linkText, result, htmlAttributes) : MvcHtmlString.Create(linkText);
        }

        private static bool IsActionAllowed(HttpContextBase httpContext, ActionResult action)
        {
            var navigationResult = action as IRequiresPermission;
            if (navigationResult != null)
            {
                return navigationResult.RequiredPermission == null || httpContext.User.HasPermission(navigationResult.RequiredPermission);
            }
            else
            {
                throw new ArgumentException("Action result passed is not a 'RequiredContextRights' enhanced T4MVC result.", "action");
            }
        }

        #endregion Security

        #region Data

        public static string CurrentWithPageNumber(this UrlHelper urlHelper, int pageNumber)
        {
            var url = new UriBuilder(urlHelper.RequestContext.HttpContext.Request.Url);
            var queryValues = HttpUtility.ParseQueryString(url.Query);

            queryValues[PagingData.PageNumberPropertyName] = pageNumber.ToString();

            url.Query = queryValues.ToQueryString();
            return url.Uri.PathAndQuery;
        }

        public static ActionResult AddPageNumber(this ActionResult result, int pageNumber)
        {
            return result.AddRouteValue(PagingData.PageNumberPropertyName, pageNumber);
        }

        public static HtmlTag SortingLink<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression)
        {
            string displayText = ModelMetadata.FromLambdaExpression(expression, html.ViewData).DisplayName ?? expression.GetPropertyName();
            string sortPropertyName = expression.GetPropertyName();

            return html.SortingLink(displayText, sortPropertyName);
        }

        public static MvcHtmlString SortingLink<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression, ActionResult result)
        {
            string displayText = ModelMetadata.FromLambdaExpression(expression, html.ViewData).DisplayName ?? expression.GetPropertyName();
            string sortPropertyName = expression.GetPropertyName();

            return html.ActionLink(displayText, result.AddRouteValue(PagingData.SortColumnPropertyName, sortPropertyName));
        }

        public static HtmlTag SortingLink(this HtmlHelper html, string displayText, string sortPropertyName)
        {
            var url = new UriBuilder(html.ViewContext.HttpContext.Request.Url);
            var queryValues = HttpUtility.ParseQueryString(url.Query);

            string cssClass = "";

            if (queryValues[PagingData.SortColumnPropertyName] == sortPropertyName)
            {
                if (queryValues[PagingData.SortDirectionPropertyName] != SortingDirection.Descending.ToString())
                {
                    queryValues[PagingData.SortDirectionPropertyName] = SortingDirection.Descending.ToString();
                    cssClass = "descending";
                }
                else
                {
                    queryValues.Remove(PagingData.SortDirectionPropertyName);
                    cssClass = "ascending";
                }
            }
            else
            {
                queryValues[PagingData.SortColumnPropertyName] = sortPropertyName;
                queryValues.Remove(PagingData.SortDirectionPropertyName);
            }

            url.Query = queryValues.ToQueryString();

            return new HtmlTag("a").Attr("href", url.Uri.PathAndQuery).Text(displayText).AddClass(cssClass);
        }

        #endregion Data

        public static string ToQueryString(this NameValueCollection collection)
        {
            // This is based off the NameValueCollection.ToString() implementation with bugfix for UrlEncoding
            int count = collection.Count;
            if (count == 0)
            {
                return string.Empty;
            }

            StringBuilder stringBuilder = new StringBuilder();

            for (int i = 0; i < count; i++)
            {
                string text = collection.GetKey(i);
                text = HttpUtility.UrlEncode(text);
                string value = (text != null) ? (text + "=") : string.Empty;
                string[] values = collection.GetValues(i);
                if (stringBuilder.Length > 0)
                {
                    stringBuilder.Append('&');
                }
                if (values == null || values.Length == 0)
                {
                    stringBuilder.Append(value);
                }
                else
                {
                    if (values.Length == 1)
                    {
                        stringBuilder.Append(value);
                        string text2 = values[0];
                        text2 = HttpUtility.UrlEncode(text2);
                        stringBuilder.Append(text2);
                    }
                    else
                    {
                        for (int j = 0; j < values.Length; j++)
                        {
                            if (j > 0)
                            {
                                stringBuilder.Append('&');
                            }
                            stringBuilder.Append(value);
                            string text2 = values[j];
                            text2 = HttpUtility.UrlEncode(text2);
                            stringBuilder.Append(text2);
                        }
                    }
                }
            }

            return stringBuilder.ToString();
        }
    }
}