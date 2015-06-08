using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using Baud.Deployment.Web.Framework.Web.Layout.Handlers;
using ChameleonForms.FieldGenerators.Handlers;
using ChameleonForms.Templates;

namespace Baud.Deployment.Web.Framework.Web.Layout
{
    public class ReadOnlyFieldGenerator<TModel, T> : CustomFieldGeneratorBase<TModel, T>
    {
        public ReadOnlyFieldGenerator(HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, T>> fieldProperty, IFormTemplate template)
            : base(htmlHelper, fieldProperty, template)
        {
        }

        protected override IFieldGeneratorHandler<TModel, T> GetFieldGeneratorHandler(string customControlName)
        {
            return new DisplayFieldHandler<TModel, T>(this);
        }
    }
}