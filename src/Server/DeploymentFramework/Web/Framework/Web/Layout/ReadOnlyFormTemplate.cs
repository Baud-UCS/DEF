using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ChameleonForms.Templates.TwitterBootstrap3;

namespace Baud.Deployment.Web.Framework.Web.Layout
{
    public class ReadOnlyFormTemplate : TwitterBootstrapFormTemplate
    {
        public override IHtmlString BeginField(IHtmlString labelHtml, IHtmlString elementHtml, IHtmlString validationHtml, System.Web.Mvc.ModelMetadata fieldMetadata, ChameleonForms.Component.Config.IReadonlyFieldConfiguration fieldConfiguration, bool isValid)
        {
            fieldMetadata.IsRequired = false;

            return base.BeginField(labelHtml, elementHtml, validationHtml, fieldMetadata, fieldConfiguration, isValid);
        }

        public override IHtmlString Field(IHtmlString labelHtml, IHtmlString elementHtml, IHtmlString validationHtml, System.Web.Mvc.ModelMetadata fieldMetadata, ChameleonForms.Component.Config.IReadonlyFieldConfiguration fieldConfiguration, bool isValid)
        {
            fieldMetadata.IsRequired = false;

            return base.Field(labelHtml, elementHtml, validationHtml, fieldMetadata, fieldConfiguration, isValid);
        }
    }
}