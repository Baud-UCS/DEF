using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc.Html;
using ChameleonForms.Component.Config;
using ChameleonForms.FieldGenerators;
using ChameleonForms.FieldGenerators.Handlers;

namespace Baud.Deployment.Web.Framework.Web.Layout.Handlers
{
    public static class SearchBoxHandler
    {
        public const string SearchBoxControlName = "SearchBox";
    }

    public class SearchBoxHandler<TModel, T> : FieldGeneratorHandler<TModel, T>
    {
        public SearchBoxHandler(IFieldGenerator<TModel, T> fieldGenerator)
            : base(fieldGenerator)
        {
        }

        public override bool CanHandle()
        {
            return true;
        }

        public override IHtmlString GenerateFieldHtml(IReadonlyFieldConfiguration fieldConfiguration)
        {
            var selectedTextProperty = fieldConfiguration.GetBagData<string>("SelectedTextProperty");
            return GenerateFieldHtml(fieldConfiguration, selectedTextProperty);
        }

        protected IHtmlString GenerateFieldHtml(IReadonlyFieldConfiguration fieldConfiguration, string selectedTextProperty)
        {
            return FieldGenerator.HtmlHelper.HiddenFor(FieldGenerator.FieldProperty, fieldConfiguration.HtmlAttributes);

            ////return new HtmlString(string.Concat(
            ////    FieldGenerator.HtmlHelper.Hidden(selectedTextProperty),
            ////    FieldGenerator.HtmlHelper.HiddenFor(FieldGenerator.FieldProperty, fieldConfiguration.HtmlAttributes)));
        }

        public override ChameleonForms.Enums.FieldDisplayType GetDisplayType(IReadonlyFieldConfiguration fieldConfiguration)
        {
            return ChameleonForms.Enums.FieldDisplayType.DropDown;
        }
    }
}