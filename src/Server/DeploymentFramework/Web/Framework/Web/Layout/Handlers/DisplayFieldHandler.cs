using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ChameleonForms.Component.Config;
using ChameleonForms.FieldGenerators;
using ChameleonForms.FieldGenerators.Handlers;

namespace Baud.Deployment.Web.Framework.Web.Layout.Handlers
{
    public class DisplayFieldHandler<TModel, T> : FieldGeneratorHandler<TModel, T>
    {
        public DisplayFieldHandler(IFieldGenerator<TModel, T> fieldGenerator)
            : base(fieldGenerator)
        {
        }

        public override bool CanHandle()
        {
            return true;
        }

        public override IHtmlString GenerateFieldHtml(IReadonlyFieldConfiguration fieldConfiguration)
        {
            var value = FieldGenerator.GetValue();

            //var valueAsEnum = value as Enum;
            //if (valueAsEnum != null)
            //{
            //    return new HtmlString(valueAsEnum.GetDescription());
            //}

            return value != null ? new HtmlString(value.ToString()) : null;

            ////return FieldGenerator.HtmlHelper.DisplayTextFor(FieldGenerator.FieldProperty);;
        }

        public override ChameleonForms.Enums.FieldDisplayType GetDisplayType(IReadonlyFieldConfiguration fieldConfiguration)
        {
            return ChameleonForms.Enums.FieldDisplayType.SingleLineText;
        }
    }
}