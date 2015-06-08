using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ChameleonForms;
using ChameleonForms.Enums;
using ChameleonForms.Templates;

namespace Baud.Deployment.Web.Framework.Web.Layout
{
    public class ReadOnlyForm<TModel, TTemplate> : Form<TModel, TTemplate> where TTemplate : IFormTemplate
    {
        public ReadOnlyForm(HtmlHelper<TModel> helper, TTemplate template, string action, FormMethod method, HtmlAttributes htmlAttributes, EncType? enctype)
            : base(helper, template, action, method, htmlAttributes, enctype)
        {
        }

        public override ChameleonForms.FieldGenerators.IFieldGenerator GetFieldGenerator<T>(System.Linq.Expressions.Expression<Func<TModel, T>> property)
        {
            return new ReadOnlyFieldGenerator<TModel, T>(HtmlHelper, property, Template);
        }
    }
}