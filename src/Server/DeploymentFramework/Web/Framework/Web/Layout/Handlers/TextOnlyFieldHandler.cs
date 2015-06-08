using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ChameleonForms.FieldGenerators;

namespace Baud.Deployment.Web.Framework.Web.Layout.Handlers
{
    public static class TextOnlyFieldHandler
    {
        public const string TextOnlyControlName = "TextOnly";
    }

    public class TextOnlyFieldHandler<TModel, T> : DisplayFieldHandler<TModel, T>
    {
        public TextOnlyFieldHandler(IFieldGenerator<TModel, T> fieldGenerator)
            : base(fieldGenerator)
        {
        }

        //public override IHtmlString GenerateFieldHtml(ChameleonForms.Component.Config.IReadonlyFieldConfiguration fieldConfiguration)
        //{
        //    var text = base.GenerateFieldHtml(fieldConfiguration);
        //    return Original.OriginalDetailFormTemplateHelpers.FieldContent(text);
        //}
    }
}