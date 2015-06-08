using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using ChameleonForms;
using ChameleonForms.Component;
using ChameleonForms.Component.Config;
using ChameleonForms.FieldGenerators;
using ChameleonForms.FieldGenerators.Handlers;
using ChameleonForms.Templates;

namespace Baud.Deployment.Web.Framework.Web.Layout
{
    public abstract class CustomFieldGeneratorBase<TModel, T> : IFieldGenerator<TModel, T>
    {
        public ModelMetadata Metadata { get; private set; }
        public HtmlHelper<TModel> HtmlHelper { get; private set; }
        public Expression<Func<TModel, T>> FieldProperty { get; private set; }
        public IFormTemplate Template { get; private set; }

        /// <summary>
        /// Constructs the field generator.
        /// </summary>
        /// <param name="htmlHelper">The HTML helper for the current view</param>
        /// <param name="fieldProperty">Expression to identify the property to generate the field for</param>
        /// <param name="template">The template being used to output the form</param>
        public CustomFieldGeneratorBase(HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, T>> fieldProperty, IFormTemplate template)
        {
            HtmlHelper = htmlHelper;
            FieldProperty = fieldProperty;
            Template = template;
            Metadata = ModelMetadata.FromLambdaExpression(FieldProperty, HtmlHelper.ViewData);
        }

        public IHtmlString GetLabelHtml(IReadonlyFieldConfiguration fieldConfiguration)
        {
            fieldConfiguration = fieldConfiguration ?? new ReadonlyFieldConfiguration(new FieldConfiguration());

            string @for;
            if (fieldConfiguration.HtmlAttributes.ContainsKey("id"))
            {
                @for = fieldConfiguration.HtmlAttributes["id"].ToString();
            }
            else
            {
                @for = HtmlHelper.ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldName(
                    ExpressionHelper.GetExpressionText(FieldProperty));
            }

            var labelText = fieldConfiguration.LabelText ?? GetFieldDisplayName().ToHtml();

            if (!fieldConfiguration.HasLabel)
            {
                return labelText;
            }

            var labelAttrs = new HtmlAttributes();
            if (!string.IsNullOrEmpty(fieldConfiguration.LabelClasses))
            {
                labelAttrs.AddClass(fieldConfiguration.LabelClasses);
            }

            return HtmlCreator.BuildLabel(@for, labelText, labelAttrs);
        }

        public string GetFieldDisplayName()
        {
            return Metadata.DisplayName
                ?? Metadata.PropertyName
                ?? ExpressionHelper.GetExpressionText(FieldProperty).Split('.').Last();
        }

        public IHtmlString GetValidationHtml(IReadonlyFieldConfiguration fieldConfiguration)
        {
            return HtmlHelper.ValidationMessageFor(FieldProperty, null, fieldConfiguration.ValidationClasses != null ? new SimpleHtmlAttributes("class", fieldConfiguration.ValidationClasses) : null);
        }

        public IHtmlString GetFieldHtml(IFieldConfiguration fieldConfiguration)
        {
            return GetFieldHtml(PrepareFieldConfiguration(fieldConfiguration, FieldParent.Form));
        }

        public IHtmlString GetLabelHtml(IFieldConfiguration fieldConfiguration)
        {
            return GetLabelHtml(PrepareFieldConfiguration(fieldConfiguration, FieldParent.Form));
        }

        public IHtmlString GetValidationHtml(IFieldConfiguration fieldConfiguration)
        {
            return GetValidationHtml(PrepareFieldConfiguration(fieldConfiguration, FieldParent.Form));
        }

        public IReadonlyFieldConfiguration PrepareFieldConfiguration(IFieldConfiguration fieldConfiguration, FieldParent fieldParent)
        {
            fieldConfiguration = fieldConfiguration ?? new FieldConfiguration();
            if (!string.IsNullOrEmpty(Metadata.EditFormatString) && string.IsNullOrEmpty(fieldConfiguration.FormatString))
            {
                fieldConfiguration.WithFormatString(Metadata.EditFormatString);
            }
            if (!string.IsNullOrEmpty(Metadata.NullDisplayText) && string.IsNullOrEmpty(fieldConfiguration.NoneString))
            {
                fieldConfiguration.WithNoneAs(Metadata.NullDisplayText);
            }
            if (Metadata.IsReadOnly)
            {
                fieldConfiguration.Readonly();
            }

            string customControlName = fieldConfiguration.GetCustomControlName();
            var handler = GetFieldGeneratorHandler(customControlName);
            handler.PrepareFieldConfiguration(fieldConfiguration);
            Template.PrepareFieldConfiguration(this, handler, fieldConfiguration, fieldParent);

            return new ReadonlyFieldConfiguration(fieldConfiguration);
        }

        public IHtmlString GetFieldHtml(IReadonlyFieldConfiguration fieldConfiguration)
        {
            fieldConfiguration = fieldConfiguration ?? new ReadonlyFieldConfiguration(new FieldConfiguration());
            if (fieldConfiguration.FieldHtml != null)
            {
                return fieldConfiguration.FieldHtml;
            }

            string customControlName = fieldConfiguration.GetCustomControlName();
            return GetFieldGeneratorHandler(customControlName).GenerateFieldHtml(fieldConfiguration);
        }

        public T GetValue()
        {
            var model = GetModel();

            return model == null ? default(T) : FieldProperty.Compile().Invoke(model);
        }

        public string GetFieldId()
        {
            return ((MemberExpression)FieldProperty.Body).Member.Name;
        }

        public TModel GetModel()
        {
            return (TModel)HtmlHelper.ViewData.ModelMetadata.Model;
        }

        protected abstract IFieldGeneratorHandler<TModel, T> GetFieldGeneratorHandler(string customControlName);
    }
}