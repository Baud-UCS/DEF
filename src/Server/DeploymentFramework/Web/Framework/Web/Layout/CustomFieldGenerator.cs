using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using Baud.Deployment.Web.Framework.Web.Layout.Handlers;
using ChameleonForms;
using ChameleonForms.Component;
using ChameleonForms.Component.Config;
using ChameleonForms.FieldGenerators;
using ChameleonForms.FieldGenerators.Handlers;
using ChameleonForms.Templates;

namespace Baud.Deployment.Web.Framework.Web.Layout
{
    public static class CustomFieldGenerator
    {
        public static void SetCustomControlName(this IFieldConfiguration fieldConfiguration, string customControlName)
        {
            fieldConfiguration.Bag.CustomControlName = customControlName;
        }

        public static string GetCustomControlName(this IFieldConfiguration fieldConfiguration)
        {
            return fieldConfiguration.GetBagData<string>("CustomControlName");
        }

        public static string GetCustomControlName(this IReadonlyFieldConfiguration fieldConfiguration)
        {
            return fieldConfiguration.GetBagData<string>("CustomControlName");
        }
    }

    public class CustomFieldGenerator<TModel, T> : CustomFieldGeneratorBase<TModel, T>
    {
        public CustomFieldGenerator(HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, T>> fieldProperty, IFormTemplate template)
            : base(htmlHelper, fieldProperty, template)
        {
        }

        protected override IFieldGeneratorHandler<TModel, T> GetFieldGeneratorHandler(string customControlName)
        {
            return GetHandlers(this, customControlName).First(h => h.CanHandle());
        }

        private static IEnumerable<FieldGeneratorHandler<TModel, T>> GetHandlers(IFieldGenerator<TModel, T> g, string customControlName)
        {
            if (customControlName == TextOnlyFieldHandler.TextOnlyControlName)
            {
                yield return new TextOnlyFieldHandler<TModel, T>(g);
            }
            if (customControlName == SelectListHandler.SelectListControlName)
            {
                yield return new SelectListHandler<TModel, T>(g);
            }
            if (customControlName == SearchBoxHandler.SearchBoxControlName)
            {
                yield return new SearchBoxHandler<TModel, T>(g);
            }

            yield return new EnumListHandler<TModel, T>(g);
            yield return new PasswordHandler<TModel, T>(g);
            yield return new TextAreaHandler<TModel, T>(g);
            yield return new BooleanHandler<TModel, T>(g);
            yield return new FileHandler<TModel, T>(g);
            yield return new ListHandler<TModel, T>(g);
            yield return new DateTimeHandler<TModel, T>(g);
            yield return new DefaultHandler<TModel, T>(g);
        }
    }
}